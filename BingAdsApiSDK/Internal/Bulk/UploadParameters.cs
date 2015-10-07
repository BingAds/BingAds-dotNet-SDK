using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Bulk.Common;
using Microsoft.BingAds.Bulk.Entities;

namespace Microsoft.BingAds.Internal.Bulk
{
    /// <summary>
    /// The abstract base class for all upload parameter classes. You can use this class to dynamically instantiate a derived upload parameters class at run time.
    /// This class cannot be instantiated, and instead you should use either <see cref="EntityUploadParameters"/> or <see cref="FileUploadParameters"/>. 
    /// </summary>
    /// <seealso cref="EntityUploadParameters"/>
    /// <seealso cref="FileUploadParameters"/>
    public abstract class UploadParameters
    {
        /// <summary>
        /// Determines whether the bulk service should return upload errors with the corresponding <see cref="BulkEntity"/> data.
        /// For possible values, see <see href="http://go.microsoft.com/fwlink/?LinkId=511681">ResponseMode Value Set</see>.
        /// </summary>
        /// <remarks>If not specified, this property is set by default to ErrorsAndResults.</remarks>
        public ResponseMode ResponseMode { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the class derived from <see cref="UploadParameters"/>.
        /// </summary>
        protected UploadParameters()
        {
            ResponseMode = ResponseMode.ErrorsAndResults;
        }
    }
}