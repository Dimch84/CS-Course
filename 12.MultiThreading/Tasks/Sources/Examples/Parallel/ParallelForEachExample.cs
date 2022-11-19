using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace Tasks
{
    internal sealed class ParallelForEachExample
    {
        private void Action1(int i, ParallelLoopState loopState)
        {
            CustomLogger.LogMessage("Action1, Item '{0}', TaskId '{1}', Thread '{2}' started", 
                i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
        }

        private void Action2(int i, ParallelLoopState loopState)
        {
            CustomLogger.LogMessage("Action2, Item '{0}', TaskId '{1}', Thread '{2}' started. IsStopped '{3}'", 
                i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId, loopState.IsStopped);

            if (i == 10)
            {
                CustomLogger.LogMessage("Stopping iteration");
                loopState.Stop();
            }
        }

        private void Action3(int i, ParallelLoopState loopState)
        {
            CustomLogger.LogMessage("Action3, Item '{0}', TaskId '{1}', Thread '{2}' started. IsStopped '{3}'", 
                i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId, loopState.IsStopped);          

            if (i == 10)
            {
                CustomLogger.LogMessage("Breaking iteration");
                loopState.Break();
            }           
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Parallel For: example");

            IEnumerable<Int32> sequence = Enumerable.Range(1, 20);

            ParallelLoopResult loopResult1 = Parallel.ForEach(sequence, Action1);
            CustomLogger.LogMessage("LoopResult1: '{0}'", loopResult1.IsCompleted);
            CustomLogger.LogEndExample();
            Console.ReadKey();
            

            CustomLogger.LogStartExample("Parallel ForEach: Stop example");
            ParallelLoopResult loopResult2 = Parallel.ForEach(sequence, Action2);
            CustomLogger.LogMessage("LoopResult2: '{0}', LowestBreakIteration '{1}'", loopResult2.IsCompleted, loopResult2.LowestBreakIteration);
            CustomLogger.LogEndExample();
            Console.ReadKey();

            CustomLogger.LogStartExample("Parallel ForEach: Break example");
            ParallelLoopResult loopResult3 = Parallel.ForEach(sequence, Action3);
            CustomLogger.LogMessage("LoopResult3: '{0}', LowestBreakIteration '{1}'", loopResult3.IsCompleted, loopResult3.LowestBreakIteration);
            CustomLogger.LogEndExample();
            Console.ReadKey();

            CustomLogger.LogStartExample("Parallel ForEach: use options example");
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                ParallelOptions parallelOptions = new ParallelOptions
                {
                    CancellationToken = cancellationTokenSource.Token,
                    MaxDegreeOfParallelism = Environment.ProcessorCount,
                    TaskScheduler = new ThreadPerTaskScheduler()
                };

                ParallelLoopResult loopResult4 = Parallel.ForEach(sequence, parallelOptions, Action1);
                CustomLogger.LogMessage("LoopResult4: '{0}', LowestBreakIteration '{1}'", loopResult4.IsCompleted, loopResult4.LowestBreakIteration);
            }
            CustomLogger.LogEndExample();
            Console.ReadKey();           
        }

    }
}
