using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V10.Bulk;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.CustomerManagement;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add ads and keywords to a new ad group using the BulkServiceManager class.
    /// </summary>
    public class BulkKeywordsAds : BulkExampleBase
    {
        public static ServiceClient<ICustomerManagementService> CustomerService;

        public override string Description
        {
            get { return "Keywords and Ads | Bulk V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(String.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var uploadEntities = new List<BulkEntity>();

                #region Add

                CustomerService = new ServiceClient<ICustomerManagementService>(authorizationData);

                // Determine whether you are able to add shared budgets by checking the pilot flags.

                bool enabledForSharedBudgets = false;
                var featurePilotFlags = await GetCustomerPilotFeaturesAsync(authorizationData.CustomerId);

                // The pilot flag value for shared budgets is 263.
                // Pilot flags apply to all accounts within a given customer.
                if (featurePilotFlags.Any(pilotFlag => pilotFlag == 263))
                {
                    OutputStatusMessage("Customer is in pilot for Shared Budget.\n");
                    enabledForSharedBudgets = true;
                }
                else
                {
                    OutputStatusMessage("Customer is not in pilot for Shared Budget.\n");
                }

                // If the customer is enabled for shared budgets, let's create a new budget and
                // share it with a new campaign.

                if (enabledForSharedBudgets)
                {
                    var bulkBudget = new BulkBudget
                    {
                        ClientId = "YourClientIdGoesHere",
                        Budget = new Budget
                        {
                            Amount = 50,
                            BudgetType = BudgetLimitType.DailyBudgetStandard,
                            Id = budgetIdKey,
                            Name = "My Shared Budget " + DateTime.UtcNow,
                        }
                    };
                    uploadEntities.Add(bulkBudget);
                }

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
                        // will be used when associating this new campaign with a new call ad extension in the BulkCampaignCallAdExtension object below. 
                        Id = campaignIdKey,
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = enabledForSharedBudgets ? budgetIdKey : 0,
                        DailyBudget = enabledForSharedBudgets ? 0 : 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,

                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // DaylightSaving is not supported in the Bulk file schema. Whether or not you specify it in a BulkCampaign,
                        // the value is not written to the Bulk file, and by default DaylightSaving is set to true.
                        DaylightSaving = true,

                        // You can set your campaign bid strategy to Enhanced CPC (EnhancedCpcBiddingScheme) 
                        // and then, at any time, set an individual ad group or keyword bid strategy to 
                        // Manual CPC (ManualCpcBiddingScheme).
                        // For campaigns you can use either of the EnhancedCpcBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then ManualCpcBiddingScheme is used by default.
                        BiddingScheme = new EnhancedCpcBiddingScheme { },

                        Status = CampaignStatus.Paused,
                        
                        // Used with FinalUrls shown in the expanded text ads that we will add below.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    }
                };

                // Specify one or more ad groups.
                
                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        BiddingModel = BiddingModel.Keyword,
                        PricingModel = PricingModel.Cpc,
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        Language = "English",
                        Status = AdGroupStatus.Active,

                        // For ad groups you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                        // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                        BiddingScheme = new ManualCpcBiddingScheme { },

                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,
                    },
                };

                // In this example only the second keyword should succeed. The Text of the first keyword exceeds the limit,
                // and the third keyword is a duplicate of the second keyword. 

                var bulkKeywords = new [] {
                    new BulkKeyword{
                        AdGroupId = adGroupIdKey,
                        Keyword = new Keyword
                        {
                            Bid = new Bid { Amount = 0.47 },
                            Param2 = "10% Off",
                            MatchType = MatchType.Broad,
                            Text = "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes " +
                                   "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes " +
                                   "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes",
                            // For keywords you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                            // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                            BiddingScheme = new InheritFromParentBiddingScheme { },
                        },
                    },
                    new BulkKeyword{
                        AdGroupId = adGroupIdKey,
                        Keyword = new Keyword
                        {
                            Bid = new Bid { Amount = 0.47 },
                            Param2 = "10% Off",
                            MatchType = MatchType.Phrase,
                            Text = "Brand-A Shoes",
                            // For keywords you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                            // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                            BiddingScheme = new InheritFromParentBiddingScheme { },
                        },
                    },
                    new BulkKeyword{
                        AdGroupId = adGroupIdKey,
                        Keyword = new Keyword
                        {
                            Bid = new Bid { Amount = 0.47 },
                            Param2 = "10% Off",
                            MatchType = MatchType.Phrase,
                            Text = "Brand-A Shoes",
                            // For keywords you can use either of the InheritFromParentBiddingScheme or ManualCpcBiddingScheme objects. 
                            // If you do not set this element, then InheritFromParentBiddingScheme is used by default.
                            BiddingScheme = new InheritFromParentBiddingScheme { },
                        },
                    },
                };

                // In this example only the first 3 ads should succeed. 
                // The TitlePart2 of the fourth ad is empty and not valid,
                // and the fifth ad is a duplicate of the second ad. 

                var bulkExpandedTextAds = new [] {
                    new BulkExpandedTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        ExpandedTextAd = new ExpandedTextAd
                        {
                            TitlePart1 = "Contoso",
                            TitlePart2 = "Fast & Easy Setup",
                            Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                            Path1 = "seattle",
                            Path2 = "shoe sale",

                            // With FinalUrls you can separate the tracking template, custom parameters, and 
                            // landing page URLs. 
                            FinalUrls = new[] {
                                "http://www.contoso.com/womenshoesale",
                            },
                            // Final Mobile URLs can also be used if you want to direct the user to a different page 
                            // for mobile devices.
                            FinalMobileUrls = new[] {
                                "http://mobile.contoso.com/womenshoesale",
                            }, 
                            // You could use a tracking template which would override the campaign level
                            // tracking template. Tracking templates defined for lower level entities 
                            // override those set for higher level entities.
                            // In this example we are using the campaign level tracking template.
                            TrackingUrlTemplate = null,

                            // Set custom parameters that are specific to this ad, 
                            // and can be used by the ad, ad group, campaign, or account level tracking template. 
                            // In this example we are using the campaign level tracking template.
                            UrlCustomParameters = new CustomParameters {
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
                        },
                    },
                    new BulkExpandedTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        ExpandedTextAd = new ExpandedTextAd
                        {
                            TitlePart1 = "Contoso",
                            TitlePart2 = "Quick & Easy Setup",
                            Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                            Path1 = "seattle",
                            Path2 = "shoe sale",

                            // With FinalUrls you can separate the tracking template, custom parameters, and 
                            // landing page URLs. 
                            FinalUrls = new[] {
                                "http://www.contoso.com/womenshoesale"
                            },
                            // Final Mobile URLs can also be used if you want to direct the user to a different page 
                            // for mobile devices.
                            FinalMobileUrls = new[] {
                                "http://mobile.contoso.com/womenshoesale"
                            }, 
                            // You could use a tracking template which would override the campaign level
                            // tracking template. Tracking templates defined for lower level entities 
                            // override those set for higher level entities.
                            // In this example we are using the campaign level tracking template.
                            TrackingUrlTemplate = null,

                            // Set custom parameters that are specific to this ad, 
                            // and can be used by the ad, ad group, campaign, or account level tracking template. 
                            // In this example we are using the campaign level tracking template.
                            UrlCustomParameters = new CustomParameters {
                                Parameters = new[] {
                                    new CustomParameter(){
                                        Key = "promoCode",
                                        Value = "PROMO2"
                                    },
                                    new CustomParameter(){
                                        Key = "season",
                                        Value = "summer"
                                    },
                                }
                            },
                        },
                    },
                    new BulkExpandedTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        ExpandedTextAd = new ExpandedTextAd
                        {
                            TitlePart1 = "Contoso",
                            TitlePart2 = "Fast & Simple Setup",
                            Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                            Path1 = "seattle",
                            Path2 = "shoe sale",

                            // With FinalUrls you can separate the tracking template, custom parameters, and 
                            // landing page URLs. 
                            FinalUrls = new[] {
                                "http://www.contoso.com/womenshoesale"
                            },
                            // Final Mobile URLs can also be used if you want to direct the user to a different page 
                            // for mobile devices.
                            FinalMobileUrls = new[] {
                                "http://mobile.contoso.com/womenshoesale"
                            }, 
                            // You could use a tracking template which would override the campaign level
                            // tracking template. Tracking templates defined for lower level entities 
                            // override those set for higher level entities.
                            // In this example we are using the campaign level tracking template.
                            TrackingUrlTemplate = null,

                            // Set custom parameters that are specific to this ad, 
                            // and can be used by the ad, ad group, campaign, or account level tracking template. 
                            // In this example we are using the campaign level tracking template.
                            UrlCustomParameters = new CustomParameters {
                                Parameters = new[] {
                                    new CustomParameter(){
                                        Key = "promoCode",
                                        Value = "PROMO3"
                                    },
                                    new CustomParameter(){
                                        Key = "season",
                                        Value = "summer"
                                    },
                                }
                            },
                        },
                    },
                    new BulkExpandedTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        ExpandedTextAd = new ExpandedTextAd
                        {
                            TitlePart1 = "Contoso",
                            TitlePart2 = "",
                            Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                            Path1 = "seattle",
                            Path2 = "shoe sale",  

                            // With FinalUrls you can separate the tracking template, custom parameters, and 
                            // landing page URLs. 
                            FinalUrls = new[] {
                                "http://www.contoso.com/womenshoesale"
                            },
                            // Final Mobile URLs can also be used if you want to direct the user to a different page 
                            // for mobile devices.
                            FinalMobileUrls = new[] {
                                "http://mobile.contoso.com/womenshoesale"
                            }, 
                            // You could use a tracking template which would override the campaign level
                            // tracking template. Tracking templates defined for lower level entities 
                            // override those set for higher level entities.
                            // In this example we are using the campaign level tracking template.
                            TrackingUrlTemplate = null,

                            // Set custom parameters that are specific to this ad, 
                            // and can be used by the ad, ad group, campaign, or account level tracking template. 
                            // In this example we are using the campaign level tracking template.
                            UrlCustomParameters = new CustomParameters {
                                Parameters = new[] {
                                    new CustomParameter(){
                                        Key = "promoCode",
                                        Value = "PROMO4"
                                    },
                                    new CustomParameter(){
                                        Key = "season",
                                        Value = "summer"
                                    },
                                }
                            },
                        },
                    },
                    new BulkExpandedTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        ExpandedTextAd = new ExpandedTextAd
                        {
                            TitlePart1 = "Contoso",
                            TitlePart2 = "Quick & Easy Setup",
                            Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                            Path1 = "seattle",
                            Path2 = "shoe sale",

                            // With FinalUrls you can separate the tracking template, custom parameters, and 
                            // landing page URLs. 
                            FinalUrls = new[] {
                                "http://www.contoso.com/womenshoesale"
                            },
                            // Final Mobile URLs can also be used if you want to direct the user to a different page 
                            // for mobile devices.
                            FinalMobileUrls = new[] {
                                "http://mobile.contoso.com/womenshoesale"
                            }, 
                            // You could use a tracking template which would override the campaign level
                            // tracking template. Tracking templates defined for lower level entities 
                            // override those set for higher level entities.
                            // In this example we are using the campaign level tracking template.
                            TrackingUrlTemplate = null,

                            // Set custom parameters that are specific to this ad, 
                            // and can be used by the ad, ad group, campaign, or account level tracking template. 
                            // In this example we are using the campaign level tracking template.
                            UrlCustomParameters = new CustomParameters {
                                Parameters = new[] {
                                    new CustomParameter(){
                                        Key = "promoCode",
                                        Value = "PROMO5"
                                    },
                                    new CustomParameter(){
                                        Key = "season",
                                        Value = "summer"
                                    },
                                }
                            },
                        },
                    },
                };

                uploadEntities.Add(bulkCampaign);
                uploadEntities.Add(bulkAdGroup);

                foreach (var bulkKeyword in bulkKeywords)
                {
                    uploadEntities.Add(bulkKeyword);
                }

                foreach (var bulkExpandedTextAd in bulkExpandedTextAds)
                {
                    uploadEntities.Add(bulkExpandedTextAd);
                }

                // Upload and write the output

                OutputStatusMessage("Adding campaign, budget, ad group, ads, and keywords...\n");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var budgetResults = downloadEntities.OfType<BulkBudget>().ToList();
                OutputBulkBudgets(budgetResults);

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var keywordResults = downloadEntities.OfType<BulkKeyword>().ToList();
                OutputBulkKeywords(keywordResults);

                var expandedTextAdResults = downloadEntities.OfType<BulkExpandedTextAd>().ToList();
                OutputBulkExpandedTextAds(expandedTextAdResults);

                Reader.Dispose();

                #endregion Add

                #region Update

                // Here is a simple example that updates the campaign budget.
                
                var downloadParameters = new DownloadParameters
                {
                    Entities = BulkDownloadEntity.Budgets | BulkDownloadEntity.Campaigns,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true,
                    LastSyncTimeInUTC = null
                };

                // Download all campaigns and shared budgets in the account.
                var bulkFilePath = await BulkService.DownloadFileAsync(downloadParameters);
                OutputStatusMessage("\nDownloaded all campaigns and shared budgets in the account.\n");
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.FullDownload, FileType);
                downloadEntities = Reader.ReadEntities().ToList();
                var getBudgetResults = downloadEntities.OfType<BulkBudget>().ToList();
                OutputBulkBudgets(getBudgetResults);
                var getCampaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(getCampaignResults);

                uploadEntities = new List<BulkEntity>();

                // If the campaign has a shared budget you cannot update the Campaign budget amount,
                // and you must instead update the amount in the Budget record. If you try to update 
                // the budget amount of a Campaign that has a shared budget, the service will return 
                // the CampaignServiceCannotUpdateSharedBudget error code.

                foreach (var entity in getBudgetResults)
                {
                    if (entity.Budget.Id > 0)
                    {
                        // Increase budget by 20 %
                        entity.Budget.Amount *= 1.2m;
                        uploadEntities.Add(entity);
                    }
                }

                foreach (var entity in getCampaignResults)
                {
                    if (entity.Campaign.BudgetId == null || entity.Campaign.BudgetId <= 0)
                    {
                        // Increase existing budgets by 20%
                        // Monthly budgets are deprecated and there will be a forced migration to daily budgets in calendar year 2017. 
                        // Shared budgets do not support the monthly budget type, so this is only applicable to unshared budgets. 
                        // During the migration all campaign level unshared budgets will be rationalized as daily. 
                        // The formula that will be used to convert monthly to daily budgets is: Monthly budget amount / 30.4.
                        // Moving campaign monthly budget to daily budget is encouraged before monthly budgets are migrated. 

                        if (entity.Campaign.BudgetType == BudgetLimitType.MonthlyBudgetSpendUntilDepleted)
                        {
                            // Increase budget by 20 %
                            entity.Campaign.BudgetType = BudgetLimitType.DailyBudgetStandard;
                            entity.Campaign.DailyBudget = entity.Campaign.MonthlyBudget / 30.4 * 1.2;
                        }
                        else
                        {
                            // Increase budget by 20 %
                            entity.Campaign.DailyBudget *= 1.2;
                        }
                        uploadEntities.Add(entity);
                    }
                }

                Reader.Dispose();

                if (uploadEntities.Count > 0)
                {
                    OutputStatusMessage("\nChanged local campaign budget amounts. Starting upload.\n");

                    Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                    downloadEntities = Reader.ReadEntities().ToList();
                    getBudgetResults = downloadEntities.OfType<BulkBudget>().ToList();
                    OutputBulkBudgets(getBudgetResults);
                    getCampaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                    OutputBulkCampaigns(getCampaignResults);
                    Reader.Dispose();
                }
                else
                {
                    OutputStatusMessage("\nNo campaigns or shared budgets in account.\n");
                }

                #endregion Update

                #region CleanUp

                //Delete the campaign, ad group, ads, and keywords that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                //When you delete a BulkCampaign, the dependent entities such as BulkAdGroup, BulkKeyword, 
                //and BulkExpandedTextAd are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var budgetResult in budgetResults)
                {
                    budgetResult.Status = Status.Deleted;
                    uploadEntities.Add(budgetResult);
                }

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }
                
                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign, budget, ad group, keywords, and ads . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                getBudgetResults = downloadEntities.OfType<BulkBudget>().ToList();
                OutputBulkBudgets(getBudgetResults);
                getCampaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(getCampaignResults);

                Reader.Dispose();

                #endregion Cleanup
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V10.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.Bulk.ApiFaultDetail> ex)
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
        /// Gets the list of pilot features that the customer is able to use.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<IList<int>> GetCustomerPilotFeaturesAsync(long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request)).FeaturePilotFlags.ToArray();
        }
    }
}
