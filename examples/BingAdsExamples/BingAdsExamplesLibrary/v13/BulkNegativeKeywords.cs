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
    /// How to add negative keywords with the Bulk service.
    /// </summary>
    public class BulkNegativeKeywords : BulkExampleBase
    {
        public override string Description
        {
            get { return "Negative Keywords | Bulk V13"; }
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

                // Define a set of negative keywords that can be applied to the campaign.

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

                foreach (var bulkCampaignNegativeKeyword in bulkCampaignNegativeKeywords)
                {
                    uploadEntities.Add(bulkCampaignNegativeKeyword);
                }

                // Negative keywords can also be added and deleted from a shared negative keyword list. 
                // The negative keyword list can be shared or associated with multiple campaigns. 

                // To create a negative keyword list, upload a BulkNegativeKeywordList (Negative Keyword List record type). 

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

                uploadEntities.Add(bulkNegativeKeywordList);

                // For each negative keyword that you want to add to the list, 
                // upload a BulkSharedNegativeKeyword (Shared Negative Keyword record type). 

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

                foreach (var bulkSharedNegativeKeyword in bulkSharedNegativeKeywords)
                {
                    uploadEntities.Add(bulkSharedNegativeKeyword);
                }

                // To associate the negative keyword list with a campaign, 
                // also upload a BulkCampaignNegativeKeywordList (Campaign Negative Keyword List Association record type). 

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
                
                uploadEntities.Add(bulkCampaignNegativeKeywordList);

                // Upload and write the output

                OutputStatusMessage("-----\nAdding the campaign and negative keywords...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

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
                
                // Delete the campaign and everything it contains e.g., ad groups and ads.
                // Delete the account's shared negative keyword list. 

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
