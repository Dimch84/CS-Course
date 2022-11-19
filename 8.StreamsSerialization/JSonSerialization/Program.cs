using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
            {
                new Person("Ann", Gender.Woman, 21, "a@gmail.com"),
                new Person("Bill", Gender.Man, 24, "b@yahoo.com"),
                new Person("Clay", Gender.Man, 54, "c@gmail.com"),
                new Person("Dan", Gender.Man, 22, "d@yahoo.com"),
                new Person("Eugene", Gender.Woman, 65, "e@gmail.com"),
                new Person("Frank", Gender.Man, 34, "f@yahoo.com"),
                new Person("Glen", Gender.etc, 32, "g@gmail.com"),
                new Person("Hank", Gender.Man, 21, "h@yahoo.com")
            };

            Company microsoft = new Company("Microsoft");
            Company apple = new Company("Apple");

            people.ForEach(x => {
                if (x.Age > people.Average(a => a.Age))
                    x.SetCompany(microsoft);
                else
                    x.SetCompany(apple);
            });

            var msFile = "../../../../Data/microsoft";
            var appleFile = "../../../../Data/apple";

            JsonSerialize(msFile, microsoft);
            JsonSerialize(appleFile, apple);

            Company appleFromFile = JsonDeserialize<Company>(appleFile);

            Console.WriteLine(appleFromFile.Id == apple.Id &&
                appleFromFile.People.Count == apple.People.Count);

            // check people list serialization
            var peopleFile = "../../../../Data/peopleJson";
            JsonSerialize(peopleFile, people);
            var peopleFromFile = JsonDeserialize<List<Person>>(peopleFile);
            Console.WriteLine(people.Count == peopleFromFile.Count);
        }

        #region Serialization

        public static void JsonSerialize<T>(string path, T obj) where T : class
        {
            using (var fs = new FileStream($"{path}.json", FileMode.OpenOrCreate))
            {
                string strObj = JsonConvert.SerializeObject(obj, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                byte[] data = strObj
                    .Select(x => (byte)x)
                    .ToArray();
                fs.Write(data, 0, data.Length);
            }
        }

        public static T JsonDeserialize<T>(string path)
        {
            using (var streamReader = new StreamReader($"{path}.json"))
            {
                var startMemory = GC.GetTotalMemory(true);
                
                string dataStr = streamReader.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(dataStr);

                var endMemory = GC.GetTotalMemory(true);
                Console.WriteLine($"Total memory used: {endMemory - startMemory}");

                return result;
            }
        }

        #endregion
    }
}
