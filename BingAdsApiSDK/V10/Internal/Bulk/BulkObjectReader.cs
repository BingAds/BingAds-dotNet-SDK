

using System;

namespace Microsoft.BingAds.V10.Internal.Bulk
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
