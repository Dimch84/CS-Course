using System;
using Common;

namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                {
                    CreateThreadExample example = new CreateThreadExample();
                    example.Run();
                }

                {
                    ThreadStartOrderExample example = new ThreadStartOrderExample();
                    example.Run();
                }

                {
                    InterruptThreadExample example = new InterruptThreadExample();
                    example.Run();
                }

                {
                    CancellationThreadExample example = new CancellationThreadExample();
                    example.Run();
                }

                {
                    ThreadLocalStorage example = new ThreadLocalStorage();
                    example.Run();
                }

                {
                    ThrowExceptionExample example = new ThrowExceptionExample();
                    example.Run();
                }

                {
                    AbortThreadExample example = new AbortThreadExample();
                    example.Run();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }
        }
    }
}
