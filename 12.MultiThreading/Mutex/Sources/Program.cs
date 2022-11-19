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
                using (MutexOrderExample example = new MutexOrderExample())
                {
                    example.Run();
                }

                using (MutexRecursiveExample example = new MutexRecursiveExample())
                {
                    example.Run();
                }

                using (ReleaseNotOwnerMutex example = new ReleaseNotOwnerMutex())
                {
                    example.Run();
                }

                using (AbandonedMutexExample example = new AbandonedMutexExample())
                {
                    example.Run();
                }

                using (WaitAllExample example = new WaitAllExample())
                {
                    example.Run();
                }

                {
                    NamedMutexExample example = new NamedMutexExample();
                    example.Run();
                }
            }
            catch (Exception exc)
            {
                CustomLogger.LogMessage(exc.Message);
            }
        }
    }
}
