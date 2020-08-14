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

namespace Microsoft.BingAds.V13.Internal.Bulk
{
    internal class StringTable
    {
        // Bulk Datetime format
        public static string[] LocalDateTimeFormats =
        {
            "M/d/yyyy HH:mm",
            "M/d/yyyy h:mm tt",
            "M/d/yyyy hh:mm tt",
            "M/d/yyyy H:mm",
            "M/d/yyyy HH:mm:ss",
            "M/d/yyyy h:mm:ss tt",
            "M/d/yyyy hh:mm:ss tt",
            "M/d/yyyy H:mm:ss",
            "M/d/yyyy HH:mm:ss.fff",
            "M/d/yyyy h:mm:ss.fff tt",
            "M/d/yyyy hh:mm:ss.fff tt",
            "M/d/yyyy H:mm:ss.fff",

            "M/dd/yyyy",

            "M/d/yy HH:mm",
            "M/d/yy h:mm tt",
            "M/d/yy hh:mm tt",
            "M/d/yy H:mm",
            "M/d/yy HH:mm:ss",
            "M/d/yy h:mm:ss tt",
            "M/d/yy hh:mm:ss tt",
            "M/d/yy H:mm:ss",
            "M/d/yy HH:mm:ss.fff",
            "M/d/yy h:mm:ss.fff tt",
            "M/d/yy hh:mm:ss.fff tt",
            "M/d/yy H:mm:ss.fff",


            "MM/dd/yy HH:mm",
            "MM/dd/yy h:mm tt",
            "MM/dd/yy hh:mm tt",
            "MM/dd/yy H:mm",
            "MM/dd/yy HH:mm:ss",
            "MM/dd/yy h:mm:ss tt",
            "MM/dd/yy hh:mm:ss tt",
            "MM/dd/yy H:mm:ss",
            "MM/dd/yy HH:mm:ss.fff",
            "MM/dd/yy h:mm:ss.fff tt",
            "MM/dd/yy hh:mm:ss.fff tt",
            "MM/dd/yy H:mm:ss.fff",

            "MM/dd/yyyy HH:mm",
            "MM/dd/yyyy h:mm tt",
            "MM/dd/yyyy hh:mm tt",
            "MM/dd/yyyy H:mm",
            "MM/dd/yyyy HH:mm:ss",
            "MM/dd/yyyy h:mm:ss tt",
            "MM/dd/yyyy hh:mm:ss tt",
            "MM/dd/yyyy H:mm:ss",
            "MM/dd/yyyy HH:mm:ss.fff",
            "MM/dd/yyyy h:mm:ss.fff tt",
            "MM/dd/yyyy hh:mm:ss.fff tt",
            "MM/dd/yyyy H:mm:ss.fff",

            "yy/MM/dd HH:mm",
            "yy/MM/dd h:mm tt",
            "yy/MM/dd hh:mm tt",
            "yy/MM/dd H:mm",
            "yy/MM/dd HH:mm:ss",
            "yy/MM/dd h:mm:ss tt",
            "yy/MM/dd hh:mm:ss tt",
            "yy/MM/dd H:mm:ss",
            "yy/MM/dd HH:mm:ss.fff",
            "yy/MM/dd h:mm:ss.fff tt",
            "yy/MM/dd hh:mm:ss.fff tt",
            "yy/MM/dd H:mm:ss.fff",

            "yyyy-MM-dd HH:mm",
            "yyyy-MM-dd h:mm tt",
            "yyyy-MM-dd hh:mm tt",
            "yyyy-MM-dd H:mm",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd h:mm:ss tt",
            "yyyy-MM-dd hh:mm:ss tt",
            "yyyy-MM-dd H:mm:ss",
            "yyyy-MM-dd HH:mm:ss.fff",
            "yyyy-MM-dd h:mm:ss.fff tt",
            "yyyy-MM-dd hh:mm:ss.fff tt",
            "yyyy-MM-dd H:mm:ss.fff",

            "dd-mmm-yy HH:mm",
            "dd-mmm-yy h:mm tt",
            "dd-mmm-yy hh:mm tt",
            "dd-mmm-yy H:mm",
            "dd-mmm-yy HH:mm:ss",
            "dd-mmm-yy h:mm:ss tt",
            "dd-mmm-yy hh:mm:ss tt",
            "dd-mmm-yy H:mm:ss",
            "dd-mmm-yy HH:mm:ss.fff",
            "dd-mmm-yy h:mm:ss.fff tt",
            "dd-mmm-yy hh:mm:ss.fff tt",
            "dd-mmm-yy H:mm:ss.fff",

            "yyyy-MM-ddTHH:mm",
            "yyyy-MM-ddTh:mm tt",
            "yyyy-MM-ddThh:mm tt",
            "yyyy-MM-ddTH:mm",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-ddTh:mm:ss tt",
            "yyyy-MM-ddThh:mm:ss tt",
            "yyyy-MM-ddTH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss.fff",
            "yyyy-MM-ddTh:mm:ss.fff tt",
            "yyyy-MM-ddThh:mm:ss.fff tt",
            "yyyy-MM-ddTH:mm:ss.fff",

            "yyyy-M-ddTHH:mm:ss",
            "yyyy-MM-dTHH:mm:ss",
            "yyyy-M-dTHH:mm:ss",
            "yyyy-M-dd HH:mm:ss",
            "yyyy-MM-d HH:mm:ss",
            "yyyy-M-d HH:mm:ss",
            "MMM dd, yyyy hh:mm:ss tt",
            "MMM d, yyyy hh:mm:ss tt",
            "M/dd/yyyy hh:mm:ss tt",
            "MM/d/yyyy hh:mm:ss tt",
            "M/dd/yyyy HH:mm:ss",
            "MM/d/yyyy HH:mm:ss",
            "yyyy-M-ddTHH:mm:ss.fff",
            "yyyy-MM-dTHH:mm:ss.fff",
            "yyyy-M-dTHH:mm:ss.fff",
            "yyyy-M-dd HH:mm:ss.fff",
            "yyyy-MM-d HH:mm:ss.fff",
            "yyyy-M-d HH:mm:ss.fff",
            "MMM dd, yyyy hh:mm:ss.fff tt",
            "MMM d, yyyy hh:mm:ss.fff tt",
            "M/dd/yyyy hh:mm:ss.fff tt",
            "MM/d/yyyy hh:mm:ss.fff tt",
            "M/dd/yyyy HH:mm:ss.fff",
            "MM/d/yyyy HH:mm:ss.fff",

            "dd-MMM-yy HH:mm:ss",
            "dd-MMM-yy HH:mm:ss tt",
            "dd-MMM-yy HH:mm:ss.fff",
            "dd-MMM-yy HH:mm:ss.fff tt",
            "dd-MMM-yy HH:mm",
            "dd-MMM-yy HH:mm tt",
            "dd-MMM-yyyy HH:mm:ss",
            "dd-MMM-yyyy HH:mm:ss tt",
            "dd-MMM-yyyy HH:mm:ss.fff",
            "dd-MMM-yyyy HH:mm:ss.fff tt",
            "dd-MMM-yyyy HH:mm",
            "dd-MMM-yyyy HH:mm tt",
        };

