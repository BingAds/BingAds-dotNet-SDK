
using System;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal interface IBulkObjectReader : IDisposable
    {
        BulkObject ReadNextBulkObject();
    }
}