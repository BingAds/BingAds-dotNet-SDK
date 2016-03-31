//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
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

using Microsoft.BingAds.V10.Internal.Bulk.Operations;

namespace Microsoft.BingAds.V10.Bulk
{
    /// <summary>
    /// Represents a bulk download operation requested by a user. 
    /// You can use this class to poll for the download status, and then download the file when available.
    /// </summary>
    /// <example>The <see cref="BulkServiceManager.SubmitDownloadAsync"/> method returns an instance of this class. 
    /// If for any reason you do not want to wait for the file to be prepared for download, 
    /// for example if your application quits unexpectedly or you have other tasks to process, you can 
    /// use an instance of <see cref="BulkDownloadOperation"/> to download the file when it is available.</example>
    public class BulkDownloadOperation : BulkOperation<DownloadStatus>
    {        
        /// <summary>
        /// Initializes a new instance of this class with the specified <paramref name="requestId"/> and <see cref="AuthorizationData"/>.
        /// </summary>
        /// <param name="requestId">The identifier of a download request that has previously been submitted.</param>
        /// <param name="authorizationData">
        /// Represents a user who intends to access the corresponding customer and account. 
        /// </param>
        public BulkDownloadOperation(string requestId, AuthorizationData authorizationData)
            : this(requestId, authorizationData, null, null)
        {
             
        }
        /// <summary>
        /// Initializes a new instance of this class with the specified <paramref name="requestId"/>, <see cref="AuthorizationData"/> and <paramref name="apiEnvironment"/>.
        /// </summary>
        /// <param name="requestId">The identifier of a download request that has previously been submitted.</param>
        /// <param name="authorizationData">
        /// Represents a user who intends to access the corresponding customer and account.
        /// </param>
        /// <param name="apiEnvironment">Bing Ads API environment</param>
        public BulkDownloadOperation(string requestId, AuthorizationData authorizationData, ApiEnvironment? apiEnvironment)
            : this(requestId, authorizationData, null, apiEnvironment)
        {

        }

        internal BulkDownloadOperation(string requestId, AuthorizationData authorizationData, string trackingId)
            : base(requestId, authorizationData, new DownloadStatusProvider(requestId), trackingId)
        {

        }

        internal BulkDownloadOperation(string requestId, AuthorizationData authorizationData, string trackingId, ApiEnvironment? apiEnvironment)
            : base(requestId, authorizationData, new DownloadStatusProvider(requestId), trackingId, apiEnvironment)
        {

        }      
    }
}