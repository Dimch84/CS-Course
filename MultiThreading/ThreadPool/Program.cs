using System;
using Common;

namespace Lectures
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                {
                    ThreadPoolBasicExample example = new ThreadPoolBasicExample();
                    example.Run();
                }

                {
                    ThreadPoolManyThreadsExample example = new ThreadPoolManyThreadsExample();
                    example.Run();
                }

                {
                    ThreadPoolExceptionExample example = new ThreadPoolExceptionExample();
                    example.Run();
                }

                {
                    ThreadPoolRegisterWaitExample example = new ThreadPoolRegisterWaitExample();
                    example.Run();
                }

                {
                    ThreadPoolDeadLockExample example = new ThreadPoolDeadLockExample();
                    example.Run();
                }

                {
                    ThreadPoolIOOperationsExample example = new ThreadPoolIOOperationsExample();
                    example.Run();
                }
            }
            catch (Exception e)
            {
                CustomLogger.LogException(e);
            }
        }
    }
}
