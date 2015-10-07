using System.Globalization;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk gender target bid classes.
    /// </summary>
    public abstract class BulkGenderTargetBid : BulkTargetBid
    {
        /// <summary>
        /// Defines a specific gender target.
        /// </summary>
        public GenderTargetBid GenderTargetBid { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkGenderTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {
            
        }

        private static readonly IBulkMapping<BulkGenderTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkGenderTargetBid>(StringTable.Target,
                c => c.GenderTargetBid.Gender.ToString(),
                (v, c) => c.GenderTargetBid.Gender = v.Parse<GenderType>()
            ),

            new SimpleBulkMapping<BulkGenderTargetBid>(StringTable.BidAdjustment,
                c => c.GenderTargetBid.BidAdjustment.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.GenderTargetBid.BidAdjustment = v.Parse<int>()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            GenderTargetBid = new GenderTargetBid();

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(GenderTargetBid, "GenderTargetBid");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
