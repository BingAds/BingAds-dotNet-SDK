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
    /// Represents a base conversion goal that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkConversionGoal.ConversionGoal"/> property that can be read and written as fields of the ConversionGoal record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">ConversionGoal</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public abstract class BulkConversionGoal<T> : SingleRecordBulkEntity where T : ConversionGoal, new()
    {
        /// <summary>
        /// The conversion goal.
        /// </summary>
        protected T ConversionGoal { get; set; }

        private static readonly IBulkMapping<BulkConversionGoal<T>>[] Mappings =
        {
            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.Status,
                c => c.ConversionGoal.Status.ToBulkString(),
                (v, c) => c.ConversionGoal.Status = v.ParseOptional<ConversionGoalStatus>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.Id,
                c => c.ConversionGoal.Id.ToBulkString(),
                (v, c) => c.ConversionGoal.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.Name,
                c => c.ConversionGoal.Name,
                (v, c) => c.ConversionGoal.Name = v
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.AttributionModelType,
                c => c.ConversionGoal.AttributionModelType.ToBulkString(),
                (v, c) => 
                {
                    switch (v)
                    {
                        case "Default":
                            c.ConversionGoal.AttributionModelType = AttributionModelType.LastClick;
                            break;
                        case "External":
                            c.ConversionGoal.AttributionModelType = null;
                            break;
                        default:
                            c.ConversionGoal.AttributionModelType = v.ParseOptional<AttributionModelType>();
                            break;
                    }
                }
            ),


            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.ConversionWindowInMinutes,
                c => c.ConversionGoal.ConversionWindowInMinutes.ToBulkString(),
                (v, c) => c.ConversionGoal.ConversionWindowInMinutes = v.ParseOptional<int>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.CountType,
                c => c.ConversionGoal.CountType.ToBulkString(),
                (v, c) => c.ConversionGoal.CountType = v.ParseOptional<ConversionGoalCountType>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.ExcludeFromBidding,
                c => c.ConversionGoal.ExcludeFromBidding.ToString(),
                (v, c) => c.ConversionGoal.ExcludeFromBidding = v.ParseOptional<bool>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.GoalCategory,
                c => c.ConversionGoal.GoalCategory.ToBulkString(),
                (v, c) =>
                {
                    c.ConversionGoal.GoalCategory = v switch
                    {
                        // To handle the old typo. 
                        "Subcribe" => (ConversionGoalCategory?)ConversionGoalCategory.Subscribe,
                        "InStoreVisit" => (ConversionGoalCategory?)ConversionGoalCategory.Other,
                        _ => v.ParseOptional<ConversionGoalCategory>(),
                    };
                 }
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.IsEnhancedConversionsEnabled,
                c => c.ConversionGoal.IsEnhancedConversionsEnabled.ToString(),
                (v, c) => c.ConversionGoal.IsEnhancedConversionsEnabled = v.ParseOptional<bool>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.CurrencyCode,
                c =>
                {
                    if (c.ConversionGoal.Revenue != null)
                    {
                        return c.ConversionGoal.Revenue.CurrencyCode;
                    }
                    return null;
                },
                (v, c) =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        if (c.ConversionGoal.Revenue == null)
                        {
                            c.ConversionGoal.Revenue = new ConversionGoalRevenue();
                        }
                        c.ConversionGoal.Revenue.CurrencyCode = v;
                    }
                }
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.RevenueValue,
                c =>
                {
                    if (c.ConversionGoal.Revenue != null)
                    {
                        return c.ConversionGoal.Revenue.Value.ToBulkString();
                    }
                    return null;
                },
                (v, c) =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        if (c.ConversionGoal.Revenue == null)
                        {
                            c.ConversionGoal.Revenue = new ConversionGoalRevenue();
                        }
                        c.ConversionGoal.Revenue.Value = v.ParseOptional<decimal>();
                    }
                }
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.RevenueType,
                c =>
                {
                    if (c.ConversionGoal.Revenue != null)
                    {
                        return c.ConversionGoal.Revenue.Type.ToBulkString();
                    }
                    return null;
                },
                (v, c) =>
                {
                    if (!string.IsNullOrEmpty(v))
                    {
                        if (c.ConversionGoal.Revenue == null)
                        {
                            c.ConversionGoal.Revenue = new ConversionGoalRevenue();
                        }

                        if (v == "VariantValue")
                        {
                            c.ConversionGoal.Revenue.Type = ConversionGoalRevenueType.VariableValue;
                        }
                        else
                        {
                            c.ConversionGoal.Revenue.Type = v.ParseOptional<ConversionGoalRevenueType>();
                        }
                    }
                }
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.Scope,
                c => c.ConversionGoal.Scope.ToBulkString(),
                (v, c) => c.ConversionGoal.Scope = v.ParseOptional<EntityScope>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.TagId,
                c => c.ConversionGoal.TagId.ToBulkString(),
                (v, c) => c.ConversionGoal.TagId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkConversionGoal<T>>(StringTable.ViewThroughConversionWindowInMinutes,
                c => c.ConversionGoal.ViewThroughConversionWindowInMinutes.ToBulkString(),
                (v, c) => c.ConversionGoal.ViewThroughConversionWindowInMinutes = v.ParseOptional<int>()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ConversionGoal = new T();
            ConversionGoal.Revenue = new ConversionGoalRevenue();
            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ConversionGoal, typeof(T).Name);

            this.ConvertToValues(values, Mappings);
        }
    }
}
