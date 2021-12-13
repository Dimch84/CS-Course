using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskWhenAllExample
    {
        private void Task1(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task2(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task3(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task when all example");

            using (CancellationTokenSource cancellationSource = new CancellationTokenSource())
            {
                Task task1 = Task.Run(() => Task1(cancellationSource.Token));
                Task task2 = Task.Run(() => Task2(cancellationSource.Token));
                Task task3 = Task.Run(() => Task3(cancellationSource.Token));

                Task continueWhenAll = Task.Factory.ContinueWhenAll(
                    new Task[] { task1, task2, task3 }, tasks =>
                    { 
                        CustomLogger.LogMessage("ContinuaWhenAll, TaskId '{0}'", Task.CurrentId); 
                    });
                continueWhenAll.Wait();
                CustomLogger.LogMessage("\n");
                Console.ReadKey();


                Task task11 = Task.Run(() => Task1(cancellationSource.Token));
                Task task21 = Task.Run(() => Task2(cancellationSource.Token));
                Task task31 = Task.Run(() => Task3(cancellationSource.Token));

                Task continueWhenAny = Task.Factory.ContinueWhenAny(
                    new Task[] { task11, task21, task31 }, tasks =>
                { 
                    CustomLogger.LogMessage("ContinuaWhenAny, TaskId '{0}'", Task.CurrentId); 
                });
                continueWhenAny.Wait();
                Console.ReadKey();

                CustomLogger.LogMessage("\n");

                Task task12 = Task.Run(() => Task1(cancellationSource.Token));
                Task task22 = Task.Run(() => Task2(cancellationSource.Token));
                Task task32 = Task.Run(() => Task3(cancellationSource.Token));

                Task taskWhenAll = Task.WhenAll(task12, task22, task32);
                taskWhenAll.Wait();
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();

        }
    }
}
