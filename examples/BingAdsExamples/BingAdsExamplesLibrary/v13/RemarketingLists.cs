using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to associate remarketing lists with a new ad group.
    /// </summary>
    public class RemarketingLists : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Remarketing Lists | Campaign Management V13"; }
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

                // Before you can track conversions or target audiences using a remarketing list 
                // you need to create a UET tag, and then add the UET tag tracking code to every page of your website.
                // For more information, please see Universal Event Tracking at https://go.microsoft.com/fwlink/?linkid=829965.

                // First you should call the GetUetTagsByIds operation to check whether a tag has already been created. 
                // You can leave the TagIds element null or empty to request all UET tags available for the customer.

                OutputStatusMessage("-----\nGetUetTagsByIds:");
                var uetTags = (await CampaignManagementExampleHelper.GetUetTagsByIdsAsync(
                    tagIds: null))?.UetTags;

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
                    OutputStatusMessage("-----\nAddUetTags:");
                    uetTags = (await CampaignManagementExampleHelper.AddUetTagsAsync(
                        uetTags: new[] { uetTag })).UetTags;
                }

                if (uetTags == null || uetTags.Count < 1)
                {
                    OutputStatusMessage(
                        string.Format("You do not have any UET tags registered for CustomerId {0}.", authorizationData.CustomerId)
                    );
                    return;
                }

                OutputStatusMessage("List of all UET Tags:");
                CampaignManagementExampleHelper.OutputArrayOfUetTag(uetTags);

                // After you retreive the tracking script from the AddUetTags or GetUetTagsByIds operation, 
                // the next step is to add the UET tag tracking code to your website. 

                // We will use the same UET tag for the remainder of this example.
                var tagId = uetTags[0].Id;
                
                // Add remarketing lists that depend on the UET Tag Id retreived above.

                var addAudiences = new[] {
                    new RemarketingList
                    {
                        Description = "New list with CustomEventsRule",
                        MembershipDuration = 30,
                        Name = "Remarketing List with CustomEventsRule " + DateTime.UtcNow,
                        ParentId = authorizationData.AccountId,
                        // The rule definition is translated to the following logical expression: 
                        // (Category Equals video) and (Action Equals play) and (Label Equals trailer) 
                        // and (Value Equals 5)
                        Rule = new CustomEventsRule
                        {
                            // The type of user interaction you want to track.
                            Action = "play",
                            ActionOperator = StringOperator.Equals,
                            // The category of event you want to track. 
                            Category = "video",
                            CategoryOperator = StringOperator.Equals,
                            // The name of the element that caused the action.
                            Label = "trailer",
                            LabelOperator = StringOperator.Equals,
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

                OutputStatusMessage("-----\nAddAudiences:");
                var addAudiencesResponse = await CampaignManagementExampleHelper.AddAudiencesAsync(
                    audiences: addAudiences);
                long?[] audienceIds = addAudiencesResponse.AudienceIds.ToArray();
                BatchError[] audienceErrors = addAudiencesResponse.PartialErrors.ToArray();
                OutputStatusMessage("AudienceIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(audienceIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(audienceErrors);
                
                // Add an ad group in a campaign. The ad group will later be associated with remarketing lists. 

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
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
                
                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },

                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        Settings = new[]
                        {
                            new TargetSetting
                            {
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
                
                OutputStatusMessage("-----\nAddAdGroups:");
                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync(
                    campaignId: (long)campaignIds[0],
                    adGroups: adGroups,
                    returnInheritedBidStrategyTypes: false);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputStatusMessage("AdGroupIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

                // Associate all of the remarketing lists created above with the new ad group.

                var adGroupRemarketingListAssociations = new List<AdGroupCriterion>();                               
                
                foreach (var audienceId in audienceIds)
                {
                    if(audienceId != null)
                    {
                        var biddableAdGroupCriterion = new BiddableAdGroupCriterion
                        {
                            AdGroupId = (long)adGroupIds[0],
                            Criterion = new AudienceCriterion
                            {
                                AudienceId = audienceId,
                                AudienceType = AudienceType.RemarketingList,
                            },
                            CriterionBid = new BidMultiplier
                            {
                                Multiplier = 20.00,
                            },
                            Status = AdGroupCriterionStatus.Active,
                        };

                        adGroupRemarketingListAssociations.Add(biddableAdGroupCriterion);
                    }
                }
                
                OutputStatusMessage("-----\nAddAdGroupCriterions:");
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupRemarketingListAssociations);
                AddAdGroupCriterionsResponse addAdGroupCriterionsResponse = await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(
                        adGroupCriterions: adGroupRemarketingListAssociations,
                        criterionType: AdGroupCriterionType.Audience);
                long?[] nullableAdGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds.ToArray();
                OutputStatusMessage("AdGroupCriterionIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableAdGroupCriterionIds);
                BatchErrorCollection[] adGroupCriterionErrors =
                    addAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("NestedPartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(adGroupCriterionErrors);
                
                // Delete the campaign and everything it contains e.g., ad groups and ads.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}", campaignIds[0]));

                // Delete the remarketing lists.

                OutputStatusMessage("-----\nDeleteAudiences:");
                await CampaignManagementExampleHelper.DeleteAudiencesAsync(
                    audienceIds: new[] { (long)audienceIds[0] });
                OutputStatusMessage(string.Format("Deleted Audience Id {0}", audienceIds[0]));
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
