using System;
using System.Collections.Generic;

namespace Samples.Collections
{
    public static class ExtensionMethods
    {
        public static String FormatString<T>(this IEnumerable<T> enumerable, String separator = ", ")
        {
            return String.Join(separator, enumerable);
        }
    }
}