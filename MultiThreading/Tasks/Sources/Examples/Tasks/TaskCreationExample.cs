using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskCreationExample
    {
        private void Task1()
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            CustomLogger.LogMessage("TaskScheduler.Current: '{0}'", TaskScheduler.Current);
            CustomLogger.LogMessage("TaskScheduler.Current.MaximumConcurrencyLevel: '{0}'", TaskScheduler.Current.MaximumConcurrencyLevel);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private String Task2()
        {
            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            return "test";
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task creation example");

            Task task1 = Task.Run(() => Task1());
            task1.Wait();

            //Task.Run(someAction);
            //is simplified equivalent of method:

            //Task.Factory.StartNew(someAction,
            //         CancellationToken.None,
            //         TaskCreationOptions.DenyChildAttach,
            //         TaskScheduler.Default);

            Task task2 = new Task(() => Task1());
            task2.Start();
            task2.Wait();

            Task task3 = Task.Factory.StartNew(() => Task1(), 
                CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Current);
            task3.Wait();

            Task<String> task4 = Task.Run(() => Task2());
            String result = task4.Result;

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
