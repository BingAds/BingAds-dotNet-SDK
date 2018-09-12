using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to add, get, and delete media in an account's
    /// media library. You can use media with responsive ads and image ad extensions,
    /// and please note the required dimensions vary for each.
    /// </summary>
    public class ImageMedia : ExampleBase
    {
        // To run this example you'll need access to image media files 
        // for responsive ads and image ad extensions.

        private const string MediaFilePath = "c:\\dev\\media\\";

        // For required aspect ratios and recommended dimensions please see 
        // Image remarks at https://go.microsoft.com/fwlink/?linkid=872754.

        private const string LandscapeImageMediaFileName = "imageresponsivead1200x628.png";
        private const string LandscapeLogoMediaFileName = "imageresponsivead1200x300.png";
        private const string SquareImageMediaFileName = "imageresponsivead1200x1200.png";
        private const string SquareLogoMediaFileName = "imageresponsivead1100x1100.png";
        private const string ImageAdExtensionMediaFileName = "imageadextension300x200.png";

        public override string Description
        {
            get { return "Image Media | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CampaignManagementExampleHelper CampaignManagementExampleHelper =
                    new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService =
                    new ServiceClient<ICampaignManagementService>(authorizationData, environment);

                #region MediaRepresentations

                var landscapeImageMedia = GetImageMedia(
                    "Image191x100",
                    MediaFilePath + LandscapeImageMediaFileName, 
                    System.Drawing.Imaging.ImageFormat.Png);

                var landscapeLogoMedia = GetImageMedia(
                    "Image4x1",
                    MediaFilePath + LandscapeLogoMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var squareImageMedia = GetImageMedia(
                    "Image1x1",
                    MediaFilePath + SquareImageMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var squareLogoMedia = GetImageMedia(
                    "Image1x1",
                    MediaFilePath + SquareLogoMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var imageAdExtensionMedia = GetImageMedia(
                    "Image15x10",
                    MediaFilePath + ImageAdExtensionMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var addMedia = new Media[]
                {
                    landscapeImageMedia,
                    landscapeLogoMedia,
                    squareImageMedia,
                    squareLogoMedia,
                    imageAdExtensionMedia
                };
                CampaignManagementExampleHelper.OutputArrayOfMedia(addMedia);

                var imageMediaIds = (await CampaignManagementExampleHelper.AddMediaAsync(
                    authorizationData.AccountId,
                    addMedia)).MediaIds;

                // The index of returned IDs is consistent with the order you submitted them in the request;
                // however, the sequence of the IDs themselves is not guaranteed. For example you might observe:
                // - Landscape Image Media Id == imageMediaIds[0] == 1
                // - Landscape Logo Media Id == imageMediaIds[1] == 4
                // - Square Image Media Id == imageMediaIds[2] == 3
                // - Square Logo Media Id == imageMediaIds[3] == 2
                // - Image Ad Extension Media Id == imageMediaIds[4] == 0

                // You can use the first four Media Ids when you add or update a Responsive Ad
                // in an Audience campaign e.g., see AudienceCampaigns.cs. 

                var adMediaIds = new List<long> { imageMediaIds[0], imageMediaIds[1], imageMediaIds[2], imageMediaIds[3] };
                OutputStatusMessage("Media Ids for Responsive Ad:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adMediaIds);

                // You can use the fifth Media Id when you add or update an Image Ad Extension
                // in a Search campaign e.g., see AdExtensions.cs.

                var extensionMediaIds = new List<long> { imageMediaIds[4] };
                OutputStatusMessage("Media Ids for Image Ad Extension:");
                CampaignManagementExampleHelper.OutputArrayOfLong(extensionMediaIds);
                
                // Get the media representations to confirm the stored dimensions
                // and get the Url where you can later view or download the media.

                var getResponsiveAdMediaMetaData = (await CampaignManagementExampleHelper.GetMediaMetaDataByAccountIdAsync(
                    MediaEnabledEntityFilter.ResponsiveAd)).MediaMetaData;
                CampaignManagementExampleHelper.OutputArrayOfMediaMetaData(getResponsiveAdMediaMetaData);

                var getImageAdExtensionMediaMetaData = (await CampaignManagementExampleHelper.GetMediaMetaDataByAccountIdAsync(
                    MediaEnabledEntityFilter.ImageAdExtension)).MediaMetaData;
                CampaignManagementExampleHelper.OutputArrayOfMediaMetaData(getImageAdExtensionMediaMetaData);

                var getMediaMetaData = (await CampaignManagementExampleHelper.GetMediaMetaDataByIdsAsync(imageMediaIds)).MediaMetaData;
                CampaignManagementExampleHelper.OutputArrayOfMediaMetaData(getMediaMetaData);

                #endregion MediaRepresentations

                #region Delete

                // Comment out (disable) the delete operation if you want to use the media IDs  
                // in another example e.g., AudienceCampaigns.cs.

                var deleteMediaResponse = (await CampaignManagementExampleHelper.DeleteMediaAsync(
                    authorizationData.AccountId,
                    imageMediaIds));

                foreach (var id in imageMediaIds)
                {
                    OutputStatusMessage(string.Format("Deleted Media Id {0}\n", id));
                }

                #endregion Delete
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.EditorialApiFaultDetail> ex)
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
        /// Bing Ads Campaign Management API.
        /// </summary>
        /// <param name="mediaType">The media type reflects the aspect ratio.</param>
        /// <param name="imageFileName">The file name and path.</param>
        /// <param name="imageFormat">For supported image formats see <see href="https://go.microsoft.com/fwlink/?linkid=872754">Image remarks</see>.</param>
        /// <returns>A Campaign Management Image object.</returns>
        private Microsoft.BingAds.V12.CampaignManagement.Image GetImageMedia(
            string mediaType, 
            string imageFileName,
            System.Drawing.Imaging.ImageFormat imageFormat)
        {
            var image = new Microsoft.BingAds.V12.CampaignManagement.Image();
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
