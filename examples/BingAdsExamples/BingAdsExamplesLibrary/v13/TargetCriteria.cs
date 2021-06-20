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
    /// How to show ads to people based on target criteria with the Campaign Management API.
    /// </summary>
    public class TargetCriteria : ExampleBase
    {
        public override string Description
        {
            get { return "Target Criteria | Campaign Management V13"; }
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

                // Setup a campaign with one ad group.

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
                
                // When you first create a campaign or ad group using the Bing Ads API, it will not have any 
                // target criteria. Effectively, the brand new campaign and ad group target all ages, days, hours, 
                // devices, genders, and locations. As a best practice, you should consider at a minimum 
                // adding a campaign location criterion corresponding to the customer market country.

                var campaignCriterions = new List<CampaignCriterion>();
                var campaignLocationCriterion = new BiddableCampaignCriterion
                {
                    CampaignId = (long)campaignIds[0],
                    Criterion = new LocationCriterion
                    {
                        // United States
                        LocationId = 190,
                        Type = "LocationCriterion"
                    },
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 20
                    },
                };
                campaignCriterions.Add(campaignLocationCriterion);

                OutputStatusMessage("-----\nAddCampaignCriterions:");
                var addCampaignCriterionsResponse = await CampaignManagementExampleHelper.AddCampaignCriterionsAsync(
                    campaignCriterions: campaignCriterions,
                    criterionType: CampaignCriterionType.Targets);
                long?[] campaignCriterionIds = addCampaignCriterionsResponse.CampaignCriterionIds.ToArray();
                OutputStatusMessage("CampaignCriterionIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignCriterionIds);
                BatchErrorCollection[] campaignCriterionErrors =
                    addCampaignCriterionsResponse.NestedPartialErrors.ToArray();
                OutputStatusMessage("NestedPartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(campaignCriterionErrors);
                
                // A negative location criterion is an excluded location.
                // Ads in this ad group will not be shown to people in Redmond, WA.

                var adGroupCriterions = new List<AdGroupCriterion>();
                var adGroupNegativeAgeCriterion = new NegativeAdGroupCriterion
                {
                    AdGroupId = (long)adGroupIds[0],
                    Criterion = new LocationCriterion
                    {
                        // Redmond|Washington|United States
                        LocationId = 67555,
                        Type = "LocationCriterion"
                    },
                };
                adGroupCriterions.Add(adGroupNegativeAgeCriterion);

                OutputStatusMessage("-----\nAddAdGroupCriterions:");
                var addAdGroupCriterionsResponse = await CampaignManagementExampleHelper.AddAdGroupCriterionsAsync(
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
