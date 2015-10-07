using System;

namespace Microsoft.BingAds.Internal
{
    internal static class ErrorMessages
    {
        public const string UriDoesntContainCode = "Uri passed doesn't contain code param. Please make sure the uri has a code in it, for example http://myurl.com?code=123";

        public const string UriDoesntContainAccessToken = "Uri passed doesn't contain access_token param. Please make sure the has an access_token param in it";

        public const string UriDoesntContainExpiresIn = "Uri passed doesn't contain expires_in param. Please make sure the has an expires_in param in it";

        public const string UserDataAuthenticationIsNull = "UserData object has the Authentication property set to null. Please make sure it's not null before calling this method";
        public const string UserDataDeveloperTokenIsNull = "UserData object has the DeveloperToken property set to null. Please make sure it's not null before calling this method";
        
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
