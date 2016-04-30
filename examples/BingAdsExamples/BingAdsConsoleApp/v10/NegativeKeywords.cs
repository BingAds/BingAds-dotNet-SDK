using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsConsoleApp.V10
{
    /// <summary>
    /// This example demonstrates how to associate negative keywords and negative keyword lists with a campaign.
    /// </summary>
    public class NegativeKeywords : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Negative Keywords | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Add a campaign that will later be associated with negative keywords. 

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Women's Shoes" + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        DaylightSaving = true,
                    }
                };

                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
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
                    await AddNegativeKeywordsToEntitiesAsync(entityNegativeKeywords);
                OutputNegativeKeywordIds(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                OutputNestedPartialErrors(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);
                if (addNegativeKeywordsToEntitiesResponse.NestedPartialErrors == null
                    || addNegativeKeywordsToEntitiesResponse.NestedPartialErrors.Count == 0)
                {
                    OutputStatusMessage("Added an exclusive set of negative keywords to the Campaign.\n");
                    OutputNegativeKeywordIds(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                }
                else
                {
                    OutputNestedPartialErrors(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);
                }

                GetNegativeKeywordsByEntityIdsResponse getNegativeKeywordsByEntityIdsResponse =
                    await GetNegativeKeywordsByEntityIdsAsync(new[] { campaignId }, "Campaign", authorizationData.AccountId);
                OutputEntityNegativeKeywords(getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                OutputPartialErrors(getNegativeKeywordsByEntityIdsResponse.PartialErrors);
                if (getNegativeKeywordsByEntityIdsResponse.PartialErrors == null
                    || getNegativeKeywordsByEntityIdsResponse.PartialErrors.Count == 0)
                {
                    OutputStatusMessage("Retrieved an exclusive set of negative keywords for the Campaign.\n");
                    OutputEntityNegativeKeywords(getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                }
                else
                {
                    OutputPartialErrors(getNegativeKeywordsByEntityIdsResponse.PartialErrors);
                }
                
                // If you attempt to delete a negative keyword without an identifier the operation will
                // succeed but will return partial errors corresponding to the index of the negative keyword
                // that was not deleted. 
                var nestedPartialErrors = (BatchErrorCollection[])await DeleteNegativeKeywordsFromEntitiesAsync(entityNegativeKeywords);
                if (nestedPartialErrors == null || nestedPartialErrors.Length == 0)
                {
                    OutputStatusMessage("Deleted an exclusive set of negative keywords from the Campaign.\n");
                }
                else
                {
                    OutputStatusMessage("Attempt to DeleteNegativeKeywordsFromEntities without NegativeKeyword identifier partially fails by design.");
                    OutputNestedPartialErrors(nestedPartialErrors);
                }

                // Delete the negative keywords with identifiers that were returned above.
                nestedPartialErrors = (BatchErrorCollection[])await DeleteNegativeKeywordsFromEntitiesAsync(
                    getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                if (nestedPartialErrors == null || nestedPartialErrors.Length == 0)
                {
                    OutputStatusMessage("Deleted an exclusive set of negative keywords from the Campaign.\n");
                }
                else
                {
                    OutputNestedPartialErrors(nestedPartialErrors);
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

                var addSharedEntityResponse = await AddSharedEntityAsync(negativeKeywordList, negativeKeywords);
                var sharedEntityId = addSharedEntityResponse.SharedEntityId;
                long[] listItemIds = addSharedEntityResponse.ListItemIds.ToArray();

                OutputStatusMessage(String.Format("NegativeKeywordList successfully added to account library and assigned identifer {0}\n", sharedEntityId));

                OutputNegativeKeywordsWithPartialErrors(
                    sharedEntityId,
                    negativeKeywords,
                    listItemIds,
                    addSharedEntityResponse.PartialErrors.ToArray());

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])await GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId });
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    OutputNegativeKeywords(negativeKeywords.Cast<NegativeKeyword>());
                }

                // To update the list of negative keywords, you must either add or remove from the list
                // using the respective AddListItemsToSharedList or DeleteListItemsFromSharedList operations.
                // To remove the negative keywords from the list pass the negative keyword identifers
                // and negative keyword list (SharedEntity) identifer.

                var partialErrors = await DeleteListItemsFromSharedListAsync(listItemIds, new NegativeKeywordList { Id = sharedEntityId });
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage("Deleted most recently added negative keywords from negative keyword list.\n");

                }
                else
                {
                    OutputPartialErrors(partialErrors);
                }

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])await GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId });
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    OutputNegativeKeywords(negativeKeywords.Cast<NegativeKeyword>());
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

                var addListItemsToSharedListResponse = await AddListItemsToSharedListAsync(
                    negativeKeywords,
                    new NegativeKeywordList { Id = sharedEntityId });
                listItemIds = addListItemsToSharedListResponse.ListItemIds.ToArray();

                OutputNegativeKeywordsWithPartialErrors(
                    sharedEntityId,
                    negativeKeywords,
                    listItemIds,
                    addListItemsToSharedListResponse.PartialErrors.ToArray());

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])await GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId });
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    OutputNegativeKeywords(negativeKeywords.Cast<NegativeKeyword>());
                }

                // You can update the name of the negative keyword list. 

                negativeKeywordList = new NegativeKeywordList
                {
                    Id = sharedEntityId,
                    Name = "My Updated Negative Keyword List",
                    Type = "NegativeKeywordList"
                };

                partialErrors = await UpdateSharedEntitiesAsync(new SharedEntity[] { negativeKeywordList });
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(String.Format("Updated Negative Keyword List Name to {0}.\n", negativeKeywordList.Name));
                }
                else
                {
                    OutputPartialErrors(partialErrors);
                }

                // Get and output the negative keyword lists and store the list of identifiers.

                const string sharedEntityType = "NegativeKeywordList";
                var sharedEntities = await GetSharedEntitiesByAccountIdAsync(sharedEntityType);
                OutputSharedEntityIdentifiersAsync(sharedEntities);
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

                partialErrors = await SetSharedEntityAssociationsAsync(associations);
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(String.Format("Associated CampaignId {0} with Negative Keyword List Id {1}.\n",
                        campaignId, sharedEntityId));
                }
                else
                {
                    OutputPartialErrors(partialErrors);
                }

                // Get and output the associations either by Campaign or NegativeKeywordList identifier.
                GetSharedEntityAssociationsByEntityIdsResponse getSharedEntityAssociationsByEntityIdsResponse =
                    await GetSharedEntityAssociationsByEntityIdsAsync(new[] { campaignId }, "Campaign", "NegativeKeywordList");
                OutputSharedEntityAssociations(getSharedEntityAssociationsByEntityIdsResponse.Associations);
                OutputPartialErrors(getSharedEntityAssociationsByEntityIdsResponse.PartialErrors);

                // Currently the GetSharedEntityAssociationsBySharedEntityIds operation accepts only one shared entity identifier in the list.
                GetSharedEntityAssociationsBySharedEntityIdsResponse getSharedEntityAssociationsBySharedEntityIdsResponse =
                    await GetSharedEntityAssociationsBySharedEntityIdsAsync("Campaign", new[] { sharedEntityIds[sharedEntityIds.Length - 1] }, "NegativeKeywordList");
                OutputSharedEntityAssociations(getSharedEntityAssociationsBySharedEntityIdsResponse.Associations);
                OutputPartialErrors(getSharedEntityAssociationsBySharedEntityIdsResponse.PartialErrors);

                // Explicitly delete the association between the campaign and the negative keyword list.

                partialErrors = await DeleteSharedEntityAssociationsAsync(associations);
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage("Deleted NegativeKeywordList associations\n");
                }
                else
                {
                    OutputPartialErrors(partialErrors);
                }

                // Delete the campaign and any remaining assocations. 

                await DeleteCampaigns(authorizationData.AccountId, new[] { campaignId });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignId));

                // DeleteCampaigns does not delete the negative keyword list from the account's library. 
                // Call the DeleteSharedEntities operation to delete the shared entities.

                partialErrors = await DeleteSharedEntitiesAsync(new SharedEntity[] { new NegativeKeywordList { Id = sharedEntityId } });
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(String.Format("Deleted Negative Keyword List (SharedEntity) Id {0}\n", sharedEntityId));
                }
                else
                {
                    OutputPartialErrors(partialErrors);
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }


        // Adds list items such as negative keywords to the corresponding list. 

        private async Task<AddListItemsToSharedListResponse> AddListItemsToSharedListAsync(
            IList<SharedListItem> listItems,
            SharedList sharedList)
        {
            var request = new AddListItemsToSharedListRequest
            {
                ListItems = listItems,
                SharedList = sharedList
            };

            return (await Service.CallAsync((s, r) => s.AddListItemsToSharedListAsync(r), request));
        }

        // Adds a shared entity such as a negative keyword list to the account's library. 
        // Lists in the account's library can be associated with any campaign within the account. 

        private async Task<AddSharedEntityResponse> AddSharedEntityAsync(
            SharedEntity sharedEntity,
            IList<SharedListItem> listItems)
        {
            var request = new AddSharedEntityRequest
            {
                ListItems = listItems,
                SharedEntity = sharedEntity
            };

            return (await Service.CallAsync((s, r) => s.AddSharedEntityAsync(r), request));
        }

        // Deletes list items such as negative keywords from the corresponding list. 

        private async Task<IList<BatchError>> DeleteListItemsFromSharedListAsync(
            IList<long> listItemIds,
            SharedList sharedList)
        {
            var request = new DeleteListItemsFromSharedListRequest
            {
                ListItemIds = listItemIds,
                SharedList = sharedList
            };

            return (await Service.CallAsync((s, r) => s.DeleteListItemsFromSharedListAsync(r), request)).PartialErrors;
        }

        // Deletes shared entities such as negative keyword lists from the account's library. 

        private async Task<IList<BatchError>> DeleteSharedEntitiesAsync(IList<SharedEntity> sharedEntities)
        {
            var request = new DeleteSharedEntitiesRequest
            {
                SharedEntities = sharedEntities
            };

            return (await Service.CallAsync((s, r) => s.DeleteSharedEntitiesAsync(r), request)).PartialErrors;
        }

        // Removes the association between a shared entity such as a negative keyword list and an entity such as a campaign. 

        private async Task<IList<BatchError>> DeleteSharedEntityAssociationsAsync(IList<SharedEntityAssociation> associations)
        {
            var request = new DeleteSharedEntityAssociationsRequest
            {
                Associations = associations
            };

            return (await Service.CallAsync((s, r) => s.DeleteSharedEntityAssociationsAsync(r), request)).PartialErrors;
        }

        // Gets the list items such as the negative keywords of a negative keyword list.

        private async Task<IList<SharedListItem>> GetListItemsBySharedListAsync(SharedList sharedList)
        {
            var request = new GetListItemsBySharedListRequest
            {
                SharedList = sharedList
            };

            return (await Service.CallAsync((s, r) => s.GetListItemsBySharedListAsync(r), request)).ListItems;
        }

        // Gets the shared entities such as negative keyword lists from the account's library. 

        private async Task<IList<SharedEntity>> GetSharedEntitiesByAccountIdAsync(string sharedEntityType)
        {
            var request = new GetSharedEntitiesByAccountIdRequest
            {
                SharedEntityType = sharedEntityType
            };

            return (await Service.CallAsync((s, r) => s.GetSharedEntitiesByAccountIdAsync(r), request)).SharedEntities;
        }

        // Gets associations between a campaign and a shared entity such as a negative keyword list. 
        // You can request associations by associated entity identifiers.

        private async Task<GetSharedEntityAssociationsByEntityIdsResponse> GetSharedEntityAssociationsByEntityIdsAsync(
            IList<long> entityIds,
            string entityType,
            string sharedEntityType)
        {
            var request = new GetSharedEntityAssociationsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                SharedEntityType = sharedEntityType
            };

            return (await Service.CallAsync((s, r) => s.GetSharedEntityAssociationsByEntityIdsAsync(r), request));
        }

        // Gets associations between a campaign and a shared entity such as a negative keyword list. 
        // You can request associations by shared entity identifiers.

        private async Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(
            string entityType,
            IList<long> sharedEntityIds,
            string sharedEntityType)
        {
            var request = new GetSharedEntityAssociationsBySharedEntityIdsRequest
            {
                EntityType = entityType,
                SharedEntityIds = sharedEntityIds,
                SharedEntityType = sharedEntityType
            };

            return (await Service.CallAsync((s, r) => s.GetSharedEntityAssociationsBySharedEntityIdsAsync(r), request));
        }

        // Sets the association between a campaign and a shared entity such as a negative keyword list. 

        private async Task<IList<BatchError>> SetSharedEntityAssociationsAsync(IList<SharedEntityAssociation> sharedEntityAssociations)
        {
            var request = new SetSharedEntityAssociationsRequest
            {
                Associations = sharedEntityAssociations
            };

            return (await Service.CallAsync((s, r) => s.SetSharedEntityAssociationsAsync(r), request)).PartialErrors;
        }


        // Updates shared entities such as negative keyword lists within the account's library.

        private async Task<IList<BatchError>> UpdateSharedEntitiesAsync(IList<SharedEntity> sharedEntities)
        {
            var request = new UpdateSharedEntitiesRequest
            {
                SharedEntities = sharedEntities
            };

            return (await Service.CallAsync((s, r) => s.UpdateSharedEntitiesAsync(r), request)).PartialErrors;
        }

        // Adds negative keywords to the specified campaign or ad group. 

        private async Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(IList<EntityNegativeKeyword> entityNegativeKeywords)
        {
            var request = new AddNegativeKeywordsToEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords
            };

            return (await Service.CallAsync((s, r) => s.AddNegativeKeywordsToEntitiesAsync(r), request));
        }

        // Deletes negative keywords from the specified campaign or ad group. 

        private async Task<IList<BatchErrorCollection>> DeleteNegativeKeywordsFromEntitiesAsync(IList<EntityNegativeKeyword> entityNegativeKeywords)
        {
            var request = new DeleteNegativeKeywordsFromEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords,
            };

            return (await Service.CallAsync((s, r) => s.DeleteNegativeKeywordsFromEntitiesAsync(r), request)).NestedPartialErrors;
        }

        // Gets the negative keywords that are only associated with the specified campaigns or ad groups. 

        private async Task<GetNegativeKeywordsByEntityIdsResponse> GetNegativeKeywordsByEntityIdsAsync(
            IList<long> entityIds,
            string entityType,
            long parentEntityId)
        {
            var request = new GetNegativeKeywordsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                ParentEntityId = parentEntityId
            };

            return (await Service.CallAsync((s, r) => s.GetNegativeKeywordsByEntityIdsAsync(r), request));
        }

        // Adds one or more campaigns to the specified account.

        private async Task<AddCampaignsResponse> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await Service.CallAsync((s, r) => s.AddCampaignsAsync(r), request));
        }

        // Deletes one or more campaigns from the specified account.

        private async Task DeleteCampaigns(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            await Service.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        
    }
}
