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
    /// Provides methods to write bulk entities to a file.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>.
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
        /// Initializes a new instance of this class with the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="fileFormat">The bulk file format.</param>
        public BulkFileWriter(Stream stream, DownloadFileType fileFormat)
            : this(new BulkObjectWriter(stream, fileFormat))
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
