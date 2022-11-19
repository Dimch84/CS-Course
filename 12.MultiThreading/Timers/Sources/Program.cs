using Common;
using System;

namespace Timers
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                {
                    BasicSystemTimerExample example = new BasicSystemTimerExample();
                    example.Run();
                }

                {
                    BasicThreadPoolTimerExample example = new BasicThreadPoolTimerExample();
                    example.Run();
                }

                {
                    WaitAllBeforeExitThreadPoolTimerExample example = new WaitAllBeforeExitThreadPoolTimerExample();
                    example.Run();
                }

                {
                    ExecuteOneByOneThreadPoolTimerExampleByLock example = new ExecuteOneByOneThreadPoolTimerExampleByLock();
                    example.Run();
                }

                {
                    ExecuteOneByOneThreadPoolTimerExampleByTimerChange example = new ExecuteOneByOneThreadPoolTimerExampleByTimerChange();
                    example.Run();
                }
            }
            catch(Exception exc)
            {
                CustomLogger.LogException(exc);
            }
            Console.ReadLine();
        }
    }
}
