//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.5
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
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V11.Bulk.Entities;
using BulkAccount = Microsoft.BingAds.V11.Bulk.Entities.BulkAccount;
using BulkAdGroup = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroup;
using BulkAdGroupAppAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupAppAdExtension;
using BulkAdGroupCalloutAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupCalloutAdExtension;
using BulkAdGroupDynamicSearchAdTarget = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupDynamicSearchAdTarget;
using BulkAdGroupImageAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupImageAdExtension;
using BulkAdGroupNegativeDynamicSearchAdTarget = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupNegativeDynamicSearchAdTarget;
using BulkAdGroupNegativeKeyword = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupNegativeKeyword;
using BulkAdGroupNegativeSite = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupNegativeSite;
using BulkAdGroupNegativeSitesIdentifier = Microsoft.BingAds.V11.Internal.Bulk.Entities.BulkAdGroupNegativeSitesIdentifier;
using BulkAdGroupProductPartition = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupProductPartition;
//using BulkAdGroupRemarketingListAssociation = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupRemarketingListAssociation;
using BulkAdGroupReviewAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupReviewAdExtension;
using BulkAdGroupSitelink2AdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupSitelink2AdExtension;
using BulkAdGroupSiteLinkAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupSiteLinkAdExtension;
using BulkAdGroupStructuredSnippetAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAdGroupStructuredSnippetAdExtension;
using BulkAppAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkAppAdExtension;
using BulkAppInstallAd = Microsoft.BingAds.V11.Bulk.Entities.BulkAppInstallAd;
using BulkBudget = Microsoft.BingAds.V11.Bulk.Entities.BulkBudget;
using BulkCallAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCallAdExtension;
using BulkCalloutAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCalloutAdExtension;
using BulkCampaign = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaign;
using BulkCampaignAppAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignAppAdExtension;
using BulkCampaignCallAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignCallAdExtension;
using BulkCampaignCalloutAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignCalloutAdExtension;
using BulkCampaignImageAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignImageAdExtension;
using BulkCampaignLocationAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignLocationAdExtension;
using BulkCampaignNegativeDynamicSearchAdTarget = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignNegativeDynamicSearchAdTarget;
using BulkCampaignNegativeKeyword = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignNegativeKeyword;
using BulkCampaignNegativeKeywordList = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignNegativeKeywordList;
using BulkCampaignNegativeSite = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignNegativeSite;
using BulkCampaignNegativeSitesIdentifier = Microsoft.BingAds.V11.Internal.Bulk.Entities.BulkCampaignNegativeSitesIdentifier;
using BulkCampaignProductScope = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignProductScope;
using BulkCampaignReviewAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignReviewAdExtension;
using BulkCampaignSitelink2AdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignSitelink2AdExtension;
using BulkCampaignSiteLinkAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignSiteLinkAdExtension;
using BulkCampaignStructuredSnippetAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkCampaignStructuredSnippetAdExtension;
using BulkDynamicSearchAd = Microsoft.BingAds.V11.Bulk.Entities.BulkDynamicSearchAd;
using BulkError = Microsoft.BingAds.V11.Bulk.Entities.BulkError;
using BulkExpandedTextAd = Microsoft.BingAds.V11.Bulk.Entities.BulkExpandedTextAd;
using BulkImageAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkImageAdExtension;
using BulkKeyword = Microsoft.BingAds.V11.Bulk.Entities.BulkKeyword;
using BulkKeywordBestPositionBid = Microsoft.BingAds.V11.Internal.Bulk.Entities.BulkKeywordBestPositionBid;
using BulkKeywordFirstPageBid = Microsoft.BingAds.V11.Internal.Bulk.Entities.BulkKeywordFirstPageBid;
using BulkKeywordMainLineBid = Microsoft.BingAds.V11.Bulk.Entities.BulkKeywordMainLineBid;
using BulkLocationAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkLocationAdExtension;
using BulkNegativeKeywordList = Microsoft.BingAds.V11.Bulk.Entities.BulkNegativeKeywordList;
using BulkProductAd = Microsoft.BingAds.V11.Bulk.Entities.BulkProductAd;
using BulkRemarketingList = Microsoft.BingAds.V11.Bulk.Entities.BulkRemarketingList;
using BulkReviewAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkReviewAdExtension;
using BulkSharedNegativeKeyword = Microsoft.BingAds.V11.Bulk.Entities.BulkSharedNegativeKeyword;
using BulkSitelink2AdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkSitelink2AdExtension;
using BulkSiteLink = Microsoft.BingAds.V11.Bulk.Entities.BulkSiteLink;
using BulkStructuredSnippetAdExtension = Microsoft.BingAds.V11.Bulk.Entities.BulkStructuredSnippetAdExtension;
using BulkTextAd = Microsoft.BingAds.V11.Bulk.Entities.BulkTextAd;
using SiteLinkAdExtensionIdentifier = Microsoft.BingAds.V11.Internal.Bulk.Entities.SiteLinkAdExtensionIdentifier;
using UnknownBulkEntity = Microsoft.BingAds.V11.Bulk.Entities.UnknownBulkEntity;

