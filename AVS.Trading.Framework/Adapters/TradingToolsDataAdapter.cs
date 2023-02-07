using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AVS.CoreLib.Caching;
using AVS.CoreLib.Extensions;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System.Debug;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Data.Services.System;
using AVS.Trading.Data.Services.TradingTools;

namespace AVS.Trading.Framework.Adapters
{
    public class TradingToolsDataAdapter
    {
        private readonly ITradingToolsService _tradingToolsService;
        private readonly ITradingDataPreprocessor _dataPreprocessor;
        private readonly ITradeItemEntityService _tradeItemEntityService;

        public TradingToolsDataAdapter(ITradingToolsService tradingToolsService, 
            ITradingDataPreprocessor dataPreprocessor, ITradeItemEntityService tradeItemEntityService)
        {
            _tradingToolsService = tradingToolsService;
            _dataPreprocessor = dataPreprocessor;
            _tradeItemEntityService = tradeItemEntityService;
        }

        public ExchangeClient Client
        {
            get => ((ExchangeServiceBase)_tradingToolsService).Client;
            set => ((ExchangeServiceBase)_tradingToolsService).Client = value;
        }

        private void Validate(string pair)
        {
            if (!Client.Pairs.GetAllPairs().Contains(pair))
                throw new ArgumentException($"Pair {pair} is not valid for {Client.Exchange}");
        }

        public TradeItem GetLastTrade(string pair, TradeCategory category)
        {
            Validate(pair);
            return _tradeItemEntityService.GetLastTrade(Client.Exchange, pair, category);
        }

        public IList<TradeItem> LoadLatestTrades(TradeItem lastTrade, string pair, int timespan = 600)
        {
            Validate(pair);
            DateTime from = DateTime.Today.StartOfYear();
            if (lastTrade != null)
            {
                from = lastTrade.DateUtc;
            }

            if (from < DateTime.UtcNow.AddSeconds(-timespan))
            {
                return LoadTrades(pair, from, DateTime.UtcNow);
            }

            return new List<TradeItem>();
        }

        /// <summary>
        /// loads market data via trading tools,
        /// does preprocessing of data and importing it into database
        /// </summary>
        private IList<TradeItem> LoadTrades(string pair, DateTime from, DateTime to)
        {
            var mytrades = _tradingToolsService.LoadTrades(pair, @from, to);
            List<TradeItem> tradeItems = _dataPreprocessor.PreprocessTrades(mytrades.Data, pair);
            _tradeItemEntityService.ImportTrades(tradeItems);
            return tradeItems;
        }

    }
}