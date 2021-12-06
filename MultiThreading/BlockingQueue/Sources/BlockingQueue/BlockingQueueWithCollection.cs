using System.Collections.Concurrent;

namespace Lectures
{
    class BlockingQueueWithCollection<T> : IBlockingQueue<T>
    {
        readonly BlockingCollection<T> _collection;

        public BlockingQueueWithCollection(int capacity)
        {
            _collection = new BlockingCollection<T>(new ConcurrentQueue<T>(), capacity);
        }

        public void Dispose()
        {
            _collection.Dispose();
        }

        public void Enqueue(T obj)
        {
            _collection.Add(obj);
        }

        public T Dequeue()
        {
            return _collection.Take();
        }
    }
}
