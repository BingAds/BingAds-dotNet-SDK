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
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using V13.Reporting;

    /// <summary>
    /// Reserved for internal use.
    /// </summary> 
    public class XmlReport : Report
    {
        private readonly Regex timeregex = new Regex(@"\d+\/\d+\/\d{4} \d+:\d{2}:\d{2} AM|\d+\/\d+\/\d{4} \d+:\d{2}:\d{2} PM");

        public XmlReport(string filePath)
        {
            this.ReportHeader = new ReportHeader();

            XmlReader reader = XmlReader.Create(filePath);
            this.ReadHeader(reader);
            this.recordIterator = new XmlReportRecordIterator(reader, filePath);
        }

        public void ReadHeader(XmlReader reader)
        {
            if (!reader.ReadToNextSibling("Report"))
            {
                return;
            }

            if (reader.GetAttribute("ReportName") != null)
            {
                this.ReportHeader.SetReportName(reader.GetAttribute("ReportName"));
            }

            if (reader.GetAttribute("ReportTime") != null)
            {
                var time = reader.GetAttribute("ReportTime").Split(',');
                this.ReportHeader.SetReportTimeStart(time[0]);
                this.ReportHeader.SetReportTimeEnd(time.Length > 1 ? time[1] : time[0]);
            }

            if (reader.GetAttribute("LastCompletedAvailableDay") != null)
            {
                this.ReportHeader.SetLastCompletedAvailableDate(
                    this.timeregex.Match(reader.GetAttribute("LastCompletedAvailableDay")).Value);
            }

            if (reader.GetAttribute("TimeZone") != null)
            {
                this.ReportHeader.SetReportTimeZone(reader.GetAttribute("TimeZone"));
            }

            if (reader.GetAttribute("ReportAggregation") != null)
            {
                this.ReportHeader.SetReportAggregation(reader.GetAttribute("ReportAggregation"));
            }

            if (reader.GetAttribute("PotentialIncompleteData") != null)
            {
                this.ReportHeader.SetPotentialIncompleteData(reader.GetAttribute("PotentialIncompleteData"));
            }

            if (reader.GetAttribute("Rows") != null)
            {
                this.ReportHeader.SetRecordCount(reader.GetAttribute("Rows"));
            }

            while (reader.Read())
            {
                if (reader.Name == "Table" || reader.Name.EndsWith("ReportColumns"))
                {
                    break;
                }
            }

            List<string> tmpColumns = new List<string>();
            List<string> row = new List<string>();
            if (reader.Name.EndsWith("ReportColumns"))
            {
                while (reader.Read() && reader.Name == "Column" || reader.Name == string.Empty)
                {
                    var columnName = reader.GetAttribute("name");
                    if (columnName != null)
                    {
                        tmpColumns.Add(columnName);
                    }
                }
            }

            this.ReportHeader.SetReportColumns(tmpColumns.ToArray());
        }
    }
}
