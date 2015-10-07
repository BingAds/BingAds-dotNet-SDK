using System;
using Microsoft.BingAds.Internal;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Provides data for <see cref="OAuthWithAuthorizationCode.NewOAuthTokensReceived"/> event.
    /// </summary>
    public class NewOAuthTokensReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// New access token.
        /// </summary>
        public string NewAccessToken { get; private set; }

        /// <summary>
        /// New refresh token.
        /// </summary>
        public string NewRefreshToken { get; private set; }

        /// <summary>
        /// Initializes a new instance of this class with the specified access and refresh tokens.
        /// </summary>
        /// <param name="newAccessToken">The new access token.</param>
        /// <param name="newRefreshToken">The new refresh token.</param>
        public NewOAuthTokensReceivedEventArgs(string newAccessToken, string newRefreshToken)
        {
            NewAccessToken = newAccessToken;
            NewRefreshToken = newRefreshToken;
        }
    }    
}
