using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.AdInsight;
using Microsoft.BingAds.V12.Bulk;
using Microsoft.BingAds.V12.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;
using System.Threading;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example uses the Bing Ads Bulk service to setup a Dynamic Search Ads (DSA) campaign.
    /// </summary>
    public class BulkDynamicSearchCampaigns : BulkExampleBase
    {
        public const string DOMAIN_NAME = "contoso.com";
        public const string LANGUAGE = "EN";

        public override string Description
        {
            get { return "Dynamic Search Ads (DSA) Campaigns | Bulk V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                AdInsightExampleHelper AdInsightExampleHelper = new AdInsightExampleHelper(this.OutputStatusMessage);
                AdInsightExampleHelper.AdInsightService = new ServiceClient<IAdInsightService>(authorizationData);

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(String.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var uploadEntities = new List<BulkEntity>();

                #region Add

                // To get started with dynamic search ads, first you'll need to add a new Campaign 
                // with its type set to DynamicSearchAds. When you create the campaign, you'll need to 
                // include a DynamicSearchAdsSetting that specifies the target website domain and language.

                var bulkCampaign = new BulkCampaign
                {
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        CampaignType = CampaignType.DynamicSearchAds,
                        Settings = new[] {
                            new DynamicSearchAdsSetting
                            {
                                DomainName = "contoso.com",
                                Language = "English"
                            }
                        },

                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = Microsoft.BingAds.V12.CampaignManagement.BudgetLimitType.DailyBudgetStandard,

                        // You can set your campaign bid strategy to Enhanced CPC (EnhancedCpcBiddingScheme) 
                        // and then, at any time, set an individual ad group bid strategy to 
                        // Manual CPC (ManualCpcBiddingScheme).
                        BiddingScheme = new EnhancedCpcBiddingScheme { },

                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // Used with CustomParameters defined in lower level entities such as ads.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    },
                };

                uploadEntities.Add(bulkCampaign);

                // Next, create a new AdGroup within the dynamic search ads campaign. 

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Women's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date
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
                    },
                };

                uploadEntities.Add(bulkAdGroup);

                // You can add one or more Webpage criterion to each ad group that helps determine 
                // whether or not to serve dynamic search ads.

                var adGroupWebpagePositivePageContent = new BulkAdGroupDynamicSearchAdTarget
                {
                    BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                    {
                        AdGroupId = adGroupIdKey,
                        CriterionBid = new FixedBid
                        {
                            Amount = 0.50
                        },
                        Criterion = new Webpage
                        {
                            Parameter = new WebpageParameter
                            {
                                Conditions = new[]
                                {
                                    new WebpageCondition
                                    {
                                        Argument = "flowers",
                                        Operand = WebpageConditionOperand.PageContent,
                                    }
                                },
                                CriterionName = "Ad Group Webpage Positive Page Content Criterion"
                            },
                        },
                        // DestinationUrl and FinalUrls are not supported with Webpage criterion. 
                        // The Final URL is dynamically created at the ad level.
                        DestinationUrl = null,
                        FinalUrls = null,
                        
                        // In this example we are deferring to the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this webpage criterion.
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
                    }
                };
                uploadEntities.Add(adGroupWebpagePositivePageContent);

                // To discover the categories that you can use for Webpage criterion (positive or negative), 
                // use the GetDomainCategories operation with the Ad Insight service.

                var getDomainCategoriesResponse = await AdInsightExampleHelper.GetDomainCategoriesAsync(
                    categoryName: null, 
                    domainName: DOMAIN_NAME, 
                    language: LANGUAGE);
                var categories = getDomainCategoriesResponse.Categories;

                // If any categories are available let's use one as a condition.
                if (categories.Count > 0)
                {
                    var adGroupWebpagePositiveCategory = new BulkAdGroupDynamicSearchAdTarget
                    {
                        BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                        {
                            AdGroupId = adGroupIdKey,
                            CriterionBid = new FixedBid
                            {
                                Amount = 0.50
                            },
                            Criterion = new Webpage
                            {
                                Parameter = new WebpageParameter
                                {
                                    Conditions = new[]
                                    {
                                        new WebpageCondition
                                        {
                                            Argument = categories[0].CategoryName,
                                            Operand = WebpageConditionOperand.Category,
                                        }
                                    },
                                    CriterionName = "Ad Group Webpage Positive Category Criterion"
                                },
                            }
                        }
                    };
                    uploadEntities.Add(adGroupWebpagePositiveCategory);
                }

                // If you want to exclude certain portions of your website, you can add negative Webpage 
                // criterion at the campaign and ad group level. 

                var adGroupWebpageNegativeUrl = new BulkAdGroupNegativeDynamicSearchAdTarget
                {
                    NegativeAdGroupCriterion = new NegativeAdGroupCriterion
                    {
                        AdGroupId = adGroupIdKey,
                        Criterion = new Webpage
                        {
                            Parameter = new WebpageParameter
                            {
                                // You can choose whether you want the criterion argument to match partial URLs, 
                                // page content, page title, or categories that Bing thinks applies to your website.
                                Conditions = new[]
                                {
                                    new WebpageCondition
                                    {
                                        Argument = DOMAIN_NAME,
                                        Operand = WebpageConditionOperand.Url,
                                    }
                                },
                                // If you do not specify any name, then it will be set to a concatenated list of conditions. 
                                CriterionName = null
                            }
                        }
                    }
                };
                uploadEntities.Add(adGroupWebpageNegativeUrl);

                // The negative Webpage criterion at the campaign level applies to all ad groups 
                // within the campaign; however, if you define ad group level negative Webpage criterion, 
                // the campaign criterion is ignored for that ad group.

                var campaignWebpageNegative = new BulkCampaignNegativeDynamicSearchAdTarget
                {
                    NegativeCampaignCriterion = new NegativeCampaignCriterion
                    {
                        CampaignId = campaignIdKey,
                        Criterion = new Webpage
                        {
                            Parameter = new WebpageParameter
                            {
                                Conditions = new[]
                                {
                                    new WebpageCondition
                                    {
                                        Argument = DOMAIN_NAME + "\\seattle",
                                        Operand = WebpageConditionOperand.Url,
                                    }
                                },
                                CriterionName = "Campaign Negative Webpage Url Criterion"
                            }
                        }
                    }
                };
                uploadEntities.Add(campaignWebpageNegative);


                // Finally you can add a DynamicSearchAd into the ad group. The ad title and display URL 
                // are generated automatically based on the website domain and language that you want to target.

                var bulkDynamicSearchAd = new BulkDynamicSearchAd
                {
                    ClientId = "here",
                    AdGroupId = adGroupIdKey,
                    DynamicSearchAd = new DynamicSearchAd
                    {
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",

                        // You cannot set FinalUrls. The Final URL will be a dynamically selected landing page.
                        // The final URL is distinct from the path that customers will see and click on in your ad.
                        FinalUrls = null,
                        
                        // In this example we are deferring to the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this ad.
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
                    },
                };

                uploadEntities.Add(bulkDynamicSearchAd);
                
                // Upload and write the output

                OutputStatusMessage("Adding campaign, ad group, criterions, and ads . . .\n");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var campaignNegativeDynamicSearchAdTargetResults = downloadEntities.OfType<BulkCampaignNegativeDynamicSearchAdTarget>().ToList();
                OutputBulkCampaignNegativeDynamicSearchAdTargets(campaignNegativeDynamicSearchAdTargetResults);

                var adGroupDynamicSearchAdTargetResults = downloadEntities.OfType<BulkAdGroupDynamicSearchAdTarget>().ToList();
                OutputBulkAdGroupDynamicSearchAdTargets(adGroupDynamicSearchAdTargetResults);

                var adGroupNegativeDynamicSearchAdTargetResults = downloadEntities.OfType<BulkAdGroupNegativeDynamicSearchAdTarget>().ToList();
                OutputBulkAdGroupNegativeDynamicSearchAdTargets(adGroupNegativeDynamicSearchAdTargetResults);

                var dynamicSearchAdResults = downloadEntities.OfType<BulkDynamicSearchAd>().ToList();
                OutputBulkDynamicSearchAds(dynamicSearchAdResults);

                Reader.Dispose();

                #endregion Add

                #region Update

                uploadEntities = new List<BulkEntity>();

                // You can update the bid for BiddableAdGroupCriterion

                var updateBid = new FixedBid
                {
                    Amount = 0.75
                };

                // You can update the Webpage criterion name but cannot update the conditions. 
                // To update the conditions you must delete the criterion and add a new criterion.
                // This update attempt will return an error.

                var updateCriterionAttemptFailure = new Webpage
                {
                    Parameter = new WebpageParameter
                    {
                        Conditions = new[]
                        {
                            new WebpageCondition
                            {
                                Argument = "Books",
                                Operand = WebpageConditionOperand.PageContent,
                            }
                        },
                        CriterionName = "Update Attempt Failure"
                    },
                };

                var updateCriterionAttemptSuccess = new Webpage
                {
                    Parameter = new WebpageParameter
                    {
                        CriterionName = "Update Attempt Success"
                    },
                };

                foreach (var adGroupDynamicSearchAdTargetResult in adGroupDynamicSearchAdTargetResults)
                {
                    var biddableAdGroupCriterion = adGroupDynamicSearchAdTargetResult.BiddableAdGroupCriterion as BiddableAdGroupCriterion;
                    if (biddableAdGroupCriterion != null)
                    {
                        ((BiddableAdGroupCriterion)adGroupDynamicSearchAdTargetResult.BiddableAdGroupCriterion).CriterionBid = updateBid;
                        adGroupDynamicSearchAdTargetResult.BiddableAdGroupCriterion.Criterion = updateCriterionAttemptSuccess;
                        uploadEntities.Add(adGroupDynamicSearchAdTargetResult);
                    }
                }

                foreach (var adGroupNegativeDynamicSearchAdTargetResult in adGroupNegativeDynamicSearchAdTargetResults)
                {
                    var negativeAdGroupCriterion = adGroupNegativeDynamicSearchAdTargetResult.NegativeAdGroupCriterion as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        adGroupNegativeDynamicSearchAdTargetResult.NegativeAdGroupCriterion.Criterion = updateCriterionAttemptFailure;
                        uploadEntities.Add(adGroupNegativeDynamicSearchAdTargetResult);
                    }
                }

                OutputStatusMessage("Updating Ad Group Webpage Criterion . . . \n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                adGroupDynamicSearchAdTargetResults = downloadEntities.OfType<BulkAdGroupDynamicSearchAdTarget>().ToList();
                OutputBulkAdGroupDynamicSearchAdTargets(adGroupDynamicSearchAdTargetResults);

                adGroupNegativeDynamicSearchAdTargetResults = downloadEntities.OfType<BulkAdGroupNegativeDynamicSearchAdTarget>().ToList();
                OutputBulkAdGroupNegativeDynamicSearchAdTargets(adGroupNegativeDynamicSearchAdTargetResults);

                Reader.Dispose();


                #endregion Update

                #region Get
                
                var entities = new[] {
                    DownloadEntity.AdGroupDynamicSearchAdTargets,
                    DownloadEntity.AdGroupNegativeDynamicSearchAdTargets
                };

                var downloadParameters = new DownloadParameters
                {
                    CampaignIds = null,
                    DataScope = DataScope.EntityData | DataScope.EntityPerformanceData,
                    PerformanceStatsDateRange = new PerformanceStatsDateRange { PredefinedTime = ReportTimePeriod.LastFourWeeks },
                    DownloadEntities = entities,
                    FileType = FileType,
                    LastSyncTimeInUTC = null,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true
                };

                // You may optionally cancel the DownloadFileAsync operation after a specified time interval. 
                var tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(TimeoutInMilliseconds);

                var resultFilePath = await BulkServiceManager.DownloadFileAsync(downloadParameters, progress, tokenSource.Token);
                OutputStatusMessage(String.Format("Download result file: {0}\n", resultFilePath));

                #endregion Get

                #region CleanUp

                // Delete the campaign, ad group, criterion, and ad that were previously added. 
                // You should remove this operation if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                // You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                // When you delete a BulkCampaign, the dependent entities such as BulkAdGroup 
                // are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                OutputStatusMessage("\nDeleting DSA campaign, criterions, and ad . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

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
    }
}
