using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal interface IBulkFileReaderFactory
    {
        BulkFileReader CreateBulkFileReader(string bulkFilePath, ResultFileType bulkFileType, DownloadFileType bulkFileFormat);
    }
}
