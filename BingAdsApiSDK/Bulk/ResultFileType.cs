namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Defines the possible types of result files. 
    /// </summary>
    public enum ResultFileType
    {
        /// <summary>
        /// The result file represents the full sync of entities that were specified in the download request.
        /// </summary>
        FullDownload,
        /// <summary>
        /// The result file represents the partial sync of entities that were specified in the download request.
        /// </summary>
        PartialDownload,
        /// <summary>
        /// The result file represents the entities specified in the upload request, or the corresponding errors, 
        /// or both entities and errors.
        /// </summary>
        Upload
    }
}
