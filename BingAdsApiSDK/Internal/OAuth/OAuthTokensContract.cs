using System.Runtime.Serialization;

namespace Microsoft.BingAds.Internal.OAuth
{
    [DataContract]
    internal class OAuthTokensContract
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int AccessTokenExpiresInSeconds { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
