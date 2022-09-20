using System;
using System.Linq;
using DataStructures.HashTable;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class HashTableTestsFactory
    {
        Type[] hashTableTypes;
        public HashTableTestsFactory()
        {
            Type sorterType = typeof(IHashTable);

            this.hashTableTypes = AppDomain
               .CurrentDomain
               .GetAssemblies()
               .SelectMany(assembly => assembly.GetTypes())
               .Where(type => sorterType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
               .ToArray();
        }

        [Fact]
        public void HashTableCreationTest()
        {
            foreach (Type hashTableType in this.hashTableTypes)
            {
                IHashTable hashTable = Activator.CreateInstance(hashTableType) as IHashTable;

                Assert.NotNull(hashTable);
                Assert.False(hashTable.Contains(5),
                    $"hash table [{hashTableType.Name}] should not contains any value after creation");
            }
        }

        [Fact]
        public void HashTableAddingTest()
        {
            foreach (Type hashTableType in this.hashTableTypes)
            {
                IHashTable hashTable = Activator.CreateInstance(hashTableType) as IHashTable;
                Assert.True(hashTable.Add(5));

                Assert.True(hashTable.Contains(5),
                    $"hash table [{hashTableType.Name}] should contains added value");
            }
        }

        [Fact]
        public void HashTableClearTest()
        {
            foreach (Type hashTableType in this.hashTableTypes)
            {
                IHashTable hashTable = Activator.CreateInstance(hashTableType) as IHashTable;
                Assert.True(hashTable.Add(5));
                hashTable.Clear();

                Assert.True(hashTable.Count() == 0,
                    $"hash table [{hashTableType.Name}] should contains 0 elements after clearing");
            }
        }

        [Fact]
        public void HashTableRemovingTest()
        {
            foreach (Type hashTableType in this.hashTableTypes)
            {
                IHashTable hashTable = Activator.CreateInstance(hashTableType) as IHashTable;
                Assert.True(hashTable.Add(5));
                Assert.True(hashTable.Add(15));
                Assert.True(hashTable.Add(14));

                Assert.True(hashTable.Remove(15),
                    $"hash table [{hashTableType.Name}] should remove existed element");

                Assert.False(hashTable.Remove(17),
                    $"hash table [{hashTableType.Name}] should not be able to remove not existed element");

                Assert.True(hashTable.Contains(5),
                    $"hash table [{hashTableType.Name}] should contains not removed element");

                Assert.False(hashTable.Contains(15),
                    $"hash table [{hashTableType.Name}] should not contains removed element");
            }
        }

        [Fact]
        public void HashTableRebuildTest()
        {
            foreach (Type hashTableType in this.hashTableTypes)
            {
                IHashTable hashTable = Activator.CreateInstance(hashTableType, 2) as IHashTable;
                Assert.True(hashTable.Add(5));
                Assert.True(hashTable.Add(15));
                Assert.True(hashTable.Add(13));
                Assert.True(hashTable.Add(17));

                Assert.True(hashTable.Contains(15),
                    $"hash table [{hashTableType.Name}] should contains element 15 after rebuilding");

                Assert.True(hashTable.Contains(5),
                    $"hash table [{hashTableType.Name}] should contains element 5 after rebuilding");

                Assert.True(hashTable.Contains(13),
                    $"hash table [{hashTableType.Name}] should contains element 14 after rebuilding");
            }
        }

        [Fact]
        public void HashTableEnumeratorTest()
        {
            foreach (Type hashTableType in this.hashTableTypes)
            {
                IHashTable hashTable = Activator.CreateInstance(hashTableType, 2) as IHashTable;

                var items = new int[] { 5, 15, 13 };
                foreach (var item in items)
                {
                    hashTable.Add(item);
                }

                hashTable.Add(31);
                hashTable.Remove(31);

                foreach (var item in hashTable)
                {
                    Assert.True(items.Contains(item),
                        $"hash table [{hashTableType.Name}] should contains element [{item}] in enumerator");
                }
            }
        }

        [Fact]
        public void QuadraticHashTableOverloadingAddingTest()
        {
            IHashTable hashTable = new QuadraticHashTable(2);
            Assert.True(hashTable.Add(3));
            Assert.True(hashTable.Add(3));
            Assert.False(hashTable.Add(3), "quadritic hash table should not be able to add element");

            Assert.True(hashTable.Count() == 2, "quadritic hash table count should be 2 after not-successfull adding");
        }

        [Fact]
        public void QuadraticHashTableOverloadingContainsTest()
        {
            IHashTable hashTable = new QuadraticHashTable(2);
            Assert.True(hashTable.Add(3));
            Assert.True(hashTable.Add(3));
            Assert.False(hashTable.Add(6), "quadritic hash table should not be able to add element");

            Assert.False(hashTable.Contains(6), "quadritic hash table should not contains not added element");
        }

        [Fact]
        public void QuadraticHashTableOverloadingRemoveTest()
        {
            IHashTable hashTable = new QuadraticHashTable(2);
            Assert.True(hashTable.Add(3));
            Assert.True(hashTable.Add(3));
            Assert.False(hashTable.Add(6), "quadritic hash table should not be able to add element");

            Assert.False(hashTable.Remove(6), "quadritic hash table should not be able to remove not-added element");

            Assert.True(hashTable.Remove(3), "quadritic hash table should be able to remove existed element");

            Assert.True(hashTable.Add(3), "quadritic hash table should be able to add element again");

            Assert.True(hashTable.Count() == 2, "quadritic hash table has 2 elements after testing");
        }

        [Fact]
        public void DoubleHashTableOverloadingRemoveTest()
        {
            IHashTable hashTable = new DoubleHashTable(2);
            Assert.True(hashTable.Add(3));
            Assert.True(hashTable.Add(18));

            Assert.False(hashTable.Remove(16), "quadritic hash table should not be able to remove not-added element");

            Assert.True(hashTable.Remove(18), "quadritic hash table should be able to remove existed element");

            Assert.True(hashTable.Add(18), "quadritic hash table should be able to add element again");

            Assert.True(hashTable.Count() == 2, "quadritic hash table has 2 elements after testing");
        }
    }
}
