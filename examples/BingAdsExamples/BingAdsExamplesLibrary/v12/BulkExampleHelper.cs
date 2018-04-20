using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.Bulk;

namespace BingAdsExamplesLibrary.V12
{
    public class BulkExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | Bulk V12"; }
        }
        public ServiceClient<IBulkService> BulkService;
        public BulkExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }
        public async Task<DownloadCampaignsByAccountIdsResponse> DownloadCampaignsByAccountIdsAsync(
            IList<long> accountIds,
            CompressionType? compressionType,
            DataScope dataScope,
            IList<DownloadEntity> downloadEntities,
            DownloadFileType? downloadFileType,
            String formatVersion,
            DateTime? lastSyncTimeInUTC,
            PerformanceStatsDateRange performanceStatsDateRange)
        {
            var request = new DownloadCampaignsByAccountIdsRequest
            {
                AccountIds = accountIds,
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadEntities = downloadEntities,
                DownloadFileType = downloadFileType,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC,
                PerformanceStatsDateRange = performanceStatsDateRange
            };

            return (await BulkService.CallAsync((s, r) => s.DownloadCampaignsByAccountIdsAsync(r), request));
        }
        public async Task<DownloadCampaignsByCampaignIdsResponse> DownloadCampaignsByCampaignIdsAsync(
            IList<CampaignScope> campaigns,
            CompressionType? compressionType,
            DataScope dataScope,
            IList<DownloadEntity> downloadEntities,
            DownloadFileType? downloadFileType,
            String formatVersion,
            DateTime? lastSyncTimeInUTC,
            PerformanceStatsDateRange performanceStatsDateRange)
        {
            var request = new DownloadCampaignsByCampaignIdsRequest
            {
                Campaigns = campaigns,
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadEntities = downloadEntities,
                DownloadFileType = downloadFileType,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC,
                PerformanceStatsDateRange = performanceStatsDateRange
            };

            return (await BulkService.CallAsync((s, r) => s.DownloadCampaignsByCampaignIdsAsync(r), request));
        }
        public async Task<GetBulkDownloadStatusResponse> GetBulkDownloadStatusAsync(
            String requestId)
        {
            var request = new GetBulkDownloadStatusRequest
            {
                RequestId = requestId
            };

            return (await BulkService.CallAsync((s, r) => s.GetBulkDownloadStatusAsync(r), request));
        }
        public async Task<GetBulkUploadStatusResponse> GetBulkUploadStatusAsync(
            String requestId)
        {
            var request = new GetBulkUploadStatusRequest
            {
                RequestId = requestId
            };

            return (await BulkService.CallAsync((s, r) => s.GetBulkUploadStatusAsync(r), request));
        }
        public async Task<GetBulkUploadUrlResponse> GetBulkUploadUrlAsync(
            ResponseMode responseMode,
            long accountId)
        {
            var request = new GetBulkUploadUrlRequest
            {
                ResponseMode = responseMode,
                AccountId = accountId
            };

            return (await BulkService.CallAsync((s, r) => s.GetBulkUploadUrlAsync(r), request));
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdApiError(dataObject.Errors);
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputArrayOfOperationError(dataObject.OperationErrors);
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(adapifaultdetail != null)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(apifaultdetail != null)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApplicationFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("FieldPath: {0}", dataObject.FieldPath));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var editorialerror = dataObject as EditorialError;
                if(editorialerror != null)
                {
                    OutputEditorialError((EditorialError)dataObject);
                }
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBatchError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignScope(CampaignScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("ParentAccountId: {0}", dataObject.ParentAccountId));
            }
        }
        public void OutputArrayOfCampaignScope(IList<CampaignScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDate(Date dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
            }
        }
        public void OutputArrayOfDate(IList<Date> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEditorialError(EditorialError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Appealable: {0}", dataObject.Appealable));
                OutputStatusMessage(string.Format("DisapprovedText: {0}", dataObject.DisapprovedText));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("PublisherCountry: {0}", dataObject.PublisherCountry));
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
            }
        }
        public void OutputArrayOfEditorialError(IList<EditorialError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEditorialError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOperationError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPerformanceStatsDateRange(PerformanceStatsDateRange dataObject)
        {
            if (null != dataObject)
            {
                OutputDate(dataObject.CustomDateRangeEnd);
                OutputDate(dataObject.CustomDateRangeStart);
                OutputStatusMessage(string.Format("PredefinedTime: {0}", dataObject.PredefinedTime));
            }
        }
        public void OutputArrayOfPerformanceStatsDateRange(IList<PerformanceStatsDateRange> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPerformanceStatsDateRange(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCompressionType(CompressionType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CompressionType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCompressionType(IList<CompressionType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCompressionType(valueSet);
                }
            }
        }
        public void OutputDataScope(DataScope valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DataScope)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDataScope(IList<DataScope> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDataScope(valueSet);
                }
            }
        }
        public void OutputDownloadEntity(DownloadEntity valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DownloadEntity)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDownloadEntity(IList<DownloadEntity> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDownloadEntity(valueSet);
                }
            }
        }
        public void OutputDownloadFileType(DownloadFileType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DownloadFileType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDownloadFileType(IList<DownloadFileType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDownloadFileType(valueSet);
                }
            }
        }
        public void OutputReportTimePeriod(ReportTimePeriod valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportTimePeriod)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportTimePeriod(IList<ReportTimePeriod> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportTimePeriod(valueSet);
                }
            }
        }
        public void OutputResponseMode(ResponseMode valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ResponseMode)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfResponseMode(IList<ResponseMode> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputResponseMode(valueSet);
                }
            }
        }
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the string: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the long: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable long: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the int: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable int: {0}", item));
                }
            }
        }
        public void OutputKeyValuePairOfstringstring(KeyValuePair<string,string> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringstring(IList<KeyValuePair<string,string>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringstring(dataObject);
                }
            }
        }
    }
}