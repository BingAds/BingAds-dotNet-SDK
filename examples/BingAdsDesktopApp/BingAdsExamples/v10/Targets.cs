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
    /// This example demonstrates how to associate targets with a campaign and ad group.
    /// </summary>
    public class Targets : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Targets | Campaign Management V10"; }
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
                    }
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
                        EndDate = new Date { Month = 12, Day = 31, Year = 2015 },
                        SearchBid = new Bid { Amount = 0.09 },
                        Language = "English",

                    }
                };

                // Add the campaign and ad group
                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                long campaignId = (long)campaignIds[0];

                AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)campaignId, adGroups);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                long adGroupId = (long)adGroupIds[0];

                // Print the new assigned campaign and ad group identifiers
                OutputCampaignsWithPartialErrors(campaigns, campaignIds, campaignErrors);
                OutputAdGroupsWithPartialErrors(adGroups, adGroupIds, adGroupErrors);

                // Create targets to associate with the campaign and ad group.

                var campaignTarget = new Target
                {
                    Name = "My Campaign Target",
                    
                    DeviceOS = new DeviceOSTarget
                    {
                        Bids = new[]
                            {
                                new DeviceOSTargetBid
                                {
                                    BidAdjustment = 10,
                                    DeviceName = "Tablets",
                                },
                            },
                    },
                    DayTime = new DayTimeTarget
                    {
                        Bids = new[]
                        {
                            new DayTimeTargetBid
                                {
                                    BidAdjustment = 10,
                                    Day = Day.Monday,
                                    FromHour = 1,
                                    ToHour = 12,
                                    FromMinute = Minute.Zero,
                                    ToMinute = Minute.FortyFive
                                }
                        }
                    },
                    Location = new LocationTarget
                    {
                        IntentOption = IntentOption.PeopleIn,
                        MetroAreaTarget = new MetroAreaTarget
                        {
                            Bids = new List<MetroAreaTargetBid>
                            {
                                new MetroAreaTargetBid
                                {
                                    BidAdjustment = 15,
                                    MetroArea = "Seattle-Tacoma, WA, WA US",
                                    IsExcluded = false
                                }
                            }
                        },
                        RadiusTarget = new RadiusTarget
                        {
                            Bids = new[]
                            {
                                new RadiusTargetBid
                                {
                                    BidAdjustment = 50,
                                    LatitudeDegrees = 47.755367,
                                    LongitudeDegrees = -122.091827,
                                    Radius = 5,
                                    RadiusUnit = DistanceUnit.Miles
                                }
                            }
                        }
                    }
                };

                var adGroupTarget = new Target
                {
                    Name = "My Ad Group Target",
                    DayTime = new DayTimeTarget
                    {
                        Bids = new[]
                        {
                            new DayTimeTargetBid
                                {
                                    BidAdjustment = 10,
                                    Day = Day.Friday,
                                    FromHour = 1,
                                    ToHour = 12,
                                    FromMinute = Minute.Zero,
                                    ToMinute = Minute.FortyFive
                                }
                        }
                    },
                };

                // Each customer has a target library that can be used to set up targeting for any campaign
                // or ad group within the specified customer. 

                // Add a target to the library and associate it with the campaign.
                var campaignTargetId = (await AddTargetsToLibraryAsync(new[] { campaignTarget }))[0];
                OutputStatusMessage(String.Format("Added Target Id: {0}\n", campaignTargetId));
                SetTargetToCampaignAsync(campaignId, campaignTargetId);
                OutputStatusMessage(String.Format("Associated CampaignId {0} with TargetId {1}.\n", campaignId, campaignTargetId));

                // Add a target to the library and associate it with the ad group.
                var adGroupTargetId = (await AddTargetsToLibraryAsync(new[] { adGroupTarget }))[0];
                OutputStatusMessage(String.Format("Added Target Id: {0}\n", adGroupTargetId));
                SetTargetToAdGroupAsync(adGroupId, adGroupTargetId);
                OutputStatusMessage(String.Format("Associated AdGroupId {0} with TargetId {1}.\n", adGroupId, adGroupTargetId));

                // Get and print the targets with the GetTargetsByIds operation
                OutputStatusMessage("Get Campaign and AdGroup targets: \n");
                var targets = await GetTargetsByIdsAsync(new[] { campaignTargetId, adGroupTargetId });
                foreach (var target in targets)
                {
                    OutputTarget(target);
                }
                
                // Update the ad group's Target object with additional target types.
                // Existing target types such as DayTime must be specified 
                // or they will not be included in the updated target.

                var updateAdGroupTarget = new Target
                {
                    Id = adGroupTargetId,
                    Name = "My Target",
                    Age = new AgeTarget
                    {
                        Bids = new[]
                            {
                                new AgeTargetBid
                                    {
                                        BidAdjustment = 10,
                                        Age = AgeRange.EighteenToTwentyFive
                                    }
                            }
                    },
                    DayTime = new DayTimeTarget
                    {
                        Bids = new[]
                        {
                            new DayTimeTargetBid
                                {
                                    BidAdjustment = 10,
                                    Day = Day.Friday,
                                    FromHour = 1,
                                    ToHour = 12,
                                    FromMinute = Minute.Zero,
                                    ToMinute = Minute.FortyFive
                                }
                        }
                    },
                    DeviceOS = new DeviceOSTarget
                    {
                        Bids = new[]
                            {
                                new DeviceOSTargetBid
                                {
                                    BidAdjustment = 20,
                                    DeviceName = "Tablets",
                                },
                            },
                    },
                    Gender = new GenderTarget
                    {
                        Bids = new[]
                            {
                                new GenderTargetBid
                                    {
                                        BidAdjustment = 10,
                                        Gender = GenderType.Female
                                    }
                            }
                    },
                    Location = new LocationTarget
                    {
                        IntentOption = IntentOption.PeopleSearchingForOrViewingPages,
                        CountryTarget = new CountryTarget
                        {
                            Bids = new[]
                                {
                                    new CountryTargetBid
                                        {
                                            BidAdjustment = 10,
                                            CountryAndRegion = "US",
                                            IsExcluded = false
                                        }
                                }
                        },
                        MetroAreaTarget = new MetroAreaTarget
                        {
                            Bids = new List<MetroAreaTargetBid>
                                            {
                                                new MetroAreaTargetBid
                                                    {
                                                        BidAdjustment = 15,
                                                        MetroArea = "Seattle-Tacoma, WA, WA US",
                                                        IsExcluded = false
                                                    }
                                            }
                        },
                        PostalCodeTarget = new PostalCodeTarget
                        {
                            Bids = new[]
                                {
                                    new PostalCodeTargetBid
                                        {
                                            // Bid adjustments are not allowed for location exclusions. 
                                            // If IsExcluded is true, this element will be ignored.
                                            BidAdjustment = 10,
                                            PostalCode = "98052, WA US",
                                            IsExcluded = true
                                        }
                                }
                        },
                        RadiusTarget = new RadiusTarget
                        {
                            Bids = new[]
                                {
                                    new RadiusTargetBid
                                        {
                                            BidAdjustment = 51,
                                            LatitudeDegrees = 47.755367,
                                            LongitudeDegrees = -122.091827,
                                            Radius = 11,
                                            RadiusUnit = DistanceUnit.Miles
                                        }
                                }
                        }
                    }
                };

                // Update the Target object associated with the ad group.  
                UpdateTargetsInLibraryAsync(new[] { updateAdGroupTarget });
                OutputStatusMessage("Updated the ad group level target as a Target object.\n");

                // Get and print the targets with the GetTargetsByIds operation
                OutputStatusMessage("Get Campaign and AdGroup targets: \n");
                targets = await GetTargetsByIdsAsync(new[] { campaignTargetId, adGroupTargetId });
                foreach (var target in targets)
                {
                    OutputTarget(target);
                }

                // Get all new and existing targets in the customer library, whether or not they are
                // associated with campaigns or ad groups.

                var allTargetsInfo = await GetTargetsInfoFromLibraryAsync();
                OutputStatusMessage("All target identifiers and names from the customer library: \n");
                PrintTargetsInfo(allTargetsInfo);

                // Delete the campaign, ad group, and targets that were previously added. 
                // DeleteCampaigns would remove the campaign and ad group, as well as the association
                // between ad groups and campaigns. To explicitly delete the association between an entity 
                // and the target, use DeleteTargetFromCampaign and DeleteTargetFromAdGroup respectively.

                DeleteTargetFromCampaignAsync(campaignId);
                DeleteTargetFromAdGroupAsync(adGroupId);

                DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignId });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignId));

                // DeleteCampaigns deletes the association between the campaign and target, but does not 
                // delete the target from the customer library. 
                // Call the DeleteTargetsFromLibrary operation for each target that you want to delete. 
                // You must specify an array with exactly one item.

                DeleteTargetsFromLibraryAsync(new[] { campaignTargetId });
                OutputStatusMessage(String.Format("Deleted TargetId {0}\n", campaignTargetId));

                DeleteTargetsFromLibraryAsync(new[] { adGroupTargetId });
                OutputStatusMessage(String.Format("Deleted TargetId {0}\n", adGroupTargetId));
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

        // Gets all target info from the customer library.
        private async Task<IEnumerable<TargetInfo>> GetTargetsInfoFromLibraryAsync()
        {
            return (await Service.CallAsync((s, r) => s.GetTargetsInfoFromLibraryAsync(r),
                new GetTargetsInfoFromLibraryRequest())).TargetsInfo;
        }

        // Gets the list of Target objects given the specified target identifiers.
        private async Task<IList<Target>> GetTargetsByIdsAsync(IList<long> targetIds)
        {
            var request = new GetTargetsByIdsRequest
            {
                TargetIds = targetIds,
            };

            return (await Service.CallAsync((s, r) => s.GetTargetsByIdsAsync(r), request)).Targets;
        }

        // Adds the specified Target object to the customer library. 
        // The operation requires exactly one Target in a list.
        private async Task<IList<long>> AddTargetsToLibraryAsync(IList<Target> targets)
        {
            var request = new AddTargetsToLibraryRequest
            {
                Targets = targets
            };

            return (await Service.CallAsync((s, r) => s.AddTargetsToLibraryAsync(r), request)).TargetIds.ToArray();
        }

        // Updates the specified Target object within the customer library. 
        // The operation requires exactly one Target in a list.
        private void UpdateTargetsInLibraryAsync(IList<Target> targets)
        {
            var request = new UpdateTargetsInLibraryRequest
            {
                Targets = targets
            };

            Service.CallAsync((s, r) => s.UpdateTargetsInLibraryAsync(r), request);
        }

        // Deletes the specified target from the customer library. 
        // The operation requires exactly one identifier in a list.
        private void DeleteTargetsFromLibraryAsync(IList<long> targetIds)
        {
            var request = new DeleteTargetsFromLibraryRequest
            {
                TargetIds = targetIds
            };

            Service.CallAsync((s, r) => s.DeleteTargetsFromLibraryAsync(r), request);
        }

        // Removes the target association from the specified campaign. 
        // Does not delete the target or the campaign.
        private void DeleteTargetFromCampaignAsync(long campaignId)
        {
            var request = new DeleteTargetFromCampaignRequest
            {
                CampaignId = campaignId
            };

            Service.CallAsync((s, r) => s.DeleteTargetFromCampaignAsync(r), request);
        }

        // Removes the target association from the specified ad group. 
        // Does not delete the target or the ad group.
        private void DeleteTargetFromAdGroupAsync(long adGroupId)
        {
            var request = new DeleteTargetFromAdGroupRequest
            {
                AdGroupId = adGroupId
            };

            Service.CallAsync((s, r) => s.DeleteTargetFromAdGroupAsync(r), request);
        }

        // Associates the specified campaign and target. 
        private void SetTargetToCampaignAsync(long campaignId, long targetId)
        {
            var request = new SetTargetToCampaignRequest
            {
                CampaignId = campaignId,
                TargetId = targetId
            };

            Service.CallAsync((s, r) => s.SetTargetToCampaignAsync(r), request);
        }

        // Associates the specified ad group and target. 
        private void SetTargetToAdGroupAsync(long adGroupId, long targetId)
        {
            var request = new SetTargetToAdGroupRequest
            {
                AdGroupId = adGroupId,
                TargetId = targetId
            };

            Service.CallAsync((s, r) => s.SetTargetToAdGroupAsync(r), request);
        }

        // Prints the info for each target. 
        private void PrintTargetsInfo(IEnumerable<TargetInfo> targetsInfo)
        {
            if (targetsInfo == null)
            {
                return;
            }

            foreach (var info in targetsInfo)
            {
                OutputStatusMessage(String.Format("Target Id: {0}", info.Id));
                OutputStatusMessage(String.Format("Target Name: {0}\n", info.Name));
            }
        }
    }
}
