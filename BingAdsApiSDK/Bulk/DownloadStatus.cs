
namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Defines the possible status values of the bulk download.
    /// </summary>
    /// <seealso cref="BulkOperation{TStatus}.GetStatusAsync"/>
    /// <seealso cref="BulkOperation{TStatus}.TrackAsync()"/>
    public enum DownloadStatus
    {
        /// <summary>
        /// The download completed successfully.
        /// </summary>
        Completed,
        /// <summary>
        /// The download is in progress.
        /// </summary>
        InProgress,
        /// <summary>
        /// The download failed. You may submit a new download with fewer entities, without performance data, or try again to submit the same download later.
        /// </summary>
        Failed,
        /// <summary>
        /// The last sync time must be null. 
        /// The request's LastSyncTimeInUTC element must be set to null if the specified account was included in a data migration, for example the URL by match type migration. After requesting a full download, you may begin requesting delta downloads again.
        /// </summary>
        FailedFullSyncRequired
    }
}
