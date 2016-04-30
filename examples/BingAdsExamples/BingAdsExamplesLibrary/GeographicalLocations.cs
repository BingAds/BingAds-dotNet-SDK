// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL
// All other rights reserved.

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary
{
    /// <summary>
    /// This example demonstrates how to download the comma separated value (CSV) file that contains geographical location information 
    /// that can be used with Bing Ads location targeting.
    /// </summary>
    public class GeographicalLocations : ExampleBase
    {
        // The full path to the geographical locations file.

        private const string LocalFile = @"c:\geolocations\geolocations.csv";

        // The Url of the geographical locations file available for download.
        // This example uses 'en' (English). Supported locales are 'zh-Hant' (Traditional Chinese), 'en' (English), 'fr' (French), 
        // 'de' (German), 'it' (Italian), 'pt-BR' (Portuguese - Brazil), and 'es' (Spanish). 

        private const string FileUrl = "https://api.bingads.microsoft.com/Api/SystemCodes/v1/en/GeoLocations.csv";

        // The ETag from a previous download, if known. The ETag is not required to run this example. 

        private const string ETag = "f38d4b69cfd9cf1:0";

        public override string Description
        {
            get { return "Targets | Geographical Locations"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {

            Stream responseStream = null;
            FileStream fileStream = null;
            
            var fileInfo = new FileInfo(LocalFile);

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(FileUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                // You can set a request condition on either the last modified time or ETag of the file at the Url. 

                request.Headers.Add(string.Format(@"If-None-Match: ""{0}""", ETag));

                var response = (HttpWebResponse) request.GetResponse();

                PrintETag(response);

            if (response.StatusCode == HttpStatusCode.OK)
                {
                    fileStream = new FileStream(fileInfo.FullName, FileMode.Create);
                    responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        responseStream.CopyTo(fileStream);
                    }
                    OutputStatusMessage(string.Format("Downloaded the geographical locations to {0}.\n", LocalFile));
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    if (((HttpWebResponse) e.Response).StatusCode == HttpStatusCode.NotModified)
                    {
                        PrintETag((HttpWebResponse) e.Response);
                        OutputStatusMessage("The locations file has not been modified since last download.\n");
                    }
                    else
                    {
                        OutputStatusMessage("Unexpected status code = " + ((HttpWebResponse)e.Response).StatusCode);
                    }
                }
                else
                    OutputStatusMessage("Unexpected Web Exception " + e.Message);
            }
            catch (IOException ex)
            {
                OutputStatusMessage(ex.Message);
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

        private void PrintETag(HttpWebResponse response)
        {
            for (int i = 0; i < response.Headers.Count; i++)
            {
                if (response.Headers.Keys[i] == "ETag")
                {
                    OutputStatusMessage(string.Format("Current {0}: {1}", response.Headers.Keys[i], response.Headers[i]));
                }
            }
        }
    }
}