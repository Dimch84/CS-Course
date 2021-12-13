using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskCancellationExample
    {
        private void Task1(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            while (true)
            {
                // Poll on this property if you have to do
                // other cleanup before throwing.
                if (ct.IsCancellationRequested)
                {
                    // Clean up here, then...
                    ct.ThrowIfCancellationRequested();
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));

            }
        }

        private void Task2(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            while (true)
            {
                // Poll on this property if you have to do
                // other cleanup before throwing.
                if (ct.IsCancellationRequested)
                {
                    // Clean up here, then...
                    ct.ThrowIfCancellationRequested();
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));

            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task cancellation example");

            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {              
                Task task1 = Task.Factory.StartNew(() => Task1(tokenSource.Token), tokenSource.Token);
                Task task2 = Task.Factory.StartNew(() => Task2(tokenSource.Token), tokenSource.Token);

                tokenSource.CancelAfter(TimeSpan.FromSeconds(4));
                CustomLogger.LogMessage("TokenSource cancel scheduled");

                try
                {
                    Task.WaitAll(new Task[]{task1, task2});
                }
                catch (AggregateException e)
                {
                    CustomLogger.LogMessage(e.Message);
                    foreach (Exception innerExc in e.InnerExceptions)
                        CustomLogger.LogMessage("    Inner: '{0}'",innerExc.Message);
                }
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
