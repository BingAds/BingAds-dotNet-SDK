using System.Threading.Tasks;
using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk.Operations
{
    internal class UploadStatusProvider : IBulkOperationStatusProvider<UploadStatus>
    {
        private readonly string _requestId;

        public UploadStatusProvider(string requestId)
        {
            _requestId = requestId;
        }

        public async Task<BulkOperationStatus<UploadStatus>> GetCurrentStatus(ServiceClient<IBulkService> bulkServiceClient)
        {
            var request = new GetDetailedBulkUploadStatusRequest
            {
                RequestId = _requestId,
            };
            
            var response = await bulkServiceClient.CallAsync((s, r) => s.GetDetailedBulkUploadStatusAsync(r), request).ConfigureAwait(false);

            return new BulkOperationStatus<UploadStatus>
            {
                TrackingId = response.TrackingId,
                Status = response.RequestStatus.Parse<UploadStatus>(),
                ResultFileUrl = response.ResultFileUrl,
                PercentComplete = response.PercentComplete,
                Errors = response.Errors
            };
        }

        public bool IsFinalStatus(BulkOperationStatus<UploadStatus> status)
        {
            return
                status.Status == UploadStatus.Completed ||
                status.Status == UploadStatus.CompletedWithErrors ||
                status.Status == UploadStatus.Failed ||
                status.Status == UploadStatus.Expired ||
                status.Status == UploadStatus.Aborted;
        }

        public bool IsSuccessStatus(UploadStatus status)
        {
            return status == UploadStatus.Completed || status == UploadStatus.CompletedWithErrors;
        }
    }
}
