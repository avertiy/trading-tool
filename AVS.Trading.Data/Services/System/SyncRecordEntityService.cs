using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Data.Domain.System;

namespace AVS.Trading.Data.Services.System
{
    public interface ISyncRecordEntityService : IEntityServiceBase<SyncRecord>
    {
        void UpdateLastSyncDate<T>() where T : BaseEntity;
        DateTime? GetLastSyncDate<T>() where T : BaseEntity;

        Task<SyncRecord> GetSyncRecordAsync<T>(bool createIfNotExist) where T : BaseEntity;
    }

    public class SyncRecordEntityService : EntityServiceBase<SyncRecord>, ISyncRecordEntityService
    {
        public SyncRecordEntityService(IRepository<SyncRecord> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public SyncRecord GetSyncRecord<T>(bool createIfNotExist) where T : BaseEntity
        {
            var type = typeof(T).Name;
            var record = Repository.Table.FirstOrDefault(t => t.EntityName == type);
            if (record == null)
            {
                record = new SyncRecord() { EntityName = typeof(T).Name, Timestamp = DateTime.Today};
                this.Insert(record);
            }

            return record;
        }

        public void UpdateLastSyncDate<T>() where T : BaseEntity
        {
            var record = GetSyncRecord<T>(true);
            record.Timestamp = DateTime.Now;
            this.Update(record);
        }

        public DateTime? GetLastSyncDate<T>() where T : BaseEntity
        {
            return GetSyncRecord<T>(true).Timestamp;
        }


        public async Task<SyncRecord> GetSyncRecordAsync<T>(bool createIfNotExist) where T : BaseEntity
        {
            var type = typeof(T).Name;
            var record = await Repository.Table.FirstOrDefaultAsync(t => t.EntityName == type);
            if (record == null)
            {
                record = new SyncRecord() { EntityName = typeof(T).Name };
                this.Insert(record);
            }

            return record;
        }
    }
}