using System;

namespace Microsoft.BingAds.V10.Internal.Bulk.Mappings
{
    internal class DynamicColumnNameMapping<T> : IBulkMapping<T>        
    {
        public Func<T, string> CsvHeader;

        public Func<T, string> FieldToCsv;

        public Action<string, T> CsvToField;

        public DynamicColumnNameMapping(Func<T, string> csvHeader, Func<T, string> fieldToCsv, Action<string, T> csvToField)
        {
            CsvHeader = csvHeader;
            FieldToCsv = fieldToCsv;
            CsvToField = csvToField;
        }

        public void ConvertToCsv(T entity, RowValues values)
        {
            values[CsvHeader(entity)] = FieldToCsv(entity);
        }

        public void ConvertToEntity(RowValues values, T entity)
        {
            CsvToField(values[CsvHeader(entity)], entity);
        }
    }
}
