using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace AsyncAwait
{
    internal sealed class AsyncAwaitVsContinueWithExample
    {
        private int Wait(TimeSpan timeout)
        {
            CustomLogger.LogMessage("Wait started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(timeout);
            CustomLogger.LogMessage("Wait ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            return 42;
        }

        private Task<int> WaitTask(TimeSpan timeout)
        {
            return Task.Run(() => Wait(timeout));
        }

        private async Task<int> WaitAsync()
        {
            CustomLogger.LogMessage("WaitAsync started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            int result1 = await WaitTask(TimeSpan.FromSeconds(3));
            CustomLogger.LogMessage("WaitAsync middle1, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            int result2 = await WaitTask(TimeSpan.FromSeconds(4));
            CustomLogger.LogMessage("WaitAsync ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            return result1 + result2;
        }

        internal Task<int> WaitAsyncContinueWith()
        {
            TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
            try
            {
                CustomLogger.LogMessage("WaitAsync started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                WaitTask(TimeSpan.FromSeconds(3)).ContinueWith(await1 =>
                {
                    try
                    {
                        int resultOfAwait1 = await1.Result;
                        CustomLogger.LogMessage("WaitAsync middle1, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                        WaitTask(TimeSpan.FromSeconds(4)).ContinueWith(await2 =>
                        {
                            try
                            {
                                int resultOfAwait2 = await2.Result;
                                CustomLogger.LogMessage("WaitAsync ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                                int resultToReturn = resultOfAwait1 + resultOfAwait2;
                                taskCompletionSource.SetResult(resultToReturn);
                            }
                            catch (Exception exception)
                            {
                                taskCompletionSource.SetException(exception);
                            }
                        });
                    }
                    catch (Exception exception)
                    {
                        taskCompletionSource.SetException(exception);
                    }
                });
            }
            catch (Exception exception)
            {
                taskCompletionSource.SetException(exception);
            }
            return taskCompletionSource.Task;
        }


        public void Run()
        {
            CustomLogger.LogStartExample("AsyncAwait vs ContinueWith example2");
            CustomLogger.LogMessage("Run started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            int result = WaitAsync().GetAwaiter().GetResult();

            CustomLogger.LogMessage("Run ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();

            CustomLogger.LogMessage("Run started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            int result2 = WaitAsyncContinueWith().GetAwaiter().GetResult();
            CustomLogger.LogMessage("Run ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
