//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
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

using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// Contains bulk file error details in a seperate record that corresponds to the record of a <see cref="BulkEntity"/> derived instance. 
    /// Properties of this class and of classes that it is derived from, correspond to error fields of the 'Error' records in a bulk file.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
    /// </summary>
    /// <example>
    /// If you upload a <see cref="BulkCampaign"/> without setting the campaign name 
    /// using <see cref="BulkServiceManager.UploadEntitiesAsync(EntityUploadParameters)"/>, and if you request 
    /// errors to be returned in the results using the corresponding <see cref="SubmitUploadParameters.ResponseMode"/>, then the upload result file 
    /// will contain a record that can be read with a <see cref="BulkFileReader"/> as an instance of <see cref="BulkError"/>.
    /// </example>
    public class BulkError : BulkObject
    {
        internal string Type { get; private set; }

        /// <summary>
        /// The error code, for example 'CampaignServiceEditorialValidationError'.
        /// Corresponds to the 'Error' field in the bulk file. 
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkId=511884">Bing Ads Operation Error Codes</see>.
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// The error number, for example '1042'.
        /// Corresponds to the 'Error Number' field in the bulk file. 
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkId=511884">Bing Ads Operation Error Codes</see>.
        /// </summary>
        public int? Number { get; private set; }

        /// <summary>
        /// The location of the entity property that resulted in the editorial error, for example 'AdDescription'.
        /// Corresponds to the 'Editorial Location' field in the bulk file. 
        /// </summary>
        public string EditorialLocation { get; private set; }

        /// <summary>
        /// The term that resulted in the editorial error, for example 'bing'.
        /// Corresponds to the 'Editorial Term' field in the bulk file. 
        /// </summary>
        public string EditorialTerm { get; private set; }

        /// <summary>
        /// The term that resulted in the editorial error, for example '17'.
        /// Corresponds to the 'Editorial Reason Code' field in the bulk file. 
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkId=511883">Bing Ads Editorial Failure Reason Codes</see>.
        /// </summary>
        public int? EditorialReasonCode { get; private set; }

        /// <summary>
        /// The publisher countries where editorial restriction is enforced, for example 'US'.
        /// Corresponds to the 'Publisher Countries' field in the bulk file. 
        /// </summary>
        /// <remarks>In a bulk file, the list of publisher countries are delimited with a semicolon (;).</remarks>
        public string PublisherCountries { get; private set; }

        private static readonly IBulkMapping<BulkError>[] Mappings = 
        {
            new SimpleBulkMapping<BulkError>(StringTable.Type,
                c => c.Type,
                (v, c) => c.Type = v
            ),

            new SimpleBulkMapping<BulkError>(StringTable.Error,
                c => c.Error,
                (v, c) => c.Error = v
            ),

            new SimpleBulkMapping<BulkError>(StringTable.ErrorNumber,
                c => c.Number.ToBulkString(),
                (v, c) => c.Number = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<BulkError>(StringTable.EditorialLocation,
                c => c.EditorialLocation,
                (v, c) => c.EditorialLocation = v
            ),

            new SimpleBulkMapping<BulkError>(StringTable.EditorialReasonCode,
                c => c.EditorialReasonCode.ToBulkString(),
                (v, c) => c.EditorialReasonCode = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<BulkError>(StringTable.EditorialTerm,
                c => c.EditorialTerm,
                (v, c) => c.EditorialTerm = v
            ),

            new SimpleBulkMapping<BulkError>(StringTable.PublisherCountries,
                c => c.PublisherCountries,
                (v, c) => c.PublisherCountries = v
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
