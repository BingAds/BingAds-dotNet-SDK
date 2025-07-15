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
    /// <para>
    /// Represents a Campaign Negative Store Criterion that can be read or written in a bulk file.
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Campaign Negative Store Criterion</see> </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignNegativeStoreCriterion : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines a Negative Campaign Criterion.
        /// </summary>
        public NegativeCampaignCriterion NegativeCampaignCriterion { get; set; }

        /// <summary>
        /// The status of the Campaign Criterion.
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// The name of the campaign.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        private static readonly IBulkMapping<BulkCampaignNegativeStoreCriterion>[] Mappings =
        {
            new SimpleBulkMapping<BulkCampaignNegativeStoreCriterion>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkCampaignNegativeStoreCriterion>(StringTable.Id,
                c => c.NegativeCampaignCriterion.Id.ToBulkString(),
                (v, c) => c.NegativeCampaignCriterion.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaignNegativeStoreCriterion>(StringTable.ParentId,
                c => c.NegativeCampaignCriterion.CampaignId.ToBulkString(true),
                (v, c) => c.NegativeCampaignCriterion.CampaignId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkCampaignNegativeStoreCriterion>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),
            
            new SimpleBulkMapping<BulkCampaignNegativeStoreCriterion>(StringTable.MerchantCenterId,
                c =>
                {
                    var store = c.NegativeCampaignCriterion.Criterion as StoreCriterion;
                    return store == null ? null : store.StoreId.ToBulkString();
                },
                (v, c) =>
                {
                    var store = c.NegativeCampaignCriterion.Criterion as StoreCriterion;

                    var storeId = v.ParseOptional<long>();
                    if(storeId != null && store != null)
                    {
                        store.StoreId = storeId;
                    }
                }
            ),
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(NegativeCampaignCriterion, typeof(NegativeCampaignCriterion).Name);

            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            NegativeCampaignCriterion = new NegativeCampaignCriterion()
            {
                Criterion = new StoreCriterion()
                {
                    Type = typeof(StoreCriterion).Name,
                },
                Type = typeof(NegativeCampaignCriterion).Name
            };

            values.ConvertToEntity(this, Mappings);
        }
    }
}
