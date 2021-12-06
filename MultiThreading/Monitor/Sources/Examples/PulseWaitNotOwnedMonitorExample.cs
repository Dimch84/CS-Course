using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class PulseWaitNotOwnedMonitorExample : IDisposable
    {
        private readonly Object _object = new Object();

        public PulseWaitNotOwnedMonitorExample()
        {
        }

        public void Dispose()
        {
        }

        void Thread1()
        {
            try
            {
                CustomLogger.LogMessage("Thread1: '{0}' start", Thread.CurrentThread.ManagedThreadId);

                Thread.Sleep(CustomTimeouts.ShortTimePeriod);

                Monitor.Pulse(_object);

                CustomLogger.LogMessage("Thread1: '{0}' end", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread1: '{0}'", exc.Message);
            }
        }

        void Thread2()
        {
            try
            {
                CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

                Monitor.Wait(_object);

                CustomLogger.LogMessage("Thread2: '{0}' end", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("PulseWaitNotOwnedExample");
            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
