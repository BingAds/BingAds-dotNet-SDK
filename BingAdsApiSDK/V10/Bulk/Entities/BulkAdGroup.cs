//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
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

using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group. 
    /// This class exposes the <see cref="BulkAdGroup.AdGroup"/> property that can be read and written as fields of the Ad Group record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620252">Ad Group</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroup : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the campaign that contains the ad group.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The AdGroup Data Object of the Campaign Management Service. A subset of AdGroup properties are available 
        /// in the Ad Group record. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620252">Ad Group</see>.
        /// </summary>
        public AdGroup AdGroup { get; set; }

        /// <summary>
        /// Indicates whether the AdGroup is expired.
        /// </summary>
        public bool IsExpired { get; private set; }

        /// <summary>
        /// The quality score data for the ad group.
        /// </summary>
        public QualityScoreData QualityScoreData { get; private set; }

        /// <summary>
        /// The historical performance data for the ad group.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        private static readonly IBulkMapping<BulkAdGroup>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdGroup>(StringTable.Id,
                c => c.AdGroup.Id.ToBulkString(),
                (v, c) => c.AdGroup.Id = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Status,
                c => c.IsExpired ? "Expired" : c.AdGroup.Status.ToBulkString(),
                (v, c) =>
                {
                    if (v == "Expired")
                    {
                        c.AdGroup.Status = AdGroupStatus.Deleted;
                        c.IsExpired = true;
                    }
                    else
                    {
                        c.AdGroup.Status = v.ParseOptional<AdGroupStatus>();
                    }
                }
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ParentId,
                c => c.CampaignId.ToBulkString(),
                (v, c) => c.CampaignId = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.AdGroup,
                c => c.AdGroup.Name,
                (v, c) => c.AdGroup.Name = v
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.StartDate,
                c => c.AdGroup.StartDate.ToDateBulkString(),
                (v, c) => c.AdGroup.StartDate = v.ParseDate()
                ),
            new SimpleBulkMapping<BulkAdGroup>(StringTable.EndDate,
                c => c.AdGroup.EndDate.ToDateBulkString(),
                (v, c) => c.AdGroup.EndDate = v.ParseDate()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.NetworkDistribution,
                c => c.AdGroup.Network.ToBulkString(),
                (v, c) => c.AdGroup.Network = v.ParseOptional<Network>()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.PricingModel,
                c => c.AdGroup.PricingModel.ToPricingModelBulkString(),
                (v, c) => c.AdGroup.PricingModel = v.ParseOptionalPricingModel()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.AdRotation,
                c => c.AdGroup.AdRotation.ToAdRotationBulkString(),
                (v, c) => c.AdGroup.AdRotation = v.ParseAdRotation()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.SearchNetwork,
                c => c.AdGroup.AdDistribution.ToSearchAdDistributionBulkString(),
                (v, c) => c.AdGroup.AdDistribution |= v.ParseSearchAdDistribution()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ContentNetwork,
                c => c.AdGroup.AdDistribution.ToContentAdDistributionBulkString(),
                (v, c) => c.AdGroup.AdDistribution |= v.ParseContentAdDistribution()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.SearchBid,
                c => c.AdGroup.SearchBid.ToAdGroupBidBulkString(),
                (v, c) => c.AdGroup.SearchBid = v.ParseAdGroupBid()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.ContentBid,
                c => c.AdGroup.ContentMatchBid.ToAdGroupBidBulkString(),
                (v, c) => c.AdGroup.ContentMatchBid = v.ParseAdGroupBid()
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.Language,
                c => c.AdGroup.Language,
                (v, c) => c.AdGroup.Language = v
                ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.BidAdjustment,
                c => c.AdGroup.NativeBidAdjustment.ToBulkString(),
                (v, c) => c.AdGroup.NativeBidAdjustment = v.ParseOptional<int>()
                ),
                        
            new SimpleBulkMapping<BulkAdGroup>(StringTable.TrackingTemplate,
                c => c.AdGroup.TrackingUrlTemplate.ToOptionalBulkString(),
                (v, c) => c.AdGroup.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.CustomParameter,
                c => c.AdGroup.UrlCustomParameters.ToBulkString(),
                (v, c) => c.AdGroup.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkAdGroup>(StringTable.BidStrategyType,
                c => c.AdGroup.BiddingScheme.ToBiddingSchemeBulkString(),
                (v, c) => c.AdGroup.BiddingScheme = v.ParseBiddingScheme()
            ),

        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AdGroup = new AdGroup {AdDistribution = 0};

            values.ConvertToEntity(this, Mappings);

            QualityScoreData = QualityScoreData.ReadFromRowValuesOrNull(values);

            PerformanceData = PerformanceData.ReadFromRowValuesOrNull(values);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(AdGroup, "AdGroup");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                QualityScoreData.WriteToRowValuesIfNotNull(QualityScoreData, values);

                PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
            }
        }
    }
}