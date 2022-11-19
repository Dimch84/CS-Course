using Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    internal sealed class ParallelForExceptionExample
    {
        private void Action(int i, ParallelLoopState loopState)
        {
            CustomLogger.LogMessage("Action2, Item '{0}', TaskId '{1}', Thread '{2}' started. IsExceptional '{3}'", 
                i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId, loopState.IsExceptional);


            if (i == 10)
            {
                CustomLogger.LogMessage("throwing Exception");
                throw new Exception("Break iteration");
            }
        }

        public void Run()
        {
            CustomLogger.LogStartExample("Parallel For: exception example");

            try
            {
                ParallelLoopResult loopResult = Parallel.For(1, 20, Action);
                CustomLogger.LogMessage("LoopResult1: '{0}'", loopResult.IsCompleted);             
            }
            catch(Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
