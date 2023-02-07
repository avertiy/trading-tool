using System;
using System.Collections.Generic;
using System.Linq;
using AVS.BinanceApi.Services;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;

namespace AVS.BinanceApi
{
    public class BinancePairProvider : DefaultPairProvider
    {
        public override bool IsBaseCurrencyFirst => false;

        protected override void Initialize()
        {
            Pairs = new List<string>();
            Pairs.AddRange(CreateCombinations("UAH", "BNB,BTC,USDT"));
            Pairs.AddRange(CreateCombinations("VAI", "BUSD"));
            Pairs.AddRange(CreateCombinations("USDT", "BUSD,DAI,USDC"));
            Pairs.AddRange(CreateCombinations("USDT", "BCH,BTC,BTT,DASH,DOGE,DOT,ETH,IOTA,LTC,NEO,TRX,XMR,XRP"));
            Pairs.AddRange(CreateCombinations("USDT", "ADAUP,EOSUP,TRXUP,XRPUP"));
            Pairs.AddRange(CreateCombinations("USDT", "ADADOWN,EOSDOWN,TRXDOWN,XRPDOWN"));
            Pairs.AddRange(CreateCombinations("USDT", "DIA,REEF,LIT,OCEAN,ALPHA"));
            Pairs.AddRange(CreateCombinations("BUSD", "BNB,BCH,COMP,DASH,DOGE,EOS,ETH,FIL,IOTA,JST,LIT,LTC,MKR,NEO,OCEAN,PAX,TRX,USDC,XLM,XMR,XRP,YFI,ZRX"));
            Pairs.AddRange(CreateCombinations("USDC", "BCH,BTC,BTT,DASH,DOGE,ETH,IOTA,LTC,XMR,XRP"));
            Pairs.AddRange(CreateCombinations("BTC", "BCH,DASH,DOGE,ETH,IOTA,LTC,TRX,XMR,XRP"));
        }

        public override string[] GetCoinsFor(AccountType type)
        {
            switch (type)
            {
                case AccountType.Lending:
                {
                    throw new NotSupportedException($"Binance does not support {type}");
                }
                case AccountType.Margin:
                {
                    throw new NotSupportedException($"Binance does not support {type}");
                }
                default:
                {
                    //* only coins that i'm interested in
                    return new[] {"BCH", "BTC", "DASH","DOGE","EOS", "ETH", "LTC", "XEM", "XRP", "USDT", "WAVES" };
                }
            }
        }

        public override List<string> GetRecentPairs()
        {
            var pairUsageService = EngineContext.Current.Resolve<BinancePairUsageService>();
            var recentPairs = pairUsageService.GetRecentPairs(1);

            if (recentPairs.Any())
            {
                return recentPairs;
            }

            return GetAllPairs();
        }
    }
}