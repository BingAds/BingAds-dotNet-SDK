using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// A base class for all bulk negative location target classes, for example <see cref="BulkAdGroupNegativeLocationTarget"/>.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkNegativeLocationTargetBid"/></typeparam>
    public abstract class BulkNegativeLocationTarget<TBid> : BulkLocationTargetWithStringLocation<TBid>
        where TBid : BulkNegativeLocationTargetBid
    {
        // Should only convert bid if it's excluded

        internal override bool ShouldConvertCityTargetBid(CityTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertMetroAreaTargetBid(MetroAreaTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertStateTargetBid(StateTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertCountryTargetBid(CountryTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertPostalCodeTargetBid(PostalCodeTargetBid bid)
        {
            return bid.IsExcluded;
        }

        // Set IsExcluded to true when converting to API bids

        internal override CityTargetBid SetCityBidAdditionalProperties(CityTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override MetroAreaTargetBid SetMetroAreaBidAdditionalProperties(MetroAreaTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override StateTargetBid SetStateBidAdditionalProperties(StateTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override CountryTargetBid SetCountryBidAdditionalProperties(CountryTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override PostalCodeTargetBid SetPostalCodeBidAdditionalProperties(PostalCodeTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        // No additional properties need to be set for BulkNegativeLocationTargetBids (Location and LocationType are set by the base class)

        internal override void SetBulkCityBidAdditionalProperties(TBid bulkBid, CityTargetBid apiBid)
        {
            
        }

        internal override void SetBulkMetroAreaBidAdditionalProperties(TBid bulkBid, MetroAreaTargetBid apiBid)
        {
            
        }

        internal override void SetBulkStateBidAdditionalProperties(TBid bulkBid, StateTargetBid apiBid)
        {
            
        }

        internal override void SetBulkCountryBidAdditionalProperties(TBid bulkBid, CountryTargetBid apiBid)
        {
            
        }

        internal override void SetBulkPostalCodeBidAdditionalProperties(TBid bulkBid, PostalCodeTargetBid apiBid)
        {
            
        }
    }
}
