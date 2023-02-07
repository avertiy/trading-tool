using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools.Chart;

namespace AVS.Trading.Data.Mappings.Market
{
    public class ChartMap : DynamicLoadEntityTypeConfiguration<Chart>
    {
        public ChartMap() 
        {
            ToTable(nameof(Chart), new MarketTableNameResolver());
            this.HasKey(p => p.Id);
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.HasMany(b => b.Candlesticks).WithRequired().HasForeignKey(i => i.ChartDataId).WillCascadeOnDelete(true);
        }
    }

    public class ChartDataItemMap : DynamicLoadEntityTypeConfiguration<ChartDataItem>
    {
        public ChartDataItemMap()
        {
            ToTable(nameof(ChartDataItem), new MarketTableNameResolver());
            this.HasKey(p => p.Id);
        }
    }
}