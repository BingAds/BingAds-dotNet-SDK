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
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an video ad extension that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkVideoAdExtension.VideoAdExtension"/> property that can be read and written 
    /// as fields of the Video Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Video Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkVideoAdExtension : BulkAdExtensionBase<VideoAdExtension>
    {
        /// <summary>
        /// The video ad extension.
        /// </summary>
        public VideoAdExtension VideoAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkVideoAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.Name,
                c => c.VideoAdExtension.Name.ToOptionalBulkString(c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.Name = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.DisplayText,
                c => c.VideoAdExtension.DisplayText.ToOptionalBulkString(c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.DisplayText = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.AltText,
                c => c.VideoAdExtension.AlternativeText.ToOptionalBulkString(c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.AlternativeText = v.GetValueOrEmptyString()
            ), 
            
            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.ActionText,
                c => c.VideoAdExtension.ActionText.ToOptionalBulkString(c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.ActionText = v.GetValueOrEmptyString()
            ), 
            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.ThumbnailUrl,
                c => c.VideoAdExtension.ThumbnailUrl,
                (v, c) => c.VideoAdExtension.ThumbnailUrl = v
            ), 
            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.ThumbnailId,
                c => c.VideoAdExtension.ThumbnailId.ToBulkString(),
                (v, c) => c.VideoAdExtension.ThumbnailId = v.ParseOptional<long>()
            ),
            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.VideoId,
                c => c.VideoAdExtension.VideoId.ToBulkString(),
                (v, c) => c.VideoAdExtension.VideoId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.FinalUrl,
                c => c.VideoAdExtension.FinalUrls.WriteUrls("; ", c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.FinalMobileUrl,
                c => c.VideoAdExtension.FinalMobileUrls.WriteUrls("; ", c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.TrackingTemplate,
                c => c.VideoAdExtension.TrackingUrlTemplate.ToOptionalBulkString(c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.CustomParameter,
                c => c.VideoAdExtension.UrlCustomParameters.ToBulkString(c.VideoAdExtension.Id),
                (v, c) => c.VideoAdExtension.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkVideoAdExtension>(StringTable.FinalUrlSuffix,
                c => c.VideoAdExtension.FinalUrlSuffix.ToOptionalBulkString(c.AdExtension.Id),
                (v, c) => c.VideoAdExtension.FinalUrlSuffix = v.GetValueOrEmptyString()
            )
        }; 

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            VideoAdExtension = new VideoAdExtension { Type = "VideoAdExtension" };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(VideoAdExtension, "VideoAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
