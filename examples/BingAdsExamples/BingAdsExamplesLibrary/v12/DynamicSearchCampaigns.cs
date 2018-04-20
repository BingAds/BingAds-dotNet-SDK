using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds.V12.AdInsight;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example uses the Bing Ads Campaign Management service to setup a Dynamic Search Ads (DSA) campaign.
    /// </summary>
    public class DynamicSearchCampaigns : ExampleBase
    {
        public const string DOMAIN_NAME = "contoso.com";
        public const string LANGUAGE = "EN";
        
        public override string Description
        {
            get { return "Dynamic Search Ads (DSA) Campaigns | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                AdInsightExampleHelper AdInsightExampleHelper = new AdInsightExampleHelper(this.OutputStatusMessage);
                AdInsightExampleHelper.AdInsightService = new ServiceClient<IAdInsightService>(authorizationData);

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

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
                        BudgetType = Microsoft.BingAds.V12.CampaignManagement.BudgetLimitType.DailyBudgetStandard,
                        
                        // You can set your campaign bid strategy to Enhanced CPC (EnhancedCpcBiddingScheme) 
                        // and then, at any time, set an individual ad group bid strategy to 
                        // Manual CPC (ManualCpcBiddingScheme).
                        BiddingScheme = new EnhancedCpcBiddingScheme { },

                        TimeZone = "PacificTimeUSCanadaTijuana",
                                                
                        // Used with CustomParameters defined in lower level entities such as ads.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    },
                };

                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                Microsoft.BingAds.V12.CampaignManagement.BatchError[] campaignErrors = 
                    addCampaignsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                // Next, create a new AdGroup within the dynamic search ads campaign. 

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

                        // For ad groups you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new ManualCpcBiddingScheme { },
                    }
                };

                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync((long)campaignIds[0], adGroups, null);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                Microsoft.BingAds.V12.CampaignManagement.BatchError[] adGroupErrors = 
                    addAdGroupsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

                // You can add one or more Webpage criterion to each ad group that helps determine 
                // whether or not to serve dynamic search ads.

                var adGroupCriterions = new List<AdGroupCriterion>();

                var adGroupWebpagePositivePageContent = new BiddableAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    CriterionBid = new FixedBid
                    {
                        Amount = 0.50
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
                    // DestinationUrl and FinalUrls are not supported with Webpage criterion. 
                    // The Final URL is dynamically created at the ad level.
                    DestinationUrl = null,
                    FinalUrls = null,

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
                
                var getDomainCategoriesResponse = await AdInsightExampleHelper.GetDomainCategoriesAsync(
                    null,
                    DOMAIN_NAME, 
                    LANGUAGE);
                var categories = getDomainCategoriesResponse.Categories;

                // If any categories are available let's use one as a condition.
                if (categories.Count > 0)
                {
                    var adGroupWebpagePositiveCategory = new BiddableAdGroupCriterion
                    {
                        AdGroupId = (long)adGroupIds[0],
                        CriterionBid = new FixedBid
                        {
                            Amount = 0.50
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
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                AddAdGroupCriterionsResponse addAdGroupCriterionsResponse =
                    await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(adGroupCriterions, AdGroupCriterionType.Webpage);
                long?[] adGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds.ToArray();
                OutputStatusMessage("New Ad Group Criterion Ids:\n");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupCriterionIds);
                BatchErrorCollection[] adGroupCriterionErrors =
                    addAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("\nAddAdGroupCriterions Errors:\n");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(adGroupCriterionErrors);

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
                CampaignManagementExampleHelper.OutputArrayOfCampaignCriterion(campaignCriterions);
                AddCampaignCriterionsResponse addCampaignCriterionsResponse =
                    await CampaignManagementExampleHelper.AddCampaignCriterionsAsync(campaignCriterions, CampaignCriterionType.Webpage);
                long?[] campaignCriterionIds = addCampaignCriterionsResponse.CampaignCriterionIds.ToArray();
                OutputStatusMessage("\nNew Campaign Criterion Ids:\n");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignCriterionIds);
                BatchErrorCollection[] campaignCriterionErrors =
                    addCampaignCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("\nAddCampaignCriterions Errors:\n");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(campaignCriterionErrors);


                // Finally you can add a DynamicSearchAd into the ad group. The ad title and display URL 
                // are generated automatically based on the website domain and language that you want to target.

                var ads = new Ad[] {
                    new DynamicSearchAd
                    {
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",

                        // You cannot set FinalUrls. The Final URL will be a dynamically selected landing page.
                        // The final URL is distinct from the path that customers will see and click on in your ad.
                        FinalUrls = null,

                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this ad, 
                        // and can be used by the ad, webpage, ad group, campaign, or account level tracking template. 
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

                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync((long)adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                Microsoft.BingAds.V12.CampaignManagement.BatchError[] adErrors = 
                    addAdsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);

                // Retrieve the Webpage criterion for the campaign.
                var getCampaignCriterionsByIdsResponse = await CampaignManagementExampleHelper.GetCampaignCriterionsByIdsAsync(
                    null,
                    (long)campaignIds[0],
                    CampaignCriterionType.Webpage
                );

                OutputStatusMessage("Retrieving the Campaign Webpage Criterions that we added . . . \n");
                campaignCriterions = getCampaignCriterionsByIdsResponse.CampaignCriterions.ToList();
                CampaignManagementExampleHelper.OutputArrayOfCampaignCriterion(campaignCriterions);

                // Retrieve the Webpage criterion for the ad group and then test some update scenarios.
                var getAdGroupCriterionsByIdsResponse = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    null,
                    (long)adGroupIds[0],
                    AdGroupCriterionType.Webpage
                );

                OutputStatusMessage("Retrieving the Ad Group Webpage Criterions that we added . . . \n");
                adGroupCriterions = getAdGroupCriterionsByIdsResponse.AdGroupCriterions.ToList();
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                
                // You can update the bid for BiddableAdGroupCriterion

                var updateBid = new FixedBid
                {
                    Amount = 0.75
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
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                UpdateAdGroupCriterionsResponse updateAdGroupCriterionsResponse =
                    await CampaignManagementExampleHelper.UpdateAdGroupCriterionsAsync(adGroupCriterions, AdGroupCriterionType.Webpage);
                adGroupCriterionErrors =
                    updateAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("UpdateAdGroupCriterions Errors:\n");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(adGroupCriterionErrors);

                // Delete the campaign, ad group, criterion, and ad that were previously added. 
                // You should remove this operation if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("\nDeleted Campaign Id {0}\n", campaignIds[0]));
                
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
