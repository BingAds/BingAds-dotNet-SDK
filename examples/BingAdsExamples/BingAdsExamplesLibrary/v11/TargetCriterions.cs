using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.CampaignManagement;
using Microsoft.BingAds;

using ICampaignManagementServiceV10 = Microsoft.BingAds.V10.CampaignManagement.ICampaignManagementService;

using AddTargetsToLibraryRequestV10 = Microsoft.BingAds.V10.CampaignManagement.AddTargetsToLibraryRequest;
using AddTargetsToLibraryResponseV10 = Microsoft.BingAds.V10.CampaignManagement.AddTargetsToLibraryResponse;
using SetTargetToAdGroupRequestV10 = Microsoft.BingAds.V10.CampaignManagement.SetTargetToAdGroupRequest;

using TargetV10 = Microsoft.BingAds.V10.CampaignManagement.Target;
using DeviceOSTargetV10 = Microsoft.BingAds.V10.CampaignManagement.DeviceOSTarget;
using DeviceOSTargetBidV10 = Microsoft.BingAds.V10.CampaignManagement.DeviceOSTargetBid;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to use target criterions with the Campaign Management API.
    /// </summary>
    public class TargetCriterions : ExampleBase
    {
        public static ServiceClient<ICampaignManagementServiceV10> CampaignServiceV10;

        public override string Description
        {
            get { return "Target Criterions | Campaign Management V10 to V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);
                CampaignServiceV10 = new ServiceClient<ICampaignManagementServiceV10>(authorizationData);

                List<long> campaignIds = (await GetExampleCampaignIdsAsync(authorizationData)).ToList();
                
                // You can set campaignIds null to get all campaigns in the account, instead of 
                // adding and retrieving the example campaigns.

                var getCampaigns = (await GetCampaignsByIdsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: campaignIds,
                    campaignType: AllCampaignTypes)).Campaigns;
                
                // Loop through all campaigns and ad groups to get the target criterion IDs.

                foreach (var campaign in getCampaigns)
                {
                    var campaignId = (long)campaign.Id;

                    // Set campaignCriterionIds null to get all criterions 
                    // (of the specified target criterion type or types) for the current campaign.
                    
                    var campaignCriterions = (await GetCampaignCriterionsByIdsAsync(
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

                        var addCampaignCriterionsResponse = await AddCampaignCriterionsAsync(
                            campaignCriterions: campaignCriterions,
                            criterionType: CampaignCriterionType.Targets
                        );

                        // If the campaign used to shared target criterions with another campaign or ad group,
                        // and the add operation resulted in new targer criterion identifiers for this campaign,
                        // then we need to get the new criterion IDs.

                        // Otherwise we only need to capture the new criterion IDs.

                        if (addCampaignCriterionsResponse.IsMigrated == true)
                        {
                            campaignCriterions = (await GetCampaignCriterionsByIdsAsync(
                                campaignId: campaignId,
                                campaignCriterionIds: null,
                                criterionType: AllTargetCampaignCriterionTypes)).CampaignCriterions.ToList();
                        }
                        else if (addCampaignCriterionsResponse?.CampaignCriterionIds.Count > 0)
                        {
                            var criterionIds = addCampaignCriterionsResponse?.CampaignCriterionIds;
                            for (int index = 0; index < criterionIds.Count; index++)
                            {
                                campaignCriterions[index].Id = criterionIds[index] ?? null;
                            }
                        }
                    }
                    
                    // You can now store or output the campaign criterions, whether or not they were 
                    // migrated from a shared target library.

                    OutputStatusMessage("Campaign Criterions: \n");
                    OutputCampaignCriterions(campaignCriterions);

                    var getAdGroups = (await GetAdGroupsByCampaignIdAsync(
                        campaignId
                    )).AdGroups;

                    // Loop through all ad groups to get the target criterion IDs.
                    foreach (var adGroup in getAdGroups)
                    {
                        var adGroupId = (long)adGroup.Id;

                        // Set adGroupCriterionIds null to get all criterions 
                        // (of the specified target criterion type or types) for the current ad group.
                        var adGroupCriterions = (await GetAdGroupCriterionsByIdsAsync(
                            adGroupId: adGroupId,
                            adGroupCriterionIds: null,
                            criterionType: AllTargetAdGroupCriterionTypes)).AdGroupCriterions.ToList();


                        // If the Smartphones device criterion already exists, we'll increase the bid multiplier by 5 percent.
 
                        var updateAdGroupCriterions = new List<AdGroupCriterion>();
                        foreach(var adGroupCriterion in adGroupCriterions)
                        {
                            var deviceCriterion = adGroupCriterion.Criterion as DeviceCriterion;
                            if(deviceCriterion != null && string.Equals(deviceCriterion.DeviceName, "Smartphones"))
                            {
                                ((BidMultiplier)((BiddableAdGroupCriterion)adGroupCriterion).CriterionBid).Multiplier *= 1.05;
                                updateAdGroupCriterions.Add(adGroupCriterion);
                            }
                        }
                        
                        if (updateAdGroupCriterions != null && updateAdGroupCriterions.ToList().Count > 0)
                        {
                            var updateAdGroupCriterionsResponse = await UpdateAdGroupCriterionsAsync(
                                adGroupCriterions: updateAdGroupCriterions,
                                criterionType: AdGroupCriterionType.Targets
                            );

                            // If the ad group used to shared target criterions with another campaign or ad group,
                            // and the update operation resulted in new target criterion identifiers for this ad group,
                            // then we need to get the new criterion IDs.

                            if (updateAdGroupCriterionsResponse.IsMigrated == true)
                            {
                                adGroupCriterions = (await GetAdGroupCriterionsByIdsAsync(
                                    adGroupId: adGroupId,
                                    adGroupCriterionIds: null,
                                    criterionType: AllTargetAdGroupCriterionTypes)).AdGroupCriterions.ToList();
                            }
                        }
                        
                        // You can now store or output the ad group criterions, whether or not they were 
                        // migrated from a shared target library.

                        OutputStatusMessage("Ad Group Criterions: ");
                        OutputAdGroupCriterions(adGroupCriterions);
                    }
                }

                // Delete the campaign and ad group that were previously added. 

                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignIds[0] });
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

        private async Task<IList<long>> GetExampleCampaignIdsAsync(AuthorizationData authorizationData)
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
                    AdDistribution = AdDistribution.Search,
                    SearchBid = new Bid { Amount = 0.09 },
                    Language = "English",
                },
                new AdGroup
                {
                    Name = "Women's Shoe Sale Two " + DateTime.UtcNow,
                    AdDistribution = AdDistribution.Search,
                    SearchBid = new Bid { Amount = 0.09 },
                    Language = "English",
                },
                new AdGroup
                {
                    Name = "Women's Shoe Sale Three " + DateTime.UtcNow,
                    AdDistribution = AdDistribution.Search,
                    SearchBid = new Bid { Amount = 0.09 },
                    Language = "English",
                },
            };

            // Add the campaigns and ad groups

            OutputStatusMessage("Add campaigns:\n");
            AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
            long?[] nillableCampaignIds = addCampaignsResponse.CampaignIds.ToArray();
            BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
            OutputIds(nillableCampaignIds);
            OutputPartialErrors(campaignErrors);

            OutputStatusMessage("Add ad groups:\n");
            AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)nillableCampaignIds[0], adGroups);
            long?[] nillableAdGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
            BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
            OutputIds(nillableAdGroupIds);
            OutputPartialErrors(adGroupErrors);

            // This example uses the deprecated version 10 shared target library in order to later demonstrate
            // the inline migration from shared target criterions to unshared target criterions.

            List<long> adGroupIds = new List<long>();
            foreach (var adGroupId in nillableAdGroupIds)
            {
                adGroupIds.Add((long)adGroupId);
            }

            // The shared target ID is output within the ShareDeprecatedTargetsAsync method.
            // We won't do anything further with it in this example.

            var sharedTargetId = await ShareDeprecatedTargetsAsync(authorizationData, adGroupIds);

            List<long> campaignIds = new List<long>();
            foreach (var campaignId in nillableCampaignIds)
            {
                campaignIds.Add((long)campaignId);
            }

            return campaignIds;
        }

        /// <summary>
        /// Shares a target with multiple new ad groups. This helper function is used to setup
        /// criterion migration scenarios.
        /// 
        /// This is an example of a deprecated scenario. In Bing Ads API version 11 you can no longer use 
        /// the AddTargetsToLibrary, SetTargetToCampaign, or SetTargetToAdGroup operations. Instead you will 
        /// be required to use criterions. Support for targets will end no later than the sunset 
        /// of Bing Ads API version 10. 
        /// </summary>
        /// <param name="authorizationData"></param>
        /// <returns></returns>
        private async Task<long> ShareDeprecatedTargetsAsync(
            AuthorizationData authorizationData,
            IList<long> adGroupIds)
        {            
            var sharedTarget = new TargetV10
            {
                Name = "My Target",

                DeviceOS = new DeviceOSTargetV10
                {
                    Bids = new[]
                    {
                            new DeviceOSTargetBidV10
                            {
                                BidAdjustment = 20,
                                DeviceName = "Computers",
                            },
                        },
                },
            };

            var addTargetsToLibraryResponse = await AddTargetsToLibraryAsync(new[] { sharedTarget });
            var sharedTargetId = addTargetsToLibraryResponse.TargetIds[0];
            OutputStatusMessage(string.Format("Added Target Id: {0}\n", sharedTargetId));

            await SetTargetToAdGroupAsync(adGroupIds[0], sharedTargetId);
            OutputStatusMessage(string.Format("Associated AdGroupId {0} with TargetId {1}.\n", adGroupIds[0], sharedTargetId));
            await SetTargetToAdGroupAsync(adGroupIds[1], sharedTargetId);
            OutputStatusMessage(string.Format("Associated AdGroupId {0} with TargetId {1}.\n", adGroupIds[1], sharedTargetId));
            await SetTargetToAdGroupAsync(adGroupIds[2], sharedTargetId);
            OutputStatusMessage(string.Format("Associated AdGroupId {0} with TargetId {1}.\n", adGroupIds[2], sharedTargetId));
            
            return sharedTargetId;
        }

        // Adds the specified Target object to the customer library. 
        private async Task<AddTargetsToLibraryResponseV10> AddTargetsToLibraryAsync(IList<TargetV10> targets)
        {
            var request = new AddTargetsToLibraryRequestV10
            {
                Targets = targets
            };

            return (await CampaignServiceV10.CallAsync((s, r) => s.AddTargetsToLibraryAsync(r), request));
        }
        
        // Associates the specified ad group and target. 
        private async Task SetTargetToAdGroupAsync(long adGroupId, long targetId)
        {
            var request = new SetTargetToAdGroupRequestV10
            {
                AdGroupId = adGroupId,
                TargetId = targetId,
                ReplaceAssociation = true
            };

            await CampaignServiceV10.CallAsync((s, r) => s.SetTargetToAdGroupAsync(r), request);
        }
    }
}
