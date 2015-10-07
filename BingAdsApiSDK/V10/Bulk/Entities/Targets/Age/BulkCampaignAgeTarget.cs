
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>    
    /// Represents an age target that is associated with a campaign. The age target contains one or more age target bids. 
    /// This class exposes the <see cref="BulkAgeTarget{TBid}.AgeTarget"/> property that can be read and written as fields of the Campaign Age Target record in a bulk file.         
    /// </summary>
    /// <remarks>
    /// <para>
    /// One <see cref="BulkCampaignAgeTarget"/> exposes a read only list of <see cref="BulkCampaignAgeTargetBid"/>. Each <see cref="BulkCampaignAgeTargetBid"/> instance 
    /// corresponds to one Campaign Age Target record in the bulk file. If you upload a <see cref="BulkCampaignAgeTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding age target. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620248">Campaign Age Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignAgeTarget : BulkAgeTarget<BulkCampaignAgeTargetBid>
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
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkCampaignAgeTargetBid CreateBid()
        {
            return new BulkCampaignAgeTargetBid();
        }
    }
}
