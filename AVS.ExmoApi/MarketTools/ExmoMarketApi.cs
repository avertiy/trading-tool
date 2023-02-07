using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Net;
using AVS.ExmoApi.MarketTools.Models;
using AVS.ExmoApi.Services;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;

using AVS.Trading.Core.ResponseModels.MarketTools;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.ExmoApi.MarketTools
{
    public class ExmoMarketApi : ApiToolsBase, IMarketApi
    {
        private readonly ExmoSymbolService _symbolService;

        public ExmoMarketApi(PublicApiWebClient apiWebClient) : base(apiWebClient)
        {
            _symbolService = new ExmoSymbolService();
        }

        public TickerResponse GetTicker()
        {
            var jsonResult = Execute("ticker", RequestData.Empty);
            var projection = jsonResult.AsDictionary<string, IMarketData>();

            projection.Key(symbol => _symbolService.SymbolToPair(symbol));
            var ticker = projection.Map<TickerResponse, MarketData>();
            return ticker;
        }

        public Response<IPublicOrderBook> GetOrderBook(PairString pair, uint limit = 50)
        {
            var symbol = _symbolService.PairToSymbol(pair);
            var jsonResult = Execute("order_book", new[]
            {
                "pair=" + symbol,
                "limit=" + limit
            });

            jsonResult.Take($"\"{symbol}\":(?<data>{{.*?}})");

            var response = jsonResult.AsObject<IPublicOrderBook>().Map<Response<IPublicOrderBook>, ExmoOrderBook>();

            if (response.Success)
                response.Data.Pair = pair;

            return response;
        }

        public MarketTradeHistory GetTrades(PairString pair)
        {
            var jsonResult = Execute("trades", new[]
            {
                "pair=" + pair,
                "limit=" + 200//limit with 200 the same as on poloniex, the maximum limit is 10000
            });

            var response = jsonResult.AsDictionary<string, IList<IMarketTrade>>().Map<MarketTrade>();

            if (!response.Success)
            {
                return new MarketTradeHistory() {Error = response.Error};
            }
            return new MarketTradeHistory() { Data = response.Data[pair]};
        }

        public MarketTradeHistory GetTrades(PairString pair, DateTime startTime, DateTime endTime)
        {
            var jsonResult = Execute("trades", new[]
            {
                "pair=" + pair,
                "limit=" + 10000//limit with 200 the same as on poloniex, the maximum limit is 10000
            });

            var response = jsonResult.AsDictionary<string, IList<IMarketTrade>>().Map<MarketTrade>();

            if (!response.Success)
            {
                return new MarketTradeHistory() { Error = response.Error };
            }

            var data = response.Data.Any() ? response.Data[pair].Where(t => t.DateUtc > startTime && t.DateUtc <= endTime).ToList() : new List<IMarketTrade>();
            
            return new MarketTradeHistory() { Data =  data};
        }

        public ChartData GetChartData(PairString pair, MarketPeriod period, DateTime startTime, DateTime endTime)
        {
            throw new NotSupportedException();
            //var jsonResult = ExecuteCommand(PublicApiCommands.ReturnChartData, parameters);
            //var response = jsonResult.AsList<ICandlestick>().Map<ChartData, Candlestick>();
            //return response;
        }


        public async Task<TickerResponse> GetTickerAsync()
        {
            var jsonResult = await ExecuteAsync("ticker", RequestData.Empty).ConfigureAwait(false);
            var projection = jsonResult.AsDictionary<string, IMarketData>();
            projection.Key(symbol => _symbolService.SymbolToPair(symbol));
            var ticker = projection.Map<TickerResponse, MarketData>();
            return ticker;
        }

        public Task<Response<IPublicOrderBook>> GetOrderBookAsync(PairString pair, uint depth = 50)
        {
            throw new NotImplementedException();
        }

        public Task<MarketTradeHistory> GetTradesAsync(PairString pair)
        {
            throw new NotImplementedException();
        }

        public Task<MarketTradeHistory> GetTradesAsync(PairString pair, DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        public Task<ChartData> GetChartDataAsync(PairString pair, MarketPeriod period, DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }


        public LoanOrders GetLoanOrders(string currency)
        {
            throw new NotSupportedException();
        }
    }
}