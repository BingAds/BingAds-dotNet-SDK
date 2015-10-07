using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal class CsvReader : CsvLight, ICsvReader
    {
        private bool disposed;

        private Dictionary<string, int> _mappings;

        public CsvReader(string fileName, char delimiter)
            : base(GetStream(fileName), delimiter)
        {
        }

        /// <summary>
        /// For unit tests
        /// </summary>        
        internal CsvReader(StreamReader streamReader, char delimiter)
            : base(streamReader, delimiter)
        {
            
        }

        public RowValues ReadNextRow()
        {
            if (_mappings == null)
            {
                _mappings = Headers.Select((i, h) => new { key = i, value = h }).ToDictionary(x => x.key, x => x.value);
            }

            if (!ReadNextRecord())
            {
                return null;
            }

            if (Columns == null)
            {
                return null;
            }

            var rowValues = new RowValues(Columns, _mappings);

            return rowValues;
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                try
                {
                    if (disposing)
                    {
                        Stream.Dispose();
                    }
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }

            this.disposed = true;
        }

        private static StreamReader GetStream(string fileName)
        {
            return new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), System.Text.Encoding.Default, true);
        }
    }
}
