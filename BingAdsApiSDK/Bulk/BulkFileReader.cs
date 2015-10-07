using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Entities;

namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Provides a method to read bulk entities from a bulk file and make them accessible as an enumerable list.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
    /// </summary>
    public class BulkFileReader : IDisposable
    {
        private IBulkStreamReader _bulkStreamReader;

        private readonly string _bulkFilePath;

        private readonly bool _isForFullDownload;

        /// <summary>
        /// The path of the bulk file to read. 
        /// </summary>
        public string BulkFilePath
        {
            get { return _bulkFilePath; }
        }

        /// <summary>
        /// Initializes a new instance of this class with the specified file details.
        /// </summary>
        /// <param name="filePath">The path of the bulk file to read.</param>
        /// <param name="resultFileType">The result file type.</param>
        /// <param name="fileFormat">The bulk file format.</param>
        public BulkFileReader(string filePath, ResultFileType resultFileType, Bulk.DownloadFileType fileFormat)
        {
            _bulkFilePath = filePath;

            _isForFullDownload = resultFileType == ResultFileType.FullDownload;

            _bulkStreamReader = new BulkStreamReader(filePath, fileFormat);
        }

        /// <summary>
        /// For unit testing
        /// </summary>        
        internal BulkFileReader(IBulkStreamReader streamReader, bool isForFullDownload)
        {
            _isForFullDownload = isForFullDownload;

            _bulkStreamReader = streamReader;
        }

        private IEnumerator<BulkEntity> _entitiesEnumerator;

        /// <summary>
        /// Reads next entity from the bulk file.
        /// </summary>
        /// <returns>Next entity.</returns>
        public BulkEntity ReadNextEntity()
        {
            if (_entitiesEnumerator == null)
            {
                _entitiesEnumerator = ReadEntities().GetEnumerator();
            }

            return _entitiesEnumerator.MoveNext() ? _entitiesEnumerator.Current : null;
        }

        /// <summary>
        /// Gets an enumerable list of bulk entities that were read from the file. 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of type <see cref="BulkEntity"/>.</returns>
        public IEnumerable<BulkEntity> ReadEntities()
        {
            IEnumerable<BulkEntity> nextBatch;

            while ((nextBatch = ReadNextBatch()) != null)
            {
                foreach (var entity in nextBatch)
                {
                    yield return entity;
                }
            }
        }

        /// <summary>
        /// Reads next batch of entities from the file
        /// </summary>
        /// <returns>Next batch of entities</returns>
        /// <remarks>
        /// Batch means a set of related entities. 
        /// It can be one <see cref="SingleRecordBulkEntity"/>, one <see cref="MultiRecordBulkEntity"/> containing its child entities or a set of related child entities (for example several <see cref="BulkSiteLink"/>s logically belonging to the same SiteLink AdExtension.
        /// </remarks>
        private IEnumerable<BulkEntity> ReadNextBatch()
        {
            // Parse the next row in the file. The returned object can be:
            // * Object inherited from SingleLineBulkEntity - representing an entity from a single file line, such as BulkCampaign or BulkKeyword or BulkSiteLink
            // * Object interited from BulkEntityIdentifier with Status = Deleted - representing a delete all row
            var nextObject = _bulkStreamReader.Read();

            // If returned object is null means we have reached the end of file
            if (nextObject == null)
            {
                return null;
            }

            // If returned object is logically part of multiline entity (for example BulkSiteLink is logically part of multiline BulkSiteLinkAdExtension)
            if (nextObject.CanEncloseInMultilineEntity)
            {
                // Create multiline object containing the current child object
                var multilineEntity = nextObject.EncloseInMultilineEntity();

                // Read related data for the multiline object (will read other child objects belonging to the same parent)
                multilineEntity.ReadRelatedDataFromStream(_bulkStreamReader);

                if (_isForFullDownload)
                {
                    // If the file is from full download, multiline object always represents fully constructed entity, regardless of the delete row presence
                    return new[] { multilineEntity };
                }

                // Otherwise, either return the multiline entity itself or its child objects (depending on if the multiline entity is fully constructed (has all child objects), which is determined by the presence of the delete all row)
                return ExtractChildEntitiesIfNeeded(multilineEntity);
            }

            // If the current object is not part of multiline entity (it must be at least BulkEntity), just return it
            var bulkEntity = nextObject as BulkEntity;

            if (bulkEntity != null)
            {
                return new[] { bulkEntity };
            }

            // Something went wrong and we got an unexpected object from the file at this point...
            throw new InvalidOperationException("Invalid bulk object returned.");
        }

        // Returns either the multiline entity itself or its child objects
        private IEnumerable<BulkEntity> ExtractChildEntitiesIfNeeded(BulkEntity entity)
        {
            var multilineEntity = entity as MultiRecordBulkEntity;

            // If the entity is a multiline entity and it has all child objects (delete all row was present), just return it
            if (multilineEntity == null || multilineEntity.AllChildrenArePresent)
            {
                yield return entity;
            }
            else
            {
                // If not all child objects are present (there was no delete all row and we only have part of the multiline entity), return child object individually 
                foreach (var child in (multilineEntity.ChildEntities.SelectMany(ExtractChildEntitiesIfNeeded)))
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>You should use this method when finished with an instance of <see cref="BulkFileReader"/>.</remarks>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Disposes of the stream reader if set to true.</param>
        /// <remarks>You should use this method when finished with an instance of <see cref="BulkFileReader"/>.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_bulkStreamReader != null)
                {
                    _bulkStreamReader.Dispose();

                    _bulkStreamReader = null;
                }
            }
        }
    }
}