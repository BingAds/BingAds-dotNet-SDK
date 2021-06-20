using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to add negative sites with the Bulk service.
    /// </summary>
    public class BulkNegativeSites : BulkExampleBase
    {
        public override string Description
        {
            get { return "Negative Sites | Bulk V13"; }
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
                
                var uploadEntities = new List<BulkEntity>();

                // Define a campaign 

                var bulkCampaign = new BulkCampaign
                {
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };

                uploadEntities.Add(bulkCampaign);

                // Define a set of negative sites that can be applied to the campaign.
                // You can set one negative site via the BulkCampaignNegativeSite (singular) bulk entity, 
                // or multiple negative sites via the BulkCampaignNegativeSites (plural) bulk entity.
                //
                // If you upload a BulkCampaignNegativeSites bulk entity, then you are effectively replacing any existing 
                // negative sites assigned to the campaign. 
                // 
                // When the SDK writes a BulkCampaignNegativeSites entity to the bulk upload file, 
                // an extra Campaign Negative Site record is included where the Status is Deleted and the 
                // Website field is empty. (This is the record that deletes any existing campaign negative sites.)
                // 
                // If you include additional BulkCampaignNegativeSite or BulkCampaignNegativeSites in the same upload, 
                // they will also be included in the set of negative sites applied to the campaign.

                var bulkCampaignNegativeSite = new BulkCampaignNegativeSite[] {
                    new BulkCampaignNegativeSite {
                        CampaignId = campaignIdKey,
                        Website = "contoso.com/negativesite1"
                    },
                    new BulkCampaignNegativeSite {
                        CampaignId = campaignIdKey,
                        Website = "contoso.com/negativesite2"
                    },
                };

                foreach (var campaignNegativeSite in bulkCampaignNegativeSite)
                {
                    uploadEntities.Add(campaignNegativeSite);
                }

                var bulkCampaignNegativeSites = new BulkCampaignNegativeSites {
                    CampaignNegativeSites = new CampaignNegativeSites
                    {
                        CampaignId = campaignIdKey,
                        NegativeSites = new string[]
                        {
                            "contoso.com/negativesite3",
                            "contoso.com/negativesite4",
                        }
                    }
                };

                uploadEntities.Add(bulkCampaignNegativeSites);
                                
                // Upload and write the output

                OutputStatusMessage("-----\nApplying negative sites to a new campaign...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                // If the upload result file contains a Campaign Negative Site record where the Status is Deleted  
                // and the Website field is empty, the SDK represents all negative sites for the campaign 
                // via a BulkCampaignNegativeSites (plural) object. Otherwise the SDK represents negative sites 
                // for the campaign via one or more BulkCampaignNegativeSite (singlular) objects.

                var campaignNegativeSiteResults = downloadEntities.OfType<BulkCampaignNegativeSite>().ToList();
                OutputBulkCampaignNegativeSite(campaignNegativeSiteResults);
                
                var campaignNegativeSitesResults = downloadEntities.OfType<BulkCampaignNegativeSites>().ToList();
                OutputBulkCampaignNegativeSites(campaignNegativeSitesResults);

                Reader.Dispose();

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("-----\nDeleting the campaign and everything it contains e.g., ad groups and ads...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkNegativeKeywordLists(downloadEntities.OfType<BulkNegativeKeywordList>().ToList());

                Reader.Dispose();
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
