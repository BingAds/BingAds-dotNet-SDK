using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal.OAuth
{
    internal interface IOAuthService
    {
        Task<OAuthTokens> GetAccessTokensAsync(OAuthRequestParameters oAuthParameters);        
    }
}