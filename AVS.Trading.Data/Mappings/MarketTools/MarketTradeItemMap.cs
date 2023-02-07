using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;

namespace AVS.Trading.Data.Mappings.Market
{
    public class MarketTradeItemMap : DynamicLoadEntityTypeConfiguration<MarketTradeItem>
    {
        public MarketTradeItemMap()
        {
            ToTable(nameof(MarketTradeItem), new MarketTableNameResolver());
            this.HasKey(p => p.Id);
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.HasIndex(p => p.Pair);
        }
    }
}