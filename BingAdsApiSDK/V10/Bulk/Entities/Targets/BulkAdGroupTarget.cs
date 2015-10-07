using System;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a target that is associated with an ad group. The target contains one or more sub targets, including  
    /// age, gender, day and time, device OS, and location. Each target can be read or written in a bulk file. 
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// When requesting downloaded entities of type <see cref="BulkAdGroupTarget"/>, the results will include 
    /// Ad Group Age Target, Ad Group DayTime Target, Ad Group DeviceOS Target, Ad Group Gender Target, Ad Group Location Target, 
    /// Ad Group Negative Location Target, and Ad Group Radius Target records. 
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
    public class BulkAdGroupTarget : BulkTarget<BulkAdGroupTargetIdentifier,
        BulkAdGroupAgeTargetBid, BulkAdGroupAgeTarget,
        BulkAdGroupGenderTargetBid, BulkAdGroupGenderTarget,
        BulkAdGroupDayTimeTargetBid, BulkAdGroupDayTimeTarget,
        BulkAdGroupLocationTargetBid, BulkAdGroupLocationTarget,
        BulkAdGroupNegativeLocationTargetBid, BulkAdGroupNegativeLocationTarget,
        BulkAdGroupRadiusTargetBid, BulkAdGroupRadiusTarget,
        BulkAdGroupDeviceOsTargetBid, BulkAdGroupDeviceOsTarget>
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
        /// Initializes a new instanced of the BulkAdGroupTarget class with the specified bulk target bid. 
        /// </summary>
        internal BulkAdGroupTarget(BulkTargetBid bid)
            : base(bid,
            new BulkAdGroupLocationTarget(),
            new BulkAdGroupAgeTarget(),
            new BulkAdGroupGenderTarget(),
            new BulkAdGroupDayTimeTarget(),
            new BulkAdGroupDeviceOsTarget(),
            new BulkAdGroupNegativeLocationTarget(),
            new BulkAdGroupRadiusTarget())
        {

        }

        internal BulkAdGroupTarget(BulkAdGroupTargetIdentifier identifier)
            : base(identifier,
            new BulkAdGroupLocationTarget(),
            new BulkAdGroupAgeTarget(),
            new BulkAdGroupGenderTarget(),
            new BulkAdGroupDayTimeTarget(),
            new BulkAdGroupDeviceOsTarget(),
            new BulkAdGroupNegativeLocationTarget(),
            new BulkAdGroupRadiusTarget())
        {

        }

        /// <summary>
        /// Initializes a new instanced of the BulkAdGroupTarget class. 
        /// </summary>
        public BulkAdGroupTarget()
            : base(new BulkAdGroupLocationTarget(),
            new BulkAdGroupAgeTarget(),
            new BulkAdGroupGenderTarget(),
            new BulkAdGroupDayTimeTarget(),
            new BulkAdGroupDeviceOsTarget(),
            new BulkAdGroupNegativeLocationTarget(),
            new BulkAdGroupRadiusTarget())
        {

        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkAdGroupTargetIdentifier CreateIdentifier(Type bidType)
        {
            return new BulkAdGroupTargetIdentifier(bidType);
        }
    }
}
