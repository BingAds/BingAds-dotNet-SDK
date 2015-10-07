
namespace Microsoft.BingAds.V10.Internal.Bulk.Mappings
{
    internal interface IBulkMapping<in T>   
    {
        void ConvertToCsv(T entity, RowValues values);

        void ConvertToEntity(RowValues values, T entity);
    }
}