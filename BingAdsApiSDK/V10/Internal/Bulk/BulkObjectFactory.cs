using System;
using System.Collections.Generic;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal class BulkObjectFactory : IBulkObjectFactory
    {
        private static readonly Dictionary<string, EntityInfo> IndividualEntityMap;

        private static readonly Dictionary<string, Func<BulkObject>> AdditionalObjectMap;

        private static readonly Dictionary<Type, string> TypeReverseMap;

        private static readonly Dictionary<Type, Dictionary<Type, string>> TargetIdentifierTypeReverseMap;

        static BulkObjectFactory()
        {
            IndividualEntityMap = new Dictionary<string, EntityInfo>
            {
                {StringTable.Account,                       new EntityInfo(() => new BulkAccount())},
                {StringTable.Campaign,                      new EntityInfo(() => new BulkCampaign())},
                {StringTable.AdGroup,                       new EntityInfo(() => new BulkAdGroup())},
                {StringTable.TextAd,                        new EntityInfo(() => new BulkTextAd())},
                {StringTable.ProductAd,                     new EntityInfo(() => new BulkProductAd())},
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
                {"Campaign Negative Site",                  new EntityInfo(() => new BulkCampaignNegativeSite(),                StringTable.Website,                () => new BulkCampaignNegativeSitesIdentifier())},
                {"Ad Group Negative Site",                  new EntityInfo(() => new BulkAdGroupNegativeSite(),                 StringTable.Website,                () => new BulkAdGroupNegativeSitesIdentifier())},
                
                {"Campaign Location Target",                new EntityInfo(() => new BulkCampaignLocationTargetBid(),           StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignLocationTargetBid)))},
                {"Campaign Negative Location Target",       new EntityInfo(() => new BulkCampaignNegativeLocationTargetBid(),   StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignNegativeLocationTargetBid)))},
                {"Campaign Age Target",                     new EntityInfo(() => new BulkCampaignAgeTargetBid(),                StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignAgeTargetBid)))},
                {"Campaign DayTime Target",                 new EntityInfo(() => new BulkCampaignDayTimeTargetBid(),            StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignDayTimeTargetBid)))},                
                {"Campaign Gender Target",                  new EntityInfo(() => new BulkCampaignGenderTargetBid(),             StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignGenderTargetBid)))},
                {"Campaign DeviceOS Target",                new EntityInfo(() => new BulkCampaignDeviceOsTargetBid(),           StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignDeviceOsTargetBid)))},
                {"Campaign Radius Target",                  new EntityInfo(() => new BulkCampaignRadiusTargetBid(),             StringTable.Target,                 () => new BulkCampaignTargetIdentifier(typeof(BulkCampaignRadiusTargetBid)))},
                
                {"Ad Group Location Target",                new EntityInfo(() => new BulkAdGroupLocationTargetBid(),           StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupLocationTargetBid)))},
                {"Ad Group Negative Location Target",       new EntityInfo(() => new BulkAdGroupNegativeLocationTargetBid(),   StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupNegativeLocationTargetBid)))},
                {"Ad Group Age Target",                     new EntityInfo(() => new BulkAdGroupAgeTargetBid(),                StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupAgeTargetBid)))},
                {"Ad Group DayTime Target",                 new EntityInfo(() => new BulkAdGroupDayTimeTargetBid(),            StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupDayTimeTargetBid)))},                
                {"Ad Group Gender Target",                  new EntityInfo(() => new BulkAdGroupGenderTargetBid(),             StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupGenderTargetBid)))},
                {"Ad Group DeviceOS Target",                new EntityInfo(() => new BulkAdGroupDeviceOsTargetBid(),           StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupDeviceOsTargetBid)))},
                {"Ad Group Radius Target",                  new EntityInfo(() => new BulkAdGroupRadiusTargetBid(),             StringTable.Target,                 () => new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupRadiusTargetBid)))},

                {StringTable.NegativeKeywordList,           new EntityInfo(() => new BulkNegativeKeywordList())},
                {StringTable.ListNegativeKeyword,           new EntityInfo(() => new BulkSharedNegativeKeyword())},
                {StringTable.CampaignNegativeKeywordList,   new EntityInfo(() => new BulkCampaignNegativeKeywordList())},
                {StringTable.CampaignNegativeKeyword,       new EntityInfo(() => new BulkCampaignNegativeKeyword())},
                {StringTable.AdGroupNegativeKeyword,        new EntityInfo(() => new BulkAdGroupNegativeKeyword())},

                {"Campaign Product Scope",                  new EntityInfo(() => new BulkCampaignProductScope())},
                {"Ad Group Product Partition",              new EntityInfo(() => new BulkAdGroupProductPartition())}
            };

            AdditionalObjectMap = new Dictionary<string, Func<BulkObject>>
            {
                {"Keyword Best Position Bid",           () => new BulkKeywordBestPositionBid()},
                {"Keyword Main Line Bid",               () => new BulkKeywordMainLineBid()},
                {"Keyword First Page Bid",              () => new BulkKeywordFirstPageBid()},

                {"Format Version",                      () => new FormatVersion()}
            };

            TypeReverseMap = new Dictionary<Type, string>();

            TargetIdentifierTypeReverseMap = new Dictionary<Type, Dictionary<Type, string>>();

            foreach (var pair in IndividualEntityMap)
            {
                TypeReverseMap[pair.Value.CreateFunc().GetType()] = pair.Key;

                if (pair.Value.CreateIdentifierFunc != null)
                {
                    var identifier = pair.Value.CreateIdentifierFunc();

                    if (identifier is BulkTargetIdentifier)
                    {
                        if (!TargetIdentifierTypeReverseMap.ContainsKey(identifier.GetType()))
                        {
                            TargetIdentifierTypeReverseMap[identifier.GetType()] = new Dictionary<Type, string>();
                        }

                        TargetIdentifierTypeReverseMap[identifier.GetType()][((BulkTargetIdentifier)identifier).TargetBidType] = pair.Key;
                    }
                    else
                    {
                        TypeReverseMap[identifier.GetType()] = pair.Key;
                    }
                }
            }
        }

        public string GetBulkRowType(BulkObject bulkObject)
        {
            if (bulkObject is BulkTargetIdentifier)
            {
                var identifier = (BulkTargetIdentifier)bulkObject;

                return TargetIdentifierTypeReverseMap[identifier.GetType()][identifier.TargetBidType];
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