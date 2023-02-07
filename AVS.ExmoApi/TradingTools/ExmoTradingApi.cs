using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Net;
using AVS.ExmoApi.Models;
using AVS.ExmoApi.Services;
using AVS.ExmoApi.TradingTools.Models;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.ExmoApi.TradingTools
{
    public class ExmoTradingApi : ApiToolsBase, ITradingApi
    {
        private readonly ExmoSymbolService _symbolService;
        public ExmoTradingApi(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
            _symbolService = new ExmoSymbolService();
        }

        #region get trades
        [DebuggerStepThrough]
        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
                return GetAllTrades(request.From.Value, request.To.Value, request.Offset, request.Pairs);
            return GetAllTrades(request.Offset, request.Limit, request.Pairs);
        }
        [DebuggerStepThrough]
        public Response<IList<ITrade>> GetTrades(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
                return GetTrades(request.Pair, request.From.Value, request.To.Value);
            return GetTrades(request.Pair, request.Offset, request.Limit);
        }

        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(DateTime from, DateTime to, int offset, params string[] pairs)
        {
            var limit = 10000;
            var response = GetAllTrades(offset, limit, pairs);
            if (response.Success)
            {
                var rowCount = response.Data.Sum(kp => kp.Value.Count);
                var exceedsLimit = rowCount >= limit;

                //filter items by date range
                DateTime minDate = to;
                var dict = new Dictionary<string, IList<ITrade>>();
                foreach (var kp in response.Data)
                {
                    var list = kp.Value.Where(trade => trade.DateUtc >= from && trade.DateUtc <= to).ToList();
                    if (exceedsLimit)
                    {
                        var d = list.Min(t => t.DateUtc);
                        if (d < minDate)
                            minDate = d;
                    }
                    dict.Add(kp.Key, list);
                }
                response.Data = dict;

                if (exceedsLimit)
                {
                    if (minDate > from)
                    {
                        var response2 = GetAllTrades(from, minDate, offset, pairs);
                        if (response2.Success)
                        {
                            dict.Merge(response2.Data, t => t.IdTrade, t => t.DateUtc >= from && t.DateUtc <= from);
                        }
                    }
                }
            }

            return response;
        }

        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(int offset = 0, int limit = 10000, params string[] pairs)
        {
            if (pairs == null)
                throw new ArgumentNullException(nameof(pairs));

            List<string> symbols = new List<string>();
            foreach (var pair in pairs) 
                symbols.Add(_symbolService.PairToSymbol(pair));

            string pairsStr = string.Join(",", symbols);

            var postData = new Dictionary<string, object> {
                { "pair", pairsStr},
                { "offset", offset},
                { "limit", limit }
            };

            var result = Execute("user_trades", postData);
            var response = result.AsDictionary<string, IList<ITrade>>().Key(k=>_symbolService.SymbolToPair(k)).Map<Trade>();
            return response;
        }

        public Response<IList<ITrade>> GetTrades(PairString pair, DateTime from, DateTime to, int offset = 0, int limit = 10000)
        {
            var response = GetTrades(pair, offset, limit);
            if (response.Success)
            {
                response.Data = response.Data.Where(trade => trade.DateUtc >= from && trade.DateUtc <= to).ToList();
                var trades = response.Data;
                var exceedsLimit = trades.Count >= limit;
                if (exceedsLimit)
                {
                    //set offset and request trandes using offset
                    var minDate = trades.Min(t => t.DateUtc);
                    if (minDate > from)
                    {
                        var response2 = GetTrades(pair, from, minDate, offset + limit);
                        if (response2.Success && response2.Data.Any())
                        {
                            trades.Merge(response2.Data, t => t.IdTrade, t => t.DateUtc >= from && t.DateUtc <= from);
                        }

                    }
                }
            }

            return response;
        }

        private Response<IList<ITrade>> GetTrades(PairString pair, int offset = 0, int limit = 10000)
        {
            pair.EnsureHasValue();
            var postData = new Dictionary<string, object> {
                { "pair", _symbolService.PairToSymbol(pair)},
                { "offset", offset},
                { "limit", limit }
            };

            var result = Execute("user_trades", postData);
            var response = result.AsDictionary<string, IList<ITrade>>().Key(key=> _symbolService.SymbolToPair(key))
                .Map<KeyedResponse<string, IList<ITrade>>, Trade>();
            return response.ToResponse(pair);
        }

        public Response<IList<ITrade>> GetOrderTrades(string orderNumber)
        {
            throw new NotImplementedException();
            //var postData = new Dictionary<string, object> {
            //    { "order_id", orderNumber }
            //};
            //var data = ExecuteCommand<ListResponse<Trade>>("order_trades", postData);
            //return data.ToListResponse<ITrade>();
        } 
        #endregion

        public Response<IList<ILimitOrder>> GetOpenOrders(PairString pair)
        {
            pair.EnsureHasValue();
            var symbol = _symbolService.PairToSymbol(pair);
            
            var jsonResult = Execute("user_open_orders", RequestData.Empty);
            var projection = jsonResult.AsDictionary<string, IList<ILimitOrder>>().Where(key => key == symbol);

            var response = projection.Map<LimitOrder>();

            var result = new Response<IList<ILimitOrder>>();

            if (response.Success)
                result.Data = response.Data.ContainsKey(symbol) ? response.Data[symbol] : new List<ILimitOrder>();
            else
                result.Error = response.Error;

            return result;
        }

        public Response<IPostOrderResult> PostOrder(PairString pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            pair.EnsureHasValue();
            var postData = new Dictionary<string, object> {
                { "pair", _symbolService.PairToSymbol(pair)},
                { "price", pricePerCoin.ToStringNormalized() },
                { "quantity", amountQuote.ToStringNormalized() },
                {"type", type == OrderType.Buy? "buy":"sell" }
            };

            var response = Execute("order_create", postData).AsObject<IPostOrderResult>().Map<PostOrder>();
            return response;
        }

        public Response<IPostOrderResult> MoveOrder(string orderIdOrder, double orderPrice)
        {
            throw new NotImplementedException();
        }

        public SimpleResponse CancelOrder(PairString pair, string orderNumber)
        {
            var response = Execute("order_cancel", $"order_id={orderNumber}").Deserialize<ExmoSimpleResponse>();
            if(response.Success)
                response.SetMessage($"Order #{orderNumber} canceled.");
            return response;
        }
        
        public async Task<Response<IPostOrderResult>> PostOrderAsync(PairString pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            pair.EnsureHasValue();
            var postData = new Dictionary<string, object> {
                { "pair", _symbolService.PairToSymbol(pair)},
                { "price", pricePerCoin.ToStringNormalized() },
                { "quantity", amountQuote.ToStringNormalized() },
                {"type", type == OrderType.Buy? "buy":"sell" }
            };

            var jsonResponse = await ExecuteAsync("order_create", postData).ConfigureAwait(false);
            var response = jsonResponse.AsObject<IPostOrderResult>().Map<PostOrder>();
            return response;
        }

        public async Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair)
        {
            pair.EnsureHasValue();
            var jsonResult = await ExecuteAsync("user_open_orders", RequestData.Empty);

            var response = jsonResult.AsDictionary<string, IList<ILimitOrder>>().Key(k=> _symbolService.SymbolToPair(k)).Map<LimitOrder>();
            var result = new Response<IList<ILimitOrder>>();

            if (response.Success && response.Data.ContainsKey(pair.Value))
                result.Data = response.Data[pair.Value];
            else
                result.Error = response.Error;

            return result;
        }

        public Task<TradeHistory> GetTradesAsync(GetTradesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}