//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.5
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

using Microsoft.BingAds.V12.Internal;
using Microsoft.BingAds.V12.Internal.Bulk;
using Microsoft.BingAds.V12.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V12.Bulk.Entities
{
    /// <summary>
    /// Represents a subset of the fields available in bulk records that support historical performance data, for example <see cref="BulkKeyword"/>. 
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>. 
    /// </summary>
    public class PerformanceData
    {
        /// <summary>
        /// Corresponds to the 'Spend' field in the bulk file. 
        /// </summary>
        public double? Spend { get; private set; }

        /// <summary>
        /// Corresponds to the 'Impressions' field in the bulk file. 
        /// </summary>
        public int? Impressions { get; private set; }

        /// <summary>
        /// Corresponds to the 'Clicks' field in the bulk file. 
        /// </summary>
        public int? Clicks { get; private set; }

        /// <summary>
        /// Corresponds to the 'CTR' field in the bulk file. 
        /// </summary>
        public double? ClickThroughRate { get; private set; }

        /// <summary>
        /// Corresponds to the 'Avg CPC' field in the bulk file. 
        /// </summary>
        public double? AverageCostPerClick { get; private set; }

        /// <summary>
        /// Corresponds to the 'Avg CPM' field in the bulk file. 
        /// </summary>
        public double? AverageCostPerThousandImpressions { get; private set; }

        /// <summary>
        /// Corresponds to the 'Avg position' field in the bulk file. 
        /// </summary>
        public double? AveragePosition { get; private set; }

        /// <summary>
        /// Corresponds to the 'Conversions' field in the bulk file. 
        /// </summary>
        public int? Conversions { get; private set; }

        /// <summary>
        /// Corresponds to the 'CPA' field in the bulk file. 
        /// </summary>
        public double? CostPerConversion { get; private set; }

        private static readonly IBulkMapping<PerformanceData>[] Mappings =
        {
            new SimpleBulkMapping<PerformanceData>(StringTable.Spend,                
                c => c.Spend.ToBulkString(),
                (v, c) => c.Spend = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.Impressions,                
                c => c.Impressions.ToBulkString(),
                (v, c) => c.Impressions = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.Clicks,                
                c => c.Clicks.ToBulkString(),
                (v, c) => c.Clicks = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.CTR,     
                c => c.ClickThroughRate.ToBulkString(),
                (v, c) => c.ClickThroughRate = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.AvgCPC,                
                c => c.AverageCostPerClick.ToBulkString(),
                (v, c) => c.AverageCostPerClick = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.AvgCPM,                
                c => c.AverageCostPerThousandImpressions.ToBulkString(),
                (v, c) => c.AverageCostPerThousandImpressions = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.AvgPosition,                
                c => c.AveragePosition.ToBulkString(),
                (v, c) => c.AveragePosition = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.Conversions,                
                c => c.Conversions.ToBulkString(),
                (v, c) => c.Conversions = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<PerformanceData>(StringTable.CPA,
                c => c.CostPerConversion.ToBulkString(),
                (v, c) => c.CostPerConversion = v.ParseOptional<double>()
            )
        };

        internal static PerformanceData ReadFromRowValuesOrNull(RowValues values)
        {
            var performanceData = new PerformanceData();

            performanceData.ReadFromRowValues(values);

            return performanceData.HasAnyValues ? performanceData : null;            
        }

        internal static void WriteToRowValuesIfNotNull(PerformanceData performanceData, RowValues values)
        {
            if (performanceData != null)
            {
                performanceData.WriteToRowValues(values);
            }
        }

        internal void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        private void WriteToRowValues(RowValues values)
        {
            this.ConvertToValues(values, Mappings);
        }

        private bool HasAnyValues
        {
            get
            {
                return 
                    Spend.HasValue || 
                    Impressions.HasValue || 
                    Clicks.HasValue || 
                    ClickThroughRate.HasValue ||
                    AverageCostPerClick.HasValue || 
                    AverageCostPerThousandImpressions.HasValue ||
                    AveragePosition.HasValue || 
                    Conversions.HasValue || 
                    CostPerConversion.HasValue;
            }
        }

    }
}
