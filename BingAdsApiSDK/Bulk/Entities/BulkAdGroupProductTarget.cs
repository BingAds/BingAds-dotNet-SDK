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

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group product target. 
    /// This class exposes the <see cref="BulkAdGroupProductTarget.BiddableAdGroupCriterion"/> property that can be read and written as fields of the Ad Group Product Target record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511550">Ad Group Product Target</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupProductTarget : SingleRecordBulkEntity
    {
        /// <summary>
        /// The BiddableAdGroupCriterion Data Object of the Campaign Management Service. A subset of BiddableAdGroupCriterion properties are available 
        /// in the Ad Group Product Target record. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511550">Ad Group Product Target</see>.
        /// </summary>
        public BiddableAdGroupCriterion BiddableAdGroupCriterion { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group product target.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the ad group that contains the ad group product target.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        private static readonly IBulkMapping<BulkAdGroupProductTarget>[] Mappings =
        {           
            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Status,
                c => c.BiddableAdGroupCriterion.Status.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Status = v.ParseOptional<AdGroupCriterionStatus>()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Id,
                c => c.BiddableAdGroupCriterion.Id.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Id = v.ParseOptional<long>()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.ParentId,
                c => c.BiddableAdGroupCriterion.AdGroupId.ToBulkString(returnNullForDefaultValue: true),
                (v, c) => c.BiddableAdGroupCriterion.AdGroupId = v.Parse<long>(returnDefaultValueOnNullOrEmpty: true)
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.EditorialStatus,
                c => c.BiddableAdGroupCriterion.EditorialStatus.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.EditorialStatus = v.ParseOptional<AdGroupCriterionEditorialStatus>()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Bid,
                c => ((FixedBid)c.BiddableAdGroupCriterion.CriterionBid).Bid.ToAdGroupBidBulkString(),
                (v, c) => ((FixedBid)c.BiddableAdGroupCriterion.CriterionBid).Bid = v.ParseAdGroupBid()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.DestinationUrl,
                c => c.BiddableAdGroupCriterion.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.DestinationUrl = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Param1,
                c => c.BiddableAdGroupCriterion.Param1.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Param1 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Param2,
                c => c.BiddableAdGroupCriterion.Param2.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Param2 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Param3,
                c => c.BiddableAdGroupCriterion.Param3.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Param3 = v.GetValueOrEmptyString()
            ), 

            new ComplexBulkMapping<BulkAdGroupProductTarget>(
                ConditionsToRowValues,
                RowValuesToConditions
            )
        };

        private static void RowValuesToConditions(RowValues values, BulkAdGroupProductTarget c)
        {
            var product = (Product)c.BiddableAdGroupCriterion.Criterion;
            
            product.Conditions = new List<ProductCondition>();

            ProductConditionHelper.AddConditionsFromRowValues(values, product.Conditions);
        }

        private static void ConditionsToRowValues(BulkAdGroupProductTarget c, RowValues values)
        {
            var product = (Product)c.BiddableAdGroupCriterion.Criterion;

            if (product.Conditions == null)
            {
                return;
            }

            ProductConditionHelper.AddRowValuesFromConditions(product.Conditions, values);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            BiddableAdGroupCriterion = new BiddableAdGroupCriterion
            {
                CriterionBid = new FixedBid
                {
                    Type = "FixedBid"
                },
                Criterion = new Product
                {
                    Type = "Product"
                },
                Type = "BiddableAdGroupCriterion"
            };

            values.ConvertToEntity(this, Mappings);
        }
    }
}
