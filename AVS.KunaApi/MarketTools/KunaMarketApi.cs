using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Net;
using AVS.KunaApi.MarketTools.Models;
using AVS.KunaApi.Services;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;

using AVS.Trading.Core.ResponseModels.MarketTools;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.KunaApi.MarketTools
{
    public class KunaMarketApi : ApiToolsBase, IMarketApi
    {
        private readonly KunaMarketToolsPreprocessor _preprocessor;
        private readonly KunaSymbolService _symbolService;

        public KunaMarketApi(PublicApiWebClient apiWebClient) : base(apiWebClient)
        {
            _symbolService = new KunaSymbolService();
            _preprocessor = new KunaMarketToolsPreprocessor(_symbolService);
        }

        public TickerResponse GetTicker()
        {
            //version 3 api but it is not published yet
            //var jsonResult = Execute("tickers", "symbols=ALL");
            //var projection = jsonResult.AsList<IMarketData>();
            //var response = projection.Map<KunaMarketData>();
            //var ticker = _preprocessor.PreprocessTickers(response);
            var jsonResult = Execute("tickers", RequestData.Empty);
            
            var response = jsonResult.AsDictionary<string, KunaV2Tickers>().Key(k=> _symbolService.SymbolToPair(k)).Map<KunaV2Tickers>();

            var ticker = _preprocessor.PreprocessTickers(response);
            return ticker;
        }

        public Response<IPublicOrderBook> GetOrderBook(PairString pair, uint limit = 50)
        {
            var jsonResult = Execute("book", $"/{_symbolService.PairToSymbol(pair)}", "https://api.kuna.io/v3/");
            var projection = jsonResult.AsList<KunaOrderBookEntry>();
            var mapResult = projection.Map<KunaOrderBookEntry>();

            var response = mapResult.AsResponse<IPublicOrderBook>(data =>
            {
                var book = KunaOrderBook.From(data);
                book.Pair = pair;
                return book;
            });
            
            return response;
        }

        public MarketTradeHistory GetTrades(PairString pair)
        {
            throw new NotImplementedException();
            //var jsonResult = CreateCommand("trades").Execute(new[]
            //{
            //    "pair=" + pair,
            //    "limit=" + 200//limit with 200 the same as on poloniex, the maximum limit is 10000
            //});
            //var response = jsonResult.AsDictionary<string, IList<IMarketTrade>>().Map<MarketTrade>();
            //if (response.HasError)
            //{
            //    return new MarketTradeHistory() {Error = response.Error};
            //}
            //return new MarketTradeHistory() { Data = response.Data[pair]};
        }

        public MarketTradeHistory GetTrades(PairString pair, DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
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
            var jsonResult = await ExecuteAsync("tickers", RequestData.Empty).ConfigureAwait(false);
            //var ticker = jsonResult.AsDictionary<string, IMarketData>().Map<TickerResponse, KunaMarketData>();

            var response = jsonResult.AsDictionary<string, KunaV2Tickers>().Key(k => _symbolService.SymbolToPair(k)).Map<KunaV2Tickers>();
            var ticker = _preprocessor.PreprocessTickers(response);
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