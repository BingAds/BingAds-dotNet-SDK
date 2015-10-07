namespace Microsoft.BingAds.Internal.Utilities
{
    internal interface IZipExtractor
    {
        string ExtractFirstEntryToFile(string zipFilePath, string resultFilePath, bool setZippedFileExtension, bool overwrite);

        void CompressFile(string sourceFilePath, string resultFilePath);
    }
}
