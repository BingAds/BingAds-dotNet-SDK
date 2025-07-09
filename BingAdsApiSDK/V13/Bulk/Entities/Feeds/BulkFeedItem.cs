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
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.CampaignManagement;
using MatchType = Microsoft.BingAds.V13.CampaignManagement.MatchType;

namespace Microsoft.BingAds.V13.Bulk.Entities.Feeds
{
    /// <summary>
    /// <para>
    /// Represents a feed item that can be read or written in a bulk file. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Feed Item record in a bulk file.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Feed Item</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkFeedItem : SingleRecordBulkEntity
    {

        /// <summary>
        /// The identifier of the feed item.
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// The identifier of the feed that contains the feed item.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? FeedId { get; set; }

        /// <summary>
        /// The name of the target campaign for the feed item.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the target ad group for the feed item.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        /// <summary>
        /// The identifier of the target audience for the feed item.
        /// Corresponds to the 'Audience Id' field in the bulk file. 
        /// </summary>
        public long? AudienceId { get; set; }

        /// <summary>
        /// The custom attributes for your feed item.
        /// Corresponds to the 'Custom Attributes' field in the bulk file. 
        /// </summary>
        public string CustomAttributes { get; set; }
        
        /// <summary>
        /// The status for the bulk feed item.
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// The feed item schedule.
        /// Corresponds to the 'Ad Schedule' field in the bulk file. 
        /// </summary>
        public System.Collections.Generic.IList<DayTime> DayTimeRanges { get; set; }

        /// <summary>
        /// The feed item end date.
        /// Corresponds to the 'End Date' field in the bulk file. 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The feed item start date.
        /// Corresponds to the 'Start Date' field in the bulk file. 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The target keyword for the feed item.
        /// Corresponds to the 'Keyword' field in the bulk file. 
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// The target keyword match type for the feed item.
        /// Corresponds to the 'Match Type' field in the bulk file. 
        /// </summary>
        public MatchType? MatchType { get; set; }

        /// <summary>
        /// The feed item's target location.
        /// Corresponds to the 'Target' field in the bulk file. 
        /// </summary>
        public long? LocationId { get; set; }

        /// <summary>
        /// The physical intent option for the feed item's target location.
        /// Corresponds to the 'Physical Intent' field in the bulk file. 
        /// </summary>
        public IntentOption? IntentOption { get; set; }

        /// <summary>
        /// The device preference for the feed item.
        /// Corresponds to the 'DevicePreference' field in the bulk file. 
        /// </summary>
        public long? DevicePreference { get; set; }

        /// <summary>
        /// The feed item's target ad group id.
        /// Corresponds to the 'Target Ad Group Id' field in the bulk file. 
        /// </summary>
        public string AdGroupId { get; set; }

        /// <summary>
        /// The feed item's target campaign id.
        /// Corresponds to the 'Target Campaign Id' field in the bulk file. 
        /// </summary>
        public string CampaignId { get; set; }


        private static readonly IBulkMapping<BulkFeedItem>[] Mappings =
        {
            new SimpleBulkMapping<BulkFeedItem>(StringTable.Id,
                c => c.Id.ToBulkString(),
                (v, c) => c.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.ParentId,
                c => c.FeedId.ToBulkString(),
                (v, c) => c.FeedId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.AudienceId,
                c => c.AudienceId.ToBulkString(),
                (v, c) => c.AudienceId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.CustomAttributes,
                c => c.CustomAttributes,
                (v, c) => c.CustomAttributes = v
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.AdSchedule,
                c => c.DayTimeRanges == null ? null : c.DayTimeRanges.ToDayTimeRangesBulkString(c.Id),
                (v, c) => c.DayTimeRanges = v.ParseDayTimeRanges()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.StartDate,
                c => c.StartDate.ToDateTimeBulkString(c.Id),
                (v, c) => c.StartDate = v.ParseDateTime()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.EndDate,
                c => c.EndDate.ToDateTimeBulkString(c.Id),
                (v, c) => c.EndDate = v.ParseDateTime()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.Keyword,
                c => c.Keyword,
                (v, c) => c.Keyword = v
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.MatchType,
                c => c.MatchType.ToBulkString(),
                (v, c) => c.MatchType = v.ParseOptional<MatchType>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.Target,
                c => c.LocationId.ToBulkString(),
                (v, c) => c.LocationId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.PhysicalIntent,
                c => c.IntentOption.ToBulkString(),
                (v, c) => c.IntentOption = v.ParseOptional<IntentOption>()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.DevicePreference,
                c => c.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.DevicePreference = v.ParseDevicePreference()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.TargetAdGroupId,
                c => c.AdGroupId.ToOptionalBulkString(c.Id),
                (v, c) => c.AdGroupId = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkFeedItem>(StringTable.TargetCampaignId,
                c => c.CampaignId.ToOptionalBulkString(c.Id),
                (v, c) => c.CampaignId = v.GetValueOrEmptyString()
            ),
        };

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
