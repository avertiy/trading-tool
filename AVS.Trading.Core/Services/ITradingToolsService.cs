using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;
using ITrade = AVS.Trading.Core.Interfaces.TradingTools.ITrade;

namespace AVS.Trading.Core.Services
{
    public interface ITradingToolsService
    {
        //async methods
        Task<TradeHistory> LoadTradesAsync(PairString pair);
        Task<Response<IList<ILimitOrder>>> GetOpenOrdersAsync(PairString pair);
        Task<Response<IPostOrderResult>> SubmitOrderAsync(string market, OrderType type, double pricePerCoin,
            double amountQuote);


        Response<IDictionary<string, IList<ITrade>>> LoadAllTrades();
        
        Response<IDictionary<string, IList<ITrade>>> LoadAllTrades(DateTime startTime, DateTime endTime);


        /// <summary>
        /// Returns your trade history for a given market (currencyPair) for a one day
        /// </summary>
        Response<IList<ITrade>> LoadTrades(PairString pair);
        /// <summary>
        /// Returns your trade history for a given market* 
        /// *market e.g. LTC/BTC but pair is BTC_LTC or LTC_BTC depending on exchange
        /// in ITradingToolsService implementation market is translated into valid exchange pair
        /// so you can supply either market or pair as an argument
        /// </summary>
        Response<IList<ITrade>> LoadTrades(PairString pair, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Returns all trades involving a given order, specified by the "orderNumber" 
        /// </summary>
        Response<IList<ITrade>> GetOrderTrades(string orderNumber);

        /// <summary>
        /// Returns your open orders for a given market (currencyPair)
        /// </summary>
        Response<IList<ILimitOrder>> GetOpenOrders(PairString pair);

        /// <summary>
        /// Returns all your open orders
        /// </summary>
        IList<ILimitOrder> GetAllOpenOrders();

        ///// <summary>
        ///// Returns available balances for the quote and base currency of the currency pair
        ///// </summary>
        //Response<IDictionary<string, double>> GetAllBalances();

        /// <summary>
        /// Places a limit buy/sell order in a given market.  
        /// </summary>
        /// <returns>If successful, the method returns the order number.</returns>
        Response<IPostOrderResult> PostOrder(PairString pair, OrderType type, double pricePerCoin, double amountQuote);
        
        ///// <summary>
        ///// Places a limit buy/sell order in a given market.  
        ///// </summary>
        ///// <returns>If successful, the method sets the order number.</returns>
        //bool PostOrder(string pair, ILimitOrder order);

        bool MoveOrder(ILimitOrder order);

        SimpleResponse CancelOrder(PairString pair, string orderNumber);
    }
}