using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>    
    /// Represents a gender target that is associated with a campaign. 
    /// This class exposes the <see cref="BulkGenderTarget{TBid}.GenderTarget"/> property that can be read and written as fields of the Campaign Gender Target record in a bulk file.       
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkCampaignGenderTarget"/> exposes a read only list of <see cref="BulkCampaignGenderTargetBid"/>. Each <see cref="BulkCampaignGenderTargetBid"/> instance 
    /// corresponds to one Campaign Gender Target record in the bulk file. If you upload a <see cref="BulkCampaignGenderTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding gender target. 
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620246">Campaign Gender Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignGenderTarget : BulkGenderTarget<BulkCampaignGenderTargetBid>
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
        protected internal override BulkCampaignGenderTargetBid CreateBid()
        {
            return new BulkCampaignGenderTargetBid();
        }
    }
}
