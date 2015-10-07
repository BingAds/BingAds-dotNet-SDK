using System.Collections.Generic;
using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// Reserved to support new record types that may be added to the Bulk schema. 
    /// </summary>
    public class UnknownBulkEntity : SingleRecordBulkEntity
    {
        /// <summary>
        /// The forward compatibility map of fields and values. 
        /// </summary>
        public IDictionary<string, string> Values { get; private set; }        
        
        internal override void ProcessMappingsFromRowValues(RowValues rowValues)
        {
            Values = rowValues.ToDictionary();
        }

        internal override void ProcessMappingsToRowValues(RowValues rowValues, bool excludeReadonlyData)
        {
            foreach (var pair in Values)
            {
                rowValues[pair.Key] = pair.Value;
            }
        }
    }
}
