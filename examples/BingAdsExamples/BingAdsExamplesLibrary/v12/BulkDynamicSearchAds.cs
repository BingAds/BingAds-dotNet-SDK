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

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// How to setup a Dynamic Search Ads (DSA) campaign with the Bulk service.
    /// </summary>
    public class BulkDynamicSearchAds : BulkExampleBase
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
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                AdInsightExampleHelper AdInsightExampleHelper = new AdInsightExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                AdInsightExampleHelper.AdInsightService = new ServiceClient<IAdInsightService>(
                    authorizationData: authorizationData,
                    environment: environment);

                // Used to output the Campaign Management objects within Bulk entities.
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);

                var uploadEntities = new List<BulkEntity>();
                
                // To get started with dynamic search ads, first you'll need to add a new Campaign 
                // with its type set to DynamicSearchAds. When you create the campaign, you'll need to 
                // include a DynamicSearchAdsSetting that specifies the target website domain and language.

                var bulkCampaign = new BulkCampaign
                {
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        BudgetType = Microsoft.BingAds.V12.CampaignManagement.BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.DynamicSearchAds,
                        Languages = new string[] { "All" },
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        Settings = new[] {
                            new DynamicSearchAdsSetting
                            {
                                DomainName = "contoso.com",
                                Language = "English"
                            }
                        },
                    },
                };

                uploadEntities.Add(bulkCampaign);

                // Create a new ad group within the dynamic search ads campaign. 

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
                        CpcBid = new Bid { Amount = 0.09 },
                    },
                };

                uploadEntities.Add(bulkAdGroup);

                // You can add one or more Webpage criteria to each ad group that helps determine 
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
                    }
                };
                uploadEntities.Add(adGroupWebpagePositivePageContent);

                // To discover the categories that you can use for Webpage criterion (positive or negative), 
                // use the GetDomainCategories operation with the Ad Insight service.

                OutputStatusMessage("-----\nGetDomainCategories:");
                var getDomainCategoriesResponse = await AdInsightExampleHelper.GetDomainCategoriesAsync(
                    categoryName: null,
                    domainName: DOMAIN_NAME,
                    language: LANGUAGE);
                var categories = getDomainCategoriesResponse.Categories;
                AdInsightExampleHelper.OutputArrayOfDomainCategory(categories);

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


                // Finally you must add at least one DynamicSearchAd into the ad group. The ad title and display URL 
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
                        // You cannot set FinalUrls for dynamic search ads. 
                        // The Final URL will be a dynamically selected landing page.
                        // The final URL is distinct from the path that customers will see and click on in your ad.
                        FinalUrls = null,
                    },
                };

                uploadEntities.Add(bulkDynamicSearchAd);
                
                // Upload and write the output

                OutputStatusMessage("-----\nAdding campaign, ad group, criterions, and ads...");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

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

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                OutputStatusMessage("-----\nDeleting DSA campaign, criterions, and ad...");

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
