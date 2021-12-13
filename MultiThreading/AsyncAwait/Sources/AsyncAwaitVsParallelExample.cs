using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace AsyncAwait
{
    internal sealed class AsyncAwaitVsParallelExample
    {
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(2);
        
        private async Task AsyncWork()
        {
            CustomLogger.LogMessage("Async started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(_timeout);            
            await Task.Delay(_timeout);

            CustomLogger.LogMessage("Async ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private Task ParallelWork()
        {
            CustomLogger.LogMessage("Parallel started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            Task task1 = Task.Run(() => Task.Delay(_timeout));
            Task task2 = Task.Run(() => Task.Delay(_timeout));
            Task waitAll = Task.WhenAll(task1, task2);

            CustomLogger.LogMessage("Parallel ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            return waitAll;
        }

        public void Run()
        {
            CustomLogger.LogStartExample("AsyncAwait vs parallel  example");
            CustomLogger.LogMessage("Run started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
           
            Stopwatch stopwatchAsync = new Stopwatch();
            stopwatchAsync.Start();
            AsyncWork().GetAwaiter().GetResult();
            stopwatchAsync.Stop();
            CustomLogger.LogMessage("AsyncWork takes '{0}'", stopwatchAsync.Elapsed);


            Stopwatch stopwatchParallel = new Stopwatch();
            stopwatchParallel.Start();
            ParallelWork().Wait();
            stopwatchParallel.Stop();
            CustomLogger.LogMessage("ParallelWork takes '{0}'", stopwatchParallel.Elapsed);                    
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
