using System;
using System.Collections.Generic;
using Microsoft.BingAds.V10.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal static class MappingExtensions
    {
        public static void ConvertToEntity<T>(this RowValues values, T entity, IEnumerable<IBulkMapping<T>> mappings)
        {
            foreach (var mapping in mappings)
            {
                try
                {
                    mapping.ConvertToEntity(values, entity);
                }
                catch (InvalidCastException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (FormatException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (OverflowException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (ArgumentNullException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (ArgumentException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (NullReferenceException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
            }
        }

        private static Exception CreateEntityParsingException<T>(RowValues values, IBulkMapping<T> mapping, Exception ex)
        {
            var entityType = typeof (T).Name;

            var simpleMapping = mapping as SimpleBulkMapping<T>;

            var message = simpleMapping != null
                ? string.Format("Couldn't parse column {0} of {1} entity: {2}", simpleMapping.CsvHeader, entityType,
                    ex.Message)
                : string.Format("Couldn't parse {0} entity: {1}", entityType, ex.Message);

            message += " See ColumnValues for detailed row information and InnerException for error details.";

            return new EntityReadException(message, values.ToDebugString(), ex);
        }

        public static void ConvertToValues<T>(this T entity, RowValues values, IEnumerable<IBulkMapping<T>> mappings)
        {
            foreach (var mapping in mappings)
            {
                try
                {
                    mapping.ConvertToCsv(entity, values);
                }
                catch (ArgumentException ex)
                {
                    throw CreateEntityWriteException(mapping, ex);
                }
                catch (NullReferenceException ex)
                {
                    throw CreateEntityWriteException(mapping, ex);
                }
            }
        }

        private static Exception CreateEntityWriteException<T>(IBulkMapping<T> mapping, Exception ex)
        {
            var entityType = typeof (T).Name;

            var simpleMapping = mapping as SimpleBulkMapping<T>;

            var message = simpleMapping != null
                ? string.Format("Couldn't write column {0} of {1} entity: {2}", simpleMapping.CsvHeader, entityType,
                    ex.Message)
                : string.Format("Couldn't write {0} entity: {1}", entityType, ex.Message);

            message += " See InnerException for error details.";

            return new EntityWriteException(message, ex);
        }
    }
}