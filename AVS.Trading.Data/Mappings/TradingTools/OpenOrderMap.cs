using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Data.Mappings.TradingTools
{
    public class OpenOrderMap : DynamicLoadEntityTypeConfiguration<OpenOrder>
    {
        public OpenOrderMap()
        {
            ToTable(nameof(OpenOrder), new TradingTableNameResolver());
            this.HasKey(p => p.Id);
            this.HasIndex(p => p.Pair);
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.Property(p => p.Exchange).HasMaxLength(10).IsRequired();

        }
    }
}