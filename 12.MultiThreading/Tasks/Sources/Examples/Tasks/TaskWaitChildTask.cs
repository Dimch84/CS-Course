using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskWaitChildTask
    {
        private void Task1()
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            Task task1 = Task.Run(() => Task2());

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

            Task task4 = Task.Factory.StartNew(() => Task4(), CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Current);

            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task4()
        {
            CustomLogger.LogMessage("Task4, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("Task4, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task5()
        {
            CustomLogger.LogMessage("Task5, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            Task task4 = Task.Factory.StartNew(() => Task6(), CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Current);

            CustomLogger.LogMessage("Task5, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Task6()
        {
            CustomLogger.LogMessage("Task6, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            throw new Exception("From Task6");            
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task wait child example");

            try
            {
                CustomLogger.LogMessage("Task1 starting. Task2 without AttachedToParent");
                Task task1 = Task.Factory.StartNew(() => Task1());
                task1.Wait();
                CustomLogger.LogMessage("Task1.Wait finished");
                Console.ReadKey();

                CustomLogger.LogMessage("Task3 starting. Task4 with AttachedToParent");
                Task task3 = Task.Factory.StartNew(() => Task3());
                task3.Wait();
                CustomLogger.LogMessage("Task3.Wait finished");
                Console.ReadKey();

                CustomLogger.LogMessage("Task3 starting with DenyChildAttach. Task4 with AttachedToParent");
                Task task31 = Task.Factory.StartNew(() => Task3(), CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
                task31.Wait();
                CustomLogger.LogMessage("Task3.Wait finished");
                Console.ReadKey();

                CustomLogger.LogMessage("Task5 starting. Task5 with AttachedToParent");
                Task task5 = Task.Factory.StartNew(() => Task5());
                task5.Wait();
                CustomLogger.LogMessage("Task5.Wait finished");
                Console.ReadKey();
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();

        }
    }
}
