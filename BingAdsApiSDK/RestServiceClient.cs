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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Microsoft.BingAds.Internal;

namespace Microsoft.BingAds
{
    internal class RestServiceClient
    {
        private readonly AuthorizationData _authorizationData;

        private readonly IServiceClientFactory _serviceClientFactory;

        private ApiEnvironment _environment;

        public bool RefreshOAuthTokensAutomatically { get; set; }

        private readonly ConcurrentDictionary<Type, object> _services = new();

        private static readonly JsonSerializerOptions SerializerOptions;

        private static readonly ConcurrentDictionary<Type, DateTimeOffset> RetryAfter = new();

        static RestServiceClient()
        {
            SerializerOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                    {
                        RequireAllProperties
                    }
                },
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
            };

            SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            SerializerOptions.Converters.Add(new LongToStringConverter());

            Func<string, Exception> unsupportedTypeValueException = x => new UnsupportedTypeValueException(x);

            Microsoft.BingAds.RestApiGeneration.Apply(SerializerOptions, unsupportedTypeValueException);
        }

        private static void RequireAllProperties(JsonTypeInfo jsonTypeInfo)
        {
            if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
            {
                return;
            }

            var isDataContract = jsonTypeInfo.Type.GetCustomAttributes(typeof(DataContractAttribute)).Any() ||
                                 jsonTypeInfo.Type.Name.EndsWith("Request", StringComparison.InvariantCulture) ||
                                 jsonTypeInfo.Type.Name.EndsWith("Response", StringComparison.InvariantCulture);

            if (!isDataContract)
            {
                return;
            }

            for (int i = 0; i < jsonTypeInfo.Properties.Count; i++)
            {
                if (jsonTypeInfo.Properties[i].Set != null)
                {
                    jsonTypeInfo.Properties[i].IsRequired = true;
                }
            }
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
            ValidateObjectStateAndParameters(method, request);

            var service = (TService)CreateService(typeof(TService));

            var response = await method(service, request).ConfigureAwait(false);

            return response;
        }

        internal async Task<TResponse> CallServiceAsync<TResponse>(string methodName, object request, Type serviceType, Action<TResponse, string> setTrackingId)
        {
            if (RetryAfter.TryGetValue(serviceType, out var retryAfter) &&
                DateTimeOffset.UtcNow < retryAfter)
            {
                return default;
            }

            RestMethodInfo? mappedRestApiMethod = null;

            if (serviceType == typeof(V13.CampaignManagement.ICampaignManagementService))
            {
                mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.CampaignManagementServiceActionMethods);
            }
            else if (serviceType == typeof(V13.Bulk.IBulkService))
            {
                mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.BulkServiceActionMethods);
            }
            else if (serviceType == typeof(V13.Reporting.IReportingService))
            {
                mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.ReportingServiceActionMethods);
            }
            else if (serviceType == typeof(V13.AdInsight.IAdInsightService))
            {
                mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.AdInsightServiceActionMethods);
            }
            else if (serviceType == typeof(V13.CustomerManagement.ICustomerManagementService))
            {
                mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.AdInsightServiceActionMethods);
            }
            else if (serviceType == typeof(V13.CustomerBilling.ICustomerBillingService))
            {
                mappedRestApiMethod = RestApiMethodMapper.Map(methodName, RestApiMethodMapper.AdInsightServiceActionMethods);
            }

            if (mappedRestApiMethod == null)
            {
                throw new InvalidOperationException($"Unknown method '{methodName}'");
            }

            await RefreshAccessToken(oAuth => oAuth.OAuthTokens.AccessToken == null || RefreshOAuthTokensAutomatically && oAuth.OAuthTokens.AccessTokenExpired).ConfigureAwait(false);

            var originalFieldValues = new List<(FieldInfo, object)>();

            var headerValues = MergeRequestFieldsWithAuthorizationData(request, originalFieldValues);

            var requestJson = JsonSerializer.Serialize(request, SerializerOptions);

            var restApiMethodInfo = mappedRestApiMethod.Value;

            var methodAction = restApiMethodInfo.Action;

            if (!string.IsNullOrEmpty(methodAction) && !methodAction.StartsWith("/"))
            {
                methodAction = $"/{methodAction}";
            }

            var requestUri = new Uri($"{restApiMethodInfo.EntityName}{methodAction}", UriKind.Relative);

            var retry = false;

            HttpResponseMessage response;

            do
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = restApiMethodInfo.HttpMethod,
                    RequestUri = requestUri,
                    Content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json")
                };

                requestMessage.Headers.Add("CustomerId", headerValues.CustomerId);

                requestMessage.Headers.Add("CustomerAccountId", headerValues.CustomerAccountId);

                requestMessage.Headers.Add("DeveloperToken", _authorizationData.DeveloperToken);

                requestMessage.Headers.Add("ApplicationToken", _authorizationData.DeveloperToken);

                _authorizationData.Authentication.AddAuthenticationHeaders(requestMessage.Headers);

                var httpClient = _serviceClientFactory.GetRestHttpClientProvider().GetHttpClient(serviceType, _environment);

                response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.Unauthorized && !retry && RefreshOAuthTokensAutomatically) // allow at most one retry due to expired token
                {
                    await RefreshAccessToken().ConfigureAwait(false);

                    retry = true;
                }
                else
                {
                    retry = false;
                }
            } while (retry);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.StatusCode is HttpStatusCode.NotImplemented or HttpStatusCode.NotFound)
            {
                var retryAfterDelta = response.Headers.RetryAfter?.Delta ?? TimeSpan.FromHours(1);

                var retryAfterUtcTime = DateTimeOffset.UtcNow + retryAfterDelta;

                RetryAfter.AddOrUpdate(serviceType, retryAfterUtcTime, (type, offset) => retryAfterUtcTime);

                foreach (var (field, value) in originalFieldValues)
                {
                    field.SetValue(request, value);
                }

                return default;
            }

            T ParseResponse<T>()
            {
                var obj = JsonSerializer.Deserialize<T>(responseContent, SerializerOptions);

                if (obj == null)
                {
                    throw new InvalidOperationException($"Couldn't deserialize type '{typeof(T)}' from response content: {responseContent}");
                }

                return obj;
            }

            if (response.StatusCode is 
                HttpStatusCode.InternalServerError or 
                (HttpStatusCode)429 or 
                HttpStatusCode.BadRequest or 
                HttpStatusCode.Unauthorized or 
                HttpStatusCode.Forbidden)
            {
                const string faultMessage = "An error occurred while executing the request. Please check the exception Detail property for more information. TrackingId:";

                if (serviceType == typeof(V13.CampaignManagement.ICampaignManagementService))
                {
                    var faultDetail = ParseResponse<V13.CampaignManagement.ApplicationFault>();

                    var faultReason = new FaultReason($"{faultMessage} {faultDetail.TrackingId}.");

                    switch (faultDetail)
                    {
                        case V13.CampaignManagement.ApiFaultDetail apiFaultDetail:
                            throw new FaultException<V13.CampaignManagement.ApiFaultDetail>(apiFaultDetail, faultReason);
                        case V13.CampaignManagement.EditorialApiFaultDetail editorialApiFaultDetail:
                            throw new FaultException<V13.CampaignManagement.EditorialApiFaultDetail>(editorialApiFaultDetail, faultReason);
                        case V13.CampaignManagement.AdApiFaultDetail adApiFaultDetail:
                            throw new FaultException<V13.CampaignManagement.AdApiFaultDetail>(adApiFaultDetail, faultReason);
                        default:
                            throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                    }
                }

                if (serviceType == typeof(V13.Bulk.IBulkService))
                {
                    var faultDetail = ParseResponse<V13.Bulk.ApplicationFault>();

                    var faultReason = new FaultReason($"An error occurred while executing the request. Please check the exception Detail property for more information. TrackingId: {faultDetail.TrackingId}.");

                    switch (faultDetail)
                    {
                        case V13.Bulk.ApiFaultDetail apiFaultDetail:
                            throw new FaultException<V13.Bulk.ApiFaultDetail>(apiFaultDetail, faultReason);
                        case V13.Bulk.AdApiFaultDetail adApiFaultDetail:
                            throw new FaultException<V13.Bulk.AdApiFaultDetail>(adApiFaultDetail, faultReason);
                        default:
                            throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                    }
                }

                if (serviceType == typeof(V13.Reporting.IReportingService))
                {
                    var faultDetail = ParseResponse<V13.Reporting.ApplicationFault>();

                    var faultReason = new FaultReason($"An error occurred while executing the request. Please check the exception Detail property for more information. TrackingId: {faultDetail.TrackingId}.");

                    switch (faultDetail)
                    {
                        case V13.Reporting.ApiFaultDetail apiFaultDetail:
                            throw new FaultException<V13.Reporting.ApiFaultDetail>(apiFaultDetail, faultReason);
                        case V13.Reporting.AdApiFaultDetail adApiFaultDetail:
                            throw new FaultException<V13.Reporting.AdApiFaultDetail>(adApiFaultDetail, faultReason);
                        default:
                            throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                    }
                }

                if (serviceType == typeof(V13.AdInsight.IAdInsightService))
                {
                    var faultDetail = ParseResponse<V13.AdInsight.ApplicationFault>();

                    var faultReason = new FaultReason($"An error occurred while executing the request. Please check the exception Detail property for more information. TrackingId: {faultDetail.TrackingId}.");

                    switch (faultDetail)
                    {
                        case V13.AdInsight.ApiFaultDetail apiFaultDetail:
                            throw new FaultException<V13.AdInsight.ApiFaultDetail>(apiFaultDetail, faultReason);
                        case V13.AdInsight.AdApiFaultDetail adApiFaultDetail:
                            throw new FaultException<V13.AdInsight.AdApiFaultDetail>(adApiFaultDetail, faultReason);
                        default:
                            throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                    }
                }

                if (serviceType == typeof(V13.CustomerManagement.ICustomerManagementService))
                {
                    var faultDetail = ParseResponse<V13.CustomerManagement.ApplicationFault>();

                    var faultReason = new FaultReason($"An error occurred while executing the request. Please check the exception Detail property for more information. TrackingId: {faultDetail.TrackingId}.");

                    switch (faultDetail)
                    {
                        case V13.CustomerManagement.ApiFault apiFault:
                            throw new FaultException<V13.CustomerManagement.ApiFault>(apiFault, faultReason);
                        case V13.CustomerManagement.AdApiFaultDetail adApiFaultDetail:
                            throw new FaultException<V13.CustomerManagement.AdApiFaultDetail>(adApiFaultDetail, faultReason);
                        default:
                            throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                    }
                }

                if (serviceType == typeof(V13.CustomerBilling.ICustomerBillingService))
                {
                    var faultDetail = ParseResponse<V13.CustomerBilling.ApplicationFault>();

                    var faultReason = new FaultReason($"An error occurred while executing the request. Please check the exception Detail property for more information. TrackingId: {faultDetail.TrackingId}.");

                    switch (faultDetail)
                    {
                        case V13.CustomerBilling.ApiFault apiFault:
                            throw new FaultException<V13.CustomerBilling.ApiFault>(apiFault, faultReason);
                        case V13.CustomerBilling.AdApiFaultDetail adApiFaultDetail:
                            throw new FaultException<V13.CustomerBilling.AdApiFaultDetail>(adApiFaultDetail, faultReason);
                        default:
                            throw new InvalidOperationException($"Unknown fault type '{faultDetail.GetType()}'");
                    }
                }

                throw new InvalidOperationException($"Unknown service type '{serviceType}'");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Got unexpected status code '{response.StatusCode}' with response content: {responseContent}");
            }            

            var responseObj = ParseResponse<TResponse>();

            if (response.Headers.TryGetValues("TrackingId", out var trackingIdValues))
            {
                setTrackingId(responseObj, trackingIdValues.First());
            }

            return responseObj;
        }

        private void ValidateObjectStateAndParameters(object method, object request)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (request == null) throw new ArgumentNullException(nameof(request));

            _authorizationData.Validate();
        }

        private async Task RefreshAccessToken(Func<OAuthWithAuthorizationCode, bool> condition = null)
        {
            if (_authorizationData.Authentication is OAuthWithAuthorizationCode { OAuthTokens: not null } oAuthWithCode && (condition == null || condition(oAuthWithCode)))
            {
                await oAuthWithCode.RefreshAccessTokenAsync().ConfigureAwait(false);
            }
        }

        private (string CustomerAccountId, string CustomerId) MergeRequestFieldsWithAuthorizationData(object request, List<(FieldInfo, object)> originalFieldValues)
        {
            SetRequestFieldIfNeeded(request, "AccountId", _authorizationData.AccountId, originalFieldValues);

            var customerAccountId = SetRequestFieldIfNeeded(request, "CustomerAccountId", _authorizationData.AccountId, originalFieldValues);

            var customerId = SetRequestFieldIfNeeded(request, "CustomerId", _authorizationData.CustomerId, originalFieldValues);

            SetRequestFieldIfNeeded(request, "DeveloperToken", _authorizationData.DeveloperToken, originalFieldValues, alwaysOverwriteRequestField: true);

            return (CustomerAccountId: customerAccountId, CustomerId: customerId);
        }

        private string SetRequestFieldIfNeeded<TAuthData>(object request, string name, TAuthData authorizationDataValue, List<(FieldInfo, object)> originalFieldValues, bool alwaysOverwriteRequestField = false)
        {
            var field = request.GetType().GetField(name);

            if (field == null)
            {
                return null;
            }

            dynamic requestValue = field.GetValue(request);

            if (!authorizationDataValue.Equals(default(TAuthData)) && (alwaysOverwriteRequestField || requestValue == null || requestValue.Equals(0)))
            {
                var targetType = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;

                field.SetValue(request, Convert.ChangeType(authorizationDataValue, targetType, CultureInfo.InvariantCulture));

                originalFieldValues.Add((field, requestValue));

                return (string)Convert.ChangeType(authorizationDataValue, typeof(string), CultureInfo.InvariantCulture);
            }

            return Convert.ChangeType(requestValue, typeof(string), CultureInfo.InvariantCulture);
        }

        internal object CreateService(Type serviceType)
        {
            if (serviceType == typeof(V13.CampaignManagement.ICampaignManagementService))
            {
                return _services.GetOrAdd(serviceType, t => new CampaignManagementService(this, t));
            }

            if (serviceType == typeof(V13.Bulk.IBulkService))
            {
                return _services.GetOrAdd(serviceType, t => new BulkService(this, t));
            }

            if (serviceType == typeof(V13.Reporting.IReportingService))
            {
                return _services.GetOrAdd(serviceType, t => new ReportingService(this, t));
            }

            if (serviceType == typeof(V13.AdInsight.IAdInsightService))
            {
                return _services.GetOrAdd(serviceType, t => new AdInsightService(this, t));
            }

            if (serviceType == typeof(V13.CustomerManagement.ICustomerManagementService))
            {
                return _services.GetOrAdd(serviceType, t => new CustomerManagementService(this, t));
            }

            if (serviceType == typeof(V13.CustomerBilling.ICustomerBillingService))
            {
                return _services.GetOrAdd(serviceType, t => new CustomerBillingService(this, t));
            }

            throw new NotImplementedException($"Service {serviceType} doesn't support REST API");
        }
    }
}