        // CSV header strings
        public const string ClientId = "Client Id";

        public const string Type = "Type";

        public const string Status = "Status";

        public const string Campaign = "Campaign";

        public const string Id = "Id";

        public const string BusinessId = "Business Id";

        public const string ParentId = "Parent Id";

        public const string TimeZone = "Time Zone";

        public const string Budget = "Budget";
        public const string BudgetId = "Budget Id";
        public const string BudgetType = "Budget Type";
        public const string BudgetName = "Budget Name";

        public const string Experiment = "Experiment";
        public const string TrafficSplitPercent = "Traffic Split Percent";
        public const string BaseCampaignId = "Base Campaign Id";
        public const string ExperimentCampaignId = "Experiment Campaign Id";
        public const string ExperimentId = "Experiment Id";
        public const string ExperimentType = "Experiment Type";

        public const string AdGroup = "Ad Group";
        public const string Keyword = "Keyword";

        public const string TextAd = "Text Ad";
        public const string MobileAd = "Mobile Ad";
        public const string ProductAd = "Product Ad";
        public const string AppInstallAd = "App Install Ad";

        public const string Title = "Title";
        public const string EditorialStatus = "Editorial Status";
        public const string EditorialAppealStatus = "Editorial Appeal Status";
        public const string Error = "Error";
        public const string ErrorNumber = "Error Number";
        public const string FieldPath = "Field Path";
        public const string EditorialLocation = "Editorial Location";
        public const string EditorialTerm = "Editorial Term";
        public const string EditorialReasonCode = "Editorial Reason Code";

        public const string MigrationErrors = "Migration Errors";

        public const string DisplayUrl = "Display Url";
        public const string DestinationUrl = "Destination Url";
        public const string BusinessName = "Business Name";
        public const string PhoneNumber = "Phone Number";
        public const string PromotionalText = "Promotion";
        public const string MatchType = "Match Type";
        public const string Param1 = "Param1";
        public const string Param2 = "Param2";
        public const string Param3 = "Param3";
        public const string DevicePreference = "Device Preference";

