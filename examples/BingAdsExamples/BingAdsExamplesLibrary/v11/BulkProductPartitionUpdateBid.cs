using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.Bulk;
using Microsoft.BingAds.V11.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to apply product conditions for Bing Shopping Campaigns
    /// using the BulkServiceManager class.
    /// </summary>
    public class BulkProductPartitionUpdateBid : BulkExampleBase
    {
        public override string Description
        {
            get { return "Product Partition Bid Update | Bulk V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkServiceManager = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var downloadParameters = new DownloadParameters
                {
                    DownloadEntities = new[] { DownloadEntity.AdGroupProductPartitions },
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true,
                    LastSyncTimeInUTC = null
                };

                // Download all product partitions across all ad groups in the account.
                var bulkFilePath = await BulkServiceManager.DownloadFileAsync(downloadParameters);
                OutputStatusMessage("Downloaded all product partitions across all ad groups in the account.\n");
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.FullDownload, FileType);
                var downloadEntities = Reader.ReadEntities().ToList().OfType<BulkAdGroupProductPartition>().ToList();
                OutputBulkAdGroupProductPartitions(downloadEntities);
                
                var uploadEntities = new List<BulkEntity>();

                // Within the downloaded records, find all product partition leaf nodes that have bids.
                foreach (var entity in downloadEntities)
                {
                    var biddableAdGroupCriterion = ((BulkAdGroupProductPartition)entity).AdGroupCriterion as BiddableAdGroupCriterion;
                    if (biddableAdGroupCriterion != null &&
                        (((ProductPartition)(entity.AdGroupCriterion.Criterion)).PartitionType == ProductPartitionType.Unit))
                    {
                        // Increase all bids by some predetermined amount or percentage. 
                        // Implement your own logic to update bids by varying amounts.
                        ((FixedBid)((BiddableAdGroupCriterion)((BulkAdGroupProductPartition)entity).AdGroupCriterion).CriterionBid).Amount += .01;
                        uploadEntities.Add(entity);
                    }
                }

                Reader.Dispose();
                
                if (uploadEntities.Count > 0)
                {
                    OutputStatusMessage("Changed local bid of all product partitions. Starting upload.\n");

                    Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                    downloadEntities = Reader.ReadEntities().ToList().OfType<BulkAdGroupProductPartition>().ToList();
                    OutputBulkAdGroupProductPartitions(downloadEntities);
                    Reader.Dispose();
                }
                else
                {
                    OutputStatusMessage("No product partitions in account. \n");
                }

                OutputStatusMessage("Program execution completed\n");

            }
            // Catch Microsoft Account authorization exceptions.
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
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.ApiFaultDetail> ex)
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
