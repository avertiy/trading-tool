using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Core.Interfaces.TradingTools
{
    public interface ITradingApi
    {
        Response<IDictionary<string, IList<ITrade>>> GetAllTrades(GetTradesRequest request);
        Response<IList<ITrade>> GetTrades(GetTradesRequest request);

        Response<IList<ILimitOrder>> GetOpenOrders(PairString pair);
        Response<IList<ITrade>> GetOrderTrades(string orderNumber);

        Response<IPostOrderResult> MoveOrder(string orderIdOrder, double orderPrice);
        Response<IPostOrderResult> PostOrder(PairString pair, OrderType type, double pricePerCoin, double amountQuote);
        SimpleResponse CancelOrder(PairString pair, string orderNumber);


        //async
        Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair);
        Task<TradeHistory> GetTradesAsync(GetTradesRequest request);
        Task<Response<IPostOrderResult>> PostOrderAsync(PairString pair, OrderType type, double pricePerCoin, double amountQuote);

    }

    public class GetTradesRequest
    {
        public int Offset = 0;
        public int Limit = 10000;
        public string[] Pairs;
        public DateTime? From;
        public DateTime? To;
        public PairString Pair;
    }
}