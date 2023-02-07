using System;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.EF;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Data.Mappings.TradingTools;

namespace AVS.ExmoApi.Data.Domain
{
    public class ExmoTradeItem : BaseEntity, ITradeItem
    {
        public string Pair { get; set; }
        public DateTime DateUtc { get; set; }
        public TradeType Type { get; set; }
        public double Price { get; set; }
        public double AmountQuote { get; set; }
        public double AmountBase { get; set; }
        
        public string OrderId { get; set; }
        public string TradeId { get; set; }
        public double Fee { get; set; }
    }

    public class ExmoTradeItemMap : DynamicLoadEntityTypeConfiguration<ExmoTradeItem>
    {
        public ExmoTradeItemMap()
        {
            ToTable(nameof(ExmoTradeItem), new TradingTableNameResolver());
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            this.HasIndex(p => p.Pair);
            this.HasKey(p => p.Id);
        }
    }
}