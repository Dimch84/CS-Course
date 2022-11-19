using System;
using System.Threading;
using Common;

namespace Lectures
{
    class SemaphoreOrderExample : IDisposable
    {
        private readonly Semaphore _semaphore;

        public SemaphoreOrderExample()
        {
            _semaphore = new Semaphore(0, 1); 
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }

        private void ThreadRoutine(Int32 threadNumber)
        {
            CustomLogger.LogMessage("Thread.LocalNumber = {0}: start", threadNumber);

            _semaphore.WaitOne();

            CustomLogger.LogMessage("ThreadLocalNumber = {0}:  semaphore acquired", threadNumber);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            _semaphore.Release();
        }

        public void Run()
        {
            CustomLogger.LogStartExample("SemaphoreOrderExample");

            try
            {
                const Int32 TestThreadNumber = 7;
                Thread[] threads = new Thread[TestThreadNumber];

                for (Int32 i = 0; i < TestThreadNumber; ++i)
                {
                    Int32 copy = i;
                    threads[i] = new Thread(() => ThreadRoutine(copy));
                    threads[i].Start();

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }

                _semaphore.Release();

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
