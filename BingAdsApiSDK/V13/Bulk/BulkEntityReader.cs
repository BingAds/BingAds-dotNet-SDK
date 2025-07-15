//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;

namespace Microsoft.BingAds.V13.Bulk
{
    /// <summary>
    /// Provides a method to read bulk entities from a bulk source and make them accessible as an enumerable list.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>.
    /// </summary>
    public class BulkEntityReader : IBulkEntityReader
    {
        private IBulkRecordReader _bulkRecordReader;

        public BulkEntityReader(IList<string> resultRows)
        {
            _bulkRecordReader = new BulkCsvRowReader(resultRows);
        }

        public BulkEntityReader(IBulkRecordReader bulkStreamReader)
        {
            this._bulkRecordReader = bulkStreamReader;
        }

        private IEnumerator<BulkEntity> _entitiesEnumerator;

        /// <summary>
        /// Reads next entity from the bulk source.
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
        /// Gets an enumerable list of bulk entities that were read from the source. 
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
        /// Reads next batch of entities from the source
        /// </summary>
        /// <returns>Next batch of entities</returns>
        /// <remarks>
        /// Batch means a set of related entities. 
        /// It can be one <see cref="SingleRecordBulkEntity"/>, one <see cref="MultiRecordBulkEntity"/> containing its child entities or a set of related child entities (for example several <see cref="BulkSiteLink"/>s logically belonging to the same SiteLink AdExtension.
        /// </remarks>
        private IEnumerable<BulkEntity> ReadNextBatch()
        {
            // Parse the next row in the source. The returned object can be:
            // * Object inherited from SingleLineBulkEntity - representing an entity from a single line, such as BulkCampaign or BulkKeyword or BulkSiteLink
            // * Object interited from BulkEntityIdentifier with Status = Deleted - representing a delete all row
            var nextObject = _bulkRecordReader.Read();

            // If returned object is null means we have reached the end of source
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
                multilineEntity.ReadRelatedData(_bulkRecordReader);

                if (IsForFullDownload())
                {
                    // If the source is from full download, multiline object always represents fully constructed entity, regardless of the delete row presence
                    return new[] {multilineEntity};
                }

                // Otherwise, either return the multiline entity itself or its child objects (depending on if the multiline entity is fully constructed (has all child objects), which is determined by the presence of the delete all row)
                return ExtractChildEntitiesIfNeeded(multilineEntity);
            }

            // If the current object is not part of multiline entity (it must be at least BulkEntity), just return it
            var bulkEntity = nextObject as BulkEntity;

            if (bulkEntity != null)
            {
                return new[] {bulkEntity};
            }

            // Something went wrong and we got an unexpected object from the source at this point...
            throw new InvalidOperationException("Invalid bulk object returned.");
        }

        protected virtual bool IsForFullDownload()
        {
            return false;
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
        /// <remarks>You should use this method when finished with an instance of <see cref="BulkEntityReader"/>.</remarks>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Disposes of the stream reader if set to true.</param>
        /// <remarks>You should use this method when finished with an instance of <see cref="BulkEntityReader"/>.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_bulkRecordReader != null)
                {
                    _bulkRecordReader.Dispose();

                    _bulkRecordReader = null;
                }
            }
        }

    }
}
