using System;
using System.Collections.Generic;
using AVS.ExmoApi.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;

namespace AVS.ExmoApi
{
    public class ExmoPairProvider : DefaultPairProvider
    {
        public override bool IsBaseCurrencyFirst => ExmoConstants.IsBaseCurrencyFirst;

        protected override void Initialize()
        {
            Pairs = new List<string>();
            Pairs.Add("UAH_USDT");
            Pairs.AddRange(CreateCombinations("UAH", "BCH,BTC,BTT,DASH,DCR,ETH,LTC,ONG,ONT,TRX,XEM,XMR,XRP"));
            Pairs.AddRange(CreateCombinations("USDT", "BTC,BCH,DASH,ETH,EXM,PTI,USDC,VITAE,XRP"));
            Pairs.AddRange(CreateCombinations("BTC", "ADA,DOGE,ETH,EXM,LTC,WAVES,XEM,XMR,XRP"));
            Pairs.Add("USD_USDT");
            Pairs.AddRange(CreateCombinations("USD", "ADA,BTC,DAI,ETH,DOGE,LTC,TRX,XLM,XEM,XRP,USDC,WAVES,ZEC"));
            Pairs.Add("ETH_BCH");
            Pairs.Add("ETH_EXM");
            Pairs.AddRange(CreateCombinations("RUB", "BTT,DAI,DCR,ETH,PTI,USDT,XLM,XRP,WAVES,ZEC"));
        }

        public override string[] GetCoinsFor(AccountType type)
        {
            switch (type)
            {
                case AccountType.Lending:
                {
                    throw new NotSupportedException($"Exmo does not support {type}");
                }
                case AccountType.Margin:
                {
                    throw new NotSupportedException($"Exmo does not support {type}");
                }
                default:
                {
                    return new[] {"BTC", "XRP"};
                }
            }
        }
    }
}