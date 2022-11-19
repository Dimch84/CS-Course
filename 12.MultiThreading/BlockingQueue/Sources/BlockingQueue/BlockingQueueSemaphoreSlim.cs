using System;
using System.Collections.Generic;
using System.Threading;

namespace Lectures
{
    public sealed class BlockingBoundedSemaphoreSlim<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();
        private readonly object m_queueLock = new Object();
        private readonly SemaphoreSlim m_producerSemaphore;
        private readonly SemaphoreSlim m_consumerSemaphore;

        public BlockingBoundedSemaphoreSlim(int capacity)
        {
            m_producerSemaphore = new SemaphoreSlim(capacity, capacity);
            m_consumerSemaphore = new SemaphoreSlim(0, capacity);
        }

        public void Dispose()
        {
            m_producerSemaphore.Dispose();
            m_consumerSemaphore.Dispose();
        }

        public void Enqueue(T obj)
        {
            m_producerSemaphore.Wait();
            lock (m_queueLock)
            {
                m_queue.Enqueue(obj);
            }
            m_consumerSemaphore.Release();
        }

        public T Dequeue()
        {

            m_consumerSemaphore.Wait();

            T value;
            lock (m_queueLock)
            {
                value = m_queue.Dequeue();
            }

            m_producerSemaphore.Release();
            return value;
        }
    }
}
