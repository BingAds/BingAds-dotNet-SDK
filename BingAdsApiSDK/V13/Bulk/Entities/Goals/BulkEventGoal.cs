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
using Microsoft.BingAds.V13.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a event goal that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkEventGoal.EventGoal"/> property that can be read and written as fields of the event goal record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">event goal</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkEventGoal : BulkConversionGoal<EventGoal>
    {
        /// <summary>
        /// The event goal.
        /// </summary>
        public EventGoal EventGoal { get { return ConversionGoal; } set { ConversionGoal = value; } }

        private static readonly IBulkMapping<BulkEventGoal>[] Mappings =
        {
            new SimpleBulkMapping<BulkEventGoal>(StringTable.CategoryExpression,
                c => c.ConversionGoal.CategoryExpression,
                (v, c) => c.ConversionGoal.CategoryExpression = v
            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.CategoryOperator,
                c => c.ConversionGoal.CategoryOperator.ToBulkString(),
                (v, c) => 
                {
                    switch (v)
                    {
                        case "EqualsTo":
                            c.ConversionGoal.CategoryOperator = ExpressionOperator.Equals;
                            break;
                        case "NoExpression":
                            c.ConversionGoal.CategoryOperator = null;
                            break;
                        default:
                            c.ConversionGoal.CategoryOperator = v.ParseOptional<ExpressionOperator>();
                            break;
                    }
                }
            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.ActionExpression,
                c => c.ConversionGoal.ActionExpression,
                (v, c) => c.ConversionGoal.ActionExpression = v
            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.ActionOperator,
                c => c.ConversionGoal.ActionOperator.ToBulkString(),
                (v, c) =>
                {
                    switch (v)
                    {
                        case "EqualsTo":
                            c.ConversionGoal.ActionOperator = ExpressionOperator.Equals;
                            break;
                        case "NoExpression":
                            c.ConversionGoal.ActionOperator = null;
                            break;
                        default:
                            c.ConversionGoal.ActionOperator = v.ParseOptional<ExpressionOperator>();
                            break;
                    }
                }

            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.LabelExpression,
                c => c.ConversionGoal.LabelExpression,
                (v, c) => c.ConversionGoal.LabelExpression = v
            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.LabelOperator,
                c => c.ConversionGoal.LabelOperator.ToBulkString(),
                (v, c) =>
                {
                    switch (v)
                    {
                        case "EqualsTo":
                            c.ConversionGoal.LabelOperator = ExpressionOperator.Equals;
                            break;
                        case "NoExpression":
                            c.ConversionGoal.LabelOperator = null;
                            break;
                        default:
                            c.ConversionGoal.LabelOperator = v.ParseOptional<ExpressionOperator>();
                            break;
                    }
                }
            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.EventValue,
                c => c.ConversionGoal.Value.ToBulkString(),
                (v, c) => c.ConversionGoal.Value = v.ParseOptional<decimal>()
            ),

            new SimpleBulkMapping<BulkEventGoal>(StringTable.EventValueOperator,
                c => c.ConversionGoal.ValueOperator.ToBulkString(),
                (v, c) => 
                {
                    switch (v)
                    {
                        case "EqualTo":
                            c.ConversionGoal.ValueOperator = ValueOperator.Equals;
                            break;
                        case "NoValue":
                            c.ConversionGoal.ValueOperator = null;
                            break;
                        default:
                            c.ConversionGoal.ValueOperator = v.ParseOptional<ValueOperator>();
                            break;
                    }
                }
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            base.ProcessMappingsFromRowValues(values);
            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            base.ProcessMappingsToRowValues(values, excludeReadonlyData);
            this.ConvertToValues(values, Mappings);
        }
    }
}
