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




namespace Microsoft.BingAds.V13.Internal.Bulk
{
    /// <summary>
    /// Provides a method to read one row from bulk file and return the corresponding <see cref="BulkObject"/>
    /// </summary>
    internal class BulkObjectReader : IBulkObjectReader
    {
        private ICsvReader _csvReader;

        private readonly IBulkObjectFactory _bulkObjectFactory;        

        public BulkObjectReader(string fileName, char delimiter)            
        {
            _csvReader = new CsvReader(fileName, delimiter);

            _bulkObjectFactory = new BulkObjectFactory();
        }

        public BulkObjectReader(Stream stream, char delimiter)
        {
            _csvReader = new CsvReader(stream, delimiter);

            _bulkObjectFactory = new BulkObjectFactory();
        }

        public BulkObjectReader(IList<string> csvRows)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            foreach (var row in csvRows)
            {
                writer.WriteLine(row);
            }
            writer.Flush();
            stream.Position = 0;

            _csvReader = new CsvReader(stream, ',');
            _bulkObjectFactory = new BulkObjectFactory();
        }

        /// <summary>
        /// For unit tests
        /// </summary>
        public BulkObjectReader(ICsvReader csvReader, IBulkObjectFactory bulkObjectFactory)
        {
            _csvReader = csvReader;

            _bulkObjectFactory = bulkObjectFactory;
        }

        /// <summary>
        /// Reads the next csv row values, creates a new instance of the object and populates it with the row values
        /// </summary>
        /// <returns>Next <see cref="BulkObject"/></returns>
        public BulkObject ReadNextBulkObject()
        {
            var rowValues = _csvReader.ReadNextRow();

            if (rowValues == null)
            {
                return null;
            }

            var bulkObject = _bulkObjectFactory.CreateBulkObject(rowValues);

            bulkObject.ReadFromRowValues(rowValues);

            return bulkObject;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>You should use this method when finished with an instance of <see cref="BulkObjectReader"/>.</remarks>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Disposes of the stream reader if set to true.</param>
        /// <remarks>You should use this method when finished with an instance of <see cref="BulkObjectReader"/>.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_csvReader != null)
                {
                    _csvReader.Dispose();

                    _csvReader = null;
                }
            }
        }
    }
}
