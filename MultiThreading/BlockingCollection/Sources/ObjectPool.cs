using System;
using System.Collections.Concurrent;

namespace Lectures
{
    public sealed class ObjectPool<T>
    {
        private readonly IProducerConsumerCollection<T> _container;
        private readonly Func<T> _generator;
        
        public ObjectPool(Func<T> generator) 
            : this(generator, new ConcurrentQueue<T>())
        {
        }
        
        public ObjectPool(Func<T> generator, IProducerConsumerCollection<T> collection)            
        {            
            _generator = generator;
            _container = collection;
        }

        //Adds the provided item into the pool    
        public void PutObject(T item)
        {
            _container.TryAdd(item);
        }
        
        //If the pool is empty, a new item will be created and returned
        public T GetObject()
        {
            T value;
            return _container.TryTake(out value) ? value : _generator();            
        }
    }
}
