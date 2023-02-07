using System.ComponentModel.DataAnnotations.Schema;
using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Data.Mappings.Market
{
    public class OrderBookMap : DynamicLoadEntityTypeConfiguration<OrderBook>
    {
        public OrderBookMap()
        {
            ToTable(nameof(OrderBook), new MarketTableNameResolver());
            this.HasKey(p => p.Id);
            this.HasIndex(p => p.Pair);
            this.Property(p => p.Pair).HasMaxLength(12).IsRequired();
            
            this.HasMany(b=>b.BuyOrders).WithRequired().HasForeignKey(i=>i.OrderBookId).WillCascadeOnDelete(true);
            this.HasMany(b=>b.SellOrders).WithRequired().HasForeignKey(i=>i.OrderBookId).WillCascadeOnDelete(true);
        }
    }

    //public class WallMap : DynamicLoadEntityTypeConfiguration<Wall>
    //{
    //    public WallMap()
    //    {
    //        //AmountQuote has been added recently just not to add extra sql work tmp ignore them
    //        this.Ignore(b => b.AmountQuote);
    //    }
    //}

    public class BuyOrderMap : DynamicLoadEntityTypeConfiguration<BuyOrder>
    {
        public BuyOrderMap()
        {
            Map(m =>
            {
                var resolver = new MarketTableNameResolver();
                m.MapInheritedProperties();
                m.ToTable(resolver.ResolveTableName(nameof(BuyOrder)), resolver.SchemaName);
            });
            this.Ignore(p => p.SumQuoteAmount);
            
            this.HasKey(p => p.Id);
            this.HasIndex(p => p.Price);
            this.HasIndex(p => p.OrderBookId);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class SellOrderMap : DynamicLoadEntityTypeConfiguration<SellOrder>
    {
        public SellOrderMap()
        {
            Map(m =>
            {
                var resolver = new MarketTableNameResolver();
                m.MapInheritedProperties();
                m.ToTable(resolver.ResolveTableName(nameof(SellOrder)), resolver.SchemaName);
            });
            this.Ignore(p => p.SumQuoteAmount);
            this.HasKey(p => p.Id);
            this.HasIndex(p => p.OrderBookId);
            this.HasIndex(p => p.Price);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}