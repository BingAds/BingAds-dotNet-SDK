using System;
using System.Globalization;
using System.Linq;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk device OS target bid classes.
    /// </summary>
    public abstract class BulkDeviceOsTargetBid : BulkTargetBid
    {
        /// <summary>
        /// Defines a specific device to target.
        /// </summary>
        public DeviceOSTargetBid DeviceOsTargetBid { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkDeviceOsTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {

        }

        private static readonly IBulkMapping<BulkDeviceOsTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkDeviceOsTargetBid>(StringTable.Target,
                c => c.DeviceOsTargetBid.DeviceName,
                (v, c) => c.DeviceOsTargetBid.DeviceName = v
            ),

            new SimpleBulkMapping<BulkDeviceOsTargetBid>(StringTable.OsNames,
                c => c.DeviceOsTargetBid.OSNames != null 
                    ? string.Join(";", c.DeviceOsTargetBid.OSNames) 
                    : null,
                (v, c) =>
                {                    
                    c.DeviceOsTargetBid.OSNames = !string.IsNullOrEmpty(v) 
                        ? v.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries).ToList()
                        : null;
                }
            ),

            new SimpleBulkMapping<BulkDeviceOsTargetBid>(StringTable.BidAdjustment,
                c => c.DeviceOsTargetBid.BidAdjustment.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.DeviceOsTargetBid.BidAdjustment = int.Parse(v)
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            DeviceOsTargetBid = new DeviceOSTargetBid();

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(DeviceOsTargetBid, "DeviceOsTargetBid");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
