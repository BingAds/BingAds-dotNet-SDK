using System;

namespace Microsoft.BingAds.V10.Internal.Bulk.Mappings
{
    internal class SimpleBulkMapping<T> : IBulkMapping<T>
    {
        private readonly string _csvHeader;

        public string CsvHeader
        {
            get { return _csvHeader; }
        }

        private readonly Func<T, string> _fieldToCsv;

        private readonly Action<string, T> _csvToField;

        public SimpleBulkMapping(string csvHeader, Func<T, string> fieldToCsv, Action<string, T> csvToField)
        {
            _csvHeader = csvHeader;
            _fieldToCsv = fieldToCsv;
            _csvToField = csvToField;
        }

        public void ConvertToCsv(T entity, RowValues values)
        {
            values[_csvHeader] = _fieldToCsv(entity);
        }

        public void ConvertToEntity(RowValues values, T entity)
        {
            _csvToField(values[_csvHeader], entity);
        }
    }
}