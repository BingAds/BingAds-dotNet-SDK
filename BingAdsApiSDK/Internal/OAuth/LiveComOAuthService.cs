using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal.OAuth
{
    /// <summary>
    /// Provides method for getting OAuth tokens from the live.com authorization server using <see cref="OAuthRequestParameters"/>.
    /// </summary>
    internal class LiveComOAuthService : IOAuthService
    {
        /// <summary>
        /// The redirect <see cref="Uri"/> for a desktop or mobile application.
        /// </summary>
        public static readonly Uri DesktopRedirectUri = new Uri("https://login.live.com/oauth20_desktop.srf");

        private readonly IHttpService _httpService;

        public LiveComOAuthService()
        {
            _httpService = new HttpService();
        }

        public LiveComOAuthService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        /// <summary>
        /// Calls live.com authorization server with the <see cref="OAuthRequestParameters"/> passed in, deserializes the response and returns back OAuth tokens.
        /// </summary>
        /// <param name="oAuthParameters">OAuth parameters for authorization server call</param>
        /// <exception cref="OAuthTokenRequestException">Thrown if tokens can't be received</exception>
        /// <returns>OAuth tokens</returns>
        public async Task<OAuthTokens> GetAccessTokensAsync(OAuthRequestParameters oAuthParameters)
        {
            var values = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", oAuthParameters.ClientId),
                new KeyValuePair<string, string>("grant_type", oAuthParameters.GrantType),
                new KeyValuePair<string, string>(oAuthParameters.GrantParamName, oAuthParameters.GrantValue),
                new KeyValuePair<string, string>("redirect_uri", oAuthParameters.RedirectUri.ToString())
            };

            if (!string.IsNullOrEmpty(oAuthParameters.ClientSecret))
            {
                values.Add(new KeyValuePair<string, string>("client_secret", oAuthParameters.ClientSecret));
            }
            
            var response = await _httpService.PostAsync(new Uri("https://login.live.com/oauth20_token.srf"), values).ConfigureAwait(false);

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {                
                var serializer = new DataContractJsonSerializer(typeof(OAuthTokensContract));

                var tokensContract = (OAuthTokensContract)serializer.ReadObject(stream);

                return new OAuthTokens(tokensContract.AccessToken, tokensContract.AccessTokenExpiresInSeconds, tokensContract.RefreshToken);                
            }
            else
            {
                var serializer = new DataContractJsonSerializer(typeof(OAuthErrorDetailsContract));

                var errorDetailsContract = (OAuthErrorDetailsContract)serializer.ReadObject(stream);

                throw new OAuthTokenRequestException(ErrorMessages.OAuthError, new OAuthErrorDetails { Description = errorDetailsContract.Description, Error = errorDetailsContract.Error });
            }
        }
        
        public static Uri GetAuthorizationEndpoint(OAuthUrlParameters parameters)
        {
            return new Uri(string.Format(
                "https://login.live.com/oauth20_authorize.srf?client_id={0}&scope=bingads.manage&response_type={1}&redirect_uri={2}",
                parameters.ClientId,
                parameters.ResponseType,
                parameters.RedirectUri)
            );
        }
    }
}
