using System;
using System.Threading;
using Common;

namespace Events
{
    internal sealed class AutoResetEventOrderExample : IDisposable
    {
        private readonly AutoResetEvent _event;

        public AutoResetEventOrderExample()
        {
            _event = new AutoResetEvent(false);
        }

        public void Dispose()
        {
            _event.Dispose();
        }

        private void ThreadRoutine(Int32 threadNumber)
        {
            CustomLogger.LogMessage("Thread#{0}: '{1}' start", threadNumber, Thread.CurrentThread.ManagedThreadId);

            _event.WaitOne();

            CustomLogger.LogMessage("Thread#{0}: '{1}' event acquired", threadNumber, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            _event.Set();
        }

        public void Run()
        {
            CustomLogger.LogStartExample("AutoResetEventOrderExample");

            try
            {
                const Int32 TestThreadNumber = 7;
                Thread[] threads = new Thread[TestThreadNumber];

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    Int32 copy = i;
                    threads[i] = new Thread(() => ThreadRoutine(copy));
                    threads[i].Start();

                    Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                }

                _event.Set();

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    threads[i].Join();
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
            }
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
