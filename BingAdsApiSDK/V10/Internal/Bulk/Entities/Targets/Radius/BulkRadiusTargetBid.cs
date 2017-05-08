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

using System.Globalization;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk radius target bid classes, for example <see cref="BulkAdGroupRadiusTargetBid"/>.
    /// </summary>
    public abstract class BulkRadiusTargetBid : BulkTargetBid
    {
        /// <summary>
        /// Defines a specific geographical radius to target.
        /// </summary>
        public RadiusTargetBid RadiusTargetBid { get; set; }

        /// <summary>
        /// Defines the possible intent options for location targeting.
        /// </summary>
        public IntentOption? IntentOption { get; internal set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkRadiusTargetBid(BulkTargetIdentifier identifier)
            : base(identifier)
        {
            
        }

        private static readonly IBulkMapping<BulkRadiusTargetBid>[] Mappings =
        {
            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.RadiusTargetId,
                c => c.RadiusTargetBid.Id.ToBulkString(),
                (v, c) => c.RadiusTargetBid.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Name,
                c => c.RadiusTargetBid.Name,
                (v, c) => c.RadiusTargetBid.Name = v
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Radius,
                c => c.RadiusTargetBid.Radius.ToBulkString(),
                (v, c) => c.RadiusTargetBid.Radius = v.Parse<double>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Unit,
                c => c.RadiusTargetBid.RadiusUnit.ToBulkString(),
                (v, c) => c.RadiusTargetBid.RadiusUnit = v.Parse<DistanceUnit>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Latitude,
                c => c.RadiusTargetBid.LatitudeDegrees.ToBulkString(),
                (v, c) => c.RadiusTargetBid.LatitudeDegrees = v.Parse<double>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.Longitude,
                c => c.RadiusTargetBid.LongitudeDegrees.ToBulkString(),
                (v, c) => c.RadiusTargetBid.LongitudeDegrees = v.Parse<double>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.BidAdjustment,
                c => c.RadiusTargetBid.BidAdjustment.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.RadiusTargetBid.BidAdjustment = v.Parse<int>()
            ),

            new SimpleBulkMapping<BulkRadiusTargetBid>(StringTable.PhysicalIntent,
                c => c.IntentOption.ToBulkString(),
                (v, c) => c.IntentOption = v.ParseOptional<IntentOption>()
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            RadiusTargetBid = new RadiusTargetBid();

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(RadiusTargetBid, "RadiusTargetBid");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
