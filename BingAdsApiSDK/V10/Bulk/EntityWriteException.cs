using System;
using System.Runtime.Serialization;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// This exception is thrown when attempting to write entities to a bulk file using <see cref="BulkFileWriter.WriteEntity(Microsoft.BingAds.V10.Bulk.Entities.BulkEntity)"/>.
    /// </summary>
    [Serializable]
    public class EntityWriteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the EntityWriteException class.
        /// </summary>
        public EntityWriteException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityWriteException class with the specified message and column values.
        /// </summary>
        /// <param name="message">The error message.</param>
        public EntityWriteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityReadException class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The details of the exception from the bulk service operation.</param>
        public EntityWriteException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="info">Reserved for internal use.</param>
        /// <param name="context">Reserved for internal use.</param>
        protected EntityWriteException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
