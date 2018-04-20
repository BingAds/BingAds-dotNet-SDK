using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to associate negative keywords and negative keyword lists with a campaign.
    /// </summary>
    public class NegativeKeywords : ExampleBase
    {
        public override string Description
        {
            get { return "Negative Keywords | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Add a campaign that will later be associated with negative keywords. 

                var campaigns = new[]{
                    new Campaign
                    {
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

                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
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
                                MatchType = MatchType.Phrase,
                                Text = "auto"
                            },
                        }
                    }
                };

                AddNegativeKeywordsToEntitiesResponse addNegativeKeywordsToEntitiesResponse =
                    await CampaignManagementExampleHelper.AddNegativeKeywordsToEntitiesAsync(entityNegativeKeywords);
                CampaignManagementExampleHelper.OutputArrayOfIdCollection(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);
                if (addNegativeKeywordsToEntitiesResponse.NestedPartialErrors == null
                    || addNegativeKeywordsToEntitiesResponse.NestedPartialErrors.Count == 0)
                {
                    OutputStatusMessage("Added an exclusive set of negative keywords to the Campaign.\n");
                    CampaignManagementExampleHelper.OutputArrayOfIdCollection(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);
                }

                GetNegativeKeywordsByEntityIdsResponse getNegativeKeywordsByEntityIdsResponse =
                    await CampaignManagementExampleHelper.GetNegativeKeywordsByEntityIdsAsync(new[] { campaignId }, "Campaign", authorizationData.AccountId);
                CampaignManagementExampleHelper.OutputArrayOfEntityNegativeKeyword(getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(getNegativeKeywordsByEntityIdsResponse.PartialErrors);
                if (getNegativeKeywordsByEntityIdsResponse.PartialErrors == null
                    || getNegativeKeywordsByEntityIdsResponse.PartialErrors.Count == 0)
                {
                    OutputStatusMessage("Retrieved an exclusive set of negative keywords for the Campaign.\n");
                    CampaignManagementExampleHelper.OutputArrayOfEntityNegativeKeyword(getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchError(getNegativeKeywordsByEntityIdsResponse.PartialErrors);
                }

                // If you attempt to delete a negative keyword without an identifier the operation will
                // succeed but will return partial errors corresponding to the index of the negative keyword
                // that was not deleted. 
                var nestedPartialErrors = (BatchErrorCollection[])(await CampaignManagementExampleHelper.DeleteNegativeKeywordsFromEntitiesAsync(entityNegativeKeywords)).NestedPartialErrors;
                if (nestedPartialErrors == null || nestedPartialErrors.Length == 0)
                {
                    OutputStatusMessage("Deleted an exclusive set of negative keywords from the Campaign.\n");
                }
                else
                {
                    OutputStatusMessage("Attempt to DeleteNegativeKeywordsFromEntities without NegativeKeyword identifier partially fails by design.");
                    CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(nestedPartialErrors);
                }

                // Delete the negative keywords with identifiers that were returned above.
                nestedPartialErrors = (BatchErrorCollection[])(await CampaignManagementExampleHelper.DeleteNegativeKeywordsFromEntitiesAsync(
                    getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords)).NestedPartialErrors;
                if (nestedPartialErrors == null || nestedPartialErrors.Length == 0)
                {
                    OutputStatusMessage("Deleted an exclusive set of negative keywords from the Campaign.\n");
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(nestedPartialErrors);
                }

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

                // You can create a new list for negative keywords with or without negative keywords.

                var addSharedEntityResponse = await CampaignManagementExampleHelper.AddSharedEntityAsync(negativeKeywordList, negativeKeywords);
                var sharedEntityId = addSharedEntityResponse.SharedEntityId;
                long[] listItemIds = addSharedEntityResponse.ListItemIds.ToArray();

                OutputStatusMessage(string.Format("NegativeKeywordList successfully added to account library and assigned identifer {0}\n", sharedEntityId));

                negativeKeywordList.Id = addSharedEntityResponse.SharedEntityId;
                CampaignManagementExampleHelper.OutputSharedEntity(negativeKeywordList);
                CampaignManagementExampleHelper.OutputArrayOfSharedListItem(negativeKeywords.ToList());
                CampaignManagementExampleHelper.OutputArrayOfBatchError(addSharedEntityResponse.PartialErrors);

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])(await CampaignManagementExampleHelper.GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId })).ListItems;
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfNegativeKeyword(negativeKeywords.Cast<NegativeKeyword>().ToList());
                }

                // To update the list of negative keywords, you must either add or remove from the list
                // using the respective AddListItemsToSharedList or DeleteListItemsFromSharedList operations.
                // To remove the negative keywords from the list pass the negative keyword identifers
                // and negative keyword list (SharedEntity) identifer.

                var partialErrors = (await CampaignManagementExampleHelper.DeleteListItemsFromSharedListAsync(listItemIds, new NegativeKeywordList { Id = sharedEntityId }))?.PartialErrors;
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage("Deleted most recently added negative keywords from negative keyword list.\n");

                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchError(partialErrors);
                }

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])(await CampaignManagementExampleHelper.GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId })).ListItems;
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfNegativeKeyword(negativeKeywords.Cast<NegativeKeyword>().ToList());
                }

                // Whether you created the list with or without negative keywords, more can be added 
                // using the AddListItemsToSharedList operation.

                negativeKeywords = new SharedListItem[]
                {
                    new NegativeKeyword
                    {
                        Text = "auto",
                        Type = "NegativeKeyword",
                        MatchType = MatchType.Exact
                    },
                    new NegativeKeyword
                    {
                        Text = "auto",
                        Type = "NegativeKeyword",
                        MatchType = MatchType.Phrase
                    }
                };

                var addListItemsToSharedListResponse = await CampaignManagementExampleHelper.AddListItemsToSharedListAsync(
                    negativeKeywords,
                    new NegativeKeywordList { Id = sharedEntityId });
                listItemIds = addListItemsToSharedListResponse.ListItemIds.ToArray();

                CampaignManagementExampleHelper.OutputArrayOfSharedListItem(negativeKeywords.ToList());
                CampaignManagementExampleHelper.OutputArrayOfBatchError(addListItemsToSharedListResponse.PartialErrors);

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])(await CampaignManagementExampleHelper.GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId })).ListItems;
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfNegativeKeyword(negativeKeywords.Cast<NegativeKeyword>().ToList());
                }

                // You can update the name of the negative keyword list. 

                negativeKeywordList = new NegativeKeywordList
                {
                    Id = sharedEntityId,
                    Name = "My Updated Negative Keyword List",
                    Type = "NegativeKeywordList"
                };

                partialErrors = (await CampaignManagementExampleHelper.UpdateSharedEntitiesAsync(new SharedEntity[] { negativeKeywordList })).PartialErrors;
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(string.Format("Updated Negative Keyword List Name to {0}.\n", negativeKeywordList.Name));
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchError(partialErrors);
                }

                // Get and output the negative keyword lists and store the list of identifiers.

                const string sharedEntityType = "NegativeKeywordList";
                var sharedEntities = (await CampaignManagementExampleHelper.GetSharedEntitiesByAccountIdAsync(sharedEntityType)).SharedEntities;
                CampaignManagementExampleHelper.OutputArrayOfSharedEntity(sharedEntities);
                var sharedEntityIds = new long[sharedEntities.Count];
                for (int index = 0; index < sharedEntities.Count; index++)
                {
                    if (sharedEntities[index].Id != null)
                    {
                        sharedEntityIds[index] = (long)sharedEntities[index].Id;
                    }
                }

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

                partialErrors = (await CampaignManagementExampleHelper.SetSharedEntityAssociationsAsync(associations)).PartialErrors;
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(string.Format("Associated CampaignId {0} with Negative Keyword List Id {1}.\n",
                        campaignId, sharedEntityId));
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchError(partialErrors);
                }

                // Get and output the associations either by Campaign or NegativeKeywordList identifier.
                GetSharedEntityAssociationsByEntityIdsResponse getSharedEntityAssociationsByEntityIdsResponse =
                    await CampaignManagementExampleHelper.GetSharedEntityAssociationsByEntityIdsAsync(new[] { campaignId }, "Campaign", "NegativeKeywordList");
                CampaignManagementExampleHelper.OutputArrayOfSharedEntityAssociation(getSharedEntityAssociationsByEntityIdsResponse.Associations);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(getSharedEntityAssociationsByEntityIdsResponse.PartialErrors);

                // Currently the GetSharedEntityAssociationsBySharedEntityIds operation accepts only one shared entity identifier in the list.
                GetSharedEntityAssociationsBySharedEntityIdsResponse getSharedEntityAssociationsBySharedEntityIdsResponse =
                    await CampaignManagementExampleHelper.GetSharedEntityAssociationsBySharedEntityIdsAsync("Campaign", new[] { sharedEntityIds[sharedEntityIds.Length - 1] }, "NegativeKeywordList");
                CampaignManagementExampleHelper.OutputArrayOfSharedEntityAssociation(getSharedEntityAssociationsBySharedEntityIdsResponse.Associations);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(getSharedEntityAssociationsBySharedEntityIdsResponse.PartialErrors);

                // Explicitly delete the association between the campaign and the negative keyword list.

                partialErrors = (await CampaignManagementExampleHelper.DeleteSharedEntityAssociationsAsync(associations)).PartialErrors;
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage("Deleted NegativeKeywordList associations\n");
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchError(partialErrors);
                }

                // Delete the campaign and any remaining assocations. 

                await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignId });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}\n", campaignId));

                // DeleteCampaigns does not delete the negative keyword list from the account's library. 
                // Call the DeleteSharedEntities operation to delete the shared entities.

                partialErrors = (await CampaignManagementExampleHelper.DeleteSharedEntitiesAsync(new SharedEntity[] { new NegativeKeywordList { Id = sharedEntityId } }))?.PartialErrors;
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(string.Format("Deleted Negative Keyword List (SharedEntity) Id {0}\n", sharedEntityId));
                }
                else
                {
                    CampaignManagementExampleHelper.OutputArrayOfBatchError(partialErrors);
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.EditorialApiFaultDetail> ex)
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
