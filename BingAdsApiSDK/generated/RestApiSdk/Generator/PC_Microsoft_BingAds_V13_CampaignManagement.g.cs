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
using Microsoft.BingAds.V13.CampaignManagement;

public static partial class RestApiGeneration
{
    public class Microsoft_BingAds_V13_CampaignManagement_AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_AdConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_AdExtensionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_AdGroupCriterionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_AssetConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_AudienceConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_AudienceGroupDimensionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_BatchErrorConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_BatchErrorCollectionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_BiddingSchemeConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_CampaignCriterionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_ConversionGoalConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_CriterionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_CriterionBidConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_CriterionCashbackConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_ImportJobConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_ImportOptionConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_LinkedInSegmentConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_MediaConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_MediaRepresentationConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_RemarketingRuleConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_RuleItemConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_SettingConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_SharedEntityConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_SharedListConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CampaignManagement_SharedListItemConverter(originalOptions, createUnsupportedTypeValueException));
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_AdConverter : JsonConverter<Ad>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_AdConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Ad? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "ResponsiveSearch" => jsonObj.Deserialize<ResponsiveSearchAd>(options),
                "ResponsiveAd" => jsonObj.Deserialize<ResponsiveAd>(options),
                "DynamicSearch" => jsonObj.Deserialize<DynamicSearchAd>(options),
                "ExpandedText" => jsonObj.Deserialize<ExpandedTextAd>(options),
                "AppInstall" => jsonObj.Deserialize<AppInstallAd>(options),
                "Hotel" => jsonObj.Deserialize<HotelAd>(options),
                "Product" => jsonObj.Deserialize<ProductAd>(options),
                "Text" => jsonObj.Deserialize<TextAd>(options),
                "Ad" => jsonObj.Deserialize<Ad>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Ad value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case ResponsiveSearchAd responsiveSearchAd:
                    JsonSerializer.Serialize(writer, responsiveSearchAd, options);
                    break;
                case ResponsiveAd responsiveAd:
                    JsonSerializer.Serialize(writer, responsiveAd, options);
                    break;
                case DynamicSearchAd dynamicSearchAd:
                    JsonSerializer.Serialize(writer, dynamicSearchAd, options);
                    break;
                case ExpandedTextAd expandedTextAd:
                    JsonSerializer.Serialize(writer, expandedTextAd, options);
                    break;
                case AppInstallAd appInstallAd:
                    JsonSerializer.Serialize(writer, appInstallAd, options);
                    break;
                case HotelAd hotelAd:
                    JsonSerializer.Serialize(writer, hotelAd, options);
                    break;
                case ProductAd productAd:
                    JsonSerializer.Serialize(writer, productAd, options);
                    break;
                case TextAd textAd:
                    JsonSerializer.Serialize(writer, textAd, options);
                    break;
                case Ad ad:
                    JsonSerializer.Serialize(writer, ad, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_AdExtensionConverter : JsonConverter<AdExtension>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_AdExtensionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override AdExtension? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "LogoAdExtension" => jsonObj.Deserialize<LogoAdExtension>(options),
                "DisclaimerAdExtension" => jsonObj.Deserialize<DisclaimerAdExtension>(options),
                "VideoAdExtension" => jsonObj.Deserialize<VideoAdExtension>(options),
                "FlyerAdExtension" => jsonObj.Deserialize<FlyerAdExtension>(options),
                "FilterLinkAdExtension" => jsonObj.Deserialize<FilterLinkAdExtension>(options),
                "PromotionAdExtension" => jsonObj.Deserialize<PromotionAdExtension>(options),
                "PriceAdExtension" => jsonObj.Deserialize<PriceAdExtension>(options),
                "StructuredSnippetAdExtension" => jsonObj.Deserialize<StructuredSnippetAdExtension>(options),
                "ActionAdExtension" => jsonObj.Deserialize<ActionAdExtension>(options),
                "SitelinkAdExtension" => jsonObj.Deserialize<SitelinkAdExtension>(options),
                "CalloutAdExtension" => jsonObj.Deserialize<CalloutAdExtension>(options),
                "ReviewAdExtension" => jsonObj.Deserialize<ReviewAdExtension>(options),
                "AppAdExtension" => jsonObj.Deserialize<AppAdExtension>(options),
                "ImageAdExtension" => jsonObj.Deserialize<ImageAdExtension>(options),
                "CallAdExtension" => jsonObj.Deserialize<CallAdExtension>(options),
                "LocationAdExtension" => jsonObj.Deserialize<LocationAdExtension>(options),
                "AdExtension" => jsonObj.Deserialize<AdExtension>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, AdExtension value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case LogoAdExtension logoAdExtension:
                    JsonSerializer.Serialize(writer, logoAdExtension, options);
                    break;
                case DisclaimerAdExtension disclaimerAdExtension:
                    JsonSerializer.Serialize(writer, disclaimerAdExtension, options);
                    break;
                case VideoAdExtension videoAdExtension:
                    JsonSerializer.Serialize(writer, videoAdExtension, options);
                    break;
                case FlyerAdExtension flyerAdExtension:
                    JsonSerializer.Serialize(writer, flyerAdExtension, options);
                    break;
                case FilterLinkAdExtension filterLinkAdExtension:
                    JsonSerializer.Serialize(writer, filterLinkAdExtension, options);
                    break;
                case PromotionAdExtension promotionAdExtension:
                    JsonSerializer.Serialize(writer, promotionAdExtension, options);
                    break;
                case PriceAdExtension priceAdExtension:
                    JsonSerializer.Serialize(writer, priceAdExtension, options);
                    break;
                case StructuredSnippetAdExtension structuredSnippetAdExtension:
                    JsonSerializer.Serialize(writer, structuredSnippetAdExtension, options);
                    break;
                case ActionAdExtension actionAdExtension:
                    JsonSerializer.Serialize(writer, actionAdExtension, options);
                    break;
                case SitelinkAdExtension sitelinkAdExtension:
                    JsonSerializer.Serialize(writer, sitelinkAdExtension, options);
                    break;
                case CalloutAdExtension calloutAdExtension:
                    JsonSerializer.Serialize(writer, calloutAdExtension, options);
                    break;
                case ReviewAdExtension reviewAdExtension:
                    JsonSerializer.Serialize(writer, reviewAdExtension, options);
                    break;
                case AppAdExtension appAdExtension:
                    JsonSerializer.Serialize(writer, appAdExtension, options);
                    break;
                case ImageAdExtension imageAdExtension:
                    JsonSerializer.Serialize(writer, imageAdExtension, options);
                    break;
                case CallAdExtension callAdExtension:
                    JsonSerializer.Serialize(writer, callAdExtension, options);
                    break;
                case LocationAdExtension locationAdExtension:
                    JsonSerializer.Serialize(writer, locationAdExtension, options);
                    break;
                case AdExtension adExtension:
                    JsonSerializer.Serialize(writer, adExtension, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_AdGroupCriterionConverter : JsonConverter<AdGroupCriterion>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_AdGroupCriterionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override AdGroupCriterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "NegativeAdGroupCriterion" => jsonObj.Deserialize<NegativeAdGroupCriterion>(options),
                "BiddableAdGroupCriterion" => jsonObj.Deserialize<BiddableAdGroupCriterion>(options),
                "AdGroupCriterion" => jsonObj.Deserialize<AdGroupCriterion>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, AdGroupCriterion value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case NegativeAdGroupCriterion negativeAdGroupCriterion:
                    JsonSerializer.Serialize(writer, negativeAdGroupCriterion, options);
                    break;
                case BiddableAdGroupCriterion biddableAdGroupCriterion:
                    JsonSerializer.Serialize(writer, biddableAdGroupCriterion, options);
                    break;
                case AdGroupCriterion adGroupCriterion:
                    JsonSerializer.Serialize(writer, adGroupCriterion, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_ApplicationFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
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
                "EditorialApiFaultDetail" => jsonObj.Deserialize<EditorialApiFaultDetail>(options),
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
                case EditorialApiFaultDetail editorialApiFaultDetail:
                    JsonSerializer.Serialize(writer, editorialApiFaultDetail, options);
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

    class Microsoft_BingAds_V13_CampaignManagement_AssetConverter : JsonConverter<Asset>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_AssetConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Asset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "VideoAsset" => jsonObj.Deserialize<VideoAsset>(options),
                "ImageAsset" => jsonObj.Deserialize<ImageAsset>(options),
                "TextAsset" => jsonObj.Deserialize<TextAsset>(options),
                "Asset" => jsonObj.Deserialize<Asset>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Asset value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case VideoAsset videoAsset:
                    JsonSerializer.Serialize(writer, videoAsset, options);
                    break;
                case ImageAsset imageAsset:
                    JsonSerializer.Serialize(writer, imageAsset, options);
                    break;
                case TextAsset textAsset:
                    JsonSerializer.Serialize(writer, textAsset, options);
                    break;
                case Asset asset:
                    JsonSerializer.Serialize(writer, asset, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_AudienceConverter : JsonConverter<Audience>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_AudienceConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Audience? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CustomSegment" => jsonObj.Deserialize<CustomSegment>(options),
                "ImpressionBasedRemarketingList" => jsonObj.Deserialize<ImpressionBasedRemarketingList>(options),
                "CustomerList" => jsonObj.Deserialize<CustomerList>(options),
                "CombinedList" => jsonObj.Deserialize<CombinedList>(options),
                "SimilarRemarketingList" => jsonObj.Deserialize<SimilarRemarketingList>(options),
                "Product" => jsonObj.Deserialize<ProductAudience>(options),
                "InMarket" => jsonObj.Deserialize<InMarketAudience>(options),
                "Custom" => jsonObj.Deserialize<CustomAudience>(options),
                "RemarketingList" => jsonObj.Deserialize<RemarketingList>(options),
                "Audience" => jsonObj.Deserialize<Audience>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Audience value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case CustomSegment customSegment:
                    JsonSerializer.Serialize(writer, customSegment, options);
                    break;
                case ImpressionBasedRemarketingList impressionBasedRemarketingList:
                    JsonSerializer.Serialize(writer, impressionBasedRemarketingList, options);
                    break;
                case CustomerList customerList:
                    JsonSerializer.Serialize(writer, customerList, options);
                    break;
                case CombinedList combinedList:
                    JsonSerializer.Serialize(writer, combinedList, options);
                    break;
                case SimilarRemarketingList similarRemarketingList:
                    JsonSerializer.Serialize(writer, similarRemarketingList, options);
                    break;
                case ProductAudience productAudience:
                    JsonSerializer.Serialize(writer, productAudience, options);
                    break;
                case InMarketAudience inMarketAudience:
                    JsonSerializer.Serialize(writer, inMarketAudience, options);
                    break;
                case CustomAudience customAudience:
                    JsonSerializer.Serialize(writer, customAudience, options);
                    break;
                case RemarketingList remarketingList:
                    JsonSerializer.Serialize(writer, remarketingList, options);
                    break;
                case Audience audience:
                    JsonSerializer.Serialize(writer, audience, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_AudienceGroupDimensionConverter : JsonConverter<AudienceGroupDimension>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_AudienceGroupDimensionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override AudienceGroupDimension? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "Profile" => jsonObj.Deserialize<ProfileDimension>(options),
                "Audience" => jsonObj.Deserialize<AudienceDimension>(options),
                "Gender" => jsonObj.Deserialize<GenderDimension>(options),
                "Age" => jsonObj.Deserialize<AgeDimension>(options),
                "AudienceGroupDimension" => jsonObj.Deserialize<AudienceGroupDimension>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, AudienceGroupDimension value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case ProfileDimension profileDimension:
                    JsonSerializer.Serialize(writer, profileDimension, options);
                    break;
                case AudienceDimension audienceDimension:
                    JsonSerializer.Serialize(writer, audienceDimension, options);
                    break;
                case GenderDimension genderDimension:
                    JsonSerializer.Serialize(writer, genderDimension, options);
                    break;
                case AgeDimension ageDimension:
                    JsonSerializer.Serialize(writer, ageDimension, options);
                    break;
                case AudienceGroupDimension audienceGroupDimension:
                    JsonSerializer.Serialize(writer, audienceGroupDimension, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_BatchErrorConverter : JsonConverter<BatchError>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_BatchErrorConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override BatchError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "EditorialError" => jsonObj.Deserialize<EditorialError>(options),
                "BatchError" => jsonObj.Deserialize<BatchError>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, BatchError value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case EditorialError editorialError:
                    JsonSerializer.Serialize(writer, editorialError, options);
                    break;
                case BatchError batchError:
                    JsonSerializer.Serialize(writer, batchError, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_BatchErrorCollectionConverter : JsonConverter<BatchErrorCollection>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_BatchErrorCollectionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override BatchErrorCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "EditorialErrorCollection" => jsonObj.Deserialize<EditorialErrorCollection>(options),
                "BatchErrorCollection" => jsonObj.Deserialize<BatchErrorCollection>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, BatchErrorCollection value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case EditorialErrorCollection editorialErrorCollection:
                    JsonSerializer.Serialize(writer, editorialErrorCollection, options);
                    break;
                case BatchErrorCollection batchErrorCollection:
                    JsonSerializer.Serialize(writer, batchErrorCollection, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_BiddingSchemeConverter : JsonConverter<BiddingScheme>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_BiddingSchemeConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override BiddingScheme? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CostPerSale" => jsonObj.Deserialize<CostPerSaleBiddingScheme>(options),
                "ManualCpaBiddingScheme" => jsonObj.Deserialize<ManualCpaBiddingScheme>(options),
                "CommissionBiddingScheme" => jsonObj.Deserialize<CommissionBiddingScheme>(options),
                "PercentCpcBiddingScheme" => jsonObj.Deserialize<PercentCpcBiddingScheme>(options),
                "TargetImpressionShareBiddingScheme" => jsonObj.Deserialize<TargetImpressionShareBiddingScheme>(options),
                "MaxConversionValueBiddingScheme" => jsonObj.Deserialize<MaxConversionValueBiddingScheme>(options),
                "MaxRoasBiddingScheme" => jsonObj.Deserialize<MaxRoasBiddingScheme>(options),
                "TargetRoasBiddingScheme" => jsonObj.Deserialize<TargetRoasBiddingScheme>(options),
                "InheritFromParent" => jsonObj.Deserialize<InheritFromParentBiddingScheme>(options),
                "ManualCpm" => jsonObj.Deserialize<ManualCpmBiddingScheme>(options),
                "ManualCpv" => jsonObj.Deserialize<ManualCpvBiddingScheme>(options),
                "EnhancedCpc" => jsonObj.Deserialize<EnhancedCpcBiddingScheme>(options),
                "ManualCpc" => jsonObj.Deserialize<ManualCpcBiddingScheme>(options),
                "TargetCpa" => jsonObj.Deserialize<TargetCpaBiddingScheme>(options),
                "MaxConversions" => jsonObj.Deserialize<MaxConversionsBiddingScheme>(options),
                "MaxClicks" => jsonObj.Deserialize<MaxClicksBiddingScheme>(options),
                "BiddingScheme" => jsonObj.Deserialize<BiddingScheme>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, BiddingScheme value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case CostPerSaleBiddingScheme costPerSaleBiddingScheme:
                    JsonSerializer.Serialize(writer, costPerSaleBiddingScheme, options);
                    break;
                case ManualCpaBiddingScheme manualCpaBiddingScheme:
                    JsonSerializer.Serialize(writer, manualCpaBiddingScheme, options);
                    break;
                case CommissionBiddingScheme commissionBiddingScheme:
                    JsonSerializer.Serialize(writer, commissionBiddingScheme, options);
                    break;
                case PercentCpcBiddingScheme percentCpcBiddingScheme:
                    JsonSerializer.Serialize(writer, percentCpcBiddingScheme, options);
                    break;
                case TargetImpressionShareBiddingScheme targetImpressionShareBiddingScheme:
                    JsonSerializer.Serialize(writer, targetImpressionShareBiddingScheme, options);
                    break;
                case MaxConversionValueBiddingScheme maxConversionValueBiddingScheme:
                    JsonSerializer.Serialize(writer, maxConversionValueBiddingScheme, options);
                    break;
                case MaxRoasBiddingScheme maxRoasBiddingScheme:
                    JsonSerializer.Serialize(writer, maxRoasBiddingScheme, options);
                    break;
                case TargetRoasBiddingScheme targetRoasBiddingScheme:
                    JsonSerializer.Serialize(writer, targetRoasBiddingScheme, options);
                    break;
                case InheritFromParentBiddingScheme inheritFromParentBiddingScheme:
                    JsonSerializer.Serialize(writer, inheritFromParentBiddingScheme, options);
                    break;
                case ManualCpmBiddingScheme manualCpmBiddingScheme:
                    JsonSerializer.Serialize(writer, manualCpmBiddingScheme, options);
                    break;
                case ManualCpvBiddingScheme manualCpvBiddingScheme:
                    JsonSerializer.Serialize(writer, manualCpvBiddingScheme, options);
                    break;
                case EnhancedCpcBiddingScheme enhancedCpcBiddingScheme:
                    JsonSerializer.Serialize(writer, enhancedCpcBiddingScheme, options);
                    break;
                case ManualCpcBiddingScheme manualCpcBiddingScheme:
                    JsonSerializer.Serialize(writer, manualCpcBiddingScheme, options);
                    break;
                case TargetCpaBiddingScheme targetCpaBiddingScheme:
                    JsonSerializer.Serialize(writer, targetCpaBiddingScheme, options);
                    break;
                case MaxConversionsBiddingScheme maxConversionsBiddingScheme:
                    JsonSerializer.Serialize(writer, maxConversionsBiddingScheme, options);
                    break;
                case MaxClicksBiddingScheme maxClicksBiddingScheme:
                    JsonSerializer.Serialize(writer, maxClicksBiddingScheme, options);
                    break;
                case BiddingScheme biddingScheme:
                    JsonSerializer.Serialize(writer, biddingScheme, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_CampaignCriterionConverter : JsonConverter<CampaignCriterion>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_CampaignCriterionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override CampaignCriterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "BiddableCampaignCriterion" => jsonObj.Deserialize<BiddableCampaignCriterion>(options),
                "NegativeCampaignCriterion" => jsonObj.Deserialize<NegativeCampaignCriterion>(options),
                "CampaignCriterion" => jsonObj.Deserialize<CampaignCriterion>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, CampaignCriterion value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case BiddableCampaignCriterion biddableCampaignCriterion:
                    JsonSerializer.Serialize(writer, biddableCampaignCriterion, options);
                    break;
                case NegativeCampaignCriterion negativeCampaignCriterion:
                    JsonSerializer.Serialize(writer, negativeCampaignCriterion, options);
                    break;
                case CampaignCriterion campaignCriterion:
                    JsonSerializer.Serialize(writer, campaignCriterion, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_ConversionGoalConverter : JsonConverter<ConversionGoal>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_ConversionGoalConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ConversionGoal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AppDownload" => jsonObj.Deserialize<AppDownloadGoal>(options),
                "InStoreTransaction" => jsonObj.Deserialize<InStoreTransactionGoal>(options),
                "OfflineConversion" => jsonObj.Deserialize<OfflineConversionGoal>(options),
                "AppInstall" => jsonObj.Deserialize<AppInstallGoal>(options),
                "Event" => jsonObj.Deserialize<EventGoal>(options),
                "PagesViewedPerVisit" => jsonObj.Deserialize<PagesViewedPerVisitGoal>(options),
                "Duration" => jsonObj.Deserialize<DurationGoal>(options),
                "Url" => jsonObj.Deserialize<UrlGoal>(options),
                "ConversionGoal" => jsonObj.Deserialize<ConversionGoal>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ConversionGoal value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AppDownloadGoal appDownloadGoal:
                    JsonSerializer.Serialize(writer, appDownloadGoal, options);
                    break;
                case InStoreTransactionGoal inStoreTransactionGoal:
                    JsonSerializer.Serialize(writer, inStoreTransactionGoal, options);
                    break;
                case OfflineConversionGoal offlineConversionGoal:
                    JsonSerializer.Serialize(writer, offlineConversionGoal, options);
                    break;
                case AppInstallGoal appInstallGoal:
                    JsonSerializer.Serialize(writer, appInstallGoal, options);
                    break;
                case EventGoal eventGoal:
                    JsonSerializer.Serialize(writer, eventGoal, options);
                    break;
                case PagesViewedPerVisitGoal pagesViewedPerVisitGoal:
                    JsonSerializer.Serialize(writer, pagesViewedPerVisitGoal, options);
                    break;
                case DurationGoal durationGoal:
                    JsonSerializer.Serialize(writer, durationGoal, options);
                    break;
                case UrlGoal urlGoal:
                    JsonSerializer.Serialize(writer, urlGoal, options);
                    break;
                case ConversionGoal conversionGoal:
                    JsonSerializer.Serialize(writer, conversionGoal, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_CriterionConverter : JsonConverter<Criterion>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_CriterionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
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
                "TopicCriterion" => jsonObj.Deserialize<TopicCriterion>(options),
                "PlacementCriterion" => jsonObj.Deserialize<PlacementCriterion>(options),
                "GenreCriterion" => jsonObj.Deserialize<GenreCriterion>(options),
                "DealCriterion" => jsonObj.Deserialize<DealCriterion>(options),
                "StoreCriterion" => jsonObj.Deserialize<StoreCriterion>(options),
                "ProfileCriterion" => jsonObj.Deserialize<ProfileCriterion>(options),
                "AudienceCriterion" => jsonObj.Deserialize<AudienceCriterion>(options),
                "LocationIntentCriterion" => jsonObj.Deserialize<LocationIntentCriterion>(options),
                "LocationCriterion" => jsonObj.Deserialize<LocationCriterion>(options),
                "RadiusCriterion" => jsonObj.Deserialize<RadiusCriterion>(options),
                "GenderCriterion" => jsonObj.Deserialize<GenderCriterion>(options),
                "DayTimeCriterion" => jsonObj.Deserialize<DayTimeCriterion>(options),
                "DeviceCriterion" => jsonObj.Deserialize<DeviceCriterion>(options),
                "AgeCriterion" => jsonObj.Deserialize<AgeCriterion>(options),
                "Webpage" => jsonObj.Deserialize<Webpage>(options),
                "ProductScope" => jsonObj.Deserialize<ProductScope>(options),
                "HotelLengthOfStayCriterion" => jsonObj.Deserialize<HotelLengthOfStayCriterion>(options),
                "HotelDateSelectionTypeCriterion" => jsonObj.Deserialize<HotelDateSelectionTypeCriterion>(options),
                "HotelCheckInDayCriterion" => jsonObj.Deserialize<HotelCheckInDayCriterion>(options),
                "HotelCheckInDateCriterion" => jsonObj.Deserialize<HotelCheckInDateCriterion>(options),
                "HotelAdvanceBookingWindowCriterion" => jsonObj.Deserialize<HotelAdvanceBookingWindowCriterion>(options),
                "HotelGroup" => jsonObj.Deserialize<HotelGroup>(options),
                "ProductPartition" => jsonObj.Deserialize<ProductPartition>(options),
                "Criterion" => jsonObj.Deserialize<Criterion>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Criterion value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case TopicCriterion topicCriterion:
                    JsonSerializer.Serialize(writer, topicCriterion, options);
                    break;
                case PlacementCriterion placementCriterion:
                    JsonSerializer.Serialize(writer, placementCriterion, options);
                    break;
                case GenreCriterion genreCriterion:
                    JsonSerializer.Serialize(writer, genreCriterion, options);
                    break;
                case DealCriterion dealCriterion:
                    JsonSerializer.Serialize(writer, dealCriterion, options);
                    break;
                case StoreCriterion storeCriterion:
                    JsonSerializer.Serialize(writer, storeCriterion, options);
                    break;
                case ProfileCriterion profileCriterion:
                    JsonSerializer.Serialize(writer, profileCriterion, options);
                    break;
                case AudienceCriterion audienceCriterion:
                    JsonSerializer.Serialize(writer, audienceCriterion, options);
                    break;
                case LocationIntentCriterion locationIntentCriterion:
                    JsonSerializer.Serialize(writer, locationIntentCriterion, options);
                    break;
                case LocationCriterion locationCriterion:
                    JsonSerializer.Serialize(writer, locationCriterion, options);
                    break;
                case RadiusCriterion radiusCriterion:
                    JsonSerializer.Serialize(writer, radiusCriterion, options);
                    break;
                case GenderCriterion genderCriterion:
                    JsonSerializer.Serialize(writer, genderCriterion, options);
                    break;
                case DayTimeCriterion dayTimeCriterion:
                    JsonSerializer.Serialize(writer, dayTimeCriterion, options);
                    break;
                case DeviceCriterion deviceCriterion:
                    JsonSerializer.Serialize(writer, deviceCriterion, options);
                    break;
                case AgeCriterion ageCriterion:
                    JsonSerializer.Serialize(writer, ageCriterion, options);
                    break;
                case Webpage webpage:
                    JsonSerializer.Serialize(writer, webpage, options);
                    break;
                case ProductScope productScope:
                    JsonSerializer.Serialize(writer, productScope, options);
                    break;
                case HotelLengthOfStayCriterion hotelLengthOfStayCriterion:
                    JsonSerializer.Serialize(writer, hotelLengthOfStayCriterion, options);
                    break;
                case HotelDateSelectionTypeCriterion hotelDateSelectionTypeCriterion:
                    JsonSerializer.Serialize(writer, hotelDateSelectionTypeCriterion, options);
                    break;
                case HotelCheckInDayCriterion hotelCheckInDayCriterion:
                    JsonSerializer.Serialize(writer, hotelCheckInDayCriterion, options);
                    break;
                case HotelCheckInDateCriterion hotelCheckInDateCriterion:
                    JsonSerializer.Serialize(writer, hotelCheckInDateCriterion, options);
                    break;
                case HotelAdvanceBookingWindowCriterion hotelAdvanceBookingWindowCriterion:
                    JsonSerializer.Serialize(writer, hotelAdvanceBookingWindowCriterion, options);
                    break;
                case HotelGroup hotelGroup:
                    JsonSerializer.Serialize(writer, hotelGroup, options);
                    break;
                case ProductPartition productPartition:
                    JsonSerializer.Serialize(writer, productPartition, options);
                    break;
                case Criterion criterion:
                    JsonSerializer.Serialize(writer, criterion, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_CriterionBidConverter : JsonConverter<CriterionBid>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_CriterionBidConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override CriterionBid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "RateBid" => jsonObj.Deserialize<RateBid>(options),
                "BidMultiplier" => jsonObj.Deserialize<BidMultiplier>(options),
                "FixedBid" => jsonObj.Deserialize<FixedBid>(options),
                "CriterionBid" => jsonObj.Deserialize<CriterionBid>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, CriterionBid value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case RateBid rateBid:
                    JsonSerializer.Serialize(writer, rateBid, options);
                    break;
                case BidMultiplier bidMultiplier:
                    JsonSerializer.Serialize(writer, bidMultiplier, options);
                    break;
                case FixedBid fixedBid:
                    JsonSerializer.Serialize(writer, fixedBid, options);
                    break;
                case CriterionBid criterionBid:
                    JsonSerializer.Serialize(writer, criterionBid, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_CriterionCashbackConverter : JsonConverter<CriterionCashback>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_CriterionCashbackConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override CriterionCashback? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CashbackAdjustment" => jsonObj.Deserialize<CashbackAdjustment>(options),
                "CriterionCashback" => jsonObj.Deserialize<CriterionCashback>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, CriterionCashback value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case CashbackAdjustment cashbackAdjustment:
                    JsonSerializer.Serialize(writer, cashbackAdjustment, options);
                    break;
                case CriterionCashback criterionCashback:
                    JsonSerializer.Serialize(writer, criterionCashback, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_ImportJobConverter : JsonConverter<ImportJob>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_ImportJobConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ImportJob? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "FileImportJob" => jsonObj.Deserialize<FileImportJob>(options),
                "GoogleImportJob" => jsonObj.Deserialize<GoogleImportJob>(options),
                "ImportJob" => jsonObj.Deserialize<ImportJob>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ImportJob value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case FileImportJob fileImportJob:
                    JsonSerializer.Serialize(writer, fileImportJob, options);
                    break;
                case GoogleImportJob googleImportJob:
                    JsonSerializer.Serialize(writer, googleImportJob, options);
                    break;
                case ImportJob importJob:
                    JsonSerializer.Serialize(writer, importJob, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_ImportOptionConverter : JsonConverter<ImportOption>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_ImportOptionConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ImportOption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "FileImportOption" => jsonObj.Deserialize<FileImportOption>(options),
                "GoogleImportOption" => jsonObj.Deserialize<GoogleImportOption>(options),
                "ImportOption" => jsonObj.Deserialize<ImportOption>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ImportOption value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case FileImportOption fileImportOption:
                    JsonSerializer.Serialize(writer, fileImportOption, options);
                    break;
                case GoogleImportOption googleImportOption:
                    JsonSerializer.Serialize(writer, googleImportOption, options);
                    break;
                case ImportOption importOption:
                    JsonSerializer.Serialize(writer, importOption, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_LinkedInSegmentConverter : JsonConverter<LinkedInSegment>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_LinkedInSegmentConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override LinkedInSegment? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CompanyList" => jsonObj.Deserialize<CompanyList>(options),
                "LinkedInSegment" => jsonObj.Deserialize<LinkedInSegment>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, LinkedInSegment value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case CompanyList companyList:
                    JsonSerializer.Serialize(writer, companyList, options);
                    break;
                case LinkedInSegment linkedInSegment:
                    JsonSerializer.Serialize(writer, linkedInSegment, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_MediaConverter : JsonConverter<Media>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_MediaConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Media? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "Image" => jsonObj.Deserialize<Image>(options),
                "Media" => jsonObj.Deserialize<Media>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Media value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case Image image:
                    JsonSerializer.Serialize(writer, image, options);
                    break;
                case Media media:
                    JsonSerializer.Serialize(writer, media, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_MediaRepresentationConverter : JsonConverter<MediaRepresentation>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_MediaRepresentationConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override MediaRepresentation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "ImageMediaRepresentation" => jsonObj.Deserialize<ImageMediaRepresentation>(options),
                "MediaRepresentation" => jsonObj.Deserialize<MediaRepresentation>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, MediaRepresentation value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case ImageMediaRepresentation imageMediaRepresentation:
                    JsonSerializer.Serialize(writer, imageMediaRepresentation, options);
                    break;
                case MediaRepresentation mediaRepresentation:
                    JsonSerializer.Serialize(writer, mediaRepresentation, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_RemarketingRuleConverter : JsonConverter<RemarketingRule>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_RemarketingRuleConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override RemarketingRule? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CustomEvents" => jsonObj.Deserialize<CustomEventsRule>(options),
                "PageVisitorsWhoDidNotVisitAnotherPage" => jsonObj.Deserialize<PageVisitorsWhoDidNotVisitAnotherPageRule>(options),
                "PageVisitorsWhoVisitedAnotherPage" => jsonObj.Deserialize<PageVisitorsWhoVisitedAnotherPageRule>(options),
                "PageVisitors" => jsonObj.Deserialize<PageVisitorsRule>(options),
                "RemarketingRule" => jsonObj.Deserialize<RemarketingRule>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, RemarketingRule value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case CustomEventsRule customEventsRule:
                    JsonSerializer.Serialize(writer, customEventsRule, options);
                    break;
                case PageVisitorsWhoDidNotVisitAnotherPageRule pageVisitorsWhoDidNotVisitAnotherPageRule:
                    JsonSerializer.Serialize(writer, pageVisitorsWhoDidNotVisitAnotherPageRule, options);
                    break;
                case PageVisitorsWhoVisitedAnotherPageRule pageVisitorsWhoVisitedAnotherPageRule:
                    JsonSerializer.Serialize(writer, pageVisitorsWhoVisitedAnotherPageRule, options);
                    break;
                case PageVisitorsRule pageVisitorsRule:
                    JsonSerializer.Serialize(writer, pageVisitorsRule, options);
                    break;
                case RemarketingRule remarketingRule:
                    JsonSerializer.Serialize(writer, remarketingRule, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_RuleItemConverter : JsonConverter<RuleItem>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_RuleItemConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override RuleItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "Number" => jsonObj.Deserialize<NumberRuleItem>(options),
                "String" => jsonObj.Deserialize<StringRuleItem>(options),
                "RuleItem" => jsonObj.Deserialize<RuleItem>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, RuleItem value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case NumberRuleItem numberRuleItem:
                    JsonSerializer.Serialize(writer, numberRuleItem, options);
                    break;
                case StringRuleItem stringRuleItem:
                    JsonSerializer.Serialize(writer, stringRuleItem, options);
                    break;
                case RuleItem ruleItem:
                    JsonSerializer.Serialize(writer, ruleItem, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_SettingConverter : JsonConverter<Setting>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_SettingConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override Setting? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "NewCustomerAcquisitionGoalSetting" => jsonObj.Deserialize<NewCustomerAcquisitionGoalSetting>(options),
                "ThirdPartyMeasurementSetting" => jsonObj.Deserialize<ThirdPartyMeasurementSetting>(options),
                "AppSetting" => jsonObj.Deserialize<AppSetting>(options),
                "VanityPharmaSetting" => jsonObj.Deserialize<VanityPharmaSetting>(options),
                "CallToActionSetting" => jsonObj.Deserialize<CallToActionSetting>(options),
                "PerformanceMaxSetting" => jsonObj.Deserialize<PerformanceMaxSetting>(options),
                "ResponsiveSearchAdsSetting" => jsonObj.Deserialize<ResponsiveSearchAdsSetting>(options),
                "HotelSetting" => jsonObj.Deserialize<HotelSetting>(options),
                "DisclaimerSetting" => jsonObj.Deserialize<DisclaimerSetting>(options),
                "CoOpSetting" => jsonObj.Deserialize<CoOpSetting>(options),
                "TargetSetting" => jsonObj.Deserialize<TargetSetting>(options),
                "DynamicSearchAdsSetting" => jsonObj.Deserialize<DynamicSearchAdsSetting>(options),
                "DynamicFeedSetting" => jsonObj.Deserialize<DynamicFeedSetting>(options),
                "ShoppingSetting" => jsonObj.Deserialize<ShoppingSetting>(options),
                "VerifiedTrackingSetting" => jsonObj.Deserialize<VerifiedTrackingSetting>(options),
                "Setting" => jsonObj.Deserialize<Setting>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, Setting value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case NewCustomerAcquisitionGoalSetting newCustomerAcquisitionGoalSetting:
                    JsonSerializer.Serialize(writer, newCustomerAcquisitionGoalSetting, options);
                    break;
                case ThirdPartyMeasurementSetting thirdPartyMeasurementSetting:
                    JsonSerializer.Serialize(writer, thirdPartyMeasurementSetting, options);
                    break;
                case AppSetting appSetting:
                    JsonSerializer.Serialize(writer, appSetting, options);
                    break;
                case VanityPharmaSetting vanityPharmaSetting:
                    JsonSerializer.Serialize(writer, vanityPharmaSetting, options);
                    break;
                case CallToActionSetting callToActionSetting:
                    JsonSerializer.Serialize(writer, callToActionSetting, options);
                    break;
                case PerformanceMaxSetting performanceMaxSetting:
                    JsonSerializer.Serialize(writer, performanceMaxSetting, options);
                    break;
                case ResponsiveSearchAdsSetting responsiveSearchAdsSetting:
                    JsonSerializer.Serialize(writer, responsiveSearchAdsSetting, options);
                    break;
                case HotelSetting hotelSetting:
                    JsonSerializer.Serialize(writer, hotelSetting, options);
                    break;
                case DisclaimerSetting disclaimerSetting:
                    JsonSerializer.Serialize(writer, disclaimerSetting, options);
                    break;
                case CoOpSetting coOpSetting:
                    JsonSerializer.Serialize(writer, coOpSetting, options);
                    break;
                case TargetSetting targetSetting:
                    JsonSerializer.Serialize(writer, targetSetting, options);
                    break;
                case DynamicSearchAdsSetting dynamicSearchAdsSetting:
                    JsonSerializer.Serialize(writer, dynamicSearchAdsSetting, options);
                    break;
                case DynamicFeedSetting dynamicFeedSetting:
                    JsonSerializer.Serialize(writer, dynamicFeedSetting, options);
                    break;
                case ShoppingSetting shoppingSetting:
                    JsonSerializer.Serialize(writer, shoppingSetting, options);
                    break;
                case VerifiedTrackingSetting verifiedTrackingSetting:
                    JsonSerializer.Serialize(writer, verifiedTrackingSetting, options);
                    break;
                case Setting setting:
                    JsonSerializer.Serialize(writer, setting, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_SharedEntityConverter : JsonConverter<SharedEntity>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_SharedEntityConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override SharedEntity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AccountPlacementInclusionList" => jsonObj.Deserialize<AccountPlacementInclusionList>(options),
                "AccountPlacementExclusionList" => jsonObj.Deserialize<AccountPlacementExclusionList>(options),
                "BrandList" => jsonObj.Deserialize<BrandList>(options),
                "AccountNegativeKeywordList" => jsonObj.Deserialize<AccountNegativeKeywordList>(options),
                "PlacementExclusionList" => jsonObj.Deserialize<PlacementExclusionList>(options),
                "NegativeKeywordList" => jsonObj.Deserialize<NegativeKeywordList>(options),
                "SharedList" => jsonObj.Deserialize<SharedList>(options),
                "SharedEntity" => jsonObj.Deserialize<SharedEntity>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, SharedEntity value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AccountPlacementInclusionList accountPlacementInclusionList:
                    JsonSerializer.Serialize(writer, accountPlacementInclusionList, options);
                    break;
                case AccountPlacementExclusionList accountPlacementExclusionList:
                    JsonSerializer.Serialize(writer, accountPlacementExclusionList, options);
                    break;
                case BrandList brandList:
                    JsonSerializer.Serialize(writer, brandList, options);
                    break;
                case AccountNegativeKeywordList accountNegativeKeywordList:
                    JsonSerializer.Serialize(writer, accountNegativeKeywordList, options);
                    break;
                case PlacementExclusionList placementExclusionList:
                    JsonSerializer.Serialize(writer, placementExclusionList, options);
                    break;
                case NegativeKeywordList negativeKeywordList:
                    JsonSerializer.Serialize(writer, negativeKeywordList, options);
                    break;
                case SharedList sharedList:
                    JsonSerializer.Serialize(writer, sharedList, options);
                    break;
                case SharedEntity sharedEntity:
                    JsonSerializer.Serialize(writer, sharedEntity, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_SharedListConverter : JsonConverter<SharedList>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_SharedListConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override SharedList? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AccountPlacementInclusionList" => jsonObj.Deserialize<AccountPlacementInclusionList>(options),
                "AccountPlacementExclusionList" => jsonObj.Deserialize<AccountPlacementExclusionList>(options),
                "BrandList" => jsonObj.Deserialize<BrandList>(options),
                "AccountNegativeKeywordList" => jsonObj.Deserialize<AccountNegativeKeywordList>(options),
                "PlacementExclusionList" => jsonObj.Deserialize<PlacementExclusionList>(options),
                "NegativeKeywordList" => jsonObj.Deserialize<NegativeKeywordList>(options),
                "SharedList" => jsonObj.Deserialize<SharedList>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, SharedList value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AccountPlacementInclusionList accountPlacementInclusionList:
                    JsonSerializer.Serialize(writer, accountPlacementInclusionList, options);
                    break;
                case AccountPlacementExclusionList accountPlacementExclusionList:
                    JsonSerializer.Serialize(writer, accountPlacementExclusionList, options);
                    break;
                case BrandList brandList:
                    JsonSerializer.Serialize(writer, brandList, options);
                    break;
                case AccountNegativeKeywordList accountNegativeKeywordList:
                    JsonSerializer.Serialize(writer, accountNegativeKeywordList, options);
                    break;
                case PlacementExclusionList placementExclusionList:
                    JsonSerializer.Serialize(writer, placementExclusionList, options);
                    break;
                case NegativeKeywordList negativeKeywordList:
                    JsonSerializer.Serialize(writer, negativeKeywordList, options);
                    break;
                case SharedList sharedList:
                    JsonSerializer.Serialize(writer, sharedList, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CampaignManagement_SharedListItemConverter : JsonConverter<SharedListItem>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CampaignManagement_SharedListItemConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override SharedListItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "NegativeKeyword" => jsonObj.Deserialize<NegativeKeyword>(options),
                "Site" => jsonObj.Deserialize<Site>(options),
                "BrandItem" => jsonObj.Deserialize<BrandItem>(options),
                "NegativeSite" => jsonObj.Deserialize<NegativeSite>(options),
                "SharedListItem" => jsonObj.Deserialize<SharedListItem>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, SharedListItem value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case NegativeKeyword negativeKeyword:
                    JsonSerializer.Serialize(writer, negativeKeyword, options);
                    break;
                case Site site:
                    JsonSerializer.Serialize(writer, site, options);
                    break;
                case BrandItem brandItem:
                    JsonSerializer.Serialize(writer, brandItem, options);
                    break;
                case NegativeSite negativeSite:
                    JsonSerializer.Serialize(writer, negativeSite, options);
                    break;
                case SharedListItem sharedListItem:
                    JsonSerializer.Serialize(writer, sharedListItem, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }
}