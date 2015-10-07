using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk age target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkAgeTargetBid"/></typeparam>
    public abstract class BulkAgeTarget<TBid> : BulkSubTarget<TBid>
        where TBid : BulkAgeTargetBid
    {
        /// <summary>
        /// Defines a list of age ranges to target with bid adjustments.
        /// </summary>
        public AgeTarget AgeTarget { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            AgeTarget = new AgeTarget
            {
                Bids = Bids.Select(t => t.AgeTargetBid).ToList()
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            if (AgeTarget == null || AgeTarget.Bids == null)
            {
                return new List<TBid>();
            }

            return AgeTarget.Bids.Select(b => CreateAndPopulateBid(x => x.AgeTargetBid = b)).ToList();
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(AgeTarget, "AgeTarget");            
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidateBidsNotNullOrEmpty()
        {
            if (AgeTarget != null)
            {
                ValidateListNotNullOrEmpty(AgeTarget.Bids, "AgeTarget.Bids");
            }
        }
    }
}
