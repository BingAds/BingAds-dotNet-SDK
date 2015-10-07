using System;
using System.Runtime.Serialization;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// This exception is thrown if you are attempting to download a results file that is not yet available for download. 
    /// </summary>
    [Serializable]
    public class BulkOperationInProgressException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BulkOperationInProgressException class. 
        /// </summary>
        public BulkOperationInProgressException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BulkOperationInProgressException class with the specified message. 
        /// </summary>
        /// <param name="message">The error message.</param>
        public BulkOperationInProgressException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BulkOperationInProgressException class with the specified message and inner exception. 
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The details of the exception from the bulk service operation.</param>
        public BulkOperationInProgressException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="info">Reserved for internal use.</param>
        /// <param name="context">Reserved for internal use.</param>
        protected BulkOperationInProgressException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
