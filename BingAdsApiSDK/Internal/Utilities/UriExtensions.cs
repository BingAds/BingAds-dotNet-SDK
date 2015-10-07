using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BingAds.Internal.Utilities
{
    internal static class UriExtensions
    {
        public static Dictionary<string, string> ParseFragment(this Uri uri)
        {
            var fragment = uri.Fragment.Substring(1);

            return ParseUri(fragment);
        }

        public static Dictionary<string, string> ParseQuery(this Uri uri)
        {
            var fragment = uri.Query.Substring(1);

            return ParseUri(fragment);
        }

        private static Dictionary<string, string> ParseUri(string uri)
        {
            var dict =
                (from pairs in uri.Split('&')
                    select pairs.Split('=')
                    into parts
                    select new
                    {
                        name = parts[0],
                        val = parts[1]
                    })
                    .ToDictionary(x => x.name, x => x.val);

            return dict;
        }
    }
}
