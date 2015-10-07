using System.Collections.Generic;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// A base class for all bulk location target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkLocationTargetBid"/></typeparam>
    public abstract class BulkLocationTarget<TBid> : BulkLocationTargetWithStringLocation<TBid>
        where TBid : BulkLocationTargetBid
    {
        // Should only convert bids that are not excluded

        internal override bool ShouldConvertCityTargetBid(CityTargetBid bid)
        {
            return !bid.IsExcluded;
        }

        internal override bool ShouldConvertMetroAreaTargetBid(MetroAreaTargetBid bid)
        {
            return !bid.IsExcluded;
        }

        internal override bool ShouldConvertStateTargetBid(StateTargetBid bid)
        {
            return !bid.IsExcluded;
        }

        internal override bool ShouldConvertCountryTargetBid(CountryTargetBid bid)
        {
            return !bid.IsExcluded;
        }

        internal override bool ShouldConvertPostalCodeTargetBid(PostalCodeTargetBid bid)
        {
            return !bid.IsExcluded;
        }

        // Copy BidAdjustment from bulk bid

        internal override CityTargetBid SetCityBidAdditionalProperties(CityTargetBid apiBid, TBid bulkBid)
        {
            apiBid.BidAdjustment = bulkBid.BidAdjustment;

            return apiBid;
        }

        internal override MetroAreaTargetBid SetMetroAreaBidAdditionalProperties(MetroAreaTargetBid apiBid, TBid bulkBid)
        {
            apiBid.BidAdjustment = bulkBid.BidAdjustment;

            return apiBid;
        }

        internal override StateTargetBid SetStateBidAdditionalProperties(StateTargetBid apiBid, TBid bulkBid)
        {
            apiBid.BidAdjustment = bulkBid.BidAdjustment;

            return apiBid;
        }

        internal override CountryTargetBid SetCountryBidAdditionalProperties(CountryTargetBid apiBid, TBid bulkBid)
        {
            apiBid.BidAdjustment = bulkBid.BidAdjustment;

            return apiBid;
        }

        internal override PostalCodeTargetBid SetPostalCodeBidAdditionalProperties(PostalCodeTargetBid apiBid, TBid bulkBid)
        {
            apiBid.BidAdjustment = bulkBid.BidAdjustment;

            return apiBid;
        }

        // Copy BidAdjustment from API bid

        internal override void SetBulkCityBidAdditionalProperties(TBid bulkBid, CityTargetBid apiBid)
        {
            bulkBid.BidAdjustment = apiBid.BidAdjustment;
        }

        internal override void SetBulkMetroAreaBidAdditionalProperties(TBid bulkBid, MetroAreaTargetBid apiBid)
        {
            bulkBid.BidAdjustment = apiBid.BidAdjustment;
        }

        internal override void SetBulkStateBidAdditionalProperties(TBid bulkBid, StateTargetBid apiBid)
        {
            bulkBid.BidAdjustment = apiBid.BidAdjustment;
        }

        internal override void SetBulkCountryBidAdditionalProperties(TBid bulkBid, CountryTargetBid apiBid)
        {
            bulkBid.BidAdjustment = apiBid.BidAdjustment;
        }

        internal override void SetBulkPostalCodeBidAdditionalProperties(TBid bulkBid, PostalCodeTargetBid apiBid)
        {
            bulkBid.BidAdjustment = apiBid.BidAdjustment;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>        
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            var bulkBids = base.ConvertApiToBulkBids();

            foreach (var bulkBid in bulkBids)
            {
                bulkBid.IntentOption = Location.IntentOption;
            }

            return bulkBids;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            base.ReconstructSubTargets();

            if (Bids.Count > 0 && Bids[0].IntentOption != null)
            {
                Location.IntentOption = Bids[0].IntentOption;
            }
        }
    }
}
