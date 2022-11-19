using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBuilderExample
{
    internal class Program
    {
        private static void Main()
        {
            IEnumerable<Int32> numbers = Enumerable.Range(1, 100000);

            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            RunWithTimeTest(() => ConcatWithStringAddition(numbers), "ConcatWithStringAddition");
            RunWithTimeTest(() => ConcatWithStringBuilder(numbers), "ConcatWithStringBuilder");
            RunWithTimeTest(() => ConcatWithStringBuilderAggregation(numbers), "ConcatWithStringBuilderAggregation");
        }

        private static void RunWithTimeTest(Action action, String actionName)
        {
            Int32 gen0Collections = GC.CollectionCount(0);
            Int32 gen1Collections = GC.CollectionCount(1);
            Int32 gen2Collections = GC.CollectionCount(2);

            Console.WriteLine("{0} started", actionName);

            Stopwatch stopwatch = Stopwatch.StartNew();

            action();

            Console.WriteLine("{0} ran in {1}", actionName, stopwatch.Elapsed);

            Int32 deltaGen0Collections = GC.CollectionCount(0) - gen0Collections;
            Int32 deltaGen1Collections = GC.CollectionCount(1) - gen1Collections;
            Int32 deltaGen2Collections = GC.CollectionCount(2) - gen2Collections;
            Console.WriteLine("There were {0} gen 0 collections, {1} gen 1 collections, {2} gen 2 collections", deltaGen0Collections, deltaGen1Collections, deltaGen2Collections);
            Console.WriteLine();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void ConcatWithStringBuilder<T>(IEnumerable<T> words)
        {
            StringBuilder builder = new StringBuilder();

            foreach (T word in words)
            {
                builder.Append(word);
            }

            String result = builder.ToString();
        }

        private static void ConcatWithStringAddition<T>(IEnumerable<T> words)
        {
            String result = words.Select(number => number.ToString()).Aggregate((acc, word) => acc + word);
        }

        private static void ConcatWithStringBuilderAggregation<T>(IEnumerable<T> words)
        {
            String result = words.Aggregate(new StringBuilder(), (builder, word) => builder.Append(word), (builder) => builder.ToString());
        }
    }
}
