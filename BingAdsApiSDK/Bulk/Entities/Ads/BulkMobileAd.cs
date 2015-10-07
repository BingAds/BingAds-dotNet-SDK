using System.Collections.Generic;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a mobile ad. 
    /// This class exposes the <see cref="BulkMobileAd.MobileAd"/> property that can be read and written as fields of the Mobile Ad record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511553">Mobile Ad</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkMobileAd : BulkAd<MobileAd>
    {
        /// <summary>
        /// <para>
        /// The mobile ad. 
        /// </para>
        /// </summary>
        public MobileAd MobileAd
        {
            get { return Ad; }
            set { Ad = value; }
        }

        private static readonly IEnumerable<IBulkMapping<BulkMobileAd>> Mappings = new IBulkMapping<BulkMobileAd>[]
        {            
            new SimpleBulkMapping<BulkMobileAd>(StringTable.Title,
                c => c.MobileAd.Title,
                (v, c) => c.MobileAd.Title = v
            ),

            new SimpleBulkMapping<BulkMobileAd>(StringTable.Text,
                c => c.MobileAd.Text,
                (v, c) => c.MobileAd.Text = v
            ),

            new SimpleBulkMapping<BulkMobileAd>(StringTable.DisplayUrl,
                c => c.MobileAd.DisplayUrl.ToOptionalBulkString(),
                (v, c) => c.MobileAd.DisplayUrl = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkMobileAd>(StringTable.DestinationUrl,
                c => c.MobileAd.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.MobileAd.DestinationUrl = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkMobileAd>(StringTable.BusinessName,
                c => c.MobileAd.BusinessName.ToOptionalBulkString(),
                (v, c) => c.MobileAd.BusinessName = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkMobileAd>(StringTable.PhoneNumber,
                c => c.MobileAd.PhoneNumber.ToOptionalBulkString(),
                (v, c) => c.MobileAd.PhoneNumber = v.GetValueOrEmptyString()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            MobileAd = new MobileAd { Type = AdType.Mobile };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(MobileAd, "MobileAd");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
