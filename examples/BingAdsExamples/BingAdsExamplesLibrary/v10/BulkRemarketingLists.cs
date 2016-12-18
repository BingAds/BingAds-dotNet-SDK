using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V10.Bulk;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to how to associate remarketing lists with a new ad group using the BulkServiceManager class.
    /// </summary>
    public class BulkRemarketingLists : BulkExampleBase
    {
        public static ServiceClient<ICampaignManagementService> CampaignService;

        public override string Description
        {
            get { return "Bulk Remarketing List Associations | Bulk V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(String.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

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
                
                #region Add

                // Prepare the bulk entities that you want to upload.  

                var uploadEntities = new List<BulkEntity>();

                // Add a remarketing list that depend on the UET Tag Id retreived above.

                var bulkRemarketingLists = new[] {
                    new BulkRemarketingList
                    {
                        ClientId = "List with CustomEventsRule",
                        RemarketingList = new RemarketingList
                        {
                            Description = "New list with CustomEventsRule",
                            Id = -1,
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
                    },
                    new BulkRemarketingList
                    {
                        ClientId = "List with PageVisitorsRule",
                        RemarketingList = new RemarketingList
                        {
                            Description = "New list with PageVisitorsRule",
                            Id = -2,
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
                    },
                    new BulkRemarketingList
                    {
                        ClientId = "List with PageVisitorsWhoDidNotVisitAnotherPageRule",
                        RemarketingList = new RemarketingList
                        {
                            Description = "New list with PageVisitorsWhoDidNotVisitAnotherPageRule",
                            Id = -3,
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
                    },
                    new BulkRemarketingList
                    {
                        ClientId = "List with PageVisitorsWhoVisitedAnotherPageRule",
                        RemarketingList = new RemarketingList
                        {
                            Description = "New list with PageVisitorsWhoVisitedAnotherPageRule",
                            Id = -4,
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
                    },
                };

                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
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

                        // DaylightSaving is not supported in the Bulk file schema. Whether or not you specify it in a BulkCampaign,
                        // the value is not written to the Bulk file, and by default DaylightSaving is set to true.
                        DaylightSaving = true,

                        TrackingUrlTemplate = null
                    }
                };

                // Specify one or more ad groups.

                var bulkAdGroup = new BulkAdGroup
                {
                    // ClientId may be used to associate records in the bulk upload file with records in the results file. The value of this field 
                    // is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record.
                    // Note: This bulk file Client Id is not related to an application Client Id for OAuth. 
                    ClientId = "YourClientIdGoesHere",
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        // When using the Campaign Management service, the Id cannot be set. In the context of a BulkAdGroup, the Id is optional 
                        // and may be used as a negative reference key during bulk upload. For example the same negative value set for the  
                        // ad group Id will be used when associating this new ad group with a new ad group remarketing list association
                        // in the BulkAdGroupRemarketingListAssociation object below. 
                        Id = adGroupIdKey,
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        BiddingModel = BiddingModel.Keyword,
                        BiddingScheme = new InheritFromParentBiddingScheme(),
                        PricingModel = PricingModel.Cpc,
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        Language = "English",
                        Status = AdGroupStatus.Active,
                        TrackingUrlTemplate = null,

                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        RemarketingTargetingSetting = RemarketingTargetingSetting.TargetAndBid,
                    },
                };

                uploadEntities.Add(bulkCampaign);
                uploadEntities.Add(bulkAdGroup);

                // This example associates all of the remarketing lists with the new ad group.

                foreach (var bulkRemarketingList in bulkRemarketingLists)
                {
                    uploadEntities.Add(bulkRemarketingList);

                    var BulkAdGroupRemarketingListAssociation = new BulkAdGroupRemarketingListAssociation
                    {
                        ClientId = "MyBulkAdGroupRemarketingListAssociation " + bulkRemarketingList.ClientId,
                        AdGroupRemarketingListAssociation = new AdGroupRemarketingListAssociation
                        {
                            AdGroupId = adGroupIdKey,
                            BidAdjustment = 20.00,
                            RemarketingListId = (long)bulkRemarketingList.RemarketingList.Id,
                            Status = AdGroupRemarketingListAssociationStatus.Paused
                        },
                    };

                    uploadEntities.Add(BulkAdGroupRemarketingListAssociation);
                }

                // Upload and write the output

                OutputStatusMessage("\nAdding campaign, ad group, and ad group remarketing list associations...\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var remarketingListResults = downloadEntities.OfType<BulkRemarketingList>().ToList();
                OutputBulkRemarketingLists(remarketingListResults);

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var adGroupRemarketingListResults = downloadEntities.OfType<BulkAdGroupRemarketingListAssociation>().ToList();
                OutputBulkAdGroupRemarketingListAssociations(adGroupRemarketingListResults);

                Reader.Dispose();

                #endregion Add
                
                #region CleanUp

                // Delete the campaign, ad group, and ad group remarketing list associations that were previously added.
                // The remarketing lists will not be deleted. 
                // You should remove this region if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                // You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                // When you delete a BulkCampaign, the dependent entities such as BulkAdGroup and BulkAdGroupRemarketingListAssociation 
                // are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign, ad group, and ad group remarketing list associations . . .\n");

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
            catch (FaultException<Microsoft.BingAds.V10.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.Bulk.ApiFaultDetail> ex)
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

        // Adds one or more UET tags.

        private async Task<AddUetTagsResponse> AddUetTagsAsync(IList<UetTag> uetTags)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = uetTags,
            };

            return (await CampaignService.CallAsync((s, r) => s.AddUetTagsAsync(r), request));
        }

        // Gets one or more UET Tags.

        private async Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(IList<long> tagIds)
        {
            var request = new GetUetTagsByIdsRequest
            {
                TagIds = tagIds,
            };

            return await CampaignService.CallAsync((s, r) => s.GetUetTagsByIdsAsync(r), request);
        }
    }
}
