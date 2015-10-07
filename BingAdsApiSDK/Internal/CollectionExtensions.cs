using System.Collections.Generic;

namespace Microsoft.BingAds.Internal
{
    internal static class CollectionExtensions
    {
        public static void AddRange<T>(this IList<T> targetList, IEnumerable<T> sourceList)
        {
            foreach (var sourceElement in sourceList)
            {
                targetList.Add(sourceElement);
            }
        }
    }
}
