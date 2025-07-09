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


namespace Microsoft.BingAds.Internal
{
    internal static class ErrorMessages
    {
        public const string UriDoesntContainCode = "Uri passed doesn't contain code param. Please make sure the uri has a code in it, for example http://myurl.com?code=123";

        public const string UriDoesntContainAccessToken = "Uri passed doesn't contain access_token param. Please make sure the has an access_token param in it";

        public const string UriDoesntContainExpiresIn = "Uri passed doesn't contain expires_in param. Please make sure the has an expires_in param in it";

        public const string UriDoesntContainState = "Uri passed doesn't contain state param while authentication requires a state verification. Please make sure the uri has a state in it, for example http://myurl.com?code=123&state=MyState";

        public const string UriDoesntMatchState = "The state passed in Uri does not match the state value specified in authentication";

        public const string UserDataAuthenticationIsNull = "AuthorizationData object has the Authentication property set to null. Please make sure it's not null before calling this method";
        public const string UserDataDeveloperTokenIsNull = "AuthorizationData object has the DeveloperToken property set to null. Please make sure it's not null before calling this method";
        
        public static string GetFullOAuthAccessTokenNotRequestedMessage(Type oAuthType)
        {
            string classAndMethod = "the corresponding OAuth class method"; // in the case we add a new OAuth class and forget to update this method

            if (oAuthType == typeof(OAuthWithAuthorizationCode))
            {
                classAndMethod = typeof (OAuthWithAuthorizationCode).Name + ".RequestAccessAndRefreshTokens";
            }
            else if (oAuthType == typeof(OAuthDesktopMobileImplicitGrant))
            {
                classAndMethod = typeof (OAuthDesktopMobileImplicitGrant).Name + ".ExtractAccessTokenFromUri";
            }

            return "OAuth access token hasn't been requested. Please request it using " + classAndMethod + " before calling this method";
        }

        public const string ApiServiceTypeMustBeInterface = "Invalid service interface type is passed to the generic ApiService class. See ApiService documentation for supported types.";

        public static string GetPropertyMustNotBeNullMessage(string entityType, string propertyName)
        {
            return string.Format("Property {0}.{1} must not be null when calling WriteEntity.", entityType, propertyName);
        }

        public static string GetListMustNotBeEmptyMessage(string entityType, string propertyName)
        {
            return string.Format("List {0}.{1} must not be null or empty when calling WriteEntity.", entityType, propertyName);
        }

        public const string OAuthError = "Couldn't request OAuth AccessTokens. Please check the Details property for more information.";

        public const string EntitiesMustNotBeNull = "parameters.Entities must not be null.";

        public const string UploadFilePathMustNotBeNull = "FileUploadParameters.UploadFilePath must be not null.";

        public const string FormatVersionIsNotSupported = "Format version is not supported: ";

        public const string TypeColumnNotFound = "'Type' column wasn't found in the file. Please check your file format.";

        public const string SiteLinkAdExtensionIdMustBeSet = "SiteLinkAdExtension.Id must not be null. Please set it to a positive or negative Id.";

        public const string AtLeastOneLocationSubTargetMustNotBeNull = "At least one location sub target must not be null.";

        public const string AtLeastOneSubTargetMustNotBeNull = "At least one sub target must not be null.";
    }
}
