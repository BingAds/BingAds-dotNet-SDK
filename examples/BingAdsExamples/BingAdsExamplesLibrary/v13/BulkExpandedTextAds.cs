using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.Bulk.Entities.Feeds;
using Microsoft.BingAds.V13.CampaignManagement;
using Newtonsoft.Json;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create Expanded Text Ads with the Bulk service.
    /// </summary>
    public class BulkExpandedTextAds : BulkExampleBase
    {
        public override string Description
        {
            get { return "Expanded Text Ads | Bulk V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                // Used to output the Campaign Management objects within Bulk entities.
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var uploadEntities = new List<BulkEntity>();

                // Setup an ad customizer feed that can be referenced later in the ad copy. 
                
                var bulkAdCustomizerFeed = new BulkFeed
                {
                    CustomAttributes = new[]
                    {
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "String",
                            Name = "Product"
                        },
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "String",
                            Name = "Materials_Lightweight"
                        },
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "String",
                            Name = "Description_Lightweight"
                        },
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "Int64",
                            Name = "Finishes"
                        },
                        new FeedCustomAttributeContract
                        {
                            FeedAttributeType = "Price",
                            Name = "StartPrice"
                        },
                    },
                    Id = feedIdKey,
                    Name = "My AdCustomizerFeed " + DateTime.UtcNow,                    
                    Status = Status.Active,
                    SubType = "AdCustomizerFeed",
                };

                uploadEntities.Add(bulkAdCustomizerFeed);

                var adCustomizerFeedItemCustomAttributes = new Dictionary<string, object>();
                adCustomizerFeedItemCustomAttributes.Add("Product", "Contoso 900");
                adCustomizerFeedItemCustomAttributes.Add("Materials_Lightweight", "titanium or acetate");
                adCustomizerFeedItemCustomAttributes.Add("Description_Lightweight", "Stylish, lightweight shades");
                adCustomizerFeedItemCustomAttributes.Add("Finishes", 8);
                adCustomizerFeedItemCustomAttributes.Add("StartPrice", "$24.99");

                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                var adCustomizerFeedItemCustomAttributesJson = JsonConvert.SerializeObject(
                    adCustomizerFeedItemCustomAttributes, serializerSettings);

                var bulkAdCustomizerFeedItem = new BulkFeedItem
                {
                    FeedId = feedIdKey,
                    CustomAttributes = adCustomizerFeedItemCustomAttributesJson,
                    Id = null,
                    AdGroupName = null,
                    AudienceId = null,
                    CampaignName = null,
                    DayTimeRanges = new[]
                    {
                        new DayTime
                        {
                            Day = Day.Monday,
                            StartHour = 9,
                            StartMinute = Minute.Zero,
                            EndHour = 21,
                            EndMinute = Minute.Zero,
                        },
                    },
                    EndDate = null,
                    StartDate = DateTime.UtcNow,
                    IntentOption = IntentOption.PeopleIn,
                    Keyword = "lightweight sunglasses",
                    LocationId = 190,
                    MatchType = MatchType.Broad,
                    DevicePreference = null,
                    Status = Status.Active
                };

                uploadEntities.Add(bulkAdCustomizerFeedItem);

                // Add a search campaign.

                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
                        Id = campaignIdKey,
                        Languages = new string[] { "All" },
                        Name = "Summer Sunglasses " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
                };
                uploadEntities.Add(bulkCampaign);

                // Add an ad group within the campaign.

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Sunglasses Sale",
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

                // Add keywords and ads within the ad group.

                var bulkKeyword = new BulkKeyword{
                    AdGroupId = adGroupIdKey,
                    Keyword = new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Sunglasses",
                    },                    
                };
                uploadEntities.Add(bulkKeyword);

                var bulkExpandedTextAd = new BulkExpandedTextAd
                {
                    AdGroupId = adGroupIdKey,
                    ExpandedTextAd = new ExpandedTextAd
                    {
                        TitlePart1 = "The latest {=Sunglasses.Product}s",
                        TitlePart2 = "In {=Sunglasses.Materials_Lightweight}",
                        TitlePart3 = null,
                        Text = "{=Sunglasses.Description_Lightweight} in {=Sunglasses.Finishes} finishes.",
                        TextPart2 = "Starting at only {=Sunglasses.StartPrice}!",
                        Path1 = "deals",
                        Path2 = null,
                        FinalUrls = new[] {
                            "https://www.contoso.com"
                        },
                    },
                };
                uploadEntities.Add(bulkExpandedTextAd);

                // Upload and write the output

                OutputStatusMessage("-----\nAdding the ad customizer feed, campaign, ad group, keyword, and ad...");

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

                var keywordResults = downloadEntities.OfType<BulkKeyword>().ToList();
                OutputBulkKeywords(keywordResults);

                var expandedTextAdResults = downloadEntities.OfType<BulkExpandedTextAd>().ToList();
                OutputBulkExpandedTextAds(expandedTextAdResults);

                Reader.Dispose();

                // Delete the feed and campaign and everything it contains e.g., ad groups and ads.

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
                
                // Upload and write the output

                OutputStatusMessage("-----\nDeleting the feed and campaign and everything it contains e.g., ad groups and ads...");

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
