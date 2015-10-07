using System.Runtime.Serialization;

namespace Microsoft.BingAds.Internal.OAuth
{
    [DataContract]
    internal class OAuthErrorDetailsContract
    {
        [DataMember(Name = "error")]
        public string Error { get; internal set; }

        [DataMember(Name = "error_description")]
        public string Description { get; internal set; }
    }
}
