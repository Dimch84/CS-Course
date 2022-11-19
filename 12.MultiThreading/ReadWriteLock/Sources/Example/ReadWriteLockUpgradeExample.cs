using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace ReadWriteLock
{
    internal sealed class ReadWriteLockUpgradeExample : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;
        private readonly HashSet<String> _hashset = new HashSet<string>();

        public ReadWriteLockUpgradeExample()
        {
            _rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

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
                CustomLogger.LogMessage("TaskReader1 '{0}' started. Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

                _rwLock.EnterReadLock();
                CustomLogger.LogMessage("TaskReader1 '{0}' inside lock", Task.CurrentId);

                foreach (String item in _hashset)
                {
                    CustomLogger.LogMessage("TaskReader1: '{0}'", item);
                }

                Thread.Sleep(TimeSpan.FromSeconds(7));

                _rwLock.ExitReadLock();

                CustomLogger.LogMessage("TaskReader1 '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Reader1: {0}", exc.Message);
            }
        }

        private void TaskReader2()
        {
            try
            {
                CustomLogger.LogMessage("TaskReader2 '{0}' started. Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

                _rwLock.EnterReadLock();
                CustomLogger.LogMessage("TaskReader2 '{0}' inside lock", Task.CurrentId);

                foreach (String item in _hashset)
                {
                    CustomLogger.LogMessage("TaskReader2: '{0}'", item);
                }

                _rwLock.ExitReadLock();

                CustomLogger.LogMessage("TaskReader2 '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Reader2: {0}", exc.Message);
            }
        }

        private void TaskUpgrade1()
        {
            try
            {
                CustomLogger.LogMessage("TaskUpgrade '{0}' started. Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

                _rwLock.EnterUpgradeableReadLock();

                CustomLogger.LogMessage("TaskUpgrade '{0}' inside upgradable lock", Task.CurrentId);

                _rwLock.EnterWriteLock();

                CustomLogger.LogMessage("TaskUpgrade '{0}' inside write lock", Task.CurrentId);

                _hashset.Add("4");
                _hashset.Add("5");

                _rwLock.ExitWriteLock();
                CustomLogger.LogMessage("TaskUpgrade '{0}' leave write lock", Task.CurrentId);

                Thread.Sleep(TimeSpan.FromSeconds(5));

                _rwLock.ExitUpgradeableReadLock();

                CustomLogger.LogMessage("TaskUpgrade '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Writer: {0}", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("ReadWriteLock Upgrade example");

            Task task1 = Task.Run(() => TaskReader1());
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Task task3 = Task.Run(() => TaskUpgrade1());
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Task task2 = Task.Run(() => TaskReader2());
            Thread.Sleep(TimeSpan.FromSeconds(2));

            task1.Wait();
            task2.Wait();
            task3.Wait();
            
            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
