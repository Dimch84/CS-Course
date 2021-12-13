namespace AsyncAwait
{
    static class Program
    {
        static void Main(string[] args)
        {
            {
                AsyncAwaitBasicExample example = new AsyncAwaitBasicExample();
                example.Run();
            }

            {
                AsyncAwaitVsParallelExample example = new AsyncAwaitVsParallelExample();
                example.Run();
            }

            {
                AsyncAwaitVsContinueWithExample example = new AsyncAwaitVsContinueWithExample();
                example.Run();
            }

            {
                AsyncVoidExample example = new AsyncVoidExample();
                example.Run();
            }

            {
                AsyncAwaitExceptionExample example = new AsyncAwaitExceptionExample();
                example.Run();
            }

            {
                CustomAwaitExample example = new CustomAwaitExample();
                example.Run();
            }

            {
                AsyncPrimitiveExample example = new AsyncPrimitiveExample();
                example.Run();
            }
        }
    }
}
