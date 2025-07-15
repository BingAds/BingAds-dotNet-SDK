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
    /// Represents a Campaign Criterion that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkCampaignProductScope.BiddableCampaignCriterion"/> property that can be read and written as fields of the Campaign Criterion record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkId=618643">Campaign Product Scope</see> </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignProductScope : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines an Campaign Criterion.
        /// </summary>
        public BiddableCampaignCriterion BiddableCampaignCriterion { get; set; }

        /// <summary>
        /// Defines the status of the Campaign Criterion.
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        private static readonly IBulkMapping<BulkCampaignProductScope>[] Mappings =
        {
            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.Status,
                c => c.BiddableCampaignCriterion.Status.ToBulkString(),
                (v, c) => c.BiddableCampaignCriterion.Status = v.ParseOptional<CampaignCriterionStatus>()
            ),

            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.Id,
                c => c.BiddableCampaignCriterion.Id.ToBulkString(),
                (v, c) => c.BiddableCampaignCriterion.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.ParentId,
                c => c.BiddableCampaignCriterion.CampaignId.ToBulkString(true),
                (v, c) => c.BiddableCampaignCriterion.CampaignId = v.Parse<long>()
            ),
            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new ComplexBulkMapping<BulkCampaignProductScope>(
                ConditionsToRowValues,
                RowValuesToConditions
            )
        };

        private static void RowValuesToConditions(RowValues values, BulkCampaignProductScope c)
        {
            var product = (ProductScope) c.BiddableCampaignCriterion.Criterion;

            product.Conditions = new List<ProductCondition>();

            ProductConditionHelper.AddConditionsFromRowValues(values, product.Conditions);
        }

        private static void ConditionsToRowValues(BulkCampaignProductScope c, RowValues values)
        {
            if (c.BiddableCampaignCriterion.Criterion == null)
            {
                return;
            }

            var product = (ProductScope) c.BiddableCampaignCriterion.Criterion;

            if (product.Conditions == null)
            {
                return;
            }

            ProductConditionHelper.AddRowValuesFromConditions(product.Conditions, values);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(BiddableCampaignCriterion, typeof(CampaignCriterion).Name);

            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            BiddableCampaignCriterion = new BiddableCampaignCriterion
            {
                Criterion = new ProductScope
                {
                    Type = typeof(ProductScope).Name,
                },
                CriterionBid = null,
                Type = typeof(BiddableCampaignCriterion).Name,
            };

            values.ConvertToEntity(this, Mappings);
        }
    }
}
