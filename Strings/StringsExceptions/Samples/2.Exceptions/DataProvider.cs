using System;
using System.Collections.Generic;
using System.IO;

namespace Samples.Exceptions
{
    static class DataProvider
    {
        private static Int32 _enumerated;

        public static IEnumerable<String> FindRarFiles()
        {
            return Directory.GetFiles(".", "*.rar");
        }

        public static IEnumerable<String> FindZipFiles()
        {
            _enumerated++;

            foreach (var file in Directory.EnumerateFiles(".", "*.zip"))
                yield return file;
        }
    }
}