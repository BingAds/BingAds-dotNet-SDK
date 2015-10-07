namespace Microsoft.BingAds.Reporting
{
    /// <summary>
    /// Contains tracking details about the results and status of the corresponding <see cref="ReportingDownloadOperation"/> .
    /// </summary>
    public class ReportingOperationStatus
    {
        /// <summary>
        /// The identifier of the log entry that contains the details of the download request.  
        /// </summary>
        public string TrackingId { get; set; }

        /// <summary>
        /// Defines the status of a report.
        /// </summary>
        /// <remarks>
        /// See <see href="http://msdn.microsoft.com/en-us/library/bb671578(v=msads.90).aspx">ReportRequestStatusType Value Set</see> http://msdn.microsoft.com/en-us/library/bb671578(v=msads.90).aspx for details.
        /// <para>Used by <see cref="ReportRequestStatus"/> data object.</para>
        /// </remarks>
        public ReportRequestStatusType Status { get; set; }

        /// <summary>
        /// The download result file Url.
        /// </summary>
        public string ResultFileUrl { get; set; }
    }
}