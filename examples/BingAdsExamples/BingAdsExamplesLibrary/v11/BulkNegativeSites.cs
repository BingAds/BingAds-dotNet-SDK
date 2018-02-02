using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.Bulk;
using Microsoft.BingAds.V11.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to add negative sites using the BulkServiceManager class.
    /// </summary>
    public class BulkNegativeSites : BulkExampleBase
    {
        public override string Description
        {
            get { return "Negative Sites | Bulk V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                #region Add

                // Prepare the bulk entities that you want to upload. Each bulk entity contains the corresponding campaign management object, 
                // and additional elements needed to read from and write to a bulk file. 
                
                var bulkCampaign = new BulkCampaign
                {
                    // ClientId may be used to associate records in the bulk upload file with records in the results file. The value of this field 
                    // is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record.
                    // Note: This bulk file Client Id is not related to an application Client Id for OAuth. 
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        // When using the Campaign Management service, the Id cannot be set. In the context of a BulkCampaign, the Id is optional 
                        // and may be used as a negative reference key during bulk upload. For example the same negative value set for the campaign Id 
                        // will be used when associating this new campaign with a new call ad extension in the BulkCampaignCallAdExtension object below. 
                        Id = campaignIdKey,
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        BiddingScheme = new EnhancedCpcBiddingScheme(),

                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };

                // You can specify one negative site per BulkCampaignNegativeSite (singular), or multiple negative sites
                // in a BulkCampaignNegativeSites (plural) object.

                var bulkCampaignNegativeSite = new BulkCampaignNegativeSite[] {
                    new BulkCampaignNegativeSite {
                        // CampaignName will be ignored if you specify CampaignId.
                        CampaignName = null,
                        CampaignId = campaignIdKey,
                        Website = "contoso.com/negativesite1"
                    },
                    new BulkCampaignNegativeSite {
                        CampaignId = campaignIdKey,
                        Website = "contoso.com/negativesite2"
                    },
                };

                // If you upload a BulkCampaignNegativeSites bulk entity, then you are effectively replacing any existing 
                // negative sites assigned to the campaign. Thus, when a BulkCampaignNegativeSites entity is written to the 
                // upload file, an extra Campaign Negative Site record is included where the Status is Deleted and the 
                // Website field is empty. 
                // That said, if you include additional BulkCampaignNegativeSite or BulkCampaignNegativeSites in the same upload, 
                // they will be included in the new set of negative sites.
                var bulkCampaignNegativeSites = new BulkCampaignNegativeSites[] {
                    new BulkCampaignNegativeSites {
                        // CampaignName will be ignored if you specify CampaignId.
                        CampaignName = null,
                        CampaignNegativeSites = new CampaignNegativeSites
                        {
                           CampaignId = campaignIdKey,
                           NegativeSites = new string[]
                           {
                               "contoso.com/negativesite3",
                               "contoso.com/negativesite4",
                           }
                        }
                    },
                };

                var uploadEntities = new List<BulkEntity>();
                uploadEntities.Add(bulkCampaign);

                foreach (var campaignNegativeSite in bulkCampaignNegativeSite)
                {
                    uploadEntities.Add(campaignNegativeSite);
                }

                foreach (var campaignNegativeSites in bulkCampaignNegativeSites)
                {
                    uploadEntities.Add(campaignNegativeSites);
                }

                // Upload and write the output

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                // If you modify the sample to upload only BulkCampaignNegativeSite entities for a campaign, the SDK will abstract  
                // the results file contents as one or more BulkCampaignNegativeSite. If you upload both BulkCampaignNegativeSite 
                // and BulkCampaignNegativeSites as shown above, then the SDK will abstract the results file contents as a 
                // BulkCampaignNegativeSites object containing all of the negative sites for the campaign, 
                // including those uploaded as a BulkCampaignNegativeSite. 

                // Whether you use the SDK to upload the entities, or only use the SDK to read an upload results file,
                // the SDK will abstract the results file as follows:
                // If the file contains an extra Campaign Negative Site record where the Status is Deleted and the 
                // Website field is empty, the SDK returns a BulkCampaignNegativeSites (plural) object.
                // Otherwise the SDK returns one or more BulkCampaignNegativeSite (singlular) objects.

                var campaignNegativeSiteResults = downloadEntities.OfType<BulkCampaignNegativeSite>().ToList();
                OutputBulkCampaignNegativeSite(campaignNegativeSiteResults);
                
                var campaignNegativeSitesResults = downloadEntities.OfType<BulkCampaignNegativeSites>().ToList();
                OutputBulkCampaignNegativeSites(campaignNegativeSitesResults);

                Reader.Dispose();

                #endregion Add

                #region CleanUp

                //Delete the campaign and negative sites that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                //When you delete a BulkCampaign, the dependent entities such as BulkCampaignNegativeSite 
                //are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign and negative sites . . .\n");

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
            finally
            {
                if (Reader != null) { Reader.Dispose(); }
                if (Writer != null) { Writer.Dispose(); }
            }
        }


    }
}
