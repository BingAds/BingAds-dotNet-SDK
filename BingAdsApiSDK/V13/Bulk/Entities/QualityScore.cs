//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using Microsoft.BingAds.V13.Internal;
using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// Represents a subset of the fields available in bulk records that support quality score data, for example <see cref="BulkKeyword"/>. 
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>. 
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
