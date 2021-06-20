using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;
using System.Drawing;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create Responsive Ads with the Campaign Management service.
    /// </summary>
    public class ResponsiveAds : ExampleBase
    {
        // To run this example you'll need to provide your own image.  
        // For required aspect ratios and recommended dimensions please see 
        // Image remarks at https://go.microsoft.com/fwlink/?linkid=872754.

        private const string MediaFilePath = "c:\\dev\\media\\";
        private const string ResponsiveAdMediaFileName = "imageresponsivead1200x628.png";

        public override string Description
        {
            get { return "Responsive Ads | Campaign Management V13"; }
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

                // Add an image to your media library. 
                // The image asset is needed later to create the responsive ad.

                var landscapeImageMedia = GetImageMedia(
                    "Image191x100",
                    MediaFilePath + ResponsiveAdMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var media = new Media[]
                {
                    landscapeImageMedia,
                };

                OutputStatusMessage("-----\nAddMedia:");
                AddMediaResponse addMediaResponse = await CampaignManagementExampleHelper.AddMediaAsync(
                    accountId: authorizationData.AccountId,
                    media: media);
                long[] mediaIds = addMediaResponse.MediaIds.ToArray();
                OutputStatusMessage("MediaIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(mediaIds);
                
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
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
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

                var ads = new Ad[] {
                    new ResponsiveAd
                    {
                        BusinessName = "Contoso",
                        CallToAction = CallToAction.AddToCart,
                        FinalUrls = new[] {
                            "https://www.contoso.com/womenshoesale"
                        },
                        Headline = "Fast & Easy Setup",
                        Images = new []
                        {
                            // You are only required to provide a landscape image asset. 
                            // Optionally you can include additional asset links, i.e., one image asset for each supported sub type. 
                            // For any image asset sub types that you do not explicitly set, 
                            // the service will automatically create image asset links by cropping the LandscapeImageMedia.
                            new AssetLink
                            {
                                Asset = new ImageAsset
                                {
                                    CropHeight = null,
                                    CropWidth = null,
                                    CropX = null,
                                    CropY = null,
                                    Id = mediaIds[0],
                                    Name = "My LandscapeImageMedia",
                                    SubType = "LandscapeImageMedia", 
                                },
                            },                           
                        },
                        LongHeadlineString = "Find New Customers & Increase Sales!",
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                    },
                };

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

                // Delete the account's media.

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

        /// <summary>
        /// Get image media that can be created via the 
        /// Campaign Management API.
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
