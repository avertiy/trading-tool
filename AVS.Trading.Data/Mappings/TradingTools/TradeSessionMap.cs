using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Data.Mappings.TradingTools
{
    public class TradeSessionMap : DynamicLoadEntityTypeConfiguration<TradeSession>
    {
        public TradeSessionMap()
        {
            ToTable(nameof(TradeSession), new TradingTableNameResolver());
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.HasIndex(p => p.Pair);
            this.Property(p => p.Algorithm).HasMaxLength(255);
            this.HasKey(p => p.Id);
            this.Property(p => p.Exchange).HasMaxLength(10).IsRequired();
            this.HasIndex(p => p.Exchange);
            this.Ignore(p => p.BalanceSheet);
        }
    }
}