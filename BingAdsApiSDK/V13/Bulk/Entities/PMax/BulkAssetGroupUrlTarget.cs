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
    /// Represents an Asset Group Url Target that can be read or written in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Asset Group Url Target</see> </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAssetGroupUrlTarget : SingleRecordBulkEntity

    {
        /// <summary>
        ///  Possible values are Active and Deleted
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// The Id of target.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// The Id of asset group that contains the target.
        /// Corresponds to the 'ParentId' field in the bulk file. 
        /// </summary>
        public long AssetGroupId { get; set; }

        /// <summary>
        /// TargetCondition
        /// </summary>
        public string TargetCondition1 { get; set; }

        public string TargetCondition2 { get; set; }

        public string TargetCondition3 { get; set; }

        /// <summary>
        /// TargetConditionOperator
        /// </summary>
        public string TargetConditionOperator1 { get; set; }

        public string TargetConditionOperator2 { get; set; }

        public string TargetConditionOperator3 { get; set; }

        /// <summary>
        /// TargetValue
        /// </summary>
        public string TargetValue1 { get; set; }

        public string TargetValue2 { get; set; }

        public string TargetValue3 { get; set; }


        private static readonly IBulkMapping<BulkAssetGroupUrlTarget>[] Mappings =
        {
            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.Id,
                c => c.Id.ToBulkString(),
                (v, c) => c.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.ParentId,
                c => c.AssetGroupId.ToBulkString(),
                (v, c) => c.AssetGroupId = v.Parse<long>(true)
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetCondition1,
                c => c.TargetCondition1,
                (v, c) => c.TargetCondition1 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetCondition2,
                c => c.TargetCondition2,
                (v, c) => c.TargetCondition2 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetCondition3,
                c => c.TargetCondition3,
                (v, c) => c.TargetCondition3 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetConditionOperator1,
                c => c.TargetConditionOperator1,
                (v, c) => c.TargetConditionOperator1 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetConditionOperator2,
                c => c.TargetConditionOperator2,
                (v, c) => c.TargetConditionOperator2 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetConditionOperator3,
                c => c.TargetConditionOperator3,
                (v, c) => c.TargetConditionOperator3 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetValue1,
                c => c.TargetValue1,
                (v, c) => c.TargetValue1 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetValue2,
                c => c.TargetValue2,
                (v, c) => c.TargetValue2 = v
            ),

            new SimpleBulkMapping<BulkAssetGroupUrlTarget>(StringTable.AssetGroupTargetValue3,
                c => c.TargetValue3,
                (v, c) => c.TargetValue3 = v
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

    }
}
