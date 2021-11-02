using NUnit.Framework;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StreamsSerialization
{
    [TestFixture]
    public class Streams
    {
        private const string TestFilePath = @"..\..\..\..\Data";

        [Test]
        public void Test1_StreamReader()
        {
            using (StreamReader reader = new StreamReader(Path.Combine(TestFilePath, "text_file.txt")))
            {
                int lineNumber = 0;
                string line = reader.ReadLine();
                while (line != null)
                {
                    lineNumber++;
                    Console.WriteLine("Line {0}: {1}", lineNumber, line);
                    line = reader.ReadLine();
                }
            }
        }

        [Test]
        public void Test2_StreamWriter()
        {
            using (var reader = new StreamReader(Path.Combine(TestFilePath, "text_file.txt")))
            {
                using (var writer = new StreamWriter(Path.Combine(TestFilePath, "reversed_file.txt")))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        for (int i = line.Length - 1; i >= 0; i--)
                        {
                            writer.Write(line[i]);
                        }

                        writer.WriteLine();
                        line = reader.ReadLine();
                    }
                }
            }
        }

        [Test]
        public void Test3_FileStream()
        {
            string text = "А вот текст с буквами Ы, Ъ, Ю и Щ!";
            var fileStream = new FileStream(Path.Combine(TestFilePath, "test_text.txt"), FileMode.Create);
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                fileStream.Write(bytes, 0, bytes.Length);
            }
            finally
            {
                fileStream.Close();
            }
        }

        [Test]
        public void Test3_FileStream2()
        {
            StringBuilder sb = new StringBuilder();
            using (FileStream fileStream = File.OpenRead(Path.Combine(TestFilePath, "text_file.txt")))
            {
                byte[] buffer = new byte[50];
                while (fileStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    char[] chars =
                        ASCIIEncoding.ASCII.GetChars(buffer, 0, buffer.Length);
                    sb.Append(chars, 0, chars.Length);
                    sb.Append(Environment.NewLine);
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }

            Console.WriteLine(sb.ToString());
        }
        [Test]
        public void Test4_CopyFile()
        {
            using (var source = new FileStream(Path.Combine(TestFilePath, "sheep.jpg"), FileMode.Open))
            {
                using (var destination = new FileStream(Path.Combine(TestFilePath, "result.jpg"), FileMode.Create))
                {
                    double fileLength = source.Length;
                    byte[] buffer = new byte[4096];
                    while (true)
                    {
                        int readBytes = source.Read(buffer, 0, buffer.Length);
                        if (readBytes == 0)
                        {
                            break;
                        }

                        destination.Write(buffer, 0, readBytes);

                        Console.WriteLine("{0:P}", Math.Min(source.Position / fileLength, 1));
                    }
                }
            }
        }

        [Test]
        public void Test5_MemoryStream()
        {
            string text = 
                @"Let me not to the marriage of true minds
Admit impediments; love is not love
Which alters when it alteration finds,
Or bends with the remover to remove";

            byte[] bytes = Encoding.UTF8.GetBytes(text);

            using (var memoryStream = new MemoryStream(bytes))
            {
                while (true)
                {
                    int readByte = memoryStream.ReadByte();
                    if (readByte == -1)
                    {
                        break;
                    }

                    // ToString("X") converts the Integer to Hexadecimal
                    Console.WriteLine(readByte.ToString("X"));
                }
            }
        }

        [Test]
        public void Test6_EncryptingStream()
        {
            string EncryptionKey = "ABCDEFGH";

            SaveEncrypted("Hi! Here is my encrypted text", EncryptionKey, Path.Combine(TestFilePath, "encrypted.txt"));

            string result = Decrypt(EncryptionKey, Path.Combine(TestFilePath, "encrypted.txt"));
            Console.WriteLine(result);
        }

        static void SaveEncrypted(string text, string key, string path)
        {
            using (var destinationStream =
                new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var cryptoProvider = new DESCryptoServiceProvider();

                cryptoProvider.Key = Encoding.ASCII.GetBytes(key);
                cryptoProvider.IV = Encoding.ASCII.GetBytes(key);

                using (CryptoStream cryptoStream = new CryptoStream(destinationStream,
                   cryptoProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] data = Encoding.ASCII.GetBytes(text);

                    cryptoStream.Write(data, 0, data.Length);
                }
            }
        }

        static string Decrypt(string key, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var cryptoProvider = new DESCryptoServiceProvider();
                cryptoProvider.Key = Encoding.ASCII.GetBytes(key);
                cryptoProvider.IV = Encoding.ASCII.GetBytes(key);

                using (var cryptoStream = new CryptoStream(fileStream, cryptoProvider.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(cryptoStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        [Test]
        public void Test7_ZipAndUnzip()
        {
            var zippedFile = Path.Combine(TestFilePath, "zipped.gz");
            var unZippedFile = Path.Combine(TestFilePath, "unzipped.txt");

            Compress(Path.Combine(TestFilePath, "text_file.txt"), zippedFile);

            Decompress(zippedFile, unZippedFile);
        }

        static void Decompress(string inputFile, string outputFile)
        {
            using (var inputStream = new FileStream(inputFile, FileMode.Open))
            {
                using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress, false))
                {
                    using (var outputStream = new FileStream(outputFile, FileMode.Create))
                    {
                        byte[] buffer = new byte[4096];
                        while (true)
                        {
                            int readBytes = compressionStream.Read(buffer, 0, buffer.Length);
                            if (readBytes == 0)
                            {
                                break;
                            }

                            outputStream.Write(buffer, 0, readBytes);
                        }
                    }
                }
            }
        }

        static void Compress(string inputFile, string outputFile)
        {
            using (var inputStream = new FileStream(inputFile, FileMode.Open))
            {
                using (var outputStream = new FileStream(outputFile, FileMode.Create))
                {
                    using (var compressionStream = new GZipStream(outputStream, CompressionMode.Compress, false))
                    {
                        byte[] buffer = new byte[4096];
                        while (true)
                        {
                            int readBytes = inputStream.Read(buffer, 0, buffer.Length);
                            if (readBytes == 0)
                            {
                                break;
                            }

                            compressionStream.Write(buffer, 0, readBytes);
                        }
                    }
                }
            }
        }

        [Test]
        public void Test8_AppendToFile()
        {
            var filePath = Path.Combine(TestFilePath, "text_file.txt");

            string data = "My test data to add at the end of the file\n";
            byte[] array = data.Select(x => (byte)x).ToArray();

            using (var fileWriter = new FileStream(filePath, FileMode.Append))
            {
                fileWriter.Write(array, 0, array.Length);
            }

            // alternative. with possibility to replace data from the middle
            using (var fileWriter = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                long fileDataLength = fileWriter.Length;
                fileWriter.Seek(fileDataLength, SeekOrigin.Begin);
                fileWriter.Write(array, 0, array.Length);
            }
        }

        [Test]
        public void Test9()
        {

        }

        [Test]
        public void Test10()
        {

        }

        /*
                [Test]
                public void Test3()
                {

                }
        */

    }
}
