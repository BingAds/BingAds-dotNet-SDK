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
    /// This example demonstrates how to use target criterions with the Campaign Management API.
    /// </summary>
    public class TargetCriterions : ExampleBase
    {
        public override string Description
        {
            get { return "Target Criterions | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                List<long> campaignIds = (await GetExampleCampaignIdsAsync(CampaignManagementExampleHelper, authorizationData)).ToList();

                // You can set campaignIds null to get all campaigns in the account, instead of 
                // adding and retrieving the example campaigns.

                var getCampaigns = (await CampaignManagementExampleHelper.GetCampaignsByIdsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: campaignIds,
                    campaignType: AllCampaignTypes)).Campaigns;

                // Loop through all campaigns and ad groups to get the target criterion IDs.

                foreach (var campaign in getCampaigns)
                {
                    var campaignId = (long)campaign.Id;

                    // Set campaignCriterionIds null to get all criterions 
                    // (of the specified target criterion type or types) for the current campaign.

                    var campaignCriterions = (await CampaignManagementExampleHelper.GetCampaignCriterionsByIdsAsync(
                        campaignId: campaignId,
                        campaignCriterionIds: null,
                        criterionType: AllTargetCampaignCriterionTypes))?.CampaignCriterions.ToList();

                    // When you first create a campaign or ad group using the Bing Ads API, it will not have any 
                    // criterions. Effectively, the brand new campaign and ad group target all ages, days, hours, 
                    // devices, genders, and locations. As a best practice, you should consider at a minimum 
                    // adding a campaign location criterion corresponding to the customer market country.

                    if (campaignCriterions == null || campaignCriterions.Count <= 0)
                    {
                        campaignCriterions = new List<CampaignCriterion>();

                        campaignCriterions.Add(new BiddableCampaignCriterion
                        {
                            CampaignId = campaignId,
                            Criterion = new LocationCriterion
                            {
                                // United States
                                LocationId = 190,
                                Type = "LocationCriterion"
                            },
                            CriterionBid = new BidMultiplier
                            {
                                Multiplier = 0
                            },
                            Type = "BiddableCampaignCriterion"
                        });

                        campaignCriterions.Add(new BiddableCampaignCriterion
                        {
                            CampaignId = campaignId,
                            Criterion = new LocationIntentCriterion
                            {
                                IntentOption = IntentOption.PeopleInOrSearchingForOrViewingPages
                            },
                        });

                        var addCampaignCriterionsResponse = await CampaignManagementExampleHelper.AddCampaignCriterionsAsync(
                            campaignCriterions: campaignCriterions,
                            criterionType: CampaignCriterionType.Targets
                        );

                        // Capture the new criterion IDs.

                        if (addCampaignCriterionsResponse?.CampaignCriterionIds.Count > 0)
                        {
                            var criterionIds = addCampaignCriterionsResponse?.CampaignCriterionIds;
                            for (int index = 0; index < criterionIds.Count; index++)
                            {
                                campaignCriterions[index].Id = criterionIds[index] ?? null;
                            }
                        }
                    }

                    // You can now store or output the campaign criterions.

                    OutputStatusMessage("Campaign Criterions: \n");
                    CampaignManagementExampleHelper.OutputArrayOfCampaignCriterion(campaignCriterions);

                    var getAdGroups = (await CampaignManagementExampleHelper.GetAdGroupsByCampaignIdAsync(
                        campaignId
                    )).AdGroups;

                    // Loop through all ad groups to get the target criterion IDs.
                    foreach (var adGroup in getAdGroups)
                    {
                        var adGroupId = (long)adGroup.Id;

                        // Set adGroupCriterionIds null to get all criterions 
                        // (of the specified target criterion type or types) for the current ad group.
                        var adGroupCriterions = (await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                            adGroupCriterionIds: null,
                            adGroupId: adGroupId,
                            criterionType: AllTargetAdGroupCriterionTypes)).AdGroupCriterions.ToList();


                        if (adGroupCriterions != null)
                        {
                            // If the Smartphones device criterion already exists, we'll increase the bid multiplier by 5 percent.

                            var updateAdGroupCriterions = new List<AdGroupCriterion>();
                            foreach (var adGroupCriterion in adGroupCriterions)
                            {
                                var deviceCriterion = adGroupCriterion.Criterion as DeviceCriterion;
                                if (deviceCriterion != null && string.Equals(deviceCriterion.DeviceName, "Smartphones"))
                                {
                                    ((BidMultiplier)((BiddableAdGroupCriterion)adGroupCriterion).CriterionBid).Multiplier *= 1.05;
                                    updateAdGroupCriterions.Add(adGroupCriterion);
                                }
                            }

                            if (updateAdGroupCriterions != null && updateAdGroupCriterions.ToList().Count > 0)
                            {
                                var updateAdGroupCriterionsResponse = await CampaignManagementExampleHelper.UpdateAdGroupCriterionsAsync(
                                    adGroupCriterions: updateAdGroupCriterions,
                                    criterionType: AdGroupCriterionType.Targets
                                );
                            }

                            // You can now store or output the ad group criterions.

                            OutputStatusMessage("Ad Group Criterions: ");
                            CampaignManagementExampleHelper.OutputArrayOfAdGroupCriterion(adGroupCriterions);
                        }
                    }
                }

                // Delete the campaign and ad group that were previously added. 

                await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignIds[0] });
                OutputStatusMessage(string.Format("\nDeleted Campaign Id {0}\n", campaignIds[0]));
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

        private async Task<IList<long>> GetExampleCampaignIdsAsync(
            CampaignManagementExampleHelper CampaignManagementExampleHelper,
            AuthorizationData authorizationData)
        {
            var campaigns = new[]{
                new Campaign
                {
                    Name = "Campaign One " + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    DailyBudget = 20,
                    BudgetType = BudgetLimitType.DailyBudgetStandard,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                },
            };

            var adGroups = new[] {
                new AdGroup
                {
                    Name = "Women's Shoe Sale One " + DateTime.UtcNow,
                    CpcBid = new Bid { Amount = 0.09 },
                    Language = "English",
                },
                new AdGroup
                {
                    Name = "Women's Shoe Sale Two " + DateTime.UtcNow,
                    CpcBid = new Bid { Amount = 0.09 },
                    Language = "English",
                },
                new AdGroup
                {
                    Name = "Women's Shoe Sale Three " + DateTime.UtcNow,
                    CpcBid = new Bid { Amount = 0.09 },
                    Language = "English",
                },
            };

            // Add the campaigns and ad groups

            OutputStatusMessage("Add campaigns:\n");
            AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(authorizationData.AccountId, campaigns);
            long?[] nillableCampaignIds = addCampaignsResponse.CampaignIds.ToArray();
            BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
            CampaignManagementExampleHelper.OutputArrayOfLong(nillableCampaignIds);
            CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

            OutputStatusMessage("Add ad groups:\n");
            AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync((long)nillableCampaignIds[0], adGroups, null);
            long?[] nillableAdGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
            BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
            CampaignManagementExampleHelper.OutputArrayOfLong(nillableAdGroupIds);
            CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

            List<long> campaignIds = new List<long>();
            foreach (var campaignId in nillableCampaignIds)
            {
                campaignIds.Add((long)campaignId);
            }

            return campaignIds;
        }
    }
}
