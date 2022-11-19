using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    internal sealed class PLINQBasicExample
    {
        private IEnumerable<int> PrepareData()
        {
            int[] sourceData = new int[10];
            for (int i = 0; i < sourceData.Length; i++)
            {
                sourceData[i] = i;
            }
            return sourceData;
        }

        private void PrintResults(IEnumerable<double> results)
        {
            String strResults = String.Join<double>(", ", results);
            CustomLogger.LogMessage("Results: {0}", strResults);
        }

        public void Run()
        {
            CustomLogger.LogStartExample("PLINQ basic example");

            IEnumerable<int> sourceData = PrepareData();
            IEnumerable<double> results = sourceData.AsParallel()
                .WithDegreeOfParallelism(2)
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));
            PrintResults(results);

            CustomLogger.LogEndExample();
            Console.ReadKey();

            CustomLogger.LogStartExample("PLINQ: Ordered");            
            IEnumerable<double> results2 = sourceData.AsParallel()
                .AsOrdered()
                .WithDegreeOfParallelism(2)
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));
            PrintResults(results2);

            CustomLogger.LogEndExample();
            Console.ReadKey();
        }
    }
}
