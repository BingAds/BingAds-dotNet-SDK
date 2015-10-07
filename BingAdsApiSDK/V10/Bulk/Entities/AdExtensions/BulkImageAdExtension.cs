using System.Linq;
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
    /// Represents an image ad extension that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkImageAdExtension.ImageAdExtension"/> property that can be read and written 
    /// as fields of the Image Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620277">Image Ad Extension</see>. </para>
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

            new SimpleBulkMapping<BulkImageAdExtension>(StringTable.MediaIds,
                c =>
                {
                    if (c.ImageAdExtension.ImageMediaIds == null)
                    {
                        return null;
                    }
                    return string.Join(";", c.ImageAdExtension.ImageMediaIds);
                },
                (v, c) => c.ImageAdExtension.ImageMediaIds = v.Split(';').Select(long.Parse).ToList()
            ), 
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
