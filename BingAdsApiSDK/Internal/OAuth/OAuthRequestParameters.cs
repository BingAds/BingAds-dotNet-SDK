using System;

namespace Microsoft.BingAds.Internal.OAuth
{
    internal class OAuthRequestParameters
    {        
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Uri RedirectUri { get; set; }

        public string GrantType { get; set; }

        public string GrantParamName { get; set; }

        public string GrantValue { get; set; }
    }
}