using System;
using Microsoft.BingAds.V10.Bulk.Entities;

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public abstract class BulkEntityIdentifier : BulkObject, IEquatable<BulkEntityIdentifier>
    {
        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="other">Reserved for internal use.</param>
        /// <returns>Reserved for internal use.</returns>
        public abstract bool Equals(BulkEntityIdentifier other);

        internal abstract bool IsDeleteRow { get; }

        internal abstract MultiRecordBulkEntity CreateEntityWithThisIdentifier();

        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            rowWriter.WriteObjectRow(this, excludeReadonlyData);
        }

        internal override void ReadRelatedDataFromStream(IBulkStreamReader reader)
        {
            // If this is a delete all row, just skip any error rows after this delete row
            if (IsDeleteRow)
            {
                var hasMoreErrors = true;

                while (hasMoreErrors)
                {
                    BulkError error;

                    hasMoreErrors = reader.TryRead(out error);
                }
            }
        }

        internal sealed override bool CanEncloseInMultilineEntity
        {
            get { return true; }
        }

        internal override MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            return CreateEntityWithThisIdentifier();
        }
    }
}