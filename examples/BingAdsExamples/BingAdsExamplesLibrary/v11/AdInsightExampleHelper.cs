using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.AdInsight;

namespace BingAdsExamplesLibrary.V11
{
    public class AdInsightExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | AdInsight V11"; }
        }
        public ServiceClient<IAdInsightService> AdInsightService;
        public AdInsightExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
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
            IList<String> publisherCountries,
            Currency? currency,
            long? campaignId,
            long? adGroupId,
            String entityLevelBid)
        {
            var request = new GetEstimatedBidByKeywordsRequest
            {
                Keywords = keywords,
                TargetPositionForAds = targetPositionForAds,
                Language = language,
                PublisherCountries = publisherCountries,
                Currency = currency,
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
            IList<String> publisherCountries,
            Currency? currency,
            IList<MatchType> matchTypes,
            long? campaignId,
            long? adGroupId)
        {
            var request = new GetEstimatedPositionByKeywordsRequest
            {
                Keywords = keywords,
                MaxBid = maxBid,
                Language = language,
                PublisherCountries = publisherCountries,
                Currency = currency,
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
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdApiError(dataObject.Errors);
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupBidLandscape(AdGroupBidLandscape dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupBidLandscapeType: {0}", dataObject.AdGroupBidLandscapeType));
                OutputDayMonthAndYear(dataObject.StartDate);
                OutputDayMonthAndYear(dataObject.EndDate);
                OutputArrayOfBidLandscapePoint(dataObject.BidLandscapePoints);
            }
        }
        public void OutputArrayOfAdGroupBidLandscape(IList<AdGroupBidLandscape> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupBidLandscape(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupBidLandscapeInput(AdGroupBidLandscapeInput dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupBidLandscapeType: {0}", dataObject.AdGroupBidLandscapeType));
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
            }
        }
        public void OutputArrayOfAdGroupBidLandscapeInput(IList<AdGroupBidLandscapeInput> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupBidLandscapeInput(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupEstimate(AdGroupEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputArrayOfKeywordEstimate(dataObject.KeywordEstimates);
            }
        }
        public void OutputArrayOfAdGroupEstimate(IList<AdGroupEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupEstimate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupEstimator(AdGroupEstimator dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputArrayOfKeywordEstimator(dataObject.KeywordEstimators);
                OutputStatusMessage(string.Format("MaxCpc: {0}", dataObject.MaxCpc));
            }
        }
        public void OutputArrayOfAdGroupEstimator(IList<AdGroupEstimator> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupEstimator(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputArrayOfOperationError(dataObject.OperationErrors);
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(adapifaultdetail != null)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(apifaultdetail != null)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApplicationFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBatchError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBidLandscapePoint(BidLandscapePoint dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Bid: {0}", dataObject.Bid));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("TopImpressions: {0}", dataObject.TopImpressions));
                OutputStatusMessage(string.Format("Currency: {0}", dataObject.Currency));
                OutputStatusMessage(string.Format("Cost: {0}", dataObject.Cost));
                OutputStatusMessage(string.Format("MarginalCPC: {0}", dataObject.MarginalCPC));
            }
        }
        public void OutputArrayOfBidLandscapePoint(IList<BidLandscapePoint> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBidLandscapePoint(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBidOpportunity(BidOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("CurrentBid: {0}", dataObject.CurrentBid));
                OutputStatusMessage(string.Format("EstimatedIncreaseInClicks: {0}", dataObject.EstimatedIncreaseInClicks));
                OutputStatusMessage(string.Format("EstimatedIncreaseInCost: {0}", dataObject.EstimatedIncreaseInCost));
                OutputStatusMessage(string.Format("EstimatedIncreaseInImpressions: {0}", dataObject.EstimatedIncreaseInImpressions));
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("SuggestedBid: {0}", dataObject.SuggestedBid));
            }
        }
        public void OutputArrayOfBidOpportunity(IList<BidOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBidOpportunity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBroadMatchKeywordOpportunity(BroadMatchKeywordOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("AverageCTR: {0}", dataObject.AverageCTR));
                OutputStatusMessage(string.Format("ClickShare: {0}", dataObject.ClickShare));
                OutputStatusMessage(string.Format("ImpressionShare: {0}", dataObject.ImpressionShare));
                OutputStatusMessage(string.Format("ReferenceKeywordBid: {0}", dataObject.ReferenceKeywordBid));
                OutputStatusMessage(string.Format("ReferenceKeywordId: {0}", dataObject.ReferenceKeywordId));
                OutputStatusMessage(string.Format("ReferenceKeywordMatchType: {0}", dataObject.ReferenceKeywordMatchType));
                OutputArrayOfBroadMatchSearchQueryKPI(dataObject.SearchQueryKPIs);
            }
        }
        public void OutputArrayOfBroadMatchKeywordOpportunity(IList<BroadMatchKeywordOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBroadMatchKeywordOpportunity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBroadMatchSearchQueryKPI(BroadMatchSearchQueryKPI dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AverageCTR: {0}", dataObject.AverageCTR));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("SRPV: {0}", dataObject.SRPV));
                OutputStatusMessage(string.Format("SearchQuery: {0}", dataObject.SearchQuery));
            }
        }
        public void OutputArrayOfBroadMatchSearchQueryKPI(IList<BroadMatchSearchQueryKPI> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBroadMatchSearchQueryKPI(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBudgetOpportunity(BudgetOpportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBudgetPoint(dataObject.BudgetPoints);
                OutputStatusMessage(string.Format("BudgetType: {0}", dataObject.BudgetType));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("CurrentBudget: {0}", dataObject.CurrentBudget));
                OutputStatusMessage(string.Format("IncreaseInClicks: {0}", dataObject.IncreaseInClicks));
                OutputStatusMessage(string.Format("IncreaseInImpressions: {0}", dataObject.IncreaseInImpressions));
                OutputStatusMessage(string.Format("PercentageIncreaseInClicks: {0}", dataObject.PercentageIncreaseInClicks));
                OutputStatusMessage(string.Format("PercentageIncreaseInImpressions: {0}", dataObject.PercentageIncreaseInImpressions));
                OutputStatusMessage(string.Format("RecommendedBudget: {0}", dataObject.RecommendedBudget));
            }
        }
        public void OutputArrayOfBudgetOpportunity(IList<BudgetOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBudgetOpportunity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBudgetPoint(BudgetPoint dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("BudgetAmount: {0}", dataObject.BudgetAmount));
                OutputStatusMessage(string.Format("BudgetPointType: {0}", dataObject.BudgetPointType));
                OutputStatusMessage(string.Format("EstimatedWeeklyClicks: {0}", dataObject.EstimatedWeeklyClicks));
                OutputStatusMessage(string.Format("EstimatedWeeklyCost: {0}", dataObject.EstimatedWeeklyCost));
                OutputStatusMessage(string.Format("EstimatedWeeklyImpressions: {0}", dataObject.EstimatedWeeklyImpressions));
            }
        }
        public void OutputArrayOfBudgetPoint(IList<BudgetPoint> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBudgetPoint(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignEstimate(CampaignEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdGroupEstimate(dataObject.AdGroupEstimates);
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
            }
        }
        public void OutputArrayOfCampaignEstimate(IList<CampaignEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignEstimate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignEstimator(CampaignEstimator dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdGroupEstimator(dataObject.AdGroupEstimators);
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputArrayOfCriterion(dataObject.Criteria);
                OutputStatusMessage(string.Format("DailyBudget: {0}", dataObject.DailyBudget));
                OutputArrayOfNegativeKeyword(dataObject.NegativeKeywords);
            }
        }
        public void OutputArrayOfCampaignEstimator(IList<CampaignEstimator> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignEstimator(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCategorySearchParameter(CategorySearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CategoryId: {0}", dataObject.CategoryId));
            }
        }
        public void OutputArrayOfCategorySearchParameter(IList<CategorySearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCategorySearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCompetitionSearchParameter(CompetitionSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfCompetitionLevel(dataObject.CompetitionLevels);
            }
        }
        public void OutputArrayOfCompetitionSearchParameter(IList<CompetitionSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCompetitionSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCriterion(Criterion dataObject)
        {
            if (null != dataObject)
            {
                var devicecriterion = dataObject as DeviceCriterion;
                if(devicecriterion != null)
                {
                    OutputDeviceCriterion((DeviceCriterion)dataObject);
                }
                var languagecriterion = dataObject as LanguageCriterion;
                if(languagecriterion != null)
                {
                    OutputLanguageCriterion((LanguageCriterion)dataObject);
                }
                var locationcriterion = dataObject as LocationCriterion;
                if(locationcriterion != null)
                {
                    OutputLocationCriterion((LocationCriterion)dataObject);
                }
                var networkcriterion = dataObject as NetworkCriterion;
                if(networkcriterion != null)
                {
                    OutputNetworkCriterion((NetworkCriterion)dataObject);
                }
            }
        }
        public void OutputArrayOfCriterion(IList<Criterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDateRangeSearchParameter(DateRangeSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputDayMonthAndYear(dataObject.EndDate);
                OutputDayMonthAndYear(dataObject.StartDate);
            }
        }
        public void OutputArrayOfDateRangeSearchParameter(IList<DateRangeSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDateRangeSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDayMonthAndYear(DayMonthAndYear dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
            }
        }
        public void OutputArrayOfDayMonthAndYear(IList<DayMonthAndYear> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDayMonthAndYear(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDeviceCriterion(DeviceCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("DeviceName: {0}", dataObject.DeviceName));
            }
        }
        public void OutputArrayOfDeviceCriterion(IList<DeviceCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDeviceCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDeviceSearchParameter(DeviceSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputDeviceCriterion(dataObject.Device);
            }
        }
        public void OutputArrayOfDeviceSearchParameter(IList<DeviceSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDeviceSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDomainCategory(DomainCategory dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Bid: {0}", dataObject.Bid));
                OutputStatusMessage(string.Format("CategoryName: {0}", dataObject.CategoryName));
                OutputStatusMessage(string.Format("Coverage: {0}", dataObject.Coverage));
            }
        }
        public void OutputArrayOfDomainCategory(IList<DomainCategory> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDomainCategory(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEstimatedBidAndTraffic(EstimatedBidAndTraffic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("MinClicksPerWeek: {0}", dataObject.MinClicksPerWeek));
                OutputStatusMessage(string.Format("MaxClicksPerWeek: {0}", dataObject.MaxClicksPerWeek));
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("MinImpressionsPerWeek: {0}", dataObject.MinImpressionsPerWeek));
                OutputStatusMessage(string.Format("MaxImpressionsPerWeek: {0}", dataObject.MaxImpressionsPerWeek));
                OutputStatusMessage(string.Format("CTR: {0}", dataObject.CTR));
                OutputStatusMessage(string.Format("MinTotalCostPerWeek: {0}", dataObject.MinTotalCostPerWeek));
                OutputStatusMessage(string.Format("MaxTotalCostPerWeek: {0}", dataObject.MaxTotalCostPerWeek));
                OutputStatusMessage(string.Format("Currency: {0}", dataObject.Currency));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("EstimatedMinBid: {0}", dataObject.EstimatedMinBid));
            }
        }
        public void OutputArrayOfEstimatedBidAndTraffic(IList<EstimatedBidAndTraffic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEstimatedBidAndTraffic(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEstimatedPositionAndTraffic(EstimatedPositionAndTraffic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("MinClicksPerWeek: {0}", dataObject.MinClicksPerWeek));
                OutputStatusMessage(string.Format("MaxClicksPerWeek: {0}", dataObject.MaxClicksPerWeek));
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("MinImpressionsPerWeek: {0}", dataObject.MinImpressionsPerWeek));
                OutputStatusMessage(string.Format("MaxImpressionsPerWeek: {0}", dataObject.MaxImpressionsPerWeek));
                OutputStatusMessage(string.Format("CTR: {0}", dataObject.CTR));
                OutputStatusMessage(string.Format("MinTotalCostPerWeek: {0}", dataObject.MinTotalCostPerWeek));
                OutputStatusMessage(string.Format("MaxTotalCostPerWeek: {0}", dataObject.MaxTotalCostPerWeek));
                OutputStatusMessage(string.Format("Currency: {0}", dataObject.Currency));
                OutputStatusMessage(string.Format("EstimatedAdPosition: {0}", dataObject.EstimatedAdPosition));
            }
        }
        public void OutputArrayOfEstimatedPositionAndTraffic(IList<EstimatedPositionAndTraffic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEstimatedPositionAndTraffic(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputExcludeAccountKeywordsSearchParameter(ExcludeAccountKeywordsSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ExcludeAccountKeywords: {0}", dataObject.ExcludeAccountKeywords));
            }
        }
        public void OutputArrayOfExcludeAccountKeywordsSearchParameter(IList<ExcludeAccountKeywordsSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputExcludeAccountKeywordsSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputHistoricalSearchCountPeriodic(HistoricalSearchCountPeriodic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("SearchCount: {0}", dataObject.SearchCount));
                OutputDayMonthAndYear(dataObject.DayMonthAndYear);
            }
        }
        public void OutputArrayOfHistoricalSearchCountPeriodic(IList<HistoricalSearchCountPeriodic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputHistoricalSearchCountPeriodic(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputIdeaTextSearchParameter(IdeaTextSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfKeyword(dataObject.Excluded);
                OutputArrayOfKeyword(dataObject.Included);
            }
        }
        public void OutputArrayOfIdeaTextSearchParameter(IList<IdeaTextSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputIdeaTextSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputImpressionShareSearchParameter(ImpressionShareSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Maximum: {0}", dataObject.Maximum));
                OutputStatusMessage(string.Format("Minimum: {0}", dataObject.Minimum));
            }
        }
        public void OutputArrayOfImpressionShareSearchParameter(IList<ImpressionShareSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputImpressionShareSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeyword(Keyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
            }
        }
        public void OutputArrayOfKeyword(IList<Keyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyword(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordAndConfidence(KeywordAndConfidence dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("SuggestedKeyword: {0}", dataObject.SuggestedKeyword));
                OutputStatusMessage(string.Format("ConfidenceScore: {0}", dataObject.ConfidenceScore));
            }
        }
        public void OutputArrayOfKeywordAndConfidence(IList<KeywordAndConfidence> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordAndConfidence(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordAndMatchType(KeywordAndMatchType dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("KeywordText: {0}", dataObject.KeywordText));
                OutputArrayOfMatchType(dataObject.MatchTypes);
            }
        }
        public void OutputArrayOfKeywordAndMatchType(IList<KeywordAndMatchType> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordAndMatchType(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordBidLandscape(KeywordBidLandscape dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputDayMonthAndYear(dataObject.StartDate);
                OutputDayMonthAndYear(dataObject.EndDate);
                OutputArrayOfBidLandscapePoint(dataObject.BidLandscapePoints);
            }
        }
        public void OutputArrayOfKeywordBidLandscape(IList<KeywordBidLandscape> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordBidLandscape(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordCategory(KeywordCategory dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Category: {0}", dataObject.Category));
                OutputStatusMessage(string.Format("ConfidenceScore: {0}", dataObject.ConfidenceScore));
            }
        }
        public void OutputArrayOfKeywordCategory(IList<KeywordCategory> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordCategory(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordCategoryResult(KeywordCategoryResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfKeywordCategory(dataObject.KeywordCategories);
            }
        }
        public void OutputArrayOfKeywordCategoryResult(IList<KeywordCategoryResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordCategoryResult(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordDemographic(KeywordDemographic dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage(string.Format("Age18_24: {0}", dataObject.Age18_24));
                OutputStatusMessage(string.Format("Age25_34: {0}", dataObject.Age25_34));
                OutputStatusMessage(string.Format("Age35_49: {0}", dataObject.Age35_49));
                OutputStatusMessage(string.Format("Age50_64: {0}", dataObject.Age50_64));
                OutputStatusMessage(string.Format("Age65Plus: {0}", dataObject.Age65Plus));
                OutputStatusMessage(string.Format("AgeUnknown: {0}", dataObject.AgeUnknown));
                OutputStatusMessage(string.Format("Female: {0}", dataObject.Female));
                OutputStatusMessage(string.Format("Male: {0}", dataObject.Male));
                OutputStatusMessage(string.Format("GenderUnknown: {0}", dataObject.GenderUnknown));
            }
        }
        public void OutputArrayOfKeywordDemographic(IList<KeywordDemographic> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordDemographic(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordDemographicResult(KeywordDemographicResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfKeywordDemographic(dataObject.KeywordDemographics);
            }
        }
        public void OutputArrayOfKeywordDemographicResult(IList<KeywordDemographicResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordDemographicResult(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordEstimate(KeywordEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputKeyword(dataObject.Keyword);
                OutputTrafficEstimate(dataObject.Maximum);
                OutputTrafficEstimate(dataObject.Minimum);
            }
        }
        public void OutputArrayOfKeywordEstimate(IList<KeywordEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordEstimate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordEstimatedBid(KeywordEstimatedBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfEstimatedBidAndTraffic(dataObject.EstimatedBids);
            }
        }
        public void OutputArrayOfKeywordEstimatedBid(IList<KeywordEstimatedBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordEstimatedBid(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordEstimatedPosition(KeywordEstimatedPosition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfEstimatedPositionAndTraffic(dataObject.EstimatedPositions);
            }
        }
        public void OutputArrayOfKeywordEstimatedPosition(IList<KeywordEstimatedPosition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordEstimatedPosition(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordEstimator(KeywordEstimator dataObject)
        {
            if (null != dataObject)
            {
                OutputKeyword(dataObject.Keyword);
                OutputStatusMessage(string.Format("MaxCpc: {0}", dataObject.MaxCpc));
            }
        }
        public void OutputArrayOfKeywordEstimator(IList<KeywordEstimator> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordEstimator(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordHistoricalPerformance(KeywordHistoricalPerformance dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfKeywordKPI(dataObject.KeywordKPIs);
            }
        }
        public void OutputArrayOfKeywordHistoricalPerformance(IList<KeywordHistoricalPerformance> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordHistoricalPerformance(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordIdea(KeywordIdea dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", dataObject.AdGroupName));
                OutputStatusMessage(string.Format("AdImpressionShare: {0}", dataObject.AdImpressionShare));
                OutputStatusMessage(string.Format("Competition: {0}", dataObject.Competition));
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfLong(dataObject.MonthlySearchCounts);
                OutputStatusMessage(string.Format("Relevance: {0}", dataObject.Relevance));
                OutputStatusMessage(string.Format("Source: {0}", dataObject.Source));
                OutputStatusMessage(string.Format("SuggestedBid: {0}", dataObject.SuggestedBid));
            }
        }
        public void OutputArrayOfKeywordIdea(IList<KeywordIdea> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordIdea(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordIdeaCategory(KeywordIdeaCategory dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CategoryId: {0}", dataObject.CategoryId));
                OutputStatusMessage(string.Format("CategoryName: {0}", dataObject.CategoryName));
            }
        }
        public void OutputArrayOfKeywordIdeaCategory(IList<KeywordIdeaCategory> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordIdeaCategory(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordIdEstimatedBid(KeywordIdEstimatedBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputKeywordEstimatedBid(dataObject.KeywordEstimatedBid);
            }
        }
        public void OutputArrayOfKeywordIdEstimatedBid(IList<KeywordIdEstimatedBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordIdEstimatedBid(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordIdEstimatedPosition(KeywordIdEstimatedPosition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("KeywordId: {0}", dataObject.KeywordId));
                OutputKeywordEstimatedPosition(dataObject.KeywordEstimatedPosition);
            }
        }
        public void OutputArrayOfKeywordIdEstimatedPosition(IList<KeywordIdEstimatedPosition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordIdEstimatedPosition(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordKPI(KeywordKPI dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("AdPosition: {0}", dataObject.AdPosition));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("AverageCPC: {0}", dataObject.AverageCPC));
                OutputStatusMessage(string.Format("CTR: {0}", dataObject.CTR));
                OutputStatusMessage(string.Format("TotalCost: {0}", dataObject.TotalCost));
                OutputStatusMessage(string.Format("AverageBid: {0}", dataObject.AverageBid));
            }
        }
        public void OutputArrayOfKeywordKPI(IList<KeywordKPI> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordKPI(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordLocation(KeywordLocation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("Percentage: {0}", dataObject.Percentage));
            }
        }
        public void OutputArrayOfKeywordLocation(IList<KeywordLocation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordLocation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordLocationResult(KeywordLocationResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfKeywordLocation(dataObject.KeywordLocations);
            }
        }
        public void OutputArrayOfKeywordLocationResult(IList<KeywordLocationResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordLocationResult(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordOpportunity(KeywordOpportunity dataObject)
        {
            if (null != dataObject)
            {
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
                if(broadmatchkeywordopportunity != null)
                {
                    OutputBroadMatchKeywordOpportunity((BroadMatchKeywordOpportunity)dataObject);
                }
            }
        }
        public void OutputArrayOfKeywordOpportunity(IList<KeywordOpportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordOpportunity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordSearchCount(KeywordSearchCount dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfSearchCountsByAttributes(dataObject.SearchCountsByAttributes);
            }
        }
        public void OutputArrayOfKeywordSearchCount(IList<KeywordSearchCount> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordSearchCount(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordSuggestion(KeywordSuggestion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Keyword: {0}", dataObject.Keyword));
                OutputArrayOfKeywordAndConfidence(dataObject.SuggestionsAndConfidence);
            }
        }
        public void OutputArrayOfKeywordSuggestion(IList<KeywordSuggestion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordSuggestion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLanguageCriterion(LanguageCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
            }
        }
        public void OutputArrayOfLanguageCriterion(IList<LanguageCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLanguageCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLanguageSearchParameter(LanguageSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfLanguageCriterion(dataObject.Languages);
            }
        }
        public void OutputArrayOfLanguageSearchParameter(IList<LanguageSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLanguageSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLocationCriterion(LocationCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("LocationId: {0}", dataObject.LocationId));
            }
        }
        public void OutputArrayOfLocationCriterion(IList<LocationCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLocationCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLocationSearchParameter(LocationSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfLocationCriterion(dataObject.Locations);
            }
        }
        public void OutputArrayOfLocationSearchParameter(IList<LocationSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLocationSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeKeyword(NegativeKeyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
            }
        }
        public void OutputArrayOfNegativeKeyword(IList<NegativeKeyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeKeyword(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNetworkCriterion(NetworkCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Network: {0}", dataObject.Network));
            }
        }
        public void OutputArrayOfNetworkCriterion(IList<NetworkCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNetworkCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNetworkSearchParameter(NetworkSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputNetworkCriterion(dataObject.Network);
            }
        }
        public void OutputArrayOfNetworkSearchParameter(IList<NetworkSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNetworkSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOperationError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOpportunity(Opportunity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("OpportunityKey: {0}", dataObject.OpportunityKey));
                var bidopportunity = dataObject as BidOpportunity;
                if(bidopportunity != null)
                {
                    OutputBidOpportunity((BidOpportunity)dataObject);
                }
                var budgetopportunity = dataObject as BudgetOpportunity;
                if(budgetopportunity != null)
                {
                    OutputBudgetOpportunity((BudgetOpportunity)dataObject);
                }
                var keywordopportunity = dataObject as KeywordOpportunity;
                if(keywordopportunity != null)
                {
                    OutputKeywordOpportunity((KeywordOpportunity)dataObject);
                }
            }
        }
        public void OutputArrayOfOpportunity(IList<Opportunity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOpportunity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputQuerySearchParameter(QuerySearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfString(dataObject.Queries);
            }
        }
        public void OutputArrayOfQuerySearchParameter(IList<QuerySearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputQuerySearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchCountsByAttributes(SearchCountsByAttributes dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Device: {0}", dataObject.Device));
                OutputArrayOfHistoricalSearchCountPeriodic(dataObject.HistoricalSearchCounts);
            }
        }
        public void OutputArrayOfSearchCountsByAttributes(IList<SearchCountsByAttributes> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchCountsByAttributes(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchParameter(SearchParameter dataObject)
        {
            if (null != dataObject)
            {
                var categorysearchparameter = dataObject as CategorySearchParameter;
                if(categorysearchparameter != null)
                {
                    OutputCategorySearchParameter((CategorySearchParameter)dataObject);
                }
                var competitionsearchparameter = dataObject as CompetitionSearchParameter;
                if(competitionsearchparameter != null)
                {
                    OutputCompetitionSearchParameter((CompetitionSearchParameter)dataObject);
                }
                var daterangesearchparameter = dataObject as DateRangeSearchParameter;
                if(daterangesearchparameter != null)
                {
                    OutputDateRangeSearchParameter((DateRangeSearchParameter)dataObject);
                }
                var devicesearchparameter = dataObject as DeviceSearchParameter;
                if(devicesearchparameter != null)
                {
                    OutputDeviceSearchParameter((DeviceSearchParameter)dataObject);
                }
                var excludeaccountkeywordssearchparameter = dataObject as ExcludeAccountKeywordsSearchParameter;
                if(excludeaccountkeywordssearchparameter != null)
                {
                    OutputExcludeAccountKeywordsSearchParameter((ExcludeAccountKeywordsSearchParameter)dataObject);
                }
                var ideatextsearchparameter = dataObject as IdeaTextSearchParameter;
                if(ideatextsearchparameter != null)
                {
                    OutputIdeaTextSearchParameter((IdeaTextSearchParameter)dataObject);
                }
                var impressionsharesearchparameter = dataObject as ImpressionShareSearchParameter;
                if(impressionsharesearchparameter != null)
                {
                    OutputImpressionShareSearchParameter((ImpressionShareSearchParameter)dataObject);
                }
                var languagesearchparameter = dataObject as LanguageSearchParameter;
                if(languagesearchparameter != null)
                {
                    OutputLanguageSearchParameter((LanguageSearchParameter)dataObject);
                }
                var locationsearchparameter = dataObject as LocationSearchParameter;
                if(locationsearchparameter != null)
                {
                    OutputLocationSearchParameter((LocationSearchParameter)dataObject);
                }
                var networksearchparameter = dataObject as NetworkSearchParameter;
                if(networksearchparameter != null)
                {
                    OutputNetworkSearchParameter((NetworkSearchParameter)dataObject);
                }
                var querysearchparameter = dataObject as QuerySearchParameter;
                if(querysearchparameter != null)
                {
                    OutputQuerySearchParameter((QuerySearchParameter)dataObject);
                }
                var searchvolumesearchparameter = dataObject as SearchVolumeSearchParameter;
                if(searchvolumesearchparameter != null)
                {
                    OutputSearchVolumeSearchParameter((SearchVolumeSearchParameter)dataObject);
                }
                var suggestedbidsearchparameter = dataObject as SuggestedBidSearchParameter;
                if(suggestedbidsearchparameter != null)
                {
                    OutputSuggestedBidSearchParameter((SuggestedBidSearchParameter)dataObject);
                }
                var urlsearchparameter = dataObject as UrlSearchParameter;
                if(urlsearchparameter != null)
                {
                    OutputUrlSearchParameter((UrlSearchParameter)dataObject);
                }
            }
        }
        public void OutputArrayOfSearchParameter(IList<SearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchVolumeSearchParameter(SearchVolumeSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Maximum: {0}", dataObject.Maximum));
                OutputStatusMessage(string.Format("Minimum: {0}", dataObject.Minimum));
            }
        }
        public void OutputArrayOfSearchVolumeSearchParameter(IList<SearchVolumeSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchVolumeSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSuggestedBidSearchParameter(SuggestedBidSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Maximum: {0}", dataObject.Maximum));
                OutputStatusMessage(string.Format("Minimum: {0}", dataObject.Minimum));
            }
        }
        public void OutputArrayOfSuggestedBidSearchParameter(IList<SuggestedBidSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSuggestedBidSearchParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputTrafficEstimate(TrafficEstimate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AverageCpc: {0}", dataObject.AverageCpc));
                OutputStatusMessage(string.Format("AveragePosition: {0}", dataObject.AveragePosition));
                OutputStatusMessage(string.Format("Clicks: {0}", dataObject.Clicks));
                OutputStatusMessage(string.Format("Ctr: {0}", dataObject.Ctr));
                OutputStatusMessage(string.Format("Impressions: {0}", dataObject.Impressions));
                OutputStatusMessage(string.Format("TotalCost: {0}", dataObject.TotalCost));
            }
        }
        public void OutputArrayOfTrafficEstimate(IList<TrafficEstimate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputTrafficEstimate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputUrlSearchParameter(UrlSearchParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
            }
        }
        public void OutputArrayOfUrlSearchParameter(IList<UrlSearchParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputUrlSearchParameter(dataObject);
                    OutputStatusMessage("\n");
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
        public void OutputCurrency(Currency valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(Currency)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCurrency(IList<Currency> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCurrency(valueSet);
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
                    OutputStatusMessage(string.Format("Value of the string: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the long: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable long: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the int: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable int: {0}", item));
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
    }
}