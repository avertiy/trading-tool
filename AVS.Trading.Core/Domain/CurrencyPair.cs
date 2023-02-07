using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AVS.Trading.Core.Extensions;
namespace AVS.Trading.Core
{
    /// <summary>
    /// везде где оперируем парой абстрагируясь от биржи нужно использовать 
    /// CurrencyPair 
    /// т.к. если использовать просто pair "BCH_ETH" мы дальше не знаем какая валюта является base а какая quote
    /// </summary>
    public class CurrencyPair
    {
        private const char SeparatorCharacter = '_';
        public const string All = "all";
        public static CurrencyPair Any => new CurrencyPair("*", "*");
        
        public string BaseCurrency { get; private set; }
        public string QuoteCurrency { get; private set; }

        public bool IsAll => QuoteCurrency == "*";
        public bool IsBitcoinPair => BaseCurrency == "BTC";
        public bool HasValue => BaseCurrency != null && QuoteCurrency != null;
        public CurrencyPair(string baseCurrency, string quoteCurrency)
        {
            BaseCurrency = baseCurrency;
            QuoteCurrency = quoteCurrency;
        }

        public CurrencyPair(string pair, bool isBaseCurrencyFirst = true)
        {
            var parts = pair.Split(SeparatorCharacter);
            if (parts.Length != 2)
                throw new ArgumentException($"Pair string '{pair}' could not be spliced on base and quote currencies");

            if (isBaseCurrencyFirst)
            {
                BaseCurrency = parts[0];
                QuoteCurrency = parts[1];
            }
            else
            {
                BaseCurrency = parts[1];
                QuoteCurrency = parts[0];
            }
        }

        public string ToMarketString()
        {
            if (BaseCurrency == "*" && QuoteCurrency == "*")
                return All;
            return QuoteCurrency + '/'+ BaseCurrency;
        }

        #region Equals GetHashCode operators == and !=
        public override bool Equals(object obj)
        {
            var b = obj as CurrencyPair;
            return (object)b != null && Equals(b);
        }

        public bool Equals(CurrencyPair b)
        {
            return b.BaseCurrency == BaseCurrency && b.QuoteCurrency == QuoteCurrency;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(CurrencyPair a, CurrencyPair b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null ^ (object)b == null) return false;

            return a.BaseCurrency == b.BaseCurrency && a.QuoteCurrency == b.QuoteCurrency;
        }

        public static bool operator !=(CurrencyPair a, CurrencyPair b)
        {
            return !(a == b);
        }

        #endregion

        [DebuggerStepThrough]
        public override string ToString()
        {
            if (BaseCurrency == "*" && QuoteCurrency == "*")
                return All;
            return BaseCurrency + SeparatorCharacter + QuoteCurrency;
        }

        

        //[DebuggerStepThrough]

        /// <summary>
        /// Parses pair string into CurrencyPair
        /// </summary>
        /// <param name="pair">a string representation of currency pair, currencises are separated by '_' character</param>
        /// <param name="baseCurrencyIsFirst">e.g. if true in BTC_LTC pair a base currency is BTC</param>
        public static CurrencyPair ParsePair(string pair, bool baseCurrencyIsFirst = true)
        {
            if(string.IsNullOrEmpty(pair))
                throw new ArgumentNullException(nameof(pair));

            var parts = pair.Split(SeparatorCharacter);

            if (parts.Length != 2)
                throw new ArgumentException($"Invalid pair: {pair}");

            return baseCurrencyIsFirst ? new CurrencyPair(parts[0], parts[1]) : new CurrencyPair(parts[1], parts[0]);
        }

        //public static CurrencyPair Parse(string text)
        //{
        //    if (string.IsNullOrEmpty(text))
        //        return null;
        //    if (text == All || text == "*")
        //        return Any;
        //    var valueSplit = text.Split(SeparatorCharacter, '/');

        //    if (valueSplit.Length < 2)
        //        throw new ArgumentException($"Unable to parse currency pair from '{text}'");

        //    //in LTC/BTC notation base currency is BTC
        //    if (text.Contains("/"))
        //    {
        //        return new CurrencyPair(valueSplit[1], valueSplit[0]);
        //    }
        //    //in BTC_LTC notation the base currency is BTC
        //    return new CurrencyPair(valueSplit[0], valueSplit[1]);
        //}

        public static CurrencyPair Parse(string pair, bool isBaseCurrencyFirst = true)
        {
            if (string.IsNullOrEmpty(pair))
                    return null;
            
            if (pair == All || pair == "*")
                    return Any;

            var str = pair.ToUpper();
            CurrencyPair cp = default;
            if (str.Contains(SeparatorCharacter))
            {
                cp = new CurrencyPair(str, isBaseCurrencyFirst);
            }
            else
            {
                if (str.StartsWith("USDC", "USDT"))
                {
                    cp = new CurrencyPair(str.Substring(4, str.Length - 4), str.Substring(0, 4));
                }
                else if (str.StartsWith("BTC", "USD", "UAH", "ETH", "EUR", "RUB", "DAI"))
                {
                    cp = new CurrencyPair(str.Substring(3, str.Length - 3), str.Substring(0, 3));
                }
            }

            return cp;
        }
    }
}
