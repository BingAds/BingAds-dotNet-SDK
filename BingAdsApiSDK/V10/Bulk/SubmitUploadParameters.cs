using Microsoft.BingAds.V10.Bulk.Entities;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Describes the minimum available parameters when submitting a file for upload, such as the path of the upload file.
    /// </summary>
    public class SubmitUploadParameters
    {
        /// <summary>
        /// Initializes a new instance of the SubmitUploadParameters class.
        /// </summary>
        public SubmitUploadParameters()
        {
            ResponseMode = ResponseMode.ErrorsAndResults;

            CompressUploadFile = true;
        }

        /// <summary>
        /// Determines whether the bulk service should return upload errors with the corresponding entity data.
        /// </summary>
        /// <remarks>If not specified, this property is set by default to ErrorsAndResults.</remarks>
        public ResponseMode ResponseMode { get; set; }

        /// <summary>
        /// The fully qualified local path of the upload file.
        /// </summary>
        public string UploadFilePath { get; set; }

        /// <summary>
        /// Determines whether the upload file should be compressed before uploading. The default value is True.
        /// </summary>
        public bool CompressUploadFile { get; set; }

        internal bool RenameUploadFileToMatchRequestId { get; set; }
    }
}