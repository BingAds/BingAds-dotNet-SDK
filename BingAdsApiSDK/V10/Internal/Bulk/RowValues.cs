using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal class RowValues
    {
        private readonly string[] _columns;

        private readonly Dictionary<string, int> _mappings;

        public string[] Columns
        {
            get { return _columns; }
        }

        public RowValues(string[] columns, Dictionary<string, int> mappings)
        {
            _columns = columns;
            _mappings = mappings;
        }

        public RowValues()
        {
            _mappings = CsvHeaders.GetMappings();
            _columns = new string[_mappings.Count];
        }

        public RowValues(Dictionary<string, string> dict)
        {
            _mappings = CsvHeaders.GetMappings();
            _columns = new string[_mappings.Count];

            foreach (var pair in dict)
            {
                this[pair.Key] = pair.Value;
            }
        }

        public string this[string header]
        {
            get
            {
                return _columns[_mappings[header]];
            }
            set
            {
                _columns[_mappings[header]] = value;
            }
        }

        public bool ContainsHeader(string header)
        {
            return _mappings.ContainsKey(header);
        }

        public bool TryGetValue(string header, out string result)
        {
            int index;

            if (!_mappings.TryGetValue(header, out index))
            {
                result = null;

                return false;
            }

            result = _columns[index];

            return true;
        }

        public Dictionary<string, string> ToDictionary()
        {
            return _mappings.Select(m => new { key = m.Key, value = _columns[m.Value] }).ToDictionary(x => x.key, x => x.value);
        }

        public string ToDebugString()
        {
            return string.Join("; ", _mappings.Select(m => string.Format("{0} = '{1}'", m.Key, _columns[m.Value])));
        }
    }
}
