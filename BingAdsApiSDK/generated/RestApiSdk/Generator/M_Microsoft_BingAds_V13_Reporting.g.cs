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
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Microsoft.BingAds.V13.Reporting;

public static partial class RestApiGeneration
{
    public static class Microsoft_BingAds_V13_Reporting_EntityModifiers
    {
        private static void AddPrivateField(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
        {
            var field = containingType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(field.FieldType, jsonName);
            jsonPropertyInfo.Get = field.GetValue;
            jsonPropertyInfo.Set = field.SetValue;
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }

        private static void AddPrivateProperty(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
        {
            var property = containingType.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(property.PropertyType, jsonName);
            jsonPropertyInfo.Get = property.GetValue;
            jsonPropertyInfo.Set = property.SetValue;
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }

        public static void CustomizeEntities(JsonTypeInfo jsonTypeInfo)
        {
            if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
                return;

            if (CustomizeActions.TryGetValue(jsonTypeInfo.Type, out var customize))
            {
                customize(jsonTypeInfo);
            }
        }

        private static Dictionary<Type, Action<JsonTypeInfo>> CustomizeActions = new Dictionary<Type, Action<JsonTypeInfo>>
        {
            { typeof(AccountPerformanceReportFilter), static t => CustomizeAccountPerformanceReportFilter(t) },
            { typeof(AccountPerformanceReportRequest), static t => CustomizeAccountPerformanceReportRequest(t) },
            { typeof(AccountReportScope), static t => CustomizeAccountReportScope(t) },
            { typeof(AccountThroughAdGroupReportScope), static t => CustomizeAccountThroughAdGroupReportScope(t) },
            { typeof(AccountThroughAssetGroupReportScope), static t => CustomizeAccountThroughAssetGroupReportScope(t) },
            { typeof(AccountThroughCampaignReportScope), static t => CustomizeAccountThroughCampaignReportScope(t) },
            { typeof(AdApiError), static t => CustomizeAdApiError(t) },
            { typeof(AdApiFaultDetail), static t => CustomizeAdApiFaultDetail(t) },
            { typeof(AdDynamicTextPerformanceReportFilter), static t => CustomizeAdDynamicTextPerformanceReportFilter(t) },
            { typeof(AdDynamicTextPerformanceReportRequest), static t => CustomizeAdDynamicTextPerformanceReportRequest(t) },
            { typeof(AdExtensionByAdReportFilter), static t => CustomizeAdExtensionByAdReportFilter(t) },
            { typeof(AdExtensionByAdReportRequest), static t => CustomizeAdExtensionByAdReportRequest(t) },
            { typeof(AdExtensionByKeywordReportFilter), static t => CustomizeAdExtensionByKeywordReportFilter(t) },
            { typeof(AdExtensionByKeywordReportRequest), static t => CustomizeAdExtensionByKeywordReportRequest(t) },
            { typeof(AdExtensionDetailReportFilter), static t => CustomizeAdExtensionDetailReportFilter(t) },
            { typeof(AdExtensionDetailReportRequest), static t => CustomizeAdExtensionDetailReportRequest(t) },
            { typeof(AdGroupPerformanceReportFilter), static t => CustomizeAdGroupPerformanceReportFilter(t) },
            { typeof(AdGroupPerformanceReportRequest), static t => CustomizeAdGroupPerformanceReportRequest(t) },
            { typeof(AdGroupReportScope), static t => CustomizeAdGroupReportScope(t) },
            { typeof(AdPerformanceReportFilter), static t => CustomizeAdPerformanceReportFilter(t) },
            { typeof(AdPerformanceReportRequest), static t => CustomizeAdPerformanceReportRequest(t) },
            { typeof(AgeGenderAudienceReportFilter), static t => CustomizeAgeGenderAudienceReportFilter(t) },
            { typeof(AgeGenderAudienceReportRequest), static t => CustomizeAgeGenderAudienceReportRequest(t) },
            { typeof(ApiFaultDetail), static t => CustomizeApiFaultDetail(t) },
            { typeof(ApplicationFault), static t => CustomizeApplicationFault(t) },
            { typeof(AppsPerformanceReportFilter), static t => CustomizeAppsPerformanceReportFilter(t) },
            { typeof(AppsPerformanceReportRequest), static t => CustomizeAppsPerformanceReportRequest(t) },
            { typeof(AssetGroupPerformanceReportFilter), static t => CustomizeAssetGroupPerformanceReportFilter(t) },
            { typeof(AssetGroupPerformanceReportRequest), static t => CustomizeAssetGroupPerformanceReportRequest(t) },
            { typeof(AssetGroupReportScope), static t => CustomizeAssetGroupReportScope(t) },
            { typeof(AssetPerformanceReportRequest), static t => CustomizeAssetPerformanceReportRequest(t) },
            { typeof(AudiencePerformanceReportFilter), static t => CustomizeAudiencePerformanceReportFilter(t) },
            { typeof(AudiencePerformanceReportRequest), static t => CustomizeAudiencePerformanceReportRequest(t) },
            { typeof(BatchError), static t => CustomizeBatchError(t) },
            { typeof(BidStrategyReportFilter), static t => CustomizeBidStrategyReportFilter(t) },
            { typeof(BidStrategyReportRequest), static t => CustomizeBidStrategyReportRequest(t) },
            { typeof(BudgetSummaryReportRequest), static t => CustomizeBudgetSummaryReportRequest(t) },
            { typeof(CallDetailReportFilter), static t => CustomizeCallDetailReportFilter(t) },
            { typeof(CallDetailReportRequest), static t => CustomizeCallDetailReportRequest(t) },
            { typeof(CampaignPerformanceReportFilter), static t => CustomizeCampaignPerformanceReportFilter(t) },
            { typeof(CampaignPerformanceReportRequest), static t => CustomizeCampaignPerformanceReportRequest(t) },
            { typeof(CampaignReportScope), static t => CustomizeCampaignReportScope(t) },
            { typeof(CategoryClickCoverageReportFilter), static t => CustomizeCategoryClickCoverageReportFilter(t) },
            { typeof(CategoryClickCoverageReportRequest), static t => CustomizeCategoryClickCoverageReportRequest(t) },
            { typeof(CategoryInsightsReportFilter), static t => CustomizeCategoryInsightsReportFilter(t) },
            { typeof(CategoryInsightsReportRequest), static t => CustomizeCategoryInsightsReportRequest(t) },
            { typeof(CombinationPerformanceReportFilter), static t => CustomizeCombinationPerformanceReportFilter(t) },
            { typeof(CombinationPerformanceReportRequest), static t => CustomizeCombinationPerformanceReportRequest(t) },
            { typeof(ConversionPerformanceReportFilter), static t => CustomizeConversionPerformanceReportFilter(t) },
            { typeof(ConversionPerformanceReportRequest), static t => CustomizeConversionPerformanceReportRequest(t) },
            { typeof(Date), static t => CustomizeDate(t) },
            { typeof(DestinationUrlPerformanceReportFilter), static t => CustomizeDestinationUrlPerformanceReportFilter(t) },
            { typeof(DestinationUrlPerformanceReportRequest), static t => CustomizeDestinationUrlPerformanceReportRequest(t) },
            { typeof(DSAAutoTargetPerformanceReportFilter), static t => CustomizeDSAAutoTargetPerformanceReportFilter(t) },
            { typeof(DSAAutoTargetPerformanceReportRequest), static t => CustomizeDSAAutoTargetPerformanceReportRequest(t) },
            { typeof(DSACategoryPerformanceReportFilter), static t => CustomizeDSACategoryPerformanceReportFilter(t) },
            { typeof(DSACategoryPerformanceReportRequest), static t => CustomizeDSACategoryPerformanceReportRequest(t) },
            { typeof(DSASearchQueryPerformanceReportFilter), static t => CustomizeDSASearchQueryPerformanceReportFilter(t) },
            { typeof(DSASearchQueryPerformanceReportRequest), static t => CustomizeDSASearchQueryPerformanceReportRequest(t) },
            { typeof(FeedItemPerformanceReportFilter), static t => CustomizeFeedItemPerformanceReportFilter(t) },
            { typeof(FeedItemPerformanceReportRequest), static t => CustomizeFeedItemPerformanceReportRequest(t) },
            { typeof(GeographicPerformanceReportFilter), static t => CustomizeGeographicPerformanceReportFilter(t) },
            { typeof(GeographicPerformanceReportRequest), static t => CustomizeGeographicPerformanceReportRequest(t) },
            { typeof(GoalsAndFunnelsReportFilter), static t => CustomizeGoalsAndFunnelsReportFilter(t) },
            { typeof(GoalsAndFunnelsReportRequest), static t => CustomizeGoalsAndFunnelsReportRequest(t) },
            { typeof(HotelDimensionPerformanceReportFilter), static t => CustomizeHotelDimensionPerformanceReportFilter(t) },
            { typeof(HotelDimensionPerformanceReportRequest), static t => CustomizeHotelDimensionPerformanceReportRequest(t) },
            { typeof(HotelGroupPerformanceReportFilter), static t => CustomizeHotelGroupPerformanceReportFilter(t) },
            { typeof(HotelGroupPerformanceReportRequest), static t => CustomizeHotelGroupPerformanceReportRequest(t) },
            { typeof(KeywordPerformanceReportFilter), static t => CustomizeKeywordPerformanceReportFilter(t) },
            { typeof(KeywordPerformanceReportRequest), static t => CustomizeKeywordPerformanceReportRequest(t) },
            { typeof(KeywordPerformanceReportSort), static t => CustomizeKeywordPerformanceReportSort(t) },
            { typeof(NegativeKeywordConflictReportFilter), static t => CustomizeNegativeKeywordConflictReportFilter(t) },
            { typeof(NegativeKeywordConflictReportRequest), static t => CustomizeNegativeKeywordConflictReportRequest(t) },
            { typeof(OperationError), static t => CustomizeOperationError(t) },
            { typeof(PollGenerateReportRequest), static t => CustomizePollGenerateReportRequest(t) },
            { typeof(PollGenerateReportResponse), static t => CustomizePollGenerateReportResponse(t) },
            { typeof(ProductDimensionPerformanceReportFilter), static t => CustomizeProductDimensionPerformanceReportFilter(t) },
            { typeof(ProductDimensionPerformanceReportRequest), static t => CustomizeProductDimensionPerformanceReportRequest(t) },
            { typeof(ProductMatchCountReportRequest), static t => CustomizeProductMatchCountReportRequest(t) },
            { typeof(ProductNegativeKeywordConflictReportFilter), static t => CustomizeProductNegativeKeywordConflictReportFilter(t) },
            { typeof(ProductNegativeKeywordConflictReportRequest), static t => CustomizeProductNegativeKeywordConflictReportRequest(t) },
            { typeof(ProductPartitionPerformanceReportFilter), static t => CustomizeProductPartitionPerformanceReportFilter(t) },
            { typeof(ProductPartitionPerformanceReportRequest), static t => CustomizeProductPartitionPerformanceReportRequest(t) },
            { typeof(ProductPartitionUnitPerformanceReportFilter), static t => CustomizeProductPartitionUnitPerformanceReportFilter(t) },
            { typeof(ProductPartitionUnitPerformanceReportRequest), static t => CustomizeProductPartitionUnitPerformanceReportRequest(t) },
            { typeof(ProductSearchQueryPerformanceReportFilter), static t => CustomizeProductSearchQueryPerformanceReportFilter(t) },
            { typeof(ProductSearchQueryPerformanceReportRequest), static t => CustomizeProductSearchQueryPerformanceReportRequest(t) },
            { typeof(ProfessionalDemographicsAudienceReportFilter), static t => CustomizeProfessionalDemographicsAudienceReportFilter(t) },
            { typeof(ProfessionalDemographicsAudienceReportRequest), static t => CustomizeProfessionalDemographicsAudienceReportRequest(t) },
            { typeof(PublisherUsagePerformanceReportFilter), static t => CustomizePublisherUsagePerformanceReportFilter(t) },
            { typeof(PublisherUsagePerformanceReportRequest), static t => CustomizePublisherUsagePerformanceReportRequest(t) },
            { typeof(ReportRequest), static t => CustomizeReportRequest(t) },
            { typeof(ReportRequestStatus), static t => CustomizeReportRequestStatus(t) },
            { typeof(ReportTime), static t => CustomizeReportTime(t) },
            { typeof(SearchCampaignChangeHistoryReportFilter), static t => CustomizeSearchCampaignChangeHistoryReportFilter(t) },
            { typeof(SearchCampaignChangeHistoryReportRequest), static t => CustomizeSearchCampaignChangeHistoryReportRequest(t) },
            { typeof(SearchInsightPerformanceReportFilter), static t => CustomizeSearchInsightPerformanceReportFilter(t) },
            { typeof(SearchInsightPerformanceReportRequest), static t => CustomizeSearchInsightPerformanceReportRequest(t) },
            { typeof(SearchQueryPerformanceReportFilter), static t => CustomizeSearchQueryPerformanceReportFilter(t) },
            { typeof(SearchQueryPerformanceReportRequest), static t => CustomizeSearchQueryPerformanceReportRequest(t) },
            { typeof(ShareOfVoiceReportFilter), static t => CustomizeShareOfVoiceReportFilter(t) },
            { typeof(ShareOfVoiceReportRequest), static t => CustomizeShareOfVoiceReportRequest(t) },
            { typeof(SubmitGenerateReportRequest), static t => CustomizeSubmitGenerateReportRequest(t) },
            { typeof(SubmitGenerateReportResponse), static t => CustomizeSubmitGenerateReportResponse(t) },
            { typeof(TravelQueryInsightReportFilter), static t => CustomizeTravelQueryInsightReportFilter(t) },
            { typeof(TravelQueryInsightReportRequest), static t => CustomizeTravelQueryInsightReportRequest(t) },
            { typeof(UserLocationPerformanceReportFilter), static t => CustomizeUserLocationPerformanceReportFilter(t) },
            { typeof(UserLocationPerformanceReportRequest), static t => CustomizeUserLocationPerformanceReportRequest(t) }
        };

        private static void CustomizeAccountPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAccountPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AccountPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAccountReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAccountThroughAdGroupReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAccountThroughAssetGroupReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAccountThroughCampaignReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdApiError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAdDynamicTextPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdDynamicTextPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdDynamicTextPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAdExtensionByAdReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionByAdReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdExtensionByAdReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAdExtensionByKeywordReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionByKeywordReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdExtensionByKeywordReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAdExtensionDetailReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionDetailReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdExtensionDetailReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAdGroupPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdGroupPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdGroupPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAdGroupReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAgeGenderAudienceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAgeGenderAudienceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AgeGenderAudienceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeApplicationFault(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ApplicationFault";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAppsPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAppsPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AppsPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAssetGroupPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AssetGroupPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAssetGroupReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AssetPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAudiencePerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudiencePerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AudiencePerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeBatchError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBidStrategyReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBidStrategyReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "BidStrategyReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeBudgetSummaryReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "BudgetSummaryReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeCallDetailReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCallDetailReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "CallDetailReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeCampaignPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCampaignPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "CampaignPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeCampaignReportScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCategoryClickCoverageReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCategoryClickCoverageReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "CategoryClickCoverageReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeCategoryInsightsReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCategoryInsightsReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "CategoryInsightsReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeCombinationPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCombinationPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "CombinationPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeConversionPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeConversionPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ConversionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeDate(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDestinationUrlPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDestinationUrlPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "DestinationUrlPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeDSAAutoTargetPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDSAAutoTargetPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "DSAAutoTargetPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeDSACategoryPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDSACategoryPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "DSACategoryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeDSASearchQueryPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDSASearchQueryPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "DSASearchQueryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeFeedItemPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeFeedItemPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "FeedItemPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeGeographicPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGeographicPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "GeographicPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeGoalsAndFunnelsReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AssetGroupStatus":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeGoalsAndFunnelsReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "GoalsAndFunnelsReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeHotelDimensionPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHotelDimensionPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "HotelDimensionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeHotelGroupPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHotelGroupPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "HotelGroupPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeKeywordPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeKeywordPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "KeywordPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeKeywordPerformanceReportSort(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeNegativeKeywordConflictReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeNegativeKeywordConflictReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "NegativeKeywordConflictReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeOperationError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizePollGenerateReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizePollGenerateReportResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductDimensionPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductDimensionPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProductDimensionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeProductMatchCountReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProductMatchCountReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeProductNegativeKeywordConflictReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductNegativeKeywordConflictReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProductNegativeKeywordConflictReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeProductPartitionPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductPartitionPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProductPartitionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeProductPartitionUnitPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductPartitionUnitPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProductPartitionUnitPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeProductSearchQueryPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductSearchQueryPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProductSearchQueryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeProfessionalDemographicsAudienceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProfessionalDemographicsAudienceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ProfessionalDemographicsAudienceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizePublisherUsagePerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizePublisherUsagePerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "PublisherUsagePerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeReportRequestStatus(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeReportTime(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSearchCampaignChangeHistoryReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSearchCampaignChangeHistoryReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "SearchCampaignChangeHistoryReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeSearchInsightPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSearchInsightPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "SearchInsightPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeSearchQueryPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSearchQueryPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "SearchQueryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeShareOfVoiceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeShareOfVoiceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ShareOfVoiceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeSubmitGenerateReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSubmitGenerateReportResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeTravelQueryInsightReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeTravelQueryInsightReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "TravelQueryInsightReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeUserLocationPerformanceReportFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUserLocationPerformanceReportRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "UserLocationPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }
    }
}