using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace ReadWriteLock
{
    internal sealed class ReadWriteLockWRWExample : IDisposable
    {
        private readonly IReadWriteLock _rwLock;
        private readonly HashSet<String> _hashset = new HashSet<string>();

        public ReadWriteLockWRWExample(IReadWriteLock rwLock)
        {
            _rwLock = rwLock;

            _hashset.Add("1");
            _hashset.Add("2");
            _hashset.Add("3");
        }

        public void Dispose()
        {

        }

        private void TaskReader1()
        {
            try
            {
                CustomLogger.LogMessage("TaskReader1 '{0}' started", Task.CurrentId);

                _rwLock.EnterReadLock();
                CustomLogger.LogMessage("TaskReader1 '{0}' inside lock", Task.CurrentId);

                foreach (String item in _hashset)
                {
                    CustomLogger.LogMessage("    TaskReader1: '{0}'", item);
                }

                _rwLock.ExitReadLock();

                CustomLogger.LogMessage("TaskReader1 '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Reader1: {0}", exc.Message);
            }
        }

        private void TaskWriter1()
        {
            try
            {
                CustomLogger.LogMessage("TaskWriter1 '{0}' started", Task.CurrentId);

                _rwLock.EnterWriteLock();

                CustomLogger.LogMessage("TaskWriter1 '{0}' inside lock", Task.CurrentId);

                _hashset.Add("4");
                _hashset.Add("5");

                Thread.Sleep(TimeSpan.FromSeconds(7));


                _rwLock.ExitWriteLock();

                CustomLogger.LogMessage("TaskWriter1 '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Writer1: {0}", exc.Message);
            }
        }

        private void TaskWriter2()
        {
            try
            {
                CustomLogger.LogMessage("TaskWriter2 '{0}' started", Task.CurrentId);

                _rwLock.EnterWriteLock();

                CustomLogger.LogMessage("TaskWriter2 '{0}' inside lock", Task.CurrentId);

                _hashset.Add("6");
                _hashset.Add("7");

                _rwLock.ExitWriteLock();

                CustomLogger.LogMessage("TaskWriter2 '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Writer2: {0}", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("ReadWriteLock Write-Read-Write example");

            Task task1 = Task.Run(() => TaskWriter1());
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Task task2 = Task.Run(() => TaskReader1());
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Task task3 = Task.Run(() => TaskWriter2());
            Thread.Sleep(TimeSpan.FromSeconds(2));

            task1.Wait();
            task2.Wait();
            task3.Wait();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
