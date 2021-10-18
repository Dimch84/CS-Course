using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LINQ_Samples
{
    [TestFixture]
    public class LINQLabs
    {
        //Read a sequence of integers, given on a single line separated by a space.
        //Finds all unique elements, that are in range [10, 20] inclusive and print only the first 2 elements.
        //If there are fewer than 2 elements, print as much as there are. If there are no elements, print nothing.
        [Test]
        public void Test1_TakeTwo()
        {
            var str = "1 12 32 8 43 16 14 32 12 17 74";

            //LINQ Extension Methods
            IEnumerable<int> selectedELements = str.Split()
                .Select(int.Parse)
                .Where(n => n >= 10 && n <= 20)
                .Distinct()
                .Take(2);

            Console.WriteLine(string.Join(" ", selectedELements));
        }

        //Read a sequence of names, given on a single line, separated by a space.
        //Read a sequence of letters, given on the next line, separated by a space.
        //Find the names that start with one of the given letters and print the first of them (ordered lexicographically).
        //If there is no name that conforms to the requirement, print "No match".
        [Test]
        public void Test2_FirstName()
        {
            var strNames = "John Mike James David Lu Andrew";
            var strLetters = "J K A L";

            var names = strNames.Split();
            var letters = strLetters.ToLower().ToCharArray().Where(c => c != ' ').ToArray();
            var filteredNames = names.Where(n => letters.Contains(char.ToLower(n[0]))).ToArray();

            Console.WriteLine((filteredNames.Length == 0)
                ? "No match"
                : filteredNames.OrderBy(n => n).First());
        }

        //Read a sequence of double numbers, given on a single line, separated by a space.
        //Find the average of all elements, using LINQ.
        //Round the output to the second digit after the decimal separator.
        [Test]
        public void Test3_AverageDoubles()
        {
            var strNumbers = "12,2 24,5 32,5 2,8 12,3465 87,1342";
            var numbers = strNumbers.Split().Select(double.Parse);
            Console.WriteLine($"{numbers.Average():F2}");
        }

        //Read a sequence of numbers, given on a single line, separated by a space.
        //Find the smallest number of all even numbers, using LINQ.
        //If there are no numbers in the sequence, print "No match".
        //Numbers in the output should be formatted with 2 decimal places after floating point.
        [Test]
        public void Test4_MinEvenNumber()
        {
            var strNumbers = "12 7 9 11 23 24 2 6 26";

            var evenNumbers = strNumbers
                .Split()
                .Select(double.Parse)
                .Where(n => n % 2 == 0)
                .ToArray();

            Console.WriteLine(evenNumbers.Length == 0
                ? "No match"
                : $"{evenNumbers.Min():F2}");
        }

        //Read a sequence of elements, given on a single line, separated by a space.
        //Filter all elements that are integers and calculate their sum, using LINQ.
        //If there are no numbers in the sequence, print "No match".
        [Test]
        public void Test5_FindAndSumIntegers()
        {
            var strAny = "a 12 Joe12 32,4 Adxf 321 AaA 65,78 222";

            var numbers = strAny
                .Split()
                .Select(str =>
                {
                    double number;
                    var isNumber = double.TryParse(str, out number);
                    return new { number, isNumber };
                })
                .Where(obj => obj.isNumber)
                .Select(obj => obj.number)
                .ToArray();

            Console.WriteLine(numbers.Length == 0
                ? "No match"
                : $"{numbers.Sum()}");
        }

        //On the first line, read two numbers, a lower and an upper bound, separated by a space.
        //The bigger number is the upper bound and the smaller number is the lower bound.
        //On the second line, read a sequence of numbers, separated by a space.
        //Print all numbers, such that [lower bound] ≤ n ≤ [upper bound].
        [Test]
        public void Test6_BoundedNumbers()
        {
            var boundaries = "32 10"
               .Split()
               .Select(int.Parse)
               .ToArray();

            if (boundaries.Length != 2)
            {
                return;
            }

            var numbers = "14 8 3 65 100 3 56 6 9 24 120"
                .Split()
                .Select(int.Parse)
                .Where(n => n >= boundaries[0] && n <= boundaries[1] ||
                        n >= boundaries[1] && n <= boundaries[0])
                .ToArray();

            Console.WriteLine(string.Join(" ", numbers));
        }

        //On the first line, you are given the population of districts in different cities, 
        //separated by a single space in the format "city(district):population".
        //On the second line, you are given the minimum population for filtering of the towns.
        //The population of a town is the sum of populations of all of its districts.
        //Print all cities with population greater than a given number on the second line. 
        //Sort cities and districts by descending population and print top 5 districts for a given city.
        [Test]
        public void Test7_TownsPopulation()
        {
            var townsPopulation = GetTownsPopulation();
            var minBorder = 38;

            var sums = townsPopulation.ToDictionary(x => x.Key, x => x.Value.Sum());

            Console.WriteLine(string.Join(Environment.NewLine, townsPopulation
                .Where(kvp => sums[kvp.Key] >= minBorder)
                .OrderByDescending(kvp => sums[kvp.Key])
                .Select(t => $"{t.Key}: {string.Join(" ", t.Value.OrderByDescending(n => n).Take(5))}")));
        }

        private static Dictionary<string, List<long>> GetTownsPopulation()
        {
            var inputData = "Spb:12 Msk:24 Kzn:20 Kzn:32 Nsk:36 Spb:321 Msk:500 Sch:20 Msk:240 Msk:12 Spb:12 Spb:123 Msk:566 Spb:21 Msk:78 Msk:321 Spb:24 Spb:12 Spb:11 Spb:2 Spb:1 Spb:6 Msk:89";
            var districtsPopulation = inputData.Split();
            var townsPopulation = new Dictionary<string, List<long>>();

            foreach (var district in districtsPopulation)
            {
                var indexOfSeparation = district.IndexOf(':');

                if (indexOfSeparation < 0)
                {
                    continue;
                }

                var town = district.Substring(0, indexOfSeparation);
                var currentPopulation = int.Parse(district.Substring(indexOfSeparation + 1));

                if (!townsPopulation.ContainsKey(town))
                {
                    townsPopulation[town] = new List<long>();
                }

                townsPopulation[town].Add(currentPopulation);
            }

            return townsPopulation;
        }
    }
}
