using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels.TradingTools;
using AVS.PoloniexApi.PoloniexCommands;
using AVS.PoloniexApi.Services;
using AVS.PoloniexApi.TradingTools.Models;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.Services;

namespace AVS.PoloniexApi.TradingTools
{
    
    public class PoloniexTradingApi : ApiToolsBase, ITradingApi
    {
        private SymbolService _symbolService;

        public PoloniexTradingApi(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
            _symbolService = new PoloniexSymbolService();
        }

        #region Async
        
        public async Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair)
        {
            pair.EnsureHasValue();
            var result = await ExecuteAsync(TradingCommands.ReturnOpenOrders, new Dictionary<string, object> { { "currencyPair", _symbolService.PairToSymbol(pair) } });
            //no need to map async due to list size is not so big so it's ok to map without async
            var response = result.AsList<ILimitOrder>().Map<LimitOrder>();
            return response;
        }

        #region GetTrades
        public Task<TradeHistory> GetTradesAsync(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
                return GetTradesAsync(request.Pair, request.From.Value, request.To.Value, request.Limit);
            return GetTradesAsync(request.Pair, request.Limit);
        }

        private async Task<TradeHistory> GetTradesAsync(PairString pair, DateTime startTime, DateTime endTime, int limit = 10000)
        {
            pair.EnsureHasValue();

            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair) },
                { "start", startTime.DateTimeToUnixTimeStamp() },
                { "end", endTime.DateTimeToUnixTimeStamp() },
                { "limit", limit },//if limit is not specified Poloniex returns only last 500 trades
            };

            var result = await ExecuteAsync(TradingCommands.ReturnTradeHistory, postData);
            var response = result.AsIEnumerable<ITrade>().Map<TradeHistory, Trade>();
            return response;
        }

        private async Task<TradeHistory> GetTradesAsync(PairString pair, int limit = 10000)
        {
            pair.EnsureHasValue();

            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair) },
                { "limit", limit }
            };

            var result = await ExecuteAsync(TradingCommands.ReturnTradeHistory, postData);
            var response = result.AsIEnumerable<ITrade>().Map<TradeHistory, Trade>();
            return response;
        } 
        #endregion

        public async Task<Response<IPostOrderResult>> PostOrderAsync(PairString pair, OrderType type, 
            double pricePerCoin, double amountQuote)
        {
            pair.EnsureHasValue();
            var result = await ExecuteAsync(type == OrderType.Buy ? TradingCommands.Exchange.Buy : TradingCommands.Exchange.Sell,
                    $"currencyPair={_symbolService.PairToSymbol(pair)}&rate={pricePerCoin.ToStringNormalized()}&amount={amountQuote.ToStringNormalized()}")
                .ConfigureAwait(false);
            return result.AsObject<IPostOrderResult>().Map<PostOrder>();
        }

        #endregion

        #region GetAllTrades & GetTrades
        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
                return GetAllTrades(request.From.Value, request.To.Value, request.Limit);
            return GetAllTrades(request.Limit);
        }

        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(int limit = 10000)
        {
            var postData = new Dictionary<string, object> {
                { "currencyPair", "all" },
                { "limit", limit }
            };
            var result = Execute(TradingCommands.ReturnTradeHistory, postData);
            var response = result.AsDictionary<string, IList<ITrade>>().Map<Trade>();
            return response;
        }

        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(DateTime startTime, DateTime endTime, int limit = 10000)
        {
            Response<IDictionary<string, IList<ITrade>>> response;

            //т.к. полоникс ограничивает выдачу 10 000 трейдов то когда лоадим трейды по всем маркетам 
            //может не хватать лимита
            //поэтому делаем лоадинг порционно
            const int limitDays = 10;
            var daysRange = (startTime - endTime).Days;
            if (daysRange <= limitDays)
            {
                var postData = new Dictionary<string, object> {
                    { "currencyPair", "all" },
                    { "start", startTime.DateTimeToUnixTimeStamp() },
                    { "end", endTime.DateTimeToUnixTimeStamp() },
                    { "limit", limit }
                };

                var result = Execute(TradingCommands.ReturnTradeHistory, postData);
                response = result.AsDictionary<string, IList<ITrade>>().Map<Trade>();
                return response;
            }

            response = GetAllTrades(startTime, startTime.AddDays(limitDays));

            //r=19 /10 = 1     
            for (int i = 1; i <= daysRange / limitDays; i++)
            {
                var from = startTime.AddDays(limitDays * i);
                var to = from.AddDays(limitDays);
                if (i == daysRange / limitDays)
                {
                    to = endTime;
                }
                var response2 = GetAllTrades(from, to);
                response.Data.Merge(response2.Data, t => t.IdTrade, t => t.DateUtc >= from && t.DateUtc <= to);
            }

            return response;
        }

        public Response<IList<ITrade>> GetTrades(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
                return GetTrades(request.Pair, request.From.Value, request.To.Value, request.Limit);
            return GetTrades(request.Pair, request.Limit);
        }

        public Response<IList<ITrade>> GetTrades(PairString pair, int limit = 10000)
        {
            pair.EnsureHasValue();
            
            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair) },
                { "limit", limit }
            };

            var result = Execute(TradingCommands.ReturnTradeHistory, postData);
            var response = result.AsList<ITrade>().Map<Trade>();
            return response;
        }

        public Response<IList<ITrade>> GetTrades(PairString pair, DateTime startTime, DateTime endTime, int limit = 10000)
        {
            pair.EnsureHasValue();
            

            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair) },
                { "start", startTime.DateTimeToUnixTimeStamp() },
                { "end", endTime.DateTimeToUnixTimeStamp() },
                { "limit", limit },//if limit is not specified Poloniex returns only last 500 trades
            };

            var result = Execute(TradingCommands.ReturnTradeHistory, postData);
            var response = result.AsList<ITrade>().Map<Trade>();
            return response;
        } 
        #endregion
        
        public Response<IList<ILimitOrder>> GetOpenOrders(PairString pair)
        {
            pair.EnsureHasValue();
            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair) }
            };

            var result = Execute(TradingCommands.ReturnOpenOrders, postData);
            var response = result.AsList<ILimitOrder>().Map<LimitOrder>();
            return response;
        }
        
        public Response<IPostOrderResult> PostOrder(PairString pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            pair.EnsureHasValue();

            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair)},
                { "rate", pricePerCoin.ToStringNormalized() },
                { "amount", amountQuote.ToStringNormalized() }
            };
            
            
            var command = type == OrderType.Buy ? TradingCommands.Exchange.Buy : TradingCommands.Exchange.Sell;

            var response = Execute(command, postData).AsObject<IPostOrderResult>().Map<PostOrder>();
            return response;
        }
        
        public Response<IPostOrderResult> MoveOrder(string orderId, double pricePerCoin)
        {
            var postData = new Dictionary<string, object> {
                { "orderNumber", orderId },
                { "rate", pricePerCoin.ToStringNormalized() },
            };

            var response = Execute(TradingCommands.MoveOrder, postData).AsObject<IPostOrderResult>().Map<PostOrder>();
            return response;
        }

        public SimpleResponse CancelOrder(PairString pair, string orderNumber)
        {
            pair.EnsureHasValue();
            var postData = new Dictionary<string, object> {
                { "currencyPair", _symbolService.PairToSymbol(pair) },
                { "orderNumber", orderNumber }
            };
            
            var reponse = Execute(TradingCommands.CancelOrder,postData).Deserialize<SimpleResponse>();
            return reponse;
        }

        public Response<IList<ITrade>> GetOrderTrades(string orderNumber)
        {
            var postData = new Dictionary<string, object> {
                { "orderNumber", orderNumber }
            };

            var result = Execute(TradingCommands.ReturnOrderTrades, postData);
            var response = result.AsList<ITrade>().Map<Trade>();
            return response;
        }
    }
}