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


namespace Microsoft.BingAds.V13.Internal
{
    public class CsvLight : IDisposable
    {
        /// <summary>
        /// Input buffer size.
        /// </summary>
        private const int InputBufferSize = 16 * 1024;

        /// <summary>
        /// Carriage return character.
        /// </summary>
        private const char CR = '\r';

        /// <summary>
        /// Line feed character.
        /// </summary>
        private const char LF = '\n';

        /// <summary>
        /// Double quote character.
        /// </summary>
        private const char QUOTE = '\"';

        /// <summary>
        /// Delimeter to sperate fields.
        /// </summary>
        private char delimeter;

        /// <summary>
        /// Whether to trim spaces.
        /// </summary>
        private bool trimSpaces;

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Stream reader variable.
        /// </summary>
        private StreamReader sr;

        /// <summary>
        /// EOF character.
        /// </summary>
        private char charEOF;

        /// <summary>
        /// Char data variable.
        /// </summary>
        private char ch;

        /// <summary>
        /// Read parts list.
        /// </summary>
        private List<string> parts = new List<string>();

        /// <summary>
        /// Input buffering (in favor of streamed char processing).
        /// </summary>
        private char[] inputBuffer;

        /// <summary>
        /// Input Index.
        /// </summary>
        private int inputIndex;

        /// <summary>
        /// Input Length.
        /// Keep in variable for faster reference.
        /// </summary>
        private int inputLen;

