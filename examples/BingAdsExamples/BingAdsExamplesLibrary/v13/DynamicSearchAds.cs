using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.AdInsight;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to setup Dynamic Search Ads (DSA) in a Search campaign with the Campaign Management service.
    /// </summary>
    public class DynamicSearchAds : ExampleBase
    {
        public const string DOMAIN_NAME = "contoso.com";
        public const string LANGUAGE = "EN";
        
        public override string Description
        {
            get { return "Dynamic Search Ads (DSA) Campaigns | Campaign Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                AdInsightExampleHelper AdInsightExampleHelper = new AdInsightExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                AdInsightExampleHelper.AdInsightService = new ServiceClient<IAdInsightService>(
                    authorizationData: authorizationData,
                    environment: environment);

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                // To get started with dynamic search ads, first you'll need to add a new Search campaign 
                // Include a DynamicSearchAdsSetting that specifies the target website domain and language.

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = Microsoft.BingAds.V13.CampaignManagement.BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        Settings = new [] {
                            new DynamicSearchAdsSetting
                            {
                                DomainName = "contoso.com",
                                Language = "English"
                            }
                        },
                    },
                };

                OutputStatusMessage("-----\nAddCampaigns:");
                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaigns: campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                Microsoft.BingAds.V13.CampaignManagement.BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("CampaignIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                // Create a new ad group with type set to "SearchDynamic"

                var adGroups = new[] {
                    new AdGroup
                    {
                        AdGroupType = "SearchDynamic",
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                    }
                };

                OutputStatusMessage("-----\nAddAdGroups:");
                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync(
                    campaignId: (long)campaignIds[0],
                    adGroups: adGroups,
                    returnInheritedBidStrategyTypes: false);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                Microsoft.BingAds.V13.CampaignManagement.BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputStatusMessage("AdGroupIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

                // You can add one or more Webpage criteria to each ad group that helps determine 
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
                };
                adGroupCriterions.Add(adGroupWebpagePositivePageContent);

                // To discover the categories that you can use for Webpage criteria (positive or negative), 
                // use the GetDomainCategories operation with the Ad Insight service.

                OutputStatusMessage("-----\nGetDomainCategories:");
                var getDomainCategoriesResponse = await AdInsightExampleHelper.GetDomainCategoriesAsync(
                    categoryName: null,
                    domainName: DOMAIN_NAME, 
                    language: LANGUAGE);
                var categories = getDomainCategoriesResponse.Categories;
                OutputStatusMessage("Categories:");
                AdInsightExampleHelper.OutputArrayOfDomainCategory(categories);

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

                OutputStatusMessage("-----\nAddAdGroupCriterions:");
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                AddAdGroupCriterionsResponse addAdGroupCriterionsResponse = await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(
                    adGroupCriterions: adGroupCriterions, 
                    criterionType: AdGroupCriterionType.Webpage);
                long?[] adGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds.ToArray();
                OutputStatusMessage("AdGroupCriterionIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupCriterionIds);
                BatchErrorCollection[] adGroupCriterionErrors =
                    addAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("NestedPartialErrors:");
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

                OutputStatusMessage("-----\nAddCampaignCriterions:");
                CampaignManagementExampleHelper.OutputArrayOfCampaignCriterion(campaignCriterions);
                AddCampaignCriterionsResponse addCampaignCriterionsResponse = await CampaignManagementExampleHelper.AddCampaignCriterionsAsync(
                        campaignCriterions: campaignCriterions, 
                        criterionType: CampaignCriterionType.Webpage);
                long?[] campaignCriterionIds = addCampaignCriterionsResponse.CampaignCriterionIds.ToArray();
                OutputStatusMessage("CampaignCriterionIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignCriterionIds);
                BatchErrorCollection[] campaignCriterionErrors =
                    addCampaignCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("NestedPartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(campaignCriterionErrors);

                // Finally you must add at least one DynamicSearchAd into the ad group. The ad title and display URL 
                // are generated automatically based on the website domain and language that you want to target.

                var ads = new Ad[] {
                    new DynamicSearchAd
                    {
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",
                        // You cannot set FinalUrls for dynamic search ads. 
                        // The Final URL will be a dynamically selected landing page.
                        // The final URL is distinct from the path that customers will see and click on in your ad.
                        FinalUrls = null,                        
                    },
                };
                
                OutputStatusMessage("-----\nAddAds:");
                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync(
                    adGroupId: (long)adGroupIds[0],
                    ads: ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                Microsoft.BingAds.V13.CampaignManagement.BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                OutputStatusMessage("AdIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);
                
                // Delete the campaign and everything it contains e.g., ad groups and ads.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}", campaignIds[0]));
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
