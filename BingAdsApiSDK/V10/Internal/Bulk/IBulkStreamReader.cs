using System;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal interface IBulkStreamReader : IDisposable
    {        
        BulkObject Read();

        bool TryRead<T>(out T result)
            where T: BulkObject;

        bool TryRead<T>(Predicate<T> predicate, out T result)
            where T : BulkObject;
    }
}