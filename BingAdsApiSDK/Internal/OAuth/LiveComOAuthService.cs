//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

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
            
            var response = await _httpService.PostAsync(new Uri("https://login.live.com/oauth20_token.srf"), values, TimeSpan.FromSeconds(100)).ConfigureAwait(false);

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
                parameters.RedirectUri) + (string.IsNullOrEmpty(parameters.State) ? "": string.Format("&state={0}", parameters.State))
            );
        }
    }
}
