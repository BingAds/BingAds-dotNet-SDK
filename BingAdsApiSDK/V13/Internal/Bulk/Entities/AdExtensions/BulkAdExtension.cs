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

using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
{
    /// <summary>
    /// <para>This abstract class provides properties that are shared by all bulk ad extension classes.</para>
    /// </summary>
    /// <typeparam name="T">The type of ad extension from the <see cref="Microsoft.BingAds.V13.CampaignManagement"/> namespace, for example a <see cref="CallAdExtension"/> object.</typeparam>
    /// <seealso cref="BulkCallAdExtension"/>
    /// <seealso cref="BulkImageAdExtension"/>
    /// <seealso cref="BulkLocationAdExtension"/>
    /// <seealso cref="BulkSitelinkAdExtension"/>
    public abstract class BulkAdExtensionBase<T> : SingleRecordBulkEntity
        where T: AdExtension
    {
        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }                

        /// <summary>
        /// The type of ad extension from the <see cref="Microsoft.BingAds.V13.CampaignManagement"/> namespace, for example a <see cref="CallAdExtension"/> object.
        /// </summary>
        protected T AdExtension { get; set; }

        private static readonly IEnumerable<IBulkMapping<BulkAdExtensionBase<T>>> Mappings = new IBulkMapping<BulkAdExtensionBase<T>>[]
        {
            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.Status,
                c => c.AdExtension.Status.ToBulkString(),
                (v, c) => c.AdExtension.Status = v.ParseOptional<AdExtensionStatus>()
            ), 

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.Id,
                c => c.AdExtension.Id.ToBulkString(),
                (v, c) => c.AdExtension.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.ParentId,
                c => c.AccountId.ToBulkString(),
                (v, c) => c.AccountId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.Version,
                c => c.AdExtension.Version.ToBulkString(),
                (v, c) => c.AdExtension.Version = v.ParseOptional<int>()
            ), 

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.AdSchedule,
                c => c.AdExtension.Scheduling == null ? null : c.AdExtension.Scheduling.DayTimeRanges.ToDayTimeRangesBulkString(c.AdExtension.Id),
                (v, c) =>
                {
                    if (c.AdExtension.Scheduling == null)
                    {
                        c.AdExtension.Scheduling = new Schedule();
                    }
                    c.AdExtension.Scheduling.DayTimeRanges = v.ParseDayTimeRanges();
                }
            ),
            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.StartDate,
                c => c.AdExtension.Scheduling == null ? null : c.AdExtension.Scheduling.StartDate.ToScheduleDateBulkString(c.AdExtension.Id),
                (v, c) =>
                {
                    if (c.AdExtension.Scheduling == null)
                    {
                        c.AdExtension.Scheduling = new Schedule();
                    }
                    c.AdExtension.Scheduling.StartDate = v.ParseDate();
                }
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.EndDate,
                c => c.AdExtension.Scheduling == null ? null : c.AdExtension.Scheduling.EndDate.ToScheduleDateBulkString(c.AdExtension.Id),
                (v, c) =>
                {
                    if (c.AdExtension.Scheduling == null)
                    {
                        c.AdExtension.Scheduling = new Schedule();
                    }
                    c.AdExtension.Scheduling.EndDate = v.ParseDate();
                }
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.UseSearcherTimeZone,
                c =>c.AdExtension.Scheduling == null ? null : c.AdExtension.Scheduling.UseSearcherTimeZone.ToUseSearcherTimeZoneBulkString(c.AdExtension.Id),
                (v, c) =>
                {
                    if (c.AdExtension.Scheduling == null)
                    {
                        c.AdExtension.Scheduling = new Schedule();
                    }
                    c.AdExtension.Scheduling.UseSearcherTimeZone = v.ParseOptional<bool>();
                }
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.DevicePreference,
                c => c.AdExtension.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.AdExtension.DevicePreference = v.ParseDevicePreference()
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
