using System;
using System.Collections.Generic;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// The abstract base class for all bulk entities that can be read or written in a bulk file. 
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>. 
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public abstract class BulkEntity : BulkObject
    {              
        /// <summary>
        /// Determines whether the bulk entity has associated errors.
        /// </summary>
        public abstract bool HasErrors { get; }

        /// <summary>
        /// Gets the last modified time for the entity.
        /// </summary>
        public abstract DateTime? LastModifiedTime { get; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected void ValidatePropertyNotNull(object propertyValue, string propertyName)
        {
            if (propertyValue == null)
            {
                throw new InvalidOperationException(ErrorMessages.GetPropertyMustNotBeNullMessage(GetType().Name, propertyName));
            }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected void ValidateListNotNullOrEmpty<T>(IList<T> listValue, string propertyName)
        {
            ValidatePropertyNotNull(listValue, propertyName);

            if (listValue.Count == 0)
            {
                throw new InvalidOperationException(ErrorMessages.GetListMustNotBeEmptyMessage(GetType().Name, propertyName));
            }
        }
    }
}
