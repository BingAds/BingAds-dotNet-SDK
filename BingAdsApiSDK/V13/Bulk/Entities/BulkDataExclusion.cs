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
    /// Represents an data exclusion. 
    /// This class exposes the <see cref="BulkDataExclusion.DataExclusion"/> property that can be read and written as fields of the Data Exclusion record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Data Exclusion</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkDataExclusion : SingleRecordBulkEntity
    {
        /// <summary>
        /// The DataExclusion Data Object of the Campaign Management Service. A subset of DataExclusion properties are available 
        /// in the Seasonality Adjustment record. For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Data Exclusion</see>.
        /// </summary>
        public DataExclusion DataExclusion { get; set; }


        private static readonly IBulkMapping<BulkDataExclusion>[] Mappings =
        {
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.Id,
                c => c.DataExclusion.Id.ToBulkString(),
                (v, c) => c.DataExclusion.Id = v.ParseOptional<long>()
                ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.DataExclusion,
                c => c.DataExclusion.Name,
                (v, c) => c.DataExclusion.Name = v
                ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.Description,
                c => c.DataExclusion.Description,
                (v, c) => c.DataExclusion.Description = v
                ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.StartDate,
                c => c.DataExclusion.StartDate.ToDateTimeBulkString(null),
                (v, c) => c.DataExclusion.StartDate = v.ParseOptionalDateTime()
                ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.EndDate,
                c => c.DataExclusion.EndDate.ToDateTimeBulkString(null),
                (v, c) => c.DataExclusion.EndDate = v.ParseOptionalDateTime()
                ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.CampaignType,
                c => c.DataExclusion.CampaignTypeFilter.ToBulkString(),
                (v, c) => c.DataExclusion.CampaignTypeFilter =  v.ParseOptional<CampaignType>()
                ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.DeviceType,
                c => c.DataExclusion.DeviceTypeFilter.ToBulkString(),
                (v, c) => c.DataExclusion.DeviceTypeFilter = v.ParseOptional<DeviceType>()
            ),
            new SimpleBulkMapping<BulkDataExclusion>(StringTable.CampaignAssociations,
                c => c.DataExclusion.CampaignAssociations.WriteCampaignAssociationsToBulkString(),
                (v, c) => c.DataExclusion.CampaignAssociations = v.ParseCampaignAssociations()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            DataExclusion = new DataExclusion { };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(DataExclusion, "DataExclusion");

            this.ConvertToValues(values, Mappings);
        }
    }
}
