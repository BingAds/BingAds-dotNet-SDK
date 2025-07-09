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
    /// Represents a promotion ad extension. 
    /// This class exposes the <see cref="BulkPromotionAdExtension.PromotionAdExtension"/> property that can be read and written 
    /// as fields of the Promotion Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Promotion Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkPromotionAdExtension : BulkAdExtensionBase<PromotionAdExtension>
    {
        /// <summary>
        /// The promotion ad extension.
        /// </summary>
        public PromotionAdExtension PromotionAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IBulkMapping<BulkPromotionAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.CurrencyCode,
                c => c.PromotionAdExtension.CurrencyCode,
                (v, c) => c.PromotionAdExtension.CurrencyCode = v
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.Language,
                c => c.PromotionAdExtension.Language,
                (v, c) => c.PromotionAdExtension.Language = v
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.PromotionCode,
                c => c.PromotionAdExtension.PromotionCode.ToOptionalBulkString(c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.PromotionCode = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.PromotionTarget,
                c => c.PromotionAdExtension.PromotionItem,
                (v, c) => c.PromotionAdExtension.PromotionItem = v
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.PromotionStart,
                c => c.PromotionAdExtension.PromotionStartDate.ToScheduleDateBulkString(c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.PromotionStartDate = v.ParseDate()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.PromotionEnd,
                c => c.PromotionAdExtension.PromotionEndDate.ToScheduleDateBulkString(c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.PromotionEndDate = v.ParseDate()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.MoneyAmountOff,
                c => c.PromotionAdExtension.MoneyAmountOff.ToBulkString(),
                (v, c) => c.PromotionAdExtension.MoneyAmountOff = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.OrdersOverAmount,
                c => c.PromotionAdExtension.OrdersOverAmount.ToBulkString(),
                (v, c) => c.PromotionAdExtension.OrdersOverAmount = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.PercentOff,
                c => c.PromotionAdExtension.PercentOff.ToBulkString(),
                (v, c) => c.PromotionAdExtension.PercentOff = v.ParseOptional<double>()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.DiscountModifier,
                c => c.PromotionAdExtension.DiscountModifier.ToBulkString(),
                (v, c) =>
                {
                    var extension = c.PromotionAdExtension as PromotionAdExtension;

                    if (extension == null) return;

                    PromotionDiscountModifier? promotionDiscountModifier = v.ParseOptional<PromotionDiscountModifier>();
                    if (promotionDiscountModifier != null)
                    {
                        c.PromotionAdExtension.DiscountModifier = (PromotionDiscountModifier)promotionDiscountModifier;
                    }
                }
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.Occasion,
                c => c.PromotionAdExtension.PromotionOccasion.ToBulkString(),
                (v, c) =>
                {
                    var extension = c.PromotionAdExtension as PromotionAdExtension;

                    if (extension == null) return;

                    PromotionOccasion? promotionOccasion = v.ParseOptional<PromotionOccasion>();
                    if (promotionOccasion != null)
                    {
                        c.PromotionAdExtension.PromotionOccasion = (PromotionOccasion)promotionOccasion;
                    }
                }
            ),


            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.FinalUrl,
                c => c.PromotionAdExtension.FinalUrls.WriteUrls("; ", c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.FinalUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.FinalMobileUrl,
                c => c.PromotionAdExtension.FinalMobileUrls.WriteUrls("; ", c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.FinalMobileUrls = v.ParseUrls()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.TrackingTemplate,
                c => c.PromotionAdExtension.TrackingUrlTemplate.ToOptionalBulkString(c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.CustomParameter,
                c => c.PromotionAdExtension.UrlCustomParameters.ToBulkString(c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkPromotionAdExtension>(StringTable.FinalUrlSuffix,
                c => c.PromotionAdExtension.FinalUrlSuffix.ToOptionalBulkString(c.PromotionAdExtension.Id),
                (v, c) => c.PromotionAdExtension.FinalUrlSuffix = v.GetValueOrEmptyString()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            PromotionAdExtension = new PromotionAdExtension
            {
                Type = "PromotionAdExtension",
            };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(PromotionAdExtension, "PromotionAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
