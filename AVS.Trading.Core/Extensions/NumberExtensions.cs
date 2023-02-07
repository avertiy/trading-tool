using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AVS.Trading.Core.Extensions
{
    public static class NumberExtensions
    {
        private const int DoubleRoundingPrecisionDigits = 8;

        public static double Abs(this double value)
        {
            if (value < 0)
                value = value * -1;
            return value;
        }

        public static double DiffPercentage(this double value1, double value2)
        {
            var diff = (value1 - value2);
            var diffPercentage = diff / value1 * 100;
            return diffPercentage;
        }
        
        /// <summary>
        /// format numbers to string using n.FormatPrice()
        /// leading zeros will be replaced by replacement
        /// (e.g. 0.0001234 => 0..1234) 
        /// </summary>
        public static string[] FormatPrices(this double[] numbers, string replacement="0..")
        {
            if(numbers.Length < 2)
                throw new ArgumentException("At least 2 numbers are expected");
            var arr = new string[numbers.Length];
            var str = numbers[0].FormatAsPrice();
            var ind = str.IndexOfAny(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            string strToReplace = string.Empty;
            if (ind > 4)
                strToReplace = str.Substring(0, ind);
            //arr[0] = str;
            for (var i = 0; i < numbers.Length; i++)
            {
                str = numbers[i].FormatAsPrice();
                if(ind > 4)
                    arr[i] = str.Replace(strToReplace, replacement);
                else
                arr[i] = str;
            }

            return arr;
        }

        public static string FormatAsPrice(this double value)
        {
            var M = 1000 * 1000;
            var K = 1000;

            if (value > M)
                return $@"{value:N2}";

            if (value > K)
                return $@"{value:N2}";

            if (value > 10)
            {
                return $@"{value:N4}";
            }

            return $@"{value:N8}";
        }

        public static string FormatAsQuantity(this double value)
        {
            var M = 1000 * 1000;
            var K = 1000;

            if (value > M)
                return $@"{value / M:N4}M";

            if (value > K)
                return $@"{value / K:N4}K";

            if (value > 1)
                return $@"{value:N2}";

            if (value > 0.1)
                return $@"{value:N3}";

            if (value > 0.001)
                return $@"{value:N4}";

            if (value > 0.0001)
                return $@"{value:N5}";

            if (value > 0.00001)
                return $@"{value:N6}";

            return $@"{value:N8}";
        }

        public static string FormatAsAmount(this double value)
        {
            return value.FormatNumber();
        }

        public static string FormatNumber(this double value)
        {
            if (value > 10000)
            {
                var million = 1000 * 1000;
                if (value < million)
                {
                    var k = value / 1000;
                    return $@"{k:N3}K";
                }
                if (value < (million * 1000))
                {
                    var m = value / million;
                    return $@"{m:N3}M";
                }
                if (value < (million * 1000 * 1000))
                {
                    var m = value / (million * 1000);
                    return $@"{m:N3}B";
                }
                var b = value / (million * 1000 * 1000);
                return $@"{b:N3}T";
            }

            if (value >= 1)
            {
                if (value > 10000.0)
                    return $@"{value:0.#}";
                if (value > 100.0)
                    return $@"{value:0.##}";
                if (value > 10.0)
                    return $@"{value:0.###}";
                return $@"{value:0.####}";
            }
            else
            {
                if (value <= 0)
                    return $@"{value:N0}";
                if (value < 0.000099)
                    return $@"{value:0.########}";
                if (value < 0.00099)
                    return $@"{value:0.#######}";
                if (value < 0.0099)
                    return $@"{value:0.######}";
                if (value < 0.099)
                    return $@"{value:0.#####}";
                if (value < 0.99)
                    return $@"{value:0.####}";
                if (value < 9.9)
                    return $@"{value:0.###}";
                if (value < 99)
                    return $@"{value:0.##}";
            }
            return $@"{value:0.########}";
        }

        public static string FormatNumber(this double value, string currency)
        {
            if (value > 10000)
            {
                var million = 1000 * 1000;
                if (value < million)
                {
                    var k = value / 1000;
                    return $@"{k:N3}K {currency}";
                }
                if (value < (million*1000))
                {
                    var m = value / million;
                    return $@"{m:N3}M {currency}";
                }
                if (value < (million * 1000 * 1000))
                {
                    var m = value / (million*1000);
                    return $@"{m:N3}B {currency}";
                }
                var b = value / (million*1000*1000);
                return $@"{b:N3}T {currency}";
            }
            if (value >= 1)
            {
                if (value > 1000.0)
                    return $@"{value:0.#} {currency}";
                if (value > 100.0)
                    return $@"{value:0.##} {currency}";
                if (value > 10.0)
                    return $@"{value:0.###} {currency}";
                return $@"{value:0.####} {currency}";
            }
            else
            {
                if (value <= 0)
                    return $@"{value:N0} {currency}";
                if (value > 0.1)
                    return $@"{value:N4} {currency}";
                if (value < 0.0000001)
                    return $@"{value:N9} {currency}";
            }
            return $@"{value:N8} {currency}";
        }

        public static double NormalizeForDisplay(this double value)
        {
            if (value >= 1)
            {
                if (value > 1000.0)
                    return value.RoundUp(2);
                if (value > 100.0)
                    return value.RoundUp(3);
                if (value > 10.0)
                    return value.RoundUp(4);
                return value.RoundUp(5);
            }
            else
            {
                if (value <= 0)
                    return value.RoundUp(0);
                if (value < 0.0000001)
                    return value.RoundUp(8);
                if (value > 0.1)
                    return value.RoundUp(4);

            }
            return value.RoundUp(8);
        }

        public static double Round(this double value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static double RoundUp(this double value, int decimals)
        {
            var k = Math.Pow(10, decimals);
            return Math.Ceiling((value * k)) / k;
        }
        public static double RoundDown(this double value, int decimals)
        {
            var k = Math.Pow(10, decimals);
            return Math.Floor((value * k)) / k;
        }

        /// <summary>
        /// Rounds value to 8 digits AwayFromZero
        /// </summary>
        public static double Normalize(this double value)
        {
            return Math.Round(value, DoubleRoundingPrecisionDigits, MidpointRounding.AwayFromZero);
        }

        public static string ToStringNormalized(this double value)
        {
            return value.ToString("0." + new string('#', DoubleRoundingPrecisionDigits), CultureInfo.InvariantCulture);
        }

    }
}