        public const string CampaignNegativeKeyword = "Campaign Negative Keyword";
        public const string CampaignNegativeSite = "Campaign Negative Site";
        public const string AdGroupNegativeKeyword = "Ad Group Negative Keyword";
        public const string KeywordNegativeKeyword = "Keyword Negative Keyword";

        public const string AdGroupNegativeSite = "Ad Group Negative Site";
        public const string Text = "Text";
        public const string Website = "Website";

        public const string Target = "Target";

        public const string Feed = "Feed";
        public const string FeedItem = "Feed Item";
        public const string FeedName = "Feed Name";
        public const string CustomAttributes = "Custom Attributes";
        public const string TargetAdGroupId = "Target Ad Group Id";
        public const string TargetCampaignId = "Target Campaign Id";

        public const string PhysicalIntent = "Physical Intent";

        public const string Bid = "Bid";

        public const string Profile = "Profile";
        public const string ProfileId = "Profile Id";
        public const string BidAdjustment = "Bid Adjustment";
        public const string SubType = "Sub Type";

        public const string OsNames = "OS Names";

        public const string StartDate = "Start Date";
        public const string EndDate = "End Date";
        public const string NetworkDistribution = "Network Distribution";
        public const string Language = "Language";
        public const string CpcBid = "Cpc Bid";
        public const string AdRotation = "Ad Rotation";
        public const string PrivacyStatus = "Privacy Status";

        public const string Account = "Account";
        public const string SyncTime = "Sync Time";
        public const string Name = "Name";
        public const string MSCLKIDAutoTaggingEnabled = "MSCLKID Auto Tagging Enabled";
        public const string IncludeViewThroughConversions = "Include View Through Conversions";
        public const string ProfileExpansionEnabled = "Profile Expansion Enabled";

        public const string LastModifiedTime = "Modified Time";

        public const string AdFormatPreference = "Ad Format Preference";

        // entity types
        public const string SemanticVersion = "Format Version";
        public const string LocationTarget = "Location Target";
        public const string RadiusTarget = "Radius Target";
        public const string BusinessTarget = "Business Location Target";
        public const string RadiusTargetId = "Radius Target Id";
        public const string NegativeLocationTarget = "Negative Location Target";
        public const string AgeTarget = "Age Target";
        public const string GenderTarget = "Gender Target";
        public const string DayTarget = "Day Target";
        public const string HourTarget = "Hour Target";
        public const string DeviceOsTarget = "DeviceOS Target";
        public const string Radius = "Radius";
        public const string Unit = "Unit";

        public const string LocationAdExtension = "Location Ad Extension";
        public const string CallAdExtension = "Call Ad Extension";
        public const string ImageAdExtension = "Image Ad Extension";

        public const string Version = "Version";

        //Image ad extension
        public const string AltText = "Alternative Text";
        public const string MediaIds = "Media Ids";
        public const string AccountImageAdExtension = "Account Image Ad Extension";
        public const string CampaignImageAdExtension = "Campaign Image Ad Extension";
        public const string AdGroupImageAdExtension = "Ad Group Image Ad Extension";
        public const string Layouts = "Layouts";
        public const string DisplayText = "Display Text";

        // Filter Link ad extension
        public const string FilterLinkAdExtension = "Filter Link Ad Extension";
        public const string AccountFilterLinkAdExtension = "Account Filter Link Ad Extension";
        public const string CampaignFilterLinkAdExtension = "Campaign Filter Link Ad Extension";
        public const string AdGroupFilterLinkAdExtension = "Ad Group Filter Link Ad Extension";
        public const string AdExtensionHeaderType = "AdExtension Header Type";
        public const string Texts = "Texts";

        // Sitelink Ad Extension
        public const string SitelinkAdExtension = "Sitelink Ad Extension";
        public const string AccountSitelinkAdExtension = "Account Sitelink Ad Extension";
        public const string CampaignSitelinkAdExtension = "Campaign Sitelink Ad Extension";
        public const string AdGroupSitelinkAdExtension = "Ad Group Sitelink Ad Extension";
        public const string SiteLinkExtensionOrder = "Sitelink Extension Order";
        public const string SiteLinkDisplayText = "Sitelink Extension Link Text";
        public const string SiteLinkDestinationUrl = "Sitelink Extension Destination Url";
        public const string SiteLinkDescription1 = "Sitelink Extension Description1";
        public const string SiteLinkDescription2 = "Sitelink Extension Description2";

        // location ad extensions
        public const string AddressLine1 = "Address Line 1";
        public const string AddressLine2 = "Address Line 2";
        public const string PostalCode = "Postal Code";
        public const string City = "City";
        public const string StateOrProvince = "State Or Province Code";
        public const string ProvinceName = "Province Name";

