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
    /// This example demonstrates how to add negative keywords using the BulkServiceManager class.
    /// </summary>
    public class BulkNegativeKeywords : BulkExampleBase
    {
        public override string Description
        {
            get { return "Negative Keywords | Bulk V11"; }
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
                        // will be used when associating this new campaign with a new negative keyword in the BulkCampaignNegativeKeyword object below. 
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

                var bulkCampaignNegativeKeywords = new BulkCampaignNegativeKeyword[] {
                    new BulkCampaignNegativeKeyword {
                        CampaignId = campaignIdKey,
                        NegativeKeyword = new NegativeKeyword
                        {
                            MatchType = MatchType.Phrase,
                            Text = "auto",
                        }
                    },
                    new BulkCampaignNegativeKeyword {
                        CampaignId = campaignIdKey,
                        NegativeKeyword = new NegativeKeyword
                        {
                            MatchType = MatchType.Exact,
                            Text = "auto",
                        }
                    },
                    new BulkCampaignNegativeKeyword {
                        CampaignId = campaignIdKey,
                        NegativeKeyword = new NegativeKeyword
                        {
                            MatchType = MatchType.Phrase,
                            Text = "car",
                        }
                    },
                    new BulkCampaignNegativeKeyword {
                        CampaignId = campaignIdKey,
                        NegativeKeyword = new NegativeKeyword
                        {
                            MatchType = MatchType.Exact,
                            Text = "car",
                        }
                    },
                };

                // Negative keywords can be added and deleted from a shared negative keyword list. The negative keyword list can be shared or associated with multiple campaigns.
                // You can create up to 20 negative keyword lists per account and share or associate them with any campaign in the same account. 
                // To create a negative keyword list, upload a BulkNegativeKeywordList (Negative Keyword List record type). 
                // For each negative keyword that you want to add to the list, upload a BulkSharedNegativeKeyword (Shared Negative Keyword record type). 
                // To associate the negative keyword list with a campaign, also upload a BulkCampaignNegativeKeywordList (Campaign Negative Keyword List Association record type). 
                
                var bulkNegativeKeywordList = new BulkNegativeKeywordList
                {
                    NegativeKeywordList = new NegativeKeywordList
                    {
                        // Since we are adding the list and the negative keywords during the same upload, 
                        // we will use a reference key to the negative keyword list identifier.
                        Id = negativeKeywordListIdKey,
                        Name = "My NKW List",
                    },
                };

                var bulkSharedNegativeKeywords = new BulkSharedNegativeKeyword[] {
                    new BulkSharedNegativeKeyword {
                        NegativeKeyword = new NegativeKeyword
                        {
                            MatchType = MatchType.Phrase,
                            Text = "mobile",
                        },
                        NegativeKeywordListId = negativeKeywordListIdKey,
                    },
                    new BulkSharedNegativeKeyword {
                        NegativeKeyword = new NegativeKeyword
                        {
                            MatchType = MatchType.Exact,
                            Text = "mobile",
                        },
                        NegativeKeywordListId = negativeKeywordListIdKey,
                    },
                };

                var bulkCampaignNegativeKeywordList = new BulkCampaignNegativeKeywordList
                {
                    SharedEntityAssociation = new SharedEntityAssociation
                    {
                        EntityId = campaignIdKey,
                        EntityType = "Campaign",
                        SharedEntityId = negativeKeywordListIdKey,
                        SharedEntityType = "NegativeKeywordList",
                    }
                };
                
                var uploadEntities = new List<BulkEntity>();
                uploadEntities.Add(bulkCampaign);

                foreach (var bulkCampaignNegativeKeyword in bulkCampaignNegativeKeywords)
                {
                    uploadEntities.Add(bulkCampaignNegativeKeyword);
                }

                uploadEntities.Add(bulkNegativeKeywordList);
                foreach (var bulkSharedNegativeKeyword in bulkSharedNegativeKeywords)
                {
                    uploadEntities.Add(bulkSharedNegativeKeyword);
                }
                uploadEntities.Add(bulkCampaignNegativeKeywordList);

                // Upload and write the output

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var campaignNegativeKeywordResults = downloadEntities.OfType<BulkCampaignNegativeKeyword>().ToList();
                OutputBulkCampaignNegativeKeywords(campaignNegativeKeywordResults);

                var negativeKeywordListResults = downloadEntities.OfType<BulkNegativeKeywordList>().ToList();
                OutputBulkNegativeKeywordLists(negativeKeywordListResults);

                var sharedNegativeKeywordListResults = downloadEntities.OfType<BulkSharedNegativeKeyword>().ToList();
                OutputBulkSharedNegativeKeywords(sharedNegativeKeywordListResults);

                var campaignNegativeKeywordListResults = downloadEntities.OfType<BulkCampaignNegativeKeywordList>().ToList();
                OutputBulkCampaignNegativeKeywordLists(campaignNegativeKeywordListResults);

                Reader.Dispose();

                #endregion Add

                #region CleanUp

                //Delete the campaign and negative keywords that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                //When you delete a BulkCampaign, the dependent entities such as BulkCampaignNegativeKeyword 
                //are deleted without being specified explicitly.  
                //When you delete a BulkNegativeKeywordList, the dependent entities such as BulkSharedNegativeKeyword 
                //are deleted without being specified explicitly.

                uploadEntities = new List<BulkEntity>();
                
                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                foreach (var negativeKeywordListResult in negativeKeywordListResults)
                {
                    negativeKeywordListResult.Status = Status.Deleted;
                    uploadEntities.Add(negativeKeywordListResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign and negative keywords . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkNegativeKeywordLists(downloadEntities.OfType<BulkNegativeKeywordList>().ToList());
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
