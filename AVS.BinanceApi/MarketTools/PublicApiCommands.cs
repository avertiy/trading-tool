using AVS.CoreLib.ClientApi;

namespace AVS.BinanceApi.MarketTools
{
    public class PublicApiCommands
    {
        public static class V3
        {
            /// <summary>
            /// ticker/bookTicker - gets the best price/quantity on the order book for a symbol or all symbols if symbol is missing.
            /// </summary>
            public static ApiCommand BookTicker => new ApiCommand("ticker/bookTicker", "GET");

            public static ApiCommand OrderBook => new ApiCommand("depth","GET");
            /// <summary>
            /// symbol STRING  Required
            /// limit  INT     Default 500; max 1000.
            /// fromId LONG    Trade id to fetch from. Default gets most recent trades.
            /// </summary>
            public static ApiCommand HistoricalTrades => new ApiCommand("historicalTrades","GET");
            /// <summary>
            /// symbol STRING  Required
            /// limit  INT     Default 500; max 1000.
            /// /// </summary>
            public static ApiCommand Trades => new ApiCommand("trades","GET");
        }
    }
}