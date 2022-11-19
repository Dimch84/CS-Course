using System;
using System.Threading;
using Common;

namespace Events
{
    internal sealed class NamedDiffEventsExample
    {
        private const String _eventName = "EventName";

        private void Thread1()
        {
            CustomLogger.LogMessage("Thread1: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            try
            {
                using (EventWaitHandle autoEvent = new EventWaitHandle(false, EventResetMode.AutoReset, _eventName, out var createdNew))
                {

                    CustomLogger.LogMessage("Thread1: '{0}', event created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                    Thread.Sleep(CustomTimeouts.MiddleTimePeriod);

                    autoEvent.WaitOne();
                    CustomLogger.LogMessage("Thread1: '{0}', event waited1", Thread.CurrentThread.ManagedThreadId);

                    // thread is blocked here
                    // as AutoResetEvent is not recursive
                    autoEvent.WaitOne();
                    CustomLogger.LogMessage("Thread1: '{0}', event waited2", Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }

            CustomLogger.LogMessage("Thread1: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        private void Thread2()
        {
            CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            try
            {
                using (EventWaitHandle manualEvent = new EventWaitHandle(false, EventResetMode.ManualReset, _eventName, out var createdNew))
                {
                    CustomLogger.LogMessage("Thread2: '{0}', event created '{1}'", Thread.CurrentThread.ManagedThreadId, createdNew);

                    // this is actually auto reset event
                    manualEvent.Set();
                    CustomLogger.LogMessage("Thread2: '{0}', event set", Thread.CurrentThread.ManagedThreadId);                   
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }

            CustomLogger.LogMessage("Thread2: '{0}' end", Thread.CurrentThread.ManagedThreadId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("NamedDiffEventExample");

            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
