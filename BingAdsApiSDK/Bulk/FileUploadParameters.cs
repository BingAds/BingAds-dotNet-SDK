using Microsoft.BingAds.Bulk.Entities;

namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Describes the available parameters when submitting a file for upload, such as the path of the upload result file.
    /// </summary>
    public class FileUploadParameters
    {
        private readonly SubmitUploadParameters _submitUploadParameters = new SubmitUploadParameters();

        internal SubmitUploadParameters SubmitUploadParameters
        {
            get { return _submitUploadParameters; }
        }

        /// <summary>
        /// Determines whether the bulk service should return upload errors with the corresponding <see cref="BulkEntity"/> data.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=511681">ResponseMode Value Set</see>.
        /// </summary>
        /// <remarks>If not specified, this property is set to ErrorsAndResults.</remarks>
        public ResponseMode ResponseMode
        {
            get { return _submitUploadParameters.ResponseMode; }
            set { _submitUploadParameters.ResponseMode = value; }
        }

        /// <summary>
        /// The fully qualified local path of the upload file.
        /// </summary>
        public string UploadFilePath
        {
            get { return _submitUploadParameters.UploadFilePath; }
            set { _submitUploadParameters.UploadFilePath = value; }
        }

        /// <summary>
        /// Determines whether the upload file should be compressed before uploading. The default value is True.
        /// </summary>
        public bool CompressUploadFile
        {
            get { return _submitUploadParameters.CompressUploadFile; }
            set { _submitUploadParameters.CompressUploadFile = value; }
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

        internal bool RenameUploadFileToMatchRequestId
        {
            get { return _submitUploadParameters.RenameUploadFileToMatchRequestId; }
            set { _submitUploadParameters.RenameUploadFileToMatchRequestId = value; }
        }
    }
}