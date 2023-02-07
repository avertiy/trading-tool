using System;
using System.Collections.Generic;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;

namespace AVS.KunaApi
{
    public class KunaPairProvider : DefaultPairProvider
    {
        public override bool IsBaseCurrencyFirst => false;

        protected override void Initialize()
        {
            Pairs = new List<string>();
            Pairs.AddRange(CreateCombinations("UAH", "BNB,BCH,BTC,DASH,DAI,ETH,EOS,LINK,LTC,TRX,XEM,XLM,XRP,UNI,USDT,USDC,WAVES,ZEC"));
            Pairs.AddRange(CreateCombinations("BTC", "ETH"));
            Pairs.AddRange(CreateCombinations("USDT", "BCH,BTC,DASH,ETH,LTC,USD,XLM,XRP"));
            Pairs.AddRange(CreateCombinations("RUB", "BNB,BTC,ETH,USDT,XRP"));
        }

        public override string[] GetCoinsFor(AccountType type)
        {
            switch (type)
            {
                case AccountType.Lending:
                {
                    throw new NotSupportedException($"Kuna does not support {type}");
                }
                case AccountType.Margin:
                {
                    throw new NotSupportedException($"Kuna does not support {type}");
                }
                default:
                {
                    //* only coins that i'm interested in
                    return new[] { "BTC", "EOS", "DASH", "ETH",  "LTC", "XEM", "XRP", "WAVES","USDT", "ZEC" };
                }
            }
        }
    }
}