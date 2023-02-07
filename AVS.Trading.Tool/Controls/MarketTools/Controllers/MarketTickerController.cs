using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Tool.Controls.MarketTools.ChildControls;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.MarketTools.Controllers
{
    public class MarketTickerController : ControllerBase<IMarketTickerView>
    {
        private readonly IMarketToolsService _loadMarketService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;

        public MarketTickerController(IMarketToolsService loadMarketService, IMarketDataPreprocessor dataPreprocessor)
        {
            _loadMarketService = loadMarketService;
            _dataPreprocessor = dataPreprocessor;
        }

        public void LoadMarketData(string pair)
        {
            if (string.IsNullOrEmpty(pair))
                return;
            //load can be done from poloniex or from local db if data filler works
            var response = _loadMarketService.GetTicker();
            if (!response.Success)
            {
                View.DisplayError(response.Error);
                return;
            }

            IList<MarketData> marketData = _dataPreprocessor.PreprocessTickerData(response.Data, pair);
            if(marketData.Count ==0)
                throw new ArgumentException($"Ticker does not contain {pair} pair");
            var data = marketData.First();
            View.SetMarketData(data);
        }

        public async Task<MarketData> LoadMarketDataAsync(string market)
        {
            if (string.IsNullOrEmpty(market))
                return null;
            var response = await _loadMarketService.GetTickerAsync();

            if (!response.Success)
            {
                View.DisplayError(response.Error);
                return null;
            }

            IList<MarketData> marketData = _dataPreprocessor.PreprocessTickerData(response.Data, market);
            var data = marketData.First();
            return data;
        }
    }
}