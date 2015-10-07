using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk day and time target bid classes.
    /// </summary>
    public abstract class BulkDayTimeTargetBid : BulkTargetBid
    {
        /// <summary>
        /// Defines a specific day of the week and time range to target.
        /// </summary>
        public DayTimeTargetBid DayTimeTargetBid { get; set; }

        /// <summary>
        /// Reserved for internal use. 
        /// </summary>
        protected BulkDayTimeTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {
        }

        private static readonly IBulkMapping<BulkDayTimeTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkDayTimeTargetBid>(StringTable.Target,
                c => c.DayTimeTargetBid.Day.ToBulkString(),
                (v, c) => c.DayTimeTargetBid.Day = v.Parse<Day>()
                ),
            new SimpleBulkMapping<BulkDayTimeTargetBid>(StringTable.FromHour,
                c => c.DayTimeTargetBid.FromHour.ToBulkString(),
                (v, c) => c.DayTimeTargetBid.FromHour = v.Parse<int>()
                ),
            new SimpleBulkMapping<BulkDayTimeTargetBid>(StringTable.ToHour,
                c => c.DayTimeTargetBid.ToHour.ToBulkString(),
                (v, c) => c.DayTimeTargetBid.ToHour = v.Parse<int>()
                ),
            new SimpleBulkMapping<BulkDayTimeTargetBid>(StringTable.FromMinute,
                c => c.DayTimeTargetBid.FromMinute.ToMinuteBulkString(),
                (v, c) => c.DayTimeTargetBid.FromMinute = v.ParseMinute()
                ),
            new SimpleBulkMapping<BulkDayTimeTargetBid>(StringTable.ToMinute,
                c => c.DayTimeTargetBid.ToMinute.ToMinuteBulkString(),
                (v, c) => c.DayTimeTargetBid.ToMinute = v.ParseMinute()
                ),
            new SimpleBulkMapping<BulkDayTimeTargetBid>(StringTable.BidAdjustment,
                c => c.DayTimeTargetBid.BidAdjustment.ToBulkString(),
                (v, c) => c.DayTimeTargetBid.BidAdjustment = v.Parse<int>()
                ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            DayTimeTargetBid = new DayTimeTargetBid();

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(DayTimeTargetBid, "DayTimeTargetBid");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}