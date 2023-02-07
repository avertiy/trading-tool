using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Caching;
using AVS.Trading.Framework.Extensions;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Services.MarketTools;

namespace AVS.Trading.Framework.Adapters
{
    //it's kind of helper
    public class MarketToolsDataAdapter
    {
        private readonly IMarketTradeItemEntityService _tradeItemEntityService;
        private readonly IMarketDataEntityService _marketDataEntityService;
        private readonly IMarketToolsService _marketToolsService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        /// <summary>
        /// in seconds, if data in local database is older than it must be loaded
        /// </summary>
        private int MarketDataExpiration = 120;

        public MarketToolsDataAdapter(IMarketDataEntityService marketDataEntityService,
            IMarketToolsService marketToolsService, IMarketDataPreprocessor dataPreprocessor, 
            IMarketTradeItemEntityService tradeItemEntityService, ICacheManager cacheManager, IWorkContext workContext)
        {
            _marketDataEntityService = marketDataEntityService;
            _marketToolsService = marketToolsService;
            _dataPreprocessor = dataPreprocessor;
            _tradeItemEntityService = tradeItemEntityService;
            _cacheManager = cacheManager;
            this._workContext = workContext;
        }
        
        public double GetLastPrice(string market)
        {
            return _cacheManager.Get<double>($"{market}-last-price", () =>
            {
                var marketData = _marketDataEntityService.GetMarketPrice(market);
                //if no fresh data, load fresh data
                if (marketData.DateUtc < DateTime.UtcNow.AddSeconds(-MarketDataExpiration))
                {
                    var data = GetMarketData(market);
                    marketData = data.FirstOrDefault(d => d.Pair == market.ToString());
                }

                if (marketData == null)
                    return 0;
                return marketData.PriceLast;
            });
        }

        public IEnumerable<MarketData> GetMarketData(params string[] markets)
        {
            var pairs = _workContext.Client.Pairs.GetPairs(markets);
            var data = GetTickerData().Where(d => pairs.Contains(d.Pair));
            return data;
        }

        public IList<MarketData> GetTickerData()
        {
            return _cacheManager.Get("ticker-data", () =>
            {
                var pairs = _workContext.Client.Pairs.GetAllPairs();
                var reponse = _marketToolsService.GetTicker();
                IList<MarketData> tickerData = _dataPreprocessor.PreprocessTickerData(reponse.Data, pairs.ToArray());
                return tickerData;
            });
        }


        /// <summary>
        /// gets market trade history
        /// if date range is less than 2 weeks it queries poloniex
        /// if data range is greater than 2 weeks it  queries database due to poloniex returns a max 50 000 records per request
        /// </summary>
        public IList<MarketTradeItem> GetMarketTradeHistory(string pair, DateTime fromUtc, DateTime toUtc)
        {
            IList<MarketTradeItem> result = null;
            TimeSpan ts = toUtc - fromUtc;
            if (ts.TotalDays < 15)
            {
                var tradeHistory = _marketToolsService.LoadMarketTradeHistory(pair, fromUtc, toUtc);
                if (tradeHistory.Success)
                {
                    double
                        koef = Constants
                            .DefaultReduceKoefficient; //i.e. threshold will be 20% from average amount this will reduce really small orders
                    result = _dataPreprocessor.PreprocessTrades(tradeHistory.Data, pair).Reduce(koef);
                }
            }
            else
            {
                result = _tradeItemEntityService.Search(pair, fromUtc, toUtc, null);
            }
            return result;
        }
    }
}