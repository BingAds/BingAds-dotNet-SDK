using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    internal class BulkProductAdExtensionIdentifier : BulkAdExtensionIdentifier
    {
        public string Name { get; set; }

        public override bool Equals(BulkEntityIdentifier other)
        {
            var otherProductAdExtensionIdentifier = other as BulkProductAdExtensionIdentifier;

            if (otherProductAdExtensionIdentifier == null)
            {
                return false;
            }

            return AccountId == otherProductAdExtensionIdentifier.AccountId &&
                   AdExtensionId == otherProductAdExtensionIdentifier.AdExtensionId;
        }

        private static readonly IBulkMapping<BulkProductAdExtensionIdentifier>[] Mappings =
        {
            new SimpleBulkMapping<BulkProductAdExtensionIdentifier>(StringTable.Name,
                c => c.Name,
                (v, c) => c.Name = v
            )
        };

        internal override void ReadFromRowValues(RowValues values)
        {
            base.ReadFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            base.WriteToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }

        internal override MultiRecordBulkEntity CreateEntityWithThisIdentifier()
        {
            return new BulkProductAdExtension(this);
        }
    }
}
