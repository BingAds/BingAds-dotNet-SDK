using Microsoft.BingAds.V10.Internal;
using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// Represents a subset of the fields available in bulk records that support quality score data, for example <see cref="BulkKeyword"/>. 
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620269">Bulk File Schema</see>. 
    /// </summary>
    public class QualityScoreData
    {
        /// <summary>
        /// Corresponds to the 'Quality Score' field in the bulk file. 
        /// </summary>
        public int? QualityScore { get; private set; }

        /// <summary>
        /// Corresponds to the 'Keyword Relevance' field in the bulk file. 
        /// </summary>
        public int? KeywordRelevance { get; private set; }

        /// <summary>
        /// Corresponds to the 'Landing Page Relevance' field in the bulk file. 
        /// </summary>
        public int? LandingPageRelevance { get; private set; }

        /// <summary>
        /// Corresponds to the 'Landing Page User Experience' field in the bulk file. 
        /// </summary>
        public int? LandingPageUserExperience { get; private set; }

        private static readonly IBulkMapping<QualityScoreData>[] Mappings =
        {
            new SimpleBulkMapping<QualityScoreData>(StringTable.QualityScore,                
                c => c.QualityScore.ToBulkString(),
                (v, c) => c.QualityScore = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<QualityScoreData>(StringTable.KeywordRelevance,                
                c => c.KeywordRelevance.ToBulkString(),
                (v, c) => c.KeywordRelevance = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<QualityScoreData>(StringTable.LandingPageRelevance,                
                c => c.LandingPageRelevance.ToBulkString(),
                (v, c) => c.LandingPageRelevance = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<QualityScoreData>(StringTable.LandingPageUserExperience,                
                c => c.LandingPageUserExperience.ToBulkString(),
                (v, c) => c.LandingPageUserExperience = v.ParseOptional<int>()
            )
        };

        internal static QualityScoreData ReadFromRowValuesOrNull(RowValues values)
        {
            var qualityScoreData = new QualityScoreData();

            qualityScoreData.ReadFromRowValues(values);

            return qualityScoreData.HasAnyValues ? qualityScoreData : null;
        }

        internal static void WriteToRowValuesIfNotNull(QualityScoreData qualityScoreData, RowValues values)
        {
            if (qualityScoreData != null)
            {
                qualityScoreData.WriteToRowValues(values);
            }
        }

        private void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        private void WriteToRowValues(RowValues values)
        {
            this.ConvertToValues(values, Mappings);
        }

        private bool HasAnyValues
        {
            get
            {
                return 
                    QualityScore.HasValue || 
                    KeywordRelevance.HasValue || 
                    LandingPageRelevance.HasValue || 
                    LandingPageUserExperience.HasValue;
            }
        }
    }
}
