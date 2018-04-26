//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.12
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
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Microsoft.BingAds.Internal
{
    internal class MessagePair
    {
        public string Request
        {
            get;
            set;
        }

        public string Reply
        {
            get;
            set;
        }
    }
    internal class SimpleMessageStore
    {
        private List<MessagePair> messageList = new List<MessagePair>(20);

        static SimpleMessageStore()
        {
            Store = new SimpleMessageStore();
        }

        public static SimpleMessageStore Store
        {
            get;
            private set;
        }
        
        public string CheckTestResultForTestInitializeOrCleanup()
        {
            var message = new StringBuilder(1024);
            try
            {

                var errorMessages = new StringBuilder(1024);
                foreach (var msg in messageList)
                {
                    errorMessages.AppendLine("<WCFCall>");

                    errorMessages.AppendLine("<Request>");
                    errorMessages.AppendLine(msg.Request);
                    errorMessages.AppendLine("</Request>");

                    errorMessages.AppendLine("<Reply>");
                    errorMessages.AppendLine(msg.Reply);
                    errorMessages.AppendLine("</Reply>");

                    errorMessages.AppendLine("</WCFCall>");
                }

                message.AppendLine();
                message.AppendLine("===============================");
                if (errorMessages.Length > 0)
                {
                    message.AppendLine("MT error messages for this test");
                }
                else
                {
                    message.AppendLine("No MT error messages were found for this test");
                }
                message.AppendLine("===============================");
                message.AppendLine(errorMessages.ToString());
            }
            catch (Exception e)
            {
                message.AppendFormat("Exception after test ran {0}", e);
            }
            return message.ToString();
        }

        public void AfterReceiveReply(Message reply)
        {
            messageList.Last().Reply = reply.ToString();
        }

        public void AfterReceiveReply(string bulkFile, bool download = true)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(bulkFile);
            string line;
            StringBuilder sb = new StringBuilder(1024);
            while ((line = file.ReadLine()) != null)
            {
                sb.AppendLine(line);
            }

            file.Close();
            MessagePair pair = new MessagePair()
            {
                Request = download ? "BulkDownloadResult" : "BulkUploadResult",
                Reply = sb.ToString()
            };
            messageList.Add(pair);
        }
        

        public void BeforeSendRequest(Message request)
        {
            if (!SkipBulkStatusRequest(request))
            {
                messageList.Add(new MessagePair()
                {
                    Request = request.ToString()
                });
            }
        }

        private bool SkipBulkStatusRequest(Message request)
        {
            return messageList.Count > 0 &&  messageList.Last().Request == request.ToString();
        }
    }
}