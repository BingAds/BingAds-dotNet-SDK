using System.Globalization;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk age target bid classes.
    /// </summary>
    public abstract class BulkAgeTargetBid : BulkTargetBid
    {
        /// <summary>
        /// Defines a list of age ranges to target with bid adjustments.
        /// </summary>
        public AgeTargetBid AgeTargetBid { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkAgeTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {
            
        }

        private static readonly IBulkMapping<BulkAgeTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkAgeTargetBid>(StringTable.Target,
                c => c.AgeTargetBid.Age.ToString(),
                (v, c) => c.AgeTargetBid.Age = v.Parse<AgeRange>()
            ),

            new SimpleBulkMapping<BulkAgeTargetBid>(StringTable.BidAdjustment,
                c => c.AgeTargetBid.BidAdjustment.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.AgeTargetBid.BidAdjustment = v.Parse<int>()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AgeTargetBid = new AgeTargetBid();

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(AgeTargetBid, "AgeTargetBid");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
