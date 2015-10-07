using System;
using Microsoft.BingAds.V10.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public class BulkAdGroupTargetIdentifier : BulkTargetIdentifier
    {
        /// <summary>
        /// Initializes a new instanced of the BulkAdGroupTargetIdentifier class.
        /// </summary>
        public BulkAdGroupTargetIdentifier(Type targetBidType)
            : base(targetBidType)
        {

        }

        internal override MultiRecordBulkEntity CreateEntityWithThisIdentifier()
        {
            return new BulkAdGroupTarget(this);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override string EntityColumnName
        {
            get { return StringTable.AdGroup; }
        }
    }
}
