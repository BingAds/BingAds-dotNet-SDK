namespace Microsoft.BingAds.V13.CustomerManagement;

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

        if (jsonTypeInfo.Type == typeof(AccountInfo))
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
        else if (jsonTypeInfo.Type == typeof(AccountInfoWithCustomerData))
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
                     case "AccountMode":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         jsonPropertyInfo.IsRequired = false;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AccountTaxCertificate))
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
        else if (jsonTypeInfo.Type == typeof(AddAccountRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AddAccountResponse))
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
        else if (jsonTypeInfo.Type == typeof(AddClientLinksRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AddClientLinksResponse))
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
        else if (jsonTypeInfo.Type == typeof(AddPrepayAccountRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AddPrepayAccountResponse))
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
        else if (jsonTypeInfo.Type == typeof(Address))
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
        else if (jsonTypeInfo.Type == typeof(AdvertiserAccount))
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
                     case "TaxCertificate":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         jsonPropertyInfo.IsRequired = false;
                         break;
                     case "AccountMode":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         jsonPropertyInfo.IsRequired = false;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ApiFault))
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
            jsonPropertyInfo.Get = _ => "ApiFault";
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
        else if (jsonTypeInfo.Type == typeof(ClientLink))
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
        else if (jsonTypeInfo.Type == typeof(ContactInfo))
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
        else if (jsonTypeInfo.Type == typeof(Customer))
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
        else if (jsonTypeInfo.Type == typeof(CustomerInfo))
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
        else if (jsonTypeInfo.Type == typeof(CustomerRole))
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
        else if (jsonTypeInfo.Type == typeof(DateRange))
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
        else if (jsonTypeInfo.Type == typeof(DeleteAccountRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DeleteAccountResponse))
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
        else if (jsonTypeInfo.Type == typeof(DeleteCustomerRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DeleteCustomerResponse))
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
        else if (jsonTypeInfo.Type == typeof(DeleteUserRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DeleteUserResponse))
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
        else if (jsonTypeInfo.Type == typeof(DismissNotificationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DismissNotificationsResponse))
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
        else if (jsonTypeInfo.Type == typeof(FindAccountsOrCustomersInfoRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FindAccountsOrCustomersInfoResponse))
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
        else if (jsonTypeInfo.Type == typeof(FindAccountsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FindAccountsResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetAccessibleCustomerRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetAccessibleCustomerResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetAccountPilotFeaturesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetAccountPilotFeaturesResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetAccountRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetAccountResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetAccountsInfoRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetAccountsInfoResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetCurrentUserRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetCurrentUserResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetCustomerPilotFeaturesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetCustomerPilotFeaturesResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetCustomerRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetCustomerResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetCustomersInfoRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetCustomersInfoResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetLinkedAccountsAndCustomersInfoRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetLinkedAccountsAndCustomersInfoResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetNotificationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetNotificationsResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetPilotFeaturesCountriesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetPilotFeaturesCountriesResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetUserMFAStatusRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetUserMFAStatusResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetUserRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetUserResponse))
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
        else if (jsonTypeInfo.Type == typeof(GetUsersInfoRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GetUsersInfoResponse))
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
        else if (jsonTypeInfo.Type == typeof(KeyValueEntityOflongint))
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
        else if (jsonTypeInfo.Type == typeof(KeyValueEntityOfstringstring))
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
        else if (jsonTypeInfo.Type == typeof(MapAccountIdToExternalAccountIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MapAccountIdToExternalAccountIdsResponse))
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
        else if (jsonTypeInfo.Type == typeof(MapCustomerIdToExternalCustomerIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MapCustomerIdToExternalCustomerIdResponse))
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
        else if (jsonTypeInfo.Type == typeof(Notification))
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
        else if (jsonTypeInfo.Type == typeof(OrderBy))
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
        else if (jsonTypeInfo.Type == typeof(Paging))
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
        else if (jsonTypeInfo.Type == typeof(PersonName))
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
        else if (jsonTypeInfo.Type == typeof(Predicate))
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
        else if (jsonTypeInfo.Type == typeof(SearchAccountsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SearchAccountsResponse))
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
        else if (jsonTypeInfo.Type == typeof(SearchClientLinksRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SearchClientLinksResponse))
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
        else if (jsonTypeInfo.Type == typeof(SearchCustomersRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SearchCustomersResponse))
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
        else if (jsonTypeInfo.Type == typeof(SearchUserInvitationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SearchUserInvitationsResponse))
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
        else if (jsonTypeInfo.Type == typeof(SendUserInvitationRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SendUserInvitationResponse))
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
        else if (jsonTypeInfo.Type == typeof(SignupCustomerRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SignupCustomerResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpdateAccountRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpdateAccountResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpdateClientLinksRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpdateClientLinksResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpdateCustomerRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpdateCustomerResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpdatePrepayAccountRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpdatePrepayAccountResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpdateUserRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpdateUserResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpdateUserRolesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpdateUserRolesResponse))
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
        else if (jsonTypeInfo.Type == typeof(UpgradeCustomerToAgencyRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UpgradeCustomerToAgencyResponse))
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
        else if (jsonTypeInfo.Type == typeof(User))
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
                     case "AuthenticationToken":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         jsonPropertyInfo.IsRequired = false;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UserInfo))
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
        else if (jsonTypeInfo.Type == typeof(UserInvitation))
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
        else if (jsonTypeInfo.Type == typeof(ValidateAddressRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ValidateAddressResponse))
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