using System;
using System.Net.Http.Headers;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal
{
    /// <summary>
    /// The abstract base class for all OAuth authentication classes. You can use this class to dynamically instantiate a derived OAuth authentication class at run time.
    /// This class cannot be instantiated, and instead you should use either <see cref="OAuthDesktopMobileAuthCodeGrant"/>, <see cref="OAuthDesktopMobileImplicitGrant"/>, 
    /// or <see cref="OAuthWebAuthCodeGrant"/>, which extend this class.
    /// </summary>    
    /// <seealso cref="OAuthDesktopMobileImplicitGrant"/>
    /// <seealso cref="OAuthDesktopMobileAuthCodeGrant"/>
    /// <seealso cref="OAuthWebAuthCodeGrant"/>
    public abstract class OAuthAuthorization : Authentication
    {
        private readonly string _clientId;

        /// <summary>
        /// The client identifier corresponding to your registered application.         
        /// </summary>
        /// <remarks>
        /// For more information about using a client identifier for authentication, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </remarks>
        public string ClientId
        {
            get { return _clientId; }
        }

        /// <summary>
        /// The URI to which the user of the app will be redirected after receiving user consent.
        /// </summary>
        public abstract Uri RedirectionUri { get; }

        /// <summary>
        /// Initializes a new instance of the OAuthAuthorization class with the specified <paramref name="clientId"/>.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.         
        /// </param>
        /// <remarks>
        /// For more information about using a client identifier for authentication, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </remarks>
        protected OAuthAuthorization(string clientId)
        {
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            _clientId = clientId;
        }

        /// <summary>
        /// Sets the AuthenticationToken header element for the corresponding Bing Ads service operation. 
        /// </summary>
        /// <param name="apiRequest">The Bing Ads service operation request object.</param>
        /// <remarks>
        /// <para>
        /// This method will set the OAuth access token as the AuthenticationToken header on the API request object.
        /// </para>
        /// <para>
        /// For example, <paramref name="apiRequest"/> object can be the 
        /// GetUserRequest message of the GetUser service operation. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511603">GetUser</see>. 
        /// </para>
        /// </remarks>
        protected internal override void SetAuthenticationFieldsOnApiRequestObject(dynamic apiRequest)
        {
            if (OAuthTokens == null)
            {
                throw new InvalidOperationException(ErrorMessages.GetFullOAuthAccessTokenNotRequestedMessage(GetType()));
            }

            apiRequest.AuthenticationToken = OAuthTokens.AccessToken;
        }

        /// <summary>
        /// Gets the Microsoft Account authorization endpoint where the user should be navigated to give his or her consent.
        /// </summary>
        /// <returns>The Microsoft Account authorization endpoint of type <see cref="Uri"/>.</returns>
        public abstract Uri GetAuthorizationEndpoint();

        /// <summary>
        /// Contains information about OAuth access tokens received from the Microsoft Account authorization service.
        /// </summary>
        /// <remarks>
        /// <para>
        /// You can get OAuthTokens using the RequestAccessAndRefreshTokens method of one of the desktop or web application grant flow classes, 
        /// for example <see cref="OAuthWithAuthorizationCode.RequestAccessAndRefreshTokensAsync(System.Uri)"/>.
        /// </para>        
        /// </remarks>
        public OAuthTokens OAuthTokens { get; protected set; }

        /// <summary>
        /// Adds the AuthenticationToken header element for the corresponding bulk file upload operation. 
        /// </summary>
        /// <param name="requestHeaders">The headers collection to which authentication requests should be added.</param>
        protected internal override void AddAuthenticationHeaders(HttpRequestHeaders requestHeaders)
        {
            if (OAuthTokens == null)
            {
                throw new InvalidOperationException(ErrorMessages.GetFullOAuthAccessTokenNotRequestedMessage(GetType()));
            }

            requestHeaders.Add("AuthenticationToken", OAuthTokens.AccessToken);
        }
    }
}
