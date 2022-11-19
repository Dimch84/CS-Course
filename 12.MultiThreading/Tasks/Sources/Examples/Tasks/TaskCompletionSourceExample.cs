using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskCompletionSourceExample
    {
        private void Thread1(TaskCompletionSource<int> tcs)
        {
            CustomLogger.LogMessage("Thread1 started: '{0}'", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            tcs.SetResult(42);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            CustomLogger.LogMessage("Thread1 ended: '{0}'", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task completion source example");

            TaskCompletionSource<int> source = new TaskCompletionSource<int>();

            Thread thread1 = new Thread(() => Thread1(source));
            thread1.Start();

            Task<int> task = source.Task; //slave task
            // task.Start(); // this call will raise exception
            CustomLogger.LogMessage("Task result: '{0}'", task.Result);

            thread1.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
