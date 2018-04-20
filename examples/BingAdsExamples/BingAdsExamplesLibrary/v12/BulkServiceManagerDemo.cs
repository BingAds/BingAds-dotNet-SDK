using System;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.Bulk;
using Microsoft.BingAds.V12.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;
using System.Threading;
using System.Collections.Generic;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates multiple ways to download entities such as keywords, 
    /// using the BulkServiceManager class.
    /// </summary>
    public class BulkServiceManagerDemo : BulkExampleBase
    {
        public override string Description
        {
            get { return "Bulk Service Manager Upload and Download Demo | Bulk V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkServiceManager = new BulkServiceManager(authorizationData);
                BulkServiceManager.StatusPollIntervalInMilliseconds = 5000;

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                #region Upload

                // In this example we'll upload a new campaign and then delete it. 

                var uploadEntities = new List<BulkEntity>();
                uploadEntities.Add(GetExampleBulkCampaign());

                OutputStatusMessage("Uploading a campaign with UploadEntitiesAsync . . .");
                var resultEntities = await UploadEntitiesAsync(uploadEntities, progress);

                uploadEntities = new List<BulkEntity>();
                foreach(var campaignResult in resultEntities.OfType<BulkCampaign>().ToList())
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                OutputStatusMessage("Deleting a campaign with UploadEntitiesAsync . . .");
                resultEntities = await UploadEntitiesAsync(uploadEntities, progress);
                
                #endregion Upload

                #region Download

                // In this example we will download all campaigns, ad groups, and ads in the account.
                var entities = new[] {
                    DownloadEntity.Campaigns,
                    DownloadEntity.AdGroups,
                    DownloadEntity.Ads
                };

                // DownloadParameters is used for Option A below.
                var downloadParameters = new DownloadParameters
                {
                    CampaignIds = null,
                    DataScope = DataScope.EntityData | DataScope.QualityScoreData,
                    PerformanceStatsDateRange = new PerformanceStatsDateRange { PredefinedTime = ReportTimePeriod.LastFourWeeks },
                    DownloadEntities = entities,
                    FileType = FileType,
                    LastSyncTimeInUTC = null,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true
                };

                // SubmitDownloadParameters is used for Option B and Option C below.
                var submitDownloadParameters = new SubmitDownloadParameters
                {
                    CampaignIds = null,
                    DataScope = DataScope.EntityData | DataScope.QualityScoreData,
                    PerformanceStatsDateRange = new PerformanceStatsDateRange { PredefinedTime = ReportTimePeriod.LastFourWeeks },
                    DownloadEntities = entities,
                    FileType = FileType,
                    LastSyncTimeInUTC = null
                };

                // Option A - Background Completion with BulkServiceManager
                // You can submit a download or upload request and the BulkServiceManager will automatically 
                // return results. The BulkServiceManager abstracts the details of checking for result file 
                // completion, and you don't have to write any code for results polling.

                OutputStatusMessage("Awaiting Background Completion with DownloadFileAsync . . .");
                await BackgroundCompletionAsync(downloadParameters, progress);

                // Alternatively we can use DownloadEntitiesAsync if we want to work with the entities in memory.
                // If you enable this option the result file from BackgroundCompletionAsync will also be deleted
                // if written to the same working directory.
                OutputStatusMessage("Awaiting Background Completion with DownloadEntitiesAsync . . .");
                var downloadEntities = await DownloadEntitiesAsync(downloadParameters, progress);

                // Option B - Submit and Download with BulkServiceManager
                // Submit the download request and then use the BulkDownloadOperation result to 
                // track status until the download is complete e.g. either using
                // TrackAsync or GetStatusAsync.

                //OutputStatusMessage("Awaiting Submit and Download . . .");
                //await SubmitAndDownloadAsync(submitDownloadParameters);

                // Option C - Download Results with BulkServiceManager
                // If for any reason you have to resume from a previous application state, 
                // you can use an existing download request identifier and use it 
                // to download the result file. 

                // For example you might have previously retrieved a request ID using SubmitDownloadAsync.
                //var bulkDownloadOperation = await BulkService.SubmitDownloadAsync(submitDownloadParameters);
                //var requestId = bulkDownloadOperation.RequestId;

                // Given the request ID above, you can resume the workflow and download the bulk file.
                // The download request identifier is valid for two days. 
                // If you do not download the bulk file within two days, you must request it again.
                //OutputStatusMessage("Awaiting Download Results . . .");
                //await DownloadResultsAsync(requestId, authorizationData);

                #endregion Download
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V12.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(String.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the bulk operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (BulkOperationCouldNotBeCompletedException<DownloadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationCouldNotBeCompletedException<UploadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        /// <summary>
        /// Writes the specified entities to a local temporary file prior to upload.  
        /// </summary>
        /// <param name="uploadEntities"></param>
        /// <returns></returns>
        protected async Task<List<BulkEntity>> UploadEntitiesAsync(
            IEnumerable<BulkEntity> uploadEntities,
            Progress<BulkOperationProgressInfo> progress)
        {
            // The system temp directory will be used if another working directory is not specified. If you are 
            // using a cloud service such as Azure you'll want to ensure you do not exceed the file or directory limits. 
            // You can specify a different working directory for each BulkServiceManager instance.

            BulkServiceManager.WorkingDirectory = FileDirectory;
                        
            var entityUploadParameters = new EntityUploadParameters
            {
                Entities = uploadEntities,
                OverwriteResultFile = true,
                ResultFileDirectory = FileDirectory,
                ResultFileName = ResultFileName,
                ResponseMode = ResponseMode.ErrorsAndResults
            };

            // The UploadEntitiesAsync method returns IEnumerable<BulkEntity>, so the result file will not
            // be accessible e.g. for CleanupTempFiles until you iterate over the result e.g. via ToList().

            var resultEntities = (await BulkServiceManager.UploadEntitiesAsync(entityUploadParameters)).ToList();

            // The CleanupTempFiles method removes all files (not sub-directories) within the working directory, 
            // whether or not the files were created by this BulkServiceManager instance. 

            BulkServiceManager.CleanupTempFiles();

            return resultEntities;
        }

        /// <summary>
        /// Writes the specified entities to a local temporary file after download. 
        /// </summary>
        /// <param name="downloadParameters"></param>
        /// <returns></returns>
        protected async Task<List<BulkEntity>> DownloadEntitiesAsync(
            DownloadParameters downloadParameters,
            Progress<BulkOperationProgressInfo> progress)
        {
            // The system temp directory will be used if another working directory is not specified. If you are 
            // using a cloud service such as Azure you'll want to ensure you do not exceed the file or directory limits. 
            // You can specify a different working directory for each BulkServiceManager instance.

            BulkServiceManager.WorkingDirectory = FileDirectory;

            // The DownloadEntitiesAsync method returns IEnumerable<BulkEntity>, so the download file will not
            // be accessible e.g. for CleanupTempFiles until you iterate over the result e.g. via ToList().

            var resultEntities = (await BulkServiceManager.DownloadEntitiesAsync(downloadParameters)).ToList();

            // The CleanupTempFiles method removes all files (not sub-directories) within the working directory, 
            // whether or not the files were created by this BulkServiceManager instance. 

            BulkServiceManager.CleanupTempFiles();

            return resultEntities;
        }

        /// <summary>
        /// You can submit a download or upload request and the BulkServiceManager will automatically
        /// return results. The BulkServiceManager abstracts the details of checking for result file
        /// completion, and you don't have to write any code for results polling.
        /// </summary>
        /// <param name="downloadParameters"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task BackgroundCompletionAsync(
            DownloadParameters downloadParameters, 
            Progress<BulkOperationProgressInfo> progress)
        {
            // You may optionally cancel the DownloadFileAsync operation after a specified time interval. 
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            var resultFilePath = await BulkServiceManager.DownloadFileAsync(downloadParameters, progress, tokenSource.Token);
            OutputStatusMessage(string.Format("Download result file: {0}\n", resultFilePath));
        }

        /// <summary>
        /// Submit the download request and then use the BulkDownloadOperation result to 
        /// track status until the download is complete e.g. either using
        /// TrackAsync or GetStatusAsync.
        /// </summary>
        /// <param name="submitDownloadParameters"></param>
        /// <returns></returns>
        private async Task SubmitAndDownloadAsync(SubmitDownloadParameters submitDownloadParameters)
        {
            var bulkDownloadOperation = await BulkServiceManager.SubmitDownloadAsync(submitDownloadParameters);

            // You may optionally cancel the TrackAsync operation after a specified time interval. 
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            BulkOperationStatus<DownloadStatus> downloadStatus = await bulkDownloadOperation.TrackAsync(null, tokenSource.Token);

            // You can use TrackAsync to poll until complete as shown above, 
            // or use custom polling logic with GetStatusAsync as shown below.

            //BulkOperationStatus<DownloadStatus> downloadStatus;
            //var waitTime = new TimeSpan(0, 0, 5);

            //for (int i = 0; i < 24; i++)
            //{
            //    Thread.Sleep(waitTime);

            //    downloadStatus = await bulkDownloadOperation.GetStatusAsync();

            //    if (downloadStatus.Status == DownloadStatus.Completed)
            //    {
            //        break;
            //    }
            //}

            var resultFilePath = await bulkDownloadOperation.DownloadResultFileAsync(
                FileDirectory,
                ResultFileName,
                decompress: true,
                overwrite: true // Set this value true if you want to overwrite the same file.
            );   

            OutputStatusMessage(string.Format("Download result file: {0}\n", resultFilePath));
        }

        /// <summary>
        /// If for any reason you have to resume from a previous application state, 
        /// you can use an existing download request identifier and use it 
        /// to download the result file. Use TrackAsync to indicate that the application 
        /// should wait to ensure that the download status is completed.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="authorizationData"></param>
        /// <returns></returns>
        private async Task DownloadResultsAsync(
            string requestId,
            AuthorizationData authorizationData)
        {
            // You may optionally cancel the TrackAsync operation after a specified time interval. 
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            var bulkDownloadOperation = new BulkDownloadOperation(requestId, authorizationData);

            // Use TrackAsync to indicate that the application should wait to ensure that 
            // the download status is completed.
            var bulkOperationStatus = await bulkDownloadOperation.TrackAsync(null, tokenSource.Token);

            var resultFilePath = await bulkDownloadOperation.DownloadResultFileAsync(
                FileDirectory,
                ResultFileName,
                decompress: true,
                overwrite: true);   // Set this value true if you want to overwrite the same file.

            OutputStatusMessage(string.Format("Download result file: {0}", resultFilePath));
            OutputStatusMessage(string.Format("Status: {0}", bulkOperationStatus.Status));
            OutputStatusMessage(string.Format("TrackingId: {0}\n", bulkOperationStatus.TrackingId));
        }
    }
}
