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

using Microsoft.BingAds.Internal.OAuth;
using Microsoft.BingAds.Internal.Utilities;
using System.Web;

// ReSharper disable once CheckNamespace

namespace Microsoft.BingAds.Internal
{
    /// <summary>
    /// Represents a proxy to the Microsoft account authorization service. 
    /// Implement an extension of this class in compliance with the authorization code grant flow for 
    /// <see href="http://go.microsoft.com/fwlink/?LinkID=511609">Managing User Authentication with OAuth 
    /// documented</see>. This is a standard OAuth 2.0 flow and is defined in detail in the 
    /// <see href="https://tools.ietf.org/html/rfc6749#section-4.1">Authorization Code Grant section of the OAuth 2.0 spec</see>.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511607">registering a Bing Ads application</see>. 
    /// </summary>
    public abstract class OAuthWithAuthorizationCode : OAuthAuthorization
    {
        private readonly string _optionalClientSecret;

        private readonly Uri _redirectionUri;
        
        private readonly IOAuthService _oauthService;

        /// <summary>
        /// An opaque value used by the client to maintain state between the request and callback. 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected string OptionalClientSecret => _optionalClientSecret;

        /// <summary>
        /// The URI to which the user of the app will be redirected after receiving user consent.
        /// </summary>
        public override Uri RedirectionUri => _redirectionUri;


        /// <summary>
        /// Occurs when a new refresh token is received.
        /// </summary>
        public event EventHandler<NewOAuthTokensReceivedEventArgs> NewOAuthTokensReceived;

