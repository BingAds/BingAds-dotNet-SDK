using System;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an account that can be read or written in a bulk file. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Account record in a bulk file.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511513">Account</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAccount : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the account.
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The identifier of the customer that contains the account.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long CustomerId { get; private set; }

        /// <summary>
        /// The date and time that you last synced your account using the bulk service. 
        /// You should keep track of this value in UTC time. 
        /// Corresponds to the 'Sync Time' field in the bulk file. 
        /// </summary>
        public DateTime SyncTime { get; private set; }

        private static readonly IBulkMapping<BulkAccount>[] Mappings =
        {
            new SimpleBulkMapping<BulkAccount>(StringTable.Id,
                c => c.Id.ToBulkString(),
                (v, c) => c.Id = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.ParentId,
                c => c.CustomerId.ToBulkString(),
                (v, c) => c.CustomerId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.SyncTime,
                c => c.SyncTime.ToBulkString(),
                (v, c) => c.SyncTime = v.Parse<DateTime>()
            )
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }
    }
}
