using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class ThreadPoolBasicExample
    {
        void Func1(Object obj)
        {
            ManualResetEvent endEvent = (ManualResetEvent)obj;
            CustomLogger.LogMessage("Func1 started '{0}'", Thread.CurrentThread.ManagedThreadId);
            CustomLogger.LogMessage("IsThreadPoolThread '{0}'", Thread.CurrentThread.IsThreadPoolThread);
            CustomLogger.LogMessage("IsBackground '{0}'", Thread.CurrentThread.IsBackground);
            CustomLogger.LogMessage("Name '{0}'", Thread.CurrentThread.Name);

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Func1 ended'{0}'", Thread.CurrentThread.ManagedThreadId);
            endEvent.Set();
        }

        void Func2(Object obj)
        {
            ManualResetEvent endEvent = (ManualResetEvent)obj;
            CustomLogger.LogMessage("Func2 started '{0}'", Thread.CurrentThread.ManagedThreadId);
            CustomLogger.LogMessage("IsThreadPoolThread '{0}'", Thread.CurrentThread.IsThreadPoolThread);
            CustomLogger.LogMessage("IsBackground '{0}'", Thread.CurrentThread.IsBackground);
            CustomLogger.LogMessage("Name '{0}'", Thread.CurrentThread.Name);

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

            CustomLogger.LogMessage("Func2 ended '{0}'", Thread.CurrentThread.ManagedThreadId);
            endEvent.Set();
        }

        public void Run()
        {
            CustomLogger.LogStartExample("thread pool basic example");

            using (ManualResetEvent func1Event = new ManualResetEvent(false))
            using (ManualResetEvent func2Event = new ManualResetEvent(false))
            {
                ThreadPool.QueueUserWorkItem(Func1, func1Event);
                ThreadPool.QueueUserWorkItem(Func2, func2Event);

                WaitHandle.WaitAll(new WaitHandle[] { func1Event, func2Event });
            }
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }

    }
}
