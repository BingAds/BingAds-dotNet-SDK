using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamples.V10
{
    /// <summary>
    /// This example demonstrates how to add ads and keywords to a new ad group, 
    /// and handle partial errors when some ads or keywords are not successfully created.
    /// </summary>
    public class KeywordsAds : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Keywords and Ads | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Specify one or more campaigns.

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Women's Shoes" + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        DaylightSaving = true,
                        
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
                               "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes"
                    },
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes",
                    },
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes",
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

                var updateCampaign = new Campaign
                {
                    Id = campaignIds[0],
                    MonthlyBudget = 500,
                };

                // As an exercise you can step through using the debugger and view the results.

                await GetCampaignsByIdsAsync(
                    authorizationData.AccountId, 
                    new [] { (long)campaignIds[0] },
                    CampaignType.SearchAndContent | CampaignType.Shopping
                );
                await UpdateCampaignsAsync(authorizationData.AccountId, new[] { updateCampaign });
                await GetCampaignsByIdsAsync(
                    authorizationData.AccountId,
                    new[] { (long)campaignIds[0] },
                    CampaignType.SearchAndContent | CampaignType.Shopping
                );
                
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

                await GetAdsByAdGroupIdAsync((long)adGroupIds[0]);
                var updateAdsResponse = await UpdateAdsAsync((long)adGroupIds[0], updateAds);
                await GetAdsByAdGroupIdAsync((long)adGroupIds[0]);


                // Here is a simple example that updates the keyword bid to use the ad group bid.
                
                var updateKeyword = new Keyword
                {
                    // Set Bid.Amount null (new empty Bid) to use the ad group bid.
                    // If the Bid property is null, your keyword bid will not be updated.
                    Bid = new Bid(),
                    Id = keywordIds[1],
                };

                // As an exercise you can step through using the debugger and view the results.

                await GetKeywordsByAdGroupIdAsync((long)adGroupIds[0]);
                await UpdateKeywordsAsync((long)adGroupIds[0], new[] { updateKeyword });
                await GetKeywordsByAdGroupIdAsync((long)adGroupIds[0]);

                // Delete the campaign, ad group, keyword, and ad that were previously added. 
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

        // Updates one or more campaigns.

        private async Task UpdateCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new UpdateCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            await Service.CallAsync((s, r) => s.UpdateCampaignsAsync(r), request);
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

        // Gets one or more campaigns for the specified campaign identifiers.

        private async Task<IList<Campaign>> GetCampaignsByIdsAsync(
            long accountId, 
            IList<long> campaignIds,
            CampaignType campaignType
            )
        {
            var request = new GetCampaignsByIdsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds,
                CampaignType = campaignType
            };

            return (await Service.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request)).Campaigns;
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

        // Updates one or more ad groups.

        private async Task UpdateAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new UpdateAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            await Service.CallAsync((s, r) => s.UpdateAdGroupsAsync(r), request);
        }

        // Adds one or more keywords to the specified ad group.

        private async Task<AddKeywordsResponse> AddKeywordsAsync(long adGroupId, IList<Keyword> keywords)
        {
            var request = new AddKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords
            };

            return (await Service.CallAsync((s, r) => s.AddKeywordsAsync(r), request));
        }

        // Updates one or more keywords.

        private async Task UpdateKeywordsAsync(long adGroupId, IList<Keyword> keywords)
        {
            var request = new UpdateKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords
            };

            await Service.CallAsync((s, r) => s.UpdateKeywordsAsync(r), request);
        }

        private async Task<IList<Keyword>> GetKeywordsByAdGroupIdAsync(long adGroupId)
        {
            var request = new GetKeywordsByAdGroupIdRequest
            {
                AdGroupId = adGroupId,
            };

            return (await Service.CallAsync((s, r) => s.GetKeywordsByAdGroupIdAsync(r), request)).Keywords;
        }

        // Adds one or more ads to the specified ad group.

        private async Task<AddAdsResponse> AddAdsAsync(long adGroupId, IList<Ad> ads)
        {
            var request = new AddAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await Service.CallAsync((s, r) => s.AddAdsAsync(r), request));
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

            return (await Service.CallAsync((s, r) => s.UpdateAdsAsync(r), request));
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

            return (await Service.CallAsync((s, r) => s.GetAdsByAdGroupIdAsync(r), request));
        }


    }
}
