using Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    internal sealed class CustomShedulerExample
    {
        private void ExecuteTasks(TaskScheduler taskScheduler)
        {
            CustomLogger.LogMessage("Custom scheduler ID: {0}", taskScheduler.Id);
            CustomLogger.LogMessage("Default scheduler ID: {0}", TaskScheduler.Default.Id);
                                         
            Task task1 = new Task(() => 
            {
                CustomLogger.LogMessage("Task#1 '{0}' executed by scheduler {1}", Task.CurrentId, TaskScheduler.Current.Id);
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);

                Task.Factory.StartNew(() => 
                {
                    CustomLogger.LogMessage("ChildTask '{0}' executed by scheduler {1}", Task.CurrentId, TaskScheduler.Current.Id);
                    Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                }, CancellationToken.None, TaskCreationOptions.AttachedToParent, taskScheduler);                                
            });            
            task1.Start(taskScheduler);
                     
            task1.ContinueWith(antecedent => 
            {
                CustomLogger.LogMessage("Task {0} continuation executed by scheduler {1}", Task.CurrentId, TaskScheduler.Current.Id);
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
            }, CancellationToken.None, TaskContinuationOptions.AttachedToParent, taskScheduler);

            CustomLogger.LogMessage("Waiting for Task#1");
            task1.Wait();
            CustomLogger.LogMessage("Task#1 completed");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Custom sheduler examples");

            CustomLogger.LogMessage("Native task scheduler");
            ExecuteTasks(TaskScheduler.Current);
            Console.ReadLine();

            CustomLogger.LogMessage("Current thread task scheduler");
            CurrentThreadTaskScheduler currentThreadTaskScheduler = new CurrentThreadTaskScheduler();
            ExecuteTasks(currentThreadTaskScheduler);
            Console.ReadLine();

            CustomLogger.LogMessage("ThreadPerTask task scheduler");
            ThreadPerTaskScheduler threadPerTaskScheduler = new ThreadPerTaskScheduler();
            ExecuteTasks(threadPerTaskScheduler);

            Console.ReadLine();
            CustomLogger.LogMessage("Blocking queue task scheduler");
            BlockingQueueTaskScheduler blockingQueueTaskScheduler = new BlockingQueueTaskScheduler(2);
            ExecuteTasks(blockingQueueTaskScheduler);

            CustomLogger.LogEndExample();
            Console.ReadLine();
        }
    }
}
