using System.Collections.Generic;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;

namespace AVS.PoloniexApi.General
{
    public class PoloniexPairProvider : DefaultPairProvider
    {
        public override bool SupportAllKeyword => true;

        protected override void Initialize()
        {
            Pairs = new List<string>();
            Pairs.AddRange(CreateCombinations("BTC", "ATOM,BCH,DASH,DOGE,ETH,LOOM,LTC,STR,SWAP,SXP,TRU,TRX,XMR,XRP"));
            Pairs.AddRange(CreateCombinations("USDT", "ATOM,BNB,BTC,DASH,DOGE,DOT,EOS,ETH,JST,LINK,LSK,LTC,OCEAN,REEF,STR,SWAP,SUN,TRU,TRX,TUSD,XMR,XRP"));
            Pairs.AddRange(CreateCombinations("USDT", "ADABULL,EOSBULL,LINKBULL,TRXBULL,XRPBULL"));
            Pairs.AddRange(CreateCombinations("USDT", "ADABEAR,EOSBEAR,LINKBEAR,TRXBEAR,XRPBEAR"));
            Pairs.AddRange(CreateCombinations("USDC", "ATOM,BCH,BTC,DASH,DOGE,EOS,ETH,LTC,STR,TRX,USDT,XMR,XRP"));
        }

        public override string[] GetCoinsFor(AccountType type)
        {
            switch (type)
            {
                case AccountType.Lending:
                {
                    return new[] { "ATOM", "BTC", "DASH", "DOGE", "EOS", "ETH", "LTC", "STR", "USDC", "USDT", "XMR", "XRP" };
                }
                case AccountType.Margin:
                {
                    return new[] { "ATOM","BTC", "DASH", "DOGE","EOS", "ETH", "LTC", "STR","USDC","USDT", "XMR", "XRP" };
                }
                default:
                {
                    //* only coins that i'm interested in
                    //todo move this into AppSettings
                    return new[] { "BTC", "BTS", "DASH", "DOGE", "EOS","ETH", "LSK", "LTC",  "STR", "USDC", "USDT", "XMR", "XRP", "ZEC","ZRX"};
                }
            }
        }
    }
}