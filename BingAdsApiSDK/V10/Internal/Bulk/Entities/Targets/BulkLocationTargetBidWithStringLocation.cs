using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk location target bid classes defining Location as string.
    /// </summary>
    public abstract class BulkLocationTargetBidWithStringLocation : BulkTargetBid
    {        
        /// <summary>
        /// The geographical location code.
        /// Corresponds to the 'Target' field in the bulk file.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The sub location target type.
        /// Corresponds to the 'Sub Type' field in the bulk file.
        /// </summary>
        public LocationTargetType LocationType { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="identifier">Reserved for internal use.</param>
        protected BulkLocationTargetBidWithStringLocation(BulkTargetIdentifier identifier)
            : base(identifier)
        {

        }

        private static readonly IBulkMapping<BulkLocationTargetBidWithStringLocation>[] Mappings =
        {
            new SimpleBulkMapping<BulkLocationTargetBidWithStringLocation>(StringTable.Target,
                c => c.Location,
                (v, c) => c.Location = v
            ),
            
            new SimpleBulkMapping<BulkLocationTargetBidWithStringLocation>(StringTable.SubType,
                c => c.LocationType.ToLocationTargetTypeBulkString(),
                (v, c) => c.LocationType = v.ParseLocationTargetType()
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