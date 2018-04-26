// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL
// All other rights reserved.

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
    /// This example demonstrates how to apply product conditions for Bing Shopping Campaigns.
    /// </summary>
    public class ShoppingCampaigns : ExampleBase
    {
        public override string Description
        {
            get { return "Bing Shopping Campaigns | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Get a list of all Bing Merchant Center stores associated with your CustomerId

                IList<BMCStore> stores = (await CampaignManagementExampleHelper.GetBMCStoresByCustomerIdAsync())?.BMCStores;
                if (stores == null)
                {
                    OutputStatusMessage(
                        string.Format("You do not have any BMC stores registered for CustomerId {0}.\n", authorizationData.CustomerId)
                    );
                    return;
                }

                #region ManageCampaign

                /* Add a new Bing Shopping campaign that will be associated with a ProductScope criterion.
                 *  - Set the CampaignType element of the Campaign to Shopping.
                 *  - Create a ShoppingSetting instance and set its Priority (0, 1, or 2), SalesCountryCode, and StoreId elements. 
                 *    Add this shopping setting to the Settings list of the Campaign.
                 */

                var campaigns = new[] {
                    new Campaign
                    {
                        CampaignType = CampaignType.Shopping,
                        Settings = new[] {
                            new ShoppingSetting() {
                                Priority = 0,
                                SalesCountryCode = "US",
                                StoreId = (int)stores[0].Id
                            }
                        },
                        Name = "Bing Shopping Campaign " + DateTime.UtcNow,
                        Description = "Bing Shopping Campaign Example.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,

                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // Used with CustomParameters defined in lower level entities such as ad group criterion.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    }
                };

                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);
                long campaignId = (long)campaignIds[0];

                /* Optionally, you can create a ProductScope criterion that will be associated with your Bing Shopping campaign. 
                 * Use the product scope criterion to include a subset of your product catalog, for example a specific brand, 
                 * category, or product type. A campaign can only be associated with one ProductScope, which contains a list 
                 * of up to 7 ProductCondition. You'll also be able to specify more specific product conditions for each ad group.
                 */

                var campaignCriterions = new BiddableCampaignCriterion[] {
                    new BiddableCampaignCriterion() {
                        CampaignId = campaignId,
                        CriterionBid = null,  // Not applicable for product scope
                        Criterion = new ProductScope() {
                            Conditions = new ProductCondition[] {
                                new ProductCondition {
                                    Operand = "Condition",
                                    Attribute = "New"
                                },
                                new ProductCondition {
                                    Operand = "CustomLabel0",
                                    Attribute = "MerchantDefinedCustomLabel"
                                },
                            }
                        },
                    }
                };

                var addCampaignCriterionsResponse = await (CampaignManagementExampleHelper.AddCampaignCriterionsAsync(
                    campaignCriterions,
                    CampaignCriterionType.ProductScope)
                );

                #endregion ManageCampaign

                #region ManageAdGroup

                // Create the ad group that will have the product partitions.

                var adGroups = new[]{
                    new AdGroup
                    {
                        Name = "Product Categories",
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        Language = "English",
                        CpcBid = new Bid { Amount = 0.090 }
                    }
                };

                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync((long)campaignId, adGroups, null);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);
                long adGroupId = (long)adGroupIds[0];

                #region BidAllProducts

                var helper = new PartitionActionHelper(adGroupId);

                var root = helper.AddUnit(
                    null,
                    new ProductCondition { Operand = "All", Attribute = null },
                    0.35,
                    false
                );

                OutputStatusMessage("Applying only the root as a Unit with a bid . . . \n");
                var applyProductPartitionActionsResponse = await CampaignManagementExampleHelper.ApplyProductPartitionActionsAsync(helper.PartitionActions);

                var adGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    null,
                    adGroupId,
                    AdGroupCriterionType.ProductPartition
                );

                OutputStatusMessage("The ad group's product partition only has a tree root node: \n");
                OutputProductPartitions(adGroupCriterions?.AdGroupCriterions);

                /*
                 * Let's update the bid of the root Unit we just added.
                 */

                BiddableAdGroupCriterion updatedRoot = new BiddableAdGroupCriterion
                {
                    Id = applyProductPartitionActionsResponse.AdGroupCriterionIds[0],
                    CriterionBid = new FixedBid
                    {
                        Amount = 0.45
                    }
                };

                helper = new PartitionActionHelper(adGroupId);
                helper.UpdatePartition(updatedRoot);

                OutputStatusMessage("Updating the bid for the tree root node . . . \n");
                await CampaignManagementExampleHelper.ApplyProductPartitionActionsAsync(helper.PartitionActions);

                adGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    null,
                    adGroupId,
                    AdGroupCriterionType.ProductPartition
                );

                OutputStatusMessage("Updated the bid for the tree root node: \n");
                OutputProductPartitions(adGroupCriterions?.AdGroupCriterions);

                #endregion BidAllProducts

                #region InitializeTree

                /*
                 * Now we will overwrite any existing tree root, and build a product partition group tree structure in multiple steps. 
                 * You could build the entire tree in a single call since there are less than 5,000 nodes; however, 
                 * we will build it in steps to demonstrate how to use the results from ApplyProductPartitionActions to update the tree. 
                 * 
                 * For a list of validation rules, see the Product Ads technical guide:
                 * https://docs.microsoft.com/en-us/bingads/guides/product-ads
                 */

                helper = new PartitionActionHelper(adGroupId);

                /*
                 * Check whether a root node exists already.
                 */
                adGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    null,
                    adGroupId,
                    AdGroupCriterionType.ProductPartition
                );
                var existingRoot = GetRootNode(adGroupCriterions?.AdGroupCriterions);
                if (existingRoot != null)
                {
                    helper.DeletePartition(existingRoot);
                }

                root = helper.AddSubdivision(
                    null,
                    new ProductCondition { Operand = "All", Attribute = null }
                );

                /*
                 * The direct children of any node must have the same Operand. 
                 * For this example we will use CategoryL1 nodes as children of the root. 
                 * For a list of valid CategoryL1 through CategoryL5 values, see the Bing Category Taxonomy:
                 * http://go.microsoft.com/fwlink?LinkId=507666
                 */
                var animalsSubdivision = helper.AddSubdivision(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Animals & Pet Supplies" }
                );

                /*
                 * If you use a CategoryL2 node, it must be a descendant (child or later) of a CategoryL1 node. 
                 * In other words you cannot have a CategoryL2 node as parent of a CategoryL1 node. 
                 * For this example we will a CategoryL2 node as child of the CategoryL1 Animals & Pet Supplies node. 
                 */
                var petSuppliesSubdivision = helper.AddSubdivision(
                    animalsSubdivision,
                    new ProductCondition { Operand = "CategoryL2", Attribute = "Pet Supplies" }
                );

                var brandA = helper.AddUnit(
                    petSuppliesSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand A" },
                    0.35,
                    false
                );

                /*
                 * If you won't bid on Brand B, set the helper method's bidAmount to '0' and isNegative to true. 
                 * The helper method will create a NegativeAdGroupCriterion and apply the condition.
                 */
                var brandB = helper.AddUnit(
                    petSuppliesSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand B" },
                    0,
                    true
                );

                var otherBrands = helper.AddUnit(
                    petSuppliesSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = null },
                    0.35,
                    false
                );

                var otherPetSupplies = helper.AddUnit(
                    animalsSubdivision,
                    new ProductCondition { Operand = "CategoryL2", Attribute = null },
                    0.35,
                    false
                );

                var electronics = helper.AddUnit(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Electronics" },
                    0.35,
                    false
                );

                var otherCategoryL1 = helper.AddUnit(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = null },
                    0.35,
                    false
                );

                OutputStatusMessage("Applying product partitions to the ad group . . . \n");
                applyProductPartitionActionsResponse = await CampaignManagementExampleHelper.ApplyProductPartitionActionsAsync(helper.PartitionActions);

                // To retrieve product partitions after they have been applied, call GetAdGroupCriterionsByIds. 
                // The product partition with ParentCriterionId set to null is the root node.

                adGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    null,
                    adGroupId,
                    AdGroupCriterionType.ProductPartition
                );

                /*
                 * The product partition group tree now has 9 nodes. 
                 
                   All other (Root Node)
                    |
                    +-- Animals & Pet Supplies (CategoryL1)
                    |    |
                    |    +-- Pet Supplies (CategoryL2)
                    |    |    |
                    |    |    +-- Brand A
                    |    |    |    
                    |    |    +-- Brand B
                    |    |    |    
                    |    |    +-- All other (Brand)
                    |    |         
                    |    +-- All other (CategoryL2)
                    |        
                    +-- Electronics (CategoryL1)
                    |   
                    +-- All other (CategoryL1)

                 */

                OutputStatusMessage("The product partition group tree now has 9 nodes: \n");
                OutputProductPartitions(adGroupCriterions?.AdGroupCriterions);

                #endregion InitializeTree

                #region UpdateTree

                /*
                 * Let's replace the Electronics (CategoryL1) node created above with an Electronics (CategoryL1) node that 
                 * has children i.e. Brand C (Brand), Brand D (Brand), and All other (Brand) as follows: 
                 
                    Electronics (CategoryL1)
                    |
                    +-- Brand C (Brand)
                    |
                    +-- Brand D (Brand)
                    |
                    +-- All other (Brand)
           
                 */

                helper = new PartitionActionHelper(adGroupId);

                /*
                 * To replace a node we must know its Id and its ParentCriterionId. In this case the parent of the node 
                 * we are replacing is All other (Root Node), and was created at Index 1 of the previous ApplyProductPartitionActions call. 
                 * The node that we are replacing is Electronics (CategoryL1), and was created at Index 8. 
                 */
                var rootId = applyProductPartitionActionsResponse.AdGroupCriterionIds[1];
                electronics.Id = applyProductPartitionActionsResponse.AdGroupCriterionIds[8];
                helper.DeletePartition(electronics);

                var parent = new BiddableAdGroupCriterion() { Id = rootId };

                var electronicsSubdivision = helper.AddSubdivision(
                    parent,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Electronics" }
                );

                var brandC = helper.AddUnit(
                    electronicsSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand C" },
                    0.35,
                    false
                );

                var brandD = helper.AddUnit(
                    electronicsSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand D" },
                    0.35,
                    false
                );

                var otherElectronicsBrands = helper.AddUnit(
                    electronicsSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = null },
                    0.35,
                    false
                );

                OutputStatusMessage(
                    "Updating the product partition group to refine Electronics (CategoryL1) with 3 child nodes . . . \n"
                );
                applyProductPartitionActionsResponse = await CampaignManagementExampleHelper.ApplyProductPartitionActionsAsync(helper.PartitionActions);

                adGroupCriterions = await CampaignManagementExampleHelper.GetAdGroupCriterionsByIdsAsync(
                    null,
                    adGroupId,
                    AdGroupCriterionType.ProductPartition
                );

                /*
                 * The product partition group tree now has 12 nodes, including the children of Electronics (CategoryL1):
                 
                   All other (Root Node)
                    |
                    +-- Animals & Pet Supplies (CategoryL1)
                    |    |
                    |    +-- Pet Supplies (CategoryL2)
                    |    |    |
                    |    |    +-- Brand A
                    |    |    |    
                    |    |    +-- Brand B
                    |    |    |    
                    |    |    +-- All other (Brand)
                    |    |         
                    |    +-- All other (CategoryL2)
                    |        
                    +-- Electronics (CategoryL1)
                    |    |
                    |    +-- Brand C (Brand)
                    |    |
                    |    +-- Brand D (Brand)
                    |    |
                    |    +-- All other (Brand)
                    |   
                    +-- All other (CategoryL1)
                 
                 */

                OutputStatusMessage(
                    "The product partition group tree now has 12 nodes, including the children of Electronics (CategoryL1): \n"
                );
                OutputProductPartitions(adGroupCriterions?.AdGroupCriterions);

                #endregion UpdateTree

                #endregion ManageAdGroup

                #region ManageAds

                /*
                 * Create a product ad. You must add at least one ProductAd to the corresponding ad group. 
                 * A ProductAd is not used directly for delivered ad copy. Instead, the delivery engine generates 
                 * product ads from the product details that it finds in your Bing Merchant Center store's product catalog. 
                 * The primary purpose of the ProductAd object is to provide promotional text that the delivery engine 
                 * adds to the product ads that it generates. For example, if the promotional text is set to 
                 * 'Free shipping on $99 purchases', the delivery engine will set the product ad's description to 
                 * 'Free shipping on $99 purchases.'
                 */

                var ads = new Ad[] {
                    new ProductAd
                    {
                        PromotionalText = "Free shipping on $99 purchases."
                    },
                };

                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync((long)adGroupIds[0], ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                CampaignManagementExampleHelper.OutputArrayOfLong(adIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);

                #endregion ManageAds

                #region CleanUp

                /* Delete the campaign, ad group, criterion, and ad that were previously added. 
                 * You should remove this region if you want to view the added entities in the 
                 * Bing Ads web application or another tool.
                 */

                await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { campaignId });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}\n", campaignId));

                #endregion CleanUp
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

        /// <summary>
        /// Returns the root node of a tree. This operation assumes that a complete 
        /// product partition tree is provided for one ad group. The node that has
        /// null ParentCriterionId is the root node.
        /// </summary>
        /// <param name="adGroupCriterions">The ad group criterions that contain 
        /// the product partition tree.</param>
        /// <returns>The ad group criterion that represents the tree root node.</returns>
        private AdGroupCriterion GetRootNode(IList<AdGroupCriterion> adGroupCriterions)
        {
            AdGroupCriterion rootNode = null;
            foreach (AdGroupCriterion adGroupCriterion in adGroupCriterions)
            {
                if (((ProductPartition)(adGroupCriterion.Criterion)).ParentCriterionId == null)
                {
                    rootNode = adGroupCriterion;
                    break;
                }
            }
            return rootNode;
        }

        /// <summary>
        /// Helper class used to maintain a list of product partition actions for an ad group.
        /// The list of partition actions can be passed to the Bing Ads ApplyProductPartitionActions service operation.
        /// </summary>
        private class PartitionActionHelper
        {
            /// <summary>
            /// Each criterion is associated with the same ad group.
            /// </summary>
            private long adGroupId;

            /// <summary>
            /// Each new subdivision will be assigned a temporary negative identifier, since it does not exist 
            /// and does not yet have a Bing Ads system identifier. This identifier will be used as the ParentCriterionId 
            /// for any child node of the subdivision. 
            /// </summary>
            private long referenceId = -1;

            /// <summary>
            /// The list of partition actions that can be passed to the Bing Ads ApplyProductPartitionActions service operation.
            /// </summary>
            private List<AdGroupCriterionAction> partitionActions = new List<AdGroupCriterionAction>();

            /// <summary>
            /// Initializes an instance of the PartitionActionHelper class.
            /// </summary>
            /// <param name="adGroupId">The ad group identifier associated with each criterion.</param>
            public PartitionActionHelper(long adGroupId)
            {
                this.adGroupId = adGroupId;
            }

            /// <summary>
            /// Returns the list of partition actions that can be passed to the Bing Ads ApplyProductPartitionActions service operation.
            /// </summary>
            public IList<AdGroupCriterionAction> PartitionActions
            {
                get
                {
                    return partitionActions;
                }
            }

            /// <summary>
            /// Sets the Add action for a new BiddableAdGroupCriterion corresponding to the specified ProductCondition, 
            /// and adds it to the helper's list of AdGroupCriterionAction. 
            /// </summary>
            /// <param name="parent">The parent of the product partition subdivision that you want to add.</param>
            /// <param name="condition">The condition or product filter for the new product partition.</param>
            /// <returns>The ad group criterion that was added to the list of PartitionActions.</returns>
            public AdGroupCriterion AddSubdivision(
                AdGroupCriterion parent,
                ProductCondition condition
                )
            {
                var biddableAdGroupCriterion = new BiddableAdGroupCriterion()
                {
                    Id = this.referenceId--,
                    Criterion = new ProductPartition()
                    {
                        // If the root node is a unit, it would not have a parent
                        ParentCriterionId = parent != null ? parent.Id : null,
                        Condition = condition,
                        PartitionType = ProductPartitionType.Subdivision
                    },
                    CriterionBid = null,
                    AdGroupId = this.adGroupId
                };

                var partitionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Add,
                    AdGroupCriterion = biddableAdGroupCriterion
                };

                this.partitionActions.Add(partitionAction);

                return biddableAdGroupCriterion;
            }

            /// <summary>
            /// Sets the Add action for a new AdGroupCriterion corresponding to the specified ProductCondition, 
            /// and adds it to the helper's list of AdGroupCriterionAction. 
            /// </summary>
            /// <param name="parent">The parent of the product partition unit that you want to add.</param>
            /// <param name="condition">The condition or product filter for the new product partition.</param>
            /// <param name="bidAmount">The bid amount for the new product partition.</param>
            /// <param name="isNegative">Indicates whether or not to add a NegativeAdGroupCriterion. 
            /// The default value is false, in which case a BiddableAdGroupCriterion will be added.</param>
            /// <returns>The ad group criterion that was added to the list of PartitionActions.</returns>
            public AdGroupCriterion AddUnit(
                AdGroupCriterion parent,
                ProductCondition condition,
                double bidAmount,
                bool isNegative
                )
            {
                AdGroupCriterion adGroupCriterion;

                if (isNegative)
                {
                    adGroupCriterion = new NegativeAdGroupCriterion();
                }
                else
                {
                    adGroupCriterion = new BiddableAdGroupCriterion()
                    {
                        CriterionBid = new FixedBid()
                        {
                            Amount = bidAmount
                        },

                        // This destination URL is used if specified; otherwise, the destination URL is determined 
                        // by the corresponding value of the 'Link' that you specified for the product offer 
                        // in your Bing Merchant Center catalog.
                        DestinationUrl = null,

                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this criterion, 
                        // and can be used by the criterion, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
                        UrlCustomParameters = new CustomParameters
                        {
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO1"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        }
                    };
                }

                adGroupCriterion.Criterion = new ProductPartition()
                {
                    // If the root node is a unit, it would not have a parent
                    ParentCriterionId = parent != null ? parent.Id : null,
                    Condition = condition,
                    PartitionType = ProductPartitionType.Unit
                };

                adGroupCriterion.AdGroupId = this.adGroupId;

                var partitionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Add,
                    AdGroupCriterion = adGroupCriterion
                };

                this.partitionActions.Add(partitionAction);

                return adGroupCriterion;
            }

            /// <summary>
            /// Sets the Delete action for the specified AdGroupCriterion, 
            /// and adds it to the helper's list of AdGroupCriterionAction. 
            /// </summary>
            /// <param name="adGroupCriterion">The ad group criterion whose product partition you want to delete.</param>
            public void DeletePartition(AdGroupCriterion adGroupCriterion)
            {
                adGroupCriterion.AdGroupId = this.adGroupId;

                var partitionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Delete,
                    AdGroupCriterion = adGroupCriterion
                };

                this.partitionActions.Add(partitionAction);

                return;
            }

            /// <summary>
            /// Sets the Update action for the specified BiddableAdGroupCriterion, 
            /// and adds it to the helper's list of AdGroupCriterionAction. 
            /// You can only update the CriterionBid and DestinationUrl elements 
            /// of the BiddableAdGroupCriterion. 
            /// When working with product partitions, youu cannot update the Criterion (ProductPartition). 
            /// To update a ProductPartition, you must delete the existing node (DeletePartition) and 
            /// add a new one (AddUnit or AddSubdivision) during the same call to ApplyProductPartitionActions. 
            /// </summary>
            /// <param name="biddableAdGroupCriterion">The biddable ad group criterion to update.</param>
            public void UpdatePartition(BiddableAdGroupCriterion biddableAdGroupCriterion)
            {
                biddableAdGroupCriterion.AdGroupId = this.adGroupId;

                var partitionAction = new AdGroupCriterionAction()
                {
                    Action = ItemAction.Update,
                    AdGroupCriterion = biddableAdGroupCriterion
                };

                this.partitionActions.Add(partitionAction);

                return;
            }
        }

        /// <summary>
        /// Outputs the list of AdGroupCriterion, formatted as a tree. 
        /// Each AdGroupCriterion must be either a BiddableAdGroupCriterion or NegativeAdGroupCriterion. 
        /// To ensure the complete tree is represented, you should first call GetAdGroupCriterionsByIds 
        /// where CriterionTypeFilter is ProductPartition, and pass the returned list of AdGroupCriterion to this method. 
        /// </summary>
        /// <param name="adGroupCriterions">The list of ad group criterions to output formatted as a tree.</param>
        private void OutputProductPartitions(IList<AdGroupCriterion> adGroupCriterions)
        {
            // Set up the tree for output

            Dictionary<long, List<AdGroupCriterion>> childBranches = new Dictionary<long, List<AdGroupCriterion>>();
            AdGroupCriterion treeRoot = null;

            foreach (var adGroupCriterion in adGroupCriterions)
            {
                ProductPartition partition = (ProductPartition)adGroupCriterion.Criterion;
                childBranches[(long)(adGroupCriterion.Id)] = new List<AdGroupCriterion>();

                // The product partition with ParentCriterionId set to null is the root node.
                if (partition.ParentCriterionId != null)
                {
                    childBranches[(long)(partition.ParentCriterionId)].Add(adGroupCriterion);
                }
                else
                {
                    treeRoot = adGroupCriterion;
                }
            }

            // Outputs the tree root node and any children recursively
            OutputProductPartitionTree(treeRoot, childBranches, 0);
        }

        /// <summary>
        /// Outputs the details of the specified product partition node, 
        /// and passes any children to itself recursively.
        /// </summary>
        /// <param name="node">The node to output, whether a Subdivision or Unit.</param>
        /// <param name="childBranches">The child branches or nodes if any exist.</param>
        /// <param name="treeLevel">
        /// The number of descendents from the tree root node. 
        /// Used by this operation to format the tree structure output.
        /// </param>
        private void OutputProductPartitionTree(
            AdGroupCriterion node,
            Dictionary<long, List<AdGroupCriterion>> childBranches,
            int treeLevel)
        {
            OutputStatusMessage(string.Format("{0}{1}",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(node.Criterion)).PartitionType)
            );

            OutputStatusMessage(string.Format("{0}ParentCriterionId: {1}",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(node.Criterion)).ParentCriterionId)
            );

            OutputStatusMessage(string.Format("{0}Id: {1}",
                "".PadLeft(treeLevel, '\t'),
                node.Id)
            );

            if (((ProductPartition)(node.Criterion)).PartitionType == ProductPartitionType.Unit)
            {
                var biddableAdGroupCriterion = node as BiddableAdGroupCriterion;
                if (biddableAdGroupCriterion != null)
                {
                    OutputStatusMessage(string.Format("{0}Bid Amount: {1}",
                        "".PadLeft(treeLevel, '\t'),
                        ((FixedBid)(biddableAdGroupCriterion.CriterionBid)).Amount)
                    );
                    OutputStatusMessage(string.Format("{0}DestinationUrl: {1}",
                        "".PadLeft(treeLevel, '\t'),
                        biddableAdGroupCriterion.DestinationUrl)
                    );
                    OutputStatusMessage(string.Format("{0}TrackingUrlTemplate: {1}",
                        "".PadLeft(treeLevel, '\t'),
                        biddableAdGroupCriterion.TrackingUrlTemplate)
                    );
                }
                else
                {
                    var negativeAdGroupCriterion = node as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        OutputStatusMessage(string.Format("{0}Not Bidding on this Condition",
                            "".PadLeft(treeLevel, '\t'))
                        );
                    }
                }
            }

            var nullAttribute = ((ProductPartition)(node.Criterion)).ParentCriterionId != null ? "(All other)" : "(Tree Root)";
            OutputStatusMessage(string.Format("{0}Attribute: {1}",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(node.Criterion)).Condition.Attribute ?? nullAttribute)
            );

            OutputStatusMessage(string.Format("{0}Operand: {1}\n",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(node.Criterion)).Condition.Operand)
            );

            foreach (AdGroupCriterion childNode in childBranches[(long)(node.Id)])
            {
                OutputProductPartitionTree(childNode, childBranches, treeLevel + 1);
            }
        }

    }
}
