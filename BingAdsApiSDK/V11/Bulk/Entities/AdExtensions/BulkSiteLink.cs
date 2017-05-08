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
using Microsoft.BingAds.V11.Internal.Bulk;
using Microsoft.BingAds.V11.Internal.Bulk.Mappings;
using Microsoft.BingAds.V11.Internal.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V11.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a sitelink. 
    /// This class exposes the <see cref="BulkSiteLink.SiteLink"/> property that can be read and written 
    /// as fields of the Sitelink Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Sitelink Ad Extension</see>. </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Sitelink Ad Extension record includes the distinct properties of the <see cref="BulkSiteLink"/> class, combined with 
    /// the commmon properties of the <see cref="BulkSiteLinkAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="SiteLinksAdExtension"/>.
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
    public class BulkSiteLink : SingleRecordBulkEntity
    {        
        internal SiteLinkAdExtensionIdentifier Identifier { get; private set; }

        /// <summary>
        /// The identifier of the ad extension. 
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? AdExtensionId
        {
            get { return Identifier.AdExtensionId; }
            set { Identifier.AdExtensionId = value; }
        }

        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId
        {
            get { return Identifier.AccountId; }
            set { Identifier.AccountId = value; }
        }

        /// <summary>
        /// The status of the ad extension. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public AdExtensionStatus? Status
        {
            get { return Identifier.Status; }
            set { Identifier.Status = value; }
        }

        /// <summary>
        /// The version of the ad extension. 
        /// Corresponds to the 'Version' field in the bulk file. 
        /// </summary>
        public int? Version
        {
            get { return Identifier.Version; }
            set { Identifier.Version = value; }
        }

        /// <summary>
        /// The order of the sitelink displayed to a search user in the ad. 
        /// Corresponds to the 'Sitelink Extension Order' field in the bulk file. 
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The sitelink.
        /// </summary>
        public SiteLink SiteLink { get; set; }

        /// <summary>
        /// Initializes a new instance of the BulkSiteLink class. 
        /// </summary>
        public BulkSiteLink()
        {            
            Identifier = new SiteLinkAdExtensionIdentifier();
        }

        private static readonly IBulkMapping<BulkSiteLink>[] Mappings =
        {
            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkExtensionOrder,
                c => c.Order.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.Order = int.Parse(v)
            ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDisplayText,
                c => c.SiteLink.DisplayText,
                (v, c) => c.SiteLink.DisplayText = v
            ),
 
            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDestinationUrl,
                c => c.SiteLink.DestinationUrl.ToOptionalBulkString(),
                (v, c) => c.SiteLink.DestinationUrl = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDescription1,
                c => c.SiteLink.Description1,
                (v, c) => c.SiteLink.Description1 = v
            ), 

            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDescription2,
                c => c.SiteLink.Description2,
                (v, c) => c.SiteLink.Description2 = v
            ), 

            // TODO: figure out the exact implementation for SiteLink DevicePreference. Seems different from TextAd DevicePreference
            new SimpleBulkMapping<BulkSiteLink>(StringTable.DevicePreference,
                c => c.SiteLink.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.SiteLink.DevicePreference = v.ParseDevicePreference()
            ), 
            
            new SimpleBulkMapping<BulkSiteLink>(StringTable.FinalUrl,
                c => c.SiteLink.FinalUrls.WriteUrls("; "),
                (v, c) => c.SiteLink.FinalUrls = v.ParseUrls()
            ), 

            new SimpleBulkMapping<BulkSiteLink>(StringTable.FinalMobileUrl,
                c => c.SiteLink.FinalMobileUrls.WriteUrls("; "),
                (v, c) => c.SiteLink.FinalMobileUrls = v.ParseUrls()
            ), 

            new SimpleBulkMapping<BulkSiteLink>(StringTable.TrackingTemplate,
                c => c.SiteLink.TrackingUrlTemplate.ToOptionalBulkString(),
                (v, c) => c.SiteLink.TrackingUrlTemplate = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.CustomParameter,
                c => c.SiteLink.UrlCustomParameters.ToBulkString(),
                (v, c) => c.SiteLink.UrlCustomParameters = v.ParseCustomParameters()
            ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.AdSchedule,
                c => c.SiteLink.Scheduling == null ? null : c.SiteLink.Scheduling.DayTimeRanges.ToDayTimeRangesBulkString(),
                (v, c) =>
                {
                    if (c.SiteLink.Scheduling == null)
                    {
                        c.SiteLink.Scheduling = new Schedule();
                    }
                    c.SiteLink.Scheduling.DayTimeRanges = v.ParseDayTimeRanges();
                }
                ),
            new SimpleBulkMapping<BulkSiteLink>(StringTable.StartDate,
                c => c.SiteLink.Scheduling == null ? null : c.SiteLink.Scheduling.StartDate.ToScheduleDateBulkString(),
                (v, c) =>
                {
                    if (c.SiteLink.Scheduling == null)
                    {
                        c.SiteLink.Scheduling = new Schedule();
                    }
                    c.SiteLink.Scheduling.StartDate = v.ParseDate();
                }
                ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.EndDate,
                c => c.SiteLink.Scheduling == null ? null : c.SiteLink.Scheduling.EndDate.ToScheduleDateBulkString(),
                (v, c) =>
                {
                    if (c.SiteLink.Scheduling == null)
                    {
                        c.SiteLink.Scheduling = new Schedule();
                    }
                    c.SiteLink.Scheduling.EndDate = v.ParseDate();
                }
                ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.UseSearcherTimeZone,
                c =>c.SiteLink.Scheduling == null ? null : c.SiteLink.Scheduling.UseSearcherTimeZone.ToUseSearcherTimeZoneBulkString(),
                (v, c) =>
                {
                    if (c.SiteLink.Scheduling == null)
                    {
                        c.SiteLink.Scheduling = new Schedule();
                    }
                    c.SiteLink.Scheduling.UseSearcherTimeZone = v.ParseUseSearcherTimeZone();
                }
                ),
        };
        
        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            SiteLink = new SiteLink();

            Identifier.ReadFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(SiteLink, "SiteLink");
            
            Identifier.WriteToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }

        internal override bool CanEncloseInMultilineEntity
        {
            get { return true; }
        }

        internal override MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            return new BulkSiteLinkAdExtension(this);
        }
    }
}
