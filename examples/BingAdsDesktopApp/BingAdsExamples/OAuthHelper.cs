// Copyright 2014 Microsoft Corporation 

// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 

//    http://www.apache.org/licenses/LICENSE-2.0 

// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BingAdsExamples.Properties;
using Microsoft.BingAds;

namespace BingAdsExamples
{
    /// <summary>
    /// Abstracts the details of interacting with a browser window or using a refresh token for the purpose of  
    /// obtaining authorization for your application to manage a user's Bing Ads account. 
    /// </summary>
    public class OAuthHelper
    {
        /// <summary>
        /// Ensures that your application has authorization to manage a Bing Ads account. 
        /// If authorization has previously been granted, authentication is attempted with the refresh token.
        /// If a refresh token is not available, authorization is requested in a browser window. 
        /// </summary>
        /// <returns>Returns an instance of OAuthDesktopMobileAuthCodeGrant when the authorization 
        /// request task completes. </returns>
        public static async Task<OAuthDesktopMobileAuthCodeGrant> AuthorizeDesktopMobileAuthCodeGrant()
        {
            var auth = new OAuthDesktopMobileAuthCodeGrant(Settings.Default["ClientId"].ToString());
            
            string refreshToken;

            if (GetRefreshToken(out refreshToken))
            {
                await AuthorizeWithRefreshTokenAsync(auth, refreshToken);
            }
            else
            {
                await AuthorizeInBrowser(auth);

                SaveRefreshToken(auth);
            }

            return auth;
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

        private static void SaveRefreshToken(OAuthDesktopMobileAuthCodeGrant auth)
        {
            Settings.Default["RefreshToken"] = auth.OAuthTokens.RefreshToken.Protect();
            Settings.Default.Save();
        }

        private static async Task AuthorizeInBrowser(OAuthDesktopMobileAuthCodeGrant auth)
        {
            var browserWindow = new BrowserWindow(auth.GetAuthorizationEndpoint(), auth.RedirectionUri.AbsolutePath);

            browserWindow.Show();

            var redirectUri = await browserWindow.GetRedirectUri();

            await auth.RequestAccessAndRefreshTokensAsync(redirectUri);
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

        private static async Task AuthorizeImplicitlyInBrowser(OAuthDesktopMobileImplicitGrant auth)
        {
            var browserWindow = new BrowserWindow(auth.GetAuthorizationEndpoint(), auth.RedirectionUri.AbsolutePath);

            browserWindow.Show();

            var redirectUri = await browserWindow.GetRedirectUri();

            auth.ExtractAccessTokenFromUri(redirectUri);
        }
    }
}
