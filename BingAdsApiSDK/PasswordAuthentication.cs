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
