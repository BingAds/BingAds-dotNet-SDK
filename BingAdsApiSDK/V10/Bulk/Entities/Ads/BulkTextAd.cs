using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a text ad. 
    /// This class exposes the <see cref="BulkTextAd.TextAd"/> property that can be read and written as fields of the Text Ad record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620263">Text Ad</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkTextAd : BulkAd<TextAd>
    {
        /// <summary>
        /// <para>
        /// The text ad. 
        /// </para>
        /// </summary>
        public TextAd TextAd
        {
            get { return Ad; }
            set { Ad = value; }
        }

        private static readonly IBulkMapping<BulkTextAd>[] Mappings =
        {            
            new SimpleBulkMapping<BulkTextAd>(StringTable.Title,
                c => c.TextAd.Title,
                (v, c) => c.TextAd.Title = v
            ),

            new SimpleBulkMapping<BulkTextAd>(StringTable.Text,
                c => c.TextAd.Text,
                (v, c) => c.TextAd.Text = v
            ),

            new SimpleBulkMapping<BulkTextAd>(StringTable.DisplayUrl,
                c => c.TextAd.DisplayUrl,
                (v, c) => c.TextAd.DisplayUrl = v
            ),

            new SimpleBulkMapping<BulkTextAd>(StringTable.DestinationUrl,
                c => c.TextAd.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.TextAd.DestinationUrl = v.GetValueOrEmptyString()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            TextAd = new TextAd { Type = AdType.Text };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }


        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(TextAd, "TextAd");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
