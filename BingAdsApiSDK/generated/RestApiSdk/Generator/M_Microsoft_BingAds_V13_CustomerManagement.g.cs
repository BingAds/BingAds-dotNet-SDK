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
using Microsoft.BingAds.V13.CustomerManagement;

public static partial class RestApiGeneration
{
    public static class Microsoft_BingAds_V13_CustomerManagement_EntityModifiers
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
            { typeof(AccountInfo), static t => CustomizeAccountInfo(t) },
            { typeof(AccountInfoWithCustomerData), static t => CustomizeAccountInfoWithCustomerData(t) },
            { typeof(AccountTaxCertificate), static t => CustomizeAccountTaxCertificate(t) },
            { typeof(AdApiError), static t => CustomizeAdApiError(t) },
            { typeof(AdApiFaultDetail), static t => CustomizeAdApiFaultDetail(t) },
            { typeof(AddAccountRequest), static t => CustomizeAddAccountRequest(t) },
            { typeof(AddAccountResponse), static t => CustomizeAddAccountResponse(t) },
            { typeof(AddClientLinksRequest), static t => CustomizeAddClientLinksRequest(t) },
            { typeof(AddClientLinksResponse), static t => CustomizeAddClientLinksResponse(t) },
            { typeof(AddPrepayAccountRequest), static t => CustomizeAddPrepayAccountRequest(t) },
            { typeof(AddPrepayAccountResponse), static t => CustomizeAddPrepayAccountResponse(t) },
            { typeof(Address), static t => CustomizeAddress(t) },
            { typeof(AdvertiserAccount), static t => CustomizeAdvertiserAccount(t) },
            { typeof(ApiFault), static t => CustomizeApiFault(t) },
            { typeof(ApplicationFault), static t => CustomizeApplicationFault(t) },
            { typeof(ClientLink), static t => CustomizeClientLink(t) },
            { typeof(ContactInfo), static t => CustomizeContactInfo(t) },
            { typeof(Customer), static t => CustomizeCustomer(t) },
            { typeof(CustomerInfo), static t => CustomizeCustomerInfo(t) },
            { typeof(CustomerRole), static t => CustomizeCustomerRole(t) },
            { typeof(DateRange), static t => CustomizeDateRange(t) },
            { typeof(DeleteAccountRequest), static t => CustomizeDeleteAccountRequest(t) },
            { typeof(DeleteAccountResponse), static t => CustomizeDeleteAccountResponse(t) },
            { typeof(DeleteCustomerRequest), static t => CustomizeDeleteCustomerRequest(t) },
            { typeof(DeleteCustomerResponse), static t => CustomizeDeleteCustomerResponse(t) },
            { typeof(DeleteUserRequest), static t => CustomizeDeleteUserRequest(t) },
            { typeof(DeleteUserResponse), static t => CustomizeDeleteUserResponse(t) },
            { typeof(DismissNotificationsRequest), static t => CustomizeDismissNotificationsRequest(t) },
            { typeof(DismissNotificationsResponse), static t => CustomizeDismissNotificationsResponse(t) },
            { typeof(FindAccountsOrCustomersInfoRequest), static t => CustomizeFindAccountsOrCustomersInfoRequest(t) },
            { typeof(FindAccountsOrCustomersInfoResponse), static t => CustomizeFindAccountsOrCustomersInfoResponse(t) },
            { typeof(FindAccountsRequest), static t => CustomizeFindAccountsRequest(t) },
            { typeof(FindAccountsResponse), static t => CustomizeFindAccountsResponse(t) },
            { typeof(GetAccessibleCustomerRequest), static t => CustomizeGetAccessibleCustomerRequest(t) },
            { typeof(GetAccessibleCustomerResponse), static t => CustomizeGetAccessibleCustomerResponse(t) },
            { typeof(GetAccountPilotFeaturesRequest), static t => CustomizeGetAccountPilotFeaturesRequest(t) },
            { typeof(GetAccountPilotFeaturesResponse), static t => CustomizeGetAccountPilotFeaturesResponse(t) },
            { typeof(GetAccountRequest), static t => CustomizeGetAccountRequest(t) },
            { typeof(GetAccountResponse), static t => CustomizeGetAccountResponse(t) },
            { typeof(GetAccountsInfoRequest), static t => CustomizeGetAccountsInfoRequest(t) },
            { typeof(GetAccountsInfoResponse), static t => CustomizeGetAccountsInfoResponse(t) },
            { typeof(GetCurrentUserRequest), static t => CustomizeGetCurrentUserRequest(t) },
            { typeof(GetCurrentUserResponse), static t => CustomizeGetCurrentUserResponse(t) },
            { typeof(GetCustomerPilotFeaturesRequest), static t => CustomizeGetCustomerPilotFeaturesRequest(t) },
            { typeof(GetCustomerPilotFeaturesResponse), static t => CustomizeGetCustomerPilotFeaturesResponse(t) },
            { typeof(GetCustomerRequest), static t => CustomizeGetCustomerRequest(t) },
            { typeof(GetCustomerResponse), static t => CustomizeGetCustomerResponse(t) },
            { typeof(GetCustomersInfoRequest), static t => CustomizeGetCustomersInfoRequest(t) },
            { typeof(GetCustomersInfoResponse), static t => CustomizeGetCustomersInfoResponse(t) },
            { typeof(GetLinkedAccountsAndCustomersInfoRequest), static t => CustomizeGetLinkedAccountsAndCustomersInfoRequest(t) },
            { typeof(GetLinkedAccountsAndCustomersInfoResponse), static t => CustomizeGetLinkedAccountsAndCustomersInfoResponse(t) },
            { typeof(GetNotificationsRequest), static t => CustomizeGetNotificationsRequest(t) },
            { typeof(GetNotificationsResponse), static t => CustomizeGetNotificationsResponse(t) },
            { typeof(GetPilotFeaturesCountriesRequest), static t => CustomizeGetPilotFeaturesCountriesRequest(t) },
            { typeof(GetPilotFeaturesCountriesResponse), static t => CustomizeGetPilotFeaturesCountriesResponse(t) },
            { typeof(GetUserMFAStatusRequest), static t => CustomizeGetUserMFAStatusRequest(t) },
            { typeof(GetUserMFAStatusResponse), static t => CustomizeGetUserMFAStatusResponse(t) },
            { typeof(GetUserRequest), static t => CustomizeGetUserRequest(t) },
            { typeof(GetUserResponse), static t => CustomizeGetUserResponse(t) },
            { typeof(GetUsersInfoRequest), static t => CustomizeGetUsersInfoRequest(t) },
            { typeof(GetUsersInfoResponse), static t => CustomizeGetUsersInfoResponse(t) },
            { typeof(KeyValueEntityOflongint), static t => CustomizeKeyValueEntityOflongint(t) },
            { typeof(KeyValueEntityOfstringstring), static t => CustomizeKeyValueEntityOfstringstring(t) },
            { typeof(MapAccountIdToExternalAccountIdsRequest), static t => CustomizeMapAccountIdToExternalAccountIdsRequest(t) },
            { typeof(MapAccountIdToExternalAccountIdsResponse), static t => CustomizeMapAccountIdToExternalAccountIdsResponse(t) },
            { typeof(MapCustomerIdToExternalCustomerIdRequest), static t => CustomizeMapCustomerIdToExternalCustomerIdRequest(t) },
            { typeof(MapCustomerIdToExternalCustomerIdResponse), static t => CustomizeMapCustomerIdToExternalCustomerIdResponse(t) },
            { typeof(Notification), static t => CustomizeNotification(t) },
            { typeof(OperationError), static t => CustomizeOperationError(t) },
            { typeof(OrderBy), static t => CustomizeOrderBy(t) },
            { typeof(Paging), static t => CustomizePaging(t) },
            { typeof(PersonName), static t => CustomizePersonName(t) },
            { typeof(Predicate), static t => CustomizePredicate(t) },
            { typeof(SearchAccountsRequest), static t => CustomizeSearchAccountsRequest(t) },
            { typeof(SearchAccountsResponse), static t => CustomizeSearchAccountsResponse(t) },
            { typeof(SearchClientLinksRequest), static t => CustomizeSearchClientLinksRequest(t) },
            { typeof(SearchClientLinksResponse), static t => CustomizeSearchClientLinksResponse(t) },
            { typeof(SearchCustomersRequest), static t => CustomizeSearchCustomersRequest(t) },
            { typeof(SearchCustomersResponse), static t => CustomizeSearchCustomersResponse(t) },
            { typeof(SearchUserInvitationsRequest), static t => CustomizeSearchUserInvitationsRequest(t) },
            { typeof(SearchUserInvitationsResponse), static t => CustomizeSearchUserInvitationsResponse(t) },
            { typeof(SendUserInvitationRequest), static t => CustomizeSendUserInvitationRequest(t) },
            { typeof(SendUserInvitationResponse), static t => CustomizeSendUserInvitationResponse(t) },
            { typeof(SignupCustomerRequest), static t => CustomizeSignupCustomerRequest(t) },
            { typeof(SignupCustomerResponse), static t => CustomizeSignupCustomerResponse(t) },
            { typeof(UpdateAccountRequest), static t => CustomizeUpdateAccountRequest(t) },
            { typeof(UpdateAccountResponse), static t => CustomizeUpdateAccountResponse(t) },
            { typeof(UpdateClientLinksRequest), static t => CustomizeUpdateClientLinksRequest(t) },
            { typeof(UpdateClientLinksResponse), static t => CustomizeUpdateClientLinksResponse(t) },
            { typeof(UpdateCustomerRequest), static t => CustomizeUpdateCustomerRequest(t) },
            { typeof(UpdateCustomerResponse), static t => CustomizeUpdateCustomerResponse(t) },
            { typeof(UpdatePrepayAccountRequest), static t => CustomizeUpdatePrepayAccountRequest(t) },
            { typeof(UpdatePrepayAccountResponse), static t => CustomizeUpdatePrepayAccountResponse(t) },
            { typeof(UpdateUserRequest), static t => CustomizeUpdateUserRequest(t) },
            { typeof(UpdateUserResponse), static t => CustomizeUpdateUserResponse(t) },
            { typeof(UpdateUserRolesRequest), static t => CustomizeUpdateUserRolesRequest(t) },
            { typeof(UpdateUserRolesResponse), static t => CustomizeUpdateUserRolesResponse(t) },
            { typeof(UpgradeCustomerToAgencyRequest), static t => CustomizeUpgradeCustomerToAgencyRequest(t) },
            { typeof(UpgradeCustomerToAgencyResponse), static t => CustomizeUpgradeCustomerToAgencyResponse(t) },
            { typeof(User), static t => CustomizeUser(t) },
            { typeof(UserInfo), static t => CustomizeUserInfo(t) },
            { typeof(UserInvitation), static t => CustomizeUserInvitation(t) },
            { typeof(ValidateAddressRequest), static t => CustomizeValidateAddressRequest(t) },
            { typeof(ValidateAddressResponse), static t => CustomizeValidateAddressResponse(t) }
        };

        private static void CustomizeAccountInfo(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAccountInfoWithCustomerData(JsonTypeInfo jsonTypeInfo)
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
                    case "AccountMode":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeAccountTaxCertificate(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAddAccountRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeAddAccountResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAddClientLinksRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeAddClientLinksResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAddPrepayAccountRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeAddPrepayAccountResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAddress(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAdvertiserAccount(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeApiFault(JsonTypeInfo jsonTypeInfo)
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
            newJsonPropertyInfo.Get = _ => "ApiFault";
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

        private static void CustomizeClientLink(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeContactInfo(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCustomer(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCustomerInfo(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCustomerRole(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDateRange(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDeleteAccountRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeDeleteAccountResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDeleteCustomerRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeDeleteCustomerResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDeleteUserRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeDeleteUserResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDismissNotificationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeDismissNotificationsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeFindAccountsOrCustomersInfoRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeFindAccountsOrCustomersInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeFindAccountsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeFindAccountsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetAccessibleCustomerRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetAccessibleCustomerResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetAccountPilotFeaturesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetAccountPilotFeaturesResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetAccountRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetAccountResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetAccountsInfoRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetAccountsInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetCurrentUserRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetCurrentUserResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetCustomerPilotFeaturesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetCustomerPilotFeaturesResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetCustomerRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetCustomerResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetCustomersInfoRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetCustomersInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetLinkedAccountsAndCustomersInfoRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetLinkedAccountsAndCustomersInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetNotificationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetNotificationsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetPilotFeaturesCountriesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetPilotFeaturesCountriesResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetUserMFAStatusRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetUserMFAStatusResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetUserRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetUserResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetUsersInfoRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeGetUsersInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeKeyValueEntityOflongint(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeKeyValueEntityOfstringstring(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeMapAccountIdToExternalAccountIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeMapAccountIdToExternalAccountIdsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeMapCustomerIdToExternalCustomerIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeMapCustomerIdToExternalCustomerIdResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeNotification(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeOrderBy(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizePaging(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizePersonName(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizePredicate(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchAccountsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeSearchAccountsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchClientLinksRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeSearchClientLinksResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchCustomersRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeSearchCustomersResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchUserInvitationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeSearchUserInvitationsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSendUserInvitationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeSendUserInvitationResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSignupCustomerRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeSignupCustomerResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateAccountRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpdateAccountResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateClientLinksRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpdateClientLinksResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateCustomerRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpdateCustomerResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdatePrepayAccountRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpdatePrepayAccountResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateUserRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpdateUserResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateUserRolesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpdateUserRolesResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpgradeCustomerToAgencyRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeUpgradeCustomerToAgencyResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUser(JsonTypeInfo jsonTypeInfo)
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
                    case "AuthenticationToken":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeUserInfo(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUserInvitation(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeValidateAddressRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
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

        private static void CustomizeValidateAddressResponse(JsonTypeInfo jsonTypeInfo)
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