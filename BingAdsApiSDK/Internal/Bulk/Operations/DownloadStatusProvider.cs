using System.Threading.Tasks;
using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk.Operations
{
    internal class DownloadStatusProvider : IBulkOperationStatusProvider<DownloadStatus>
    {
        private readonly string _requestId;        

        public DownloadStatusProvider(string requestId)
        {
            _requestId = requestId;            
        }

        public async Task<BulkOperationStatus<DownloadStatus>> GetCurrentStatus(ServiceClient<IBulkService> bulkServiceClient)
        {
            var request = new GetDetailedBulkDownloadStatusRequest
            {
                RequestId = _requestId,
            };

            var response = await bulkServiceClient.CallAsync((s, r) => s.GetDetailedBulkDownloadStatusAsync(request), request).ConfigureAwait(false);            

            return new BulkOperationStatus<DownloadStatus>
            {
                TrackingId = response.TrackingId, 
                Status = response.RequestStatus.Parse<DownloadStatus>(), 
                ResultFileUrl = response.ResultFileUrl, 
                PercentComplete = response.PercentComplete,
                Errors = response.Errors
            };
        }

        public bool IsFinalStatus(BulkOperationStatus<DownloadStatus> status)
        {
            return 
                status.Status == DownloadStatus.Completed || 
                status.Status == DownloadStatus.Failed ||
                status.Status == DownloadStatus.FailedFullSyncRequired;
        }

        public bool IsSuccessStatus(DownloadStatus status)
        {
            return status == DownloadStatus.Completed;
        }
    }
}
