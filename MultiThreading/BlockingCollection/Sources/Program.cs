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
                using (BlockingCollectionExample example = new BlockingCollectionExample())
                {
                    example.Run();
                }

                //{
                //    ObjectPoolExample example = new ObjectPoolExample();
                //    example.Run();
                //}
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }
        }
    }
}
