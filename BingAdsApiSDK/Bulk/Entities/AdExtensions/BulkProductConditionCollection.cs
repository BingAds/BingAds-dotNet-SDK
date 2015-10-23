//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
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
    /// Represents the product condition collection for a product ad extension. Each product condition collection can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkProductConditionCollection.ProductConditionCollection"/> property that can be read and written 
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
    public class BulkProductConditionCollection : SingleRecordBulkEntity
    {
        /// <summary>
        /// The collection of product conditions for a product ad extension.
        /// </summary>
        public ProductConditionCollection ProductConditionCollection { get; set; }

        internal BulkProductAdExtensionIdentifier Identifier { get; private set; }

        /// <summary>
        /// The identifier of the ad extension. 
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? AdExtensionId
        {
            get { return Identifier.AdExtensionId; }
            set { Identifier.AdExtensionId = value; }
        }

        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId
        {
            get { return Identifier.AccountId; }
            set { Identifier.AccountId = value; }
        }

        /// <summary>
        /// The status of the ad extension. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public AdExtensionStatus? Status
        {
            get { return Identifier.Status; }
            set { Identifier.Status = value; }
        }

        /// <summary>
        /// The version of the ad extension. 
        /// Corresponds to the 'Version' field in the bulk file. 
        /// </summary>
        public int? Version
        {
            get { return Identifier.Version; }
            set { Identifier.Version = value; }
        }

        /// <summary>
        /// The name of the ad extension. 
        /// Corresponds to the 'Name' field in the bulk file. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The product ad extension's store identifier. 
        /// Corresponds to the 'Store Id' field in the bulk file. 
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// The product ad extension's store name. 
        /// Corresponds to the 'Store Name' field in the bulk file. 
        /// </summary>
        public string StoreName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BulkProductConditionCollection class.
        /// </summary>
        public BulkProductConditionCollection()
        {
            Identifier = new BulkProductAdExtensionIdentifier();
        }

        private static readonly IBulkMapping<BulkProductConditionCollection>[] Mappings =
        {
            new ComplexBulkMapping<BulkProductConditionCollection>(
                ConditionsToRowValues,
                RowValuesToConditions
            ),
            
            new SimpleBulkMapping<BulkProductConditionCollection>(StringTable.Name,
                c => c.Name,
                (v, c) => c.Name = v
            ),

            new SimpleBulkMapping<BulkProductConditionCollection>(StringTable.BingMerchantCenterId,
                c => c.StoreId.ToBulkString(),
                (v, c) => c.StoreId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkProductConditionCollection>(StringTable.BingMerchantCenterName,                
                c => c.StoreName,
                (v, c) => c.StoreName = v                
            )
        };        

        private static void RowValuesToConditions(RowValues values, BulkProductConditionCollection c)
        {
            ProductConditionHelper.AddConditionsFromRowValues(values, c.ProductConditionCollection.Conditions);             
        }
        
        private static void ConditionsToRowValues(BulkProductConditionCollection c, RowValues values)
        {
            ProductConditionHelper.AddRowValuesFromConditions(c.ProductConditionCollection.Conditions, values);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ProductConditionCollection = new ProductConditionCollection
            {
                Conditions = new List<ProductCondition>()
            };

            Identifier.ReadFromRowValues(values);

            values.ConvertToEntity(this, Mappings);            
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ProductConditionCollection, "ProductConditionCollection");

            ValidatePropertyNotNull(ProductConditionCollection.Conditions, "ProductConditionCollection.Conditions");

            Identifier.WriteToRowValues(values, excludeReadonlyData);                       

            this.ConvertToValues(values, Mappings);
        }

        internal override bool CanEncloseInMultilineEntity
        {
            get { return true; }
        }

        internal override MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            return new BulkProductAdExtension(this);
        }
    }
}
