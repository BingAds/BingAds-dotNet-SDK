using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create Responsive Ads with the Bulk service.
    /// </summary>
    public class BulkResponsiveAds : BulkExampleBase
    {
        // To run this example you'll need to provide your own image.  
        // For required aspect ratios and recommended dimensions please see 
        // Image remarks at https://go.microsoft.com/fwlink/?linkid=872754.

        private const string MediaFilePath = "c:\\dev\\media\\";
        private const string ResponsiveAdMediaFileName = "imageresponsivead1200x628.png";

        public override string Description
        {
            get { return "Responsive Search Ads | Bulk V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                // Media cannot be added via the Bulk service, so we'll use the Campaign Management service 
                // i.e., the AddMedia service operation below. 

                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));
                
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

                var uploadEntities = new List<BulkEntity>();

                // Create an Audience campaign with one ad group and a responsive ad.
                
                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        // CampaignType must be set for Audience campaigns
                        CampaignType = CampaignType.Audience,
                        // Languages must be set for Audience campaigns
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        Settings = null,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };
                uploadEntities.Add(bulkCampaign);

                // Specify one or more ad groups.

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V13.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                        // Language cannot be set for ad groups in Audience campaigns
                        Language = null,
                        // Network cannot be set for ad groups in Audience campaigns
                        Network = null,
                    },
                };
                uploadEntities.Add(bulkAdGroup);

                var bulkResponsiveAd = new BulkResponsiveAd
                {
                    AdGroupId = adGroupIdKey,
                    ResponsiveAd = new ResponsiveAd
                    {
                        Id = responsiveAdIdKey,
                        BusinessName = "Contoso",
                        CallToAction = CallToAction.AddToCart,
                        FinalUrls = new[] {
                            "https://www.contoso.com/womenshoesale"
                        },
                        ForwardCompatibilityMap = null,
                        Headline = "Fast & Easy Setup",
                        Images = new[]
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
                                    Type = null
                                },
                            },
                        },
                        LongHeadlineString = "Find New Customers & Increase Sales!",
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                    },
                };            
                uploadEntities.Add(bulkResponsiveAd);

                // Upload and write the output

                OutputStatusMessage("-----\nAdding campaign, ad group, and ad...");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");
                
                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);
                
                var responsiveAdResults = downloadEntities.OfType<BulkResponsiveAd>().ToList();
                OutputBulkResponsiveAds(responsiveAdResults);

                Reader.Dispose();
                
                // Delete the campaign and everything it contains e.g., ad groups and ads.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                OutputStatusMessage("-----\nDeleting campaign, ad group, and ad...");

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
