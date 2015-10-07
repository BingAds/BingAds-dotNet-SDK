using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one age target bid within an age target that is associated with an ad group.  
    /// This class exposes the <see cref="BulkAgeTargetBid.AgeTargetBid"/> property that can be read and written as fields of the Ad Group Age Target record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511546">Ad Group Age Target</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupAgeTarget"/> exposes a read only list of <see cref="BulkAdGroupAgeTargetBid"/>. Each <see cref="BulkAdGroupAgeTargetBid"/> instance 
    /// corresponds to one Ad Group Age Target record in the bulk file. If you upload a <see cref="BulkAdGroupAgeTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding age target. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupAgeTargetBid : BulkAgeTargetBid
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
        /// The name of the ad group that target is associated.
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
        /// Initializes a new instanced of the BulkAdGroupAgeTargetBid class. 
        /// </summary>
        public BulkAdGroupAgeTargetBid()
            : base(new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupAgeTargetBid)))
        {

        }
    }
}
