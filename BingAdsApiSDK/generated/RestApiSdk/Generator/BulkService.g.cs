using System;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.Bulk;

namespace Microsoft.BingAds
{
    internal class BulkService : IBulkService
    {
        private readonly RestServiceClient _restServiceClient;

        private readonly Type _serviceType;

        public BulkService(RestServiceClient restServiceClient, Type serviceType)
        {
            _restServiceClient = restServiceClient;

            _serviceType = serviceType;
        }

        public DownloadCampaignsByAccountIdsResponse DownloadCampaignsByAccountIds(DownloadCampaignsByAccountIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DownloadCampaignsByAccountIdsResponse> DownloadCampaignsByAccountIdsAsync(DownloadCampaignsByAccountIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DownloadCampaignsByAccountIdsResponse>("DownloadCampaignsByAccountIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DownloadCampaignsByCampaignIdsResponse DownloadCampaignsByCampaignIds(DownloadCampaignsByCampaignIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DownloadCampaignsByCampaignIdsResponse> DownloadCampaignsByCampaignIdsAsync(DownloadCampaignsByCampaignIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DownloadCampaignsByCampaignIdsResponse>("DownloadCampaignsByCampaignIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBulkDownloadStatusResponse GetBulkDownloadStatus(GetBulkDownloadStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBulkDownloadStatusResponse> GetBulkDownloadStatusAsync(GetBulkDownloadStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBulkDownloadStatusResponse>("GetBulkDownloadStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBulkUploadUrlResponse GetBulkUploadUrl(GetBulkUploadUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBulkUploadUrlResponse> GetBulkUploadUrlAsync(GetBulkUploadUrlRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBulkUploadUrlResponse>("GetBulkUploadUrl", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBulkUploadStatusResponse GetBulkUploadStatus(GetBulkUploadStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBulkUploadStatusResponse> GetBulkUploadStatusAsync(GetBulkUploadStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBulkUploadStatusResponse>("GetBulkUploadStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UploadEntityRecordsResponse UploadEntityRecords(UploadEntityRecordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UploadEntityRecordsResponse> UploadEntityRecordsAsync(UploadEntityRecordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UploadEntityRecordsResponse>("UploadEntityRecords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}