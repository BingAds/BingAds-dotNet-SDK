using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.CustomerManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add, get, and delete extensions for an account’s ad extension library, 
    /// set, get, and delete the extension associations with a campaign, and determine why an extension failed 
    /// editorial review.
    /// 
    /// This example also demonstrates how to determine whether your account supports multiple sitelinks
    /// per ad extension or a single sitelink per ad extension. During calendar year 2017, Bing Ads 
    /// will migrate all SiteLinksAdExtension objects (contains multiple sitelinks per ad extension) 
    /// to Sitelink2AdExtension objects (contains one sitelink per ad extension). 
    /// </summary>
    public class AdExtensions : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> CampaignService;
        public static ServiceClient<ICustomerManagementService> CustomerService;

        private const string SITELINK_MIGRATION = "SiteLinkAdExtension";

        public override string Description
        {
            get { return "Ad Extensions | Campaign Management V10"; }
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
                var featurePilotFlags = await GetCustomerPilotFeaturesAsync(authorizationData.CustomerId);

                // The pilot flag value for Sitelink ad extension migration is 253.
                // Pilot flags apply to all accounts within a given customer; however,
                // each account goes through migration individually and has its own migration status.
                if (featurePilotFlags.SingleOrDefault(pilotFlag => pilotFlag == 253) > 0)
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
                )).ToArray();

                foreach (var accountMigrationStatusesInfo in accountMigrationStatusesInfos)
                {
                    OutputAccountMigrationStatusesInfo(accountMigrationStatusesInfo);

                    if (accountMigrationStatusesInfo.MigrationStatusInfo.SingleOrDefault(
                        statusInfo =>
                        statusInfo.Status == MigrationStatus.Completed && SITELINK_MIGRATION.CompareTo(statusInfo.MigrationType) == 0)
                        != null)
                    {
                        sitelinkMigrationIsCompleted = true;
                    }
                }

                #endregion MigrationStatus

                // Add a campaign that will later be associated with ad extensions. 

                var campaigns = new[] {
                    new Campaign
                    {
                        Id = null,
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        DaylightSaving = true,

                        // Used with FinalUrls shown in the sitelinks that we will add below.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    }
                };

                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();

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
                            EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date {
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
                    //    ImageMediaIds = new long[] { await AddImageAsync(authorizationData) }
                    //},
                    new LocationAdExtension {
                        PhoneNumber = "206-555-0100",
                        CompanyName = "Contoso Shoes",
                        IconMediaId = null,
                        ImageMediaId = null,
                        Address = new Microsoft.BingAds.V10.CampaignManagement.Address {
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
                            EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date {
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
                var adExtensionIdentities = await AddAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensions
                );

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
                var adExtensionEditorialReasonCollection =
                    (AdExtensionEditorialReasonCollection[])await GetAdExtensionsEditorialReasons(
                        authorizationData.AccountId,
                        adExtensionIdToEntityIdAssociations,
                        AssociationType.Campaign
                    );

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
                adExtensions = (AdExtension[]) await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    adExtensionIds,
                    adExtensionsTypeFilter,
                    AdExtensionAdditionalField.Scheduling
                );

                OutputStatusMessage("List of ad extensions that were added above:\n");
                OutputAdExtensionsWithEditorialReasons(adExtensions, adExtensionEditorialReasonCollection);

                // Get only the location extensions and remove scheduling.

                adExtensionsTypeFilter = AdExtensionsTypeFilter.LocationAdExtension;

                adExtensions = (AdExtension[])await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    adExtensionIds,
                    adExtensionsTypeFilter,
                    AdExtensionAdditionalField.Scheduling
                    );

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

                // Get only the location extension to output the result.
                adExtensions = (AdExtension[])await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    updateExtensionIds,
                    adExtensionsTypeFilter,
                    AdExtensionAdditionalField.Scheduling
                );

                OutputStatusMessage("List of ad extensions that were updated above:\n");
                OutputAdExtensionsWithEditorialReasons(adExtensions, null);

                // Delete the ad extension associations, ad extensions, and campaign, that were previously added. 
                // You should remove these lines if you want to view the added entities in the 
                // Bing Ads web application or another tool.

                // Remove the specified associations from the respective campaigns or ad groups. 
                // At this point the ad extensions are still available in the account's ad extensions library. 
                await DeleteAdExtensionsAssociationsAsync(
                    authorizationData.AccountId,
                    adExtensionIdToEntityIdAssociations,
                    AssociationType.Campaign
                );
                OutputStatusMessage("Deleted ad extension associations.\n");

                // Deletes the ad extensions from the account’s ad extension library.
                await DeleteAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensionIds
                );
                OutputStatusMessage("Deleted ad extensions.\n");
                
                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)campaignIds[0] });
                OutputStatusMessage(String.Format("Deleted CampaignId {0}\n", (long)campaignIds[0]));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        // Adds one or more campaigns to the specified account.

        private async Task<AddCampaignsResponse> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await CampaignService.CallAsync((s, r) => s.AddCampaignsAsync(r), request));
        }

        // Deletes one or more campaigns from the specified account.
        private async Task DeleteCampaignsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            await CampaignService.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        private async Task<long> AddImageAsync(AuthorizationData authorizationData)
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

            var Service = new ServiceClient<ICampaignManagementService>(authorizationData);
            return (await Service.CallAsync((s, r) => s.AddMediaAsync(r), request)).MediaIds[0];
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

        // Adds one or more ad extensions to the account's ad extension library.

        private async Task<IList<AdExtensionIdentity>> AddAdExtensionsAsync(long accountId, IList<AdExtension> adExtensions)
        {
            var request = new AddAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdExtensionsAsync(r), request)).AdExtensionIdentities;
        }

        // Deletes one or more ad extensions from the account’s ad extension library.

        private async Task DeleteAdExtensionsAsync(long accountId, IList<long> adExtensionIds)
        {
            var request = new DeleteAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds
            };

            await CampaignService.CallAsync((s, r) => s.DeleteAdExtensionsAsync(r), request);
        }

        // Updates one or more ad extensions within the account's ad extension library.

        private async Task<UpdateAdExtensionsResponse> UpdateAdExtensionsAsync(long accountId, IList<AdExtension> adExtensions)
        {
            var request = new UpdateAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdExtensionsAsync(r), request));
        }


        // Associates one or more extensions with the corresponding campaign or ad group entities.

        private async Task SetAdExtensionsAssociationsAsync(long accountId, IList<AdExtensionIdToEntityIdAssociation> associations, AssociationType associationType)
        {
            var request = new SetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            await CampaignService.CallAsync((s, r) => s.SetAdExtensionsAssociationsAsync(r), request);
        }

        // Removes the specified association from the respective campaigns or ad groups.

        private async Task DeleteAdExtensionsAssociationsAsync(long accountId, IList<AdExtensionIdToEntityIdAssociation> associations, AssociationType associationType)
        {
            var request = new DeleteAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            await CampaignService.CallAsync((s, r) => s.DeleteAdExtensionsAssociationsAsync(r), request);
        }

        // Gets the specified ad extensions from the account's extension library.

        private async Task<IEnumerable<AdExtension>> GetAdExtensionsByIdsAsync(
            long accountId, 
            IList<long> adExtensionIds, 
            AdExtensionsTypeFilter adExtensionsTypeFilter,
            AdExtensionAdditionalField returnAdditionalFields)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds,
                AdExtensionType = adExtensionsTypeFilter,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdExtensionsByIdsAsync(r), request)).AdExtensions;
        }

        // Gets the reasons why the specified extension failed editorial when 
        // in the context of an associated campaign or ad group.

        private async Task<IList<AdExtensionEditorialReasonCollection>> GetAdExtensionsEditorialReasons(
            long accountId,
            IList<AdExtensionIdToEntityIdAssociation> associations,
            AssociationType associationType)
        {
            var request = new GetAdExtensionsEditorialReasonsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            return (await CampaignService.CallAsync(
                (s, r) => s.GetAdExtensionsEditorialReasonsAsync(r), request)).EditorialReasons;
        }

        // Gets the account's migration statuses.

        private async Task<IEnumerable<AccountMigrationStatusesInfo>> GetAccountMigrationStatusesAsync(
            long[] accountIds,
            string migrationType)
        {
            var request = new GetAccountMigrationStatusesRequest
            {
                AccountIds = accountIds,
                MigrationType = migrationType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAccountMigrationStatusesAsync(r), request)).MigrationStatuses;
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
                        EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date {
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
