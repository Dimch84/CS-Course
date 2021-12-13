using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks
{
    //Provides a task scheduler that runs tasks on the current thread.
    public sealed class CurrentThreadTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            CustomLogger.LogMessage("Queue task: execute task '{0}'", task.Id);
            TryExecuteTask(task);
        }

        //Runs the provided Task synchronously on the current thread.    
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            CustomLogger.LogMessage("Execute task inline: '{0}'", task.Id);
            return TryExecuteTask(task);
        }

        //An empty enumerable, as Tasks are never queued, only executed.
        protected override IEnumerable<Task> GetScheduledTasks()
        {            
            return Enumerable.Empty<Task>();
        }

        //Gets the maximum degree of parallelism for this scheduler.
        public override int MaximumConcurrencyLevel { get { return 1; } }
    }
}
