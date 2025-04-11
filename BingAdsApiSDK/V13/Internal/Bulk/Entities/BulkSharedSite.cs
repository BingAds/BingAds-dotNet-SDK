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

using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class for the bulk sites that are assigned individually to a list.
    /// </summary>
    public class BulkSharedSite : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines a negative site.
        /// </summary>
        public Site Site { get; set; }

        /// <summary>
        /// The status of the site association.
        /// The value is Active if the negative site is assigned to the parent entity identified by the EntityId. 
        /// The value is Deleted if the negative site is removed from the parent entity, or should be removed in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// The URL of a website on which you do not want your ads displayed.
        /// Corresponds to the 'Website' field in the bulk file.  
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected long? ParentId { get; set; }

        internal BulkSharedSite()
        {
        }

        private static readonly IBulkMapping<BulkSharedSite>[] Mappings =
        {      
            new SimpleBulkMapping<BulkSharedSite>(StringTable.Id,
                c => c.Site.Id.ToBulkString(),
                (v, c) => c.Site.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkSharedSite>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkSharedSite>(StringTable.ParentId,
                c => c.ParentId.ToBulkString(),
                (v, c) => c.ParentId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkSharedSite>(StringTable.AccountPlacementListItemUrl,
                c => c.Url,
                (v, c) => c.Url = v
            )
        };
        
        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Site = new Site { Type = "Site" };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(Site, "Site");

            this.ConvertToValues(values, Mappings);
        }
    }
}
