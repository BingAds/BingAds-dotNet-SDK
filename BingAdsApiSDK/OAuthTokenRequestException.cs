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

using System.Runtime.Serialization;

namespace Microsoft.BingAds
{
    /// <summary>
    /// This exception is thrown if an error was returned from the Microsft Account authorization server.
    /// To resolve this exception you can first check the stack trace to see the error details, in case there is some action you can take to resolve the issue.
    /// For example you might have specified an invalid client ID.
    /// </summary>
    [Serializable]
    public class OAuthTokenRequestException : Exception
    {
        /// <summary>
        /// Represents details of an error returned from the Microsft Account authorization server.
        /// </summary>
        public OAuthErrorDetails Details { get; private set; }

        /// <summary>
        /// Initializes a new instance of the OAuthTokenRequestException with the specified error message and OAuth error details.
        /// </summary>
        /// <param name="message">The error message returned by the client library.</param>
        /// <param name="details">The details of an error returned from the Microsft Account authorization server.</param>
        public OAuthTokenRequestException(string message, OAuthErrorDetails details)
            : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// Initializes a new instance of the OAuthTokenRequestException with the specified error message, OAuth error details, and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="details">The details of an authentication error returned from the Microsft Account authorization server.</param>
        /// <param name="inner">The inner exception.</param>
        public OAuthTokenRequestException(string message, OAuthErrorDetails details, Exception inner)
            : base(message, inner)
        {
            Details = details;
        }

        /// <summary>
        /// Returns the message.
        /// </summary>
        public override string Message
        {
            get { return base.Message + " Details: " + Details.Error + " - " + Details.Description; }
        }
    }
}
