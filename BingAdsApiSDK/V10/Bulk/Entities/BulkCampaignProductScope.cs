using System.Collections.Generic;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a Campaign Criterion that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkCampaignProductScope.CampaignCriterion"/> property that can be read and written as fields of the Campaign Criterion record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkId=618643">Campaign Product Scope</see> </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignProductScope : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines an Campaign Criterion.
        /// </summary>
        public CampaignCriterion CampaignCriterion { get; set; }

        /// <summary>
        /// Defines the status of the Campaign Criterion.
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        private static readonly IBulkMapping<BulkCampaignProductScope>[] Mappings =
        {
            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.Id,
                c => c.CampaignCriterion.Id.ToBulkString(),
                (v, c) => c.CampaignCriterion.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.ParentId,
                c => c.CampaignCriterion.CampaignId.ToBulkString(true),
                (v, c) => c.CampaignCriterion.CampaignId = v.Parse<long>()
            ),
            new SimpleBulkMapping<BulkCampaignProductScope>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new ComplexBulkMapping<BulkCampaignProductScope>(
                ConditionsToRowValues,
                RowValuesToConditions
            )
        };

        private static void RowValuesToConditions(RowValues values, BulkCampaignProductScope c)
        {
            var product = (ProductScope)c.CampaignCriterion.Criterion;

            product.Conditions = new List<ProductCondition>();

            ProductConditionHelper.AddConditionsFromRowValues(values, product.Conditions);
        }

        private static void ConditionsToRowValues(BulkCampaignProductScope c, RowValues values)
        {
            if (c.CampaignCriterion.Criterion == null)
            {
                return;
            }

            var product = (ProductScope)c.CampaignCriterion.Criterion;

            if (product.Conditions == null)
            {
                return;
            }

            ProductConditionHelper.AddRowValuesFromConditions(product.Conditions, values);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(CampaignCriterion, typeof(CampaignCriterion).Name);

            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            CampaignCriterion = new CampaignCriterion
            {
                Criterion = new ProductScope
                {
                    Type = typeof(ProductScope).Name,
                },
                Type = typeof(CampaignCriterion).Name,
            };

            values.ConvertToEntity(this, Mappings);
        }
    }
}