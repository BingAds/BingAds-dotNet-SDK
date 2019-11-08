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

using System;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a Ad Group Product Partition that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkAdGroupProductPartition.AdGroupCriterion"/> property that can be read and written as fields of the Ad Group Product Partition record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Ad Group Product Partition</see> </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupProductPartition : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines an Ad Group Criterion.
        /// </summary>
        public AdGroupCriterion AdGroupCriterion { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The AdGroup Data Object of the Campaign Management Service. A subset of AdGroup properties are available 
        /// in the Ad Group record. For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Ad Group</see>.
        /// </summary>
        public string AdGroupName { get; set; }

        private static readonly IBulkMapping<BulkAdGroupProductPartition>[] Mappings =
        {
            // NOTE: This mapping should be in front of other mapping which access AdGroupCriterion
            
            new ComplexBulkMapping<BulkAdGroupProductPartition>(BiddingToCsv, CsvToBidding),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.Status,
                c => c.AdGroupCriterion.Status.ToBulkString(),
                (v, c) => c.AdGroupCriterion.Status = v.ParseOptional<AdGroupCriterionStatus>()
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.Id,
                c => c.AdGroupCriterion.Id.ToBulkString(),
                (v, c) => c.AdGroupCriterion.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.ParentId,
                c => c.AdGroupCriterion.AdGroupId.ToBulkString(true),
                (v, c) => c.AdGroupCriterion.AdGroupId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.SubType,
                c =>
                {
                    var productPartition = c.AdGroupCriterion.Criterion as ProductPartition;

                    if (productPartition == null)
                    {
                        return null;
                    }

                    return productPartition.PartitionType.ToBulkString(returnNullForDefaultValue: true);
                },
                (v, c) =>
                {
                    ((ProductPartition)c.AdGroupCriterion.Criterion).PartitionType =
                            v.Parse<ProductPartitionType>(returnDefaultValueOnNullOrEmpty: true);
                }
                ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.ParentAdGroupCriterionId,
                c =>
                {
                    var productPartition = c.AdGroupCriterion.Criterion as ProductPartition;

                    if (productPartition == null)
                    {
                        return null;
                    }

                    return productPartition.ParentCriterionId.ToBulkString();
                },
                (v, c) => ((ProductPartition) c.AdGroupCriterion.Criterion).ParentCriterionId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.ProductCondition1,
                c =>
                {
                    var productPartition = c.AdGroupCriterion.Criterion as ProductPartition;

                    if (productPartition == null)
                    {
                        return null;
                    }

                    var condition = productPartition.Condition;

                    if (condition == null)
                    {
                        return null;
                    }

                    return condition.Operand;
                },
                (v, c) => ((ProductPartition) c.AdGroupCriterion.Criterion).Condition.Operand = v
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.ProductValue1,
                c =>
                {
                    var productPartition = c.AdGroupCriterion.Criterion as ProductPartition;

                    if (productPartition == null)
                    {
                        return null;
                    }

                    var condition = productPartition.Condition;

                    if (condition == null)
                    {
                        return null;
                    }

                    return condition.Attribute;
                },
                (v, c) => ((ProductPartition) c.AdGroupCriterion.Criterion).Condition.Attribute = v
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.DestinationUrl,
                c =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        return criterion.DestinationUrl.ToOptionalBulkString(criterion.Id);
                    }

                    return null;
                },
                (v, c) =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        criterion.DestinationUrl = v.GetValueOrEmptyString();
                    }
                }
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.FinalUrl,
                c =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        return criterion.FinalUrls.WriteUrls("; ", criterion.Id);
                    }

                    return null;
                },
                (v, c) =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        criterion.FinalUrls = v.ParseUrls();
                    }
                }
            ), 

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.FinalMobileUrl,
                c =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        return criterion.FinalMobileUrls.WriteUrls("; ", criterion.Id);
                    }

                    return null;
                },
                (v, c) =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        criterion.FinalMobileUrls = v.ParseUrls();
                    }
                }
            ), 

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.TrackingTemplate,
                c =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        return criterion.TrackingUrlTemplate.ToOptionalBulkString(criterion.Id);
                    }

                    return null;
                },
                (v, c) =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        criterion.TrackingUrlTemplate = v.GetValueOrEmptyString();
                    }
                }
            ),

            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.CustomParameter,
                c =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        return criterion.UrlCustomParameters.ToBulkString(c.AdGroupCriterion.Id);
                    }

                    return null;
                },
                (v, c) =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        criterion.UrlCustomParameters = v.ParseCustomParameters();
                    }
                }
            ),
            new SimpleBulkMapping<BulkAdGroupProductPartition>(StringTable.FinalUrlSuffix,
                c =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        return criterion.FinalUrlSuffix.ToOptionalBulkString(criterion.Id);
                    }

                    return null;
                },
                (v, c) =>
                {
                    var criterion = c.AdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion != null)
                    {
                        criterion.FinalUrlSuffix = v.GetValueOrEmptyString();
                    }
                }
            ),
        };

        private static void CsvToBidding(RowValues values, BulkAdGroupProductPartition entity)
        {
            //string exclude;
            values.TryGetValue(StringTable.IsExcluded, out string exclude);

            exclude = exclude.GetValueOrEmptyString().ToLower();
            bool isExcluded;
            switch (exclude)
            {
                case "yes":
                    isExcluded = true;
                    break;
                case "true":
                    isExcluded = true;
                    break;
                case "no":
                    isExcluded = false;
                    break;
                case "false":
                    isExcluded = false;
                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format("\"{0}\" can only be set to TRUE|FALSE in {1}",
                            StringTable.IsExcluded, typeof(BulkAdGroupProductPartition).Name));
            }

            if (isExcluded)
            {
                entity.AdGroupCriterion = new NegativeAdGroupCriterion
                {
                    Criterion = new ProductPartition
                    {
                        Condition = new ProductCondition(),
                        Type = typeof(ProductPartition).Name,
                    },
                    Type = typeof(NegativeAdGroupCriterion).Name,
                };
            }
            else
            {
                var biddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    Criterion = new ProductPartition
                    {
                        Condition = new ProductCondition(),
                        Type = typeof(ProductPartition).Name,
                    },
                    Type = typeof(BiddableAdGroupCriterion).Name,
                };

                values.TryGetValue(StringTable.Bid, out string bidStr);
                values.TryGetValue(StringTable.BidAdjustment, out string bidAdjustmentStr);
                double? bid = bidStr.ParseOptional<double>();
                double? bidAdjustment = bidAdjustmentStr.ParseOptional<double>();

                if (bid != null)
                {
                    biddableAdGroupCriterion.CriterionBid = new FixedBid
                    {
                        Amount = bid.Value,
                        Type = typeof(FixedBid).Name,
                    };
                }
                else if (bidAdjustment != null)
                {
                    biddableAdGroupCriterion.CriterionBid = new BidMultiplier
                    {
                        Multiplier = bidAdjustment.Value,
                        Type = typeof(FixedBid).Name,
                    };
                }
                else
                {
                    biddableAdGroupCriterion.CriterionBid = new FixedBid
                    {
                        Type = typeof(FixedBid).Name,
                    };
                }
                entity.AdGroupCriterion = biddableAdGroupCriterion;
            }
        }

        private static void BiddingToCsv(BulkAdGroupProductPartition entity, RowValues values)
        {
            var criterion = entity.AdGroupCriterion as BiddableAdGroupCriterion;
            values[StringTable.IsExcluded] = criterion == null ? "True" : "False";

            if (criterion == null) return;

            var fixedBid = criterion.CriterionBid as FixedBid;

            if (fixedBid != null)
            {
                values[StringTable.Bid] = fixedBid.ToAdGroupCriterionFixedBidBulkString();
                return;
            }

            var multiplicativeBid = criterion.CriterionBid as BidMultiplier;

            if (multiplicativeBid != null)
            {
                values[StringTable.BidAdjustment] = multiplicativeBid.Multiplier.ToBulkString();
            }
        }

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
