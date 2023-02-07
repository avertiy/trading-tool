using System;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Core.Formatters
{
    /// <summary>
    /// qualifiers: n|amount; p|price; q|qty 
    /// </summary>
    public static class DoubleFormatter
    {
        public static string GetQualifiers => "n|amount; p|price; q|qty";

        public static string Format(string format, double d)
        {
            // Set default format specifier             

            if (string.IsNullOrEmpty(format))
                format = "n";

            if (format == "n" || format == "amount")
                return d.FormatNumber();

            if (format == "price" || format == "p")
                return d.FormatAsPrice();

            if (format == "q" || format == "qty")
                return d.FormatAsQuantity();

            return d.ToString(format);
        }
    }
}