using System;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class TaskContinuationExample
    {
        private Int32 Task1(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            return 42;
        }

        private Int32 Task2(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task2, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            throw new Exception("Task failed");
        }

        private Int32 Task3(CancellationToken ct)
        {
            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' started", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(2));

            ct.ThrowIfCancellationRequested();
            CustomLogger.LogMessage("Task3, TaskId '{0}', Thread '{1}' ended", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            return 42;
        }

        private void ExecuteFunc(Func<CancellationToken, Int32> func, CancellationToken token)
        {
            try
            {
                CustomLogger.LogMessage("++++Execute task");

                Task<int> task = Task.Run(() => func(token), token);

                task.ContinueWith(
                    t => CustomLogger.LogMessage("Result: {0}", t.Result),
                    TaskContinuationOptions.OnlyOnRanToCompletion);

                task.ContinueWith(
                    _ => CustomLogger.LogMessage("Canceled"),
                    TaskContinuationOptions.OnlyOnCanceled);

                task.ContinueWith(
                    t => CustomLogger.LogMessage("Error: {0}", t.Exception),
                    TaskContinuationOptions.OnlyOnFaulted);

                task.Wait();
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }

        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task continuation example1");

            using (CancellationTokenSource cancellationSource = new CancellationTokenSource())
            {
                ExecuteFunc(Task1, cancellationSource.Token);

                Thread.Sleep(TimeSpan.FromSeconds(2));

                ExecuteFunc(Task2, cancellationSource.Token);

                Thread.Sleep(TimeSpan.FromSeconds(2));
                cancellationSource.Cancel();

                ExecuteFunc(Task3, cancellationSource.Token);
            }

            CustomLogger.LogMessage("====End====");
            Console.ReadKey();

        }
    }
}
