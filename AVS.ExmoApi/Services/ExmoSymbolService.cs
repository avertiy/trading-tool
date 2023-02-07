using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.ExmoApi.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Services;

namespace AVS.ExmoApi.Services
{
    public class ExmoSymbolService : SymbolService
    {
        public ExmoSymbolService()
        {
        }

        protected override string SymbolToPairInternal(string str)
        {
            var cp = new CurrencyPair(str, ExmoConstants.IsBaseCurrencyFirst);
            return cp.ToString();
        }

        public override string PairToSymbol(PairString pair)
        {
            //we suppose PairString is supplied in a normalized form i.e. base currency in first position
            //e.g. BTC_XRP but in exmo form it should be XRP_BTC

            //so we do the swap 
            //("BTC_XRP", false)=> ToString()=> XRP_BTC
            var cp = new CurrencyPair(pair, ExmoConstants.IsBaseCurrencyFirst);
            return cp.ToString().ToUpper();
        }
    }
}
