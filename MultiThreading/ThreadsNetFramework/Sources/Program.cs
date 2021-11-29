using Common;
using System;

namespace ThreadsNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {                
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
