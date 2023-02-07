using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Core.Enums;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools.Chart;

namespace AVS.Trading.Data.Services.MarketTools
{
    public interface IChartDataEntityService : IEntityServiceBase<Chart, ChartDataItem>
    {
        Chart GetLastCandle(string market, MarketPeriod period);
    }

    public class ChartDataEntityService : EntityServiceBase<Chart, ChartDataItem>, IChartDataEntityService
    {
        public ChartDataEntityService(IRepository<Chart> repository, IEventPublisher eventPublisher, IRepository<ChartDataItem> itemRepository) 
            : base(repository, eventPublisher, itemRepository)
        {
        }
        
        public Chart GetLastCandle(string market, MarketPeriod period)
        {
            return Repository.Table.Where(t => t.Pair == market).OrderByDescending(t => t.From).FirstOrDefault();
        }
    }
}