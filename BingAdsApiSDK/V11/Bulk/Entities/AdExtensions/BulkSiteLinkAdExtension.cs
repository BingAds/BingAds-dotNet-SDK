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
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V11.Internal;
using Microsoft.BingAds.V11.Internal.Bulk;
using Microsoft.BingAds.V11.Internal.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V11.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a sitelink ad extension. 
    /// This class exposes the <see cref="BulkSiteLinkAdExtension.SiteLinksAdExtension"/> property that can be read and written 
    /// as fields of the Sitelink Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Sitelink Ad Extension</see>. </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Sitelink Ad Extension record includes the distinct properties of the <see cref="BulkSiteLink"/> class, combined with 
    /// the commmon properties of the <see cref="BulkSiteLinkAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="CampaignManagement.SiteLinksAdExtension"/>.
    /// </para>
    /// <para>
    /// One <see cref="BulkSiteLinkAdExtension"/> has one or more <see cref="BulkSiteLink"/>. Each <see cref="BulkSiteLink"/> instance 
    /// corresponds to one Sitelink Ad Extension record in the bulk file. If you upload a <see cref="BulkSiteLinkAdExtension"/>, 
    /// then you are effectively replacing any existing site links for the sitelink ad extension. 
    /// </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkSiteLinkAdExtension : MultiRecordBulkEntity
    {
        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Defines an ad extension that specifies one or more sitelinks to add to a text add.
        /// </summary>
        public SiteLinksAdExtension SiteLinksAdExtension { get; set; }

        private readonly List<BulkSiteLink> _bulkSiteLinkResults = new List<BulkSiteLink>();

        /// <summary>
        /// The list of <see cref="BulkSiteLink"/> are represented by multiple Sitelink Ad Extension records in the file.
        /// Each item in the list corresponds to a separate Sitelink Ad Extension record that includes the distinct properties of the <see cref="BulkSiteLink"/> class, combined with 
        /// the commmon properties of the <see cref="BulkSiteLinkAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="SiteLinksAdExtension"/>.
        /// </summary>
        public IReadOnlyList<BulkSiteLink> SiteLinks
        {
            get { return _bulkSiteLinkResults; }
        }

        internal override IReadOnlyList<BulkEntity> ChildEntities
        {
            get { return SiteLinks; }
        }

        private readonly SiteLinkAdExtensionIdentifier _identifier;

        private bool _hasDeleteAllRow;

        /// <summary>
        /// Initializes a new instance of the BulkSiteLinkAdExtension class. 
        /// </summary>
        public BulkSiteLinkAdExtension()
        {

        }

        internal BulkSiteLinkAdExtension(SiteLinkAdExtensionIdentifier identifier)
            : this()
        {
            _identifier = identifier;

            _hasDeleteAllRow = identifier.Status == AdExtensionStatus.Deleted;

            SiteLinksAdExtension = new SiteLinksAdExtension { Type = "SiteLinksAdExtension" };

            SiteLinksAdExtension.Id = identifier.AdExtensionId;

            SiteLinksAdExtension.Status = identifier.Status;

            SiteLinksAdExtension.Version = identifier.Version;

            AccountId = identifier.AccountId;
        }

        internal BulkSiteLinkAdExtension(BulkSiteLink firstSiteLink)
            : this(firstSiteLink.Identifier)
        {
            _bulkSiteLinkResults.Add(firstSiteLink);
        }

        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(SiteLinksAdExtension, "SiteLinksAdExtension");

            if (SiteLinksAdExtension.Status != AdExtensionStatus.Deleted)
            {
                ValidateListNotNullOrEmpty(SiteLinksAdExtension.SiteLinks, "SiteLinksAdExtension.SiteLinks");
            }

            if (SiteLinksAdExtension.Id == null)
            {
                throw new InvalidOperationException(ErrorMessages.SiteLinkAdExtensionIdMustBeSet);
            }

            rowWriter.WriteObjectRow(new SiteLinkAdExtensionIdentifier
            {
                Status = AdExtensionStatus.Deleted,
                AccountId = AccountId,
                AdExtensionId = SiteLinksAdExtension.Id
            }, excludeReadonlyData);

            if (SiteLinksAdExtension.Status == AdExtensionStatus.Deleted)
            {
                return;
            }

            foreach (var bulkSiteLink in ConvertRawToBulkSiteLinks())
            {
                bulkSiteLink.WriteToStream(rowWriter, excludeReadonlyData);
            }
        }

        internal override void ReadRelatedDataFromStream(IBulkStreamReader reader)
        {
            var hasMoreRows = true;

            while (hasMoreRows)
            {
                BulkSiteLink nextSiteLink;

                SiteLinkAdExtensionIdentifier identitifier;

                if (reader.TryRead(x => x.Identifier.Equals(_identifier), out nextSiteLink))
                {
                    _bulkSiteLinkResults.Add(nextSiteLink);
                }
                else if (reader.TryRead(x => x.Equals(_identifier) && x.IsDeleteRow, out identitifier))
                {
                    _hasDeleteAllRow = true;
                }
                else
                {
                    hasMoreRows = false;
                }
            }

            if (_bulkSiteLinkResults.Count > 0)
            {
                SiteLinksAdExtension.SiteLinks = _bulkSiteLinkResults.OrderBy(s => s.Order).Select(s => s.SiteLink).ToList();
                SiteLinksAdExtension.Status = AdExtensionStatus.Active;
            }
            else
            {
                SiteLinksAdExtension.Status = AdExtensionStatus.Deleted;
            }
        }

        private IEnumerable<BulkSiteLink> ConvertRawToBulkSiteLinks()
        {
            return SiteLinksAdExtension.SiteLinks.Select((s, i) => new BulkSiteLink
            {
                SiteLink = s,
                AccountId = AccountId,
                AdExtensionId = SiteLinksAdExtension.Id,
                Order = i + 1,
                Version = SiteLinksAdExtension.Version
            });
        }

        internal override bool AllChildrenArePresent
        {
            get { return _hasDeleteAllRow; }
        }
    }
}
