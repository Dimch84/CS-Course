using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Collections
{
    class TestCollections
    {
        static void Main(String[] args)
        {
            try
            {
                TestArray();
                Test2DArray();
                TestListExpand();
                TestListRemove();
                TestDictionary();
                TestEnumerator();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: " + ex);
            }
        }

        private static void TestArray()
        {
            String[] array1 = new[] {"Hello", "World!"};
            String[] array2 = new[] {"Hello", "World!"};

            Console.WriteLine(array1 == array2);
            Console.WriteLine(array1.SequenceEqual(array2));

            Array.Reverse(array1);
            Array.Resize(ref array2, array2.Length + array1.Length);
            Array.Copy(array1, 0, array2, array1.Length, array1.Length);

            Array.Sort(array2);
            Console.WriteLine(array2.FormatString(" "));

            var index = Array.BinarySearch(array1, "World!");
            Console.WriteLine($"Index of the \"World!\" is {index}.");
        }

        private static void Test2DArray()
        {
            String[,] table = new String[10, 20];
            Array baseArray = table;

            Int32 width = baseArray.GetLength(0);
            Int32 height = baseArray.GetLength(1);
            for (int i = 0; i < width * height; i++)
            {
                var x = i % width;
                var y = i / width;

                baseArray.SetValue(i.ToString("D3"), new[] {x, y});

                if (x == 0 && i > 0)
                    Console.WriteLine();

                Console.Write(table[x, y]);
                Console.Write(' ');
            }

            String[][] array2D = new String[10][];
            baseArray = array2D;

            for (int i = 0; i < baseArray.Length; i++)
            {
                String[] line = new String[i];
                for (Int32 n = 0; n < i; n++)
                    line[n] = i.ToString();

                array2D[i] = line;

                line = (String[]) baseArray.GetValue(i);
                Console.WriteLine(line.FormatString(" "));
            }
        }

        private static void TestListExpand()
        {
            Random rnd = new Random();
            Stopwatch sw = Stopwatch.StartNew();

            List<TimeSpan> time = new List<TimeSpan>();

            List<Int32> list = new List<Int32>();
            for (TimeSpan maxTime = TimeSpan.Zero; maxTime.TotalMilliseconds < 100;)
            {
                sw.Restart();
                list.Add(rnd.Next(Int32.MinValue, Int32.MaxValue));
                sw.Stop();

                if (sw.Elapsed.Ticks > maxTime.Ticks * 1.2)
                {
                    maxTime = sw.Elapsed;
                    time.Add(maxTime);
                }
            }

            Console.WriteLine(time.Select(t => t.TotalMilliseconds).FormatString(Environment.NewLine));
            Console.WriteLine($"List size is {list.Count}");

            list.Clear();
            list.TrimExcess();
        }

        private static void TestListRemove()
        {
            Random rnd = new Random();

            List<Int32> list = new List<Int32>(capacity: 1000);

            while (list.Count < list.Capacity)
                list.Add(rnd.Next(Int32.MinValue, Int32.MaxValue));

            try
            {
                var negativeNumbers = list.Where(item => item < 0);
                foreach (var number in negativeNumbers)
                    list.Remove(number);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }

            for (Int32 i = list.Count - 1; i >= 0; i--)
            {
                Int32 item = list[i];
                if (item < 0)
                    list.RemoveAt(i);
            }

            HashSet<Int32> unique = new HashSet<Int32>(list);
            Console.WriteLine(unique.OrderBy(item => item).FormatString());
        }

        private static void TestDictionary()
        {
            Dictionary<MyKey, String> descriptions = new Dictionary<MyKey, String>(capacity: 1);

            for (int i = 0; i < 100; i++)
            {
                MyKey key = new MyKey(i, "The Answer");
                String description = "Undefined key " + i;

                if (i == 42)
                    description = "The answer to life, universe and everything.";

                descriptions.Add(key, description);
            }

            if (descriptions.TryGetValue(new MyKey(42, "The Answer"), out var desc))
                Console.WriteLine(desc);
            else
                throw new ArgumentException("Cannot find the answer to life.");

            MyKey[] keysToRemove = descriptions.Where(p => p.Value.StartsWith("Undefined")).Select(p => p.Key).ToArray();
            foreach (var key in keysToRemove)
                descriptions.Remove(key);
        }

        private static void TestEnumerator()
        {
            IEnumerable coroutine = Coroutine();
            IEnumerator enumerator = coroutine.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine("Do something");
            }
        }

        private static IEnumerable Coroutine()
        {
            Console.WriteLine("Begin");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Progress: {i * 20}%");
                yield return i;
            }

            Console.WriteLine("End");
        }
    }
}