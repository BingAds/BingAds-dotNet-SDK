namespace Microsoft.BingAds.V13.Reporting;

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

        if (jsonTypeInfo.Type == typeof(AccountPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AccountPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AccountPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AccountReportScope))
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
        else if (jsonTypeInfo.Type == typeof(AccountThroughAdGroupReportScope))
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
        else if (jsonTypeInfo.Type == typeof(AccountThroughAssetGroupReportScope))
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
        else if (jsonTypeInfo.Type == typeof(AccountThroughCampaignReportScope))
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
        else if (jsonTypeInfo.Type == typeof(AdDynamicTextPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AdDynamicTextPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AdDynamicTextPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionByAdReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AdExtensionByAdReportRequest))
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
            jsonPropertyInfo.Get = _ => "AdExtensionByAdReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionByKeywordReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AdExtensionByKeywordReportRequest))
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
            jsonPropertyInfo.Get = _ => "AdExtensionByKeywordReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionDetailReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AdExtensionDetailReportRequest))
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
            jsonPropertyInfo.Get = _ => "AdExtensionDetailReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdGroupPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AdGroupPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AdGroupPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdGroupReportScope))
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
        else if (jsonTypeInfo.Type == typeof(AdPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AdPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AdPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AgeGenderAudienceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AgeGenderAudienceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AgeGenderAudienceReportRequest";
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
        else if (jsonTypeInfo.Type == typeof(AssetGroupPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AssetGroupPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AssetGroupPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AssetGroupReportScope))
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
        else if (jsonTypeInfo.Type == typeof(AudiencePerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(AudiencePerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "AudiencePerformanceReportRequest";
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
        }
        else if (jsonTypeInfo.Type == typeof(BudgetSummaryReportRequest))
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
            jsonPropertyInfo.Get = _ => "BudgetSummaryReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(CallDetailReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(CallDetailReportRequest))
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
            jsonPropertyInfo.Get = _ => "CallDetailReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(CampaignPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(CampaignPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "CampaignPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(CampaignReportScope))
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
        else if (jsonTypeInfo.Type == typeof(ConversionPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ConversionPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ConversionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(Date))
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
        else if (jsonTypeInfo.Type == typeof(DestinationUrlPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(DestinationUrlPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "DestinationUrlPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(DSAAutoTargetPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(DSAAutoTargetPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "DSAAutoTargetPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(DSACategoryPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(DSACategoryPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "DSACategoryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(DSASearchQueryPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(DSASearchQueryPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "DSASearchQueryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(GeographicPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(GeographicPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "GeographicPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(GoalsAndFunnelsReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(GoalsAndFunnelsReportRequest))
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
            jsonPropertyInfo.Get = _ => "GoalsAndFunnelsReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(HotelDimensionPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(HotelDimensionPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "HotelDimensionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(HotelGroupPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(HotelGroupPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "HotelGroupPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(KeywordPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(KeywordPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "KeywordPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(KeywordPerformanceReportSort))
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
        else if (jsonTypeInfo.Type == typeof(NegativeKeywordConflictReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(NegativeKeywordConflictReportRequest))
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
            jsonPropertyInfo.Get = _ => "NegativeKeywordConflictReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
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
        else if (jsonTypeInfo.Type == typeof(PollGenerateReportRequest))
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
        else if (jsonTypeInfo.Type == typeof(PollGenerateReportResponse))
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
        else if (jsonTypeInfo.Type == typeof(ProductDimensionPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ProductDimensionPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProductDimensionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ProductMatchCountReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProductMatchCountReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ProductNegativeKeywordConflictReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ProductNegativeKeywordConflictReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProductNegativeKeywordConflictReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ProductPartitionPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ProductPartitionPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProductPartitionPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ProductPartitionUnitPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ProductPartitionUnitPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProductPartitionUnitPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ProductSearchQueryPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ProductSearchQueryPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProductSearchQueryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ProfessionalDemographicsAudienceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ProfessionalDemographicsAudienceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ProfessionalDemographicsAudienceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(PublisherUsagePerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(PublisherUsagePerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "PublisherUsagePerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ReportRequest))
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
            jsonPropertyInfo.Get = _ => "ReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ReportRequestStatus))
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
        else if (jsonTypeInfo.Type == typeof(ReportTime))
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
        else if (jsonTypeInfo.Type == typeof(SearchCampaignChangeHistoryReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(SearchCampaignChangeHistoryReportRequest))
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
            jsonPropertyInfo.Get = _ => "SearchCampaignChangeHistoryReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(SearchQueryPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(SearchQueryPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "SearchQueryPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ShareOfVoiceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(ShareOfVoiceReportRequest))
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
            jsonPropertyInfo.Get = _ => "ShareOfVoiceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(SubmitGenerateReportRequest))
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
        else if (jsonTypeInfo.Type == typeof(SubmitGenerateReportResponse))
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
        else if (jsonTypeInfo.Type == typeof(UserLocationPerformanceReportFilter))
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
        else if (jsonTypeInfo.Type == typeof(UserLocationPerformanceReportRequest))
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
            jsonPropertyInfo.Get = _ => "UserLocationPerformanceReportRequest";
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
    }
}