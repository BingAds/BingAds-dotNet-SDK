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

using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// The best position, main line, and first page bid suggestion data corresponding to one keyword. 
    /// If the requested <see cref="SubmitDownloadParameters.DataScope"/> includes BidSuggestionsData, 
    /// the download will include bulk records corresponding to the properties of this class. 
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{DownloadStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    public class BidSuggestionData
    {
        /// <summary>
        /// Represents a best position bid suggestion that is derived from <see cref="BulkObject"/> and can only be read from a bulk file by the 
        /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
        /// An instance of this class can represent a single best position bid, and thus one record in the bulk file. 
        /// Properties of this class and of classes that it is derived from, correspond to fields of the Keyword Best Position Bid record in a bulk file.
        /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Keyword Best Position Bid</see>. 
        /// </summary>
        public BulkKeywordBidSuggestion BestPosition { get; internal set; }

        /// <summary>
        /// Represents a main line bid suggestion that is derived from <see cref="BulkObject"/> and can only be read from a bulk file by the 
        /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
        /// An instance of this class can represent a single main line bid, and thus one record in the bulk file. 
        /// Properties of this class and of classes that it is derived from, correspond to fields of the Keyword Main Line Bid record in a bulk file.
        /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Keyword Main Line Bid</see>. 
        /// </summary>
        public BulkKeywordBidSuggestion MainLine { get; internal set; }

        /// <summary>
        /// Represents a first page bid suggestion that is derived from <see cref="BulkObject"/> and can only be read from a bulk file by the 
        /// <see cref="BulkFileReader"/> when reading the corresponding <see cref="BulkKeyword"/>. 
        /// An instance of this class can represent a single first page bid, and thus one record in the bulk file. 
        /// Properties of this class and of classes that it is derived from, correspond to fields of the Keyword First Page Bid record in a bulk file.
        /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Keyword First Page Bid</see>. 
        /// </summary>
        public BulkKeywordBidSuggestion FirstPage { get; internal set; }
    }
}
