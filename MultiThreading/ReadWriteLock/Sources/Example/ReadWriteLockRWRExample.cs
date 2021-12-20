using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace ReadWriteLock
{
    internal sealed class ReadWriteLockRWRExample : IDisposable
    {
        private readonly IReadWriteLock _rwLock;
        private readonly HashSet<String> _hashset = new HashSet<string>();

        public ReadWriteLockRWRExample(IReadWriteLock rwLock)
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
                CustomLogger.LogMessage("TaskReader1 '{0}' started. Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

                _rwLock.EnterReadLock();
                CustomLogger.LogMessage("TaskReader1 '{0}' inside lock", Task.CurrentId);

                foreach (String item in _hashset)
                {
                    CustomLogger.LogMessage("    TaskReader1: '{0}'", item);
                }

                Thread.Sleep(TimeSpan.FromSeconds(7));

                CustomLogger.LogMessage("TaskReader1 '{0}' exit read lock", Task.CurrentId);
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
                    CustomLogger.LogMessage("    TaskReader2: '{0}'", item);
                }

                CustomLogger.LogMessage("TaskReader2 '{0}' exit read lock", Task.CurrentId);
                _rwLock.ExitReadLock();

                CustomLogger.LogMessage("TaskReader2 '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Reader2: {0}", exc.Message);
            }
        }

        private void TaskWriter()
        {
            try
            {
                CustomLogger.LogMessage("TaskWriter '{0}' started. Thread '{1}'", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

                _rwLock.EnterWriteLock();

                CustomLogger.LogMessage("TaskWriter '{0}' inside lock", Task.CurrentId);

                _hashset.Add("4");
                _hashset.Add("5");

                _rwLock.ExitWriteLock();

                CustomLogger.LogMessage("TaskWriter '{0}' ended", Task.CurrentId);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("Writer: {0}", exc.Message);
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("ReadWriteLock Read-Write-Read example");

            Task task1 = Task.Factory.StartNew(TaskReader1, TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Task task3 = Task.Factory.StartNew(TaskWriter, TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Task task2 = Task.Factory.StartNew(TaskReader2, TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            task1.Wait();
            task2.Wait();
            task3.Wait();
            
            CustomLogger.LogEndExample();

            Console.ReadKey();
        }
    }
}
