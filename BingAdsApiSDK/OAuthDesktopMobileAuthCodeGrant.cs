using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.OAuth;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Represents an OAuth authorization object implementing the authorization code grant flow for use in a desktop or mobile application. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// You can use an instance of this class as the <see cref="AuthorizationData.Authentication"/> property of an <see cref="AuthorizationData"/> object to authenticate with Bing Ads services.
    /// In this case the AuthenticationToken request header will be set to the corresponding <see cref="OAuthTokens.AccessToken"/> value.
    /// </para>
    /// <para>
    /// This class implements the authorization code grant flow for 
    /// <see href="http://go.microsoft.com/fwlink/?LinkID=511609">Managing User Authentication with OAuth 
    /// documented</see>. This is a standard OAuth 2.0 flow and is defined in detail in the 
    /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-4.1">Authorization Code Grant section of the OAuth 2.0 spec</see>.
    /// For more information , see <see href="http://go.microsoft.com/fwlink/?LinkID=511607">registering a Bing Ads application</see>. 
    /// </para>    
    /// </remarks>
    
    public class OAuthDesktopMobileAuthCodeGrant : OAuthWithAuthorizationCode
    {       
        /// <summary>
        /// Initializes a new instance of the OAuthDesktopMobileAuthCodeGrant class with the specified ClientId.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.  
        /// </param>        
        /// <remarks>
        /// For more information about using a client identifier for authentication, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">
        /// Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </remarks>
        public OAuthDesktopMobileAuthCodeGrant(string clientId)
            : base(clientId, null, LiveComOAuthService.DesktopRedirectUri)
        {

        }

        /// <summary>
        /// Initializes a new instance of the OAuthDesktopMobileAuthCodeGrant class with the specified ClientId.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application.  
        /// </param>
        /// <param name="refreshToken">
        /// The refresh token that should be used to request an access token.
        /// </param>
        /// <remarks>
        /// For more information about using a client identifier for authentication, see <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">
        /// Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </remarks>
        public OAuthDesktopMobileAuthCodeGrant(string clientId, string refreshToken)
            : base(clientId, null, LiveComOAuthService.DesktopRedirectUri, refreshToken)
        {

        }

        internal OAuthDesktopMobileAuthCodeGrant(string clientId, IOAuthService oauthService)
            : base(clientId, null, LiveComOAuthService.DesktopRedirectUri, oauthService)
        {

        }
    }
}
