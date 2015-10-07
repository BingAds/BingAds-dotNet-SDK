using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk gender target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkGenderTargetBid"/></typeparam>
    public abstract class BulkGenderTarget<TBid> : BulkSubTarget<TBid>
        where TBid : BulkGenderTargetBid
    {
        /// <summary>
        /// Defines a list of genders to target with bid adjustments.
        /// </summary>
        public GenderTarget GenderTarget { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            GenderTarget = new GenderTarget()
            {
                Bids = Bids.Select(t => t.GenderTargetBid).ToList()
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            if (GenderTarget == null || GenderTarget.Bids == null)
            {
                return new List<TBid>();
            }

            return GenderTarget.Bids.Select(b => CreateAndPopulateBid(x => x.GenderTargetBid = b)).ToList();
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(GenderTarget, "GenderTarget");            
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidateBidsNotNullOrEmpty()
        {
            if (GenderTarget != null)
            {
                ValidateListNotNullOrEmpty(GenderTarget.Bids, "GenderTarget.Bids");
            }
        }
    }
}
