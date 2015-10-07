using System.Globalization;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk location target bid classes.
    /// </summary>
    public abstract class BulkLocationTargetBid : BulkLocationTargetBidWithStringLocation
    {               
        /// <summary>
        /// The percentage adjustment to the base bid.
        /// Corresponds to the 'Bid Adjustment' field in the bulk file.
        /// </summary>
        public int BidAdjustment { get; set; }

        /// <summary>
        /// Defines the possible intent options for location targeting.
        /// </summary>
        public IntentOption? IntentOption { get; internal set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="identifier">Reserved for internal use.</param>
        protected internal BulkLocationTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {

        }

        private static readonly IBulkMapping<BulkLocationTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkLocationTargetBid>(StringTable.BidAdjustment,
                c => c.BidAdjustment.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.BidAdjustment = int.Parse(v)
            ),

            new SimpleBulkMapping<BulkLocationTargetBid>(StringTable.PhysicalIntent,
                c => c.IntentOption.ToBulkString(),
                (v, c) => c.IntentOption = v.ParseOptional<IntentOption>()
            )                        
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}