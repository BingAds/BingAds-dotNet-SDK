namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal interface ICsvTextFormatter
    {
        string FormatCsvRow(string[] columns);
        string GetHeaders();
    }
}