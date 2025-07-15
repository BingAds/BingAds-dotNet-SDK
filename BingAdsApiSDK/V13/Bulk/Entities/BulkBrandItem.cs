//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
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

using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a brand item that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkBrandItem.BrandItem"/> property that can be read and written as fields of the BrandItem record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Brand Item</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkBrandItem : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the brand item that contains the brand item.
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// The identifier of the brand list that contains the brand item.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? BrandListId { get; set; }

        /// <summary>
        /// Defines the name of brand item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines the brand name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Defines the brand url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// Defines the editorial status
        /// </summary>
        public string EditorialStatus { get; set; }

        /// <summary>
        /// Defines the editorial status date
        /// </summary>
        public DateTime? EditorialStatusDate { get; set; }

        /// <summary>
        /// Defines a brand item within an brand list. 
        /// </summary>
        public BrandItem BrandItem { get; set; }

        private static readonly IBulkMapping<BulkBrandItem>[] Mappings =
        {
            new SimpleBulkMapping<BulkBrandItem>(StringTable.Id,
                c => c.Id.ToBulkString(),
                (v, c) => c.Id = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkBrandItem>(StringTable.ParentId,
                c => c.BrandListId.ToBulkString(),
                (v, c) => c.BrandListId = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkBrandItem>(StringTable.Name,
                c => c.Name,
                (v, c) => c.Name = v
                ),

            new SimpleBulkMapping<BulkBrandItem>(StringTable.BrandId,
                c => c.BrandItem.BrandId.ToBulkString(),
                (v, c) => c.BrandItem.BrandId = v.ParseOptional<long>() == null ? 0 : (long)v.ParseOptional<long>()
                ),

             new SimpleBulkMapping<BulkBrandItem>(StringTable.BrandName,
                c => c.BrandName,
                (v, c) => c.BrandName = v
                ),

             new SimpleBulkMapping<BulkBrandItem>(StringTable.BrandUrl,
                c => c.BrandUrl,
                (v, c) => c.BrandUrl = v
                ),

             new SimpleBulkMapping<BulkBrandItem>(StringTable.EditorialStatus,
                c => c.EditorialStatus,
                (v, c) => c.EditorialStatus = v
                ),

             new SimpleBulkMapping<BulkBrandItem>(StringTable.StatusDateTime,
                c => c.EditorialStatusDate.ToDateTimeBulkString(null),
                (v, c) => c.EditorialStatusDate = v.ParseOptionalDateTime()
                ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            BrandItem = new BrandItem();
            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(BrandItem, "BrandItem");

            this.ConvertToValues(values, Mappings);
        }
    }
}
