using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.BingAds.V10.Bulk
{    
    /// <summary>
    /// This exception is thrown if you are attempting to poll for a completed results file and the bulk service returns a failed status. 
    /// </summary>
    /// <typeparam name="TStatus">Possible values include <see cref="DownloadStatus"/> and <see cref="UploadStatus"/>.</typeparam>
    [Serializable]
    public class BulkOperationCouldNotBeCompletedException<TStatus> : Exception
    {
        /// <summary>
        /// The list of operation errors returned by the bulk service.
        /// </summary>
        public IList<OperationError> Errors { get; private set; }

        /// <summary>
        /// <para>The request status of the bulk operation.</para>
        /// <para>For bulk download, this value corresponds to the <see cref="GetBulkDownloadStatusResponse.RequestStatus"/> property.</para>
        /// <para>For bulk upload, this value corresponds to the <see cref="GetBulkUploadStatusResponse.RequestStatus"/> property.</para>
        /// </summary>
        public TStatus Status { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BulkOperationCouldNotBeCompletedException class with the specified errors and status. 
        /// </summary>
        /// <param name="errors">The list of operation errors.</param>
        /// <param name="status">The request status of the bulk operation.</param>
        public BulkOperationCouldNotBeCompletedException(IList<OperationError> errors, TStatus status)
        {
            Errors = errors;
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the BulkOperationCouldNotBeCompletedException class with the specified errors, status, and message. 
        /// </summary>
        /// <param name="errors">The list of operation errors.</param>
        /// <param name="status">The request status of the bulk operation.</param>
        /// <param name="message">The error message.</param>
        public BulkOperationCouldNotBeCompletedException(IList<OperationError> errors, TStatus status, string message)
            : base(message)
        {
            Errors = errors;
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the BulkOperationCouldNotBeCompletedException class with the specified errors, status, 
        /// message, and inner exception. 
        /// </summary>
        /// <param name="errors">The list of operation errors.</param>
        /// <param name="status">The request status of the bulk operation.</param>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The details of the exception from the bulk service operation.</param>
        public BulkOperationCouldNotBeCompletedException(IList<OperationError> errors, TStatus status, string message, Exception inner)
            : base(message, inner)
        {
            Errors = errors;
            Status = status;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="info">Reserved for internal use.</param>
        /// <param name="context">Reserved for internal use.</param>
        protected BulkOperationCouldNotBeCompletedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Errors = (IList<OperationError>)info.GetValue("Errors", typeof(IList<OperationError>));
            Status = (TStatus) info.GetValue("Status", typeof (TStatus));
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Errors", Errors);
            info.AddValue("Status", Status);
        }
    }
}
