using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal class BulkFileReaderFactory : IBulkFileReaderFactory
    {
        public BulkFileReader CreateBulkFileReader(string bulkFilePath, ResultFileType bulkFileType, DownloadFileType bulkFileFormat)
        {
            return new BulkFileReader(bulkFilePath, bulkFileType, bulkFileFormat);
        }
    }
}
