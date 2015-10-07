using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal interface IBulkFileReaderFactory
    {
        BulkFileReader CreateBulkFileReader(string bulkFilePath, ResultFileType bulkFileType, DownloadFileType bulkFileFormat);
    }
}
