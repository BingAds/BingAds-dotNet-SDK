using System;

namespace Microsoft.BingAds.Internal.Bulk.Mappings
{
    internal class ConditionalBulkMapping<T> : IBulkMapping<T>
    {
        private readonly string _csvHeader;

        public string CsvHeader
        {
            get { return _csvHeader; }
        }

        private readonly Func<T, bool> _condition; 

        private readonly Func<T, string> _fieldToCsv;

        private readonly Action<string, T> _csvToField;

        public ConditionalBulkMapping(string csvHeader, Func<T, bool> condition, Func<T, string> fieldToCsv, Action<string, T> csvToField)
        {
            _csvHeader = csvHeader;
            _condition = condition;
            _fieldToCsv = fieldToCsv;
            _csvToField = csvToField;
        }

        public void ConvertToCsv(T entity, RowValues values)
        {
            if (_condition(entity))
            {
                values[_csvHeader] = _fieldToCsv(entity);
            }
        }

        public void ConvertToEntity(RowValues values, T entity)
        {
            if (_condition(entity))
            {
                _csvToField(values[_csvHeader], entity);
            }
        }
    }
}
