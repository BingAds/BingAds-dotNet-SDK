using System;
using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Internal;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Represents a user who intends to access the corresponding customer and account. 
    /// An instance of this class is required to authenticate with Bing Ads if you are using either 
    /// <see cref="ServiceClient{TService}"/> or <see cref="BulkServiceManager"/>.
    /// </summary>
    public class AuthorizationData
    {
        /// <summary>
        /// An object representing the authentication method that should be used in calls to the Bing Ads web services.
        /// </summary>        
        /// <seealso cref="OAuthDesktopMobileAuthCodeGrant"/>
        /// <seealso cref="OAuthDesktopMobileImplicitGrant"/>
        /// <seealso cref="OAuthWebAuthCodeGrant"/>
        /// <seealso cref="PasswordAuthentication"/>
        public Authentication Authentication { get; set; }

        /// <summary>
        /// The identifier of the account that owns the entities in the request. Used as the CustomerAccountId header and the AccountId body elements in calls to the Bing Ads web services.
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// The identifier of the customer that owns the account. Used as the CustomerId header element in calls to the Bing Ads web services.
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// The Bing Ads developer access token. Used as the DeveloperToken header element in calls to the Bing Ads web services.
        /// </summary>
        public string DeveloperToken { get; set; }

        internal void Validate()
        {
            if (Authentication == null)
            {
                throw new InvalidOperationException(ErrorMessages.UserDataAuthenticationIsNull);
            }

            if (DeveloperToken == null)
            {
                throw new InvalidOperationException(ErrorMessages.UserDataDeveloperTokenIsNull);
            }
        }
    }
}
