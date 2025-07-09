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
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a video that can be read or written in a bulk file. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Video record in a bulk file.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Image</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkVideo : SingleRecordBulkEntity
    {
        /// <summary>
        /// The identifier of the account that contains the video.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// The Video Object of the Campaign Management Service. 
        /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Video</see>.
        /// </summary>
        public Video Video { get; set; }

        private static readonly IBulkMapping<BulkVideo>[] Mappings =
        {
            new SimpleBulkMapping<BulkVideo>(StringTable.Id,
                c => c.Video.Id.ToBulkString(),
                (v, c) => c.Video.Id = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.ParentId,
                c => c.AccountId.ToBulkString(),
                (v, c) => c.AccountId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.Status,
                c => c.Video.Status,
                (v, c) => c.Video.Status = v
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.Description,
                c => c.Video.Description,
                (v, c) => c.Video.Description = v
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.AspectRatio,
                c => c.Video.AspectRatio,
                (v, c) => c.Video.AspectRatio = v
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.Url,
                c => c.Video.Url,
                (v, c) => c.Video.Url = v
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.SourceUrl,
                c => c.Video.SourceUrl,
                (v, c) => c.Video.SourceUrl = v
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.ThumbnailUrl,
                c => c.Video.ThumbnailUrl,
                (v, c) => c.Video.ThumbnailUrl = v
            ),

            new SimpleBulkMapping<BulkVideo>(StringTable.DurationInMillionSeconds,
                c => c.Video.DurationInMilliseconds.ToBulkString(),
                (v, c) => c.Video.DurationInMilliseconds = v.ParseOptional<int>()
            ),

        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Video = new Video { };

            values.ConvertToEntity(this, Mappings); values.ConvertToEntity(this, Mappings);
        }
    }
}
