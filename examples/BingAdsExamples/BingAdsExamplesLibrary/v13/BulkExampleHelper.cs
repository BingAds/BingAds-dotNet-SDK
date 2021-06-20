using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;

namespace BingAdsExamplesLibrary.V13
{
    public class BulkExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | Bulk V13"; }
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
            DateTime? lastSyncTimeInUTC)
        {
            var request = new DownloadCampaignsByAccountIdsRequest
            {
                AccountIds = accountIds,
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadEntities = downloadEntities,
                DownloadFileType = downloadFileType,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC
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
            DateTime? lastSyncTimeInUTC)
        {
            var request = new DownloadCampaignsByCampaignIdsRequest
            {
                Campaigns = campaigns,
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadEntities = downloadEntities,
                DownloadFileType = downloadFileType,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC
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
                OutputStatusMessage("* * * Begin OutputAdApiError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputAdApiError * * *");
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiError(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiFaultDetail * * *");
                OutputStatusMessage("Errors:");
                OutputArrayOfAdApiError(dataObject.Errors);
                OutputStatusMessage("* * * End OutputAdApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiFaultDetail * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                OutputStatusMessage("* * * End OutputApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApplicationFault * * *");
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(null != adapifaultdetail)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(null != apifaultdetail)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
                OutputStatusMessage("* * * End OutputApplicationFault * * *");
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApplicationFault(dataObject);
                    }
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBatchError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("FieldPath: {0}", dataObject.FieldPath));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var editorialerror = dataObject as EditorialError;
                if(null != editorialerror)
                {
                    OutputEditorialError((EditorialError)dataObject);
                }
                OutputStatusMessage("* * * End OutputBatchError * * *");
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBatchError(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignScope(CampaignScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignScope * * *");
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("ParentAccountId: {0}", dataObject.ParentAccountId));
                OutputStatusMessage("* * * End OutputCampaignScope * * *");
            }
        }
        public void OutputArrayOfCampaignScope(IList<CampaignScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignScope(dataObject);
                    }
                }
            }
        }
        public void OutputEditorialError(EditorialError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEditorialError * * *");
                OutputStatusMessage(string.Format("Appealable: {0}", dataObject.Appealable));
                OutputStatusMessage(string.Format("DisapprovedText: {0}", dataObject.DisapprovedText));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("PublisherCountry: {0}", dataObject.PublisherCountry));
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage("* * * End OutputEditorialError * * *");
            }
        }
        public void OutputArrayOfEditorialError(IList<EditorialError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEditorialError(dataObject);
                    }
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOperationError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputOperationError * * *");
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOperationError(dataObject);
                    }
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
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
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
        public void OutputKeyValuePairOflonglong(KeyValuePair<long,long> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOflonglong(IList<KeyValuePair<long,long>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOflonglong(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOfstringbase64Binary(KeyValuePair<string,byte[]> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringbase64Binary(IList<KeyValuePair<string,byte[]>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringbase64Binary(dataObject);
                }
            }
        }
    }
}