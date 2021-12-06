using System.Collections.Generic;
using System.Threading;

namespace Lectures
{
    public sealed class BlockingQueueWithMonitor<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();

        public void Dispose()
        {
        }

        public void Enqueue(T obj)
        {
            lock (m_queue)
            {
                m_queue.Enqueue(obj);
                Monitor.Pulse(m_queue);
            }
        }

        public T Dequeue()
        {
            lock (m_queue)
            {
                while (m_queue.Count == 0)
                    Monitor.Wait(m_queue);

                return m_queue.Dequeue();
            }
        }
    }
}
