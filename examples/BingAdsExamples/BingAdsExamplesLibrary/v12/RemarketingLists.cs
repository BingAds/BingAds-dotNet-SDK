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
    /// This example demonstrates how to associate remarketing lists with a new ad group.
    /// </summary>
    public class RemarketingLists : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Remarketing Lists | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Before you can track conversions or target audiences using a remarketing list, 
                // you need to create a UET tag in Bing Ads (web application or API) and then 
                // add the UET tag tracking code to every page of your website. For more information, please see 
                // Universal Event Tracking at https://docs.microsoft.com/en-us/bingads/guides/universal-event-tracking.

                // First you should call the GetUetTagsByIds operation to check whether a tag has already been created. 
                // You can leave the TagIds element null or empty to request all UET tags available for the customer.

                var uetTags = (await CampaignManagementExampleHelper.GetUetTagsByIdsAsync(null))?.UetTags;

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
                    uetTags = (await CampaignManagementExampleHelper.AddUetTagsAsync(new[] { uetTag })).UetTags;
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
                    CampaignManagementExampleHelper.OutputUetTag(uetTag);
                }

                // After you retreive the tracking script from the AddUetTags or GetUetTagsByIds operation, 
                // the next step is to add the UET tag tracking code to your website. We recommend that you, 
                // or your website administrator, add it to your entire website in either the head or body sections. 
                // If your website has a master page, then that is the best place to add it because you add it once 
                // and it is included on all pages. For more information, please see 
                // Universal Event Tracking at https://docs.microsoft.com/en-us/bingads/guides/universal-event-tracking.

                // We will use the same UET tag for the remainder of this example.
                var tagId = uetTags[0].Id;

                // Add a remarketing list that depend on the UET Tag Id retreived above.

                var addAudiences = new[] {
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

                // RemarketingList extends the Audience base class. 
                // We manage remarketing lists with Audience operations.

                var addAudiencesResponse = await CampaignManagementExampleHelper.AddAudiencesAsync(addAudiences);
                var audienceIds = addAudiencesResponse.AudienceIds;

                // You must already have at least one remarketing list for the remainder of this example. 

                if (audienceIds.Count < 1)
                {
                    return;
                }

                var updateAudiences = new[] {
                    new RemarketingList
                    {
                        Id = audienceIds[0],
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
                        Id = audienceIds[1],
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
                        Id = audienceIds[2],
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
                        Id = audienceIds[3],
                        MembershipDuration = 20,
                        // If not specified during update, the previous rule settings are retained.
                        Rule = null,
                    },
                };

                var updateAudiencesResponse = await CampaignManagementExampleHelper.UpdateAudiencesAsync(updateAudiences);
                OutputStatusMessage("Updated audiences. List of errors (if applicable):\n");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(updateAudiencesResponse.PartialErrors);

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
                    },
                };

                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Women's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                        Language = "English",
                        TrackingUrlTemplate = null,

                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        Settings = new[]
                    {
                        new TargetSetting
                        {
                            // Each target setting detail is delimited by a semicolon (;) in the Bulk file
                            Details = new []
                            {
                                new TargetSettingDetail
                                {
                                    CriterionTypeGroup = CriterionTypeGroup.Audience,
                                    TargetAndBid = true
                                }
                            }
                        }
                    },
                    }
                };


                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync((long)campaignIds[0], adGroups, null);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

                // If the campaign or ad group add operations failed then we cannot continue this example. 

                if (adGroupIds == null || adGroupIds.Length < 1)
                {
                    return;
                }

                var adGroupRemarketingListAssociations = new List<AdGroupCriterion>();

                // This example associates all of the remarketing lists with the new ad group.

                var getAudienceIds = new List<long>();
                foreach (var listId in audienceIds)
                {
                    getAudienceIds.Add((long)listId);
                }
                var remarketingLists = (await CampaignManagementExampleHelper.GetAudiencesByIdsAsync(
                    getAudienceIds,
                    AudienceType.RemarketingList)).Audiences;

                foreach (var remarketingList in remarketingLists)
                {
                    if (remarketingList.Id != null)
                    {
                        var biddableAdGroupCriterion = new BiddableAdGroupCriterion
                        {
                            AdGroupId = (long)adGroupIds[0],
                            Criterion = new AudienceCriterion
                            {
                                AudienceId = (long)remarketingList.Id,
                                AudienceType = AudienceType.RemarketingList,
                            },
                            CriterionBid = new BidMultiplier
                            {
                                Multiplier = 20.00,
                            },
                            Status = AdGroupCriterionStatus.Active,
                        };

                        adGroupRemarketingListAssociations.Add(biddableAdGroupCriterion);

                        OutputStatusMessage(string.Format("Associating the following remarketing list with AdGroup Id {0}.\n", (long)adGroupIds[0]));
                        CampaignManagementExampleHelper.OutputRemarketingList((RemarketingList)remarketingList);
                    }
                }

                var addAdGroupCriterionsResponse = await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(
                    adGroupRemarketingListAssociations,
                    AdGroupCriterionType.Audience);

                var adGroupCriterionIds = new List<long>();
                foreach (long id in addAdGroupCriterionsResponse.AdGroupCriterionIds)
                {
                    adGroupCriterionIds.Add(id);
                }

                var getAdGroupCriterionsByIdsResponse = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    adGroupCriterionIds,
                    (long)adGroupIds[0],
                    AdGroupCriterionType.RemarketingList);

                foreach (var adGroupRemarketingListAssociation in getAdGroupCriterionsByIdsResponse.AdGroupCriterions)
                {
                    OutputStatusMessage("The following ad group remarketing list association was added.\n");
                    CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(new AdGroupCriterion[] { adGroupRemarketingListAssociation });
                }

                // You can store the association IDs which can be used to update or delete associations later. 

                var nullableAdGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds;

                // If the associations were added and retrieved successfully let's practice updating and deleting one of them.

                if (nullableAdGroupCriterionIds != null && nullableAdGroupCriterionIds.Count > 0)
                {
                    var updateAdGroupRemarketingListAssociation = new BiddableAdGroupCriterion
                    {
                        AdGroupId = (long)adGroupIds[0],
                        Criterion = new AudienceCriterion
                        {
                            AudienceType = AudienceType.RemarketingList,
                        },
                        CriterionBid = new BidMultiplier
                        {
                            Multiplier = 10.00,
                        },
                        Id = nullableAdGroupCriterionIds[0],
                        Status = AdGroupCriterionStatus.Active,
                    };

                    var updateAdGroupCriterionsResponse = await CampaignManagementExampleHelper.UpdateAdGroupCriterionsAsync(
                        new BiddableAdGroupCriterion[] { updateAdGroupRemarketingListAssociation },
                        AdGroupCriterionType.Audience
                    );

                    var deleteAdGroupCriterionsResponse = await CampaignManagementExampleHelper.DeleteAdGroupCriterionsAsync(
                        adGroupCriterionIds,
                        (long)adGroupIds[0],
                        AdGroupCriterionType.Audience
                    );
                }

                // Delete the campaign, ad group, and ad group remarketing list associations that were previously added. 

                var deleteCampaignsResponse = (await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] }));
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}\n", campaignIds[0]));

                // Delete the remarketing list.

                var deleteAudiencesResponse = (await CampaignManagementExampleHelper.DeleteAudiencesAsync(new[] { (long)audienceIds[0] }));
                OutputStatusMessage(string.Format("Deleted Audience Id {0}\n", audienceIds[0]));
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
