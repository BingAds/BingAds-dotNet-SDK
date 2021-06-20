using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to add responsive search ads in a new ad group.
    /// </summary>
    public class ResponsiveSearchAds : ExampleBase
    {
        public override string Description
        {
            get { return "Responsive Search Ads | Campaign Management V13"; }
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

                // Create a Search campaign with one ad group and a responsive search ad.

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Everyone's Shoes " + DateTime.UtcNow,                      
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        Languages = new string[] { "All"},
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

                // The responsive search ad descriptions and headlines are stored as text assets. 
                // You must set between 2-4 descriptions and 3-15 headlines.

                var ads = new Ad[] {
                    new ResponsiveSearchAd
                    {
                        Descriptions = new AssetLink []
                        {
                            new AssetLink
                            {
                                Asset = new TextAsset
                                {
                                    Text = "Find New Customers & Increase Sales!"
                                },
                                PinnedField = "Description1"
                            },
                            new AssetLink
                            {
                                Asset = new TextAsset
                                {
                                    Text = "Start Advertising on Contoso Today."
                                },
                                PinnedField = "Description2"
                            },
                        },
                        Headlines = new AssetLink []
                        {
                            new AssetLink
                            {
                                Asset = new TextAsset
                                {
                                    Text = "Contoso"
                                },
                                PinnedField = "Headline1"
                            },
                            new AssetLink
                            {
                                Asset = new TextAsset
                                {
                                    Text = "Quick & Easy Setup"
                                },
                                PinnedField = null
                            },
                            new AssetLink
                            {
                                Asset = new TextAsset
                                {
                                    Text = "Seemless Integration"
                                },
                                PinnedField = null
                            },
                        },
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
