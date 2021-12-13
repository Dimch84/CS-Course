using System;
using System.Collections.Generic;
using System.Threading;

namespace _10.CustomThreadPool
{
    class SuperQueue : IDisposable
    {
        readonly object _locker = new object();
        readonly List<Thread> _workers;
        readonly Queue<Task> _taskQueue = new Queue<Task>();

        public SuperQueue(int workerCount)
        {
            _workers = new List<Thread>(workerCount);

            // Create and start a separate thread for each worker
            for (int i = 0; i < workerCount; i++)
            {
                Thread t = new Thread(Consume) { IsBackground = true, Name = string.Format("SuperQueue worker {0}", i) };
                _workers.Add(t);
                t.Start();
            }
        }

        public void EnqueueTask(Task task)
        {
            lock (_locker)
            {
                _taskQueue.Enqueue(task);
                Monitor.PulseAll(_locker);
            }
        }

        private void Consume()
        {
            while (true)
            {
                Task item;
                lock (_locker)
                {
                    while (_taskQueue.Count == 0) Monitor.Wait(_locker);
                    item = _taskQueue.Dequeue();
                }
                if (item == null) return;

                // run actual method
                item.Action(item.Arg);
            }
        }

        public void Dispose()
        {
            // Enqueue one null task per worker to make each exit.
            _workers.ForEach(thread => EnqueueTask(null));

            _workers.ForEach(thread => thread.Join());
        }
    }
}
