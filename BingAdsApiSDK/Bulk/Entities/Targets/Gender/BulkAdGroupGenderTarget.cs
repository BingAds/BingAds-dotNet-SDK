using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>    
    /// Represents a gender target that is associated with an ad group. 
    /// This class exposes the <see cref="BulkGenderTarget{TBid}.GenderTarget"/> property that can be read and written as fields of the Ad Group Gender Target record in a bulk file.       
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupGenderTarget"/> exposes a read only list of <see cref="BulkAdGroupGenderTargetBid"/>. Each <see cref="BulkAdGroupGenderTargetBid"/> instance 
    /// corresponds to one Ad Group Gender Target record in the bulk file. If you upload a <see cref="BulkAdGroupGenderTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding gender target. 
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511544">Ad Group Gender Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupGenderTarget : BulkGenderTarget<BulkAdGroupGenderTargetBid>
    {
        /// <summary>
        /// The identifier of the ad group that the target is associated.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// The name of the ad group that the target is associated.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// The name of the ad group that target is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return ParentEntityName; }
            set { ParentEntityName = value; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkAdGroupGenderTargetBid CreateBid()
        {
            return new BulkAdGroupGenderTargetBid();
        }
    }
}
