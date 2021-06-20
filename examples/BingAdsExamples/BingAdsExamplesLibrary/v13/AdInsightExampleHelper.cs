using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.AdInsight;

namespace BingAdsExamplesLibrary.V13
{
    public class AdInsightExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | AdInsight V13"; }
        }
        public ServiceClient<IAdInsightService> AdInsightService;
        public AdInsightExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }
        public async Task<GetAuctionInsightDataResponse> GetAuctionInsightDataAsync(
            EntityType entityType,
            IList<long> entityIds,
            IList<SearchParameter> searchParameters,
            AuctionInsightKpiAdditionalField? returnAdditionalFields)
        {
            var request = new GetAuctionInsightDataRequest
            {
                EntityType = entityType,
                EntityIds = entityIds,
                SearchParameters = searchParameters,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetAuctionInsightDataAsync(r), request));
        }
        public async Task<GetBidLandscapeByAdGroupIdsResponse> GetBidLandscapeByAdGroupIdsAsync(
            IList<AdGroupBidLandscapeInput> adGroupBidLandscapeInputs)
        {
            var request = new GetBidLandscapeByAdGroupIdsRequest
            {
                AdGroupBidLandscapeInputs = adGroupBidLandscapeInputs
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetBidLandscapeByAdGroupIdsAsync(r), request));
        }
        public async Task<GetBidLandscapeByKeywordIdsResponse> GetBidLandscapeByKeywordIdsAsync(
            IList<long> keywordIds,
            bool? includeCurrentBid)
        {
            var request = new GetBidLandscapeByKeywordIdsRequest
            {
                KeywordIds = keywordIds,
                IncludeCurrentBid = includeCurrentBid
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetBidLandscapeByKeywordIdsAsync(r), request));
        }
        public async Task<GetBidOpportunitiesResponse> GetBidOpportunitiesAsync(
            long? adGroupId,
            long? campaignId,
            BidOpportunityType opportunityType)
        {
            var request = new GetBidOpportunitiesRequest
            {
                AdGroupId = adGroupId,
                CampaignId = campaignId,
                OpportunityType = opportunityType
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetBidOpportunitiesAsync(r), request));
        }
        public async Task<GetBudgetOpportunitiesResponse> GetBudgetOpportunitiesAsync(
            long? campaignId)
        {
            var request = new GetBudgetOpportunitiesRequest
            {
                CampaignId = campaignId
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetBudgetOpportunitiesAsync(r), request));
        }
        public async Task<GetDomainCategoriesResponse> GetDomainCategoriesAsync(
            String categoryName,
            String domainName,
            String language)
        {
            var request = new GetDomainCategoriesRequest
            {
                CategoryName = categoryName,
                DomainName = domainName,
                Language = language
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetDomainCategoriesAsync(r), request));
        }
        public async Task<GetEstimatedBidByKeywordIdsResponse> GetEstimatedBidByKeywordIdsAsync(
            IList<long> keywordIds,
            TargetAdPosition targetPositionForAds)
        {
            var request = new GetEstimatedBidByKeywordIdsRequest
            {
                KeywordIds = keywordIds,
                TargetPositionForAds = targetPositionForAds
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetEstimatedBidByKeywordIdsAsync(r), request));
        }
        public async Task<GetEstimatedBidByKeywordsResponse> GetEstimatedBidByKeywordsAsync(
            IList<KeywordAndMatchType> keywords,
            TargetAdPosition targetPositionForAds,
            String language,
            IList<long> locationIds,
            CurrencyCode? currencyCode,
            long? campaignId,
            long? adGroupId,
            String entityLevelBid)
        {
            var request = new GetEstimatedBidByKeywordsRequest
            {
                Keywords = keywords,
                TargetPositionForAds = targetPositionForAds,
                Language = language,
                LocationIds = locationIds,
                CurrencyCode = currencyCode,
                CampaignId = campaignId,
                AdGroupId = adGroupId,
                EntityLevelBid = entityLevelBid
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetEstimatedBidByKeywordsAsync(r), request));
        }
        public async Task<GetEstimatedPositionByKeywordIdsResponse> GetEstimatedPositionByKeywordIdsAsync(
            IList<long> keywordIds,
            double maxBid)
        {
            var request = new GetEstimatedPositionByKeywordIdsRequest
            {
                KeywordIds = keywordIds,
                MaxBid = maxBid
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetEstimatedPositionByKeywordIdsAsync(r), request));
        }
        public async Task<GetEstimatedPositionByKeywordsResponse> GetEstimatedPositionByKeywordsAsync(
            IList<String> keywords,
            double maxBid,
            String language,
            IList<long> locationIds,
            CurrencyCode? currencyCode,
            IList<MatchType> matchTypes,
            long? campaignId,
            long? adGroupId)
        {
            var request = new GetEstimatedPositionByKeywordsRequest
            {
                Keywords = keywords,
                MaxBid = maxBid,
                Language = language,
                LocationIds = locationIds,
                CurrencyCode = currencyCode,
                MatchTypes = matchTypes,
                CampaignId = campaignId,
                AdGroupId = adGroupId
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetEstimatedPositionByKeywordsAsync(r), request));
        }
        public async Task<GetHistoricalKeywordPerformanceResponse> GetHistoricalKeywordPerformanceAsync(
            IList<String> keywords,
            TimeInterval? timeInterval,
            AdPosition? targetAdPosition,
            IList<MatchType> matchTypes,
            String language,
            IList<String> publisherCountries,
            IList<String> devices)
        {
            var request = new GetHistoricalKeywordPerformanceRequest
            {
                Keywords = keywords,
                TimeInterval = timeInterval,
                TargetAdPosition = targetAdPosition,
                MatchTypes = matchTypes,
                Language = language,
                PublisherCountries = publisherCountries,
                Devices = devices
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetHistoricalKeywordPerformanceAsync(r), request));
        }
        public async Task<GetHistoricalSearchCountResponse> GetHistoricalSearchCountAsync(
            IList<String> keywords,
            String language,
            IList<String> publisherCountries,
            DayMonthAndYear startDate,
            DayMonthAndYear endDate,
            String timePeriodRollup,
            IList<String> devices)
        {
            var request = new GetHistoricalSearchCountRequest
            {
                Keywords = keywords,
                Language = language,
                PublisherCountries = publisherCountries,
                StartDate = startDate,
                EndDate = endDate,
                TimePeriodRollup = timePeriodRollup,
                Devices = devices
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetHistoricalSearchCountAsync(r), request));
        }
        public async Task<GetKeywordCategoriesResponse> GetKeywordCategoriesAsync(
            IList<String> keywords,
            String language,
            String publisherCountry,
            int? maxCategories)
        {
            var request = new GetKeywordCategoriesRequest
            {
                Keywords = keywords,
                Language = language,
                PublisherCountry = publisherCountry,
                MaxCategories = maxCategories
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordCategoriesAsync(r), request));
        }
        public async Task<GetKeywordDemographicsResponse> GetKeywordDemographicsAsync(
            IList<String> keywords,
            String language,
            String publisherCountry,
            IList<String> device)
        {
            var request = new GetKeywordDemographicsRequest
            {
                Keywords = keywords,
                Language = language,
                PublisherCountry = publisherCountry,
                Device = device
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordDemographicsAsync(r), request));
        }
        public async Task<GetKeywordIdeaCategoriesResponse> GetKeywordIdeaCategoriesAsync()
        {
            var request = new GetKeywordIdeaCategoriesRequest
            {
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordIdeaCategoriesAsync(r), request));
        }
        public async Task<GetKeywordIdeasResponse> GetKeywordIdeasAsync(
            bool? expandIdeas,
            IList<KeywordIdeaAttribute> ideaAttributes,
            IList<SearchParameter> searchParameters)
        {
            var request = new GetKeywordIdeasRequest
            {
                ExpandIdeas = expandIdeas,
                IdeaAttributes = ideaAttributes,
                SearchParameters = searchParameters
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordIdeasAsync(r), request));
        }
        public async Task<GetKeywordLocationsResponse> GetKeywordLocationsAsync(
            IList<String> keywords,
            String language,
            String publisherCountry,
            IList<String> device,
            int? level,
            String parentCountry,
            int? maxLocations)
        {
            var request = new GetKeywordLocationsRequest
            {
                Keywords = keywords,
                Language = language,
                PublisherCountry = publisherCountry,
                Device = device,
                Level = level,
                ParentCountry = parentCountry,
                MaxLocations = maxLocations
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordLocationsAsync(r), request));
        }
        public async Task<GetKeywordOpportunitiesResponse> GetKeywordOpportunitiesAsync(
            long? adGroupId,
            long? campaignId,
            KeywordOpportunityType opportunityType)
        {
            var request = new GetKeywordOpportunitiesRequest
            {
                AdGroupId = adGroupId,
                CampaignId = campaignId,
                OpportunityType = opportunityType
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordOpportunitiesAsync(r), request));
        }
        public async Task<GetKeywordTrafficEstimatesResponse> GetKeywordTrafficEstimatesAsync(
            IList<CampaignEstimator> campaignEstimators)
        {
            var request = new GetKeywordTrafficEstimatesRequest
            {
                CampaignEstimators = campaignEstimators
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordTrafficEstimatesAsync(r), request));
        }
        public async Task<SuggestKeywordsForUrlResponse> SuggestKeywordsForUrlAsync(
            String url,
            String language,
            int? maxKeywords,
            double? minConfidenceScore,
            bool? excludeBrand)
        {
            var request = new SuggestKeywordsForUrlRequest
            {
                Url = url,
                Language = language,
                MaxKeywords = maxKeywords,
                MinConfidenceScore = minConfidenceScore,
                ExcludeBrand = excludeBrand
            };

            return (await AdInsightService.CallAsync((s, r) => s.SuggestKeywordsForUrlAsync(r), request));
        }
        public async Task<SuggestKeywordsFromExistingKeywordsResponse> SuggestKeywordsFromExistingKeywordsAsync(
            IList<String> keywords,
            String language,
            IList<String> publisherCountries,
            int? maxSuggestionsPerKeyword,
            int? suggestionType,
            bool? removeDuplicates,
            bool? excludeBrand,
            long? adGroupId,
            long? campaignId)
        {
            var request = new SuggestKeywordsFromExistingKeywordsRequest
            {
                Keywords = keywords,
                Language = language,
                PublisherCountries = publisherCountries,
                MaxSuggestionsPerKeyword = maxSuggestionsPerKeyword,
                SuggestionType = suggestionType,
                RemoveDuplicates = removeDuplicates,
                ExcludeBrand = excludeBrand,
                AdGroupId = adGroupId,
                CampaignId = campaignId
            };

            return (await AdInsightService.CallAsync((s, r) => s.SuggestKeywordsFromExistingKeywordsAsync(r), request));
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputAdApiError * * *");
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiError(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiFaultDetail * * *");
                OutputStatusMessage("Errors:");
                OutputArrayOfAdApiError(dataObject.Errors);
                OutputStatusMessage("* * * End OutputAdApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupBidLandscape(AdGroupBidLandscape dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupBidLandscape * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupBidLandscapeType: {0}", dataObject.AdGroupBidLandscapeType));
                OutputStatusMessage("StartDate:");
                OutputDayMonthAndYear(dataObject.StartDate);
                OutputStatusMessage("EndDate:");
                OutputDayMonthAndYear(dataObject.EndDate);
                OutputStatusMessage("BidLandscapePoints:");
                OutputArrayOfBidLandscapePoint(dataObject.BidLandscapePoints);
                OutputStatusMessage("* * * End OutputAdGroupBidLandscape * * *");
            }
        }
        public void OutputArrayOfAdGroupBidLandscape(IList<AdGroupBidLandscape> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupBidLandscape(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupBidLandscapeInput(AdGroupBidLandscapeInput dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupBidLandscapeInput * * *");
                OutputStatusMessage(string.Format("AdGroupBidLandscapeType: {0}", dataObject.AdGroupBidLandscapeType));
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage("* * * End OutputAdGroupBidLandscapeInput * * *");
            }
        }
        public void OutputArrayOfAdGroupBidLandscapeInput(IList<AdGroupBidLandscapeInput> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupBidLandscapeInput(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupEstimate(AdGroupEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupEstimate * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage("KeywordEstimates:");
                OutputArrayOfKeywordEstimate(dataObject.KeywordEstimates);
                OutputStatusMessage("* * * End OutputAdGroupEstimate * * *");
            }
        }
        public void OutputArrayOfAdGroupEstimate(IList<AdGroupEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupEstimate(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupEstimator(AdGroupEstimator dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupEstimator * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage("KeywordEstimators:");
                OutputArrayOfKeywordEstimator(dataObject.KeywordEstimators);
                OutputStatusMessage(string.Format("MaxCpc: {0}", dataObject.MaxCpc));
                OutputStatusMessage("* * * End OutputAdGroupEstimator * * *");
            }
        }
        public void OutputArrayOfAdGroupEstimator(IList<AdGroupEstimator> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupEstimator(dataObject);
                    }
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiFaultDetail * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                OutputStatusMessage("* * * End OutputApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApplicationFault * * *");
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(null != adapifaultdetail)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(null != apifaultdetail)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
                OutputStatusMessage("* * * End OutputApplicationFault * * *");
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApplicationFault(dataObject);
                    }
                }
            }
        }
        public void OutputAuctionInsightEntry(AuctionInsightEntry dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAuctionInsightEntry * * *");
                OutputStatusMessage(string.Format("DisplayDomain: {0}", dataObject.DisplayDomain));
                OutputStatusMessage("AggregatedKpi:");
                OutputAuctionInsightKpi(dataObject.AggregatedKpi);
                OutputStatusMessage("SegmentedKpis:");
                OutputArrayOfAuctionInsightKpi(dataObject.SegmentedKpis);
                OutputStatusMessage("* * * End OutputAuctionInsightEntry * * *");
            }
        }
        public void OutputArrayOfAuctionInsightEntry(IList<AuctionInsightEntry> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAuctionInsightEntry(dataObject);
                    }
                }
            }
        }
        public void OutputAuctionInsightKpi(AuctionInsightKpi dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAuctionInsightKpi * * *");
                OutputStatusMessage("Segments:");
                OutputArrayOfString(dataObject.Segments);
                OutputStatusMessage(string.Format("ImpressionShare: {0}", dataObject.ImpressionShare));
                OutputStatusMessage(string.Format("OverlapRate: {0}", dataObject.OverlapRate));
                OutputStatusMessage(string.Format("AveragePosition: {0}", dataObject.AveragePosition));
                OutputStatusMessage(string.Format("AboveRate: {0}", dataObject.AboveRate));
                OutputStatusMessage(string.Format("TopOfPageRate: {0}", dataObject.TopOfPageRate));
                OutputStatusMessage(string.Format("OutrankingShare: {0}", dataObject.OutrankingShare));
                OutputStatusMessage(string.Format("AbsoluteTopOfPageRate: {0}", dataObject.AbsoluteTopOfPageRate));
                OutputStatusMessage("* * * End OutputAuctionInsightKpi * * *");
            }
        }
        public void OutputArrayOfAuctionInsightKpi(IList<AuctionInsightKpi> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAuctionInsightKpi(dataObject);
                    }
                }
            }
        }
        public void OutputAuctionInsightResult(AuctionInsightResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAuctionInsightResult * * *");
                OutputStatusMessage("Segments:");
                OutputArrayOfAuctionSegment(dataObject.Segments);
                OutputStatusMessage("Entries:");
                OutputArrayOfAuctionInsightEntry(dataObject.Entries);
                OutputStatusMessage(string.Format("UsedImpressions: {0}", dataObject.UsedImpressions));
                OutputStatusMessage(string.Format("UsedKeywords: {0}", dataObject.UsedKeywords));
                OutputStatusMessage("* * * End OutputAuctionInsightResult * * *");
            }
        }
        public void OutputArrayOfAuctionInsightResult(IList<AuctionInsightResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAuctionInsightResult(dataObject);
                    }
                }
            }
        }
        public void OutputAuctionSegmentSearchParameter(AuctionSegmentSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAuctionSegmentSearchParameter * * *");
                OutputStatusMessage(string.Format("Segment: {0}", dataObject.Segment));
                OutputStatusMessage("* * * End OutputAuctionSegmentSearchParameter * * *");
            }
        }
        public void OutputArrayOfAuctionSegmentSearchParameter(IList<AuctionSegmentSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAuctionSegmentSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBatchError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputBatchError * * *");
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBatchError(dataObject);
                    }
                }
            }
        }
        public void OutputBidLandscapePoint(BidLandscapePoint dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBidLandscapePoint * * *");
                OutputStatusMessage(string.Format("Bid: {0}", dataObject.Bid));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("TopImpressions: {0}", dataObject.TopImpressions));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("Cost: {0}", dataObject.Cost));
                OutputStatusMessage(string.Format("MarginalCPC: {0}", dataObject.MarginalCPC));
                OutputStatusMessage("* * * End OutputBidLandscapePoint * * *");
            }
        }
        public void OutputArrayOfBidLandscapePoint(IList<BidLandscapePoint> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBidLandscapePoint(dataObject);
                    }
                }
            }
        }
        public void OutputBidOpportunity(BidOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBidOpportunity * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("CurrentBid: {0}", dataObject.CurrentBid));
                OutputStatusMessage(string.Format("EstimatedIncreaseInClicks: {0}", dataObject.EstimatedIncreaseInClicks));
                OutputStatusMessage(string.Format("EstimatedIncreaseInCost: {0}", dataObject.EstimatedIncreaseInCost));
                OutputStatusMessage(string.Format("EstimatedIncreaseInImpressions: {0}", dataObject.EstimatedIncreaseInImpressions));
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("SuggestedBid: {0}", dataObject.SuggestedBid));
                OutputStatusMessage("* * * End OutputBidOpportunity * * *");
            }
        }
        public void OutputArrayOfBidOpportunity(IList<BidOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBidOpportunity(dataObject);
                    }
                }
            }
        }
        public void OutputBroadMatchKeywordOpportunity(BroadMatchKeywordOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBroadMatchKeywordOpportunity * * *");
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("AverageCTR: {0}", dataObject.AverageCTR));
                OutputStatusMessage(string.Format("ClickShare: {0}", dataObject.ClickShare));
                OutputStatusMessage(string.Format("ImpressionShare: {0}", dataObject.ImpressionShare));
                OutputStatusMessage(string.Format("ReferenceKeywordBid: {0}", dataObject.ReferenceKeywordBid));
                OutputStatusMessage(string.Format("ReferenceKeywordId: {0}", dataObject.ReferenceKeywordId));
                OutputStatusMessage(string.Format("ReferenceKeywordMatchType: {0}", dataObject.ReferenceKeywordMatchType));
                OutputStatusMessage("SearchQueryKPIs:");
                OutputArrayOfBroadMatchSearchQueryKPI(dataObject.SearchQueryKPIs);
                OutputStatusMessage("* * * End OutputBroadMatchKeywordOpportunity * * *");
            }
        }
        public void OutputArrayOfBroadMatchKeywordOpportunity(IList<BroadMatchKeywordOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBroadMatchKeywordOpportunity(dataObject);
                    }
                }
            }
        }
        public void OutputBroadMatchSearchQueryKPI(BroadMatchSearchQueryKPI dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBroadMatchSearchQueryKPI * * *");
                OutputStatusMessage(string.Format("AverageCTR: {0}", dataObject.AverageCTR));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("SRPV: {0}", dataObject.SRPV));
                OutputStatusMessage(string.Format("SearchQuery: {0}", dataObject.SearchQuery));
                OutputStatusMessage("* * * End OutputBroadMatchSearchQueryKPI * * *");
            }
        }
        public void OutputArrayOfBroadMatchSearchQueryKPI(IList<BroadMatchSearchQueryKPI> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBroadMatchSearchQueryKPI(dataObject);
                    }
                }
            }
        }
        public void OutputBudgetOpportunity(BudgetOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBudgetOpportunity * * *");
                OutputStatusMessage("BudgetPoints:");
                OutputArrayOfBudgetPoint(dataObject.BudgetPoints);
                OutputStatusMessage(string.Format("BudgetType: {0}", dataObject.BudgetType));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("CurrentBudget: {0}", dataObject.CurrentBudget));
                OutputStatusMessage(string.Format("IncreaseInClicks: {0}", dataObject.IncreaseInClicks));
                OutputStatusMessage(string.Format("IncreaseInImpressions: {0}", dataObject.IncreaseInImpressions));
                OutputStatusMessage(string.Format("PercentageIncreaseInClicks: {0}", dataObject.PercentageIncreaseInClicks));
                OutputStatusMessage(string.Format("PercentageIncreaseInImpressions: {0}", dataObject.PercentageIncreaseInImpressions));
                OutputStatusMessage(string.Format("RecommendedBudget: {0}", dataObject.RecommendedBudget));
                OutputStatusMessage("* * * End OutputBudgetOpportunity * * *");
            }
        }
        public void OutputArrayOfBudgetOpportunity(IList<BudgetOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBudgetOpportunity(dataObject);
                    }
                }
            }
        }
        public void OutputBudgetPoint(BudgetPoint dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBudgetPoint * * *");
                OutputStatusMessage(string.Format("BudgetAmount: {0}", dataObject.BudgetAmount));
                OutputStatusMessage(string.Format("BudgetPointType: {0}", dataObject.BudgetPointType));
                OutputStatusMessage(string.Format("EstimatedWeeklyClicks: {0}", dataObject.EstimatedWeeklyClicks));
                OutputStatusMessage(string.Format("EstimatedWeeklyCost: {0}", dataObject.EstimatedWeeklyCost));
                OutputStatusMessage(string.Format("EstimatedWeeklyImpressions: {0}", dataObject.EstimatedWeeklyImpressions));
                OutputStatusMessage("* * * End OutputBudgetPoint * * *");
            }
        }
        public void OutputArrayOfBudgetPoint(IList<BudgetPoint> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBudgetPoint(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignEstimate(CampaignEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignEstimate * * *");
                OutputStatusMessage("AdGroupEstimates:");
                OutputArrayOfAdGroupEstimate(dataObject.AdGroupEstimates);
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage("* * * End OutputCampaignEstimate * * *");
            }
        }
        public void OutputArrayOfCampaignEstimate(IList<CampaignEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignEstimate(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignEstimator(CampaignEstimator dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignEstimator * * *");
                OutputStatusMessage("AdGroupEstimators:");
                OutputArrayOfAdGroupEstimator(dataObject.AdGroupEstimators);
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage("Criteria:");
                OutputArrayOfCriterion(dataObject.Criteria);
                OutputStatusMessage(string.Format("DailyBudget: {0}", dataObject.DailyBudget));
                OutputStatusMessage("NegativeKeywords:");
                OutputArrayOfNegativeKeyword(dataObject.NegativeKeywords);
                OutputStatusMessage("* * * End OutputCampaignEstimator * * *");
            }
        }
        public void OutputArrayOfCampaignEstimator(IList<CampaignEstimator> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignEstimator(dataObject);
                    }
                }
            }
        }
        public void OutputCategorySearchParameter(CategorySearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCategorySearchParameter * * *");
                OutputStatusMessage(string.Format("CategoryId: {0}", dataObject.CategoryId));
                OutputStatusMessage("* * * End OutputCategorySearchParameter * * *");
            }
        }
        public void OutputArrayOfCategorySearchParameter(IList<CategorySearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCategorySearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputCompetitionSearchParameter(CompetitionSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCompetitionSearchParameter * * *");
                OutputStatusMessage("CompetitionLevels:");
                OutputArrayOfCompetitionLevel(dataObject.CompetitionLevels);
                OutputStatusMessage("* * * End OutputCompetitionSearchParameter * * *");
            }
        }
        public void OutputArrayOfCompetitionSearchParameter(IList<CompetitionSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCompetitionSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputCriterion(Criterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCriterion * * *");
                var devicecriterion = dataObject as DeviceCriterion;
                if(null != devicecriterion)
                {
                    OutputDeviceCriterion((DeviceCriterion)dataObject);
                }
                var languagecriterion = dataObject as LanguageCriterion;
                if(null != languagecriterion)
                {
                    OutputLanguageCriterion((LanguageCriterion)dataObject);
                }
                var locationcriterion = dataObject as LocationCriterion;
                if(null != locationcriterion)
                {
                    OutputLocationCriterion((LocationCriterion)dataObject);
                }
                var networkcriterion = dataObject as NetworkCriterion;
                if(null != networkcriterion)
                {
                    OutputNetworkCriterion((NetworkCriterion)dataObject);
                }
                OutputStatusMessage("* * * End OutputCriterion * * *");
            }
        }
        public void OutputArrayOfCriterion(IList<Criterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputDateRangeSearchParameter(DateRangeSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDateRangeSearchParameter * * *");
                OutputStatusMessage("EndDate:");
                OutputDayMonthAndYear(dataObject.EndDate);
                OutputStatusMessage("StartDate:");
                OutputDayMonthAndYear(dataObject.StartDate);
                OutputStatusMessage("* * * End OutputDateRangeSearchParameter * * *");
            }
        }
        public void OutputArrayOfDateRangeSearchParameter(IList<DateRangeSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDateRangeSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputDayMonthAndYear(DayMonthAndYear dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDayMonthAndYear * * *");
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
                OutputStatusMessage("* * * End OutputDayMonthAndYear * * *");
            }
        }
        public void OutputArrayOfDayMonthAndYear(IList<DayMonthAndYear> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDayMonthAndYear(dataObject);
                    }
                }
            }
        }
        public void OutputDeviceCriterion(DeviceCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDeviceCriterion * * *");
                OutputStatusMessage(string.Format("DeviceName: {0}", dataObject.DeviceName));
                OutputStatusMessage("* * * End OutputDeviceCriterion * * *");
            }
        }
        public void OutputArrayOfDeviceCriterion(IList<DeviceCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDeviceCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputDeviceSearchParameter(DeviceSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDeviceSearchParameter * * *");
                OutputStatusMessage("Device:");
                OutputDeviceCriterion(dataObject.Device);
                OutputStatusMessage("* * * End OutputDeviceSearchParameter * * *");
            }
        }
        public void OutputArrayOfDeviceSearchParameter(IList<DeviceSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDeviceSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputDomainCategory(DomainCategory dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDomainCategory * * *");
                OutputStatusMessage(string.Format("Bid: {0}", dataObject.Bid));
                OutputStatusMessage(string.Format("CategoryName: {0}", dataObject.CategoryName));
                OutputStatusMessage(string.Format("Coverage: {0}", dataObject.Coverage));
                OutputStatusMessage("* * * End OutputDomainCategory * * *");
            }
        }
        public void OutputArrayOfDomainCategory(IList<DomainCategory> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDomainCategory(dataObject);
                    }
                }
            }
        }
        public void OutputEstimatedBidAndTraffic(EstimatedBidAndTraffic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEstimatedBidAndTraffic * * *");
                OutputStatusMessage(string.Format("MinClicksPerWeek: {0}", dataObject.MinClicksPerWeek));
                OutputStatusMessage(string.Format("MaxClicksPerWeek: {0}", dataObject.MaxClicksPerWeek));
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("MinImpressionsPerWeek: {0}", dataObject.MinImpressionsPerWeek));
                OutputStatusMessage(string.Format("MaxImpressionsPerWeek: {0}", dataObject.MaxImpressionsPerWeek));
                OutputStatusMessage(string.Format("CTR: {0}", dataObject.CTR));
                OutputStatusMessage(string.Format("MinTotalCostPerWeek: {0}", dataObject.MinTotalCostPerWeek));
                OutputStatusMessage(string.Format("MaxTotalCostPerWeek: {0}", dataObject.MaxTotalCostPerWeek));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("EstimatedMinBid: {0}", dataObject.EstimatedMinBid));
                OutputStatusMessage("* * * End OutputEstimatedBidAndTraffic * * *");
            }
        }
        public void OutputArrayOfEstimatedBidAndTraffic(IList<EstimatedBidAndTraffic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEstimatedBidAndTraffic(dataObject);
                    }
                }
            }
        }
        public void OutputEstimatedPositionAndTraffic(EstimatedPositionAndTraffic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEstimatedPositionAndTraffic * * *");
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("MinClicksPerWeek: {0}", dataObject.MinClicksPerWeek));
                OutputStatusMessage(string.Format("MaxClicksPerWeek: {0}", dataObject.MaxClicksPerWeek));
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("MinImpressionsPerWeek: {0}", dataObject.MinImpressionsPerWeek));
                OutputStatusMessage(string.Format("MaxImpressionsPerWeek: {0}", dataObject.MaxImpressionsPerWeek));
                OutputStatusMessage(string.Format("CTR: {0}", dataObject.CTR));
                OutputStatusMessage(string.Format("MinTotalCostPerWeek: {0}", dataObject.MinTotalCostPerWeek));
                OutputStatusMessage(string.Format("MaxTotalCostPerWeek: {0}", dataObject.MaxTotalCostPerWeek));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("EstimatedAdPosition: {0}", dataObject.EstimatedAdPosition));
                OutputStatusMessage("* * * End OutputEstimatedPositionAndTraffic * * *");
            }
        }
        public void OutputArrayOfEstimatedPositionAndTraffic(IList<EstimatedPositionAndTraffic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEstimatedPositionAndTraffic(dataObject);
                    }
                }
            }
        }
        public void OutputExcludeAccountKeywordsSearchParameter(ExcludeAccountKeywordsSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputExcludeAccountKeywordsSearchParameter * * *");
                OutputStatusMessage(string.Format("ExcludeAccountKeywords: {0}", dataObject.ExcludeAccountKeywords));
                OutputStatusMessage("* * * End OutputExcludeAccountKeywordsSearchParameter * * *");
            }
        }
        public void OutputArrayOfExcludeAccountKeywordsSearchParameter(IList<ExcludeAccountKeywordsSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputExcludeAccountKeywordsSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputHistoricalSearchCountPeriodic(HistoricalSearchCountPeriodic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputHistoricalSearchCountPeriodic * * *");
                OutputStatusMessage(string.Format("SearchCount: {0}", dataObject.SearchCount));
                OutputStatusMessage("DayMonthAndYear:");
                OutputDayMonthAndYear(dataObject.DayMonthAndYear);
                OutputStatusMessage("* * * End OutputHistoricalSearchCountPeriodic * * *");
            }
        }
        public void OutputArrayOfHistoricalSearchCountPeriodic(IList<HistoricalSearchCountPeriodic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputHistoricalSearchCountPeriodic(dataObject);
                    }
                }
            }
        }
        public void OutputIdeaTextSearchParameter(IdeaTextSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputIdeaTextSearchParameter * * *");
                OutputStatusMessage("Excluded:");
                OutputArrayOfKeyword(dataObject.Excluded);
                OutputStatusMessage("Included:");
                OutputArrayOfKeyword(dataObject.Included);
                OutputStatusMessage("* * * End OutputIdeaTextSearchParameter * * *");
            }
        }
        public void OutputArrayOfIdeaTextSearchParameter(IList<IdeaTextSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputIdeaTextSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputImpressionShareSearchParameter(ImpressionShareSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImpressionShareSearchParameter * * *");
                OutputStatusMessage(string.Format("Maximum: {0}", dataObject.Maximum));
                OutputStatusMessage(string.Format("Minimum: {0}", dataObject.Minimum));
                OutputStatusMessage("* * * End OutputImpressionShareSearchParameter * * *");
            }
        }
        public void OutputArrayOfImpressionShareSearchParameter(IList<ImpressionShareSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImpressionShareSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputKeyword(Keyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeyword * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage("* * * End OutputKeyword * * *");
            }
        }
        public void OutputArrayOfKeyword(IList<Keyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeyword(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordAndConfidence(KeywordAndConfidence dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordAndConfidence * * *");
                OutputStatusMessage(string.Format("SuggestedKeyword: {0}", dataObject.SuggestedKeyword));
                OutputStatusMessage(string.Format("ConfidenceScore: {0}", dataObject.ConfidenceScore));
                OutputStatusMessage("* * * End OutputKeywordAndConfidence * * *");
            }
        }
        public void OutputArrayOfKeywordAndConfidence(IList<KeywordAndConfidence> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordAndConfidence(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordAndMatchType(KeywordAndMatchType dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordAndMatchType * * *");
                OutputStatusMessage(string.Format("KeywordText: {0}", dataObject.KeywordText));
                OutputStatusMessage("MatchTypes:");
                OutputArrayOfMatchType(dataObject.MatchTypes);
                OutputStatusMessage("* * * End OutputKeywordAndMatchType * * *");
            }
        }
        public void OutputArrayOfKeywordAndMatchType(IList<KeywordAndMatchType> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordAndMatchType(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordBidLandscape(KeywordBidLandscape dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordBidLandscape * * *");
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputStatusMessage("StartDate:");
                OutputDayMonthAndYear(dataObject.StartDate);
                OutputStatusMessage("EndDate:");
                OutputDayMonthAndYear(dataObject.EndDate);
                OutputStatusMessage("BidLandscapePoints:");
                OutputArrayOfBidLandscapePoint(dataObject.BidLandscapePoints);
                OutputStatusMessage("* * * End OutputKeywordBidLandscape * * *");
            }
        }
        public void OutputArrayOfKeywordBidLandscape(IList<KeywordBidLandscape> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordBidLandscape(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordCategory(KeywordCategory dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordCategory * * *");
                OutputStatusMessage(string.Format("Category: {0}", dataObject.Category));
                OutputStatusMessage(string.Format("ConfidenceScore: {0}", dataObject.ConfidenceScore));
                OutputStatusMessage("* * * End OutputKeywordCategory * * *");
            }
        }
        public void OutputArrayOfKeywordCategory(IList<KeywordCategory> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordCategory(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordCategoryResult(KeywordCategoryResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordCategoryResult * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("KeywordCategories:");
                OutputArrayOfKeywordCategory(dataObject.KeywordCategories);
                OutputStatusMessage("* * * End OutputKeywordCategoryResult * * *");
            }
        }
        public void OutputArrayOfKeywordCategoryResult(IList<KeywordCategoryResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordCategoryResult(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordDemographic(KeywordDemographic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordDemographic * * *");
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage(string.Format("EighteenToTwentyFour: {0}", dataObject.EighteenToTwentyFour));
                OutputStatusMessage(string.Format("TwentyFiveToThirtyFour: {0}", dataObject.TwentyFiveToThirtyFour));
                OutputStatusMessage(string.Format("ThirtyFiveToFourtyNine: {0}", dataObject.ThirtyFiveToFourtyNine));
                OutputStatusMessage(string.Format("FiftyToSixtyFour: {0}", dataObject.FiftyToSixtyFour));
                OutputStatusMessage(string.Format("SixtyFiveAndAbove: {0}", dataObject.SixtyFiveAndAbove));
                OutputStatusMessage(string.Format("AgeUnknown: {0}", dataObject.AgeUnknown));
                OutputStatusMessage(string.Format("Female: {0}", dataObject.Female));
                OutputStatusMessage(string.Format("Male: {0}", dataObject.Male));
                OutputStatusMessage(string.Format("GenderUnknown: {0}", dataObject.GenderUnknown));
                OutputStatusMessage("* * * End OutputKeywordDemographic * * *");
            }
        }
        public void OutputArrayOfKeywordDemographic(IList<KeywordDemographic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordDemographic(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordDemographicResult(KeywordDemographicResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordDemographicResult * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("KeywordDemographics:");
                OutputArrayOfKeywordDemographic(dataObject.KeywordDemographics);
                OutputStatusMessage("* * * End OutputKeywordDemographicResult * * *");
            }
        }
        public void OutputArrayOfKeywordDemographicResult(IList<KeywordDemographicResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordDemographicResult(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordEstimate(KeywordEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordEstimate * * *");
                OutputStatusMessage("Keyword:");
                OutputKeyword(dataObject.Keyword);
                OutputStatusMessage("Maximum:");
                OutputTrafficEstimate(dataObject.Maximum);
                OutputStatusMessage("Minimum:");
                OutputTrafficEstimate(dataObject.Minimum);
                OutputStatusMessage("* * * End OutputKeywordEstimate * * *");
            }
        }
        public void OutputArrayOfKeywordEstimate(IList<KeywordEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordEstimate(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordEstimatedBid(KeywordEstimatedBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordEstimatedBid * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("EstimatedBids:");
                OutputArrayOfEstimatedBidAndTraffic(dataObject.EstimatedBids);
                OutputStatusMessage("* * * End OutputKeywordEstimatedBid * * *");
            }
        }
        public void OutputArrayOfKeywordEstimatedBid(IList<KeywordEstimatedBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordEstimatedBid(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordEstimatedPosition(KeywordEstimatedPosition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordEstimatedPosition * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("EstimatedPositions:");
                OutputArrayOfEstimatedPositionAndTraffic(dataObject.EstimatedPositions);
                OutputStatusMessage("* * * End OutputKeywordEstimatedPosition * * *");
            }
        }
        public void OutputArrayOfKeywordEstimatedPosition(IList<KeywordEstimatedPosition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordEstimatedPosition(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordEstimator(KeywordEstimator dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordEstimator * * *");
                OutputStatusMessage("Keyword:");
                OutputKeyword(dataObject.Keyword);
                OutputStatusMessage(string.Format("MaxCpc: {0}", dataObject.MaxCpc));
                OutputStatusMessage("* * * End OutputKeywordEstimator * * *");
            }
        }
        public void OutputArrayOfKeywordEstimator(IList<KeywordEstimator> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordEstimator(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordHistoricalPerformance(KeywordHistoricalPerformance dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordHistoricalPerformance * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("KeywordKPIs:");
                OutputArrayOfKeywordKPI(dataObject.KeywordKPIs);
                OutputStatusMessage("* * * End OutputKeywordHistoricalPerformance * * *");
            }
        }
        public void OutputArrayOfKeywordHistoricalPerformance(IList<KeywordHistoricalPerformance> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordHistoricalPerformance(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordIdea(KeywordIdea dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordIdea * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", dataObject.AdGroupName));
                OutputStatusMessage(string.Format("AdImpressionShare: {0}", dataObject.AdImpressionShare));
                OutputStatusMessage(string.Format("Competition: {0}", dataObject.Competition));
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("MonthlySearchCounts:");
                OutputArrayOfLong(dataObject.MonthlySearchCounts);
                OutputStatusMessage(string.Format("Relevance: {0}", dataObject.Relevance));
                OutputStatusMessage(string.Format("Source: {0}", dataObject.Source));
                OutputStatusMessage(string.Format("SuggestedBid: {0}", dataObject.SuggestedBid));
                OutputStatusMessage("* * * End OutputKeywordIdea * * *");
            }
        }
        public void OutputArrayOfKeywordIdea(IList<KeywordIdea> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordIdea(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordIdeaCategory(KeywordIdeaCategory dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordIdeaCategory * * *");
                OutputStatusMessage(string.Format("CategoryId: {0}", dataObject.CategoryId));
                OutputStatusMessage(string.Format("CategoryName: {0}", dataObject.CategoryName));
                OutputStatusMessage("* * * End OutputKeywordIdeaCategory * * *");
            }
        }
        public void OutputArrayOfKeywordIdeaCategory(IList<KeywordIdeaCategory> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordIdeaCategory(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordIdEstimatedBid(KeywordIdEstimatedBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordIdEstimatedBid * * *");
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputStatusMessage("KeywordEstimatedBid:");
                OutputKeywordEstimatedBid(dataObject.KeywordEstimatedBid);
                OutputStatusMessage("* * * End OutputKeywordIdEstimatedBid * * *");
            }
        }
        public void OutputArrayOfKeywordIdEstimatedBid(IList<KeywordIdEstimatedBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordIdEstimatedBid(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordIdEstimatedPosition(KeywordIdEstimatedPosition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordIdEstimatedPosition * * *");
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputStatusMessage("KeywordEstimatedPosition:");
                OutputKeywordEstimatedPosition(dataObject.KeywordEstimatedPosition);
                OutputStatusMessage("* * * End OutputKeywordIdEstimatedPosition * * *");
            }
        }
        public void OutputArrayOfKeywordIdEstimatedPosition(IList<KeywordIdEstimatedPosition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordIdEstimatedPosition(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordKPI(KeywordKPI dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordKPI * * *");
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("AdPosition: {0}", dataObject.AdPosition));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("CTR: {0}", dataObject.CTR));
                OutputStatusMessage(string.Format("TotalCost: {0}", dataObject.TotalCost));
                OutputStatusMessage(string.Format("AverageBid: {0}", dataObject.AverageBid));
                OutputStatusMessage("* * * End OutputKeywordKPI * * *");
            }
        }
        public void OutputArrayOfKeywordKPI(IList<KeywordKPI> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordKPI(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordLocation(KeywordLocation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordLocation * * *");
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("Percentage: {0}", dataObject.Percentage));
                OutputStatusMessage("* * * End OutputKeywordLocation * * *");
            }
        }
        public void OutputArrayOfKeywordLocation(IList<KeywordLocation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordLocation(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordLocationResult(KeywordLocationResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordLocationResult * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("KeywordLocations:");
                OutputArrayOfKeywordLocation(dataObject.KeywordLocations);
                OutputStatusMessage("* * * End OutputKeywordLocationResult * * *");
            }
        }
        public void OutputArrayOfKeywordLocationResult(IList<KeywordLocationResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordLocationResult(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordOpportunity(KeywordOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordOpportunity * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", dataObject.AdGroupName));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", dataObject.CampaignName));
                OutputStatusMessage(string.Format("Competition: {0}", dataObject.Competition));
                OutputStatusMessage(string.Format("EstimatedIncreaseInClicks: {0}", dataObject.EstimatedIncreaseInClicks));
                OutputStatusMessage(string.Format("EstimatedIncreaseInCost: {0}", dataObject.EstimatedIncreaseInCost));
                OutputStatusMessage(string.Format("EstimatedIncreaseInImpressions: {0}", dataObject.EstimatedIncreaseInImpressions));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("MonthlySearches: {0}", dataObject.MonthlySearches));
                OutputStatusMessage(string.Format("SuggestedBid: {0}", dataObject.SuggestedBid));
                OutputStatusMessage(string.Format("SuggestedKeyword: {0}", dataObject.SuggestedKeyword));
                var broadmatchkeywordopportunity = dataObject as BroadMatchKeywordOpportunity;
                if(null != broadmatchkeywordopportunity)
                {
                    OutputBroadMatchKeywordOpportunity((BroadMatchKeywordOpportunity)dataObject);
                }
                OutputStatusMessage("* * * End OutputKeywordOpportunity * * *");
            }
        }
        public void OutputArrayOfKeywordOpportunity(IList<KeywordOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordOpportunity(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordSearchCount(KeywordSearchCount dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordSearchCount * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("SearchCountsByAttributes:");
                OutputArrayOfSearchCountsByAttributes(dataObject.SearchCountsByAttributes);
                OutputStatusMessage("* * * End OutputKeywordSearchCount * * *");
            }
        }
        public void OutputArrayOfKeywordSearchCount(IList<KeywordSearchCount> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordSearchCount(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordSuggestion(KeywordSuggestion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordSuggestion * * *");
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputStatusMessage("SuggestionsAndConfidence:");
                OutputArrayOfKeywordAndConfidence(dataObject.SuggestionsAndConfidence);
                OutputStatusMessage("* * * End OutputKeywordSuggestion * * *");
            }
        }
        public void OutputArrayOfKeywordSuggestion(IList<KeywordSuggestion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordSuggestion(dataObject);
                    }
                }
            }
        }
        public void OutputLanguageCriterion(LanguageCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLanguageCriterion * * *");
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputLanguageCriterion * * *");
            }
        }
        public void OutputArrayOfLanguageCriterion(IList<LanguageCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLanguageCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputLanguageSearchParameter(LanguageSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLanguageSearchParameter * * *");
                OutputStatusMessage("Languages:");
                OutputArrayOfLanguageCriterion(dataObject.Languages);
                OutputStatusMessage("* * * End OutputLanguageSearchParameter * * *");
            }
        }
        public void OutputArrayOfLanguageSearchParameter(IList<LanguageSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLanguageSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputLocationCriterion(LocationCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLocationCriterion * * *");
                OutputStatusMessage(string.Format("LocationId: {0}", dataObject.LocationId));
                OutputStatusMessage("* * * End OutputLocationCriterion * * *");
            }
        }
        public void OutputArrayOfLocationCriterion(IList<LocationCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLocationCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputLocationSearchParameter(LocationSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLocationSearchParameter * * *");
                OutputStatusMessage("Locations:");
                OutputArrayOfLocationCriterion(dataObject.Locations);
                OutputStatusMessage("* * * End OutputLocationSearchParameter * * *");
            }
        }
        public void OutputArrayOfLocationSearchParameter(IList<LocationSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLocationSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeKeyword(NegativeKeyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeKeyword * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage("* * * End OutputNegativeKeyword * * *");
            }
        }
        public void OutputArrayOfNegativeKeyword(IList<NegativeKeyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeKeyword(dataObject);
                    }
                }
            }
        }
        public void OutputNetworkCriterion(NetworkCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNetworkCriterion * * *");
                OutputStatusMessage(string.Format("Network: {0}", dataObject.Network));
                OutputStatusMessage("* * * End OutputNetworkCriterion * * *");
            }
        }
        public void OutputArrayOfNetworkCriterion(IList<NetworkCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNetworkCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputNetworkSearchParameter(NetworkSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNetworkSearchParameter * * *");
                OutputStatusMessage("Network:");
                OutputNetworkCriterion(dataObject.Network);
                OutputStatusMessage("* * * End OutputNetworkSearchParameter * * *");
            }
        }
        public void OutputArrayOfNetworkSearchParameter(IList<NetworkSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNetworkSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOperationError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputOperationError * * *");
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOperationError(dataObject);
                    }
                }
            }
        }
        public void OutputOpportunity(Opportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOpportunity * * *");
                OutputStatusMessage(string.Format("OpportunityKey: {0}", dataObject.OpportunityKey));
                var bidopportunity = dataObject as BidOpportunity;
                if(null != bidopportunity)
                {
                    OutputBidOpportunity((BidOpportunity)dataObject);
                }
                var budgetopportunity = dataObject as BudgetOpportunity;
                if(null != budgetopportunity)
                {
                    OutputBudgetOpportunity((BudgetOpportunity)dataObject);
                }
                var keywordopportunity = dataObject as KeywordOpportunity;
                if(null != keywordopportunity)
                {
                    OutputKeywordOpportunity((KeywordOpportunity)dataObject);
                }
                OutputStatusMessage("* * * End OutputOpportunity * * *");
            }
        }
        public void OutputArrayOfOpportunity(IList<Opportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOpportunity(dataObject);
                    }
                }
            }
        }
        public void OutputQuerySearchParameter(QuerySearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputQuerySearchParameter * * *");
                OutputStatusMessage("Queries:");
                OutputArrayOfString(dataObject.Queries);
                OutputStatusMessage("* * * End OutputQuerySearchParameter * * *");
            }
        }
        public void OutputArrayOfQuerySearchParameter(IList<QuerySearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputQuerySearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputSearchCountsByAttributes(SearchCountsByAttributes dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchCountsByAttributes * * *");
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage("HistoricalSearchCounts:");
                OutputArrayOfHistoricalSearchCountPeriodic(dataObject.HistoricalSearchCounts);
                OutputStatusMessage("* * * End OutputSearchCountsByAttributes * * *");
            }
        }
        public void OutputArrayOfSearchCountsByAttributes(IList<SearchCountsByAttributes> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchCountsByAttributes(dataObject);
                    }
                }
            }
        }
        public void OutputSearchParameter(SearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchParameter * * *");
                var auctionsegmentsearchparameter = dataObject as AuctionSegmentSearchParameter;
                if(null != auctionsegmentsearchparameter)
                {
                    OutputAuctionSegmentSearchParameter((AuctionSegmentSearchParameter)dataObject);
                }
                var categorysearchparameter = dataObject as CategorySearchParameter;
                if(null != categorysearchparameter)
                {
                    OutputCategorySearchParameter((CategorySearchParameter)dataObject);
                }
                var competitionsearchparameter = dataObject as CompetitionSearchParameter;
                if(null != competitionsearchparameter)
                {
                    OutputCompetitionSearchParameter((CompetitionSearchParameter)dataObject);
                }
                var daterangesearchparameter = dataObject as DateRangeSearchParameter;
                if(null != daterangesearchparameter)
                {
                    OutputDateRangeSearchParameter((DateRangeSearchParameter)dataObject);
                }
                var devicesearchparameter = dataObject as DeviceSearchParameter;
                if(null != devicesearchparameter)
                {
                    OutputDeviceSearchParameter((DeviceSearchParameter)dataObject);
                }
                var excludeaccountkeywordssearchparameter = dataObject as ExcludeAccountKeywordsSearchParameter;
                if(null != excludeaccountkeywordssearchparameter)
                {
                    OutputExcludeAccountKeywordsSearchParameter((ExcludeAccountKeywordsSearchParameter)dataObject);
                }
                var ideatextsearchparameter = dataObject as IdeaTextSearchParameter;
                if(null != ideatextsearchparameter)
                {
                    OutputIdeaTextSearchParameter((IdeaTextSearchParameter)dataObject);
                }
                var impressionsharesearchparameter = dataObject as ImpressionShareSearchParameter;
                if(null != impressionsharesearchparameter)
                {
                    OutputImpressionShareSearchParameter((ImpressionShareSearchParameter)dataObject);
                }
                var languagesearchparameter = dataObject as LanguageSearchParameter;
                if(null != languagesearchparameter)
                {
                    OutputLanguageSearchParameter((LanguageSearchParameter)dataObject);
                }
                var locationsearchparameter = dataObject as LocationSearchParameter;
                if(null != locationsearchparameter)
                {
                    OutputLocationSearchParameter((LocationSearchParameter)dataObject);
                }
                var networksearchparameter = dataObject as NetworkSearchParameter;
                if(null != networksearchparameter)
                {
                    OutputNetworkSearchParameter((NetworkSearchParameter)dataObject);
                }
                var querysearchparameter = dataObject as QuerySearchParameter;
                if(null != querysearchparameter)
                {
                    OutputQuerySearchParameter((QuerySearchParameter)dataObject);
                }
                var searchvolumesearchparameter = dataObject as SearchVolumeSearchParameter;
                if(null != searchvolumesearchparameter)
                {
                    OutputSearchVolumeSearchParameter((SearchVolumeSearchParameter)dataObject);
                }
                var suggestedbidsearchparameter = dataObject as SuggestedBidSearchParameter;
                if(null != suggestedbidsearchparameter)
                {
                    OutputSuggestedBidSearchParameter((SuggestedBidSearchParameter)dataObject);
                }
                var urlsearchparameter = dataObject as UrlSearchParameter;
                if(null != urlsearchparameter)
                {
                    OutputUrlSearchParameter((UrlSearchParameter)dataObject);
                }
                OutputStatusMessage("* * * End OutputSearchParameter * * *");
            }
        }
        public void OutputArrayOfSearchParameter(IList<SearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputSearchVolumeSearchParameter(SearchVolumeSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchVolumeSearchParameter * * *");
                OutputStatusMessage(string.Format("Maximum: {0}", dataObject.Maximum));
                OutputStatusMessage(string.Format("Minimum: {0}", dataObject.Minimum));
                OutputStatusMessage("* * * End OutputSearchVolumeSearchParameter * * *");
            }
        }
        public void OutputArrayOfSearchVolumeSearchParameter(IList<SearchVolumeSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchVolumeSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputSuggestedBidSearchParameter(SuggestedBidSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSuggestedBidSearchParameter * * *");
                OutputStatusMessage(string.Format("Maximum: {0}", dataObject.Maximum));
                OutputStatusMessage(string.Format("Minimum: {0}", dataObject.Minimum));
                OutputStatusMessage("* * * End OutputSuggestedBidSearchParameter * * *");
            }
        }
        public void OutputArrayOfSuggestedBidSearchParameter(IList<SuggestedBidSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSuggestedBidSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputTrafficEstimate(TrafficEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTrafficEstimate * * *");
                OutputStatusMessage(string.Format("AverageCpc: {0}", dataObject.AverageCpc));
                OutputStatusMessage(string.Format("AveragePosition: {0}", dataObject.AveragePosition));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Ctr: {0}", dataObject.Ctr));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("TotalCost: {0}", dataObject.TotalCost));
                OutputStatusMessage("* * * End OutputTrafficEstimate * * *");
            }
        }
        public void OutputArrayOfTrafficEstimate(IList<TrafficEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTrafficEstimate(dataObject);
                    }
                }
            }
        }
        public void OutputUrlSearchParameter(UrlSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUrlSearchParameter * * *");
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                OutputStatusMessage("* * * End OutputUrlSearchParameter * * *");
            }
        }
        public void OutputArrayOfUrlSearchParameter(IList<UrlSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUrlSearchParameter(dataObject);
                    }
                }
            }
        }
        public void OutputBidOpportunityType(BidOpportunityType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BidOpportunityType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBidOpportunityType(IList<BidOpportunityType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBidOpportunityType(valueSet);
                }
            }
        }
        public void OutputBudgetPointType(BudgetPointType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BudgetPointType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBudgetPointType(IList<BudgetPointType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBudgetPointType(valueSet);
                }
            }
        }
        public void OutputBudgetLimitType(BudgetLimitType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BudgetLimitType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBudgetLimitType(IList<BudgetLimitType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBudgetLimitType(valueSet);
                }
            }
        }
        public void OutputKeywordOpportunityType(KeywordOpportunityType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(KeywordOpportunityType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfKeywordOpportunityType(IList<KeywordOpportunityType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputKeywordOpportunityType(valueSet);
                }
            }
        }
        public void OutputTargetAdPosition(TargetAdPosition valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(TargetAdPosition)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfTargetAdPosition(IList<TargetAdPosition> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputTargetAdPosition(valueSet);
                }
            }
        }
        public void OutputCurrencyCode(CurrencyCode valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CurrencyCode)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCurrencyCode(IList<CurrencyCode> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCurrencyCode(valueSet);
                }
            }
        }
        public void OutputMatchType(MatchType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(MatchType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfMatchType(IList<MatchType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputMatchType(valueSet);
                }
            }
        }
        public void OutputAdGroupBidLandscapeType(AdGroupBidLandscapeType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupBidLandscapeType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupBidLandscapeType(IList<AdGroupBidLandscapeType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupBidLandscapeType(valueSet);
                }
            }
        }
        public void OutputTimeInterval(TimeInterval valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(TimeInterval)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfTimeInterval(IList<TimeInterval> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputTimeInterval(valueSet);
                }
            }
        }
        public void OutputAdPosition(AdPosition valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdPosition)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdPosition(IList<AdPosition> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdPosition(valueSet);
                }
            }
        }
        public void OutputEntityType(EntityType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(EntityType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfEntityType(IList<EntityType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputEntityType(valueSet);
                }
            }
        }
        public void OutputNetworkType(NetworkType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(NetworkType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfNetworkType(IList<NetworkType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputNetworkType(valueSet);
                }
            }
        }
        public void OutputCompetitionLevel(CompetitionLevel valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CompetitionLevel)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCompetitionLevel(IList<CompetitionLevel> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCompetitionLevel(valueSet);
                }
            }
        }
        public void OutputAuctionSegment(AuctionSegment valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AuctionSegment)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAuctionSegment(IList<AuctionSegment> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAuctionSegment(valueSet);
                }
            }
        }
        public void OutputAuctionInsightKpiAdditionalField(AuctionInsightKpiAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AuctionInsightKpiAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAuctionInsightKpiAdditionalField(IList<AuctionInsightKpiAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAuctionInsightKpiAdditionalField(valueSet);
                }
            }
        }
        public void OutputKeywordIdeaAttribute(KeywordIdeaAttribute valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(KeywordIdeaAttribute)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfKeywordIdeaAttribute(IList<KeywordIdeaAttribute> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputKeywordIdeaAttribute(valueSet);
                }
            }
        }
        public void OutputSourceType(SourceType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SourceType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSourceType(IList<SourceType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSourceType(valueSet);
                }
            }
        }
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputKeyValuePairOfstringstring(KeyValuePair<string,string> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringstring(IList<KeyValuePair<string,string>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringstring(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOflonglong(KeyValuePair<long,long> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOflonglong(IList<KeyValuePair<long,long>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOflonglong(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOfstringbase64Binary(KeyValuePair<string,byte[]> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringbase64Binary(IList<KeyValuePair<string,byte[]>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringbase64Binary(dataObject);
                }
            }
        }
    }
}