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

using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// Represents a company item that can be read or written in a bulk file.
    /// </summary>
    public class BulkCompanyItem : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the company list that contains the company item.
        /// Corresponds to the 'Parent Id' field in the bulk file.
        /// </summary>
        public long? CompanyListId { get; set; }

        /// <summary>
        /// The company item.
        /// </summary>
        public CompanyName CompanyItem { get; set; }

        /// <summary>
        /// The bulk upload status (Active or Deleted). Only this property is written to 'Status'.
        /// Downloaded matching status is available on <see cref="CompanyItem"/>.
        /// The service currently uses the same CSV column for both kinds of status.
        /// </summary>
        public Status? Status { get; set; }

        private static readonly IBulkMapping<BulkCompanyItem>[] Mappings =
        {
            new SimpleBulkMapping<BulkCompanyItem>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) =>
                {
                    if (!string.IsNullOrEmpty(v) && Enum.IsDefined(typeof(Status), v))
                    {
                        c.Status = v.ParseOptional<Status>();
                    }
                    else
                    {
                        c.CompanyItem.Status = v.ParseOptional<CompanyNameStatus>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCompanyItem>(StringTable.Id,
                c => c.CompanyItem.Id.ToBulkString(),
                (v, c) => c.CompanyItem.Id = v.ParseOptional<long>()
            ),
            new SimpleBulkMapping<BulkCompanyItem>(StringTable.ParentId,
                c => c.CompanyListId.ToBulkString(),
                (v, c) => c.CompanyListId = v.ParseOptional<long>()
            ),
            new SimpleBulkMapping<BulkCompanyItem>(StringTable.CompanyName,
                c => c.CompanyItem.Name,
                (v, c) => c.CompanyItem.Name = v
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            CompanyItem = new CompanyName();
            Status = null;
            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(CompanyItem, nameof(CompanyItem));
            this.ConvertToValues(values, Mappings);
        }
    }
}
