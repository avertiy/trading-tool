namespace AVS.PoloniexApi.PoloniexCommands
{
    public static class TradingCommands
    {
        /// <summary>
        /// Returns all of your available balances. 
        /// Sample output: {"BTC":"0.59098578","LTC":"3.31117268", ... } 
        /// </summary>
        public const string ReturnBalances = "returnBalances";
        /// <summary>
        /// Returns your open orders for a given market, specified by the "currencyPair" POST parameter, e.g. "BTC_XCP". 
        /// Set "currencyPair" to "all" to return open orders for all markets. 
        /// Sample output for single market: [{"orderNumber":"120466","type":"sell","rate":"0.025","amount":"100","total":"2.5"},{"orderNumber":"120467","type":"sell","rate":"0.04","amount":"100","total":"4"}, ... ]
        /// </summary>
        public const string ReturnOpenOrders = "returnOpenOrders";
        /// <summary>
        /// Returns your trade history for a given market, specified by the "currencyPair" POST parameter. 
        /// You may specify "all" as the currencyPair to receive your trade history for all markets. 
        /// You may optionally specify a range via "start" and/or "end" POST parameters, given in UNIX timestamp format; 
        /// if you do not specify a range, it will be limited to one day. 
        /// You may optionally limit the number of entries returned using the "limit" parameter, up to a maximum of 10,000. 
        /// If the "limit" parameter is not specified, no more than 500 entries will be returned. 
        /// </summary>
        /// <returns>
        /// Sample output: [
        /// { "globalTradeID": 25129732, "tradeID": "6325758", "date": "2016-04-05 08:08:40", "rate": "0.02565498", "amount": "0.10000000", 
        /// "total": "0.00256549", "fee": "0.00200000", "orderNumber": "34225313575", "type": "sell", "category": "exchange" }, 
        /// { "globalTradeID": 25129628, "tradeID": "6325741", "date": "2016-04-05 08:07:55", "rate": "0.02565499", 
        /// "amount": "0.10000000", "total": "0.00256549", "fee": "0.00200000", "orderNumber": "34225195693", "type": "buy", 
        /// "category": "exchange" }, ... ]
        /// </returns>
        public const string ReturnTradeHistory = "returnTradeHistory";
        /// <summary>
        /// Returns all trades involving a given order, specified by the "orderNumber" POST parameter. 
        /// If no trades for the order have occurred or you specify an order that does not belong to you, 
        /// you will receive an error. 
        /// </summary>
        public const string ReturnOrderTrades = "returnOrderTrades";
        /// <summary>
        /// Cancels an order you have placed in a given market. Required POST parameter is "orderNumber". 
        /// If successful, the method will return: {"success":1}
        /// </summary>
        public const string CancelOrder = "cancelOrder";
        /// <summary>
        /// Cancels an order and places a new one of the same type in a single atomic transaction, 
        /// meaning either both operations will succeed or both will fail. 
        /// Required POST parameters are "orderNumber" and "rate"; 
        /// you may optionally specify "amount" if you wish to change the amount of the new order. 
        /// "postOnly" or "immediateOrCancel" may be specified for exchange orders, but will have no effect on margin orders.
        /// Sample output: "success":1,"orderNumber":"239574176","resultingTrades":{"BTC_BTS":[]}}
        /// </summary>
        public const string MoveOrder = "moveOrder";

        public static class Exchange
        {
            /// <summary>
            /// Places a limit buy order in a given market. Required POST parameters are "currencyPair", "rate", and "amount". 
            /// If successful, the method will return the order number. 
            /// Sample output: {"orderNumber":31226040,"resultingTrades":[{"amount":"338.8732","date":"2014-10-18 23:03:21","rate":"0.00000173","total":"0.00058625","tradeID":"16164","type":"buy"}]}
            /// </summary>
            public const string Buy = "buy";
            /// <summary>
            /// Places a sell order in a given market. Parameters and output are the same as for the buy method.
            /// </summary>
            public const string Sell = "sell";

        }
        
        public static class Margin
        {
            /// <summary>
            /// Returns your current tradable balances for each currency in each market for which margin trading is enabled. Please note that these balances may vary continually with market conditions. 
            /// Sample output: {
            /// "BTC_DASH":{"BTC":"8.50274777","DASH":"654.05752077"},
            /// "BTC_LTC":{"BTC":"8.50274777","LTC":"1214.67825290"},
            /// "BTC_XMR":{"BTC":"8.50274777","XMR":"3696.84685650"}
            /// }
            /// </summary>
            public const string ReturnTradableBalances = "returnTradableBalances";
            public const string ReturnMarginAccountSummary = "returnMarginAccountSummary";
            public const string MarginBuy = "marginBuy";
            /// <summary>
            /// Places a margin sell order in a given market. Parameters and output are the same as for the marginBuy method.
            /// </summary>
            public const string MarginSell = "marginSell";
            /// <summary>
            /// Returns information about your margin position in a given market, specified by the "currencyPair" POST parameter. 
            /// You may set "currencyPair" to "all" if you wish to fetch all of your margin positions at once. If you have no margin position in the specified market, "type" will be set to "none". "liquidationPrice" is an estimate, and does not necessarily represent the price at which an actual forced liquidation will occur. 
            /// If you have no liquidation price, the value will be -1.
            /// </summary>
            public const string GetMarginPosition = "getMarginPosition";
            public const string CloseMarginPosition = "closeMarginPosition";

            /// <summary>
            /// Returns a summary of your entire margin account. 
            /// This is the same information you will find in the Margin Account section of the Margin Trading page, 
            /// under the Markets list. 
            /// </summary>
            public const string ReturnAvailableAccountBalances = "returnAvailableAccountBalances";
        }
    }

    
}