using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to associate negative keywords and negative keyword lists with a campaign.
    /// </summary>
    public class NegativeKeywords : ExampleBase
    {
        public override string Description
        {
            get { return "Negative Keywords | Campaign Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                // Add a campaign that will later be associated with negative keywords. 

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
                };

                OutputStatusMessage("-----\nAddCampaigns:");
                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaigns: campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("CampaignIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                long campaignId = (long)campaignIds[0];

                // You may choose to associate an exclusive set of negative keywords to an individual campaign 
                // or ad group. An exclusive set of negative keywords cannot be shared with other campaigns 
                // or ad groups. This example only associates negative keywords with a campaign.

                var entityNegativeKeywords = new[]
                {
                    new EntityNegativeKeyword
                    {
                        EntityId = campaignId,
                        EntityType = "Campaign",
                        NegativeKeywords = new[]
                        {
                            new NegativeKeyword
                            {
                                MatchType = MatchType.Phrase,
                                Text = "auto"
                            },
                            new NegativeKeyword
                            {
                                MatchType = MatchType.Exact,
                                Text = "auto"
                            },
                        }
                    }
                };

                OutputStatusMessage("-----\nAddNegativeKeywordsToEntities:");
                AddNegativeKeywordsToEntitiesResponse addNegativeKeywordsToEntitiesResponse =
                    await CampaignManagementExampleHelper.AddNegativeKeywordsToEntitiesAsync(
                        entityNegativeKeywords: entityNegativeKeywords);
                OutputStatusMessage("Added an exclusive set of negative keywords to the Campaign");
                OutputStatusMessage("NegativeKeywordIds:");
                CampaignManagementExampleHelper.OutputArrayOfIdCollection(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                OutputStatusMessage("NestedPartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);

                // If you attempt to delete a negative keyword without an identifier the operation will return
                // partial errors corresponding to the index of the negative keyword that was not deleted. 

                OutputStatusMessage("-----\nDeleteNegativeKeywordsFromEntities:");
                BatchErrorCollection[] nestedPartialErrors = (await CampaignManagementExampleHelper.DeleteNegativeKeywordsFromEntitiesAsync(
                    entityNegativeKeywords: entityNegativeKeywords)).NestedPartialErrors.ToArray();
                OutputStatusMessage("Attempt to DeleteNegativeKeywordsFromEntities without NegativeKeyword identifier partially fails by design.");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(nestedPartialErrors);

                // Negative keywords can also be added and deleted from a shared negative keyword list. 
                // The negative keyword list can be shared or associated with multiple campaigns.
                // NegativeKeywordList inherits from SharedList which inherits from SharedEntity.

                var negativeKeywordList = new NegativeKeywordList
                {
                    Name = "My Negative Keyword List" + DateTime.UtcNow,
                    Type = "NegativeKeywordList"
                };

                SharedListItem[] negativeKeywords =
                {
                    new NegativeKeyword
                    {
                        Text = "car",
                        Type = "NegativeKeyword",
                        MatchType = MatchType.Exact
                    },
                    new NegativeKeyword
                    {
                        Text = "car",
                        Type = "NegativeKeyword",
                        MatchType = MatchType.Phrase
                    }
                };

                // Add a negative keyword list that can be shared.

                OutputStatusMessage("-----\nAddSharedEntity:");
                var addSharedEntityResponse = await CampaignManagementExampleHelper.AddSharedEntityAsync(
                    sharedEntity: negativeKeywordList, 
                    listItems: negativeKeywords,
                    sharedEntityScope: EntityScope.Account);
                var sharedEntityId = addSharedEntityResponse.SharedEntityId;
                long[] listItemIds = addSharedEntityResponse.ListItemIds.ToArray();

                OutputStatusMessage(string.Format(
                    "NegativeKeywordList added to account library and assigned identifer {0}",
                    sharedEntityId)
                );
                
                // Negative keywords were added to the negative keyword list above. You can associate the 
                // shared list of negative keywords with a campaign with or without negative keywords. 
                // Shared negative keyword lists cannot be associated with an ad group. An ad group can only 
                // be assigned an exclusive set of negative keywords. 

                var associations = new[]
                {
                    new SharedEntityAssociation
                    {
                        EntityId = campaignId,
                        EntityType = "Campaign",
                        SharedEntityId = sharedEntityId,
                        SharedEntityType = "NegativeKeywordList"
                    }
                };

                OutputStatusMessage("-----\nSetSharedEntityAssociations:");
                var partialErrors = (await CampaignManagementExampleHelper.SetSharedEntityAssociationsAsync(
                    associations: associations,
                    sharedEntityScope: EntityScope.Account)).PartialErrors;
                OutputStatusMessage(string.Format(
                    "Associated CampaignId {0} with Negative Keyword List Id {1}.",
                    campaignId, sharedEntityId)
                );
                                
                // Delete the campaign and everything it contains e.g., ad groups and ads.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}", campaignIds[0]));

                // DeleteCampaigns does not delete the negative keyword list from the account's library. 
                // Call the DeleteSharedEntities operation to delete the negative keyword list.

                OutputStatusMessage("-----\nDeleteSharedEntities:");
                partialErrors = (await CampaignManagementExampleHelper.DeleteSharedEntitiesAsync(
                    sharedEntities: new SharedEntity[] { new NegativeKeywordList { Id = sharedEntityId } },
                    sharedEntityScope: EntityScope.Account))?.PartialErrors;
                OutputStatusMessage(string.Format("Deleted Negative Keyword List Id {0}", sharedEntityId));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
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
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }
    }
}
