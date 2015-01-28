// Copyright 2014 Microsoft Corporation 

// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 

//    http://www.apache.org/licenses/LICENSE-2.0 

// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.Bulk;

namespace BingAdsExamples
{
    /// <summary>
    /// This example demonstrates how to download and upload the entities of one or more campaigns in the background 
    /// using the low level Bulk service operations, for example DownloadCampaignsByAccountIds.
    /// </summary>
    public class BulkServiceClientDownloadUpload : ExampleBase
    {
        public static ServiceClient<IBulkService> Service;

        // The full path to the bulk file.

        private const string BulkFilePath = @"c:\bulk\campaigns.zip";

        // The full path to the extracted bulk file.

        private const string ExtractedFilePath = @"c:\bulk\extracted\Accounts.csv";

        // The full path to the upload result file.

        private const string UploadResultFilePath = @"c:\bulk\uploadresults.zip";

        // Specifies the bulk file format. 

        private const DownloadFileType FileFormat = DownloadFileType.Csv;

        // Specify credentials if using PasswordAuthentication

        private const string UserName = "<UserNameGoesHere>";
        private const string Password = "<PasswordGoesHere>";


        public override string Description
        {
            get { return "BulkServiceClient | Download and Upload using the low level service operations"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<IBulkService>(authorizationData);

                // Use the bulk service to download a bulk file.
                #region Download

                const DataScope dataScope = DataScope.EntityData;

                const BulkDownloadEntity entities = BulkDownloadEntity.Ads |
                                                    BulkDownloadEntity.AdGroups |
                                                    BulkDownloadEntity.Campaigns |
                                                    BulkDownloadEntity.Keywords
                                                    ;

                const string formatVersion = "3.0";

                object lastSyncTimeInUtc = GetLastSyncTime(ExtractedFilePath);

                // You may include a non-null date range if the lastSyncTime is null, and the data scope includes   
                // either EntityPerformanceData, BidSuggestionsData, or QualityScoreData.  

                /*
                var performanceStatsDateRange = new PerformanceStatsDateRange
                    {
                        CustomDateRangeEnd = new Date
                            {
                                Day = DateTime.Now.Day,
                                Month = DateTime.Now.Month,
                                Year = DateTime.Now.Year
                            },
                        CustomDateRangeStart = new Date
                            {
                                Day = DateTime.Now.Day,
                                Month = DateTime.Now.Month,
                                Year = DateTime.Now.Year - 1
                            }
                    };
                 */

                // GetDownloadRequestId helper method calls the corresponding Bing Ads service operation 
                // to request the download identifier.
                string downloadRequestId = await GetDownloadRequestId(
                    authorizationData.AccountId,
                    dataScope,
                    entities,
                    formatVersion,
                    (DateTime?)lastSyncTimeInUtc,
                    null
                    );

                OutputStatusMessage(String.Format("Download Request Id: " + downloadRequestId));

                var waitTime = new TimeSpan(0, 0, 5);
                var downloadSuccess = false;

                // This sample polls every 30 seconds up to 5 minutes.
                // In production you may poll the status every 1 to 2 minutes for up to one hour.
                // If the call succeeds, stop polling. If the call or 
                // download fails, the call throws a fault.

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(waitTime);

                    // GetDownloadRequestStatus helper method calls the corresponding Bing Ads service operation 
                    // to get the download status.
                    string downloadRequestStatus = await GetDownloadRequestStatus(downloadRequestId);

                    if ((downloadRequestStatus != null) && (downloadRequestStatus == "Completed"))
                    {
                        downloadSuccess = true;
                        break;
                    }
                }

                if (downloadSuccess)
                {
                    // GetDownloadUrl helper method calls the corresponding Bing Ads service operation 
                    // to get the download Url.
                    string downloadUrl = await GetDownloadUrl(downloadRequestId);
                    OutputStatusMessage(String.Format("Downloading from {0}\n.", downloadUrl));
                    DownloadFile(downloadUrl, BulkFilePath);
                    OutputStatusMessage(String.Format("The download file was written to {0}.", BulkFilePath));
                }
                else // Pending
                {
                    OutputStatusMessage(String.Format("The request is taking longer than expected.\n" +
                                      "Save the download request ID ({0}) and try again later.", downloadRequestId));
                }

                #endregion Download

                // You may unzip and update the downloaded bulk file or prepare a new file elsewhere.
                // Changes to the bulk file are not shown here.

                DecompressFile(BulkFilePath, ExtractedFilePath);
                CompressFile(ExtractedFilePath, BulkFilePath);


                // Use the bulk service to upload a bulk file.
                #region Upload

                const ResponseMode responseMode = ResponseMode.ErrorsAndResults;

                var uploadResponse = await GetBulkUploadUrl(responseMode, authorizationData.AccountId);

                string uploadRequestId = uploadResponse.RequestId;
                string uploadUrl = uploadResponse.UploadUrl;

                OutputStatusMessage(String.Format("Uploading file from {0}.", BulkFilePath));
                OutputStatusMessage(String.Format("Upload Request Id: {0}", uploadRequestId));
                OutputStatusMessage(String.Format("Upload Url: {0}", uploadUrl));

                UploadFile(authorizationData, uploadUrl, BulkFilePath);

                var uploadSuccess = false;

                // This sample polls every 30 seconds up to 5 minutes.
                // In production you may poll the status every 1 to 2 minutes for up to one hour.
                // If the call succeeds, stop polling. If the call or 
                // download fails, the call throws a fault.

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(waitTime);

                    // GetUploadRequestStatus helper method calls the corresponding Bing Ads service operation 
                    // to get the upload status.
                    string uploadRequestStatus = await GetUploadRequestStatus(uploadRequestId);

                    if ((uploadRequestStatus != null) && ((uploadRequestStatus == "Completed")
                        || (uploadRequestStatus == "CompletedWithErrors")))
                    {
                        uploadSuccess = true;
                        break;
                    }
                }

