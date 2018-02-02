using System;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.Bulk;
using Microsoft.BingAds.V11.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;
using System.Threading;
using System.Collections.Generic;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates ad group updates using the BulkServiceManager class.
    /// </summary>
    public class BulkAdGroupUpdate : BulkExampleBase
    {
        public override string Description
        {
            get { return "Update Ad Groups with BulkServiceManager | Bulk V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(authorizationData);
                BulkServiceManager.StatusPollIntervalInMilliseconds = 5000;

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                #region Download

                // In this example we will download all ad groups in the account.

                var entities = new[] {
                    DownloadEntity.AdGroups,
                };

                // You can limit by specific campaign IDs and request performance data.

                var downloadParameters = new DownloadParameters
                {
                    CampaignIds = null,
                    DataScope = DataScope.EntityData,
                    PerformanceStatsDateRange = null,
                    DownloadEntities = entities,
                    FileType = FileType,
                    LastSyncTimeInUTC = null,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true
                };
                
                // You can submit a download or upload request and the BulkServiceManager will automatically 
                // return results. The BulkServiceManager abstracts the details of checking for result file 
                // completion, and you don't have to write any code for results polling.

                var bulkFilePath = await BulkServiceManager.DownloadFileAsync(downloadParameters);
                OutputStatusMessage("Downloaded all ad groups in the account.\n");
                
                #endregion Download

                #region Parse

                Reader = new BulkFileReader(bulkFilePath, ResultFileType.FullDownload, FileType);
                var bulkAdGroups = Reader.ReadEntities().ToList().OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(bulkAdGroups);

                Writer = new BulkFileWriter(FileDirectory + UploadFileName);

                // We will activate ad groups for one month starting from today as an example.

                var nextMonth = DateTime.UtcNow.AddMonths(1);

                // Within the downloaded records, find all ad groups that you want to update.
                
                foreach (var bulkAdGroup in bulkAdGroups)
                {
                    var adGroup = bulkAdGroup.AdGroup;
                    if (adGroup != null && bulkAdGroup.IsExpired)
                    {
                        // For best performance, only upload the properties that you want to update.

                        Writer.WriteEntity(new BulkAdGroup
                        {
                            CampaignId = bulkAdGroup.CampaignId,
                            AdGroup = new AdGroup
                            {
                                Id = adGroup.Id,
                                EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                                {
                                    Month = nextMonth.Month,
                                    Day = nextMonth.Day,
                                    Year = nextMonth.Year
                                },
                                Status = AdGroupStatus.Active,
                            }
                        });
                    }
                }

                Reader.Dispose();
                Writer.Dispose();

                #endregion Parse

                #region Upload
                
                // Upload the local file that we already prepared

                var fileUploadParameters = new FileUploadParameters
                {
                    ResultFileDirectory = FileDirectory,
                    CompressUploadFile = true,
                    ResultFileName = ResultFileName,
                    OverwriteResultFile = true,
                    UploadFilePath = FileDirectory + UploadFileName,
                    ResponseMode = ResponseMode.ErrorsAndResults
                };

                var resultFilePath = await BulkServiceManager.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                OutputStatusMessage("Updated ad groups.\n");

                Reader = new BulkFileReader(resultFilePath, ResultFileType.Upload, FileType);
                bulkAdGroups = Reader.ReadEntities().ToList().OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(bulkAdGroups);
                Reader.Dispose();

                #endregion Upload


                #region Entities

                // We can make the same update without explicitly reading or writing a local file.
                // When working with entities a file is downloaded to the temp directory,
                // although you don't need to manage it.

                var downloadEntities = await BulkServiceManager.DownloadEntitiesAsync(downloadParameters);
                OutputStatusMessage("Downloaded all ad groups in the account.\n");
                bulkAdGroups = downloadEntities.ToList().OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(bulkAdGroups);

                var uploadEntities = new List<BulkEntity>();
                
                foreach (var bulkAdGroup in bulkAdGroups)
                {
                    var adGroup = bulkAdGroup.AdGroup;
                    if (adGroup != null && bulkAdGroup.IsExpired)
                    {
                        // Instead of Writer.WriteEntity, we will add to the in-memory list

                        uploadEntities.Add(new BulkAdGroup
                        {
                            CampaignId = bulkAdGroup.CampaignId,
                            AdGroup = new AdGroup
                            {
                                Id = adGroup.Id,
                                EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                                {
                                    Month = nextMonth.Month,
                                    Day = nextMonth.Day,
                                    Year = nextMonth.Year
                                },
                                Status = AdGroupStatus.Active,
                            }
                        });
                    }
                }

                var entityUploadParameters = new EntityUploadParameters
                {
                    Entities = uploadEntities,
                    ResponseMode = ResponseMode.ErrorsAndResults,
                };

                var resultEntities = await BulkServiceManager.UploadEntitiesAsync(entityUploadParameters, progress, CancellationToken.None);
                OutputStatusMessage("Updated ad groups.\n");

                bulkAdGroups = resultEntities.ToList().OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(bulkAdGroups);

                #endregion Entities

            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V11.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
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
    }
}