        public const string GeoCodeStatus = "Geo Code Status";
        public const string IconMediaId = "Map Icon";
        public const string ImageMediaId = "Business Icon";
        public const string AccountLocationAdExtension = "Account Location Ad Extension";
        public const string CampaignLocationAdExtension = "Campaign Location Ad Extension";

        // Call ad extensions
        public const string CountryCode = "Country Code";
        public const string IsCallOnly = "Call Only";
        public const string CampaignCallAdExtension = "Campaign Call Ad Extension";
        public const string IsCallTrackingEnabled = "Call Tracking Enabled";
        public const string RequireTollFreeTrackingNumber = "Toll Free";

        // Editorial rejection reasons
        public const string PublisherCountries = "Publisher Countries";

        // BTE types
        public const string KeywordFirstPageBidType = "Keyword First Page Bid";
        public const string KeywordMainLineBidType = "Keyword Main Line Bid";
        public const string KeywordBestPositionBidType = "Keyword Best Position Bid";

        // Product ad extension
        public const string ProductCondition1 = "Product Condition 1";
        public const string ProductCondition2 = "Product Condition 2";
        public const string ProductCondition3 = "Product Condition 3";
        public const string ProductCondition4 = "Product Condition 4";
        public const string ProductCondition5 = "Product Condition 5";
        public const string ProductCondition6 = "Product Condition 6";
        public const string ProductCondition7 = "Product Condition 7";
        public const string ProductValue1 = "Product Value 1";
        public const string ProductValue2 = "Product Value 2";
        public const string ProductValue3 = "Product Value 3";
        public const string ProductValue4 = "Product Value 4";
        public const string ProductValue5 = "Product Value 5";
        public const string ProductValue6 = "Product Value 6";
        public const string ProductValue7 = "Product Value 7";
        public const string BingMerchantCenterId = "Store Id";

        // App Ad Extension        
        public const string AppAdExtension = "App Ad Extension";
        public const string AppPlatform = "App Platform";
        public const string AppStoreId = "App Id";
        public const string AccountAppAdExtension = "Account App Ad Extension";
        public const string CampaignAppAdExtension = "Campaign App Ad Extension";
        public const string AdGroupAppAdExtension = "Ad Group App Ad Extension";
        public const string IsTrackingEnabled = "Tracking Enabled";

        // misc stuff
        public const string EntityActiveStatus = "Active";
        public const string EntityDeletedStatus = "Deleted";
        public const char IntraFieldSeparator = ';';
        public const string Active = "Active";
        public const string Latitude = "Latitude";
        public const string Longitude = "Longitude";
                
        // BI
        public const string Spend = "Spend";
        public const string Impressions = "Impressions";
        public const string Clicks = "Clicks";
        public const string CTR = "CTR";
        public const string AvgCPC = "Avg CPC";
        public const string AvgCPM = "Avg CPM";
        public const string AvgPosition = "Avg position";
        public const string Conversions = "Conversions";
        public const string CPA = "CPA";

        public const string QualityScore = "Quality Score";
        public const string KeywordRelevance = "Keyword Relevance";
        public const string LandingPageRelevance = "Landing Page Relevance";
        public const string LandingPageUserExperience = "Landing Page User Experience";

        // DayTime target
        public const string DayTimeTarget = "DayTime Target";
        public const string FromHour = "From Hour";
        public const string ToHour = "To Hour";
        public const string FromMinute = "From Minute";
        public const string ToMinute = "To Minute";

        // Shared Entities
        public const string NegativeKeywordList = "Negative Keyword List";
        public const string CampaignNegativeKeywordList = "Campaign Negative Keyword List Association";
        public const string ListNegativeKeyword = "Shared Negative Keyword";

        // Subtypes
        public const string MetroAreaSubType = "Metro Area";
        public const string CountrySubType = "Country";
        public const string StateSubType = "State";
        public const string CitySubType = "City";
        public const string PostalCodeSubType = "Postal Code";

        // BSC
        public const string IsExcluded = "Is Excluded";
        public const string ParentAdGroupCriterionId = "Parent Criterion Id";
        public const string CampaignType = "Campaign Type";
        public const string CampaignPriority = "Priority";
        public const string LocalInventoryAdsEnabled = "LocalInventoryAdsEnabled";

        // SPA
        public const string CampaignNegativeStoreCriterion = "Campaign Negative Store Criterion";

        // CoOp
        public const string BidOption = "Bid Option";
        public const string BidBoostValue = "Bid Boost Value";
        public const string MaximumBid = "Maximum Bid";

        // Upgrade URL
        public const string FinalUrl = "Final Url";
        public const string FinalMobileUrl = "Mobile Final Url";
        public const string TrackingTemplate = "Tracking Template";
        public const string CustomParameter = "Custom Parameter";

