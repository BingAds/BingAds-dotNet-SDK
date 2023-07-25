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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.CampaignManagement
{
    public class PolymorphicSerialization
    {
        public static JsonSerializerOptions? SerializerOptionsWithoutConverters { get; set; }
    }

    public class AllPolymorphicConverters
    {
        public static void AddTo(IList<JsonConverter> list)
        {
            list.Add(new BiddingSchemeConverter());
            list.Add(new SettingConverter());
            list.Add(new BatchErrorConverter());
            list.Add(new ApplicationFaultConverter());
            list.Add(new CriterionBidConverter());
            list.Add(new AdConverter());
            list.Add(new AssetConverter());
            list.Add(new AdExtensionConverter());
            list.Add(new BatchErrorCollectionConverter());
            list.Add(new MediaConverter());
            list.Add(new MediaRepresentationConverter());
            list.Add(new AdGroupCriterionConverter());
            list.Add(new CriterionConverter());
            list.Add(new CriterionCashbackConverter());
            list.Add(new SharedListItemConverter());
            list.Add(new SharedEntityConverter());
            list.Add(new SharedListConverter());
            list.Add(new CampaignCriterionConverter());
            list.Add(new AudienceGroupDimensionConverter());
            list.Add(new AudienceConverter());
            list.Add(new RemarketingRuleConverter());
            list.Add(new RuleItemConverter());
            list.Add(new ConversionGoalConverter());
            list.Add(new ImportJobConverter());
            list.Add(new ImportOptionConverter());
        }
    }

    class BiddingSchemeConverter : JsonConverter<BiddingScheme>
    {
        public override BiddingScheme? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
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
                "BiddingScheme" => jsonObj.Deserialize<BiddingScheme>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, BiddingScheme value, JsonSerializerOptions options)
        {
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
                    JsonSerializer.Serialize(writer, biddingScheme, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class SettingConverter : JsonConverter<Setting>
    {
        public override Setting? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "PerformanceMaxSetting" => jsonObj.Deserialize<PerformanceMaxSetting>(options),
                "ResponsiveSearchAdsSetting" => jsonObj.Deserialize<ResponsiveSearchAdsSetting>(options),
                "HotelSetting" => jsonObj.Deserialize<HotelSetting>(options),
                "DisclaimerSetting" => jsonObj.Deserialize<DisclaimerSetting>(options),
                "VerifiedTrackingSetting" => jsonObj.Deserialize<VerifiedTrackingSetting>(options),
                "CoOpSetting" => jsonObj.Deserialize<CoOpSetting>(options),
                "TargetSetting" => jsonObj.Deserialize<TargetSetting>(options),
                "DynamicSearchAdsSetting" => jsonObj.Deserialize<DynamicSearchAdsSetting>(options),
                "DynamicFeedSetting" => jsonObj.Deserialize<DynamicFeedSetting>(options),
                "ShoppingSetting" => jsonObj.Deserialize<ShoppingSetting>(options),
                "Setting" => jsonObj.Deserialize<Setting>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, Setting value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                case VerifiedTrackingSetting verifiedTrackingSetting:
                    JsonSerializer.Serialize(writer, verifiedTrackingSetting, options);
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
                case Setting setting:
                    JsonSerializer.Serialize(writer, setting, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class BatchErrorConverter : JsonConverter<BatchError>
    {
        public override BatchError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "EditorialError" => jsonObj.Deserialize<EditorialError>(options),
                "BatchError" => jsonObj.Deserialize<BatchError>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, BatchError value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case EditorialError editorialError:
                    JsonSerializer.Serialize(writer, editorialError, options);
                    break;
                case BatchError batchError:
                    JsonSerializer.Serialize(writer, batchError, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        public override ApplicationFault? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AdApiFaultDetail" => jsonObj.Deserialize<AdApiFaultDetail>(options),
                "EditorialApiFaultDetail" => jsonObj.Deserialize<EditorialApiFaultDetail>(options),
                "ApiFaultDetail" => jsonObj.Deserialize<ApiFaultDetail>(options),
                "ApplicationFault" => jsonObj.Deserialize<ApplicationFault>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, ApplicationFault value, JsonSerializerOptions options)
        {
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
                    JsonSerializer.Serialize(writer, applicationFault, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class CriterionBidConverter : JsonConverter<CriterionBid>
    {
        public override CriterionBid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "RateBid" => jsonObj.Deserialize<RateBid>(options),
                "BidMultiplier" => jsonObj.Deserialize<BidMultiplier>(options),
                "FixedBid" => jsonObj.Deserialize<FixedBid>(options),
                "CriterionBid" => jsonObj.Deserialize<CriterionBid>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, CriterionBid value, JsonSerializerOptions options)
        {
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
                    JsonSerializer.Serialize(writer, criterionBid, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class AdConverter : JsonConverter<Ad>
    {
        public override Ad? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
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
                "Ad" => jsonObj.Deserialize<Ad>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, Ad value, JsonSerializerOptions options)
        {
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
                    JsonSerializer.Serialize(writer, ad, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class AssetConverter : JsonConverter<Asset>
    {
        public override Asset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "VideoAsset" => jsonObj.Deserialize<VideoAsset>(options),
                "ImageAsset" => jsonObj.Deserialize<ImageAsset>(options),
                "TextAsset" => jsonObj.Deserialize<TextAsset>(options),
                "Asset" => jsonObj.Deserialize<Asset>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, Asset value, JsonSerializerOptions options)
        {
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
                    JsonSerializer.Serialize(writer, asset, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class AdExtensionConverter : JsonConverter<AdExtension>
    {
        public override AdExtension? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
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
                "AdExtension" => jsonObj.Deserialize<AdExtension>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, AdExtension value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                    JsonSerializer.Serialize(writer, adExtension, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class BatchErrorCollectionConverter : JsonConverter<BatchErrorCollection>
    {
        public override BatchErrorCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "EditorialErrorCollection" => jsonObj.Deserialize<EditorialErrorCollection>(options),
                "BatchErrorCollection" => jsonObj.Deserialize<BatchErrorCollection>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, BatchErrorCollection value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case EditorialErrorCollection editorialErrorCollection:
                    JsonSerializer.Serialize(writer, editorialErrorCollection, options);
                    break;
                case BatchErrorCollection batchErrorCollection:
                    JsonSerializer.Serialize(writer, batchErrorCollection, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class MediaConverter : JsonConverter<Media>
    {
        public override Media? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "Image" => jsonObj.Deserialize<Image>(options),
                "Media" => jsonObj.Deserialize<Media>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, Media value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Image image:
                    JsonSerializer.Serialize(writer, image, options);
                    break;
                case Media media:
                    JsonSerializer.Serialize(writer, media, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class MediaRepresentationConverter : JsonConverter<MediaRepresentation>
    {
        public override MediaRepresentation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "ImageMediaRepresentation" => jsonObj.Deserialize<ImageMediaRepresentation>(options),
                "MediaRepresentation" => jsonObj.Deserialize<MediaRepresentation>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, MediaRepresentation value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ImageMediaRepresentation imageMediaRepresentation:
                    JsonSerializer.Serialize(writer, imageMediaRepresentation, options);
                    break;
                case MediaRepresentation mediaRepresentation:
                    JsonSerializer.Serialize(writer, mediaRepresentation, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class AdGroupCriterionConverter : JsonConverter<AdGroupCriterion>
    {
        public override AdGroupCriterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "NegativeAdGroupCriterion" => jsonObj.Deserialize<NegativeAdGroupCriterion>(options),
                "BiddableAdGroupCriterion" => jsonObj.Deserialize<BiddableAdGroupCriterion>(options),
                "AdGroupCriterion" => jsonObj.Deserialize<AdGroupCriterion>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, AdGroupCriterion value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case NegativeAdGroupCriterion negativeAdGroupCriterion:
                    JsonSerializer.Serialize(writer, negativeAdGroupCriterion, options);
                    break;
                case BiddableAdGroupCriterion biddableAdGroupCriterion:
                    JsonSerializer.Serialize(writer, biddableAdGroupCriterion, options);
                    break;
                case AdGroupCriterion adGroupCriterion:
                    JsonSerializer.Serialize(writer, adGroupCriterion, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class CriterionConverter : JsonConverter<Criterion>
    {
        public override Criterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
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
                "Criterion" => jsonObj.Deserialize<Criterion>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, Criterion value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                    JsonSerializer.Serialize(writer, criterion, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class CriterionCashbackConverter : JsonConverter<CriterionCashback>
    {
        public override CriterionCashback? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CashbackAdjustment" => jsonObj.Deserialize<CashbackAdjustment>(options),
                "CriterionCashback" => jsonObj.Deserialize<CriterionCashback>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, CriterionCashback value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case CashbackAdjustment cashbackAdjustment:
                    JsonSerializer.Serialize(writer, cashbackAdjustment, options);
                    break;
                case CriterionCashback criterionCashback:
                    JsonSerializer.Serialize(writer, criterionCashback, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class SharedListItemConverter : JsonConverter<SharedListItem>
    {
        public override SharedListItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "NegativeKeyword" => jsonObj.Deserialize<NegativeKeyword>(options),
                "NegativeSite" => jsonObj.Deserialize<NegativeSite>(options),
                "SharedListItem" => jsonObj.Deserialize<SharedListItem>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, SharedListItem value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case NegativeKeyword negativeKeyword:
                    JsonSerializer.Serialize(writer, negativeKeyword, options);
                    break;
                case NegativeSite negativeSite:
                    JsonSerializer.Serialize(writer, negativeSite, options);
                    break;
                case SharedListItem sharedListItem:
                    JsonSerializer.Serialize(writer, sharedListItem, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class SharedEntityConverter : JsonConverter<SharedEntity>
    {
        public override SharedEntity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "PlacementExclusionList" => jsonObj.Deserialize<PlacementExclusionList>(options),
                "NegativeKeywordList" => jsonObj.Deserialize<NegativeKeywordList>(options),
                "SharedList" => jsonObj.Deserialize<SharedList>(options),
                "SharedEntity" => jsonObj.Deserialize<SharedEntity>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, SharedEntity value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                    JsonSerializer.Serialize(writer, sharedEntity, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class SharedListConverter : JsonConverter<SharedList>
    {
        public override SharedList? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "PlacementExclusionList" => jsonObj.Deserialize<PlacementExclusionList>(options),
                "NegativeKeywordList" => jsonObj.Deserialize<NegativeKeywordList>(options),
                "SharedList" => jsonObj.Deserialize<SharedList>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, SharedList value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case PlacementExclusionList placementExclusionList:
                    JsonSerializer.Serialize(writer, placementExclusionList, options);
                    break;
                case NegativeKeywordList negativeKeywordList:
                    JsonSerializer.Serialize(writer, negativeKeywordList, options);
                    break;
                case SharedList sharedList:
                    JsonSerializer.Serialize(writer, sharedList, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class CampaignCriterionConverter : JsonConverter<CampaignCriterion>
    {
        public override CampaignCriterion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "BiddableCampaignCriterion" => jsonObj.Deserialize<BiddableCampaignCriterion>(options),
                "NegativeCampaignCriterion" => jsonObj.Deserialize<NegativeCampaignCriterion>(options),
                "CampaignCriterion" => jsonObj.Deserialize<CampaignCriterion>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, CampaignCriterion value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case BiddableCampaignCriterion biddableCampaignCriterion:
                    JsonSerializer.Serialize(writer, biddableCampaignCriterion, options);
                    break;
                case NegativeCampaignCriterion negativeCampaignCriterion:
                    JsonSerializer.Serialize(writer, negativeCampaignCriterion, options);
                    break;
                case CampaignCriterion campaignCriterion:
                    JsonSerializer.Serialize(writer, campaignCriterion, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class AudienceGroupDimensionConverter : JsonConverter<AudienceGroupDimension>
    {
        public override AudienceGroupDimension? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "Audience" => jsonObj.Deserialize<AudienceDimension>(options),
                "Gender" => jsonObj.Deserialize<GenderDimension>(options),
                "Age" => jsonObj.Deserialize<AgeDimension>(options),
                "AudienceGroupDimension" => jsonObj.Deserialize<AudienceGroupDimension>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, AudienceGroupDimension value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                    JsonSerializer.Serialize(writer, audienceGroupDimension, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class AudienceConverter : JsonConverter<Audience>
    {
        public override Audience? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CustomerList" => jsonObj.Deserialize<CustomerList>(options),
                "CombinedList" => jsonObj.Deserialize<CombinedList>(options),
                "SimilarRemarketingList" => jsonObj.Deserialize<SimilarRemarketingList>(options),
                "Product" => jsonObj.Deserialize<ProductAudience>(options),
                "InMarket" => jsonObj.Deserialize<InMarketAudience>(options),
                "Custom" => jsonObj.Deserialize<CustomAudience>(options),
                "RemarketingList" => jsonObj.Deserialize<RemarketingList>(options),
                "Audience" => jsonObj.Deserialize<Audience>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, Audience value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                    JsonSerializer.Serialize(writer, audience, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class RemarketingRuleConverter : JsonConverter<RemarketingRule>
    {
        public override RemarketingRule? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "CustomEvents" => jsonObj.Deserialize<CustomEventsRule>(options),
                "PageVisitorsWhoDidNotVisitAnotherPage" => jsonObj.Deserialize<PageVisitorsWhoDidNotVisitAnotherPageRule>(options),
                "PageVisitorsWhoVisitedAnotherPage" => jsonObj.Deserialize<PageVisitorsWhoVisitedAnotherPageRule>(options),
                "PageVisitors" => jsonObj.Deserialize<PageVisitorsRule>(options),
                "RemarketingRule" => jsonObj.Deserialize<RemarketingRule>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, RemarketingRule value, JsonSerializerOptions options)
        {
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
                    JsonSerializer.Serialize(writer, remarketingRule, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class RuleItemConverter : JsonConverter<RuleItem>
    {
        public override RuleItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "Number" => jsonObj.Deserialize<NumberRuleItem>(options),
                "String" => jsonObj.Deserialize<StringRuleItem>(options),
                "RuleItem" => jsonObj.Deserialize<RuleItem>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, RuleItem value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case NumberRuleItem numberRuleItem:
                    JsonSerializer.Serialize(writer, numberRuleItem, options);
                    break;
                case StringRuleItem stringRuleItem:
                    JsonSerializer.Serialize(writer, stringRuleItem, options);
                    break;
                case RuleItem ruleItem:
                    JsonSerializer.Serialize(writer, ruleItem, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class ConversionGoalConverter : JsonConverter<ConversionGoal>
    {
        public override ConversionGoal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "InStoreTransaction" => jsonObj.Deserialize<InStoreTransactionGoal>(options),
                "OfflineConversion" => jsonObj.Deserialize<OfflineConversionGoal>(options),
                "AppInstall" => jsonObj.Deserialize<AppInstallGoal>(options),
                "Event" => jsonObj.Deserialize<EventGoal>(options),
                "PagesViewedPerVisit" => jsonObj.Deserialize<PagesViewedPerVisitGoal>(options),
                "Duration" => jsonObj.Deserialize<DurationGoal>(options),
                "Url" => jsonObj.Deserialize<UrlGoal>(options),
                "ConversionGoal" => jsonObj.Deserialize<ConversionGoal>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, ConversionGoal value, JsonSerializerOptions options)
        {
            switch (value)
            {
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
                    JsonSerializer.Serialize(writer, conversionGoal, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class ImportJobConverter : JsonConverter<ImportJob>
    {
        public override ImportJob? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "FileImportJob" => jsonObj.Deserialize<FileImportJob>(options),
                "GoogleImportJob" => jsonObj.Deserialize<GoogleImportJob>(options),
                "ImportJob" => jsonObj.Deserialize<ImportJob>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, ImportJob value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case FileImportJob fileImportJob:
                    JsonSerializer.Serialize(writer, fileImportJob, options);
                    break;
                case GoogleImportJob googleImportJob:
                    JsonSerializer.Serialize(writer, googleImportJob, options);
                    break;
                case ImportJob importJob:
                    JsonSerializer.Serialize(writer, importJob, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class ImportOptionConverter : JsonConverter<ImportOption>
    {
        public override ImportOption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "FileImportOption" => jsonObj.Deserialize<FileImportOption>(options),
                "GoogleImportOption" => jsonObj.Deserialize<GoogleImportOption>(options),
                "ImportOption" => jsonObj.Deserialize<ImportOption>(PolymorphicSerialization.SerializerOptionsWithoutConverters),
                _ => throw new InvalidOperationException($"Unknown type '{type}'")
            };
        }

        public override void Write(Utf8JsonWriter writer, ImportOption value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case FileImportOption fileImportOption:
                    JsonSerializer.Serialize(writer, fileImportOption, options);
                    break;
                case GoogleImportOption googleImportOption:
                    JsonSerializer.Serialize(writer, googleImportOption, options);
                    break;
                case ImportOption importOption:
                    JsonSerializer.Serialize(writer, importOption, PolymorphicSerialization.SerializerOptionsWithoutConverters);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }
}