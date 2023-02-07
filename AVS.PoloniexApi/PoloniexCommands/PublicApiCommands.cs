namespace Jojatekok.PoloniexAPI.PoloniexCommands
{
    public static class PublicApiCommands
    {
        public const string ReturnTicker = "returnTicker";
        public const string ReturnOrderBook = "returnOrderBook";
        /// <summary>
        /// Returns the past 200 trades for a given market, or up to 50,000 trades between a range specified in UNIX timestamps 
        /// by the "start" and "end" GET parameters. 
        /// </summary>
        public const string ReturnTradeHistory = "returnTradeHistory";
        public const string ReturnChartData = "returnChartData";
    }
}