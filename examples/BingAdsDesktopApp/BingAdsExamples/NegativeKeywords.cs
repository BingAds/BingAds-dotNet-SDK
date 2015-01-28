// Copyright 2014 Microsoft Corporation 

// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 

//    http://www.apache.org/licenses/LICENSE-2.0 

// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsExamples
{
    /// <summary>
    /// This example demonstrates how to associate negative keywords and negative keyword lists with a campaign.
    /// </summary>
    public class NegativeKeywords : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Campaign Management | Negative Keywords"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Add a campaign that will later be associated with negative keywords. 

                var campaign = new Campaign
                {
                    Name = "Women's Shoes" + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                    MonthlyBudget = 1000.00,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                    DaylightSaving = true
                };

                var campaignIds = await AddCampaignsAsync(authorizationData.AccountId, new[] { campaign });

                // Print the new assigned campaign identifier

                PrintCampaignIdentifiers(campaignIds);

                // You may choose to associate an exclusive set of negative keywords to an individual campaign 
                // or ad group. An exclusive set of negative keywords cannot be shared with other campaigns 
                // or ad groups. This example only associates negative keywords with a campaign.

                var entityNegativeKeywords = new[]
                    {
                        new EntityNegativeKeyword
                            {
                                EntityId = campaignIds[0],
                                EntityType = "Campaign",
                                NegativeKeywords = new[]
                                    {
                                        new NegativeKeyword
                                            {
                                                MatchType = MatchType.Phrase,
                                                Text = "auto"
                                            }
                                    }
                            }
                    };

                AddNegativeKeywordsToEntitiesResponse addNegativeKeywordsToEntitiesResponse =
                    await AddNegativeKeywordsToEntitiesAsync(entityNegativeKeywords);
                PrintNegativeKeywordIds(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                PrintNestedPartialErrors(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);
                if (addNegativeKeywordsToEntitiesResponse.NestedPartialErrors == null
                    || addNegativeKeywordsToEntitiesResponse.NestedPartialErrors.Count == 0)
                {
                    OutputStatusMessage("Added an exclusive set of negative keywords to the Campaign.\n");
                    PrintNegativeKeywordIds(addNegativeKeywordsToEntitiesResponse.NegativeKeywordIds);
                }
                else
                {
                    PrintNestedPartialErrors(addNegativeKeywordsToEntitiesResponse.NestedPartialErrors);
                }

                GetNegativeKeywordsByEntityIdsResponse getNegativeKeywordsByEntityIdsResponse =
                    await GetNegativeKeywordsByEntityIdsAsync(campaignIds, "Campaign", authorizationData.AccountId);
                PrintEntityNegativeKeywords(getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                PrintPartialErrors(getNegativeKeywordsByEntityIdsResponse.PartialErrors);
                if (getNegativeKeywordsByEntityIdsResponse.PartialErrors == null
                    || getNegativeKeywordsByEntityIdsResponse.PartialErrors.Count == 0)
                {
                    OutputStatusMessage("Retrieved an exclusive set of negative keywords for the Campaign.\n");
                    PrintEntityNegativeKeywords(getNegativeKeywordsByEntityIdsResponse.EntityNegativeKeywords);
                }
                else
                {
                    PrintPartialErrors(getNegativeKeywordsByEntityIdsResponse.PartialErrors);
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
                    PrintNestedPartialErrors(nestedPartialErrors);
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
                    PrintNestedPartialErrors(nestedPartialErrors);
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

                PrintNegativeKeywordResults(
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
                    PrintNegativeKeywords(negativeKeywords.Cast<NegativeKeyword>());
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
                    PrintPartialErrors(partialErrors);
                }

                OutputStatusMessage("Negative keywords currently in NegativeKeywordList:");
                negativeKeywords = (SharedListItem[])await GetListItemsBySharedListAsync(new NegativeKeywordList { Id = sharedEntityId });
                if (negativeKeywords == null || negativeKeywords.Length == 0)
                {
                    OutputStatusMessage("None\n");
                }
                else
                {
                    PrintNegativeKeywords(negativeKeywords.Cast<NegativeKeyword>());
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

                PrintNegativeKeywordResults(
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
                    PrintNegativeKeywords(negativeKeywords.Cast<NegativeKeyword>());
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
                    PrintPartialErrors(partialErrors);
                }

                // Get and print the negative keyword lists and return the list of identifiers.

                const string sharedEntityType = "NegativeKeywordList";
                var sharedEntityIds = await GetAndPrintSharedEntityIdentifiersAsync(sharedEntityType);

                // Negative keywords were added to the negative keyword list above. You can associate the 
                // shared list of negative keywords with a campaign with or without negative keywords. 
                // Shared negative keyword lists cannot be associated with an ad group. An ad group can only 
                // be assigned an exclusive set of negative keywords. 

                var associations = new[]
                    {
                        new SharedEntityAssociation
                            {
                                EntityId = campaignIds[0],
                                EntityType = "Campaign",
                                SharedEntityId = sharedEntityId,
                                SharedEntityType = "NegativeKeywordList" 
                            }
                    };

                partialErrors = await SetSharedEntityAssociationsAsync(associations);
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(String.Format("Associated CampaignId {0} with Negative Keyword List Id {1}.\n", 
                        campaignIds[0], sharedEntityId));
                }
                else
                {
                    PrintPartialErrors(partialErrors);
                }

                // Get and print the associations either by Campaign or NegativeKeywordList identifier.
                GetSharedEntityAssociationsByEntityIdsResponse getSharedEntityAssociationsByEntityIdsResponse =
                    await GetSharedEntityAssociationsByEntityIdsAsync(new[] { campaignIds[0] }, "Campaign", "NegativeKeywordList");
                PrintSharedEntityAssociations(getSharedEntityAssociationsByEntityIdsResponse.Associations);
                PrintPartialErrors(getSharedEntityAssociationsByEntityIdsResponse.PartialErrors);

                // Currently the GetSharedEntityAssociationsBySharedEntityIds operation accepts only one shared entity identifier in the list.
                GetSharedEntityAssociationsBySharedEntityIdsResponse getSharedEntityAssociationsBySharedEntityIdsResponse =
                    await GetSharedEntityAssociationsBySharedEntityIdsAsync("Campaign", new[] { sharedEntityIds[sharedEntityIds.Count - 1] }, "NegativeKeywordList");
                PrintSharedEntityAssociations(getSharedEntityAssociationsBySharedEntityIdsResponse.Associations);
                PrintPartialErrors(getSharedEntityAssociationsBySharedEntityIdsResponse.PartialErrors);

                // Explicitly delete the association between the campaign and the negative keyword list.

                partialErrors = await DeleteSharedEntityAssociationsAsync(associations);
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage("Deleted NegativeKeywordList associations\n");
                }
                else
                {
                    PrintPartialErrors(partialErrors);
                }

                // Delete the campaign and any remaining assocations. 

                DeleteCampaigns(authorizationData.AccountId, new[] { campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignIds[0]));

                // DeleteCampaigns does not delete the negative keyword list from the account's library. 
                // Call the DeleteSharedEntities operation to delete the shared entities.

                partialErrors = await DeleteSharedEntitiesAsync(new SharedEntity[] { new NegativeKeywordList { Id = sharedEntityId } });
                if (partialErrors == null || !partialErrors.Any())
                {
                    OutputStatusMessage(String.Format("Deleted Negative Keyword List (SharedEntity) Id {0}\n", sharedEntityId));
                }
                else
                {
                    PrintPartialErrors(partialErrors);
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CampaignManagement.EditorialApiFaultDetail> ex)
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

        private async Task<IList<long>> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await Service.CallAsync((s, r) => s.AddCampaignsAsync(r), request)).CampaignIds;
        }

        // Deletes one or more campaigns from the specified account.

        private void DeleteCampaigns(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
               AccountId = accountId,
                CampaignIds = campaignIds
            };

            Service.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        // Prints the campaign identifiers for each campaign added. 

        private void PrintCampaignIdentifiers(IEnumerable<long> campaignIds)
        {
            if (campaignIds == null)
            {
                return;
            }

            foreach (var id in campaignIds)
            {
                OutputStatusMessage(String.Format("Campaign successfully added and assigned CampaignId {0}.\n", id));
            }
        }

        // Prints the negative keyword identifiers added to each campaign or ad group entity. 
        // The IdCollection items are returned by calling AddNegativeKeywordsToEntities. 

        private void PrintNegativeKeywordIds(IList<IdCollection> idCollections)
        {
            if (idCollections == null)
            {
                return;
            }

            for (int index = 0; index < idCollections.Count; index++)
            {
                OutputStatusMessage(String.Format("NegativeKeyword Ids at entity index {0}:\n", index));
                foreach (var id in idCollections[index].Ids)
                {
                    OutputStatusMessage(String.Format("\tId: {0}\n", id));
                }
            }
        }

        // Prints a list of EntityNegativeKeyword objects

        private void PrintEntityNegativeKeywords(IEnumerable<EntityNegativeKeyword> entityNegativeKeywords)
        {
            if (entityNegativeKeywords == null)
            {
                return;
            }

            OutputStatusMessage("EntityNegativeKeyword item:\n");
            foreach (EntityNegativeKeyword entityNegativeKeyword in entityNegativeKeywords)
            {
                OutputStatusMessage(String.Format("\tEntityId: {0}", entityNegativeKeyword.EntityId));
                OutputStatusMessage(String.Format("\tEntityType: {0}\n", entityNegativeKeyword.EntityType));
                PrintNegativeKeywords(entityNegativeKeyword.NegativeKeywords);
            }
        }

        // Print and return the shared entity identifiers, for example negative keyword list identifiers.

        private async Task<IList<long>> GetAndPrintSharedEntityIdentifiersAsync(string sharedEntityType)
        {
            var sharedEntities = await GetSharedEntitiesByAccountIdAsync(sharedEntityType);
            var sharedEntityIds = new long[sharedEntities.Count];
            for (int index = 0; index < sharedEntities.Count; index++)
            {
                var sharedEntity = sharedEntities[index];
                if (sharedEntity.Id != null)
                {
                    sharedEntityIds[index] = (long)sharedEntity.Id;
                    OutputStatusMessage(String.Format("SharedEntity[{0}] ({1}) has SharedEntity Id {2}.\n",
                                      index,
                                      sharedEntities[index].Name,
                                      sharedEntities[index].Id));
                }
            }

            return sharedEntityIds;
        }

        // Prints the negative keywords

        private void PrintNegativeKeywords(IEnumerable<NegativeKeyword> negativeKeywords)
        {
            if (negativeKeywords == null)
            {
                return;
            }

            OutputStatusMessage("NegativeKeyword item:\n");
            foreach (var negativeKeyword in negativeKeywords)
            {
                OutputStatusMessage(String.Format("\tText: {0}", negativeKeyword.Text));
                OutputStatusMessage(String.Format("\tId: {0}", negativeKeyword.Id));
                OutputStatusMessage(String.Format("\tMatchType: {0}\n", negativeKeyword.MatchType));
            }
        }

        // Prints the list item identifiers, as well as any partial errors

        private void PrintNegativeKeywordResults(
            long sharedListId,
            SharedListItem[] sharedListItems,
            long[] sharedListItemIds,
            BatchError[] partialErrors)
        {
            if (sharedListItemIds == null)
            {
                return;
            }

            for (var index = 0; index < sharedListItems.Length; index++)
            {
                // Determine if the SharedListItem is a NegativeKeyword.
                if (sharedListItems[index] is NegativeKeyword)
                {
                    // Determine if the corresponding index has a valid identifier
                    if (sharedListItemIds[index] > 0)
                    {
                        OutputStatusMessage(String.Format("NegativeKeyword[{0}] ({1}) successfully added to NegativeKeywordList ({2}) and assigned Negative Keyword Id {3}.",
                                  index,
                                  ((NegativeKeyword)(sharedListItems[index])).Text,
                                  sharedListId,
                                  sharedListItemIds[index]));
                    }
                }
                else
                {
                    OutputStatusMessage("SharedListItem is not a NegativeKeyword.");
                }
            }
            
            PrintPartialErrors(partialErrors);
        }

        // Prints a list of SharedEntityAssociation objects.

        private void PrintSharedEntityAssociations(IList<SharedEntityAssociation> associations)
        {
            if (associations == null || associations.Count == 0)
            {
                return;
            }

            OutputStatusMessage("SharedEntityAssociation item:\n");
            foreach (SharedEntityAssociation sharedEntityAssociation in associations)
            {
                OutputStatusMessage(String.Format("\tEntityId: {0}", sharedEntityAssociation.EntityId));
                OutputStatusMessage(String.Format("\tEntityType: {0}", sharedEntityAssociation.EntityType));
                OutputStatusMessage(String.Format("\tSharedEntityId: {0}", sharedEntityAssociation.SharedEntityId));
                OutputStatusMessage(String.Format("\tSharedEntityType: {0}\n", sharedEntityAssociation.SharedEntityType));
            }
        }

        // Prints a list of BatchError objects that represent partial errors while managing negative keywords.

        private void PrintPartialErrors(IList<BatchError> partialErrors)
        {
            if (partialErrors == null || partialErrors.Count == 0)
            {
                return;
            }

            OutputStatusMessage("BatchError (PartialErrors) item:\n");
            foreach (BatchError error in partialErrors)
            {
                OutputStatusMessage(String.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(String.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(String.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(String.Format("\tMessage: {0}\n", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(String.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(String.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(String.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(String.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }
        }

        // Prints a list of BatchErrorCollection objects that represent partial errors while managing 
        // negative keywords.

        private void PrintNestedPartialErrors(IList<BatchErrorCollection> nestedPartialErrors)
        {
            if (nestedPartialErrors == null || nestedPartialErrors.Count == 0)
            {
                return;
            }

            OutputStatusMessage("BatchErrorCollection (NestedPartialErrors) item:\n");
            foreach (BatchErrorCollection collection in nestedPartialErrors)
            {
                // The top level list index corresponds to the campaign or ad group index identifier.
                if (collection != null)
                {
                    if (collection.Code != null)
                    {
                        OutputStatusMessage(String.Format("\tIndex: {0}", collection.Index));
                        OutputStatusMessage(String.Format("\tCode: {0}", collection.Code));
                        OutputStatusMessage(String.Format("\tErrorCode: {0}", collection.ErrorCode));
                        OutputStatusMessage(String.Format("\tMessage: {0}\n", collection.Message));
                    }

                    // The nested list of batch errors would include any errors specific to the negative keywords 
                    // that you attempted to add or remove from the campaign or ad group.
                    foreach (BatchError error in collection.BatchErrors)
                    {
                        OutputStatusMessage(String.Format("\tIndex: {0}", error.Index));
                        OutputStatusMessage(String.Format("\tCode: {0}", error.Code));
                        OutputStatusMessage(String.Format("\tErrorCode: {0}", error.ErrorCode));
                        OutputStatusMessage(String.Format("\tMessage: {0}\n", error.Message));

                        // In the case of an EditorialError, more details are available
                        if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                        {
                            OutputStatusMessage(String.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                            OutputStatusMessage(String.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                            OutputStatusMessage(String.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                            OutputStatusMessage(String.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                        }
                    }
                }
            }
        }
    }
}
