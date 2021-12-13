using Common;
using System;
using System.Threading;

namespace Lectures
{
    internal sealed class ThreadPoolRegisterWaitExample
    {
        private void WaitCallback(object state, bool timedOut)
        {            
            CustomLogger.LogMessage("Action started,  ThreadId '{0}', IsThreadPoolThread '{1}', timedOut: '{2}'", 
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread, timedOut);            
            
            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Action ended '{0}'", Thread.CurrentThread.ManagedThreadId);            
        }

        public void Run()
        {
            CustomLogger.LogStartExample("thread pool: register wait example");

            using (AutoResetEvent autoEvent = new AutoResetEvent(false))            
            {
                CustomLogger.LogMessage("Register wait handle");
                RegisteredWaitHandle handle = ThreadPool.RegisterWaitForSingleObject(
                    autoEvent, WaitCallback, null, 
                    TimeSpan.FromSeconds(4), executeOnlyOnce: false);

                Thread.Sleep(TimeSpan.FromSeconds(1));
                autoEvent.Set();

                Thread.Sleep(TimeSpan.FromSeconds(10));
                CustomLogger.LogMessage("Unregister wait handle");
                handle.Unregister(autoEvent);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
