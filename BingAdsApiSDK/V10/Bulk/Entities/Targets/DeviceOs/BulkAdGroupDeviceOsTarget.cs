using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>    
    /// Represents a device OS target that is associated with an ad group. 
    /// This class exposes the <see cref="BulkDeviceOsTarget{TBid}.DeviceOsTarget"/> property that can be read and written as fields of the Ad Group DeviceOS Target record in a bulk file.     
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupDeviceOsTarget"/> exposes a read only list of <see cref="BulkAdGroupDeviceOsTargetBid"/>. Each <see cref="BulkAdGroupDeviceOsTargetBid"/> instance 
    /// corresponds to one Ad Group DeviceOS Target record in the bulk file. If you upload a <see cref="BulkAdGroupDeviceOsTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding device OS target. 
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620259">Ad Group DeviceOS Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupDeviceOsTarget : BulkDeviceOsTarget<BulkAdGroupDeviceOsTargetBid>
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
        protected internal override BulkAdGroupDeviceOsTargetBid CreateBid()
        {
            return new BulkAdGroupDeviceOsTargetBid();
        }
    }
}
