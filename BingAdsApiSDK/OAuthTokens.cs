//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
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
        private readonly IDictionary<string, string> _responseFragments;
        private readonly DateTime _accessTokenReceivedDateTime;

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="accessTokenExpiresInSeconds">Access token expiration time</param>
        /// <param name="refreshToken">Refresh token</param>
        public OAuthTokens(string accessToken, int accessTokenExpiresInSeconds, string refreshToken, IDictionary<string, string> fragments = null)
        {
            _accessToken = accessToken;
            _accessTokenExpiresInSeconds = accessTokenExpiresInSeconds;
            _refreshToken = refreshToken;
            _responseFragments = fragments;
            _accessTokenReceivedDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Check if the Access Token has been expired.
        /// </summary>
        public bool AccessTokenExpired => AccessTokenExpiresInSeconds > 0 && DateTime.UtcNow > _accessTokenReceivedDateTime.AddSeconds(AccessTokenExpiresInSeconds);

        /// <summary>
        /// OAuth access token that will be used for authorization in the Bing Ads services.
        /// </summary>
        public string AccessToken => _accessToken;

        /// <summary>
        /// Expiration time for the corresponding access token in seconds.
        /// </summary>
        public int AccessTokenExpiresInSeconds => _accessTokenExpiresInSeconds;

        /// <summary>
        /// OAuth refresh token that can be user to refresh an access token. 
        /// </summary>
        public string RefreshToken => _refreshToken;

        /// <summary>
        /// OAuth WholeFragments.
        /// </summary>
        public IDictionary<string, string> ResponseFragments => _responseFragments;
    }
}