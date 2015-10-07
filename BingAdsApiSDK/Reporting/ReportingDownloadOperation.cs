using System;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Utilities;

namespace Microsoft.BingAds.Reporting
{
    /// <summary>
    /// This class represent a reporting operation requested by a user. 
    /// You can use this class to poll for the operation status, and then download the results file when available.
    /// </summary>
    public class ReportingDownloadOperation : IDisposable
    {
        private ServiceClient<IReportingService> _reportingServiceClient;

        private readonly ReportingStatusProvider _statusProvider;

        internal IHttpService HttpService { get; set; }

        internal IZipExtractor ZipExtractor { get; set; }

        internal IFileSystem FileSystem { get; set; }

        /// <summary>
        /// Represents a user who intends to access the corresponding customer and account. 
        /// </summary>
        public AuthorizationData AuthorizationData { get; private set; }

        /// <summary>
        /// The request identifier corresponding to the reporting download.
        /// </summary>
        public string RequestId { get; private set; }

        /// <summary>
        /// The identifier of the log entry that contains the details of the download request.  
        /// </summary>
        public string TrackingId { get; private set; }

        /// <summary>
        /// Gets the final status of the reporting operation or null if the operation is still running.
        /// </summary>
        public ReportingOperationStatus FinalStatus { get; private set; }

        /// <summary>
        /// The amount of time in milliseconds between two status polling attempts. 
        /// </summary>
        public int StatusPollIntervalInMilliseconds { get; set; }

        /// <summary>
        /// Initializes a new instance of this class with the specified <paramref name="requestId"/> and <see cref="BingAds.AuthorizationData"/>.
        /// </summary>
        /// <param name="requestId">The identifier of a download request that has previously been submitted.</param>
        /// <param name="authorizationData">
        /// Represents a user who intends to access the corresponding customer and account. 
        /// </param>
        public ReportingDownloadOperation(string requestId, AuthorizationData authorizationData) : this(requestId, authorizationData, null)
        {
        }

        internal ReportingDownloadOperation(string requestId, AuthorizationData authorizationData, string trackingId)
            : this(requestId, authorizationData, new ReportingStatusProvider(requestId), trackingId)
        {
        }

        internal ReportingDownloadOperation(string requestId, AuthorizationData authorizationData, ReportingStatusProvider statusProvider, string trackingId)
        {
            RequestId = requestId;

            AuthorizationData = authorizationData;

            _statusProvider = statusProvider;

            TrackingId = trackingId;

            StatusPollIntervalInMilliseconds = ReportingServiceManager.DefaultStatusPollIntervalInMilliseconds;

            _reportingServiceClient = new ServiceClient<IReportingService>(authorizationData);

            ZipExtractor = new ZipExtractor();

            HttpService = new HttpService();

            FileSystem = new FileSystem();
        }

        /// <summary>
        /// Runs asynchonously until the reporting service has finished processing the download request.
        /// </summary>
        /// <returns>The <see cref="ReportingOperationStatus"/> of this method is available when the Task completes.</returns>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception> 
        public Task<ReportingOperationStatus> TrackAsync()
        {
            return TrackAsync(CancellationToken.None);
        }

        /// <summary>
        /// Runs asynchonously until the reporting service has finished processing the download request.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The <see cref="ReportingOperationStatus"/> of this method is available when the Task completes.</returns>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception> 
        public async Task<ReportingOperationStatus> TrackAsync(CancellationToken cancellationToken)
        {
            if (FinalStatus == null)
            {
                var tracker = new ReportingOperationTracker(_statusProvider, _reportingServiceClient, cancellationToken, StatusPollIntervalInMilliseconds);

                FinalStatus = await tracker.TrackResultFileAsync().ConfigureAwait(false);
            }

            return FinalStatus;
        }

        /// <summary>
        /// Gets the status of the reporting operation.
        /// </summary>
        /// <returns><see cref="ReportingOperationStatus"/></returns>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception>  
        public async Task<ReportingOperationStatus> GetStatusAsync()
        {
            if (FinalStatus != null)
            {
                return FinalStatus;
            }

            var currentStatus = await _statusProvider.GetCurrentStatus(_reportingServiceClient).ConfigureAwait(false);

            if (_statusProvider.IsFinalStatus(currentStatus))
            {
                FinalStatus = currentStatus;
            }

            return currentStatus;
        }

