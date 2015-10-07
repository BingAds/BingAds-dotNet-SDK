using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk ad classes.
    /// </summary>
    /// <typeparam name="T">The type of ad from the <see cref="Microsoft.BingAds.CampaignManagement"/> namespace, for example a <see cref="TextAd"/> object.</typeparam>
    /// <seealso cref="BulkMobileAd"/>
    /// <seealso cref="BulkProductAd"/>
    /// <seealso cref="BulkTextAd"/>
    public abstract class BulkAd<T> : SingleRecordBulkEntity
        where T: Ad, new()
    {
        /// <summary>
        /// The identifier of the ad group that contains the ad.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the ad group that contains the ad.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        /// <summary>
        /// The type of ad from the <see cref="Microsoft.BingAds.CampaignManagement"/> namespace, for example a <see cref="TextAd"/> object.
        /// </summary>
        protected T Ad { get; set; }

        /// <summary>
        /// The historical performance data for the ad.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        private static readonly IBulkMapping<BulkAd<T>>[] Mappings =
        {
            new SimpleBulkMapping<BulkAd<T>>(StringTable.Status,
                c => c.Ad.Status.ToBulkString(),
                (v, c) => c.Ad.Status = v.ParseOptional<AdStatus>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.Id,
                c => c.Ad.Id.ToBulkString(),
                (v, c) => c.Ad.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.ParentId,
                c => c.AdGroupId.ToBulkString(),
                (v, c) => c.AdGroupId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.EditorialStatus,
                c => c.Ad.EditorialStatus.ToBulkString(),
                (v, c) => c.Ad.EditorialStatus = v.ParseOptional<AdEditorialStatus>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.DevicePreference,
                c => c.Ad.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.Ad.DevicePreference = v.ParseDevicePreference()
            )
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
            }
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);

            PerformanceData = PerformanceData.ReadFromRowValuesOrNull(values);
        }
    }
}
