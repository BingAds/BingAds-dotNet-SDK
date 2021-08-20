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
    using System;
    using System.Text.RegularExpressions;
    using Bulk;
    using V13.Reporting;

    /// <summary>
    /// Reserved for internal use.
    /// </summary> 
    public class ReportHeader
    {
        private readonly Regex columnRegex = new Regex(@"^(([1-9]\d*.?\d*)|(0\.\d*[1-9]))%?$");

        private string reportName;

        private long recordCount = -1;

        private string[] reportColumns;

        private DateTime reportTimeStart;

        private DateTime reportTimeEnd;

        private string timeZone;

        private ReportAggregation reportAggregation;

        private DateTime lastCompletedAvailableDate;

        private bool potentialIncompleteData;

        public DateTime GetLastCompletedAvailableDate()
        {
            return this.lastCompletedAvailableDate;
        }

        public void SetLastCompletedAvailableDate(string strLastCompletedAvailableDate)
        {
            this.lastCompletedAvailableDate = strLastCompletedAvailableDate.ParseDateTime();
        }

        public string GetReportName()
        {
            return this.reportName;
        }

        public string[] GetReportColumns()
        {
            return this.reportColumns;
        }

        public DateTime GetReportTimeStart()
        {
            return this.reportTimeStart;
        }

        public void SetReportTimeStart(string strReportTime)
        {
            this.reportTimeStart = strReportTime.ParseDateTime();
        }

        public DateTime GetReportTimeEnd()
        {
            return this.reportTimeEnd;
        }

        public void SetReportTimeEnd(string strReportTime)
        {
            this.reportTimeEnd = strReportTime.ParseDateTime();
        }

        public string GetReportTimeZone()
        {
            return this.timeZone;
        }

        public void SetReportTimeZone(string strReportTimeZone)
        {
            this.timeZone = strReportTimeZone;
        }

        public long GetRecordCount()
        {
            if (this.recordCount == -1)
            {
                throw new CouldNotGetReportingMetadataException("Rows");
            }

            return this.recordCount;
        }

        public ReportAggregation GetReportAggregation()
        {
            return this.reportAggregation;
        }

        public void SetReportAggregation(string reportAggregationString)
        {
            Enum.TryParse(reportAggregationString, out this.reportAggregation);
        }

        public void SetReportName(string name)
        {
            this.reportName = name;
        }

        public bool SetReportColumns(string[] columns)
        {
            foreach (string s in columns)
            {
                // Validate column names in case "Exclude Column Header".
                // If there is a pure value, it should not be column names.
                if (this.columnRegex.IsMatch(s))
                {
                    return false;
                }
            }
            this.reportColumns = columns;
            return true;
        }

        public bool GetPotentialIncompleteData()
        {
            return this.potentialIncompleteData;
        }

        public void SetPotentialIncompleteData(string potentialIncompleteDataStr)
        {
            this.potentialIncompleteData = bool.Parse(potentialIncompleteDataStr);
        }

        public void SetReportFilter(string reportFilter)
        {
            // TODO: report filter format to be define.
        }

        public void SetRecordCount(string recordCountStr)
        {
            this.recordCount = long.Parse(recordCountStr);
        }
    }
}
