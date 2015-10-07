using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    internal class SiteLinkAdExtensionIdentifier : BulkAdExtensionIdentifier
    {                
        public override bool Equals(BulkEntityIdentifier other)
        {
            var otherSiteLinkIdentity = other as SiteLinkAdExtensionIdentifier;

            if (otherSiteLinkIdentity == null)
            {
                return false;
            }

            return AccountId == otherSiteLinkIdentity.AccountId &&
                   AdExtensionId == otherSiteLinkIdentity.AdExtensionId;
        }

        internal override MultiRecordBulkEntity CreateEntityWithThisIdentifier()
        {
            return new BulkSiteLinkAdExtension(this);            
        }

        internal override bool IsDeleteRow
        {
            get { return Status == AdExtensionStatus.Deleted; }
        }
    }
}