using Microsoft.BingAds.V10.Internal.Bulk.Operations;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Represents a bulk download operation requested by a user. 
    /// You can use this class to poll for the download status, and then download the file when available.
    /// </summary>
    /// <example>The <see cref="BulkServiceManager.SubmitDownloadAsync"/> method returns an instance of this class. 
    /// If for any reason you do not want to wait for the file to be prepared for download, 
    /// for example if your application quits unexpectedly or you have other tasks to process, you can 
    /// use an instance of <see cref="BulkDownloadOperation"/> to download the file when it is available.</example>
    public class BulkDownloadOperation : BulkOperation<DownloadStatus>
    {        
        /// <summary>
        /// Initializes a new instance of this class with the specified <paramref name="requestId"/> and <see cref="AuthorizationData"/>.
        /// </summary>
        /// <param name="requestId">The identifier of a download request that has previously been submitted.</param>
        /// <param name="authorizationData">
        /// Represents a user who intends to access the corresponding customer and account. 
        /// </param>
        public BulkDownloadOperation(string requestId, AuthorizationData authorizationData)
            : this(requestId, authorizationData, null)
        {
             
        }

        internal BulkDownloadOperation(string requestId, AuthorizationData authorizationData, string trackingId)
            : base(requestId, authorizationData, new DownloadStatusProvider(requestId), trackingId)
        {

        }      
    }
}