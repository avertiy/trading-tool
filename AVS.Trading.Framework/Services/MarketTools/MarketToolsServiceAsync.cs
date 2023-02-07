using System;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Core.ResponseModels.MarketTools;

namespace AVS.Trading.Framework.Services.MarketTools
{
    public interface IMarketToolsService : IMarketToolsServiceBase
    {
        Task<TickerResponse> GetTickerAsync();
        Task<Response<IPublicOrderBook>> LoadOrderBookAsync(string market, uint depth = 2000);
        Task<MarketTradeHistory> LoadTradeHystoryAsync(string market, DateTime start, DateTime end);
        Task<ChartData> LoadChartDataAsync(string market, MarketPeriod period, DateRange dateRange);
    }

    public class MarketToolsService : MarketToolsServiceBase, IMarketToolsService
    {
        public MarketToolsService(IWorkContext workContext) : base(workContext)
        {
        }

        public Task<TickerResponse> GetTickerAsync()
        {
            return Client.MarketTools.GetTickerAsync();
        }

        public Task<Response<IPublicOrderBook>> LoadOrderBookAsync(string pair, uint depth = 2000)
        {
            return Client.MarketTools.GetOrderBookAsync(pair, depth);
        }

        public Task<MarketTradeHistory> LoadTradeHystoryAsync(string pair, DateTime start, DateTime end)
        {
            return Client.MarketTools.GetTradesAsync(pair, start, end);
        }

        public Task<MarketTradeHistory> LoadTradeHystoryAsync(string pair)
        {
            return Client.MarketTools.GetTradesAsync(pair);
        }

        public Task<ChartData> LoadChartDataAsync(string pair, MarketPeriod period, DateRange dateRange)
        {
            if(dateRange == null)
                throw new ArgumentNullException(nameof(dateRange));
            return Client.MarketTools.GetChartDataAsync(pair, period, dateRange.From, dateRange.To);
        }
    }
}