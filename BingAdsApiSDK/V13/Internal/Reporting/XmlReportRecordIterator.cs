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

using System;
using System.Collections.Generic;

namespace Microsoft.BingAds.V13.Internal.Reporting
{
    using System.Collections;
    using System.IO;
    using System.Xml;
    using V13.Reporting;
    public class XmlReportRecordIterator : IEnumerator<IReportRecord>
    {
        private XmlReader _xmlReader;
        private readonly string _xmlFilePath;
        private bool disposed = false;

        public XmlReportRecordIterator(XmlReader reader, string filePath)
        {
            this._xmlReader = reader;
            this._xmlFilePath = filePath;
        }

        public bool MoveNext()
        {
            while (this._xmlReader.Read() && this._xmlReader.Name != "Row") ;
            if (this._xmlReader.EOF)
            {
                return false;
            }

            Dictionary<string, string> rowValues = new Dictionary<string, string>();
            while (this._xmlReader.Read() && this._xmlReader.Name != "Row")
            {
                var rowValue = this._xmlReader.GetAttribute("value");
                var rowName = this._xmlReader.Name;
                if (rowValue != null)
                {
                    rowValues.Add(rowName, rowValue);
                }
            }

            Current = new XmlReportRecord(rowValues);
            return true;
        }

        public void Reset()
        {
            Dispose();
            this._xmlReader = XmlReader.Create(this._xmlFilePath);
        }

        public IReportRecord Current { get; private set; }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }

                _xmlReader?.Close();
                _xmlReader = null;

                // Note disposing has been done.
                disposed = true;

            }
        }
    }
}
