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

        public const string CampaignId = "Campaign Id";

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
        public const string AdGroupType = "Ad Group Type";
        public const string HotelAdGroupType = "Hotel Ad Group Type";
        public const string CommissionRate = "Commission Rate";
        public const string PercentCpcBid = "Percent Cpc Bid";


        public const string HotelListingGroupType = "Ad Group Hotel Listing Group";
        public const string HotelAttribute = "Hotel Attribute";
        public const string HotelAttributeValue = "Hotel Attribute Value";


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
        public const string VerifiedTrackingData = "Verified Tracking Setting";
        public const string VerifiedTrackingDatas = "Verified Tracking Settings";

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
        public const string Schedule = "Schedule";

        public const string PhysicalIntent = "Physical Intent";

        public const string FeedId = "Feed Id";

        public const string Bid = "Bid";

        public const string Profile = "Profile";
        public const string ProfileId = "Profile Id";
        public const string BidAdjustment = "Bid Adjustment";
        public const string CashbackAdjustment = "Cashback Percent";
        public const string SubType = "Sub Type";
        public const string MultiMediaAdBidAdjustment = "Multi Media Ad Bid Adjustment";
        public const string UseOptimizedTargeting = "Use Optimized Targeting";

        public const string OsNames = "OS Names";

        public const string StartDate = "Start Date";
        public const string EndDate = "End Date";
        public const string NetworkDistribution = "Network Distribution";
        public const string Language = "Language";
        public const string CpcBid = "Cpc Bid";
        public const string AdRotation = "Ad Rotation";
        public const string PrivacyStatus = "Privacy Status";
        public const string CpvBid = "Cpv Bid";
        public const string CpmBid = "Cpm Bid";
        public const string FrequencyCapSettings = "Frequency Cap Settings";

        public const string Account = "Account";
        public const string SyncTime = "Sync Time";
        public const string Name = "Name";
        public const string MSCLKIDAutoTaggingEnabled = "MSCLKID Auto Tagging Enabled";
        public const string IncludeViewThroughConversions = "Include View Through Conversions";
        public const string ProfileExpansionEnabled = "Profile Expansion Enabled";
        public const string AdClickParallelTracking = "Ad Click Parallel Tracking";
        public const string AutoApplyRecommendations = "Auto Apply Recommendations";
        public const string AllowImageAutoRetrieve = "Allow Image Auto Retrieve";
        public const string BusinessAttributes = "Business Attributes";
        public const string NetflixTCAccepted = "Netflix TC Accepted";

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

        
        //Disclaimer Ads  
        public const string DisclaimerAdsEnabled = "Disclaimer Ads Enabled";
        public const string DisclaimerName = "Disclaimer Name";
        public const string DisclaimerTitle = "Disclaimer Title";
        public const string DisclaimerLayout = "Disclaimer Layout";
        public const string DisclaimerPopupText = "Disclaimer Popup Text";
        public const string DisclaimerLineText = "Disclaimer Line Text";

        public const string DisclaimerAdExtension = "Disclaimer Ad Extension";
        public const string CampaignDisclaimerAdExtension = "Campaign Disclaimer Ad Extension";


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
        public const string MerchantCenterId = "Store Id";

        // App Ad Extension        
        public const string AppAdExtension = "App Ad Extension";
        public const string AppPlatform = "App Platform";
        public const string AppStoreId = "App Id";
        public const string AccountAppAdExtension = "Account App Ad Extension";
        public const string CampaignAppAdExtension = "Campaign App Ad Extension";
        public const string AdGroupAppAdExtension = "Ad Group App Ad Extension";
        public const string IsTrackingEnabled = "Tracking Enabled";

        // Flyer Ad Extension
        public const string FlyerAdExtension = "Flyer Ad Extension";
        public const string AccountFlyerAdExtension = "Account Flyer Ad Extension";
        public const string CampaignFlyerAdExtension = "Campaign Flyer Ad Extension";
        public const string AdGroupFlyerAdExtension = "Ad Group Flyer Ad Extension";
        public const string FlyerName = "Flyer Name";
        public const string MediaUrls = "Media Urls";


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
        public const string AccountNegativeKeywordList = "Account Negative Keyword List";
        public const string AccountNegativeKeywordListAssociation = "Account Negative Keyword List Association";
        public const string AccountSharedNegativeKeyword = "Account Shared Negative Keyword";

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
        public const string DynamicDescriptionEnabled = "Dynamic Description Enabled";

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

        // Video Ad Extension
        public const string VideoAdExtension = "Video Ad Extension";
        public const string ThumbnailUrl = "Thumbnail Url";
        public const string ThumbnailId = "Thumbnail Id";
        public const string VideoId = "Video Id";
        public const string AccountVideoAdExtension = "Account Video Ad Extension";
        public const string CampaignVideoAdExtension = "Campaign Video Ad Extension";
        public const string AdGroupVideoAdExtension = "Ad Group Video Ad Extension";

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
        public const string BidStrategy = "Bid Strategy";
        public const string BidStrategyId = "Bid Strategy Id";
        public const string BidStrategyName = "Bid Strategy Name";
        public const string BidStrategyType = "Bid Strategy Type";
        public const string BidStrategyMaxCpc = "Bid Strategy MaxCpc";
        public const string BidStrategyTargetCpa = "Bid Strategy TargetCpa";
        public const string BidStrategyTargetRoas = "Bid Strategy TargetRoas";
        public const string InheritedBidStrategyType = "Inherited Bid Strategy Type";
        public const string BidStrategyTargetAdPosition = "Bid Strategy TargetAdPosition";
        public const string BidStrategyTargetImpressionShare = "Bid Strategy TargetImpressionShare";
        public const string BidStrategyPercentMaxCpc = "Bid Strategy PercentMaxCpc";
        public const string BidStrategyCommissionRate = "Bid Strategy CommissionRate";
        public const string BidStrategyTargetCostPerSale = "Bid Strategy TargetCostPerSale";
        public const string BidStrategyMaxCpm = "Bid Strategy MaxCpm";
        public const string BidStrategyScope = "Bid Strategy Scope";

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
        public const string ImpressionBasedRemarketingList = "Impression Based Remarketing List";
        public const string AdGroupImpressionBasedRemarketingListAssociation = "Ad Group Impression Based Remarketing List Association";
        public const string AdGroupNegativeImpressionBasedRemarketingListAssociation = "Ad Group Negative Impression Based Remarketing List Association";
        public const string CampaignImpressionBasedRemarketingListAssociation = "Campaign Impression Based Remarketing List Association";
        public const string CampaignNegativeImpressionBasedRemarketingListAssociation = "Campaign Negative Impression Based Remarketing List Association";
        public const string EntityType = "Entity Type";
        public const string ImpressionCampaignId = "Impression Campaign Id";
        public const string ImpressionAdGroupId = "Impression Ad Group Id";
        public const string ImpressionCampaignIds = "Impression Campaign Ids";
        public const string ImpressionAdGroupIds = "Impression Ad Group Ids";

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
        public const string CallToActionLanguage = "Call To Action Language";
        public const string Descriptions = "Descriptions";
        public const string Headline = "Headline";
        public const string Headlines = "Headlines";
        public const string Images = "Images";
        public const string LandscapeImageMediaId = "Landscape Image Media Id";
        public const string LandscapeLogoMediaId = "Landscape Logo Media Id";
        public const string LongHeadline = "Long Headline";
        public const string LongHeadlines = "Long Headlines";
        public const string SquareImageMediaId = "Square Image Media Id";
        public const string SquareLogoMediaId = "Square Logo Media Id";
        public const string ImpressionTrackingUrls = "Impression Tracking Urls";
        public const string Videos = "Videos";
        public const string AdType = "Ad Type";

        // Responsive Search Ad
        public const string ResponsiveSearchAd = "Responsive Search Ad";

        // Image
        public const string Image = "Image";
        public const string Height = "Height";
        public const string Width = "Width";

        // Video
        public const string Video = "Video";
        public const string SourceUrl = "Source Url";
        public const string AspectRatio = "Aspect Ratio";
        public const string DurationInMillionSeconds = "Duration In Milliseconds";

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
        public const string DynamicAdTargetConditionOperator1 = "Dynamic Ad Target Condition Operator 1";
        public const string DynamicAdTargetConditionOperator2 = "Dynamic Ad Target Condition Operator 2";
        public const string DynamicAdTargetConditionOperator3 = "Dynamic Ad Target Condition Operator 3";

        public const string PageFeedIds = "Page Feed Ids";


        // Target Criterions
        public const string AdGroupAgeCriterion = "Ad Group Age Criterion";
        public const string AdGroupCompanyNameCriterion = "Ad Group Company Name Criterion";
        public const string AdGroupDayTimeCriterion = "Ad Group DayTime Criterion";
        public const string AdGroupDeviceCriterion = "Ad Group DeviceOS Criterion";
        public const string AdGroupGenderCriterion = "Ad Group Gender Criterion";
        public const string AdGroupIndustryCriterion = "Ad Group Industry Criterion";
        public const string AdGroupJobFunctionCriterion = "Ad Group Job Function Criterion";
        public const string AdGroupJobSeniorityCriterion = "Ad Group Job Seniority Criterion";
        public const string AdGroupCustomLinkedInCriterion = "Ad Group Custom LinkedIn Criterion";
        public const string AdGroupLocationCriterion = "Ad Group Location Criterion";
        public const string AdGroupLocationIntentCriterion = "Ad Group Location Intent Criterion";
        public const string AdGroupNegativeAgeCriterion = "Ad Group Negative Age Criterion";
        public const string AdGroupNegativeCompanyNameCriterion = "Ad Group Negative Company Name Criterion";
        public const string AdGroupNegativeGenderCriterion = "Ad Group Negative Gender Criterion";
        public const string AdGroupNegativeIndustryCriterion = "Ad Group Negative Industry Criterion";
        public const string AdGroupNegativeJobFunctionCriterion = "Ad Group Negative Job Function Criterion";
        public const string AdGroupNegativeLocationCriterion = "Ad Group Negative Location Criterion";
        public const string AdGroupRadiusCriterion = "Ad Group Radius Criterion";
        public const string AdGroupGenreCriterion = "Ad Group Genre Criterion";
        public const string AdGroupPlacementCriterion = "Ad Group Placement Criterion";
        public const string AdGroupTopicCriterion = "Ad Group Topic Criterion";
        public const string CampaignAgeCriterion = "Campaign Age Criterion";
        public const string CampaignCompanyNameCriterion = "Campaign Company Name Criterion";
        public const string CampaignDayTimeCriterion = "Campaign DayTime Criterion";
        public const string CampaignDeviceCriterion = "Campaign DeviceOS Criterion";
        public const string CampaignGenderCriterion = "Campaign Gender Criterion";
        public const string CampaignIndustryCriterion = "Campaign Industry Criterion";
        public const string CampaignJobFunctionCriterion = "Campaign Job Function Criterion";
        public const string CampaignJobTitleCriterion = "Campaign Job Title Criterion";
        public const string CampaignJobSeniorityCriterion = "Campaign Job Seniority Criterion";
        public const string CampaignCustomLinkedInCriterion = "Campaign Custom LinkedIn Criterion";
        public const string CampaignLocationCriterion = "Campaign Location Criterion";
        public const string CampaignLocationIntentCriterion = "Campaign Location Intent Criterion";
        public const string CampaignNegativeLocationCriterion = "Campaign Negative Location Criterion";
        public const string CampaignRadiusCriterion = "Campaign Radius Criterion";
        public const string CampaignDealCriterion = "Campaign Deal Criterion";

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
        public const string HashedEmailAddress = "Hashed Email Address";
        public const string HashedPhoneNumber = "Hashed Phone Number";

        // Online Conversion
        public const string OnlineConversionAdjustment = "Online Conversion Adjustment";
        public const string TransactionId = "Transaction Id";

        // Final Url Suffix
        public const string FinalUrlSuffix = "Final Url Suffix";

        // Campaign Conversion Goal
        public const string GoalId = "Goal Id";
        public const string CampaignConversionGoal = "Campaign Conversion Goal"; 
        public const string CampaignGoal = "Campaign Goal";

        // RSA AdCustomizer
        public const string AdCustomizerAttribute = "Adcustomizer Attribute";
        public const string CampaignAdcustomizerAttribute = "Campaign Adcustomizer Attribute";
        public const string AdGroupAdcustomizerAttribute = "Adgroup Adcustomizer Attribute";
        public const string KeywordAdcustomizerAttribute = "Keyword Adcustomizer Attribute";
        public const string AdCustomizerDataType = "AdCustomizer DataType";
        public const string AdCustomizerAttributeValue = "AdCustomizer AttributeValue";

        // Hotel Ad
        public const string AdGroupAdvanceBookingWindowCriterion = "Ad Group Advance Booking Window Criterion";
        public const string AdGroupCheckInDayCriterion = "Ad Group Check In Day Criterion";
        public const string AdGroupLengthOfStayCriterion = "Ad Group Length of Stay Criterion";
        public const string AdGroupHotelDateSelectionTypeCriterion = "Ad Group Hotel Date Selection Type Criterion";
        public const string AdGroupCheckInDateCriterion = "Ad Group Check In Date Criterion";

        public const string MinTargetValue = "Min Target Value";
        public const string MaxTargetValue = "Max Target Value";

        // PMax Campaign
        public const string CampaignNegativeWebpage = "Campaign Negative Webpage";
        public const string AssetGroupListingGroup = "Asset Group Listing Group";
        public const string AudienceGroupAssetGroupAssociation = "Audience Group Asset Group Association";

        public const string AssetGroup = "Asset Group";
        public const string AudienceGroup = "Audience Group";
        public const string Audiences = "Audiences";
        public const string AgeRanges = "Age Ranges";
        public const string GenderTypes = "Gender Types";
        public const string AudienceGroupName = "Audience Group Name";
        public const string ParentListingGroupId = "Parent Listing Group Id";
        public const string UrlExpansionOptOut= "Url Expansion Opt Out";
        public const string AutoGeneratedTextOptOut = "Auto Generated Text Assets Opt Out";
        public const string AutoGeneratedImageOptOut = "Auto Generated Image Assets Opt Out";
        public const string CostPerSaleOptOut = "Cost Per Sale Opt Out";
        public const string AssetGroupSearchTheme = "Asset Group Search Theme";
        public const string SearchTheme = "Search Theme";

        // MultiChannel Campaign
        public const string DestinationChannel = "Destination Channel";
        public const string IsMultiChannelCampaign = "Is Multi Channel Campaign";

        public const string EnabledExternalChannelSync = "Enabled External Channel Sync";

        // DNV Serving on MSAN
        public const string ShouldServeOnMSAN = "Should Serve On MSAN";

        // Seasonality Adjustment
        public const string SeasonalityAdjustment = "Seasonality Adjustment";
        public const string DataExclusion = "Data Exclusion";
        public const string DeviceType = "Device Type";
        public const string CampaignAssociations = "Campaign Associations";

        // Conversion Goal
        public const string AttributionModelType = "Attribution Model Type";
        public const string ConversionWindowInMinutes = "Conversion Window In Minutes";
        public const string CountType = "Count Type";
        public const string ExcludeFromBidding = "Exclude From Bidding";
        public const string GoalCategory = "Goal Category";
        public const string IsEnhancedConversionsEnabled = "Is Enhanced Conversions Enabled";
        public const string RevenueType = "Revenue Type";
        public const string RevenueValue = "Revenue Value";
        public const string TrackingStatus = "Tracking Status";
        public const string ViewThroughConversionWindowInMinutes = "View Through Conversion Window In Minutes";
        public const string MinimumDurationInSecond = "Minimum Duration In Second";
        public const string ActionExpression = "Action Expression";
        public const string ActionOperator = "Action Operator";
        public const string CategoryExpression = "Category Expression";
        public const string CategoryOperator = "Category Operator";
        public const string LabelExpression = "Label Expression";
        public const string LabelOperator = "Label Operator";
        public const string EventValue = "Event Value";
        public const string EventValueOperator = "Event Value Operator";
        public const string IsExternallyAttributed = "Is Externally Attributed";
        public const string MinimumPagesViewed = "Minimum Pages Viewed";
        public const string UrlExpression = "URL Expression";
        public const string UrlOperator = "URL Operator";
        public const string ConversionGoal = "Conversion Goal";
        public const string EventGoal = "Event Goal";
        public const string AppInstallGoal = "AppInstall Goal";
        public const string MultiStageGoal = "MultiStage Goal";
        public const string DurationGoal = "Duration Goal";
        public const string OfflineConversionGoal = "OfflineConversion Goal";
        public const string UrlGoal = "URL Goal";
        public const string InStoreTransactionGoal = "InStoreTransaction Goal";
        public const string PagesViewedPerVisitGoal = "PagesViewedPerVisit Goal";
        public const string SmartGoal = "Smart Goal";
        public const string InStoreVisitGoal = "InStoreVisit Goal";
        public const string ProductGoal = "Product Goal";

        // Brand List
        public const string BrandList = "Brand List";
        public const string BrandItem = "Brand Item";
        public const string CampaignBrandList = "Campaign Brand List Association";
        public const string BrandId = "Brand Id";
        public const string BrandUrl = "Brand Url";
        public const string BrandName = "Brand Name";
        public const string StatusDateTime = "Editorial Status Date";

        // Deal
        public const string IsDealCampaign = "Is Deal Campaign";

        // Asset Group Url Target
        public const string AssetGroupUrlTarget = "Asset Group Url Target";
        public const string AssetGroupTargetCondition1 = "Asset Group Target Condition 1";
        public const string AssetGroupTargetCondition2 = "Asset Group Target Condition 2";
        public const string AssetGroupTargetCondition3 = "Asset Group Target Condition 3";
        public const string AssetGroupTargetConditionOperator1 = "Asset Group Target Condition Operator 1";
        public const string AssetGroupTargetConditionOperator2 = "Asset Group Target Condition Operator 2";
        public const string AssetGroupTargetConditionOperator3 = "Asset Group Target Condition Operator 3";
        public const string AssetGroupTargetValue1 = "Asset Group Target Value 1";
        public const string AssetGroupTargetValue2 = "Asset Group Target Value 2";
        public const string AssetGroupTargetValue3 = "Asset Group Target Value 3";

        // New Customer Acquisition Goal
        public const string NewCustomerAcquisitionGoal = "New Customer Acquisition Goal";
        public const string AdditionalConversionValue = "Additional Conversion Value";
        public const string NewCustomerAcquisitionGoalId = "New Customer Acquisition Goal Id";
        public const string NewCustomerAcquisitionBidOnlyMode = "New Customer Acquisition Bid Only Mode";

        public const string AppStore = "App Store";

        public const string AccountPlacementExclusionList = "Account Placement Exclusion List";
        public const string AccountPlacementExclusionListItem = "Account Placement Exclusion List Item";
        public const string AccountPlacementListItemUrl = "Site List Item Url";
        public const string CampaignAccountPlacementListAssociation = "Campaign Account Placement Exclusion List Association";
        public const string AccountPlacementExclusionListId = "Account Placement Exclusion List Id";
        public const string AccountPlacementExclusionListItemId = "Account Placement Exclusion List Item Id";
        public const string AccountPlacementInclusionList = "Account Placement Inclusion List";
        public const string AccountPlacementInclusionListItem = "Account Placement Inclusion List Item";
        public const string CampaignAccountPlacementInclusionListAssociation = "Campaign Account Placement Inclusion List Association";
        public const string AccountPlacementInclusionListId = "Account Placement Inclusion List Id";
        public const string AccountPlacementInclusionListItemId = "Account Placement Inclusion List Item Id";

        //Brand Kit
        public const string BrandKit = "Brand Kit";
        public const string SquareLogos = "Square Logos";
        public const string LandscapeLogos = "Landscape Logos";
        public const string Palettes = "Palettes";
        public const string Fonts = "Fonts";
        public const string BrandVoice = "Brand Voice";

        public const string Topic = "Topic";
        public const string ContentPlacement = "Content Placement";
        public const string TopicParentId = "Topic Parent Id";
        public const string IsPolitical = "Is Political";
    }
}
