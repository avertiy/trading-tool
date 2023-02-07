using AVS.CoreLib.Data.EF;

namespace AVS.Trading.Data.Mappings.System
{
    public class SystemTableNameResolver : TableNameResolver
    {
        public SystemTableNameResolver() : base("", "System")
        {
        }
    }
}