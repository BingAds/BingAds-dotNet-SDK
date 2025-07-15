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
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an account that can be read or written in a bulk file. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Account record in a bulk file.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Account</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAccount : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the account.
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The identifier of the customer that contains the account.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// The date and time that you last synced your account using the bulk service. 
        /// You should keep track of this value in UTC time. 
        /// Corresponds to the 'Sync Time' field in the bulk file. 
        /// </summary>
        public DateTime SyncTime { get; set; }

        /// <summary>
        /// auto-tagging of the MSCLKID query string parameter is enabled or not.
        /// Corresponds to the 'MSCLKID Auto Tagging Enabled' field in the bulk file. 
        /// </summary>
        public bool? MSCLKIDAutoTaggingEnabled { get; set; }

        /// <summary>
        /// Corresponds to the 'Include View Through Conversions' field in the bulk file. 
        /// </summary>
        public bool? IncludeViewThroughConversions { get; set; }

        /// <summary>
        /// Corresponds to the 'Profile Expansion Enabled' field in the bulk file. 
        /// </summary>
        public bool? ProfileExpansionEnabled { get; set; }

        /// <summary>
        /// The tracking template to use as default for all URLs in your account.
        /// Corresponds to the 'Tracking Template' field in the bulk file. 
        /// </summary>
        public string TrackingUrlTemplate { get; set; }

        /// <summary>
        /// The Final Url Suffix in the account.
        /// Corresponds to the 'Final Url Suffix' field in the bulk file. 
        /// </summary>
        public string FinalUrlSuffix { get; set; }

        /// <summary>
        /// Corresponds to the 'Ad Click Parallel Tracking' field in the bulk file. 
        /// </summary>
        public bool? AdClickParallelTracking { get; set; }

        /// <summary>
        /// Corresponds to the 'Auto Apply Recommendations' field in the bulk file. 
        /// </summary>
        public IDictionary<string, bool> AutoApplyRecommendations { get; set; }

        /// <summary>
        /// Corresponds to the 'Allow Image Auto Retrieve' field in the bulk file. 
        /// </summary>
        public bool? AllowImageAutoRetrieve { get; set; }

        /// <summary>
        /// Corresponds to the 'Business Attributes' field in the bulk file. 
        /// </summary>
        public IList<string> BusinessAttributes { get; set; }

        /// <summary>
        /// Corresponds to the 'Netflix TC Accepted' field in the bulk file. 
        /// </summary>
        //public bool? NetflixTCAccepted { get; set; }

        private static readonly IBulkMapping<BulkAccount>[] Mappings =
        {
            new SimpleBulkMapping<BulkAccount>(StringTable.Id,
                c => c.Id.ToBulkString(),
                (v, c) => c.Id = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.ParentId,
                c => c.CustomerId.ToBulkString(),
                (v, c) => c.CustomerId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.SyncTime,
                c => c.SyncTime.ToBulkString(),
                (v, c) => c.SyncTime = v.ParseDateTime()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.MSCLKIDAutoTaggingEnabled,
                c => c.MSCLKIDAutoTaggingEnabled?.ToString(),
                (v, c) => c.MSCLKIDAutoTaggingEnabled = v.ParseOptional<bool>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.ProfileExpansionEnabled,
                c => c.ProfileExpansionEnabled?.ToString(),
                (v, c) => c.ProfileExpansionEnabled = v.ParseOptional<bool>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.IncludeViewThroughConversions,
                c => c.IncludeViewThroughConversions?.ToString(),
                (v, c) => c.IncludeViewThroughConversions = v.ParseOptional<bool>()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.TrackingTemplate,
                c => c.TrackingUrlTemplate.ToOptionalBulkString(c.Id),
                (v, c) => c.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.FinalUrlSuffix,
            c => c.FinalUrlSuffix.ToOptionalBulkString(c.Id),
            (v, c) => c.FinalUrlSuffix = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.AdClickParallelTracking,
            c => c.AdClickParallelTracking?.ToString(),
            (v, c) => c.AdClickParallelTracking = v.ParseOptional < bool >()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.AutoApplyRecommendations,
            c => c.AutoApplyRecommendations.WriteAutoApplyRecommendations(";"),
            (v, c) => c.AutoApplyRecommendations = v.ParseAutoApplyRecommendations()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.AllowImageAutoRetrieve,
            c => c.AllowImageAutoRetrieve?.ToString(),
            (v, c) => c.AllowImageAutoRetrieve = v.ParseOptional < bool >()
            ),

            new SimpleBulkMapping<BulkAccount>(StringTable.BusinessAttributes,
            c => c.BusinessAttributes.WriteBusinessAttributes(";"),
            (v, c) => c.BusinessAttributes = v.ParseBusinessAttributes()
            ),
            /*
            new SimpleBulkMapping<BulkAccount>(StringTable.NetflixTCAccepted,
            c => c.NetflixTCAccepted?.ToString(),
            (v, c) => c.NetflixTCAccepted = v.ParseOptional<bool>()
            ),
            */
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }
    }
}
