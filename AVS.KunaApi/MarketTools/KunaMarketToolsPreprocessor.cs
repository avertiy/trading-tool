using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.KunaApi.MarketTools.Models;
using AVS.KunaApi.Services;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels.MarketTools;

namespace AVS.KunaApi.MarketTools
{
    public class KunaMarketToolsPreprocessor
    {
        private readonly KunaSymbolService _symbolService;

        public KunaMarketToolsPreprocessor(KunaSymbolService symbolService)
        {
            _symbolService = symbolService;
        }

        public TickerResponse PreprocessTickers(Response<IList<IMarketData>> response)
        {
            return response.To<TickerResponse>((list, result) =>
            {
                result.Data = new Dictionary<string, IMarketData>();
                foreach (var item in list)
                {
                    var marketData = (KunaMarketData)item;
                    var pair = _symbolService.SymbolToPair(marketData.Symbol);
                    result.Data.Add(pair, marketData);
                }
            });
        }

        public TickerResponse PreprocessTickers(Response<IDictionary<string, KunaV2Tickers>> response)
        {
            return response.To<TickerResponse>((dict, result) =>
            {
                result.Data = new Dictionary<string, IMarketData>();
                foreach (var kp in dict)
                {
                    var pair = _symbolService.SymbolToPair(kp.Key);
                    result.Data.Add(pair, kp.Value.Ticker);
                }
            });
        }

    }
}