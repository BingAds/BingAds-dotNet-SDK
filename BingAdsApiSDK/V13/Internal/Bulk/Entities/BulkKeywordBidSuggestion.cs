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

using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
{
    /// <summary>
    /// Represents a best position bid suggestion that can only be read from a bulk file by the 
    /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
    /// An instance of this class can represent a single keyword bid position, and thus one record in the bulk file. 
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{DownloadStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    public class BulkKeywordBidSuggestion : BulkObject
    {
        /// <summary>
        /// The keyword corresponding to the suggested bid.
        /// Corresponds to the 'Keyword' field in the bulk file.
        /// </summary>
        public string KeywordText { get; private set; }

        /// <summary>
        /// The suggested bid for the keyword.
        /// Corresponds to the 'Bid' field in the bulk file.
        /// </summary>
        public double? Bid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BulkKeywordBidSuggestion class.
        /// </summary>
        public BulkKeywordBidSuggestion()
        {
        }

        private static readonly IBulkMapping<BulkKeywordBidSuggestion>[] Mappings =
        {
            new SimpleBulkMapping<BulkKeywordBidSuggestion>(StringTable.Keyword,
                c => c.KeywordText,
                (v, c) => c.KeywordText = v
            ),

            new SimpleBulkMapping<BulkKeywordBidSuggestion>(StringTable.Bid,
                c => c.Bid.ToBulkString(),
                (v, c) => c.Bid = v.ParseOptional<double>()
            )
        };

        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }
    }
}
