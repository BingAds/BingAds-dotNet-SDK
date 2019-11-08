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

using Microsoft.BingAds.V13.Bulk.Entities;

namespace Microsoft.BingAds.V13.Bulk
{
    /// <summary>
    /// Describes the available parameters when submitting a file for upload, such as the path of the upload result file.
    /// </summary>
    public class FileUploadParameters
    {
        private readonly SubmitUploadParameters _submitUploadParameters = new SubmitUploadParameters();

        internal SubmitUploadParameters SubmitUploadParameters
        {
            get { return _submitUploadParameters; }
        }

        /// <summary>
        /// Determines whether the bulk service should return upload errors with the corresponding <see cref="BulkEntity"/> data.
        /// For possible values, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">ResponseMode Value Set</see>.
        /// </summary>
        /// <remarks>If not specified, this property is set to ErrorsAndResults.</remarks>
        public ResponseMode ResponseMode
        {
            get { return _submitUploadParameters.ResponseMode; }
            set { _submitUploadParameters.ResponseMode = value; }
        }

        /// <summary>
        /// The fully qualified local path of the upload file.
        /// </summary>
        public string UploadFilePath
        {
            get { return _submitUploadParameters.UploadFilePath; }
            set { _submitUploadParameters.UploadFilePath = value; }
        }

        /// <summary>
        /// Determines whether the upload file should be compressed before uploading. The default value is True.
        /// </summary>
        public bool CompressUploadFile
        {
            get { return _submitUploadParameters.CompressUploadFile; }
            set { _submitUploadParameters.CompressUploadFile = value; }
        }

        /// <summary>
        /// The directory where the file will be downloaded.
        /// </summary>
        public string ResultFileDirectory { get; set; }

        /// <summary>
        /// The name of the download result file.
        /// </summary>
        public string ResultFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the local result file should be overwritten if it already exists.
        /// </summary>
        public bool OverwriteResultFile { get; set; }

        internal bool RenameUploadFileToMatchRequestId
        {
            get { return _submitUploadParameters.RenameUploadFileToMatchRequestId; }
            set { _submitUploadParameters.RenameUploadFileToMatchRequestId = value; }
        }
    }
}
