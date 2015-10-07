using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal class BulkFileReaderFactory : IBulkFileReaderFactory
    {
        public BulkFileReader CreateBulkFileReader(string bulkFilePath, ResultFileType bulkFileType, DownloadFileType bulkFileFormat)
        {
            return new BulkFileReader(bulkFilePath, bulkFileType, bulkFileFormat);
        }
    }
}
