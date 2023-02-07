using System;
using System.Diagnostics;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Core.Formatters
{
    /// <summary> 
    /// Qualifiers:
    /// double: all standard + p|price - format as price, amount|n - format as number, q|qty e.g. 0.0015:price
    /// CurrencyPair: default|p|pair - formatted as a pair; q|quote - quote currency; b|base - base currency; m|market - market
    /// IOHLC: ohlc, oc, hl, o, h, l, c
    /// </summary>
    public class TradingFormatProvider : IFormatProvider
    {
        private readonly TradingFormatter _formatter = new TradingFormatter();

        [DebuggerStepThrough]
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return _formatter;
            return null;
        }

    }
}