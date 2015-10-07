using Microsoft.BingAds.V10.Bulk.Entities;

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public class BulkCampaignNegativeSitesIdentifier : BulkNegativeSiteIdentifier
    {
        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public long CampaignId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public string CampaignName
        {
            get { return EntityName; }
            internal set { EntityName = value; }
        }

        internal override MultiRecordBulkEntity CreateEntityWithThisIdentifier()
        {
            return new BulkCampaignNegativeSites(this);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public override bool Equals(BulkEntityIdentifier other)
        {
            var otherIdentifier = other as BulkCampaignNegativeSitesIdentifier;

            if (otherIdentifier == null)
            {
                return false;
            }

            var isNameNotEmpty = !string.IsNullOrEmpty(CampaignName);

            return CampaignId == otherIdentifier.CampaignId || 
                (isNameNotEmpty && CampaignName == otherIdentifier.CampaignName);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override string ParentColumnName
        {
            get { return StringTable.Campaign; }
        }
    }
}
