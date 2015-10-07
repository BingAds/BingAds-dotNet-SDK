using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BingAds.Internal.Bulk
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

            // campaign
            StringTable.TimeZone,
            StringTable.Budget,
            StringTable.BudgetType,
            StringTable.KeywordVariantMatchEnabled,

            // Ad Group
            StringTable.StartDate,
            StringTable.EndDate,
            StringTable.NetworkDistribution,
            StringTable.PricingModel,
            StringTable.AdRotation,
            StringTable.SearchNetwork,
            StringTable.SearchBroadBid,
            StringTable.ContentNetwork,
            StringTable.ContentBid,            
            StringTable.Language,
            
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
            
            // Keywords
            StringTable.Keyword,
            StringTable.MatchType,
            StringTable.Bid,
            StringTable.Param1,
            StringTable.Param2,
            StringTable.Param3,

            // location target
            StringTable.Target,
            StringTable.PhysicalIntent,
            StringTable.TargetAll,
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

            // AdExtensions common
            StringTable.Version,

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
            StringTable.MediaId,

            StringTable.PublisherCountries,
    
            // Product target
            StringTable.BingMerchantCenterId, 
            StringTable.BingMerchantCenterName,      
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
            StringTable.ProductCondition8,
            StringTable.ProductValue8,

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

            // Bing Shopping Campaigns
            StringTable.IsExcluded,
            StringTable.ParentAdGroupCriterionId,
            StringTable.CampaignType,
            StringTable.CampaignPriority,
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
