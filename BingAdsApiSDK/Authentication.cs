using System.Net.Http.Headers;
using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Internal;

namespace Microsoft.BingAds
{
    /// <summary>
    /// The abstract base class for all authentication classes.
    /// </summary>
    /// <example>
    /// <see cref="OAuthAuthorization"/> sets the AuthenticationToken header element for the corresponding service client.
    /// </example>
    /// <seealso cref="ServiceClient{TService}"/>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="AuthorizationData"/>
    public abstract class Authentication
    {
        /// <summary>
        /// Sets the required SOAP header elements for the corresponding Bing Ads service operation. 
        /// </summary>
        /// <param name="apiRequest">The Bing Ads service operation request object. For example,
        /// this object can be the GetUserRequest message of the <see href="http://go.microsoft.com/fwlink/?LinkID=511603">GetUser</see> service operation.
        /// </param>
        /// <remarks>
        /// The header elements that the method sets will differ depending on the type of authentication. 
        /// For example if you use one of the OAuth classes, the AuthenticationToken header will be set by this method, 
        /// whereas the UserName and Password headers will remain empty. 
        /// </remarks>
        protected internal abstract void SetAuthenticationFieldsOnApiRequestObject(object apiRequest);

        /// <summary>
        /// Adds the required http headers elements for the corresponding bulk file upload operation.
        /// </summary>
        /// <param name="requestHeaders">The headers collection to which authentication requests should be added.</param>
        /// <remarks>
        /// The header elements that the method sets will differ depending on the type of authentication. 
        /// For example if you use one of the OAuth classes, the AuthenticationToken header will be set by this method, 
        /// whereas the UserName and Password headers will remain empty. 
        /// </remarks>
        protected internal abstract void AddAuthenticationHeaders(HttpRequestHeaders requestHeaders);
    }
}
