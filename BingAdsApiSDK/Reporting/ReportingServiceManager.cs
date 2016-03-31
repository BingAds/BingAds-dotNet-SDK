//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using System;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Utilities;

namespace Microsoft.BingAds.Reporting
{
    /// <summary>
    /// Provides high level methods for downloading entities using the Reporting API functionality. Also provides methods for submitting download operations.
    /// </summary>
    /// <remarks>
    /// <see cref="DownloadFileAsync(ReportingDownloadParameters)"/> will submit the download request to the reporting service, 
    /// poll until the status is completed (or returns an error), and downloads the file locally. 
    /// If instead you want to manage the low level details you would first call <see cref="SubmitDownloadAsync"/>, 
    /// wait for the results file to be prepared using either <see cref="ReportingDownloadOperation.GetStatusAsync()"/> 
    /// or <see cref="ReportingDownloadOperation.TrackAsync()"/>, and then download the file with the 
    /// <see cref="ReportingDownloadOperation.DownloadResultFileAsync(string,string,bool)"/> method.
    /// </remarks>
    public class ReportingServiceManager
    {
        private readonly AuthorizationData _authorizationData;

        internal IHttpService HttpService { get; set; }

        internal IZipExtractor ZipExtractor { get; set; }

        internal IFileSystem FileSystem { get; set; }

        internal const int DefaultStatusPollIntervalInMilliseconds = 5000;

        private ApiEnvironment? _apiEnvironment;

        /// <summary>
        /// The time interval in milliseconds between two status polling attempts. The default value is 5000 (5 seconds).
        /// </summary>
        public int StatusPollIntervalInMilliseconds { get; set; }

        /// <summary>
        /// Directory for storing downloaded files if result folder is not specified.
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Initializes a new instance of this class with the specified <see cref="AuthorizationData"/>.
        /// </summary>
        /// <param name="authorizationData">Represents a user who intends to access the corresponding customer and account. </param>
        public ReportingServiceManager(AuthorizationData authorizationData)
            : this(authorizationData, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this class with the specified <see cref="AuthorizationData"/> and <paramref name="apiEnvironment"/>.
        /// </summary>
        /// <param name="authorizationData">Represents a user who intends to access the corresponding customer and account. </param>
        /// <param name="apiEnvironment">Bing Ads API environment</param>
        public ReportingServiceManager(AuthorizationData authorizationData, ApiEnvironment? apiEnvironment)
        {
            if (authorizationData == null)
            {
                throw new ArgumentNullException("authorizationData");
            }

            _authorizationData = authorizationData;

            HttpService = new HttpService();

            ZipExtractor = new ZipExtractor();

            FileSystem = new FileSystem();

            StatusPollIntervalInMilliseconds = DefaultStatusPollIntervalInMilliseconds;

            WorkingDirectory = Path.Combine(Path.GetTempPath(), "BingAdsSDK", "Reporting");

            if (apiEnvironment != null) _apiEnvironment = apiEnvironment.Value;
        }

        /// <summary>
        /// Downloads the specified reporting entities to a local file. 
        /// </summary>
        /// <param name="parameters">Determines various download parameters, for example what entities to download and where the file should be downloaded.
        /// Please see <see cref="ReportingDownloadParameters"/> for more information about available parameters.</param>
        /// <returns>A task that represents the asynchronous operation. The task result will be the local reporting file path.</returns>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception>  
        /// <exception cref="ReportingOperationCouldNotBeCompletedException">Thrown if the reporting operation has failed </exception>
        public Task<string> DownloadFileAsync(ReportingDownloadParameters parameters)
        {
            return DownloadFileAsync(parameters, CancellationToken.None);
        }

        /// <summary>
        /// Downloads the specified reporting entities to a local file. 
        /// </summary>
        /// <param name="parameters">Determines various download parameters, for example what entities to download and where the file should be downloaded.
        /// Please see <see cref="ReportingDownloadParameters"/> for more information about available parameters.</param>
        /// <param name="cancellationToken">Cancellation token that can be used to cancel the tracking of the reporting operation on the client. Doesn't cancel the actual reporting operation on the server.</param>
        /// <returns>A task that represents the asynchronous operation. The task result will be the local reporting file path.</returns>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception>  
        /// <exception cref="ReportingOperationCouldNotBeCompletedException">Thrown if the reporting operation has failed </exception>
        public Task<string> DownloadFileAsync(ReportingDownloadParameters parameters, CancellationToken cancellationToken)
        {
            return DownloadFileAsyncImpl(parameters, cancellationToken);
        }

        /// <summary>
        /// Submits a download request to the Bing Ads reporting service with the specified parameters.
        /// </summary>
        /// <param name="request">Determines various download parameters, for example what entities to download. </param>
        /// <returns>A task that represents the asynchronous operation. The task result will be the submitted download operation.</returns>        
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception>  
        public Task<ReportingDownloadOperation> SubmitDownloadAsync(ReportRequest request)
        {
            return SubmitDownloadAsyncImpl(request);
        }

        private async Task<ReportingDownloadOperation> SubmitDownloadAsyncImpl(ReportRequest request)
        {
            var submitRequest = new SubmitGenerateReportRequest {ReportRequest = request,};
            SubmitGenerateReportResponse response;

            using (var apiService = new ServiceClient<IReportingService>(_authorizationData, _apiEnvironment))
            {
                try
                {
                    response = await apiService.CallAsync((s, r) => s.SubmitGenerateReportAsync(r), submitRequest).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw new CouldNotSubmitReportingDownloadException("Submit download operation failed.", e);
                }               
            }

            return new ReportingDownloadOperation(response.ReportRequestId, _authorizationData, response.TrackingId, _apiEnvironment) {StatusPollIntervalInMilliseconds = StatusPollIntervalInMilliseconds};
        }

        private async Task<string> DownloadFileAsyncImpl(ReportingDownloadParameters parameters, CancellationToken cancellationToken)
        {
            using (var operation = await SubmitDownloadAsyncImpl(parameters.ReportRequest).ConfigureAwait(false))
            {
                await operation.TrackAsync(cancellationToken).ConfigureAwait(false);

                return await DownloadReportingFile(parameters.ResultFileDirectory, parameters.ResultFileName, parameters.OverwriteResultFile, operation).ConfigureAwait(false);
            }
        }

        private async Task<string> DownloadReportingFile(string resultFileDirectory, string resultFileName, bool overwrite, ReportingDownloadOperation operation)
        {
            operation.HttpService = HttpService;
            operation.ZipExtractor = ZipExtractor;
            operation.FileSystem = FileSystem;

            CreateWorkingDirectoryIfNeeded();

            var localFile = await operation.DownloadResultFileAsync(resultFileDirectory ?? WorkingDirectory, resultFileName, true, overwrite).ConfigureAwait(false);

            return localFile;
        }

        private void CreateWorkingDirectoryIfNeeded()
        {
            FileSystem.CreateDirectoryIfDoesntExist(WorkingDirectory);
        }
    }
}