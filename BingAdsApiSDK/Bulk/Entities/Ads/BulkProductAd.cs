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
    /// Represents a product ad. 
    /// This class exposes the <see cref="BulkProductAd.ProductAd"/> property that can be read and written as fields of the Product Ad record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511555">Product Ad</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkProductAd : BulkAd<ProductAd>
    {
        /// <summary>
        /// <para>
        /// The product ad. 
        /// </para>
        /// </summary>
        public ProductAd ProductAd
        {
            get { return Ad; }
            set { Ad = value; }
        }

        private static readonly IEnumerable<IBulkMapping<BulkProductAd>> Mappings = new IBulkMapping<BulkProductAd>[]
        {            
            new SimpleBulkMapping<BulkProductAd>(StringTable.PromotionalText,
                c => c.ProductAd.PromotionalText.ToOptionalBulkString(),
                (v, c) => c.ProductAd.PromotionalText = v.GetValueOrEmptyString()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ProductAd = new ProductAd { Type = AdType.Product };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ProductAd, "ProductAd");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
