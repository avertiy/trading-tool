using System;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.ResponseModels.MarketTools;

namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IMarketApi 
    {
        /// <summary>
        /// Gets a data summary of the markets available.
        /// Command: returnTicker
        /// </summary>
        TickerResponse GetTicker();

        /// <summary>
        /// https://poloniex.com/public?command=returnOrderBook&pair=PAIR&depth=50
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        Response<IPublicOrderBook> GetOrderBook(PairString pair, uint depth = 50);
        MarketTradeHistory GetTrades(PairString pair);
        MarketTradeHistory GetTrades(PairString pair, DateTime startTime, DateTime endTime);
        ChartData GetChartData(PairString pair, MarketPeriod period, DateTime startTime, DateTime endTime);

        #region Async
        /// <summary>
        /// Gets a data summary of the markets available.
        /// Command: returnTicker
        /// </summary>
        Task<TickerResponse> GetTickerAsync();

        /// <summary>
        /// Fetches the best priced orders for a given market.
        /// Command: ReturnOrderBook
        /// </summary>
        /// <param name="pair">The currency pair, which consists of the currency being traded on the market, and the base's code.</param>
        /// <param name="depth">The number of orders to fetch from each side.</param>
        Task<Response<IPublicOrderBook>> GetOrderBookAsync(PairString pair, uint depth = 50);

        /// <summary>Fetches the last 200 trades of a given market.
        /// Command: ReturnTradeHistory
        /// </summary>
        /// <param name="pair">The currency pair, which consists of the currency being traded on the market, and the base's code.</param>
        Task<MarketTradeHistory> GetTradesAsync(PairString pair);


        /// <summary>Fetches the trades of a given market in a given time period.</summary>
        /// <param name="pair">The currency pair, which consists of the currency being traded on the market, and the base's code.</param>
        /// <param name="startTime">The time to start fetching data from.</param>
        /// <param name="endTime">The time to stop fetching data at.</param>
        Task<MarketTradeHistory> GetTradesAsync(PairString pair, DateTime startTime, DateTime endTime);


        /// <summary>Fetches the chart data which Poloniex uses for their candlestick graphs for a market view of a given time period.</summary>
        /// <param name="pair">The currency pair, which consists of the currency being traded on the market, and the base's code.</param>
        /// <param name="period">The sampling frequency of the chart.</param>
        /// <param name="startTime">The time to start fetching data from.</param>
        /// <param name="endTime">The time to stop fetching data at.</param>
        Task<ChartData> GetChartDataAsync(PairString pair, MarketPeriod period, DateTime startTime,
            DateTime endTime); 
        #endregion

    }
}
