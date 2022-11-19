using Common;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Threads
{
    internal class InterruptThreadExample: IDisposable
    {
        private Mutex _mutex;

        public InterruptThreadExample()
        {
            _mutex = new Mutex(initiallyOwned: true);
        }

        public void Dispose()
        {
            _mutex.Dispose();
        }

        private void ThreadSleep()
        {
            CustomLogger.LogMessage("ThreadSleep start");
            try
            {                
                Thread.Sleep(CustomTimeouts.InfiniteTimePeriod);
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("ThreadSleep: exc '{0}'", exc.Message);
            }
            CustomLogger.LogMessage("ThreadSleep: end");
        }

        private void ThreadWait()
        {
            CustomLogger.LogMessage("ThreadWait start");
            try
            {
                _mutex.WaitOne();   
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("ThreadWait: exc '{0}'", exc.Message);
            }
            CustomLogger.LogMessage("ThreadWait: end");
        }

        private void ThreadHeavyRoutine()
        {
            CustomLogger.LogMessage("ThreadWork start");
            try
            {
               for(int i = 0; i < 10000; i++)
               {
                    // simulate long calculations
                    using (var f = File.Create(@"C:\temp\tmp.txt"))
                    {
                        var s = "test string sample";
                        byte[] bytes = Encoding.ASCII.GetBytes(s);
                        f.Write(bytes, 0, s.Length);
                    }
                    File.Delete(@"C:\temp\tmp.txt");
               }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage("ThreadWork: exc '{0}'", exc.Message);
            }
            CustomLogger.LogMessage("ThreadWork: end");
        }

        public void Run()
        {
            CustomLogger.LogStartExample("InterruptThreadExample");
            
            try
            {
                Thread threadSleep = new Thread(ThreadSleep);
                threadSleep.Start();
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                CustomLogger.LogMessage("Thread state: '{0}'", threadSleep.ThreadState);
                threadSleep.Interrupt();
                threadSleep.Join();

                Thread threadWait = new Thread(ThreadWait);
                threadWait.Start();
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                CustomLogger.LogMessage("Thread state: '{0}'", threadWait.ThreadState);
                threadWait.Interrupt();
                threadWait.Join();

                Thread threadWork = new Thread(ThreadHeavyRoutine);
                threadWork.Start();
                Thread.Sleep(CustomTimeouts.ShortTimePeriod);
                CustomLogger.LogMessage("Thread state: '{0}'", threadWork.ThreadState);
                threadWork.Interrupt();
                threadWork.Join();
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }            

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
