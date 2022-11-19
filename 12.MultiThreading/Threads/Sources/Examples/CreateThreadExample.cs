using System;
using System.Threading;
using Common;

namespace Threads
{
    internal class CreateThreadExample
    {
        private static void Thread1()
        {
            CustomLogger.LogMessage("Thread1");
        }

        private void Thread2(object obj)
        {
            CustomLogger.LogMessage("Thread2: Obj '{0}'", obj);
        }

        private static void Thread3(Int32 i, String s)
        {
            CustomLogger.LogMessage("Thread3, I '{0}', S '{1}'", i, s);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("CreateThreadExample");

            Thread thread1 = new Thread(Thread1);
            thread1.Start();
            thread1.Join();

            Thread thread2 = new Thread(Thread2);
            thread2.Start(5);
            thread2.Join();

            Thread thread3 = new Thread(_ => Thread3(5, "text"));
            thread3.Start();
            thread3.Join();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
