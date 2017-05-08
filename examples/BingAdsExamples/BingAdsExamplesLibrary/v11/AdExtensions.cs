using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.CampaignManagement;
using Microsoft.BingAds.V11.CustomerManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to add, get, and delete extensions for an account’s ad extension library, 
    /// set, get, and delete the extension associations with a campaign, and determine why an extension failed 
    /// editorial review.
    /// 
    /// This example also demonstrates how to determine whether your account supports multiple sitelinks
    /// per ad extension or a single sitelink per ad extension. At the end of Q3 calendar year 2017, Bing Ads 
    /// will migrate all SiteLinksAdExtension objects (contains multiple sitelinks per ad extension) 
    /// to Sitelink2AdExtension objects (contains one sitelink per ad extension). 
    /// You must be prepared for migration to sitelink2 ad extensions by September 30th.
    /// </summary>
    public class AdExtensions : ExampleBase
    {
        private const string SITELINK_MIGRATION = "SiteLinkAdExtension";

        public override string Description
        {
            get { return "Ad Extensions | Campaign Management V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);
                CustomerService = new ServiceClient<ICustomerManagementService>(authorizationData);

                #region MigrationStatus

                // To prepare for the sitelink ad extensions migration in 2017, you will need to determine
                // whether the account has been migrated from SiteLinksAdExtension to Sitelink2AdExtension. 
                // All ad extension service operations available for both types of sitelinks; however you will 
                // need to determine which type to add, update, and retrieve.

                bool sitelinkMigrationIsCompleted = false;

                // Optionally you can find out which pilot features the customer is able to use. Even if the customer 
                // is in pilot for sitelink migrations, the accounts that it contains might not be migrated.
                var featurePilotFlags = (await GetCustomerPilotFeaturesAsync(authorizationData.CustomerId))?.FeaturePilotFlags.ToArray();
                
                // The pilot flag value for Sitelink ad extension migration is 253.
                // Pilot flags apply to all accounts within a given customer; however,
                // each account goes through migration individually and has its own migration status.
                if (featurePilotFlags.Any(pilotFlag => pilotFlag == 253))
                {
                    // Account migration status below will be either NotStarted, InProgress, or Completed.
                    OutputStatusMessage("Customer is in pilot for Sitelink migration.\n");
                }
                else
                {
                    // Account migration status below will be NotInPilot.
                    OutputStatusMessage("Customer is not in pilot for Sitelink migration.\n");
                }

                // Even if you have multiple accounts per customer, each account will have its own
                // migration status. This example checks one account using the provided AuthorizationData.
                var accountMigrationStatusesInfos = (await GetAccountMigrationStatusesAsync(
                    new long[] { authorizationData.AccountId },
                    SITELINK_MIGRATION
                ))?.MigrationStatuses.ToArray();

                foreach (var accountMigrationStatusesInfo in accountMigrationStatusesInfos)
                {
                    OutputAccountMigrationStatusesInfo(accountMigrationStatusesInfo);

                    if (accountMigrationStatusesInfo.MigrationStatusInfo.Any(
                        statusInfo =>
                        statusInfo.Status == MigrationStatus.Completed && SITELINK_MIGRATION.CompareTo(statusInfo.MigrationType) == 0))
                    {
                        sitelinkMigrationIsCompleted = true;
                    }
                }

                #endregion MigrationStatus

                // Add a campaign that will later be associated with ad extensions. 

                var campaigns = new[] {
                    new Campaign
                    {
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",

                        // You must choose to set either the shared  budget ID or daily amount.
                        // You can set one or the other, but you may not set both.
                        BudgetId = null,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        BiddingScheme = new EnhancedCpcBiddingScheme(),

                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // Used with FinalUrls shown in the sitelinks that we will add below.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    }
                };

                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                OutputIds(campaignIds);
                OutputPartialErrors(addCampaignsResponse?.PartialErrors);

                // Specify the extensions.

                var adExtensions = new AdExtension[] {
                    //new AppAdExtension
                    //{
                    //    AppPlatform = "Windows",
                    //    AppStoreId = "AppStoreIdGoesHere",
                    //    DestinationUrl = "DestinationUrlGoesHere",
                    //    DisplayText = "Contoso",
                    //},
                    new CallAdExtension {
                        CountryCode = "US",
                        PhoneNumber = "2065550100",
                        IsCallOnly = false,
                        Scheduling = new Schedule {

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
                            EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    },
                    new CalloutAdExtension
                    {
                        Text = "Callout Text"
                    },
                    //new ImageAdExtension
                    //{
                    //    AlternativeText = "Image Extension Alt Text",
                    //    ImageMediaIds = new long[] { (await AddMediaAsync(GetImageMedia())).MediaIds[0] }
                    //},
                    new LocationAdExtension {
                        PhoneNumber = "206-555-0100",
                        CompanyName = "Contoso Shoes",
                        IconMediaId = null,
                        ImageMediaId = null,
                        Address = new Microsoft.BingAds.V11.CampaignManagement.Address {
                            StreetAddress = "1234 Washington Place",
                            StreetAddress2 = "Suite 1210",
                            CityName = "Woodinville",
                            ProvinceName = "WA",
                            CountryCode = "US",
                            PostalCode = "98608"
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
                            EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    },
                    new ReviewAdExtension
                    {
                        IsExact = true,
                        Source = "Review Source Name",
                        Text = "Review Text",
                        Url = "http://review.contoso.com" // The Url of the third-party review. This is not your business Url.
                    },
                    new StructuredSnippetAdExtension
                    {
                        Header = "Brands",
                        Values = new [] { "Windows", "Xbox", "Skype"}
                    }
                };

                // Before migration only the deprecated SiteLinksAdExtension type can be added, 
                // and after migration only the new Sitelink2AdExtension type can be added.
                adExtensions = adExtensions.Concat(sitelinkMigrationIsCompleted ? (AdExtension[])
                    GetSampleSitelink2AdExtensions() : GetSampleSiteLinksAdExtensions()).ToArray();


                // Add all extensions to the account's ad extension library
                var addAdExtensionsResponse = (await AddAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensions
                ));
                var adExtensionIdentities = addAdExtensionsResponse?.AdExtensionIdentities;
                OutputBatchErrorCollections(addAdExtensionsResponse?.NestedPartialErrors);

                OutputStatusMessage("Added ad extensions.\n");

                // DeleteAdExtensionsAssociations, SetAdExtensionsAssociations, and GetAdExtensionsEditorialReasons 
                // operations each require a list of type AdExtensionIdToEntityIdAssociation.
                var adExtensionIdToEntityIdAssociations = new AdExtensionIdToEntityIdAssociation[adExtensionIdentities.Count];

                // GetAdExtensionsByIds requires a list of type long.
                var adExtensionIds = new long[adExtensionIdentities.Count];

                // Loop through the list of extension IDs and build any required data structures
                // for subsequent operations. 

                for (int i = 0; i < adExtensionIdentities.Count; i++)
                {
                    adExtensionIdToEntityIdAssociations[i] = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = adExtensionIdentities[i].Id,
                        EntityId = (long)campaignIds[0]
                    };

                    adExtensionIds[i] = adExtensionIdentities[i].Id;
                }

                // Associate the specified ad extensions with the respective campaigns or ad groups. 
                await SetAdExtensionsAssociationsAsync(
                    authorizationData.AccountId,
                    adExtensionIdToEntityIdAssociations,
                    AssociationType.Campaign
                );

                OutputStatusMessage("Set ad extension associations.\n");

                // Get editorial rejection reasons for the respective ad extension and entity associations.
                var getAdExtensionsEditorialReasonsResponse =
                    (await GetAdExtensionsEditorialReasonsAsync(
                        authorizationData.AccountId,
                        adExtensionIdToEntityIdAssociations,
                        AssociationType.Campaign
                    ));
                var adExtensionEditorialReasonCollection =
                    (AdExtensionEditorialReasonCollection[])getAdExtensionsEditorialReasonsResponse?.EditorialReasons;
                OutputPartialErrors(getAdExtensionsEditorialReasonsResponse?.PartialErrors);

                // If migration has been completed, then you should request the Sitelink2AdExtension objects.
                // You can always request both types; however, before migration only the deprecated SiteLinksAdExtension
                // type will be returned, and after migration only the new Sitelink2AdExtension type will be returned.
                AdExtensionsTypeFilter adExtensionsTypeFilter = (sitelinkMigrationIsCompleted ?
                    AdExtensionsTypeFilter.Sitelink2AdExtension : AdExtensionsTypeFilter.SiteLinksAdExtension) |
                    AdExtensionsTypeFilter.AppAdExtension |
                    AdExtensionsTypeFilter.CallAdExtension |
                    AdExtensionsTypeFilter.CalloutAdExtension |
                    AdExtensionsTypeFilter.ImageAdExtension |
                    AdExtensionsTypeFilter.LocationAdExtension |
                    AdExtensionsTypeFilter.ReviewAdExtension |
                    AdExtensionsTypeFilter.StructuredSnippetAdExtension;

                // Get all ad extensions added above.
                var getAdExtensionsByIdsResponse = (await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    adExtensionIds,
                    adExtensionsTypeFilter
                ));
                adExtensions = getAdExtensionsByIdsResponse?.AdExtensions.ToArray();
                OutputPartialErrors(getAdExtensionsByIdsResponse?.PartialErrors);

                OutputStatusMessage("List of ad extensions that were added above:\n");
                OutputAdExtensionsWithEditorialReasons(adExtensions, adExtensionEditorialReasonCollection);

                // Get only the location extensions and remove scheduling.

                adExtensionsTypeFilter = AdExtensionsTypeFilter.LocationAdExtension;

                getAdExtensionsByIdsResponse = (await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    adExtensionIds,
                    adExtensionsTypeFilter
                ));
                adExtensions = getAdExtensionsByIdsResponse?.AdExtensions.ToArray();

                // In this example partial errors will be returned for indices where the ad extensions 
                // are not location ad extensions because we only requested AdExtensionsTypeFilter.LocationAdExtension.
                // This is an example, and ideally you would only send the required ad extension IDs.

                OutputPartialErrors(getAdExtensionsByIdsResponse?.PartialErrors);

                var updateExtensions = new List<AdExtension>();
                var updateExtensionIds = new List<long>();

                foreach (var extension in adExtensions)
                {
                    // GetAdExtensionsByIds will return a nil element if the request filters / conditions were not met.
                    if(extension != null && extension.Id != null)
                    {
                        // Remove read-only elements that would otherwise cause the update operation to fail.
                        var updateExtension = SetReadOnlyAdExtensionElementsToNull(extension);

                        // If you set the Scheduling element null, any existing scheduling set for the ad extension will remain unchanged. 
                        // If you set this to any non-null Schedule object, you are effectively replacing existing scheduling 
                        // for the ad extension. In this example, we will remove any existing scheduling by setting this element  
                        // to an empty Schedule object.
                        updateExtension.Scheduling = new Schedule { };

                        updateExtensions.Add(updateExtension);
                        updateExtensionIds.Add((long)updateExtension.Id);
                    }
                }

                OutputStatusMessage("Removing scheduling from the location ad extensions..\n");
                await UpdateAdExtensionsAsync(authorizationData.AccountId, updateExtensions);

                // Get only the location extensions to output the result.

                getAdExtensionsByIdsResponse = (await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    updateExtensionIds,
                    adExtensionsTypeFilter
                ));
                adExtensions = getAdExtensionsByIdsResponse?.AdExtensions.ToArray();
                OutputPartialErrors(getAdExtensionsByIdsResponse?.PartialErrors);

                OutputStatusMessage("List of ad extensions that were updated above:\n");
                OutputAdExtensionsWithEditorialReasons(adExtensions, null);

                // Delete the ad extension associations, ad extensions, and campaign, that were previously added.  
                // At this point the ad extensions are still available in the account's ad extensions library. 

                await DeleteAdExtensionsAssociationsAsync(
                    authorizationData.AccountId,
                    adExtensionIdToEntityIdAssociations,
                    AssociationType.Campaign
                );
                OutputStatusMessage("Deleted ad extension associations.\n");

                // Delete the ad extensions from the account’s ad extension library.

                await DeleteAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensionIds
                );
                OutputStatusMessage("Deleted ad extensions.\n");
                
                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}\n", (long)campaignIds[0]));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.V11.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        private IList<Media> GetImageMedia()
        {
            var media = new List<Media>();
            var image = new Image();

            // This example uses an image with 1.5:1 aspect ratio.
            // For more information about available aspect ratios and min / max dimensions,
            // see the Image data object reference documentation on MSDN.

            image.Data = GetImage15x10Data();
            image.Type = "Image15x10";
            image.MediaType = "Image";
            media.Add(image);

            var request = new AddMediaRequest
            {
                Media = media
            };

            return media;
        }
        
        public string GetImage15x10Data()
        {
            var png = new System.Drawing.Bitmap("blankimageadextension.png");
            using (MemoryStream ms = new MemoryStream())
            {
                png.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        
        
        
        

        // Gets an example SiteLinksAdExtension object. You can use this type of ad extension if your account
        // has not yet been migrated to Sitelink2AdExtension.
        private SiteLinksAdExtension[] GetSampleSiteLinksAdExtensions()
        {
            return new[] {
                new SiteLinksAdExtension
                {
                    SiteLinks = new[] {
                        new SiteLink
                        {
                            Description1 = "Simple & Transparent.",
                            Description2 = "No Upfront Cost.",
                            DisplayText = "Women's Shoe Sale 1",

                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls, you can set DestinationUrl
                            // to an empty string when updating the sitelink. If you are removing DestinationUrl,
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

                            // Set custom parameters that are specific to this sitelink, 
                            // and can be used by the sitelink, ad group, campaign, or account level tracking template. 
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
                            },
                        },
                        new SiteLink
                        {
                            Description1 = "Do Amazing Things With Contoso.",
                            Description2 = "Read Our Case Studies.",
                            DisplayText = "Women's Shoe Sale 2",

                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                            // To migrate from DestinationUrl to FinalUrls, you can set DestinationUrl
                            // to an empty string when updating the sitelink. If you are removing DestinationUrl,
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

                            // Set custom parameters that are specific to this sitelink, 
                            // and can be used by the sitelink, ad group, campaign, or account level tracking template. 
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
                        }
                    }
                }
            };
        }

        // Gets an example Sitelink2AdExtension object. You can use this type of ad extension if your account
        // has not yet been migrated to Sitelink2AdExtension.
        private Sitelink2AdExtension[] GetSampleSitelink2AdExtensions()
        {
            return new[] {
                new Sitelink2AdExtension {
                    Description1 = "Simple & Transparent.",
                    Description2 = "No Upfront Cost.",
                    DisplayText = "Women's Shoe Sale 1",

                    // If you are currently using Destination URLs, you must replace them with Final URLs. 
                    // Here is an example of a DestinationUrl you might have used previously. 
                    // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                    // To migrate from DestinationUrl to FinalUrls, you can set DestinationUrl
                    // to an empty string when updating the ad extension. If you are removing DestinationUrl,
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
                new Sitelink2AdExtension
                {
                    Description1 = "Do Amazing Things With Contoso.",
                    Description2 = "Read Our Case Studies.",
                    DisplayText = "Women's Shoe Sale 2",

                    // If you are currently using Destination URLs, you must replace them with Final URLs. 
                    // Here is an example of a DestinationUrl you might have used previously. 
                    // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                    // To migrate from DestinationUrl to FinalUrls, you can set DestinationUrl
                    // to an empty string when updating the ad extension. If you are removing DestinationUrl,
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
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date {
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
            };
        }
    }
}
