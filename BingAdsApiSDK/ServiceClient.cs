//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
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
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V13.AdInsight;
using Microsoft.BingAds.V13.CustomerBilling;
using Microsoft.BingAds.V13.CustomerManagement;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Provides an interface for calling the methods of the specified Bing Ads service.
    /// </summary>
    /// <typeparam name="TService">
    /// The Bing Ads service interface that should be called.
    /// </typeparam>
    /// <remarks>Valid values of <typeparamref name="TService"/> are:
    ///     <para>IAdInsightService</para>
    ///     <para>IBulkService</para>
    ///     <para>ICampaignManagementService</para>
    ///     <para>ICustomerBillingService</para>
    ///     <para>ICustomerManagementService</para>
    ///     <para>IReportingService</para>
    /// </remarks>
    public class ServiceClient<TService> : IDisposable
        where TService : class
    {
        private readonly WcfServiceClient<TService> _wcfServiceClient;

        private readonly RestServiceClient _restServiceClient;

        private bool DisableRestApi
        {
            get
            {
                if (AppContext.TryGetSwitch($"Switch.BingAds.{typeof(TService).Name}.DisableRestApi", out var isSwitchOn) && isSwitchOn)
                {
                    Events.Log.RestApiDisable(Activity.Current?.Id, typeof(TService).Name, "AppContext switch");

                    return true;
                }

                var envVarValue = Environment.GetEnvironmentVariable($"BINGADS_{typeof(TService).Name}.DisableRestApi");

                if (envVarValue != null && bool.TryParse(envVarValue, out var disableRestApi) && disableRestApi)
                {
                    Events.Log.RestApiDisable(Activity.Current?.Id, typeof(TService).Name, "Environment variable");

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Represents a user who intends to access the corresponding customer and account.
        /// </summary>
        public AuthorizationData AuthorizationData => _wcfServiceClient.AuthorizationData;

        /// <summary>
        /// Gets or sets a value indicating whether OAuth access and refresh tokens should be refreshed automatically upon access token expiration.
        /// </summary>
        /// <remarks>
        /// This value is <value>true</value> be default.
        /// </remarks>
        public bool RefreshOAuthTokensAutomatically
        {
            get { return _wcfServiceClient.RefreshOAuthTokensAutomatically; }
            set
            {
                _wcfServiceClient.RefreshOAuthTokensAutomatically = value;
                _restServiceClient.RefreshOAuthTokensAutomatically = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of this class with the specified <see cref="AuthorizationData"/>.
        /// </summary>
        /// <param name="authorizationData">Represents a user who intends to access the corresponding customer and account.</param>
        public ServiceClient(AuthorizationData authorizationData): this(authorizationData, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of this class with the specified <see cref="AuthorizationData"/>.
        /// </summary>
        /// <param name="authorizationData">Represents a user who intends to access the corresponding customer and account.</param>
        /// <param name="environment">Bing Ads API environment</param>
        public ServiceClient(AuthorizationData authorizationData, ApiEnvironment? environment)
        {
            _wcfServiceClient = new WcfServiceClient<TService>(authorizationData, environment);

            _restServiceClient = new RestServiceClient(authorizationData, environment);
        }

        /// <summary>
        /// Calls the Bing Ads service and returns the response of the corresponding <paramref name="method"/> of the Bing Ads service. 
        /// </summary>
        /// <typeparam name="TRequest">The type of the Bing Ads service request message.</typeparam>
        /// <typeparam name="TResponse">The type of the Bing Ads service response message.</typeparam>
        /// <param name="method">A delegate representing the Bing Ads service operation that should be called.</param>
        /// <param name="request">The request message object for the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result will be the Bing Ads service response.</returns>
        /// <remarks>
        /// <para>
        /// The header elements that the method sets will differ depending on the type of authentication specified in 
        /// <see cref="AuthorizationData"/> and the requirements of the service. For example if you use one of the OAuth classes, 
        /// the AuthenticationToken will be set by this method, whereas the UserName and Password headers will remain empty. 
        /// Some services such as Customer Management do not accept CustomerId and CustomerAccountId headers, 
        /// so they will be ignored if you specified them in the <see cref="AuthorizationData"/> object. 
        /// </para>        
        /// <para>
        /// If you are using one of the OAuth classes for authentication and the access token has expired 
        /// (error 109 is returned from the API), this method will try to refresh 
        /// it using the current refresh token and retry the request with the new access token.
        /// </para>
        /// </remarks>
        /// <exception cref="FaultException{TDetail}">Thrown if a fault is returned from the Bing Ads service.</exception>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be refreshed due to an error received from the Microsoft Account authorization server.</exception>  
        public async Task<TResponse> CallAsync<TRequest, TResponse>(
            Func<TService, TRequest, Task<TResponse>> method, TRequest request)
            where TRequest : class
        {
            if (DisableRestApi)
            {
                return await _wcfServiceClient.CallAsync(method, request).ConfigureAwait(false);
            }

            var restApiResponse = await _restServiceClient.CallAsync(method, request).ConfigureAwait(false);

            if (restApiResponse == null)
            {
                return await _wcfServiceClient.CallAsync(method, request).ConfigureAwait(false);
            }

            return restApiResponse;
        }

        public TService Service => (TService)_restServiceClient.CreateService(typeof(TService));
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>You should use this method when finished with an instance of <see cref="ServiceClient{TService}"/>.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Disposes of the channel factory if set to true.</param>
        /// <remarks>You should use this method when finished with an instance of <see cref="ServiceClient{TService}"/>.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_wcfServiceClient != null)
                {
                    _wcfServiceClient.Dispose();
                }
            }
        }
    }
}
