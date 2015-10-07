using System;
using System.Threading.Tasks;
using Microsoft.BingAds.Internal.OAuth;
using Microsoft.BingAds.Internal.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.BingAds.Internal
{
    /// <summary>
    /// Represents a proxy to the Microsoft account authorization service. 
    /// Implement an extension of this class in compliance with the authorization code grant flow for 
    /// <see href="http://go.microsoft.com/fwlink/?LinkID=511609">Managing User Authentication with OAuth 
    /// documented</see>. This is a standard OAuth 2.0 flow and is defined in detail in the 
    /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-4.1">Authorization Code Grant section of the OAuth 2.0 spec</see>.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511607">registering a Bing Ads application</see>. 
    /// </summary>
    public abstract class OAuthWithAuthorizationCode : OAuthAuthorization
    {
        private readonly string _optionalClientSecret;

        private readonly Uri _redirectionUri;

        private readonly IOAuthService _oauthService;

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected string OptionalClientSecret
        {
            get { return _optionalClientSecret; }
        }

        /// <summary>
        /// The URI to which the user of the app will be redirected after receiving user consent.
        /// </summary>
        public override Uri RedirectionUri
        {
            get { return _redirectionUri; }
        }

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
        /// <remarks>
        /// <para>
        /// For more information about using a client identifier for authentication, see 
        /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        protected OAuthWithAuthorizationCode(string clientId, string optionalClientSecret, Uri redirectionUri, string refreshToken)
            : this(clientId, optionalClientSecret, redirectionUri, new LiveComOAuthService())
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException("refreshToken");
            }

            OAuthTokens = new OAuthTokens(null, 0, refreshToken);
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
        /// <remarks>
        /// <para>
        /// For more information about using a client identifier for authentication, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        protected OAuthWithAuthorizationCode(string clientId, string optionalClientSecret, Uri redirectionUri)
            : this(clientId, optionalClientSecret, redirectionUri, new LiveComOAuthService())
        {

        }

        internal OAuthWithAuthorizationCode(string clientId, string clientSecret, Uri redirectionUri, IOAuthService oauthService)
            : base(clientId)
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
            return LiveComOAuthService.GetAuthorizationEndpoint(new OAuthUrlParameters
            {
                ClientId = ClientId,
                ResponseType = "code",
                RedirectUri = RedirectionUri
            });
        }

        /// <summary>
        /// Retrieves OAuth access and refresh tokens from the Microsoft Account authorization service 
        /// using the specified authorization response redirect <see cref="Uri"/>.
        /// </summary>
        /// <param name="responseUri">
        /// The authorization response redirect <see cref="Uri"/> that contains the authorization code.        
        /// </param>
        /// <remarks>
        /// For more information, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-4.1.2">Authorization Response section in the OAuth 2.0 spec</see>.
        /// </remarks>
        /// <returns>A task that represents the asynchronous operation. The task result will be an <see cref="OAuthTokens"/> object.</returns>      
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be received due to an error received from the Microsoft Account authorization server.</exception>  
        public async Task<OAuthTokens> RequestAccessAndRefreshTokensAsync(Uri responseUri)
        {
            if (responseUri == null)
            {
                throw new ArgumentNullException("responseUri");
            }

            var queryParts = responseUri.ParseQuery();

            string error;

            if (queryParts.TryGetValue("error", out error))
            {
                var details = new OAuthErrorDetails { Error = Uri.UnescapeDataString(error) };

                string errorDescription;

                if (queryParts.TryGetValue("error_description", out errorDescription))
                {
                    details.Description = Uri.UnescapeDataString(errorDescription);
                }

                throw new OAuthTokenRequestException(ErrorMessages.OAuthError, details);
            }

            if (!queryParts.ContainsKey("code"))
            {
                throw new ArgumentException(ErrorMessages.UriDoesntContainCode);
            }

            var code = queryParts["code"];

            OAuthTokens = await _oauthService.GetAccessTokensAsync(new OAuthRequestParameters
            {
                ClientId = ClientId,
                ClientSecret = _optionalClientSecret,
                RedirectUri = RedirectionUri,
                GrantType = "authorization_code",
                GrantParamName = "code",
                GrantValue = code
            }).ConfigureAwait(false);

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
        /// For more information, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-6">Refreshing an Access Token section in the OAuth 2.0 spec</see>.
        /// </remarks>
        /// <returns>A task that represents the asynchronous operation. The task result will be an <see cref="OAuthTokens"/> object.</returns>        
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be received due to an error received from the Microsoft Account authorization server.</exception>
        public async Task<OAuthTokens> RequestAccessAndRefreshTokensAsync(string refreshToken)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException("refreshToken");
            }

            OAuthTokens = await _oauthService.GetAccessTokensAsync(new OAuthRequestParameters
            {
                ClientId = ClientId,
                ClientSecret = _optionalClientSecret,
                RedirectUri = RedirectionUri,
                GrantType = "refresh_token",
                GrantParamName = "refresh_token",
                GrantValue = refreshToken
            }).ConfigureAwait(false);

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
            if (NewOAuthTokensReceived != null)
            {
                NewOAuthTokensReceived(this, new NewOAuthTokensReceivedEventArgs(OAuthTokens.AccessToken, OAuthTokens.RefreshToken));
            }
        }
    }
}