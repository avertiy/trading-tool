using AVS.CoreLib.Utils;

namespace AVS.Trading.Core.Domain
{
    public class PairString: IHasValue
    {
        public PairString(string pair)
        {
            Value = pair;
        }

        public string Value { get; set; }
        public bool HasValue => !string.IsNullOrEmpty(Value);

        public static implicit operator string(PairString s)
        {
            return s.Value;
        }
        public static implicit operator PairString(string pair)
        {
            return new PairString(pair);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}