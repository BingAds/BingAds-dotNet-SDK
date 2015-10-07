using System;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Entities;

namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Provides methods to write bulk entities to a file.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
    /// </summary>
    public class BulkFileWriter : IDisposable
    {
        private IBulkObjectWriter _bulkObjectWriter;        
        
        /// <summary>
        /// Initializes a new instance of this class with the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the bulk file to write.</param>
        public BulkFileWriter(string filePath)
            : this(filePath, DownloadFileType.Csv)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of this class with the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the bulk file to write.</param>
        /// <param name="fileFormat">The bulk file format.</param>
        public BulkFileWriter(string filePath, DownloadFileType fileFormat)
            : this(new BulkObjectWriter(filePath, fileFormat))
        {

        }

        /// <summary>
        /// For unit tests
        /// </summary>        
        internal BulkFileWriter(IBulkObjectWriter bulkObjectWriter)
        {
            _bulkObjectWriter = bulkObjectWriter;

            _bulkObjectWriter.WriteFileMetadata();
        }

        /// <summary>
        /// Writes the specified <see cref="BulkEntity"/> to the file.
        /// </summary>
        /// <param name="entity">The bulk entity to write to the file.</param>
        /// <remarks>
        /// Bulk entities that are derived from <see cref="SingleRecordBulkEntity"/> will be written to a single row in the file.
        /// Bulk entities that are derived from <see cref="MultiRecordBulkEntity"/> will be written to multiple rows in the file.
        /// </remarks>
        public void WriteEntity(BulkEntity entity)
        {            
            entity.WriteToStream(_bulkObjectWriter, false);            
        }

        /// <summary>
        /// Writes the specified <see cref="BulkEntity"/> to the file.
        /// </summary>
        /// <param name="entity">The bulk entity to write to the file.</param>
        /// <param name="excludeReadonlyData">Whether read only fields should be ignored.</param>
        /// <remarks>
        /// Bulk entities that are derived from <see cref="SingleRecordBulkEntity"/> will be written to a single row in the file.
        /// Bulk entities that are derived from <see cref="MultiRecordBulkEntity"/> will be written to multiple rows in the file.
        /// </remarks>
        public void WriteEntity(BulkEntity entity, bool excludeReadonlyData)
        {
            entity.WriteToStream(_bulkObjectWriter, excludeReadonlyData);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>You should use this method before you upload the file written with <see cref="BulkFileWriter"/>.</remarks>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Disposes of the object writer if set to true.</param>
        /// <remarks>You should use this method before you upload the file written with <see cref="BulkFileWriter"/>.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_bulkObjectWriter != null)
                {
                    _bulkObjectWriter.Dispose();

                    _bulkObjectWriter = null;
                }
            }            
        }
    }
}
