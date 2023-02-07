using System.Globalization;

namespace AVS.Trading.Tool.Utils
{
    public static class Helper
    {
        public static NumberFormatInfo NumberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public static double ParseDouble(string text)
        {
            return double.Parse(text, NumberFormatInfo);
        }
    }
}