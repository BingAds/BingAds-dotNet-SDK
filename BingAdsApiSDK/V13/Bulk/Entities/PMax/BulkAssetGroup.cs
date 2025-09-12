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
    /// Represents an asset group. 
    /// This class exposes the <see cref="BulkAssetGroup.AssetGroup"/> property that can be read and written as fields of the Asset Group record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Asset Group</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAssetGroup : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the campaign that contains the asset group.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId { get; set; }

        /// <summary>
        /// The name of the campaign that contains the asset group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The AssetGroup Data Object of the Campaign Management Service. A subset of AssetGroup properties are available 
        /// in the Asset Group record. For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Asset Group</see>.
        /// </summary>
        public AssetGroup AssetGroup { get; set; }


        private static readonly IBulkMapping<BulkAssetGroup>[] Mappings =
        {
            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Id,
                c => c.AssetGroup.Id.ToBulkString(),
                (v, c) => c.AssetGroup.Id = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Status,
                c => c.AssetGroup.Status.ToBulkString(),
                (v, c) =>c.AssetGroup.Status = v.ParseOptional<AssetGroupStatus>()
                ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.ParentId,
                c => c.CampaignId.ToBulkString(),
                (v, c) => c.CampaignId = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
                ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.AssetGroup,
                c => c.AssetGroup.Name,
                (v, c) => c.AssetGroup.Name = v
                ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.StartDate,
                c => c.AssetGroup.StartDate.ToDateBulkString(),
                (v, c) => c.AssetGroup.StartDate = v.ParseDate()
                ),
            new SimpleBulkMapping<BulkAssetGroup>(StringTable.EndDate,
                c => c.AssetGroup.EndDate.ToDateBulkString(),
                (v, c) => c.AssetGroup.EndDate = v.ParseDate()
                ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.BusinessName,
                c => c.AssetGroup.BusinessName,
                (v, c) => c.AssetGroup.BusinessName = v
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Path1,
                c => c.AssetGroup.Path1,
                (v, c) => c.AssetGroup.Path1 = v
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Path2,
                c => c.AssetGroup.Path2,
                (v, c) => c.AssetGroup.Path2 = v
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Descriptions,
                c => c.AssetGroup.Descriptions.ToTextAssetLinksBulkString(),
                (v, c) => c.AssetGroup.Descriptions = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Headlines,
                c => c.AssetGroup.Headlines.ToTextAssetLinksBulkString(),
                (v, c) => c.AssetGroup.Headlines = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.Images,
                c => c.AssetGroup.Images == null ? null : c.AssetGroup.Images.ToImageAssetLinksBulkString(),
                (v, c) => c.AssetGroup.Images = v.ParseImageAssetLinks()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.LongHeadlines,
                c => c.AssetGroup.LongHeadlines.ToTextAssetLinksBulkString(),
                (v, c) => c.AssetGroup.LongHeadlines = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.CallToAction,
                c => c.AssetGroup.CallToAction.ToBulkString(),
                (v, c) => c.AssetGroup.CallToAction = v.ParseOptional<CallToAction>()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.FinalUrl,
                c => c.AssetGroup.FinalUrls.WriteUrls("; ", c.AssetGroup.Id),
                (v, c) => c.AssetGroup.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.FinalMobileUrl,
                c => c.AssetGroup.FinalMobileUrls.WriteUrls("; ", c.AssetGroup.Id),
                (v, c) => c.AssetGroup.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.EditorialStatus,
                c => c.AssetGroup.EditorialStatus.ToBulkString(),
                (v, c) => c.AssetGroup.EditorialStatus = v.ParseOptional<AssetGroupEditorialStatus>()
            ),
            
            new SimpleBulkMapping<BulkAssetGroup>(StringTable.TrackingTemplate,
                c => c.AssetGroup.TrackingUrlTemplate.ToOptionalBulkString(c.AssetGroup.Id),
                (v, c) => c.AssetGroup.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),
            
            new SimpleBulkMapping<BulkAssetGroup>(StringTable.FinalUrlSuffix,
                c => c.AssetGroup.FinalUrlSuffix.ToOptionalBulkString(c.AssetGroup.Id),
                (v, c) => c.AssetGroup.FinalUrlSuffix = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAssetGroup>(StringTable.CustomParameter,
                c => c.AssetGroup.UrlCustomParameters.ToBulkString(c.AssetGroup.Id),
                (v, c) => c.AssetGroup.UrlCustomParameters = v.ParseCustomParameters()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AssetGroup = new AssetGroup {};

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(AssetGroup, "AssetGroup");

            this.ConvertToValues(values, Mappings);
        }
    }
}
