using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.AdInsight;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example uses the Bing Ads Campaign Management service to setup a Dynamic Search Ads (DSA) campaign.
    /// </summary>
    public class DynamicSearchCampaigns : ExampleBase
    {
        public static ServiceClient<IAdInsightService> AdInsightService;
        public static ServiceClient<ICampaignManagementService> CampaignService;

        public const string DOMAIN_NAME = "booking.com";
        public const string LANGUAGE = "EN";
        
        public override string Description
        {
            get { return "Dynamic Search Ads (DSA) Campaigns | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                AdInsightService = new ServiceClient<IAdInsightService>(authorizationData);
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                // To get started with dynamic search ads, first you'll need to add a new Campaign 
                // with its type set to DynamicSearchAds. When you create the campaign, you'll need to 
                // include a DynamicSearchAdsSetting that specifies the target website domain and language.

                var campaigns = new[]{
                    new Campaign
                    {
                        CampaignType = CampaignType.DynamicSearchAds,
                        Settings = new [] {
                            new DynamicSearchAdsSetting
                            {
                                DomainName = "contoso.com",
                                Language = "English"
                            }
                        },

                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        
                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = Microsoft.BingAds.V10.CampaignManagement.BudgetLimitType.DailyBudgetStandard,
                        
                        // You can set your campaign bid strategy to Enhanced CPC (EnhancedCpcBiddingScheme) 
                        // and then, at any time, set an individual ad group or keyword bid strategy to 
                        // Manual CPC (ManualCpcBiddingScheme).
                        BiddingScheme = new EnhancedCpcBiddingScheme { },

                        TimeZone = "PacificTimeUSCanadaTijuana",
                        DaylightSaving = true,
                                                
                        // Used with CustomParameters defined in lower level entities such as ads.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    },
                };

                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                Microsoft.BingAds.V10.CampaignManagement.BatchError[] campaignErrors = 
                    addCampaignsResponse.PartialErrors.ToArray();
                OutputCampaignsWithPartialErrors(campaigns, campaignIds, campaignErrors);

                // Next, create a new AdGroup within the dynamic search ads campaign. 

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

                        // For ad groups you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new ManualCpcBiddingScheme { },
                    }
                };

                AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)campaignIds[0], adGroups);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                Microsoft.BingAds.V10.CampaignManagement.BatchError[] adGroupErrors = 
                    addAdGroupsResponse.PartialErrors.ToArray();
                OutputAdGroupsWithPartialErrors(adGroups, adGroupIds, adGroupErrors);

                // You can add one or more Webpage criterion to each ad group that helps determine 
                // whether or not to serve dynamic search ads.

                var adGroupCriterions = new List<AdGroupCriterion>();

                var adGroupWebpagePositivePageContent = new BiddableAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    CriterionBid = new FixedBid
                    {
                        Bid = new Bid
                        {
                            Amount = 0.50
                        }
                    },
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            Conditions = new[]
                            {
                                new WebpageCondition
                                {
                                    Argument = "flowers",
                                    Operand = WebpageConditionOperand.PageContent,
                                }
                            },
                            CriterionName = "Ad Group Webpage Positive Page Content Criterion"
                        },
                    },
                    // DestinationUrl is not supported with Webpage criterion.
                    DestinationUrl = null,

                    // You could use a tracking template which would override the campaign level
                    // tracking template. Tracking templates defined for lower level entities 
                    // override those set for higher level entities.
                    // In this example we are using the campaign level tracking template.
                    TrackingUrlTemplate = null,

                    // Set custom parameters that are specific to this webpage criterion, 
                    // and can be used by the criterion, ad group, campaign, or account level tracking template. 
                    // In this example we are using the campaign level tracking template.
                    UrlCustomParameters = new CustomParameters
                    {
                        Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO1"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                    }
                };
                adGroupCriterions.Add(adGroupWebpagePositivePageContent);

                // To discover the categories that you can use for Webpage criterion (positive or negative), 
                // use the GetDomainCategories operation with the Ad Insight service.
                
                var getDomainCategoriesResponse = await GetDomainCategoriesAsync(DOMAIN_NAME, LANGUAGE);
                var categories = getDomainCategoriesResponse.Categories;

                // If any categories are available let's use one as a condition.
                if (categories.Count > 0)
                {
                    var adGroupWebpagePositiveCategory = new BiddableAdGroupCriterion
                    {
                        AdGroupId = (long)adGroupIds[0],
                        CriterionBid = new FixedBid
                        {
                            Bid = new Bid
                            {
                                Amount = 0.50
                            }
                        },
                        Criterion = new Webpage
                        {
                            Parameter = new WebpageParameter
                            {
                                Conditions = new[]
                                {
                                    new WebpageCondition
                                    {
                                        Argument = categories[0].CategoryName,
                                        Operand = WebpageConditionOperand.Category,
                                    }
                                },
                                CriterionName = "Ad Group Webpage Positive Category Criterion"
                            },
                        }
                    };
                    adGroupCriterions.Add(adGroupWebpagePositiveCategory);
                }

                // If you want to exclude certain portions of your website, you can add negative Webpage 
                // criterion at the campaign and ad group level. 

                var adGroupWebpageNegativeUrl = new NegativeAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            // You can choose whether you want the criterion argument to match partial URLs, 
                            // page content, page title, or categories that Bing thinks applies to your website.
                            Conditions = new[]
                            {
                                new WebpageCondition
                                {
                                    Argument = DOMAIN_NAME,
                                    Operand = WebpageConditionOperand.Url,
                                }
                            },
                            // If you do not specify any name, then it will be set to a concatenated list of conditions. 
                            CriterionName = null
                        }
                    },
                };
                adGroupCriterions.Add(adGroupWebpageNegativeUrl);

                OutputStatusMessage("Adding Ad Group Webpage Criterion . . . \n");
                OutputAdGroupCriterions(adGroupCriterions);
                AddAdGroupCriterionsResponse addAdGroupCriterionsResponse =
                    await AddAdGroupCriterionsAsync(adGroupCriterions, CriterionType.Webpage);
                long?[] adGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds.ToArray();
                OutputStatusMessage("\nNew Ad Group Criterion Ids:\n");
                OutputIds(adGroupCriterionIds);
                BatchErrorCollection[] adGroupCriterionErrors =
                    addAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("\nAddAdGroupCriterions Errors:\n");
                OutputBatchErrorCollections(adGroupCriterionErrors);

                // The negative Webpage criterion at the campaign level applies to all ad groups 
                // within the campaign; however, if you define ad group level negative Webpage criterion, 
                // the campaign criterion is ignored for that ad group.

                var campaignCriterions = new List<CampaignCriterion>();
                var campaignWebpageNegative = new NegativeCampaignCriterion
                {
                    CampaignId = (long)campaignIds[0],
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            Conditions = new[]
                            {
                                new WebpageCondition
                                {
                                    Argument = DOMAIN_NAME + "\\seattle",
                                    Operand = WebpageConditionOperand.Url,
                                }
                            },
                            CriterionName = "Campaign Negative Webpage Url Criterion"
                        }
                    }
                };
                campaignCriterions.Add(campaignWebpageNegative);

                OutputStatusMessage("Adding Campaign Webpage Criterion . . . \n");
                OutputCampaignCriterions(campaignCriterions);
                AddCampaignCriterionsResponse addCampaignCriterionsResponse =
                    await AddCampaignCriterionsAsync(campaignCriterions, CampaignCriterionType.Webpage);
                long?[] campaignCriterionIds = addCampaignCriterionsResponse.CampaignCriterionIds.ToArray();
                OutputStatusMessage("\nNew Campaign Criterion Ids:\n");
                OutputIds(campaignCriterionIds);
                BatchErrorCollection[] campaignCriterionErrors =
                    addCampaignCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("\nAddCampaignCriterions Errors:\n");
                OutputBatchErrorCollections(campaignCriterionErrors);


                // Finally you can add a DynamicSearchAd into the ad group. The ad title and display URL 
                // are generated automatically based on the website domain and language that you want to target.

                var ads = new Ad[] {
                    new DynamicSearchAd
                    {
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",

                        // With FinalUrls you can separate the tracking template, custom parameters, and 
                        // landing page URLs. 
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                        // Final Mobile URLs can also be used if you want to direct the user to a different page 
                        // for mobile devices.
                        FinalMobileUrls = new[] {
                            "http://mobile.contoso.com/womenshoesale"
                        }, 
                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this ad, 
                        // and can be used by the ad, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
                        UrlCustomParameters = new CustomParameters {
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO1"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        }
                    },
                };

                AddAdsResponse addAdsResponse = await AddAdsAsync((long)adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                Microsoft.BingAds.V10.CampaignManagement.BatchError[] adErrors = 
                    addAdsResponse.PartialErrors.ToArray();
                OutputAdsWithPartialErrors(ads, adIds, adErrors);
                

                // Retrieve the Webpage criterion for the ad group and then test some update scenarios.
                var getAdGroupCriterionsByIdsResponse = await GetAdGroupCriterionsByIdsAsync(
                    (long)adGroupIds[0],
                    null,
                    CriterionType.Webpage
                    );
                                
                adGroupCriterions = getAdGroupCriterionsByIdsResponse.AdGroupCriterions.ToList();
                
                // You can update the bid for BiddableAdGroupCriterion

                var updateBid = new FixedBid
                {
                    Bid = new Bid
                    {
                        Amount = 0.75
                    }
                };

                // You can update the Webpage criterion name but cannot update the conditions. 
                // To update the conditions you must delete the criterion and add a new criterion.
                // This update attempt will return an error.

                var updateCriterionAttemptFailure = new Webpage
                {
                    Parameter = new WebpageParameter
                    {
                        Conditions = new[]
                        {
                            new WebpageCondition
                            {
                                Argument = "Books",
                                Operand = WebpageConditionOperand.PageContent,
                            }
                        },
                        CriterionName = "Update Attempt Failure"
                    },
                };

                var updateCriterionAttemptSuccess = new Webpage
                {
                    Parameter = new WebpageParameter
                    {
                        CriterionName = "Update Attempt Success"
                    },
                };

                foreach (var adGroupCriterion in adGroupCriterions)
                {
                    var biddableAdGroupCriterion = adGroupCriterion as BiddableAdGroupCriterion;
                    if (biddableAdGroupCriterion != null)
                    {
                        ((BiddableAdGroupCriterion)(adGroupCriterion)).CriterionBid = updateBid;
                        ((BiddableAdGroupCriterion)(adGroupCriterion)).Criterion = updateCriterionAttemptSuccess;
                    }
                    else
                    {
                        var negativeAdGroupCriterion = adGroupCriterion as NegativeAdGroupCriterion;
                        if (negativeAdGroupCriterion != null)
                        {
                            ((NegativeAdGroupCriterion)(adGroupCriterion)).Criterion = updateCriterionAttemptFailure;
                        }
                    }
                }                

                OutputStatusMessage("Updating Ad Group Webpage Criterion . . . \n");
                OutputAdGroupCriterions(adGroupCriterions);
                UpdateAdGroupCriterionsResponse updateAdGroupCriterionsResponse =
                    await UpdateAdGroupCriterionsAsync(adGroupCriterions, CriterionType.Webpage);
                adGroupCriterionErrors =
                    updateAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("\nUpdateAdGroupCriterions Errors:\n");
                OutputBatchErrorCollections(adGroupCriterionErrors);

                // Delete the campaign, ad group, criterion, and ad that were previously added. 
                // You should remove this operation if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(String.Format("\nDeleted CampaignId {0}\n", campaignIds[0]));
                
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

        // Adds one or more campaign criterion.

        private async Task<AddCampaignCriterionsResponse> AddCampaignCriterionsAsync(
            IList<CampaignCriterion> campaignCriterions,
            CampaignCriterionType criterionType)
        {
            var request = new AddCampaignCriterionsRequest
            {
                CampaignCriterions = campaignCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.AddCampaignCriterionsAsync(r), request));
        }

        // Adds one or more ad group criterion.

        private async Task<AddAdGroupCriterionsResponse> AddAdGroupCriterionsAsync(
            IList<AdGroupCriterion> adGroupCriterions,
            CriterionType criterionType)
        {
            var request = new AddAdGroupCriterionsRequest
            {
                AdGroupCriterions = adGroupCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdGroupCriterionsAsync(r), request));
        }

        // Gets one or more ad group criterion.

        private async Task<GetAdGroupCriterionsByIdsResponse> GetAdGroupCriterionsByIdsAsync(
            long adGroupId,
            IList<long> adGroupCriterionIds,
            CriterionType criterionType)
        {
            var request = new GetAdGroupCriterionsByIdsRequest
            {
                AdGroupId = adGroupId,
                CriterionType = criterionType,
                AdGroupCriterionIds = adGroupCriterionIds
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdGroupCriterionsByIdsAsync(r), request));
        }

        // Updates one or more ad group criterion.

        private async Task<UpdateAdGroupCriterionsResponse> UpdateAdGroupCriterionsAsync(
            IList<AdGroupCriterion> adGroupCriterions,
            CriterionType criterionType)
        {
            var request = new UpdateAdGroupCriterionsRequest
            {
                AdGroupCriterions = adGroupCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdGroupCriterionsAsync(r), request));
        }

        // Adds one or more campaigns to the specified account.

        private async Task<AddCampaignsResponse> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await CampaignService.CallAsync((s, r) => s.AddCampaignsAsync(r), request));
        }

        // Updates one or more campaigns.

        private async Task UpdateCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new UpdateCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            await CampaignService.CallAsync((s, r) => s.UpdateCampaignsAsync(r), request);
        }

        // Deletes one or more campaigns from the specified account.

        private async Task DeleteCampaignsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            await CampaignService.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        // Gets one or more campaigns for the specified campaign identifiers.

        private async Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(
            long accountId,
            IList<long> campaignIds,
            CampaignType campaignType,
            CampaignAdditionalField returnAdditionalFields)
        {
            var request = new GetCampaignsByIdsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds,
                CampaignType = campaignType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request));
        }

        // Retrieves all the requested campaign types in the account.

        private async Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(
            long accountId,
            CampaignType campaignType,
            CampaignAdditionalField returnAdditionalFields)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request));
        }

        // Adds one or more ad groups to the specified campaign.

        private async Task<AddAdGroupsResponse> AddAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdGroupsAsync(r), request));
        }

        // Updates one or more ad groups.

        private async Task UpdateAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new UpdateAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            await CampaignService.CallAsync((s, r) => s.UpdateAdGroupsAsync(r), request);
        }
                
        // Adds one or more ads to the specified ad group.

        private async Task<AddAdsResponse> AddAdsAsync(long adGroupId, IList<Ad> ads)
        {
            var request = new AddAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdsAsync(r), request));
        }

        /// <summary>
        /// Updates the ads.
        /// </summary>
        /// <param name="adGroupId">The identifier of the ad group that contains the ads.</param>
        /// <param name="ads">The ads that you want to update.</param>
        /// <returns></returns>
        private async Task<UpdateAdsResponse> UpdateAdsAsync(long adGroupId, IList<Ad> ads)
        {
            var request = new UpdateAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdsAsync(r), request));
        }

        /// <summary>
        /// Gets the ads in the specified ad group.
        /// </summary>
        /// <param name="adGroupId">The identifier of the ad group that contains the ads.</param>
        /// <returns></returns>
        private async Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(long adGroupId)
        {
            var request = new GetAdsByAdGroupIdRequest
            {
                AdGroupId = adGroupId,
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdsByAdGroupIdAsync(r), request));
        }

        /// <summary>
        /// Gets the categories that you can use for Webpage criterion.
        /// </summary>
        /// <param name="domainName">The website name corresponding to the pages you want your ads to target.</param>
        /// <param name="language">The language of the website domain.</param>
        /// <returns></returns>
        private async Task<GetDomainCategoriesResponse> GetDomainCategoriesAsync(
            string domainName,
            string language)
        {
            var request = new GetDomainCategoriesRequest
            {
                DomainName = domainName,
                Language = language
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetDomainCategoriesAsync(r), request));
        }
    }
}
