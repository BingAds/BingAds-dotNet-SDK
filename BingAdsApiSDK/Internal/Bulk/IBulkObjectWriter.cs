using System;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal interface IBulkObjectWriter : IDisposable
    {
        void WriteFileMetadata();

        void WriteObjectRow(BulkObject bulkObject);

        void WriteObjectRow(BulkObject bulkObject, bool excludeReadonlyData);
    }
}