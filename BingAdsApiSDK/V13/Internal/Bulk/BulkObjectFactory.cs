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

using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V13.Bulk.Entities;
using BulkAccount = Microsoft.BingAds.V13.Bulk.Entities.BulkAccount;
using BulkAccountActionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountActionAdExtension;
using BulkAccountAppAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountAppAdExtension;
using BulkAccountCalloutAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountCalloutAdExtension;
using BulkAccountFilterLinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountFilterLinkAdExtension;
using BulkAccountImageAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountImageAdExtension;
using BulkAccountLocationAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountLocationAdExtension;
using BulkAccountPriceAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountPriceAdExtension;
using BulkAccountReviewAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountReviewAdExtension;
using BulkAccountSitelinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountSitelinkAdExtension;
using BulkAccountStructuredSnippetAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountStructuredSnippetAdExtension;
using BulkAccountPromotionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAccountPromotionAdExtension;
using BulkAdGroup = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroup;
using BulkAdGroupActionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupActionAdExtension;
using BulkAdGroupAppAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupAppAdExtension;
using BulkAdGroupCalloutAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupCalloutAdExtension;
using BulkAdGroupCustomAudienceAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupCustomAudienceAssociation;
using BulkAdGroupDynamicSearchAdTarget = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupDynamicSearchAdTarget;
using BulkAdGroupImageAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupImageAdExtension;
using BulkAdGroupInMarketAudienceAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupInMarketAudienceAssociation;
using BulkAdGroupNegativeCustomAudienceAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeCustomAudienceAssociation;
using BulkAdGroupNegativeDynamicSearchAdTarget = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeDynamicSearchAdTarget;
using BulkAdGroupNegativeInMarketAudienceAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeInMarketAudienceAssociation;
using BulkAdGroupNegativeKeyword = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeKeyword;
using BulkAdGroupNegativeRemarketingListAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeRemarketingListAssociation;
using BulkAdGroupNegativeSite = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeSite;
using BulkAdGroupNegativeSitesIdentifier = Microsoft.BingAds.V13.Internal.Bulk.Entities.BulkAdGroupNegativeSitesIdentifier;
using BulkAdGroupProductPartition = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupProductPartition;
using BulkRemarketingList = Microsoft.BingAds.V13.Bulk.Entities.BulkRemarketingList;
using BulkAdGroupRemarketingListAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupRemarketingListAssociation;
using BulkCombinedList = Microsoft.BingAds.V13.Bulk.Entities.BulkCombinedList;
using BulkAdGroupNegativeCombinedListAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupNegativeCombinedListAssociation;
using BulkAdGroupCombinedListAssociation = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupCombinedListAssociation;
using BulkAdGroupFilterLinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupFilterLinkAdExtension;
using BulkAdGroupPriceAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupPriceAdExtension;
using BulkAdGroupReviewAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupReviewAdExtension;
using BulkAdGroupSitelinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupSitelinkAdExtension;
using BulkAdGroupStructuredSnippetAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupStructuredSnippetAdExtension;
using BulkAdGroupPromotionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupPromotionAdExtension;
using BulkAdGroupVideoAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupVideoAdExtension;
using BulkAppAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkAppAdExtension;
using BulkAppInstallAd = Microsoft.BingAds.V13.Bulk.Entities.BulkAppInstallAd;
using BulkBudget = Microsoft.BingAds.V13.Bulk.Entities.BulkBudget;
using BulkBidStrategy = Microsoft.BingAds.V13.Bulk.Entities.BulkBidStrategy;
using BulkImage = Microsoft.BingAds.V13.Bulk.Entities.BulkImage;
using BulkFeed = Microsoft.BingAds.V13.Bulk.Entities.Feeds.BulkFeed;
using BulkFeedItem = Microsoft.BingAds.V13.Bulk.Entities.Feeds.BulkFeedItem;
using BulkExperiment = Microsoft.BingAds.V13.Bulk.Entities.BulkExperiment;
using BulkActionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkActionAdExtension;
using BulkCallAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCallAdExtension;
using BulkCalloutAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCalloutAdExtension;
using BulkCampaign = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaign;
using BulkCampaignActionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignActionAdExtension;
using BulkCampaignAppAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignAppAdExtension;
using BulkCampaignCallAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignCallAdExtension;
using BulkCampaignCalloutAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignCalloutAdExtension;
using BulkCampaignImageAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignImageAdExtension;
using BulkCampaignLocationAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignLocationAdExtension;
using BulkCampaignNegativeDynamicSearchAdTarget = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignNegativeDynamicSearchAdTarget;
using BulkCampaignNegativeKeyword = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignNegativeKeyword;
using BulkCampaignNegativeKeywordList = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignNegativeKeywordList;
using BulkCampaignNegativeSite = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignNegativeSite;
using BulkCampaignNegativeSitesIdentifier = Microsoft.BingAds.V13.Internal.Bulk.Entities.BulkCampaignNegativeSitesIdentifier;
using BulkCampaignFilterLinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignFilterLinkAdExtension;
using BulkCampaignPriceAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignPriceAdExtension;
using BulkCampaignProductScope = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignProductScope;
using BulkCampaignReviewAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignReviewAdExtension;
using BulkCampaignSitelinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignSitelinkAdExtension;
using BulkCampaignStructuredSnippetAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignStructuredSnippetAdExtension;
using BulkCampaignPromotionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignPromotionAdExtension;
using BulkCampaignVideoAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignVideoAdExtension;
using BulkCampaignNegativeStoreCriterion = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignNegativeStoreCriterion;
using BulkCustomAudience = Microsoft.BingAds.V13.Bulk.Entities.BulkCustomAudience;
using BulkDynamicSearchAd = Microsoft.BingAds.V13.Bulk.Entities.BulkDynamicSearchAd;
using BulkError = Microsoft.BingAds.V13.Bulk.Entities.BulkError;
using BulkExpandedTextAd = Microsoft.BingAds.V13.Bulk.Entities.BulkExpandedTextAd;
using BulkImageAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkImageAdExtension;
using BulkInMarketAudience = Microsoft.BingAds.V13.Bulk.Entities.BulkInMarketAudience;
using BulkKeyword = Microsoft.BingAds.V13.Bulk.Entities.BulkKeyword;
using BulkKeywordBestPositionBid = Microsoft.BingAds.V13.Internal.Bulk.Entities.BulkKeywordBestPositionBid;
using BulkKeywordFirstPageBid = Microsoft.BingAds.V13.Internal.Bulk.Entities.BulkKeywordFirstPageBid;
using BulkKeywordMainLineBid = Microsoft.BingAds.V13.Bulk.Entities.BulkKeywordMainLineBid;
using BulkLocationAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkLocationAdExtension;
using BulkNegativeKeywordList = Microsoft.BingAds.V13.Bulk.Entities.BulkNegativeKeywordList;
using BulkPriceAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkPriceAdExtension;
using BulkProductAd = Microsoft.BingAds.V13.Bulk.Entities.BulkProductAd;
using BulkFilterLinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkFilterLinkAdExtension;
using BulkReviewAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkReviewAdExtension;
using BulkSharedNegativeKeyword = Microsoft.BingAds.V13.Bulk.Entities.BulkSharedNegativeKeyword;
using BulkSitelinkAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkSitelinkAdExtension;
using BulkStructuredSnippetAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkStructuredSnippetAdExtension;
using BulkPromotionAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkPromotionAdExtension;
using BulkVideoAdExtension = Microsoft.BingAds.V13.Bulk.Entities.BulkVideoAdExtension;
using BulkTextAd = Microsoft.BingAds.V13.Bulk.Entities.BulkTextAd;
using UnknownBulkEntity = Microsoft.BingAds.V13.Bulk.Entities.UnknownBulkEntity;
using BulkLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkLabel;
using BulkCampaignLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkCampaignLabel;
using BulkAdGroupLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkAdGroupLabel;
using BulkKeywordLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkKeywordLabel;
using BulkAppInstallAdLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkAppInstallAdLabel;
using BulkDynamicSearchAdLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkDynamicSearchAdLabel;
using BulkExpandedTextAdLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkExpandedTextAdLabel;
using BulkProductAdLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkProductAdLabel;
using BulkTextAdLabel = Microsoft.BingAds.V13.Bulk.Entities.BulkTextAdLabel;
using BulkOfflineConversion = Microsoft.BingAds.V13.Bulk.Entities.BulkOfflineConversion;
using BulkVideo = Microsoft.BingAds.V13.Bulk.Entities.BulkVideo;
using Microsoft.BingAds.V13.Bulk.Entities.AdCustomizerAttrributes;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.Internal.Bulk
{
    internal class BulkObjectFactory : IBulkObjectFactory
    {
        private static readonly Dictionary<string, EntityInfo> IndividualEntityMap;

        private static readonly Dictionary<string, Func<BulkObject>> AdditionalObjectMap;

        private static readonly Dictionary<Type, string> TypeReverseMap;

        static BulkObjectFactory()
        {
            IndividualEntityMap = new Dictionary<string, EntityInfo>
            {
                {StringTable.Account,                       new EntityInfo(() => new BulkAccount())},
                {StringTable.Campaign,                      new EntityInfo(() => new BulkCampaign())},
                {StringTable.AdGroup,                       new EntityInfo(() => new BulkAdGroup())},
                {StringTable.TextAd,                        new EntityInfo(() => new BulkTextAd())},
                {StringTable.ProductAd,                     new EntityInfo(() => new BulkProductAd())},
                {StringTable.AppInstallAd,                  new EntityInfo(() => new BulkAppInstallAd())},
                {StringTable.ExpandedTextAd,                new EntityInfo(() => new BulkExpandedTextAd())},
                {StringTable.ResponsiveAd,                new EntityInfo(() => new BulkResponsiveAd())},
                {StringTable.DisclaimerAdExtension,                new EntityInfo(() => new BulkDisclaimerAdExtension())},
                {StringTable.ResponsiveSearchAd,                new EntityInfo(() => new BulkResponsiveSearchAd())},
                {StringTable.Keyword,                       new EntityInfo(() => new BulkKeyword())},
                {StringTable.ImageAdExtension,              new EntityInfo(() => new BulkImageAdExtension())},
                {StringTable.AccountImageAdExtension,      new EntityInfo(() => new BulkAccountImageAdExtension())},
                {StringTable.CampaignImageAdExtension,      new EntityInfo(() => new BulkCampaignImageAdExtension())},
                {StringTable.AdGroupImageAdExtension,       new EntityInfo(() => new BulkAdGroupImageAdExtension())},
                {StringTable.VideoAdExtension,              new EntityInfo(() => new BulkVideoAdExtension())},
                {StringTable.CampaignVideoAdExtension,      new EntityInfo(() => new BulkCampaignVideoAdExtension())},
                {StringTable.CampaignDisclaimerAdExtension,      new EntityInfo(() => new BulkCampaignDisclaimerAdExtension())},
                {StringTable.AccountVideoAdExtension,      new EntityInfo(() => new BulkAccountVideoAdExtension())},
                {StringTable.AdGroupVideoAdExtension,       new EntityInfo(() => new BulkAdGroupVideoAdExtension())},
                {StringTable.LocationAdExtension,           new EntityInfo(() => new BulkLocationAdExtension())},
                {StringTable.AccountLocationAdExtension,   new EntityInfo(() => new BulkAccountLocationAdExtension())},
                {StringTable.CampaignLocationAdExtension,   new EntityInfo(() => new BulkCampaignLocationAdExtension())},
                {StringTable.CallAdExtension,               new EntityInfo(() => new BulkCallAdExtension())},
                {StringTable.CampaignCallAdExtension,       new EntityInfo(() => new BulkCampaignCallAdExtension())},
                {StringTable.AppAdExtension,                new EntityInfo(() => new BulkAppAdExtension())},
                {StringTable.AccountAppAdExtension,        new EntityInfo(() => new BulkAccountAppAdExtension())},
                {StringTable.CampaignAppAdExtension,        new EntityInfo(() => new BulkCampaignAppAdExtension())},
                {StringTable.AdGroupAppAdExtension,         new EntityInfo(() => new BulkAdGroupAppAdExtension())},
                {StringTable.ReviewAdExtension,             new EntityInfo(() => new BulkReviewAdExtension()) },
                {StringTable.AccountReviewAdExtension,     new EntityInfo(() => new BulkAccountReviewAdExtension())},
                {StringTable.CampaignReviewAdExtension,     new EntityInfo(() => new BulkCampaignReviewAdExtension())},
                {StringTable.AdGroupReviewAdExtension,      new EntityInfo(() => new BulkAdGroupReviewAdExtension())},
                {StringTable.CalloutAdExtension,            new EntityInfo(() => new BulkCalloutAdExtension()) },
                {StringTable.AccountCalloutAdExtension,    new EntityInfo(() => new BulkAccountCalloutAdExtension())},
                {StringTable.CampaignCalloutAdExtension,    new EntityInfo(() => new BulkCampaignCalloutAdExtension())},
                {StringTable.AdGroupCalloutAdExtension,     new EntityInfo(() => new BulkAdGroupCalloutAdExtension())},
                {StringTable.ActionAdExtension,            new EntityInfo(() => new BulkActionAdExtension()) },
                {StringTable.AccountActionAdExtension,    new EntityInfo(() => new BulkAccountActionAdExtension())},
                {StringTable.CampaignActionAdExtension,    new EntityInfo(() => new BulkCampaignActionAdExtension())},
                {StringTable.AdGroupActionAdExtension,     new EntityInfo(() => new BulkAdGroupActionAdExtension())},
                {StringTable.FilterLinkAdExtension,  new EntityInfo(() => new BulkFilterLinkAdExtension()) },
                {StringTable.AccountFilterLinkAdExtension,    new EntityInfo(() => new BulkAccountFilterLinkAdExtension())},
                {StringTable.CampaignFilterLinkAdExtension,    new EntityInfo(() => new BulkCampaignFilterLinkAdExtension())},
                {StringTable.AdGroupFilterLinkAdExtension,     new EntityInfo(() => new BulkAdGroupFilterLinkAdExtension())},
                {StringTable.StructuredSnippetAdExtension,  new EntityInfo(() => new BulkStructuredSnippetAdExtension()) },
                {StringTable.AccountStructuredSnippetAdExtension,    new EntityInfo(() => new BulkAccountStructuredSnippetAdExtension())},
                {StringTable.CampaignStructuredSnippetAdExtension,    new EntityInfo(() => new BulkCampaignStructuredSnippetAdExtension())},
                {StringTable.AdGroupStructuredSnippetAdExtension,     new EntityInfo(() => new BulkAdGroupStructuredSnippetAdExtension())},
                {StringTable.PromotionAdExtension,  new EntityInfo(() => new BulkPromotionAdExtension()) },
                {StringTable.AccountPromotionAdExtension,    new EntityInfo(() => new BulkAccountPromotionAdExtension())},
                {StringTable.CampaignPromotionAdExtension,    new EntityInfo(() => new BulkCampaignPromotionAdExtension())},
                {StringTable.AdGroupPromotionAdExtension,     new EntityInfo(() => new BulkAdGroupPromotionAdExtension())},
                {StringTable.PriceAdExtension,             new EntityInfo(() => new BulkPriceAdExtension()) },
                {StringTable.AccountPriceAdExtension,     new EntityInfo(() => new BulkAccountPriceAdExtension())},
                {StringTable.CampaignPriceAdExtension,     new EntityInfo(() => new BulkCampaignPriceAdExtension())},
                {StringTable.AdGroupPriceAdExtension,      new EntityInfo(() => new BulkAdGroupPriceAdExtension())},
                {StringTable.FlyerAdExtension,  new EntityInfo(() => new BulkFlyerAdExtension()) },
                {StringTable.AccountFlyerAdExtension,    new EntityInfo(() => new BulkAccountFlyerAdExtension())},
                {StringTable.CampaignFlyerAdExtension,    new EntityInfo(() => new BulkCampaignFlyerAdExtension())},
                {StringTable.AdGroupFlyerAdExtension,     new EntityInfo(() => new BulkAdGroupFlyerAdExtension())},
                {"Campaign Negative Site",                  new EntityInfo(() => new BulkCampaignNegativeSite(),                StringTable.Website,                () => new BulkCampaignNegativeSitesIdentifier())},
                {"Ad Group Negative Site",                  new EntityInfo(() => new BulkAdGroupNegativeSite(),                 StringTable.Website,                () => new BulkAdGroupNegativeSitesIdentifier())},

                {StringTable.NegativeKeywordList,           new EntityInfo(() => new BulkNegativeKeywordList())},
                {StringTable.ListNegativeKeyword,           new EntityInfo(() => new BulkSharedNegativeKeyword())},
                {StringTable.CampaignNegativeKeywordList,   new EntityInfo(() => new BulkCampaignNegativeKeywordList())},
                {StringTable.CampaignNegativeKeyword,       new EntityInfo(() => new BulkCampaignNegativeKeyword())},
                {StringTable.AdGroupNegativeKeyword,        new EntityInfo(() => new BulkAdGroupNegativeKeyword())},

                {"Campaign Product Scope",                  new EntityInfo(() => new BulkCampaignProductScope())},
                {"Ad Group Product Partition",              new EntityInfo(() => new BulkAdGroupProductPartition())},
                {StringTable.RemarketingList,               new EntityInfo(() => new BulkRemarketingList())},
                {StringTable.AdGroupRemarketingListAssociation, new EntityInfo(() => new BulkAdGroupRemarketingListAssociation())},
                {StringTable.AdGroupNegativeRemarketingListAssociation, new EntityInfo(() => new BulkAdGroupNegativeRemarketingListAssociation())},
                {StringTable.CampaignRemarketingListAssociation, new EntityInfo(() => new BulkCampaignRemarketingListAssociation())},
                {StringTable.CampaignNegativeRemarketingListAssociation, new EntityInfo(() => new BulkCampaignNegativeRemarketingListAssociation())},
                {StringTable.SimilarRemarketingList,  new EntityInfo(() => new BulkSimilarRemarketingList())},
                {StringTable.AdGroupSimilarRemarketingListAssociation,  new EntityInfo(() => new BulkAdGroupSimilarRemarketingListAssociation())},
                {StringTable.AdGroupNegativeSimilarRemarketingListAssociation,  new EntityInfo(() => new BulkAdGroupNegativeSimilarRemarketingListAssociation())},
                {StringTable.CampaignSimilarRemarketingListAssociation,  new EntityInfo(() => new BulkCampaignSimilarRemarketingListAssociation())},
                {StringTable.CampaignNegativeSimilarRemarketingListAssociation,  new EntityInfo(() => new BulkCampaignNegativeSimilarRemarketingListAssociation())},
                {StringTable.CustomAudience,               new EntityInfo(() => new BulkCustomAudience())},
                {StringTable.AdGroupCustomAudienceAssociation, new EntityInfo(() => new BulkAdGroupCustomAudienceAssociation())},
                {StringTable.AdGroupNegativeCustomAudienceAssociation, new EntityInfo(() => new BulkAdGroupNegativeCustomAudienceAssociation())},
                {StringTable.CampaignCustomAudienceAssociation, new EntityInfo(() => new BulkCampaignCustomAudienceAssociation())},
                {StringTable.CampaignNegativeCustomAudienceAssociation, new EntityInfo(() => new BulkCampaignNegativeCustomAudienceAssociation())},
                {StringTable.InMarketAudience,               new EntityInfo(() => new BulkInMarketAudience())},
                {StringTable.AdGroupInMarketAudienceAssociation, new EntityInfo(() => new BulkAdGroupInMarketAudienceAssociation())},
                {StringTable.AdGroupNegativeInMarketAudienceAssociation, new EntityInfo(() => new BulkAdGroupNegativeInMarketAudienceAssociation())},
                {StringTable.CampaignInMarketAudienceAssociation, new EntityInfo(() => new BulkCampaignInMarketAudienceAssociation())},
                {StringTable.CampaignNegativeInMarketAudienceAssociation, new EntityInfo(() => new BulkCampaignNegativeInMarketAudienceAssociation())},
                {StringTable.ProductAudience,               new EntityInfo(() => new BulkProductAudience())},
                {StringTable.AdGroupProductAudienceAssociation, new EntityInfo(() => new BulkAdGroupProductAudienceAssociation())},
                {StringTable.AdGroupNegativeProductAudienceAssociation, new EntityInfo(() => new BulkAdGroupNegativeProductAudienceAssociation())},
                {StringTable.CampaignProductAudienceAssociation, new EntityInfo(() => new BulkCampaignProductAudienceAssociation())},
                {StringTable.CampaignNegativeProductAudienceAssociation, new EntityInfo(() => new BulkCampaignNegativeProductAudienceAssociation())},
                {StringTable.CombinedList,               new EntityInfo(() => new BulkCombinedList())},
                {StringTable.AdGroupCombinedListAssociation, new EntityInfo(() => new BulkAdGroupCombinedListAssociation())},
                {StringTable.AdGroupNegativeCombinedListAssociation, new EntityInfo(() => new BulkAdGroupNegativeCombinedListAssociation())},
                {StringTable.CampaignCombinedListAssociation, new EntityInfo(() => new BulkCampaignCombinedListAssociation())},
                {StringTable.CampaignNegativeCombinedListAssociation, new EntityInfo(() => new BulkCampaignNegativeCombinedListAssociation())},
                {StringTable.CustomerList,               new EntityInfo(() => new BulkCustomerList())},
                {StringTable.CustomerListItem,               new EntityInfo(() => new BulkCustomerListItem())},
                {StringTable.AdGroupCustomerListAssociation, new EntityInfo(() => new BulkAdGroupCustomerListAssociation())},
                {StringTable.AdGroupNegativeCustomerListAssociation, new EntityInfo(() => new BulkAdGroupNegativeCustomerListAssociation())},
                {StringTable.CampaignCustomerListAssociation, new EntityInfo(() => new BulkCampaignCustomerListAssociation())},
                {StringTable.CampaignNegativeCustomerListAssociation, new EntityInfo(() => new BulkCampaignNegativeCustomerListAssociation())},
                {StringTable.ImpressionBasedRemarketingList, new EntityInfo(() => new BulkImpressionBasedRemarketingList())},
                {StringTable.AdGroupImpressionBasedRemarketingListAssociation, new EntityInfo(() => new BulkAdGroupImpressionBasedRemarketingListAssociation())},
                {StringTable.AdGroupNegativeImpressionBasedRemarketingListAssociation, new EntityInfo(() => new BulkAdGroupNegativeImpressionBasedRemarketingListAssociation())},
                {StringTable.CampaignImpressionBasedRemarketingListAssociation, new EntityInfo(() => new BulkCampaignImpressionBasedRemarketingListAssociation())},
                {StringTable.CampaignNegativeImpressionBasedRemarketingListAssociation, new EntityInfo(() => new BulkCampaignNegativeImpressionBasedRemarketingListAssociation())},


                {StringTable.SitelinkAdExtension,          new EntityInfo(() => new BulkSitelinkAdExtension())},
                {StringTable.AccountSitelinkAdExtension,  new EntityInfo(() => new BulkAccountSitelinkAdExtension())},
                {StringTable.CampaignSitelinkAdExtension,  new EntityInfo(() => new BulkCampaignSitelinkAdExtension())},
                {StringTable.AdGroupSitelinkAdExtension,   new EntityInfo(() => new BulkAdGroupSitelinkAdExtension())},
                {StringTable.Budget,                        new EntityInfo(() => new BulkBudget())},
                {StringTable.BidStrategy,                        new EntityInfo(() => new BulkBidStrategy())},                
                {StringTable.Image,                        new EntityInfo(() => new BulkImage())},
                {StringTable.Video,                        new EntityInfo(() => new BulkVideo())},
                {StringTable.Feed,                        new EntityInfo(() => new BulkFeed())},
                {StringTable.FeedItem,                        new EntityInfo(() => new BulkFeedItem())},
                {StringTable.Experiment,                    new EntityInfo(() => new BulkExperiment())},

                {StringTable.DynamicSearchAd,               new EntityInfo(() => new BulkDynamicSearchAd())},
                {StringTable.AdGroupDynamicSearchAdTarget,  new EntityInfo(() => new BulkAdGroupDynamicSearchAdTarget())},
                {StringTable.AdGroupNegativeDynamicSearchAdTarget, new EntityInfo(() => new BulkAdGroupNegativeDynamicSearchAdTarget())},
                {StringTable.CampaignNegativeDynamicSearchAdTarget, new EntityInfo(() => new BulkCampaignNegativeDynamicSearchAdTarget())},


                {StringTable.CampaignNegativeStoreCriterion, new EntityInfo(() => new BulkCampaignNegativeStoreCriterion())},

                {StringTable.AdGroupAgeCriterion,  new EntityInfo(() => new BulkAdGroupAgeCriterion())},
                {StringTable.AdGroupCompanyNameCriterion,  new EntityInfo(() => new BulkAdGroupCompanyNameCriterion())},
                {StringTable.AdGroupDayTimeCriterion,  new EntityInfo(() => new BulkAdGroupDayTimeCriterion())},
                {StringTable.AdGroupDeviceCriterion,  new EntityInfo(() => new BulkAdGroupDeviceCriterion())},
                {StringTable.AdGroupGenderCriterion,  new EntityInfo(() => new BulkAdGroupGenderCriterion())},
                {StringTable.AdGroupIndustryCriterion,  new EntityInfo(() => new BulkAdGroupIndustryCriterion())},
                {StringTable.AdGroupJobFunctionCriterion,  new EntityInfo(() => new BulkAdGroupJobFunctionCriterion())},
                {StringTable.AdGroupLocationCriterion,  new EntityInfo(() => new BulkAdGroupLocationCriterion())},
                {StringTable.AdGroupLocationIntentCriterion,  new EntityInfo(() => new BulkAdGroupLocationIntentCriterion())},
                {StringTable.AdGroupNegativeAgeCriterion,  new EntityInfo(() => new BulkAdGroupNegativeAgeCriterion())},
                {StringTable.AdGroupNegativeCompanyNameCriterion,  new EntityInfo(() => new BulkAdGroupNegativeCompanyNameCriterion())},
                {StringTable.AdGroupNegativeGenderCriterion,  new EntityInfo(() => new BulkAdGroupNegativeGenderCriterion())},
                {StringTable.AdGroupNegativeIndustryCriterion,  new EntityInfo(() => new BulkAdGroupNegativeIndustryCriterion())},
                {StringTable.AdGroupNegativeJobFunctionCriterion,  new EntityInfo(() => new BulkAdGroupNegativeJobFunctionCriterion())},
                {StringTable.AdGroupNegativeLocationCriterion,  new EntityInfo(() => new BulkAdGroupNegativeLocationCriterion())},
                {StringTable.AdGroupRadiusCriterion,  new EntityInfo(() => new BulkAdGroupRadiusCriterion())},
                {StringTable.AdGroupGenreCriterion,  new EntityInfo(() => new BulkAdGroupGenreCriterion())},
                {StringTable.AdGroupPlacementCriterion,  new EntityInfo(() => new BulkAdGroupPlacementCriterion())},
                {StringTable.AdGroupTopicCriterion,  new EntityInfo(() => new BulkAdGroupTopicCriterion())},
                {StringTable.CampaignAgeCriterion,  new EntityInfo(() => new BulkCampaignAgeCriterion())},
                {StringTable.CampaignCompanyNameCriterion,  new EntityInfo(() => new BulkCampaignCompanyNameCriterion())},
                {StringTable.CampaignDayTimeCriterion,  new EntityInfo(() => new BulkCampaignDayTimeCriterion())},
                {StringTable.CampaignDeviceCriterion,  new EntityInfo(() => new BulkCampaignDeviceCriterion())},
                {StringTable.CampaignGenderCriterion,  new EntityInfo(() => new BulkCampaignGenderCriterion())},
                {StringTable.CampaignIndustryCriterion,  new EntityInfo(() => new BulkCampaignIndustryCriterion())},
                {StringTable.CampaignJobFunctionCriterion,  new EntityInfo(() => new BulkCampaignJobFunctionCriterion())},
                {StringTable.CampaignJobTitleCriterion,  new EntityInfo(() => new BulkCampaignJobTitleCriterion())},
                {StringTable.CampaignJobSeniorityCriterion,  new EntityInfo(() => new BulkCampaignJobSeniorityCriterion())},
                {StringTable.CampaignLocationCriterion,  new EntityInfo(() => new BulkCampaignLocationCriterion())},
                {StringTable.CampaignLocationIntentCriterion,  new EntityInfo(() => new BulkCampaignLocationIntentCriterion())},
                {StringTable.CampaignNegativeLocationCriterion,  new EntityInfo(() => new BulkCampaignNegativeLocationCriterion())},
                {StringTable.CampaignRadiusCriterion,  new EntityInfo(() => new BulkCampaignRadiusCriterion())},
                {StringTable.CampaignDealCriterion,  new EntityInfo(() => new BulkCampaignDealCriterion())},

                {StringTable.Label,  new EntityInfo(() => new BulkLabel())},
                {StringTable.CampaignLabel,  new EntityInfo(() => new BulkCampaignLabel())},
                {StringTable.AdGroupLabel,  new EntityInfo(() => new BulkAdGroupLabel())},
                {StringTable.KeywordLabel,  new EntityInfo(() => new BulkKeywordLabel())},
                {StringTable.AppInstallAdLabel,  new EntityInfo(() => new BulkAppInstallAdLabel())},
                {StringTable.DynamicSearchAdLabel,  new EntityInfo(() => new BulkDynamicSearchAdLabel())},
                {StringTable.ExpandedTextAdLabel,  new EntityInfo(() => new BulkExpandedTextAdLabel())},
                {StringTable.ProductAdLabel,  new EntityInfo(() => new BulkProductAdLabel())},
                {StringTable.ResponsiveAdLabel,  new EntityInfo(() => new BulkResponsiveAdLabel())},
                {StringTable.ResponsiveSearchAdLabel,  new EntityInfo(() => new BulkResponsiveSearchAdLabel())},
                {StringTable.TextAdLabel,  new EntityInfo(() => new BulkTextAdLabel())},

                {StringTable.OfflineConversion,  new EntityInfo(() => new BulkOfflineConversion())},

                {StringTable.AdCustomizerAttribute, new EntityInfo(() => new BulkAdCustomizerAttribute())},
                {StringTable.CampaignAdcustomizerAttribute, new EntityInfo(() => new BulkCampaignAdCustomizerAttribute())},
                {StringTable.AdGroupAdcustomizerAttribute, new EntityInfo(() => new BulkAdGroupAdCustomizerAttribute())},
                {StringTable.KeywordAdcustomizerAttribute, new EntityInfo(() => new BulkKeywordAdCustomizerAttribute())},
                {StringTable.CampaignConversionGoal, new EntityInfo(() => new BulkCampaignConversionGoal())},
                {StringTable.OnlineConversionAdjustment, new EntityInfo(() => new BulkOnlineConversionAdjustment())},


                {StringTable.AdGroupAdvanceBookingWindowCriterion, new EntityInfo(() => new BulkAdGroupHotelAdvanceBookingWindowCriterion())},
                {StringTable.AdGroupHotelDateSelectionTypeCriterion, new EntityInfo(() => new BulkAdGroupHotelDateSelectionTypeCriterion())},
                {StringTable.AdGroupLengthOfStayCriterion, new EntityInfo(() => new BulkAdGroupHotelLengthOfStayCriterion())},
                {StringTable.AdGroupCheckInDayCriterion, new EntityInfo(() => new BulkAdGroupHotelCheckInDayCriterion())},
                {StringTable.AdGroupCheckInDateCriterion, new EntityInfo(() => new BulkAdGroupHotelCheckInDateCriterion())},
                {StringTable.HotelListingGroupType, new EntityInfo(() => new BulkAdGroupHotelListingGroup())},

                {StringTable.AssetGroup, new EntityInfo(() => new BulkAssetGroup())},
                {StringTable.AudienceGroup, new EntityInfo(() => new BulkAudienceGroup())},
                {StringTable.CampaignNegativeWebpage, new EntityInfo(() => new BulkCampaignNegativeWebPage())},
                {StringTable.AssetGroupListingGroup, new EntityInfo(() => new BulkAssetGroupListingGroup())},
                {StringTable.AudienceGroupAssetGroupAssociation, new EntityInfo(() => new BulkAudienceGroupAssetGroupAssociation())},
                {StringTable.SeasonalityAdjustment, new EntityInfo(() => new BulkSeasonalityAdjustment())},
                {StringTable.DataExclusion, new EntityInfo(() => new BulkDataExclusion())},
                {StringTable.AccountNegativeKeywordList, new EntityInfo(() => new BulkAccountNegativeKeywordList())},
                {StringTable.AccountNegativeKeywordListAssociation, new EntityInfo(() => new BulkAccountNegativeKeywordListAssociation())},
                {StringTable.AccountSharedNegativeKeyword, new EntityInfo(() => new BulkAccountSharedNegativeKeyword())},
                {StringTable.EventGoal, new EntityInfo(() => new BulkEventGoal())},
                {StringTable.UrlGoal, new EntityInfo(() => new BulkUrlGoal())},
                {StringTable.OfflineConversionGoal, new EntityInfo(() => new BulkOfflineConversionGoal())},
                {StringTable.AppInstallGoal, new EntityInfo(() => new BulkAppInstallGoal())},
                {StringTable.PagesViewedPerVisitGoal, new EntityInfo(() => new BulkPagesViewedPerVisitGoal())},
                {StringTable.DurationGoal, new EntityInfo(() => new BulkDurationGoal())},
                {StringTable.InStoreTransactionGoal, new EntityInfo(() => new BulkInStoreTransactionGoal())},
                {StringTable.ProductGoal, new EntityInfo(() => new BulkProductGoal())},
                {StringTable.InStoreVisitGoal, new EntityInfo(() => new BulkInStoreVisitGoal())},
                {StringTable.AssetGroupSearchTheme, new EntityInfo(() => new BulkAssetGroupSearchTheme())},
                {StringTable.BrandList, new EntityInfo(() => new BulkBrandList())},
                {StringTable.BrandItem, new EntityInfo(() => new BulkBrandItem())},
                {StringTable.CampaignBrandList, new EntityInfo(() => new BulkCampaignBrandListAssociation())},
                {StringTable.AssetGroupUrlTarget, new EntityInfo(() => new BulkAssetGroupUrlTarget())},
                {StringTable.NewCustomerAcquisitionGoal, new EntityInfo(() => new BulkNewCustomerAcquisitionGoal())},

                {StringTable.AccountPlacementExclusionList, new EntityInfo(() => new BulkAccountPlacementExclusionList())},
                {StringTable.AccountPlacementExclusionListItem, new EntityInfo(() => new BulkSharedListNegativeSite())},
                {StringTable.CampaignAccountPlacementListAssociation, new EntityInfo(() => new BulkAccountPlacementExclusionListAssociation())},
                {StringTable.AccountPlacementInclusionList, new EntityInfo(() => new BulkAccountPlacementInclusionList())},
                {StringTable.AccountPlacementInclusionListItem, new EntityInfo(() => new BulkSharedListSite())},
                {StringTable.CampaignAccountPlacementInclusionListAssociation, new EntityInfo(() => new BulkAccountPlacementInclusionListAssociation())},
                {StringTable.Topic, new EntityInfo(() => new BulkTopic())},
                {StringTable.ContentPlacement, new EntityInfo(() => new BulkContentPlacement())},
            };

            AdditionalObjectMap = new Dictionary<string, Func<BulkObject>>
            {
                {"Keyword Best Position Bid",           () => new BulkKeywordBestPositionBid()},
                {"Keyword Main Line Bid",               () => new BulkKeywordMainLineBid()},
                {"Keyword First Page Bid",              () => new BulkKeywordFirstPageBid()},

                {"Format Version",                      () => new FormatVersion()}
            };

            TypeReverseMap = new Dictionary<Type, string>();

            foreach (var pair in IndividualEntityMap)
            {
                TypeReverseMap[pair.Value.CreateFunc().GetType()] = pair.Key;

                if (pair.Value.CreateIdentifierFunc != null)
                {
                    var identifier = pair.Value.CreateIdentifierFunc();

                     TypeReverseMap[identifier.GetType()] = pair.Key;
                }
            }
        }

        public string GetBulkRowType(BulkObject bulkObject)
        {
            //BulkLinkedInAd use the same Type as BulkResponsiveAd
            if (bulkObject is BulkLinkedInAd)
            {
                return StringTable.ResponsiveAd;
            }

            return TypeReverseMap[bulkObject.GetType()];
        }

        public BulkObject CreateBulkObject(RowValues values)
        {
            string type;

            if (!values.TryGetValue(StringTable.Type, out type))
            {
                throw new InvalidOperationException(ErrorMessages.TypeColumnNotFound);
            }

            if (type.EndsWith("Error"))
            {
                return new BulkError();
            }

            Func<BulkObject> additionalObjectFunc;

            if (AdditionalObjectMap.TryGetValue(type, out additionalObjectFunc))
            {
                return additionalObjectFunc();
            }

            EntityInfo info;

            if (!IndividualEntityMap.TryGetValue(type, out info))
            {
                return new UnknownBulkEntity();
            }

            if (values[StringTable.Status] == "Deleted" &&
                !string.IsNullOrEmpty(info.DeleteAllColumnName) &&
                string.IsNullOrEmpty(values[info.DeleteAllColumnName]))
            {
                return info.CreateIdentifierFunc();
            }

            return info.CreateFunc();
        }
    }
}
