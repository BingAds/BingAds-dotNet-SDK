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
    /// How to apply Profile Criteria e.g., LinkedIn targeting with the Campaign Management service.
    /// </summary>
    public class ProfileCriteria : ExampleBase
    {
        public override string Description
        {
            get { return "Profile Criteria | Campaign Management V13"; }
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

                // Create an Audience campaign with one ad group.

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        // CampaignType must be set for Audience campaigns
                        CampaignType = CampaignType.Audience,
                        DailyBudget = 50,
                        // Languages must be set for Audience campaigns
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
                        // Network cannot be set for ad groups in Audience campaigns
                        Network = null,
                        Settings = new[]
                        {
                            new TargetSetting
                            {
                                // Sets the "target and bid" option for CompanyName, Industry, and JobFunction. 
                                // Microsoft will only deliver ads to people who meet at least one of your criteria.
                                // By default the "bid only" option is set for Audience, Age, and Gender.
                                // Microsoft will deliver ads to all audiences, ages, and genders, if they meet
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
                                
                // Whether or not the "target and bid" option has been set for a given
                // criterion type group, you can set bid adjustments for specific criteria.

                var adGroupCriterions = new List<AdGroupCriterion>();

                var adGroupCompanyNameCriterion = new BiddableAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 15
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
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 20
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
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 25
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

                OutputStatusMessage("-----\nAddAdGroupCriterions:");
                CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                AddAdGroupCriterionsResponse addAdGroupCriterionsResponse = await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(
                        adGroupCriterions: adGroupCriterions,
                        criterionType: AdGroupCriterionType.Targets);
                long?[] adGroupCriterionIds = addAdGroupCriterionsResponse.AdGroupCriterionIds.ToArray();
                OutputStatusMessage("AdGroupCriterionIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupCriterionIds);
                BatchErrorCollection[] adGroupCriterionErrors =
                    addAdGroupCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("NestedPartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(adGroupCriterionErrors);

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
