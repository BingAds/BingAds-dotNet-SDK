using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;
using System.Collections.Generic;
using System.Net.Http;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create Responsive Ad recommendation with the Campaign Management service.
    /// </summary>
    public class ResponsiveAdRecommendation : ExampleBase
    {
        // To run this example you'll need to provide a valid Ad Final URL
        private const string AdFinalURL = "https://contoso.com";

        // Set to false to disable cleanup of created entities at the end
        private const bool DoCleanup = true;

        private static readonly HttpClient HttpClient = new HttpClient();

        public override string Description
        {
            get { return "Responsive Ad Recommendation | Campaign Management V13"; }
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

                Console.WriteLine($"Getting ad recommendation for URL {AdFinalURL} ...");

                var recommendationResponse = await CampaignManagementExampleHelper.CampaignManagementService.CreateResponsiveAdRecommendationAsync(new CreateResponsiveAdRecommendationRequest
                {
                    FinalUrls = new[] { AdFinalURL }
                });

                var ad = recommendationResponse.ResponsiveAd;

                // select a few images from the suggested list. This example picks first 5 images
                var selectedImages = recommendationResponse.ImageSuggestions.Take(5).ToArray();

                // add selected images to your media library
                await SaveImages(selectedImages, CampaignManagementExampleHelper.CampaignManagementService);

                ad.Images = selectedImages.Select(x => x.AssetLink).ToArray();

                ad.BusinessName = "Contoso";
                ad.CallToActionLanguage = LanguageName.English;
                
                // Create an Audience campaign with one ad group and a responsive ad.

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        // CampaignType must be set for Audience campaigns
                        CampaignType = CampaignType.Audience,
                        DailyBudget = 50,
                        // Languages must be set for Audience campaigns
                        Languages = new string[] { "All" },
                        Name = "Ad recommendation test " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
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

                // Add an ad group within the campaign.

                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                        // Network cannot be set for ad groups in Audience campaigns
                        Network = null,
                    }
                };

                OutputStatusMessage("-----\nAddAdGroups:");
                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync(
                    campaignId: (long)campaignIds[0],
                    adGroups: adGroups,
                    returnInheritedBidStrategyTypes: false);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputStatusMessage("AdGroupIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

                // Add a responsive ad within the ad group.

                var ads = new Ad[] { ad };

                OutputStatusMessage("-----\nAddAds:");
                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync(
                    adGroupId: (long)adGroupIds[0],
                    ads: ads);
                long?[] adIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                OutputStatusMessage("AdIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);

                Console.WriteLine("Created campaign: " + campaigns[0].Name);

                if (!DoCleanup)
                {
                    return;
                }

                // Delete the account's media.

                var mediaIds = ad.Images.Select(x => x.Asset.Id.Value).ToArray();

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
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        private async Task SaveImages(IList<AdRecommendationImageSuggestion> imageSuggestions, ServiceClient<ICampaignManagementService> serviceClient)
        {
            var mediaToAdd = new List<Media>();

            foreach (var imageSuggestion in imageSuggestions)
            {
                var bytes = await (await HttpClient.GetAsync(imageSuggestion.ImageUrl)).Content.ReadAsByteArrayAsync();

                var image = imageSuggestion.Image;

                image.Data = Convert.ToBase64String(bytes);

                mediaToAdd.Add(image);
            }

            var mediaRequest = new AddMediaRequest { Media = mediaToAdd.ToArray() };

            var addMediaResponse = await serviceClient.AddMediaAsync(mediaRequest);

            for (int i = 0; i < addMediaResponse.MediaIds.Count; i++)
            {
                imageSuggestions[i].AssetLink.Asset.Id = addMediaResponse.MediaIds[i];
            }
        }
    }
}
