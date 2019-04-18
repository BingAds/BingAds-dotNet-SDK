using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to manage images in an account's media library. 
    /// </summary>
    public class ImageMedia : ExampleBase
    {
        // To run this example you'll need to provide your own images.  
        // For required aspect ratios and recommended dimensions please see 
        // Image remarks at https://go.microsoft.com/fwlink/?linkid=872754.

        private const string MediaFilePath = "c:\\dev\\media\\";
        private const string ResponsiveAdMediaFileName = "imageresponsivead1200x628.png";
        private const string ImageAdExtensionMediaFileName = "imageadextension300x200.png";        

        public override string Description
        {
            get { return "Image Media | Campaign Management V13"; }
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
                
                var responsiveAdImageMedia = GetImageMedia(
                    "Image191x100",
                    MediaFilePath + ResponsiveAdMediaFileName, 
                    System.Drawing.Imaging.ImageFormat.Png);
                
                var imageAdExtensionMedia = GetImageMedia(
                    "Image15x10",
                    MediaFilePath + ImageAdExtensionMediaFileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                var addMedia = new Media[]
                {
                    responsiveAdImageMedia,
                    imageAdExtensionMedia
                };
                OutputStatusMessage("Ready to upload image media:");
                CampaignManagementExampleHelper.OutputArrayOfMedia(addMedia);

                OutputStatusMessage("-----\nAddMedia:");
                AddMediaResponse addMediaResponse = await CampaignManagementExampleHelper.AddMediaAsync(
                    accountId: authorizationData.AccountId,
                    media: addMedia);
                long[] mediaIds = addMediaResponse.MediaIds.ToArray();
                OutputStatusMessage("MediaIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(mediaIds);
                
                // Get the media representations to confirm the stored dimensions
                // and get the Url where you can later view or download the media.

                OutputStatusMessage("-----\nGetMediaMetaDataByAccountId:");
                var getResponsiveAdMediaMetaData = (await CampaignManagementExampleHelper.GetMediaMetaDataByAccountIdAsync(
                    mediaEnabledEntities: MediaEnabledEntityFilter.ResponsiveAd,
                    pageInfo: null)).MediaMetaData;
                OutputStatusMessage("MediaMetaData:");
                CampaignManagementExampleHelper.OutputArrayOfMediaMetaData(getResponsiveAdMediaMetaData);

                OutputStatusMessage("-----\nGetMediaMetaDataByAccountId:");
                var getImageAdExtensionMediaMetaData = (await CampaignManagementExampleHelper.GetMediaMetaDataByAccountIdAsync(
                    mediaEnabledEntities: MediaEnabledEntityFilter.ImageAdExtension,
                    pageInfo: null)).MediaMetaData;
                OutputStatusMessage("MediaMetaData:");
                CampaignManagementExampleHelper.OutputArrayOfMediaMetaData(getImageAdExtensionMediaMetaData);

                OutputStatusMessage("-----\nGetMediaMetaDataByIds:");
                var getMediaMetaData = (await CampaignManagementExampleHelper.GetMediaMetaDataByIdsAsync(
                    mediaIds: mediaIds)).MediaMetaData;
                OutputStatusMessage("MediaMetaData:");
                CampaignManagementExampleHelper.OutputArrayOfMediaMetaData(getMediaMetaData);
                
                // Delete the account's media.

                OutputStatusMessage("-----\nDeleteMedia:");
                await CampaignManagementExampleHelper.DeleteMediaAsync(
                    accountId: authorizationData.AccountId,
                    mediaIds: mediaIds);

                foreach (var id in mediaIds)
                {
                    OutputStatusMessage(string.Format("Deleted Media Id {0}", id));
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<EditorialApiFaultDetail> ex)
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
