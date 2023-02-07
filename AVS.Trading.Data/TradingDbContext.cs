using System.Data.Entity.Infrastructure;
using System.Reflection;
using AVS.CoreLib.Data.EF;

namespace AVS.Trading.Data
{
    public class TradingDbContext: BaseDbContext
    {
        public TradingDbContext(string nameOrConnectionString1, string connectionStringName2)
            : base(nameOrConnectionString1, connectionStringName2)
        {
            this.LazyLoadingEnabled = false;
        }

        protected override Assembly GetDbContextAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
