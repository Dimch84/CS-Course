using System;
using System.Threading;
using Common;

namespace Threads
{
    internal class ThrowExceptionExample
    {
        void Thread1()
        {
            CustomLogger.LogMessage("Thread1: started");

            throw new Exception("Thread1 exception");
        }

        Exception _threadExc;
        void Thread2()
        {
            try
            {
                CustomLogger.LogMessage("Thread2: started");

                throw new Exception("Thread2 exception");
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
                _threadExc = exc;
            }
        }

        public void Run()
        {
            try
            {
                CustomLogger.LogStartExample("ThrowExceptionExample");

                AppDomain currentDomain = AppDomain.CurrentDomain;
                currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

                Thread thread1 = new Thread(Thread1);
                thread1.IsBackground = true;
                thread1.Start();
                thread1.Join();

                Thread thread2 = new Thread(Thread2);
                thread2.Start();
                thread2.Join();

                if (_threadExc != null)
                {
                    CustomLogger.LogException(_threadExc);
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);                
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            CustomLogger.LogMessage("MyHandler caught : " + e.Message);
        }
    }
}
