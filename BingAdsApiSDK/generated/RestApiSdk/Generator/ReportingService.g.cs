using System;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.Reporting;

namespace Microsoft.BingAds
{
    internal class ReportingService : IReportingService
    {
        private readonly RestServiceClient _restServiceClient;

        private readonly Type _serviceType;

        public ReportingService(RestServiceClient restServiceClient, Type serviceType)
        {
            _restServiceClient = restServiceClient;

            _serviceType = serviceType;
        }

        public SubmitGenerateReportResponse SubmitGenerateReport(SubmitGenerateReportRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SubmitGenerateReportResponse> SubmitGenerateReportAsync(SubmitGenerateReportRequest request)
        {
            return _restServiceClient.CallServiceAsync<SubmitGenerateReportResponse>("SubmitGenerateReport", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public PollGenerateReportResponse PollGenerateReport(PollGenerateReportRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PollGenerateReportResponse> PollGenerateReportAsync(PollGenerateReportRequest request)
        {
            return _restServiceClient.CallServiceAsync<PollGenerateReportResponse>("PollGenerateReport", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}