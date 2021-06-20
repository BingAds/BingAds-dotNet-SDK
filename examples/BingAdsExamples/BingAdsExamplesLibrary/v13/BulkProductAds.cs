using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to apply product conditions for Microsoft Shopping Campaigns with the Bulk service.
    /// </summary>
    public class BulkProductAds : BulkExampleBase
    {
        public override string Description
        {
            get { return "Microsoft Shopping Campaigns | Bulk V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;
                
                // The Bing Merchant Center Store Id cannot be retrieved via the Bulk service, 
                // so we'll use the Campaign Management service i.e., the GetBMCStoresByCustomerId service operation below.

                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                // Get a list of all Bing Merchant Center stores associated with your CustomerId.

                OutputStatusMessage("-----\nGetBMCStoresByCustomerId:");
                IList<BMCStore> stores = (await CampaignManagementExampleHelper.GetBMCStoresByCustomerIdAsync(null))?.BMCStores;
                if (stores == null)
                {
                    OutputStatusMessage(
                        string.Format("You do not have any BMC stores registered for CustomerId {0}.", authorizationData.CustomerId)
                    );
                    return;
                }                

                var uploadEntities = new List<BulkEntity>();

                // Create a Shopping campaign with product conditions.

                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        CampaignType = CampaignType.Shopping,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        Settings = new[] {
                            new ShoppingSetting() {
                                Priority = 0,
                                SalesCountryCode = "US",
                                StoreId = (int)stores[0].Id
                            }
                        },
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };
                uploadEntities.Add(bulkCampaign);

                // Optionally, you can create a ProductScope criterion that will be associated with your Microsoft Shopping campaign. 
                // You'll also be able to add more specific product conditions for each ad group.

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
                        Status = CampaignCriterionStatus.Active
                    },
                };
                uploadEntities.Add(bulkCampaignProductScope);

                // Create the ad group that will have the product partitions.

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V13.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                    },
                };
                uploadEntities.Add(bulkAdGroup);

                // Create a product ad. You must add at least one product ad to the ad group. 
                // The product ad identifier can be used for reporting analytics.
                // Use Merchant Promotions if you want tags to appear at the bottom of your product ad 
                // as "special offer" links, helping to increase customer engagement. For details
                // on Merchant Promotions see https://help.bingads.microsoft.com/#apex/3/en/56805/0.

                var bulkProductAd = new BulkProductAd
                {
                    AdGroupId = adGroupIdKey,
                    ProductAd = new ProductAd {}
                };
                uploadEntities.Add(bulkProductAd);

                // Upload and write the output

                OutputStatusMessage("-----\nAdding the campaign, product scope, ad group, and ad...");
                
                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var campaignProductScopeResults = downloadEntities.OfType<BulkCampaignProductScope>().ToList();
                OutputBulkCampaignProductScopes(campaignProductScopeResults);

                var productAdResults = downloadEntities.OfType<BulkProductAd>().ToList();
                OutputBulkProductAds(productAdResults);
                                
                Reader.Dispose();
                
                // Bid all products

                var adGroupId = (long)adGroupResults[0].AdGroup.Id;

                var helper = new ProductPartitionHelper(adGroupId);

                var root = helper.AddUnit(
                    null,
                    new ProductCondition { Operand = "All", Attribute = null },
                    0.35,
                    false,
                    "root"
                );

                OutputStatusMessage("-----\nApplying only the root as a Unit with a bid...");
                var applyBulkProductPartitionActionsResults =
                    await ApplyBulkProductPartitionActions(helper.PartitionActions);

                var productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

                OutputStatusMessage("The ad group's product partition only has a tree root node:");
                OutputProductPartitions(productPartitions);

                // Let's update the bid of the root Unit we just added.

                var updatedRoot = GetNodeByClientId(applyBulkProductPartitionActionsResults, "root");
                var bid = new FixedBid
                {
                    Amount = 0.45
                };
                ((BiddableAdGroupCriterion)(updatedRoot.AdGroupCriterion)).CriterionBid = bid;

                helper = new ProductPartitionHelper(adGroupId);
                helper.UpdatePartition(updatedRoot);

                OutputStatusMessage("Updating the bid for the tree root node...");
                await ApplyBulkProductPartitionActions(helper.PartitionActions);

                productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

                OutputStatusMessage("Updated the bid for the tree root node:");
                OutputProductPartitions(productPartitions);

                // Initialize and overwrite any existing tree root, and build a product partition group tree structure in multiple steps. 
                // You could build the entire tree in a single call since there are less than 20,000 nodes; however, 
                // we will build it in steps to demonstrate how to use the results from bulk upload to update the tree. 

                helper = new ProductPartitionHelper(adGroupId);

                // Check whether a root node exists already.

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

                // The direct children of any node must have the same Operand. 
                // For this example we will use CategoryL1 nodes as children of the root. 
                // For a list of valid CategoryL1 through CategoryL5 values, see the Bing Category Taxonomy:
                // https://go.microsoft.com/fwlink?LinkId=507666

                var animalsSubdivision = helper.AddSubdivision(
                    root,
                    new ProductCondition { Operand = "CategoryL1", Attribute = "Animals & Pet Supplies" },
                    "animalsSubdivision"
                );

                // If you use a CategoryL2 node, it must be a descendant (child or later) of a CategoryL1 node. 
                // In other words you cannot have a CategoryL2 node as parent of a CategoryL1 node. 
                // For this example we will a CategoryL2 node as child of the CategoryL1 Animals & Pet Supplies node. 

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

                // If you won't bid on Brand B, set the helper method's bidAmount to '0' and isNegative to true. 
                // The helper method will create a NegativeAdGroupCriterion and apply the condition.

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

                OutputStatusMessage("-----\nApplying product partitions to the ad group...");
                applyBulkProductPartitionActionsResults =
                    await ApplyBulkProductPartitionActions(helper.PartitionActions);

                productPartitions = await GetBulkAdGroupProductPartitionTree(adGroupId);

                // The product partition group tree now has 9 nodes. 

                //All other (Root Node)
                // |
                // +-- Animals & Pet Supplies (CategoryL1)
                // |    |
                // |    +-- Pet Supplies (CategoryL2)
                // |    |    |
                // |    |    +-- Brand A
                // |    |    |    
                // |    |    +-- Brand B
                // |    |    |    
                // |    |    +-- All other (Brand)
                // |    |         
                // |    +-- All other (CategoryL2)
                // |        
                // +-- Electronics (CategoryL1)
                // |   
                // +-- All other (CategoryL1)

                OutputStatusMessage("The product partition group tree now has 9 nodes:");
                OutputProductPartitions(productPartitions);

                // Let's replace the Electronics (CategoryL1) node created above with an Electronics (CategoryL1) node that 
                // has children i.e. Brand C (Brand), Brand D (Brand), and All other (Brand) as follows: 

                //Electronics (CategoryL1)
                //|
                //+-- Brand C (Brand)
                //|
                //+-- Brand D (Brand)
                //|
                //+-- All other (Brand)

                helper = new ProductPartitionHelper(adGroupId);

                // To replace a node we must know its Id and its ParentCriterionId. In this case the parent of the node 
                // we are replacing is All other (Root Node). The node that we are replacing is Electronics (CategoryL1). 

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

                // The product partition group tree now has 12 nodes, including the children of Electronics (CategoryL1):

                //All other (Root Node)
                // |
                // +-- Animals & Pet Supplies (CategoryL1)
                // |    |
                // |    +-- Pet Supplies (CategoryL2)
                // |    |    |
                // |    |    +-- Brand A
                // |    |    |    
                // |    |    +-- Brand B
                // |    |    |    
                // |    |    +-- All other (Brand)
                // |    |         
                // |    +-- All other (CategoryL2)
                // |        
                // +-- Electronics (CategoryL1)
                // |    |
                // |    +-- Brand C (Brand)
                // |    |
                // |    +-- Brand D (Brand)
                // |    |
                // |    +-- All other (Brand)
                // |   
                // +-- All other (CategoryL1)

                OutputStatusMessage(
                    "The product partition group tree now has 12 nodes, including the children of Electronics (CategoryL1): \n"
                );
                OutputProductPartitions(productPartitions);

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("-----\nDeleting the campaign and everything it contains e.g., ad groups and ads...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                Reader.Dispose();
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V13.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
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

            var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeoutInMilliseconds);

            Writer = new BulkFileWriter(
                filePath: FileDirectory + UploadFileName);

            foreach (var partitionAction in partitionActions)
            {
                Writer.WriteEntity(partitionAction);
            }
            Writer.Dispose();

            var bulkFilePath = await BulkServiceManager.UploadFileAsync(
                parameters: fileUploadParameters,
                progress: progress,
                cancellationToken: tokenSource.Token);

            Reader = new BulkFileReader(
                filePath: bulkFilePath, 
                resultFileType: ResultFileType.Upload, 
                fileFormat: FileType);

            OutputStatusMessage("Upload results:");

            var downloadEntities = Reader.ReadEntities().ToList();
            var bulkAdGroupProductPartitionResults = downloadEntities.OfType<BulkAdGroupProductPartition>().ToList();
            OutputBulkAdGroupProductPartitions(bulkAdGroupProductPartitionResults);

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
            /// and does not yet have a Microsoft Advertising system identifier. This identifier will be used as the ParentCriterionId 
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
