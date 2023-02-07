using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Extensions;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Tool.Models.Market;
using AVS.Trading.Tool.Models.Trading;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Controls.MarketTools.ModelFactories
{
    public class MarketTradeHistoryModelFactory
    {
        private readonly IWorkContext _workContext;
        private readonly MarketToolsDataAdapter _dataAdapter;

        public MarketTradeHistoryModelFactory(IWorkContext workContext, MarketToolsDataAdapter dataAdapter)
        {
            _workContext = workContext;
            _dataAdapter = dataAdapter;
        }

        public IList<MarketTradeItemModel> GetTradeHistoryModel(ITradeHistoryFilters filters, Action<string, int> reportProgress)
        {
            var items = LoadTradeHistory(filters, reportProgress);
            IPairProvider pairProvider = _workContext.Client.Pairs;
            var model = new List<MarketTradeItemModel>(items.Count);
            foreach (var marketTradeItem in items)
            {
                var modelItem = new MarketTradeItemModel();
                modelItem = marketTradeItem.Map(modelItem);
                modelItem.Market = CurrencyPair.Parse(marketTradeItem.Pair).ToMarketString();
                model.Add(modelItem);
            }
            return model;
        }

        private IList<MarketTradeItem> LoadTradeHistory(ITradeHistoryFilters filters,
            Action<string, int> reportProgress)
        {
            IPairProvider pairProvider = _workContext.Client.Pairs;
            if (!pairProvider.TryGetPairByMarket(filters.Market, out string pair))
            {
                //if market is not recognized return empty list => nothing to display
                return new List<MarketTradeItem>();
            }

            reportProgress("Loading trades..", 1);

            var dateRange = filters.DateRange ?? new DateRange(DateTime.Now.AddHours(-2), DateTime.Now);

            IList<MarketTradeItem> tradeItems = null;


            var fromUtc = dateRange.From.ToUniversalTime();
            var toUtc = dateRange.To.ToUniversalTime();
            if ((toUtc - fromUtc).TotalMinutes < 15)
                fromUtc = fromUtc.AddHours(-2);

            tradeItems = _dataAdapter.GetMarketTradeHistory(pair, fromUtc, toUtc);

            reportProgress("Trades loaded, applying filters..", 50);

            if (filters.ReduceKoef.HasValue)
            {
                var threshold = 0.0;
                var trade = tradeItems.FirstOrDefault();
                if (filters.AmountMin.HasValue && trade != null && trade.Price > 0)
                {
                    threshold = filters.AmountMin.Value * trade.Price;
                }

                tradeItems = tradeItems.Reduce(filters.ReduceKoef.Value, threshold);
            }

            tradeItems = filters.ApplyFilters(tradeItems);
            return tradeItems;
        }
    }
}