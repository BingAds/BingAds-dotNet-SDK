using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.Bulk.Entities;

namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// Bulk entity that has its data in multiple records within the bulk file.
    /// For example, <see cref="BulkSiteLinkAdExtension"/> is a multi record bulk entity which can contain one or more 
    /// <see cref="BulkSiteLink"/> child entities, which are themselves derived from <see cref="SingleRecordBulkEntity"/>.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
    /// </summary>
    public abstract class MultiRecordBulkEntity : BulkEntity
    {
        /// <summary>
        /// The child entities that this multi record entity contains. 
        /// </summary>
        internal abstract IReadOnlyList<BulkEntity> ChildEntities { get; }
        
        /// <summary>
        /// True, if the object is fully constructed (contains all of its children), determined by the presence of delete all row, false otherwise
        /// </summary>
        internal abstract bool AllChildrenArePresent { get; }

        /// <summary>
        /// Indicates whether or not the Errors property of any of the ChildEntities is null or empty. 
        /// If true, one or more ChildEntities contains the details of one or more <see cref="BulkError"/> objects. 
        /// </summary>
        public override bool HasErrors
        {
            get { return ChildEntities.Any(c => c.HasErrors); }
        }

        /// <summary>
        /// Gets the last modified time for the first child entity, or null if there are no ChildEntities.
        /// </summary>
        public override DateTime? LastModifiedTime
        {
            get
            {
                return ChildEntities.Count > 0 ? ChildEntities[0].LastModifiedTime : null;
            }
        }
    }
}