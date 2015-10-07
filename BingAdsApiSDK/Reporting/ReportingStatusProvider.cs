using System.Threading.Tasks;

namespace Microsoft.BingAds.Reporting
{
    internal class ReportingStatusProvider
    {
        private readonly string _requestId;

        public ReportingStatusProvider(string requestId)
        {
            _requestId = requestId;
        }

        public async Task<ReportingOperationStatus> GetCurrentStatus(ServiceClient<IReportingService> reportingServiceClient)
        {
            var request = new PollGenerateReportRequest {ReportRequestId = _requestId,};

            var response = await reportingServiceClient.CallAsync((s, r) => s.PollGenerateReportAsync(r), request).ConfigureAwait(false);

            return new ReportingOperationStatus
                   {
                       TrackingId = response.TrackingId,
                       Status = response.ReportRequestStatus.Status,
                       ResultFileUrl = response.ReportRequestStatus.ReportDownloadUrl,
                   };
        }

        public bool IsFinalStatus(ReportingOperationStatus status)
        {
            return status.Status == ReportRequestStatusType.Error || status.Status == ReportRequestStatusType.Success;
        }

        public bool IsSuccessStatus(ReportingOperationStatus status)
        {
            return status.Status == ReportRequestStatusType.Success;
        }
    }
}