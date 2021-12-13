using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace AsyncAwait
{
    internal sealed class AsyncAwaitBasicExample
    {
        private async Task WaitAsync()
        {
            CustomLogger.LogMessage("WaitAsync started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await Task.Delay(TimeSpan.FromSeconds(3));

            CustomLogger.LogMessage("WaitAsync middle1, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await Task.Delay(TimeSpan.FromSeconds(4));

            CustomLogger.LogMessage("WaitAsync middle2, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            await Task.Delay(TimeSpan.FromSeconds(2));

            CustomLogger.LogMessage("WaitAsync ended, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        private async Task WaitLoopAsync()
        {
            CustomLogger.LogMessage("WaitLoopAsync started, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            IEnumerable<Int32> source = Enumerable.Range(0, 10);
            foreach (Int32 item in source)
            {
                CustomLogger.LogMessage("WaitLoopAsync loop1, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
                await Task.Delay(TimeSpan.FromSeconds(3));
                CustomLogger.LogMessage("WaitLoopAsync loop2, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            }
            
            CustomLogger.LogMessage("WaitLoopAsync ended, Task '{0}', Thread '{1}', Thread.CurrentThread.IsThreadPoolThread {2}",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Basic example");
            CustomLogger.LogMessage("Run started, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Task asyncTask = WaitAsync();
            CustomLogger.LogMessage("Async task returned");
            asyncTask.GetAwaiter().GetResult();                   

            CustomLogger.LogMessage("Run ended, Task '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            //Console.ReadKey();
            //Task task = Task.Run(() => WaitAsync());
            //task.Wait();

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