namespace Microsoft.BingAds.V11.Internal.Bulk
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
                {StringTable.Keyword,                       new EntityInfo(() => new BulkKeyword())},                
                {StringTable.SiteLinksAdExtension,          new EntityInfo(() => new BulkSiteLink(),                            StringTable.SiteLinkExtensionOrder, () => new SiteLinkAdExtensionIdentifier())},
                {StringTable.CampaignSiteLinksAdExtension,  new EntityInfo(() => new BulkCampaignSiteLinkAdExtension())},
                {StringTable.AdGroupSiteLinksAdExtension,   new EntityInfo(() => new BulkAdGroupSiteLinkAdExtension())},
                {StringTable.ImageAdExtension,              new EntityInfo(() => new BulkImageAdExtension())},
                {StringTable.CampaignImageAdExtension,      new EntityInfo(() => new BulkCampaignImageAdExtension())},
                {StringTable.AdGroupImageAdExtension,       new EntityInfo(() => new BulkAdGroupImageAdExtension())},
                {StringTable.LocationAdExtension,           new EntityInfo(() => new BulkLocationAdExtension())},
                {StringTable.CampaignLocationAdExtension,   new EntityInfo(() => new BulkCampaignLocationAdExtension())},
                {StringTable.CallAdExtension,               new EntityInfo(() => new BulkCallAdExtension())},            
                {StringTable.CampaignCallAdExtension,       new EntityInfo(() => new BulkCampaignCallAdExtension())},            
                {StringTable.AppAdExtension,                new EntityInfo(() => new BulkAppAdExtension())},
                {StringTable.CampaignAppAdExtension,        new EntityInfo(() => new BulkCampaignAppAdExtension())},
                {StringTable.AdGroupAppAdExtension,         new EntityInfo(() => new BulkAdGroupAppAdExtension())},
                {StringTable.ReviewAdExtension,             new EntityInfo(() => new BulkReviewAdExtension()) },
                {StringTable.CampaignReviewAdExtension,     new EntityInfo(() => new BulkCampaignReviewAdExtension())},
                {StringTable.AdGroupReviewAdExtension,      new EntityInfo(() => new BulkAdGroupReviewAdExtension())},
                {StringTable.CalloutAdExtension,            new EntityInfo(() => new BulkCalloutAdExtension()) },
                {StringTable.CampaignCalloutAdExtension,    new EntityInfo(() => new BulkCampaignCalloutAdExtension())},
                {StringTable.AdGroupCalloutAdExtension,     new EntityInfo(() => new BulkAdGroupCalloutAdExtension())},
                {StringTable.StructuredSnippetAdExtension,  new EntityInfo(() => new BulkStructuredSnippetAdExtension()) },
                {StringTable.CampaignStructuredSnippetAdExtension,    new EntityInfo(() => new BulkCampaignStructuredSnippetAdExtension())},
                {StringTable.AdGroupStructuredSnippetAdExtension,     new EntityInfo(() => new BulkAdGroupStructuredSnippetAdExtension())},
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

                {StringTable.Sitelink2AdExtesnion,          new EntityInfo(() => new BulkSitelink2AdExtension())},
                {StringTable.CampaignSitelink2AdExtension,  new EntityInfo(() => new BulkCampaignSitelink2AdExtension())},
                {StringTable.AdGroupSitelink2AdExtension,   new EntityInfo(() => new BulkAdGroupSitelink2AdExtension())}, 
                {StringTable.Budget,                        new EntityInfo(() => new BulkBudget())},

                {StringTable.DynamicSearchAd,               new EntityInfo(() => new BulkDynamicSearchAd())},
                {StringTable.AdGroupDynamicSearchAdTarget,  new EntityInfo(() => new BulkAdGroupDynamicSearchAdTarget())},
                {StringTable.AdGroupNegativeDynamicSearchAdTarget, new EntityInfo(() => new BulkAdGroupNegativeDynamicSearchAdTarget())},
                {StringTable.CampaignNegativeDynamicSearchAdTarget, new EntityInfo(() => new BulkCampaignNegativeDynamicSearchAdTarget())},

                {StringTable.AdGroupAgeCriterion,  new EntityInfo(() => new BulkAdGroupAgeCriterion())},
                {StringTable.AdGroupDayTimeCriterion,  new EntityInfo(() => new BulkAdGroupDayTimeCriterion())},
                {StringTable.AdGroupDeviceCriterion,  new EntityInfo(() => new BulkAdGroupDeviceCriterion())},
                {StringTable.AdGroupGenderCriterion,  new EntityInfo(() => new BulkAdGroupGenderCriterion())},
                {StringTable.AdGroupLocationCriterion,  new EntityInfo(() => new BulkAdGroupLocationCriterion())},
                {StringTable.AdGroupLocationIntentCriterion,  new EntityInfo(() => new BulkAdGroupLocationIntentCriterion())},
                {StringTable.AdGroupNegativeLocationCriterion,  new EntityInfo(() => new BulkAdGroupNegativeLocationCriterion())},
                {StringTable.AdGroupRadiusCriterion,  new EntityInfo(() => new BulkAdGroupRadiusCriterion())},
                {StringTable.CampaignAgeCriterion,  new EntityInfo(() => new BulkCampaignAgeCriterion())},
                {StringTable.CampaignDayTimeCriterion,  new EntityInfo(() => new BulkCampaignDayTimeCriterion())},
                {StringTable.CampaignDeviceCriterion,  new EntityInfo(() => new BulkCampaignDeviceCriterion())},
                {StringTable.CampaignGenderCriterion,  new EntityInfo(() => new BulkCampaignGenderCriterion())},
                {StringTable.CampaignLocationCriterion,  new EntityInfo(() => new BulkCampaignLocationCriterion())},
                {StringTable.CampaignLocationIntentCriterion,  new EntityInfo(() => new BulkCampaignLocationIntentCriterion())},
                {StringTable.CampaignNegativeLocationCriterion,  new EntityInfo(() => new BulkCampaignNegativeLocationCriterion())},
                {StringTable.CampaignRadiusCriterion,  new EntityInfo(() => new BulkCampaignRadiusCriterion())},
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