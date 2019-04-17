//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 12.13
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
    /// Represents a Similar Remarketing List that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkSimilarRemarketingList.SimilarRemarketingList"/> property that can be read and written as fields of the Similar Remarketing List record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Similar Remarketing List</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkSimilarRemarketingList : SingleRecordBulkEntity
    {
        /// <summary>
        /// The Similar Remarketing List.
        /// </summary>
        public SimilarRemarketingList SimilarRemarketingList { get; set; }

        /// <summary>
        /// The status of the Similar Remarketing List.
        /// The value is Active if the Similar Remarketing List is available to be associated with an ad group. 
        /// The value is Deleted if the Similar Remarketing List is deleted. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        private static readonly IBulkMapping<BulkSimilarRemarketingList>[] Mappings =
        {
            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.Id,
                c => c.SimilarRemarketingList.Id.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.ParentId,
                c => c.SimilarRemarketingList.ParentId.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.ParentId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.Audience,
                c => c.SimilarRemarketingList.Name,
                (v, c) => c.SimilarRemarketingList.Name = v
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.Description,
                c => c.SimilarRemarketingList.Description,
                (v, c) => c.SimilarRemarketingList.Description = v
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.MembershipDuration,
                c => c.SimilarRemarketingList.MembershipDuration.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.MembershipDuration = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.Scope,
                c => c.SimilarRemarketingList.Scope.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.Scope = v.ParseOptional<EntityScope>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.AudienceSearchSize,
                c => c.SimilarRemarketingList.SearchSize.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.SearchSize = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.AudienceNetworkSize,
                c => c.SimilarRemarketingList.AudienceNetworkSize.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.AudienceNetworkSize = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.SupportedCampaignTypes,
                c => c.SimilarRemarketingList.SupportedCampaignTypes.WriteAudienceSupportedCampaignTypes(";"),
                (v, c) => c.SimilarRemarketingList.SupportedCampaignTypes = v.ParseAudienceSupportedCampaignTypes()
            ),

            new SimpleBulkMapping<BulkSimilarRemarketingList>(StringTable.SourceId,
                c => c.SimilarRemarketingList.SourceId.ToBulkString(),
                (v, c) => c.SimilarRemarketingList.SourceId = v.Parse<long>()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            SimilarRemarketingList = new SimilarRemarketingList { };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(SimilarRemarketingList, typeof(SimilarRemarketingList).Name);

            this.ConvertToValues(values, Mappings);
        }
    }
}