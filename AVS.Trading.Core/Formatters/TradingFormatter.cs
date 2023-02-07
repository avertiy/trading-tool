using System;
using System.Diagnostics;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.Formatters
{
    /// <summary>
    /// Represents ICustomFormatter to format strings containing trading data
    /// using wide range of qualifiers provided by following formatters:
    /// - DoubleFormatter (n|amount; p|price; q|qty )
    /// - CurrencyPairFormatter
    /// - OhlcFormatter
    /// </summary>
    /// <example>
    /// TradingFormatter.Format($"Buys: {Volume} {pair:q} {Total} {pair:b}");
    /// </example>
    public class TradingFormatter: BaseFormatter
    {
        public static readonly TradingFormatProvider FormatProvider = new TradingFormatProvider();

        [DebuggerStepThrough]
        public static string Format(FormattableString formattable)
        {
            return formattable.ToString(FormatProvider);
        }

        [DebuggerStepThrough]
        public static string StringFormat(string format, params object[] args)
        {
            return string.Format(FormatProvider, format, args);
        }

        [DebuggerStepThrough]
        public static string Ohlc(FormattableString formattable)
        {
            return formattable.ToString(new OhlcFormatProvider());
        }

        protected override string Format(string format, object arg)
        {
            switch (arg)
            {
                case double d:
                    return DoubleFormatter.Format(format, d);

                case CurrencyPair cp:
                    return CurrencyPairFormatter.Format(format, cp);

                case IOhlc ohlc:
                    return OhlcFormatter.Format(format, ohlc);

                default:
                    return base.Format(format, arg);
            }
        }
    }
}