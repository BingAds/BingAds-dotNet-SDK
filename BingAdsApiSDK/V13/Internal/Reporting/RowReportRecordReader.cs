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

namespace Microsoft.BingAds.V13.Internal.Reporting
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class RowReportRecordReader:IEnumerator<RowReportRecord>
    {
        private readonly CsvReportRecordReader reader;
        private readonly Regex footerRegex = new Regex("Total|©\\d+ Microsoft Corporation. All rights reserved.*");
        private Dictionary<string, int> columnMapping;
        private RowReportRecord nextRecord;

        public RowReportRecordReader(CsvReportRecordReader reader)
        {
            this.reader = reader;
        }

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            return this.nextRecord != null;
        }

        public void Reset()
        {
            this.reader.Reset();
        }

        public RowReportRecord Current
        {
            get
            {
                RowReportRecord ret = this.nextRecord;
                this.nextRecord = null;
                this.Peek();
                return ret;
            }
        }

        public void Dispose()
        {
            this.reader.Dispose();
        }



        private void Peek()
        {
            if (this.reader.MoveNext())
            {
                string[] fields = this.reader.Current;

                if (!this.ValidateRecord(fields))
                {
                    this.Peek();
                    return;
                }

                this.nextRecord = new RowReportRecord(new RowValues(fields, this.columnMapping));
            }
        }

        private bool ValidateHeader(string[] fields)
        {
            if (fields == null)
                return false;

            long validValues = fields.Select(s => s.Replace("-", string.Empty)).Select(s => s.Trim())
                .Count(s => !string.IsNullOrEmpty(s));
            return validValues >= 1;
        }


        private bool ValidateRecord(string[] fields)
        {
            if (fields == null)
            {
                return false;
            }
                
            long validValues = fields.Select(s => s.Replace("-", string.Empty)).Select(s => s.Trim())
                                   .Count(s => s.Length > 0);
            if (validValues == 0)
            {
                return false;
            }

            if (this.footerRegex.IsMatch(fields[0]))
            {
                return false;
            }

            return true;
        }

        private Dictionary<string, int> GenerateColumnMapping(string[] headers)
        {
            Dictionary<string, int> mapping = new Dictionary<string, int>();

            for (int i = 0; i < headers.Length; i++)
            {
                mapping.Add(headers[i], i);
            }

            return mapping;
        }

        public bool ReadNextHeader(IRowReportHeaderParser parser)
        {
            if (!reader.MoveNext())
            {
                return false;
            }

            string[] fields = this.reader.Current;
            fields = fields.Select(s => s.Trim()).Where(s => s.Length > 0).ToArray();
            long validValueCount = fields.Length;
            if (!this.ValidateHeader(fields))
            {
                return this.ReadNextHeader(parser);
            }


            if (validValueCount == 0)
            {
                return false;
            }

            bool header = parser.ParseHeader(fields);
            if (validValueCount > 1)
            {
                if (header)
                {
                    this.columnMapping = this.GenerateColumnMapping(fields);
                    this.Peek();
                }
                else
                {
                    // when ExcludeColumnHeader is set, this line is a record indeed
                    this.nextRecord = new RowReportRecord(new RowValues(fields, this.columnMapping));
                }

                return false;
            }
            
            return true;
        }
    }
}
