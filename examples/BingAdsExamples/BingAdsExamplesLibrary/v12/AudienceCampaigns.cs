using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to set up Audience campaigns
    /// with the Campaign Management service.
    /// </summary>
    public class AudienceCampaigns : ExampleBase
    {
        // You'll need to add media before you can run this example. 
        // For details, see ImageMedia.cs

        public const long LandscapeImageMediaId = 0;
        public const long LandscapeLogoMediaId = 0;
        public const long SquareImageMediaId = 0;
        public const long SquareLogoMediaId = 0;
        
        public override string Description
        {
            get { return "Audience Campaigns | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CampaignManagementExampleHelper CampaignManagementExampleHelper = 
                    new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = 
                    new ServiceClient<ICampaignManagementService>(authorizationData, environment);

                // Setup an Audience campaign with one ad group and a responsive ad.

                var campaigns = new[]{
                    new Campaign
                    {
                        // CampaignType must be set for Audience campaigns
                        CampaignType = CampaignType.Audience,
                        // Languages must be set for Audience campaigns
                        Languages = new string[] { "All" },
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
                };

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
                        // Language cannot be set for ad groups in Audience campaigns
                        Language = null,
                        // Network cannot be set for ad groups in Audience campaigns
                        Network = null,
                        Settings = new[]
                        {
                            new TargetSetting
                            {
                                // By including the corresponding TargetSettingDetail, 
                                // this example sets the "target and bid" option for 
                                // CompanyName, Industry, and JobFunction. We will only deliver ads to 
                                // people who meet at least one of your criteria.
                                // By default the "bid only" option is set for Audience, Age, and Gender.
                                // We will deliver ads to all audiences, ages, and genders, if they meet
                                // your company name, industry, or job function criteria. 
                                Details = new []
                                {
                                    new TargetSettingDetail
                                    {
                                        CriterionTypeGroup = CriterionTypeGroup.CompanyName,
                                        TargetAndBid = true
                                    },
                                    new TargetSettingDetail
                                    {
                                        CriterionTypeGroup = CriterionTypeGroup.Industry,
                                        TargetAndBid = true
                                    },
                                    new TargetSettingDetail
                                    {
                                        CriterionTypeGroup = CriterionTypeGroup.JobFunction,
                                        TargetAndBid = true
                                    },
                                }
                            }
                        },
                    }
                };

                var ads = new Ad[] {
                    new ResponsiveAd
                    {
                        // Not applicable for responsive ads
                        AdFormatPreference = null,
                        BusinessName = "Contoso",
                        CallToAction = CallToAction.AddToCart,
                        // Not applicable for responsive ads
                        DevicePreference = null,
                        EditorialStatus = null,
                        FinalAppUrls = null,
                        FinalMobileUrls = new[] {
                            "http://mobile.contoso.com/womenshoesale"
                        },
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                        ForwardCompatibilityMap = null,
                        Headline = "Fast & Easy Setup",
                        Id = null,
                        LandscapeImageMediaId = LandscapeImageMediaId,
                        LandscapeLogoMediaId = LandscapeLogoMediaId,
                        LongHeadline = "Find New Customers & Increase Sales!",
                        SquareImageMediaId = SquareImageMediaId,
                        SquareLogoMediaId = SquareLogoMediaId,
                        Status = null,
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                        TrackingUrlTemplate = null,
                        Type = null,
                        UrlCustomParameters = null,
                    }
                };

                // Add the campaign, ad group, and ad

                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(
                    authorizationData.AccountId, 
                    campaigns,
                    false);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync(
                    (long)campaignIds[0], 
                    adGroups,
                    false);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);
                
                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync((long)adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);


                // Whether or not the "target and bid" option has been set for a given
                // criterion type group, you can set bid adjustments for specific criteria.

                var adGroupCriterions = new List<AdGroupCriterion>();

                var adGroupCompanyNameCriterion = new BiddableAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    CriterionBid = new FixedBid
                    {
                        Amount = 0.50
                    },
                    Criterion = new ProfileCriterion
                    {
                        ProfileId = 808251207, // Microsoft
                        ProfileType = ProfileType.CompanyName
                    },
                };
                adGroupCriterions.Add(adGroupCompanyNameCriterion);

                var adGroupIndustryCriterion = new BiddableAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    CriterionBid = new FixedBid
                    {
                        Amount = 0.50
                    },
                    Criterion = new ProfileCriterion
                    {
                        ProfileId = 807658654, // Computer & Network Security
                        ProfileType = ProfileType.Industry
                    },
                };
                adGroupCriterions.Add(adGroupIndustryCriterion);

                var adGroupJobFunctionCriterion = new BiddableAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    CriterionBid = new FixedBid
                    {
                        Amount = 0.50
                    },
                    Criterion = new ProfileCriterion
                    {
                        ProfileId = 807658477, // Engineering
                        ProfileType = ProfileType.JobFunction
                    },
                };
                adGroupCriterions.Add(adGroupJobFunctionCriterion);

                // Exclude ages twenty-five through thirty-four.

                var adGroupNegativeAgeCriterion = new NegativeAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    Criterion = new AgeCriterion
                    {
                        AgeRange = AgeRange.TwentyFiveToThirtyFour
                    },
                };
                adGroupCriterions.Add(adGroupNegativeAgeCriterion);

                OutputStatusMessage("Adding Ad Group Criteria . . . \n");
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                AddAdGroupCriterionsResponse addAdGroupCriterionsResponse =
                    await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(
                        adGroupCriterions, 
                        AdGroupCriterionType.Targets);
                long?[] adGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds.ToArray();
                OutputStatusMessage("New Ad Group Criterion Ids:\n");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupCriterionIds);
                BatchErrorCollection[] adGroupCriterionErrors =
                    addAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("\nAddAdGroupCriterions Errors:\n");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(adGroupCriterionErrors);
                
                // Delete the campaign, ad group, criteria, and ad that were previously added. 
                // You should remove this line if you want to view the added entities in the 
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
