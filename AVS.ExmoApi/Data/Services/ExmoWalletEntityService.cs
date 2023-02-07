using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.ExmoApi.Data.Domain;

namespace AVS.ExmoApi.Data.Services
{
    public class ExmoWalletEntityService : EntityServiceBase<ExmoWalletTransaction>
    {
        public ExmoWalletEntityService(IRepository<ExmoWalletTransaction> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public ExmoWalletTransaction GetLast()
        {
            var items = Repository.Table.OrderByDescending(t => t.DateUtc);
            return items.FirstOrDefault();
        }
    }
}