        /// <summary>
        /// Initializes a new instance of the OAuthWithAuthorizationCode class.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.         
        /// </param>
        /// <param name="optionalClientSecret">
        /// The client secret corresponding to your registered application, or null if your app is a desktop or mobile app.        
        /// </param>
        /// <param name="redirectionUri">
        /// The URI to which the user of the app will be redirected after receiving user consent.        
        /// </param>
        /// <param name="refreshToken">
        /// The refresh token that should be used to request an access token.
        /// </param>
        /// <param name="environment">Bing Ads API environment</param>
        /// <remarks>
        /// <para>
        /// For more information about using a client identifier for authentication, see 
        /// <see href="https://tools.ietf.org/html/rfc6749#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="https://tools.ietf.org/html/rfc6749#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        protected OAuthWithAuthorizationCode(
            string clientId, 
            string optionalClientSecret, 
            Uri redirectionUri, 
            string refreshToken, 
            ApiEnvironment? environment,
            OAuthScope oAuthScope,
            string tenant,
            bool useMsaProd = true)
            : this(clientId, optionalClientSecret, redirectionUri, environment, oAuthScope, tenant, useMsaProd)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException("refreshToken");
            }

            OAuthTokens = new OAuthTokens(null, 0, refreshToken);
        }

        /// <summary>
        /// Initializes a new instance of the OAuthWebAuthCodeGrant class.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application. 
        /// </param>
        /// <param name="optionalClientSecret">
        /// The client secret corresponding to your registered application, or null if your app is a desktop or mobile app.
        /// </param>
        /// <param name="redirectionUri">
        /// The URI to which the user of the app will be redirected after receiving user consent.
        /// </param>
        /// <param name="oauthTokens">
        /// Contains information about OAuth access tokens received from the Microsoft Account authorization service.
        /// </param>
        /// <param name="environment">Bing Ads API environment</param>
        /// <remarks>
        /// <para>
        /// For more information about using a client identifier for authentication, see 
        /// <see href="https://tools.ietf.org/html/rfc6749#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="https://tools.ietf.org/html/rfc6749#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        protected OAuthWithAuthorizationCode(
            string clientId, 
            string optionalClientSecret, 
            Uri redirectionUri, 
            OAuthTokens oauthTokens, 
            ApiEnvironment? environment,
            OAuthScope oAuhthScope,
            string tenant,
            bool useMsaProd = true)
            : this(clientId, optionalClientSecret, redirectionUri, environment, oAuhthScope, tenant, useMsaProd)
        {
            if (oauthTokens == null || oauthTokens.RefreshToken == null)
            {
                throw new ArgumentNullException("oAuthTokens");
            }

            OAuthTokens = new OAuthTokens(null, 0, oauthTokens.RefreshToken, oauthTokens.ResponseFragments);
        }

        /// <summary>
        /// Initializes a new instance of the OAuthWithAuthorizationCode class.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.         
        /// </param>
        /// <param name="optionalClientSecret">
        /// The client secret corresponding to your registered application, or null if your app is a desktop or mobile app.        
        /// </param>
        /// <param name="redirectionUri">
        /// The URI to which the user of the app will be redirected after receiving user consent.        
        /// </param>
        /// <param name="environment">Bing Ads API environment</param>
        /// <remarks>
        /// <para>
        /// For more information about using a client identifier for authentication, see <see href="https://tools.ietf.org/html/rfc6749#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="https://tools.ietf.org/html/rfc6749#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        protected OAuthWithAuthorizationCode(
            string clientId, 
            string optionalClientSecret, 
            Uri redirectionUri, 
            ApiEnvironment? environment,
            OAuthScope oAuthScope, 
            string tenant,
            bool useMsaProd = true)
            :  base(clientId, environment, oAuthScope, tenant, useMsaProd)
        {
            _optionalClientSecret = optionalClientSecret;
            _oauthService = new UriOAuthService(Environment);
            _redirectionUri = redirectionUri ?? _oauthService.RedirectionUri(OAuthScope);
        }

        internal OAuthWithAuthorizationCode(
            string clientId, 
            string clientSecret, 
            Uri redirectionUri, 
            IOAuthService oauthService, 
            ApiEnvironment env,
            OAuthScope oAuhthScope, 
            string tenant,
            bool useMsaProd = true)
            : base(clientId, env, oAuhthScope, tenant, useMsaProd)
        {
            if (redirectionUri == null)
            {
                throw new ArgumentNullException("redirectionUri");
            }

            _optionalClientSecret = clientSecret;
            _redirectionUri = redirectionUri;
            _oauthService = oauthService;
        }

        /// <summary>
        /// Gets the Microsoft Account authorization endpoint where the user should be navigated to give his or her consent.
        /// </summary>
        /// <returns>The Microsoft Account authorization endpoint of type <see cref="Uri"/>.</returns>
        public override Uri GetAuthorizationEndpoint()
        {
            return UriOAuthService.GetAuthorizationEndpoint(new OAuthUrlParameters
            {
                ClientId = ClientId,
                ResponseType = "code",
                RedirectUri = _redirectionUri,
                State = State,
            }, 
            Environment, 
            OAuthScope, 
            Tenant);
        }

        /// <summary>
        /// Retrieves OAuth access and refresh tokens from the Microsoft Account authorization service 
        /// using the specified authorization response redirect <see cref="Uri"/>.
        /// </summary>
        /// <param name="responseUri">
        /// The authorization response redirect <see cref="Uri"/> that contains the authorization code.        
        /// </param>
        /// <remarks>
        /// For more information, see <see href="https://tools.ietf.org/html/rfc6749#section-4.1.2">Authorization Response section in the OAuth 2.0 spec</see>.
        /// </remarks>
        /// <returns>A task that represents the asynchronous operation. The task result will be an <see cref="OAuthTokens"/> object.</returns>      
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be received due to an error received from the Microsoft Account authorization server.</exception>  
        public async Task<OAuthTokens> RequestAccessAndRefreshTokensAsync(Uri responseUri, List<KeyValuePair<string, string>> additionalParams = null)
        {
            if (responseUri == null)
            {
                throw new ArgumentNullException("responseUri");
            }

            var queryParts = HttpUtility.ParseQueryString(responseUri.Query);

            var error = queryParts["error"];

            if (!string.IsNullOrEmpty(error))
            {
                var details = new OAuthErrorDetails { Error = error };

                var errorDescription = queryParts["error_description"];

                if (!string.IsNullOrEmpty(errorDescription))
                {
                    details.Description = errorDescription;
                }

                throw new OAuthTokenRequestException(ErrorMessages.OAuthError, details);
            }

            var code = queryParts["code"];

            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException(ErrorMessages.UriDoesntContainCode);
            }

            OAuthTokens = await _oauthService.GetAccessTokensAsync(new OAuthRequestParameters
            {
                ClientId = ClientId,
                ClientSecret = _optionalClientSecret,
                RedirectUri = RedirectionUri,
                GrantType = "authorization_code",
                GrantParamName = "code",
                GrantValue = code,
            }, 
            OAuthScope,
            Tenant, additionalParams).ConfigureAwait(false);

            RaiseNewTokensReceivedEvent();

            return OAuthTokens;
        }

        /// <summary>
        /// Retrieves OAuth access and refresh tokens from the Microsoft Account authorization service 
        /// using the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">
        /// The refresh token used to request new access and refresh tokens.        
        /// </param>
        /// <remarks>
        /// For more information, see <see href="https://tools.ietf.org/html/rfc6749#section-6">Refreshing an Access Token section in the OAuth 2.0 spec</see>.
        /// </remarks>
        /// <returns>A task that represents the asynchronous operation. The task result will be an <see cref="OAuthTokens"/> object.</returns>        
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be received due to an error received from the Microsoft Account authorization server.</exception>
        public async Task<OAuthTokens> RequestAccessAndRefreshTokensAsync(string refreshToken, List<KeyValuePair<string, string>> additionalParams = null)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException("refreshToken");
            }

            OAuthTokens = await _oauthService.GetAccessTokensAsync(new OAuthRequestParameters
            {
                ClientId = ClientId,
                ClientSecret = _optionalClientSecret,
                RedirectUri = null,
                GrantType = "refresh_token",
                GrantParamName = "refresh_token",
                GrantValue = refreshToken,
            }, 
            OAuthScope,
            Tenant,
            additionalParams).ConfigureAwait(false);

            RaiseNewTokensReceivedEvent();

            return OAuthTokens;
        }

        /// <summary>
        /// Retrieves OAuth tokens from authorization server using the last known refresh token from the current session.
        /// </summary>
        /// <returns>OAuth tokens</returns>
        /// <remarks>
        /// When the current access token expires, it needs to be refreshed. 
        /// It can be refreshed using the refresh token that was receive before (either provided directly by user or retrieved using the authorization code).
        /// The <see cref="ServiceClient{TService}"/> detects access token expiration and calls this method to refresh it.
        /// </remarks>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be received.</exception>
        internal Task<OAuthTokens> RefreshAccessTokenAsync()
        {
            return RequestAccessAndRefreshTokensAsync(OAuthTokens.RefreshToken);
        }

        private void RaiseNewTokensReceivedEvent()
        {
            NewOAuthTokensReceived?.Invoke(this, new NewOAuthTokensReceivedEventArgs(OAuthTokens.AccessToken, OAuthTokens.RefreshToken));
        }
    }
}