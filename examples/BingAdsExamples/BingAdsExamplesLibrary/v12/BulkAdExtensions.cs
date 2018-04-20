using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.Bulk;
using Microsoft.BingAds.V12.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;

using Microsoft.BingAds.V12.Internal.Bulk.Entities;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to add and update ad extensions using the BulkServiceManager class.
    /// </summary>
    public class BulkAdExtensions : BulkExampleBase
    {
        public override string Description
        {
            get { return "AdExtensions | Bulk V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                // Used to output the Campaign Management objects within Bulk entities.
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                OutputStatusMessage(string.Format("{0} % Complete", x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                #region Add

                // Prepare the bulk entities that you want to upload. Each bulk entity contains the corresponding campaign management object, 
                // and additional elements needed to read from and write to a bulk file. 

                var uploadEntities = new List<BulkEntity>();

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
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        BiddingScheme = new EnhancedCpcBiddingScheme(),

                        TimeZone = "PacificTimeUSCanadaTijuana",
                        Status = CampaignStatus.Paused,

                        // Used with FinalUrls shown in the sitelinks that we will add below.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    }
                };

                // Prepare ad extensions for upload

                var bulkAppAdExtension = new BulkAppAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    AppAdExtension = new AppAdExtension
                    {
                        AppPlatform = "Windows",
                        AppStoreId = "AppStoreIdGoesHere",
                        DestinationUrl = "DestinationUrlGoesHere",
                        DisplayText = "Contoso",
                        Id = appAdExtensionIdKey,
                    }
                };

                var bulkCallAdExtension = new BulkCallAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    CallAdExtension = new CallAdExtension
                    {
                        CountryCode = "US",
                        PhoneNumber = "2065550100",
                        IsCallOnly = false,
                        Id = callAdExtensionIdKey,
                        Scheduling = new Schedule
                        {

                            // For this example assume the call center is open Monday - Friday from 9am - 9pm
                            // in the account's time zone.

                            UseSearcherTimeZone = false,
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
                                new DayTime
                                {
                                    Day = Day.Tuesday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Wednesday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Thursday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Friday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                            },
                            StartDate = null,
                            EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date
                            {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    }
                };

                var bulkCalloutAdExtension = new BulkCalloutAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    CalloutAdExtension = new CalloutAdExtension
                    {
                        Text = "Callout Text",
                        Id = calloutAdExtensionIdKey
                    }
                };

                var bulkLocationAdExtension = new BulkLocationAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    LocationAdExtension = new LocationAdExtension
                    {
                        Id = locationAdExtensionIdKey,
                        PhoneNumber = "206-555-0100",
                        CompanyName = "Contoso Shoes",
                        Address = new Microsoft.BingAds.V12.CampaignManagement.Address
                        {
                            StreetAddress = "1234 Washington Place",
                            StreetAddress2 = "Suite 1210",
                            CityName = "Woodinville",
                            ProvinceName = "WA",
                            CountryCode = "US",
                            PostalCode = "98608"
                        },
                        Scheduling = new Schedule
                        {

                            // For this example assume you want to drive traffic every Saturday morning
                            // in the search user's time zone.

                            UseSearcherTimeZone = true,
                            DayTimeRanges = new[]
                            {
                                new DayTime
                                {
                                    Day = Day.Saturday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 12,
                                    EndMinute = Minute.Zero,
                                },
                            },
                            StartDate = null,
                            EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date
                            {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    }
                };

                var bulkPriceAdExtension = new BulkPriceAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    PriceAdExtension = new PriceAdExtension
                    {
                        Id = priceAdExtensionIdKey,
                        Language = "English",
                        TableRows = new PriceTableRow[]
                        {
                            new PriceTableRow
                            {
                                CurrencyCode = "USD",
                                Description = "Come to the event",
                                FinalUrls = new string[]
                                {
                                    "https://contoso.com"
                                },
                                Header = "New Event",
                                Price = 9.99,
                                PriceQualifier = PriceQualifier.From,
                                PriceUnit = PriceUnit.PerDay,
                            },
                            new PriceTableRow
                            {
                                CurrencyCode = "USD",
                                Description = "Come to the next event",
                                FinalUrls = new string[]
                                {
                                    "https://contoso.com"
                                },
                                Header = "Next Event",
                                Price = 9.99,
                                PriceQualifier = PriceQualifier.From,
                                PriceUnit = PriceUnit.PerDay,
                            },
                            new PriceTableRow
                            {
                                CurrencyCode = "USD",
                                Description = "Come to the final event",
                                FinalUrls = new string[]
                                {
                                    "https://contoso.com"
                                },
                                Header = "Final Event",
                                Price = 9.99,
                                PriceQualifier = PriceQualifier.From,
                                PriceUnit = PriceUnit.PerDay,
                            },
                        },
                        PriceExtensionType = PriceExtensionType.Events,
                        TrackingUrlTemplate = "http://tracker.com?url={lpurl}&matchtype={matchtype}",
                        UrlCustomParameters = new CustomParameters
                        {
                            // Each custom parameter is delimited by a semicolon (;) in the Bulk file
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
                        },
                    }
                };

                var bulkReviewAdExtension = new BulkReviewAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    ReviewAdExtension = new ReviewAdExtension
                    {
                        IsExact = true,
                        Source = "Review Source Name",
                        Text = "Review Text",
                        Url = "http://review.contoso.com", // The Url of the third-party review. This is not your business Url.
                        Id = reviewAdExtensionIdKey
                    }
                };

                var bulkStructuredSnippetAdExtension = new BulkStructuredSnippetAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    StructuredSnippetAdExtension = new StructuredSnippetAdExtension
                    {
                        Header = "Brands",
                        Values = new[] { "Windows", "Xbox", "Skype" },
                        Id = structuredSnippetAdExtensionIdKey
                    }
                };

                // Prepare ad extension associations for upload

                var bulkCampaignAppAdExtension = new BulkCampaignAppAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = appAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignCallAdExtension = new BulkCampaignCallAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = callAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignCalloutAdExtension = new BulkCampaignCalloutAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = calloutAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignLocationAdExtension = new BulkCampaignLocationAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = locationAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignPriceAdExtension = new BulkCampaignPriceAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = priceAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignReviewAdExtension = new BulkCampaignReviewAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = reviewAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignStructuredSnippetAdExtension = new BulkCampaignStructuredSnippetAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = structuredSnippetAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };


                // Upload the entities created above.
                // Dependent entities such as BulkCampaignCallAdExtension must be written after any dependencies,  
                // for example the BulkCampaign and BulkCallAdExtension. 

                uploadEntities.Add(bulkCampaign);

                uploadEntities.Add(bulkAppAdExtension);
                uploadEntities.Add(bulkCallAdExtension);
                uploadEntities.Add(bulkCalloutAdExtension);
                uploadEntities.Add(bulkLocationAdExtension);
                uploadEntities.Add(bulkPriceAdExtension);
                uploadEntities.Add(bulkReviewAdExtension);
                uploadEntities.Add(bulkStructuredSnippetAdExtension);

                uploadEntities.Add(bulkCampaignAppAdExtension);
                uploadEntities.Add(bulkCampaignCallAdExtension);
                uploadEntities.Add(bulkCampaignCalloutAdExtension);
                uploadEntities.Add(bulkCampaignLocationAdExtension);
                uploadEntities.Add(bulkCampaignPriceAdExtension);
                uploadEntities.Add(bulkCampaignReviewAdExtension);
                uploadEntities.Add(bulkCampaignStructuredSnippetAdExtension);

                var bulkSLExtensions = GetSampleBulkSitelinkAdExtensions(authorizationData.AccountId).ToArray();

                foreach (var bulkSLExtension in bulkSLExtensions)
                {
                    uploadEntities.Add(bulkSLExtension);
                }

                OutputStatusMessage("Adding campaign, ad extensions, and associations . . .");

                // Upload and write the output

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var appAdExtensionResults = downloadEntities.OfType<BulkAppAdExtension>().ToList();
                OutputBulkAppAdExtensions(appAdExtensionResults);

                var callAdExtensionResults = downloadEntities.OfType<BulkCallAdExtension>().ToList();
                OutputBulkCallAdExtensions(callAdExtensionResults);

                var calloutAdExtensionResults = downloadEntities.OfType<BulkCalloutAdExtension>().ToList();
                OutputBulkCalloutAdExtensions(calloutAdExtensionResults);

                var imageAdExtensionResults = downloadEntities.OfType<BulkImageAdExtension>().ToList();
                OutputBulkImageAdExtensions(imageAdExtensionResults);

                var locationAdExtensionResults = downloadEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                var priceAdExtensionResults = downloadEntities.OfType<BulkPriceAdExtension>().ToList();
                OutputBulkPriceAdExtensions(priceAdExtensionResults);

                var reviewAdExtensionResults = downloadEntities.OfType<BulkReviewAdExtension>().ToList();
                OutputBulkReviewAdExtensions(reviewAdExtensionResults);

                var structuredSnippetAdExtensionResults = downloadEntities.OfType<BulkStructuredSnippetAdExtension>().ToList();
                OutputBulkStructuredSnippetAdExtensions(structuredSnippetAdExtensionResults);

                var sitelinkAdExtensionResults = downloadEntities.OfType<BulkSitelinkAdExtension>().ToList();
                OutputBulkSitelinkAdExtensions(sitelinkAdExtensionResults);

                OutputBulkCampaignAdExtensionAssociations(downloadEntities.OfType<BulkCampaignAdExtensionAssociation>().ToList());

                Reader.Dispose();

                #endregion Add

                #region Update

                // Use only the location extension results and remove scheduling.

                uploadEntities = new List<BulkEntity>();

                foreach (var locationAdExtensionResult in locationAdExtensionResults)
                {
                    if (locationAdExtensionResult.LocationAdExtension.Id > 0)
                    {
                        // If you set the Scheduling element null, any existing scheduling set for the ad extension will remain unchanged. 
                        // If you set this to any non-null Schedule object, you are effectively replacing existing scheduling 
                        // for the ad extension. In this example, we will remove any existing scheduling by setting this element  
                        // to an empty Schedule object.
                        // The "delete_value" keyword will be written to the corresponding columns in the bulk file.
                        locationAdExtensionResult.LocationAdExtension.Scheduling = new Schedule();
                        uploadEntities.Add(locationAdExtensionResult);
                    }
                }

                OutputStatusMessage("\nRemoving scheduling from location ad extensions . . .\n");

                // Upload and write the output

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                locationAdExtensionResults = downloadEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                Reader.Dispose();

                #endregion Update

                #region CleanUp

                // Delete the campaign and ad extensions that were previously added.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    if (campaignResult.Campaign != null)
                    {
                        campaignResult.Campaign.Status = CampaignStatus.Deleted;
                        uploadEntities.Add(campaignResult);
                    }
                }

                foreach (var appAdExtensionResult in appAdExtensionResults)
                {
                    //By default the sample does not successfully create any app ad extensions,
                    //because you need to provide details above such as the AppStoreId.
                    if (appAdExtensionResult.AppAdExtension.Id > 0)
                    {
                        appAdExtensionResult.AppAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(appAdExtensionResult);
                    }
                }

                foreach (var callAdExtensionResult in callAdExtensionResults)
                {
                    if (callAdExtensionResult.CallAdExtension.Id > 0)
                    {
                        callAdExtensionResult.CallAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(callAdExtensionResult);
                    }
                }

                foreach (var calloutAdExtensionResult in calloutAdExtensionResults)
                {
                    if (calloutAdExtensionResult.CalloutAdExtension.Id > 0)
                    {
                        calloutAdExtensionResult.CalloutAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(calloutAdExtensionResult);
                    }
                }

                foreach (var imageAdExtensionResult in imageAdExtensionResults)
                {
                    if (imageAdExtensionResult.ImageAdExtension.Id > 0)
                    {
                        imageAdExtensionResult.ImageAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(imageAdExtensionResult);
                    }
                }

                foreach (var locationAdExtensionResult in locationAdExtensionResults)
                {
                    if (locationAdExtensionResult.LocationAdExtension.Id > 0)
                    {
                        locationAdExtensionResult.LocationAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(locationAdExtensionResult);
                    }
                }

                foreach (var priceAdExtensionResult in priceAdExtensionResults)
                {
                    if (priceAdExtensionResult.PriceAdExtension.Id > 0)
                    {
                        priceAdExtensionResult.PriceAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(priceAdExtensionResult);
                    }
                }

                foreach (var reviewAdExtensionResult in reviewAdExtensionResults)
                {
                    if (reviewAdExtensionResult.ReviewAdExtension.Id > 0)
                    {
                        reviewAdExtensionResult.ReviewAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(reviewAdExtensionResult);
                    }
                }

                foreach (var sitelinkAdExtensionResult in sitelinkAdExtensionResults)
                {
                    if (sitelinkAdExtensionResult.SitelinkAdExtension.Id > 0)
                    {
                        sitelinkAdExtensionResult.SitelinkAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(sitelinkAdExtensionResult);
                    }
                }

                foreach (var structuredSnippetAdExtensionResult in structuredSnippetAdExtensionResults)
                {
                    if (structuredSnippetAdExtensionResult.StructuredSnippetAdExtension.Id > 0)
                    {
                        structuredSnippetAdExtensionResult.StructuredSnippetAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(structuredSnippetAdExtensionResult);
                    }
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign and ad extensions . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkAppAdExtensions(downloadEntities.OfType<BulkAppAdExtension>().ToList());
                OutputBulkCallAdExtensions(downloadEntities.OfType<BulkCallAdExtension>().ToList());
                OutputBulkCalloutAdExtensions(downloadEntities.OfType<BulkCalloutAdExtension>().ToList());
                OutputBulkImageAdExtensions(downloadEntities.OfType<BulkImageAdExtension>().ToList());
                OutputBulkLocationAdExtensions(downloadEntities.OfType<BulkLocationAdExtension>().ToList());
                OutputBulkPriceAdExtensions(downloadEntities.OfType<BulkPriceAdExtension>().ToList());
                OutputBulkReviewAdExtensions(downloadEntities.OfType<BulkReviewAdExtension>().ToList());
                OutputBulkSitelinkAdExtensions(downloadEntities.OfType<BulkSitelinkAdExtension>().ToList());
                OutputBulkStructuredSnippetAdExtensions(downloadEntities.OfType<BulkStructuredSnippetAdExtension>().ToList());
                Reader.Dispose();

                #endregion Cleanup

            }
            #region CatchExceptions
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
            #endregion CatchExceptions 
        }

        // Gets example BulkSitelinkAdExtension and BulkCampaignSitelinkAdExtension objects. 
        private BulkEntity[] GetSampleBulkSitelinkAdExtensions(long accountId)
        {
            return new BulkEntity[] {
                new BulkSitelinkAdExtension
                {
                    AccountId = accountId,
                    SitelinkAdExtension = new SitelinkAdExtension {
                        Id = sitelinkAdExtensionIdKey,
                        Description1 = "Simple & Transparent.",
                        Description2 = "No Upfront Cost.",
                        DisplayText = "Women's Shoe Sale 1",

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

                        // Set custom parameters that are specific to this ad extension, 
                        // and can be used by the ad extension, ad group, campaign, or account level tracking template. 
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
                        },
                    },
                },
                new BulkSitelinkAdExtension
                {
                    AccountId = accountId,
                    SitelinkAdExtension = new SitelinkAdExtension
                    {
                        Id = sitelinkAdExtensionIdKey,
                        Description1 = "Do Amazing Things With Contoso.",
                        Description2 = "Read Our Case Studies.",
                        DisplayText = "Women's Shoe Sale 2",

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

                        Scheduling = new Schedule {

                            // For this example assume you want to drive traffic every Saturday morning
                            // in the search user's time zone.

                            UseSearcherTimeZone = true,
                            DayTimeRanges = new[]
                            {
                                new DayTime
                                {
                                    Day = Day.Saturday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 12,
                                    EndMinute = Minute.Zero,
                                },
                            },
                            StartDate = null,
                            EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        },

                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this ad extension, 
                        // and can be used by the ad extension, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
                        UrlCustomParameters = new CustomParameters
                        {
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
                    }
                },
                new BulkCampaignSitelinkAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = sitelinkAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                }
            };
        }
    }
}
