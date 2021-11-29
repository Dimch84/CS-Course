using System;
using System.Threading;
using Common;

namespace Threads
{
    internal class ThreadLocalStorage
    {       
        private readonly LocalDataStoreSlot _threadLocalStorage;
        public ThreadLocalStorage()
        {
            _threadLocalStorage = Thread.GetNamedDataSlot("Name1");
        }

        void Thread1()
        {
            CustomLogger.LogMessage("Thread1: started");

            String dataSet = "thread1";
            Thread.SetData(_threadLocalStorage, dataSet);
            CustomLogger.LogMessage("Thread1: set data '{0}'", dataSet);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            object data = Thread.GetData(_threadLocalStorage);

            CustomLogger.LogMessage("Thread1: data '{0}'", data);
        }

        void Thread2()
        {
            CustomLogger.LogMessage("Thread2 started");

            String dataSet = "thread2";
            Thread.SetData(_threadLocalStorage, dataSet);
            CustomLogger.LogMessage("Thread2: set data '{0}'", dataSet);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            object data = Thread.GetData(_threadLocalStorage);

            CustomLogger.LogMessage("Thread2: data '{0}'", data);
        }

        ThreadLocal<String> _threadLocal = new ThreadLocal<string>(trackAllValues: true);

        void Thread3()
        {
            CustomLogger.LogMessage("Thread3: started");

            String dataSet = "thread3";
            _threadLocal.Value = dataSet;
            CustomLogger.LogMessage("Thread3: set data '{0}'", dataSet);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            object data = _threadLocal.Value;

            CustomLogger.LogMessage("Thread3: data '{0}'", data);
        }

        void Thread4()
        {
            CustomLogger.LogMessage("Thread4 started");

            String dataSet = "thread4";
            _threadLocal.Value = dataSet;
            CustomLogger.LogMessage("Thread4: set data '{0}'", dataSet);

            Thread.Sleep(CustomTimeouts.ShortTimePeriod);

            object data = _threadLocal.Value;

            CustomLogger.LogMessage("Thread4: data '{0}'", data);
        }

        public void Run()
        {
            try
            {
                CustomLogger.LogStartExample("TLSExample");

                // LocalDataStoreSlot example
                Thread thread1 = new Thread(Thread1);
                thread1.Start();

                Thread thread2 = new Thread(Thread2);
                thread2.Start();

                thread1.Join();
                thread2.Join();

                // ThreadLocal example
                Thread thread3 = new Thread(Thread3);
                thread3.Start();

                Thread thread4 = new Thread(Thread4);
                thread4.Start();

                thread3.Join();
                thread4.Join();

                CustomLogger.LogMessage("All values");
                foreach (String value in _threadLocal.Values)
                {
                    CustomLogger.LogTabMessage(1, value);
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
