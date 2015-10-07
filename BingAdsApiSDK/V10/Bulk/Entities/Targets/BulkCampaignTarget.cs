using System;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a target that is associated with a campaign. The target contains one or more sub targets, including  
    /// age, gender, day and time, device OS, and location. Each target can be read or written in a bulk file. 
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// When requesting downloaded entities of type <see cref="BulkCampaignTarget"/>, the results will include 
    /// Campaign Age Target, Campaign DayTime Target, Campaign DeviceOS Target, Campaign Gender Target, Campaign Location Target, 
    /// Campaign Negative Location Target, and Campaign Radius Target records. 
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620269">Bulk File Schema</see>. 
    /// </para>
    /// <para>
    /// For upload you must set the <see cref="Target"/> object, which will effectively replace any existing bids for the corresponding target.
    /// </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignTarget : BulkTarget<BulkCampaignTargetIdentifier,
        BulkCampaignAgeTargetBid, BulkCampaignAgeTarget,
        BulkCampaignGenderTargetBid, BulkCampaignGenderTarget,
        BulkCampaignDayTimeTargetBid, BulkCampaignDayTimeTarget,
        BulkCampaignLocationTargetBid, BulkCampaignLocationTarget,
        BulkCampaignNegativeLocationTargetBid, BulkCampaignNegativeLocationTarget,
        BulkCampaignRadiusTargetBid, BulkCampaignRadiusTarget,
        BulkCampaignDeviceOsTargetBid, BulkCampaignDeviceOsTarget>
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
        /// Initializes a new instanced of the BulkCampaignTarget class with the specified bulk target bid. 
        /// </summary>
        internal BulkCampaignTarget(BulkTargetBid bid)
            : base(bid,
            new BulkCampaignLocationTarget(), 
            new BulkCampaignAgeTarget(), 
            new BulkCampaignGenderTarget(), 
            new BulkCampaignDayTimeTarget(),
            new BulkCampaignDeviceOsTarget(), 
            new BulkCampaignNegativeLocationTarget(), 
            new BulkCampaignRadiusTarget())
        {

        }

        internal BulkCampaignTarget(BulkCampaignTargetIdentifier identifier)
            : base(identifier, 
            new BulkCampaignLocationTarget(), 
            new BulkCampaignAgeTarget(), 
            new BulkCampaignGenderTarget(), 
            new BulkCampaignDayTimeTarget(),
            new BulkCampaignDeviceOsTarget(), 
            new BulkCampaignNegativeLocationTarget(), 
            new BulkCampaignRadiusTarget())
        {

        }

        /// <summary>
        /// Initializes a new instanced of the BulkCampaignTarget class. 
        /// </summary>
        public BulkCampaignTarget()
            : base(
            new BulkCampaignLocationTarget(), 
            new BulkCampaignAgeTarget(), 
            new BulkCampaignGenderTarget(), 
            new BulkCampaignDayTimeTarget(),
            new BulkCampaignDeviceOsTarget(), 
            new BulkCampaignNegativeLocationTarget(), 
            new BulkCampaignRadiusTarget())
        {

        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="bidType">Reserved for internal use.</param>
        /// <returns>Reserved for internal use.</returns>
        protected internal override BulkCampaignTargetIdentifier CreateIdentifier(Type bidType)
        {
            return new BulkCampaignTargetIdentifier(bidType);
        }
    }
}
