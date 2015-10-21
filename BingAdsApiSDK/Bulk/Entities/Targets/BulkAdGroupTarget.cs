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

using System;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a target that is associated with an ad group. The target contains one or more sub targets, including  
    /// age, gender, day and time, device OS, and location. Each target can be read or written in a bulk file. 
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// When requesting downloaded entities of type <see cref="BulkAdGroupTarget"/>, the results will include 
    /// Ad Group Age Target, Ad Group DayTime Target, Ad Group DeviceOS Target, Ad Group Gender Target, Ad Group Location Target, 
    /// Ad Group Negative Location Target, and Ad Group Radius Target records. 
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>. 
    /// </para>
    /// <para>
    /// For upload you must set the <see cref="Target2"/> object, which will effectively replace any existing bids for the corresponding target.
    /// </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupTarget : BulkTarget<BulkAdGroupTargetIdentifier,
        BulkAdGroupAgeTargetBid, BulkAdGroupAgeTarget,
        BulkAdGroupGenderTargetBid, BulkAdGroupGenderTarget,
        BulkAdGroupDayTimeTargetBid, BulkAdGroupDayTimeTarget,
        BulkAdGroupLocationTargetBid, BulkAdGroupLocationTarget,
        BulkAdGroupNegativeLocationTargetBid, BulkAdGroupNegativeLocationTarget,
        BulkAdGroupRadiusTargetBid, BulkAdGroupRadiusTarget,
        BulkAdGroupDeviceOsTargetBid, BulkAdGroupDeviceOsTarget>
    {
        /// <summary>
        /// The identifier of the ad group that the target is associated.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// The name of the ad group that the target is associated.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// The name of the ad group that target is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return ParentEntityName; }
            set { ParentEntityName = value; }
        }

        /// <summary>
        /// Initializes a new instanced of the BulkAdGroupTarget class with the specified bulk target bid. 
        /// </summary>
        internal BulkAdGroupTarget(BulkTargetBid bid)
            : base(bid,
            new BulkAdGroupLocationTarget(),
            new BulkAdGroupAgeTarget(),
            new BulkAdGroupGenderTarget(),
            new BulkAdGroupDayTimeTarget(),
            new BulkAdGroupDeviceOsTarget(),
            new BulkAdGroupNegativeLocationTarget(),
            new BulkAdGroupRadiusTarget())
        {

        }

        internal BulkAdGroupTarget(BulkAdGroupTargetIdentifier identifier)
            : base(identifier,
            new BulkAdGroupLocationTarget(),
            new BulkAdGroupAgeTarget(),
            new BulkAdGroupGenderTarget(),
            new BulkAdGroupDayTimeTarget(),
            new BulkAdGroupDeviceOsTarget(),
            new BulkAdGroupNegativeLocationTarget(),
            new BulkAdGroupRadiusTarget())
        {

        }

        /// <summary>
        /// Initializes a new instanced of the BulkAdGroupTarget class. 
        /// </summary>
        public BulkAdGroupTarget()
            : base(new BulkAdGroupLocationTarget(),
            new BulkAdGroupAgeTarget(),
            new BulkAdGroupGenderTarget(),
            new BulkAdGroupDayTimeTarget(),
            new BulkAdGroupDeviceOsTarget(),
            new BulkAdGroupNegativeLocationTarget(),
            new BulkAdGroupRadiusTarget())
        {

        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkAdGroupTargetIdentifier CreateIdentifier(Type bidType)
        {
            return new BulkAdGroupTargetIdentifier(bidType);
        }
    }
}
