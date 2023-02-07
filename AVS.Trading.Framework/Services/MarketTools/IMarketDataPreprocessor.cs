using System;
using System.Collections.Generic;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Data.Domain.MarketTools.Chart;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using MarketData = AVS.Trading.Data.Domain.MarketTools.MarketData;
using OrderBook = AVS.Trading.Data.Domain.MarketTools.OrderBook;

namespace AVS.Trading.Framework.Services.MarketTools
{
    public interface IMarketDataPreprocessor
    {
        IList<MarketData> PreprocessTickerData(IDictionary<string, IMarketData> items, 
            params string[] filterCurrencies);
        /// <summary>
        /// Creates  OrderBook from IOrderBook
        /// </summary>
        /// <param name="orderBook">order book</param>
        /// <param name="amountBaseThreshold">orders less than threshold are filtered out</param>
        /// <param name="priceRangeKoef">cuts orders out of the market price (1+-priceRangeKoef e.g. +-60% from current price) </param>
        OrderBook PreprocessOrderBook(IPublicOrderBook orderBook, double? amountBaseThreshold, double priceRangeKoef);

        OrderBook PreprocessOrderBook(IPublicOrderBook orderBook);


        Chart PreprocessChartData(IList<ICandlestick> data, string pair, MarketPeriod period, 
            DateTime from, DateTime to);
        
        IList<MarketTradeItem> PreprocessTrades(IList<IMarketTrade> trades, string pair);
    }
}