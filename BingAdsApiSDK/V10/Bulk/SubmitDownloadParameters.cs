using System;
using System.Collections.Generic;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Describes the service request parameters such as the type of entities and data scope that you want to download.
    /// </summary>
    public class SubmitDownloadParameters
    {
        /// <summary>
        /// The scope or types of data to download. 
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=620271">DataScope Value Set</see>.
        /// </summary>
        public DataScope DataScope { get; set; }

        /// <summary>
        /// The type of entities to download.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=620272">BulkDownloadEntity Value Set</see>.
        /// </summary>
        public BulkDownloadEntity Entities { get; set; }

        /// <summary>
        /// The extension type of the downloaded file.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=620273">DownloadFileType Value Set</see>.
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
        /// The date range values for the requested performance data in a bulk download.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=620274">PerformanceStatsDateRange Data Object</see>.
        /// </summary>
        public PerformanceStatsDateRange PerformanceStatsDateRange { get; set; }

        /// <summary>
        /// Location target version
        /// </summary>
        public string LocationTargetVersion { get; set; }
    }
}