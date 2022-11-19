using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    internal sealed class PLINQExceptionExample
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
            CustomLogger.LogStartExample("PLINQ exception example");

            try
            {
                IEnumerable<int> sourceData = PrepareData();
                IEnumerable<double> results = sourceData.AsParallel()
                    .Select(item =>
                    {
                        if (item == 5)
                        {
                            throw new Exception("Exception from PLINQ");
                        }
                        return Math.Pow(item, 2);
                    });
                PrintResults(results);
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

