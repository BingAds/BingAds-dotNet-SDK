using System;
using System.Text;
using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal class CsvTextFormatter : ICsvTextFormatter
    {
        public const char TabSeparator = '\t';
        public const char CommaSeparator = ',';

        private readonly char separatorValue;
        private readonly string headers;

        public CsvTextFormatter(DownloadFileType fileType)
        {
            switch (fileType)
            {
                case DownloadFileType.Csv:
                    separatorValue = CommaSeparator;
                    break;

                case DownloadFileType.Tsv:
                    separatorValue = TabSeparator;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            this.headers = GetContentsForRow(CsvHeaders.Headers, false);
        }

        public string FormatCsvRow(string[] columns)
        {
            return this.GetContentsForRow(columns, true);
        }

        public string GetHeaders()
        {
            return this.headers;
        }

        private string GetContentsForRow(string[] values, bool escapeBroadMatchModifier)
        {
            if (values == null || values.Length == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < values.Length - 1; i++)
            {
                sb.Append(this.FormatSpecialCharacters(values[i], escapeBroadMatchModifier));
                sb.Append(this.separatorValue);
            }

            sb.Append(this.FormatSpecialCharacters(values[values.Length - 1], escapeBroadMatchModifier));

            return sb.ToString();
        }

        /// <remarks>
        /// If broad match modifier is found, the entire string will be escaped and returned. No further check for special characters will be made.
        /// </remarks>
        private string FormatSpecialCharacters(string strValue, bool escapeBroadMatchModifier)
        {
            bool escapeNeeded = false;
            StringBuilder stringBuilder = null;

            if (string.IsNullOrEmpty(strValue))
            {
                return string.Empty;
            }

            if (escapeBroadMatchModifier && (strValue[0] == '+'))
            {
                stringBuilder = new StringBuilder(strValue);
                // Insert space in the beggining to be able to escape the + sign. 
                // Without the space the character is still recognized as Excel formula.
                stringBuilder.Insert(0, " ");
                escapeNeeded = true;
            }

            // If the inputString contains comma in its content then it has to be escaped.
            if (strValue.IndexOf(this.separatorValue) >= 0)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder(strValue);
                }

                escapeNeeded = true;
            }

            // If the inputString contains double quotes in its content then it has to be escaped.
            if (strValue.IndexOf('"') >= 0)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder(strValue);
                }

                stringBuilder.Replace("\"", "\"\"");
                escapeNeeded = true;
            }

            if (escapeNeeded)
            {
                stringBuilder.Insert(0, "\"");
                stringBuilder.Insert(stringBuilder.Length, "\"");
                return stringBuilder.ToString();
            }

            return strValue;
        }
    }
}