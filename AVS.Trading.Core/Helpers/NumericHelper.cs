using System;
using System.Globalization;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Core.Helpers
{
    public static class NumericHelper
    {
        public static double ParseDouble(string value, NumberStyles style = NumberStyles.Float)
        {
            if (string.IsNullOrEmpty(value))
                return 0.0;
            return double.Parse(value, style, CultureInfo.InvariantCulture).Normalize();
        }

        public static bool TryParseDouble(string value, out double res)
        {
            res = 0;
            if (string.IsNullOrEmpty(value))
                return false;
            double k = 1;
            if (value.EndsWith("K"))
                k = 1000;
            if (value.EndsWith("M"))
                k = 1000 * 1000;

            if (double.TryParse(value.Substring(0, value.Length - 1), out res))
            {
                if (k > 1)
                    res = (res * k).RoundUp(8);
                return true;
            }
            return false;
        }


        public static bool AreSame(double price1, double price2)
        {
            return Math.Abs(price1 - price2) <= Constants.OneSatoshi;
        }
    }
}