using System;
using AVS.CoreLib.WinForms.MVC;
using AVS.PoloniexApi.General;
using AVS.PoloniexApi.LiveTools;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.MarketTools.Controllers
{
    
    public class OrderBookController: ControllerBase<IOrderBookView>
    {
        private readonly IMarketToolsService _loadMarketService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;
        private readonly IWorkContext _workContext;
        public event Action<OrderBook> Ready;
        public OrderBookController(IMarketToolsService loadMarketService, IMarketDataPreprocessor dataPreprocessor, IWorkContext workContext)
        {
            _loadMarketService = loadMarketService;
            _dataPreprocessor = dataPreprocessor;
            _workContext = workContext;
        }

        public OrderBook LoadOrderBook(uint depth = 2000)
        {
            if (_workContext.Exchange == PoloniexConstants.PoloniexExchange)
            {
                var channel = new PoloniexChannelClient();
            }

            return SafeExecute(() =>
            {
                var response = _loadMarketService.LoadOrderBook(View.Market, depth);
                if (!response.Success)
                {
                    View.StatusText = response.Error;
                }
                OrderBook orderBook = _dataPreprocessor.PreprocessOrderBook(
                    response.Data, 
                    View.AmountBaseThreshold, 
                    View.PriceRangeKoef);
                return orderBook;
            });
        }
        

        protected virtual void OnReady(OrderBook obj)
        {
            Ready?.Invoke(obj);
        }
    }
}