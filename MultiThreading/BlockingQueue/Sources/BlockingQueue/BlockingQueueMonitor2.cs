using System;
using System.Collections.Generic;
using System.Threading;

namespace Lectures
{
    public sealed class BlockingQueueWithMonitor2<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();
        private readonly int m_capacity;
        private readonly Object m_emptyLock = new Object();
        private readonly Object m_fullLock = new Object();
        private int m_emptyWaiters;
        private int m_fullWaiters;

        public BlockingQueueWithMonitor2(int capacity)
        {
            m_capacity = capacity;
        }

        public void Dispose()
        {
        }

        public void Enqueue(T obj)
        {
            lock (m_queue)
            {
                while (m_queue.Count == m_capacity)
                {
                    m_fullWaiters++;
                    try
                    {
                        lock (m_fullLock)
                        {
                            Monitor.Exit(m_queue);
                            Monitor.Wait(m_fullLock);
                            Monitor.Enter(m_queue);
                        }
                    }
                    finally
                    {
                        m_fullWaiters--;
                    }
                }

                m_queue.Enqueue(obj);
            }

            if (m_emptyWaiters > 0)
                lock (m_emptyLock)
                    Monitor.Pulse(m_emptyLock);
        }

        public T Dequeue()
        {
            T item;

            lock (m_queue)
            {
                while (m_queue.Count == 0)
                {
                    m_emptyWaiters++;
                    try
                    {
                        lock (m_emptyLock)
                        {
                            Monitor.Exit(m_queue);
                            Monitor.Wait(m_emptyLock);
                            Monitor.Enter(m_queue);
                        }
                    }
                    finally
                    {
                        m_emptyWaiters--;
                    }
                }

                item = m_queue.Dequeue();
            }

            if (m_fullWaiters > 0)
                lock (m_fullLock)
                    Monitor.Pulse(m_fullLock);

            return item;
        }
    }
}
