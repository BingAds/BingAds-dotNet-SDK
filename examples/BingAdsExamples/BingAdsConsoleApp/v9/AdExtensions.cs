using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsConsoleApp.V9
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
            get { return "Ad Extensions | Campaign Management V9 (Deprecated)"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Add a campaign that will later be associated with ad extensions. 

                var campaign = GetExampleCampaign();

                var campaignIds = await AddCampaignsAsync(authorizationData.AccountId, new[] { campaign });

                // Specify the extensions.

                var adExtensions = new AdExtension[] {
                    new AppAdExtension
                    {
                        AppPlatform="Windows",
                        AppStoreId="AppStoreIdGoesHere",
                        DestinationUrl="DestinationUrlGoesHere",
                        DisplayText="Contoso",
                    },
                    new CallAdExtension {
                        CountryCode = "US",
                        PhoneNumber = "2065550100",
                        IsCallOnly = false
                    },
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
                    new SiteLinksAdExtension {
                        SiteLinks = new [] {
                            new SiteLink {
                                DestinationUrl = "Contoso.com/WomenShoeSale",
                                DisplayText = "Women's Shoe Sale"
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
                        EntityId = campaignIds[0]
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
                    (AdExtensionEditorialReasonCollection[]) await GetAdExtensionsEditorialReasons(
                        authorizationData.AccountId,
                        adExtensionIdToEntityIdAssociations,
                        AssociationType.Campaign
                        );

                const AdExtensionsTypeFilter adExtensionsTypeFilter = AdExtensionsTypeFilter.AppAdExtension | 
                                                                      AdExtensionsTypeFilter.CallAdExtension |
                                                                      AdExtensionsTypeFilter.LocationAdExtension |
                                                                      AdExtensionsTypeFilter.SiteLinksAdExtension;

                // Get the specified ad extensions from the account’s ad extension library.
                adExtensions = (AdExtension[]) await GetAdExtensionsByIdsAsync(
                    authorizationData.AccountId,
                    adExtensionIds,
                    adExtensionsTypeFilter
                    );

                PrintAdExtensions(adExtensions, adExtensionEditorialReasonCollection);
                
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
            catch (FaultException<Microsoft.BingAds.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CampaignManagement.EditorialApiFaultDetail> ex)
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

        private async Task<IList<long>> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await Service.CallAsync((s, r) => s.AddCampaignsAsync(r), request)).CampaignIds;
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

        private void PrintAdExtensions(IEnumerable<AdExtension> adExtensions,
                                       IList<AdExtensionEditorialReasonCollection> adExtensionEditorialReasonCollection)
        {
            int index = 0;

            foreach (var extension in adExtensions)
            {
                if (extension == null || extension.Id == null)
                {
                    OutputStatusMessage("Extension is null or invalid.");
                }
                else
                {
                    OutputStatusMessage("Ad extension ID: " + extension.Id);
                    OutputStatusMessage("Ad extension Type: " + extension.Type);

                    var appAdExtension = extension as AppAdExtension;
                    if (appAdExtension != null)
                    {
                        OutputStatusMessage(string.Format("AppPlatform: {0}", appAdExtension.AppPlatform));
                        OutputStatusMessage(string.Format("AppStoreId: {0}", appAdExtension.AppStoreId));
                        OutputStatusMessage(string.Format("DestinationUrl: {0}", appAdExtension.DestinationUrl));
                        OutputStatusMessage(string.Format("DevicePreference: {0}", appAdExtension.DevicePreference));
                        OutputStatusMessage(string.Format("DisplayText: {0}", appAdExtension.DisplayText));
                        OutputStatusMessage(string.Format("Id: {0}", appAdExtension.Id));
                        OutputStatusMessage(string.Format("Status: {0}", appAdExtension.Status));
                        OutputStatusMessage(string.Format("Version: {0}", appAdExtension.Version));
                        OutputStatusMessage("\n");
                    }
                    else
                    {
                        var callAdExtension = extension as CallAdExtension;
                        if (callAdExtension != null)
                        {
                            OutputStatusMessage("Phone number: " + callAdExtension.PhoneNumber);
                            OutputStatusMessage("Country: " + callAdExtension.CountryCode);
                            OutputStatusMessage("Is only clickable item: " + callAdExtension.IsCallOnly);
                            OutputStatusMessage("\n");
                        }
                        else
                        {
                            var locationAdExtension = extension as LocationAdExtension;
                            if (locationAdExtension != null)
                            {
                                OutputStatusMessage("Company name: " + locationAdExtension.CompanyName);
                                OutputStatusMessage("Phone number: " + locationAdExtension.PhoneNumber);
                                OutputStatusMessage("Street: " + locationAdExtension.Address.StreetAddress);
                                OutputStatusMessage("City: " + locationAdExtension.Address.CityName);
                                OutputStatusMessage("State: " + locationAdExtension.Address.ProvinceName);
                                OutputStatusMessage("Country: " + locationAdExtension.Address.CountryCode);
                                OutputStatusMessage("Zip code: " + locationAdExtension.Address.PostalCode);
                                OutputStatusMessage("Business coordinates determined?: " +
                                             locationAdExtension.GeoCodeStatus);
                                OutputStatusMessage("Map icon ID: " + locationAdExtension.IconMediaId);
                                OutputStatusMessage("Business image ID: " + locationAdExtension.ImageMediaId);
                                OutputStatusMessage("\n");
                            }
                            else
                            {
                                var linksAdExtension = extension as SiteLinksAdExtension;
                                if (linksAdExtension != null)
                                {
                                    foreach (SiteLink siteLink in linksAdExtension.SiteLinks)
                                    {
                                        OutputStatusMessage("Display URL: " + siteLink.DisplayText);
                                        OutputStatusMessage("Destination URL: " + siteLink.DestinationUrl);
                                        OutputStatusMessage("\n");
                                    }
                                }
                                else
                                {
                                    OutputStatusMessage("Unknown extension type");
                                }
                            }
                        }
                    }

                    if (adExtensionEditorialReasonCollection != null
                        && adExtensionEditorialReasonCollection.Count > 0
                        && adExtensionEditorialReasonCollection[index] != null)
                    {
                        // Print any editorial rejection reasons for the corresponding extension. This example 
                        // assumes the same list index for adExtensions and adExtensionEditorialReasonCollection
                        // as defined above.

                        foreach (var adExtensionEditorialReason in adExtensionEditorialReasonCollection[index].Reasons)
                        {
                            if (adExtensionEditorialReason != null &&
                                adExtensionEditorialReason.PublisherCountries != null)
                            {
                                OutputStatusMessage("Editorial Rejection Location: " + adExtensionEditorialReason.Location);
                                OutputStatusMessage("Editorial Rejection PublisherCountries: ");
                                foreach (var publisherCountry in adExtensionEditorialReason.PublisherCountries)
                                {
                                    OutputStatusMessage("  " + publisherCountry);
                                }
                                OutputStatusMessage("Editorial Rejection ReasonCode: " + adExtensionEditorialReason.ReasonCode);
                                OutputStatusMessage("Editorial Rejection Term: " + adExtensionEditorialReason.Term);
                                OutputStatusMessage("\n");
                            }
                        }
                    }
                }

                index++;

            }
        }
    }
}
