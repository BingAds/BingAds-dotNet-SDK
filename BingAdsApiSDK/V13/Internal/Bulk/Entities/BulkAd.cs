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

using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk ad classes.
    /// </summary>
    /// <typeparam name="T">The type of ad from the <see cref="Microsoft.BingAds.V13.CampaignManagement"/> namespace, for example a <see cref="TextAd"/> object.</typeparam>
    /// <seealso cref="BulkProductAd"/>
    /// <seealso cref="BulkTextAd"/>
    /// <seealso cref="BulkAppInstallAd"/>
    /// <seealso cref="BulkExpandedTextAd"/>
    /// <seealso cref="BulkDynamicSearchAd"/>
    public abstract class BulkAd<T> : SingleRecordBulkEntity
        where T: Ad, new()
    {
        /// <summary>
        /// The identifier of the ad group that contains the ad.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the ad group that contains the ad.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        public string EditorialAppealStatus { get; set; }

        /// <summary>
        /// The type of ad from the <see cref="Microsoft.BingAds.V13.CampaignManagement"/> namespace, for example a <see cref="TextAd"/> object.
        /// </summary>
        protected T Ad { get; set; }

        private static readonly IBulkMapping<BulkAd<T>>[] Mappings =
        {
            new SimpleBulkMapping<BulkAd<T>>(StringTable.Status,
                c => c.Ad.Status.ToBulkString(),
                (v, c) => c.Ad.Status = v.ParseOptional<AdStatus>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.Id,
                c => c.Ad.Id.ToBulkString(),
                (v, c) => c.Ad.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.ParentId,
                c => c.AdGroupId.ToBulkString(),
                (v, c) => c.AdGroupId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.EditorialStatus,
                c => c.Ad.EditorialStatus.ToBulkString(),
                (v, c) => c.Ad.EditorialStatus = v.ParseOptional<AdEditorialStatus>()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.EditorialAppealStatus,
                c => c.EditorialAppealStatus,
                (v, c) => c.EditorialAppealStatus = v
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.DevicePreference,
                c => c.Ad.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.Ad.DevicePreference = v.ParseDevicePreference()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.AdFormatPreference,
                c => c.Ad.AdFormatPreference,
                (v, c) => c.Ad.AdFormatPreference = v
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.FinalUrl,
                c => c.Ad.FinalUrls.WriteUrls("; ", c.Ad.Id),
                (v, c) => c.Ad.FinalUrls = v.ParseUrls()
            ), 

            new SimpleBulkMapping<BulkAd<T>>(StringTable.FinalMobileUrl,
                c => c.Ad.FinalMobileUrls.WriteUrls("; ", c.Ad.Id),
                (v, c) => c.Ad.FinalMobileUrls = v.ParseUrls()
            ), 

            new SimpleBulkMapping<BulkAd<T>>(StringTable.TrackingTemplate,
                c => c.Ad.TrackingUrlTemplate.ToOptionalBulkString(c.Ad.Id),
                (v, c) => c.Ad.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAd<T>>(StringTable.CustomParameter,
                c => c.Ad.UrlCustomParameters.ToBulkString(c.Ad.Id),
                (v, c) => c.Ad.UrlCustomParameters = v.ParseCustomParameters()
            ),
            new SimpleBulkMapping<BulkAd<T>>(StringTable.FinalUrlSuffix,
                c => c.Ad.FinalUrlSuffix.ToOptionalBulkString(c.Ad.Id),
                (v, c) => c.Ad.FinalUrlSuffix = v.GetValueOrEmptyString()
            ),
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
