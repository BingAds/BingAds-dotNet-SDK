namespace Microsoft.BingAds.Reporting
{
    /// <summary>
    /// Describes the available parameters when downloading file from reporting service.
    /// </summary>
    public class ReportingDownloadParameters
    {
        /// <summary>
        /// Defines the base object for all report requests.
        /// </summary>
        /// <remarks>
        /// See <see href="http://msdn.microsoft.com/en-us/library/bb671813(v=msads.90).aspx">ReportRequest Data Object</see> http://msdn.microsoft.com/en-us/library/bb671813(v=msads.90).aspx for details.
        /// <para>Used by <see cref="ReportingServiceClient.SubmitGenerateReport">SubmitGenerateReport</see> service operation.</para>
        /// </remarks>
        public ReportRequest ReportRequest { get; set; }

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