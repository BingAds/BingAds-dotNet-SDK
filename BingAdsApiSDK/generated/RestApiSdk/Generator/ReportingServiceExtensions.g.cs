using System.Threading.Tasks;
using Microsoft.BingAds.V13.Reporting;

namespace Microsoft.BingAds
{
    public static partial class ServiceClientExtensions
    {
        public static Task<SubmitGenerateReportResponse> SubmitGenerateReportAsync(this ServiceClient<IReportingService> service, SubmitGenerateReportRequest request)
        {
            return service.CallAsync((s, r) => s.SubmitGenerateReportAsync(r), request);
        }

        public static Task<PollGenerateReportResponse> PollGenerateReportAsync(this ServiceClient<IReportingService> service, PollGenerateReportRequest request)
        {
            return service.CallAsync((s, r) => s.PollGenerateReportAsync(r), request);
        }
    }
}