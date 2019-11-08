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
using System.Runtime.Serialization;

namespace Microsoft.BingAds.V13.Bulk
{
    /// <summary>
    /// This exception is thrown when attempting to write entities to a bulk file using <see cref="BulkFileWriter.WriteEntity(Microsoft.BingAds.V13.Bulk.Entities.BulkEntity)"/>.
    /// To resolve this exception you can first check the stack trace to see the error details, in case there is some action you can take to resolve the issue.
    /// For example you might have specified an invalid value for one of the upload entities.
    /// </summary>
    [Serializable]
    public class EntityWriteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the EntityWriteException class.
        /// </summary>
        public EntityWriteException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityWriteException class with the specified message and column values.
        /// </summary>
        /// <param name="message">The error message.</param>
        public EntityWriteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityReadException class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The details of the exception from the bulk service operation.</param>
        public EntityWriteException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="info">Reserved for internal use.</param>
        /// <param name="context">Reserved for internal use.</param>
        protected EntityWriteException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
