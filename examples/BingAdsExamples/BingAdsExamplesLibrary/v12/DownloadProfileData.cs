using System;
using System.IO;
using System.Net;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to download the comma separated value (CSV) file that contains profile data 
    /// that can be used with Bing Ads profile criteria.
    /// </summary>
    public class DownloadProfileData : ExampleBase
    {
        // The full path to the profile data.

        private const string LocalFile = @"c:\profiledata\profiledata.csv";

        // The language and locale of the profile data available for download.
        // This example uses 'en' (English). 

        private const string LanguageLocale = "en";
        
        public override string Description
        {
            get { return "Download Profile Data | Campaign Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {

            Stream responseStream = null;
            FileStream fileStream = null;

            var fileInfo = new FileInfo(LocalFile);

            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Supported profile types are CompanyName, Industry, and JobFunction

                var getProfileDataFileUrlResponse = await CampaignManagementExampleHelper.GetProfileDataFileUrlAsync(
                    LanguageLocale,
                    ProfileType.CompanyName);

                // Going forward you should track the date and time of the previous download,  
                // and compare it with the last modified time provided by the service.
                var previousSyncTimeUtc = new DateTime(2018, 4, 26, 0, 0, 0, DateTimeKind.Utc);

                var fileUrl = getProfileDataFileUrlResponse.FileUrl;
                var fileUrlExpiryTimeUtc = getProfileDataFileUrlResponse.FileUrlExpiryTimeUtc;
                var lastModifiedTimeUtc = getProfileDataFileUrlResponse.LastModifiedTimeUtc;

                OutputStatusMessage(string.Format("FileUrl: {0}\n", fileUrl));
                OutputStatusMessage(string.Format("FileUrlExpiryTimeUtc: {0}\n", fileUrlExpiryTimeUtc));
                OutputStatusMessage(string.Format("LastModifiedTimeUtc: {0}\n", lastModifiedTimeUtc));

                // Download the file if it was modified since the previous download.
                if (DateTime.Compare(previousSyncTimeUtc, lastModifiedTimeUtc) < 0)
                {
                    DownloadFile(fileUrl, LocalFile);
                }
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
            finally
            {
                if (fileStream != null) fileStream.Close();
                if (responseStream != null) responseStream.Close();
            }
        }

        private void DownloadFile(string fileUrl, string localFile)
        {
            Stream responseStream = null;
            FileStream fileStream = null;

            var fileInfo = new FileInfo(localFile);

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(fileUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    fileStream = new FileStream(fileInfo.FullName, FileMode.Create);
                    responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        responseStream.CopyTo(fileStream);
                    }
                    OutputStatusMessage(string.Format("Downloaded the profile data to {0}.\n", localFile));
                }
            }
            catch (WebException e)
            {
                if (e != null && e.Response != null)
                {
                    OutputStatusMessage("Unexpected status code = " + ((HttpWebResponse)e.Response).StatusCode);
                }
                else
                {
                    OutputStatusMessage("Unexpected Web Exception " + e.Message);
                }
            }
            catch (IOException ex)
            {
                OutputStatusMessage(ex.Message);
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
                if (responseStream != null) responseStream.Close();
            }
        }
    }
}
