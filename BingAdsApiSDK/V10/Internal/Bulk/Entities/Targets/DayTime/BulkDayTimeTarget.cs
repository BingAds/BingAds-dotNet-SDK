using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk day and time target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkDayTimeTargetBid"/><see cref="BulkAgeTargetBid"/></typeparam>
    public abstract class BulkDayTimeTarget<TBid> : BulkSubTarget<TBid>
        where TBid : BulkDayTimeTargetBid
    {
        /// <summary>
        /// Defines a list of days of the week and time ranges to target with bid adjustments. 
        /// </summary>
        public DayTimeTarget DayTimeTarget { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            DayTimeTarget = new DayTimeTarget()
            {
                Bids = Bids.Select(t => t.DayTimeTargetBid).ToList()
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            if (DayTimeTarget == null || DayTimeTarget.Bids == null)
            {
                return new List<TBid>();
            }

            return DayTimeTarget.Bids.Select(dayTime => CreateAndPopulateBid(b => b.DayTimeTargetBid = dayTime)).ToList();
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(DayTimeTarget, "DayTimeTarget");
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidateBidsNotNullOrEmpty()
        {
            if (DayTimeTarget != null)
            {
                ValidateListNotNullOrEmpty(DayTimeTarget.Bids, "DayTimeTarget.Bids");
            }
        }
    }
}
