using System;
using System.Threading;

namespace FinalizationTimer
{
    internal class Program
    {
        private static void Main()
        {
            TestTimer(new StaticTimerHolder());

            Console.WriteLine();

            TestTimer(new InstanceTimerHolder());
        }
        
        private static void TestTimer(Object timerHolder)
        {
            Console.WriteLine("Test started for {0}", timerHolder.GetType());
            Console.ReadKey();

            timerHolder = null;

            Console.WriteLine("Timer holder is no longer referenced");
            Console.ReadKey();

            GC.Collect();
            GC.WaitForFullGCComplete();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("GC is finished and all finalizers are completed");

            Console.ReadKey();
        }
    }

    internal class StaticTimerHolder
    {
        private Timer _timer;

        public StaticTimerHolder()
        {
            _timer = new Timer(StaticTimerCallback, null, 1000, 1000);
        }

        private static void StaticTimerCallback(Object state)
        {
            Console.WriteLine("Static timer callback");
        }
    }

    internal class InstanceTimerHolder
    {
        private Timer _timer;

        public InstanceTimerHolder()
        {
            _timer = new Timer(InstanceTimerCallback, null, 1000, 1000);
        }

        private void InstanceTimerCallback(Object state)
        {
            Console.WriteLine("Instance timer callback");
        }
    }
}
