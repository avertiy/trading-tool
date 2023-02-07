using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.Trading.Core.Domain;

namespace AVS.Trading.Core.Services
{
    public interface ISymbolService
    {
        string SymbolToPair(string symbol);
        string PairToSymbol(PairString pair);
    }

    public abstract class SymbolService : ISymbolService
    {
        protected readonly Dictionary<string, string> AmbiguousTokens = new Dictionary<string, string>();

        protected string ReplaceOfficialSymbolWithExchangeSymbol(string pair)
        {
            foreach (var kp in AmbiguousTokens)
            {
                if (pair.Contains(kp.Value))
                {
                    return pair.Replace(kp.Value, kp.Key);
                }
            }
            return pair;
        }

        protected bool CheckAmbiguousToken(string symbol, out string alias)
        {
            alias = symbol;
            foreach (var key in AmbiguousTokens.Keys)
            {
                if (symbol.Contains(key))
                {
                    alias = symbol.Replace(key, AmbiguousTokens[key]);
                    return true;
                }
            }
            return false;
        }

        public string SymbolToPair(string symbol)
        {
            CheckAmbiguousToken(symbol, out string str);
            return SymbolToPairInternal(str);
        }

        protected abstract string SymbolToPairInternal(string symbol);

        public virtual string PairToSymbol(PairString pair)
        {
            return ReplaceOfficialSymbolWithExchangeSymbol(pair.Value);
        }
    }
}
