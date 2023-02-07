using System;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Mappings.TradingTools;

namespace AVS.ExmoApi.Data.Domain
{
    public class ExmoWalletTransaction: BaseEntity
    {
        public DateTime DateUtc { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Provider { get; set; }
        public string Account { get; set; }
        public double Amount { get; set; }
    }

    public class ExmoWalletTransactionMap : DynamicLoadEntityTypeConfiguration<ExmoWalletTransaction>
    {
        public ExmoWalletTransactionMap()
        {
            ToTable("ExmoWallet", new TradingTableNameResolver());
            this.HasKey(p => p.Id);
        }
    }
}