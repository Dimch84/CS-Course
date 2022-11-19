using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal class FakeWakeUpExample : IDisposable
    {
        private readonly Object _object = new Object();
        private bool _waitCondition;

        public FakeWakeUpExample()
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

                while (true)
                {
                    lock (_object)
                    {
                        Int32 waitCount = 0;

                        while (!_waitCondition)
                        {
                            CustomLogger.LogMessage("Thread1: '{0}' monitor wait", Thread.CurrentThread.ManagedThreadId);

                            Monitor.Wait(_object);
                            ++waitCount;
                        }

                        //CustomLogger.LogMessage("Thread1: '{0}' reset condition", Thread.CurrentThread.ManagedThreadId);
                        _waitCondition = false;

                        // thread was waked-up but condition become false
                        // so that thread blocked again. That's why waitCount != 1
                        if (waitCount > 1)
                        {
                            CustomLogger.LogMessage("============================Thread1: '{0}' fake wake up detected", Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                }
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

                while (true)
                {
                    lock (_object)
                    {
                        if (!_waitCondition)
                        {
                            _waitCondition = true;
                            Monitor.Pulse(_object);
                            //CustomLogger.LogMessage("Thread2: '{0}' monitor pulse", Thread.CurrentThread.ManagedThreadId);
                            Thread.Sleep(5);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Thread2: '{0}'", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("FakeWakeUpMonitorExample");

            Thread thread2 = new Thread(Thread2);
            thread2.Start();

            for (int i = 0; i < 10; ++i)
            {
                Thread thread11 = new Thread(Thread1);
                thread11.Start();
            }

            thread2.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
