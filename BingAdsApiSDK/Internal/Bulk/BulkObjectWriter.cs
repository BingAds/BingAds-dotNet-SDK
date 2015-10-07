using System;
using System.IO;
using System.Text;
using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk
{
    internal class BulkObjectWriter : IBulkObjectWriter
    {
        private readonly ICsvTextFormatter _formatter;

        private readonly IBulkObjectFactory _bulkObjectFactory;

        private StreamWriter _streamWriter;

        public BulkObjectWriter(string fileName, DownloadFileType fileFormat)
            : this(new StreamWriter(fileName, false, Encoding.UTF8), new BulkObjectFactory(), new CsvTextFormatter(fileFormat))
        {
            
        }

        /// <summary>
        /// For unit testing
        /// </summary>
        internal BulkObjectWriter(StreamWriter streamWriter, IBulkObjectFactory bulkObjectFactory, ICsvTextFormatter csvTextFormatter)
        {
            _streamWriter = streamWriter;

            _bulkObjectFactory = bulkObjectFactory;

            _formatter = csvTextFormatter;
        }

        public void WriteFileMetadata()
        {
            WriteHeaders();
            WriteFormatVersion();
        }

        public void WriteObjectRow(BulkObject bulkObject, bool excludeReadonlyData)
        {            
            var values = new RowValues();

            bulkObject.WriteToRowValues(values, excludeReadonlyData);

            values[StringTable.Type] = _bulkObjectFactory.GetBulkRowType(bulkObject);

            _streamWriter.WriteLine(_formatter.FormatCsvRow(values.Columns));
        }

        public void WriteObjectRow(BulkObject bulkObject)
        {
            WriteObjectRow(bulkObject, false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void WriteHeaders()
        {
            _streamWriter.WriteLine(_formatter.GetHeaders());
        }

        private void WriteFormatVersion()
        {
            var versionRow = new RowValues();

            versionRow[StringTable.Type] = StringTable.SemanticVersion;
            versionRow[StringTable.Name] = BulkServiceManager.FormatVersion;

            _streamWriter.WriteLine(_formatter.FormatCsvRow(versionRow.Columns));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {         
                if (_streamWriter != null)
                {
                    _streamWriter.Dispose();
                    _streamWriter = null;
                }
            }            
        }
    }
}
