using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.CustomerManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add ads and keywords to a new ad group, 
    /// and handle partial errors when some ads or keywords are not successfully created.
    /// </summary>
    public class KeywordsAds : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> CampaignService;
        public static ServiceClient<ICustomerManagementService> CustomerService;

        public override string Description
        {
            get { return "Keywords and Ads | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);
                CustomerService = new ServiceClient<ICustomerManagementService>(authorizationData);

                // Determine whether you are able to add shared budgets by checking the pilot flags.

                bool enabledForSharedBudgets = false;
                var featurePilotFlags = await GetCustomerPilotFeaturesAsync(authorizationData.CustomerId);

                // The pilot flag value for shared budgets is 263.
                // Pilot flags apply to all accounts within a given customer.
                if (featurePilotFlags.Any(pilotFlag => pilotFlag == 263))
                {
                    OutputStatusMessage("Customer is in pilot for Shared Budget.\n");
                    enabledForSharedBudgets = true;
                }
                else
                {
                    OutputStatusMessage("Customer is not in pilot for Shared Budget.\n");
                }

                // If the customer is enabled for shared budgets, let's create a new budget and
                // share it with a new campaign.

                var budgetIds = new List<long?>();
                if (enabledForSharedBudgets)
                {
                    var budgets = new List<Budget>();
                    budgets.Add(new Budget
                    {
                        Amount = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        Name = "My Shared Budget " + DateTime.UtcNow,
                    });
                    
                    budgetIds = (await AddBudgetsAsync(budgets)).BudgetIds.ToList();
                }

                // Specify one or more campaigns.

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Women's Shoes" + DateTime.UtcNow,
                        Description = "Red shoes line.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = enabledForSharedBudgets ? budgetIds[0] : null,
                        DailyBudget = enabledForSharedBudgets ? 0 : 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,

                        TimeZone = "PacificTimeUSCanadaTijuana",
                        DaylightSaving = true,

                        // You can set your campaign bid strategy to Enhanced CPC (EnhancedCpcBiddingScheme) 
                        // and then, at any time, set an individual ad group or keyword bid strategy to 
                        // Manual CPC (ManualCpcBiddingScheme).
                        // For campaigns you can use either of the EnhancedCpcBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then ManualCpcBiddingScheme is used by default.
                        BiddingScheme = new EnhancedCpcBiddingScheme { },
                        
                        // Used with FinalUrls shown in the text ads that we will add below.
                        TrackingUrlTemplate = 
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    },
                };

                // Specify one or more ad groups.

                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        BiddingModel = BiddingModel.Keyword,
                        PricingModel = PricingModel.Cpc,
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        SearchBid = new Bid { Amount = 0.09 },
                        Language = "English",

                        // For ad groups you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new ManualCpcBiddingScheme { },
                        
                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,
                    }
                };

                // In this example only the second keyword should succeed. The Text of the first keyword exceeds the limit,
                // and the third keyword is a duplicate of the second keyword. 

                var keywords = new[] {
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Broad,
                        Text = "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes " +
                               "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes " +
                               "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes",
                        // For keywords you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new InheritFromParentBiddingScheme { },
                    },
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes",
                        // For keywords you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new InheritFromParentBiddingScheme { },
                    },
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes",
                        // For keywords you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new InheritFromParentBiddingScheme { },
                    }
                };

                // In this example only the first 3 ads should succeed. 
                // The Title of the fourth ad is empty and not valid,
                // and the fifth ad is a duplicate of the second ad. 

                var ads = new Ad[] {
                    new TextAd 
                    {
                        Title = "Women's Shoe Sale",
                        Text = "Huge Savings on red shoes.",
                        DisplayUrl = "Contoso.com",
                        
                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                        // to an empty string when updating the ad. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

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
                    new TextAd {
                        Title = "Women's Super Shoe Sale",
                        Text = "Huge Savings on red shoes.",
                        DisplayUrl = "Contoso.com",                       
                        
                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                        // to an empty string when updating the ad. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

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
                                    Value = "PROMO2"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                    new TextAd {
                        Title = "Women's Red Shoe Sale",
                        Text = "Huge Savings on red shoes.",
                        DisplayUrl = "Contoso.com",

                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                        // to an empty string when updating the ad. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

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
                                    Value = "PROMO3"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                    new TextAd {
                        Title = "",
                        Text = "Huge Savings on red shoes.",
                        DisplayUrl = "Contoso.com",                       
                        
                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                        // to an empty string when updating the ad. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

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
                                    Value = "PROMO4"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                    new TextAd {
                        Title = "Women's Super Shoe Sale",
                        Text = "Huge Savings on red shoes.",
                        DisplayUrl = "Contoso.com",                       
                        
                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously.                
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                        // to an empty string when updating the ad. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

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
                                    Value = "PROMO5"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                };

                // Add the campaign, ad group, keywords, and ads

                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();

                AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)campaignIds[0], adGroups);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();

                AddKeywordsResponse addKeywordsResponse = await AddKeywordsAsync((long)adGroupIds[0], keywords);
                long?[] keywordIds = addKeywordsResponse.KeywordIds.ToArray();
                BatchError[] keywordErrors = addKeywordsResponse.PartialErrors.ToArray();

                AddAdsResponse addAdsResponse = await AddAdsAsync((long)adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();

                
                // Output the new assigned entity identifiers, as well as any partial errors

                OutputCampaignsWithPartialErrors(campaigns, campaignIds, campaignErrors);
                OutputAdGroupsWithPartialErrors(adGroups, adGroupIds, adGroupErrors);
                OutputKeywordsWithPartialErrors(keywords, keywordIds, keywordErrors);
                OutputAdsWithPartialErrors(ads, adIds, adErrors);


                // Here is a simple example that updates the campaign budget.
                // If the campaign has a shared budget you cannot update the Campaign budget amount,
                // and you must instead update the amount in the Budget object. If you try to update 
                // the budget amount of a campaign that has a shared budget, the service will return 
                // the CampaignServiceCannotUpdateSharedBudget error code.

                var getCampaigns = (await GetCampaignsByAccountIdAsync(
                    authorizationData.AccountId,
                    CampaignType.SearchAndContent | CampaignType.Shopping,
                    CampaignAdditionalField.BiddingScheme | CampaignAdditionalField.BudgetId
                )).Campaigns;

                var updateCampaigns = new List<Campaign>();
                var updateBudgets = new List<Budget>();
                var getCampaignIds = new List<long>();
                var getBudgetIds = new List<long>();

                // Increase existing budgets by 20%
                foreach (var campaign in getCampaigns)
                {
                    // If the campaign has a shared budget, let's add the budget ID to the list we will update later.
                    if (campaign != null && campaign.BudgetId > 0)
                    {
                        getBudgetIds.Add((long)campaign.BudgetId);
                    }
                    // If the campaign has its own budget, let's add it to the list of campaigns to update later.
                    else if(campaign != null)
                    {
                        updateCampaigns.Add(campaign);
                    }
                }

                // Update shared budgets in Budget objects.
                if (getBudgetIds.Count > 0)
                {
                    getBudgetIds = getBudgetIds.Distinct().ToList();
                    var getBudgets = (await GetBudgetsByIdsAsync(getBudgetIds)).Budgets;

                    OutputStatusMessage("List of shared budgets BEFORE update:\n");
                    foreach (var budget in getBudgets)
                    {
                        OutputStatusMessage("Budget:");
                        OutputBudget(budget);
                    }

                    OutputStatusMessage("List of campaigns that share each budget:\n");
                    var getCampaignIdCollection = (await GetCampaignIdsByBudgetIdsAsync(getBudgetIds)).CampaignIdCollection;
                    for(int index = 0; index < getCampaignIdCollection.Count; index++)
                    {
                        OutputStatusMessage(string.Format("BudgetId: {0}", getBudgetIds[index]));
                        OutputStatusMessage("Campaign Ids:");
                        if(getCampaignIdCollection[index] != null)
                        {
                            foreach (var id in getCampaignIdCollection[index].Ids)
                            {
                                OutputStatusMessage(string.Format("\t{0}", id));
                            }
                        }
                    }

                    foreach (var budget in getBudgets)
                    {
                        if (budget != null)
                        {
                            // Increase budget by 20 %
                            budget.Amount *= 1.2m;
                            updateBudgets.Add(budget);
                        }
                    }
                    await UpdateBudgetsAsync(updateBudgets);

                    getBudgets = (await GetBudgetsByIdsAsync(getBudgetIds)).Budgets;

                    OutputStatusMessage("List of shared budgets AFTER update:\n");
                    foreach (var budget in getBudgets)
                    {
                        OutputStatusMessage("Budget:");
                        OutputBudget(budget);
                    }
                }

                // Update unshared budgets in Campaign objects.
                if(updateCampaigns.Count > 0)
                {
                    // The UpdateCampaigns operation only accepts 100 Campaign objects per call. 
                    // To simply the example we will update the first 100.
                    updateCampaigns = updateCampaigns.Take(100).ToList();

                    OutputStatusMessage("List of campaigns with unshared budget BEFORE budget update:\n");
                    foreach (var campaign in updateCampaigns)
                    {
                        OutputStatusMessage("Campaign:");
                        OutputCampaign(campaign);

                        // Monthly budgets are deprecated and there will be a forced migration to daily budgets in calendar year 2017. 
                        // Shared budgets do not support the monthly budget type, so this is only applicable to unshared budgets. 
                        // During the migration all campaign level unshared budgets will be rationalized as daily. 
                        // The formula that will be used to convert monthly to daily budgets is: Monthly budget amount / 30.4.
                        // Moving campaign monthly budget to daily budget is encouraged before monthly budgets are migrated. 

                        if (campaign.BudgetType == BudgetLimitType.MonthlyBudgetSpendUntilDepleted)
                        {
                            // Increase budget by 20 %
                            campaign.BudgetType = BudgetLimitType.DailyBudgetStandard;
                            campaign.DailyBudget = (campaign.MonthlyBudget / 30.4) * 1.2;
                        }
                        else
                        {
                            // Increase budget by 20 %
                            campaign.DailyBudget *= 1.2;
                        }

                        getCampaignIds.Add((long)campaign.Id);
                    }

                    await UpdateCampaignsAsync(authorizationData.AccountId, updateCampaigns);

                    getCampaigns = (await GetCampaignsByIdsAsync(
                        authorizationData.AccountId,
                        getCampaignIds,
                        CampaignType.SearchAndContent | CampaignType.Shopping,
                        CampaignAdditionalField.BiddingScheme | CampaignAdditionalField.BudgetId
                    )).Campaigns;

                    OutputStatusMessage("List of campaigns with unshared budget AFTER budget update:\n");
                    foreach (var campaign in getCampaigns)
                    {
                        OutputStatusMessage("Campaign:");
                        OutputCampaign(campaign);
                    }
                }
                
                // Update the Text for the 3 successfully created ads, and update some UrlCustomParameters.
                var updateAds = new Ad[] {
                    new TextAd {
                        Id = adIds[0],
                        Text = "Huge Savings on All Red Shoes.",
                        // Set the UrlCustomParameters element to null or empty to retain any 
                        // existing custom parameters.
                        UrlCustomParameters = null,
                    },
                    new TextAd {
                        Id = adIds[1],
                        Text = "Huge Savings on All Red Shoes.",
                        // To remove all custom parameters, set the Parameters element of the 
                        // CustomParameters object to null or empty.
                        UrlCustomParameters = new CustomParameters {
                            Parameters = null,
                        },
                    },
                    new TextAd {
                        Id = adIds[2],
                        Text = "Huge Savings on All Red Shoes.",
                        // To remove a subset of custom parameters, specify the custom parameters that 
                        // you want to keep in the Parameters element of the CustomParameters object.
                        UrlCustomParameters = new CustomParameters {
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "updatedpromo"
                                },
                            }
                        }
                    },
                };

                // As an exercise you can step through using the debugger and view the results.

                var getAdsByAdGroupIdResponse = await GetAdsByAdGroupIdAsync((long)adGroupIds[0]);
                var updateAdsResponse = await UpdateAdsAsync((long)adGroupIds[0], updateAds);
                getAdsByAdGroupIdResponse = await GetAdsByAdGroupIdAsync((long)adGroupIds[0]);


                // Here is a simple example that updates the keyword bid to use the ad group bid.
                
                var updateKeyword = new Keyword
                {
                    // Set Bid.Amount null (new empty Bid) to use the ad group bid.
                    // If the Bid property is null, your keyword bid will not be updated.
                    Bid = new Bid(),
                    Id = keywordIds[1],
                };

                // As an exercise you can step through using the debugger and view the results.

                var getKeywordsByAdGroupIdResponse = await GetKeywordsByAdGroupIdAsync((long)adGroupIds[0], KeywordAdditionalField.BiddingScheme);
                var updateKeywordsResponse = await UpdateKeywordsAsync((long)adGroupIds[0], new[] { updateKeyword });
                getKeywordsByAdGroupIdResponse = await GetKeywordsByAdGroupIdAsync((long)adGroupIds[0], KeywordAdditionalField.BiddingScheme);
                
                // Delete the campaign, ad group, keyword, and ad that were previously added. 
                // You should remove this line if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignIds[0]));

                // This sample will attempt to delete the budget that was created above 
                // if the customer is enabled for shared budgets.
                if (enabledForSharedBudgets)
                {
                    await DeleteBudgetsAsync(new[] { (long)budgetIds[0] });
                    OutputStatusMessage(String.Format("Deleted Budget Id {0}\n", budgetIds[0]));
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

        /// <summary>
        /// Gets the list of pilot features that the customer is able to use.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<IList<int>> GetCustomerPilotFeaturesAsync(long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request)).FeaturePilotFlags.ToArray();
        }

        // Adds one or more budgets that can be shared by campaigns in the account.

        private async Task<AddBudgetsResponse> AddBudgetsAsync(IList<Budget> budgets)
        {
            var request = new AddBudgetsRequest
            {
                Budgets = budgets
            };

            return (await CampaignService.CallAsync((s, r) => s.AddBudgetsAsync(r), request));
        }
        
        /// <summary>
        /// Gets the specified budgets from the account's shared budget library.
        /// </summary>
        /// <param name="budgetIds">The identifiers of the budgets you want to retrieve. 
        /// If you leave BudgetIds nil or empty, then the operation will return all budgets 
        /// that are available to be shared with campaigns in the account.</param>
        /// <returns></returns>
        private async Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(IList<long> budgetIds)
        {
            var request = new GetBudgetsByIdsRequest
            {
                BudgetIds = budgetIds
            };
            
            return (await CampaignService.CallAsync((s, r) => s.GetBudgetsByIdsAsync(r), request));
        }

        /// <summary>
        /// Gets the identifiers of campaigns that share each specified budget.
        /// </summary>
        /// <param name="budgetIds">A list of unique budget identifiers that identify the campaign identifiers to get.</param>
        /// <returns></returns>
        private async Task<GetCampaignIdsByBudgetIdsResponse> GetCampaignIdsByBudgetIdsAsync(IList<long> budgetIds)
        {
            var request = new GetCampaignIdsByBudgetIdsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignIdsByBudgetIdsAsync(r), request));
        }

        // Updates one or more budgets that can be shared by campaigns in the account.

        private async Task<UpdateBudgetsResponse> UpdateBudgetsAsync(IList<Budget> budgets)
        {
            var request = new UpdateBudgetsRequest
            {
                Budgets = budgets
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateBudgetsAsync(r), request));
        }

        // Deletes one or more budgets.

        private async Task DeleteBudgetsAsync(IList<long> budgetIds)
        {
            var request = new DeleteBudgetsRequest
            {
                BudgetIds = budgetIds
            };

            await CampaignService.CallAsync((s, r) => s.DeleteBudgetsAsync(r), request);
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

        // Adds one or more keywords to the specified ad group.

        private async Task<AddKeywordsResponse> AddKeywordsAsync(long adGroupId, IList<Keyword> keywords)
        {
            var request = new AddKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords
            };

            return (await CampaignService.CallAsync((s, r) => s.AddKeywordsAsync(r), request));
        }

        // Updates one or more keywords.

        private async Task<UpdateKeywordsResponse> UpdateKeywordsAsync(long adGroupId, IList<Keyword> keywords)
        {
            var request = new UpdateKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords
            };

            return await CampaignService.CallAsync((s, r) => s.UpdateKeywordsAsync(r), request);
        }

        private async Task<IList<Keyword>> GetKeywordsByAdGroupIdAsync(
            long adGroupId, 
            KeywordAdditionalField returnAdditionalFields)
        {
            var request = new GetKeywordsByAdGroupIdRequest
            {
                AdGroupId = adGroupId,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignService.CallAsync((s, r) => s.GetKeywordsByAdGroupIdAsync(r), request)).Keywords;
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


    }
}
