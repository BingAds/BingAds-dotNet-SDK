using Microsoft.BingAds.Internal;

namespace Microsoft.BingAds
{    
    /// <summary>
    /// Contains information about OAuth access tokens received from the Microsoft Account authorization service.
    /// </summary>
    /// <remarks>
    /// You can get OAuthTokens using the RequestAccessAndRefreshTokens method of RequestAccessAndRefreshTokens method of 
    /// either the <see cref="OAuthDesktopMobileAuthCodeGrant"/> or <see cref="OAuthWebAuthCodeGrant"/> classes.
    /// </remarks>
    public class OAuthTokens
    {
        private readonly string _accessToken;
        private readonly int _accessTokenExpiresInSeconds;
        private readonly string _refreshToken;

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="accessTokenExpiresInSeconds">Access token expiration time</param>
        /// <param name="refreshToken">Refresh token</param>
        internal OAuthTokens(string accessToken, int accessTokenExpiresInSeconds, string refreshToken)
        {
            _accessToken = accessToken;
            _accessTokenExpiresInSeconds = accessTokenExpiresInSeconds;
            _refreshToken = refreshToken;
        }

        /// <summary>
        /// OAuth access token that will be used for authorization in the Bing Ads services.
        /// </summary>
        public string AccessToken
        {
            get { return _accessToken; }            
        }

        /// <summary>
        /// Expiration time for the corresponding access token in seconds.
        /// </summary>
        public int AccessTokenExpiresInSeconds
        {
            get { return _accessTokenExpiresInSeconds; }            
        }

        /// <summary>
        /// OAuth refresh token that can be user to refresh an access token. 
        /// </summary>
        public string RefreshToken
        {
            get { return _refreshToken; }            
        }
    }
}