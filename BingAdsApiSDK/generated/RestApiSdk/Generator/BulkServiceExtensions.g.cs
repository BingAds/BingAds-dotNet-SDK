using System.Threading.Tasks;
using Microsoft.BingAds.V13.Bulk;

namespace Microsoft.BingAds
{
    public static partial class ServiceClientExtensions
    {
        public static Task<DownloadCampaignsByAccountIdsResponse> DownloadCampaignsByAccountIdsAsync(this ServiceClient<IBulkService> service, DownloadCampaignsByAccountIdsRequest request)
        {
            return service.CallAsync((s, r) => s.DownloadCampaignsByAccountIdsAsync(r), request);
        }

        public static Task<DownloadCampaignsByCampaignIdsResponse> DownloadCampaignsByCampaignIdsAsync(this ServiceClient<IBulkService> service, DownloadCampaignsByCampaignIdsRequest request)
        {
            return service.CallAsync((s, r) => s.DownloadCampaignsByCampaignIdsAsync(r), request);
        }

        public static Task<GetBulkDownloadStatusResponse> GetBulkDownloadStatusAsync(this ServiceClient<IBulkService> service, GetBulkDownloadStatusRequest request)
        {
            return service.CallAsync((s, r) => s.GetBulkDownloadStatusAsync(r), request);
        }

        public static Task<GetBulkUploadUrlResponse> GetBulkUploadUrlAsync(this ServiceClient<IBulkService> service, GetBulkUploadUrlRequest request)
        {
            return service.CallAsync((s, r) => s.GetBulkUploadUrlAsync(r), request);
        }

        public static Task<GetBulkUploadStatusResponse> GetBulkUploadStatusAsync(this ServiceClient<IBulkService> service, GetBulkUploadStatusRequest request)
        {
            return service.CallAsync((s, r) => s.GetBulkUploadStatusAsync(r), request);
        }

        public static Task<UploadEntityRecordsResponse> UploadEntityRecordsAsync(this ServiceClient<IBulkService> service, UploadEntityRecordsRequest request)
        {
            return service.CallAsync((s, r) => s.UploadEntityRecordsAsync(r), request);
        }
    }
}