using System;
using System.Threading;
using Common;

namespace Events
{
    internal sealed class ManualResetEventExample : IDisposable
    {
        private readonly ManualResetEvent _event;

        public ManualResetEventExample()
        {
            _event = new ManualResetEvent(initialState: false);
        }

        public void Dispose()
        {
            _event.Dispose();
        }       

        void Thread1()
        {
            CustomLogger.LogMessage("Thread1: start");

            _event.WaitOne();

            CustomLogger.LogMessage("Thread1: event wait");
        }

        void Thread2()
        {
            CustomLogger.LogMessage("Thread2: start");

            _event.WaitOne();

            CustomLogger.LogMessage("Thread2: event wait");
        }

        void Thread3()
        {
            CustomLogger.LogMessage("Thread3: start");

            _event.WaitOne();

            CustomLogger.LogMessage("Thread3: event wait");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("ManualResetEventExample");           

            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            Thread thread3 = new Thread(Thread3);
            thread3.Start();

            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
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
