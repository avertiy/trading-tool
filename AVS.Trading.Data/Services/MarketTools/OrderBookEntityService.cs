using System;
using System.Data.SqlClient;
using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.EF;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Data.Services.MarketTools
{
    public interface IOrderBookEntityService : IEntityServiceBase<OrderBook>
    {
        IQueryable<OrderBook> GetOrderBooksByDate(DateTime date);
        void ShrinkOpenOrders(DateTime date, string currencyPair);
    }

    public class OrderBookEntityService : EntityServiceBase<OrderBook>, IOrderBookEntityService
    {
        private readonly IDbContext _dbContext;
        public OrderBookEntityService(IRepository<OrderBook> repository,
            IEventPublisher eventPublisher, IDbContext dbContext) : base(repository, eventPublisher)
        {
            _dbContext = dbContext;
        }

        public IQueryable<OrderBook> GetOrderBooksByDate(DateTime date)
        {
            var toDate = date.AddDays(1);
            return Repository.Table.Where(b => b.TimeStampUtc >= date && b.TimeStampUtc < toDate);
        }

        public void ShrinkOpenOrders(DateTime date, string currencyPair)
        {
            var pDate = new SqlParameter("@date", date);
            var pCurrencyPair = new SqlParameter("@currencyPair", currencyPair);
            _dbContext.ExecuteSqlCommand("EXEC[dbo].[ShrinkOpenOrders] @date, @currencyPair", true, null, pDate,
                pCurrencyPair);
        }
    }

    public interface IBuyOrderEntityService : IEntityServiceBase<BuyOrder>
    {

    }

    public class BuyOrderEntityService : EntityServiceBase<BuyOrder>, IBuyOrderEntityService
    {
        public BuyOrderEntityService(IRepository<BuyOrder> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }
    }

    public interface ISellOrderEntityService : IEntityServiceBase<SellOrder>
    {

    }

    public class SellOrderEntityService : EntityServiceBase<SellOrder>, ISellOrderEntityService
    {
        public SellOrderEntityService(IRepository<SellOrder> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }
    }

}