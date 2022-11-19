using Common;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Tasks
{
    internal sealed class PartitionerExample
    {        
        public void Run()
        {
            CustomLogger.LogStartExample("Partitioner example");

            try
            {                
                double[] resultData = new double[10000000];
                
                OrderablePartitioner<Tuple<int, int>> chunkPart = Partitioner.Create(0, resultData.Length, 10000);
                ParallelLoopResult loopResult = Parallel.ForEach(chunkPart, chunkRange => 
                {                    
                    for (int i = chunkRange.Item1; i < chunkRange.Item2; i++)
                    {
                        resultData[i] = Math.Pow(i, 2);
                    }
                });

                CustomLogger.LogMessage("LoopResult1: '{0}'", loopResult.IsCompleted);
            }
            catch (Exception exc)
            {
                CustomLogger.LogException(exc);
            }

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
