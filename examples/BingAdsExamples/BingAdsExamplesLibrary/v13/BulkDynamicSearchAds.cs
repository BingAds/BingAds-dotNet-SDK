using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.AdInsight;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.Bulk.Entities.Feeds;
using Microsoft.BingAds.V13.CampaignManagement;
using Newtonsoft.Json;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to setup Dynamic Search Ads (DSA) in a Search campaign with the Bulk service.
    /// </summary>
    public class BulkDynamicSearchAds : BulkExampleBase
    {
        public const string DOMAIN_NAME = "contoso.com";
        public const string LANGUAGE = "EN";
        public override string Description
        {
            get { return "Dynamic Search Ads (DSA) in Search campaigns | Bulk V13"; }
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

                // Setup a page feed that can be associated with one or more campaigns. 

                var bulkPageFeed = new BulkFeed
                {
                    CustomAttributes = new[]
                    {
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "Url",
                            Name = "Page Url"
                        },
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "StringList",
                            Name = "Custom Label"
                        }
                    },
                    Id = feedIdKey,
                    Name = "My PageFeed " + DateTime.UtcNow,
                    Status = Status.Active,
                    SubType = "PageFeed"
                };

                uploadEntities.Add(bulkPageFeed);
                                
                var pageFeedItemCustomAttributes = new Dictionary<string, object>();
                pageFeedItemCustomAttributes.Add(
                    "Page Url",
                    "https://" + DOMAIN_NAME + "/3001");
                pageFeedItemCustomAttributes.Add(
                    "Custom Label", new string[] {
                        "Label_1_3001",
                        "Label_1_3002"
                    });

                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                var pageFeedItemCustomAttributesJson = JsonConvert.SerializeObject(
                    pageFeedItemCustomAttributes, serializerSettings);

                var bulkPageFeedItem = new BulkFeedItem
                {
                    FeedId = feedIdKey,
                    CustomAttributes = pageFeedItemCustomAttributesJson,
                    Status = Status.Active
                };

                uploadEntities.Add(bulkPageFeedItem);


                // To get started with dynamic search ads, first you'll need to add a new Search campaign 
                // Include a DynamicSearchAdsSetting that specifies the target website domain and language.
                // Page feeds can be associated at the campaign level via 'Source' and 'Page Feed Ids'.

                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        BudgetType = Microsoft.BingAds.V13.CampaignManagement.BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        Settings = new[] {
                            // Set the target website domain and language.
                            // Be sure to set the Source to AdvertiserSuppliedUrls or All, 
                            // otherwise the PageFeedIds will be ignored. 
                            new DynamicSearchAdsSetting
                            {
                                DomainName = DOMAIN_NAME,
                                Language = LANGUAGE,
                                Source = DynamicSearchAdsSource.All,
                                PageFeedIds = new [] { feedIdKey }
                            }
                        },
                    },
                };

                uploadEntities.Add(bulkCampaign);

                // Create a new ad group with type set to "SearchDynamic"

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        AdGroupType = "SearchDynamic",
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

                // Create an auto target based on the custom label feed items created above e.g., "Label_1_3001".

                var adGroupWebpagePositiveCustomLabel = new BulkAdGroupDynamicSearchAdTarget
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
                                        Argument = "Label_1_3001",
                                        Operand = WebpageConditionOperand.CustomLabel,
                                    },
                                },
                                CriterionName = "Ad Group Webpage Positive Custom Label Criterion"
                            },
                        },
                    }
                };
                uploadEntities.Add(adGroupWebpagePositiveCustomLabel);
                
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
                                        Argument = "https://" + DOMAIN_NAME + "/3001",
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
                                
                // Finally you must add at least one Dynamic Search Ad into the ad group. The ad title and display URL 
                // are generated automatically based on the website domain and language that you want to target.

                var bulkDynamicSearchAd = new BulkDynamicSearchAd
                {
                    AdGroupId = adGroupIdKey,
                    DynamicSearchAd = new DynamicSearchAd
                    {
                        Text = "Find New Customers & Increase Sales!",
                        TextPart2 = "Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",
                        // You cannot set FinalUrls for dynamic search ads. 
                        // The Final URL will be a dynamically selected landing page.
                        // The final URL is distinct from the path that customers will see and click on in your ad.
                        FinalUrls = null
                    },
                };

                uploadEntities.Add(bulkDynamicSearchAd);
                
                // Upload and write the output

                OutputStatusMessage("-----\nAdding page feed, campaign, ad group, criterions, and ads...");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");
                
                var feedResults = downloadEntities.OfType<BulkFeed>().ToList();
                OutputBulkFeeds(feedResults);

                var feedItemResults = downloadEntities.OfType<BulkFeedItem>().ToList();
                OutputBulkFeedItems(feedItemResults);

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var adGroupDynamicSearchAdTargetResults = downloadEntities.OfType<BulkAdGroupDynamicSearchAdTarget>().ToList();
                OutputBulkAdGroupDynamicSearchAdTargets(adGroupDynamicSearchAdTargetResults);

                var adGroupNegativeDynamicSearchAdTargetResults = downloadEntities.OfType<BulkAdGroupNegativeDynamicSearchAdTarget>().ToList();
                OutputBulkAdGroupNegativeDynamicSearchAdTargets(adGroupNegativeDynamicSearchAdTargetResults);

                var dynamicSearchAdResults = downloadEntities.OfType<BulkDynamicSearchAd>().ToList();
                OutputBulkDynamicSearchAds(dynamicSearchAdResults);

                Reader.Dispose();

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                uploadEntities = new List<BulkEntity>();

                foreach (var feedResult in feedResults)
                {
                    feedResult.Status = Status.Deleted;
                    uploadEntities.Add(feedResult);
                }

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                OutputStatusMessage("-----\nDeleting page feed, DSA campaign, and all contained entities...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                feedResults = downloadEntities.OfType<BulkFeed>().ToList();
                OutputBulkFeeds(feedResults);

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
