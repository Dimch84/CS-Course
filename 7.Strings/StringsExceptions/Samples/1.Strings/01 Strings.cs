using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Samples.Strings
{
    class _01_Strings
    {
        static void _Main(String[] args)
        {
            try
            {
                TestNumbers();
                TestEncoding();
                TestChars();
                TestRegex();
                TestUnsafe();
                TestIntern();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void TestNumbers()
        {
            try
            {
                const Double initialValue = 1.0 / 3.0;

                String displayValue = initialValue.ToString(CultureInfo.CurrentCulture);
                Console.WriteLine(displayValue);

                String serializedValue = initialValue.ToString(CultureInfo.InvariantCulture);

                if (Double.TryParse(serializedValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var parsedValue))
                {
                    Console.WriteLine("Parsed value {0} is {1} to initial value {2}.", parsedValue, (initialValue == parsedValue ? "equal" : "not equal"), initialValue);
                }
                else
                {
                    throw new Exception($"Cannot parse {nameof(Double)} from [{serializedValue}] string.");
                }

                Double value = parsedValue;

                StringBuilder sb = new StringBuilder(capacity: 1024);
                for (Int32 i = 2; i < 15; i++)
                    sb.AppendLine($"{value} ^ {i:D2} = {(parsedValue *= value):#.000}");

                Console.WriteLine(sb);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to test numbers: " + ex.Message);
            }
        }

        private static void TestEncoding()
        {
            try
            {
                Console.InputEncoding = Encoding.UTF8;
                Console.OutputEncoding = Encoding.UTF8;

                Encoding win1251 = Encoding.GetEncoding(1251);
                using (var ms = new MemoryStream(capacity: 1024))
                {
                    using (var sw = new StreamWriter(ms, win1251, bufferSize: 1024, leaveOpen: true))
                    using (var xw = new XmlTextWriter(sw))
                    {
                        xw.WriteStartElement("Элемент");
                        xw.WriteAttributeString("Тип", "Преступный");
                        xw.WriteAttributeString("Имя", "Вася");
                        xw.WriteEndElement();
                    }

                    Byte[] buffer = ms.GetBuffer();

                    String str1 = win1251.GetString(buffer, 0, (Int32) ms.Length);
                    Console.WriteLine(str1);

                    String str2 = Encoding.UTF8.GetString(buffer, 0, (Int32) ms.Length);
                    Console.WriteLine(str2);

                    String str3 = new UTF8Encoding(false, true).GetString(buffer, 0, (Int32) ms.Length);
                    Console.WriteLine(str3);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to test encoding: " + ex.Message);
            }
        }

        private static void TestChars()
        {
            const String text = "+7(812)777-55-33";

            Boolean isFirst = true;
            foreach (var ch in text)
            {
                switch (ch)
                {
                    case '+':
                        if (!isFirst)
                            ThrowError();
                        break;
                    case '-':
                    case '(':
                    case ')':
                        break;
                    default:
                        if (ch < '0' || ch > '9')
                            ThrowError();
                        break;
                }

                isFirst = false;

                void ThrowError() => throw new FormatException($"{text} is not a valid phone number.");
            }

            Console.WriteLine($"{text} is valid phone number.");
        }

        private static void TestRegex()
        {
            try
            {
                const String text = "+7(812)777-55-33";

                Regex regex = new Regex(@"(\+[0-9]{1})?\(?([0-9]+?\)?)?(([0-9-]){7,9})", RegexOptions.Compiled);

                CheckPhoneNumber("+7(812)777-55-33");
                CheckPhoneNumber("+78127775533");
                CheckPhoneNumber("8127775533");
                CheckPhoneNumber("777-55-33");
                CheckPhoneNumber("ABC-55-33");

                void CheckPhoneNumber(String value)
                {
                    if (regex.IsMatch(value))
                        Console.WriteLine($"{value} is valid phone number.");
                    else
                        throw new FormatException($"{value} is not a valid phone number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to test regex: " + ex.Message);
            }
        }

        private static void TestUnsafe()
        {
            unsafe
            {
                const String constStr = "My String";
                String str = (String) constStr.Clone();

                fixed (Char* ptr = str)
                {
                    for (int i = 0; i < str.Length; i++)
                        ptr[i] = 'Z';
                }

                Console.WriteLine("My String");
            }
        }

        private static void TestIntern()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                String.Intern(rnd.Next(10, 20).ToString());
            }

            List<Int32> interned = new List<Int32>(capacity: 10);
            List<Int32> notInterned = new List<Int32>(capacity: 10);
            for (int i = 0; i < 20; i++)
            {
                if (String.IsInterned(i.ToString()) is null)
                    notInterned.Add(i);
                else
                    interned.Add(i);
            }

            Console.WriteLine($"Interned {String.Join(", ", interned)}");
            Console.WriteLine($"Not Interned {String.Join(", ", notInterned)}");
        }
    }
}