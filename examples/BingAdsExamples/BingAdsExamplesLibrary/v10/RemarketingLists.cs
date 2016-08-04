using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to associate remarketing lists to a new ad group.
    /// </summary>
    public class RemarketingLists : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Remarketing List Associations | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // To discover all remarketing lists that the user can associate with ad groups in the current account (per CustomerAccountId header), 
                // set RemarketingListIds to null when calling the GetRemarketingLists operation.

                var remarketingLists = (await GetRemarketingListsAsync(null)).RemarketingLists;

                // You must already have at least one remarketing list for the remainder of this example. 
                // The Bing Ads API does not support remarketing list add, update, or delete operations.

                if (remarketingLists.Count < 1)
                {
                    return;
                }

                // Add an ad group in a campaign. The ad group will later be associated with remarketing lists. 

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Women's Shoes" + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        DaylightSaving = true,
                        TrackingUrlTemplate = null
                    },
                };
                
                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        BiddingModel = BiddingModel.Keyword,
                        PricingModel = PricingModel.Cpc,
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        SearchBid = new Bid { Amount = 0.09 },
                        Language = "English",
                        TrackingUrlTemplate = null,

                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        RemarketingTargetingSetting = RemarketingTargetingSetting.TargetAndBid
                    }
                };


                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputCampaignsWithPartialErrors(campaigns, campaignIds, campaignErrors);

                AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)campaignIds[0], adGroups);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputAdGroupsWithPartialErrors(adGroups, adGroupIds, adGroupErrors);

                // If the campaign or ad group add operations failed then we cannot continue this example. 

                if (addAdGroupsResponse.AdGroupIds == null || addAdGroupsResponse.AdGroupIds.Count < 1)
                {
                    return;
                }

                var adGroupRemarketingListAssociations = new List<AdGroupRemarketingListAssociation>();

                // This example associates all of the remarketing lists with the new ad group.

                foreach (var remarketingList in remarketingLists)
                {
                    if (remarketingList.Id != null)
                    {
                        var adGroupRemarketingListAssociation = new AdGroupRemarketingListAssociation
                        {
                            AdGroupId = (long)adGroupIds[0],
                            BidAdjustment = 20.00,
                            RemarketingListId = (long)remarketingList.Id,
                            Status = AdGroupRemarketingListAssociationStatus.Paused
                        };

                        adGroupRemarketingListAssociations.Add(adGroupRemarketingListAssociation);

                        OutputStatusMessage("\nAssociating the following remarketing list with the ad group.\n");
                        OutputRemarketingList(remarketingList);
                    }
                }

                var addAdGroupRemarketingListAssociationsResponse = await AddAdGroupRemarketingListAssociationsAsync(adGroupRemarketingListAssociations);

                var getAdGroupRemarketingListAssociationsResponse = await GetAdGroupRemarketingListAssociationsAsync(new[] { (long)adGroupIds[0] });

                foreach (var adGroupRemarketingListAssociation in getAdGroupRemarketingListAssociationsResponse.AdGroupRemarketingListAssociations)
                {
                    OutputStatusMessage("\nThe following ad group remarketing list association was added.\n");
                    OutputAdGroupRemarketingListAssociation(adGroupRemarketingListAssociation);
                }

                // You can store the association IDs which can be used to update or delete associations later. 

                var associationIds = addAdGroupRemarketingListAssociationsResponse.AssociationIds;
                
                // If the associations were added and retrieved successfully let's practice updating and deleting one of them.

                if (associationIds != null && associationIds.Count > 0)
                {
                    var updateAdGroupRemarketingListAssociation = new AdGroupRemarketingListAssociation
                    {
                        AdGroupId = (long)adGroupIds[0],
                        BidAdjustment = 10.00,
                        Id = associationIds[0],
                        Status = AdGroupRemarketingListAssociationStatus.Active,
                    };

                    var updateAdGroupRemarketingListAssociationsResponse = 
                        await UpdateAdGroupRemarketingListAssociationsAsync(new AdGroupRemarketingListAssociation[] { updateAdGroupRemarketingListAssociation});
                    
                    var deleteAdGroupRemarketingListAssociationsResponse =
                        await DeleteAdGroupRemarketingListAssociationsAsync(new AdGroupRemarketingListAssociation[] { updateAdGroupRemarketingListAssociation });

                }

                // Delete the campaign, ad group, and ad group remarketing list associations that were previously added. 
                // The remarketing lists will not be deleted.
                // You should remove this line if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", campaignIds[0]));
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

        private async Task DeleteCampaignsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            await Service.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
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

        /// <summary>
        /// Retrieves remarketing lists. If RemarketingListIds is null or empty,
        /// the service will return all remarketing lists that the current authenticated user can access.
        /// </summary>
        /// <param name="remarketingListIds">The unique identifiers for the remarketing lists that you want to get.</param>
        /// <returns></returns>
        private async Task<GetRemarketingListsResponse> GetRemarketingListsAsync(IList<long> remarketingListIds)
        {
            var request = new GetRemarketingListsRequest
            {
                
                RemarketingListIds = remarketingListIds,
            };

            return (await Service.CallAsync((s, r) => s.GetRemarketingListsAsync(r), request));
        }

        /// <summary>
        /// Associates the specified ad groups with the respective remarketing lists.
        /// </summary>
        /// <param name="adGroupRemarketingListAssociations"></param>
        /// <returns></returns>
        private async Task<AddAdGroupRemarketingListAssociationsResponse> AddAdGroupRemarketingListAssociationsAsync(
           IList<AdGroupRemarketingListAssociation> adGroupRemarketingListAssociations)
        {
            var request = new AddAdGroupRemarketingListAssociationsRequest
            {
                AdGroupRemarketingListAssociations = adGroupRemarketingListAssociations,
            };

            return (await Service.CallAsync((s, r) => s.AddAdGroupRemarketingListAssociationsAsync(r), request));
        }

        /// <summary>
        /// Deletes one or more ad group remarketing list associations.
        /// </summary>
        /// <param name="adGroupRemarketingListAssociations"></param>
        /// <returns></returns>
        private async Task<DeleteAdGroupRemarketingListAssociationsResponse> DeleteAdGroupRemarketingListAssociationsAsync(
          IList<AdGroupRemarketingListAssociation> adGroupRemarketingListAssociations)
        {
            var request = new DeleteAdGroupRemarketingListAssociationsRequest
            {
                AdGroupRemarketingListAssociations = adGroupRemarketingListAssociations,
            };

            return (await Service.CallAsync((s, r) => s.DeleteAdGroupRemarketingListAssociationsAsync(r), request));
        }

        /// <summary>
        /// Gets the ad group remarketing list associations.
        /// </summary>
        /// <param name="adGroupIds"></param>
        /// <returns></returns>
        private async Task<GetAdGroupRemarketingListAssociationsResponse> GetAdGroupRemarketingListAssociationsAsync(
          IList<long> adGroupIds)
        {
            var request = new GetAdGroupRemarketingListAssociationsRequest
            {
                AdGroupIds = adGroupIds
            };

            return (await Service.CallAsync((s, r) => s.GetAdGroupRemarketingListAssociationsAsync(r), request));
        }

        /// <summary>
        /// Updates one or more ad group remarketing list associations.
        /// </summary>
        /// <param name="adGroupRemarketingListAssociations"></param>
        /// <returns></returns>
        private async Task<UpdateAdGroupRemarketingListAssociationsResponse> UpdateAdGroupRemarketingListAssociationsAsync(
           IList<AdGroupRemarketingListAssociation> adGroupRemarketingListAssociations)
        {
            var request = new UpdateAdGroupRemarketingListAssociationsRequest
            {
                AdGroupRemarketingListAssociations = adGroupRemarketingListAssociations,
            };

            return (await Service.CallAsync((s, r) => s.UpdateAdGroupRemarketingListAssociationsAsync(r), request));
        }
    }
}
