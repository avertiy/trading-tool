using System;
using System.Diagnostics;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Domain;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.MarketTools;

namespace AVS.Trading.Framework.Services.MarketTools
{
    public interface IMarketToolsServiceBase
    {
        TickerResponse GetTicker();

        /// <summary>
        /// Loads OrderBook form Poloniex
        /// depth 2000 is enough to cover ~ +-20% for the current price
        /// </summary>
        /// <returns>OrderBook</returns>
        Response<IPublicOrderBook> LoadOrderBook(string market, uint depth = 2000);

        //Response<IPublicOrderBook> OrderBook(PairString pair, uint depth = 2000);

        MarketTradeHistory LoadMarketTradeHistory(string market);
        MarketTradeHistory LoadMarketTradeHistory(string market, DateTime start, DateTime end);

        ChartData LoadChartData(string market, MarketPeriod period, DateTime start, DateTime end);
    }

    public class MarketToolsServiceBase : ExchangeServiceBase, IMarketToolsServiceBase
    {
        public MarketToolsServiceBase(IWorkContext workContext) : base(workContext)
        {
        }
        
        public TickerResponse GetTicker()
        {
            return Client.MarketTools.GetTicker();
        }
        /// <summary>
        /// loads open orders for a given market, specified by the "currencyPair"
        /// default depth is 2000 orders
        /// </summary>
        public Response<IPublicOrderBook> LoadOrderBook(string pair, uint depth = 2000)
        {
            return Client.MarketTools.GetOrderBook(pair, depth);
        }


        /// <summary>
        /// Loads the past 200 trades for a given market
        /// </summary>
        public MarketTradeHistory LoadMarketTradeHistory(string pair)
        {
            return Client.MarketTools.GetTrades(pair);
        }
        /// <summary>
        /// Loads up to 50,000 trades between a range for a given market
        /// </summary>
        public MarketTradeHistory LoadMarketTradeHistory(string pair, DateTime start, DateTime end)
        {
            var trades = Client.MarketTools.GetTrades(pair, start, end);
            return trades;
        }

        [DebuggerStepThrough]
        public ChartData LoadChartData(string pair, MarketPeriod period, DateTime start, DateTime end)
        {
            ChartData data = Client.MarketTools.GetChartData(pair, period, start, end);
            return data;
        }

        
    }
}
