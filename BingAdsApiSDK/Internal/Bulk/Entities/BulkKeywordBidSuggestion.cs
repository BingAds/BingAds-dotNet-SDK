using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// Represents a best position bid suggestion that can only be read from a bulk file by the 
    /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
    /// An instance of this class can represent a single keyword bid position, and thus one record in the bulk file. 
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{DownloadStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    public class BulkKeywordBidSuggestion : BulkObject
    {
        /// <summary>
        /// The keyword corresponding to the suggested bid.
        /// Corresponds to the 'Keyword' field in the bulk file.
        /// </summary>
        public string KeywordText { get; private set; }

        /// <summary>
        /// The suggested bid for the keyword.
        /// Corresponds to the 'Bid' field in the bulk file.
        /// </summary>
        public double? Bid { get; private set; }

        /// <summary>
        /// The historical performance data corresponding to the suggested bid.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BulkKeywordBidSuggestion class.
        /// </summary>
        public BulkKeywordBidSuggestion()
        {
            PerformanceData = new PerformanceData();
        }

        private static readonly IBulkMapping<BulkKeywordBidSuggestion>[] Mappings =
        {
            new SimpleBulkMapping<BulkKeywordBidSuggestion>(StringTable.Keyword,
                c => c.KeywordText,
                (v, c) => c.KeywordText = v
            ),

            new SimpleBulkMapping<BulkKeywordBidSuggestion>(StringTable.Bid,
                c => c.Bid.ToBulkString(),
                (v, c) => c.Bid = v.ParseOptional<double>()
            )
        };

        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);

            PerformanceData.ReadFromRowValues(values);
        }

        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);

            PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
        }
    }
}
