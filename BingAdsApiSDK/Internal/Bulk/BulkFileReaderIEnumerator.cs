using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Bulk.Entities;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal class BulkFileReaderIEnumerator : IEnumerator<BulkEntity>
    {
        private BulkFileReader _bulkFileReader;

        private readonly IEnumerator<BulkEntity> _readEntities; 

        public BulkFileReaderIEnumerator(BulkFileReader bulkFileReader)
        {
            _bulkFileReader = bulkFileReader;

            _readEntities = _bulkFileReader.ReadEntities().GetEnumerator();
        }

        public BulkEntity Current
        {
            get { return _readEntities.Current; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _readEntities.Current; }
        }

        public bool MoveNext()
        {
            return _readEntities.MoveNext();
        }

        public void Reset()
        {
            _readEntities.Reset();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_bulkFileReader != null)
                {
                    _bulkFileReader.Dispose();

                    _bulkFileReader = null;
                }
            }
        }
    }
}
