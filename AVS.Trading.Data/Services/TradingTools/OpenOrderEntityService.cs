using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Data.Services.TradingTools
{
    public interface IOpenOrderEntityService : IEntityServiceBase<OpenOrder>
    {
        OpenOrder GetByRefNumber(string orderNumber);
        Task<IList<OpenOrder>> GetOpenOrdersAsync(ExchangePair pair);
    }

    public class OpenOrderEntityService : EntityServiceBase<OpenOrder>, IOpenOrderEntityService
    {
        public OpenOrderEntityService(IRepository<OpenOrder> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public OpenOrder GetByRefNumber(string orderNumber)
        {
            var result = Repository.Table.FirstOrDefault(o => o.OrderNumber == orderNumber);
            return result;
        }

        public async Task<IList<OpenOrder>> GetOpenOrdersAsync(ExchangePair pair)
        {
            return await Repository.Table.Where(o =>o.Exchange == pair.Exchange  && o.Pair == pair.Value && o.State == OrderState.Open).ToListAsync();
        }
    }
}