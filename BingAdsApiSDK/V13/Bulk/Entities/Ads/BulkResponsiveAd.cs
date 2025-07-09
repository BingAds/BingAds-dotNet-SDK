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
    /// Represents a responsive ad. 
    /// This class exposes the <see cref="BulkResponsiveAd.ResponsiveAd"/> property that can be read and written as fields of the Responsive Ad record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Responsive Ad</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkResponsiveAd : BulkAd<ResponsiveAd>
    {
        /// <summary>
        /// <para>
        /// The responsive ad. 
        /// </para>
        /// </summary>
        public ResponsiveAd ResponsiveAd
        {
            get { return Ad; }
            set { Ad = value; }
        }

        private static readonly IBulkMapping<BulkResponsiveAd>[] Mappings =
        {
            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.BusinessName,
                c => c.ResponsiveAd.BusinessName,
                (v, c) => c.ResponsiveAd.BusinessName = v
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.CallToAction,
                c => c.ResponsiveAd.CallToAction.ToBulkString(),
                (v, c) => c.ResponsiveAd.CallToAction = v.ParseOptional<CallToAction>()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.CallToActionLanguage,
                c => c.ResponsiveAd.CallToActionLanguage.ToBulkString(),
                (v, c) => c.ResponsiveAd.CallToActionLanguage = v.ParseOptional<LanguageName>()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.Descriptions,
                c => c.ResponsiveAd.Descriptions.ToTextAssetLinksBulkString(),
                (v, c) => c.ResponsiveAd.Descriptions = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.Headline,
                c => c.ResponsiveAd.Headline,
                (v, c) => c.ResponsiveAd.Headline = v
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.Headlines,
                c => c.ResponsiveAd.Headlines.ToTextAssetLinksBulkString(),
                (v, c) => c.ResponsiveAd.Headlines = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.Images,
                c => c.ResponsiveAd.Images == null ? null : c.ResponsiveAd.Images.ToImageAssetLinksBulkString(),
                (v, c) => c.ResponsiveAd.Images = v.ParseImageAssetLinks()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.LongHeadline,
                c => c.ResponsiveAd.LongHeadlineString,
                (v, c) => c.ResponsiveAd.LongHeadlineString = v
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.LongHeadlines,
                c => c.ResponsiveAd.LongHeadlines.ToTextAssetLinksBulkString(),
                (v, c) => c.ResponsiveAd.LongHeadlines = v.ParseTextAssetLinks()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.Text,
                c => c.ResponsiveAd.Text,
                (v, c) => c.ResponsiveAd.Text = v
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.ImpressionTrackingUrls,
                c => c.ResponsiveAd.ImpressionTrackingUrls.WriteDelimitedStrings(";", c.ResponsiveAd.Id),
                (v, c) => c.ResponsiveAd.ImpressionTrackingUrls = v.ParseDelimitedStrings()
            ),

            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.Videos,
                c => c.ResponsiveAd.Videos == null ? null : c.ResponsiveAd.Videos.ToVideoAssetLinksBulkString(),
                (v, c) => c.ResponsiveAd.Videos = v.ParseVideoAssetLinks()
            ),
            new SimpleBulkMapping<BulkResponsiveAd>(StringTable.VerifiedTrackingDatas,
                c =>
                {
                    return c.ResponsiveAd.VerifiedTrackingSettings.WriteVerifiedTrackingDataToBulkString(c.ResponsiveAd.Id);
                },
                (v, c) =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        c.ResponsiveAd.VerifiedTrackingSettings = new VerifiedTrackingSetting();
                        v.ParseVerifiedTrackingData(c.ResponsiveAd.VerifiedTrackingSettings);
                    }
                }
            ),

        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ResponsiveAd = new ResponsiveAd { Type = AdType.ResponsiveAd };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }
        
        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ResponsiveAd, "ResponsiveAd");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
