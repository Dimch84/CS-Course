using System;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class ThreadPoolExceptionExample
    {
        void Func1(Object obj)
        {
            CustomLogger.LogMessage("Func1 started '{0}'", Thread.CurrentThread.ManagedThreadId);

            throw new Exception("Exc from func1");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("thread pool exception example");

            ThreadPool.QueueUserWorkItem(Func1);

            CustomLogger.LogEndExample();            
            Console.ReadKey();
        }
    }
}
