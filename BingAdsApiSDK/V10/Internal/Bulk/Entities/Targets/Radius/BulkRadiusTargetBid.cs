using System.Globalization;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk radius target bid classes, for example <see cref="BulkAdGroupRadiusTargetBid"/>.
    /// </summary>
    public abstract class BulkRadiusTargetBid : BulkTargetBid
    {
        /// <summary>
        /// Defines a specific geographical radius to target.
        /// </summary>
        public RadiusTargetBid RadiusTargetBid { get; set; }

        /// <summary>
        /// Defines the possible intent options for location targeting.
        /// </summary>
        public IntentOption? IntentOption { get; internal set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkRadiusTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {
            
        }

        private static readonly IBulkMapping<BulkRadiusTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.RadiusTargetId,
                c => c.RadiusTargetBid.Id.ToBulkString(),
                (v, c) => c.RadiusTargetBid.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Name,
                c => c.RadiusTargetBid.Name,
                (v, c) => c.RadiusTargetBid.Name = v
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Radius,
                c => c.RadiusTargetBid.Radius.ToBulkString(),
                (v, c) => c.RadiusTargetBid.Radius = v.Parse<double>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Unit,
                c => c.RadiusTargetBid.RadiusUnit.ToBulkString(),
                (v, c) => c.RadiusTargetBid.RadiusUnit = v.Parse<DistanceUnit>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Latitude,
                c => c.RadiusTargetBid.LatitudeDegrees.ToBulkString(),
                (v, c) => c.RadiusTargetBid.LatitudeDegrees = v.Parse<double>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Longitude,
                c => c.RadiusTargetBid.LongitudeDegrees.ToBulkString(),
                (v, c) => c.RadiusTargetBid.LongitudeDegrees = v.Parse<double>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.BidAdjustment,
                c => c.RadiusTargetBid.BidAdjustment.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.RadiusTargetBid.BidAdjustment = v.Parse<int>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.PhysicalIntent,
                c => c.IntentOption.ToBulkString(),
                (v, c) => c.IntentOption = v.ParseOptional<IntentOption>()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            RadiusTargetBid = new RadiusTargetBid();

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(RadiusTargetBid, "RadiusTargetBid");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
