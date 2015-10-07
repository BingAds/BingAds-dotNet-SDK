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
    /// Represents an image ad extension that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkImageAdExtension.ImageAdExtension"/> property that can be read and written 
    /// as fields of the Image Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511909">Image Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkImageAdExtension : BulkAdExtensionBase<ImageAdExtension>
    {
        /// <summary>
        /// The image ad extension.
        /// </summary>
        public ImageAdExtension ImageAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkImageAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkImageAdExtension>(StringTable.DestinationUrl,
                c => c.ImageAdExtension.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.ImageAdExtension.DestinationUrl = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkImageAdExtension>(StringTable.AltText,
                c => c.ImageAdExtension.AlternativeText,
                (v, c) => c.ImageAdExtension.AlternativeText = v
            ), 

            new SimpleBulkMapping<BulkImageAdExtension>(StringTable.MediaId,
                c => c.ImageAdExtension.ImageMediaId.ToBulkString(),
                (v, c) => c.ImageAdExtension.ImageMediaId = v.Parse<long>()
            )
        }; 

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ImageAdExtension = new ImageAdExtension { Type = "ImageAdExtension" };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ImageAdExtension, "ImageAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
