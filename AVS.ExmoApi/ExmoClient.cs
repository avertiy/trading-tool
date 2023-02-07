using System.Globalization;
using System.Text;
using AVS.ExmoApi.Infrastructure;
using AVS.ExmoApi.MarketTools;
using AVS.ExmoApi.TradingTools;
using AVS.ExmoApi.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;

namespace AVS.ExmoApi
{
    public class ExmoClient : ExchangeClient
    {
        public override string Exchange => ExmoConstants.ExmoExchange;
        public override bool SupportMarginTrading => false;
        public new ExmoMarketApi Markets { get; }
        public new ExmoTradingApi Trading { get; }
        public new ExmoWalletApi WalletTools { get; }

        public ExmoClient(ApiKey key)
            : base(new ExmoAccount(key), new ExmoPairProvider())
        {
            Trading = new ExmoTradingApi(Account.PrivateApiClient);
            Markets = new ExmoMarketApi(Account.PublicApiClient);
            WalletTools = new ExmoWalletApi(Account.PrivateApiClient);
            Setup(Markets, Trading, null, WalletTools, null);
        }
    }

    public class ExmoAccount : ExchangeAccount
    {
        public ExmoAccount(ApiKey key) : base(key)
        {
            PrivateApiClient.Options.BaseAddress = "https://api.exmo.com/v1/";
            PrivateApiClient.Options.RelativeUrl = "";
            PrivateApiClient.Encoding = Encoding.UTF8;
            PublicApiClient.Options.BaseAddress = "https://api.exmo.com/v1/";
            PublicApiClient.Options.RelativeUrl = "";
        }
    }
}