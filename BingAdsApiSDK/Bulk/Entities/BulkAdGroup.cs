using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group. 
    /// This class exposes the <see cref="BulkAdGroup.AdGroup"/> property that can be read and written as fields of the Ad Group record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511537">Ad Group</see>. </para>
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
        /// in the Ad Group record. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511537">Ad Group</see>.
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
                        c.AdGroup.Status = AdGroupStatus.Deleted;
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

            new SimpleBulkMapping<BulkAdGroup>(StringTable.SearchBroadBid,
                c => c.AdGroup.BroadMatchBid.ToAdGroupBidBulkString(),
                (v, c) => c.AdGroup.BroadMatchBid = v.ParseAdGroupBid()
            ), 

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ContentBid,
                c => c.AdGroup.ContentMatchBid.ToAdGroupBidBulkString(),
                (v, c) => c.AdGroup.ContentMatchBid = v.ParseAdGroupBid()
            ), 

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Language,
                c => c.AdGroup.Language,
                (v, c) => c.AdGroup.Language = v  
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.BidAdjustment,
                c => c.AdGroup.NativeBidAdjustment.ToBulkString(),
                (v, c) => c.AdGroup.NativeBidAdjustment = v.ParseOptional<int>()
            ), 
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AdGroup = new AdGroup { AdDistribution = 0 };

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
    }
}
