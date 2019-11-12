using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to update product partitions for Microsoft Shopping Campaigns with the Bulk service.
    /// </summary>
    public class BulkProductPartitionUpdateBid : BulkExampleBase
    {
        public override string Description
        {
            get { return "Product Partition Bid Update | Bulk V13"; }
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

                // In this example we will download all product partitions across all ad groups in the account.

                var downloadParameters = new DownloadParameters
                {
                    DownloadEntities = new[] { DownloadEntity.AdGroupProductPartitions },
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true,
                    LastSyncTimeInUTC = null
                };

                OutputStatusMessage("-----\nDownloading all product partitions across all ad groups in the account.");

                var bulkFilePath = await BulkServiceManager.DownloadFileAsync(
                    parameters: downloadParameters,
                    progress: progress,
                    cancellationToken: tokenSource.Token);

                OutputStatusMessage("Download results:");

                Reader = new BulkFileReader(
                    filePath: bulkFilePath,
                    resultFileType: ResultFileType.FullDownload,
                    fileFormat: FileType);
                
                var bulkAdGroupProductPartitions = Reader.ReadEntities().ToList().OfType<BulkAdGroupProductPartition>().ToList();
                OutputBulkAdGroupProductPartitions(bulkAdGroupProductPartitions);
                
                var uploadEntities = new List<BulkEntity>();

                // Within the downloaded records, find all product partition leaf nodes that have bids.

                foreach (var bulkAdGroupProductPartition in bulkAdGroupProductPartitions)
                {
                    var biddableAdGroupCriterion = (bulkAdGroupProductPartition).AdGroupCriterion as BiddableAdGroupCriterion;
                    if (biddableAdGroupCriterion != null && 
                        (((ProductPartition)biddableAdGroupCriterion.Criterion).PartitionType == ProductPartitionType.Unit))
                    {
                        // For example, let's increase all bids by some predetermined amount.
                        // For best performance, only upload the properties that you want to update e.g.,
                        // create a new BulkAdGroupProductPartition and only set the required properties. 

                        uploadEntities.Add(new BulkAdGroupProductPartition
                        {
                            AdGroupCriterion = new BiddableAdGroupCriterion
                            {
                                AdGroupId = bulkAdGroupProductPartition.AdGroupCriterion.AdGroupId,
                                CriterionBid = new FixedBid
                                {
                                    Amount = ((FixedBid)biddableAdGroupCriterion.CriterionBid).Amount + 0.01
                                },
                                Id = bulkAdGroupProductPartition.AdGroupCriterion.Id,
                            }
                        });
                    }
                }

                Reader.Dispose();
                
                if (uploadEntities.Count > 0)
                {
                    OutputStatusMessage("Changed local bid of all product partitions. Starting upload.\n");

                    Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                    bulkAdGroupProductPartitions = Reader.ReadEntities().ToList().OfType<BulkAdGroupProductPartition>().ToList();
                    OutputBulkAdGroupProductPartitions(bulkAdGroupProductPartitions);
                    Reader.Dispose();
                }
                else
                {
                    OutputStatusMessage("No product partitions in the account. \n");
                }

                OutputStatusMessage("Program execution completed\n");

            }
            // Catch Microsoft Account authorization exceptions.
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
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.ApiFaultDetail> ex)
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
            finally
            {
                if (Reader != null) { Reader.Dispose(); }
                if (Writer != null) { Writer.Dispose(); }
            }
        }
    }
}
