using System.Collections.Generic;
using System.Linq;

namespace Generations
{
    internal class Customer
    {
        private readonly int[] achievements;

        public Customer()
        {
            achievements = Enumerable.Range(1, 1000).ToArray();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public IEnumerable<int> Achievements { get { return achievements; } }
    }
}
