using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds.Reporting;
using Microsoft.BingAds;


namespace BingAdsExamples.V9
{
    /// <summary>
    /// This example demonstrates how to request and retrieve a keyword performance report.
    /// </summary>
    public class ReportRequests : ExampleBase
    {
        public static ReportingServiceManager ReportingService;

        /// <summary>
        /// The directory for the report file.
        /// </summary>
        protected const string FileDirectory = @"c:\reports\";
        
        /// <summary>
        /// The name of the report file.
        /// </summary>
        protected const string ResultFileName = @"result.csv";

        /// <summary>
        /// The report file extension type.
        /// </summary>
        protected const ReportFormat ReportFileFormat = ReportFormat.Csv;
        

        public override string Description
        {
            get { return "Report Requests | Reporting V9"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ReportingService = new ReportingServiceManager(authorizationData);
                ReportingService.StatusPollIntervalInMilliseconds = 5000;
                
                // You can submit one of the example reports, or build your own.
 
                var reportRequest = GetCampaignPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetKeywordPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetProductDimensionPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetProductPartitionPerformanceReportRequest(authorizationData.AccountId);

                var reportingDownloadParameters = new ReportingDownloadParameters
                {
                    ReportRequest = reportRequest,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = ResultFileName,
                    OverwriteResultFile = true,
                };

                // Option A - Background Completion with ReportingServiceManager
                // You can submit a download or upload request and the ReportingServiceManager will automatically 
                // return results. The ReportingServiceManager abstracts the details of checking for result file 
                // completion, and you don't have to write any code for results polling.

                OutputStatusMessage("Awaiting Background Completion . . .");
                await BackgroundCompletion(reportingDownloadParameters);

                // Option B - Submit and Download with ReportingServiceManager
                // Submit the download request and then use the ReportingDownloadOperation result to 
                // track status yourself using GetStatusAsync.

                OutputStatusMessage("Awaiting Submit and Download . . .");
                await SubmitAndDownload(reportRequest);

                // Option C - Download Results with ReportingServiceManager
                // If for any reason you have to resume from a previous application state, 
                // you can use an existing download request identifier and use it 
                // to download the result file. Use TrackAsync to indicate that the application 
                // should wait to ensure that the download status is completed.

                // For example you might have previously retrieved a request ID using SubmitDownloadAsync.
                var reportingDownloadOperation = await ReportingService.SubmitDownloadAsync(reportRequest);
                var requestId = reportingDownloadOperation.RequestId;

                // Given the request ID above, you can resume the workflow and download the report.
                // The report request identifier is valid for two days. 
                // If you do not download the report within two days, you must request the report again.
                OutputStatusMessage("Awaiting Download Results . . .");
                await DownloadResults(requestId, authorizationData);

            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(String.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Reporting service exceptions
            catch (FaultException<Microsoft.BingAds.Reporting.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.Errors.Select(error => String.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.Reporting.ApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.OperationErrors.Select(error => String.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(String.Join("; ", ex.Detail.BatchErrors.Select(error => String.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (ReportingOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the reporting operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (ReportingOperationCouldNotBeCompletedException ex)
            {
                OutputStatusMessage(String.Format("ReportingOperationCouldNotBeCompletedException Message: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        /// <summary>
        /// You can submit a download or upload request and the ReportingServiceManager will automatically
        /// return results. The ReportingServiceManager abstracts the details of checking for result file
        /// completion, and you don't have to write any code for results polling.
        /// </summary>
        /// <param name="reportingDownloadParameters"></param>
        private async Task BackgroundCompletion(ReportingDownloadParameters reportingDownloadParameters)
        {
            // You may optionally cancel the download after a specified time interval. 
            // Pass this object to the DownloadFileAsync operation or specify CancellationToken.None. 
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(36000000);

            var resultFilePath = await ReportingService.DownloadFileAsync(reportingDownloadParameters, tokenSource.Token);
            OutputStatusMessage(String.Format("Download result file: {0}\n", resultFilePath));
        }

        /// <summary>
        /// Submit the download request and then use the ReportingDownloadOperation result to
        /// track status yourself using GetStatusAsync.
        /// </summary>
        /// <param name="reportRequest"></param>
        /// <returns></returns>
        private async Task SubmitAndDownload(ReportRequest reportRequest)
        {
            var reportingDownloadOperation = await ReportingService.SubmitDownloadAsync(reportRequest);
            
            ReportingOperationStatus downloadStatus;
            var waitTime = new TimeSpan(0, 0, 5);

            // This sample polls every 5 seconds up to 2 minutes.

            for (int i = 0; i < 24; i++)
            {
                Thread.Sleep(waitTime);

                downloadStatus = await reportingDownloadOperation.GetStatusAsync();

                if (downloadStatus.Status == ReportRequestStatusType.Success)
                {
                    break;
                }
            }

            var resultFilePath = await reportingDownloadOperation.DownloadResultFileAsync(
                FileDirectory,
                ResultFileName,
                decompress: true,
                overwrite: true);   // Set this value true if you want to overwrite the same file.

            OutputStatusMessage(String.Format("Download result file: {0}\n", resultFilePath));
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
        private async Task DownloadResults(
            string requestId,
            AuthorizationData authorizationData)
        {
            var reportingDownloadOperation = new ReportingDownloadOperation(requestId, authorizationData);

            // Use TrackAsync to indicate that the application should wait to ensure that 
            // the download status is completed.
            var reportingOperationStatus = await reportingDownloadOperation.TrackAsync();

            var resultFilePath = await reportingDownloadOperation.DownloadResultFileAsync(
                FileDirectory,
                ResultFileName,
                decompress: true,
                overwrite: true);   // Set this value true if you want to overwrite the same file.

            OutputStatusMessage(String.Format("Download result file: {0}", resultFilePath));
            OutputStatusMessage(String.Format("Status: {0}", reportingOperationStatus.Status));
            OutputStatusMessage(String.Format("TrackingId: {0}\n", reportingOperationStatus.TrackingId));
        }

        private KeywordPerformanceReportRequest GetKeywordPerformanceReportRequest(long accountId)
        {
            var report = new KeywordPerformanceReportRequest
            {
                Format = ReportFileFormat,
                Language = ReportLanguage.English,
                ReportName = "My Keyword Performance Report",
                ReturnOnlyCompleteData = false,
                Aggregation = ReportAggregation.Daily,

                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },

                Time = new ReportTime
                {
                    // You may either use a custom date range or predefined time.

                    //CustomDateRangeStart = new Date
                    //    {
                    //        Month = DateTime.Now.Month,
                    //        Day = DateTime.Now.Day,
                    //        Year = DateTime.Now.Year - 1
                    //    },
                    //CustomDateRangeEnd = new Date
                    //    {
                    //    Month = DateTime.Now.Month,
                    //    Day = DateTime.Now.Day,
                    //    Year = DateTime.Now.Year
                    //    },

                    CustomDateRangeStart = new Date
                    {
                        Month = 12,
                        Day = 4,
                        Year = DateTime.Now.Year - 5
                    },
                    CustomDateRangeEnd = new Date
                    {
                        Month = 2,
                        Day = 5,
                        Year = DateTime.Now.Year
                    },

                    //PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new KeywordPerformanceReportFilter
                //{
                //    DeviceType = DeviceTypeReportFilter.Computer |
                //                 DeviceTypeReportFilter.SmartPhone
                //},

                // Specify the attribute and data report columns. 
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
                    KeywordPerformanceReportColumn.ExtendedCost,
                    KeywordPerformanceReportColumn.LandingPageRelevance,
                    KeywordPerformanceReportColumn.LandingPageUserExperience,
                    KeywordPerformanceReportColumn.Revenue,
                    KeywordPerformanceReportColumn.Assists,
                    KeywordPerformanceReportColumn.KeywordRelevance,
                    KeywordPerformanceReportColumn.DeliveredMatchType,
                    KeywordPerformanceReportColumn.AveragePosition,
                    KeywordPerformanceReportColumn.Conversions,
                    KeywordPerformanceReportColumn.AdDistribution,
                    KeywordPerformanceReportColumn.Network,
                    KeywordPerformanceReportColumn.AdId,
                    KeywordPerformanceReportColumn.AdType,
                    KeywordPerformanceReportColumn.AdGroupId
                },

                // You may optionally sort by any KeywordPerformanceReportColumn, and optionally
                // specify the maximum number of rows to return in the sorted report. 
                Sort = new[]
                {
                    new KeywordPerformanceReportSort
                        {
                            SortColumn = KeywordPerformanceReportColumn.Clicks,
                            SortOrder = SortOrder.Ascending
                        }
                },

                MaxRows = 10,
            };

            return report;
        }

        private ReportRequest GetCampaignPerformanceReportRequest(long accountId)
        {
            var report = new CampaignPerformanceReportRequest
            {
                Format = ReportFileFormat,
                Language = ReportLanguage.English,
                ReportName = "My Campaign Performance Report",
                ReturnOnlyCompleteData = false,
                Aggregation = ReportAggregation.Daily,

                Scope = new AccountThroughCampaignReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null
                },

                // Alternatively you can request data for a subset of campaigns.
                //Scope = new AccountThroughCampaignReportScope
                //{
                //    AccountIds = null,
                //    Campaigns = new [] {
                //        new CampaignReportScope
                //        {
                //            AccountId = accountId,
                //            CampaignId = <YourCampaignIdGoesHere>
                //        }
                //    }
                //},

                Time = new ReportTime
                {
                    // You may either use a custom date range or predefined time.

                    //CustomDateRangeStart = new Date
                    //    {
                    //        Month = DateTime.Now.Month,
                    //        Day = DateTime.Now.Day,
                    //        Year = DateTime.Now.Year - 1
                    //    },
                    //CustomDateRangeEnd = new Date
                    //    {
                    //    Month = DateTime.Now.Month,
                    //    Day = DateTime.Now.Day,
                    //    Year = DateTime.Now.Year
                    //    },

                    CustomDateRangeStart = new Date
                    {
                        Month = 12,
                        Day = 4,
                        Year = DateTime.Now.Year - 5
                    },
                    CustomDateRangeEnd = new Date
                    {
                        Month = 2,
                        Day = 5,
                        Year = DateTime.Now.Year
                    },

                    //PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new CampaignPerformanceReportFilter
                //{
                //    DeviceType = DeviceTypeReportFilter.Computer |
                //                 DeviceTypeReportFilter.SmartPhone
                //},

                // Specify the attribute and data report columns. 
                Columns = new[]
                {
                    CampaignPerformanceReportColumn.TimePeriod,
                    CampaignPerformanceReportColumn.AccountId,
                    CampaignPerformanceReportColumn.CampaignId,
                    CampaignPerformanceReportColumn.DeviceType,
                    CampaignPerformanceReportColumn.BidMatchType,
                    CampaignPerformanceReportColumn.QualityScore,
                    CampaignPerformanceReportColumn.ExtendedCost,
                    CampaignPerformanceReportColumn.LandingPageRelevance,
                    CampaignPerformanceReportColumn.LandingPageUserExperience,
                    CampaignPerformanceReportColumn.Revenue,
                    CampaignPerformanceReportColumn.Assists,
                    CampaignPerformanceReportColumn.KeywordRelevance,
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
                },

            };

            return report;
        }

        private ReportRequest GetProductDimensionPerformanceReportRequest(long accountId)
        {
            return new ProductDimensionPerformanceReportRequest
            {
                Format = ReportFileFormat,
                ReportName = "My Product Dimension Performance Report",
                ReturnOnlyCompleteData = false,
                Aggregation = ReportAggregation.Daily,

                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },

                Time = new ReportTime
                {
                    // You may either use a custom date range or predefined time.

                    //CustomDateRangeStart = new Date
                    //    {
                    //        Month = DateTime.Now.Month,
                    //        Day = DateTime.Now.Day,
                    //        Year = DateTime.Now.Year - 1
                    //    },
                    //CustomDateRangeEnd = new Date
                    //    {
                    //    Month = DateTime.Now.Month,
                    //    Day = DateTime.Now.Day,
                    //    Year = DateTime.Now.Year
                    //    },

                    PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new CampaignPerformanceReportFilter
                //{
                //    DeviceType = DeviceTypeReportFilter.Computer |
                //                 DeviceTypeReportFilter.SmartPhone
                //},

                // Specify the attribute and data report columns. 
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
                    ProductDimensionPerformanceReportColumn.Clicks,
                    ProductDimensionPerformanceReportColumn.Condition,
                    ProductDimensionPerformanceReportColumn.Ctr,
                    ProductDimensionPerformanceReportColumn.CurrencyCode,
                    ProductDimensionPerformanceReportColumn.CustomLabel0,
                    ProductDimensionPerformanceReportColumn.CustomLabel1,
                    ProductDimensionPerformanceReportColumn.CustomLabel2,
                    ProductDimensionPerformanceReportColumn.CustomLabel3,
                    ProductDimensionPerformanceReportColumn.CustomLabel4,
                    ProductDimensionPerformanceReportColumn.DeviceType,
                    ProductDimensionPerformanceReportColumn.Impressions,
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
                    ProductDimensionPerformanceReportColumn.Spend,
                    ProductDimensionPerformanceReportColumn.Title,
                    ProductDimensionPerformanceReportColumn.Impressions,
                    ProductDimensionPerformanceReportColumn.Clicks,
                    ProductDimensionPerformanceReportColumn.Ctr,
                    ProductDimensionPerformanceReportColumn.AverageCpc,
                    ProductDimensionPerformanceReportColumn.Spend
                },
            };
        }

        private ReportRequest GetProductPartitionPerformanceReportRequest(long accountId)
        {
            return new ProductPartitionPerformanceReportRequest
            {
                Format = ReportFileFormat,
                ReportName = "My Product Partition Performance Report",
                ReturnOnlyCompleteData = false,
                Aggregation = ReportAggregation.Daily,

                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    AdGroups = null,
                    Campaigns = null
                },

                Time = new ReportTime
                {
                    // You may either use a custom date range or predefined time.

                    //CustomDateRangeStart = new Date
                    //    {
                    //        Month = DateTime.Now.Month,
                    //        Day = DateTime.Now.Day,
                    //        Year = DateTime.Now.Year - 1
                    //    },
                    //CustomDateRangeEnd = new Date
                    //    {
                    //    Month = DateTime.Now.Month,
                    //    Day = DateTime.Now.Day,
                    //    Year = DateTime.Now.Year
                    //    },

                    PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new CampaignPerformanceReportFilter
                //{
                //    DeviceType = DeviceTypeReportFilter.Computer |
                //                 DeviceTypeReportFilter.SmartPhone
                //},

                // Specify the attribute and data report columns. 
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

    }
}
