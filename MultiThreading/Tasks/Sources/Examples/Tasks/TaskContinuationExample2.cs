using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskContinuationExample2
    {
        private void Task1()
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task2()
        {
            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task3()
        {
            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task continuation example2");

            using (CancellationTokenSource cancellationSource1 = new CancellationTokenSource())
            using (CancellationTokenSource cancellationSource2 = new CancellationTokenSource())
            using (CancellationTokenSource cancellationSource3 = new CancellationTokenSource())
            {
                CustomLogger.LogMessage("Execute all tasks");
                Task task = Task.Factory.StartNew(() => Task1(), cancellationSource1.Token)
                    .ContinueWith(t => Task2(), cancellationSource2.Token)
                    .ContinueWith(t => Task3(), cancellationSource3.Token);

                task.Wait();
                Console.ReadKey();

                cancellationSource2.Cancel();

                CustomLogger.LogMessage("Task2 is cancelled. Basic ContinueWith");
                Task task2 = Task.Factory.StartNew(() => Task1(), cancellationSource1.Token)
                    .ContinueWith(t => Task2(), cancellationSource2.Token)
                    .ContinueWith(t => Task3(), cancellationSource3.Token);

                task2.Wait();
                Console.ReadKey();

                CustomLogger.LogMessage("Task2 is cancelled. LazyCancellation");
                Task task3 = Task.Factory.StartNew(() => Task1(), cancellationSource1.Token)
                    .ContinueWith(t => Task2(), cancellationSource2.Token, TaskContinuationOptions.LazyCancellation, TaskScheduler.Current)
                    .ContinueWith(t => Task3(), cancellationSource3.Token, TaskContinuationOptions.LazyCancellation, TaskScheduler.Current);

                task3.Wait();
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();

        }
    }
}
