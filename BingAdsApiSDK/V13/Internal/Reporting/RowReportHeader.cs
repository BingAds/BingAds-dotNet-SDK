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
    using System.Linq;
    using System.Text.RegularExpressions;

    public class RowReportHeader : ReportHeader, IRowReportHeaderParser
    {
        private const string ReportName = "Report Name";

        private const string ReportTime = "Report Time";

        private const string ReportAggregation = "Report Aggregation";

        private const string ReportFilter = "Report Filter";

        private const string TimeZone = "Time Zone";

        private const string Rows = "Rows";

        private const string LastCompletedAvailableDay = "Last Completed Available Day";

        private const string LastCompletedAvailableHour = "Last Completed Available Hour";
    
        private const string PotentialIncompleteData = "Potential Incomplete Data";

        private readonly Regex headerRegex = new Regex(@"(Report Name|Report Time|Report Aggregation|Report Filter|Time Zone|Rows|Last Completed Available Day|Last Completed Available Hour|Potential Incomplete Data): (.*)$");

        private readonly Regex timeRegex = new Regex(@"\d+\/\d+\/\d{4} \d+:\d{2}:\d{2} AM|\d+\/\d+\/\d{4} \d+:\d{2}:\d{2} PM");

        private readonly RowReportStreamReader reportStreamReader;

        public RowReportHeader(RowReportStreamReader reportStreamReader)
        {
            this.reportStreamReader = reportStreamReader;
            this.ReadReportHeader();
        }

        public bool ParseHeader(string[] fields)
        {
            fields = fields.Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            long validValueCount = fields.Length;
            if (validValueCount == 1 && fields[0].Contains(":"))
            {
                this.ParseReportMetadata(fields[0]);
                return true;
            }

            return this.SetReportColumns(fields);            
        }

        private void ReadReportHeader()
        {
            this.reportStreamReader.ReadReportHeader(this);
        }

        private void ParseReportMetadata(string header)
        {
            if (header == null)
            {
                return;
            }

            var matcher = this.headerRegex.Match(header);
            if (!matcher.Success)
            {
                return;
            }

            string headerName = matcher.Groups[1].Value;
            string headerValue = matcher.Groups[2].Value.Trim();

            switch (headerName)
            {
                case ReportName:
                    this.SetReportName(headerValue);
                    break;
                case ReportTime:
                    var time = headerValue.Split(',');
                    this.SetReportTimeStart(time[0]);
                    this.SetReportTimeEnd(time.Length > 1 ? time[1] : time[0]);
                    break;
                case TimeZone:
                    this.SetReportTimeZone(headerValue);
                    break;
                case LastCompletedAvailableDay:
                    this.SetLastCompletedAvailableDate(this.timeRegex.Match(headerValue).Value);
                    break;
                case ReportAggregation:
                    this.SetReportAggregation(headerValue);
                    break;
                case ReportFilter:
                    this.SetReportFilter(headerValue);
                    break;
                case Rows:
                    this.SetRecordCount(headerValue);
                    break;
                case PotentialIncompleteData:
                    this.SetPotentialIncompleteData(headerValue);
                    break;
            }
        }
    }
}
