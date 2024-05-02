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

namespace Microsoft.BingAds.Internal;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Microsoft.BingAds.V13.Bulk;

public static partial class RestApiGeneration
{
    public static class Microsoft_BingAds_V13_Bulk_EntityModifiers
    {
        private static void AddPrivateField(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
        {
            var field = containingType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(field.FieldType, jsonName);
            jsonPropertyInfo.Get = field.GetValue;
            jsonPropertyInfo.Set = field.SetValue;
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }

        private static void AddPrivateProperty(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
        {
            var property = containingType.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(property.PropertyType, jsonName);
            jsonPropertyInfo.Get = property.GetValue;
            jsonPropertyInfo.Set = property.SetValue;
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }

        public static void CustomizeEntities(JsonTypeInfo jsonTypeInfo)
        {
            if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
                return;

            if (CustomizeActions.TryGetValue(jsonTypeInfo.Type, out var customize))
            {
                customize(jsonTypeInfo);
            }
        }

        private static Dictionary<Type, Action<JsonTypeInfo>> CustomizeActions = new Dictionary<Type, Action<JsonTypeInfo>>
        {
            { typeof(AdApiError), static t => CustomizeAdApiError(t) },
            { typeof(AdApiFaultDetail), static t => CustomizeAdApiFaultDetail(t) },
            { typeof(ApiFaultDetail), static t => CustomizeApiFaultDetail(t) },
            { typeof(ApplicationFault), static t => CustomizeApplicationFault(t) },
            { typeof(BatchError), static t => CustomizeBatchError(t) },
            { typeof(CampaignScope), static t => CustomizeCampaignScope(t) },
            { typeof(DownloadCampaignsByAccountIdsRequest), static t => CustomizeDownloadCampaignsByAccountIdsRequest(t) },
            { typeof(DownloadCampaignsByAccountIdsResponse), static t => CustomizeDownloadCampaignsByAccountIdsResponse(t) },
            { typeof(DownloadCampaignsByCampaignIdsRequest), static t => CustomizeDownloadCampaignsByCampaignIdsRequest(t) },
            { typeof(DownloadCampaignsByCampaignIdsResponse), static t => CustomizeDownloadCampaignsByCampaignIdsResponse(t) },
            { typeof(EditorialError), static t => CustomizeEditorialError(t) },
            { typeof(GetBulkDownloadStatusRequest), static t => CustomizeGetBulkDownloadStatusRequest(t) },
            { typeof(GetBulkDownloadStatusResponse), static t => CustomizeGetBulkDownloadStatusResponse(t) },
            { typeof(GetBulkUploadStatusRequest), static t => CustomizeGetBulkUploadStatusRequest(t) },
            { typeof(GetBulkUploadStatusResponse), static t => CustomizeGetBulkUploadStatusResponse(t) },
            { typeof(GetBulkUploadUrlRequest), static t => CustomizeGetBulkUploadUrlRequest(t) },
            { typeof(GetBulkUploadUrlResponse), static t => CustomizeGetBulkUploadUrlResponse(t) },
            { typeof(OperationError), static t => CustomizeOperationError(t) },
            { typeof(UploadEntityRecordsRequest), static t => CustomizeUploadEntityRecordsRequest(t) },
            { typeof(UploadEntityRecordsResponse), static t => CustomizeUploadEntityRecordsResponse(t) }
        };

        private static void CustomizeAdApiError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeApplicationFault(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ApplicationFault";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeBatchError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BatchError";
                        break;
                }
            }
        }

        private static void CustomizeCampaignScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDownloadCampaignsByAccountIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeDownloadCampaignsByAccountIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDownloadCampaignsByCampaignIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeDownloadCampaignsByCampaignIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeEditorialError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EditorialError";
                        break;
                }
            }
        }

        private static void CustomizeGetBulkDownloadStatusRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetBulkDownloadStatusResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBulkUploadStatusRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetBulkUploadStatusResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBulkUploadUrlRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetBulkUploadUrlResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeOperationError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUploadEntityRecordsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUploadEntityRecordsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
    }
}