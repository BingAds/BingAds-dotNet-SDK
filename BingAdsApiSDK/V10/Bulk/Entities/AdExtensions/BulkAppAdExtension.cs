using Microsoft.BingAds.V10.Internal;
using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a app ad extension. 
    /// This class exposes the <see cref="BulkAppAdExtension.AppAdExtension"/> property that can be read and written 
    /// as fields of the App Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620280">App Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAppAdExtension : BulkAdExtensionBase<AppAdExtension>
    {
        /// <summary>
        /// The app ad extension.
        /// </summary>
        public AppAdExtension AppAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkAppAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkAppAdExtension>(StringTable.AppPlatform,
                c => c.AppAdExtension.AppPlatform,
                (v, c) => c.AdExtension.AppPlatform = v
            ), 

            new SimpleBulkMapping<BulkAppAdExtension>(StringTable.AppStoreId,
                c => c.AppAdExtension.AppStoreId,
                (v, c) => c.AdExtension.AppStoreId = v
            ), 

            new SimpleBulkMapping<BulkAppAdExtension>(StringTable.DestinationUrl,
                c => c.AppAdExtension.DestinationUrl,
                (v, c) => c.AdExtension.DestinationUrl = v
            ), 

            new SimpleBulkMapping<BulkAppAdExtension>(StringTable.Text,
                c => c.AppAdExtension.DisplayText,
                (v, c) => c.AdExtension.DisplayText = v
            ), 

            new SimpleBulkMapping<BulkAppAdExtension>(StringTable.DevicePreference,
                c => c.AppAdExtension.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.AdExtension.DevicePreference = v.ParseDevicePreference()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AppAdExtension = new AppAdExtension { Type = "AppAdExtension" };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(AppAdExtension, "AppAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
