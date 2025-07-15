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
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a flyer ad extension. 
    /// This class exposes the <see cref="FlyerAdExtension"/> property that can be read and written 
    /// as fields of the Flyer Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Flyer Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkFlyerAdExtension : BulkAdExtensionBase<FlyerAdExtension>
    {
        /// <summary>
        /// The flyer ad extension.
        /// </summary>
        public FlyerAdExtension FlyerAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkFlyerAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.FlyerName,
                c => c.FlyerAdExtension.FlyerName,
                (v, c) => c.FlyerAdExtension.FlyerName = v
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.MerchantCenterId,
                c => c.FlyerAdExtension.StoreId.ToBulkString(),
                (v, c) => c.FlyerAdExtension.StoreId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.Description,
                c => c.FlyerAdExtension.Description,
                (v, c) => c.FlyerAdExtension.Description = v
            ),
            
            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.MediaIds,
                c =>
                {
                    if (c.FlyerAdExtension.ImageMediaIds == null || c.FlyerAdExtension.ImageMediaIds.Count == 0)
                    {
                        return null;
                    }
                    return string.Join(";", c.FlyerAdExtension.ImageMediaIds);
                },
                (v, c) =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        c.FlyerAdExtension.ImageMediaIds = v.Split(';').Select(long.Parse).ToList();
                    }
                }
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.MediaUrls,
                c => c.FlyerAdExtension.ImageMediaUrls.WriteUrls("; ", c.FlyerAdExtension.Id),
                (v, c) => c.FlyerAdExtension.ImageMediaUrls = v.ParseUrls()
            ), 
            
            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.FinalUrl,
                c => c.FlyerAdExtension.FinalUrls.WriteUrls("; ", c.FlyerAdExtension.Id),
                (v, c) => c.FlyerAdExtension.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.FinalMobileUrl,
                c => c.FlyerAdExtension.FinalMobileUrls.WriteUrls("; ", c.FlyerAdExtension.Id),
                (v, c) => c.FlyerAdExtension.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.TrackingTemplate,
                c => c.FlyerAdExtension.TrackingUrlTemplate.ToOptionalBulkString(c.FlyerAdExtension.Id),
                (v, c) => c.FlyerAdExtension.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.CustomParameter,
                c => c.FlyerAdExtension.UrlCustomParameters.ToBulkString(c.FlyerAdExtension.Id),
                (v, c) => c.FlyerAdExtension.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkFlyerAdExtension>(StringTable.FinalUrlSuffix,
                c => c.FlyerAdExtension.FinalUrlSuffix.ToOptionalBulkString(c.AdExtension.Id),
                (v, c) => c.FlyerAdExtension.FinalUrlSuffix = v.GetValueOrEmptyString()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            FlyerAdExtension = new FlyerAdExtension
            {
                Type = "FlyerAdExtension",
            };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(FlyerAdExtension, "FlyerAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
