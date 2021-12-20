using System;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Lectures
{
    internal sealed class ObjectPoolExample
    {
        internal sealed class ReusableClass
        {
            private static Int32 _nextId = 0;
            private Int32 _id;

            private ReusableClass(Int32 id)
            {
                _id = id;
            }

            public static ReusableClass Create()
            {
                return new ReusableClass(Interlocked.Increment(ref _nextId));
            }

            public override string ToString()
            {
                return _id.ToString();
            }
        }

        private readonly ObjectPool<ReusableClass> _objectPool;

        public ObjectPoolExample()
        {
            _objectPool = new ObjectPool<ReusableClass>(ReusableClass.Create);
        }

        void TaskRoutine(Int32 taskNumber, Int32 numberOfIterations)
        {
            CustomLogger.LogMessage("Task#{0} {1} started", taskNumber, Task.CurrentId);

            for (int i = 0; i < numberOfIterations; ++i)
            {
                ReusableClass objectFromPool = _objectPool.GetObject();
                try
                {
                    //Actions with object from pool
                    CustomLogger.LogMessage("Task#{0} Get object '{1}' from pool", taskNumber, objectFromPool);
                }
                finally
                {
                    _objectPool.PutObject(objectFromPool);
                }
            }
            CustomLogger.LogMessage("Task#{0} {1} ended", taskNumber, Task.CurrentId);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Blocking collection Example");
            const Int32 numberOfIterations = 10;
            const Int32 numberOfTasks = 5;

            Task[] tasks = new Task[numberOfTasks];
            for (int i = 0; i < tasks.Length; ++i)
            {
                int copy = i;
                tasks[i] = Task.Run(() => TaskRoutine(copy, numberOfIterations));
            }
            Task.WaitAll(tasks);

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
