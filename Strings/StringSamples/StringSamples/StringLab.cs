using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace StringSamples
{
    [TestFixture]
    public class StringLab
    {
        // Write a program that parses an URL address given in the format: [protocol]://[server]/[resource] 
        // and extracts from it the [protocol], [server] and [resource] elements.
        [TestCase("https://emkn.ru/teaching/assignments/")]
        [TestCase("https://www.google.bg/search?q=google&oq=goo&aqs=chrome.0.0j69i60l2://j0j69i57j69i65.2112j0j7&sourceid=chrome")]
        public void Test1_ParseURLs(string sUrl)
        {
            var url = sUrl.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);

            if (url.Length != 2)
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var protocol = url[0];

            var indexOfServerEnd = url[1].IndexOf('/');

            if (indexOfServerEnd < 0)
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            var server = url[1].Substring(0, indexOfServerEnd);

            var resources = string.Empty;

            if (indexOfServerEnd != url[1].Length - 1)
            {
                resources = url[1].Substring(indexOfServerEnd + 1);
            }

            Console.WriteLine($"Protocol = {protocol}");
            Console.WriteLine($"Server = {server}");
            Console.WriteLine($"Resources = {resources}");

        }

        public const string OpenTag = "<upcase>";
        public const string CloseTag = "</upcase>";

        // You are given a text. Write a program that changes the text in all regions 
        // surrounded by the tags <upcase> and </upcase> to upper-case.
        [TestCase(@"<upcase>StringBuilder</upcase> is <upcase>awesome</upcase> indeed")]
        public void Test2_Tags(string text)
        {
            var indexOfOpenTag = text.IndexOf(OpenTag);

            while (indexOfOpenTag >= 0)
            {
                var indexOfTextEnd = text.IndexOf(CloseTag, indexOfOpenTag);

                if (indexOfTextEnd < 0)
                {
                    break;
                }

                var indexOfCloseTagEnd = indexOfTextEnd + CloseTag.Length;
                var indexOfTextStart = indexOfOpenTag + OpenTag.Length;

                var textToReplace = text.Substring(indexOfOpenTag, indexOfCloseTagEnd - indexOfOpenTag);
                var replacedText = text.Substring(indexOfTextStart, indexOfTextEnd - indexOfTextStart).ToUpper();

                text = text.Replace(textToReplace, replacedText);

                indexOfOpenTag = text.IndexOf(OpenTag);
            }

            Console.WriteLine(text);

        }

        // Concatenate strings
        [Test]
        public void Test3_ConcatStrings()
        {
            var data = new string[] { "One", "Two", "Three" };
            var textCollector = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                textCollector.Append($"{data[i]} ");
            }
            Console.WriteLine(textCollector);

            // alternative
            Console.WriteLine(string.Join(" ", data));
        }

        // Write a program that reads a string from the console, 
        // reverses it and prints the result back at the console.
        [Test]
        public void Test4_Reverse()
        {
            var s = "my input string";

            Console.WriteLine(Reverse(s));
        }

        private static string Reverse(string initial)
        {
            StringBuilder reversed = new StringBuilder(initial.Length);

            for (int i = initial.Length - 1; i >= 0; i--)
            {
                reversed.Append(initial[i]);
            }

            return reversed.ToString();
        }

        // Write a program that reads from the console a string of maximum 20 characters. 
        // If the length of the string is less than 20, the rest of the characters should be filled with *. 
        [TestCase("Welcome to SPb!")]
        [TestCase("C# course")]
        public void Test5_StrLength(string input)
        {
            int asteriskCount = 20 - input.Length;

            if (asteriskCount > 0)
            {
                Console.WriteLine($"{input}{new string('*', asteriskCount)}");
            }
            else
            {
                Console.WriteLine(input.Substring(0, 20));
            }
        }

        // Write a program that takes a base-10 number (0 to 1050) and converts it to a base-N number, 
        // where 2 <= N <= 10.
        [TestCase(10, 7)]
        [TestCase(125, 5)]
        [TestCase(1000, 4)]
        public void Test6_Convert_Base10_to_BaseN(BigInteger tenBased, int newBase)
        {
            Console.WriteLine(ConvertToBase(tenBased, newBase));
        }
        private static BigInteger ConvertToBase(BigInteger tenBased, int newBase)
        {
            if (newBase < 2 || newBase > 10)
            {
                throw new ArgumentException("N must be between 2 and 10 (including)");
            }

            StringBuilder sb = new StringBuilder();

            while (tenBased > 0)
            {
                sb.Insert(0, tenBased % newBase);
                tenBased /= newBase;
            }

            return BigInteger.Parse(sb.ToString());
        }

        // Write a program to find how many times a given string appears in a given text as substring
        [TestCase("ababa caba", "aba")]
        public void Test7_SubstringOccurrences(string text, string key)
        {
            var indexOfKey = text.IndexOf(key);
            var occurances = 0;

            while (indexOfKey >= 0)
            {
                occurances++;
                indexOfKey = text.IndexOf(key, indexOfKey + 1);
            }

            Console.WriteLine(occurances);
        }

        // You are given two lines - each can be a really big number (0 to 1050). 
        // You must display the sum of these numbers.
        [TestCase("123456789000000", "876543210000000")]
        public void Test8_SumBigNumbers(string first, string second)
        { 
            Stack<char> firstNumber = FillStack(first);
            Stack<char> secondNumber = FillStack(second);
            Console.WriteLine(SumTwoNumbers(firstNumber, secondNumber).TrimStart('0'));
        }

        private static string SumTwoNumbers(Stack<char> firstNumber, Stack<char> secondNumber)
        {
            if (firstNumber == null || firstNumber.Count == 0)
            {
                return string.Join(string.Empty, secondNumber);
            }
            if (secondNumber == null || secondNumber.Count == 0)
            {
                return string.Join(string.Empty, firstNumber);
            }

            StringBuilder result = new StringBuilder();
            int minLength = Math.Min(firstNumber.Count, secondNumber.Count);
            var carried = 0;

            for (int i = 0; i < minLength; i++)
            {
                carried = SumChars(result, firstNumber, secondNumber, carried);
            }

            // Add the digits from the greater length
            if (firstNumber.Count > 0)
            {
                AddTheRestChars(firstNumber, result, carried);
            }

            if (secondNumber.Count > 0)
            {
                AddTheRestChars(secondNumber, result, carried);
            }

            return result.ToString();
        }

        private static void AddTheRestChars(Stack<char> stack, StringBuilder result, int carried)
        {
            while (stack.Count > 0)
            {
                int newValue = carried + (stack.Pop() - '0');
                result.Insert(0, newValue % 10);
                carried = newValue / 10;
            }

            // Check for carried
            if (carried > 0)
            {
                int newValue = (result[0] - '0') + carried;
                result.Insert(0, newValue);
            }
        }

        private static int SumChars(StringBuilder result, Stack<char> firstNumber, Stack<char> secondNumber, int carried)
        {
            int sum = (firstNumber.Pop() - '0') + (secondNumber.Pop() - '0') + carried;
            result.Insert(0, sum % 10);

            if (sum > 9)
            {
                return sum / 10;
            }

            return 0;
        }

        private static Stack<char> FillStack(string sNum)
        {
            Stack<char> stack = new Stack<char>();
            char[] input = sNum.Trim(new char[] { ' ', '\n', '\r', '\t' }).TrimStart('0').ToCharArray();

            foreach (var ch in input)
            {
                stack.Push(ch);
            }

            return stack;
        }

        // Write a program that converts a string to a sequence of Unicode character literals
        [TestCase("Hi There!")]
        public void Test9_UnicodeChars(string symbols)
        {
            foreach (var ch in symbols)
            {
                Console.Write(GetUnicode(ch));
            }
        }
        private static string GetUnicode(char character)
        {
            return $"\\u{((int)character).ToString("X4")}".ToLower();
        }

        // Write a method that takes as input two strings, and returns Boolean if they are exchangeable or not.
        // Exchangeable are words where the characters in the first string can be replaced to get the second string. Example: "egg" and "add" are exchangeable, but "aabbccbb" and "nnooppzz" are not. (First 'b' corresponds to 'o', but then it also corresponds to 'z'). The two words may not have the same length, if such is the case they are exchangeable only if the longer one doesn't have more types of characters then the shorter one ("Clint" and "Eastwaat" are exchangeable because 'a' and 't' are already mapped as 'l' and 'n', but "Clint" and "Eastwood" aren't exchangeable because 'o' and 'd' are not contained in "Clint").
        [TestCase("egg","add")]
        [TestCase("aabbccbb", "nnooppzz")]
        public void Test10_ExchangeableWords(string left, string right)
        {
            var firstChars = left.ToCharArray().Distinct().ToArray();
            var secondChars = right.ToCharArray().Distinct().ToArray();
            Console.WriteLine(firstChars.Length == secondChars.Length ? "true" : "false");
        }

        // Write a program to extract all hyperlinks (<href=…>) from a given text
        [TestCase("<a href=\"http://google.ru\" class=\"new\"></a>")]
        public void Test11_ExtractHyperlinks(string inpulLine)
        {
            var hyperlinks = ExtractLinks(inpulLine);

            if (hyperlinks.Count > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, hyperlinks));
            }
        }

        private static List<string> ExtractLinks(string inpulLine)
        {
            var html = ReadInput(inpulLine);
            var startIndex = html.IndexOf(" href");
            var links = new List<string>();

            while (startIndex >= 0)
            {
                if (!IsAnchorTag(startIndex, html))
                {
                    startIndex = html.IndexOf(" href", startIndex + 1);
                    continue;
                }

                startIndex = html.IndexOf('=', startIndex) + 1;

                // Check because the +1 above
                if (startIndex <= 0 || startIndex > html.Length)
                {
                    continue;
                }

                var link = GetLink(startIndex, html);
                if (link != null)
                {
                    links.Add(link);
                }

                startIndex = html.IndexOf(" href", html.IndexOf("a>", startIndex));
            }

            return links;
        }

        private static string GetLink(int startIndex, string html)
        {
            var linkStarstAt = -1;
            for (int i = startIndex; i < html.Length; i++)
            {
                if (html[i] != ' ')
                {
                    linkStarstAt = i;
                    break;
                }
            }

            var linkEndsAt = -1;
            switch (html[linkStarstAt])
            {
                case '"':
                    linkEndsAt = html.IndexOf('"', linkStarstAt + 1);
                    break;
                case '\'':
                    linkEndsAt = html.IndexOf('\'', linkStarstAt + 1);
                    break;
                default:

                    for (int i = linkStarstAt; i < html.Length; i++)
                    {
                        if (html[i] == '>' || html[i] == ' ')
                        {
                            linkEndsAt = i;
                            break;
                        }
                    }

                    break;
            }

            if (linkStarstAt < 0 || linkEndsAt < 0)
            {
                return null;
            }

            return html
                .Substring(linkStarstAt, linkEndsAt - linkStarstAt)
                .Trim(new char[] { '"', '\'', ' ' });
        }

        private static bool IsAnchorTag(int startLinkIndex, string html)
        {
            var tagOpenIndex = html.LastIndexOf('<', startLinkIndex);
            return html[tagOpenIndex + 1] == 'a';
        }

        private static string ReadInput(string inpulLine)
        {
            StringBuilder html = new StringBuilder(inpulLine);

            return html.ToString()
                .Replace('\n', ' ')
                .Replace('\r', ' ')
                .Replace('\t', ' ');
        }
    }
}
