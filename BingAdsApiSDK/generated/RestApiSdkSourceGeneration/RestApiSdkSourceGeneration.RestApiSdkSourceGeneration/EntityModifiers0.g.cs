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

namespace Microsoft.BingAds.V13.Bulk;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

public static class EntityModifiers
{
    private static void AddPrivateField(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
    {
        var field = containingType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(field.FieldType, jsonName);
        jsonPropertyInfo.Get = field.GetValue;
        jsonPropertyInfo.Set = field.SetValue;
        jsonTypeInfo.Properties.Add(jsonPropertyInfo);
    }

    public static void CustomizeEntities(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
            return;

        JsonPropertyInfo jsonPropertyInfo;

        if (jsonTypeInfo.Type == typeof(ApplicationFault))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "ApplicationFault";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ApiFaultDetail))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "ApiFaultDetail";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdApiFaultDetail))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "AdApiFaultDetail";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdApiError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BatchError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BatchError";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(OperationError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EditorialError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EditorialError";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CampaignScope))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DownloadCampaignsByAccountIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DownloadCampaignsByCampaignIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetBulkDownloadStatusRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetBulkUploadUrlRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetBulkUploadStatusRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UploadEntityRecordsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
    }
}