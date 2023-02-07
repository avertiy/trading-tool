using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.CoreLib.Data.EF;
using AVS.Trading.Data.Domain.System;

namespace AVS.Trading.Data.Mappings.System
{
    public class SyncRecordMap : DynamicLoadEntityTypeConfiguration<SyncRecord>
    {
        public SyncRecordMap()
        {
            ToTable(nameof(SyncRecord), new SystemTableNameResolver());
            this.HasKey(p => p.Id);
            this.Property(p => p.EntityName).HasMaxLength(55).IsRequired();
            this.HasIndex(p => p.EntityName);
        }
    }
}
