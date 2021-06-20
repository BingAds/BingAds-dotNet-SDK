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
    /// How to create Expanded Text Ads with the Campaign Management service.
    /// </summary>
    public class ExpandedTextAds : ExampleBase
    {
        public override string Description
        {
            get { return "Expanded Text Ads | Campaign Management V13"; }
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

                // Add a search campaign.

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
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

                // Add an ad group within the campaign.

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

                // Add keywords and ads within the ad group.

                var keywords = new[] {
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes",
                    },
                };

                OutputStatusMessage("-----\nAddKeywords:");
                AddKeywordsResponse addKeywordsResponse = await CampaignManagementExampleHelper.AddKeywordsAsync(
                    adGroupId: (long)adGroupIds[0],
                    keywords: keywords,
                    returnInheritedBidStrategyTypes: false);
                long?[] keywordIds = addKeywordsResponse.KeywordIds.ToArray();
                BatchError[] keywordErrors = addKeywordsResponse.PartialErrors.ToArray();
                OutputStatusMessage("KeywordIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(keywordIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(keywordErrors);

                var ads = new Ad[] {
                    new ExpandedTextAd 
                    {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "Quick & Easy Setup",
                        TitlePart3 = "Seemless Integration",
                        Text = "Find New Customers & Increase Sales!",
                        TextPart2 = "Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",
                        FinalUrls = new[] {
                            "https://www.contoso.com/womenshoesale"
                        },
                    },
                };
                
                OutputStatusMessage("-----\nAddAds:");
                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync(
                    adGroupId: (long)adGroupIds[0], 
                    ads: ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
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
            catch (FaultException<AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<EditorialApiFaultDetail> ex)
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
