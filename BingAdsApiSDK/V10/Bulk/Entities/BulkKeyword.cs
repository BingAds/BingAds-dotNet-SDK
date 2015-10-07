using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a keyword that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkKeyword.Keyword"/> property that can be read and written as fields of the Keyword record in a bulk file. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Keyword record in a bulk file.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620265">Keyword</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkKeyword : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the ad group that contains the keyword.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId { get; set; }

        /// <summary>
        /// Defines a keyword within an ad group.
        /// </summary>
        public Keyword Keyword { get; set; }

        /// <summary>
        /// The name of the campaign that contains the keyword.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the ad group that contains the keyword.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        /// <summary>
        /// The historical performance data for the keyword.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        /// <summary>
        /// The quality score data for the keyword.
        /// </summary>
        public QualityScoreData QualityScoreData { get; private set; }

        /// <summary>
        /// The bid suggestion data for the keyword.
        /// </summary>
        public BidSuggestionData BidSuggestions { get; private set; }        

        internal override void ReadAdditionalData(IBulkStreamReader reader)
        {
            BulkKeywordBidSuggestion nextBidSuggestion;

            while (reader.TryRead(out nextBidSuggestion))
            {
                if (BidSuggestions == null)
                {
                    BidSuggestions = new BidSuggestionData();
                }

                if (nextBidSuggestion is BulkKeywordBestPositionBid)
                {
                    BidSuggestions.BestPosition = nextBidSuggestion;
                }
                else if (nextBidSuggestion is BulkKeywordMainLineBid)
                {
                    BidSuggestions.MainLine = nextBidSuggestion;
                }
                else if (nextBidSuggestion is BulkKeywordFirstPageBid)
                {
                    BidSuggestions.FirstPage = nextBidSuggestion;
                }
            }            
        }

        internal override void WriteAdditionalData(IBulkObjectWriter writer)
        {
            if (BidSuggestions != null)
            {
                if (BidSuggestions.BestPosition != null)
                {
                    writer.WriteObjectRow(BidSuggestions.BestPosition);
                }

                if (BidSuggestions.MainLine != null)
                {
                    writer.WriteObjectRow(BidSuggestions.MainLine);
                }

                if (BidSuggestions.FirstPage != null)
                {
                    writer.WriteObjectRow(BidSuggestions.FirstPage);
                }
            }
        }

        private static readonly IBulkMapping<BulkKeyword>[] Mappings =
        {
            new SimpleBulkMapping<BulkKeyword>(StringTable.Status,
                c => c.Keyword.Status.ToBulkString(),
                (v, c) => c.Keyword.Status = v.ParseOptional<KeywordStatus>()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.Id,
                c => c.Keyword.Id.ToBulkString(),
                (v, c) => c.Keyword.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.ParentId,
                c => c.AdGroupId.ToBulkString(),
                (v, c) => c.AdGroupId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.Keyword,
                c => c.Keyword.Text,
                (v, c) => c.Keyword.Text = v
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.DestinationUrl,
                c => c.Keyword.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.Keyword.DestinationUrl = v.GetValueOrEmptyString()
            ),
 
            new SimpleBulkMapping<BulkKeyword>(StringTable.EditorialStatus,
                c => c.Keyword.EditorialStatus.ToBulkString(),
                (v, c) => c.Keyword.EditorialStatus = v.ParseOptional<KeywordEditorialStatus>()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.MatchType,
                c => c.Keyword.MatchType.ToBulkString(),
                (v, c) => c.Keyword.MatchType = v.ParseOptional<MatchType>()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.Bid,
                c => c.Keyword.Bid.ToKeywordBidBulkString(),
                (v, c) => c.Keyword.Bid = v.ParseKeywordBid()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.Param1,
                c => c.Keyword.Param1.ToOptionalBulkString(),
                (v, c) => c.Keyword.Param1 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.Param2,
                c => c.Keyword.Param2.ToOptionalBulkString(),
                (v, c) => c.Keyword.Param2 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.Param3,
                c => c.Keyword.Param3.ToOptionalBulkString(),
                (v, c) => c.Keyword.Param3 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.FinalUrl,
                c => c.Keyword.FinalUrls.WriteUrls("; "),
                (v, c) => c.Keyword.FinalUrls = v.ParseUrls()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.FinalMobileUrl,
                c => c.Keyword.FinalMobileUrls.WriteUrls("; "),
                (v, c) => c.Keyword.FinalMobileUrls = v.ParseUrls()
            ), 

            new SimpleBulkMapping<BulkKeyword>(StringTable.TrackingTemplate,
                c => c.Keyword.TrackingUrlTemplate.ToOptionalBulkString(),
                (v, c) => c.Keyword.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.CustomParameter,
                c => c.Keyword.UrlCustomParameters.ToBulkString(),
                (v, c) => c.Keyword.UrlCustomParameters = v.ParseCustomParameters()
            ), 
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Keyword = new Keyword();

            values.ConvertToEntity(this, Mappings);

            QualityScoreData = QualityScoreData.ReadFromRowValuesOrNull(values);

            PerformanceData = PerformanceData.ReadFromRowValuesOrNull(values);            
        }        

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(Keyword, "Keyword");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                QualityScoreData.WriteToRowValuesIfNotNull(QualityScoreData, values);

                PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
            }
        }
    }
}
