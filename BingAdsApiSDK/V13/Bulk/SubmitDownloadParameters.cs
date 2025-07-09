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


namespace Microsoft.BingAds.V13.Bulk
{
    /// <summary>
    /// Describes the service request parameters such as the type of entities and data scope that you want to download.
    /// </summary>
    public class SubmitDownloadParameters
    {
        /// <summary>
        /// The scope or types of data to download. 
        /// For possible values, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">DataScope Value Set</see>.
        /// </summary>
        public DataScope DataScope { get; set; }

        /// <summary>
        /// The entities to download
        /// </summary>
        public IList<DownloadEntity> DownloadEntities { get; set; }

        /// <summary>
        /// The extension type of the downloaded file.
        /// For possible values, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">DownloadFileType Value Set</see>.
        /// </summary>
        public DownloadFileType FileType { get; set; }

        /// <summary>
        /// The campaigns to download. You can specify a maximum of 1,000 campaigns. 
        /// The campaigns that you specify must belong to the same account.
        /// </summary>
        public IList<long> CampaignIds { get; set; }

        /// <summary>
        /// The last time that you requested a download. The date and time is expressed in Coordinated Universal Time (UTC).
        /// Typically, you request a full download the first time you call the operation by setting this element to null. On all subsequent calls you set the last sync time to the time stamp of the previous download. 
        /// The download file contains the time stamp of the download in the SyncTime column of the Account record. Use the time stamp to set LastSyncTimeInUTC the next time that you request a download. 
        /// If you specify the last sync time, only those entities that have changed (been updated or deleted) since the specified date and time will be downloaded. However, if the campaign data has not been previously downloaded, the operation performs a full download.
        /// </summary>
        public DateTime? LastSyncTimeInUTC { get; set; }

        /// <summary>
        /// Location target version
        /// </summary>
        public string LocationTargetVersion { get; set; }
    }
}
