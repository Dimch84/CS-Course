using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Samples.Exceptions
{
    class _02_Exceptions
    {
        private static List<Exception> s_exceptions = new List<Exception>();

        static void _Main(String[] args)
        {
            HandleExceptions(() => Arguments.NotNullOrEmpty(() => args));

            HandleExceptions(() =>
            {
                var rar = DataProvider.FindRarFiles();
                Arguments.NotNullOrEmpty(() => rar);
            });

            //HandleExceptions(() =>
            //{
            //    var zip = DataProvider.FindZipFiles();
            //    Arguments.NotNullOrEmpty(() => zip);
            //});

            HandleExceptions(() =>
            {
                Task task = Task.Run(() => Arguments.NotNullOrEmpty(() => args));
                task.Wait();
            });

            PrintExceptions();

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }

        private static void PrintExceptions()
        {
            try
            {
                if (s_exceptions.Count == 1)
                {
                    ExceptionDispatchInfo dispatchInfo = ExceptionDispatchInfo.Capture(s_exceptions[0]);
                    dispatchInfo.Throw();
                }
                else
                {
                    throw new AggregateException(s_exceptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("Someting went wrong: " + ex);
            }
        }

        static void HandleExceptions(Action action)
        {
            try
            {
                action();
            }
            catch (ArgumentEmptyException ex)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, ex);

                    ms.Position = 0;
                    ArgumentEmptyException ds = (ArgumentEmptyException) bf.Deserialize(ms);

                    s_exceptions.Add(ex);
                }
            }
            catch(AggregateException ex)
            {
                s_exceptions.Add(ex);
            }
        }
    }
}