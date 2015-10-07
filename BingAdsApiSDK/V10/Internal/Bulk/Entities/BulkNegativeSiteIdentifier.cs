using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public abstract class BulkNegativeSiteIdentifier : BulkEntityIdentifier
    {
        internal Status? Status { get; set; }

        internal long EntityId { get; set; }

        internal string EntityName { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal abstract string ParentColumnName { get; }

        private static readonly IBulkMapping<BulkNegativeSiteIdentifier>[] Mappings =
        {
            new SimpleBulkMapping<BulkNegativeSiteIdentifier>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
                ),
            new SimpleBulkMapping<BulkNegativeSiteIdentifier>(StringTable.ParentId,
                c => c.EntityId.ToBulkString(returnNullForDefaultValue: true),
                (v, c) => c.EntityId = v.Parse<long>(returnDefaultValueOnNullOrEmpty: true)
                ),
            new DynamicColumnNameMapping<BulkNegativeSiteIdentifier>(c => c.ParentColumnName,
                c => c.EntityName,
                (v, c) => c.EntityName = v
                )
        };

        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override bool IsDeleteRow
        {
            get { return Status == V10.Bulk.Entities.Status.Deleted; }
        }
    }
}