                if (uploadSuccess)
                {
                    // GetUploadResultFileUrl helper method calls the corresponding Bing Ads service operation 
                    // to get the upload result file Url.
                    string uploadResultFileUrl = await GetUploadResultFileUrl(uploadRequestId);
                    DownloadFile(uploadResultFileUrl, UploadResultFilePath);
                    OutputStatusMessage(String.Format("The upload result file was written to {0}.", UploadResultFilePath));
                }
                else // Pending
                {
                    OutputStatusMessage(String.Format("The request is taking longer than expected.\n" +
                                        "Save the upload ID ({0}) and try again later.", uploadRequestId));
                }

                #endregion Upload

            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (WebException ex)
            {
                OutputStatusMessage(ex.Message);

                if (ex.Response != null)
                    OutputStatusMessage("HTTP status code: " + ((HttpWebResponse)ex.Response).StatusCode);
            }
            catch (IOException ex)
            {
                OutputStatusMessage(ex.Message);
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        // GetDownloadRequestId helper method calls the DownloadCampaignsByAccountIds service operation 
        // to request the download identifier.
        private async Task<string> GetDownloadRequestId(long accountId, DataScope dataScope, BulkDownloadEntity entities,
            string formatVersion, DateTime? lastSyncTime, PerformanceStatsDateRange dateRange)
        {
            var request = new DownloadCampaignsByAccountIdsRequest
            {
                AccountIds = new List<long> { accountId },
                DataScope = dataScope,
                DownloadFileType = FileFormat,
                Entities = entities,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTime,
                PerformanceStatsDateRange = dateRange
            };

            return (await Service.CallAsync(
                (s, r) => s.DownloadCampaignsByAccountIdsAsync(r), request)).DownloadRequestId;
        }

        // GetDownloadRequestStatus helper method calls the GetDetailedBulkDownloadStatus service operation 
        // to get the download request status.
        private async Task<string> GetDownloadRequestStatus(string requestId)
        {
            var request = new GetDetailedBulkDownloadStatusRequest
            {
                RequestId = requestId
            };

            return (await Service.CallAsync(
                (s, r) => s.GetDetailedBulkDownloadStatusAsync(r), request)).RequestStatus;
        }

        // GetDownloadUrl helper method calls the GetDetailedBulkDownloadStatus service operation 
        // to get the download Url.
        private async Task<string> GetDownloadUrl(string requestId)
        {
            var request = new GetDetailedBulkDownloadStatusRequest
            {
                RequestId = requestId
            };

            return (await Service.CallAsync(
                (s, r) => s.GetDetailedBulkDownloadStatusAsync(r), request)).ResultFileUrl;
        }

        // GetBulkUploadUrl helper method calls the GetBulkUploadUrl service operation 
        // to request the upload identifier and upload Url via GetBulkUploadUrlResponse.
        private async Task<GetBulkUploadUrlResponse> GetBulkUploadUrl(ResponseMode responseMode, long accountId)
        {
            var request = new GetBulkUploadUrlRequest
            {
                ResponseMode = responseMode,
                AccountId = accountId
            };

            return (await Service.CallAsync(
                (s, r) => s.GetBulkUploadUrlAsync(r), request));
        }

        // GetUploadRequestStatus helper method calls the GetDetailedBulkUploadStatus service operation 
        // to get the upload request status.
        private async Task<string> GetUploadRequestStatus(string requestId)
        {
            var request = new GetDetailedBulkUploadStatusRequest
            {
                RequestId = requestId
            };

            return (await Service.CallAsync(
                (s, r) => s.GetDetailedBulkUploadStatusAsync(r), request)).RequestStatus;
        }

        // GetUploadResultFileUrl helper method calls the GetDetailedBulkUploadStatus service operation 
        // to get the upload result file Url.
        private async Task<string> GetUploadResultFileUrl(string requestId)
        {
            var request = new GetDetailedBulkUploadStatusRequest
            {
                RequestId = requestId
            };

            return (await Service.CallAsync(
                (s, r) => s.GetDetailedBulkUploadStatusAsync(r), request)).ResultFileUrl;
        }

        // Using the URL returned by the GetBulkUploadUrl operation, 
        // POST the bulk file using a HTTP client. 
        private static void UploadFile(
            AuthorizationData authorizationData,
            string uploadUrl,
            string filePath)
        {
            var webClient = new WebClient();

            var authCodeGrantAuthentication = authorizationData.Authentication as OAuthDesktopMobileAuthCodeGrant;
            if (authCodeGrantAuthentication != null)
            {
                webClient.Headers.Add("AuthenticationToken", authCodeGrantAuthentication.OAuthTokens.AccessToken);
            }
            else
            {
                var implicitCodeGrantAuthentication = authorizationData.Authentication as OAuthDesktopMobileImplicitGrant;
                if (implicitCodeGrantAuthentication != null)
                {
                    webClient.Headers.Add("AuthenticationToken", implicitCodeGrantAuthentication.OAuthTokens.AccessToken);
                }
                else
                {
                    var passwordAuthentication =
                        authorizationData.Authentication as PasswordAuthentication;
                    if (passwordAuthentication != null)
                    {
                        // Global UserName and Password will be used.
                        webClient.Headers.Add("UserName", UserName);
                        webClient.Headers.Add("Password", Password);
                    }
                }
            }

            webClient.Headers.Add("DeveloperToken", authorizationData.DeveloperToken);
            webClient.Headers.Add("CustomerId", authorizationData.CustomerId.ToString(CultureInfo.InvariantCulture));
            webClient.Headers.Add("CustomerAccountId", authorizationData.AccountId.ToString(CultureInfo.InvariantCulture));

            webClient.UploadFile(uploadUrl, filePath);
        }

        // Using the URL returned by the GetDetailedBulkDownloadStatus operation,
        // send an HTTP request to get the download data and write it 
        // to the specified ZIP file.

        private static void DownloadFile(string downloadUrl, string filePath)
        {
            var request = (HttpWebRequest)WebRequest.Create(downloadUrl);
            var response = (HttpWebResponse)request.GetResponse();
            var fileInfo = new FileInfo(filePath);
            Stream responseStream = null;
            BinaryWriter binaryWriter = null;
            BinaryReader binaryReader = null;

            // If the folders in the specified path do not exist, create them.

            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            // Create the ZIP file.

            var fileStream = new FileStream(fileInfo.FullName, FileMode.Create);

            try
            {
                responseStream = response.GetResponseStream();
                binaryWriter = new BinaryWriter(fileStream);
                if (responseStream != null) binaryReader = new BinaryReader(responseStream);

                const int bufferSize = 100 * 1024;

                while (true)
                {
                    // Read data from the download URL.

                    if (binaryReader != null)
                    {
                        byte[] buffer = binaryReader.ReadBytes(bufferSize);

                        // Write the campaign data to file.

                        binaryWriter.Write(buffer);

                        // If the end of the data is reached, break out of the loop.

                        if (buffer.Length != bufferSize)
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                fileStream.Close();
                if (responseStream != null) responseStream.Close();
                if (binaryReader != null) binaryReader.Close();
                if (binaryWriter != null) binaryWriter.Close();
            }
        }

        // Decompresses a ZIP Archive and writes the contents to the specified file path.

        private static void DecompressFile(string fromZipArchive, string toExtractedFile)
        {
            var fileInfo = new FileInfo(toExtractedFile);

            // If the folders in the specified path do not exist, create them.

            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            using (ZipArchive archive = ZipFile.OpenRead(fromZipArchive))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    entry.ExtractToFile(toExtractedFile, true);
                }
            }
        }

        // Compresses a bulk file to a ZIP Archive.

        private static void CompressFile(string fromExtractedFile, string toZipArchive)
        {
            using (ZipArchive archive = ZipFile.Open(toZipArchive, ZipArchiveMode.Update))
            {
                if (fromExtractedFile != null)
                {
                    archive.GetEntry(Path.GetFileName(fromExtractedFile)).Delete();
                    archive.CreateEntryFromFile(fromExtractedFile, Path.GetFileName(fromExtractedFile));
                }
            }
        }

        // Get the time stamp of the last download if available. 
        // The Sync Time column contains the time stamp. 

        private DateTime? GetLastSyncTime(string path)
        {
            DateTime? lastSyncTime = null;
            char columnDelimiter;

            switch (FileFormat)
            {
                case DownloadFileType.Tsv:
                    columnDelimiter = '\t';
                case DownloadFileType.Csv:
                    columnDelimiter = ',';
                    break;
            }

            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    int syncTimeColumn = 0;
                    string[] fields = null;

                    // The first record contains column header information, for example "Sync Time".
                    string record = reader.ReadLine();

                    if (record != null)
                    {
                        fields = record.Split(columnDelimiter);
                        int column = 0;

                        // Find the Sync Time column.
                        do
                        {
                            syncTimeColumn = (fields[column] == "Sync Time") ? column : 0;
                        } while (syncTimeColumn == 0 && ++column < fields.Length);
                    }

                    // Look for the Account record after any other metadata.

                    bool isAccount = false;

                    do
                    {
                        record = reader.ReadLine();
                        if (record != null)
                        {
                            fields = record.Split(columnDelimiter);

                            if (fields[0] == "Account")
                            {
                                fields = record.Split(columnDelimiter);
                                lastSyncTime = (fields[syncTimeColumn] != "")
                                                   ? Convert.ToDateTime(fields[syncTimeColumn])
                                                   : (DateTime?)null;
                                isAccount = true;
                            }
                        }
                    } while (!isAccount);
                    OutputStatusMessage(String.Format(fields[syncTimeColumn]));
                }
            }

            return lastSyncTime;
        }
    }
}
