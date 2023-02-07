using System;
using System.Linq;

namespace AVS.Trading.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool Either(this string value, params string[] values)
        {
            return values.Contains(value);
        }
        public static bool StartsWith(this string value, params string[] values)
        {
            return values.Any(value.StartsWith);
        }

        public static string Swap(this string str, char separator)
        {
            var parts = str.Split(separator);
            if (parts.Length > 2)
                throw new ArgumentException($"It is supposed separator '{separator}' splits the string '{str}' on 2 parts");
            var swap = parts[1] + "_" + parts[0];
            return swap;
        }
    }
}