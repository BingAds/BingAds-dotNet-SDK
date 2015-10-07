using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a product ad extension. 
    /// This class exposes the <see cref="BulkProductAdExtension.ProductAdExtension"/> property that can be read and written 
    /// as fields of the Product Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511516">Product Ad Extension</see>. </para>
    /// </summary>
    /// <remarks>
    /// <para> The Product Ad Extension record includes the distinct properties of the <see cref="BulkProductConditionCollection"/> class, combined with 
    /// the commmon properties of the <see cref="BulkProductAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="ProductAdExtension"/>.
    /// </para>
    /// <para>
    /// One <see cref="BulkProductAdExtension"/> has one or more <see cref="BulkProductConditionCollection"/>. Each <see cref="BulkProductConditionCollection"/> instance 
    /// corresponds to one Product Ad Extension record in the bulk file. If you upload a <see cref="BulkProductAdExtension"/>, 
    /// then you are effectively replacing any existing product conditions for the product ad extension. 
    /// </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkProductAdExtension : MultiRecordBulkEntity
    {
        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// The product ad extension.
        /// </summary>
        public ProductAdExtension ProductAdExtension { get; set; }

        private readonly List<BulkProductConditionCollection> _productConditionCollections = new List<BulkProductConditionCollection>();

        /// <summary>
        /// The list of <see cref="BulkProductConditionCollection"/> are represented by multiple Product Ad Extension records in the file.
        /// Each item in the list corresponds to a separate Product Ad Extension record that includes the distinct properties of the <see cref="BulkProductConditionCollection"/> class, combined with 
        /// the commmon properties of the <see cref="BulkProductAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="ProductAdExtension"/>.
        /// </summary>
        public IEnumerable<BulkProductConditionCollection> ProductConditionCollections
        {
            get { return _productConditionCollections; }
        }

        internal override IReadOnlyList<BulkEntity> ChildEntities
        {
            get { return _productConditionCollections; }
        }

        private readonly BulkProductAdExtensionIdentifier _identifier;

        private bool _hasDeleteAllRow;

        /// <summary>
        /// Initializes a new instance of the BulkProductAdExtension class. 
        /// </summary>
        public BulkProductAdExtension()
        {

        }

        internal BulkProductAdExtension(BulkProductAdExtensionIdentifier identifier)
            : this()
        {
            ProductAdExtension = new ProductAdExtension { Type = "ProductAdExtension" };

            _identifier = identifier;

            _hasDeleteAllRow = identifier.Status == AdExtensionStatus.Deleted;

            ProductAdExtension.Id = identifier.AdExtensionId;

            ProductAdExtension.Status = identifier.Status;

            ProductAdExtension.Version = identifier.Version;

            AccountId = identifier.AccountId;

            ProductAdExtension.Name = identifier.Name;
        }

        internal BulkProductAdExtension(BulkProductConditionCollection productCollection)
            : this(productCollection.Identifier)
        {
            AddProductCollection(productCollection);
        }

        private void AddProductCollection(BulkProductConditionCollection productCollection)
        {
            _productConditionCollections.Add(productCollection);

            ProductAdExtension.StoreId = productCollection.StoreId;

            ProductAdExtension.StoreName = productCollection.StoreName;
        }

        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ProductAdExtension, "ProductAdExtension");

            ValidatePropertyNotNull(ProductAdExtension.ProductSelection, "ProductAdExtension.ProductSelection");

            rowWriter.WriteObjectRow(new BulkProductAdExtensionIdentifier
            {
                Status = AdExtensionStatus.Deleted,
                AccountId = AccountId,
                AdExtensionId = ProductAdExtension.Id,
                Name = ProductAdExtension.Name,
                Version = ProductAdExtension.Version
            }, excludeReadonlyData);

            foreach (var bulkSiteLink in ConvertRawToBulkProductConditionCollections())
            {
                bulkSiteLink.WriteToStream(rowWriter, excludeReadonlyData);
            }
        }

        internal override void ReadRelatedDataFromStream(IBulkStreamReader reader)
        {
            var hasMoreRows = true;

            while (hasMoreRows)
            {
                BulkProductConditionCollection nextProductCollection;

                BulkProductAdExtensionIdentifier identitifier;

                if (reader.TryRead(x => x.Identifier.Equals(_identifier), out nextProductCollection))
                {
                    AddProductCollection(nextProductCollection);
                }
                else if (reader.TryRead(x => x.Equals(_identifier) && x.IsDeleteRow, out identitifier))
                {
                    _hasDeleteAllRow = true;                    
                }
                else
                {
                    hasMoreRows = false;
                }
            }

            // API returns empty collection instead of null. Keeping the same behavior
            ProductAdExtension.ProductSelection = _productConditionCollections.Select(s => s.ProductConditionCollection).ToList();

            ProductAdExtension.Status = _productConditionCollections.Count > 0 ? AdExtensionStatus.Active : AdExtensionStatus.Deleted;
        }

        private IEnumerable<BulkProductConditionCollection> ConvertRawToBulkProductConditionCollections()
        {
            return ProductAdExtension.ProductSelection.Select(s => new BulkProductConditionCollection
            {
                ProductConditionCollection = s,
                AccountId = AccountId,
                AdExtensionId = ProductAdExtension.Id,
                Version = ProductAdExtension.Version,
                Name = ProductAdExtension.Name,
                StoreId = ProductAdExtension.StoreId
            });
        }

        internal override bool AllChildrenArePresent
        {
            get { return _hasDeleteAllRow; }
        }
    }
}
