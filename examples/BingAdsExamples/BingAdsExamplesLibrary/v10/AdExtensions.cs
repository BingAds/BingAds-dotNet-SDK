using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add, get, and delete extensions for an account’s ad extension library, 
    /// set, get, and delete the extension associations with a campaign, and determine why an extension failed 
    /// editorial review.
    /// </summary>
    public class AdExtensions : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Ad Extensions | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

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
                        IsCallOnly = false
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
                        Address = new Address {
                            StreetAddress = "1234 Washington Place",
                            StreetAddress2 = "Suite 1210",
                            CityName = "Woodinville",
                            ProvinceName = "WA",
                            CountryCode = "US",
                            PostalCode = "98608"
                        }
                    },
                    new ReviewAdExtension
                    {
                        IsExact = true,
                        Source = "Review Source Name",
                        Text = "Review Text",
                        Url = "http://review.contoso.com" // The Url of the third-party review. This is not your business Url.
                    },
                    new SiteLinksAdExtension {
                        SiteLinks = new [] {
                            new SiteLink
                            {
                                DisplayText = "Women's Shoe Sale 1",

                                // If you are currently using Destination URLs, you must replace them with Final URLs. 
                                // Here is an example of a DestinationUrl you might have used previously. 
                                // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                                // To migrate from DestinationUrl to FinalUrls for existing sitelinks, you can set DestinationUrl
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
                                DisplayText = "Women's Shoe Sale 2",

                                // If you are currently using Destination URLs, you must replace them with Final URLs. 
                                // Here is an example of a DestinationUrl you might have used previously. 
                                // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                                // To migrate from DestinationUrl to FinalUrls for existing sitelinks, you can set DestinationUrl
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

                // Add all extensions to the account's ad extension library
                var adExtensionIdentities = await AddAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensions
                    );

                OutputStatusMessage("Added ad extensions.\n\n");

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

                OutputStatusMessage("Set ad extension associations.\n\n");

                // Get editorial rejection reasons for the respective ad extension and entity associations.
                var adExtensionEditorialReasonCollection =
                    (AdExtensionEditorialReasonCollection[])await GetAdExtensionsEditorialReasons(
                        authorizationData.AccountId,
                        adExtensionIdToEntityIdAssociations,
                        AssociationType.Campaign
                        );

                const AdExtensionsTypeFilter adExtensionsTypeFilter = AdExtensionsTypeFilter.AppAdExtension |
                                                                      AdExtensionsTypeFilter.CallAdExtension |
                                                                      AdExtensionsTypeFilter.CalloutAdExtension |
                                                                      AdExtensionsTypeFilter.ImageAdExtension | 
                                                                      AdExtensionsTypeFilter.LocationAdExtension |
                                                                      AdExtensionsTypeFilter.ReviewAdExtension |
                                                                      AdExtensionsTypeFilter.SiteLinksAdExtension;

                // Get the specified ad extensions from the account’s ad extension library.
                adExtensions = (AdExtension[]) await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    adExtensionIds,
                    adExtensionsTypeFilter
                    );

                OutputAdExtensionsWithEditorialReasons(adExtensions, adExtensionEditorialReasonCollection);

                // Remove the specified associations from the respective campaigns or ad groups. 
                // The extesions are still available in the account's extensions library. 
                await DeleteAdExtensionsAssociationsAsync(
                    authorizationData.AccountId,
                    adExtensionIdToEntityIdAssociations,
                    AssociationType.Campaign
                    );

                OutputStatusMessage("Deleted ad extension associations.\n\n");

                // Deletes the ad extensions from the account’s ad extension library.
                await DeleteAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensionIds
                    );

                OutputStatusMessage("Deleted ad extensions.\n\n");
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

            return (await Service.CallAsync((s, r) => s.AddCampaignsAsync(r), request));
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

            return (await Service.CallAsync((s, r) => s.AddAdExtensionsAsync(r), request)).AdExtensionIdentities;
        }

        // Deletes one or more ad extensions from the account’s ad extension library.

        private async Task DeleteAdExtensionsAsync(long accountId, IList<long> adExtensionIds)
        {
            var request = new DeleteAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds
            };

            await Service.CallAsync((s, r) => s.DeleteAdExtensionsAsync(r), request);
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

            await Service.CallAsync((s, r) => s.SetAdExtensionsAssociationsAsync(r), request);
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

            await Service.CallAsync((s, r) => s.DeleteAdExtensionsAssociationsAsync(r), request);
        }

        // Gets the specified ad extensions from the account's extension library.

        private async Task<IEnumerable<AdExtension>> GetAdExtensionsByIdsAsync(long accountId, IList<long> adExtensionIds, AdExtensionsTypeFilter adExtensionsTypeFilter)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds,
                AdExtensionType = adExtensionsTypeFilter
            };

            return (await Service.CallAsync((s, r) => s.GetAdExtensionsByIdsAsync(r), request)).AdExtensions;
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

            return (await Service.CallAsync(
                (s, r) => s.GetAdExtensionsEditorialReasonsAsync(r), request)).EditorialReasons;
        }
    }
}
