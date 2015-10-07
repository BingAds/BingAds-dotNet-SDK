using System;

namespace Microsoft.BingAds.Internal.OAuth
{
    internal class OAuthUrlParameters
    {
        public string ClientId { get; set; }

        public string ResponseType { get; set; }

        public Uri RedirectUri { get; set; }
    }
}
