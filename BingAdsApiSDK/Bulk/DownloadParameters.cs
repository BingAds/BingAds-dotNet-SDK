using System;
using System.Collections.Generic;

namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Describes the available parameters when submitting a file for upload, such as the type of entities and data scope that you want to download.
    /// </summary>
    public class DownloadParameters
    {
        private readonly SubmitDownloadParameters _submitDownloadParameters = new SubmitDownloadParameters();

        internal SubmitDownloadParameters SubmitDownloadParameters
        {
            get { return _submitDownloadParameters; }
        }

        /// <summary>
        /// The scope or types of data to download. 
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=511670">DataScope Value Set</see>.
        /// </summary>
        public DataScope DataScope
        {
            get { return _submitDownloadParameters.DataScope; }
            set { _submitDownloadParameters.DataScope = value; }
        }

        /// <summary>
        /// The type of entities to download.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=511671">BulkDownloadEntity Value Set</see>.
        /// </summary>
        public BulkDownloadEntity Entities
        {
            get { return _submitDownloadParameters.Entities; }
            set { _submitDownloadParameters.Entities = value; }
        }

        /// <summary>
        /// The extension type of the downloaded file.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=511672">DownloadFileType Value Set</see>.
        /// </summary>
        public DownloadFileType FileType
        {
            get { return _submitDownloadParameters.FileType; }
            set { _submitDownloadParameters.FileType = value; }
        }

        /// <summary>
        /// The campaigns to download. You can specify a maximum of 1,000 campaigns. 
        /// The campaigns that you specify must belong to the same account.
        /// </summary>
        public IList<long> CampaignIds
        {
            get { return _submitDownloadParameters.CampaignIds; }
            set { _submitDownloadParameters.CampaignIds = value; }
        }

        /// <summary>
        /// The last time that you requested a download. The date and time is expressed in Coordinated Universal Time (UTC).
        /// Typically, you request a full download the first time you call the operation by setting this element to null. On all subsequent calls you set the last sync time to the time stamp of the previous download. 
        /// The download file contains the time stamp of the download in the SyncTime column of the Account record. Use the time stamp to set LastSyncTimeInUTC the next time that you request a download. 
        /// If you specify the last sync time, only those entities that have changed (been updated or deleted) since the specified date and time will be downloaded. However, if the campaign data has not been previously downloaded, the operation performs a full download.
        /// </summary>
        public DateTime? LastSyncTimeInUTC
        {
            get { return _submitDownloadParameters.LastSyncTimeInUTC; }
            set { _submitDownloadParameters.LastSyncTimeInUTC = value; }
        }

        /// <summary>
        /// The date range values for the requested performance data in a bulk download.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=511673">PerformanceStatsDateRange Data Object</see>.
        /// </summary>
        public PerformanceStatsDateRange PerformanceStatsDateRange
        {
            get { return _submitDownloadParameters.PerformanceStatsDateRange; }
            set { _submitDownloadParameters.PerformanceStatsDateRange = value; }
        }

        /// <summary>
        /// Location target version
        /// </summary>
        public string LocationTargetVersion
        {
            get { return _submitDownloadParameters.LocationTargetVersion; }
            set { _submitDownloadParameters.LocationTargetVersion = value; }
        }

        /// <summary>
        /// The directory where the file will be downloaded.
        /// </summary>
        public string ResultFileDirectory { get; set; }

        /// <summary>
        /// The name of the download result file.
        /// </summary>
        public string ResultFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the local result file should be overwritten if it already exists.
        /// </summary>
        public bool OverwriteResultFile { get; set; }
    }
}