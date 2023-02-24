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

using System.Net.Http.Headers;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Represents a legacy Bing Ads authentication method using user name and password.
    /// </summary>
    /// <remarks>
    /// <para>
    /// You can use an instance of this class as the Authentication property of 
    /// a <see cref="AuthorizationData"/> object to authenticate with Bing Ads services.
    /// </para>
    /// <para>
    /// Existing users with legacy Bing Ads credentials may continue to specify the UserName and Password header elements.
    /// In future versions of the API, Bing Ads will transition exclusively to Microsoft Account authentication. 
    /// New customers are required to sign up for Bing Ads with a Microsoft Account, and to manage those accounts you must use OAuth. 
    /// For example instead of using this PasswordAuthentication class, you would authenticate with an instance of either 
    /// <see cref="OAuthDesktopMobileAuthCodeGrant"/>, <see cref="OAuthDesktopMobileImplicitGrant"/>, or <see cref="OAuthWebAuthCodeGrant"/>.
    /// </para>
    /// </remarks>
    public class PasswordAuthentication : Authentication
    {
        private readonly string _username;

        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the PasswordAuthentication class using the specified user name and password.
        /// </summary>
        /// <param name="username">The Bing Ads user's sign-in user name. You may not set this element to a Microsoft account.</param>
        /// <param name="password">The Bing Ads user's sign-in password.</param>
        public PasswordAuthentication(string username, string password)
        {
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Sets the UserName and Password SOAP header elements for the corresponding Bing Ads service operation. 
        /// </summary>
        /// <param name="apiRequest">The Bing Ads service operation request object.
        /// </param>
        /// <remarks>
        /// For example, <paramref name="apiRequest"/> can be the GetUserRequest message of the 
        /// <see href="http://go.microsoft.com/fwlink/?LinkID=511603">GetUser</see> service operation.
        /// </remarks>
        protected internal override void SetAuthenticationFieldsOnApiRequestObject(dynamic apiRequest)
        {
            apiRequest.UserName = _username;
            apiRequest.Password = _password;
        }

        /// <summary>
        /// Adds the user name and password as http headers elements for the corresponding bulk file upload operation.
        /// </summary>
        /// <param name="requestHeaders">The headers collection to which authentication requests should be added.</param>
        protected internal override void AddAuthenticationHeaders(HttpRequestHeaders requestHeaders)
        {
            requestHeaders.Add("UserName", _username);
            requestHeaders.Add("Password", _password);
        }
    }
}
