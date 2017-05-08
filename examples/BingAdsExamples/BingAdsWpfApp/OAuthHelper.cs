using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Net.Http;
using BingAdsWpfApp.Properties;
using Microsoft.BingAds;

namespace BingAdsWpfApp
{
    /// <summary>
    /// Abstracts the details of interacting with a browser window or using a refresh token for the purpose of  
    /// obtaining authorization for your application to manage a user's Bing Ads account. 
    /// </summary>
    public class OAuthHelper
    {
        private static string ClientState = "ClientStateGoesHere";

        /// <summary>
        /// Ensures that your application has authorization to manage a Bing Ads account. 
        /// If authorization has previously been granted, authentication is attempted with the refresh token.
        /// If a refresh token is not available, authorization is requested in a browser window. 
        /// </summary>
        /// <returns>Returns an instance of OAuthDesktopMobileAuthCodeGrant when the authorization 
        /// request task completes. </returns>
        public static async Task<OAuthDesktopMobileAuthCodeGrant> AuthorizeDesktopMobileAuthCodeGrant()
        {
            var oAuthDesktopMobileAuthCodeGrant = new OAuthDesktopMobileAuthCodeGrant(Settings.Default["ClientId"].ToString());

            // It is recommended that you specify a non guessable 'state' request parameter to help prevent
            // cross site request forgery (CSRF). 
            oAuthDesktopMobileAuthCodeGrant.State = ClientState;

            // It is important to save the most recent refresh token whenever new OAuth tokens are received. 
            // You will want to subscribe to the NewOAuthTokensReceived event handler. 
            // When calling Bing Ads services with ServiceClient<TService>, BulkServiceManager, or ReportingServiceManager, 
            // each instance will refresh your access token automatically if they detect the AuthenticationTokenExpired (109) error code. 
            oAuthDesktopMobileAuthCodeGrant.NewOAuthTokensReceived +=
                    (sender, tokens) => SaveRefreshToken(tokens.NewRefreshToken);

            string refreshToken;

            if (GetRefreshToken(out refreshToken))
            {
                await AuthorizeWithRefreshTokenAsync(oAuthDesktopMobileAuthCodeGrant, refreshToken);
            }
            else
            {
                await AuthorizeInBrowser(oAuthDesktopMobileAuthCodeGrant);
            }
                        
            return oAuthDesktopMobileAuthCodeGrant;
        }

        private static bool GetRefreshToken(out string refreshToken)
        {
            var protectedToken = Settings.Default["RefreshToken"].ToString();

            if (string.IsNullOrEmpty(protectedToken))
            {
                refreshToken = null;
                return false;
            }

            try
            {
                refreshToken = protectedToken.Unprotect();
                return true;
            }
            catch (CryptographicException)
            {
                refreshToken = null;
                return false;
            }
            catch (FormatException)
            {
                refreshToken = null;
                return false;
            }
        }

        private static Task<OAuthTokens> AuthorizeWithRefreshTokenAsync(OAuthDesktopMobileAuthCodeGrant auth, string refreshToken)
        {
            return auth.RequestAccessAndRefreshTokensAsync(refreshToken);
        }

        private static void SaveRefreshToken(string newRefreshToken)
        {
            Settings.Default["RefreshToken"] = newRefreshToken.Protect();
            Settings.Default.Save();
        }

        private static async Task AuthorizeInBrowser(OAuthDesktopMobileAuthCodeGrant authentication)
        {
            var browserWindow = new BrowserWindow(authentication.GetAuthorizationEndpoint(), authentication.RedirectionUri.AbsolutePath);

            browserWindow.Show();

            var redirectUri = await browserWindow.GetRedirectUri();
            
            if (authentication.State != ClientState)
                throw new HttpRequestException("The OAuth response state does not match the client request state.");

            await authentication.RequestAccessAndRefreshTokensAsync(redirectUri);

        }

        /// <summary>
        /// Ensures that your application has authorization to manage a Bing Ads account. 
        /// Requests authorization in a browser window.
        /// </summary>
        /// <returns>Returns an instance of OAuthDesktopMobileImplicitGrant when the authorization 
        /// request task completes. </returns>
        public static async Task<OAuthDesktopMobileImplicitGrant> AuthorizeDesktopMobileImplicitGrant()
        {
            var auth = new OAuthDesktopMobileImplicitGrant(Settings.Default["ClientId"].ToString());

            await AuthorizeImplicitlyInBrowser(auth);

            return auth;
        }

        private static async Task AuthorizeImplicitlyInBrowser(OAuthDesktopMobileImplicitGrant authentication)
        {
            var browserWindow = new BrowserWindow(authentication.GetAuthorizationEndpoint(), authentication.RedirectionUri.AbsolutePath);

            browserWindow.Show();

            var redirectUri = await browserWindow.GetRedirectUri();
            
            authentication.ExtractAccessTokenFromUri(redirectUri);

            if (authentication.State != ClientState)
                throw new HttpRequestException("The OAuth response state does not match the client request state.");

        }
    }
}
