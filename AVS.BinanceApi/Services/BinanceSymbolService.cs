using System;
using AVS.BinanceApi.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Services;

namespace AVS.BinanceApi.Services
{
    public class BinanceSymbolService : SymbolService
    {
        public BinanceSymbolService()
        {
            AmbiguousTokens.Add("HOT", "HOLO");
        }

        protected override string SymbolToPairInternal(string symbol)
        {
            if (symbol.Length == 6)
                return new CurrencyPair(symbol.Substring(3, 3), symbol.Substring(0, 3)).ToString();

            var len = symbol.Length;

            var baseCoin = symbol.Substring(len - 3, 3);
            if (baseCoin.Either("BTC", "ETH", "PAX", "BNB", "NGN"))
            {
                return new CurrencyPair(baseCoin, symbol.Substring(0, len - 3)).ToString();
            }

            baseCoin = symbol.Substring(len - 4, 4);
            if (baseCoin.Either("TUSD", "USDT", "BUSD", "USDC", "USDS"))
            {
                return new CurrencyPair(baseCoin, symbol.Substring(0, len - 4)).ToString();
            }

            return new CurrencyPair(baseCoin + "*", symbol.Substring(0, len - 4)).ToString();
        }

        public override string PairToSymbol(PairString pair)
        {
            var str = ReplaceOfficialSymbolWithExchangeSymbol(pair.Value).ToUpper();
            var cp = CurrencyPair.Parse(str, BinanceConstants.IsBaseCurrencyFirst);

            if (cp.HasValue == false)
                throw new Exception($"{this.GetType().Name} => unable to convert pair {pair} into symbol");

            var symbol = $"{cp.BaseCurrency}{cp.QuoteCurrency}";
            return symbol;
        }
    }
}
