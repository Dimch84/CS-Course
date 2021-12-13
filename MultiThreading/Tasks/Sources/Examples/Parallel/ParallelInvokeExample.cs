using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class ParallelInvokeExample
    {
        private void Action1()
        {
            CustomLogger.LogMessage("Action1 started, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            CustomLogger.LogMessage("Action1 ended, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Action2()
        {
            CustomLogger.LogMessage("Action2 started, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            CustomLogger.LogMessage("Action2 ended, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Action3()
        {
            CustomLogger.LogMessage("Action3 started, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Action3 ended, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
           CustomLogger.LogStartExample("Parallel Invoke example");                       

            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                ParallelOptions parallelOptions = new ParallelOptions
                {
                    CancellationToken = cancellationTokenSource.Token,
                    MaxDegreeOfParallelism = Environment.ProcessorCount,
                    TaskScheduler = TaskScheduler.Default
                };

                CustomLogger.LogMessage("Parallel.Invoke begin");
                Parallel.Invoke(parallelOptions, Action1, Action2, Action3);
                CustomLogger.LogMessage("Parallel.Invoke ended");
            }          
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }

    }
}
