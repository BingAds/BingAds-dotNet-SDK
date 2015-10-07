using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a negative keyword that is assigned to a campaign. Each negative keyword can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkNegativeKeyword.NegativeKeyword"/> property that can be read and written as fields of the Campaign Negative Keyword record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620240">Campaign Negative Keyword</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignNegativeKeyword : BulkEntityNegativeKeyword
    {
        /// <summary>
        /// The identifier of the campaign that the negative keyword is assigned.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId
        {
            get { return ParentId; }
            set { ParentId = value; }
        }

        /// <summary>
        /// The name of the campaign that the negative keyword is assigned.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override string EntityColumnName
        {
            get { return StringTable.Campaign; }
        }
    }
}
