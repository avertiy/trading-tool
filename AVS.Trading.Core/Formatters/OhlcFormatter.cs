using System;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.Formatters
{
    class OhlcFormatProvider : IFormatProvider
    {
        private readonly OhlcFormatter _formatter = new OhlcFormatter();

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return _formatter;
            return null;
        }

    }

    /// <summary>
    /// qualifiers:  ohlc; hl; oc; o; h; l; c
    /// usage: OhlcFormatter.Format(format, ohlc); or TradingFormatter.Format("{}");
    /// </summary>
    public class OhlcFormatter : ICustomFormatter
    {
        public static string GetQualifiers => "ohlc; hl; oc; o; h; l; c";


        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return Format(format, (IOhlc)arg);
        }

        public static string Format(string format, IOhlc arg)
        {
            if (string.IsNullOrEmpty(format))
                format = "ohlc";

            double[] arr = null;
            if (format == "ohlc")
            {
                arr = new double[] { arg.Open, arg.High, arg.Low, arg.Close };
            }

            if (format == "hl")
            {
                arr = new double[] { arg.High, arg.Low };
            }
            if (format == "oc")
            {
                arr = new double[] { arg.Open, arg.Close };
            }

            if (arr != null && arr.Length > 1)
                return string.Join(";", arr.FormatPrices("."));

            if (format == "o")
                return arg.Open.FormatAsPrice();

            if (format == "c")
                return arg.Close.FormatAsPrice();

            if (format == "h")
                return arg.High.FormatAsPrice();

            if (format == "l")
                return arg.Low.FormatAsPrice();

            throw new FormatException($"Not supported format {format} for CurrencyPair");
        }
    }

    
}