using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Framework.Utils;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;

using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools.Chart;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using MarketData = AVS.Trading.Data.Domain.MarketTools.MarketData;
using OrderBook = AVS.Trading.Data.Domain.MarketTools.OrderBook;

namespace AVS.Trading.Framework.Services.MarketTools
{
    public class MarketDataPreprocessor : IMarketDataPreprocessor
    {
        public IList<MarketData> PreprocessTickerData(IDictionary<string, IMarketData> items, params string[] filterPairs)
        {
            var date = DateTime.UtcNow;
            var list = new List<MarketData>(items.Count);
            foreach (KeyValuePair<string, IMarketData> market in items.OrderBy(kp => kp.Key.ToString()))
            {
                if (filterPairs.Length > 0 && !filterPairs.Contains(market.Key))
                    continue;

                var entity = new MarketData();
                entity = market.Value.Map(entity);
                entity.Pair = market.Key;
                entity.DateUtc = date;
                list.Add(entity);
            }
            return list;
        }

        public OrderBook PreprocessOrderBook(IPublicOrderBook orderBook)
        {
            return PreprocessOrderBook(orderBook, null, Constants.OrderBookPriceCutKoefficient);
        }

        public OrderBook PreprocessOrderBook(IPublicOrderBook orderBook, double? amountBaseThreshold, double koef)
        {
            var helper = new OrderBookHelper();
            if (amountBaseThreshold.HasValue)
                helper.OrderTotalThreshold = amountBaseThreshold.Value;
            else
                helper.DynamicThreshold = true;

            helper.Initialize(orderBook, koef);
            return helper.CreateOrderBook(orderBook.Pair);
        }

        public Chart PreprocessChartData(IList<ICandlestick> candles, string pair, MarketPeriod period,
            DateTime from, DateTime to)
        {
            var data = new Chart(candles.Count)
            {
                From = from,
                To = to,
                Period = period,
                Pair = pair,
                CreatedUtc = DateTime.UtcNow,
                LastUpdateUtc = DateTime.UtcNow,
                Open = candles.First().Open,
                Close = candles.Last().Close,
                Low = candles.First().Low,
                High = candles.First().High,
                LongCandles = 0
            };

            data.Change = (data.Close - data.Open) / data.Open;

            foreach (ICandlestick candle in candles)
            {
                var entity = new ChartDataItem();
                entity = candle.Map(entity);
                entity.TimeStampUtc = candle.Time;

                if (entity.Low < data.Low)
                    data.Low = entity.Low;
                if (entity.High > data.High)
                    data.High = entity.High;
                data.VolumeBase += entity.VolumeBase;
                data.VolumeQuote += entity.VolumeQuote;
                data.Candlesticks.Add(entity);

                var candleDelta = entity.IsGrowingCandle
                        ? (entity.Close - entity.Open) / entity.Open
                        : (entity.Open - entity.Close) / entity.Close;
                if (candleDelta > 0.004)
                    data.LongCandles++;
            }

            return data;
        }

        public IList<MarketTradeItem> PreprocessTrades(IList<IMarketTrade> trades, string pair)
        {
            var items = new List<MarketTradeItem>();
            foreach (IMarketTrade trade in trades)
            {
                var item = new MarketTradeItem();
                item = trade.Map(item);
                item.Pair = pair;
                //item.AmountQuote = item.AmountQuote.Normalize();
                //item.AmountBase = item.AmountQuote.Normalize();
                items.Add(item);
            }

            return items;
        }
    }
}
