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
    /// Represents a call ad extension. 
    /// This class exposes the <see cref="BulkCallAdExtension.CallAdExtension"/> property that can be read and written 
    /// as fields of the Call Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620234">Call Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCallAdExtension : BulkAdExtensionBase<CallAdExtension>
    {
        /// <summary>
        /// The call ad extension.
        /// </summary>
        public CallAdExtension CallAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkCallAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkCallAdExtension>(StringTable.PhoneNumber,
                c => c.CallAdExtension.PhoneNumber,
                (v, c) => c.AdExtension.PhoneNumber = v
            ), 

            new SimpleBulkMapping<BulkCallAdExtension>(StringTable.CountryCode,
                c => c.CallAdExtension.CountryCode,
                (v, c) => c.AdExtension.CountryCode = v
            ), 

            new SimpleBulkMapping<BulkCallAdExtension>(StringTable.IsCallOnly,
                c => c.CallAdExtension.IsCallOnly.ToString(),
                (v, c) => c.AdExtension.IsCallOnly = v.ParseOptional<bool>()
            ), 

            new SimpleBulkMapping<BulkCallAdExtension>(StringTable.IsCallTrackingEnabled,
                c => c.CallAdExtension.IsCallTrackingEnabled.ToString(),
                (v, c) => c.AdExtension.IsCallTrackingEnabled = v.ParseOptional<bool>()
            ), 

            new SimpleBulkMapping<BulkCallAdExtension>(StringTable.RequireTollFreeTrackingNumber,
                c => c.CallAdExtension.RequireTollFreeTrackingNumber.ToString(),
                (v, c) => c.AdExtension.RequireTollFreeTrackingNumber = v.ParseOptional<bool>()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            CallAdExtension = new CallAdExtension { Type = "CallAdExtension" };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(CallAdExtension, "CallAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
