using System;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels.MarketTools;
using Jojatekok.PoloniexAPI.PoloniexCommands;
using AVS.PoloniexApi.MarketTools.Models;
using AVS.PoloniexApi.Services;
using AVS.Trading.Core.Domain;

namespace AVS.PoloniexApi.MarketTools
{
    public class PoloniexMarketApi : ApiToolsBase
    {
        protected readonly PoloniexSymbolService _symbolService;

        public PoloniexMarketApi(PublicApiWebClient apiWebClient) : base(apiWebClient)
        {
            _symbolService = new PoloniexSymbolService();
        }
        
        public TickerResponse GetTicker()
        {
            var jsonResult = Execute(PublicApiCommands.ReturnTicker, RequestData.Empty);
            var ticker = jsonResult.AsDictionary<string, IMarketData>().Map<TickerResponse,MarketData>();
            return ticker;
        }

        public Response<IPublicOrderBook> GetOrderBook(PairString pair, uint depth = 100)
        {
            //Default depth is 50. Max depth is 100
            var jsonResult = Execute(PublicApiCommands.ReturnOrderBook, 
                RequestData.Create($"currencyPair={_symbolService.PairToSymbol(pair)}&depth={depth}"));

            Response<IPublicOrderBook> response = jsonResult.AsObject<IPublicOrderBook>().Map<PoloniexPublicOrderBook>();
            
            if (response.Success)
                ((PoloniexPublicOrderBook)response.Data).Pair = pair;
            return response;
        }
        /// <summary>
        /// Returns the past 200 trades for a given market
        /// <seealso cref="PublicApiCommands.ReturnTradeHistory"/>
        /// </summary>
        public MarketTradeHistory GetTrades(PairString pair)
        {
            var jsonResult = Execute(PublicApiCommands.ReturnTradeHistory, "currencyPair=" + pair);
            var response = jsonResult.AsList<IMarketTrade>().Map<MarketTradeHistory, Trade>();
            return response;
        }
        /// <summary>
        /// Returns up to 50,000 trades between a range
        /// </summary>
        public MarketTradeHistory GetTrades(PairString pair, DateTime startTime, DateTime endTime)
        {
            object[] parameters = new[]
            {
                "currencyPair=" + _symbolService.PairToSymbol(pair),
                "start=" + startTime.DateTimeToUnixTimeStamp(),
                "end=" + endTime.DateTimeToUnixTimeStamp()
            };

            //poloniex limits output with 1000 records so it's about 1 day period
            var jsonResult = Execute(PublicApiCommands.ReturnTradeHistory, parameters);
            var response = jsonResult.AsList<IMarketTrade>().Map<MarketTradeHistory, Trade>();
            return response;
        }

        public ChartData GetChartData(PairString pair, MarketPeriod period, DateTime startTime, DateTime endTime)
        {
            object[] parameters = new[]
            {
                "currencyPair=" + _symbolService.PairToSymbol(pair),
                "start=" + startTime.DateTimeToUnixTimeStamp(),
                "end=" + endTime.DateTimeToUnixTimeStamp(),
                "period=" + (int) period
            };

            var jsonResult = Execute(PublicApiCommands.ReturnChartData, parameters);
            var response = jsonResult.AsList<ICandlestick>().Map<ChartData, Candlestick>();
            return response;
        }
    }
}
