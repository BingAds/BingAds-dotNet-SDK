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
    /// This example demonstrates how to add ads and keywords to a new ad group, 
    /// and handle partial errors when some ads or keywords are not successfully created.
    /// </summary>
    public class KeywordsAds : ExampleBase
    {
        public override string Description
        {
            get { return "Keywords and Ads | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                var budgetIds = new List<long?>();
                var budgets = new List<Budget>();
                budgets.Add(new Budget
                {
                    Amount = 50,
                    BudgetType = BudgetLimitType.DailyBudgetStandard,
                    Name = "My Shared Budget " + DateTime.UtcNow,
                });

                budgetIds = (await CampaignManagementExampleHelper.AddBudgetsAsync(budgets)).BudgetIds.ToList();

                // Specify one or more campaigns.

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = budgetIds.Count > 0 ? budgetIds[0] : null,
                        DailyBudget = budgetIds.Count > 0 ? 0 : 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        
                        // You can set your campaign bid strategy to Enhanced CPC (EnhancedCpcBiddingScheme) 
                        // and then, at any time, set an individual ad group or keyword bid strategy to 
                        // Manual CPC (ManualCpcBiddingScheme).
                        // For campaigns you can use either of the EnhancedCpcBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then EnhancedCpcBiddingScheme is used by default.
                        BiddingScheme = new EnhancedCpcBiddingScheme { },
                        
                        TimeZone = "PacificTimeUSCanadaTijuana",
                                                
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
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                        Language = "English",
                        
                        // For ad groups you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new ManualCpcBiddingScheme { },

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
                // The TitlePart2 of the fourth ad is empty and not valid,
                // and the fifth ad is a duplicate of the second ad. 

                var ads = new Ad[] {
                    new ExpandedTextAd 
                    {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "Fast & Easy Setup",
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
                    new ExpandedTextAd {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "Quick & Easy Setup",
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
                                    Value = "PROMO2"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                    new ExpandedTextAd {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "Fast & Simple Setup",
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
                                    Value = "PROMO3"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                    new ExpandedTextAd {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "",
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
                                    Value = "PROMO4"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                    new ExpandedTextAd {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "Quick & Easy Setup",
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

                AddKeywordsResponse addKeywordsResponse = await CampaignManagementExampleHelper.AddKeywordsAsync((long)adGroupIds[0], keywords, null);
                long?[] keywordIds = addKeywordsResponse.KeywordIds.ToArray();
                BatchError[] keywordErrors = addKeywordsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(keywordIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(keywordErrors);

                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync((long)adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);

                // Here is a simple example that updates the campaign budget.
                // If the campaign has a shared budget you cannot update the Campaign budget amount,
                // and you must instead update the amount in the Budget object. If you try to update 
                // the budget amount of a campaign that has a shared budget, the service will return 
                // the CampaignServiceCannotUpdateSharedBudget error code.
                
                var getCampaigns = (await CampaignManagementExampleHelper.GetCampaignsByAccountIdAsync(
                    authorizationData.AccountId,
                    AllCampaignTypes
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
                        // Increase budget by 20 %
                        var updateCampaign = new Campaign
                        {
                            DailyBudget = campaign.DailyBudget * 1.2,
                            Id = campaign.Id,
                        };

                        updateCampaigns.Add(updateCampaign);
                    }
                }

                // Update shared budgets in Budget objects.
                if (getBudgetIds.Count > 0)
                {
                    // The UpdateBudgets operation only accepts 100 Budget objects per call. 
                    // To simply the example we will update the first 100.
                    getBudgetIds = getBudgetIds.Distinct().Take(100).ToList();
                    var getBudgets = (await CampaignManagementExampleHelper.GetBudgetsByIdsAsync(getBudgetIds)).Budgets;

                    OutputStatusMessage("List of shared budgets BEFORE update:\n");
                    foreach (var budget in getBudgets)
                    {
                        OutputStatusMessage("Budget:");
                        CampaignManagementExampleHelper.OutputBudget(budget);
                    }

                    OutputStatusMessage("List of campaigns that share each budget:\n");
                    var getCampaignIdCollection = (await CampaignManagementExampleHelper.GetCampaignIdsByBudgetIdsAsync(getBudgetIds)).CampaignIdCollection;
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
                    await CampaignManagementExampleHelper.UpdateBudgetsAsync(updateBudgets);

                    getBudgets = (await CampaignManagementExampleHelper.GetBudgetsByIdsAsync(getBudgetIds)).Budgets;

                    OutputStatusMessage("List of shared budgets AFTER update:\n");
                    foreach (var budget in getBudgets)
                    {
                        OutputStatusMessage("Budget:");
                        CampaignManagementExampleHelper.OutputBudget(budget);
                    }
                }

                // Update unshared budgets in Campaign objects.
                if(updateCampaigns.Count > 0)
                {
                    // The UpdateCampaigns operation only accepts 100 Campaign objects per call. 
                    // To simply the example we will update the first 100.
                    updateCampaigns = updateCampaigns.Take(100).ToList();
                    
                    foreach (var campaign in updateCampaigns)
                    {
                        getCampaignIds.Add((long)campaign.Id);
                    }

                    await CampaignManagementExampleHelper.UpdateCampaignsAsync(authorizationData.AccountId, updateCampaigns);

                    getCampaigns = (await CampaignManagementExampleHelper.GetCampaignsByIdsAsync(
                        authorizationData.AccountId,
                        getCampaignIds,
                        CampaignType.Search | CampaignType.Shopping | CampaignType.DynamicSearchAds
                    )).Campaigns;

                    OutputStatusMessage("List of campaigns with unshared budget AFTER budget update:\n");
                    foreach (var campaign in getCampaigns)
                    {
                        OutputStatusMessage("Campaign:");
                        CampaignManagementExampleHelper.OutputCampaign(campaign);
                    }
                }
                
                // Update the Text for the 3 successfully created ads, and update some UrlCustomParameters.
                var updateAds = new Ad[] {
                    new ExpandedTextAd {
                        Id = adIds[0],
                        Text = "Huge Savings on All Red Shoes.",
                        // Set the UrlCustomParameters element to null or empty to retain any 
                        // existing custom parameters.
                        UrlCustomParameters = null,
                    },
                    new ExpandedTextAd {
                        Id = adIds[1],
                        Text = "Huge Savings on All Red Shoes.",
                        // To remove all custom parameters, set the Parameters element of the 
                        // CustomParameters object to null or empty.
                        UrlCustomParameters = new CustomParameters {
                            Parameters = null,
                        },
                    },
                    new ExpandedTextAd {
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

                var getAdsByAdGroupIdResponse = await CampaignManagementExampleHelper.GetAdsByAdGroupIdAsync(
                    (long)adGroupIds[0],
                    AllAdTypes
                    );
                var updateAdsResponse = await CampaignManagementExampleHelper.UpdateAdsAsync((long)adGroupIds[0], updateAds);
                getAdsByAdGroupIdResponse = await CampaignManagementExampleHelper.GetAdsByAdGroupIdAsync(
                    (long)adGroupIds[0],
                    AllAdTypes
                    );


                // Here is a simple example that updates the keyword bid to use the ad group bid.

                var updateKeyword = new Keyword
                {
                    // Set Bid.Amount null (new empty Bid) to use the ad group bid.
                    // If the Bid property is null, your keyword bid will not be updated.
                    Bid = new Bid(),
                    Id = keywordIds[1],
                };

                // As an exercise you can step through using the debugger and view the results.

                var getKeywordsByAdGroupIdResponse = 
                    await CampaignManagementExampleHelper.GetKeywordsByAdGroupIdAsync((long)adGroupIds[0]);
                var updateKeywordsResponse = 
                    await CampaignManagementExampleHelper.UpdateKeywordsAsync((long)adGroupIds[0], new[] { updateKeyword }, null);
                getKeywordsByAdGroupIdResponse = 
                    await CampaignManagementExampleHelper.GetKeywordsByAdGroupIdAsync((long)adGroupIds[0]);
                
                // Delete the campaign, ad group, keyword, and ad that were previously added. 
                // You should remove this line if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("\nDeleted Campaign Id {0}\n", campaignIds[0]));

                // This sample will attempt to delete the budget that was created above.
                if (budgetIds.Count > 0)
                {
                    await CampaignManagementExampleHelper.DeleteBudgetsAsync(new[] { (long)budgetIds[0] });
                    OutputStatusMessage(string.Format("\nDeleted Budget Id {0}\n", budgetIds[0]));
                }
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
