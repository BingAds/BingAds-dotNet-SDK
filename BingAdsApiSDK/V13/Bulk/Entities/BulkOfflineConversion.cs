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

using System;
using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a offline conversion that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkOfflineConversion.OfflineConversion"/> property that can be read and written as fields of the OfflineConversion record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">OfflineConversion</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkOfflineConversion : SingleRecordBulkEntity
    {
        /// <summary>
        /// The offline conversion.
        /// </summary>
        public OfflineConversion OfflineConversion { get; set; }

        /// <summary>
        /// The offline conversion adjustment value.
        /// Corresponds to the 'Adjustment Value' field in the bulk file. 
        /// </summary>
        public double? AdjustmentValue { get; set; }

        /// <summary>
        /// The offline conversion adjustment time.
        /// Corresponds to the 'Adjustment Time' field in the bulk file. 
        /// </summary>
        public DateTime? AdjustmentTime { get; set; }

        /// <summary>
        /// The offline conversion adjustment currency code.
        /// Corresponds to the 'Adjustment Currency Code' field in the bulk file. 
        /// </summary>
        public string AdjustmentCurrencyCode { get; set; }

        /// <summary>
        /// The offline conversion adjustment type.
        /// Corresponds to the 'Adjustment Type' field in the bulk file. 
        /// </summary>
        public string AdjustmentType { get; set; }

        /// <summary>
        /// The offline conversion External Attribution Model.
        /// Corresponds to the 'External Attribution Model' field in the bulk file. 
        /// </summary>
        public string ExternalAttributionModel { get; set; }

        /// <summary>
        /// The offline conversion External Attribution Credit.
        /// Corresponds to the 'External Attribution Credit' field in the bulk file. 
        /// </summary>
        public double? ExternalAttributionCredit { get; set; }
          

        private static readonly IBulkMapping<BulkOfflineConversion>[] Mappings =
        {
            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.ConversionCurrencyCode,
                c => c.OfflineConversion.ConversionCurrencyCode,
                (v, c) => c.OfflineConversion.ConversionCurrencyCode = v
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.ConversionName,
                c => c.OfflineConversion.ConversionName,
                (v, c) => c.OfflineConversion.ConversionName = v
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.ConversionTime,
                c => c.OfflineConversion.ConversionTime.ToBulkString(),
                (v, c) =>c.OfflineConversion.ConversionTime = v.ParseDateTime()
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.ConversionValue,
                c => c.OfflineConversion.ConversionValue.ToBulkString(),
                (v, c) => c.OfflineConversion.ConversionValue = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.MicrosoftClickId,
                c => c.OfflineConversion.MicrosoftClickId,
                (v, c) => c.OfflineConversion.MicrosoftClickId = v
            ),
            
            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.AdjustmentValue,
                c => c.AdjustmentValue.ToBulkString(),
                (v, c) => c.AdjustmentValue = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.AdjustmentTime,
                c => c.AdjustmentTime.ToDateTimeBulkString(null),
                (v, c) => c.AdjustmentTime = v.ParseDateTime()
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.AdjustmentCurrencyCode,
                c => c.AdjustmentCurrencyCode,
                (v, c) => c.AdjustmentCurrencyCode = v
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.AdjustmentType,
                c => c.AdjustmentType,
                (v, c) => c.AdjustmentType = v
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.ExternalAttributionModel,
                c => c.ExternalAttributionModel,
                (v, c) => c.ExternalAttributionModel = v
            ),

            new SimpleBulkMapping<BulkOfflineConversion>(StringTable.ExternalAttributionCredit,
                c => c.ExternalAttributionCredit.ToBulkString(),
                (v, c) => c.ExternalAttributionCredit = v.ParseOptional<double>()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            OfflineConversion = new OfflineConversion { };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(OfflineConversion, "OfflineConversion");

            this.ConvertToValues(values, Mappings);
        }
    }
}
