using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public class BulkAdGroupNegativeSitesIdentifier : BulkNegativeSiteIdentifier
    {
        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public long AdGroupId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public string AdGroupName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public string CampaignName { get; set; }

        private static readonly IBulkMapping<BulkAdGroupNegativeSitesIdentifier>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdGroupNegativeSitesIdentifier>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
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
            return new BulkAdGroupNegativeSites(this);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public override bool Equals(BulkEntityIdentifier other)
        {
            var otherIdentifier = other as BulkAdGroupNegativeSitesIdentifier;

            if (otherIdentifier == null)
            {
                return false;
            }

            var isNameNotEmpty = !string.IsNullOrEmpty(CampaignName) && !string.IsNullOrEmpty(AdGroupName);

            return AdGroupId == otherIdentifier.AdGroupId ||
                   (isNameNotEmpty &&
                    CampaignName == otherIdentifier.CampaignName &&
                    AdGroupName == otherIdentifier.AdGroupName);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override string ParentColumnName
        {
            get { return StringTable.AdGroup; }
        }
    }
}
