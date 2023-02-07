using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;
using AVS.Trading.Core.Services;

namespace AVS.Trading.Framework.Services.TradingTools
{
    public class TradingToolsService : ExchangeServiceBase, ITradingToolsService
    {
        public TradingToolsService(IWorkContext workContext) : base(workContext)
        {
        }

        #region async

        public Task<TradeHistory> LoadTradesAsync(PairString pair)
        {
            return Client.TradingTools.GetTradesAsync(new GetTradesRequest() { Pair = pair });
        }

        public Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair)
        {
            return Client.TradingTools.GetOpenOrdersAsync(pair);
        }

        public Task<Response<IPostOrderResult>> SubmitOrderAsync(string pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            return Client.TradingTools.PostOrderAsync(pair, type, pricePerCoin, amountQuote);
        }

        #endregion


        public Response<IPostOrderResult> PostOrder(PairString pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            return Client.TradingTools.PostOrder(pair, type, pricePerCoin, amountQuote);
        }

        public Response<IDictionary<string, IList<ITrade>>> LoadAllTrades()
        {
            var request = new GetTradesRequest()
            {
                Limit = 10000, 
                Pairs = Client.Pairs.GetRecentPairs().ToArray()
            };
            return Client.TradingTools.GetAllTrades(request);
        }

        public Response<IDictionary<string, IList<ITrade>>> LoadAllTrades(DateTime startTime, DateTime endTime)
        {
            var request = new GetTradesRequest
            {
                Limit = 10000,
                From = startTime,
                To = endTime,
                Pairs = startTime >= DateTime.Now.AddMonths(-1)
                    ? Client.Pairs.GetRecentPairs().ToArray()
                    : Client.Pairs.GetAllPairs().ToArray()
            };

            return Client.TradingTools.GetAllTrades(request);
        }

        public Response<IList<ITrade>> LoadTrades(PairString pair)
        {
            return Client.TradingTools.GetTrades(new GetTradesRequest() {Pair = pair });
        }
        
        public Response<IList<ITrade>> LoadTrades(PairString pair, DateTime startTime, DateTime endTime)
        {
            return Client.TradingTools.GetTrades(new GetTradesRequest() { Pair = pair, From = startTime, To = endTime});
        }

        public Response<IList<ITrade>> GetOrderTrades(string orderNumber)
        {
            return Client.TradingTools.GetOrderTrades(orderNumber);
        }

        public Response<IList<ILimitOrder>> GetOpenOrders(PairString pair)
        {
            return Client.TradingTools.GetOpenOrders(pair);
        }
        
        public IList<ILimitOrder> GetAllOpenOrders()
        {
            throw new NotImplementedException();
        }
        

        public bool PostOrder(string pair, ILimitOrder order)
        {
            var response = Client.TradingTools.PostOrder(pair, order.Type, order.Price, order.AmountQuote);
            if (response.Success)
            {
                order.IdOrder = response.Data.IdOrder;
                return true;
            }

            return false;
        }

        public bool MoveOrder(ILimitOrder order)
        {
            var response = Client.TradingTools.MoveOrder(order.IdOrder, order.Price);
            if (response.Success)
            {
                order.IdOrder = response.Data.IdOrder;
                return true;
            }
            return false;
        }

        public SimpleResponse CancelOrder(PairString pair, string orderNumber)
        {
            return Client.TradingTools.CancelOrder(pair, orderNumber);
        }
    }
}

