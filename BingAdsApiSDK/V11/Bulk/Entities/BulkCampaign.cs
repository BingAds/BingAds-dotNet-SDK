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
using System.Linq;
using Microsoft.BingAds.V11.Internal;
using Microsoft.BingAds.V11.Internal.Bulk;
using Microsoft.BingAds.V11.Internal.Bulk.Mappings;
using Microsoft.BingAds.V11.Internal.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

namespace Microsoft.BingAds.V11.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a campaign that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkCampaign.Campaign"/> property that can be read and written as fields of the Campaign record in a bulk file. 
    /// You must set the BudgetType property, otherwise the DailyBudget or MonthlyBudget will not be written to the Budget field of the bulk file.
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Campaign</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaign : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the account that contains the campaign.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Defines a campaign within an account. 
        /// </summary>
        public Campaign Campaign { get; set; }

        ///<summary>
        /// The budget name for the campaign.
        /// </summary>
        public string BudgetName { get; set; }

        /// <summary>
        /// The quality score data for the campaign.
        /// </summary>
        public QualityScoreData QualityScoreData { get; private set; }

        /// <summary>
        /// The historical performance data for the campaign.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        private ShoppingSetting GetShoppingSetting()
        {
            if (Campaign.Settings == null) return null;

            var shoppingSettings = Campaign.Settings.Where(setting => setting is ShoppingSetting).ToList();

            if (shoppingSettings.Count != 1)
            {
                throw new ArgumentException("Can only have 1 ShoppingSetting in Campaign Settings");
            }

            return (ShoppingSetting) shoppingSettings[0];
        }

        private DynamicSearchAdsSetting GetDynamicSearchAdsSetting()
        {
            if (Campaign.Settings == null) return null;

            var dynamicSearchAdsSettingSettings = Campaign.Settings.Where(setting => setting is DynamicSearchAdsSetting).ToList();

            if (dynamicSearchAdsSettingSettings.Count != 1)
            {
                throw new ArgumentException("Can only have 1 DynamicSearchAdsSetting in Campaign Settings");
            }

            return (DynamicSearchAdsSetting)dynamicSearchAdsSettingSettings[0];
        }

        private static readonly IBulkMapping<BulkCampaign>[] Mappings =
        {
            // NOTE: Put this mapping before other mapping which need to access CampaignType and Settings
            new SimpleBulkMapping<BulkCampaign>(StringTable.CampaignType,
                c =>
                {
                    if (!c.Campaign.CampaignType.HasValue)
                    {
                        return null;
                    }

                    var campaignType = c.Campaign.CampaignType.Value;

                    var count =
                        Enum.GetValues(typeof (CampaignType))
                            .Cast<object>()
                            .Count(value => campaignType.HasFlag((CampaignType) value));

                    if (count != 1)
                    {
                        throw new ArgumentException("Only 1 CampaignType can be set in Campaign");
                    }

                    return campaignType.ToBulkString();
                },
                (v, c) =>
                {
                    c.Campaign.CampaignType = v.ParseOptional<CampaignType>();

                    // NOTE: If there are other type of Setting, consider to refactor this part
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        c.Campaign.Settings = new List<Setting>
                        {
                            new ShoppingSetting
                            {
                                Type = typeof (ShoppingSetting).Name,
                            },
                        };
                    }

                    if (c.Campaign.CampaignType == CampaignType.DynamicSearchAds)
                    {
                        c.Campaign.Settings = new List<Setting>
                        {
                            new DynamicSearchAdsSetting
                            {
                                Type = typeof (DynamicSearchAdsSetting).Name,
                            },
                        };
                    }
                }
                ), 

            new SimpleBulkMapping<BulkCampaign>(StringTable.Status,
                c => c.Campaign.Status.ToBulkString(),
                (v, c) => c.Campaign.Status = v.ParseOptional<CampaignStatus>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.Id,
                c => c.Campaign.Id.ToBulkString(),
                (v, c) => c.Campaign.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.ParentId,
                c => c.AccountId.ToBulkString(),
                (v, c) => c.AccountId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.Campaign, 
                c => c.Campaign.Name, 
                (v, c) => c.Campaign.Name = v 
            ),            

            new SimpleBulkMapping<BulkCampaign>(StringTable.TimeZone, 
                c => c.Campaign.TimeZone, 
                (v, c) => c.Campaign.TimeZone = v
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BudgetType,
                c => c.Campaign.BudgetType.ToBulkString(),
                (v, c) => c.Campaign.BudgetType = v.ParseOptional<BudgetLimitType>()                
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BudgetId,
                c => c.Campaign.BudgetId.ToBulkString(),
                (v, c) => c.Campaign.BudgetId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BudgetName,
                c => c.BudgetName,
                (v, c) => c.BudgetName= v
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BidAdjustment,
                c => c.Campaign.NativeBidAdjustment.ToBulkString(),
                (v, c) => c.Campaign.NativeBidAdjustment = v.ParseOptional<int>()
            ), 

            new SimpleBulkMapping<BulkCampaign>(StringTable.BingMerchantCenterId,
                c =>
                {
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        var shoppingSetting = c.GetShoppingSetting();

                        return shoppingSetting == null ? null : shoppingSetting.StoreId.ToBulkString();
                    }

                    return null;
                },
                (v, c) =>
                {
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        var shoppingSetting = c.GetShoppingSetting();

                        shoppingSetting.StoreId = v.ParseOptional<long>();
                    }
                }
            ), 

            new SimpleBulkMapping<BulkCampaign>(StringTable.CampaignPriority,
                c =>
                {
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        var shoppingSetting = c.GetShoppingSetting();

                        return shoppingSetting == null ? null : shoppingSetting.Priority.ToBulkString();
                    }

                    return null;
                },
                (v, c) =>
                {
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        var shoppingSetting = c.GetShoppingSetting();

                        shoppingSetting.Priority = v.ParseOptional<int>();
                    }
                }
            ), 

            new SimpleBulkMapping<BulkCampaign>(StringTable.CountryCode,
                c =>
                {
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        var shoppingSetting = c.GetShoppingSetting();

                        return shoppingSetting == null ? null : shoppingSetting.SalesCountryCode;
                    }

                    return null;
                },
                (v, c) =>
                {
                    if (c.Campaign.CampaignType == CampaignType.Shopping)
                    {
                        var shoppingSetting = c.GetShoppingSetting();

                        shoppingSetting.SalesCountryCode = v;
                    }
                }
            ), 

            new ComplexBulkMapping<BulkCampaign>(BudgetToCsv, CsvToBudget),
            
            new SimpleBulkMapping<BulkCampaign>(StringTable.TrackingTemplate,
                c => c.Campaign.TrackingUrlTemplate.ToOptionalBulkString(),
                (v, c) => c.Campaign.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.CustomParameter,
                c => c.Campaign.UrlCustomParameters.ToBulkString(),
                (v, c) => c.Campaign.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BidStrategyType,
                c => c.Campaign.BiddingScheme.ToBiddingSchemeBulkString(),
                (v, c) => c.Campaign.BiddingScheme = v.ParseBiddingScheme()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.Website,
                c =>
                {
                    if (c.Campaign.CampaignType == CampaignType.DynamicSearchAds)
                    {
                        var dynamicSearchAdsSetting = c.GetDynamicSearchAdsSetting();

                        return dynamicSearchAdsSetting == null ? null : dynamicSearchAdsSetting.DomainName;
                    }

                    return null;
                },
                (v, c) =>
                {
                    if (c.Campaign.CampaignType == CampaignType.DynamicSearchAds)
                    {
                        var dynamicSearchAdsSetting = c.GetDynamicSearchAdsSetting();

                        dynamicSearchAdsSetting.DomainName = v;
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.DomainLanguage,
                c =>
                {
                    if (c.Campaign.CampaignType == CampaignType.DynamicSearchAds)
                    {
                        var dynamicSearchAdsSetting = c.GetDynamicSearchAdsSetting();

                        return dynamicSearchAdsSetting == null ? null : dynamicSearchAdsSetting.Language;
                    }

                    return null;
                },
                (v, c) =>
                {
                    if (c.Campaign.CampaignType == CampaignType.DynamicSearchAds)
                    {
                        var dynamicSearchAdsSetting = c.GetDynamicSearchAdsSetting();

                        dynamicSearchAdsSetting.Language = v;
                    }
                }
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Campaign = new Campaign();

            values.ConvertToEntity(this, Mappings);

            QualityScoreData = QualityScoreData.ReadFromRowValuesOrNull(values);

            PerformanceData = PerformanceData.ReadFromRowValuesOrNull(values);            
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(Campaign, "Campaign");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                QualityScoreData.WriteToRowValuesIfNotNull(QualityScoreData, values);

                PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
            }
        }

        private static void CsvToBudget(RowValues values, BulkCampaign c)
        {
            string budgetTypeRowValue;

            BudgetLimitType? budgetType;

            if (!values.TryGetValue(StringTable.BudgetType, out budgetTypeRowValue) || (budgetType = budgetTypeRowValue.ParseOptional<BudgetLimitType>()) == null)
            {
                return;
            }

            string budgetRowValue;
            
            if (!values.TryGetValue(StringTable.Budget, out budgetRowValue))
            {
                return;
            }

            var budgetValue = budgetRowValue.ParseOptional<double>();

            c.Campaign.BudgetType = budgetType;

            if (budgetType == BudgetLimitType.DailyBudgetAccelerated || budgetType == BudgetLimitType.DailyBudgetStandard)
            {
                c.Campaign.DailyBudget = budgetValue;
            }
        }

        private static void BudgetToCsv(BulkCampaign c, RowValues values)
        {
            var budgetType = c.Campaign.BudgetType;

            if (budgetType == null)
            {
                return;
            }

            values[StringTable.Budget] = c.Campaign.DailyBudget.ToBulkString();
        }
    }
}
