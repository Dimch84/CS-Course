using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class WaitOnDisposeMutexExample : IDisposable
    {
        private readonly Mutex _mutex;

        public WaitOnDisposeMutexExample()
        {
            _mutex = new Mutex(false);
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }

        void Thread1()
        {
            try
            {
                CustomLogger.LogMessage("Thread1: '{0}' started", Thread.CurrentThread.ManagedThreadId);

                _mutex.WaitOne();

                CustomLogger.LogMessage("Thread1: '{0}' ended", Thread.CurrentThread.ManagedThreadId);
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
                CustomLogger.LogMessage("Thread2: '{0}' started", Thread.CurrentThread.ManagedThreadId);

                _mutex.WaitOne();

                CustomLogger.LogMessage("Thread2: '{0}' ended", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("DisposeMutexExample");

            _mutex.WaitOne();

            Thread thread1 = new Thread(Thread1);
            thread1.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            CustomLogger.LogMessage("Dispose mutex");
            _mutex.Dispose();

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            //thread1.Join();
            thread2.Join();
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
