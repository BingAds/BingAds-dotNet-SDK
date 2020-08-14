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

using System.Linq;
using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a filter link ad extension that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkFilterLinkAdExtension.FilterLinkAdExtension"/> property that can be read and written 
    /// as fields of the Filter Link Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Filter Link Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkFilterLinkAdExtension : BulkAdExtensionBase<FilterLinkAdExtension>
    {
        /// <summary>
        /// The filter link ad extension.
        /// </summary>
        public FilterLinkAdExtension FilterLinkAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkFilterLinkAdExtension>[] Mappings =
        {

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.AdExtensionHeaderType,
                c => c.FilterLinkAdExtension.AdExtensionHeaderType.ToBulkString(),
                (v, c) =>
                {
                    var extension = c.FilterLinkAdExtension as FilterLinkAdExtension;

                    if (extension == null) return;

                    AdExtensionHeaderType? adExtensionHeaderType = v.ParseOptional<AdExtensionHeaderType>();
                    if (adExtensionHeaderType != null)
                    {
                        c.FilterLinkAdExtension.AdExtensionHeaderType = (AdExtensionHeaderType)adExtensionHeaderType;
                    }
                }
            ),

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.Language,
                c => c.FilterLinkAdExtension.Language,
                (v, c) => c.FilterLinkAdExtension.Language = v
            ),

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.Texts,
                c => c.FilterLinkAdExtension.Texts.WriteDelimitedStrings(";"),
                (v, c) => c.FilterLinkAdExtension.Texts = v.ParseDelimitedStrings()
            ),

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.FinalUrl,
                c => c.FilterLinkAdExtension.FinalUrls.WriteUrls("; ", c.FilterLinkAdExtension.Id),
                (v, c) => c.FilterLinkAdExtension.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.FinalMobileUrl,
                c => c.FilterLinkAdExtension.FinalMobileUrls.WriteUrls("; ", c.FilterLinkAdExtension.Id),
                (v, c) => c.FilterLinkAdExtension.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.TrackingTemplate,
                c => c.FilterLinkAdExtension.TrackingUrlTemplate.ToOptionalBulkString(c.FilterLinkAdExtension.Id),
                (v, c) => c.FilterLinkAdExtension.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkFilterLinkAdExtension>(StringTable.CustomParameter,
                c => c.FilterLinkAdExtension.UrlCustomParameters.ToBulkString(c.FilterLinkAdExtension.Id),
                (v, c) => c.FilterLinkAdExtension.UrlCustomParameters = v.ParseCustomParameters()
            ),

        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            FilterLinkAdExtension = new FilterLinkAdExtension { Type = "FilterLinkAdExtension" };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(FilterLinkAdExtension, "FilterLinkAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
