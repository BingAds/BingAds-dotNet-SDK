using System;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.OAuth;

namespace Microsoft.BingAds
{
    /// <summary>    
    /// Represents an OAuth authorization object implementing the authorization code grant flow for use in a web application.     
    /// </summary>
    /// <remarks>
    /// <para>
    /// You can use an instance of this class as the <see cref="AuthorizationData.Authentication"/> property of an <see cref="AuthorizationData"/> object to authenticate with Bing Ads services.
    /// In this case the AuthenticationToken request header will be set to the corresponding <see cref="OAuthTokens.AccessToken"/> value.
    /// </para>
    /// <para>
    /// This class implements the authorization code grant flow for 
    /// <see href="http://go.microsoft.com/fwlink/?LinkID=511609">Managing User Authentication with OAuth documented</see>. This is a standard OAuth 2.0 flow and is defined in detail in the 
    /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-4.1">Authorization Code Grant section of the OAuth 2.0 spec</see>.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511607">registering a Bing Ads application</see>. 
    /// </para> 
    /// </remarks>    
    public class OAuthWebAuthCodeGrant : OAuthWithAuthorizationCode
    {
        /// <summary>
        /// The client secret for your registered web application.
        /// </summary>
        public string ClientSecret
        {
            get { return OptionalClientSecret; }
        }

        /// <summary>
        /// Initializes a new instance of the OAuthWebAuthCodeGrant class.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.  
        /// </param>        
        /// <param name="clientSecret">
        /// The client secret corresponding to your registered application, or null if your app is a desktop or mobile app.  
        /// </param>
        /// <param name="redirectionUri">
        /// The URI to which the user of the app will be redirected after receiving user consent.    
        /// </param>
        /// <remarks>
        /// <para>
        /// For more information about using a client identifier for authentication, see 
        /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        public OAuthWebAuthCodeGrant(string clientId, string clientSecret, Uri redirectionUri)
            : base(clientId, clientSecret, redirectionUri)
        {
            if (clientSecret == null)
            {
                throw new ArgumentNullException("clientSecret");
            }
        }

        /// <summary>
        /// Initializes a new instance of the OAuthWebAuthCodeGrant class.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.  
        /// </param>        
        /// <param name="clientSecret">
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
        /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </para>
        /// <para>
        /// For web applications, redirectionUri must be within the same domain of your registered application.  
        /// For more information, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-2.1.1">Redirection Uri section of the OAuth 2.0 spec</see>.
        /// </para>
        /// </remarks>
        public OAuthWebAuthCodeGrant(string clientId, string clientSecret, Uri redirectionUri, string refreshToken)
            : base(clientId, clientSecret, redirectionUri, refreshToken)
        {
            if (clientSecret == null)
            {
                throw new ArgumentNullException("clientSecret");
            }
        }

        internal OAuthWebAuthCodeGrant(string clientId, string clientSecret, Uri redirectionUri, IOAuthService oauthService)
            : base(clientId, clientSecret, redirectionUri, oauthService)
        {

        }
    }
}
