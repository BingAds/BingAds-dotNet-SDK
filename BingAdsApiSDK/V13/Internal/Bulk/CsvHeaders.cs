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
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BingAds.V13.Internal.Bulk
{
    internal class CsvHeaders
    {
        static CsvHeaders()
        {
            Headers = Headers.Where(x => !HiddenHeaders.Contains(x)).ToArray();

            columnIndexMap = InitializeMap();
        }

        public static readonly string[] HiddenHeaders =
        {
            //StringTable.AltText,
            //StringTable.MediaId,
        };

        public static readonly string[] Headers = 
        {
            // common
            StringTable.Type,
            StringTable.Status,
            StringTable.Id,
            StringTable.ParentId,
            StringTable.SubType,
            StringTable.Campaign,
            StringTable.AdGroup,
            StringTable.Website,
            StringTable.SyncTime,
            StringTable.ClientId,
            StringTable.LastModifiedTime,
            StringTable.Language,
            
            // campaign
            StringTable.TimeZone,
            StringTable.BudgetId,
            StringTable.BudgetName,
            StringTable.Budget,
            StringTable.BudgetType,
            StringTable.LocalInventoryAdsEnabled,

            // experiment
            StringTable.TrafficSplitPercent,
            StringTable.BaseCampaignId,
            StringTable.ExperimentCampaignId,
            StringTable.ExperimentId,
            StringTable.ExperimentType,

            // Ad Group
            StringTable.StartDate,
            StringTable.EndDate,
            StringTable.NetworkDistribution,
            StringTable.AdRotation,
            StringTable.CpcBid,   
            StringTable.PrivacyStatus,
            
            // Ads
            StringTable.Title,
            StringTable.Text,
            StringTable.DisplayUrl,
            StringTable.DestinationUrl,
            StringTable.BusinessName,
            StringTable.PhoneNumber,
            StringTable.PromotionalText,
            StringTable.EditorialStatus,
            StringTable.EditorialLocation,
            StringTable.EditorialTerm,
            StringTable.EditorialReasonCode,
            StringTable.EditorialAppealStatus,
            StringTable.DevicePreference,
            StringTable.AdFormatPreference,
            
            // Keywords
            StringTable.Keyword,
            StringTable.MatchType,
            StringTable.Bid,
            StringTable.Param1,
            StringTable.Param2,
            StringTable.Param3,

            // location target
            StringTable.Target,
            StringTable.BidAdjustment,
            StringTable.RadiusTargetId,
            StringTable.Name,
            StringTable.OsNames,
            StringTable.Radius,
            StringTable.Unit,
            StringTable.BusinessId,
          
            // DayTime target
            StringTable.FromHour,
            StringTable.FromMinute,
            StringTable.ToHour,
            StringTable.ToMinute,

            // Profile criterion
            StringTable.Profile,
            StringTable.ProfileId,

            // AdExtensions common
            StringTable.Version,
            StringTable.AdSchedule,
            
            // Use Searcher Time Zone
            StringTable.UseSearcherTimeZone,
            StringTable.AdScheduleUseSearcherTimeZone,

            // Action ad extension
            StringTable.ActionType,
            StringTable.ActionText,

            // Site link ad extensions
            StringTable.SiteLinkExtensionOrder,
            StringTable.SiteLinkDisplayText,
            StringTable.SiteLinkDestinationUrl,
            StringTable.SiteLinkDescription1,
            StringTable.SiteLinkDescription2,

            // Location ad extensions
            StringTable.GeoCodeStatus,
            StringTable.IconMediaId,
            StringTable.ImageMediaId,
            StringTable.AddressLine1,
            StringTable.AddressLine2,
            StringTable.PostalCode,
            StringTable.City,
            StringTable.StateOrProvince,
            StringTable.ProvinceName,
            StringTable.Latitude,
            StringTable.Longitude,

            // Call ad extensions
            StringTable.CountryCode,
            StringTable.IsCallOnly,
            StringTable.IsCallTrackingEnabled,
            StringTable.RequireTollFreeTrackingNumber,

            // Image ad extension
            StringTable.AltText,
            StringTable.MediaIds,
            StringTable.Layouts,
            StringTable.DisplayText,
            
            // Filter link ad extension
            StringTable.AdExtensionHeaderType,
            StringTable.Texts,

            // Editorial rejection reasons
            StringTable.PublisherCountries,
    
            // Product scope
            StringTable.BingMerchantCenterId, 
            StringTable.ProductCondition1,
            StringTable.ProductValue1,
            StringTable.ProductCondition2,
            StringTable.ProductValue2,
            StringTable.ProductCondition3,
            StringTable.ProductValue3,
            StringTable.ProductCondition4,
            StringTable.ProductValue4,
            StringTable.ProductCondition5,
            StringTable.ProductValue5,
            StringTable.ProductCondition6,
            StringTable.ProductValue6,
            StringTable.ProductCondition7,
            StringTable.ProductValue7,

            // BI
            StringTable.Spend,
            StringTable.Impressions,
            StringTable.Clicks,
            StringTable.CTR,
            StringTable.AvgCPC,
            StringTable.AvgCPM,
            StringTable.AvgPosition,
            StringTable.Conversions,
            StringTable.CPA,

            StringTable.QualityScore,
            StringTable.KeywordRelevance,
            StringTable.LandingPageRelevance,
            StringTable.LandingPageUserExperience,

            // App Ad Extension
            StringTable.AppPlatform,
            StringTable.AppStoreId,
            StringTable.IsTrackingEnabled,

            StringTable.Error,
            StringTable.ErrorNumber,
            StringTable.FieldPath,

            // Bing Shopping Campaigns
            StringTable.IsExcluded,
            StringTable.ParentAdGroupCriterionId,
            StringTable.CampaignType,
            StringTable.CampaignPriority,
            
            // CoOp
            StringTable.BidOption,
            StringTable.BidBoostValue,
            StringTable.MaximumBid,

            // Upgrade Url
            StringTable.FinalUrl,
            StringTable.FinalMobileUrl,
            StringTable.TrackingTemplate,
            StringTable.CustomParameter,

            // Price Ad Extension
            StringTable.PriceExtensionType,
            StringTable.CurrencyCode1,
            StringTable.CurrencyCode2,
            StringTable.CurrencyCode3,
            StringTable.CurrencyCode4,
            StringTable.CurrencyCode5,
            StringTable.CurrencyCode6,
            StringTable.CurrencyCode7,
            StringTable.CurrencyCode8,
            StringTable.PriceDescription1,
            StringTable.PriceDescription2,
            StringTable.PriceDescription3,
            StringTable.PriceDescription4,
            StringTable.PriceDescription5,
            StringTable.PriceDescription6,
            StringTable.PriceDescription7,
            StringTable.PriceDescription8,
            StringTable.Header1,
            StringTable.Header2,
            StringTable.Header3,
            StringTable.Header4,
            StringTable.Header5,
            StringTable.Header6,
            StringTable.Header7,
            StringTable.Header8,
            StringTable.FinalMobileUrl1,
            StringTable.FinalMobileUrl2,
            StringTable.FinalMobileUrl3,
            StringTable.FinalMobileUrl4,
            StringTable.FinalMobileUrl5,
            StringTable.FinalMobileUrl6,
            StringTable.FinalMobileUrl7,
            StringTable.FinalMobileUrl8,
            StringTable.FinalUrl1,
            StringTable.FinalUrl2,
            StringTable.FinalUrl3,
            StringTable.FinalUrl4,
            StringTable.FinalUrl5,
            StringTable.FinalUrl6,
            StringTable.FinalUrl7,
            StringTable.FinalUrl8,
            StringTable.Price1,
            StringTable.Price2,
            StringTable.Price3,
            StringTable.Price4,
            StringTable.Price5,
            StringTable.Price6,
            StringTable.Price7,
            StringTable.Price8,
            StringTable.PriceQualifier1,
            StringTable.PriceQualifier2,
            StringTable.PriceQualifier3,
            StringTable.PriceQualifier4,
            StringTable.PriceQualifier5,
            StringTable.PriceQualifier6,
            StringTable.PriceQualifier7,
            StringTable.PriceQualifier8,
            StringTable.PriceUnit1,
            StringTable.PriceUnit2,
            StringTable.PriceUnit3,
            StringTable.PriceUnit4,
            StringTable.PriceUnit5,
            StringTable.PriceUnit6,
            StringTable.PriceUnit7,
            StringTable.PriceUnit8,
            StringTable.TermsAndConditions1,
            StringTable.TermsAndConditions2,
            StringTable.TermsAndConditions3,
            StringTable.TermsAndConditions4,
            StringTable.TermsAndConditions5,
            StringTable.TermsAndConditions6,
            StringTable.TermsAndConditions7,
            StringTable.TermsAndConditions8,
            StringTable.TermsAndConditionsUrl1,
            StringTable.TermsAndConditionsUrl2,
            StringTable.TermsAndConditionsUrl3,
            StringTable.TermsAndConditionsUrl4,
            StringTable.TermsAndConditionsUrl5,
            StringTable.TermsAndConditionsUrl6,
            StringTable.TermsAndConditionsUrl7,
            StringTable.TermsAndConditionsUrl8,

            // Review Ad Extension
            StringTable.IsExact,
            StringTable.Source,
            StringTable.Url,
            
            // Image
            StringTable.Height,
            StringTable.Width,

            // Callout Ad Extension
            StringTable.CalloutText,

            // AutoBidding
            StringTable.BidStrategyType,
            StringTable.BidStrategyMaxCpc,
            StringTable.BidStrategyTargetCpa,
            StringTable.BidStrategyTargetRoas,
            StringTable.InheritedBidStrategyType,
            StringTable.BidStrategyTargetAdPosition,
            StringTable.BidStrategyTargetImpressionShare,

            // Target and bid
            StringTable.TargetSetting,

            // Remarketing
            StringTable.Description,
            StringTable.MembershipDuration,
            StringTable.Scope,
            StringTable.TagId,
            StringTable.SourceId,
            StringTable.AudienceId,
            StringTable.Audience,
            StringTable.RemarketingRule,
            StringTable.AudienceSearchSize,
            StringTable.AudienceNetworkSize,
            StringTable.SupportedCampaignTypes,
            StringTable.ProductAudienceType,
            StringTable.CombinationRule,

            // Expanded Text Ad
            StringTable.TitlePart1,
            StringTable.TitlePart2,
            StringTable.TitlePart3,
            StringTable.TextPart2,
            StringTable.Path1,
            StringTable.Path2,
            StringTable.Domain,

            // Responsive Ad
            StringTable.CallToAction,
            StringTable.Headline,
            StringTable.Images,
            StringTable.LandscapeImageMediaId,
            StringTable.LandscapeLogoMediaId,
            StringTable.LongHeadline,
            StringTable.SquareImageMediaId,
            StringTable.SquareLogoMediaId,

            // Structured Snippet Ad Extension
            StringTable.StructuredSnippetHeader,
            StringTable.StructuredSnippetValues,
			
			// Promotion Ad Extension
            StringTable.PromotionTarget,
            StringTable.DiscountModifier,
            StringTable.PercentOff,
            StringTable.MoneyAmountOff,
            StringTable.PromotionCode,
            StringTable.OrdersOverAmount,
            StringTable.Occasion,
            StringTable.PromotionStart,
            StringTable.PromotionEnd,
            StringTable.CurrencyCode,

            // Dynamic Search Ad
            StringTable.DomainLanguage,
            StringTable.DynamicAdTargetCondition1,
            StringTable.DynamicAdTargetCondition2,
            StringTable.DynamicAdTargetCondition3,
            StringTable.DynamicAdTargetValue1,
            StringTable.DynamicAdTargetValue2,
            StringTable.DynamicAdTargetValue3,
            StringTable.PageFeedIds,

            // Labels
            StringTable.ColorCode,
            StringTable.Label,

            // Offline Conversions
            StringTable.ConversionCurrencyCode,
            StringTable.ConversionName,
            StringTable.ConversionTime,
            StringTable.ConversionValue,
            StringTable.MicrosoftClickId,
            StringTable.AdjustmentValue,
            StringTable.AdjustmentTime,
            StringTable.AdjustmentCurrencyCode,
            StringTable.AdjustmentType,
            StringTable.ExternalAttributionCredit,
            StringTable.ExternalAttributionModel,
            
            // Account
            StringTable.MSCLKIDAutoTaggingEnabled,
            StringTable.IncludeViewThroughConversions,
            StringTable.ProfileExpansionEnabled,
            
            //Final Url Suffix
            StringTable.FinalUrlSuffix,

            // Feeds
            StringTable.CustomAttributes,
            StringTable.FeedName,
            StringTable.PhysicalIntent,
            StringTable.TargetAdGroupId,
            StringTable.TargetCampaignId,
    };

        /// <summary>
        /// maps column name into its positional index
        /// </summary>
        private static readonly Dictionary<string, int> columnIndexMap = InitializeMap();

        public static Dictionary<string, int> GetMappings()
        {
            return columnIndexMap;
        }

        public static int GetColumnIndex(string columnName)
        {
            try
            {
                return columnIndexMap[columnName];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException(string.Format("Column name {0} is not present in headers.", columnName));
            }
        }

        private static Dictionary<string, int> InitializeMap()
        {
            int i = 0;
            return CsvHeaders.Headers.ToDictionary(nextColumn => nextColumn, nextColumn => i++);
        }
    }
}
