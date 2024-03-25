using System;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.AdInsight;

namespace Microsoft.BingAds
{
    internal class AdInsightService : IAdInsightService
    {
        private readonly RestServiceClient _restServiceClient;

        private readonly Type _serviceType;

        public AdInsightService(RestServiceClient restServiceClient, Type serviceType)
        {
            _restServiceClient = restServiceClient;

            _serviceType = serviceType;
        }

        public GetBidOpportunitiesResponse GetBidOpportunities(GetBidOpportunitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBidOpportunitiesResponse> GetBidOpportunitiesAsync(GetBidOpportunitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBidOpportunitiesResponse>("GetBidOpportunities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBudgetOpportunitiesResponse GetBudgetOpportunities(GetBudgetOpportunitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBudgetOpportunitiesResponse> GetBudgetOpportunitiesAsync(GetBudgetOpportunitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBudgetOpportunitiesResponse>("GetBudgetOpportunities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordOpportunitiesResponse GetKeywordOpportunities(GetKeywordOpportunitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordOpportunitiesResponse> GetKeywordOpportunitiesAsync(GetKeywordOpportunitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordOpportunitiesResponse>("GetKeywordOpportunities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetEstimatedBidByKeywordIdsResponse GetEstimatedBidByKeywordIds(GetEstimatedBidByKeywordIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetEstimatedBidByKeywordIdsResponse> GetEstimatedBidByKeywordIdsAsync(GetEstimatedBidByKeywordIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetEstimatedBidByKeywordIdsResponse>("GetEstimatedBidByKeywordIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetEstimatedPositionByKeywordIdsResponse GetEstimatedPositionByKeywordIds(GetEstimatedPositionByKeywordIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetEstimatedPositionByKeywordIdsResponse> GetEstimatedPositionByKeywordIdsAsync(GetEstimatedPositionByKeywordIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetEstimatedPositionByKeywordIdsResponse>("GetEstimatedPositionByKeywordIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetEstimatedBidByKeywordsResponse GetEstimatedBidByKeywords(GetEstimatedBidByKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetEstimatedBidByKeywordsResponse> GetEstimatedBidByKeywordsAsync(GetEstimatedBidByKeywordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetEstimatedBidByKeywordsResponse>("GetEstimatedBidByKeywords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetEstimatedPositionByKeywordsResponse GetEstimatedPositionByKeywords(GetEstimatedPositionByKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetEstimatedPositionByKeywordsResponse> GetEstimatedPositionByKeywordsAsync(GetEstimatedPositionByKeywordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetEstimatedPositionByKeywordsResponse>("GetEstimatedPositionByKeywords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBidLandscapeByAdGroupIdsResponse GetBidLandscapeByAdGroupIds(GetBidLandscapeByAdGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBidLandscapeByAdGroupIdsResponse> GetBidLandscapeByAdGroupIdsAsync(GetBidLandscapeByAdGroupIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBidLandscapeByAdGroupIdsResponse>("GetBidLandscapeByAdGroupIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBidLandscapeByKeywordIdsResponse GetBidLandscapeByKeywordIds(GetBidLandscapeByKeywordIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBidLandscapeByKeywordIdsResponse> GetBidLandscapeByKeywordIdsAsync(GetBidLandscapeByKeywordIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBidLandscapeByKeywordIdsResponse>("GetBidLandscapeByKeywordIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetHistoricalKeywordPerformanceResponse GetHistoricalKeywordPerformance(GetHistoricalKeywordPerformanceRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetHistoricalKeywordPerformanceResponse> GetHistoricalKeywordPerformanceAsync(GetHistoricalKeywordPerformanceRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetHistoricalKeywordPerformanceResponse>("GetHistoricalKeywordPerformance", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetHistoricalSearchCountResponse GetHistoricalSearchCount(GetHistoricalSearchCountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetHistoricalSearchCountResponse> GetHistoricalSearchCountAsync(GetHistoricalSearchCountRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetHistoricalSearchCountResponse>("GetHistoricalSearchCount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordCategoriesResponse GetKeywordCategories(GetKeywordCategoriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordCategoriesResponse> GetKeywordCategoriesAsync(GetKeywordCategoriesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordCategoriesResponse>("GetKeywordCategories", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordDemographicsResponse GetKeywordDemographics(GetKeywordDemographicsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordDemographicsResponse> GetKeywordDemographicsAsync(GetKeywordDemographicsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordDemographicsResponse>("GetKeywordDemographics", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordLocationsResponse GetKeywordLocations(GetKeywordLocationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordLocationsResponse> GetKeywordLocationsAsync(GetKeywordLocationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordLocationsResponse>("GetKeywordLocations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SuggestKeywordsForUrlResponse SuggestKeywordsForUrl(SuggestKeywordsForUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SuggestKeywordsForUrlResponse> SuggestKeywordsForUrlAsync(SuggestKeywordsForUrlRequest request)
        {
            return _restServiceClient.CallServiceAsync<SuggestKeywordsForUrlResponse>("SuggestKeywordsForUrl", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SuggestKeywordsFromExistingKeywordsResponse SuggestKeywordsFromExistingKeywords(SuggestKeywordsFromExistingKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SuggestKeywordsFromExistingKeywordsResponse> SuggestKeywordsFromExistingKeywordsAsync(SuggestKeywordsFromExistingKeywordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SuggestKeywordsFromExistingKeywordsResponse>("SuggestKeywordsFromExistingKeywords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAuctionInsightDataResponse GetAuctionInsightData(GetAuctionInsightDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAuctionInsightDataResponse> GetAuctionInsightDataAsync(GetAuctionInsightDataRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAuctionInsightDataResponse>("GetAuctionInsightData", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetDomainCategoriesResponse GetDomainCategories(GetDomainCategoriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetDomainCategoriesResponse> GetDomainCategoriesAsync(GetDomainCategoriesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetDomainCategoriesResponse>("GetDomainCategories", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public PutMetricDataResponse PutMetricData(PutMetricDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PutMetricDataResponse> PutMetricDataAsync(PutMetricDataRequest request)
        {
            return _restServiceClient.CallServiceAsync<PutMetricDataResponse>("PutMetricData", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordIdeaCategoriesResponse GetKeywordIdeaCategories(GetKeywordIdeaCategoriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordIdeaCategoriesResponse> GetKeywordIdeaCategoriesAsync(GetKeywordIdeaCategoriesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordIdeaCategoriesResponse>("GetKeywordIdeaCategories", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordIdeasResponse GetKeywordIdeas(GetKeywordIdeasRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordIdeasResponse> GetKeywordIdeasAsync(GetKeywordIdeasRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordIdeasResponse>("GetKeywordIdeas", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordTrafficEstimatesResponse GetKeywordTrafficEstimates(GetKeywordTrafficEstimatesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordTrafficEstimatesResponse> GetKeywordTrafficEstimatesAsync(GetKeywordTrafficEstimatesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordTrafficEstimatesResponse>("GetKeywordTrafficEstimates", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAutoApplyOptInStatusResponse GetAutoApplyOptInStatus(GetAutoApplyOptInStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAutoApplyOptInStatusResponse> GetAutoApplyOptInStatusAsync(GetAutoApplyOptInStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAutoApplyOptInStatusResponse>("GetAutoApplyOptInStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetAutoApplyOptInStatusResponse SetAutoApplyOptInStatus(SetAutoApplyOptInStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetAutoApplyOptInStatusResponse> SetAutoApplyOptInStatusAsync(SetAutoApplyOptInStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetAutoApplyOptInStatusResponse>("SetAutoApplyOptInStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetPerformanceInsightsDetailDataByAccountIdResponse GetPerformanceInsightsDetailDataByAccountId(GetPerformanceInsightsDetailDataByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetPerformanceInsightsDetailDataByAccountIdResponse> GetPerformanceInsightsDetailDataByAccountIdAsync(GetPerformanceInsightsDetailDataByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetPerformanceInsightsDetailDataByAccountIdResponse>("GetPerformanceInsightsDetailDataByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetRecommendationsResponse GetRecommendations(GetRecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetRecommendationsResponse> GetRecommendationsAsync(GetRecommendationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetRecommendationsResponse>("GetRecommendations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public TagRecommendationsResponse TagRecommendations(TagRecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<TagRecommendationsResponse> TagRecommendationsAsync(TagRecommendationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<TagRecommendationsResponse>("TagRecommendations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetTextAssetSuggestionsByFinalUrlsResponse GetTextAssetSuggestionsByFinalUrls(GetTextAssetSuggestionsByFinalUrlsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetTextAssetSuggestionsByFinalUrlsResponse> GetTextAssetSuggestionsByFinalUrlsAsync(GetTextAssetSuggestionsByFinalUrlsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetTextAssetSuggestionsByFinalUrlsResponse>("GetTextAssetSuggestionsByFinalUrls", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyRecommendationsResponse ApplyRecommendations(ApplyRecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyRecommendationsResponse> ApplyRecommendationsAsync(ApplyRecommendationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyRecommendationsResponse>("ApplyRecommendations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DismissRecommendationsResponse DismissRecommendations(DismissRecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DismissRecommendationsResponse> DismissRecommendationsAsync(DismissRecommendationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DismissRecommendationsResponse>("DismissRecommendations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public RetrieveRecommendationsResponse RetrieveRecommendations(RetrieveRecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RetrieveRecommendationsResponse> RetrieveRecommendationsAsync(RetrieveRecommendationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<RetrieveRecommendationsResponse>("RetrieveRecommendations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAudienceFullEstimationResponse GetAudienceFullEstimation(GetAudienceFullEstimationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAudienceFullEstimationResponse> GetAudienceFullEstimationAsync(GetAudienceFullEstimationRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAudienceFullEstimationResponse>("GetAudienceFullEstimation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}