using System;
using Microsoft.BingAds.Internal.Bulk.Entities;

namespace Microsoft.BingAds.Internal.Bulk
{
    /// <summary>
    /// The abstract base class for all bulk objects that can be read and written in a file 
    /// that conforms to the Bing Ad Bulk File Schema. 
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
    /// </summary>
    public abstract class BulkObject
    {
        /// <summary>
        /// Read object data from a single row.                
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: reads entity fields.
        /// BulkError: reads error fields.
        /// BulkEntityIdentifier: reads identifier fields (Id, status etc.).
        /// </example>
        /// <param name="values"></param>
        internal virtual void ReadFromRowValues(RowValues values)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes object data to a single row.        
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: writes entity fields.
        /// BulkEntityIdentifier: writes identifier fields (Id, status etc.)
        /// </example>
        /// <param name="values"></param>
        /// <param name="excludeReadonlyData"></param>
        internal virtual void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Reads object data from consecutive rows.        
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: reads entity errors.
        /// MultilineBulkEntity: reads child entities.
        /// </example>
        /// <param name="reader"></param>
        internal virtual void ReadRelatedDataFromStream(IBulkStreamReader reader) { }

        /// <summary>
        /// Writes object data to consecutive rows.        
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: writes entity.
        /// MultilineBulkEntity: writes child entities.
        /// BulkEntityIdentifier: writes identifier information (Id, status etc.)
        /// </example>
        /// <param name="rowWriter"></param>
        /// <param name="excludeReadonlyData"></param>
        internal virtual void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns true if the entity is part of multiline entity, false otherwise
        /// </summary>
        /// <example>
        /// BulkSiteLinkAdExtension: returns true
        /// BulkCampaignTarget: returns true
        /// BulkAdGroup: returns false
        /// BulkKeyword: returns false
        /// </example>
        internal virtual bool CanEncloseInMultilineEntity
        {
            get { return false; }
        }

        /// <summary>
        /// Creates a multiline entity containing this entity
        /// </summary>
        /// <example>
        /// BulkSiteLink: returns BulkSiteLinkAdExtension containing this BulkSiteLink
        /// BulkCampaignAgeTargetBid: return BulkCampaignTarget containing this BulkCampaignAgeTargetBid
        /// </example>
        internal virtual MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            throw new NotSupportedException();
        }
    }
}