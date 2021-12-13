using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    public sealed class BlockingQueueTaskScheduler : TaskScheduler, IDisposable
    {
        private readonly BlockingCollection<Task> _taskQueue = new BlockingCollection<Task>();
        private readonly Thread[] _threads;
        public BlockingQueueTaskScheduler(int concurrency)
        {
            _threads = new Thread[concurrency];
            
            for (int i = 0; i < _threads.Length; i++)
            {
                _threads[i] = new Thread(() =>
                {
                    foreach (Task t in _taskQueue.GetConsumingEnumerable())
                    {
                        TryExecuteTask(t);
                    }
                });
                _threads[i].Start();
            }
        }

        protected override void QueueTask(Task task)
        {
            if (task.CreationOptions.HasFlag(TaskCreationOptions.LongRunning))
            {             
                new Thread(() =>
                {
                    TryExecuteTask(task);
                }).Start();
            }
            else
            {                
                _taskQueue.Add(task);
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // only allow inline execution if the executing thread is one belonging to this scheduler
            if (_threads.Contains(Thread.CurrentThread))
            {
                return TryExecuteTask(task);
            }
            else
            {
                return false;
            }
        }

        public override int MaximumConcurrencyLevel
        {
            get
            {
                return _threads.Length;
            }
        }
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _taskQueue.ToArray();
        }

        public void Dispose()
        {            
            _taskQueue.CompleteAdding();         
            foreach (Thread t in _threads)
            {
                t.Join();
            }
        }
    }
}
