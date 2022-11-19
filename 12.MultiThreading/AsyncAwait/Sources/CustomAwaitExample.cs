using Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    internal sealed class CustomAwaitExample
    {
        private static async Task AsyncSetEvent(AutoResetEvent waitHandle)
        {
            CustomLogger.LogMessage("AsyncSetEvent started");
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

            waitHandle.Set();
            CustomLogger.LogMessage("event is set.");
        }

        private static async Task AsyncAwaitEvent(AutoResetEvent waitHandle)
        {
            CustomLogger.LogMessage("AsyncAwaitEvent started");

            await waitHandle;

            CustomLogger.LogMessage("AsyncAwaitEvent ended");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Basic example");
            CustomLogger.LogMessage("Run started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            using (AutoResetEvent autoResetEvent = new AutoResetEvent(false))
            {
                Task setEvent = AsyncSetEvent(autoResetEvent);
                Task awaitEvent = AsyncAwaitEvent(autoResetEvent);
                CustomLogger.LogMessage("Before waitAll");
                Task.WaitAll(setEvent, awaitEvent);
                CustomLogger.LogMessage("After waitAll");
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
