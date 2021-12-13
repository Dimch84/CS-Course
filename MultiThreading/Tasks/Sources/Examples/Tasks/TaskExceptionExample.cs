using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Tasks
{
    internal sealed class TaskExceptionExample
    {
        private void Task1()
        {
            CustomLogger.LogMessage("Task1, TaskId '{0}', Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            throw new Exception("testTask exception");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Task Exception example");

            try
            {
                Task task1 = Task.Run(() => Task1());
                task1.Wait();
            }
            catch (AggregateException exc)
            {
                CustomLogger.LogMessage("Exception message '{0}'", exc.Message);
                foreach (Exception innerExc in exc.InnerExceptions)
                {
                    CustomLogger.LogMessage("    Inner exception: '{0}'", innerExc.Message);
                }
            }

            CustomLogger.LogMessage("Task wait completed");
            Console.ReadKey();

            // Run out of debug to enter this handler
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


            Task.Run(() => Task1());
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
