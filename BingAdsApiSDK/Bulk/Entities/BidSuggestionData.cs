using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Entities;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// The best position, main line, and first page bid suggestion data corresponding to one keyword. 
    /// If the requested <see cref="SubmitDownloadParameters.DataScope"/> includes BidSuggestionsData, 
    /// the download will include bulk records corresponding to the properties of this class. 
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{DownloadStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    public class BidSuggestionData
    {
        /// <summary>
        /// Represents a best position bid suggestion that is derived from <see cref="BulkObject"/> and can only be read from a bulk file by the 
        /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
        /// An instance of this class can represent a single best position bid, and thus one record in the bulk file. 
        /// Properties of this class and of classes that it is derived from, correspond to fields of the Keyword Best Position Bid record in a bulk file.
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511557">Keyword Best Position Bid</see>. 
        /// </summary>
        public BulkKeywordBidSuggestion BestPosition { get; internal set; }

        /// <summary>
        /// Represents a main line bid suggestion that is derived from <see cref="BulkObject"/> and can only be read from a bulk file by the 
        /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
        /// An instance of this class can represent a single main line bid, and thus one record in the bulk file. 
        /// Properties of this class and of classes that it is derived from, correspond to fields of the Keyword Main Line Bid record in a bulk file.
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511558">Keyword Main Line Bid</see>. 
        /// </summary>
        public BulkKeywordBidSuggestion MainLine { get; internal set; }

        /// <summary>
        /// Represents a first page bid suggestion that is derived from <see cref="BulkObject"/> and can only be read from a bulk file by the 
        /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
        /// An instance of this class can represent a single first page bid, and thus one record in the bulk file. 
        /// Properties of this class and of classes that it is derived from, correspond to fields of the Keyword First Page Bid record in a bulk file.
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511559">Keyword First Page Bid</see>. 
        /// </summary>
        public BulkKeywordBidSuggestion FirstPage { get; internal set; }
    }
}
