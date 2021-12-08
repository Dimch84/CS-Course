using System.Collections.Generic;
using Algorithms.Numeric;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class CatalanNumbersTest
    {
        [Fact]
        public static void DoTest()
        {
            var list = CatalanNumbers.GetRange(0, 30);
            var list2 = new List<ulong>();

            // TRY CALCULATING FROM Bin.Coeff.
            for (uint i = 0; i < list.Count; ++i)
            {
                var catalanNumber = CatalanNumbers.GetNumberByBinomialCoefficients(i);
                list2.Add(catalanNumber);

                Assert.True(list[(int)i] == list2[(int)i], 
                    $"Wrong calculation. Iteration {i}, num1 {list[(int)i]}, num2 {list2[(int)i]}");
            }
        }
    }
}