        // Review Ad Extension
        public const string ReviewAdExtension = "Review Ad Extension";
        public const string AccountReviewAdExtension = "Account Review Ad Extension";
        public const string CampaignReviewAdExtension = "Campaign Review Ad Extension";
        public const string AdGroupReviewAdExtension = "Ad Group Review Ad Extension";
        public const string IsExact = "Is Exact";
        public const string Source = "Source";
        public const string Url = "Url";

        // Price Ad Extension
        public const string PriceAdExtension = "Price Ad Extension";
        public const string AccountPriceAdExtension = "Account Price Ad Extension";
        public const string CampaignPriceAdExtension = "Campaign Price Ad Extension";
        public const string AdGroupPriceAdExtension = "Ad Group Price Ad Extension";
        public const string PriceExtensionType = "Price Extension Type";
        public const string Header1 = "Header 1";
        public const string Header2 = "Header 2";  
        public const string Header3 = "Header 3";
        public const string Header4 = "Header 4";
        public const string Header5 = "Header 5";
        public const string Header6 = "Header 6";
        public const string Header7 = "Header 7";
        public const string Header8 = "Header 8";
        public const string PriceDescription1 = "Price Description 1";
        public const string PriceDescription2 = "Price Description 2";
        public const string PriceDescription3 = "Price Description 3";
        public const string PriceDescription4 = "Price Description 4";
        public const string PriceDescription5 = "Price Description 5";
        public const string PriceDescription6 = "Price Description 6";
        public const string PriceDescription7 = "Price Description 7";
        public const string PriceDescription8 = "Price Description 8";
        public const string FinalUrl1 = "Final Url 1";
        public const string FinalUrl2 = "Final Url 2";
        public const string FinalUrl3 = "Final Url 3";
        public const string FinalUrl4 = "Final Url 4";
        public const string FinalUrl5 = "Final Url 5";
        public const string FinalUrl6 = "Final Url 6";
        public const string FinalUrl7 = "Final Url 7";
        public const string FinalUrl8 = "Final Url 8";
        public const string FinalMobileUrl1 = "Final Mobile Url 1";
        public const string FinalMobileUrl2 = "Final Mobile Url 2";
        public const string FinalMobileUrl3 = "Final Mobile Url 3";
        public const string FinalMobileUrl4 = "Final Mobile Url 4";
        public const string FinalMobileUrl5 = "Final Mobile Url 5";
        public const string FinalMobileUrl6 = "Final Mobile Url 6";
        public const string FinalMobileUrl7 = "Final Mobile Url 7";
        public const string FinalMobileUrl8 = "Final Mobile Url 8";
        public const string Price1 = "Price 1"; 
        public const string Price2 = "Price 2";
        public const string Price3 = "Price 3";
        public const string Price4 = "Price 4";
        public const string Price5 = "Price 5";
        public const string Price6 = "Price 6";
        public const string Price7 = "Price 7";
        public const string Price8 = "Price 8";
        public const string CurrencyCode1 = "Currency Code 1";
        public const string CurrencyCode2 = "Currency Code 2";
        public const string CurrencyCode3 = "Currency Code 3";
        public const string CurrencyCode4 = "Currency Code 4";
        public const string CurrencyCode5 = "Currency Code 5";
        public const string CurrencyCode6 = "Currency Code 6";
        public const string CurrencyCode7 = "Currency Code 7";
        public const string CurrencyCode8 = "Currency Code 8";
        public const string PriceUnit1 = "Price Unit 1";
        public const string PriceUnit2 = "Price Unit 2";
        public const string PriceUnit3 = "Price Unit 3";
        public const string PriceUnit4 = "Price Unit 4";
        public const string PriceUnit5 = "Price Unit 5";
        public const string PriceUnit6 = "Price Unit 6";
        public const string PriceUnit7 = "Price Unit 7";
        public const string PriceUnit8 = "Price Unit 8";
        public const string PriceQualifier1 = "Price Qualifier 1";
        public const string PriceQualifier2 = "Price Qualifier 2";
        public const string PriceQualifier3 = "Price Qualifier 3";
        public const string PriceQualifier4 = "Price Qualifier 4";
        public const string PriceQualifier5 = "Price Qualifier 5";
        public const string PriceQualifier6 = "Price Qualifier 6";
        public const string PriceQualifier7 = "Price Qualifier 7";
        public const string PriceQualifier8 = "Price Qualifier 8";
        public const string TermsAndConditions1 = "Terms And Conditions 1";
        public const string TermsAndConditions2 = "Terms And Conditions 2";
        public const string TermsAndConditions3 = "Terms And Conditions 3";
        public const string TermsAndConditions4 = "Terms And Conditions 4";
        public const string TermsAndConditions5 = "Terms And Conditions 5";
        public const string TermsAndConditions6 = "Terms And Conditions 6";
        public const string TermsAndConditions7 = "Terms And Conditions 7";
        public const string TermsAndConditions8 = "Terms And Conditions 8";
        public const string TermsAndConditionsUrl1 = "Terms And Conditions Url 1";
        public const string TermsAndConditionsUrl2 = "Terms And Conditions Url 2";
        public const string TermsAndConditionsUrl3 = "Terms And Conditions Url 3";
        public const string TermsAndConditionsUrl4 = "Terms And Conditions Url 4";
        public const string TermsAndConditionsUrl5 = "Terms And Conditions Url 5";
        public const string TermsAndConditionsUrl6 = "Terms And Conditions Url 6";
        public const string TermsAndConditionsUrl7 = "Terms And Conditions Url 7";
        public const string TermsAndConditionsUrl8 = "Terms And Conditions Url 8";

