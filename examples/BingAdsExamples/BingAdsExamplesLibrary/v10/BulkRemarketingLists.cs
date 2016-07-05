using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V10.Bulk;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add ads and keywords to a new ad group using the BulkServiceManager class.
    /// </summary>
    public class BulkRemarketingLists : BulkExampleBase
    {
        public override string Description
        {
            get { return "Bulk Remarketing List Associations | Bulk V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(String.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var downloadParameters = new DownloadParameters
                {
                    Entities = BulkDownloadEntity.RemarketingLists,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true,
                    LastSyncTimeInUTC = null
                };

                var bulkFilePath = await BulkService.DownloadFileAsync(downloadParameters);
                OutputStatusMessage("Downloaded all remarketing lists that the current user can associate with ad groups.\n");
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.FullDownload, FileType);
                var downloadEntities = Reader.ReadEntities().ToList();

                var remarketingListResults = downloadEntities.OfType<BulkRemarketingList>().ToList();
                OutputBulkRemarketingLists(remarketingListResults);

                Reader.Dispose();

                // You must already have at least one remarketing list. The Bing Ads API does not support
                // remarketing list add, update, or delete operations.
                if (remarketingListResults.Count < 1)
                {
                    OutputStatusMessage("You do not have any remarketing lists that the current user can associate with ad groups.\n");
                    return;
                }

                var uploadEntities = new List<BulkEntity>();

                #region Add

                // Prepare the bulk entities that you want to upload.  

                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        BiddingScheme = new ManualCpcBiddingScheme(),
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // DaylightSaving is not supported in the Bulk file schema. Whether or not you specify it in a BulkCampaign,
                        // the value is not written to the Bulk file, and by default DaylightSaving is set to true.
                        DaylightSaving = true,

                        TrackingUrlTemplate = null
                    }
                };

                // Specify one or more ad groups.

                var bulkAdGroup = new BulkAdGroup
                {
                    // ClientId may be used to associate records in the bulk upload file with records in the results file. The value of this field 
                    // is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record.
                    // Note: This bulk file Client Id is not related to an application Client Id for OAuth. 
                    ClientId = "YourClientIdGoesHere",
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        // When using the Campaign Management service, the Id cannot be set. In the context of a BulkAdGroup, the Id is optional 
                        // and may be used as a negative reference key during bulk upload. For example the same negative value set for the  
                        // ad group Id will be used when associating this new ad group with a new ad group remarketing list association
                        // in the BulkAdGroupRemarketingList object below. 
                        Id = adGroupIdKey,
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        BiddingModel = BiddingModel.Keyword,
                        BiddingScheme = new InheritFromParentBiddingScheme(),
                        PricingModel = PricingModel.Cpc,
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        Language = "English",
                        Status = AdGroupStatus.Active,
                        TrackingUrlTemplate = null,

                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        RemarketingTargetingSetting = RemarketingTargetingSetting.TargetAndBid,
                    },
                };

                uploadEntities.Add(bulkCampaign);
                uploadEntities.Add(bulkAdGroup);

                // This example associates all of the remarketing lists with the new ad group.

                foreach (var remarketingList in remarketingListResults)
                {
                    if (remarketingList.RemarketingList != null && remarketingList.RemarketingList.Id != null)
                    {
                        var bulkAdGroupRemarketingList = new BulkAdGroupRemarketingList
                        {
                            ClientId = "MyBulkAdGroupRemarketingList " + remarketingList.RemarketingList.Id,
                            AdGroupRemarketingListAssociation = new AdGroupRemarketingListAssociation
                            {
                                AdGroupId = adGroupIdKey,
                                BidAdjustment = 20.00,
                                RemarketingListId = (long)remarketingList.RemarketingList.Id,
                                Status = AdGroupRemarketingListAssociationStatus.Paused
                            },
                        };

                        uploadEntities.Add(bulkAdGroupRemarketingList);
                    }
                }

                // Upload and write the output

                OutputStatusMessage("\nAdding campaign, ad group, and ad group remarketing list associations...\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var adGroupRemarketingListResults = downloadEntities.OfType<BulkAdGroupRemarketingList>().ToList();
                OutputBulkAdGroupRemarketingLists(adGroupRemarketingListResults);

                Reader.Dispose();

                #endregion Add

                #region CleanUp

                // Delete the campaign, ad group, and ad group remarketing list associations that were previously added.
                // The remarketing lists will not be deleted. 
                // You should remove this region if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                // You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                // When you delete a BulkCampaign, the dependent entities such as BulkAdGroup and BulkAdGroupRemarketingList 
                // are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign, ad group, and ad group remarketing list associations . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());

                Reader.Dispose();

                #endregion Cleanup
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V10.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.Bulk.ApiFaultDetail> ex)
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
