using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Data.Domain.MarketTools.Chart;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Services.MarketTools;
using MarketData = AVS.Trading.Data.Domain.MarketTools.MarketData;
using OrderBook = AVS.Trading.Data.Domain.MarketTools.OrderBook;

namespace AVS.Trading.Framework.Services.MarketTools
{
    /// <summary>
    /// _marketToolsService
    /// </summary>
    public interface IImportDataService
    {
        void ImportTickerData(IList<MarketData> data);
        bool ImportOrderBook(OrderBook book);
        void ImportTradeItems(IList<MarketTradeItem> tradeItems);
        void ImportChartData(Chart data);
    }
    
    public class ImportDataService : IImportDataService
    {
        private readonly IMarketDataEntityService _marketDataEntityService;
        private readonly IMarketTradeItemEntityService _marketTradeItemEntityService;
        private readonly IOrderBookEntityService _orderBookEntityService;
        private readonly IChartDataEntityService _chartDataEntityService;
        private readonly ISellOrderEntityService _sellOrderEntityService;
        private readonly IBuyOrderEntityService _buyOrderEntityService;

        public ImportDataService(IMarketDataEntityService marketDataEntityService,
            IMarketTradeItemEntityService marketTradeItemEntityService, IOrderBookEntityService orderBookEntityService, 
            IChartDataEntityService chartDataEntityService, ISellOrderEntityService sellOrderEntityService, 
            IBuyOrderEntityService buyOrderEntityService)
        {
            _marketDataEntityService = marketDataEntityService;
            _marketTradeItemEntityService = marketTradeItemEntityService;
            _orderBookEntityService = orderBookEntityService;
            _chartDataEntityService = chartDataEntityService;
            _sellOrderEntityService = sellOrderEntityService;
            _buyOrderEntityService = buyOrderEntityService;
        }

        #region Markets

        private DateTime? _lastTickerTimestamp;

        public void ImportTickerData(IList<MarketData> data)
        {
            if(data.Count == 0)
                return;
            //prevent import ticker too often (1 time per minute)
            if(_lastTickerTimestamp.HasValue && _lastTickerTimestamp.Value.AddMinutes(1) > DateTime.UtcNow)
                return;
            
            _lastTickerTimestamp = DateTime.UtcNow;
            _marketDataEntityService.BulkInsert(data);
        }
        /// <summary>
        /// Imports order book in case there were walls movements
        /// </summary>
        /// <returns>true if walls movement was detected and false when walls were not changed</returns>
        public bool ImportOrderBook(OrderBook orderBook)
        {
            var date = orderBook.TimeStampUtc.AddHours(-1);
            var book = _orderBookEntityService.LastOrDefault(b=>b.Pair == orderBook.Pair && b.TimeStampUtc > date);
            if (orderBook.IsEquivalentTo(book))
            {
                //optimization trick 
                //no need to import book with the same* orders stack 
                //same* - approximately same in regards to support and resistance levels
                return false;
            }

            var buyOrders = orderBook.BuyOrders.ToArray();
            var sellOrders = orderBook.SellOrders.ToArray();
            orderBook.BuyOrders.Clear();
            orderBook.SellOrders.Clear();
            _orderBookEntityService.Insert(orderBook);

            try
            {
                foreach (var buyOrder in buyOrders)
                {
                    buyOrder.OrderBookId = orderBook.Id;
                }
                _buyOrderEntityService.BulkInsert(buyOrders, 1000);

                foreach (var order in sellOrders)
                {
                    order.OrderBookId = orderBook.Id;
                }
                _sellOrderEntityService.BulkInsert(sellOrders, 1000);
            }
            catch (Exception ex)
            {
                throw new Exception("ImportOrderBook failed", ex);
            }
            return true;
        }

        public void ImportTradeItems(IList<MarketTradeItem> tradeItems)
        {
            _marketTradeItemEntityService.BulkInsert(tradeItems);
        }
    
        public void ImportChartData(Chart data)
        {
            var existingItems = _chartDataEntityService.GetAll(t => t.Pair == data.Pair && data.Period == t.Period && t.From >= data.From && t.To <= data.To);
            if (existingItems.Any())
            {
                foreach (var item in existingItems)
                {
                    _chartDataEntityService.Delete(item);
                }
            }

            var items = data.Candlesticks.ToArray();
            data.Candlesticks.Clear();
            data.LastUpdateUtc = DateTime.UtcNow;
            _chartDataEntityService.Insert(data);

            try
            {
                foreach (var item in items)
                {
                    item.ChartDataId = data.Id;
                }
                _chartDataEntityService.BulkInsert(items, 1000);
            }
            catch (Exception ex)
            {
                throw new Exception("ImportChartData failed", ex);
            }
        } 
        #endregion
    }
}