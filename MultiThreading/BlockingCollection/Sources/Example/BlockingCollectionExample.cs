using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Lectures
{
    internal sealed class BlockingCollectionExample : IDisposable
    {
        private readonly BlockingCollection<String> _collection;

        public BlockingCollectionExample()
        {
            _collection = new BlockingCollection<string>(new ConcurrentBag<String>(), 2);
        }

        public void Dispose()
        {
            _collection.Dispose();
        }

        void Task1()
        {
            CustomLogger.LogMessage("Task1 {0} started", Task.CurrentId);

            for (int i = 0; i < 5; ++i)
            {
                _collection.Add(i.ToString(CultureInfo.InvariantCulture));

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            _collection.CompleteAdding();
            CustomLogger.LogMessage("Task1 {0} ended", Task.CurrentId);
        }

        void Task2()
        {
            CustomLogger.LogMessage("Task2 {0} started", Task.CurrentId);
            foreach (String item in _collection.GetConsumingEnumerable())
            {
                CustomLogger.LogMessage(item);
            }
            CustomLogger.LogMessage("Task2 {0} ended", Task.CurrentId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Blocking collection Example");
            Task task1 = Task.Run(() => Task1());
            Task task2 = Task.Run(() => Task2());

            task1.Wait();
            task2.Wait();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
