using System.Globalization;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    internal abstract class BulkAdExtensionIdentifier : BulkEntityIdentifier
    {
        public long AccountId { get; set; }

        public long? AdExtensionId { get; set; }

        public AdExtensionStatus? Status { get; set; }

        public int? Version { get; set; }

        private static readonly IBulkMapping<BulkAdExtensionIdentifier>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdExtensionIdentifier>(StringTable.Id,
                c => c.AdExtensionId.ToString(),
                (v, c) => c.AdExtensionId = string.IsNullOrEmpty(v) ? (long?) null : long.Parse(v)
            ),

            new SimpleBulkMapping<BulkAdExtensionIdentifier>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<AdExtensionStatus>()
            ),

            new SimpleBulkMapping<BulkAdExtensionIdentifier>(StringTable.Version,
                c => c.Version.ToString(),
                (v, c) => c.Version = !string.IsNullOrEmpty(v) ? int.Parse(v) : (int?)null
            ), 

            new SimpleBulkMapping<BulkAdExtensionIdentifier>(StringTable.ParentId,
                c => c.AccountId.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.AccountId = long.Parse(v)
            )
        };

        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        public override bool Equals(BulkEntityIdentifier other)
        {
            throw new System.NotImplementedException();
        }

        internal override bool IsDeleteRow
        {
            get { return Status == AdExtensionStatus.Deleted; }
        }
    }
}