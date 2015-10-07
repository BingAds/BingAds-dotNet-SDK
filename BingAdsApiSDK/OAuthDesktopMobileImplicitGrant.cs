using System;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.OAuth;
using Microsoft.BingAds.Internal.Utilities;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Represents an OAuth authorization object implementing the implicit grant flow for use in a desktop or mobile application. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// You can use an instance of this class as the <see cref="AuthorizationData.Authentication"/> property of an <see cref="AuthorizationData"/> object to authenticate with Bing Ads services.
    /// In this case the AuthenticationToken request header will be set to the corresponding <see cref="OAuthTokens.AccessToken"/> value.
    /// </para>
    /// <para>
    /// This class implements the implicit grant flow for 
    /// <see href="http://go.microsoft.com/fwlink/?LinkID=511608">Managing User Authentication with OAuth 
    /// documented</see>. This is a standard OAuth 2.0 flow and is defined in detail in the 
    /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-4.1">Authorization Code Grant section of the OAuth 2.0 spec</see>.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511607">registering a Bing Ads application</see>.     
    /// </para>
    /// </remarks>
    public class OAuthDesktopMobileImplicitGrant : OAuthAuthorization
    {   
        /// <summary>
        /// The URI to which your client browser will be redirected after receiving user consent.
        /// </summary>
        public override Uri RedirectionUri
        {
            get { return LiveComOAuthService.DesktopRedirectUri; }
        } 

        /// <summary>
        /// Initializes a new instance of the OAuthDesktopMobileImplicitGrant class with the specified ClientId.
        /// </summary>
        /// <param name="clientId">
        /// The client identifier corresponding to your registered application. 
        /// </param>  
        /// <remarks>
        /// For more information about using a client identifier for authentication, see 
        /// <see href="http://tools.ietf.org/html/draft-ietf-oauth-v2-15#section-3.1">Client Password Authentication section of the OAuth 2.0 spec</see>.
        /// </remarks>              
        public OAuthDesktopMobileImplicitGrant(string clientId)
            : base(clientId)
        {            
            
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
                ResponseType = "token",
                RedirectUri = LiveComOAuthService.DesktopRedirectUri
            });
        }

        /// <summary>
        /// Extracts the access token from the specified <see cref="Uri"/>.
        /// </summary>
        /// <param name="redirectUri">The redirect <see cref="Uri"/> that contains an access token.</param>
        /// <returns>
        /// The <see cref="OAuthTokens"/> object which contains both the <see cref="OAuthTokens.AccessToken"/> and 
        /// <see cref="OAuthTokens.AccessTokenExpiresInSeconds"/> properties.
        /// </returns>
        public OAuthTokens ExtractAccessTokenFromUri(Uri redirectUri)
        {            
            var fragmentParts = redirectUri.ParseFragment();

            if (!fragmentParts.ContainsKey("access_token"))
            {
                throw new InvalidOperationException(ErrorMessages.UriDoesntContainAccessToken);
            }

            if (!fragmentParts.ContainsKey("expires_in"))
            {
                throw new InvalidOperationException(ErrorMessages.UriDoesntContainAccessToken);
            }

            return OAuthTokens = new OAuthTokens(fragmentParts["access_token"], int.Parse(fragmentParts["expires_in"]), null);            
        }
    }
}
