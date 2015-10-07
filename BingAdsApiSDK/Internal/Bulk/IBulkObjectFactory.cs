namespace Microsoft.BingAds.Internal.Bulk
{
    internal interface IBulkObjectFactory
    {
        string GetBulkRowType(BulkObject bulkObject);
        BulkObject CreateBulkObject(RowValues values);
    }
}