using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class MonitorOrderExample : IDisposable
    {
        private readonly Object _object = new Object();

        public MonitorOrderExample()
        {
        }

        public void Dispose()
        {

        }

        private void ThreadRoutine(Int32 threadNumber)
        {
            CustomLogger.LogMessage("Thread#{0}: '{1}' start", threadNumber, Thread.CurrentThread.ManagedThreadId);

            lock (_object)
            {
                CustomLogger.LogMessage("Thread#{0}: '{1}' monitor acquired", threadNumber, Thread.CurrentThread.ManagedThreadId);
                //Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("MonitorOrderExample");

            Monitor.Enter(_object);

            try
            {
                const Int32 TestThreadNumber = 20;
                Thread[] threads = new Thread[TestThreadNumber];

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    Int32 copy = i;
                    threads[i] = new Thread(() => ThreadRoutine(copy));
                    threads[i].Start();

                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }

                Monitor.Exit(_object);

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
