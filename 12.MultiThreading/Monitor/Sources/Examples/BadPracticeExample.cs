using Common;
using System;
using System.Threading;

namespace Lectures
{
    sealed class BadPracticeExample
    {
        class BadLockClass
        {
            public void Method()
            {
                CustomLogger.LogMessage("Method: Trying to lock(this)");
                lock (this)
                {

                }
                CustomLogger.LogMessage("Method ended");
            }
        }

        void ThreadLockThis(BadLockClass badLockClass)
        {
            CustomLogger.LogMessage("Thread1 started");
            lock (badLockClass)
            {
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            }
            CustomLogger.LogMessage("Thread1 ended");
        }

        void ThreadLockInt(Int32 threadNumber, Object lockObject)
        {
            CustomLogger.LogMessage("Thread{0} started", threadNumber);
            lock (lockObject)
            {
                CustomLogger.LogMessage("Thread{0} inside lock", threadNumber);
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            }
            CustomLogger.LogMessage("Thread{0} ended", threadNumber);
        }

        void ThreadString()
        {
            CustomLogger.LogMessage("ThreadLockString started");
            lock ("MyLockString")
            {
                CustomLogger.LogMessage("ThreadLockString: inside lock");
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            }
            CustomLogger.LogMessage("ThreadLockString ended");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("lock(this) example");

            BadLockClass badLockClass = new BadLockClass();
            Thread thread1 = new Thread(() => ThreadLockThis(badLockClass));
            thread1.Start();
            Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            badLockClass.Method();
            thread1.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();

            CustomLogger.LogStartExample("lock(Int32) example");

            Int32 lockInt = 0;
            Thread thread2 = new Thread(() => ThreadLockInt(1, lockInt));
            thread2.Start();

            Thread thread3 = new Thread(() => ThreadLockInt(2, lockInt));
            thread3.Start();

            thread2.Join();
            thread3.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();

            CustomLogger.LogStartExample("Lock string example");

            Thread threadLockString;
            lock ("MyLockString")
            {
                threadLockString = new Thread(ThreadString);
                threadLockString.Start();
                Thread.Sleep(CustomTimeouts.MiddleTimePeriod);
            }
            CustomLogger.LogMessage("Unlock String");
            threadLockString.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
