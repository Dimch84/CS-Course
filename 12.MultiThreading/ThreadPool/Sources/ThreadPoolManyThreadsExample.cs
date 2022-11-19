using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class ThreadPoolManyThreadsExample
    {
        private Int32 funcCount;

        private void Func(Object obj)
        {
            CustomLogger.LogMessage("Func '{0}' started '{1}'", obj, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("Func '{0}' ended '{1}'", obj, Thread.CurrentThread.ManagedThreadId);

            Interlocked.Increment(ref funcCount);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("thread pool many threads example");

            Int32 workerThreads;
            Int32 completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            CustomLogger.LogMessage("Max Worker thread '{0}', max completion port thread '{1}'", workerThreads, completionPortThreads);

            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            CustomLogger.LogMessage("Min Worker thread '{0}', min completion port thread '{1}'", workerThreads, completionPortThreads);

            const Int32 threadCount = 100;
            for (int i = 0; i < threadCount; ++i)
            {
                ThreadPool.QueueUserWorkItem(Func, i);
            }

            while (Interlocked.CompareExchange(ref funcCount, 0, 0) != threadCount)
            {
                ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
                CustomLogger.LogMessage("Available Worker thread '{0}', completion port thread '{1}'", workerThreads, completionPortThreads);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }

    }
}
