//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.5
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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V11.Internal;
using Microsoft.BingAds.V11.Internal.Bulk;
using Microsoft.BingAds.V11.Internal.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;
using Microsoft.BingAds.V11.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V11.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a sitelink2 ad extension. 
    /// This class exposes the <see cref="Sitelink2AdExtension"/> property that can be read and written 
    /// as fields of the Sitelink2 Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Sitelink2 Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkSitelink2AdExtension : BulkAdExtensionBase<Sitelink2AdExtension>
    {
        /// <summary>
        /// The sitelink2 ad extension.
        /// </summary>
        public Sitelink2AdExtension Sitelink2AdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkSitelink2AdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.SiteLinkDestinationUrl,
                c => c.Sitelink2AdExtension.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.Sitelink2AdExtension.DestinationUrl = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.SiteLinkDescription1,
                c => c.Sitelink2AdExtension.Description1,
                (v, c) => c.Sitelink2AdExtension.Description1 = v
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.SiteLinkDescription2,
                c => c.Sitelink2AdExtension.Description2,
                (v, c) => c.Sitelink2AdExtension.Description2 = v
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.DevicePreference,
                c => c.Sitelink2AdExtension.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.Sitelink2AdExtension.DevicePreference = v.ParseDevicePreference()
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.SiteLinkDisplayText,
                c => c.Sitelink2AdExtension.DisplayText,
                (v, c) => c.Sitelink2AdExtension.DisplayText = v
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.FinalUrl,
                c => c.Sitelink2AdExtension.FinalUrls.WriteUrls("; "),
                (v, c) => c.Sitelink2AdExtension.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.FinalMobileUrl,
                c => c.Sitelink2AdExtension.FinalMobileUrls.WriteUrls("; "),
                (v, c) => c.Sitelink2AdExtension.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.TrackingTemplate,
                c => c.Sitelink2AdExtension.TrackingUrlTemplate.ToOptionalBulkString(),
                (v, c) => c.Sitelink2AdExtension.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkSitelink2AdExtension>(StringTable.CustomParameter,
                c => c.Sitelink2AdExtension.UrlCustomParameters.ToBulkString(),
                (v, c) => c.Sitelink2AdExtension.UrlCustomParameters = v.ParseCustomParameters()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Sitelink2AdExtension = new Sitelink2AdExtension
            {
                Type = "Sitelink2AdExtension",
            };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(Sitelink2AdExtension, "Sitelink2AdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
