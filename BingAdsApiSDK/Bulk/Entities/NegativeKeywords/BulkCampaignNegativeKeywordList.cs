using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a negative keyword list that is assigned to a campaign. Each negative keyword list can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkCampaignNegativeKeywordList.SharedEntityAssociation"/> property that can be read and written as fields of the Campaign Negative Keyword List Association record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511523">Campaign Negative Keyword List Association</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignNegativeKeywordList : SingleRecordBulkEntity
    {
        /// <summary>
        /// The campaign and negative keyword list identifiers. 
        /// </summary>
        public SharedEntityAssociation SharedEntityAssociation { get; set; }

        /// <summary>
        /// The status of the negative keyword list association.
        /// The value is Active if the negative keyword list is assoicated to the campaign. 
        /// The value is Deleted if the negative keyword list is disassociated from the campaign, or should be disassociated in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        private static readonly IBulkMapping<BulkCampaignNegativeKeywordList>[] Mappings =
        {
            new SimpleBulkMapping<BulkCampaignNegativeKeywordList>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkCampaignNegativeKeywordList>(StringTable.Id,
                c => c.SharedEntityAssociation.SharedEntityId.ToBulkString(),
                (v, c) => c.SharedEntityAssociation.SharedEntityId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkCampaignNegativeKeywordList>(StringTable.ParentId,
                c => c.SharedEntityAssociation.EntityId.ToBulkString(),
                (v, c) => c.SharedEntityAssociation.EntityId = v.Parse<long>()
            )
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {            
            ValidatePropertyNotNull(SharedEntityAssociation, "SharedEntityAssociation");

            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            SharedEntityAssociation = new SharedEntityAssociation
            {
                EntityType = "Campaign",
                SharedEntityType = "NegativeKeywordList"
            };

            values.ConvertToEntity(this, Mappings);
        }
    }
}
