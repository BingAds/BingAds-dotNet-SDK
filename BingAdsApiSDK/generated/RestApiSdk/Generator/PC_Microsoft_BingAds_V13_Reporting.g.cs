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

#nullable enable

namespace Microsoft.BingAds.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.BingAds.V13.Reporting;

public static partial class RestApiGeneration
{
    public class Microsoft_BingAds_V13_Reporting_AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new Microsoft_BingAds_V13_Reporting_ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_Reporting_ReportRequestConverter(originalOptions, createUnsupportedTypeValueException));
        }
    }

    class Microsoft_BingAds_V13_Reporting_ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_Reporting_ApplicationFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ApplicationFault? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AdApiFaultDetail" => jsonObj.Deserialize<AdApiFaultDetail>(options),
                "ApiFaultDetail" => jsonObj.Deserialize<ApiFaultDetail>(options),
                "ApplicationFault" => jsonObj.Deserialize<ApplicationFault>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ApplicationFault value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AdApiFaultDetail adApiFaultDetail:
                    JsonSerializer.Serialize(writer, adApiFaultDetail, options);
                    break;
                case ApiFaultDetail apiFaultDetail:
                    JsonSerializer.Serialize(writer, apiFaultDetail, options);
                    break;
                case ApplicationFault applicationFault:
                    JsonSerializer.Serialize(writer, applicationFault, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_Reporting_ReportRequestConverter : JsonConverter<ReportRequest>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_Reporting_ReportRequestConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ReportRequest? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "BidStrategyReportRequest" => jsonObj.Deserialize<BidStrategyReportRequest>(options),
                "TravelQueryInsightReportRequest" => jsonObj.Deserialize<TravelQueryInsightReportRequest>(options),
                "FeedItemPerformanceReportRequest" => jsonObj.Deserialize<FeedItemPerformanceReportRequest>(options),
                "AppsPerformanceReportRequest" => jsonObj.Deserialize<AppsPerformanceReportRequest>(options),
                "CombinationPerformanceReportRequest" => jsonObj.Deserialize<CombinationPerformanceReportRequest>(options),
                "CategoryClickCoverageReportRequest" => jsonObj.Deserialize<CategoryClickCoverageReportRequest>(options),
                "CategoryInsightsReportRequest" => jsonObj.Deserialize<CategoryInsightsReportRequest>(options),
                "AssetPerformanceReportRequest" => jsonObj.Deserialize<AssetPerformanceReportRequest>(options),
                "SearchInsightPerformanceReportRequest" => jsonObj.Deserialize<SearchInsightPerformanceReportRequest>(options),
                "AssetGroupPerformanceReportRequest" => jsonObj.Deserialize<AssetGroupPerformanceReportRequest>(options),
                "HotelGroupPerformanceReportRequest" => jsonObj.Deserialize<HotelGroupPerformanceReportRequest>(options),
                "HotelDimensionPerformanceReportRequest" => jsonObj.Deserialize<HotelDimensionPerformanceReportRequest>(options),
                "DSACategoryPerformanceReportRequest" => jsonObj.Deserialize<DSACategoryPerformanceReportRequest>(options),
                "DSAAutoTargetPerformanceReportRequest" => jsonObj.Deserialize<DSAAutoTargetPerformanceReportRequest>(options),
                "DSASearchQueryPerformanceReportRequest" => jsonObj.Deserialize<DSASearchQueryPerformanceReportRequest>(options),
                "GeographicPerformanceReportRequest" => jsonObj.Deserialize<GeographicPerformanceReportRequest>(options),
                "CallDetailReportRequest" => jsonObj.Deserialize<CallDetailReportRequest>(options),
                "ProductNegativeKeywordConflictReportRequest" => jsonObj.Deserialize<ProductNegativeKeywordConflictReportRequest>(options),
                "ProductMatchCountReportRequest" => jsonObj.Deserialize<ProductMatchCountReportRequest>(options),
                "ProductSearchQueryPerformanceReportRequest" => jsonObj.Deserialize<ProductSearchQueryPerformanceReportRequest>(options),
                "ProductPartitionUnitPerformanceReportRequest" => jsonObj.Deserialize<ProductPartitionUnitPerformanceReportRequest>(options),
                "ProductPartitionPerformanceReportRequest" => jsonObj.Deserialize<ProductPartitionPerformanceReportRequest>(options),
                "ProductDimensionPerformanceReportRequest" => jsonObj.Deserialize<ProductDimensionPerformanceReportRequest>(options),
                "ShareOfVoiceReportRequest" => jsonObj.Deserialize<ShareOfVoiceReportRequest>(options),
                "AdExtensionDetailReportRequest" => jsonObj.Deserialize<AdExtensionDetailReportRequest>(options),
                "AudiencePerformanceReportRequest" => jsonObj.Deserialize<AudiencePerformanceReportRequest>(options),
                "AdExtensionByKeywordReportRequest" => jsonObj.Deserialize<AdExtensionByKeywordReportRequest>(options),
                "AdExtensionByAdReportRequest" => jsonObj.Deserialize<AdExtensionByAdReportRequest>(options),
                "SearchCampaignChangeHistoryReportRequest" => jsonObj.Deserialize<SearchCampaignChangeHistoryReportRequest>(options),
                "NegativeKeywordConflictReportRequest" => jsonObj.Deserialize<NegativeKeywordConflictReportRequest>(options),
                "GoalsAndFunnelsReportRequest" => jsonObj.Deserialize<GoalsAndFunnelsReportRequest>(options),
                "ConversionPerformanceReportRequest" => jsonObj.Deserialize<ConversionPerformanceReportRequest>(options),
                "SearchQueryPerformanceReportRequest" => jsonObj.Deserialize<SearchQueryPerformanceReportRequest>(options),
                "PublisherUsagePerformanceReportRequest" => jsonObj.Deserialize<PublisherUsagePerformanceReportRequest>(options),
                "UserLocationPerformanceReportRequest" => jsonObj.Deserialize<UserLocationPerformanceReportRequest>(options),
                "ProfessionalDemographicsAudienceReportRequest" => jsonObj.Deserialize<ProfessionalDemographicsAudienceReportRequest>(options),
                "AgeGenderAudienceReportRequest" => jsonObj.Deserialize<AgeGenderAudienceReportRequest>(options),
                "BudgetSummaryReportRequest" => jsonObj.Deserialize<BudgetSummaryReportRequest>(options),
                "DestinationUrlPerformanceReportRequest" => jsonObj.Deserialize<DestinationUrlPerformanceReportRequest>(options),
                "KeywordPerformanceReportRequest" => jsonObj.Deserialize<KeywordPerformanceReportRequest>(options),
                "AdPerformanceReportRequest" => jsonObj.Deserialize<AdPerformanceReportRequest>(options),
                "AdGroupPerformanceReportRequest" => jsonObj.Deserialize<AdGroupPerformanceReportRequest>(options),
                "AdDynamicTextPerformanceReportRequest" => jsonObj.Deserialize<AdDynamicTextPerformanceReportRequest>(options),
                "CampaignPerformanceReportRequest" => jsonObj.Deserialize<CampaignPerformanceReportRequest>(options),
                "AccountPerformanceReportRequest" => jsonObj.Deserialize<AccountPerformanceReportRequest>(options),
                "ReportRequest" => jsonObj.Deserialize<ReportRequest>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ReportRequest value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case BidStrategyReportRequest bidStrategyReportRequest:
                    JsonSerializer.Serialize(writer, bidStrategyReportRequest, options);
                    break;
                case TravelQueryInsightReportRequest travelQueryInsightReportRequest:
                    JsonSerializer.Serialize(writer, travelQueryInsightReportRequest, options);
                    break;
                case FeedItemPerformanceReportRequest feedItemPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, feedItemPerformanceReportRequest, options);
                    break;
                case AppsPerformanceReportRequest appsPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, appsPerformanceReportRequest, options);
                    break;
                case CombinationPerformanceReportRequest combinationPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, combinationPerformanceReportRequest, options);
                    break;
                case CategoryClickCoverageReportRequest categoryClickCoverageReportRequest:
                    JsonSerializer.Serialize(writer, categoryClickCoverageReportRequest, options);
                    break;
                case CategoryInsightsReportRequest categoryInsightsReportRequest:
                    JsonSerializer.Serialize(writer, categoryInsightsReportRequest, options);
                    break;
                case AssetPerformanceReportRequest assetPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, assetPerformanceReportRequest, options);
                    break;
                case SearchInsightPerformanceReportRequest searchInsightPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, searchInsightPerformanceReportRequest, options);
                    break;
                case AssetGroupPerformanceReportRequest assetGroupPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, assetGroupPerformanceReportRequest, options);
                    break;
                case HotelGroupPerformanceReportRequest hotelGroupPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, hotelGroupPerformanceReportRequest, options);
                    break;
                case HotelDimensionPerformanceReportRequest hotelDimensionPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, hotelDimensionPerformanceReportRequest, options);
                    break;
                case DSACategoryPerformanceReportRequest dSACategoryPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, dSACategoryPerformanceReportRequest, options);
                    break;
                case DSAAutoTargetPerformanceReportRequest dSAAutoTargetPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, dSAAutoTargetPerformanceReportRequest, options);
                    break;
                case DSASearchQueryPerformanceReportRequest dSASearchQueryPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, dSASearchQueryPerformanceReportRequest, options);
                    break;
                case GeographicPerformanceReportRequest geographicPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, geographicPerformanceReportRequest, options);
                    break;
                case CallDetailReportRequest callDetailReportRequest:
                    JsonSerializer.Serialize(writer, callDetailReportRequest, options);
                    break;
                case ProductNegativeKeywordConflictReportRequest productNegativeKeywordConflictReportRequest:
                    JsonSerializer.Serialize(writer, productNegativeKeywordConflictReportRequest, options);
                    break;
                case ProductMatchCountReportRequest productMatchCountReportRequest:
                    JsonSerializer.Serialize(writer, productMatchCountReportRequest, options);
                    break;
                case ProductSearchQueryPerformanceReportRequest productSearchQueryPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, productSearchQueryPerformanceReportRequest, options);
                    break;
                case ProductPartitionUnitPerformanceReportRequest productPartitionUnitPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, productPartitionUnitPerformanceReportRequest, options);
                    break;
                case ProductPartitionPerformanceReportRequest productPartitionPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, productPartitionPerformanceReportRequest, options);
                    break;
                case ProductDimensionPerformanceReportRequest productDimensionPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, productDimensionPerformanceReportRequest, options);
                    break;
                case ShareOfVoiceReportRequest shareOfVoiceReportRequest:
                    JsonSerializer.Serialize(writer, shareOfVoiceReportRequest, options);
                    break;
                case AdExtensionDetailReportRequest adExtensionDetailReportRequest:
                    JsonSerializer.Serialize(writer, adExtensionDetailReportRequest, options);
                    break;
                case AudiencePerformanceReportRequest audiencePerformanceReportRequest:
                    JsonSerializer.Serialize(writer, audiencePerformanceReportRequest, options);
                    break;
                case AdExtensionByKeywordReportRequest adExtensionByKeywordReportRequest:
                    JsonSerializer.Serialize(writer, adExtensionByKeywordReportRequest, options);
                    break;
                case AdExtensionByAdReportRequest adExtensionByAdReportRequest:
                    JsonSerializer.Serialize(writer, adExtensionByAdReportRequest, options);
                    break;
                case SearchCampaignChangeHistoryReportRequest searchCampaignChangeHistoryReportRequest:
                    JsonSerializer.Serialize(writer, searchCampaignChangeHistoryReportRequest, options);
                    break;
                case NegativeKeywordConflictReportRequest negativeKeywordConflictReportRequest:
                    JsonSerializer.Serialize(writer, negativeKeywordConflictReportRequest, options);
                    break;
                case GoalsAndFunnelsReportRequest goalsAndFunnelsReportRequest:
                    JsonSerializer.Serialize(writer, goalsAndFunnelsReportRequest, options);
                    break;
                case ConversionPerformanceReportRequest conversionPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, conversionPerformanceReportRequest, options);
                    break;
                case SearchQueryPerformanceReportRequest searchQueryPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, searchQueryPerformanceReportRequest, options);
                    break;
                case PublisherUsagePerformanceReportRequest publisherUsagePerformanceReportRequest:
                    JsonSerializer.Serialize(writer, publisherUsagePerformanceReportRequest, options);
                    break;
                case UserLocationPerformanceReportRequest userLocationPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, userLocationPerformanceReportRequest, options);
                    break;
                case ProfessionalDemographicsAudienceReportRequest professionalDemographicsAudienceReportRequest:
                    JsonSerializer.Serialize(writer, professionalDemographicsAudienceReportRequest, options);
                    break;
                case AgeGenderAudienceReportRequest ageGenderAudienceReportRequest:
                    JsonSerializer.Serialize(writer, ageGenderAudienceReportRequest, options);
                    break;
                case BudgetSummaryReportRequest budgetSummaryReportRequest:
                    JsonSerializer.Serialize(writer, budgetSummaryReportRequest, options);
                    break;
                case DestinationUrlPerformanceReportRequest destinationUrlPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, destinationUrlPerformanceReportRequest, options);
                    break;
                case KeywordPerformanceReportRequest keywordPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, keywordPerformanceReportRequest, options);
                    break;
                case AdPerformanceReportRequest adPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, adPerformanceReportRequest, options);
                    break;
                case AdGroupPerformanceReportRequest adGroupPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, adGroupPerformanceReportRequest, options);
                    break;
                case AdDynamicTextPerformanceReportRequest adDynamicTextPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, adDynamicTextPerformanceReportRequest, options);
                    break;
                case CampaignPerformanceReportRequest campaignPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, campaignPerformanceReportRequest, options);
                    break;
                case AccountPerformanceReportRequest accountPerformanceReportRequest:
                    JsonSerializer.Serialize(writer, accountPerformanceReportRequest, options);
                    break;
                case ReportRequest reportRequest:
                    JsonSerializer.Serialize(writer, reportRequest, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }
}