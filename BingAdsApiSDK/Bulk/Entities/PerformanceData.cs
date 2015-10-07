using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// Represents a subset of the fields available in bulk records that support historical performance data, for example <see cref="BulkKeyword"/>. 
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>. 
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