        /// <summary>
        /// Downloads the result file of the reporting operation to the specified local path.
        /// </summary>
        /// <param name="localResultDirectoryName">The download result local directory name.</param>
        /// <param name="localResultFileName">The download result local file name.</param>
        /// <param name="decompress">Determines whether to decompress the ZIP file. 
        /// If set to true, the file will be decompressed after download.
        /// The default value is false, in which case the downloaded file is not decompressed.</param>        
        /// <returns></returns>
        /// <exception cref="ReportingOperationInProgressException">Thrown if the reporting operation is still in progress.</exception>
        /// <exception cref="ReportingOperationCouldNotBeCompletedException">Thrown if the reporting operation has failed </exception>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception> 
        public Task<string> DownloadResultFileAsync(string localResultDirectoryName, string localResultFileName, bool decompress)
        {
            return DownloadResultFileAsync(localResultDirectoryName, localResultFileName, decompress, false);
        }


        /// <summary>
        /// Downloads the result file of the reporting operation to the specified local path.
        /// </summary>
        /// <param name="localResultDirectoryName">The download result local directory name.</param>
        /// <param name="localResultFileName">The download result local file name.</param>
        /// <param name="decompress">Determines whether to decompress the ZIP file. 
        /// If set to true, the file will be decompressed after download.
        /// The default value is false, in which case the downloaded file is not decompressed.</param>        
        /// <param name="overwrite">Indicates whether the result file should overwrite the existing file if any.</param>
        /// <returns></returns>
        /// <exception cref="ReportingOperationInProgressException">Thrown if the reporting operation is still in progress.</exception>
        /// <exception cref="ReportingOperationCouldNotBeCompletedException">Thrown if the reporting operation has failed </exception>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception> 
        public Task<string> DownloadResultFileAsync(string localResultDirectoryName, string localResultFileName, bool decompress, bool overwrite)
        {
            if (localResultDirectoryName == null)
            {
                throw new ArgumentNullException("localResultDirectoryName");
            }

            if (localResultFileName != null && Path.GetExtension(localResultFileName) == ".zip" && decompress)
            {
                throw new InvalidOperationException(
                    "Result file can't be decompressed into a file with extension 'zip'. Please change the extesion of the localResultFileName or pass decompress = false");
            }

            return DownloadResultFileAsyncImpl(localResultDirectoryName, localResultFileName, decompress, overwrite);
        }

        private async Task<string> DownloadResultFileAsyncImpl(string localResultDirectoryName, string localResultFileName, bool decompress, bool overwrite)
        {
            if (FinalStatus == null)
            {
                await GetStatusAsync().ConfigureAwait(false);

                if (FinalStatus == null)
                {
                    throw new ReportingOperationInProgressException();
                }
            }

            if (!_statusProvider.IsSuccessStatus(FinalStatus))
            {
                throw new ReportingOperationCouldNotBeCompletedException();
            }

            var effectiveFileName = localResultFileName ?? RequestId;

            var fullPath = Path.Combine(localResultDirectoryName, effectiveFileName);

            var zipResultFilePath = Path.ChangeExtension(fullPath, "zip");

            await DownloadResultFileZipAsync(FinalStatus.ResultFileUrl, zipResultFilePath, overwrite).ConfigureAwait(false);

            if (!decompress)
            {
                return zipResultFilePath;
            }

            var extractedFile = ZipExtractor.ExtractFirstEntryToFile(zipResultFilePath, fullPath, localResultFileName == null, overwrite);

            FileSystem.DeleteFile(zipResultFilePath);

            return extractedFile;
        }

        private Task DownloadResultFileZipAsync(string url, string tempZipFileName, bool overwrite)
        {
            return HttpService.DownloadFileAsync(new Uri(url), tempZipFileName, overwrite);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Disposes of the stream reader if set to true.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_reportingServiceClient != null)
                {
                    _reportingServiceClient.Dispose();

                    _reportingServiceClient = null;
                }
            }
        }
    }
}