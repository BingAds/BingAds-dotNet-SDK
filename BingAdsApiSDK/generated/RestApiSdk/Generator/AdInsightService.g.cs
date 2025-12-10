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

using System;
using Microsoft.BingAds.V13.AdInsight;

namespace Microsoft.BingAds.Internal
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

        public GetBidLandscapeByCampaignIdsResponse GetBidLandscapeByCampaignIds(GetBidLandscapeByCampaignIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBidLandscapeByCampaignIdsResponse> GetBidLandscapeByCampaignIdsAsync(GetBidLandscapeByCampaignIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBidLandscapeByCampaignIdsResponse>("GetBidLandscapeByCampaignIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
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

        public GetAudienceBreakdownResponse GetAudienceBreakdown(GetAudienceBreakdownRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAudienceBreakdownResponse> GetAudienceBreakdownAsync(GetAudienceBreakdownRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAudienceBreakdownResponse>("GetAudienceBreakdown", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}