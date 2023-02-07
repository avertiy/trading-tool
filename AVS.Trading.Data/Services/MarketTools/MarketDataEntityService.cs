using System;
using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Data.Services.MarketTools
{
    public interface IMarketDataEntityService : IEntityServiceBase<MarketData>
    {
        string[] GetMarkets();
        MarketData[] GetTodayMarketPrices(string market);
        MarketData GetMarketPrice(string market);
        int Shrink();
        int DeleteOldRecords(int days = 180);
    }

    public class MarketDataEntityService: EntityServiceBase<MarketData>, IMarketDataEntityService
    {
        public MarketDataEntityService(IRepository<MarketData> repository, IEventPublisher eventPublisher) :
            base(repository, eventPublisher)
        {
        }

        public MarketData[] GetTodayMarketPrices(string market)
        {
            var date = DateTime.UtcNow.Date;
            return Repository.Table.Where(d=>d.Pair == market && d.DateUtc > date).OrderBy(d=>d.DateUtc).ToArray();
        }

        public MarketData GetMarketPrice(string market)
        {
            var date = DateTime.UtcNow.Date;
            return Repository.Table.Where(d => d.Pair == market && d.DateUtc > date).OrderBy(d => d.DateUtc).FirstOrDefault();
        }

        public int Shrink()
        {
            var command = @"
WITH CTE AS(
            SELECT Pair, PriceLast, HighestBid, RN = ROW_NUMBER() OVER (PARTITION BY PriceLast ORDER BY DateUtc)
            FROM $TABLE
            )   
  DELETE FROM CTE where RN > 1";

            return ExecuteSqlCommand(command, true);
        }

        public int DeleteOldRecords(int days = 180)
        {
            var cmd = $"delete from $TABLE where DateUtc > (getdate() - {days})";
            return ExecuteSqlCommand(cmd, true);
        }


        public string[] GetMarkets()
        {
            return Repository.Table.Select(d => d.Pair).Distinct().ToArray();
        }
    }
}
