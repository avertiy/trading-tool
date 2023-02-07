using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Data.Mappings.TradingTools
{
    public class TradeItemMap : DynamicLoadEntityTypeConfiguration<TradeItem>
    {
        public TradeItemMap()
        {
            ToTable(nameof(TradeItem), new TradingTableNameResolver());
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.HasIndex(p => p.Pair);
            this.HasKey(p => p.Id);
            this.Property(p => p.Exchange).HasMaxLength(10).IsRequired();
            this.HasIndex(p => p.Exchange);
        }
    }
}
