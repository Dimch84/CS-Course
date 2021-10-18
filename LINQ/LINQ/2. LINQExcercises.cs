using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace LINQ_Samples
{
    public class Student
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int Group { get; set; }

        public string Phone { get; set; }

        public int[] Marks { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age}, {Phone}";
        }
    }

    [TestFixture]
    public class LINQExcercise
    {
        private static readonly List<Student> Students= new List<Student>
        {
            new Student(){ FirstName = "John", LastName = "Doe", Age = 21, Group = 2, Phone = "89211234332", Marks = new int[]{ 5, 5, 3, 4} },
            new Student(){ FirstName = "Ivan", LastName = "Ivanov", Age = 26, Group = 1, Phone = "89261234332", Marks = new int[]{ 5, 5, 4, 4} },
            new Student(){ FirstName = "Andrew", LastName = "Gates", Age = 23, Group = 3, Phone = "89811234332", Marks = new int[]{ 5, 3, 3, 4} },
            new Student(){ FirstName = "Jane", LastName = "Rainman", Age = 25, Group = 1, Phone = "89211234332", Marks = new int[]{ 5, 5, 5, 4} },
            new Student(){ FirstName = "Smith", LastName = "Karter", Age = 22, Group = 2, Phone = "89991834332", Marks = new int[]{ 5, 5, 3, 5} },
            new Student(){ FirstName = "Alex", LastName = "Ivanov", Age = 24, Group = 2, Phone = "+79211234332", Marks = new int[]{ 5, 5, 5, 5} },
            new Student(){ FirstName = "Diane", LastName = "Lee", Age = 18, Group = 3, Phone = "89111234332", Marks = new int[]{ 3, 3, 4, 2} }
        };

        // Print all students from group number 2. Use LINQ. 
        // Order the students by FirstName.
        [Test]
        public void Test1_OrderStudents()
        {
            var groupToPrint = 2;

            //LINQ Extension Methods
            Console.WriteLine(string.Join(" | ",
                Students
                .Where(st => st.Group == groupToPrint)
                .OrderBy(st => st.FirstName)
                .Select(st => $"{st.FirstName} {st.LastName}")
                )
            );

            //LINQ Query Operators
            IEnumerable<string> selectedStudents = from s in Students
                               where s.Group == groupToPrint
                               orderby s.FirstName
                               select $"{s.FirstName} {s.LastName}";

            Console.WriteLine(string.Join(" | ", selectedStudents));
        }

        // Print all students whose first name is before their last name lexicographically        [Test]
        [Test]
        public void Test2_OrderStudents()
        {
            Console.WriteLine(string.Join(Environment.NewLine, 
                Students
                .Where(st => st.FirstName.CompareTo(st.LastName) < 0)
                .Select(st => $"{st.FirstName} {st.LastName}")));
        }

        // Find all students with age between 21 and 23. 
        // The query should return the first name, last name and age
        [Test]
        public void Test3_FilterByAge()
        {
            Console.WriteLine(string.Join(Environment.NewLine,
                Students
                .Where(st => st.Age >= 21 && st.Age <= 23)
                .OrderBy(st => st.Age)
                .Select(st => $"{st.FirstName} {st.LastName} {st.Age}")));
        }

        // Sort students first by last name in ascending order and then by first name in descending order
        [Test]
        public void Test4_FilterByAge()
        {
            Console.WriteLine(string.Join(Environment.NewLine, 
                Students
                .OrderBy(st => st.LastName)
                .ThenByDescending(st => st.FirstName)
                .Select(st => $"{st.FirstName} {st.LastName}")));
        }

        // Print all students with Megafon phone numbers (starting with 8921 or +7921)
        [Test]
        public void Test5_FilterByAge()
        {
            Console.WriteLine(string.Join(Environment.NewLine,
                Students
                .Where(st => st.Phone.StartsWith("8921") || st.Phone.StartsWith("+7921"))
                .Select(st => $"{st.FirstName} {st.LastName} Phone: {st.Phone}")));
        }

        // Extract all students with at least 2 marks under or equal to "3"
        [Test]
        public void Test6_WeakStudents()
        {
            Console.WriteLine(string.Join(Environment.NewLine, 
                Students
                .Where(s => s.Marks.Where(n => n <= 3).Count() >= 2)
                .Select(st => $"{st.FirstName} {st.LastName}")));
        }

        // Print all students in groups: GroupNo - Student1, Student2 etc.
        [Test]
        public void Test7_StudentGroups()
        {
            Console.WriteLine(string.Join(Environment.NewLine, 
                Students
                .GroupBy(st => st.Group)
                .OrderBy(g => g.Key)
                .Select(g => $"{g.Key} - {string.Join(", ", g.Select(st => st.FirstName))}")));

            Console.WriteLine();

            //LINQ Query Operators
            var query = from st in Students
                    group st by st.Group into g
                    orderby g.Key
                    select new { GKey = g.Key, Persons = g.Select(st => st.FirstName) };
            
            Console.WriteLine(string.Join(Environment.NewLine,
                query.Select(g => $"{g.GKey} - {string.Join(", ", g.Persons)}")));
        }

        // Aggregation example
        [Test]
        public void Test8_WeakStudents()
        {
            Console.WriteLine(Students.Aggregate((x, y) =>
                new Student() { FirstName = x.FirstName + y.FirstName, Age = x.Age + y.Age, Phone = x.Phone })
                .ToString());
        }

        /*As you probably know Little John is the right hand of the famous English hero - Robin Hood. A little known fact is that Little John can't handle Math very well. Before Robin Hood left to see Marry Ann, he asked John to count his hay of arrows and send him an encrypted message containing the arrow's count. The message should be encrypted since it can be intercepted by the Nottingham’s evil Sheriff. Your task is to help Little John before it is too late (0.10 sec).
        You are given an input array of strings (hay). Those strings may or may not contain arrows. The arrows can be of different type as follows:

        ">----->" – a small arrow
        ">>----->" – a medium arrow
        ">>>----->>" – a large arrow
        Note that the body of each arrow will always be 5 dashes long. The difference between the arrows is in their tip and tail. The given 3 types are the only ones you should count, the rest should be ignored (Robin Hood does not like them). You should start searching the hays from the largest arrow type down to the smallest arrow type.*/
        [Test]
        public void Test9_LittleJohn()
        {
            var arrowInputs = new string[]{ ">>>----->>abc>>>----->>",
                    ">>>----->>", "> ----->s", ">>----->", "asd >>----->ddve"};
            Console.WriteLine(string.Join(Environment.NewLine,
                            GetArrows(arrowInputs)
                            .Select(a => $"{a.Type}, {a.Amount}"))
                );
        }

        class Arrow
        {
            public string Type { get; set; }

            public int Amount { get; set; }
        }

        private static Arrow[] GetArrows(string[] hay)
        {
            var arrows = new Arrow[]
            {
                new Arrow { Type = ">----->", Amount = 0 },
                new Arrow { Type = ">>----->", Amount = 0 },
                new Arrow { Type = ">>>----->>", Amount = 0 }
            };

            for (int i = 0; i < hay.Length; i++)
            {
                var matches = Regex.Matches(hay[i], $"(>----->)|(>>----->)|(>>>----->>)");

                foreach (Match mathc in matches)
                {
                    arrows.Where(a => a.Type == mathc.Value).First().Amount++;
                }
            }

            return arrows;
        }
    }
}
