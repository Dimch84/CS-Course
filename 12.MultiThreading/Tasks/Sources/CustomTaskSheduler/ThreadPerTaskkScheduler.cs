using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    //Provides a task scheduler that dedicates a thread per task.
    public sealed class ThreadPerTaskScheduler : TaskScheduler
    {        
        //This will always return an empty enumerable, as tasks are launched as soon as they're queued.</remarks>
        protected override IEnumerable<Task> GetScheduledTasks() { return Enumerable.Empty<Task>(); }
        
        protected override void QueueTask(Task task)
        {
            CustomLogger.LogMessage("Queue task: execute task '{0}'", task.Id);
            new Thread(() => TryExecuteTask(task))
            {
                IsBackground = true
            }.Start();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return TryExecuteTask(task);
        }
    }
}
