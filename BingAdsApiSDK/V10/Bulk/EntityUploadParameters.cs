using System.Collections.Generic;
using Microsoft.BingAds.V10.Bulk.Entities;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Describes the available parameters when submitting entities for upload, such as the entities that you want to upload.
    /// </summary>
    public class EntityUploadParameters
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public EntityUploadParameters()
        {
            ResponseMode = ResponseMode.ErrorsAndResults;
        }

        /// <summary>
        /// Determines whether the bulk service should return upload errors with the corresponding entity data.
        /// </summary>
        /// <remarks>If not specified, this property is set to ErrorsAndResults.</remarks>
        public ResponseMode ResponseMode { get; set; }

        /// <summary>
        /// The list of bulk entities that you want to upload.
        /// </summary>
        public IEnumerable<BulkEntity> Entities { get; set; }

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
