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
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// A base class for all bulk negative location target classes, for example <see cref="BulkAdGroupNegativeLocationTarget"/>.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkNegativeLocationTargetBid"/></typeparam>
    public abstract class BulkNegativeLocationTarget<TBid> : BulkLocationTargetWithStringLocation<TBid>
        where TBid : BulkNegativeLocationTargetBid
    {
        // Should only convert bid if it's excluded

        internal override bool ShouldConvertCityTargetBid(CityTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertMetroAreaTargetBid(MetroAreaTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertStateTargetBid(StateTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertCountryTargetBid(CountryTargetBid bid)
        {
            return bid.IsExcluded;
        }

        internal override bool ShouldConvertPostalCodeTargetBid(PostalCodeTargetBid bid)
        {
            return bid.IsExcluded;
        }

        // Set IsExcluded to true when converting to API bids

        internal override CityTargetBid SetCityBidAdditionalProperties(CityTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override MetroAreaTargetBid SetMetroAreaBidAdditionalProperties(MetroAreaTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override StateTargetBid SetStateBidAdditionalProperties(StateTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override CountryTargetBid SetCountryBidAdditionalProperties(CountryTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        internal override PostalCodeTargetBid SetPostalCodeBidAdditionalProperties(PostalCodeTargetBid apiBid, TBid bulkBid)
        {
            apiBid.IsExcluded = true;

            return apiBid;
        }

        // No additional properties need to be set for BulkNegativeLocationTargetBids (Location and LocationType are set by the base class)

        internal override void SetBulkCityBidAdditionalProperties(TBid bulkBid, CityTargetBid apiBid)
        {
            
        }

        internal override void SetBulkMetroAreaBidAdditionalProperties(TBid bulkBid, MetroAreaTargetBid apiBid)
        {
            
        }

        internal override void SetBulkStateBidAdditionalProperties(TBid bulkBid, StateTargetBid apiBid)
        {
            
        }

        internal override void SetBulkCountryBidAdditionalProperties(TBid bulkBid, CountryTargetBid apiBid)
        {
            
        }

        internal override void SetBulkPostalCodeBidAdditionalProperties(TBid bulkBid, PostalCodeTargetBid apiBid)
        {
            
        }
    }
}
