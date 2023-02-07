using System;
using System.Collections.Generic;
using System.Threading;
using AVS.CoreLib._System.Net;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Tool.Models.Trading;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Controls.TradingTools.ModelFactories
{
    public class TradeHistoryModelFactory
    {
        private readonly IWorkContext _workContext;
        private readonly ITradingDataPreprocessor _dataPreprocessor;
        private readonly ITradingToolsService _tradingToolsService;

        public TradeHistoryModelFactory(IWorkContext workContext, ITradingDataPreprocessor dataPreprocessor, ITradingToolsService tradingToolsService)
        {
            _workContext = workContext;
            _dataPreprocessor = dataPreprocessor;
            _tradingToolsService = tradingToolsService;
        }

        public IList<TradeItemModel> GetTradeHistoryModel(ITradeHistoryFilters filters, Action<string, int> reportProgress)
        {
            var items = LoadTradeHistory(filters, reportProgress);
            IPairProvider pairProvider = _workContext.Client.Pairs;
            var model = new List<TradeItemModel>(items.Count);
            foreach (var myTradeItem in items)
            {
                var modelItem = new TradeItemModel();
                modelItem = myTradeItem.Map(modelItem);
                modelItem.Market = CurrencyPair.Parse(myTradeItem.Pair).ToMarketString();
                model.Add(modelItem);
            }
            return model;
        }

        private IList<TradeItem> LoadTradeHistory(ITradeHistoryFilters filters, Action<string, int> reportProgress)
        {
            var pair = CurrencyPair.Parse(filters.Market);

            reportProgress("Loading trades..", 1);
            IList<TradeItem> tradeItems;
            if (pair == null)
            {
                if (filters.DateRange != null)
                {
                    var response = _tradingToolsService.LoadAllTrades(filters.DateRange.From.ToUniversalTime(), filters.DateRange.To.ToUniversalTime());
                    if (!response.Success)
                        throw new LoadDataException($"Load trades failed: {response.Error}");
                    
                    reportProgress("Processing trades..", 90);
                    tradeItems = _dataPreprocessor.PreprocessTrades(response.Data);
                }
                else
                {
                    var response = _tradingToolsService.LoadAllTrades();
                    if (!response.Success)
                        throw new LoadDataException($"Load trades failed: {response.Error}");

                    reportProgress("Processing trades..", 80);
                    tradeItems = _dataPreprocessor.PreprocessTrades(response.Data);
                    reportProgress("Loaded trades.. #" + tradeItems.Count, 90);
                }
            }
            else
            {
                Response<IList<ITrade>> response = null;
                if (filters.DateRange != null)
                {
                    response = _tradingToolsService.LoadTrades(pair.ToString(),
                        filters.DateRange.From.ToUniversalTime(),
                        filters.DateRange.To.ToUniversalTime());
                }
                else
                {
                    response = _tradingToolsService.LoadTrades(pair.ToString());
                }

                if (!response.Success)
                    throw new LoadDataException($"Load trades failed: {response.Error}");

                var trades = response.Data;
                reportProgress("Processing trades..", 80);
                tradeItems = _dataPreprocessor.PreprocessTrades(trades, filters.Market);
                reportProgress("Loaded trades.. #" + tradeItems.Count, 90);
            }

            //_importDataService.ImportTradeHistory(tradeHistory);
            tradeItems = tradeItems != null ? filters.ApplyFilters(tradeItems) : new List<TradeItem>();
            return tradeItems;
        }
    }
}