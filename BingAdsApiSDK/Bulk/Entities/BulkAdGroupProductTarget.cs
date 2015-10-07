using System.Collections.Generic;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group product target. 
    /// This class exposes the <see cref="BulkAdGroupProductTarget.BiddableAdGroupCriterion"/> property that can be read and written as fields of the Ad Group Product Target record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511550">Ad Group Product Target</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupProductTarget : SingleRecordBulkEntity
    {
        /// <summary>
        /// The BiddableAdGroupCriterion Data Object of the Campaign Management Service. A subset of BiddableAdGroupCriterion properties are available 
        /// in the Ad Group Product Target record. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511550">Ad Group Product Target</see>.
        /// </summary>
        public BiddableAdGroupCriterion BiddableAdGroupCriterion { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group product target.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the ad group that contains the ad group product target.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        private static readonly IBulkMapping<BulkAdGroupProductTarget>[] Mappings =
        {           
            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Status,
                c => c.BiddableAdGroupCriterion.Status.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Status = v.ParseOptional<AdGroupCriterionStatus>()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Id,
                c => c.BiddableAdGroupCriterion.Id.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Id = v.ParseOptional<long>()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.ParentId,
                c => c.BiddableAdGroupCriterion.AdGroupId.ToBulkString(returnNullForDefaultValue: true),
                (v, c) => c.BiddableAdGroupCriterion.AdGroupId = v.Parse<long>(returnDefaultValueOnNullOrEmpty: true)
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.EditorialStatus,
                c => c.BiddableAdGroupCriterion.EditorialStatus.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.EditorialStatus = v.ParseOptional<AdGroupCriterionEditorialStatus>()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Bid,
                c => ((FixedBid)c.BiddableAdGroupCriterion.CriterionBid).Bid.ToAdGroupBidBulkString(),
                (v, c) => ((FixedBid)c.BiddableAdGroupCriterion.CriterionBid).Bid = v.ParseAdGroupBid()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.DestinationUrl,
                c => c.BiddableAdGroupCriterion.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.DestinationUrl = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Param1,
                c => c.BiddableAdGroupCriterion.Param1.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Param1 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Param2,
                c => c.BiddableAdGroupCriterion.Param2.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Param2 = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkAdGroupProductTarget>(StringTable.Param3,
                c => c.BiddableAdGroupCriterion.Param3.ToOptionalBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Param3 = v.GetValueOrEmptyString()
            ), 

            new ComplexBulkMapping<BulkAdGroupProductTarget>(
                ConditionsToRowValues,
                RowValuesToConditions
            )
        };

        private static void RowValuesToConditions(RowValues values, BulkAdGroupProductTarget c)
        {
            var product = (Product)c.BiddableAdGroupCriterion.Criterion;
            
            product.Conditions = new List<ProductCondition>();

            ProductConditionHelper.AddConditionsFromRowValues(values, product.Conditions);
        }

        private static void ConditionsToRowValues(BulkAdGroupProductTarget c, RowValues values)
        {
            var product = (Product)c.BiddableAdGroupCriterion.Criterion;

            if (product.Conditions == null)
            {
                return;
            }

            ProductConditionHelper.AddRowValuesFromConditions(product.Conditions, values);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            BiddableAdGroupCriterion = new BiddableAdGroupCriterion
            {
                CriterionBid = new FixedBid
                {
                    Type = "FixedBid"
                },
                Criterion = new Product
                {
                    Type = "Product"
                },
                Type = "BiddableAdGroupCriterion"
            };

            values.ConvertToEntity(this, Mappings);
        }
    }
}
