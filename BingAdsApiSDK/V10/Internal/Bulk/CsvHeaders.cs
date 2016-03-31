//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
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

namespace Microsoft.BingAds.V10.Internal.Bulk
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
            StringTable.SearchBid,
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
            StringTable.MediaIds,

            StringTable.PublisherCountries,
    
            // Product target
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

            // Upgrade Url
            StringTable.FinalUrl,
            StringTable.FinalMobileUrl,
            StringTable.TrackingTemplate,
            StringTable.CustomParameter,

            // Review Ad Extension
            StringTable.IsExact,
            StringTable.Source,
            StringTable.Url,

            // Callout Ad Extension
            StringTable.CalloutText
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
