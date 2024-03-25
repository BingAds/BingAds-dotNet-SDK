#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.BingAds.V13.Reporting
{
    public class AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new ReportRequestConverter(originalOptions, createUnsupportedTypeValueException));
        }
    }

    class ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public ApplicationFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
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

    class ReportRequestConverter : JsonConverter<ReportRequest>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public ReportRequestConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
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