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
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml.Linq;
using Microsoft.BingAds.Internal;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Provides an interface for calling the methods of the specified Bing Ads service.
    /// </summary>
    /// <typeparam name="TService">
    /// The Bing Ads service interface that should be called.
    /// </typeparam>
    /// <remarks>Valid values of <typeparamref name="TService"/> are:
    ///     <para>IAdIntelligenceService</para>
    ///     <para>IBulkService</para>
    ///     <para>ICampaignManagementService</para>
    ///     <para>ICustomerBillingService</para>
    ///     <para>ICustomerManagementService</para>
    ///     <para>IOptimizerService</para>
    ///     <para>IReportingService</para>
    /// </remarks>
    public class ServiceClient<TService> : IDisposable
        where TService : class
    {
        private readonly IServiceClientFactory _serviceClientFactory;

        private IChannelFactory<TService> _channelFactory;

        private readonly AuthorizationData _authorizationData;

        private const string EnvironmentAppSetting = "BingAdsEnvironment";

        private ApiEnvironment _environment;

        /// <summary>
        /// Represents a user who intends to access the corresponding customer and account.
        /// </summary>
        public AuthorizationData AuthorizationData
        {
            get { return _authorizationData; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether OAuth access and refresh tokens should be refreshed automatically upon access token expiration.
        /// </summary>
        /// <remarks>
        /// This value is <value>true</value> be default.
        /// </remarks>
        public bool RefreshOAuthTokensAutomatically { get; set; }

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
            if (authorizationData == null)
            {
                throw new ArgumentNullException("authorizationData");
            }

            _authorizationData = authorizationData;

            _serviceClientFactory = ServiceClientFactoryFactory.CreateServiceClientFactory();

            if (!_serviceClientFactory.SupportedServiceTypes.Contains(typeof(TService)))
            {
                throw new InvalidOperationException(ErrorMessages.ApiServiceTypeMustBeInterface);
            }
            if (environment == null)
            {
                var envSetting = HostingEnvironment.IsHosted ?
                WebConfigurationManager.AppSettings[EnvironmentAppSetting] :
                ConfigurationManager.AppSettings[EnvironmentAppSetting];

                if (!Enum.TryParse(envSetting, out _environment))
                {
                    _environment = ApiEnvironment.Production;
                }
            }
            else
            {
                _environment = environment.Value;
            }           

            _channelFactory = _serviceClientFactory.CreateChannelFactory<TService>(_environment);

            RefreshOAuthTokensAutomatically = true;
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
            ValidateObjectStateAndParameters(method, request);

            await RequestAccessTokenIfNeeded().ConfigureAwait(false);

            _authorizationData.Authentication.SetAuthenticationFieldsOnApiRequestObject(request);

            SetCommonRequestFieldsFromUserData(request);

            var client = _serviceClientFactory.CreateServiceFromFactory(_channelFactory);

            var needToRefreshToken = false;

            do
            {
                if (needToRefreshToken)
                {
                    await RefreshAccessToken().ConfigureAwait(false);

                    _authorizationData.Authentication.SetAuthenticationFieldsOnApiRequestObject(request);
                }

                try
                {
                    var response = await method(client, request).ConfigureAwait(false);

                    return response;
                }
                catch (FaultException ex)
                {
                    // If needToRefreshToken == true, it means one token refresh has alreay been attempted, and we shouldn't do it again
                    if (needToRefreshToken == false && RefreshOAuthTokensAutomatically && IsExpiredTokenException(ex))
                    {
                        needToRefreshToken = true;
                    }
                    else
                    {
                        ((IClientChannel)client).Abort();

                        throw;
                    }
                }
                catch (CommunicationException)
                {
                    ((IClientChannel)client).Abort();

                    throw;
                }
                catch (TimeoutException)
                {
                    ((IClientChannel)client).Abort();

                    throw;
                }
                catch (Exception)
                {
                    ((IClientChannel)client).Abort();

                    throw;
                }
            } while (true);
        }

        private async Task RequestAccessTokenIfNeeded()
        {
            var oAuthWithCode = _authorizationData.Authentication as OAuthWithAuthorizationCode;

            if (oAuthWithCode != null && oAuthWithCode.OAuthTokens != null && oAuthWithCode.OAuthTokens.AccessToken == null)
            {
                await oAuthWithCode.RefreshAccessTokenAsync().ConfigureAwait(false);
            }
        }

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
                if (_channelFactory != null)
                {
                    try
                    {
                        if (_channelFactory.State != CommunicationState.Faulted)
                        {
                            _channelFactory.Close();
                        }
                        else
                        {
                            _channelFactory.Abort();
                        }
                    }
                    catch (CommunicationException)
                    {
                        if (_channelFactory != null)
                        {
                            _channelFactory.Abort();
                        }
                    }
                    catch (TimeoutException)
                    {
                        if (_channelFactory != null)
                        {
                            _channelFactory.Abort();
                        }
                    }
                    catch (Exception)
                    {
                        if (_channelFactory != null)
                        {
                            _channelFactory.Abort();
                        }

                        throw;
                    }
                    finally
                    {
                        _channelFactory = null;
                    }
                }
            }
        }

        private void ValidateObjectStateAndParameters(object method, object request)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            _authorizationData.Validate();

            if (_channelFactory == null)
            {
                throw new ObjectDisposedException("ServiceClient");
            }
        }

        private void SetCommonRequestFieldsFromUserData(object request)
        {
            SetRequestFieldIfNeeded(request, "AccountId", _authorizationData.AccountId);

            SetRequestFieldIfNeeded(request, "CustomerAccountId", _authorizationData.AccountId);

            SetRequestFieldIfNeeded(request, "CustomerId", _authorizationData.CustomerId);

            SetRequestFieldIfNeeded(request, "DeveloperToken", _authorizationData.DeveloperToken, alwaysOverwriteRequestField: true);
        }

        private void SetRequestFieldIfNeeded<TAuthData>(object request, string name, TAuthData authorizationDataValue, bool alwaysOverwriteRequestField = false)
        {
            if (authorizationDataValue.Equals(default(TAuthData)))
            {
                return;
            }

            var field = request.GetType().GetField(name);

            if (field == null)
            {
                return;
            }

            dynamic requestValue = field.GetValue(request);

            if (alwaysOverwriteRequestField || requestValue == null || requestValue.Equals(0))
            {
                var targetType = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;

                field.SetValue(request, Convert.ChangeType(authorizationDataValue, targetType));
            }
        }

        private async Task RefreshAccessToken()
        {
            var oauthWithCode = _authorizationData.Authentication as OAuthWithAuthorizationCode;

            if (oauthWithCode != null)
            {
                await oauthWithCode.RefreshAccessTokenAsync().ConfigureAwait(false);
            }
        }

        private bool IsExpiredTokenException(FaultException exception)
        {
            var fault = exception.CreateMessageFault();

            if (!fault.HasDetail)
            {
                return false;
            }

            var root = fault.GetDetail<XElement>();

            if (root == null)
            {
                return false;
            }

            XNamespace ns = "https://adapi.microsoft.com";

            var errors = root.Element(ns + "Errors");

            if (errors == null)
            {
                return false;
            }

            return errors.Elements(ns + "AdApiError").Any(error =>
            {
                var code = error.Element(ns + "Code");

                return code != null && code.Value == "109";
            });
        }
    }
}
