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
using MatchType = Microsoft.BingAds.V13.CampaignManagement.MatchType;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a keyword that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkKeyword.Keyword"/> property that can be read and written as fields of the Keyword record in a bulk file. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Keyword record in a bulk file.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Keyword</see>. </para>
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
        /// The quality score data for the keyword.
        /// </summary>
        public QualityScoreData QualityScoreData { get; private set; }

        /// <summary>
        /// The bid suggestion data for the keyword.
        /// </summary>
        public BidSuggestionData BidSuggestions { get; private set; }

        internal override void ReadAdditionalData(IBulkRecordReader reader)
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
                c => c.Keyword.DestinationUrl.ToOptionalBulkString(c.Keyword.Id),
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
                c => c.Keyword.Bid.ToKeywordBidBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.Bid = v.ParseBid()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.Param1,
                c => c.Keyword.Param1.ToOptionalBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.Param1 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.Param2,
                c => c.Keyword.Param2.ToOptionalBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.Param2 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.Param3,
                c => c.Keyword.Param3.ToOptionalBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.Param3 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.FinalUrl,
                c => c.Keyword.FinalUrls.WriteUrls("; ", c.Keyword.Id),
                (v, c) => c.Keyword.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.FinalMobileUrl,
                c => c.Keyword.FinalMobileUrls.WriteUrls("; ", c.Keyword.Id),
                (v, c) => c.Keyword.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.TrackingTemplate,
                c => c.Keyword.TrackingUrlTemplate.ToOptionalBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkKeyword>(StringTable.CustomParameter,
                c => c.Keyword.UrlCustomParameters.ToBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new ComplexBulkMapping<BulkKeyword>(BiddingSchemeToCsv, CsvToBiddingScheme),

            new SimpleBulkMapping<BulkKeyword>(StringTable.FinalUrlSuffix,
                c => c.Keyword.FinalUrlSuffix.ToOptionalBulkString(c.Keyword.Id),
                (v, c) => c.Keyword.FinalUrlSuffix = v.GetValueOrEmptyString()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Keyword = new Keyword();

            values.ConvertToEntity(this, Mappings);

            QualityScoreData = QualityScoreData.ReadFromRowValuesOrNull(values);

        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(Keyword, "Keyword");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                QualityScoreData.WriteToRowValuesIfNotNull(QualityScoreData, values);
            }
        }

        private static void CsvToBiddingScheme(RowValues values, BulkKeyword c)
        {
            string bidStrategyTypeRowValue;

            BiddingScheme biddingScheme;

            if (!values.TryGetValue(StringTable.BidStrategyType, out bidStrategyTypeRowValue) || (biddingScheme = bidStrategyTypeRowValue.ParseBiddingScheme()) == null)
            {
                return;
            }

            string inheritedBidStrategyTypeRowValue;

            values.TryGetValue(StringTable.InheritedBidStrategyType, out inheritedBidStrategyTypeRowValue);

            var inheritFromParentBiddingScheme = biddingScheme as InheritFromParentBiddingScheme;
            if (inheritFromParentBiddingScheme != null)
            {
                c.Keyword.BiddingScheme = new InheritFromParentBiddingScheme
                {
                    InheritedBidStrategyType = inheritedBidStrategyTypeRowValue,
                    Type = "InheritFromParent",
                };
            }
            else
            {
                c.Keyword.BiddingScheme = biddingScheme;
            }
        }

        private static void BiddingSchemeToCsv(BulkKeyword c, RowValues values)
        {
            var biddingScheme = c.Keyword.BiddingScheme;

            if (biddingScheme == null)
            {
                return;
            }

            values[StringTable.BidStrategyType] = biddingScheme.ToBiddingSchemeBulkString();

            var inheritFromParentBiddingScheme = biddingScheme as InheritFromParentBiddingScheme;
            if (inheritFromParentBiddingScheme != null)
            {
                values[StringTable.InheritedBidStrategyType] = inheritFromParentBiddingScheme.InheritedBidStrategyType;
            }
        }
    }
}
