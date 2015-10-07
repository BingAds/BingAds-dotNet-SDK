
using System;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal interface IBulkObjectReader : IDisposable
    {
        BulkObject ReadNextBulkObject();
    }
}