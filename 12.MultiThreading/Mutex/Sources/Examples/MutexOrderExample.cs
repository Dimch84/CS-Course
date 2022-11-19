using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class MutexOrderExample : IDisposable
    {
        private readonly Mutex _mutex;

        public MutexOrderExample()
        {
            _mutex = new Mutex(false);
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }

        private void ThreadRoutine(Int32 threadNumber)
        {
            CustomLogger.LogMessage("Thread.LocalNumber = {0}: start", threadNumber);

            _mutex.WaitOne();

            CustomLogger.LogMessage("Thread.LocalNumber = {0}: mutex acquired", threadNumber);
            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            _mutex.ReleaseMutex();
        }

        public void Run()
        {
            CustomLogger.LogStartExample("MutexOrderExample");

            // mutex is locked here
            _mutex.WaitOne();

            const Int32 TestThreadNumber = 7;
            Thread[] threads = new Thread[TestThreadNumber];

            for (Int32 i = 0; i< TestThreadNumber; ++i)
            {
                Int32 copy = i;
                threads[i] = new Thread(() => ThreadRoutine(copy));
                threads[i].Start();

                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            }

            // mutex is released so that other threads can execute
            _mutex.ReleaseMutex();

            for (Int32 i = 0; i < TestThreadNumber; ++i)
            {
                threads[i].Join();
            }            

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
