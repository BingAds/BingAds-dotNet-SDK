
namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Defines the possible status values of the bulk upload.
    /// </summary>
    /// <seealso cref="BulkOperation{TStatus}.GetStatusAsync"/>
    /// <seealso cref="BulkOperation{TStatus}.TrackAsync()"/>
    public enum UploadStatus
    {
        /// <summary>
        /// The upload completed with one or more errors.
        /// </summary>
        Completed,
        /// <summary>
        /// The upload completed with one or more errors.
        /// While the overall upload was successful, one or more add or update errors could have occurred. 
        /// As a best practice you should request error information via the <see cref="SubmitUploadParameters.ResponseMode"/> element 
        /// when calling the upload service and check the result file for any errors.
        /// </summary>
        CompletedWithErrors,
        /// <summary>
        /// The entire upload failed due to an unexpected error. You may submit a new upload with fewer entities or try again to submit the same upload later. 
        /// </summary>
        Failed,
        /// <summary>
        /// The upload request has been received and is in the queue for processing.
        /// </summary>
        FileUploaded,   
        /// <summary>
        /// The upload file has been accepted and the upload is in progress.
        /// </summary>
        InProgress,
        /// <summary>
        /// The upload file has not been received for the corresponding RequestId.
        /// </summary>
        PendingFileUpload,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        Aborted,
        /// <summary>
        ///  Reserved for future use.
        /// </summary>
        Expired
    }
}
