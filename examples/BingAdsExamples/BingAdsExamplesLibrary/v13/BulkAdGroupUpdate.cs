using System;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;
using System.Threading;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to make Bulk updates across ad groups with the Bulk service.  
    /// </summary>
    public class BulkAdGroupUpdate : BulkExampleBase
    {
        public override string Description
        {
            get { return "Update Ad Groups | Bulk V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                // Used to output the Campaign Management objects within Bulk entities.
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(TimeoutInMilliseconds);
                
                // In this example we will download all ad groups in the account.
                
                var downloadParameters = new DownloadParameters
                {
                    DataScope = DataScope.EntityData,
                    DownloadEntities = new[] { DownloadEntity.AdGroups },
                    FileType = FileType,
                    LastSyncTimeInUTC = null,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true
                };

                OutputStatusMessage("-----\nDownloading all ad groups in the account.");

                var bulkFilePath = await BulkServiceManager.DownloadFileAsync(
                    parameters: downloadParameters,
                    progress: progress,
                    cancellationToken: tokenSource.Token);

                OutputStatusMessage("Download results:");

                Reader = new BulkFileReader(
                    filePath: bulkFilePath,
                    resultFileType: ResultFileType.FullDownload,
                    fileFormat: FileType);

                var bulkAdGroups = Reader.ReadEntities().ToList().OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(bulkAdGroups);

                Writer = new BulkFileWriter(FileDirectory + UploadFileName);

                // We will activate ad groups for one month starting from today as an example.

                var nextMonth = DateTime.UtcNow.AddMonths(1);

                // Within the downloaded records, find all ad groups that you want to update.
                
                foreach (var bulkAdGroup in bulkAdGroups)
                {
                    var adGroup = bulkAdGroup.AdGroup;
                    if (adGroup != null && bulkAdGroup.AdGroup.Status.ToString().CompareTo("Active") != 0)
                    {
                        // For best performance, only upload the properties that you want to update.

                        Writer.WriteEntity(new BulkAdGroup
                        {
                            CampaignId = bulkAdGroup.CampaignId,
                            AdGroup = new AdGroup
                            {
                                Id = adGroup.Id,
                                EndDate = new Microsoft.BingAds.V13.CampaignManagement.Date
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

                OutputStatusMessage("-----\nActivating the ad groups...");

                var resultFilePath = await BulkServiceManager.UploadFileAsync(
                    parameters: fileUploadParameters,
                    progress: progress,
                    cancellationToken: tokenSource.Token);

                Reader = new BulkFileReader(
                    filePath: resultFilePath,
                    resultFileType: ResultFileType.Upload,
                    fileFormat: FileType);

                OutputStatusMessage("Upload results:");

                bulkAdGroups = Reader.ReadEntities().ToList().OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(bulkAdGroups);

                Reader.Dispose();  
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V13.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.Bulk.ApiFaultDetail> ex)
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
