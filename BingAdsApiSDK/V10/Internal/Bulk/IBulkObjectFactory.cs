namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal interface IBulkObjectFactory
    {
        string GetBulkRowType(BulkObject bulkObject);
        BulkObject CreateBulkObject(RowValues values);
    }
}