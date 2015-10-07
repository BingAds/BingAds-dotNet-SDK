using System.IO;
using System.IO.Compression;

namespace Microsoft.BingAds.Internal.Utilities
{
    internal class ZipExtractor : IZipExtractor
    {
        public string ExtractFirstEntryToFile(string zipFilePath, string resultFilePath, bool setZippedFileExtension, bool overwrite)
        {
            using (var archive = ZipFile.OpenRead(zipFilePath))
            {
                var firstEntry = archive.Entries[0];

                var extension = Path.GetExtension(firstEntry.Name);

                var effectiveResultFilePath = setZippedFileExtension ? Path.ChangeExtension(resultFilePath, extension) : resultFilePath;

                firstEntry.ExtractToFile(effectiveResultFilePath, overwrite);

                return effectiveResultFilePath;
            } 
        }

        public void CompressFile(string sourceFilePath, string resultFilePath)
        {            
            using (var archive = ZipFile.Open(resultFilePath, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(sourceFilePath, Path.GetFileName(sourceFilePath));
            }
        }
    }
}
