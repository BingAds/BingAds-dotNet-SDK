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

using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.Bulk.Entities
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
        /// The bid strategy name for the campaign.
        /// </summary>
        public string BidStrategyName { get; set; }

        ///<summary>
        /// The budget name for the campaign.
        /// </summary>
        public string BudgetName { get; set; }

        /// <summary>
        /// The quality score data for the campaign.
        /// </summary>
        public QualityScoreData QualityScoreData { get; private set; }

        /// <summary>
        /// Destination Channel
        /// </summary>
        public string DestinationChannel { get; set; }

        public bool? IsMultiChannelCampaign { get; set; }

        public bool? ShouldServeOnMSAN { get; set; }

        // Only for internal use
        public bool? IsLinkedInCampaign { get; set; }

        // Setting the default value to false, as it is will always be false if not passed and we do not want to return it.
        public bool IsPolitical { get; set; } = false;

        public EnabledExternalChannelSync? EnabledExternalChannelSync { get; set; }

        public EntityScope? Scope { get; set; }

        /// <summary>
        /// Campaigns of type Shopping have exactly one ShoppingSetting.
        /// Campaigns of type Audience can have zero or one ShoppingSetting. 
        /// </summary>
        /// <returns></returns>
        private Setting GetCampaignSetting(Type campaignSettingType, bool addifNotExist = false)
        {
            if (Campaign.Settings == null && addifNotExist)
            {
                AddCampaignSettings();
            }
            if (Campaign.Settings == null) return null;

            var settings = Campaign.Settings.Where(setting => setting.GetType() == campaignSettingType).ToList();

            if (settings == null || settings.Count < 1) return null;

            if (settings.Count != 1)
            {
                throw new ArgumentException("Can only have 1 DynamicSearchAdsSetting in Campaign Settings");
            }
            return settings[0];
        }


        private void AddCampaignSettings()
        {
            if (IsLinkedInCampaign == true)
            {
                Campaign.Settings = new List<Setting>
                {
                    new LinkedInSetting
                    {
                        Type = typeof(LinkedInSetting).Name
                    }
                };
                return;
            }

            if (Campaign.CampaignType == null) return;
            switch (Campaign.CampaignType)
            {
                case CampaignType.Search:
                case CampaignType.DynamicSearchAds:
                    {
                        Campaign.Settings = new List<Setting>
                        {
                            new DynamicSearchAdsSetting
                            {
                                Type = typeof(DynamicSearchAdsSetting).Name
                            },
                            new TargetSetting
                            {
                                Type = typeof(TargetSetting).Name,
                            },
                            new DisclaimerSetting
                            {
                                Type = typeof(DisclaimerSetting).Name,
                            }
                        };
                    }
                    break;
                case CampaignType.Shopping:
                    {
                        Campaign.Settings = new List<Setting>
                        {
                            new ShoppingSetting
                            {
                                Type = typeof(ShoppingSetting).Name,
                            },
                            new TargetSetting
                            {
                                Type = typeof(TargetSetting).Name,
                            },
                        };
                    }
                    break;
                case CampaignType.Audience:
                    {
                        Campaign.Settings = new List<Setting>
                        {
                            new ShoppingSetting
                            {
                                Type = typeof(ShoppingSetting).Name,
                            },
                            new TargetSetting
                            {
                                Type = typeof(TargetSetting).Name,
                            },
                            new DynamicFeedSetting
                            {
                                Type = typeof(DynamicFeedSetting).Name,
                            },
                            new VerifiedTrackingSetting
                            {
                                Type = typeof(VerifiedTrackingSetting).Name,
                            }
                        };
                    }
                    break;
                case CampaignType.PerformanceMax:
                    {
                        Campaign.Settings = new List<Setting>
                        {
                            new PerformanceMaxSetting
                            {
                                Type = typeof(PerformanceMaxSetting).Name
                            },
                            new ShoppingSetting
                            {
                                Type = typeof(ShoppingSetting).Name,
                            },
                            new NewCustomerAcquisitionGoalSetting
                            {
                                Type = typeof(NewCustomerAcquisitionGoalSetting).Name,
                            },
                        };
                    }
                    break;
            }
        }

        private static readonly IBulkMapping<BulkCampaign>[] Mappings =
        {
            // NOTE: Put this mapping before other mapping which need to access CampaignType and Settings
            new SimpleBulkMapping<BulkCampaign>(StringTable.CampaignType,
                c =>
                {
                    if (c.IsLinkedInCampaign == true)
                    {
                        return "LinkedIn";
                    }

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
                    if (v == "LinkedIn")
                    {
                        c.IsLinkedInCampaign = true;
                    }
                    else
                    {
                        c.Campaign.CampaignType = v.ParseOptional<CampaignType>();
                    }
     
                    c.AddCampaignSettings();
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

            new SimpleBulkMapping<BulkCampaign>(StringTable.SubType,
                c => c.Campaign.SubType,
                (v, c) => c.Campaign.SubType = v
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.Language,
                c => c.Campaign.Languages.WriteCampaignLanguages(";"),
                (v, c) => c.Campaign.Languages = v.ParseCampaignLanguages()
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

            new SimpleBulkMapping<BulkCampaign>(StringTable.BidStrategyName,
                c => c.BidStrategyName,
                (v, c) => c.BidStrategyName= v
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BidStrategyScope,
                c => c.Scope.ToBulkString(),
                (v, c) => c.Scope = v.ParseOptional<EntityScope>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BidStrategyId,
                c => c.Campaign.BidStrategyId.ToBulkString(),
                (v, c) => c.Campaign.BidStrategyId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.BidAdjustment,
                c => c.Campaign.AudienceAdsBidAdjustment.ToBulkString(),
                (v, c) => c.Campaign.AudienceAdsBidAdjustment = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.IsPolitical,
                c => c.IsPolitical.ToString(),
                (v, c) => c.IsPolitical = (v.ParseOptional<bool>() ?? false)
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.MerchantCenterId,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting))) as ShoppingSetting;
                    return setting == null ? null : setting.StoreId.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting), true)) as ShoppingSetting;
                    var storeId = v.ParseOptional<long>();
                    if(storeId != null && setting != null)
                    {
                        setting.StoreId = storeId;
                    }
                }
            ), 
            
            new SimpleBulkMapping<BulkCampaign>(StringTable.FeedId,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicFeedSetting))) as DynamicFeedSetting;
                    return setting == null ? null : setting.FeedId.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicFeedSetting), true)) as DynamicFeedSetting;
                    var feedId = v.ParseOptional<long>();
                    if(feedId != null && setting != null)
                    {
                        setting.FeedId = feedId;
                    }
                }
            ), 

            new SimpleBulkMapping<BulkCampaign>(StringTable.CampaignPriority,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting))) as ShoppingSetting;
                    return setting == null ? null : setting.Priority.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting), true)) as ShoppingSetting;
                    var priority = v.ParseOptional<int>();
                    if(priority != null && setting != null)
                    {
                        setting.Priority = priority;
                    }
                }
            ), 

            new SimpleBulkMapping<BulkCampaign>(StringTable.CountryCode,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting))) as ShoppingSetting;
                    return setting == null ? null : setting.SalesCountryCode;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting), true)) as ShoppingSetting;
                    var salesCountryCode = v;
                    if(salesCountryCode != null && setting != null)
                    {
                        setting.SalesCountryCode = salesCountryCode;
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.LocalInventoryAdsEnabled,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting))) as ShoppingSetting;
                    return setting == null ? null : setting.LocalInventoryAdsEnabled.ToString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(ShoppingSetting), true)) as ShoppingSetting;
                    var localInventoryAdsEnabled = v.ParseOptional<bool>();
                    if(localInventoryAdsEnabled != null && setting != null)
                    {
                        setting.LocalInventoryAdsEnabled = localInventoryAdsEnabled;
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.TargetSetting,
                c =>
                {
                    var setting = (TargetSetting)c.GetCampaignSetting(typeof(TargetSetting));
                    return setting?.ToBulkString(c.Campaign.Id);
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(TargetSetting), true)) as TargetSetting;
                    var details = v.ParseTargetSettingDetails();
                    if(details != null && setting != null)
                    {
                        setting.Details = details;
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.CampaignGoal,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(LinkedInSetting))) as LinkedInSetting;
                    return setting == null ? null : ((int)setting.Goal).ToString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(LinkedInSetting), true)) as LinkedInSetting;
                    var goal = v.ParseOptional<CampaignGoal>();
                    if(goal != null && setting != null)
                    {
                        setting.Goal = goal;
                    }
                }
            ),

            new ComplexBulkMapping<BulkCampaign>(BudgetToCsv, CsvToBudget),
            
            new SimpleBulkMapping<BulkCampaign>(StringTable.TrackingTemplate,
                c => c.Campaign.TrackingUrlTemplate.ToOptionalBulkString(c.Campaign.Id),
                (v, c) => c.Campaign.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.CustomParameter,
                c => c.Campaign.UrlCustomParameters.ToBulkString(c.Campaign.Id),
                (v, c) => c.Campaign.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new ComplexBulkMapping<BulkCampaign>(BiddingSchemeToCsv, CsvToBiddingScheme),
                        
            new SimpleBulkMapping<BulkCampaign>(StringTable.Website,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting))) as DynamicSearchAdsSetting;
                    return setting == null ? null : setting.DomainName;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting), true)) as DynamicSearchAdsSetting;
                    if (setting != null)
                    {
                        setting.DomainName = v;
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.DomainLanguage,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting))) as DynamicSearchAdsSetting;
                    return setting == null ? null : setting.Language;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting), true)) as DynamicSearchAdsSetting;
                    if (setting != null)
                    {
                        setting.Language = v;
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.Source,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting))) as DynamicSearchAdsSetting;
                    return setting == null ? null : setting.Source.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting), true)) as DynamicSearchAdsSetting;
                    if (setting != null)
                    {
                        setting.Source = v.ParseOptional<DynamicSearchAdsSource>();
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.PageFeedIds,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting))) as DynamicSearchAdsSetting;

                    if (setting == null)
                    {
                        var performaxSetting = (c.GetCampaignSetting(typeof(PerformanceMaxSetting))) as PerformanceMaxSetting;

                        if  (performaxSetting == null || performaxSetting.PageFeedIds == null || performaxSetting.PageFeedIds.Count == 0)
                        {
                            return null;
                        }
                        return string.Join(";", performaxSetting.PageFeedIds);
                    }
                    else
                    {
                        if (setting.PageFeedIds == null || setting.PageFeedIds.Count == 0)
                        {
                            return null;
                        }
                        return string.Join(";", setting.PageFeedIds);
                    }
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting), true)) as DynamicSearchAdsSetting;

                    if (setting == null)
                    {
                        var performaxSetting = (c.GetCampaignSetting(typeof(PerformanceMaxSetting))) as PerformanceMaxSetting;

                        if (performaxSetting != null && !string.IsNullOrEmpty(v))
                        {
                            performaxSetting.PageFeedIds = v.Split(';').Select(long.Parse).ToList();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(v))
                        {
                            setting.PageFeedIds = v.Split(';').Select(long.Parse).ToList();
                        }       
                    }
                }
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.ExperimentId,
                c => c.Campaign.ExperimentId.ToBulkString(),
                (v, c) => c.Campaign.ExperimentId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.FinalUrlSuffix,
                c => c.Campaign.FinalUrlSuffix.ToOptionalBulkString(c.Campaign.Id),
                (v, c) => c.Campaign.FinalUrlSuffix = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.AdScheduleUseSearcherTimeZone,
                c => c.Campaign.AdScheduleUseSearcherTimeZone.ToUseSearcherTimeZoneBulkString(null),
                (v, c) => c.Campaign.AdScheduleUseSearcherTimeZone = v.ParseOptional<bool>()
            ),

            new SimpleBulkMapping<BulkCampaign>(StringTable.MultiMediaAdBidAdjustment,
                c => c.Campaign.MultimediaAdsBidAdjustment.ToBulkString(),
                (v, c) => c.Campaign.MultimediaAdsBidAdjustment = v.ParseOptional<int>()
            ),
            
            new SimpleBulkMapping<BulkCampaign>(StringTable.DynamicDescriptionEnabled,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting))) as DynamicSearchAdsSetting;
                    return setting?.DynamicDescriptionEnabled?.ToString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DynamicSearchAdsSetting), true)) as DynamicSearchAdsSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.DynamicDescriptionEnabled = v.ParseOptional<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.DisclaimerAdsEnabled,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DisclaimerSetting))) as DisclaimerSetting;
                    return setting?.DisclaimerAdsEnabled.ToString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(DisclaimerSetting), true)) as DisclaimerSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.DisclaimerAdsEnabled = v.Parse<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.UrlExpansionOptOut,
                c =>
                {
                    var setting = c.GetCampaignSetting(typeof(PerformanceMaxSetting)) as PerformanceMaxSetting;
                    if (setting != null)
                    {
                        return setting.FinalUrlExpansionOptOut.ToString();
                    }
                    return null;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(PerformanceMaxSetting), true)) as PerformanceMaxSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.FinalUrlExpansionOptOut = v.Parse<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.DestinationChannel,
                c => c.DestinationChannel,
                (v, c) => c.DestinationChannel = v
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.IsMultiChannelCampaign,
                c => c.IsMultiChannelCampaign?.ToString(),
                (v, c) => c.IsMultiChannelCampaign = v.ParseOptional<bool>()
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.ShouldServeOnMSAN,
                c => c.ShouldServeOnMSAN?.ToString(),
                (v, c) => c.ShouldServeOnMSAN = v.ParseOptional<bool>()
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.AutoGeneratedImageOptOut,
                c =>
                {
                    var setting = c.GetCampaignSetting(typeof(PerformanceMaxSetting)) as PerformanceMaxSetting;
                    if (setting != null)
                    {
                        return setting.AutoGeneratedImageOptOut.ToString();
                    }
                    return null;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(PerformanceMaxSetting), true)) as PerformanceMaxSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.AutoGeneratedImageOptOut = v.Parse<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.AutoGeneratedTextOptOut,
                c =>
                {
                    var setting = c.GetCampaignSetting(typeof(PerformanceMaxSetting)) as PerformanceMaxSetting;
                    if (setting != null)
                    {
                        return setting.AutoGeneratedTextOptOut.ToString();
                    }
                    return null;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(PerformanceMaxSetting), true)) as PerformanceMaxSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.AutoGeneratedTextOptOut = v.Parse<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.CostPerSaleOptOut,
                c =>
                {
                    var setting = c.GetCampaignSetting(typeof(PerformanceMaxSetting)) as PerformanceMaxSetting;
                    if (setting != null)
                    {
                        return setting.CostPerSaleOptOut.ToString();
                    }
                    return null;
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(PerformanceMaxSetting), true)) as PerformanceMaxSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.CostPerSaleOptOut = v.Parse<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.IsDealCampaign,
                c => c.Campaign.IsDealCampaign.ToString(),
                (v, c) => c.Campaign.IsDealCampaign = v.ParseOptional<bool>()
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.EnabledExternalChannelSync,
                c => c.EnabledExternalChannelSync.ToBulkString(),
                (v, c) => c.EnabledExternalChannelSync = v.ParseOptional<EnabledExternalChannelSync>()
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.NewCustomerAcquisitionGoalId,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(NewCustomerAcquisitionGoalSetting))) as NewCustomerAcquisitionGoalSetting;
                    return setting?.NewCustomerAcquisitionGoalId.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(NewCustomerAcquisitionGoalSetting), true)) as NewCustomerAcquisitionGoalSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.NewCustomerAcquisitionGoalId = v.ParseOptional<long>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.NewCustomerAcquisitionBidOnlyMode,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(NewCustomerAcquisitionGoalSetting))) as NewCustomerAcquisitionGoalSetting;
                    return setting?.NewCustomerAcquisitionBidOnlyMode.ToString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(NewCustomerAcquisitionGoalSetting), true)) as NewCustomerAcquisitionGoalSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.NewCustomerAcquisitionBidOnlyMode = v.ParseOptional<bool>();
                    }
                }
            ),
            new SimpleBulkMapping<BulkCampaign>(StringTable.AdditionalConversionValue,
                c =>
                {
                    var setting = (c.GetCampaignSetting(typeof(NewCustomerAcquisitionGoalSetting))) as NewCustomerAcquisitionGoalSetting;
                    return setting?.AdditionalConversionValue.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (c.GetCampaignSetting(typeof(NewCustomerAcquisitionGoalSetting), true)) as NewCustomerAcquisitionGoalSetting;
                    if (setting != null && !string.IsNullOrEmpty(v))
                    {
                        setting.AdditionalConversionValue = v.ParseOptional<decimal>();
                    }
                }
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Campaign = new Campaign();

            values.ConvertToEntity(this, Mappings);

            QualityScoreData = QualityScoreData.ReadFromRowValuesOrNull(values);

        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(Campaign, "Campaign");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                QualityScoreData.WriteToRowValuesIfNotNull(QualityScoreData, values);

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

        private static void CsvToBiddingScheme(RowValues values, BulkCampaign c)
        {   
            c.Campaign.BiddingScheme = values.ReadBiddingSchemaFromValues();
        }

        private static void BiddingSchemeToCsv(BulkCampaign c, RowValues values)
        {
            var biddingScheme = c.Campaign.BiddingScheme;

            if (biddingScheme == null)
            {
                return;
            }

            biddingScheme.WriteToValues(values, c.Campaign.Id);
        }
    }
}
