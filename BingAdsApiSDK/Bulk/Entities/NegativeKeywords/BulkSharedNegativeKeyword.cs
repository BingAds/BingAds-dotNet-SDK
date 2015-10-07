using Microsoft.BingAds.Internal.Bulk.Entities;
// ReSharper disable once CheckNamespace


namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a negative keyword that is shared in a negative keyword list. Each shared negative keyword can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkNegativeKeyword.NegativeKeyword"/> property that can be read and written as fields of the Shared Negative Keyword record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511520">Shared Negative Keyword</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkSharedNegativeKeyword : BulkNegativeKeyword
    {
        /// <summary>
        /// The identifier of the negative keyword list through which the negative keyword is shared.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long NegativeKeywordListId
        {
            get { return ParentId.Value; }
            set { ParentId = value; }
        }                
    }
}
