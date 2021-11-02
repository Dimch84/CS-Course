using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StreamsSerialization
{
    [TestFixture]
    class StreamsExamples
    {
        private const string TestFilePath = @"..\..\..\..\Data";

        [Test]
        public void Test1_SliceAssemble()
        {
            var sourceFile = Path.Combine(TestFilePath, "tiger.txt");

            Slice(sourceFile, TestFilePath, 3);

            var files = new List<string>() {
                Path.Combine(TestFilePath, "tiger.txt"),
                Path.Combine(TestFilePath, "text_file.txt")
            };
            Assemble(files, TestFilePath);
        }

        private static void Slice(string sourceFile, string destinationDirectory, int parts)
        {
            var extension = Path.GetExtension(sourceFile);

            using (var reader = new FileStream(sourceFile, FileMode.Open))
            {
                var partSize = reader.Length / parts + 1;

                for (int i = 1; i <= parts; i++)
                {
                    var outputFile = Path.Combine(destinationDirectory, $"Part {i}{extension}");

                    using (var writer = new FileStream(outputFile, FileMode.Create))
                    {
                        var buffer = new byte[100];

                        while (writer.Length < partSize)
                        {
                            var readBytes = reader.Read(buffer, 0, buffer.Length);

                            if (readBytes == 0)
                            {
                                break;
                            }

                            writer.Write(buffer, 0, readBytes);
                        }
                    }
                }
            }
        }

        private static void Assemble(List<string> files, string destinationDirectory)
        {
            var extension = Path.GetExtension(files[0]);
            var outputFile = Path.Combine(destinationDirectory, $"Assembled {DateTime.Now:dd-MM-yyyy - hh-mm}{extension}");

            try
            {
                using (var writer = new FileStream(outputFile, FileMode.CreateNew))
                {
                    foreach (var file in files)
                    {
                        try
                        {
                            using (var reader = new FileStream(file, FileMode.Open))
                            {
                                var buffer = new byte[100];
                                var readBytesCount = reader.Read(buffer, 0, buffer.Length);

                                while (readBytesCount != 0)
                                {
                                    writer.Write(buffer, 0, readBytesCount);
                                    readBytesCount = reader.Read(buffer, 0, buffer.Length);
                                }
                            }

                            // keep blank line between files
                            writer.Write(new byte[] { (byte)'\n', (byte)'\n' }, 0, 2);
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine($"File {file} cannot be found.{Environment.NewLine}The Assemble will be completed without this file.");
                        }
                    }
                }
            }
            catch (IOException)
            {
                destinationDirectory = Path.Combine(destinationDirectory, "Assemble");
                Assemble(files, destinationDirectory);
            }
        }


        [Test]
        public void Test2_DirectoryTraversal()
        {
            var files = GetFilesFromDirectory(@"..\..\..\..\StreamsSerialization");

            SaveReport(files, TestFilePath);
        }

        private static Dictionary<string, Dictionary<string, long>> GetFilesFromDirectory(string workDir)
        {
            var files = Directory.GetFiles(workDir);

            // The files should be grouped by their extension
            // Dictionary<extension, Dictionary<filePath, fileSize>>
            var result = new Dictionary<string, Dictionary<string, long>>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                var fileSize = new FileInfo(file).Length;

                if (!result.ContainsKey(extension))
                {
                    result[extension] = new Dictionary<string, long>();
                }

                result[extension][file] = fileSize;
            }

            return result;
        }

        private static void SaveReport(Dictionary<string, Dictionary<string, long>> files, string dir)
        {
            var report = Path.Combine(dir, "report.txt");

            using (var writer = new StreamWriter(report))
            {
                foreach (var group in files
                    .OrderByDescending(g => g.Value.Count).ThenBy(g => g.Key))
                {
                    var filesInGroup = string.Join(Environment.NewLine, group.Value
                        .OrderByDescending(f => f.Value)
                        .Select(kvp => $"--{kvp.Key} - {kvp.Value}kb"));

                    writer.Write($"{group.Key}{Environment.NewLine}{filesInGroup}{Environment.NewLine}");
                }
            }
        }

        [Test]
        public void Test3()
        {

        }

        [Test]
        public void Test4()
        {

        }

        [Test]
        public void Test5()
        {

        }

        [Test]
        public void Test6()
        {

        }

        [Test]
        public void Test7()
        {

        }

        [Test]
        public void Test8()
        {

        }

        [Test]
        public void Test9()
        {

        }

        [Test]
        public void Test10()
        {

        }

        [Test]
        public void Test11()
        {

        }

        [Test]
        public void Test12()
        {

        }
    }
}
