using System;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Services;

namespace AVS.KunaApi.Services
{
    public class KunaSymbolService : SymbolService
    {
        public KunaSymbolService()
        {
        }

        protected override string SymbolToPairInternal(string symbol)
        {
            var str = symbol.ToUpper();
            if (str.Contains("_"))
                return str;
            //xrpuah => UAH_XRP
            if (str.Length == 6)
                return new CurrencyPair(str.Substring(3, 3), str.Substring(0, 3)).ToString();

            var len = str.Length;
            //tusduah => UAH_USDT
            var baseCoin = str.Substring(len - 3, 3);
            if (baseCoin.Either("BTC", "UAH", "RUB", "USD"))
            {
                return new CurrencyPair(baseCoin, str.Substring(0, len-3)).ToString();
            }

            baseCoin = str.Substring(len - 4, 4);
            if (baseCoin.Either("TUSD", "USDT", "USDC"))
            {
                return new CurrencyPair(baseCoin, str.Substring(0, len - 4)).ToString();
            }

            return str.Substring(len - 3, 3) + "_" + str.Substring(0, len - 3) + "*";
        }

        public override string PairToSymbol(PairString pair)
        {
            //UAH_XRP => xrpuah
            var parts = pair.Value.ToLower().Split('_');
            if(parts.Length !=2)
                throw new ArgumentException($"Invalid pair {pair}");
            var symbol = parts[1]+parts[0];
            return symbol;
        }
    }
}
