#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.BingAds.V13.AdInsight
{
    public class AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new CriterionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new KeywordOpportunityConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new OpportunityConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new PerformanceInsightsMessageParameterConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new RecommendationConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new RecommendationBaseConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new RecommendationInfoConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new SearchParameterConverter(originalOptions, createUnsupportedTypeValueException));
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

    class CriterionConverter : JsonConverter<Criterion>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public CriterionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Criterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "LocationCriterion" => jsonObj.Deserialize<LocationCriterion>(options),
                "DeviceCriterion" => jsonObj.Deserialize<DeviceCriterion>(options),
                "NetworkCriterion" => jsonObj.Deserialize<NetworkCriterion>(options),
                "LanguageCriterion" => jsonObj.Deserialize<LanguageCriterion>(options),
                "Criterion" => jsonObj.Deserialize<Criterion>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Criterion value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case LocationCriterion locationCriterion:
                    JsonSerializer.Serialize(writer, locationCriterion, options);
                    break;
                case DeviceCriterion deviceCriterion:
                    JsonSerializer.Serialize(writer, deviceCriterion, options);
                    break;
                case NetworkCriterion networkCriterion:
                    JsonSerializer.Serialize(writer, networkCriterion, options);
                    break;
                case LanguageCriterion languageCriterion:
                    JsonSerializer.Serialize(writer, languageCriterion, options);
                    break;
                case Criterion criterion:
                    JsonSerializer.Serialize(writer, criterion, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class KeywordOpportunityConverter : JsonConverter<KeywordOpportunity>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public KeywordOpportunityConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override KeywordOpportunity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "BroadMatchKeywordOpportunity" => jsonObj.Deserialize<BroadMatchKeywordOpportunity>(options),
                "KeywordOpportunity" => jsonObj.Deserialize<KeywordOpportunity>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, KeywordOpportunity value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case BroadMatchKeywordOpportunity broadMatchKeywordOpportunity:
                    JsonSerializer.Serialize(writer, broadMatchKeywordOpportunity, options);
                    break;
                case KeywordOpportunity keywordOpportunity:
                    JsonSerializer.Serialize(writer, keywordOpportunity, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class OpportunityConverter : JsonConverter<Opportunity>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public OpportunityConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Opportunity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "BidOpportunity" => jsonObj.Deserialize<BidOpportunity>(options),
                "BroadMatchKeywordOpportunity" => jsonObj.Deserialize<BroadMatchKeywordOpportunity>(options),
                "KeywordOpportunity" => jsonObj.Deserialize<KeywordOpportunity>(options),
                "BudgetOpportunity" => jsonObj.Deserialize<BudgetOpportunity>(options),
                "Opportunity" => jsonObj.Deserialize<Opportunity>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Opportunity value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case BidOpportunity bidOpportunity:
                    JsonSerializer.Serialize(writer, bidOpportunity, options);
                    break;
                case BroadMatchKeywordOpportunity broadMatchKeywordOpportunity:
                    JsonSerializer.Serialize(writer, broadMatchKeywordOpportunity, options);
                    break;
                case KeywordOpportunity keywordOpportunity:
                    JsonSerializer.Serialize(writer, keywordOpportunity, options);
                    break;
                case BudgetOpportunity budgetOpportunity:
                    JsonSerializer.Serialize(writer, budgetOpportunity, options);
                    break;
                case Opportunity opportunity:
                    JsonSerializer.Serialize(writer, opportunity, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class PerformanceInsightsMessageParameterConverter : JsonConverter<PerformanceInsightsMessageParameter>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public PerformanceInsightsMessageParameterConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override PerformanceInsightsMessageParameter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "EntityParameter" => jsonObj.Deserialize<EntityParameter>(options),
                "UrlParameter" => jsonObj.Deserialize<UrlParameter>(options),
                "TextParameter" => jsonObj.Deserialize<TextParameter>(options),
                "PerformanceInsightsMessageParameter" => jsonObj.Deserialize<PerformanceInsightsMessageParameter>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, PerformanceInsightsMessageParameter value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case EntityParameter entityParameter:
                    JsonSerializer.Serialize(writer, entityParameter, options);
                    break;
                case UrlParameter urlParameter:
                    JsonSerializer.Serialize(writer, urlParameter, options);
                    break;
                case TextParameter textParameter:
                    JsonSerializer.Serialize(writer, textParameter, options);
                    break;
                case PerformanceInsightsMessageParameter performanceInsightsMessageParameter:
                    JsonSerializer.Serialize(writer, performanceInsightsMessageParameter, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class RecommendationConverter : JsonConverter<Recommendation>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public RecommendationConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Recommendation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "ResponsiveSearchAdsRecommendation" => jsonObj.Deserialize<ResponsiveSearchAdsRecommendation>(options),
                "Recommendation" => jsonObj.Deserialize<Recommendation>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Recommendation value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case ResponsiveSearchAdsRecommendation responsiveSearchAdsRecommendation:
                    JsonSerializer.Serialize(writer, responsiveSearchAdsRecommendation, options);
                    break;
                case Recommendation recommendation:
                    JsonSerializer.Serialize(writer, recommendation, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class RecommendationBaseConverter : JsonConverter<RecommendationBase>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public RecommendationBaseConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override RecommendationBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "ResponsiveSearchAdRecommendation" => jsonObj.Deserialize<ResponsiveSearchAdRecommendation>(options),
                "KeywordRecommendation" => jsonObj.Deserialize<KeywordRecommendation>(options),
                "CampaignBudgetRecommendation" => jsonObj.Deserialize<CampaignBudgetRecommendation>(options),
                "RecommendationBase" => jsonObj.Deserialize<RecommendationBase>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, RecommendationBase value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case ResponsiveSearchAdRecommendation responsiveSearchAdRecommendation:
                    JsonSerializer.Serialize(writer, responsiveSearchAdRecommendation, options);
                    break;
                case KeywordRecommendation keywordRecommendation:
                    JsonSerializer.Serialize(writer, keywordRecommendation, options);
                    break;
                case CampaignBudgetRecommendation campaignBudgetRecommendation:
                    JsonSerializer.Serialize(writer, campaignBudgetRecommendation, options);
                    break;
                case RecommendationBase recommendationBase:
                    JsonSerializer.Serialize(writer, recommendationBase, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class RecommendationInfoConverter : JsonConverter<RecommendationInfo>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public RecommendationInfoConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override RecommendationInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "RSARecommendationInfo" => jsonObj.Deserialize<RSARecommendationInfo>(options),
                "RecommendationInfo" => jsonObj.Deserialize<RecommendationInfo>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, RecommendationInfo value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case RSARecommendationInfo rSARecommendationInfo:
                    JsonSerializer.Serialize(writer, rSARecommendationInfo, options);
                    break;
                case RecommendationInfo recommendationInfo:
                    JsonSerializer.Serialize(writer, recommendationInfo, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class SearchParameterConverter : JsonConverter<SearchParameter>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public SearchParameterConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override SearchParameter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AuctionSegmentSearchParameter" => jsonObj.Deserialize<AuctionSegmentSearchParameter>(options),
                "DateRangeSearchParameter" => jsonObj.Deserialize<DateRangeSearchParameter>(options),
                "CompetitionSearchParameter" => jsonObj.Deserialize<CompetitionSearchParameter>(options),
                "LanguageSearchParameter" => jsonObj.Deserialize<LanguageSearchParameter>(options),
                "DeviceSearchParameter" => jsonObj.Deserialize<DeviceSearchParameter>(options),
                "NetworkSearchParameter" => jsonObj.Deserialize<NetworkSearchParameter>(options),
                "LocationSearchParameter" => jsonObj.Deserialize<LocationSearchParameter>(options),
                "ImpressionShareSearchParameter" => jsonObj.Deserialize<ImpressionShareSearchParameter>(options),
                "ExcludeAccountKeywordsSearchParameter" => jsonObj.Deserialize<ExcludeAccountKeywordsSearchParameter>(options),
                "IdeaTextSearchParameter" => jsonObj.Deserialize<IdeaTextSearchParameter>(options),
                "SuggestedBidSearchParameter" => jsonObj.Deserialize<SuggestedBidSearchParameter>(options),
                "SearchVolumeSearchParameter" => jsonObj.Deserialize<SearchVolumeSearchParameter>(options),
                "CategorySearchParameter" => jsonObj.Deserialize<CategorySearchParameter>(options),
                "UrlSearchParameter" => jsonObj.Deserialize<UrlSearchParameter>(options),
                "QuerySearchParameter" => jsonObj.Deserialize<QuerySearchParameter>(options),
                "SearchParameter" => jsonObj.Deserialize<SearchParameter>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, SearchParameter value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AuctionSegmentSearchParameter auctionSegmentSearchParameter:
                    JsonSerializer.Serialize(writer, auctionSegmentSearchParameter, options);
                    break;
                case DateRangeSearchParameter dateRangeSearchParameter:
                    JsonSerializer.Serialize(writer, dateRangeSearchParameter, options);
                    break;
                case CompetitionSearchParameter competitionSearchParameter:
                    JsonSerializer.Serialize(writer, competitionSearchParameter, options);
                    break;
                case LanguageSearchParameter languageSearchParameter:
                    JsonSerializer.Serialize(writer, languageSearchParameter, options);
                    break;
                case DeviceSearchParameter deviceSearchParameter:
                    JsonSerializer.Serialize(writer, deviceSearchParameter, options);
                    break;
                case NetworkSearchParameter networkSearchParameter:
                    JsonSerializer.Serialize(writer, networkSearchParameter, options);
                    break;
                case LocationSearchParameter locationSearchParameter:
                    JsonSerializer.Serialize(writer, locationSearchParameter, options);
                    break;
                case ImpressionShareSearchParameter impressionShareSearchParameter:
                    JsonSerializer.Serialize(writer, impressionShareSearchParameter, options);
                    break;
                case ExcludeAccountKeywordsSearchParameter excludeAccountKeywordsSearchParameter:
                    JsonSerializer.Serialize(writer, excludeAccountKeywordsSearchParameter, options);
                    break;
                case IdeaTextSearchParameter ideaTextSearchParameter:
                    JsonSerializer.Serialize(writer, ideaTextSearchParameter, options);
                    break;
                case SuggestedBidSearchParameter suggestedBidSearchParameter:
                    JsonSerializer.Serialize(writer, suggestedBidSearchParameter, options);
                    break;
                case SearchVolumeSearchParameter searchVolumeSearchParameter:
                    JsonSerializer.Serialize(writer, searchVolumeSearchParameter, options);
                    break;
                case CategorySearchParameter categorySearchParameter:
                    JsonSerializer.Serialize(writer, categorySearchParameter, options);
                    break;
                case UrlSearchParameter urlSearchParameter:
                    JsonSerializer.Serialize(writer, urlSearchParameter, options);
                    break;
                case QuerySearchParameter querySearchParameter:
                    JsonSerializer.Serialize(writer, querySearchParameter, options);
                    break;
                case SearchParameter searchParameter:
                    JsonSerializer.Serialize(writer, searchParameter, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }
}