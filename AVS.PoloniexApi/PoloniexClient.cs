using AVS.CoreLib.Json;
using AVS.PoloniexApi.General;
using AVS.PoloniexApi.LendingTools;
using AVS.PoloniexApi.MarketTools;
using AVS.PoloniexApi.TradingTools;
using AVS.PoloniexApi.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;

namespace AVS.PoloniexApi
{
    public class PoloniexClient: ExchangeClient
    {
        public PoloniexClient(ApiKey apiKey) 
            : base(new PoloniexAccount(apiKey), new PoloniexPairProvider())
        {
            var marketTools = new PoloniexMarketApiAsync(Account.PublicApiClient);
            var tradingTools = new PoloniexTradingApi(Account.PrivateApiClient);
            var marginTools = new MarginTradingApiAsync(Account.PrivateApiClient);
            var walletTools = new WalletApiAsync(Account.PrivateApiClient);
            var lendingTools = new LendingApi(Account.PublicApiClient, Account.PrivateApiClient);
            Setup(marketTools, tradingTools, marginTools, walletTools, lendingTools);
        }
        
        ///// <summary>A class which represents live data fetched automatically from the server.</summary>
        //public ILive Live { get; private set; }
        public override bool SupportMarginTrading => true;
        public override string Exchange => PoloniexConstants.PoloniexExchange;

    }
    
    public class PoloniexAccount : ExchangeAccount
    {
        public PoloniexAccount(ApiKey apikey) : base(apikey)
        {
            PublicApiClient.Options.BaseAddress = PoloniexConstants.ApiUrlHttpsBase;
            PublicApiClient.Options.RelativeUrl = PoloniexConstants.ApiUrlHttpsRelativePublic;
            PrivateApiClient.Options.BaseAddress = PoloniexConstants.ApiUrlHttpsBase;
            PrivateApiClient.Options.RelativeUrl = PoloniexConstants.ApiUrlHttpsRelativeTrading;
            PrivateApiClient.Options.AddCommandArg = true;
        }
    }
}
