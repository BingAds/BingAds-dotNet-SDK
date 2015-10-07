using Microsoft.BingAds.V10.Internal.Bulk.Operations;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Represents a bulk upload operation requested by a user. 
    /// You can use this class to poll for the upload status, and then download the upload results file when available.
    /// </summary>
    /// <example>The <see cref="BulkServiceManager.SubmitUploadAsync"/> method returns an instance of this class. 
    /// If for any reason you do not want to wait for the file to finish uploading, 
    /// for example if your application quits unexpectedly or you have other tasks to process, you can 
    /// use an instance of <see cref="BulkUploadOperation"/> to download the upload results file when it is available.</example>
    public class BulkUploadOperation : BulkOperation<UploadStatus>
    {
        /// <summary>
        /// Initializes a new instance of this class with the specified <paramref name="requestId"/> and <see cref="AuthorizationData"/>.
        /// </summary>
        /// <param name="requestId">The identifier of an upload request that has previously been submitted.</param>
        /// <param name="authorizationData">
        /// Represents a user who intends to access the corresponding customer and account. 
        /// </param>
        public BulkUploadOperation(string requestId, AuthorizationData authorizationData)
            : this(requestId, authorizationData, null)
        {
             
        }

        internal BulkUploadOperation(string requestId, AuthorizationData authorizationData, string trackingId)
            : base(requestId, authorizationData, new UploadStatusProvider(requestId), trackingId)
        {
            
        }
    }
}
