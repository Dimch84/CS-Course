using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringSamples
{
    [TestFixture]
    public class RexExLab
    {
        // Find the count of occurrences of a word in a given text using regex
        [TestCase("A regular expression, regex or regexp(sometimes called a rational expression) is, in theoretical computer science and formal language theory, a sequence of characters that define a search pattern, i.e. RegEx.", "regex")]
        public void Test1_MatchCount(string text, string key)
        {
            var matchesCount = Regex.Matches(text, key).Count;
            Console.WriteLine(matchesCount);
        }

        public const string pattern = "[aeiouy]";

        // Find the count of all vowels in a given text using a regex.
        [TestCase("John Snow")]
        [TestCase("n vwls.")]
        public void Test2_VowelCount(string input)
        {
            var vowels = Regex.Matches(input, pattern, RegexOptions.IgnoreCase).Count;
            Console.WriteLine($"Vowels: {vowels}");
        }

        // Extract all tags from a given HTML using regex.
        [TestCase("<html lang=\"en\"><head>    <meta charset = \"UTF-8\">    <title> Title </title></head></html> ")]
        public void Test3_ExtractTags(string input)
        {
            var matches = Regex.Matches(input, "<.+?>");

            if (matches.Count > 0)
            {
                foreach (var match in matches)
                {
                    Console.WriteLine(match);
                }
            }
        }

        // Scan for valid username
        /*
        A valid username:
        - Has length between 3 and 16 characters
        - Contains only letters, numbers, hyphens and underscores
        - Has no redundant symbols before, after or in between         
         */
        [TestCase("too_long_username")]
        [TestCase("!lleg@l ch@rs")]
        [TestCase("joe_smth")]
        public void Test4_ValidUsername(string input)
        {
            var regex = new Regex(@"^[\w-]{3,16}$");

            if (regex.IsMatch(input))
            {
                Console.WriteLine("valid");
            }
            else
            {
                Console.WriteLine("invalid");
            }
        }

        // Extract all quotations from a given text.
        //A valid quotation should:
        // - Start and end with either single quotes(valid: 'quotation') or double quotes(valid: "quotation")
        // - Not have mixed quotes(invalid: 'quotation")
        [TestCase("<a href='/' id=\"home\">Home</a><a class=\"selected\"</a><a href = '/forum'>")]
        public void Test5_ExtractQuotations(string input)
        {
            var matches = Regex.Matches(input, @"(['|""])([\S\s]+?)\1");

            foreach (Match match in matches)
            {
                Console.WriteLine(match.Groups[2]);
            }
        }

        [TestCase("+7812-123-23-34")]
        [TestCase("+7-812-123-23-34")]
        [TestCase("+7812 123 23 34")]
        public void Test6_PhoneNumber(string input)
        {
            var regex = new Regex(@"^\s*\+7812( |-)\1[0-9]{3}\1[0-9]{2}\1[0-9]{2}\b");
            if (regex.IsMatch(input))
            {
                Console.WriteLine("is valid");
            }
            else
            {
                Console.WriteLine("is invalid");
            }
        }

        // Write a program that reads a string from the console and replaces all series of consecutive 
        // identical letters with a single one.
        [TestCase("aaaaabbbbbcdddeeeedssaa")]
        public void Test7_SeriesOfLetters(string text)
        {
            // var result = Regex.Replace(text, @"([A-Za-z])\1+", m => m.Groups[1].Value); // Both variations work Identically
            var result = Regex.Replace(text, @"([A-Za-z])\1+", "$1"); // Both variations work Identically

            Console.WriteLine(result);
        }

        // Write a program that replaces in a HTML document given as string all the tags <a href=…>…</a> 
        // with corresponding tags [URL href=…>…[/URL]
        [TestCase("<ul> <li> <a href=\"www.google.com\">Google</a>")]
        public void Test8_ReplaceTag(string input)
        {
            var pattern = @"<a.{0,}?(href=?(['|""]?).+?\2).{0,}?>(.+?)<\/a>";
            var replacedLine = Regex.Replace(input, pattern, m => $"[URL {m.Groups[1].Value}]{m.Groups[3].Value}[/URL]");

            Console.WriteLine(replacedLine);
        }

        // Write a program to extract all email addresses from a given text
        [TestCase("Just send email to s.miller@mit.edu and j.hopking@york.ac.uk for more information")]
        public void Test9_ExtractEmail(string text)
        {
            var pattern = @"^[a-zA-Z0-9][\w-.]+@[a-zA-Z][a-zA-Z-]*(\.[a-zA-Z]+[a-zA-Z-]*?)+$";

            var input = text.Split().Select(x => x.TrimEnd(".,;:!?".ToCharArray()));

            foreach (var item in input)
            {
                if (Regex.IsMatch(item, pattern))
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
