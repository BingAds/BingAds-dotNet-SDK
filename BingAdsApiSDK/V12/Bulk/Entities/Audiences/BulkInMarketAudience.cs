//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 12.0
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

using Microsoft.BingAds.V12.Internal.Bulk;
using Microsoft.BingAds.V12.Internal.Bulk.Mappings;
using Microsoft.BingAds.V12.Internal.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V12.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a in market audience that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkInMarketAudience.InMarketAudience"/> property that can be read and written as fields of the In Market Audience record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">In Market Audience</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkInMarketAudience : SingleRecordBulkEntity
    {
        /// <summary>
        /// The in market audience.
        /// </summary>
        public InMarketAudience InMarketAudience { get; set; }

        /// <summary>
        /// The status of the in market audience.
        /// The value is Active if the in market audience is available to be associated with an ad group. 
        /// The value is Deleted if the in market audience is deleted, or should be deleted in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        private static readonly IBulkMapping<BulkInMarketAudience>[] Mappings =
        {
            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.Id,
                c => c.InMarketAudience.Id.ToBulkString(),
                (v, c) => c.InMarketAudience.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.ParentId,
                c => c.InMarketAudience.ParentId.ToBulkString(),
                (v, c) => c.InMarketAudience.ParentId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.Audience,
                c => c.InMarketAudience.Name,
                (v, c) => c.InMarketAudience.Name = v
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.Description,
                c => c.InMarketAudience.Description,
                (v, c) => c.InMarketAudience.Description = v
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.MembershipDuration,
                c => c.InMarketAudience.MembershipDuration.ToBulkString(),
                (v, c) => c.InMarketAudience.MembershipDuration = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.Scope,
                c => c.InMarketAudience.Scope.ToBulkString(),
                (v, c) => c.InMarketAudience.Scope = v.ParseOptional<EntityScope>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.AudienceSearchSize,
                c => c.InMarketAudience.SearchSize.ToBulkString(),
                (v, c) => c.InMarketAudience.SearchSize = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.AudienceNetworkSize,
                c => c.InMarketAudience.AudienceNetworkSize.ToBulkString(),
                (v, c) => c.InMarketAudience.AudienceNetworkSize = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkInMarketAudience>(StringTable.SupportedCampaignTypes,
                c => c.InMarketAudience.SupportedCampaignTypes.WriteAudienceSupportedCampaignTypes(";"),
                (v, c) => c.InMarketAudience.SupportedCampaignTypes = v.ParseAudienceSupportedCampaignTypes()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            InMarketAudience = new InMarketAudience { };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(InMarketAudience, "InMarketAudience");

            this.ConvertToValues(values, Mappings);
        }
    }
}
