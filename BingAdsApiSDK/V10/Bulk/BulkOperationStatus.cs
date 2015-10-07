using System.Collections.Generic;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Contains tracking details about the results and status of the corresponding 
    /// <see cref="BulkDownloadOperation"/> or <see cref="BulkUploadOperation"/>.
    /// </summary>
    /// <typeparam name="TStatus">For bulk download the status type is <see cref="DownloadStatus"/>, or 
    /// for bulk upload the status type is <see cref="UploadStatus"/>.</typeparam>
    public class BulkOperationStatus<TStatus>
    {
        /// <summary>
        /// The identifier of the log entry that contains the details of the upload or download request.  
        /// </summary>
        public string TrackingId { get; set; }

        /// <summary>
        /// For bulk download the status type is <see cref="DownloadStatus"/>, or 
        /// for bulk upload the status type is <see cref="UploadStatus"/>.
        /// </summary>
        public TStatus Status { get; set; }

        /// <summary>
        /// Percent complete progress information for the bulk operation.
        /// </summary>
        public int PercentComplete { get; set; }

        /// <summary>
        /// The download or upload result file Url.
        /// </summary>
        public string ResultFileUrl { get; set; }

        /// <summary>
        /// The list of errors associated with the operation.
        /// </summary>
        public IList<OperationError> Errors { get; set; }
    }
}
