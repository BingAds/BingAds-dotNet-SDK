using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class for all bulk negative keywords that are either assigned individually to a campaign or ad group entity, 
    /// or shared in a negative keyword list.
    /// </summary>
    /// <seealso cref="BulkAdGroupNegativeKeyword"/>
    /// <seealso cref="BulkCampaignNegativeKeyword"/>
    /// <seealso cref="BulkSharedNegativeKeyword"/>
    public abstract class BulkNegativeKeyword : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines a negative keyword with match type.
        /// </summary>
        public NegativeKeyword NegativeKeyword { get; set; }

        /// <summary>
        /// The status of the negative keyword association.
        /// The value is Active if the negative keyword is assigned to the parent entity. 
        /// The value is Deleted if the negative keyword is removed from the parent entity, or should be removed in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected long? ParentId { get; set; }

        private static readonly IBulkMapping<BulkNegativeKeyword>[] Mappings =
        {
            new SimpleBulkMapping<BulkNegativeKeyword>(StringTable.Id,
                c => c.NegativeKeyword.Id.ToBulkString(),
                (v, c) => c.NegativeKeyword.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkNegativeKeyword>(StringTable.Status,                
                c => c.Status.ToBulkString(),                
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkNegativeKeyword>(StringTable.ParentId,
                c => c.ParentId.ToBulkString(),
                (v, c) => c.ParentId = v.ParseOptional<long>()
            ),
 
            new SimpleBulkMapping<BulkNegativeKeyword>(StringTable.Keyword,
                c => c.NegativeKeyword.Text,
                (v, c) => c.NegativeKeyword.Text = v
            ),

            new SimpleBulkMapping<BulkNegativeKeyword>(StringTable.MatchType,
                c => c.NegativeKeyword.MatchType.ToBulkString(),
                (v, c) => c.NegativeKeyword.MatchType = v.Parse<MatchType>()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            NegativeKeyword = new NegativeKeyword { Type = "NegativeKeyword" };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(NegativeKeyword, "NegativeKeyword");

            this.ConvertToValues(values, Mappings);
        }
    }
}
