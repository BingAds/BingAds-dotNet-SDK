using System.Threading.Tasks;
using Microsoft.BingAds.V13.AdInsight;

namespace Microsoft.BingAds
{
    public static partial class ServiceClientExtensions
    {
        public static Task<GetBidOpportunitiesResponse> GetBidOpportunitiesAsync(this ServiceClient<IAdInsightService> service, GetBidOpportunitiesRequest request)
        {
            return service.CallAsync((s, r) => s.GetBidOpportunitiesAsync(r), request);
        }

        public static Task<GetBudgetOpportunitiesResponse> GetBudgetOpportunitiesAsync(this ServiceClient<IAdInsightService> service, GetBudgetOpportunitiesRequest request)
        {
            return service.CallAsync((s, r) => s.GetBudgetOpportunitiesAsync(r), request);
        }

        public static Task<GetKeywordOpportunitiesResponse> GetKeywordOpportunitiesAsync(this ServiceClient<IAdInsightService> service, GetKeywordOpportunitiesRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordOpportunitiesAsync(r), request);
        }

        public static Task<GetEstimatedBidByKeywordIdsResponse> GetEstimatedBidByKeywordIdsAsync(this ServiceClient<IAdInsightService> service, GetEstimatedBidByKeywordIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetEstimatedBidByKeywordIdsAsync(r), request);
        }

        public static Task<GetEstimatedPositionByKeywordIdsResponse> GetEstimatedPositionByKeywordIdsAsync(this ServiceClient<IAdInsightService> service, GetEstimatedPositionByKeywordIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetEstimatedPositionByKeywordIdsAsync(r), request);
        }

        public static Task<GetEstimatedBidByKeywordsResponse> GetEstimatedBidByKeywordsAsync(this ServiceClient<IAdInsightService> service, GetEstimatedBidByKeywordsRequest request)
        {
            return service.CallAsync((s, r) => s.GetEstimatedBidByKeywordsAsync(r), request);
        }

        public static Task<GetEstimatedPositionByKeywordsResponse> GetEstimatedPositionByKeywordsAsync(this ServiceClient<IAdInsightService> service, GetEstimatedPositionByKeywordsRequest request)
        {
            return service.CallAsync((s, r) => s.GetEstimatedPositionByKeywordsAsync(r), request);
        }

        public static Task<GetBidLandscapeByAdGroupIdsResponse> GetBidLandscapeByAdGroupIdsAsync(this ServiceClient<IAdInsightService> service, GetBidLandscapeByAdGroupIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBidLandscapeByAdGroupIdsAsync(r), request);
        }

        public static Task<GetBidLandscapeByKeywordIdsResponse> GetBidLandscapeByKeywordIdsAsync(this ServiceClient<IAdInsightService> service, GetBidLandscapeByKeywordIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBidLandscapeByKeywordIdsAsync(r), request);
        }

        public static Task<GetHistoricalKeywordPerformanceResponse> GetHistoricalKeywordPerformanceAsync(this ServiceClient<IAdInsightService> service, GetHistoricalKeywordPerformanceRequest request)
        {
            return service.CallAsync((s, r) => s.GetHistoricalKeywordPerformanceAsync(r), request);
        }

        public static Task<GetHistoricalSearchCountResponse> GetHistoricalSearchCountAsync(this ServiceClient<IAdInsightService> service, GetHistoricalSearchCountRequest request)
        {
            return service.CallAsync((s, r) => s.GetHistoricalSearchCountAsync(r), request);
        }

        public static Task<GetKeywordCategoriesResponse> GetKeywordCategoriesAsync(this ServiceClient<IAdInsightService> service, GetKeywordCategoriesRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordCategoriesAsync(r), request);
        }

        public static Task<GetKeywordDemographicsResponse> GetKeywordDemographicsAsync(this ServiceClient<IAdInsightService> service, GetKeywordDemographicsRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordDemographicsAsync(r), request);
        }

        public static Task<GetKeywordLocationsResponse> GetKeywordLocationsAsync(this ServiceClient<IAdInsightService> service, GetKeywordLocationsRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordLocationsAsync(r), request);
        }

        public static Task<SuggestKeywordsForUrlResponse> SuggestKeywordsForUrlAsync(this ServiceClient<IAdInsightService> service, SuggestKeywordsForUrlRequest request)
        {
            return service.CallAsync((s, r) => s.SuggestKeywordsForUrlAsync(r), request);
        }

        public static Task<SuggestKeywordsFromExistingKeywordsResponse> SuggestKeywordsFromExistingKeywordsAsync(this ServiceClient<IAdInsightService> service, SuggestKeywordsFromExistingKeywordsRequest request)
        {
            return service.CallAsync((s, r) => s.SuggestKeywordsFromExistingKeywordsAsync(r), request);
        }

        public static Task<GetAuctionInsightDataResponse> GetAuctionInsightDataAsync(this ServiceClient<IAdInsightService> service, GetAuctionInsightDataRequest request)
        {
            return service.CallAsync((s, r) => s.GetAuctionInsightDataAsync(r), request);
        }

        public static Task<GetDomainCategoriesResponse> GetDomainCategoriesAsync(this ServiceClient<IAdInsightService> service, GetDomainCategoriesRequest request)
        {
            return service.CallAsync((s, r) => s.GetDomainCategoriesAsync(r), request);
        }

        public static Task<PutMetricDataResponse> PutMetricDataAsync(this ServiceClient<IAdInsightService> service, PutMetricDataRequest request)
        {
            return service.CallAsync((s, r) => s.PutMetricDataAsync(r), request);
        }

        public static Task<GetKeywordIdeaCategoriesResponse> GetKeywordIdeaCategoriesAsync(this ServiceClient<IAdInsightService> service, GetKeywordIdeaCategoriesRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordIdeaCategoriesAsync(r), request);
        }

        public static Task<GetKeywordIdeasResponse> GetKeywordIdeasAsync(this ServiceClient<IAdInsightService> service, GetKeywordIdeasRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordIdeasAsync(r), request);
        }

        public static Task<GetKeywordTrafficEstimatesResponse> GetKeywordTrafficEstimatesAsync(this ServiceClient<IAdInsightService> service, GetKeywordTrafficEstimatesRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordTrafficEstimatesAsync(r), request);
        }

        public static Task<GetAutoApplyOptInStatusResponse> GetAutoApplyOptInStatusAsync(this ServiceClient<IAdInsightService> service, GetAutoApplyOptInStatusRequest request)
        {
            return service.CallAsync((s, r) => s.GetAutoApplyOptInStatusAsync(r), request);
        }

        public static Task<SetAutoApplyOptInStatusResponse> SetAutoApplyOptInStatusAsync(this ServiceClient<IAdInsightService> service, SetAutoApplyOptInStatusRequest request)
        {
            return service.CallAsync((s, r) => s.SetAutoApplyOptInStatusAsync(r), request);
        }

        public static Task<GetPerformanceInsightsDetailDataByAccountIdResponse> GetPerformanceInsightsDetailDataByAccountIdAsync(this ServiceClient<IAdInsightService> service, GetPerformanceInsightsDetailDataByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetPerformanceInsightsDetailDataByAccountIdAsync(r), request);
        }

        public static Task<GetRecommendationsResponse> GetRecommendationsAsync(this ServiceClient<IAdInsightService> service, GetRecommendationsRequest request)
        {
            return service.CallAsync((s, r) => s.GetRecommendationsAsync(r), request);
        }

        public static Task<TagRecommendationsResponse> TagRecommendationsAsync(this ServiceClient<IAdInsightService> service, TagRecommendationsRequest request)
        {
            return service.CallAsync((s, r) => s.TagRecommendationsAsync(r), request);
        }

        public static Task<GetTextAssetSuggestionsByFinalUrlsResponse> GetTextAssetSuggestionsByFinalUrlsAsync(this ServiceClient<IAdInsightService> service, GetTextAssetSuggestionsByFinalUrlsRequest request)
        {
            return service.CallAsync((s, r) => s.GetTextAssetSuggestionsByFinalUrlsAsync(r), request);
        }

        public static Task<ApplyRecommendationsResponse> ApplyRecommendationsAsync(this ServiceClient<IAdInsightService> service, ApplyRecommendationsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyRecommendationsAsync(r), request);
        }

        public static Task<DismissRecommendationsResponse> DismissRecommendationsAsync(this ServiceClient<IAdInsightService> service, DismissRecommendationsRequest request)
        {
            return service.CallAsync((s, r) => s.DismissRecommendationsAsync(r), request);
        }

        public static Task<RetrieveRecommendationsResponse> RetrieveRecommendationsAsync(this ServiceClient<IAdInsightService> service, RetrieveRecommendationsRequest request)
        {
            return service.CallAsync((s, r) => s.RetrieveRecommendationsAsync(r), request);
        }

        public static Task<GetAudienceFullEstimationResponse> GetAudienceFullEstimationAsync(this ServiceClient<IAdInsightService> service, GetAudienceFullEstimationRequest request)
        {
            return service.CallAsync((s, r) => s.GetAudienceFullEstimationAsync(r), request);
        }
    }
}