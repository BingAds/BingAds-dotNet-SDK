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

namespace Microsoft.BingAds.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

public static partial class RestApiGeneration
{
    public partial class OpenApiKnownTypesMapping
    {
        public static Dictionary<Type, Dictionary<Type, string>> TypesMap = new Dictionary<Type, Dictionary<Type, string>>
        {
            // Microsoft.BingAds.V13.AdInsight entities
            { typeof(Microsoft.BingAds.V13.AdInsight.ApplicationFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.AdApiFaultDetail), "AdApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.AdInsight.ApiFaultDetail), "ApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.AdInsight.ApplicationFault), "ApplicationFault" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.Breakdown), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.LocationBreakdown), "LocationBreakdown" },
                { typeof(Microsoft.BingAds.V13.AdInsight.Breakdown), "Breakdown" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.Criterion), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.LocationCriterion), "LocationCriterion" },
                { typeof(Microsoft.BingAds.V13.AdInsight.DeviceCriterion), "DeviceCriterion" },
                { typeof(Microsoft.BingAds.V13.AdInsight.NetworkCriterion), "NetworkCriterion" },
                { typeof(Microsoft.BingAds.V13.AdInsight.LanguageCriterion), "LanguageCriterion" },
                { typeof(Microsoft.BingAds.V13.AdInsight.Criterion), "Criterion" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.KeywordOpportunity), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.BroadMatchKeywordOpportunity), "BroadMatchKeywordOpportunity" },
                { typeof(Microsoft.BingAds.V13.AdInsight.KeywordOpportunity), "KeywordOpportunity" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.Opportunity), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.BidOpportunity), "BidOpportunity" },
                { typeof(Microsoft.BingAds.V13.AdInsight.BroadMatchKeywordOpportunity), "BroadMatchKeywordOpportunity" },
                { typeof(Microsoft.BingAds.V13.AdInsight.KeywordOpportunity), "KeywordOpportunity" },
                { typeof(Microsoft.BingAds.V13.AdInsight.BudgetOpportunity), "BudgetOpportunity" },
                { typeof(Microsoft.BingAds.V13.AdInsight.Opportunity), "Opportunity" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.PerformanceInsightsMessageParameter), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.EntityParameter), "Entities" },
                { typeof(Microsoft.BingAds.V13.AdInsight.UrlParameter), "Url" },
                { typeof(Microsoft.BingAds.V13.AdInsight.TextParameter), "Text" },
                { typeof(Microsoft.BingAds.V13.AdInsight.PerformanceInsightsMessageParameter), "PerformanceInsightsMessageParameter" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.Recommendation), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.ResponsiveSearchAdsRecommendation), "ResponsiveSearchAdsRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.Recommendation), "Recommendation" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.RecommendationBase), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.ResponsiveSearchAdAssetRecommendation), "ResponsiveSearchAdAssetRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.UseBroadMatchKeywordRecommendation), "UseBroadMatchKeywordRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.RemoveConflictingNegativeKeywordRecommendation), "RemoveConflictingNegativeKeywordRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.ResponsiveSearchAdRecommendation), "ResponsiveSearchAdRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.KeywordRecommendation), "KeywordRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.CampaignBudgetRecommendation), "CampaignBudgetRecommendation" },
                { typeof(Microsoft.BingAds.V13.AdInsight.RecommendationBase), "RecommendationBase" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.RecommendationInfo), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.RSARecommendationInfo), "RSARecommendationInfo" },
                { typeof(Microsoft.BingAds.V13.AdInsight.RecommendationInfo), "RecommendationInfo" } }
            },
            { typeof(Microsoft.BingAds.V13.AdInsight.SearchParameter), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.AdInsight.AuctionSegmentSearchParameter), "AuctionSegmentSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.DateRangeSearchParameter), "DateRangeSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.CompetitionSearchParameter), "CompetitionSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.LanguageSearchParameter), "LanguageSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.DeviceSearchParameter), "DeviceSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.NetworkSearchParameter), "NetworkSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.LocationSearchParameter), "LocationSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.ImpressionShareSearchParameter), "ImpressionShareSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.ExcludeAccountKeywordsSearchParameter), "ExcludeAccountKeywordsSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.IdeaTextSearchParameter), "IdeaTextSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.SuggestedBidSearchParameter), "SuggestedBidSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.SearchVolumeSearchParameter), "SearchVolumeSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.CategorySearchParameter), "CategorySearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.UrlSearchParameter), "UrlSearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.QuerySearchParameter), "QuerySearchParameter" },
                { typeof(Microsoft.BingAds.V13.AdInsight.SearchParameter), "SearchParameter" } }
            },

            // Microsoft.BingAds.V13.Bulk entities
            { typeof(Microsoft.BingAds.V13.Bulk.ApplicationFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.Bulk.AdApiFaultDetail), "AdApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.Bulk.ApiFaultDetail), "ApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.Bulk.ApplicationFault), "ApplicationFault" } }
            },
            { typeof(Microsoft.BingAds.V13.Bulk.BatchError), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.Bulk.EditorialError), "EditorialError" },
                { typeof(Microsoft.BingAds.V13.Bulk.BatchError), "BatchError" } }
            },

            // Microsoft.BingAds.V13.CampaignManagement entities
            { typeof(Microsoft.BingAds.V13.CampaignManagement.Ad), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ResponsiveSearchAd), "ResponsiveSearch" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ResponsiveAd), "ResponsiveAd" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DynamicSearchAd), "DynamicSearch" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ExpandedTextAd), "ExpandedText" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AppInstallAd), "AppInstall" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelAd), "Hotel" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ProductAd), "Product" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TextAd), "Text" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Ad), "Ad" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.AdExtension), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.LogoAdExtension), "LogoAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DisclaimerAdExtension), "DisclaimerAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.VideoAdExtension), "VideoAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.FlyerAdExtension), "FlyerAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.FilterLinkAdExtension), "FilterLinkAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PromotionAdExtension), "PromotionAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PriceAdExtension), "PriceAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.StructuredSnippetAdExtension), "StructuredSnippetAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ActionAdExtension), "ActionAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.SitelinkAdExtension), "SitelinkAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CalloutAdExtension), "CalloutAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ReviewAdExtension), "ReviewAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AppAdExtension), "AppAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ImageAdExtension), "ImageAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CallAdExtension), "CallAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.LocationAdExtension), "LocationAdExtension" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AdExtension), "AdExtension" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.AdGroupCriterion), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NegativeAdGroupCriterion), "NegativeAdGroupCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BiddableAdGroupCriterion), "BiddableAdGroupCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AdGroupCriterion), "AdGroupCriterion" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.ApplicationFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AdApiFaultDetail), "AdApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.EditorialApiFaultDetail), "EditorialApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ApiFaultDetail), "ApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ApplicationFault), "ApplicationFault" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.Asset), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.VideoAsset), "VideoAsset" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ImageAsset), "ImageAsset" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TextAsset), "TextAsset" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Asset), "Asset" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.Audience), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CustomSegment), "CustomSegment" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ImpressionBasedRemarketingList), "ImpressionBasedRemarketingList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CustomerList), "CustomerList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CombinedList), "CombinedList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.SimilarRemarketingList), "SimilarRemarketingList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ProductAudience), "Product" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.InMarketAudience), "InMarket" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CustomAudience), "Custom" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.RemarketingList), "RemarketingList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Audience), "Audience" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.AudienceGroupDimension), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ProfileDimension), "Profile" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AudienceDimension), "Audience" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.GenderDimension), "Gender" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AgeDimension), "Age" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AudienceGroupDimension), "AudienceGroupDimension" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.BatchError), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.EditorialError), "EditorialError" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BatchError), "BatchError" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.BatchErrorCollection), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.EditorialErrorCollection), "EditorialErrorCollection" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BatchErrorCollection), "BatchErrorCollection" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.BiddingScheme), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CostPerSaleBiddingScheme), "CostPerSale" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ManualCpaBiddingScheme), "ManualCpaBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CommissionBiddingScheme), "CommissionBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PercentCpcBiddingScheme), "PercentCpcBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TargetImpressionShareBiddingScheme), "TargetImpressionShareBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.MaxConversionValueBiddingScheme), "MaxConversionValueBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.MaxRoasBiddingScheme), "MaxRoasBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TargetRoasBiddingScheme), "TargetRoasBiddingScheme" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.InheritFromParentBiddingScheme), "InheritFromParent" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ManualCpmBiddingScheme), "ManualCpm" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ManualCpvBiddingScheme), "ManualCpv" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.EnhancedCpcBiddingScheme), "EnhancedCpc" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ManualCpcBiddingScheme), "ManualCpc" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TargetCpaBiddingScheme), "TargetCpa" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.MaxConversionsBiddingScheme), "MaxConversions" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.MaxClicksBiddingScheme), "MaxClicks" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BiddingScheme), "BiddingScheme" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.CampaignCriterion), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BiddableCampaignCriterion), "BiddableCampaignCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NegativeCampaignCriterion), "NegativeCampaignCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CampaignCriterion), "CampaignCriterion" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.ConversionGoal), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AppDownloadGoal), "AppDownload" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.InStoreTransactionGoal), "InStoreTransaction" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.OfflineConversionGoal), "OfflineConversion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AppInstallGoal), "AppInstall" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.EventGoal), "Event" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PagesViewedPerVisitGoal), "PagesViewedPerVisit" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DurationGoal), "Duration" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.UrlGoal), "Url" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ConversionGoal), "ConversionGoal" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.Criterion), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TopicCriterion), "TopicCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PlacementCriterion), "PlacementCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.GenreCriterion), "GenreCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DealCriterion), "DealCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.StoreCriterion), "StoreCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ProfileCriterion), "ProfileCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AudienceCriterion), "AudienceCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.LocationIntentCriterion), "LocationIntentCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.LocationCriterion), "LocationCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.RadiusCriterion), "RadiusCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.GenderCriterion), "GenderCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DayTimeCriterion), "DayTimeCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DeviceCriterion), "DeviceCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AgeCriterion), "AgeCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Webpage), "Webpage" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ProductScope), "ProductScope" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelLengthOfStayCriterion), "HotelLengthOfStayCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelDateSelectionTypeCriterion), "HotelDateSelectionTypeCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelCheckInDayCriterion), "HotelCheckInDayCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelCheckInDateCriterion), "HotelCheckInDateCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelAdvanceBookingWindowCriterion), "HotelAdvanceBookingWindowCriterion" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelGroup), "HotelGroup" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ProductPartition), "ProductPartition" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Criterion), "Criterion" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.CriterionBid), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.RateBid), "RateBid" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BidMultiplier), "BidMultiplier" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.FixedBid), "FixedBid" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CriterionBid), "CriterionBid" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.CriterionCashback), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CashbackAdjustment), "CashbackAdjustment" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CriterionCashback), "CriterionCashback" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.ImportJob), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.FileImportJob), "FileImportJob" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.GoogleImportJob), "GoogleImportJob" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ImportJob), "ImportJob" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.ImportOption), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.FileImportOption), "FileImportOption" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.GoogleImportOption), "GoogleImportOption" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ImportOption), "ImportOption" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.LinkedInSegment), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CompanyList), "CompanyList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.LinkedInSegment), "LinkedInSegment" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.Media), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Image), "Image" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Media), "Media" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.MediaRepresentation), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ImageMediaRepresentation), "ImageMediaRepresentation" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.MediaRepresentation), "MediaRepresentation" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.RemarketingRule), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CustomEventsRule), "CustomEvents" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PageVisitorsWhoDidNotVisitAnotherPageRule), "PageVisitorsWhoDidNotVisitAnotherPage" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PageVisitorsWhoVisitedAnotherPageRule), "PageVisitorsWhoVisitedAnotherPage" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PageVisitorsRule), "PageVisitors" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.RemarketingRule), "RemarketingRule" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.RuleItem), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NumberRuleItem), "Number" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.StringRuleItem), "String" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.RuleItem), "RuleItem" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.Setting), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NewCustomerAcquisitionGoalSetting), "NewCustomerAcquisitionGoalSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ThirdPartyMeasurementSetting), "ThirdPartyMeasurementSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AppSetting), "AppSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.VanityPharmaSetting), "VanityPharmaSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CallToActionSetting), "CallToActionSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PerformanceMaxSetting), "PerformanceMaxSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ResponsiveSearchAdsSetting), "ResponsiveSearchAdsSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.HotelSetting), "HotelSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DisclaimerSetting), "DisclaimerSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.CoOpSetting), "CoOpSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.TargetSetting), "TargetSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DynamicSearchAdsSetting), "DynamicSearchAdsSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.DynamicFeedSetting), "DynamicFeedSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.ShoppingSetting), "ShoppingSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.VerifiedTrackingSetting), "VerifiedTrackingSetting" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Setting), "Setting" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedEntity), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AccountPlacementInclusionList), "AccountPlacementInclusionList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AccountPlacementExclusionList), "AccountPlacementExclusionList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BrandList), "BrandList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AccountNegativeKeywordList), "AccountNegativeKeywordList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PlacementExclusionList), "PlacementExclusionList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NegativeKeywordList), "NegativeKeywordList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedList), "SharedList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedEntity), "SharedEntity" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedList), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AccountPlacementInclusionList), "AccountPlacementInclusionList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AccountPlacementExclusionList), "AccountPlacementExclusionList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BrandList), "BrandList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.AccountNegativeKeywordList), "AccountNegativeKeywordList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.PlacementExclusionList), "PlacementExclusionList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NegativeKeywordList), "NegativeKeywordList" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedList), "SharedList" } }
            },
            { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedListItem), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NegativeKeyword), "NegativeKeyword" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.Site), "Site" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.BrandItem), "BrandItem" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.NegativeSite), "NegativeSite" },
                { typeof(Microsoft.BingAds.V13.CampaignManagement.SharedListItem), "SharedListItem" } }
            },

            // Microsoft.BingAds.V13.CustomerBilling entities
            { typeof(Microsoft.BingAds.V13.CustomerBilling.ApiFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CustomerBilling.ApiBatchFault), "ApiBatchFault" },
                { typeof(Microsoft.BingAds.V13.CustomerBilling.ApiFault), "ApiFault" } }
            },
            { typeof(Microsoft.BingAds.V13.CustomerBilling.ApplicationFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CustomerBilling.AdApiFaultDetail), "AdApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.CustomerBilling.ApiBatchFault), "ApiBatchFault" },
                { typeof(Microsoft.BingAds.V13.CustomerBilling.ApiFault), "ApiFault" },
                { typeof(Microsoft.BingAds.V13.CustomerBilling.ApplicationFault), "ApplicationFault" } }
            },

            // Microsoft.BingAds.V13.CustomerManagement entities
            { typeof(Microsoft.BingAds.V13.CustomerManagement.ApplicationFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.CustomerManagement.AdApiFaultDetail), "AdApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.CustomerManagement.ApiFault), "ApiFault" },
                { typeof(Microsoft.BingAds.V13.CustomerManagement.ApplicationFault), "ApplicationFault" } }
            },

            // Microsoft.BingAds.V13.Reporting entities
            { typeof(Microsoft.BingAds.V13.Reporting.ApplicationFault), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.Reporting.AdApiFaultDetail), "AdApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.Reporting.ApiFaultDetail), "ApiFaultDetail" },
                { typeof(Microsoft.BingAds.V13.Reporting.ApplicationFault), "ApplicationFault" } }
            },
            { typeof(Microsoft.BingAds.V13.Reporting.ReportRequest), new Dictionary<Type, string> {                
                { typeof(Microsoft.BingAds.V13.Reporting.BidStrategyReportRequest), "BidStrategyReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.TravelQueryInsightReportRequest), "TravelQueryInsightReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.FeedItemPerformanceReportRequest), "FeedItemPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AppsPerformanceReportRequest), "AppsPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.CombinationPerformanceReportRequest), "CombinationPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.CategoryClickCoverageReportRequest), "CategoryClickCoverageReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.CategoryInsightsReportRequest), "CategoryInsightsReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AssetPerformanceReportRequest), "AssetPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.SearchInsightPerformanceReportRequest), "SearchInsightPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AssetGroupPerformanceReportRequest), "AssetGroupPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.HotelGroupPerformanceReportRequest), "HotelGroupPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.HotelDimensionPerformanceReportRequest), "HotelDimensionPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.DSACategoryPerformanceReportRequest), "DSACategoryPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.DSAAutoTargetPerformanceReportRequest), "DSAAutoTargetPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.DSASearchQueryPerformanceReportRequest), "DSASearchQueryPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.GeographicPerformanceReportRequest), "GeographicPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.CallDetailReportRequest), "CallDetailReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProductNegativeKeywordConflictReportRequest), "ProductNegativeKeywordConflictReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProductMatchCountReportRequest), "ProductMatchCountReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProductSearchQueryPerformanceReportRequest), "ProductSearchQueryPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProductPartitionUnitPerformanceReportRequest), "ProductPartitionUnitPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProductPartitionPerformanceReportRequest), "ProductPartitionPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProductDimensionPerformanceReportRequest), "ProductDimensionPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ShareOfVoiceReportRequest), "ShareOfVoiceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AdExtensionDetailReportRequest), "AdExtensionDetailReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AudiencePerformanceReportRequest), "AudiencePerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AdExtensionByKeywordReportRequest), "AdExtensionByKeywordReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AdExtensionByAdReportRequest), "AdExtensionByAdReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.SearchCampaignChangeHistoryReportRequest), "SearchCampaignChangeHistoryReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.NegativeKeywordConflictReportRequest), "NegativeKeywordConflictReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.GoalsAndFunnelsReportRequest), "GoalsAndFunnelsReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ConversionPerformanceReportRequest), "ConversionPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.SearchQueryPerformanceReportRequest), "SearchQueryPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.PublisherUsagePerformanceReportRequest), "PublisherUsagePerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.UserLocationPerformanceReportRequest), "UserLocationPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ProfessionalDemographicsAudienceReportRequest), "ProfessionalDemographicsAudienceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AgeGenderAudienceReportRequest), "AgeGenderAudienceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.BudgetSummaryReportRequest), "BudgetSummaryReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.DestinationUrlPerformanceReportRequest), "DestinationUrlPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.KeywordPerformanceReportRequest), "KeywordPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AdPerformanceReportRequest), "AdPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AdGroupPerformanceReportRequest), "AdGroupPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AdDynamicTextPerformanceReportRequest), "AdDynamicTextPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.CampaignPerformanceReportRequest), "CampaignPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.AccountPerformanceReportRequest), "AccountPerformanceReportRequest" },
                { typeof(Microsoft.BingAds.V13.Reporting.ReportRequest), "ReportRequest" } }
            },

       };
    }
}