using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.Internal;
using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a campaign that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkCampaign.Campaign"/> property that can be read and written as fields of the Campaign record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620239">Campaign</see>. </para>
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
            else
            {
                c.Campaign.MonthlyBudget = budgetValue;
            }
        }

        private static void BudgetToCsv(BulkCampaign c, RowValues values)
        {
            var budgetType = c.Campaign.BudgetType;

            if (budgetType == null)
            {
                return;
            }

            values[StringTable.Budget] =
                budgetType == BudgetLimitType.DailyBudgetAccelerated || budgetType == BudgetLimitType.DailyBudgetStandard ?
                    c.Campaign.DailyBudget.ToBulkString() :
                    c.Campaign.MonthlyBudget.ToBulkString();
        }
    }
}
