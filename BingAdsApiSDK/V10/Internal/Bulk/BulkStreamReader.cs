using System;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    /// <summary>
    /// Reads a bulk object and also its related data (for example corresponding errors) from the stream
    /// </summary>
    internal class BulkStreamReader : IBulkStreamReader
    {
        private IBulkObjectReader _bulkObjectReader;

        public BulkStreamReader(string fileName, DownloadFileType fileType)
        {
            _bulkObjectReader = new BulkObjectReader(fileName, fileType == DownloadFileType.Csv ? ',' : '\t');
        }

        internal BulkStreamReader(IBulkObjectReader reader)
        {
            _bulkObjectReader = reader;
        }

        private BulkObject _nextObject;

        /// <summary>
        /// Returns the next object from the file
        /// </summary>
        /// <returns>Next object</returns>
        public BulkObject Read()
        {
            BulkObject result;

            TryRead(_ => true, out result);

            return result;
        }

        /// <summary>
        /// Reads the object only if it has a certain type
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="result">The next object from the file if the object has the same type as requested, null otherwise</param>
        /// <returns>True is object has requested type, false otherwise</returns>
        public bool TryRead<T>(out T result)
            where T : BulkObject
        {
            return TryRead(_ => true, out result);
        }

        /// <summary>
        /// Reads the object only if it matches a predicate
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="predicate">Predicate that needs to be matched</param>
        /// <param name="result">The next object from the file if the object matches the predicate, null otherwise</param>
        /// <returns>True is object matches the predicate, false otherwise</returns>
        public bool TryRead<T>(Predicate<T> predicate, out T result)
            where T : BulkObject
        {
            var peeked = Peek();

            var instanceOfT = peeked as T;

            if (instanceOfT != null && predicate(instanceOfT))
            {
                _nextObject = null;

                instanceOfT.ReadRelatedDataFromStream(this);

                result = instanceOfT;

                return true;
            }

            result = null;

            return false;
        }

        private bool _passedFirstRow;

        private BulkObject Peek()
        {
            if (!_passedFirstRow)
            {
                var firstRowObject = _bulkObjectReader.ReadNextBulkObject();

                var formatVersion = firstRowObject as FormatVersion;

                if (formatVersion != null)
                {
                    if (formatVersion.Value != "4" && formatVersion.Value != "4.0")
                    {
                        throw new InvalidOperationException(ErrorMessages.FormatVersionIsNotSupported +
                                                            formatVersion.Value);
                    }
                }
                else
                {
                    _nextObject = firstRowObject;
                }

                _passedFirstRow = true;
            }

            if (_nextObject != null)
            {
                return _nextObject;
            }

            return _nextObject = _bulkObjectReader.ReadNextBulkObject();
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
                if (_bulkObjectReader != null)
                {
                    _bulkObjectReader.Dispose();

                    _bulkObjectReader = null;
                }
            }
        }
    }
}