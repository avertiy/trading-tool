using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Net;
using AVS.CoreLib.Json;
using AVS.KunaApi.Models;
using AVS.KunaApi.Services;
using AVS.KunaApi.TradingTools.Models;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.KunaApi.TradingTools
{
   

    public static class KunaCommands
    {
        public static class V2
        {
            public static ApiCommand OpenOrders = new ApiCommand("orders","GET");
            public static ApiCommand PostOrder = new ApiCommand("orders","POST");
        }
    }
    public class KunaTradingApi : ApiToolsBase, ITradingApi
    {
        private readonly KunaSymbolService _symbolService;
        public KunaTradingApi(PrivateApiWebClient kunaPrivateClient) : base(kunaPrivateClient)
        {
            _symbolService = new KunaSymbolService();
        }

        #region get trades
        
        public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(GetTradesRequest request)
        {
            if (request.Pairs == null)
                throw new ArgumentException("Pairs are required");

            var dict = new Dictionary<string, IList<ITrade>>();

            var filterByDateRange = request.From.HasValue && request.To.HasValue;

            foreach (var pair in request.Pairs)
            {
                var trades = GetTrades(pair);
                if (trades.Success)
                {
                    dict.Add(pair,
                        filterByDateRange
                            ? trades.Data.Where(t => t.DateUtc >= request.From && t.DateUtc <= request.To).ToList()
                            : trades.Data);
                }
            }

            return new Response<IDictionary<string, IList<ITrade>>>() { Data = dict };
        }
        [DebuggerStepThrough]
        public Response<IList<ITrade>> GetTrades(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
                return GetTrades(request.Pair, request.From.Value, request.To.Value);
            return GetTrades(request.Pair);
        }
        
        public Response<IList<ITrade>> GetTrades(string market, DateTime from, DateTime to)
        {
            var response = GetTrades(market);
            if (response.Success)
            {
                var trades = response.Data;
                response.Data = trades.Where(trade => trade.DateUtc >= from && trade.DateUtc <= to).ToList();
            }

            return response;
        }

        public Response<IList<ITrade>> GetTrades(PairString pair)
        {
            var data = $"market={_symbolService.PairToSymbol(pair)}";
            var result = Execute("trades/my", data);
            var response = result.AsList<ITrade>().Map<KunaTrade>();
            return response;
        }

        public Response<IList<ITrade>> GetOrderTrades(string orderNumber)
        {
            //todo implement function
            return new Response<IList<ITrade>>();
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
            var jsonResult = Execute(KunaCommands.V2.OpenOrders, $"market={_symbolService.PairToSymbol(pair)}");
            var projection = jsonResult.AsList<ILimitOrder>();
            var response = projection.Map<LimitOrder>();
            return response;
        }

        public Response<IPostOrderResult> PostOrder(PairString pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            var postData = new Dictionary<string, object> {
                { "market", _symbolService.PairToSymbol(pair) },
                { "price", pricePerCoin },
                { "side", type.ToString().ToLower() },
                { "volume", amountQuote }
            };

            var jsonResult = Execute(KunaCommands.V2.PostOrder, postData);
            var response = jsonResult.AsObject<IPostOrderResult>().Map<KunaPostOrderResult>();
            return response;
        }

        public Response<IPostOrderResult> MoveOrder(string orderIdOrder, double orderPrice)
        {
            throw new NotImplementedException();
        }

        public SimpleResponse CancelOrder(PairString pair, string orderNumber)
        {
            //var jsonResult = Execute("order/delete", $"id={orderNumber}", method: "POST");
            //var response = jsonResult.AsObject<SimpleResponse>().Map<KunaCancelOrderResponse>();
            var response = Execute("order/delete", $"id={orderNumber}", method: "POST").Deserialize<KunaCancelOrderResponse>();
            if (response.Success)
                response.SetMessage($"Order #{orderNumber} canceled.");
            return response;
        }

        public async Task<Response<IPostOrderResult>> PostOrderAsync(PairString pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            var postData = new Dictionary<string, object> {
                { "market", _symbolService.PairToSymbol(pair) },
                { "price", pricePerCoin },
                { "side", type.ToString().ToLower() },
                { "volume", amountQuote }
            };

            var jsonResult = await ExecuteAsync("orders", postData, null, "POST").ConfigureAwait(false);
            var response = jsonResult.AsObject<IPostOrderResult>().Map<KunaPostOrderResult>();
            return response;
        }

        public async Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair)
        {
            pair.EnsureHasValue();
            var jsonResult = await ExecuteAsync("orders", $"market={_symbolService.PairToSymbol(pair)}");

            var response = jsonResult.AsList<ILimitOrder>().Map<LimitOrder>();
            return response;
        }

        //public async Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair)
        //{
        //    pair.EnsureHasValue();
        //    var jsonResult = await ExecuteAsync("user_open_orders", RequestData.Empty);//

        //    Response<IDictionary<string, IList<ILimitOrder>>> response = jsonResult.AsDictionary<string, IList<ILimitOrder>>().Map<LimitOrder>();
        //    var result = new Response<IList<ILimitOrder>>();

        //    if (response.Success && response.Data.ContainsKey(pair.Value))
        //        result.Data = response.Data[pair.Value];
        //    else
        //        result.Error = response.Error;

        //    return result;
        //}

        public Task<TradeHistory> GetTradesAsync(GetTradesRequest request)
        {
            throw new NotImplementedException();
        }

        
    }

    
}