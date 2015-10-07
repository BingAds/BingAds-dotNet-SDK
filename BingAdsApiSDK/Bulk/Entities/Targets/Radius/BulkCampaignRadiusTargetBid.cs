using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one radius target bid within a radius target that is associated with a campaign. 
    /// This class exposes the <see cref="BulkRadiusTargetBid.RadiusTargetBid"/> property that can be read and written as fields of the Campaign Radius Target record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511527">Campaign Radius Target</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkCampaignRadiusTarget"/> exposes a read only list of <see cref="BulkCampaignRadiusTargetBid"/>. Each <see cref="BulkCampaignRadiusTargetBid"/> instance 
    /// corresponds to one Campaign Radius Target record in the bulk file. If you upload a <see cref="BulkCampaignRadiusTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding radius target. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignRadiusTargetBid : BulkRadiusTargetBid
    {
        /// <summary>
        /// The identifier of the campaign that the target is associated.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// The name of the campaign that the target is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// Initializes a new instanced of the BulkCampaignRadiusTargetBid class. 
        /// </summary>
        public BulkCampaignRadiusTargetBid()
            : base(new BulkCampaignTargetIdentifier(typeof(BulkCampaignRadiusTargetBid)))
        {

        }
    }
}
