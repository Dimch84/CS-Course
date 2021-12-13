using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace AsyncAwait
{
    internal sealed class AsyncAwaitExceptionExample
    {
        private async Task WaitAsync3()
        {
            CustomLogger.LogMessage("WaitAsync3 started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            throw new Exception("test3");
        }

        private async Task WaitAsync2()
        {
            CustomLogger.LogMessage("WaitAsync2 started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(TimeSpan.FromSeconds(3));
            CustomLogger.LogMessage("WaitAsync2 ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            throw new Exception("test2");
        }

        private async Task WaitAsync()
        {
            CustomLogger.LogMessage("WaitAsync started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(TimeSpan.FromSeconds(3));
            CustomLogger.LogMessage("WaitAsync middle1, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            await WaitAsync2();
            CustomLogger.LogMessage("WaitAsync middle2, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(TimeSpan.FromSeconds(2));
            CustomLogger.LogMessage("WaitAsync ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            throw new Exception("test");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Exception example");
            CustomLogger.LogMessage("GetAwaiter().GetResult()");
            try
            {
                WaitAsync().GetAwaiter().GetResult();
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);                
            }            
            Console.ReadKey();

            CustomLogger.LogMessage("Task.Wait()");
            try
            {
                WaitAsync().Wait();
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);                
            }            
            Console.ReadKey();


            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            try
            {
                WaitAsync3();
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Exception: '{0}'.", exc.Message);
                CustomLogger.LogMessage("StackTrace: '{0}'", exc.StackTrace);
            }



            Thread.Sleep(2000); // delay is needed to make sure the task is done before calling GC.
            CustomLogger.LogMessage("Done sleeping");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            CustomLogger.LogEndExample();
            Console.ReadKey();           
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            CustomLogger.LogMessage("Caught task exception. '{0}'", e.Exception.Message);

            e.SetObserved();
        }
    }
}
