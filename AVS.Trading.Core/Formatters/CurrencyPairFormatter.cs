using System;

namespace AVS.Trading.Core.Formatters
{

    /// <summary>
    /// qualifiers: p|pair; q|quote; b|base; m|market
    /// </summary>
    public static class CurrencyPairFormatter 
    {
        public static string GetQualifiers => "p|pair; q|quote; b|base; m|market";

        public static string Format(string format, CurrencyPair arg)
        {
            if (string.IsNullOrEmpty(format) || format == "p" || format == "pair")
                return arg.ToString();

            if (format == "q" || format == "quote")
                return arg.QuoteCurrency;

            if (format == "b" || format == "base")
                return arg.BaseCurrency;

            if (format == "m" || format == "market")
                return arg.ToMarketString();

            throw new FormatException($"Not supported format {format} for CurrencyPair");
        }
    }



}