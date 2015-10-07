using System.Collections.Generic;
using Microsoft.BingAds.V10.CampaignManagement;

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    internal static class ProductConditionHelper
    {
        public const int MaxNumberOfConditions = 8;

        public static void AddConditionsFromRowValues(RowValues values, IList<ProductCondition> conditions)
        {
            var conditionHeaderPrefix = StringTable.ProductCondition1.Remove(StringTable.ProductCondition1.Length - 1);
            var valueHeaderPrefix = StringTable.ProductValue1.Remove(StringTable.ProductValue1.Length - 1);

            for (int i = 1; i <= MaxNumberOfConditions; i++)
            {
                string productCondition;
                string productValue;

                values.TryGetValue(conditionHeaderPrefix + i, out productCondition);
                values.TryGetValue(valueHeaderPrefix + i, out productValue);

                if (!string.IsNullOrEmpty(productCondition) || !string.IsNullOrEmpty(productValue))
                {
                    conditions.Add(new ProductCondition { Operand = productCondition, Attribute = productValue });
                }
            }
        }

        public static void AddRowValuesFromConditions(IList<ProductCondition> conditions, RowValues rowValues)
        {
            var conditionHeaderPrefix = StringTable.ProductCondition1.Remove(StringTable.ProductCondition1.Length - 1);
            var valueHeaderPrefix = StringTable.ProductValue1.Remove(StringTable.ProductValue1.Length - 1);

            for (var i = 1; i <= conditions.Count; i++)
            {
                rowValues[conditionHeaderPrefix + i] = conditions[i - 1].Operand;
                rowValues[valueHeaderPrefix + i] = conditions[i - 1].Attribute;
            }
        }
    }
}
