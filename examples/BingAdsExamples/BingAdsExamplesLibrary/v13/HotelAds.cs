using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;
using System.Reflection;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to apply hotel group for Microsoft Hotel Campaigns.
    /// </summary>
    public class HotelAds : ExampleBase
    {
        public override string Description
        {
            get { return "Microsoft Hotel Campaigns | Campaign Management V13"; }
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

               // Create a hotel campaign.

                var campaigns = new[] {
                    new Campaign
                    {
                        CampaignType = CampaignType.Hotel, // set campaign type to Hotel
                        Languages = new string[] { "All" }, // for hotel campaign, language should be set to "All"
                        BiddingScheme = new CommissionBiddingScheme // hotel campaign supports commission, percent cpc and manual cpc bidding scheme
                        {
                            CommissionRate = 3.14,
                        },
                        Name = "Everyone's Hotel " + DateTime.UtcNow,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
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
                long campaignId = (long)campaignIds[0];

                // Create the hotel ad group that will have the hotel groups. This is needed.

                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Everyone's Hotel",
                        AdGroupType = "HotelAds",
                        Settings = new []
                        {
                            new HotelSetting
                            {
                                HotelAdGroupType = HotelAdGroupType.HotelAd, // set ad group type, should be HotelAd or PropertyAd or specify both
                            }
                        },
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // set ad group level bid value
                        // Here the campaign we created is using Commission bidding scheme, so we set CommissionRate
                        // if the campaign bidding scheme is PercentCpc, please set PercentCpcBid
                        // if the campaign bidding scheme is ManualCpc, please set CpcBid
                        CommissionRate = new RateBid
                        {
                            RateAmount = new RateAmount { Amount = 5.6 },
                        }
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
                long adGroupId = (long)adGroupIds[0];

                // What about hotel ad entity?
                // When we call addAdGroups method, an ad will be automatically created for hotel ad group
                // So it's not needed to add an ad for hotel ad group manually

                // Bid all hotels
                // Create a hotel group tree with one single node (root node), then update bid of the node

                var hotelGrouphelper = new HotelGroupActionHelper(adGroupId);

                var root = hotelGrouphelper.AddUnit(
                    parent: null,
                    listing: new HotelListing { Operand = "All", Attribute = null },
                    bidAmount: 0.35,
                    isNegative: false);

                OutputStatusMessage("-----\nApplyHotelGroupActions:");
                OutputStatusMessage("Applying only the root as a Unit with a bid...");
                var applytHotelGroupActionsResponse = await CampaignManagementExampleHelper.ApplyHotelGroupActionsAsync(
                    criterionActions: hotelGrouphelper.HotelGroupActions);

                // HotelGroup is a kind of AdGroupCriterion, so we can use GetAdGroupCriterionsByIds to get HotelGroup.
                // Also in this example code, "AdGroupCriterion", "HotelGroup" or "HotelGroupCriterion" are equivalent.

                OutputStatusMessage("-----\nGetAdGroupCriterionsByIds:");
                var hotelGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    adGroupCriterionIds: null,
                    adGroupId: adGroupId,
                    criterionType: AdGroupCriterionType.HotelGroup,
                    null);

                OutputStatusMessage("The ad group's hotel group tree only has a root node: \n");
                OutputHotelGroupCriterions(hotelGroupCriterions?.AdGroupCriterions);

                // Let's update the bid of the root unit we just added.

                BiddableAdGroupCriterion updatedRoot = new BiddableAdGroupCriterion
                {
                    Id = applytHotelGroupActionsResponse.AdGroupCriterionIds[0],
                    CriterionBid = new RateBid
                    {
                        RateAmount = new RateAmount { Amount = 0.45 }
                    }
                };

                hotelGrouphelper = new HotelGroupActionHelper(adGroupId);
                hotelGrouphelper.UpdateHotelGroup(updatedRoot);

                OutputStatusMessage("-----\nApplyHotelGroupActions:");
                OutputStatusMessage("Updating the bid for the tree root node...");
                await CampaignManagementExampleHelper.ApplyHotelGroupActionsAsync(
                    criterionActions: hotelGrouphelper.HotelGroupActions);

                OutputStatusMessage("-----\nGetAdGroupCriterionsByIds:");
                hotelGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    adGroupCriterionIds: null,
                    adGroupId: adGroupId,
                    criterionType: AdGroupCriterionType.HotelGroup,
                    null);

                OutputStatusMessage("Updated the bid for the tree root node: \n");
                OutputHotelGroupCriterions(hotelGroupCriterions?.AdGroupCriterions);
                
                // Initialize and overwrite existing tree root, and build a hotel group tree structure. 
                // we will build it in steps to demonstrate how to use the results from ApplyHotelGroupActions to update the tree. 

                hotelGrouphelper = new HotelGroupActionHelper(adGroupId);

                // Get existing hotel groups and find the root node.

                OutputStatusMessage("-----\nGetAdGroupCriterionsByIds:");
                hotelGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    adGroupCriterionIds: null,
                    adGroupId: adGroupId,
                    criterionType: AdGroupCriterionType.HotelGroup,
                    null);

                var existingRoot = GetRootNode(hotelGroupCriterions?.AdGroupCriterions);
                if (existingRoot != null)
                {
                    hotelGrouphelper.DeleteHotelGroup(existingRoot);
                }

                var newRoot = hotelGrouphelper.AddSubdivision(
                    null,
                    new HotelListing { Operand = "All", Attribute = null }
                );
                
                // The direct children of any node must have the same Operand. 
                // For this example we will use StarRating nodes as children of the root. 

                var starRating5SubDivision = hotelGrouphelper.AddSubdivision(
                    parent: newRoot,
                    listing: new HotelListing { Operand = "StarRating", Attribute = "5" });

                var starRating4Unit = hotelGrouphelper.AddUnit(
                    parent: newRoot,
                    listing: new HotelListing { Operand = "StarRating", Attribute = "4" },
                    bidAmount: 0.35,
                    isNegative: false);

                // "Everything Else" node for StarRating condition. We should define everything else node for each condition/operand
                // For StarRating, besides 5 stars and 4 stars, all the other hotels below 4 stars are belong to the node below
                var otherStarRatingsUnit = hotelGrouphelper.AddUnit(
                    parent: newRoot,
                    listing: new HotelListing { Operand = "StarRating", Attribute = null },
                    bidAmount: 0.35,
                    isNegative: false);

                var brandAUnit = hotelGrouphelper.AddUnit(
                    parent: starRating5SubDivision,
                    listing: new HotelListing { Operand = "Brand", Attribute = "Brand A" },
                    bidAmount: 0.35,
                    isNegative: false);
                                
                // If you won't bid on Brand B, set the helper method's bidAmount to '0' and isNegative to true. 
                // The helper method will create a NegativeAdGroupCriterion and apply the condition.
                
                var brandBUnit = hotelGrouphelper.AddUnit(
                    parent: starRating5SubDivision,
                    listing: new HotelListing { Operand = "Brand", Attribute = "Brand B" },
                    bidAmount: 0,
                    isNegative: true);

                var otherBrandsUnit = hotelGrouphelper.AddUnit(
                    parent: starRating5SubDivision,
                    listing: new HotelListing { Operand = "Brand", Attribute = null },
                    bidAmount: 0.35,
                    isNegative: false);
                
                OutputStatusMessage("-----\nApplyHotelGroupActions:");
                OutputStatusMessage("Applying hotel groups to the ad group...");
                applytHotelGroupActionsResponse = await CampaignManagementExampleHelper.ApplyHotelGroupActionsAsync(
                    criterionActions: hotelGrouphelper.HotelGroupActions);

                // To retrieve hotel groups after they have been applied, call GetAdGroupCriterionsByIds. 
                // The hotel group with ParentCriterionId set to null is the root node.

                OutputStatusMessage("-----\nGetAdGroupCriterionsByIds:");
                hotelGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    adGroupCriterionIds: null,
                    adGroupId: adGroupId,
                    criterionType: AdGroupCriterionType.HotelGroup,
                    null);

                // The hotel group tree now has 7 nodes. 

                //All hotels (Root Node)
                // |
                // +-- Star Rating 5 (StarRating)
                // |    |
                // |    +-- Brand A (Brand)
                // |    |    
                // |    +-- Brand B (Brand)
                // |    |        
                // |    +-- All other (Brand)    
                // |        
                // +-- Star Rating 4 (StarRating)
                // |   
                // +-- All other (StarRating)

                OutputStatusMessage("The hotel group tree now has 7 nodes: \n");
                OutputHotelGroupCriterions(hotelGroupCriterions?.AdGroupCriterions);

                // Let's replace the Star Rating 4 (StarRating) node created above with an Star Rating 4 (StarRating) node that 
                // has children i.e. Brand C (Brand), Brand D (Brand) and All other (Brand) as follows: 

                //Star Rating 4 (StarRating)
                //|
                //+-- Brand C (Brand)
                //|
                //+-- Brand D (Brand)
                //|
                //+-- All other (Brand)

                hotelGrouphelper = new HotelGroupActionHelper(adGroupId);

                // To replace a node we must know its Id and its ParentCriterionId. In this case the parent of the node 
                // we are replacing is All hotels (Root Node), and was created at Index 1 of the previous ApplyHotelGroupActions call. 
                // The node that we are replacing is Star Rating 4 (StarRating), and was created at Index 3. 

                var rootId = applytHotelGroupActionsResponse.AdGroupCriterionIds[1];
                starRating4Unit.Id = applytHotelGroupActionsResponse.AdGroupCriterionIds[3];
                hotelGrouphelper.DeleteHotelGroup(starRating4Unit);

                var parent = new BiddableAdGroupCriterion() { Id = rootId };

                var newStarRating4Subdivision = hotelGrouphelper.AddSubdivision(
                    parent: parent,
                    listing: new HotelListing { Operand = "StarRating", Attribute = "4" }
                );

                var brandCUnit = hotelGrouphelper.AddUnit(
                    parent: newStarRating4Subdivision,
                    listing: new HotelListing { Operand = "Brand", Attribute = "Brand C" },
                    bidAmount: 0.35,
                    isNegative: false);

                var brandDUnit = hotelGrouphelper.AddUnit(
                    parent: newStarRating4Subdivision,
                    listing: new HotelListing { Operand = "Brand", Attribute = "Brand D" },
                    bidAmount: 0.35,
                    isNegative: false);

                var otherBrands = hotelGrouphelper.AddUnit(
                    parent: newStarRating4Subdivision,
                    listing: new HotelListing { Operand = "Brand", Attribute = null },
                    bidAmount: 0.35,
                    isNegative: false);

                OutputStatusMessage("-----\nApplyHotelGroupActions:");
                OutputStatusMessage(
                    "Updating the hotel group to refine Star Rating 4 (StarRating) with 3 child nodes..."
                );
                applytHotelGroupActionsResponse = await CampaignManagementExampleHelper.ApplyHotelGroupActionsAsync(
                    criterionActions: hotelGrouphelper.HotelGroupActions);

                OutputStatusMessage("-----\nGetAdGroupCriterionsByIds:");
                hotelGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    adGroupCriterionIds: null,
                    adGroupId: adGroupId,
                    criterionType: AdGroupCriterionType.HotelGroup,
                    null);

                // The hotel group tree now has 10 nodes, including the children of Star Rating 4 (StarRating):

                //All hotels (Root Node)
                // |
                // +-- Star Rating 5 (StarRating)
                // |    |
                // |    +-- Brand A (Brand)
                // |    |    
                // |    +-- Brand B (Brand)
                // |    |        
                // |    +-- All other (Brand)    
                // |        
                // +-- Star Rating 4 (StarRating)
                // |    |
                // |    +-- Brand C (Brand)
                // |    |    
                // |    +-- Brand D (Brand)
                // |    |        
                // |    +-- All other (Brand) 
                // |   
                // +-- All other (StarRating)

                OutputStatusMessage(
                    "The hotel group tree now has 10 nodes, including the children of Star Rating 4 (StarRating): \n"
                );
                OutputHotelGroupCriterions(hotelGroupCriterions?.AdGroupCriterions);
               

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

        /// <summary>
        /// Returns the root node of a tree. This operation assumes that a complete 
        /// hotel group tree is provided for one ad group. The node that has
        /// null ParentCriterionId is the root node.
        /// </summary>
        /// <param name="adGroupCriterions">The ad group criterions that contain 
        /// the hotel group tree.</param>
        /// <returns>The ad group criterion that represents the tree root node.</returns>
        private AdGroupCriterion GetRootNode(IList<AdGroupCriterion> adGroupCriterions)
        {
            AdGroupCriterion rootNode = null;
            foreach (AdGroupCriterion adGroupCriterion in adGroupCriterions)
            {
                if (((HotelGroup)(adGroupCriterion.Criterion)).ParentCriterionId == null)
                {
                    rootNode = adGroupCriterion;
                    break;
                }
            }
            return rootNode;
        }

        /// <summary>
        /// Helper class used to maintain a list of hotel group actions for an ad group.
        /// The list of hotel group actions can be passed to the ApplyHotelGroupActions service operation.
        /// </summary>
        private class HotelGroupActionHelper
        {
            /// <summary>
            /// Each criterion is associated with the same ad group.
            /// </summary>
            private long adGroupId;

            /// <summary>
            /// Each new hotel gorup will be assigned a temporary negative identifier, since it does not exist 
            /// and does not yet have a Microsoft Advertising system identifier. This identifier will be used as the ParentCriterionId 
            /// for any child node of the subdivision. 
            /// </summary>
            private long referenceId = -1;

            /// <summary>
            /// The list of hotel group actions that can be passed to the ApplyHotelGroupActions service operation.
            /// </summary>
            private List<AdGroupCriterionAction> hotelGroupActions = new List<AdGroupCriterionAction>();

            /// <summary>
            /// Initializes an instance of the HotelGroupActionHelper class.
            /// </summary>
            /// <param name="adGroupId">The ad group identifier associated with each criterion.</param>
            public HotelGroupActionHelper(long adGroupId)
            {
                this.adGroupId = adGroupId;
            }

            /// <summary>
            /// Returns the list of hotel group actions that can be passed to the ApplyHotelGroupActions service operation.
            /// </summary>
            public IList<AdGroupCriterionAction> HotelGroupActions
            {
                get
                {
                    return hotelGroupActions;
                }
            }

            /// <summary>
            /// Sets the Add action for a new BiddableAdGroupCriterion corresponding to the specified HotelGroup, 
            /// and adds it to the helper's list of HotelGroupActions. 
            /// </summary>
            /// <param name="parent">The parent of the hotel group subdivision that you want to add.</param>
            /// <param name="listing">The hotel listing for the new hotel group.</param>
            /// <returns>The ad group criterion that was added to the list of HotelGroupActions.</returns>
            public AdGroupCriterion AddSubdivision(
                AdGroupCriterion parent,
                HotelListing listing
                )
            {
                var biddableAdGroupCriterion = new BiddableAdGroupCriterion()
                {
                    Id = this.referenceId--,
                    Criterion = new HotelGroup()
                    {
                        ParentCriterionId = parent?.Id,
                        Listing = listing,
                        ListingType = HotelListingType.Subdivision
                    },
                    CriterionBid = null,
                    AdGroupId = this.adGroupId
                };

                var hotelGroupCriterionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Add,
                    AdGroupCriterion = biddableAdGroupCriterion
                };

                this.hotelGroupActions.Add(hotelGroupCriterionAction);

                return biddableAdGroupCriterion;
            }

            /// <summary>
            /// Sets the Add action for a new AdGroupCriterion corresponding to the specified HotelListing, 
            /// and adds it to the helper's list of HotelGroupAction. 
            /// </summary>
            /// <param name="parent">The parent of the hotel group unit that you want to add.</param>
            /// <param name="listing">The hotel listing filter for the new hotel group.</param>
            /// <param name="bidAmount">The bid amount for the new hotel group.</param>
            /// <param name="isNegative">Indicates whether or not to add a NegativeAdGroupCriterion. 
            /// The default value is false, in which case a BiddableAdGroupCriterion will be added.</param>
            /// <returns>The ad group criterion that was added to the list of HotelGroupActions.</returns>
            public AdGroupCriterion AddUnit(
                AdGroupCriterion parent,
                HotelListing listing,
                double bidAmount,
                bool isNegative
                )
            {
                AdGroupCriterion hotelGroupCriterion;

                if (isNegative)
                {
                    hotelGroupCriterion = new NegativeAdGroupCriterion();
                }
                else
                {
                    hotelGroupCriterion = new BiddableAdGroupCriterion()
                    {
                        CriterionBid = new RateBid()
                        {
                            RateAmount = new RateAmount { Amount = bidAmount } 
                        }
                    };
                }

                hotelGroupCriterion.AdGroupId = this.adGroupId;
                hotelGroupCriterion.Criterion = new HotelGroup
                {
                    Listing = listing,
                    ParentCriterionId = parent != null ? parent.Id : null,
                    ListingType = HotelListingType.Unit
                };
                var hotelGroupCriterionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Add,
                    AdGroupCriterion = hotelGroupCriterion
                };

                this.hotelGroupActions.Add(hotelGroupCriterionAction);

                return hotelGroupCriterion;
            }

            /// <summary>
            /// Sets the Delete action for the specified AdGroupCriterion, 
            /// and adds it to the helper's list of HotelGroupAction. 
            /// </summary>
            /// <param name="adGroupCriterion">The ad group criterion whose hotel group you want to delete.</param>
            public void DeleteHotelGroup(AdGroupCriterion adGroupCriterion)
            {
                adGroupCriterion.AdGroupId = this.adGroupId;

                var hotelGroupCriterionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Delete,
                    AdGroupCriterion = adGroupCriterion
                };

                this.hotelGroupActions.Add(hotelGroupCriterionAction);

                return;
            }

            /// <summary>
            /// Sets the Update action for the specified BiddableAdGroupCriterion, 
            /// and adds it to the helper's list of HotelGroupAction.  
            /// When working with hotel groups, you cannot update the Criterion (HotelListing). 
            /// To update a HotelListing, you must delete the existing node (DeleteHotelGroup) and 
            /// add a new one (AddUnit or AddSubdivision) during the same call to ApplyHotelGroupActions. 
            /// </summary>
            /// <param name="biddableAdGroupCriterion">The biddable ad group criterion (hotel group criterion) to update.</param>
            public void UpdateHotelGroup(BiddableAdGroupCriterion biddableAdGroupCriterion)
            {
                biddableAdGroupCriterion.AdGroupId = this.adGroupId;

                var hotelGroupCriterionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Update,
                    AdGroupCriterion = biddableAdGroupCriterion
                };

                this.hotelGroupActions.Add(hotelGroupCriterionAction);

                return;
            }
        }

        /// <summary>
        /// Outputs the list of hotel groups, formatted as a tree. 
        /// To ensure the complete tree is represented, you should first call GetAdGroupCriterionsByIds 
        /// where CriterionTypeFilter is HotelGroup, and pass the returned list of AdGroupCriterion to this method. 
        /// </summary>
        /// <param name="hotelGroupCriterions">The list of hotel group criterions (of type AdGroupCriterion) to output formatted as a tree.</param>
        private void OutputHotelGroupCriterions(IList<AdGroupCriterion> hotelGroupCriterions)
        {
            // Set up the tree for output

            Dictionary<long, List<AdGroupCriterion>> childBranches = new Dictionary<long, List<AdGroupCriterion>>();
            AdGroupCriterion treeRoot = null;

            foreach (var hotelGroupCriterion in hotelGroupCriterions)
            {
                HotelGroup hotelGroup = (HotelGroup)hotelGroupCriterion.Criterion;
                long? parentHotelGroupId = hotelGroup.ParentCriterionId;

                if (parentHotelGroupId != null)
                {
                    if (!childBranches.ContainsKey(parentHotelGroupId.Value))
                    {
                        childBranches[parentHotelGroupId.Value] = new List<AdGroupCriterion>();
                    }
                    childBranches[parentHotelGroupId.Value].Add(hotelGroupCriterion);
                }
                else
                {
                    treeRoot = hotelGroupCriterion;
                }
            }

            // Outputs the tree root node and any children recursively
            OutputHotelGroupTree(treeRoot, childBranches, 0);
        }

        /// <summary>
        /// Outputs the details of the specified hotel group node, 
        /// and passes any children to itself recursively.
        /// </summary>
        /// <param name="node">The node to output, whether a Subdivision or Unit.</param>
        /// <param name="childBranches">The child branches or nodes if any exist.</param>
        /// <param name="treeLevel">
        /// The number of descendents from the tree root node. 
        /// Used by this operation to format the tree structure output.
        /// </param>
        private void OutputHotelGroupTree(
            AdGroupCriterion node,
            Dictionary<long, List<AdGroupCriterion>> childBranches,
            int treeLevel)
        {
            OutputStatusMessage(string.Format("{0}{1}",
                "".PadLeft(treeLevel, '\t'),
                ((HotelGroup)(node.Criterion)).ListingType)
            );

            OutputStatusMessage(string.Format("{0}ParentCriterionId: {1}",
                "".PadLeft(treeLevel, '\t'),
                ((HotelGroup)(node.Criterion)).ParentCriterionId)
            );

            OutputStatusMessage(string.Format("{0}Id: {1}",
                "".PadLeft(treeLevel, '\t'),
                node.Id)
            );

            if (((HotelGroup)(node.Criterion)).ListingType == HotelListingType.Unit)
            {
                var biddableAdGroupCriterion = node as BiddableAdGroupCriterion;
                if (biddableAdGroupCriterion != null)
                {
                    OutputStatusMessage(string.Format("{0}Bid Amount: {1}",
                        "".PadLeft(treeLevel, '\t'),
                        ((RateBid)(biddableAdGroupCriterion.CriterionBid)).RateAmount.Amount)
                    );
                }
                else
                {
                    var negativeAdGroupCriterion = node as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        OutputStatusMessage(string.Format("{0}Not Bidding on this Listing",
                            "".PadLeft(treeLevel, '\t'))
                        );
                    }
                }
            }

            string nullAttribute = ((HotelGroup)(node.Criterion)).ParentCriterionId != null ? "(All other)" : "(Tree Root)";
            OutputStatusMessage(string.Format("{0}Attribute: {1}",
                "".PadLeft(treeLevel, '\t'),
                ((HotelGroup)(node.Criterion)).Listing.Attribute ?? nullAttribute)
            );

            OutputStatusMessage(string.Format("{0}Operand: {1}\n",
                "".PadLeft(treeLevel, '\t'),
                ((HotelGroup)(node.Criterion)).Listing.Operand)
            );

            if (childBranches.ContainsKey((long)(node.Id)))
            {
                foreach (AdGroupCriterion childNode in childBranches[(long)(node.Id)])
                {
                    OutputHotelGroupTree(childNode, childBranches, treeLevel + 1);
                }
            }
        }

    }
}
