using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels.TradingTools;
using Jojatekok.PoloniexAPI.WalletTools;
using AVS.PoloniexApi.PoloniexCommands;
using AVS.PoloniexApi.TradingTools.Models;
using AVS.Trading.Core.Extensions;

namespace AVS.PoloniexApi.TradingTools
{
    public class MarginTradingApi : ApiToolsBase
    {
        public MarginTradingApi(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
        }

        #region IMarginTrading

        public Response<IMarginPosition> GetMarginPosition(CurrencyPair pair)
        {
            var postData = new Dictionary<string, object> {
                { "currencyPair", pair }
            };

            //{
            // "amount": "40.94717831","total": "-0.09671314","basePrice": "0.00236190"," +
            // "liquidationPrice": -1,"pl": "-0.00058655","lendingFees": "-0.00000038","type": "long"
            //}

            var jsonResult = Execute(TradingCommands.Margin.GetMarginPosition, postData);
            var response =  jsonResult.AsObject<IMarginPosition>().Map<MarginPosition>();
            
            if (response.Success)
                response.Data.Market = pair.ToString();
            return response;
        }

        public Response<IList<IMarginPosition>> GetMarginPositions()
        {
            var postData = new Dictionary<string, object> {
                { "currencyPair", "all" }
            };

            var jsonResult = Execute(TradingCommands.Margin.GetMarginPosition, postData);
            return jsonResult.AsList<IMarginPosition>().Map<MarginPosition>();
        }

        public Response<IMarginAccountSummary> GetMarginAccountSummary()
        {
            var jsonResult = Execute(WalletCommands.ReturnMarginAccountSummary, RequestData.Empty);
            var summary = jsonResult.AsObject<IMarginAccountSummary>().Map<MarginAccountSummary>();
            return summary;
        }

        public IPlaceOrderResult PostMarginOrder(CurrencyPair currencyPair, double pricePerCoin, double amountQuote, OrderType type)
        {
            var postData = new Dictionary<string, object> {
                { "currencyPair", currencyPair },
                { "rate", pricePerCoin.ToStringNormalized() },
                { "amount", amountQuote.ToStringNormalized() }
            };
            var command = type == OrderType.Buy ? TradingCommands.Margin.MarginBuy : TradingCommands.Margin.MarginSell;
            var order = Execute(command, postData).Deserialize<PlaceOrderResult>();
            return order;
        }
        
        public IDictionary<string, IDictionary<string, double>> GetTradableBalances()
        {
            //Sample output for single market:
            //{
            // "BTC_DASH":{"BTC":"8.50274777","DASH":"654.05752077"},
            // "BTC_LTC":{"BTC":"8.50274777","LTC":"1214.67825290"},
            // "BTC_XMR":{"BTC":"8.50274777","XMR":"3696.84685650"}
            //}
            var data = Execute(TradingCommands.Margin.ReturnTradableBalances, RequestData.Empty).Deserialize<IDictionary<string, IDictionary<string, double>>>();
            return data != null && data.Any() ? data : new Dictionary<string, IDictionary<string, double>>();
        }

        public Response<IAvailableAccountBalances> GetAvailableAccountBalances()
        {
            return Execute(TradingCommands.Margin.ReturnAvailableAccountBalances, RequestData.Empty).AsObject<IAvailableAccountBalances>().Map<AvailableAccountBalances>();
        }

        #endregion
    }

    public class MarginTradingApiAsync : MarginTradingApi, IMarginTradingApi
    {
        public MarginTradingApiAsync(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
        }

        public async Task<Response<IPlaceOrderResult>> PostMarginOrderAsync(OrderType type, string pair,
            double pricePerCoin, double amountQuote, double lendingRate)
        {
            var data = RequestData.Create("");
            data.Add("currencyPair", pair);
            data.Add("rate", pricePerCoin.ToStringNormalized());
            data.Add("amount", amountQuote.ToStringNormalized());

            if(lendingRate > 0.0081/100)
                throw new Exception("Lending rate greater than 0.0081% is prohibited by AVS trading tool [Poloniex allows any rate].");
            
            data.Add("lendingRate", lendingRate.ToStringNormalized());
            
            var jsonResult = await ExecuteAsync(type == OrderType.Buy ? TradingCommands.Margin.MarginBuy : TradingCommands.Margin.MarginSell, data);

            return jsonResult.AsObject<IPlaceOrderResult>().Map<PlaceOrderResult>();
        }
    }
}