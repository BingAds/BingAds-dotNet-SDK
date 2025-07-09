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

using Microsoft.BingAds.Internal;
using Microsoft.BingAds.V13.Bulk;

namespace Microsoft.BingAds.V13.Internal.Bulk
{
    /// <summary>
    /// Reads a bulk object and also its related data (for example corresponding errors) from the stream
    /// </summary>
    internal abstract class BulkRecordReader : IBulkRecordReader
    {
        protected IBulkObjectReader _bulkObjectReader;

        protected BulkObject _nextObject;

        protected BulkRecordReader(IBulkObjectReader reader)
        {
            _bulkObjectReader = reader;
        }

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

                instanceOfT.ReadRelatedData(this);

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
                    if (formatVersion.Value != "6" && formatVersion.Value != "6.0")
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
