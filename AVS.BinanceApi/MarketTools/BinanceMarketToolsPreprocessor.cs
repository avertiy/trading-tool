using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.BinanceApi.MarketTools.Models;
using AVS.BinanceApi.Services;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels.MarketTools;

namespace AVS.BinanceApi.MarketTools
{
    public class BinanceMarketToolsPreprocessor
    {
        private readonly BinanceSymbolService _symbolService;

        public BinanceMarketToolsPreprocessor(BinanceSymbolService symbolService)
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
                    var marketData = (BinanceMarketData)item;
                    var pair = _symbolService.SymbolToPair(marketData.Symbol);
                    result.Data.Add(pair, marketData);
                }
            });
        }

        public TickerResponse PreprocessTickers(Response<IDictionary<string, BinanceV2Tickers>> response)
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