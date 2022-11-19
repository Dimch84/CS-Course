using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class WaitAllExample : IDisposable
    {
        private readonly Mutex _mutex1;
        private readonly Mutex _mutex2 ;

        public WaitAllExample()
        {
            _mutex1 = new Mutex(false);
            _mutex2 = new Mutex(false);
        }
        
        public void Dispose()
        {
            _mutex1.Dispose();
            _mutex2.Dispose();
        }

        void Thread1()
        {
            CustomLogger.LogMessage("Thread1: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _mutex1.WaitOne();                                  
            Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            _mutex1.ReleaseMutex();

            CustomLogger.LogMessage("Thread1: '{0}' mutex1 release", Thread.CurrentThread.ManagedThreadId);            
        }

        void Thread2()
        {            
            CustomLogger.LogMessage("Thread2: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            _mutex2.WaitOne();            
            Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            _mutex2.ReleaseMutex();

            CustomLogger.LogMessage("Thread2: '{0}' mutex2 release", Thread.CurrentThread.ManagedThreadId);
        }        

        public void Run()
        {
            CustomLogger.LogStartExample("WaitAllExample");
            CustomLogger.LogMessage("Main: '{0}' start", Thread.CurrentThread.ManagedThreadId);

            Thread thread1 = new Thread(Thread1);
            thread1.Start();            
            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            CustomLogger.LogMessage("Main: '{0}' wait all", Thread.CurrentThread.ManagedThreadId);            
            WaitHandle[] handles = {_mutex1, _mutex2};
            WaitHandle.WaitAll(handles);

            CustomLogger.LogMessage("Main: '{0}' end", Thread.CurrentThread.ManagedThreadId);

            thread1.Join();
            thread2.Join();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
