using AVS.CoreLib.Data.Initializers;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Data;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Mappings.Market;

namespace AVS.Trading.Framework.Infrastructure
{
    public class EfStartUpTask : EfStartUpTaskBase<TradingDbContext>
    {
        protected override CreateTablesIfNotExist<TradingDbContext> GetInitializer()
        {
            var resolver = new MarketTableNameResolver();
            
            var tablesToValidate = new string[]
            {
                resolver.ResolveTableName(nameof(MarketData)),
                resolver.ResolveTableName(nameof(MarketTradeItem)),
                resolver.ResolveTableName(nameof(OrderBook))
            };
            var customCommands = new string[] { };
            return new CreateTablesIfNotExist<TradingDbContext>(tablesToValidate, customCommands);
        }
    }
}