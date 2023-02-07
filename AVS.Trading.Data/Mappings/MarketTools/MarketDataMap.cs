using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.MarketTools;
namespace AVS.Trading.Data.Mappings.Market
{
    public class MarketDataMap : DynamicLoadEntityTypeConfiguration<MarketData>
    {
        public MarketDataMap()
        {
            ToTable(nameof(MarketData), new MarketTableNameResolver());
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.HasIndex(p => p.Pair);
            this.HasKey(p => p.Id);
        }
    }
}