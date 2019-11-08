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

using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one or more negative sites that are assigned to an ad group. Each negative site can be read or written in a bulk file. 
    /// This class exposes properties that can be read and written as fields of the Ad Group Negative Site record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Ad Group Negative Site</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupNegativeSites"/> has one or more <see cref="BulkAdGroupNegativeSite"/>. Each <see cref="BulkAdGroupNegativeSite"/> instance 
    /// corresponds to one Ad Group Negative Site record in the bulk file. If you upload a <see cref="BulkAdGroupNegativeSites"/>, 
    /// then you are effectively replacing any existing negative sites assigned to the ad group. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupNegativeSites : BulkNegativeSites<BulkAdGroupNegativeSite, BulkAdGroupNegativeSitesIdentifier>
    {
        /// <summary>
        /// The AdGroupNegativeSites Data Object of the Campaign Management Service. A subset of AdGroupNegativeSites properties are available 
        /// in the Ad Group Negative Site record. For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Ad Group Negative Site</see>.
        /// </summary>
        public AdGroupNegativeSites AdGroupNegativeSites { get; set; }

        /// <summary>
        /// The name of the ad group that the negative site is assigned.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        /// <summary>
        /// The name of the campaign that the negative site is assigned.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Initializes a new instance of the BulkAdGroupNegativeSites class.
        /// </summary>
        public BulkAdGroupNegativeSites()
        {
        }

        internal BulkAdGroupNegativeSites(BulkAdGroupNegativeSite site)
            : base(site)
        {
            SetDataFromIdentifier(site.Identifier);
        }

        internal BulkAdGroupNegativeSites(BulkAdGroupNegativeSitesIdentifier identifier)
            : base(identifier)
        {
            SetDataFromIdentifier(identifier);
        }

        private void SetDataFromIdentifier(BulkAdGroupNegativeSitesIdentifier identifier)
        {
            AdGroupNegativeSites = new AdGroupNegativeSites
            {
                AdGroupId = identifier.AdGroupId
            };

            AdGroupName = identifier.AdGroupName;
            CampaignName = identifier.CampaignName;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IEnumerable<BulkAdGroupNegativeSite> ConvertApiToBulkNegativeSites()
        {
            ValidateListNotNullOrEmpty(AdGroupNegativeSites.NegativeSites, "AdGroupNegativeSites.NegativeSites");

            return AdGroupNegativeSites.NegativeSites.Select(s => new BulkAdGroupNegativeSite
            {
                AdGroupId = AdGroupNegativeSites.AdGroupId,
                Website = s,
                AdGroupName = AdGroupName,
                CampaignName = CampaignName
            });
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructApiObjects()
        {
            AdGroupNegativeSites.NegativeSites = NegativeSites.Select(s => s.Website).ToList();
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override BulkAdGroupNegativeSitesIdentifier CreateIdentifier()
        {
            return new BulkAdGroupNegativeSitesIdentifier
            {
                AdGroupId = AdGroupNegativeSites.AdGroupId,
                AdGroupName = AdGroupName,
                CampaignName = CampaignName
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(AdGroupNegativeSites, "AdGroupNegativeSites");
        }
    }
}
