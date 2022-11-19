namespace Tasks
{
    class Program
    {
        static void RunTasksExamples()
        {
            {
                TaskCreationExample example = new TaskCreationExample();
                example.Run();
            }

            {
                TaskWhenAllExample example = new TaskWhenAllExample();
                example.Run();
            }

            {
                TaskWaitChildTask example = new TaskWaitChildTask();
                example.Run();
            }

            {
                TaskCancellationExample example = new TaskCancellationExample();
                example.Run();
            }

            {
                TaskExceptionExample example = new TaskExceptionExample();
                example.Run();
            }

            {
                TaskContinuationExample example = new TaskContinuationExample();
                example.Run();
            }

            {
                TaskContinuationExample2 example = new TaskContinuationExample2();
                example.Run();
            }

            {
                CustomShedulerExample example = new CustomShedulerExample();
                example.Run();
            }

            {
                TaskCompletionSourceExample example = new TaskCompletionSourceExample();
                example.Run();
            }
        }

        static void RunParallelExamples()
        {
            {
                ParallelInvokeExample example = new ParallelInvokeExample();
                example.Run();
            }

            {
                ParallelForEachExample example = new ParallelForEachExample();
                example.Run();
            }

            {
                ParallelForExceptionExample example = new ParallelForExceptionExample();
                example.Run();
            }

            {
                PartitionerExample example = new PartitionerExample();
                example.Run();
            }
        }

        static void RunPLINQExamples()
        {
            {
                PLINQBasicExample example = new PLINQBasicExample();
                example.Run();
            }
            {
                PLINQExceptionExample example = new PLINQExceptionExample();
                example.Run();
            }
        }

        static void Main(string[] args)
        {
            RunTasksExamples();

            RunParallelExamples(); 
            
            RunPLINQExamples();
        }
    }
}
