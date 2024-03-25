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

        JsonPropertyInfo jsonPropertyInfo;

        if (jsonTypeInfo.Type == typeof(AdApiError))
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
        else if (jsonTypeInfo.Type == typeof(ApplicationFault))
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
        else if (jsonTypeInfo.Type == typeof(DownloadCampaignsByAccountIdsResponse))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
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
        else if (jsonTypeInfo.Type == typeof(DownloadCampaignsByCampaignIdsResponse))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
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
        else if (jsonTypeInfo.Type == typeof(GetBulkDownloadStatusResponse))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
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
        else if (jsonTypeInfo.Type == typeof(GetBulkUploadStatusResponse))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
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
        else if (jsonTypeInfo.Type == typeof(GetBulkUploadUrlResponse))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
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
        else if (jsonTypeInfo.Type == typeof(UploadEntityRecordsResponse))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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