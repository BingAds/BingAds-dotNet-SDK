using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk device OS target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkDeviceOsTargetBid"/></typeparam>
    public abstract class BulkDeviceOsTarget<TBid> : BulkSubTarget<TBid>
        where TBid : BulkDeviceOsTargetBid
    {
        /// <summary>
        /// Defines a list of devices to target with bid adjustments.
        /// </summary>
        public DeviceOSTarget DeviceOsTarget { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            DeviceOsTarget = new DeviceOSTarget()
            {
                Bids = Bids.Select(t => t.DeviceOsTargetBid).ToList()
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            if (DeviceOsTarget == null || DeviceOsTarget.Bids == null)
            {
                return new List<TBid>();
            }

            return DeviceOsTarget.Bids.Select(b => CreateAndPopulateBid(x => x.DeviceOsTargetBid = b)).ToList();
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(DeviceOsTarget, "DeviceOsTarget");            
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidateBidsNotNullOrEmpty()
        {
            if (DeviceOsTarget != null)
            {
                ValidateListNotNullOrEmpty(DeviceOsTarget.Bids, "DeviceOsTarget.Bids");
            }
        }
    }
}
