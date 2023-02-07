using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.CoreLib.Data;

namespace AVS.Trading.Data.Domain.System
{
    /// <summary>
    /// sync record keeps timestamp when the last sync operation has been run
    /// </summary>
    public class SyncRecord : BaseEntity
    {
        /// <summary>
        /// entity.GetUnproxiedEntityType().Name
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// last sync date
        /// </summary>
        public DateTime? Timestamp { get; set; }
    }
}
