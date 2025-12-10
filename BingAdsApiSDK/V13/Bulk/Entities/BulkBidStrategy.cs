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
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a bid strategy that can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkBidStrategy.BidStrategy"/> property that can be read and written as fields of the BidStrategy record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bid Strategy</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkBidStrategy : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the account that contains the bid strategy.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Defines a bid strategy within an account. 
        /// </summary>
        public BidStrategy BidStrategy { get; set; }

        /// <summary>
        /// The status for the bulk bid strategy.
        /// </summary>
        public Status? Status { get; set; }

        private static readonly IBulkMapping<BulkBidStrategy>[] Mappings =
        {
            new SimpleBulkMapping<BulkBidStrategy>(StringTable.Id,
                c => c.BidStrategy.Id.ToBulkString(),
                (v, c) => c.BidStrategy.Id = v.ParseOptional<long>()
                ),

            new SimpleBulkMapping<BulkBidStrategy>(StringTable.ParentId,
                c => c.AccountId.ToBulkString(),
                (v, c) => c.AccountId = v.Parse<long>()
                ),

            new SimpleBulkMapping<BulkBidStrategy>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
                ),
            
            new SimpleBulkMapping<BulkBidStrategy>(StringTable.CampaignType,
                c => c.BidStrategy.AssociatedCampaignType.ToBulkString(),
                (v, c) => c.BidStrategy.AssociatedCampaignType = v.ParseOptional<CampaignType>()
                ),

            new SimpleBulkMapping<BulkBidStrategy>(StringTable.BidStrategyName,
                c => c.BidStrategy.Name,
                (v, c) => c.BidStrategy.Name = v
                ),

            new SimpleBulkMapping<BulkBidStrategy>(StringTable.BidStrategyScope,
                c => c.BidStrategy.Scope.ToBulkString(),
                (v, c) => c.BidStrategy.Scope = v.ParseOptional<EntityScope>()
            ),

            new SimpleBulkMapping<BulkBidStrategy>(StringTable.CurrencyCode,
                c => c.BidStrategy.CurrencyCode,
                (v, c) => c.BidStrategy.CurrencyCode = v
                ),

            new SimpleBulkMapping<BulkBidStrategy>(StringTable.TimeZone,
                c => c.BidStrategy.ReportingTimeZone,
                (v, c) => c.BidStrategy.ReportingTimeZone = v
                ),

            new ComplexBulkMapping<BulkBidStrategy>(BiddingSchemeToCsv, CsvToBiddingScheme)
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            BidStrategy = new BidStrategy();
            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(BidStrategy, "BidStrategy");

            this.ConvertToValues(values, Mappings);
        }

        
        private static void CsvToBiddingScheme(RowValues values, BulkBidStrategy c)
        {
            c.BidStrategy.BiddingScheme = values.ReadBiddingSchemaFromValues();
        }

        private static void BiddingSchemeToCsv(BulkBidStrategy c, RowValues values)
        {
            if (c.BidStrategy.BiddingScheme == null)
            {
                return;
            }

            c.BidStrategy.BiddingScheme.WriteToValues(values, c.BidStrategy.Id);
        }
    }
}