        // Callout Ad Extension
        public const string CalloutAdExtension = "Callout Ad Extension";
        public const string AccountCalloutAdExtension = "Account Callout Ad Extension";
        public const string CampaignCalloutAdExtension = "Campaign Callout Ad Extension";
        public const string AdGroupCalloutAdExtension = "Ad Group Callout Ad Extension";
        public const string CalloutText = "Callout Text";

        // Action Ad Extension
        public const string ActionAdExtension = "Action Ad Extension";
        public const string AccountActionAdExtension = "Account Action Ad Extension";
        public const string CampaignActionAdExtension = "Campaign Action Ad Extension";
        public const string AdGroupActionAdExtension = "Ad Group Action Ad Extension";
        public const string ActionType = "Action Type";
        public const string ActionText = "Action Text";

        //Promotion AdExtension
        public const string PromotionAdExtension = "Promotion Ad Extension";
        public const string AccountPromotionAdExtension = "Account Promotion Ad Extension";
        public const string CampaignPromotionAdExtension = "Campaign Promotion Ad Extension";
        public const string AdGroupPromotionAdExtension = "Ad Group Promotion Ad Extension";
        public const string PromotionTarget = "Promotion Target";
        public const string DiscountModifier = "Discount Modifier";
        public const string PercentOff = "Percent Off";
        public const string MoneyAmountOff = "Money Amount Off";
        public const string PromotionCode = "Promotion Code";
        public const string OrdersOverAmount = "Orders Over Amount";
        public const string Occasion = "Occasion";
        public const string PromotionStart = "Promotion Start";
        public const string PromotionEnd = "Promotion End";
        public const string CurrencyCode = "Currency Code";


        // AutoBidding
        public const string BidStrategyType = "Bid Strategy Type";
        public const string BidStrategyMaxCpc = "Bid Strategy MaxCpc";
        public const string BidStrategyTargetCpa = "Bid Strategy TargetCpa";
        public const string BidStrategyTargetRoas = "Bid Strategy TargetRoas";
        public const string InheritedBidStrategyType = "Inherited Bid Strategy Type";
        public const string BidStrategyTargetAdPosition = "Bid Strategy TargetAdPosition";
        public const string BidStrategyTargetImpressionShare = "Bid Strategy TargetImpressionShare";

        // Target and bid
        public const string TargetSetting = "Target Setting";

