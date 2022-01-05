using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to manage ad extensions for an account's ad extension library, 
    /// associate the extensions with a campaign, and determine why an extension failed 
    /// editorial review.
    /// </summary>
    public class AdExtensions : ExampleBase
    {
        // To run this example you'll need to provide your own image.  
        // For required aspect ratios and recommended dimensions please see 
        // Image remarks at https://go.microsoft.com/fwlink/?linkid=872754.

        private const string MediaFilePath = "c:\\dev\\media\\";
        private const string ImageAdExtensionMediaFileName = "imageadextension300x200.png";

        public override string Description
        {
            get { return "Ad Extensions | Campaign Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                // Add a campaign to associate with ad extensions. 

                var campaigns = new[] {
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };

                OutputStatusMessage("-----\nAddCampaigns:");
                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaigns: campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("CampaignIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);
                
                // Create media for the image ad extension that we'll add later. 

                var imageAdExtensionMedia = GetImageMedia(
                    "Image15x10",
                    MediaFilePath + ImageAdExtensionMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var media = new Media[]
                {
                    imageAdExtensionMedia
                };

                // Add the media to the account's library.

                OutputStatusMessage("-----\nAddMedia:");
                AddMediaResponse addMediaResponse = await CampaignManagementExampleHelper.AddMediaAsync(
                    accountId: authorizationData.AccountId,
                    media: media);
                long[] mediaIds = addMediaResponse.MediaIds.ToArray();
                OutputStatusMessage("MediaIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(mediaIds);

                // Add the extensions to the account's library.

                var adExtensions = new AdExtension[] {
                    new ActionAdExtension
                    {
                        ActionType = ActionAdExtensionActionType.ActNow,
                        FinalUrls = new string[]
                        {
                            "https://www.contoso.com/womenshoesale"
                        },
                        Language = "English",
                        Status = AdExtensionStatus.Active,
                    },
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
                        // Include the call extension Monday - Friday from 9am - 9pm
                        // in the account's time zone.
                        Scheduling = new Schedule {
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
                            EndDate = new Microsoft.BingAds.V13.CampaignManagement.Date {
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
                    new ImageAdExtension
                    {
                        AlternativeText = "Image Extension Alt Text",
                        ImageMediaIds = mediaIds,
                        Images = new AssetLink[]
                        {
                            new AssetLink
                            {
                                Asset = new ImageAsset
                                {
                                    Id = 12345, // 1.91 aspect ratio image id. This type of image is required for Images
                                    SubType = "LandscapeImageMedia",
                                }
                            }
                        }
                    },
                    new LocationAdExtension {
                        PhoneNumber = "206-555-0100",
                        CompanyName = "Contoso Shoes",
                        Address = new Microsoft.BingAds.V13.CampaignManagement.Address {
                            StreetAddress = "1234 Washington Place",
                            StreetAddress2 = "Suite 1210",
                            CityName = "Woodinville",
                            ProvinceName = "WA",
                            CountryCode = "US",
                            PostalCode = "98608"
                        },
                        // Include the location extension every Saturday morning
                        // in the search user's time zone.
                        Scheduling = new Schedule {
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
                            EndDate = new Microsoft.BingAds.V13.CampaignManagement.Date {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    },
                    new PriceAdExtension
                    {
                        Language = "English",
                        PriceExtensionType = PriceExtensionType.Events,
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
                    },
                    new ReviewAdExtension
                    {
                        IsExact = true,
                        Source = "Review Source Name",
                        Text = "Review Text",
                        // The Url of the third-party review. This is not your business Url.
                        Url = "https://review.contoso.com" 
                    },
                    new SitelinkAdExtension {
                        Description1 = "Simple & Transparent.",
                        Description2 = "No Upfront Cost.",
                        DisplayText = "Everyone's Shoe Sale",
                        FinalUrls = new[] {
                            "https://www.contoso.com/womenshoesale"
                        },
                    },
                    new StructuredSnippetAdExtension
                    {
                        Header = "Brands",
                        Values = new [] { "Windows", "Xbox", "Skype"}
                    }
                };

                OutputStatusMessage("-----\nAddAdExtensions:");
                var addAdExtensionsResponse = await CampaignManagementExampleHelper.AddAdExtensionsAsync(
                    accountId: authorizationData.AccountId,
                    adExtensions: adExtensions);
                OutputStatusMessage("AdExtensionIdentities:");
                var adExtensionIdentities = addAdExtensionsResponse?.AdExtensionIdentities;
                OutputStatusMessage("NestedPartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchErrorCollection(addAdExtensionsResponse?.NestedPartialErrors);
                
                // DeleteAdExtensionsAssociations, SetAdExtensionsAssociations, and GetAdExtensionsEditorialReasons 
                // operations each require a list of type AdExtensionIdToEntityIdAssociation.
                var adExtensionIdToEntityIdAssociations = new List<AdExtensionIdToEntityIdAssociation>();

                // GetAdExtensionsByIds requires a list of type long.
                var adExtensionIds = new List<long>();

                // Loop through the list of extension IDs and build any required data structures
                // for subsequent operations. 

                foreach (var adExtensionIdentity in adExtensionIdentities)
                {
                    if (adExtensionIdentity != null)
                    {
                        adExtensionIdToEntityIdAssociations.Add(new AdExtensionIdToEntityIdAssociation
                        {
                            AdExtensionId = adExtensionIdentity.Id,
                            EntityId = (long)campaignIds[0]
                        });

                        adExtensionIds.Add(adExtensionIdentity.Id);
                    }
                }

                // Associate the ad extensions with the campaign. 

                OutputStatusMessage("-----\nSetAdExtensionsAssociations:");
                var setAdExtensionsAssociationsResponse = await CampaignManagementExampleHelper.SetAdExtensionsAssociationsAsync(
                    accountId: authorizationData.AccountId,
                    adExtensionIdToEntityIdAssociations: adExtensionIdToEntityIdAssociations,
                    associationType: AssociationType.Campaign);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(setAdExtensionsAssociationsResponse?.PartialErrors);

                // Get editorial rejection reasons for the ad extension and entity associations.

                OutputStatusMessage("-----\nGetAdExtensionsEditorialReasons:");
                var getAdExtensionsEditorialReasonsResponse = await CampaignManagementExampleHelper.GetAdExtensionsEditorialReasonsAsync(
                        accountId: authorizationData.AccountId,
                        adExtensionIdToEntityIdAssociations: adExtensionIdToEntityIdAssociations,
                        associationType: AssociationType.Campaign);
                OutputStatusMessage("EditorialReasons:");
                var adExtensionEditorialReasonCollection =
                    (AdExtensionEditorialReasonCollection[])getAdExtensionsEditorialReasonsResponse?.EditorialReasons;
                CampaignManagementExampleHelper.OutputArrayOfAdExtensionEditorialReasonCollection(adExtensionEditorialReasonCollection);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(getAdExtensionsEditorialReasonsResponse?.PartialErrors);
                
                // Get only the location ad extensions and then remove scheduling.

                AdExtensionsTypeFilter adExtensionsTypeFilter = AdExtensionsTypeFilter.LocationAdExtension;

                // In this example partial errors will be returned for indices where the ad extensions 
                // are not location ad extensions.
                // This is an example, and ideally you would only send the required ad extension IDs.

                OutputStatusMessage("-----\nGetAdExtensionsByIds:");
                var getAdExtensionsByIdsResponse = (await CampaignManagementExampleHelper.GetAdExtensionsByIdsAsync(
                    accountId: authorizationData.AccountId,
                    adExtensionIds: adExtensionIds,
                    adExtensionType: adExtensionsTypeFilter,
                    returnAdditionalFields: AdExtensionAdditionalField.DisplayText | AdExtensionAdditionalField.Images));
                adExtensions = getAdExtensionsByIdsResponse?.AdExtensions.ToArray();
                BatchError[] getAdExtensionErrors = getAdExtensionsByIdsResponse?.PartialErrors.ToArray();
                OutputStatusMessage("AdExtensions:");
                CampaignManagementExampleHelper.OutputArrayOfAdExtension(adExtensions);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(getAdExtensionErrors);
                
                var updateExtensions = new List<AdExtension>();
                var updateExtensionIds = new List<long>();

                foreach (var extension in adExtensions)
                {
                    // GetAdExtensionsByIds will return a nil element if the request conditions were not met.
                    if (extension != null && extension.Id != null)
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

                OutputStatusMessage("-----\nUpdateAdExtensions:");                
                await CampaignManagementExampleHelper.UpdateAdExtensionsAsync(
                    accountId: authorizationData.AccountId, 
                    adExtensions: updateExtensions);
                OutputStatusMessage("Removed scheduling from the location ad extensions.");

                // Get only the location extensions to output the result.

                OutputStatusMessage("-----\nGetAdExtensionsByIds:");
                getAdExtensionsByIdsResponse = await CampaignManagementExampleHelper.GetAdExtensionsByIdsAsync(
                    accountId: authorizationData.AccountId,
                    adExtensionIds: updateExtensionIds,
                    adExtensionType: adExtensionsTypeFilter,
                    returnAdditionalFields: AdExtensionAdditionalField.DisplayText | AdExtensionAdditionalField.Images);
                adExtensions = getAdExtensionsByIdsResponse?.AdExtensions.ToArray();
                getAdExtensionErrors = getAdExtensionsByIdsResponse?.PartialErrors.ToArray();
                OutputStatusMessage("AdExtensions:");
                CampaignManagementExampleHelper.OutputArrayOfAdExtension(adExtensions);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(getAdExtensionErrors);

                // Delete the ad extension associations, ad extensions, and campaign, that were previously added.  
                // At this point the ad extensions are still available in the account's ad extensions library. 

                OutputStatusMessage("-----\nDeleteAdExtensionsAssociations:");
                await CampaignManagementExampleHelper.DeleteAdExtensionsAssociationsAsync(
                    authorizationData.AccountId,
                    adExtensionIdToEntityIdAssociations,
                    AssociationType.Campaign);
                OutputStatusMessage("Deleted ad extension associations.");

                // Delete the ad extensions from the account's ad extension library.

                OutputStatusMessage("-----\nDeleteAdExtensions:");
                await CampaignManagementExampleHelper.DeleteAdExtensionsAsync(
                    authorizationData.AccountId,
                    adExtensionIds);
                OutputStatusMessage("Deleted ad extensions.");

                // Delete the account's media that was used for the image ad extension.

                OutputStatusMessage("-----\nDeleteMedia:");
                await CampaignManagementExampleHelper.DeleteMediaAsync(
                    accountId: authorizationData.AccountId,
                    mediaIds: mediaIds);

                foreach (var id in mediaIds)
                {
                    OutputStatusMessage(string.Format("Deleted Media Id {0}", id));
                }

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}", campaignIds[0]));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        /// <summary>
        /// Get image media that can be managed with the Campaign Management API.
        /// </summary>
        /// <param name="mediaType">The media type reflects the aspect ratio.</param>
        /// <param name="imageFileName">The file name and path.</param>
        /// <param name="imageFormat">For supported image formats see <see href="https://go.microsoft.com/fwlink/?linkid=872754">Image remarks</see>.</param>
        /// <returns>A Campaign Management Image object.</returns>
        private Microsoft.BingAds.V13.CampaignManagement.Image GetImageMedia(
            string mediaType,
            string imageFileName,
            System.Drawing.Imaging.ImageFormat imageFormat)
        {
            var image = new Microsoft.BingAds.V13.CampaignManagement.Image();
            image.Data = GetBmpBase64String(imageFileName, imageFormat);
            image.MediaType = mediaType;
            image.Type = "Image";

            return image;
        }

        /// <summary>
        /// Get the image media as base64 string.
        /// </summary>
        /// <param name="imageFileName">The file name and path.</param>
        /// <param name="imageFormat">For supported image formats see <see href="https://go.microsoft.com/fwlink/?linkid=872754">Image remarks</see>.</param>
        /// <returns></returns>
        private string GetBmpBase64String(
            string imageFileName,
            System.Drawing.Imaging.ImageFormat imageFormat)
        {
            var bmp = new Bitmap(imageFileName);
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, imageFormat);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }        
    }
}
