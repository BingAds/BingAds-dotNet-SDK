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
    /// Represents a negative keyword list that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkNegativeKeywordList.NegativeKeywordList"/> property that can be read and written as fields of the Negative Keyword List record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511519">Negative Keyword List</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkNegativeKeywordList : SingleRecordBulkEntity
    {
        /// <summary>
        /// The negative keyword list.
        /// </summary>
        public NegativeKeywordList NegativeKeywordList { get; set; }

        /// <summary>
        /// The status of the negative keyword list.
        /// The value is Active if the negative keyword list is available in the account's shared library. 
        /// The value is Deleted if the negative keyword list is deleted from the library, or should be deleted in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        private static readonly IBulkMapping<BulkNegativeKeywordList>[] Mappings =
        {
            new SimpleBulkMapping<BulkNegativeKeywordList>(StringTable.Id,
                c => c.NegativeKeywordList.Id.ToBulkString(),
                (v, c) => c.NegativeKeywordList.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkNegativeKeywordList>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ), 

            new SimpleBulkMapping<BulkNegativeKeywordList>(StringTable.Name,
                c => c.NegativeKeywordList.Name,
                (v, c) => c.NegativeKeywordList.Name = v
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            NegativeKeywordList = new NegativeKeywordList { Type = "NegativeKeywordList" };

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(NegativeKeywordList, "NegativeKeywordList");

            this.ConvertToValues(values, Mappings);
        }
    }
}
