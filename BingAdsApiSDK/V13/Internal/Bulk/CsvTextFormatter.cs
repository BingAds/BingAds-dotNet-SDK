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

using System.Text;
using Microsoft.BingAds.V13.Bulk;

namespace Microsoft.BingAds.V13.Internal.Bulk
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
