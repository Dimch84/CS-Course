using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace AsyncAwait
{
    internal sealed class AsyncVoidExample
    {
        private async void AsyncVoid()
        {
            CustomLogger.LogMessage("AsyncVoid started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(TimeSpan.FromSeconds(2));

            CustomLogger.LogMessage("AsyncVoid continue, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            //Exception is thrown in calling synchronization context
            throw new Exception("Exc from async void");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Async void example");
            CustomLogger.LogMessage("Run started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            try
            {
                AsyncVoid();
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }          

            CustomLogger.LogMessage("Run ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

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
