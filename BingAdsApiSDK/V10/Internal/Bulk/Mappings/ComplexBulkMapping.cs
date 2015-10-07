using System;

namespace Microsoft.BingAds.V10.Internal.Bulk.Mappings
{
    internal class ComplexBulkMapping<T> : IBulkMapping<T>
    {
        public Action<T, RowValues> EntityToCsv;

        public Action<RowValues, T> CsvToEntity;

        public ComplexBulkMapping(Action<T, RowValues> entityToCsv, Action<RowValues, T> csvToEntity)
        {
            EntityToCsv = entityToCsv;
            CsvToEntity = csvToEntity;
        }

        public void ConvertToCsv(T entity, RowValues values)
        {
            EntityToCsv(entity, values);
        }

        public void ConvertToEntity(RowValues values, T entity)
        {
            CsvToEntity(values, entity);
        }
    }
}