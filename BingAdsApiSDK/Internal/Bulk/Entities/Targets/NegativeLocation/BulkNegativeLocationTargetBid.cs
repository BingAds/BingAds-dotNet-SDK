using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk negative location target bid classes.
    /// </summary>
    public abstract class BulkNegativeLocationTargetBid : BulkLocationTargetBidWithStringLocation
    {
        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="identifier"></param>
        protected BulkNegativeLocationTargetBid(BulkTargetIdentifier identifier) : base(identifier)
        {
        }
    }
}
