using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk entities mapped to the API LocationTarget2 object.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkTargetBid"/></typeparam>
    public abstract class BulkTargetWithLocation<TBid> : BulkSubTarget<TBid>
        where TBid : BulkTargetBid
    {
        internal LocationTarget2 Location { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected void ReconstructApiBids<TApiBid, TTarget>(
            IReadOnlyCollection<TBid> bulkBids,
            Func<TBid, TApiBid> createBid,
            Func<TTarget> getTarget,
            Func<TTarget> createNewTarget,
            Action<TTarget> setTarget,
            Func<IList<TApiBid>> getBids,
            Action<IList<TApiBid>> setBids
        )
            where TTarget : class
        {
            var apiBidsFromFile = bulkBids.Select(createBid).ToList();

            if (apiBidsFromFile.Count == 0)
            {
                return;
            }

            if (getTarget() == null)
            {
                setTarget(createNewTarget());

                setBids(new List<TApiBid>());
            }

            getBids().AddRange(apiBidsFromFile);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected void ConvertBidsFromApi<TApiBid, TTarget>(
            IList<TBid> bulkBids,
            Func<TTarget> getTarget,
            Func<TTarget, IList<TApiBid>> getApiBids,
            Action<TBid, TApiBid> setAdditionalBidProperties,
            Func<TApiBid, bool> shouldConvertBid
        )
            where TTarget : class
        {
            var target = getTarget();

            if (target == null)
            {
                return;
            }

            var apiBids = getApiBids(target);

            if (apiBids == null)
            {
                return;
            }

            bulkBids.AddRange(apiBids.Where(shouldConvertBid).Select(apiBid => CreateAndPopulateBid(bulkBid => setAdditionalBidProperties(bulkBid, apiBid))));
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected T GetLocationProperty<T>(Func<LocationTarget2, T> getFunc)
        {
            return Location == null ? default(T) : getFunc(Location);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected void SetLocationProperty(Action<LocationTarget2> setAction)
        {
            if (Location == null)
            {
                Location = new LocationTarget2();
            }

            setAction(Location);
        }
    }
}
