using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.Bulk;
using Microsoft.BingAds.V12.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to apply product conditions for Bing Shopping Campaigns
    /// using the BulkServiceManager class.
    /// </summary>
    public class BulkShoppingCampaigns : BulkExampleBase
    {
        public override string Description
        {
            get { return "Bing Shopping Campaigns | Bulk V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                #region CampaignThroughAdGroupSetup

                // You will need to use the Campaign Management service to get the Bing Merchant Center Store Id. This will be used
                // when creating a new Bing Shopping Campaign.
                // For other operations such as adding product conditions, you can manage Bing Shopping Campaigns solely with the Bulk Service. 

                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
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

                BulkServiceManager = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));
                

                var uploadEntities = new List<BulkEntity>();

                /* Add a new Bing Shopping campaign that will be associated with a ProductScope criterion.
                 *  - Set the CampaignType element of the Campaign to Shopping.
                 *  - Create a ShoppingSetting instance and set its Priority (0, 1, or 2), SalesCountryCode, and StoreId elements. 
                 *    Add this shopping setting to the Settings list of the Campaign.
                 */

                var bulkCampaign = new BulkCampaign
                {
                    // ClientId may be used to associate records in the bulk upload file with records in the results file. The value of this field 
                    // is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record.
                    // Note: This bulk file Client Id is not related to an application Client Id for OAuth. 
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        // When using the Campaign Management service, the Id cannot be set. In the context of a BulkCampaign, the Id is optional 
                        // and may be used as a negative reference key during bulk upload. For example the same negative value set for the campaign Id 
                        // will be used when associating this new campaign with a new campaign product scope in the BulkCampaignProductScope object below. 
                        Id = campaignIdKey,
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

                /* Optionally, you can create a ProductScope criterion that will be associated with your Bing Shopping campaign. 
                 * Use the product scope criterion to include a subset of your product catalog, for example a specific brand, 
                 * category, or product type. A campaign can only be associated with one ProductScope, which contains a list 
                 * of up to 7 ProductCondition. You'll also be able to specify more specific product conditions for each ad group.
                 */

                var bulkCampaignProductScope = new BulkCampaignProductScope
                {
                    BiddableCampaignCriterion = new BiddableCampaignCriterion()
                    {
                        CampaignId = campaignIdKey,
                        CriterionBid = null,  // Not applicable for product scope
                        Criterion = new ProductScope()
                        {
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
                    },
                    Status = Status.Active,
                };

                // Specify one or more ad groups.

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Product Categories",
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        Language = "English",
                        Status = AdGroupStatus.Active
                    },
                };

                /*
                 * Create a product ad. You must add at least one ProductAd to the corresponding ad group. 
                 * A ProductAd is not used directly for delivered ad copy. Instead, the delivery engine generates 
                 * product ads from the product details that it finds in your Bing Merchant Center store's product catalog. 
                 * The primary purpose of the ProductAd object is to provide promotional text that the delivery engine 
                 * adds to the product ads that it generates. For example, if the promotional text is set to 
                 * 'Free shipping on $99 purchases', the delivery engine will set the product ad's description to 
                 * 'Free shipping on $99 purchases.'
                 */

                var bulkProductAd = new BulkProductAd
                {
                    AdGroupId = adGroupIdKey,
                    ProductAd = new ProductAd
                    {
                        PromotionalText = "Free shipping on $99 purchases."
                    }
                };

                uploadEntities.Add(bulkCampaign);
                uploadEntities.Add(bulkAdGroup);
                uploadEntities.Add(bulkCampaignProductScope);
                uploadEntities.Add(bulkProductAd);

                // Upload and write the output

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var productAdResults = downloadEntities.OfType<BulkProductAd>().ToList();
                OutputBulkProductAds(productAdResults);

                var campaignProductScopeResults = downloadEntities.OfType<BulkCampaignProductScope>().ToList();
                OutputBulkCampaignProductScopes(campaignProductScopeResults);

                Reader.Dispose();

                #endregion CampaignThroughAdGroupSetup

                #region BidAllProducts

                var adGroupId = (long)adGroupResults[0].AdGroup.Id;

                var helper = new ProductPartitionHelper(adGroupId);

                var root = helper.AddUnit(
                    null,
                    new ProductCondition { Operand = "All", Attribute = null },
                    0.35,
                    false,
                    "root"
                );

                OutputStatusMessage("Applying only the root as a Unit with a bid . . . \n");
                var applyBulkProductPartitionActionsResults =
                    await ApplyBulkProductPartitionActions(helper.PartitionActions);

                var productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

                OutputStatusMessage("The ad group's product partition only has a tree root node: \n");
                OutputProductPartitions(productPartitions);

                /*
                 * Let's update the bid of the root Unit we just added.
                 */

                var updatedRoot = GetNodeByClientId(applyBulkProductPartitionActionsResults, "root");
                var bid = new FixedBid
                {
                    Amount = 0.45
                };
                ((BiddableAdGroupCriterion)(updatedRoot.AdGroupCriterion)).CriterionBid = bid;

                helper = new ProductPartitionHelper(adGroupId);
                helper.UpdatePartition(updatedRoot);

                OutputStatusMessage("Updating the bid for the tree root node . . . \n");
                await ApplyBulkProductPartitionActions(helper.PartitionActions);

                productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

                OutputStatusMessage("Updated the bid for the tree root node: \n");
                OutputProductPartitions(productPartitions);

                #endregion BidAllProducts

                #region InitializeTree

                /*
                 * Now we will overwrite any existing tree root, and build a product partition group tree structure in multiple steps. 
                 * You could build the entire tree in a single call since there are less than 20,000 nodes; however, 
                 * we will build it in steps to demonstrate how to use the results from bulk upload to update the tree. 
                 * 
                 * For a list of validation rules, see the Product Ads technical guide:
                 * https://docs.microsoft.com/en-us/bingads/guides/product-ads
                 */

                helper = new ProductPartitionHelper(adGroupId);

                /*
                 * Check whether a root node exists already.
                 */

                var existingRoot = GetNodeByClientId(applyBulkProductPartitionActionsResults, "root");
                if (existingRoot != null)
                {
                    existingRoot.ClientId = "deletedroot";
                    helper.DeletePartition(existingRoot);
                }

                root = helper.AddSubdivision(
                    null,
                    new ProductCondition { Operand = "All", Attribute = null },
                    "root"
                );

                /*
                 * The direct children of any node must have the same Operand. 
                 * For this example we will use CategoryL1 nodes as children of the root. 
                 * For a list of valid CategoryL1 through CategoryL5 values, see the Bing Category Taxonomy:
                 * http://go.microsoft.com/fwlink?LinkId=507666
                 */
                var animalsSubdivision = helper.AddSubdivision(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Animals & Pet Supplies" },
                    "animalsSubdivision"
                );

                /*
                 * If you use a CategoryL2 node, it must be a descendant (child or later) of a CategoryL1 node. 
                 * In other words you cannot have a CategoryL2 node as parent of a CategoryL1 node. 
                 * For this example we will a CategoryL2 node as child of the CategoryL1 Animals & Pet Supplies node. 
                 */
                var petSuppliesSubdivision = helper.AddSubdivision(
                    animalsSubdivision,
                    new ProductCondition { Operand = "CategoryL2", Attribute = "Pet Supplies" },
                    "petSuppliesSubdivision"
                );

                var brandA = helper.AddUnit(
                    petSuppliesSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand A" },
                    0.35,
                    false,
                    "brandA"
                );

                /*
                 * If you won't bid on Brand B, set the helper method's bidAmount to '0' and isNegative to true. 
                 * The helper method will create a NegativeAdGroupCriterion and apply the condition.
                 */
                var brandB = helper.AddUnit(
                    petSuppliesSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand B" },
                    0,
                    true,
                    "brandB"
                );

                var otherBrands = helper.AddUnit(
                    petSuppliesSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = null },
                    0.35,
                    false,
                    "otherBrands"
                );

                var otherPetSupplies = helper.AddUnit(
                    animalsSubdivision,
                    new ProductCondition { Operand = "CategoryL2", Attribute = null },
                    0.35,
                    false,
                    "otherPetSupplies"
                );

                var electronics = helper.AddUnit(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Electronics" },
                    0.35,
                    false,
                    "electronics"
                );

                var otherCategoryL1 = helper.AddUnit(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = null },
                    0.35,
                    false,
                    "otherCategoryL1"
                );

                OutputStatusMessage("Applying product partitions to the ad group . . . \n");
                applyBulkProductPartitionActionsResults =
                    await ApplyBulkProductPartitionActions(helper.PartitionActions);

                productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

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
                OutputProductPartitions(productPartitions);

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

                helper = new ProductPartitionHelper(adGroupId);

                /*
                 * To replace a node we must know its Id and its ParentCriterionId. In this case the parent of the node 
                 * we are replacing is All other (Root Node). The node that we are replacing is Electronics (CategoryL1). 
                 */
                var rootId = GetNodeByClientId(applyBulkProductPartitionActionsResults, "root").AdGroupCriterion.Id;
                electronics.AdGroupCriterion.Id = GetNodeByClientId(applyBulkProductPartitionActionsResults, "electronics").AdGroupCriterion.Id;
                helper.DeletePartition(electronics);

                var parent = new BulkAdGroupProductPartition
                {
                    AdGroupCriterion = new BiddableAdGroupCriterion() { Id = rootId }
                };

                var electronicsSubdivision = helper.AddSubdivision(
                    parent,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Electronics" },
                    "electronicsSubdivision"
                );

                var brandC = helper.AddUnit(
                    electronicsSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand C" },
                    0.35,
                    false,
                    "brandC"
                );

                var brandD = helper.AddUnit(
                    electronicsSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = "Brand D" },
                    0.35,
                    false,
                    "brandD"
                );

                var otherElectronicsBrands = helper.AddUnit(
                    electronicsSubdivision,
                    new ProductCondition { Operand = "Brand", Attribute = null },
                    0.35,
                    false,
                    "otherElectronicsBrands"
                );

                OutputStatusMessage(
                    "Updating the product partition group to refine Electronics (CategoryL1) with 3 child nodes . . . \n"
                );
                applyBulkProductPartitionActionsResults =
                    await ApplyBulkProductPartitionActions(helper.PartitionActions);

                productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

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
                OutputProductPartitions(productPartitions);

                #endregion UpdateTree

                #region CleanUp

                //Delete the campaign, ad group, criterion, and ad that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                //When you delete a BulkCampaign, the dependent entities such as BulkAdGroup and BulkAdGroupProductPartition 
                //are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage(
                    "Deleting the campaign, product conditions, ad group, product partitions, and product ad... \n"
                );

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());

                Reader.Dispose();

                #endregion Cleanup
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V12.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
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
            catch (BulkOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the bulk operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (BulkOperationCouldNotBeCompletedException<DownloadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationCouldNotBeCompletedException<UploadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
            finally
            {
                if (Reader != null) { Reader.Dispose(); }
                if (Writer != null) { Writer.Dispose(); }
            }
        }
        
        /// <summary>
        /// Uploads a list of BulkAdGroupProductPartition objects that must represent
        /// a product partition tree for one ad group. You can include BulkAdGroupProductPartition records for more than one
        /// ad group per upload, however, this code example assumes that only one ad group is in scope. 
        /// </summary>
        /// <param name="partitionActions">The list of BulkAdGroupProductPartition objects that must represent
        /// a product partition tree.</param>
        /// <returns>The BulkAdGroupProductPartition upload results.</returns>
        private async Task<IList<BulkAdGroupProductPartition>> ApplyBulkProductPartitionActions(
            IList<BulkAdGroupProductPartition> partitionActions)
        {
            var fileUploadParameters = new FileUploadParameters
            {
                ResultFileDirectory = FileDirectory,
                ResultFileName = ResultFileName,
                OverwriteResultFile = true,
                UploadFilePath = FileDirectory + UploadFileName,
                ResponseMode = ResponseMode.ErrorsAndResults
            };

            Writer = new BulkFileWriter(FileDirectory + UploadFileName);
            foreach (var partitionAction in partitionActions)
            {
                Writer.WriteEntity(partitionAction);
            }
            Writer.Dispose();

            var bulkFilePath =
                await BulkServiceManager.UploadFileAsync(fileUploadParameters);
            Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);

            var downloadEntities = Reader.ReadEntities().ToList();
            var bulkAdGroupProductPartitionResults = downloadEntities.OfType<BulkAdGroupProductPartition>().ToList();

            // Add this output line if you want to view details of each BulkAdGroupProductPartition. 
            //OutputBulkAdGroupProductPartitions(bulkAdGroupProductPartitionResults);

            Reader.Dispose();

            return bulkAdGroupProductPartitionResults;
        }

        /// <summary>
        /// Gets the list of BulkAdGroupProductPartition that represent a product partition tree for the specified ad group.
        /// </summary>
        /// <param name="adGroupId">The identifier of the ad group whose product partition tree you want to get.</param>
        /// <returns>The BulkAdGroupProductPartition download results, filtered by the specified ad group ID.</returns>
        private async Task<IList<BulkAdGroupProductPartition>> GetBulkAdGroupProductPartitionTree(long adGroupId)
        {
            var downloadParameters = new DownloadParameters
            {
                DownloadEntities = new[] { DownloadEntity.AdGroupProductPartitions },
                ResultFileDirectory = FileDirectory,
                ResultFileName = DownloadFileName,
                OverwriteResultFile = true,
                LastSyncTimeInUTC = null
            };

            var bulkFilePath = await BulkServiceManager.DownloadFileAsync(downloadParameters);
            Reader = new BulkFileReader(bulkFilePath, ResultFileType.FullDownload, FileType);
            var downloadEntities = Reader.ReadEntities().ToList();
            var bulkAdGroupProductPartitionResults = downloadEntities.OfType<BulkAdGroupProductPartition>().ToList();

            Reader.Dispose();

            IList<BulkAdGroupProductPartition> bulkAdGroupProductPartitions = new List<BulkAdGroupProductPartition>();
            foreach (var bulkAdGroupProductPartitionResult in bulkAdGroupProductPartitionResults)
            {
                if (bulkAdGroupProductPartitionResult.AdGroupCriterion != null
                    && bulkAdGroupProductPartitionResult.AdGroupCriterion.AdGroupId == adGroupId)
                {
                    bulkAdGroupProductPartitions.Add(bulkAdGroupProductPartitionResult);
                }
            }

            return bulkAdGroupProductPartitions;
        }

        /// <summary>
        /// Returns the BulkAdGroupProductPartition corresponding to the specified Client Id.
        /// </summary>
        /// <param name="productPartitions">The list of BulkAdGroupProductPartition that make up 
        /// the product partition tree.</param>
        /// <returns>The BulkAdGroupProductPartition corresponding to the specified Client Id.</returns>
        private BulkAdGroupProductPartition GetNodeByClientId(
            IList<BulkAdGroupProductPartition> productPartitions,
            string clientId)
        {
            BulkAdGroupProductPartition clientNode = null;
            foreach (BulkAdGroupProductPartition productPartition in productPartitions)
            {
                if (productPartition.ClientId == clientId)
                {
                    clientNode = productPartition;
                    break;
                }
            }
            return clientNode;
        }

        /// <summary>
        /// Helper class used to maintain a list of product partition actions for an ad group.
        /// The list of partition actions can be uploaded to the Bulk service.
        /// </summary>
        private class ProductPartitionHelper
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
            /// The list of BulkAdGroupProductPartition that can be uploaded to the Bulk service. 
            /// </summary>
            private List<BulkAdGroupProductPartition> partitionActions = new List<BulkAdGroupProductPartition>();

            /// <summary>
            /// Initializes an instance of the ProductPartitionHelper class.
            /// </summary>
            /// <param name="adGroupId">The ad group identifier associated with each criterion.</param>
            public ProductPartitionHelper(long adGroupId)
            {
                this.adGroupId = adGroupId;
            }

            /// <summary>
            /// Returns the list of partition actions that can be uploaded to the Bulk service.
            /// </summary>
            public IList<BulkAdGroupProductPartition> PartitionActions
            {
                get
                {
                    return partitionActions;
                }
            }

            /// <summary>
            /// Sets the Add action for a new BiddableAdGroupCriterion corresponding to the specified ProductCondition, 
            /// and adds it to the helper's list of BulkAdGroupProductPartition. 
            /// </summary>
            /// <param name="parent">The parent of the product partition subdivision that you want to add.</param>
            /// <param name="condition">The condition or product filter for the new product partition.</param>
            /// <param name="clientId">The Client Id in the bulk upload file corresponding to the product partition.</param>
            /// <returns>The BulkAdGroupProductPartition that was added to the list of PartitionActions.</returns>
            public BulkAdGroupProductPartition AddSubdivision(
                BulkAdGroupProductPartition parent,
                ProductCondition condition,
                string clientId
                )
            {
                var biddableAdGroupCriterion = new BiddableAdGroupCriterion()
                {
                    Id = this.referenceId--,
                    Criterion = new ProductPartition()
                    {
                        // If the root node is a unit, it would not have a parent
                        ParentCriterionId = parent != null && parent.AdGroupCriterion != null ? parent.AdGroupCriterion.Id : null,
                        Condition = condition,
                        PartitionType = ProductPartitionType.Subdivision
                    },
                    CriterionBid = null,
                    AdGroupId = this.adGroupId
                };

                var partitionAction = new BulkAdGroupProductPartition()
                {
                    ClientId = clientId,
                    AdGroupCriterion = biddableAdGroupCriterion
                };

                this.partitionActions.Add(partitionAction);

                return partitionAction;
            }

            /// <summary>
            /// Sets the Add action for a new AdGroupCriterion corresponding to the specified ProductCondition, 
            /// and adds it to the helper's list of BulkAdGroupProductPartition. 
            /// </summary>
            /// <param name="parent">The parent of the product partition unit that you want to add.</param>
            /// <param name="condition">The condition or product filter for the new product partition.</param>
            /// <param name="bidAmount">The bid amount for the new product partition.</param>
            /// <param name="isNegative">Indicates whether or not to add a NegativeAdGroupCriterion. 
            /// The default value is false, in which case a BiddableAdGroupCriterion will be added.</param>
            /// <returns>The BulkAdGroupProductPartition that was added to the list of PartitionActions.</returns>
            public BulkAdGroupProductPartition AddUnit(
                BulkAdGroupProductPartition parent,
                ProductCondition condition,
                double bidAmount,
                bool isNegative,
                string clientId
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
                    ParentCriterionId = parent != null && parent.AdGroupCriterion != null ? parent.AdGroupCriterion.Id : null,
                    Condition = condition,
                    PartitionType = ProductPartitionType.Unit
                };

                adGroupCriterion.AdGroupId = this.adGroupId;

                var partitionAction = new BulkAdGroupProductPartition()
                {
                    ClientId = clientId,
                    AdGroupCriterion = adGroupCriterion
                };

                this.partitionActions.Add(partitionAction);

                return partitionAction;
            }

            /// <summary>
            /// Sets the Delete action for the specified AdGroupCriterion, 
            /// and adds it to the helper's list of BulkAdGroupProductPartition. 
            /// </summary>
            /// <param name="adGroupCriterion">The BulkAdGroupProductPartition whose product partition you want to delete.</param>
            public void DeletePartition(BulkAdGroupProductPartition bulkAdGroupProductPartition)
            {
                if (bulkAdGroupProductPartition != null && bulkAdGroupProductPartition.AdGroupCriterion != null)
                {
                    bulkAdGroupProductPartition.AdGroupCriterion.AdGroupId = this.adGroupId;
                    bulkAdGroupProductPartition.AdGroupCriterion.Status = AdGroupCriterionStatus.Deleted;

                    this.partitionActions.Add(bulkAdGroupProductPartition);
                }

                return;
            }

            /// <summary>
            /// Sets the Update action for the specified BiddableAdGroupCriterion, 
            /// and adds it to the helper's list of BulkAdGroupProductPartition. 
            /// You can only update the CriterionBid and DestinationUrl elements 
            /// of the BiddableAdGroupCriterion. 
            /// When working with product partitions, youu cannot update the Criterion (ProductPartition). 
            /// To update a ProductPartition, you must delete the existing node (DeletePartition) and 
            /// add a new one (AddUnit or AddSubdivision) during the same call to ApplyProductPartitionActions. 
            /// </summary>
            /// <param name="biddableAdGroupCriterion">The BulkAdGroupProductPartition to update.</param>
            public void UpdatePartition(BulkAdGroupProductPartition bulkAdGroupProductPartition)
            {
                if (bulkAdGroupProductPartition != null && bulkAdGroupProductPartition.AdGroupCriterion != null)
                {
                    bulkAdGroupProductPartition.AdGroupCriterion.AdGroupId = this.adGroupId;

                    this.partitionActions.Add(bulkAdGroupProductPartition);
                }

                return;
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupProductPartition which each contain an AdGroupCriterion, formatted as a tree. 
        /// Each AdGroupCriterion must be either a BiddableAdGroupCriterion or NegativeAdGroupCriterion. 
        /// </summary>
        /// <param name="adGroupCriterions">The list of BulkAdGroupProductPartition to output formatted as a tree.</param>
        private void OutputProductPartitions(IList<BulkAdGroupProductPartition> bulkAdGroupProductPartitions)
        {
            // Set up the tree for output

            Dictionary<long, List<BulkAdGroupProductPartition>> childBranches =
                new Dictionary<long, List<BulkAdGroupProductPartition>>();
            BulkAdGroupProductPartition treeRoot = null;

            foreach (var bulkAdGroupProductPartition in bulkAdGroupProductPartitions)
            {
                AdGroupCriterion adGroupCriterion = bulkAdGroupProductPartition.AdGroupCriterion;
                if (adGroupCriterion != null)
                {
                    ProductPartition partition = (ProductPartition)adGroupCriterion.Criterion;
                    childBranches[(long)(adGroupCriterion.Id)] = new List<BulkAdGroupProductPartition>();

                    // The product partition with ParentCriterionId set to null is the root node.
                    if (partition.ParentCriterionId != null)
                    {
                        childBranches[(long)(partition.ParentCriterionId)].Add(bulkAdGroupProductPartition);
                    }
                    else
                    {
                        treeRoot = bulkAdGroupProductPartition;
                    }
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
            BulkAdGroupProductPartition node,
            Dictionary<long, List<BulkAdGroupProductPartition>> childBranches,
            int treeLevel)
        {
            AdGroupCriterion adGroupCriterion = node.AdGroupCriterion;

            OutputStatusMessage(string.Format("{0}{1}",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(adGroupCriterion.Criterion)).PartitionType)
            );

            OutputStatusMessage(string.Format("{0}ParentCriterionId: {1}",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(adGroupCriterion.Criterion)).ParentCriterionId)
            );

            OutputStatusMessage(string.Format("{0}Id: {1}",
                "".PadLeft(treeLevel, '\t'),
                adGroupCriterion.Id)
            );

            if (((ProductPartition)(adGroupCriterion.Criterion)).PartitionType == ProductPartitionType.Unit)
            {
                var biddableAdGroupCriterion = adGroupCriterion as BiddableAdGroupCriterion;
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
                    var negativeAdGroupCriterion = adGroupCriterion as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        OutputStatusMessage(string.Format("{0}Not Bidding on this Condition",
                            "".PadLeft(treeLevel, '\t'))
                        );
                    }
                }
            }

            var nullAttribute = ((ProductPartition)(adGroupCriterion.Criterion)).ParentCriterionId != null ? "(All other)" : "(Tree Root)";
            OutputStatusMessage(string.Format("{0}Attribute: {1}",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(adGroupCriterion.Criterion)).Condition.Attribute ?? nullAttribute)
            );

            OutputStatusMessage(string.Format("{0}Operand: {1}\n",
                "".PadLeft(treeLevel, '\t'),
                ((ProductPartition)(adGroupCriterion.Criterion)).Condition.Operand)
            );

            foreach (BulkAdGroupProductPartition childNode in childBranches[(long)(adGroupCriterion.Id)])
            {
                OutputProductPartitionTree(childNode, childBranches, treeLevel + 1);
            }
        }
    }
}
