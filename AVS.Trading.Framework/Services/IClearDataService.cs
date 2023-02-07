using AVS.Trading.Data.Services.MarketTools;
using AVS.Trading.Data.Services.TradingTools;

namespace AVS.Trading.Framework.Services
{
    public interface IClearDataService
    {
        int ClearMarketData();
        int ClearMarketTradeItems();
        int ClearMarketOrderBookData();
        int ClearChartData();
        int ClearTradeHistory();
    }

    public class ClearDataService : IClearDataService
    {
        private readonly IMarketDataEntityService _marketDataEntityService;
        //private readonly IMarketTradeHistoryEntityService _marketTradeHistoryEntityService;
        private readonly IMarketTradeItemEntityService _marketTradeItemEntityService;
        private readonly ITradeItemEntityService _tradeItemEntityService;
        private readonly IOrderBookEntityService _orderBookEntityService;
        private readonly IChartDataEntityService _chartDataEntityService;

        public ClearDataService(IMarketDataEntityService marketDataEntityService,
            IMarketTradeItemEntityService marketTradeItemEntityService, IOrderBookEntityService orderBookEntityService,
            IChartDataEntityService chartDataEntityService, ITradeItemEntityService tradeItemEntityService)
        {
            _marketDataEntityService = marketDataEntityService;
            _marketTradeItemEntityService = marketTradeItemEntityService;
            _orderBookEntityService = orderBookEntityService;
            _chartDataEntityService = chartDataEntityService;
            _tradeItemEntityService = tradeItemEntityService;
        }

        #region Clear markets data 
        public int ClearMarketOrderBookData()
        {
            return _orderBookEntityService.DeleteAll();
        }
        public int ClearMarketData()
        {
            return _marketDataEntityService.DeleteAll();
        }
        public int ClearChartData()
        {
            return _chartDataEntityService.DeleteAll();
        }
        public int ClearMarketTradeItems()
        {
            return _marketTradeItemEntityService.DeleteAll();
        }
        #endregion

        public int ClearTradeHistory()
        {
            return _tradeItemEntityService.DeleteAll();
        }
    }


    //public interface ISettings
    //{
    //}



}