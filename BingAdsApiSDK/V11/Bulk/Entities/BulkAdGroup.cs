//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.12
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
using Microsoft.BingAds.V11.Internal.Bulk;
using Microsoft.BingAds.V11.Internal.Bulk.Mappings;
using Microsoft.BingAds.V11.Internal.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

namespace Microsoft.BingAds.V11.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group. 
    /// This class exposes the <see cref="BulkAdGroup.AdGroup"/> property that can be read and written as fields of the Ad Group record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Ad Group</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroup : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the campaign that contains the ad group.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The AdGroup Data Object of the Campaign Management Service. A subset of AdGroup properties are available 
        /// in the Ad Group record. For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Ad Group</see>.
        /// </summary>
        public AdGroup AdGroup { get; set; }

        /// <summary>
        /// Indicates whether the AdGroup is expired.
        /// </summary>
        public bool IsExpired { get; private set; }

        /// <summary>
        /// The quality score data for the ad group.
        /// </summary>
        public QualityScoreData QualityScoreData { get; private set; }

        /// <summary>
        /// The historical performance data for the ad group.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        public Setting GetSetting(Type settingType)
        {
            if (AdGroup.Settings == null || AdGroup.Settings.Count == 0)
            {
                return AddAdGroupSetting(settingType);
            }
            var setting = AdGroup.Settings.Where(s => settingType.Name.Equals(s.Type)).ToList();

            if (setting == null || setting.Count == 0)
            {
                return AddAdGroupSetting(settingType);
            }

            if (setting.Count != 1)
            {
                throw new ArgumentException(string.Format("Can only have 1 {0} in AdGroup Settings", settingType.Name));
            }
            return setting[0];
        }

        private Setting AddAdGroupSetting(Type settingType)
        {
            var setting = (Setting)Activator.CreateInstance(settingType);
            setting.Type = settingType.Name;
            if (AdGroup.Settings == null)
            {
                AdGroup.Settings = new List<Setting>();
            }
            AdGroup.Settings.Add(setting);
            return setting;
        }

        private static readonly IBulkMapping<BulkAdGroup>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdGroup>(StringTable.Id,
                c => c.AdGroup.Id.ToBulkString(),
                (v, c) => c.AdGroup.Id = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Status,
                c => c.IsExpired ? "Expired" : c.AdGroup.Status.ToBulkString(),
                (v, c) =>
                {
                    if (v == "Expired")
                    {
                        c.IsExpired = true;
                    }
                    else
                    {
                        c.AdGroup.Status = v.ParseOptional<AdGroupStatus>();
                    }
                }
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ParentId,
                c => c.CampaignId.ToBulkString(),
                (v, c) => c.CampaignId = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.AdGroup,
                c => c.AdGroup.Name,
                (v, c) => c.AdGroup.Name = v
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.StartDate,
                c => c.AdGroup.StartDate.ToDateBulkString(),
                (v, c) => c.AdGroup.StartDate = v.ParseDate()
                ),
            new SimpleBulkMapping<BulkAdGroup>(StringTable.EndDate,
                c => c.AdGroup.EndDate.ToDateBulkString(),
                (v, c) => c.AdGroup.EndDate = v.ParseDate()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.NetworkDistribution,
                c => c.AdGroup.Network.ToBulkString(),
                (v, c) => c.AdGroup.Network = v.ParseOptional<Network>()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.PricingModel,
                c => c.AdGroup.PricingModel.ToPricingModelBulkString(),
                (v, c) => c.AdGroup.PricingModel = v.ParseOptionalPricingModel()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.AdRotation,
                c => c.AdGroup.AdRotation.ToAdRotationBulkString(),
                (v, c) => c.AdGroup.AdRotation = v.ParseAdRotation()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.SearchNetwork,
                c => c.AdGroup.AdDistribution.ToSearchAdDistributionBulkString(),
                (v, c) => c.AdGroup.AdDistribution |= v.ParseSearchAdDistribution()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ContentNetwork,
                c => c.AdGroup.AdDistribution.ToContentAdDistributionBulkString(),
                (v, c) => c.AdGroup.AdDistribution |= v.ParseContentAdDistribution()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.SearchBid,
                c => c.AdGroup.SearchBid.ToAdGroupBidBulkString(),
                (v, c) => c.AdGroup.SearchBid = v.ParseAdGroupBid()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ContentBid,
                c => c.AdGroup.ContentMatchBid.ToAdGroupBidBulkString(),
                (v, c) => c.AdGroup.ContentMatchBid = v.ParseAdGroupBid()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Language,
                c => c.AdGroup.Language.ToOptionalBulkString(),
                (v, c) => c.AdGroup.Language = v.GetValueOrEmptyString()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.BidAdjustment,
                c => c.AdGroup.NativeBidAdjustment.ToBulkString(),
                (v, c) => c.AdGroup.NativeBidAdjustment = v.ParseOptional<int>()
                ),
                        
            new SimpleBulkMapping<BulkAdGroup>(StringTable.TrackingTemplate,
                c => c.AdGroup.TrackingUrlTemplate.ToOptionalBulkString(),
                (v, c) => c.AdGroup.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.CustomParameter,
                c => c.AdGroup.UrlCustomParameters.ToBulkString(),
                (v, c) => c.AdGroup.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.PrivacyStatus,
                c => c.AdGroup.PrivacyStatus.ToBulkString(),
                (v, c) => c.AdGroup.PrivacyStatus = v.ParseOptional<AdGroupPrivacyStatus>()
            ),
            
            new SimpleBulkMapping<BulkAdGroup>(StringTable.RemarketingTargetingSetting,
                c => c.AdGroup.RemarketingTargetingSetting.ToBulkString(),
                (v, c) => c.AdGroup.RemarketingTargetingSetting = v.ParseOptional<RemarketingTargetingSetting>()
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.TargetSetting,
                c =>
                {
                    var targetSetting = (TargetSetting)c.GetSetting(typeof(TargetSetting));

                    return targetSetting?.ToBulkString();
                },
                (v, c) =>
                {
                    var details = v.ParseTargetSettingDetails();
                    var targetSetting = (TargetSetting)c.GetSetting(typeof(TargetSetting));
                    if (details != null && targetSetting != null)
                    {
                        targetSetting.Details = details;
                    }
                }
            ),
            new SimpleBulkMapping<BulkAdGroup>(StringTable.BidOption,
                c =>
                {
                    var setting = (CoOpSetting)c.GetSetting(typeof(CoOpSetting));
                    return setting?.BidOption.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (CoOpSetting)c.GetSetting(typeof(CoOpSetting));
                    var bidOption = v.ParseOptional<BidOption>();
                    if(bidOption != null && setting != null)
                    {
                        setting.BidOption = bidOption;
                    }
                }
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.BidBoostValue,
                c =>
                {
                    var setting = (CoOpSetting)c.GetSetting(typeof(CoOpSetting));
                    return setting?.BidBoostValue.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (CoOpSetting)c.GetSetting(typeof(CoOpSetting));
                    var bidBoostValue = v.ParseOptional<double>();
                    if(bidBoostValue != null && setting != null)
                    {
                        setting.BidBoostValue = bidBoostValue;
                    }
                }
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.MaximumBid,
                c =>
                {
                    var setting = (CoOpSetting)c.GetSetting(typeof(CoOpSetting));
                    return setting?.BidMaxValue.ToBulkString();
                },
                (v, c) =>
                {
                    var setting = (CoOpSetting)c.GetSetting(typeof(CoOpSetting));
                    var bidMaxValue = v.ParseOptional<double>();
                    if(bidMaxValue != null && setting != null)
                    {
                        setting.BidMaxValue = bidMaxValue;
                    }
                }
            ),

            new ComplexBulkMapping<BulkAdGroup>(BiddingSchemeToCsv, CsvToBiddingScheme),
            
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AdGroup = new AdGroup {AdDistribution = 0};

            values.ConvertToEntity(this, Mappings);

            QualityScoreData = QualityScoreData.ReadFromRowValuesOrNull(values);

            PerformanceData = PerformanceData.ReadFromRowValuesOrNull(values);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(AdGroup, "AdGroup");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                QualityScoreData.WriteToRowValuesIfNotNull(QualityScoreData, values);

                PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
            }
        }
        
        private static void CsvToBiddingScheme(RowValues values, BulkAdGroup c)
        {
            string bidStrategyTypeRowValue;

            BiddingScheme biddingScheme;

            if (!values.TryGetValue(StringTable.BidStrategyType, out bidStrategyTypeRowValue) || (biddingScheme = bidStrategyTypeRowValue.ParseBiddingScheme()) == null)
            {
                return;
            }

            string inheritedBidStrategyTypeRowValue;

            values.TryGetValue(StringTable.InheritedBidStrategyType, out inheritedBidStrategyTypeRowValue);
            
            var inheritFromParentBiddingScheme = biddingScheme as InheritFromParentBiddingScheme;
            if (inheritFromParentBiddingScheme != null)
            {
                c.AdGroup.BiddingScheme = new InheritFromParentBiddingScheme
                {
                    InheritedBidStrategyType = inheritedBidStrategyTypeRowValue,
                    Type = "InheritFromParent",
                };
            }
            else
            {
                c.AdGroup.BiddingScheme = biddingScheme;
            }
        }

        private static void BiddingSchemeToCsv(BulkAdGroup c, RowValues values)
        {
            var biddingScheme = c.AdGroup.BiddingScheme;

            if (biddingScheme == null)
            {
                return;
            }

            values[StringTable.BidStrategyType] = biddingScheme.ToBiddingSchemeBulkString();

            var inheritFromParentBiddingScheme = biddingScheme as InheritFromParentBiddingScheme;
            if (inheritFromParentBiddingScheme != null)
            {
                values[StringTable.InheritedBidStrategyType] = inheritFromParentBiddingScheme.InheritedBidStrategyType;
            }
        }
    }
}