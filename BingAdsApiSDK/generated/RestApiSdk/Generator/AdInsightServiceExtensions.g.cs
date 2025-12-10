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

        public static Task<GetBidLandscapeByCampaignIdsResponse> GetBidLandscapeByCampaignIdsAsync(this ServiceClient<IAdInsightService> service, GetBidLandscapeByCampaignIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBidLandscapeByCampaignIdsAsync(r), request);
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

        public static Task<GetAudienceBreakdownResponse> GetAudienceBreakdownAsync(this ServiceClient<IAdInsightService> service, GetAudienceBreakdownRequest request)
        {
            return service.CallAsync((s, r) => s.GetAudienceBreakdownAsync(r), request);
        }
    }
}