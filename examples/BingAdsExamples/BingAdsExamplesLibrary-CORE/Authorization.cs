using Microsoft.BingAds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingAdsExamplesLibrary_CORE
{
    public static class Authorization
    {
        private const string ClientState = "ClientStateGoesHere";
        private const string RefreshToken = null;

        public static Authentication AuthenticateWithOAuth(ApiEnvironment environment, string clientId, string refreshToken = RefreshToken, string clientState = ClientState)
        {
            var oAuthDesktopMobileAuthCodeGrant = new OAuthDesktopMobileAuthCodeGrant(clientId, environment);

            oAuthDesktopMobileAuthCodeGrant.State = clientState;

            if (refreshToken != null)
            {
                AuthorizeWithRefreshTokenAsync(oAuthDesktopMobileAuthCodeGrant, refreshToken).Wait();
            }
            else
            {
                var codeVerifier = "mycodeverifiermycodeverifiermycodeverifiermycodeverifiermycodeverifier";
                Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));

                Console.WriteLine(string.Format(
                    "Open a new web browser and navigate to {0}\n\n" +
                    "Grant consent in the web browser for the application to access " +
                    "your advertising accounts, and then enter the response URI that includes " +
                    "the authorization 'code' parameter: \n",
                    oAuthDesktopMobileAuthCodeGrant.GetAuthorizationEndpoint() +
                    "&code_challenge_method=plain&code_challenge=" + codeVerifier)
                );

                var responseUri = new Uri(Console.ReadLine());

                if (oAuthDesktopMobileAuthCodeGrant.State != ClientState)
                    throw new HttpRequestException("The OAuth response state does not match the client request state.");

                var additionalParams = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>(
                        "code_verifier",
                        codeVerifier)
                };
                oAuthDesktopMobileAuthCodeGrant.RequestAccessAndRefreshTokensAsync(responseUri, additionalParams).Wait();

                SaveRefreshToken(oAuthDesktopMobileAuthCodeGrant.OAuthTokens.RefreshToken);
            }

            oAuthDesktopMobileAuthCodeGrant.NewOAuthTokensReceived +=
                (sender, tokens) => SaveRefreshToken(tokens.NewRefreshToken);

            return oAuthDesktopMobileAuthCodeGrant;
        }

        private static Task<OAuthTokens> AuthorizeWithRefreshTokenAsync(OAuthDesktopMobileAuthCodeGrant authentication, string refreshToken)
        {
            return authentication.RequestAccessAndRefreshTokensAsync(refreshToken);
        }

        /// <summary>
        /// This saves the refresh token to the Azure Key Vault. Feel free to replace this with a database call or local file storage. 
        /// </summary>
        /// <param name="newRefreshtoken"></param>
        private static void SaveRefreshToken(string newRefreshtoken)
        {
            KeyVault.SetValueFromKey("WebJobRefresh-txt", newRefreshtoken);
        }
    }
}
