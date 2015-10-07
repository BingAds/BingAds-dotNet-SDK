using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk.Operations
{
    internal interface IBulkFileRetriever
    {
        BulkFileReader GetFile();
    }
}