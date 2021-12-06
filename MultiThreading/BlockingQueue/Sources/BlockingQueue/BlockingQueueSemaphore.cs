using System.Collections.Generic;
using System.Threading;

namespace Lectures
{
    public sealed class BlockingQueueSemaphore<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();
        private readonly Mutex m_mutex = new Mutex();
        private readonly Semaphore m_producerSemaphore;
        private readonly Semaphore m_consumerSemaphore;

        public BlockingQueueSemaphore(int capacity)
        {
            m_producerSemaphore = new Semaphore(capacity, capacity);
            m_consumerSemaphore = new Semaphore(0, capacity);
        }

        public void Dispose()
        {
            m_producerSemaphore.Dispose();
            m_consumerSemaphore.Dispose();
        }

        public void Enqueue(T obj)
        {
            m_producerSemaphore.WaitOne();

            m_mutex.WaitOne();
            try
            {
                m_queue.Enqueue(obj);
            }
            finally
            {
                m_mutex.ReleaseMutex();
            }
            m_consumerSemaphore.Release();
        }

        public T Dequeue()
        {
            m_consumerSemaphore.WaitOne();

            T value;
            m_mutex.WaitOne();
            try
            {
                value = m_queue.Dequeue();
            }
            finally
            {
                m_mutex.ReleaseMutex();
            }

            m_producerSemaphore.Release();
            return value;
        }
    }
}
