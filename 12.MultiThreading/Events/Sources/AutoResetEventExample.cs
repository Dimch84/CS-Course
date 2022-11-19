using System;
using System.Threading;
using Common;

namespace Events
{
    internal sealed class AutoResetEventExample : IDisposable
    {
        private readonly AutoResetEvent _event;

        public AutoResetEventExample()
        {
            _event = new AutoResetEvent(initialState: false);
        }

        public void Dispose()
        {
            _event.Dispose();
        }       

        void Thread1()
        {
            CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _event.WaitOne();

            CustomLogger.LogMessage("Thread2: '{0}' event wait ended", Thread.CurrentThread.ManagedThreadId);
        }

        void Thread2()
        {
            CustomLogger.LogMessage("Thread3: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _event.WaitOne();

            CustomLogger.LogMessage("Thread3: '{0}' event wait ended", Thread.CurrentThread.ManagedThreadId);
        }

        void Thread3()
        {
            CustomLogger.LogMessage("Thread4: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _event.WaitOne();

            CustomLogger.LogMessage("Thread4: '{0}' event wait ended", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("AutoResetEventExample");            

            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            Thread thread3 = new Thread(Thread3);
            thread3.Start();

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);            
            _event.Set();
            CustomLogger.LogMessage("MainThread: event set");

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            _event.Set();
            CustomLogger.LogMessage("MainThread: event set");

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            _event.Set();
            CustomLogger.LogMessage("MainThread: event set");
         
            thread1.Join();
            thread2.Join();
            thread3.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
