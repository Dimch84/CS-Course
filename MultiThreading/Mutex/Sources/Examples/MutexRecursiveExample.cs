using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class MutexRecursiveExample : IDisposable
    {
        private readonly Mutex _mutex;

        public MutexRecursiveExample()
        {
            _mutex = new Mutex(false);
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }

        public void Run()
        {
            CustomLogger.LogStartExample("MutexRecursiveExample");

            _mutex.WaitOne();
            _mutex.WaitOne();
            _mutex.WaitOne();

            CustomLogger.LogMessage("Mutex acquired");
            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            _mutex.ReleaseMutex();
            _mutex.ReleaseMutex();
            _mutex.ReleaseMutex();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
