using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.Reporting;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to request and retrieve performance reports
    /// using the ReportingServiceManager class.
    /// </summary>
    public class ReportRequests : ExampleBase
    {
        public static ReportingServiceManager ReportingServiceManager;

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

        /// <summary>
        /// The maximum amount of time (in milliseconds) that you want to wait for the report download.
        /// </summary>
        protected const int TimeoutInMilliseconds = 360000;
        

        public override string Description
        {
            get { return "Report Requests | Reporting V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ReportingServiceManager = new ReportingServiceManager(authorizationData);
                ReportingServiceManager.StatusPollIntervalInMilliseconds = 5000;

                // You can submit one of the example reports, or build your own.

                var reportRequest = GetAccountPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetAdGroupPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetAudiencePerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetBudgetSummaryReportRequest(authorizationData.AccountId);
                //var reportRequest = GetCampaignPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetKeywordPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetProductDimensionPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetProductPartitionPerformanceReportRequest(authorizationData.AccountId);
                //var reportRequest = GetSearchCampaignChangeHistoryReportRequest(authorizationData.AccountId);

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

                OutputStatusMessage("Awaiting Background Completion . . .");
                await BackgroundCompletionAsync(reportingDownloadParameters);

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

            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Reporting service exceptions
            catch (FaultException<Microsoft.BingAds.V11.Reporting.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(String.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.Reporting.ApiFaultDetail> ex)
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
        /// <param name="reportingDownloadParameters"></param>
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
        /// <param name="reportRequest"></param>
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
                FileDirectory,
                ResultFileName,
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

            var reportingDownloadOperation = new ReportingDownloadOperation(requestId, authorizationData);

            // Use TrackAsync to indicate that the application should wait to ensure that 
            // the download status is completed.
            var reportingOperationStatus = await reportingDownloadOperation.TrackAsync(tokenSource.Token);

            var resultFilePath = await reportingDownloadOperation.DownloadResultFileAsync(
                FileDirectory,
                ResultFileName,
                decompress: true,
                overwrite: true);   // Set this value true if you want to overwrite the same file.

            OutputStatusMessage(string.Format("Download result file: {0}", resultFilePath));
            OutputStatusMessage(string.Format("Status: {0}", reportingOperationStatus.Status));
            OutputStatusMessage(string.Format("TrackingId: {0}\n", reportingOperationStatus.TrackingId));
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

                    CustomDateRangeStart = new Date
                    {
                        Month = 1,
                        Day = 1,
                        Year = DateTime.Now.Year - 1
                    },
                    CustomDateRangeEnd = new Date
                    {
                        Month = 12,
                        Day = 31,
                        Year = DateTime.Now.Year - 1
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
                    //{
                    //    Month = 1,
                    //    Day = 1,
                    //    Year = DateTime.Now.Year - 1
                    //},
                    //CustomDateRangeEnd = new Date
                    //{
                    //    Month = 12,
                    //    Day = 31,
                    //    Year = DateTime.Now.Year - 1
                    //},

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
                    CampaignPerformanceReportColumn.TimePeriod,
                    CampaignPerformanceReportColumn.AccountId,
                    CampaignPerformanceReportColumn.CampaignId,
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


        private ReportRequest GetAdGroupPerformanceReportRequest(long accountId)
        {
            var report = new AdGroupPerformanceReportRequest
            {
                Format = ReportFileFormat,
                Language = ReportLanguage.English,
                ReportName = "My Ad Group Performance Report",
                ReturnOnlyCompleteData = false,
                Aggregation = ReportAggregation.Daily,

                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = new[] { accountId },
                    Campaigns = null,
                    AdGroups = null
                },

               // Alternatively you can request data for a subset of campaigns or ad groups.
               //Scope = new AccountThroughAdGroupReportScope
               //{
               //    AccountIds = null,
               //    Campaigns = new[] {
               //         new AdGroupReportScope
               //         {
               //             AccountId = accountId,
               //             CampaignId = <YourCampaignIdGoesHere>,
               //             AdGroupId = <YourAdGroupIdGoesHere>
               //         }
               //    }
               //},

               Time = new ReportTime
                {
                    // You may either use a custom date range or predefined time.

                    //CustomDateRangeStart = new Date
                    //{
                    //    Month = 1,
                    //    Day = 1,
                    //    Year = DateTime.Now.Year - 1
                    //},
                    //CustomDateRangeEnd = new Date
                    //{
                    //    Month = 12,
                    //    Day = 31,
                    //    Year = DateTime.Now.Year - 1
                    //},

                    PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new AdGroupPerformanceReportFilter
                //{
                //    DeviceType = DeviceTypeReportFilter.Computer |
                //                 DeviceTypeReportFilter.SmartPhone
                //},

                // Specify the attribute and data report columns. 
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

        private ReportRequest GetAccountPerformanceReportRequest(long accountId)
        {
            var report = new AccountPerformanceReportRequest
            {
                Format = ReportFormat.Tsv,
                Language = ReportLanguage.English,
                ReportName = "My Account Performance Report",
                ReturnOnlyCompleteData = false,
                Aggregation = ReportAggregation.Weekly,

                Scope = new AccountReportScope
                {
                    AccountIds = new[] { accountId }
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
                //Filter = new AccountPerformanceReportFilter
                //{
                //    DeviceType = DeviceTypeReportFilter.Computer |
                //                 DeviceTypeReportFilter.SmartPhone
                //},

                // Specify the attribute and data report columns. 
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

        private ReportRequest GetBudgetSummaryReportRequest(long accountId)
        {
            var report = new BudgetSummaryReportRequest
            {
                Format = ReportFormat.Csv,
                Language = ReportLanguage.English,
                ReportName = "My Budget Summary Report",
                ReturnOnlyCompleteData = false,
                
                Scope = new AccountThroughCampaignReportScope
                {
                    AccountIds = new[] { accountId }
                },

                Time = new BudgetSummaryReportTime
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

                    PredefinedTime = BudgetSummaryReportTimePeriod.ThisMonth
                },
                
                // Specify the attribute and data report columns. 
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
                    //{
                    //    Month = 1,
                    //    Day = 1,
                    //    Year = DateTime.Now.Year - 1
                    //},
                    //CustomDateRangeEnd = new Date
                    //{
                    //    Month = 12,
                    //    Day = 31,
                    //    Year = DateTime.Now.Year - 1
                    //},

                    PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new ProductDimensionPerformanceReportFilter
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
                    //{
                    //    Month = 1,
                    //    Day = 1,
                    //    Year = DateTime.Now.Year - 1
                    //},
                    //CustomDateRangeEnd = new Date
                    //{
                    //    Month = 12,
                    //    Day = 31,
                    //    Year = DateTime.Now.Year - 1
                    //},

                    PredefinedTime = ReportTimePeriod.Yesterday
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new ProductPartitionPerformanceReportFilter
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

        private ReportRequest GetAudiencePerformanceReportRequest(long accountId)
        {
            return new AudiencePerformanceReportRequest
            {
                Format = ReportFileFormat,
                ReportName = "My Audience Performance Report",
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
                    //{
                    //    Month = 1,
                    //    Day = 1,
                    //    Year = DateTime.Now.Year - 1
                    //},
                    //CustomDateRangeEnd = new Date
                    //{
                    //    Month = 12,
                    //    Day = 31,
                    //    Year = DateTime.Now.Year - 1
                    //},

                    PredefinedTime = ReportTimePeriod.Yesterday
                },

                // Specify the attribute and data report columns. 
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


        private ReportRequest GetSearchCampaignChangeHistoryReportRequest(long accountId)
        {
            var report = new SearchCampaignChangeHistoryReportRequest
            {
                Format = ReportFileFormat,
                Language = ReportLanguage.English,
                ReportName = "My Change History Performance Report",
                ReturnOnlyCompleteData = false,

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
                    //{
                    //    Month = 1,
                    //    Day = 1,
                    //    Year = DateTime.Now.Year - 1
                    //},
                    //CustomDateRangeEnd = new Date
                    //{
                    //    Month = 12,
                    //    Day = 31,
                    //    Year = DateTime.Now.Year - 1
                    //},

                    PredefinedTime = ReportTimePeriod.LastThreeMonths
                },

                // If you specify a filter, results may differ from data you see in the Bing Ads web application
                //Filter = new SearchCampaignChangeHistoryReportFilter
                //{
                //    AdDistribution = AdDistributionReportFilter.Search | 
                //                     AdDistributionReportFilter.Native
                //},

                // Specify the attribute and data report columns. 
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

    }
}
