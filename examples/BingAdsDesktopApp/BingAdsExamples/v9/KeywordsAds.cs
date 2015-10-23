using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsExamples.V9
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
            get { return "Keywords and Ads | Campaign Management V9 (Deprecated)"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Specify one or more campaigns.

                var campaign = new Campaign
                {
                    Name = "Women's Shoes" + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                    MonthlyBudget = 1000.00,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                    DaylightSaving = true,
                };

                // Specify one or more ad groups.

                var adGroup = new AdGroup
                {
                    Name = "Women's Red Shoe Sale",
                    AdDistribution = AdDistribution.Search,
                    BiddingModel = BiddingModel.Keyword,
                    PricingModel = PricingModel.Cpc,
                    StartDate = null,
                    EndDate = new Date { Month = 12, Day = 31, Year = 2015 },
                    ExactMatchBid = new Bid { Amount = 0.09 },
                    PhraseMatchBid = new Bid { Amount = 0.07 },
                    Language = "English",

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
                        Text = "Brand-A Shoes"
                    },
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes"
                    }
                };

                // In this example only the second ad should succeed. The Title of the first ad is empty and not valid,
                // and the third ad is a duplicate of the second ad. 

                var ads = new Ad[] {
                    new TextAd 
                    {
                        DisplayUrl = "Contoso.com",
                        Text = "Huge Savings on red shoes.",
                        Title = "",
                        // Destination URLs are deprecated and will be sunset in March 2016. 
                        // If you are currently using the Destination URL, you must use Bing Ads 
                        // Campaign Management service version 10 and upgrade to Final URLs.
                        // Here is an example of a DestinationUrl in version 9. 
                        DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",
                    },
                    new TextAd {
                        DisplayUrl = "Contoso.com",
                        Text = "Huge Savings on red shoes.",
                        Title = "Women's Shoe Sale",

                        // Destination URLs are deprecated and will be sunset in March 2016. 
                        // If you are currently using the Destination URL, you must use Bing Ads 
                        // Campaign Management service version 10 and upgrade to Final URLs.
                        // Here is an example of a DestinationUrl in version 9. 
                        DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",
                    },
                    new TextAd {
                        DisplayUrl = "Contoso.com",
                        Text = "Huge Savings on red shoes.",
                        Title = "Women's Shoe Sale",
                        // Destination URLs are deprecated and will be sunset in March 2016. 
                        // If you are currently using the Destination URL, you must use Bing Ads 
                        // Campaign Management service version 10 and upgrade to Final URLs.
                        // Here is an example of a DestinationUrl in version 9. 
                        DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",
                    }
                };

                // Add the campaign, ad group, keywords, and ads

                var campaignIds = (long[]) await AddCampaignsAsync(authorizationData.AccountId, new[] { campaign });
                var adGroupIds = (long[])await AddAdGroupsAsync(campaignIds[0], new[] { adGroup });

                AddKeywordsResponse addKeywordsResponse = await AddKeywordsAsync(adGroupIds[0], keywords);
                long?[] keywordIds = addKeywordsResponse.KeywordIds.ToArray();
                BatchError[] keywordErrors = addKeywordsResponse.PartialErrors.ToArray();
                
                AddAdsResponse addAdsResponse = await AddAdsAsync(adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();

                // Print the new assigned campaign and ad group identifiers

                PrintCampaignIdentifiers(campaignIds);
                PrintAdGroupIdentifiers(adGroupIds);

                // Print the new assigned keyword and ad identifiers, as well as any partial errors

                PrintKeywordResults(keywords, keywordIds, keywordErrors);
                PrintAdResults(ads, adIds, adErrors);

                // Here is a simple example that updates the campaign budget

                var updateCampaign = new Campaign
                {
                    Id = campaignIds[0],
                    MonthlyBudget = 500,
                };

                UpdateCampaignsAsync(authorizationData.AccountId, new[] { updateCampaign });

                // Here is a simple example that updates the keyword bid to use the ad group bid

                var updateKeyword = new Keyword
                {
                    // Set Bid.Amount null (new empty Bid) to use the ad group bid.
                    // If the Bid property is null, your keyword bid will not be updated.
                    Bid = new Bid(),
                    Id = keywordIds[1],
                };

                await GetKeywordsByAdGroupIdAsync(adGroupIds[0]);
                await UpdateKeywordsAsync(adGroupIds[0], new[] { updateKeyword });
                await GetKeywordsByAdGroupIdAsync(adGroupIds[0]);

                // Delete the campaign, ad group, keyword, and ad that were previously added. 
                // You should remove this line if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignIds[0]));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CampaignManagement.EditorialApiFaultDetail> ex)
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

        private async Task<IList<long>> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await Service.CallAsync((s, r) => s.AddCampaignsAsync(r), request)).CampaignIds;
        }

        // Updates one or more campaigns.

        private void UpdateCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new UpdateCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            Service.CallAsync((s, r) => s.UpdateCampaignsAsync(r), request);
        }

        // Deletes one or more campaigns from the specified account.

        private void DeleteCampaignsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            Service.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        // Gets one or more campaigns in the specified account.

        private async Task<IList<Campaign>> GetCampaignsByIdsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new GetCampaignsByIdsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            return (await Service.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request)).Campaigns;
        }

        // Adds one or more ad groups to the specified campaign.

        private async Task<IList<long>> AddAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };
            
            return (await Service.CallAsync((s, r) => s.AddAdGroupsAsync(r), request)).AdGroupIds;
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


        // Prints the campaign identifiers for each campaign added. 
        private void PrintCampaignIdentifiers(IEnumerable<long> campaignIds)
        {
            if (campaignIds == null)
            {
                return;
            }

            foreach (var id in campaignIds)
            {
                OutputStatusMessage(String.Format("Campaign successfully added and assigned CampaignId {0}\n", id));
            }
        }

        // Prints the ad group identifiers for each ad group added. 
        private void PrintAdGroupIdentifiers(IEnumerable<long> adGroupIds)
        {
            if (adGroupIds == null)
            {
                return;
            }

            foreach (var id in adGroupIds)
            {
                OutputStatusMessage(String.Format("AdGroup successfully added and assigned AdGroupId {0}\n", id));
            }
        }

        // Prints the keyword identifiers, as well as any partial errors

        private void PrintKeywordResults(Keyword[] keywords, long?[] keywordIds, IEnumerable<BatchError> partialErrors)
        {
            if (keywordIds == null)
            {
                return;
            }

            // Print the identifier of each successfully added keyword.

            for (var index = 0; index < keywords.Length; index++)
            {
                // The array of keyword identifiers equals the size of the attempted keywords. If the element 
                // is not null, the keyword at that index was added successfully and has a keyword identifer. 

                if (keywordIds[index] != null)
                {
                    OutputStatusMessage(String.Format("Keyword[{0}] (Text:{1}) successfully added and assigned KeywordId {2}",
                        index,
                        keywords[index].Text,
                        keywordIds[index]));
                }
            }

            // Print the error details for any keyword not successfully added.
            // Note also that multiple error reasons may exist for the same attempted keyword.

            foreach (BatchError error in partialErrors)
            {
                // The index of the partial errors is equal to the index of the list
                // specified in the call to AddKeywords.

                OutputStatusMessage(String.Format("\nKeyword[{0}] (Text:{1}) not added due to the following error:", 
                    error.Index, keywords[error.Index].Text));

                OutputStatusMessage(String.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(String.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(String.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(String.Format("\tMessage: {0}", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(String.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(String.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(String.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(String.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }

            OutputStatusMessage("\n");
        }

        // Prints the ad identifiers, as well as any partial errors

        private void PrintAdResults(IList<Ad> ads, IList<long?> adIds, IEnumerable<BatchError> partialErrors)
        {
            if (adIds == null)
            {
                return;
            }

            var attributeValues = new string[ads.Count];

            for (var index = 0; index < ads.Count; index++)
            {
                // Determine the type of ad. Prepare the corresponding attribute value to be printed,
                // both for successful new ads and partial errors. 

                var ad = ads[index] as TextAd;
                if (ad != null)
                {
                    attributeValues[index] = "Title:" + ad.Title;
                }
                else
                {
                    var mobileAd = ads[index] as MobileAd;
                    if (mobileAd != null)
                    {
                        attributeValues[index] = "Title:" + mobileAd.Title;
                    }
                    else
                    {
                        var productAd = ads[index] as ProductAd;
                        if (productAd != null)
                        {
                            attributeValues[index] = "PromotionalText:" + productAd.PromotionalText;
                        }
                        else
                        {
                            attributeValues[index] = "Unknown Ad Type";
                        }
                    }
                }

                // The array of ad identifiers equals the size of the attempted ads. If the element 
                // is not null, the ad at that index was added successfully and has an ad identifer. 

                if (adIds[index] != null)
                {
                    OutputStatusMessage(String.Format("Ad[{0}] ({1}) successfully added and assigned AdId {2}",
                        index,
                        attributeValues[index],
                        adIds[index]));
                }
            }

            foreach (BatchError error in partialErrors)
            {
                // The index of the partial errors is equal to the index of the list
                // specified in the call to AddAds.

                OutputStatusMessage(String.Format("\nAd[{0}] ({1}) not added due to the following error:", 
                    error.Index, attributeValues[error.Index]));

                OutputStatusMessage(String.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(String.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(String.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(String.Format("\tMessage: {0}", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(String.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(String.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(String.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(String.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }

            OutputStatusMessage("\n");
        }
    }
}