        // Audience
        public const string Audience = "Audience";
        public const string RemarketingList = "Remarketing List";
        public const string AdGroupRemarketingListAssociation = "Ad Group Remarketing List Association";
        public const string AdGroupNegativeRemarketingListAssociation = "Ad Group Negative Remarketing List Association";
        public const string CampaignRemarketingListAssociation = "Campaign Remarketing List Association";
        public const string CampaignNegativeRemarketingListAssociation = "Campaign Negative Remarketing List Association";
        public const string CustomAudience = "Custom Audience";
        public const string AdGroupCustomAudienceAssociation = "Ad Group Custom Audience Association";
        public const string AdGroupNegativeCustomAudienceAssociation = "Ad Group Negative Custom Audience Association";
        public const string CampaignCustomAudienceAssociation = "Campaign Custom Audience Association";
        public const string CampaignNegativeCustomAudienceAssociation = "Campaign Negative Custom Audience Association";
        public const string InMarketAudience = "In Market Audience";
        public const string AdGroupInMarketAudienceAssociation = "Ad Group In Market Audience Association";
        public const string AdGroupNegativeInMarketAudienceAssociation = "Ad Group Negative In Market Audience Association";
        public const string CampaignInMarketAudienceAssociation = "Campaign In Market Audience Association";
        public const string CampaignNegativeInMarketAudienceAssociation = "Campaign Negative In Market Audience Association";
        public const string ProductAudience = "Product Audience";
        public const string AdGroupProductAudienceAssociation = "Ad Group Product Audience Association";
        public const string AdGroupNegativeProductAudienceAssociation = "Ad Group Negative Product Audience Association";
        public const string CampaignProductAudienceAssociation = "Campaign Product Audience Association";
        public const string CampaignNegativeProductAudienceAssociation = "Campaign Negative Product Audience Association";
        public const string SimilarRemarketingList = "Similar Remarketing List";
        public const string AdGroupSimilarRemarketingListAssociation = "Ad Group Similar Remarketing List Association";
        public const string AdGroupNegativeSimilarRemarketingListAssociation = "Ad Group Negative Similar Remarketing List Association";
        public const string CampaignSimilarRemarketingListAssociation = "Campaign Similar Remarketing List Association";
        public const string CampaignNegativeSimilarRemarketingListAssociation = "Campaign Negative Similar Remarketing List Association";
        public const string CombinedList = "Combined List";
        public const string AdGroupCombinedListAssociation = "Ad Group Combined List Association";
        public const string AdGroupNegativeCombinedListAssociation = "Ad Group Negative Combined List Association";
        public const string CampaignCombinedListAssociation = "Campaign Combined List Association";
        public const string CampaignNegativeCombinedListAssociation = "Campaign Negative Combined List Association";
        public const string Description = "Description";
        public const string MembershipDuration = "Membership Duration";
        public const string Scope = "Scope";
        public const string SourceId = "Source Id";
        public const string TagId = "UET Tag Id";
        public const string AudienceId = "Audience Id";
        public const string RemarketingRule = "Remarketing Rule";
        public const string AudienceSearchSize = "Audience Search Size";
        public const string AudienceNetworkSize = "Audience Network Size";
        public const string SupportedCampaignTypes = "Supported Campaign Types";
        public const string ProductAudienceType = "Product Audience Type";
        public const string CombinationRule = "Combination Rule";
        public const string CustomerList = "Customer List";
        public const string CustomerListItem = "Customer List Item";
        public const string AdGroupCustomerListAssociation = "Ad Group Customer List Association";
        public const string AdGroupNegativeCustomerListAssociation = "Ad Group Negative Customer List Association";
        public const string CampaignCustomerListAssociation = "Campaign Customer List Association";
        public const string CampaignNegativeCustomerListAssociation = "Campaign Negative Customer List Association";

        // Expanded Text Ad
        public const string ExpandedTextAd = "Expanded Text Ad";
        public const string TitlePart1 = "Title Part 1";
        public const string TitlePart2 = "Title Part 2";
        public const string TitlePart3 = "Title Part 3";
        public const string TextPart2 = "Text Part 2";
        public const string Path1 = "Path 1";
        public const string Path2 = "Path 2";
        public const string Domain = "Domain";

        // Responsive Ad
        public const string ResponsiveAd = "Responsive Ad";
        public const string CallToAction = "Call To Action";
        public const string Headline = "Headline";
        public const string Images = "Images";
        public const string LandscapeImageMediaId = "Landscape Image Media Id";
        public const string LandscapeLogoMediaId = "Landscape Logo Media Id";
        public const string LongHeadline = "Long Headline";
        public const string SquareImageMediaId = "Square Image Media Id";
        public const string SquareLogoMediaId = "Square Logo Media Id";


        // Responsive Search Ad
        public const string ResponsiveSearchAd = "Responsive Search Ad";

        // Image
        public const string Image = "Image";
        public const string Height = "Height";
        public const string Width = "Width";

        // Structured Snippet Ad Extension
        public const string AccountStructuredSnippetAdExtension = "Account Structured Snippet Ad Extension";
        public const string CampaignStructuredSnippetAdExtension = "Campaign Structured Snippet Ad Extension";
        public const string AdGroupStructuredSnippetAdExtension = "Ad Group Structured Snippet Ad Extension";
        public const string StructuredSnippetAdExtension = "Structured Snippet Ad Extension";
        public const string StructuredSnippetHeader = "Structured Snippet Header";
        public const string StructuredSnippetValues = "Structured Snippet Values";

        // Ad Extension Schedule
        public const string AdSchedule = "Ad Schedule";

        // Use Searcher Time Zone
        public const string UseSearcherTimeZone = "Use Searcher Time Zone";
        public const string AdScheduleUseSearcherTimeZone = "Ad Schedule Use Searcher Time Zone";

        // Dynamic Search Ad
        public const string DynamicSearchAd = "Dynamic Search Ad";
        public const string CampaignNegativeDynamicSearchAdTarget = "Campaign Negative Dynamic Search Ad Target";
        public const string AdGroupNegativeDynamicSearchAdTarget = "Ad Group Negative Dynamic Search Ad Target";
        public const string AdGroupDynamicSearchAdTarget = "Ad Group Dynamic Search Ad Target";

