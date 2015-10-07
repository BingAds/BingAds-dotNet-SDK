using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal class FormatVersion : BulkObject
    {
        public string Value { get; internal set; }

        private static readonly IBulkMapping<FormatVersion>[] Mappings =
        {
            new SimpleBulkMapping<FormatVersion>(StringTable.Name,
                c => c.Value,
                (v, c) => c.Value = v
            ),
        };

        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }
    }
}
