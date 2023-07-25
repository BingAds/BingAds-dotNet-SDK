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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds
{
    internal class RestServiceClient
    {
        private readonly AuthorizationData _authorizationData;

        private readonly IServiceClientFactory _serviceClientFactory;

        private ApiEnvironment _environment;

        public AuthorizationData AuthorizationData => _authorizationData;

        private bool AccessTokenExpired => (_authorizationData.Authentication as OAuthAuthorization)?.OAuthTokens?.AccessTokenExpired ?? false;

        public bool RefreshOAuthTokensAutomatically { get; set; }

        private static readonly JsonSerializerOptions SerializerOptions;

        static RestServiceClient()
        {
            SerializerOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { EntityModifiers.CustomizeEntities }
                },
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
            };

            SerializerOptions.Converters.Add(new JsonStringEnumConverter());

            AllPolymorphicConverters.AddTo(SerializerOptions.Converters);
        }

        public RestServiceClient(AuthorizationData authorizationData, ApiEnvironment? environment)
        {
            if (authorizationData == null)
            {
                throw new ArgumentNullException("authorizationData");
            }

            _authorizationData = authorizationData;

            _serviceClientFactory = ServiceClientFactoryFactory.CreateServiceClientFactory();

            DetectApiEnvironment(authorizationData, environment);

            RefreshOAuthTokensAutomatically = true;
        }

        private void DetectApiEnvironment(AuthorizationData authorizationData, ApiEnvironment? environment)
        {
            var oauth = authorizationData.Authentication as OAuthAuthorization;
            if (oauth != null)
            {
                environment = oauth.Environment;
            }

            if (environment == null)
            {
                _environment = ApiEnvironment.Production;
            }
            else
            {
                _environment = environment.Value;
            }
        }

        public async Task<TResponse> CallAsync<TResponse, TService, TRequest>(Func<TService, TRequest, Task<TResponse>> method, TRequest request) where TService : class where TRequest : class
        {
            var service = (TService)CreateService(typeof(TService));

            var response = await method(service, request);
            
            return response;
        }

        internal async Task<TResponse> CallServiceAsync<TResponse>(string methodName, object request, Type serviceType)
        {
            var customerId = _authorizationData.CustomerId;
            var accountId = _authorizationData.AccountId;
            var devToken = _authorizationData.DeveloperToken;

            var mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.CampaignManagementServiceActionMethods);

            if (mappedRestApiMethod == null)
            {
                throw new InvalidOperationException($"Unknown method '{methodName}'");
            }

            var restApiMethodInfo = mappedRestApiMethod.Value;

            SetCommonRequestFieldsFromUserData(request);

            var requestJson = JsonSerializer.Serialize(request, SerializerOptions);

            var requestUri = new Uri($"{restApiMethodInfo.EntityName}{restApiMethodInfo.Action}", UriKind.Relative);

            var requestMessage = new HttpRequestMessage
            {
                Method = restApiMethodInfo.HttpMethod,
                RequestUri = requestUri,
                Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
            };

            requestMessage.Headers.Add("CustomerId", customerId.ToString());

            requestMessage.Headers.Add("CustomerAccountId", accountId.ToString());

            requestMessage.Headers.Add("DeveloperToken", devToken);

            requestMessage.Headers.Add("ApplicationToken", devToken);

            var needToRefreshToken = RefreshOAuthTokensAutomatically && AccessTokenExpired;

            if (needToRefreshToken)
            {
                await RefreshAccessToken().ConfigureAwait(false);
            }

            _authorizationData.Authentication.AddAuthenticationHeaders(requestMessage.Headers);

            var response = await _serviceClientFactory.GetRestHttpClientProvider().GetHttpClient(serviceType, _environment).SendAsync(requestMessage);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var faultDetail = JsonSerializer.Deserialize<ApplicationFault>(responseContent, SerializerOptions);

                switch (faultDetail)
                {
                    case ApiFaultDetail apiFaultDetail:
                        throw new FaultException<ApiFaultDetail>(apiFaultDetail);
                    case EditorialApiFaultDetail editorialApiFaultDetail:
                        throw new FaultException<EditorialApiFaultDetail>(editorialApiFaultDetail);
                    case AdApiFaultDetail adApiFaultDetail:
                        throw new FaultException<AdApiFaultDetail>(adApiFaultDetail);
                    default:
                        throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                }
            }

            return JsonSerializer.Deserialize<TResponse>(responseContent, SerializerOptions);
        }

        private async Task RefreshAccessToken()
        {
            var oauthWithCode = _authorizationData.Authentication as OAuthWithAuthorizationCode;

            if (oauthWithCode != null)
            {
                await oauthWithCode.RefreshAccessTokenAsync().ConfigureAwait(false);
            }
        }

        private void SetCommonRequestFieldsFromUserData(object request)
        {
            SetRequestFieldIfNeeded(request, "AccountId", _authorizationData.AccountId);

            SetRequestFieldIfNeeded(request, "CustomerAccountId", _authorizationData.AccountId);

            SetRequestFieldIfNeeded(request, "CustomerId", _authorizationData.CustomerId);

            SetRequestFieldIfNeeded(request, "DeveloperToken", _authorizationData.DeveloperToken, alwaysOverwriteRequestField: true);

            DevTokenBehavior.Instance.DevToken = _authorizationData.DeveloperToken;
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

        internal object CreateService(Type serviceType)
        {
            if (serviceType == typeof(ICampaignManagementService))
            {
                return new CampaignManagementService(this, serviceType);
            }

            throw new NotImplementedException();
        }
    }
}