        public const string DomainLanguage = "Domain Language";
        public const string DynamicAdTargetCondition1 = "Dynamic Ad Target Condition 1";
        public const string DynamicAdTargetCondition2 = "Dynamic Ad Target Condition 2";
        public const string DynamicAdTargetCondition3 = "Dynamic Ad Target Condition 3";
        public const string DynamicAdTargetValue1 = "Dynamic Ad Target Value 1";
        public const string DynamicAdTargetValue2 = "Dynamic Ad Target Value 2";
        public const string DynamicAdTargetValue3 = "Dynamic Ad Target Value 3";
        public const string PageFeedIds = "Page Feed Ids";


        // Target Criterions
        public const string AdGroupAgeCriterion = "Ad Group Age Criterion";
        public const string AdGroupCompanyNameCriterion = "Ad Group Company Name Criterion";
        public const string AdGroupDayTimeCriterion = "Ad Group DayTime Criterion";
        public const string AdGroupDeviceCriterion = "Ad Group DeviceOS Criterion";
        public const string AdGroupGenderCriterion = "Ad Group Gender Criterion";
        public const string AdGroupIndustryCriterion = "Ad Group Industry Criterion";
        public const string AdGroupJobFunctionCriterion = "Ad Group Job Function Criterion";
        public const string AdGroupLocationCriterion = "Ad Group Location Criterion";
        public const string AdGroupLocationIntentCriterion = "Ad Group Location Intent Criterion";
        public const string AdGroupNegativeAgeCriterion = "Ad Group Negative Age Criterion";
        public const string AdGroupNegativeCompanyNameCriterion = "Ad Group Negative Company Name Criterion";
        public const string AdGroupNegativeGenderCriterion = "Ad Group Negative Gender Criterion";
        public const string AdGroupNegativeIndustryCriterion = "Ad Group Negative Industry Criterion";
        public const string AdGroupNegativeJobFunctionCriterion = "Ad Group Negative Job Function Criterion";
        public const string AdGroupNegativeLocationCriterion = "Ad Group Negative Location Criterion";
        public const string AdGroupRadiusCriterion = "Ad Group Radius Criterion";
        public const string CampaignAgeCriterion = "Campaign Age Criterion";
        public const string CampaignCompanyNameCriterion = "Campaign Company Name Criterion";
        public const string CampaignDayTimeCriterion = "Campaign DayTime Criterion";
        public const string CampaignDeviceCriterion = "Campaign DeviceOS Criterion";
        public const string CampaignGenderCriterion = "Campaign Gender Criterion";
        public const string CampaignIndustryCriterion = "Campaign Industry Criterion";
        public const string CampaignJobFunctionCriterion = "Campaign Job Function Criterion";
        public const string CampaignLocationCriterion = "Campaign Location Criterion";
        public const string CampaignLocationIntentCriterion = "Campaign Location Intent Criterion";
        public const string CampaignNegativeLocationCriterion = "Campaign Negative Location Criterion";
        public const string CampaignRadiusCriterion = "Campaign Radius Criterion";

        // Labels
        public const string ColorCode = "Color";
        public const string Label = "Label";
        public const string CampaignLabel = "Campaign Label";
        public const string AdGroupLabel = "Ad Group Label";
        public const string KeywordLabel = "Keyword Label";
        public const string AppInstallAdLabel = "App Install Ad Label";
        public const string DynamicSearchAdLabel = "Dynamic Search Ad Label";
        public const string ExpandedTextAdLabel = "Expanded Text Ad Label";
        public const string ProductAdLabel = "Product Ad Label";
        public const string ResponsiveAdLabel = "Responsive Ad Label";
        public const string ResponsiveSearchAdLabel = "Responsive Search Ad Label";
        public const string TextAdLabel = "Text Ad Label";

        // Offline Conversions
        public const string OfflineConversion = "Offline Conversion";
        public const string ConversionCurrencyCode = "Conversion Currency Code";
        public const string ConversionName = "Conversion Name";
        public const string ConversionTime = "Conversion Time";
        public const string ConversionValue = "Conversion Value";
        public const string MicrosoftClickId = "Microsoft Click Id";
        public const string AdjustmentValue = "Adjustment Value";
        public const string AdjustmentTime = "Adjustment Time";
        public const string AdjustmentCurrencyCode = "Adjustment Currency Code";
        public const string AdjustmentType = "Adjustment Type";
        public const string ExternalAttributionModel = "External Attribution Model";
        public const string ExternalAttributionCredit = "External Attribution Credit";

        // Final Url Suffix
        public const string FinalUrlSuffix = "Final Url Suffix";
    }
}
