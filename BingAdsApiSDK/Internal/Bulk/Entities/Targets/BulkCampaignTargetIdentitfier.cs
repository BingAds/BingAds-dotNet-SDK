using System;
using Microsoft.BingAds.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public class BulkCampaignTargetIdentifier : BulkTargetIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the BulkCampaignTargetIdentifier class.
        /// </summary>
        public BulkCampaignTargetIdentifier(Type targetBidType)
            : base(targetBidType)
        {
            
        }

        internal override MultiRecordBulkEntity CreateEntityWithThisIdentifier()
        {
            return new BulkCampaignTarget(this);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override string EntityColumnName
        {
            get { return StringTable.Campaign; }
        }
    }
}
