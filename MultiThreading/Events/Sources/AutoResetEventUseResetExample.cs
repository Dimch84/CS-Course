using System;
using System.Threading;
using Common;

namespace Events
{
    // However it's possible that a given event is no longer valid and no thread has waited on an AutoResetEvent since it was originally set.
    // In that scenario the Reset method becomes useful
    internal sealed class AutoResetEventUseResetExample : IDisposable
    {
        private readonly AutoResetEvent _event;

        public AutoResetEventUseResetExample()
        {
            _event = new AutoResetEvent(initialState: false);
        }

        public void Dispose()
        {
            _event.Dispose();
        }

        void Thread1()
        {
            CustomLogger.LogMessage("Thread1: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _event.WaitOne();

            CustomLogger.LogMessage("Thread1: '{0}' event wait", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("AutoResetEvent User reset Example");
            
            CustomLogger.LogMessage("Reset not signaled event");
            _event.Reset();

            _event.Set();
            _event.Reset();
            CustomLogger.LogMessage("Main: event set, event reset");

            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            _event.Set();
            CustomLogger.LogMessage("Main: event set");

            thread1.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
