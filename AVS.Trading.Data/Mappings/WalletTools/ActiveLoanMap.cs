using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.WalletTools;

namespace AVS.Trading.Data.Mappings.Wallet
{
    public class ActiveLoanMap : DynamicLoadEntityTypeConfiguration<ActiveLoan>
    {
        public ActiveLoanMap()
        {
            ToTable(nameof(ActiveLoan), new WalletTableNameResolver());
            this.HasKey(p => p.Id);
            this.Property(p => p.Currency).HasMaxLength(10).IsRequired();
        }
    }
}