        /// <summary>
        /// Partial Field.
        /// When field we are building is split across buffers.
        /// </summary>
        private string partialField;  // when field we are building is split across buffers

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLight"/> class.
        /// </summary>
        /// <param name="reader">Input Stream Reader.</param>
        public CsvLight(StreamReader reader)
        {
            this.Initialize(reader, ',', true, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLight"/> class.
        /// </summary>
        /// <param name="reader">Input Stream Reader.</param>
        /// <param name="delimiter">Fields Delimiter.</param>
        public CsvLight(StreamReader reader, char delimiter)
        {
            this.Initialize(reader, delimiter, true, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLight"/> class.
        /// </summary>
        /// <param name="reader">Input Stream Reader.</param>
        /// <param name="delimiter">Fields Delimiter.</param>
        /// <param name="trim">Whether to trim spaces.</param>
        /// <param name="headers">Whether has headers.</param>
        public CsvLight(StreamReader reader, char delimiter, bool trim, bool headers)
        {
            this.Initialize(reader, delimiter, trim, headers);
        }

        /// <summary>
        /// Gets or sets a value indicating whether has headers.
        /// </summary>
        /// <value>Whether has headers.</value>
        public bool HasHeaders { get; set; }

        /// <summary>
        /// Gets or sets headers.
        /// </summary>
        /// <value>CSV file Headers.</value>
        public List<string> Headers { get; set; }

        /// <summary>
        /// Gets or sets current row data in columns.
        /// Note we are using here an array instead of a list (in build row we use list and then convert).
        /// Reason is that array is 7% faster in read access and list takes 11% more memory (very small diff but just in case we'll keep this)
        /// </summary>
        /// <value>Current row data in columns.</value>
        public string[] Columns { get; set; }

        /// <summary>
        /// Returns filed headers.
        /// </summary>
        /// <returns>Field headers.</returns>
        public string[] GetFieldHeaders()
        {
            // Note perf of ToArray: converting 1 million lists with 20 columns takes 140 ms in total.
            return this.Headers.ToArray();
        }

        /// <summary>
        /// Read in next line.
        /// </summary>
        /// <returns>Return true if read in some data, otherwise false.</returns>
        public bool ReadNextRecord()
        {
            this.Columns = this.BuildLine().ToArray();

            if (this.Columns.Length == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected char Delimeter => delimeter;

        protected StreamReader Stream
        {
            get { return sr; }
        }

        /// <summary>
        /// Initializes csv class.
        /// </summary>
        /// <param name="reader">Input Stream Reader.</param>
        /// <param name="delimiter">Fields Delimiter.</param>
        /// <param name="trim">Whether to trim spaces.</param>
        /// <param name="headers">Whether has headers.</param>
        protected void Initialize(StreamReader reader, char delimiter, bool trim, bool headers)
        {
            this.sr = reader;

            this.delimeter = delimiter;
            this.trimSpaces = trim;            // this is the CSV standard
            this.HasHeaders = headers;

            unchecked
            {
                this.charEOF = (char)-1;
            }

            this.inputBuffer = new char[InputBufferSize];
            this.inputLen = 0;
            this.Headers = new List<string>();

            // always read header first
            this.SetHeader();
        }

        /// <summary>
        /// Pre-fill buffer.
        /// </summary>
        /// <param name="start">Starting position.</param>
        private void FillBuffer(int start)
        {
            if (start > -1)
            {
                //---- a field we are processing is split across input buffers, so store what ----
                //---- we have seen so far in "partialField" ----
                this.partialField += new string(this.inputBuffer, start, this.inputLen - start);
            }

            if (this.sr.EndOfStream)
            {
                this.inputBuffer[0] = this.charEOF;
                this.inputLen = 1;
            }
            else
            {
                this.inputLen = this.sr.Read(this.inputBuffer, 0, InputBufferSize);
            }

            this.inputIndex = 0;
        }

        /// <summary>
        /// The next char is already stored in "ch".  This is the critical loop 
        /// for perf, so inlining for ReadNextChar() occurs often (7 times).
        /// </summary>
        /// <returns>The list of columns.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private List<string> BuildLine()
        {
            this.parts.Clear();
            int start = 0;
            int end = 0;
            bool embeddedQuotes = false;

            while (this.ch != this.charEOF)
            {
                //---- process next field ----
                this.partialField = string.Empty;

                start = this.inputIndex - 1;

                // quoted field has special handling.
                if (this.ch == QUOTE)
                {
                    //---- ReadNextChar() ----
                    if (this.inputIndex == this.inputLen)
                    {
                        // do not copy quote in partial buffer
                        this.FillBuffer(-1);
                        start = 0;
                    }

                    this.ch = this.inputBuffer[this.inputIndex++];
                    start = this.inputIndex - 1;             // since we skipped over the leading QUOTE

                    while (true)
                    {
                        if (this.ch == this.charEOF)
                        {
                            // ignore error
                            // Add any missing columns except if parts > -
                            if (this.parts.Count > 0)
                            {
                                while (this.Headers.Count > this.parts.Count)
                                {
                                    this.parts.Add(string.Empty);
                                }
                            }

                            return this.parts;

                            // throw new Exception("EOF encountered while looking for end of quoted field");
                        }

                        if (this.ch == QUOTE)
                        {
                            // ---- look at following char to see if this is a QUOTE PAIR ----

                            // ---- ReadNextChar() ----
                            if (this.inputIndex == this.inputLen)
                            {
                                this.FillBuffer(start);
                                start = 0;
                            }

                            this.ch = this.inputBuffer[this.inputIndex++];

                            if (this.ch != QUOTE)
                            {
                                // need to remove quote from last buffer
                                if (this.inputIndex == 1)
                                {
                                    // ---- remove QUOTE from end of partialField ----
                                    this.partialField = this.partialField.Substring(0, this.partialField.Length - 1);
                                    end = 0;
                                }
                                else
                                {
                                    end = this.inputIndex - 2;
                                }

                                break;
                            }
                            else
                            {
                                embeddedQuotes = true;
                            }

                            // ---- QUOTE PAIR just falls thru as a single QUOTE ----
                        }

                        // ---- ReadNextChar() ----
                        if (this.inputIndex == this.inputLen)
                        {
                            this.FillBuffer(start);
                            start = 0;
                        }

                        this.ch = this.inputBuffer[this.inputIndex++];
                    }
                }
                else
                {
                    start = this.inputIndex - 1;
                    while ((this.ch != CR) && (this.ch != LF) && (this.ch != this.charEOF) && (this.ch != this.delimeter))
                    {
                        // ---- ReadNextChar() ----
                        if (this.inputIndex == this.inputLen)
                        {
                            this.FillBuffer(start);
                            start = 0;
                        }

                        this.ch = this.inputBuffer[this.inputIndex++];
                    }

                    end = this.inputIndex - 1;
                }

                //---- finish field ----
                string part = new string(this.inputBuffer, start, end - start);
                if (string.IsNullOrEmpty(this.partialField) == false)
                {
                    part = this.partialField + part;
                }

                if (embeddedQuotes)
                {
                    part = part.Replace("\"\"", "\"");
                }

                if (this.trimSpaces)
                {
                    this.parts.Add(part.Trim());
                }
                else
                {
                    this.parts.Add(part);
                }

                //---- skip over CR/LF if present ----
                if (this.ch == CR)
                {
                    //---- ReadNextChar() ----
                    if (this.inputIndex == this.inputLen)
                    {
                        this.FillBuffer(-1);
                    }

                    this.ch = this.inputBuffer[this.inputIndex++];

                    if (this.ch != LF)
                    {
                        break;
                    }
                }

                if (this.ch == LF)
                {
                    //---- ReadNextChar() ----
                    if (this.inputIndex == this.inputLen)
                    {
                        this.FillBuffer(-1);
                    }

                    this.ch = this.inputBuffer[this.inputIndex++];

                    break;
                }

                if (this.ch == this.delimeter)
                {
                    //---- ReadNextChar() ----
                    if (this.inputIndex == this.inputLen)
                    {
                        this.FillBuffer(-1);
                    }

                    this.ch = this.inputBuffer[this.inputIndex++];
                }
            }

            // Add any missing columns except if EOF and parts = 0
            if (this.ch != this.charEOF || this.parts.Count > 0)
            {
                while (this.HasHeaders &&  this.Headers.Count > this.parts.Count)
                {
                    this.parts.Add(string.Empty);
                }
            }

            return this.parts;
        }

        /// <summary>
        /// Read next character from buffer.
        /// </summary>
        private void ReadNextChar()
        {
            //---- NEXT CHAR ----
            if (this.inputIndex == this.inputLen)
            {
                this.FillBuffer(-1);
            }

            this.ch = this.inputBuffer[this.inputIndex++];
        }

        /// <summary>
        /// Set headers list.
        /// </summary>
        private void SetHeader()
        {
            this.ReadNextChar();

            if (this.HasHeaders)
            {
                this.Headers = new List<string>();
                if (this.ReadNextRecord())
                {
                    this.Headers = new List<string>(this.Columns);
                }
            }
            else
            {
                this.Headers = null;
            }
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">Flag indicating disposing in process.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    // component.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
