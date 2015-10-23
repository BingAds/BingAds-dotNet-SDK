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
    /// This example demonstrates how to associate targets with a campaign and ad group. 
    /// Bing Ads Version 9 supports both Target and Target2 objects. You should use Target2. 
    /// This example compares Target and Target2, and demonstrates the impact of updating the DayTimeTarget, 
    /// IntentOption, and RadiusTarget2 nested in a Target2 object.
    /// </summary>
    public class Targets : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Targets | Campaign Management V9 (Deprecated)"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Specify a campaign. 
                var campaign = new Campaign
                {
                    Name = "Women's Shoes" + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                    MonthlyBudget = 1000.00,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                    DaylightSaving = true
                };

                // Specify an ad group. 
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
                    Language = "English"

                };

                // Add the campaign and ad group
                var campaignIds = (long[])await AddCampaignsAsync(authorizationData.AccountId, new[] { campaign });
                var adGroupIds = (long[])await AddAdGroupsAsync(campaignIds[0], new[] { adGroup });

                // Print the new assigned campaign and ad group identifiers
                PrintCampaignIdentifiers(campaignIds);
                PrintAdGroupIdentifiers(adGroupIds);

                // Bing Ads API Version 9 supports both Target and Target2 objects. You should use Target2. 
                // This example compares Target and Target2, and demonstrates the impact of updating the 
                // DayTimeTarget, IntentOption, and RadiusTarget2 nested in a Target2 object. 

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
                                    DeviceName = "Smartphones",
                                },
                            },
                    },
                    Day = new DayTarget
                    {
                        Bids = new[]
                                {
                                    new DayTargetBid
                                        {
                                            BidAdjustment = 10,
                                            Day = Day.Friday
                                        }
                                }
                    },
                    Hour = new HourTarget
                    {
                        Bids = new[]
                                {
                                    new HourTargetBid
                                        {
                                            BidAdjustment = 10,
                                            Hour = HourRange.ElevenAMToTwoPM
                                        }
                                }
                    },
                    Location = new LocationTarget
                    {
                        HasPhysicalIntent = true,
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
                                                Radius = 5
                                            }
                                    }
                        }
                    }
                };

                var adGroupTarget = new Target
                {
                    Name = "My Ad Group Target",
                    Hour = new HourTarget
                    {
                        Bids = new[]
                                {
                                    new HourTargetBid
                                        {
                                            BidAdjustment = 10,
                                            Hour = HourRange.SixPMToElevenPM
                                        }
                                }
                    }
                };

                // Each customer has a target library that can be used to set up targeting for any campaign
                // or ad group within the specified customer. 

                // Add a target to the library and associate it with the campaign.
                var campaignTargetId = (await AddTargetsToLibraryAsync(new[] { campaignTarget }))[0];
                OutputStatusMessage(String.Format("Added Target Id: {0}\n", campaignTargetId));
                SetTargetToCampaignAsync(campaignIds[0], campaignTargetId);
                OutputStatusMessage(String.Format("Associated CampaignId {0} with TargetId {1}.\n", campaignIds[0], campaignTargetId));

                // Get and print the Target with the legacy GetTargetsByIds operation
                OutputStatusMessage("Get Campaign Target: \n");
                var targets = await GetTargetsByIdsAsync(new[] { campaignTargetId });
                PrintTarget(targets[0]);

                // Get and print the Target2 with the new GetTargetsByIds2 operation
                OutputStatusMessage("Get Campaign Target2: \n");
                var targets2 = await GetTargetsByIds2Async(new[] { campaignTargetId });
                PrintTarget2(targets2[0]);

                // Add a target to the library and associate it with the ad group.
                var adGroupTargetId = (await AddTargetsToLibraryAsync(new[] { adGroupTarget }))[0];
                OutputStatusMessage(String.Format("Added Target Id: {0}\n", adGroupTargetId));
                SetTargetToAdGroupAsync(adGroupIds[0], adGroupTargetId);
                OutputStatusMessage(String.Format("Associated AdGroupId {0} with TargetId {1}.\n", adGroupIds[0], adGroupTargetId));

                // Get and print the Target with the legacy GetTargetsByIds operation
                OutputStatusMessage("Get AdGroup Target: \n");
                targets = await GetTargetsByIdsAsync(new[] { adGroupTargetId });
                PrintTarget(targets[0]);

                // Get and print the Target2 with the new GetTargetsByIds2 operation
                OutputStatusMessage("Get AdGroup Target2: \n");
                targets2 = await GetTargetsByIds2Async(new[] { adGroupTargetId });
                PrintTarget2(targets2[0]);

                // Update the ad group's target as a Target2 object with additional target types.
                // Existing target types such as DayTime, Location, and Radius must be specified 
                // or they will not be included in the updated target.

                var target2 = new Target2
                {
                    Id = adGroupTargetId,
                    Name = "My Target2",
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
                    Location = new LocationTarget2
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
                        RadiusTarget = new RadiusTarget2
                        {
                            Bids = new[]
                                {
                                    new RadiusTargetBid2
                                        {
                                            BidAdjustment = 51,
                                            LatitudeDegrees = 47.755367,
                                            LongitudeDegrees = -122.091827,
                                            Radius = 11,
                                            //RadiusUnit = DistanceUnit.Kilometers
                                        }
                                }
                        }
                    }
                };

                // Update the same identified target as a Target2 object. 
                // Going forward when getting the specified target Id, the Day and Hour elements of the legacy
                // Target object will be nil, since the target is being updated with a DayTime target. 
                UpdateTargetsInLibrary2Async(new[] { target2 });
                OutputStatusMessage("Updated the ad group level target as a Target2 object.\n");

                // Get and print the Target with the legacy GetTargetsByIds operation
                OutputStatusMessage("Get Campaign Target: \n");
                targets = await GetTargetsByIdsAsync(new[] { campaignTargetId });
                PrintTarget(targets[0]);

                // Get and print the Target2 with the new GetTargetsByIds2 operation
                OutputStatusMessage("Get Campaign Target2: \n");
                targets2 = await GetTargetsByIds2Async(new[] { campaignTargetId });
                PrintTarget2(targets2[0]);

                // Get and print the Target with the legacy GetTargetsByIds operation
                OutputStatusMessage("Get AdGroup Target: \n");
                targets = await GetTargetsByIdsAsync(new[] { adGroupTargetId });
                PrintTarget(targets[0]);

                // Get and print the Target2 with the new GetTargetsByIds2 operation
                OutputStatusMessage("Get AdGroup Target2: \n");
                targets2 = await GetTargetsByIds2Async(new[] { adGroupTargetId });
                PrintTarget2(targets2[0]);

                // Get all new and existing targets in the customer library, whether or not they are
                // associated with campaigns or ad groups.

                var allTargetsInfo = await GetTargetsInfoFromLibraryAsync();
                OutputStatusMessage("All target identifiers and names from the customer library: \n");
                PrintTargetsInfo(allTargetsInfo);

                // Delete the campaign, ad group, and targets that were previously added. 
                // DeleteCampaigns would remove the campaign and ad group, as well as the association
                // between ad groups and campaigns. To explicitly delete the association between an entity 
                // and the target, use DeleteTargetFromCampaign and DeleteTargetFromAdGroup respectively.

                DeleteTargetFromCampaignAsync(campaignIds[0]);
                DeleteTargetFromAdGroupAsync(adGroupIds[0]);

                DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignIds[0]));

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
        private async Task<IList<long>> AddAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            return (await Service.CallAsync((s, r) => s.AddAdGroupsAsync(r), request)).AdGroupIds;
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
                LocationTargetVersion = "Latest"
            };

            return (await Service.CallAsync((s, r) => s.GetTargetsByIdsAsync(r), request)).Targets;
        }

        // Gets the list of Target2 objects given the specified target identifiers.
        private async Task<IList<Target2>> GetTargetsByIds2Async(IList<long> targetIds)
        {
            var request = new GetTargetsByIds2Request
            {
                TargetIds = targetIds,
                LocationTargetVersion = "Latest"
            };

            return (await Service.CallAsync((s, r) => s.GetTargetsByIds2Async(r), request)).Targets;
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

        // Updates the specified Target2 object within the customer library. 
        // The operation requires exactly one Target2 in a list.
        private void UpdateTargetsInLibrary2Async(IList<Target2> targets)
        {
            var request = new UpdateTargetsInLibrary2Request
            {
                Targets = targets
            };

            Service.CallAsync((s, r) => s.UpdateTargetsInLibrary2Async(r), request);
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

        // Prints the specified Target object.
        private void PrintTarget(Target target)
        {
            if (target == null)
            {
                return;
            }

            OutputStatusMessage(String.Format("Target Id: {0}", target.Id));
            OutputStatusMessage(String.Format("Target Name: {0}\n", target.Name));

            if (target.Age != null)
            {
                OutputStatusMessage("AgeTarget:");
                foreach (var bid in target.Age.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tAge: {0}\n", bid.Age));
                }
            }
            if (target.Day != null)
            {
                OutputStatusMessage("DayTarget:");
                foreach (var bid in target.Day.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tDay: {0}\n", bid.Day));
                }
            }
            if (target.DeviceOS != null)
            {
                OutputStatusMessage("DeviceOSTarget:");
                foreach (var bid in target.DeviceOS.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tDeviceName: {0}", bid.DeviceName));
                }
            }
            if (target.Gender != null)
            {
                OutputStatusMessage("GenderTarget:");
                foreach (var bid in target.Gender.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tGender: {0}\n", bid.Gender));
                }
            }
            if (target.Hour != null)
            {
                OutputStatusMessage("HourTarget:");
                foreach (var bid in target.Hour.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tHour: {0}\n", bid.Hour));
                }
            }
            if (target.Location != null)
            {
                OutputStatusMessage("LocationTarget:");
                OutputStatusMessage(String.Format("\tHasPhysicalIntent: {0}\n", target.Location.HasPhysicalIntent));
                if (target.Location.CityTarget != null)
                {
                    OutputStatusMessage("\tCityTarget:");
                    foreach (var bid in target.Location.CityTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tCity: {0}\n", bid.City));
                    }
                }
                if (target.Location.CountryTarget != null)
                {
                    OutputStatusMessage("\tCountryTarget:");
                    foreach (var bid in target.Location.CountryTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tCountryAndRegion: {0}", bid.CountryAndRegion));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
                if (target.Location.MetroAreaTarget != null)
                {
                    OutputStatusMessage("\tMetroAreaTarget:");
                    foreach (var bid in target.Location.MetroAreaTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tMetroArea: {0}", bid.MetroArea));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
                if (target.Location.RadiusTarget != null)
                {
                    OutputStatusMessage("\tRadiusTarget:");
                    foreach (var bid in target.Location.RadiusTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tLatitudeDegrees: {0}", bid.LatitudeDegrees));
                        OutputStatusMessage(String.Format("\t\tLongitudeDegrees: {0}", bid.LongitudeDegrees));
                        OutputStatusMessage(String.Format("\t\tRadius: {0} Miles\n", bid.Radius));
                    }
                }
                if (target.Location.StateTarget != null)
                {
                    OutputStatusMessage("\tStateTarget:");
                    foreach (var bid in target.Location.StateTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tState: {0}", bid.State));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
            }
        }

        // Prints the specified Target2 object.
        private void PrintTarget2(Target2 target)
        {
            if (target == null)
            {
                return;
            }

            OutputStatusMessage(String.Format("Target2 Id: {0}", target.Id));
            OutputStatusMessage(String.Format("Target2 Name: {0}\n", target.Name));

            if (target.Age != null)
            {
                OutputStatusMessage("AgeTarget:");
                foreach (var bid in target.Age.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tAge: {0}\n", bid.Age));
                }
            }
            if (target.DayTime != null)
            {
                OutputStatusMessage("DayTimeTarget:");
                foreach (var bid in target.DayTime.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tDay: {0}", bid.Day));
                    OutputStatusMessage(String.Format("\tFromHour: {0}", bid.FromHour));
                    OutputStatusMessage(String.Format("\tToHour: {0}", bid.ToHour));
                    OutputStatusMessage(String.Format("\tFromMinute: {0}", bid.FromMinute));
                    OutputStatusMessage(String.Format("\tToMinute: {0}\n", bid.ToMinute));
                }
            }
            if (target.DeviceOS != null)
            {
                OutputStatusMessage("DeviceOSTarget:");
                foreach (var bid in target.DeviceOS.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tDeviceName: {0}", bid.DeviceName));
                }
            }
            if (target.Gender != null)
            {
                OutputStatusMessage("GenderTarget:");
                foreach (var bid in target.Gender.Bids)
                {
                    OutputStatusMessage(String.Format("\tBidAdjustment: {0}", bid.BidAdjustment));
                    OutputStatusMessage(String.Format("\tGender: {0}\n", bid.Gender));
                }
            }
            if (target.Location != null)
            {
                OutputStatusMessage("LocationTarget2:");
                OutputStatusMessage(String.Format("\tIntentOption: {0}\n", target.Location.IntentOption));
                if (target.Location.CityTarget != null)
                {
                    OutputStatusMessage("\tCityTarget:");
                    foreach (var bid in target.Location.CityTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tCity: {0}\n", bid.City));
                    }
                }
                if (target.Location.CountryTarget != null)
                {
                    OutputStatusMessage("\tCountryTarget:");
                    foreach (var bid in target.Location.CountryTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tCountryAndRegion: {0}", bid.CountryAndRegion));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
                if (target.Location.MetroAreaTarget != null)
                {
                    OutputStatusMessage("\tMetroAreaTarget:");
                    foreach (var bid in target.Location.MetroAreaTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tMetroArea: {0}", bid.MetroArea));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
                if (target.Location.PostalCodeTarget != null)
                {
                    OutputStatusMessage("\tPostalCodeTarget:");
                    foreach (var bid in target.Location.PostalCodeTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tPostalCode: {0}", bid.PostalCode));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
                if (target.Location.RadiusTarget != null)
                {
                    OutputStatusMessage("\tRadiusTarget2:");
                    foreach (var bid in target.Location.RadiusTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tLatitudeDegrees: {0}", bid.LatitudeDegrees));
                        OutputStatusMessage(String.Format("\t\tLongitudeDegrees: {0}", bid.LongitudeDegrees));
                        OutputStatusMessage(String.Format("\t\tRadius: {0} {1}\n", bid.Radius, bid.RadiusUnit));
                    }
                }
                if (target.Location.StateTarget != null)
                {
                    OutputStatusMessage("\tStateTarget:");
                    foreach (var bid in target.Location.StateTarget.Bids)
                    {
                        OutputStatusMessage(String.Format("\t\tBidAdjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(String.Format("\t\tState: {0}", bid.State));
                        OutputStatusMessage(String.Format("\t\tIsExcluded: {0}\n", bid.IsExcluded));
                    }
                }
            }
        }
    }
}
