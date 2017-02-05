using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to associate remarketing lists with a new ad group.
    /// </summary>
    public class RemarketingLists : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Remarketing Lists | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Before you can track conversions or target audiences using a remarketing list, 
                // you need to create a UET tag in Bing Ads (web application or API) and then 
                // add the UET tag tracking code to every page of your website. For more information, please see 
                // Universal Event Tracking at https://msdn.microsoft.com/library/bing-ads-universal-event-tracking-guide.aspx.

                // First you should call the GetUetTagsByIds operation to check whether a tag has already been created. 
                // You can leave the TagIds element null or empty to request all UET tags available for the customer.

                var uetTags = (await GetUetTagsByIdsAsync(null)).UetTags;

                // If you do not already have a UET tag that can be used, or if you need another UET tag, 
                // call the AddUetTags service operation to create a new UET tag. If the call is successful, 
                // the tracking script that you should add to your website is included in a corresponding 
                // UetTag within the response message. 

                if (uetTags == null || uetTags.Count < 1)
                {
                    var uetTag = new UetTag
                    {
                        Description = "My First Uet Tag",
                        Name = "New Uet Tag",
                    };
                    uetTags = (await AddUetTagsAsync(new[] { uetTag })).UetTags;
                }

                if (uetTags == null || uetTags.Count < 1)
                {
                    OutputStatusMessage(
                        string.Format("You do not have any UET tags registered for CustomerId {0}.\n", authorizationData.CustomerId)
                    );
                    return;
                }

                OutputStatusMessage("List of all UET Tags:\n");
                foreach (var uetTag in uetTags)
                {
                    OutputUetTag(uetTag);
                }

                // After you retreive the tracking script from the AddUetTags or GetUetTagsByIds operation, 
                // the next step is to add the UET tag tracking code to your website. We recommend that you, 
                // or your website administrator, add it to your entire website in either the head or body sections. 
                // If your website has a master page, then that is the best place to add it because you add it once 
                // and it is included on all pages. For more information, please see 
                // Universal Event Tracking at https://msdn.microsoft.com/library/bing-ads-universal-event-tracking-guide.aspx.
                
                // We will use the same UET tag for the remainder of this example.
                var tagId = uetTags[0].Id;

                // Add a remarketing list that depend on the UET Tag Id retreived above.

                var addRemarketingLists = new[] {
                    new RemarketingList
                    {
                        Description = "New list with CustomEventsRule",
                        MembershipDuration = 30,
                        Name = "Remarketing List with CustomEventsRule " + DateTime.UtcNow,
                        ParentId = authorizationData.AccountId,
                        Rule = new CustomEventsRule
                        {
                            // The type of user interaction you want to track.
                            Action = "play",
                            ActionOperator = StringOperator.Contains,
                            // The category of event you want to track. 
                            Category = "video",
                            CategoryOperator = StringOperator.Contains,
                            // The name of the element that caused the action.
                            Label = "trailer",
                            LabelOperator = StringOperator.Contains,
                            // A numerical value associated with that event. 
                            // Could be length of the video played etc.
                            Value = 5.00m,
                            ValueOperator = NumberOperator.Equals,
                        },
                        Scope = EntityScope.Account,
                        TagId = tagId
                    },
                    new RemarketingList
                    {
                        Description = "New list with PageVisitorsRule",
                        MembershipDuration = 30,
                        Name = "Remarketing List with PageVisitorsRule " + DateTime.UtcNow,
                        ParentId = authorizationData.AccountId,
                        // The rule definition is translated to the following logical expression: 
                        // ((Url Contains X) and (ReferrerUrl DoesNotContain Z)) or ((Url DoesNotBeginWith Y)) 
                        // or ((ReferrerUrl Equals Z))
                        Rule = new PageVisitorsRule
                        {
                            RuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "X"
                                        },
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.DoesNotContain,
                                            Value = "Z"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.DoesNotBeginWith,
                                            Value = "Y"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.Equals,
                                            Value = "Z"
                                        },
                                    }
                                },
                            },
                        },
                        Scope = EntityScope.Account,
                        TagId = tagId
                    },
                    new RemarketingList
                    {
                        Description = "New list with PageVisitorsWhoDidNotVisitAnotherPageRule",
                        MembershipDuration = 30,
                        Name = "Remarketing List with PageVisitorsWhoDidNotVisitAnotherPageRule " + DateTime.UtcNow,
                        ParentId = authorizationData.AccountId,
                        // The rule definition is translated to the following logical expression: 
                        // (((Url Contains X) and (ReferrerUrl DoesNotContain Z)) or ((Url DoesNotBeginWith Y)) 
                        // or ((ReferrerUrl Equals Z))) 
                        // and not (((Url BeginsWith A) and (ReferrerUrl BeginsWith B)) or ((Url Contains C)))
                        Rule = new PageVisitorsWhoDidNotVisitAnotherPageRule
                        {
                            ExcludeRuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.BeginsWith,
                                            Value = "A"
                                        },
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.BeginsWith,
                                            Value = "B"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "C"
                                        },
                                    }
                                },
                            },
                            IncludeRuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "X"
                                        },
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.DoesNotContain,
                                            Value = "Z"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.DoesNotBeginWith,
                                            Value = "Y"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.Equals,
                                            Value = "Z"
                                        },
                                    }
                                },
                            },
                        },
                        Scope = EntityScope.Account,
                        TagId = tagId
                    },
                    new RemarketingList
                    {
                        Description = "New list with PageVisitorsWhoVisitedAnotherPageRule",
                        MembershipDuration = 30,
                        Name = "Remarketing List with PageVisitorsWhoVisitedAnotherPageRule " + DateTime.UtcNow,
                        ParentId = authorizationData.AccountId,
                        // The rule definition is translated to the following logical expression: 
                        // (((Url Contains X) and (ReferrerUrl NotEquals Z)) or ((Url DoesNotBeginWith Y)) or 
                        // ((ReferrerUrl Equals Z))) 
                        // and (((Url BeginsWith A) and (ReferrerUrl BeginsWith B)) or ((Url Contains C)))
                        Rule = new PageVisitorsWhoVisitedAnotherPageRule
                        {
                            AnotherRuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.BeginsWith,
                                            Value = "A"
                                        },
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.BeginsWith,
                                            Value = "B"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "C"
                                        },
                                    }
                                },
                            },
                            RuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "X"
                                        },
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.DoesNotContain,
                                            Value = "Z"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.DoesNotBeginWith,
                                            Value = "Y"
                                        },
                                    }
                                },
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.Equals,
                                            Value = "Z"
                                        },
                                    }
                                },
                            },
                        },
                        Scope = EntityScope.Account,
                        TagId = tagId
                    },
                };

                var addRemarketingListsResponse = await AddRemarketingListsAsync(addRemarketingLists);
                
                var updateRemarketingLists = new [] {
                    new RemarketingList
                    {
                        Id = addRemarketingListsResponse.RemarketingListIds[0],
                        // The ParentId cannot be updated, even if you update the rule type.
                        // You can either send the same value or leave ParentId empty.
                        ParentId = authorizationData.AccountId,
                        Rule = new CustomEventsRule
                        {
                            // For both add and update remarketing list operations, you must include one or more  
                            // of the following events: 
                            // Action, Category, Label, or Value.

                            // For example if you do not include Action during update, 
                            // any existing ActionOperator and Action settings will be deleted.
                            Action = null,
                            //ActionOperator = null,
                            Category = "video",
                            CategoryOperator = StringOperator.Equals,
                            // You cannot update the operator unless you also include the expression.
                            // The following attempt to update LabelOperator will result in an error.
                            Label = null,
                            LabelOperator = StringOperator.Equals,
                            // You must specify the previous settings unless you want
                            // them replaced during the update conversion goal operation.
                            Value = 5.00m,
                            ValueOperator = NumberOperator.Equals,
                        },
                        // The Scope cannot be updated, even if you update the rule type.
                        // You can either send the same value or leave Scope empty.
                        Scope = EntityScope.Account,
                        // You can update the tag as needed. In this example we will explicitly use the same UET tag.
                        // To keep the UET tag unchanged, you can also leave this element nil or empty.
                        TagId = tagId,
                    },
                    new RemarketingList
                    {
                        // You can change the remarketing rule type e.g. in this example a remarketing list
                        // with the PageVisitorsRule had been created above at index 1. 
                        // Now we are using the returned identifier at index 1 to update the type from 
                        // PageVisitorsRule to PageVisitorsWhoDidNotVisitAnotherPageRule.
                        Id = addRemarketingListsResponse.RemarketingListIds[1],
                        Rule = new PageVisitorsWhoDidNotVisitAnotherPageRule
                        {
                            // If you want to keep any of the previous rule items, 
                            // then you must explicitly set them again during update.
                            ExcludeRuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "C"
                                        },
                                    }
                                },
                            },
                            // If you leave the entire list of rule item groups null,
                            // then previous settings will be retained. 
                            IncludeRuleItemGroups = null,
                        },
                    },
                    new RemarketingList
                    {
                        Id = addRemarketingListsResponse.RemarketingListIds[2],
                        Rule = new PageVisitorsRule
                        {
                            // If you want to keep any of the previous rule items, 
                            // then you must explicitly set them again during update.
                            RuleItemGroups = new []
                            {
                                new RuleItemGroup
                                {
                                    Items = new []
                                    {
                                        new StringRuleItem
                                        {
                                            Operand = "Url",
                                            Operator = StringOperator.Contains,
                                            Value = "X"
                                        },
                                        new StringRuleItem
                                        {
                                            Operand = "ReferrerUrl",
                                            Operator = StringOperator.DoesNotContain,
                                            Value = "Z"
                                        },
                                    }
                                },
                            },
                        },
                    },
                    new RemarketingList
                    {
                        Id = addRemarketingListsResponse.RemarketingListIds[3],
                        MembershipDuration = 20,
                        // If not specified during update, the previous rule settings are retained.
                        Rule = null,
                    },
                };

                var updateRemarketingListsResponse = await UpdateRemarketingListsAsync(updateRemarketingLists);

                
                // You must already have at least one remarketing list for the remainder of this example. 

                if (addRemarketingListsResponse.RemarketingListIds.Count < 1)
                {
                    return;
                }

                // Add an ad group in a campaign. The ad group will later be associated with remarketing lists. 

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
                        DaylightSaving = true,
                    },
                };
                
                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        BiddingModel = BiddingModel.Keyword,
                        PricingModel = PricingModel.Cpc,
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        SearchBid = new Bid { Amount = 0.09 },
                        Language = "English",
                        TrackingUrlTemplate = null,

                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        RemarketingTargetingSetting = RemarketingTargetingSetting.TargetAndBid
                    }
                };


                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputCampaignsWithPartialErrors(campaigns, campaignIds, campaignErrors);

                AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)campaignIds[0], adGroups);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputAdGroupsWithPartialErrors(adGroups, adGroupIds, adGroupErrors);

                // If the campaign or ad group add operations failed then we cannot continue this example. 

                if (addAdGroupsResponse.AdGroupIds == null || addAdGroupsResponse.AdGroupIds.Count < 1)
                {
                    return;
                }

                var adGroupRemarketingListAssociations = new List<AdGroupRemarketingListAssociation>();

                // This example associates all of the remarketing lists with the new ad group.

                var getRemarketingListIds = new List<long>();
                foreach (var listId in addRemarketingListsResponse.RemarketingListIds)
                {
                    getRemarketingListIds.Add((long)listId);
                }
                var remarketingLists = (await GetRemarketingListsAsync(getRemarketingListIds, RemarketingListAdditionalField.Rule)).RemarketingLists;

                foreach (var remarketingList in remarketingLists)
                {
                    if (remarketingList.Id != null)
                    {
                        var adGroupRemarketingListAssociation = new AdGroupRemarketingListAssociation
                        {
                            AdGroupId = (long)adGroupIds[0],
                            BidAdjustment = 20.00,
                            RemarketingListId = (long)remarketingList.Id,
                            Status = AdGroupRemarketingListAssociationStatus.Paused
                        };

                        adGroupRemarketingListAssociations.Add(adGroupRemarketingListAssociation);

                        OutputStatusMessage(string.Format("Associating the following remarketing list with AdGroup Id {0}.\n", (long)adGroupIds[0]));
                        OutputRemarketingList(remarketingList);
                    }
                }

                var addAdGroupRemarketingListAssociationsResponse = await AddAdGroupRemarketingListAssociationsAsync(adGroupRemarketingListAssociations);

                var getAdGroupRemarketingListAssociationsResponse = await GetAdGroupRemarketingListAssociationsAsync(new[] { (long)adGroupIds[0] });

                foreach (var adGroupRemarketingListAssociation in getAdGroupRemarketingListAssociationsResponse.AdGroupRemarketingListAssociations)
                {
                    OutputStatusMessage("The following ad group remarketing list association was added.\n");
                    OutputAdGroupRemarketingListAssociation(adGroupRemarketingListAssociation);
                }

                // You can store the association IDs which can be used to update or delete associations later. 

                var associationIds = addAdGroupRemarketingListAssociationsResponse.AssociationIds;
                
                // If the associations were added and retrieved successfully let's practice updating and deleting one of them.

                if (associationIds != null && associationIds.Count > 0)
                {
                    var updateAdGroupRemarketingListAssociation = new AdGroupRemarketingListAssociation
                    {
                        AdGroupId = (long)adGroupIds[0],
                        BidAdjustment = 10.00,
                        Id = associationIds[0],
                        Status = AdGroupRemarketingListAssociationStatus.Active,
                    };

                    var updateAdGroupRemarketingListAssociationsResponse = 
                        await UpdateAdGroupRemarketingListAssociationsAsync(new AdGroupRemarketingListAssociation[] { updateAdGroupRemarketingListAssociation});
                    
                    var deleteAdGroupRemarketingListAssociationsResponse =
                        await DeleteAdGroupRemarketingListAssociationsAsync(new AdGroupRemarketingListAssociation[] { updateAdGroupRemarketingListAssociation });

                }

                // Delete the campaign, ad group, and ad group remarketing list associations that were previously added. 
                // The remarketing lists will not be deleted.
                // You should remove this line if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignIds[0]));
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

        // Adds one or more UET tags.

        private async Task<AddUetTagsResponse> AddUetTagsAsync(IList<UetTag> uetTags)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = uetTags,
            };

            return (await Service.CallAsync((s, r) => s.AddUetTagsAsync(r), request));
        }

        // Gets one or more UET Tags.

        private async Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(IList<long> tagIds)
        {
            var request = new GetUetTagsByIdsRequest
            {
                TagIds = tagIds,
            };

            return await Service.CallAsync((s, r) => s.GetUetTagsByIdsAsync(r), request);
        }

        /// <summary>
        /// Adds remarketing lists. 
        /// </summary>
        /// <param name="remarketingListIds">The remarketing lists that you want to add.</param>
        /// <returns></returns>
        private async Task<AddRemarketingListsResponse> AddRemarketingListsAsync(IList<RemarketingList> remarketingLists)
        {
            var request = new AddRemarketingListsRequest
            {

                RemarketingLists = remarketingLists,
            };

            return (await Service.CallAsync((s, r) => s.AddRemarketingListsAsync(r), request));
        }

        /// <summary>
        /// Retrieves remarketing lists. If RemarketingListIds is null or empty,
        /// the service will return all remarketing lists that the current authenticated user can access.
        /// </summary>
        /// <param name="remarketingListIds">The unique identifiers for the remarketing lists that you want to get.</param>
        /// <returns></returns>
        private async Task<GetRemarketingListsResponse> GetRemarketingListsAsync(
            IList<long> remarketingListIds,
            RemarketingListAdditionalField returnAdditionalFields)
        {
            var request = new GetRemarketingListsRequest
            {
                RemarketingListIds = remarketingListIds,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await Service.CallAsync((s, r) => s.GetRemarketingListsAsync(r), request));
        }

        /// <summary>
        /// Updates remarketing lists. 
        /// </summary>
        /// <param name="remarketingListIds">The remarketing lists that you want to update.</param>
        /// <returns></returns>
        private async Task<UpdateRemarketingListsResponse> UpdateRemarketingListsAsync(IList<RemarketingList> remarketingLists)
        {
            var request = new UpdateRemarketingListsRequest
            {

                RemarketingLists = remarketingLists,
            };

            return (await Service.CallAsync((s, r) => s.UpdateRemarketingListsAsync(r), request));
        }

        /// <summary>
        /// Associates the specified ad groups with the respective remarketing lists.
        /// </summary>
        /// <param name="adGroupRemarketingListAssociations"></param>
        /// <returns></returns>
        private async Task<AddAdGroupRemarketingListAssociationsResponse> AddAdGroupRemarketingListAssociationsAsync(
           IList<AdGroupRemarketingListAssociation> adGroupRemarketingListAssociations)
        {
            var request = new AddAdGroupRemarketingListAssociationsRequest
            {
                AdGroupRemarketingListAssociations = adGroupRemarketingListAssociations,
            };

            return (await Service.CallAsync((s, r) => s.AddAdGroupRemarketingListAssociationsAsync(r), request));
        }

        /// <summary>
        /// Deletes one or more ad group remarketing list associations.
        /// </summary>
        /// <param name="adGroupRemarketingListAssociations"></param>
        /// <returns></returns>
        private async Task<DeleteAdGroupRemarketingListAssociationsResponse> DeleteAdGroupRemarketingListAssociationsAsync(
          IList<AdGroupRemarketingListAssociation> adGroupRemarketingListAssociations)
        {
            var request = new DeleteAdGroupRemarketingListAssociationsRequest
            {
                AdGroupRemarketingListAssociations = adGroupRemarketingListAssociations,
            };

            return (await Service.CallAsync((s, r) => s.DeleteAdGroupRemarketingListAssociationsAsync(r), request));
        }

        /// <summary>
        /// Gets the ad group remarketing list associations.
        /// </summary>
        /// <param name="adGroupIds"></param>
        /// <returns></returns>
        private async Task<GetAdGroupRemarketingListAssociationsResponse> GetAdGroupRemarketingListAssociationsAsync(
          IList<long> adGroupIds)
        {
            var request = new GetAdGroupRemarketingListAssociationsRequest
            {
                AdGroupIds = adGroupIds
            };

            return (await Service.CallAsync((s, r) => s.GetAdGroupRemarketingListAssociationsAsync(r), request));
        }

        /// <summary>
        /// Updates one or more ad group remarketing list associations.
        /// </summary>
        /// <param name="adGroupRemarketingListAssociations"></param>
        /// <returns></returns>
        private async Task<UpdateAdGroupRemarketingListAssociationsResponse> UpdateAdGroupRemarketingListAssociationsAsync(
           IList<AdGroupRemarketingListAssociation> adGroupRemarketingListAssociations)
        {
            var request = new UpdateAdGroupRemarketingListAssociationsRequest
            {
                AdGroupRemarketingListAssociations = adGroupRemarketingListAssociations,
            };

            return (await Service.CallAsync((s, r) => s.UpdateAdGroupRemarketingListAssociationsAsync(r), request));
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

        private async Task DeleteCampaignsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            await Service.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        // Adds one or more ad groups to the specified campaign.

        private async Task<AddAdGroupsResponse> AddAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            return (await Service.CallAsync((s, r) => s.AddAdGroupsAsync(r), request));
        }
    }
}
