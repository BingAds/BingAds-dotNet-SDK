//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 9.3
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

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
    /// <para>For more information, see Product Ad Extension at http://go.microsoft.com/fwlink/?LinkID=511516. </para>
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

        internal override void WriteToStream(IBulkObjectWriter rowWriter)
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
            });

            foreach (var bulkSiteLink in ConvertRawToBulkProductConditionCollections())
            {
                bulkSiteLink.WriteToStream(rowWriter);
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
                else if (reader.TryRead(x => x.Equals(_identifier), out identitifier))
                {
                    if (identitifier.Status == AdExtensionStatus.Deleted)
                    {
                        _hasDeleteAllRow = true;
                    }
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
