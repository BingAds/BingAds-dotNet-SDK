using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk.Operations
{
    internal interface IBulkFileRetriever
    {
        BulkFileReader GetFile();
    }
}