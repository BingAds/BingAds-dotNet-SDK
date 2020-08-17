using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.Reporting;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to request and retrieve performance reports with the Reporting service. 
    /// </summary>
    public class ReportRequests : ExampleBase
    {
        public static ReportingServiceManager ReportingServiceManager;
        
        /// <summary>
        /// The report file extension type.
        /// </summary>
        protected const ReportFormat ReportFileFormat = ReportFormat.Csv;
    
        /// <summary>
        /// The directory for the report file.
        /// </summary>
        protected const string FileDirectory = @"c:\reports\";

        /// <summary>
        /// The name of the report file.
        /// </summary>
        protected string ResultFileName = @"result." + ReportFileFormat.ToString().ToLower();

        /// <summary>
        /// The maximum amount of time (in milliseconds) that you want to wait for the report download.
        /// </summary>
        protected const int TimeoutInMilliseconds = 360000;
        
        public override string Description
        {
            get { return "Report Requests | Reporting V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;
                
                ReportingServiceManager = new ReportingServiceManager(
                    authorizationData: authorizationData, 
                    apiEnvironment: environment);
                ReportingServiceManager.StatusPollIntervalInMilliseconds = 5000;

                // You can submit one of the example reports, or build your own.
                var reportRequest = GetReportRequest(authorizationData.AccountId);

                var reportingDownloadParameters = new ReportingDownloadParameters
                {
                    ReportRequest = reportRequest,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = ResultFileName,
                    OverwriteResultFile = true,
                };

                // Option A - Background Completion with ReportingServiceManager
                // You can submit a download request and the ReportingServiceManager will automatically 
                // return results. The ReportingServiceManager abstracts the details of checking for result file 
                // completion, and you don't have to write any code for results polling.

                //OutputStatusMessage("Awaiting Background Completion . . .");
                //await BackgroundCompletionAsync(reportingDownloadParameters);

                // Option B - Submit and Download with ReportingServiceManager
                // Submit the download request and then use the ReportingDownloadOperation result to 
                // track status until the report is complete e.g. either using
                // TrackAsync or GetStatusAsync.

                //OutputStatusMessage("Awaiting Submit and Download . . .");
                //await SubmitAndDownloadAsync(reportRequest);

                // Option C - Download Results with ReportingServiceManager
                // If for any reason you have to resume from a previous application state, 
                // you can use an existing download request identifier and use it 
                // to download the result file. 

                // For example you might have previously retrieved a request ID using SubmitDownloadAsync.
                //var reportingDownloadOperation = await ReportingServiceManager.SubmitDownloadAsync(reportRequest);
                //var requestId = reportingDownloadOperation.RequestId;

                // Given the request ID above, you can resume the workflow and download the report.
                // The report request identifier is valid for two days. 
                // If you do not download the report within two days, you must request the report again.
                //OutputStatusMessage("Awaiting Download Results . . .");
                //await DownloadResultsAsync(requestId, authorizationData);

                // Option D - Download the report in memory with ReportingServiceManager.DownloadReportAsync
                // The DownloadReportAsync helper function downloads the report and summarizes results.
                OutputStatusMessage("Awaiting DownloadReportAsync . . .");
                await DownloadReportAsync(reportingDownloadParameters);

            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Reporting service exceptions
            catch (FaultException<Microsoft.BingAds.V13.Reporting.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.Reporting.ApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(String.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (ReportingOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the reporting operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (ReportingOperationCouldNotBeCompletedException ex)
            {
                OutputStatusMessage(string.Format("ReportingOperationCouldNotBeCompletedException Message: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        /// <summary>
        /// You can submit a download request and the ReportingServiceManager will automatically
        /// return results. The ReportingServiceManager abstracts the details of checking for result file
        /// completion, and you don't have to write any code for results polling.
        /// </summary>
        /// <param name="reportingDownloadParameters">Includes the report request type, aggregation, time period, and result file path.</param>
        private async Task BackgroundCompletionAsync(ReportingDownloadParameters reportingDownloadParameters)
        {
            // You may optionally cancel the DownloadFileAsync operation after a specified time interval. 
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            var resultFilePath = await ReportingServiceManager.DownloadFileAsync(reportingDownloadParameters, tokenSource.Token);
            OutputStatusMessage(string.Format("Download result file: {0}\n", resultFilePath));
        }

        /// <summary>
        /// Submit the download request and then use the ReportingDownloadOperation result to 
        /// track status until the report is complete e.g. either using
        /// TrackAsync or GetStatusAsync.
        /// </summary>
        /// <param name="reportRequest">Includes the report request type, aggregation, and time period.</param>
        /// <returns></returns>
        private async Task SubmitAndDownloadAsync(ReportRequest reportRequest)
        {
            // You may optionally cancel the TrackAsync operation after a specified time interval.  
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            var reportingDownloadOperation = await ReportingServiceManager.SubmitDownloadAsync(reportRequest);

            ReportingOperationStatus reportingOperationStatus = await reportingDownloadOperation.TrackAsync(tokenSource.Token);
            
            // You can use TrackAsync to poll until complete as shown above, 
            // or use custom polling logic with GetStatusAsync as shown below.

            //ReportingOperationStatus reportingOperationStatus;

            //var waitTime = new TimeSpan(0, 0, 5);
            //for (int i = 0; i < 24; i++)
            //{
            //    Thread.Sleep(waitTime);

            //    reportingOperationStatus = await reportingDownloadOperation.GetStatusAsync();

            //    if (reportingOperationStatus.Status == ReportRequestStatusType.Success)
            //    {
            //        break;
            //    }
            //}

            var resultFilePath = await reportingDownloadOperation.DownloadResultFileAsync(
                localResultDirectoryName: FileDirectory,
                localResultFileName: ResultFileName,
                decompress: true,
                overwrite: true);   // Set this value true if you want to overwrite the same file.

            OutputStatusMessage(string.Format("Download result file: {0}\n", resultFilePath));
        }

        /// <summary>
        /// If for any reason you have to resume from a previous application state, 
        /// you can use an existing download request identifier and use it 
        /// to download the result file. Use TrackAsync to indicate that the application 
        /// should wait to ensure that the download status is completed.
        /// </summary>
        /// <param name="requestId">A previous report request ID returned by the Reporting service.</param>
        /// <param name="authorizationData">The user's credentials paired with your developer token.</param>
        /// <returns></returns>
        private async Task DownloadResultsAsync(
            string requestId,
            AuthorizationData authorizationData)
        {
            // You may optionally cancel the TrackAsync operation after a specified time interval. 
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            var reportingDownloadOperation = new ReportingDownloadOperation(requestId, authorizationData);

            // Use TrackAsync to indicate that the application should wait to ensure that 
            // the download status is completed.
            var reportingOperationStatus = await reportingDownloadOperation.TrackAsync(tokenSource.Token);

            var resultFilePath = await reportingDownloadOperation.DownloadResultFileAsync(
                localResultDirectoryName: FileDirectory,
                localResultFileName: ResultFileName,
                decompress: true,
                overwrite: true);   // Set this value true if you want to overwrite the same file.

            OutputStatusMessage(string.Format("Download result file: {0}", resultFilePath));
            OutputStatusMessage(string.Format("Status: {0}", reportingOperationStatus.Status));
            OutputStatusMessage(string.Format("TrackingId: {0}\n", reportingOperationStatus.TrackingId));
        }
        
        /// <summary>
        /// Download a report file and store the contents in-memory. 
        /// </summary>
        /// <param name="reportingDownloadParameters">Includes the report request type, aggregation, time period, and result file path.</param>
        /// <returns></returns>
        private async Task DownloadReportAsync(ReportingDownloadParameters reportingDownloadParameters)
        {
            // You can get a Report object by submitting a new download request via ReportingServiceManager. 
            // Although in this case you won�t work directly with the file, under the covers a request is 
            // submitted to the Reporting service and the report file is downloaded to a local directory. 

            Report reportContainer = (await ReportingServiceManager.DownloadReportAsync(
                parameters: reportingDownloadParameters,
                cancellationToken: CancellationToken.None));

            // Otherwise if you already have a report file that was downloaded via the API, 
            // you can get a Report object via the ReportFileReader. 

            //ReportFileReader reader = new ReportFileReader(
            //    reportingDownloadParameters.ResultFileDirectory + reportingDownloadParameters.ResultFileName,
            //    (ReportFormat)reportingDownloadParameters.ReportRequest.Format);
            //Report reportContainer = reader.GetReport();

            if(reportContainer == null)
            {
                OutputStatusMessage("There is no data for the submitted report request parameters.");
                return;
            }

            // Once you have a Report object via either workflow above, you can access the metadata and report records. 

            // Output the report metadata

            long recordCount = reportContainer.ReportRecordCount;
            OutputStatusMessage(string.Format("ReportName: {0}", reportContainer.ReportName));
            OutputStatusMessage(string.Format("ReportTimeStart: {0}", reportContainer.ReportTimeStart));
            OutputStatusMessage(string.Format("ReportTimeEnd: {0}", reportContainer.ReportTimeEnd));
            OutputStatusMessage(string.Format("LastCompletedAvailableDate: {0}", reportContainer.LastCompletedAvailableDate.ToString()));
            OutputStatusMessage(string.Format("ReportAggregation: {0}", reportContainer.ReportAggregation.ToString()));
            OutputStatusMessage(string.Format("ReportColumns: {0}", string.Join("; ", reportContainer.ReportColumns)));
            OutputStatusMessage(string.Format("ReportRecordCount: {0}", recordCount));

            // Analyze and output performance statistics

            IEnumerable<IReportRecord> reportRecordIterable = reportContainer.GetReportRecords();

            int totalImpressions = 0;
            int totalClicks = 0;
            HashSet<string> distinctDevices = new HashSet<string>();
            HashSet<string> distinctNetworks = new HashSet<string>();
            foreach (IReportRecord record in reportContainer.GetReportRecords())
            {
                totalImpressions += record.GetIntegerValue("Impressions");
                totalClicks += record.GetIntegerValue("Clicks");
                distinctDevices.Add(record.GetStringValue("DeviceType"));
                distinctNetworks.Add(record.GetStringValue("Network"));
            }

            OutputStatusMessage(String.Format("Total Impressions: {0}", totalImpressions));
            OutputStatusMessage(string.Format("Total Clicks: {0}", totalClicks));
            OutputStatusMessage(string.Format("Average Impressions: {0}", totalImpressions * 1.0 / recordCount));
            OutputStatusMessage(string.Format("Average Clicks: {0}", totalClicks * 1.0 / recordCount));
            OutputStatusMessage(string.Format("Distinct Devices: {0}", string.Join("; ", distinctDevices)));
            OutputStatusMessage(string.Format("Distinct Networks: {0}", string.Join("; ", distinctNetworks)));
            
            // Be sure to close the report before you attempt to clean up files within the working directory.

            reportContainer.Dispose();

            // The CleanupTempFiles method removes all files (not sub-directories) within the working
            //  directory, whether or not the files were created by this ReportingServiceManager instance. 
            // If you are using a cloud service such as Microsoft Azure you'll want to ensure you do not
            // exceed the file or directory limits. 

            //ReportingServiceManager.CleanupTempFiles();
        }
                
        private ReportRequest GetReportRequest(
            long accountId)
        {
            var aggregation = ReportAggregation.Daily;
            var excludeColumnHeaders = false;
            var excludeReportFooter = false;
            var excludeReportHeader = false;
            var time = new ReportTime
            {
                // You can either use a custom date range or predefined time.
                CustomDateRangeEnd = null,
                CustomDateRangeStart = null,
                PredefinedTime = ReportTimePeriod.Yesterday,
                ReportTimeZone = ReportTimeZone.PacificTimeUSCanadaTijuana
            };
            var returnOnlyCompleteData = false;

            var accountPerformanceReportRequest = GetAccountPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var adGroupPerformanceReportRequest = GetAdGroupPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var adPerformanceReportRequest = GetAdPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var ageGenderAudienceReportRequest = GetAgeGenderAudienceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var audiencePerformanceReportRequest = GetAudiencePerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var budgetSummaryReportRequest = GetBudgetSummaryReportRequest(
                    accountId: accountId,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var campaignPerformanceReportRequest = GetCampaignPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var keywordPerformanceReportRequest = GetKeywordPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            // NegativeKeywordConflictReportRequest does not contain a definition for Aggregation.
            // NegativeKeywordConflictReportRequest does not contain a definition for Time.
            var negativeKeywordConflictReportRequest = GetNegativeKeywordConflictReportRequest(
                    accountId: accountId,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var productDimensionPerformanceReportRequest = GetProductDimensionPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            // ProductMatchCountReportRequest does not contain a definition for Filter.
            var productMatchCountReportRequest = GetProductMatchCountReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var productPartitionPerformanceReportRequest = GetProductPartitionPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            // ProductSearchQueryPerformanceReportRequest does not contain a definition for Filter.
            var productSearchQueryPerformanceReportRequest = GetProductSearchQueryPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var productPartitionUnitPerformanceReportRequest = GetProductPartitionUnitPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            // SearchCampaignChangeHistoryReportRequest does not contain a definition for Aggregation.
            var searchCampaignChangeHistoryReportRequest = GetSearchCampaignChangeHistoryReportRequest(
                    accountId: accountId,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);
            var searchQueryPerformanceReportRequest = GetSearchQueryPerformanceReportRequest(
                    accountId: accountId,
                    aggregation: aggregation,
                    excludeColumnHeaders: excludeColumnHeaders,
                    excludeReportFooter: excludeReportFooter,
                    excludeReportHeader: excludeReportHeader,
                    format: ReportFileFormat,
                    returnOnlyCompleteData: returnOnlyCompleteData,
                    time: time);

            // Return one of the above report types
            return campaignPerformanceReportRequest;
        }

        private ReportRequest GetAccountPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new AccountPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,                
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Account Performance Report",
                Scope = new AccountReportScope
                {
                    AccountIds = new[] { accountId }
                },
                Filter = new AccountPerformanceReportFilter { }, 
                Columns = new[]
                {
                    AccountPerformanceReportColumn.TimePeriod,
                    AccountPerformanceReportColumn.AccountId,
                    AccountPerformanceReportColumn.DeviceType,
                    AccountPerformanceReportColumn.BidMatchType,
                    AccountPerformanceReportColumn.Revenue,
                    AccountPerformanceReportColumn.Assists,
                    AccountPerformanceReportColumn.DeliveredMatchType,
                    AccountPerformanceReportColumn.AveragePosition,
                    AccountPerformanceReportColumn.Conversions,
                    AccountPerformanceReportColumn.AdDistribution,
                    AccountPerformanceReportColumn.Network,
                    AccountPerformanceReportColumn.Clicks,
                    AccountPerformanceReportColumn.Impressions,
                    AccountPerformanceReportColumn.Ctr,
                    AccountPerformanceReportColumn.AverageCpc,
                    AccountPerformanceReportColumn.Spend,
                },
            };

            return report;
        }

        private ReportRequest GetAdGroupPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new AdGroupPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Ad Group Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new AdGroupPerformanceReportFilter { },
                Columns = new[]
                {
                    AdGroupPerformanceReportColumn.TimePeriod,
                    AdGroupPerformanceReportColumn.AccountId,
                    AdGroupPerformanceReportColumn.CampaignId,
                    AdGroupPerformanceReportColumn.DeviceType,
                    AdGroupPerformanceReportColumn.BidMatchType,
                    AdGroupPerformanceReportColumn.QualityScore,
                    AdGroupPerformanceReportColumn.AdRelevance,
                    AdGroupPerformanceReportColumn.LandingPageExperience,
                    AdGroupPerformanceReportColumn.Revenue,
                    AdGroupPerformanceReportColumn.Assists,
                    AdGroupPerformanceReportColumn.ExpectedCtr,
                    AdGroupPerformanceReportColumn.DeliveredMatchType,
                    AdGroupPerformanceReportColumn.AveragePosition,
                    AdGroupPerformanceReportColumn.Conversions,
                    AdGroupPerformanceReportColumn.AdDistribution,
                    AdGroupPerformanceReportColumn.Network,
                    AdGroupPerformanceReportColumn.Clicks,
                    AdGroupPerformanceReportColumn.Impressions,
                    AdGroupPerformanceReportColumn.Ctr,
                    AdGroupPerformanceReportColumn.AverageCpc,
                    AdGroupPerformanceReportColumn.Spend,
                },
            };

            return report;
        }

        private ReportRequest GetAdPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new AdPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Ad Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new AdPerformanceReportFilter { },
                Columns = new[]
                {
                    AdPerformanceReportColumn.TimePeriod,
                    AdPerformanceReportColumn.AccountName,
                    AdPerformanceReportColumn.AccountNumber,
                    AdPerformanceReportColumn.AdGroupId,
                    AdPerformanceReportColumn.AdGroupName,
                    AdPerformanceReportColumn.AdGroupStatus,
                    AdPerformanceReportColumn.AdId,
                    AdPerformanceReportColumn.Assists,
                    AdPerformanceReportColumn.CampaignName,
                    AdPerformanceReportColumn.Language,
                    AdPerformanceReportColumn.Impressions,
                    AdPerformanceReportColumn.Clicks,
                    AdPerformanceReportColumn.Spend
                },
            };
        }

        private ReportRequest GetAgeGenderAudienceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new AgeGenderAudienceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Age Gender Audience Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new AgeGenderAudienceReportFilter { },
                Columns = new[]
                {
                    AgeGenderAudienceReportColumn.TimePeriod,
                    AgeGenderAudienceReportColumn.AccountName,
                    AgeGenderAudienceReportColumn.AccountNumber,
                    AgeGenderAudienceReportColumn.AdGroupId,
                    AgeGenderAudienceReportColumn.AdGroupName,
                    AgeGenderAudienceReportColumn.AdGroupStatus,
                    AgeGenderAudienceReportColumn.AgeGroup,
                    AgeGenderAudienceReportColumn.Assists,
                    AgeGenderAudienceReportColumn.CampaignName,
                    AgeGenderAudienceReportColumn.Language,
                    AgeGenderAudienceReportColumn.Impressions,
                    AgeGenderAudienceReportColumn.Clicks,
                    AgeGenderAudienceReportColumn.Spend
                },
            };
        }

        private ReportRequest GetAudiencePerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new AudiencePerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Audience Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new AudiencePerformanceReportFilter { },
                Columns = new[]
                {
                    AudiencePerformanceReportColumn.TimePeriod,
                    AudiencePerformanceReportColumn.AccountId,
                    AudiencePerformanceReportColumn.CampaignId,
                    AudiencePerformanceReportColumn.AudienceId,
                    AudiencePerformanceReportColumn.AudienceName,
                    AudiencePerformanceReportColumn.BidAdjustment,
                    AudiencePerformanceReportColumn.TargetingSetting,
                    AudiencePerformanceReportColumn.Clicks,
                    AudiencePerformanceReportColumn.Impressions,
                    AudiencePerformanceReportColumn.Ctr,
                    AudiencePerformanceReportColumn.AverageCpc,
                    AudiencePerformanceReportColumn.Spend,
                },
            };
        }

        private ReportRequest GetBudgetSummaryReportRequest(
            long accountId,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new BudgetSummaryReportRequest
            {
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Budget Summary Report",                
                Scope = new AccountThroughCampaignReportScope
                {
                    AccountIds = new[] { accountId }
                }, 
                Columns = new[]
                {
                    BudgetSummaryReportColumn.Date,
                    BudgetSummaryReportColumn.MonthlyBudget,
                    BudgetSummaryReportColumn.DailySpend,
                    BudgetSummaryReportColumn.MonthToDateSpend,
                    BudgetSummaryReportColumn.AccountId,
                    BudgetSummaryReportColumn.AccountName,
                    BudgetSummaryReportColumn.AccountNumber,
                    BudgetSummaryReportColumn.CampaignId,
                    BudgetSummaryReportColumn.CampaignName,
                    BudgetSummaryReportColumn.CurrencyCode,
                },
            };

            return report;
        }

        private ReportRequest GetCampaignPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new CampaignPerformanceReportRequest
            {
                Aggregation = ReportAggregation.Hourly, //aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Campaign Performance Report",
                Scope = new AccountThroughCampaignReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null
                },
                Filter = new CampaignPerformanceReportFilter { },
                Columns = new[]
                {
                    CampaignPerformanceReportColumn.TimePeriod,
                    CampaignPerformanceReportColumn.AccountId,
                    CampaignPerformanceReportColumn.CampaignId,
                    CampaignPerformanceReportColumn.CampaignName,
                    CampaignPerformanceReportColumn.CampaignStatus,
                    CampaignPerformanceReportColumn.DeviceType,
                    CampaignPerformanceReportColumn.BidMatchType,
                    CampaignPerformanceReportColumn.QualityScore,
                    CampaignPerformanceReportColumn.AdRelevance,
                    CampaignPerformanceReportColumn.LandingPageExperience,
                    CampaignPerformanceReportColumn.Revenue,
                    CampaignPerformanceReportColumn.Assists,
                    CampaignPerformanceReportColumn.ExpectedCtr,
                    CampaignPerformanceReportColumn.DeliveredMatchType,
                    CampaignPerformanceReportColumn.AveragePosition,
                    CampaignPerformanceReportColumn.Conversions,
                    CampaignPerformanceReportColumn.AdDistribution,
                    CampaignPerformanceReportColumn.Network,
                    CampaignPerformanceReportColumn.Clicks,
                    CampaignPerformanceReportColumn.Impressions,
                    CampaignPerformanceReportColumn.Ctr,
                    CampaignPerformanceReportColumn.AverageCpc,
                    CampaignPerformanceReportColumn.Spend,
                    CampaignPerformanceReportColumn.LowQualityClicks,
                    CampaignPerformanceReportColumn.LowQualityConversionRate
                },
            };

            return report;
        }

        private KeywordPerformanceReportRequest GetKeywordPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new KeywordPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Keyword Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new KeywordPerformanceReportFilter { },
                Columns = new[]
                {
                    KeywordPerformanceReportColumn.TimePeriod,
                    KeywordPerformanceReportColumn.AccountId,
                    KeywordPerformanceReportColumn.CampaignId,
                    KeywordPerformanceReportColumn.Keyword,
                    KeywordPerformanceReportColumn.KeywordId,
                    KeywordPerformanceReportColumn.DeviceType,
                    KeywordPerformanceReportColumn.BidMatchType,
                    KeywordPerformanceReportColumn.Clicks,
                    KeywordPerformanceReportColumn.Impressions,
                    KeywordPerformanceReportColumn.Ctr,
                    KeywordPerformanceReportColumn.AverageCpc,
                    KeywordPerformanceReportColumn.Spend,
                    KeywordPerformanceReportColumn.QualityScore,
                    KeywordPerformanceReportColumn.AdRelevance,
                    KeywordPerformanceReportColumn.LandingPageExperience,
                    KeywordPerformanceReportColumn.Revenue,
                    KeywordPerformanceReportColumn.Assists,
                    KeywordPerformanceReportColumn.ExpectedCtr,
                    KeywordPerformanceReportColumn.DeliveredMatchType,
                    KeywordPerformanceReportColumn.AveragePosition,
                    KeywordPerformanceReportColumn.Conversions,
                    KeywordPerformanceReportColumn.AdDistribution,
                    KeywordPerformanceReportColumn.Network,
                    KeywordPerformanceReportColumn.AdId,
                    KeywordPerformanceReportColumn.AdType,
                    KeywordPerformanceReportColumn.AdGroupId,
                },
            };

            return report;
        }

        private ReportRequest GetNegativeKeywordConflictReportRequest(
            long accountId,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new NegativeKeywordConflictReportRequest
            {
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                ReportName = "My Negative Keyword Conflict Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new NegativeKeywordConflictReportFilter { }, 
                Columns = new[]
                {
                    NegativeKeywordConflictReportColumn.AccountName,
                    NegativeKeywordConflictReportColumn.CampaignName,
                    NegativeKeywordConflictReportColumn.ConflictLevel,
                    NegativeKeywordConflictReportColumn.ConflictType,
                    NegativeKeywordConflictReportColumn.AccountId,
                    NegativeKeywordConflictReportColumn.AccountNumber,
                    NegativeKeywordConflictReportColumn.AdGroupId,
                    NegativeKeywordConflictReportColumn.AdGroupName,
                    NegativeKeywordConflictReportColumn.CampaignId,
                    NegativeKeywordConflictReportColumn.Keyword,
                    NegativeKeywordConflictReportColumn.NegativeKeyword,
                },
            };
        }
        
        private ReportRequest GetProductDimensionPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new ProductDimensionPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Product Dimension Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },
                Filter = new ProductDimensionPerformanceReportFilter { },                
                Columns = new[]
                {
                    ProductDimensionPerformanceReportColumn.TimePeriod,
                    ProductDimensionPerformanceReportColumn.MerchantProductId,
                    ProductDimensionPerformanceReportColumn.AccountName,
                    ProductDimensionPerformanceReportColumn.AccountNumber,
                    ProductDimensionPerformanceReportColumn.AdGroupId,
                    ProductDimensionPerformanceReportColumn.AdGroupName,
                    ProductDimensionPerformanceReportColumn.AdId,
                    ProductDimensionPerformanceReportColumn.AverageCpc,
                    ProductDimensionPerformanceReportColumn.Brand,
                    ProductDimensionPerformanceReportColumn.CampaignName,
                    ProductDimensionPerformanceReportColumn.Condition,
                    ProductDimensionPerformanceReportColumn.Ctr,
                    ProductDimensionPerformanceReportColumn.CurrencyCode,
                    ProductDimensionPerformanceReportColumn.CustomLabel0,
                    ProductDimensionPerformanceReportColumn.CustomLabel1,
                    ProductDimensionPerformanceReportColumn.CustomLabel2,
                    ProductDimensionPerformanceReportColumn.CustomLabel3,
                    ProductDimensionPerformanceReportColumn.CustomLabel4,
                    ProductDimensionPerformanceReportColumn.DeviceType,
                    ProductDimensionPerformanceReportColumn.Language,
                    ProductDimensionPerformanceReportColumn.ProductCategory1,
                    ProductDimensionPerformanceReportColumn.ProductCategory2,
                    ProductDimensionPerformanceReportColumn.ProductCategory3,
                    ProductDimensionPerformanceReportColumn.ProductCategory4,
                    ProductDimensionPerformanceReportColumn.ProductCategory5,
                    ProductDimensionPerformanceReportColumn.ProductType1,
                    ProductDimensionPerformanceReportColumn.ProductType2,
                    ProductDimensionPerformanceReportColumn.ProductType3,
                    ProductDimensionPerformanceReportColumn.ProductType4,
                    ProductDimensionPerformanceReportColumn.ProductType5,
                    ProductDimensionPerformanceReportColumn.Title,
                    ProductDimensionPerformanceReportColumn.Impressions,
                    ProductDimensionPerformanceReportColumn.Clicks,
                    ProductDimensionPerformanceReportColumn.Spend
                },
            };
        }

        private ReportRequest GetProductMatchCountReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new ProductMatchCountReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Product Match Count Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },
                Columns = new[]
                {
                    ProductMatchCountReportColumn.AccountName,
                    ProductMatchCountReportColumn.CampaignName,
                    ProductMatchCountReportColumn.AdGroupCriterionId,
                    ProductMatchCountReportColumn.AccountId,
                    ProductMatchCountReportColumn.AccountNumber,
                    ProductMatchCountReportColumn.AdGroupId,
                    ProductMatchCountReportColumn.AdGroupName,
                    ProductMatchCountReportColumn.CampaignId,
                    ProductMatchCountReportColumn.CustomerId,
                    ProductMatchCountReportColumn.CustomerName,
                    ProductMatchCountReportColumn.MatchedProductsAtAdGroup,
                    ProductMatchCountReportColumn.MatchedProductsAtCampaign,
                    ProductMatchCountReportColumn.MatchedProductsAtProductGroup,
                    ProductMatchCountReportColumn.PartitionType,
                    ProductMatchCountReportColumn.ProductGroup
                },
            };
        }

        private ReportRequest GetProductPartitionPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new ProductPartitionPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Product Partition Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },
                Filter = new ProductPartitionPerformanceReportFilter { },
                Columns = new[]
                {
                    ProductPartitionPerformanceReportColumn.TimePeriod,
                    ProductPartitionPerformanceReportColumn.AccountId,
                    ProductPartitionPerformanceReportColumn.CampaignId,
                    ProductPartitionPerformanceReportColumn.AdGroupCriterionId,
                    ProductPartitionPerformanceReportColumn.ProductGroup,
                    ProductPartitionPerformanceReportColumn.PartitionType,
                    ProductPartitionPerformanceReportColumn.BidMatchType,
                    ProductPartitionPerformanceReportColumn.Clicks,
                    ProductPartitionPerformanceReportColumn.Impressions,
                    ProductPartitionPerformanceReportColumn.Ctr,
                    ProductPartitionPerformanceReportColumn.AverageCpc,
                    ProductPartitionPerformanceReportColumn.Spend,
                },
            };
        }

        private ReportRequest GetProductPartitionUnitPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            return new ProductPartitionUnitPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Product Partition Unit Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },
                Filter = new ProductPartitionUnitPerformanceReportFilter { },
                Columns = new[]
                {
                    ProductPartitionUnitPerformanceReportColumn.TimePeriod,
                    ProductPartitionUnitPerformanceReportColumn.AccountId,
                    ProductPartitionUnitPerformanceReportColumn.CampaignId,
                    ProductPartitionUnitPerformanceReportColumn.AdGroupCriterionId,
                    ProductPartitionUnitPerformanceReportColumn.ProductGroup,
                    ProductPartitionUnitPerformanceReportColumn.BidMatchType,
                    ProductPartitionUnitPerformanceReportColumn.Clicks,
                    ProductPartitionUnitPerformanceReportColumn.Impressions,
                    ProductPartitionUnitPerformanceReportColumn.Ctr,
                    ProductPartitionUnitPerformanceReportColumn.AverageCpc,
                    ProductPartitionUnitPerformanceReportColumn.Spend,
                },
            };
        }

        private ReportRequest GetProductSearchQueryPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new ProductSearchQueryPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Product Search Query Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Columns = new[]
                {
                    ProductSearchQueryPerformanceReportColumn.TimePeriod,
                    ProductSearchQueryPerformanceReportColumn.AccountId,
                    ProductSearchQueryPerformanceReportColumn.CampaignName,
                    ProductSearchQueryPerformanceReportColumn.AccountName,
                    ProductSearchQueryPerformanceReportColumn.AccountNumber,
                    ProductSearchQueryPerformanceReportColumn.AdGroupCriterionId,
                    ProductSearchQueryPerformanceReportColumn.CampaignId,
                    ProductSearchQueryPerformanceReportColumn.DeviceType,
                    ProductSearchQueryPerformanceReportColumn.PartitionType,
                    ProductSearchQueryPerformanceReportColumn.ProductGroup,
                    ProductSearchQueryPerformanceReportColumn.SearchQuery,
                    ProductSearchQueryPerformanceReportColumn.Revenue,
                    ProductSearchQueryPerformanceReportColumn.Assists,
                    ProductSearchQueryPerformanceReportColumn.Conversions,
                    ProductSearchQueryPerformanceReportColumn.Network,
                    ProductSearchQueryPerformanceReportColumn.Clicks,
                    ProductSearchQueryPerformanceReportColumn.Impressions,
                    ProductSearchQueryPerformanceReportColumn.Ctr,
                    ProductSearchQueryPerformanceReportColumn.AverageCpc,
                    ProductSearchQueryPerformanceReportColumn.Spend,
                    ProductSearchQueryPerformanceReportColumn.MerchantProductId
                },

            };

            return report;
        }
        
        private ReportRequest GetSearchCampaignChangeHistoryReportRequest(
            long accountId,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        { 
            var report = new SearchCampaignChangeHistoryReportRequest
            {
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Search Campaign Change History Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },
                Filter = new SearchCampaignChangeHistoryReportFilter { },
                Columns = new[]
                {
                    SearchCampaignChangeHistoryReportColumn.DateTime,
                    SearchCampaignChangeHistoryReportColumn.AccountId,
                    SearchCampaignChangeHistoryReportColumn.AdGroupId,
                    SearchCampaignChangeHistoryReportColumn.AdGroupName,
                    SearchCampaignChangeHistoryReportColumn.AdTitle,
                    SearchCampaignChangeHistoryReportColumn.AttributeChanged,
                    SearchCampaignChangeHistoryReportColumn.CampaignId,
                    SearchCampaignChangeHistoryReportColumn.CampaignName,
                    SearchCampaignChangeHistoryReportColumn.ChangedBy,
                    SearchCampaignChangeHistoryReportColumn.DisplayUrl,
                    SearchCampaignChangeHistoryReportColumn.HowChanged,
                    SearchCampaignChangeHistoryReportColumn.ItemChanged,
                    SearchCampaignChangeHistoryReportColumn.Keyword,
                    SearchCampaignChangeHistoryReportColumn.NewValue,
                    SearchCampaignChangeHistoryReportColumn.OldValue,
                },                
            };

            return report;
        }

        private ReportRequest GetSearchQueryPerformanceReportRequest(
            long accountId,
            ReportAggregation aggregation,
            bool excludeColumnHeaders,
            bool excludeReportFooter,
            bool excludeReportHeader,
            ReportFormat format,
            bool returnOnlyCompleteData,
            ReportTime time)
        {
            var report = new SearchQueryPerformanceReportRequest
            {
                Aggregation = aggregation,
                ExcludeColumnHeaders = excludeColumnHeaders,
                ExcludeReportFooter = excludeReportFooter,
                ExcludeReportHeader = excludeReportHeader,
                Format = format,
                ReturnOnlyCompleteData = returnOnlyCompleteData,
                Time = time,
                ReportName = "My Search Query Performance Report",
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },
                Filter = new SearchQueryPerformanceReportFilter { },
                Columns = new[]
                {
                    SearchQueryPerformanceReportColumn.TimePeriod,
                    SearchQueryPerformanceReportColumn.AccountId,
                    SearchQueryPerformanceReportColumn.CampaignId,
                    SearchQueryPerformanceReportColumn.DeviceType,
                    SearchQueryPerformanceReportColumn.BidMatchType,
                    SearchQueryPerformanceReportColumn.CampaignType,
                    SearchQueryPerformanceReportColumn.SearchQuery,
                    SearchQueryPerformanceReportColumn.Revenue,
                    SearchQueryPerformanceReportColumn.Assists,
                    SearchQueryPerformanceReportColumn.DeliveredMatchType,
                    SearchQueryPerformanceReportColumn.AveragePosition,
                    SearchQueryPerformanceReportColumn.Conversions,
                    SearchQueryPerformanceReportColumn.Network,
                    SearchQueryPerformanceReportColumn.Clicks,
                    SearchQueryPerformanceReportColumn.Impressions,
                    SearchQueryPerformanceReportColumn.Ctr,
                    SearchQueryPerformanceReportColumn.AverageCpc,
                    SearchQueryPerformanceReportColumn.Spend,
                },
            };

            return report;
        }
    }
}
