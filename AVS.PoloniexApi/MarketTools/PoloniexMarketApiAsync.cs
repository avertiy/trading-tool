using System;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels.MarketTools;
using Jojatekok.PoloniexAPI.PoloniexCommands;
using AVS.PoloniexApi.MarketTools.Models;
using AVS.Trading.Core.Domain;

namespace AVS.PoloniexApi.MarketTools
{
    public class PoloniexMarketApiAsync: PoloniexMarketApi, IMarketApi
    {
        public PoloniexMarketApiAsync(PublicApiWebClient apiWebClient) : base(apiWebClient)
        {
        }

        #region Async methods

        public async Task<TickerResponse> GetTickerAsync()
        {
            var jsonResult = await ExecuteAsync(PublicApiCommands.ReturnTicker, RequestData.Empty).ConfigureAwait(false);
            var ticker = jsonResult.AsDictionary<string, IMarketData>().Map<TickerResponse, MarketData>();
            return ticker;
        }

        public async Task<Response<IPublicOrderBook>> GetOrderBookAsync(PairString pair, uint depth = 50)
        {
            var jsonResult = await ExecuteAsync(PublicApiCommands.ReturnOrderBook, $"currencyPair={_symbolService.PairToSymbol(pair)}&depth={depth}").ConfigureAwait(false);

            Response<IPublicOrderBook> response = jsonResult.AsObject<IPublicOrderBook>().Map<PoloniexPublicOrderBook>();
            
            if (response.Success)
                ((PoloniexPublicOrderBook)response.Data).Pair = pair;
            return response;
        }

        public async Task<MarketTradeHistory> GetTradesAsync(PairString pair)
        {
            var jsonResult = await ExecuteAsync(PublicApiCommands.ReturnTradeHistory, $"currencyPair={_symbolService.PairToSymbol(pair)}").ConfigureAwait(false);
            var response = jsonResult.AsList<IMarketTrade>().Map<MarketTradeHistory, Trade>();
            return response;
        }
        
        /// <summary>
        /// Returns trade history. 
        /// Note: time frame should be no more than 1 month for 1 request
        /// </summary>
        public async Task<MarketTradeHistory> GetTradesAsync(PairString pair, DateTime startTime,
            DateTime endTime)
        {
            object[] parameters = new[]
            {
                "currencyPair=" + pair,
                "start=" + startTime.DateTimeToUnixTimeStamp(),
                "end=" + endTime.DateTimeToUnixTimeStamp()
            };
            var jsonResult = await ExecuteAsync(PublicApiCommands.ReturnTradeHistory, parameters).ConfigureAwait(false);
            var response = jsonResult.AsList<IMarketTrade>().Map<MarketTradeHistory, Trade>();
            return response;
        }

        public async Task<ChartData> GetChartDataAsync(PairString pair, MarketPeriod period, DateTime startTime,
            DateTime endTime)
        {
            object[] parameters = new[]
            {
                "currencyPair=" + _symbolService.PairToSymbol(pair),
                "start=" + startTime.DateTimeToUnixTimeStamp(),
                "end=" + endTime.DateTimeToUnixTimeStamp(),
                "period=" + (int) period
            };

            var jsonResult = await ExecuteAsync(PublicApiCommands.ReturnChartData, parameters).ConfigureAwait(false);
            var response = jsonResult.AsList<ICandlestick>().Map<ChartData, Candlestick>();
            return response;
        }
        
        #endregion

        
    }
}