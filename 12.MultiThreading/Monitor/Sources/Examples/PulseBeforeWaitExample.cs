using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class PulseBeforeWaitExample : IDisposable
    {
        private readonly Object _object = new Object();

        public PulseBeforeWaitExample()
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

                lock (_object)
                {
                    CustomLogger.LogMessage("Thread1: '{0}' monitor wait", Thread.CurrentThread.ManagedThreadId);
                    Monitor.Wait(_object);
                    CustomLogger.LogMessage("Thread1: '{0}' wait succeeded", Thread.CurrentThread.ManagedThreadId);
                }

                CustomLogger.LogMessage("Thread1: '{0}' end", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        void Thread2()
        {
            try
            {
                CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);
                lock (_object)
                {
                    CustomLogger.LogMessage("Thread2: '{0}' monitor pulse 1", Thread.CurrentThread.ManagedThreadId);
                    Monitor.Pulse(_object);
                }

                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                lock (_object)
                {
                    CustomLogger.LogMessage("Thread2: '{0}' monitor pulse 2", Thread.CurrentThread.ManagedThreadId);
                    Monitor.Pulse(_object);
                }

                CustomLogger.LogMessage("Thread2: '{0}' end", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("PulseBeforeWaitMonitorExample");
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
