using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk radius target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkRadiusTargetBid"/></typeparam>
    public abstract class BulkRadiusTarget<TBid> : BulkTargetWithLocation<TBid>
        where TBid : BulkRadiusTargetBid
    {                
        /// <summary>
        /// Defines a list of geographical radius targets with bid adjustments.  
        /// </summary>
        public RadiusTarget RadiusTarget
        {
            get { return GetLocationProperty(x => x.RadiusTarget); }
            set { SetLocationProperty(x => x.RadiusTarget = value); }
        }

        /// <summary>
        /// Defines the possible intent options for location targeting.
        /// </summary>
        public IntentOption? IntentOption
        {
            get { return GetLocationProperty(x => x.IntentOption); }
            set { SetLocationProperty(x => x.IntentOption = value); }
        }        

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            ReconstructApiBids(Bids, _ => _.RadiusTargetBid, () => Location.RadiusTarget, () => new RadiusTarget(), _ => Location.RadiusTarget = _, () => Location.RadiusTarget.Bids, _ => Location.RadiusTarget.Bids = _);

            if (Bids.Count > 0 && Bids[0].IntentOption != null)
            {
                Location.IntentOption = Bids[0].IntentOption;
            }
        }
        
        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            if (Location.RadiusTarget == null || Location.RadiusTarget.Bids == null)
            {
                return new List<TBid>();
            }

            var bulkBids = Location.RadiusTarget.Bids.Select(b => CreateAndPopulateBid(x => x.RadiusTargetBid = b)).ToList();

            foreach (var bulkBid in bulkBids)
            {
                bulkBid.IntentOption = Location.IntentOption;
            }

            return bulkBids;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(RadiusTarget, "RadiusTarget");
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidateBidsNotNullOrEmpty()
        {
            if (RadiusTarget != null)
            {
                ValidateListNotNullOrEmpty(RadiusTarget.Bids, "RadiusTarget.Bids");
            }
        }        
    }
}
