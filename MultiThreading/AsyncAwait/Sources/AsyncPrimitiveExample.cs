using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace AsyncAwait
{
    internal sealed class AsyncPrimitiveExample
    {
        private async Task WaitAsyncManualResetEvent(AsyncManualResetEvent asyncManualResetEvent)
        {
            CustomLogger.LogMessage("Wait Async started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await asyncManualResetEvent.WaitAsync();

            CustomLogger.LogMessage("Wait Async ended, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        private async Task SetAsyncManualResetEvent(AsyncManualResetEvent asyncManualResetEvent)
        {
            CustomLogger.LogMessage("Set async started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await Task.Delay(TimeSpan.FromSeconds(5));

            CustomLogger.LogMessage("Set async event, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            asyncManualResetEvent.Set();
        }

        private async Task WaitAsyncAutoResetEvent(AsyncAutoResetEvent asyncAutoResetEvent)
        {
            CustomLogger.LogMessage("Wait Async started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await asyncAutoResetEvent.WaitAsync();

            CustomLogger.LogMessage("Wait Async ended, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        private async Task SetAsyncAutoResetEvent(AsyncAutoResetEvent asyncAutoResetEvent)
        {
            CustomLogger.LogMessage("Set async started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await Task.Delay(TimeSpan.FromSeconds(5));

            CustomLogger.LogMessage("Set async event, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            asyncAutoResetEvent.Set();
        }

        private async Task WaitAsyncSemaphore(AsyncSemaphore asyncSemaphore)
        {
            CustomLogger.LogMessage("Wait Async started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await asyncSemaphore.WaitAsync();
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));

                CustomLogger.LogMessage("Semaphore awaited, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            }
            finally
            {
                asyncSemaphore.Release();
            }
            CustomLogger.LogMessage("Wait Async ended, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Coordination primitive example");

            CustomLogger.LogMessage("Manual Reset Event, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            AsyncManualResetEvent asyncManualResetEvent = new AsyncManualResetEvent();
            Task.Run(() => SetAsyncManualResetEvent(asyncManualResetEvent));
            WaitAsyncManualResetEvent(asyncManualResetEvent).GetAwaiter().GetResult();            
            Console.ReadKey();

            CustomLogger.LogMessage("Auto Reset Event, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            AsyncAutoResetEvent asyncAutoResetEvent = new AsyncAutoResetEvent();
            Task.Run(() => SetAsyncAutoResetEvent(asyncAutoResetEvent));
            WaitAsyncAutoResetEvent(asyncAutoResetEvent).GetAwaiter().GetResult();
            Console.ReadKey();

            CustomLogger.LogMessage("Semaphore, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            AsyncSemaphore asyncSemaphore = new AsyncSemaphore(1);
            Task task1 = Task.Run(() => WaitAsyncSemaphore(asyncSemaphore));
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Task task2 = Task.Run(() => WaitAsyncSemaphore(asyncSemaphore));
            Task.WaitAll(task1, task2);
            Console.ReadKey();

            CustomLogger.LogMessage("Run ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);           

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
