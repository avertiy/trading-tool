using AVS.CoreLib.Utils;

namespace AVS.Trading.Core.Domain
{
    public class ExchangePair: IHasValue
    {
        public ExchangePair(string pair, string exchange, bool isBaseCurrencyFirst)
        {
            Value = pair;
            Exchange = exchange;
            IsBaseCurrencyFirst = isBaseCurrencyFirst;
        }

        public ExchangePair(PairString pair, string exchange, bool isBaseCurrencyFirst)
        {
            Value = pair.Value;
            Exchange = exchange;
        }

        public string Value { get; set; }
        public string Exchange { get; set; }
        public bool IsBaseCurrencyFirst { get; set; }

        public static implicit operator string(ExchangePair s)
        {
            return s.Value;
        }

        public static implicit operator PairString(ExchangePair s)
        {
            return new PairString(s.Value);
        }

        public static implicit operator CurrencyPair(ExchangePair s)
        {
            return CurrencyPair.ParsePair(s.Value, s.IsBaseCurrencyFirst);
        }

        public override string ToString()
        {
            return $"{Exchange}:{Value}";
        }

        public bool HasValue => !string.IsNullOrEmpty(Value);
    }
}