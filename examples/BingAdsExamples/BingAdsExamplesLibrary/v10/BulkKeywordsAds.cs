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

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add ads and keywords to a new ad group using the BulkServiceManager class.
    /// </summary>
    public class BulkKeywordsAds : BulkExampleBase
    {
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

                // Prepare the bulk entities that you want to upload. Each bulk entity contains the corresponding campaign management object, 
                // and additional elements needed to read from and write to a bulk file. 

                var bulkAccount = new BulkAccount
                {
                    // We recommend at minimum adding tracking templates at the account level, 
                    // and then refining at lower levels e.g. campaign and ad group as needed.
                    TrackingUrlTemplate = "http://tracker.example.com/?season={_season}&u={lpurl}"
                };

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
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // DaylightSaving is not supported in the Bulk file schema. Whether or not you specify it in a BulkCampaign,
                        // the value is not written to the Bulk file, and by default DaylightSaving is set to true.
                        DaylightSaving = true,

                        // Used with FinalUrls shown in the text ads that we will add below.
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

                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,
                    },
                };

                // In this example only the second keyword should succeed. The Text of the first keyword exceeds the limit,
                // and the third keyword is a duplicate of the second keyword. 

                var bulkKeywords = new[] {
                    new BulkKeyword{
                        AdGroupId = adGroupIdKey,
                        Keyword = new Keyword
                        {
                            Bid = new Bid { Amount = 0.47 },
                            Param2 = "10% Off",
                            MatchType = MatchType.Broad,
                            Text = "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes " +
                                   "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes " +
                                   "Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes Brand-A Shoes"
                        },
                    },
                    new BulkKeyword{
                        AdGroupId = adGroupIdKey,
                        Keyword = new Keyword
                        {
                            Bid = new Bid { Amount = 0.47 },
                            Param2 = "10% Off",
                            MatchType = MatchType.Phrase,
                            Text = "Brand-A Shoes"
                        },
                    },
                    new BulkKeyword{
                        AdGroupId = adGroupIdKey,
                        Keyword = new Keyword
                        {
                            Bid = new Bid { Amount = 0.47 },
                            Param2 = "10% Off",
                            MatchType = MatchType.Phrase,
                            Text = "Brand-A Shoes"
                        },
                    },
                };

                // In this example only the first 3 ads should succeed. 
                // The Title of the fourth ad is empty and not valid,
                // and the fifth ad is a duplicate of the second ad. 

                var bulkTextAds = new[] {
                    new BulkTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        TextAd = new TextAd
                        {
                            Title = "Women's Shoe Sale",
                            Text = "Huge Savings on red shoes.",
                            DisplayUrl = "Contoso.com",
                        
                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                            // to an empty string when updating the ad. If you are removing DestinationUrl,
                            // then FinalUrls is required.
                            // DestinationUrl = "",

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
                    new BulkTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        TextAd = new TextAd
                        {
                            Title = "Women's Super Shoe Sale",
                            Text = "Huge Savings on red shoes.",
                            DisplayUrl = "Contoso.com",                       
                        
                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                            // to an empty string when updating the ad. If you are removing DestinationUrl,
                            // then FinalUrls is required.
                            // DestinationUrl = "",

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
                    new BulkTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        TextAd = new TextAd
                        {
                            Title = "Women's Red Shoe Sale",
                            Text = "Huge Savings on red shoes.",
                            DisplayUrl = "Contoso.com",

                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                            // to an empty string when updating the ad. If you are removing DestinationUrl,
                            // then FinalUrls is required.
                            // DestinationUrl = "",

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
                    new BulkTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        TextAd = new TextAd
                        {
                            Title = "",
                            Text = "Huge Savings on red shoes.",
                            DisplayUrl = "Contoso.com",                       
                        
                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                            // to an empty string when updating the ad. If you are removing DestinationUrl,
                            // then FinalUrls is required.
                            // DestinationUrl = "",

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
                    new BulkTextAd
                    {
                        AdGroupId = adGroupIdKey,
                        TextAd = new TextAd
                        {
                            Title = "Women's Super Shoe Sale",
                            Text = "Huge Savings on red shoes.",
                            DisplayUrl = "Contoso.com",                       
                        
                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls for existing ads, you can set DestinationUrl
                            // to an empty string when updating the ad. If you are removing DestinationUrl,
                            // then FinalUrls is required.
                            // DestinationUrl = "",

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

                uploadEntities.Add(bulkAccount);
                uploadEntities.Add(bulkCampaign);
                uploadEntities.Add(bulkAdGroup);

                foreach (var bulkKeyword in bulkKeywords)
                {
                    uploadEntities.Add(bulkKeyword);
                }

                foreach (var bulkTextAd in bulkTextAds)
                {
                    uploadEntities.Add(bulkTextAd);
                }

                // Upload and write the output

                OutputStatusMessage("Adding campaign, ad group, ads, and keywords...\n");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var accountResults = downloadEntities.OfType<BulkAccount>().ToList();
                OutputBulkAccount(accountResults[0]);

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var keywordResults = downloadEntities.OfType<BulkKeyword>().ToList();
                OutputBulkKeywords(keywordResults);

                var textAdResults = downloadEntities.OfType<BulkTextAd>().ToList();
                OutputBulkTextAds(textAdResults);

                Reader.Dispose();

                #endregion Add

                #region CleanUp

                //Delete the campaign, ad group, ads, and keywords that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                //When you delete a BulkCampaign, the dependent entities such as BulkAdGroup, BulkKeyword, and BulkTextAd 
                //are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign, ad group, keywords, and ads . . .\n");

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


    }
}
