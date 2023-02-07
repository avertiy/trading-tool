using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Net;
using AVS.BinanceApi.Services;
using AVS.BinanceApi.TradingTools.Models;
using AVS.CoreLib._System;
using AVS.CoreLib.Extensions;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.BinanceApi.TradingTools
{
   public static class BinanceCommands
    {
        public static class V3
        {
            public static ApiCommand OpenOrders = new ApiCommand("openOrders", "GET");
            public static ApiCommand PostOrder = new ApiCommand("orders","POST");
            public static ApiCommand Trades = new ApiCommand("myTrades", "GET");
        }
    }

   public class BinanceTradingApi : ApiToolsBase, ITradingApi
   {
       private readonly BinanceSymbolService _symbolService;

       public BinanceTradingApi(PrivateApiWebClient BinancePrivateClient) : base(BinancePrivateClient)
       {
           _symbolService = new BinanceSymbolService();
       }

       #region get trades

       private Task<Response<IList<ITrade>>> RunLoadTradesTask(string pair, DateTime? @from, DateTime? to,
           Dictionary<string, IList<ITrade>> dict)
       {
           return Task.Run(() =>
           {
               var response = @from.HasValue && to.HasValue
                   ? GetTrades(pair, @from.Value, to.Value)
                   : GetTrades(pair);
               if (response.Success && response.Data.Any())
               {
                   dict.Add(pair, response.Data);
               }

               return response;
           });
       }

       public async Task<Response<IDictionary<string, IList<ITrade>>>> GetAllTradesAsync(GetTradesRequest request)
       {
           if (request.Pairs == null)
               throw new ArgumentException("Pairs are required");
           var dict = new Dictionary<string, IList<ITrade>>();
           var response = await RunLoadTradesTask(request.Pairs.First(), request.From, request.To, dict);
           if (response.Success)
           {
               var tasks = new List<Task>(request.Pairs.Length - 1);
               foreach (var pair in request.Pairs.Skip(1))
               {
                   tasks.Add(RunLoadTradesTask(pair, request.From, request.To, dict));
               }

               await Task.WhenAll(tasks.ToArray());
               return new Response<IDictionary<string, IList<ITrade>>>() {Data = dict};
           }

           return new Response<IDictionary<string, IList<ITrade>>>() {Error = response.Error };
       }

       public Response<IDictionary<string, IList<ITrade>>> GetAllTrades(GetTradesRequest request)
       {
           if (request.Pairs == null || !request.Pairs.Any())
               throw new ArgumentException("Pairs are required");

           var pair = request.Pairs.First();
           var response = request.From.HasValue && request.To.HasValue
               ? GetTrades(pair, request.From.Value, request.To.Value)
               : GetTrades(pair);
           if (!response.Success && response.Error.Contains("Unauthorized"))
           {
                return new Response<IDictionary<string, IList<ITrade>>>() {Error = "(401) Unauthorized" };
           }

           try
           {
               var data = ParallelLoadAsync(request.Pairs, (x) => request.From.HasValue && request.To.HasValue
                   ? this.GetTrades(x, request.From.Value, request.To.Value)
                   : this.GetTrades(x));

               return new Response<IDictionary<string, IList<ITrade>>>() {Data = data};
           }
           catch (Exception ex)
           {
               return new Response<IDictionary<string, IList<ITrade>>>() {Error = ex.Message};
           }
       }

       private Dictionary<string, IList<ITrade>> ParallelLoadAsync(IEnumerable<string> pairs,
           Func<string, Response<IList<ITrade>>> fn)
       {
           var dict = new Dictionary<string, IList<ITrade>>();
           var tasks = new List<Task>();
           string error = null;
           foreach (var pair in pairs)
           {
               tasks.Add(Task.Run(() =>
               {
                   var response = fn(pair);
                   if (!response.Success)
                   {
                       error = response.Error;
                   }
                   else if (response.Data.Any())
                   {
                       dict.Add(pair, response.Data);
                   }
               }));
           }

           Task.WaitAll(tasks.ToArray());
           if (dict.Any())
           {
               return dict;
           }

           if (error != null)
           {
               throw new LoadDataException(error);
           }

           return dict;
       }

       public Response<IList<ITrade>> GetTrades(GetTradesRequest request)
        {
            if (request.From.HasValue && request.To.HasValue)
            {
                return GetTrades(request.Pair, request.From.Value, request.To.Value);
            }

            return GetTrades(request.Pair);
        }
        
        private Response<IList<ITrade>> GetTrades(PairString pair, DateTime from, DateTime to)
        {
            var data = new RequestData($"symbol={_symbolService.PairToSymbol(pair)}&limit=1000");

            if ((to - from).TotalMinutes <= 24 * 60)
            {
                data.Add("startTime", from.ToUnixTimeMs().ToString());
                data.Add("endTime", to.ToUnixTimeMs().ToString());
            }

            var result = Execute(BinanceCommands.V3.Trades, data);
            var response = result.AsList<ITrade>().Map<BinanceTrade>();

            if (response.Success && response.Data.Any())
            {
                response.Data = response.Data.Where(t => t.DateUtc > from && t.DateUtc < to).ToList();
            }

            return response;
        }

        private Response<IList<ITrade>> GetTrades(PairString pair)
        {
            var data = new RequestData($"symbol={_symbolService.PairToSymbol(pair)}&limit=1000");
            var result = Execute(BinanceCommands.V3.Trades, data);
            var response = result.AsList<ITrade>().Map<BinanceTrade>();
            return response;
        }

        public Response<IList<ITrade>> GetOrderTrades(string orderNumber)
        {
            return new Response<IList<ITrade>>();
        } 
        #endregion

        public Response<IList<ILimitOrder>> GetOpenOrders(PairString pair)
        {
            pair.EnsureHasValue();
            var jsonResult = Execute(BinanceCommands.V3.OpenOrders, $"market={_symbolService.PairToSymbol(pair)}");
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

            var jsonResult = Execute(BinanceCommands.V3.PostOrder, postData);
            var response = jsonResult.AsObject<IPostOrderResult>().Map<BinancePostOrderResult>();
            return response;
        }

        public Response<IPostOrderResult> MoveOrder(string orderIdOrder, double orderPrice)
        {
            throw new NotImplementedException();
        }

        public SimpleResponse CancelOrder(PairString pair, string orderNumber)
        {
            //var jsonResult = Execute("order/delete", $"id={orderNumber}", method: "POST");
            //var response = jsonResult.AsObject<SimpleResponse>().Map<BinanceCancelOrderResponse>();
            var response = Execute("order/delete", $"id={orderNumber}", method: "POST").Deserialize<BinanceCancelOrderResponse>();
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
            var response = jsonResult.AsObject<IPostOrderResult>().Map<BinancePostOrderResult>();
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