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
using Microsoft.BingAds.V13.CustomerBilling;

public static partial class RestApiGeneration
{
    public static class Microsoft_BingAds_V13_CustomerBilling_EntityModifiers
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
            { typeof(AddInsertionOrderRequest), static t => CustomizeAddInsertionOrderRequest(t) },
            { typeof(AddInsertionOrderResponse), static t => CustomizeAddInsertionOrderResponse(t) },
            { typeof(ApiBatchFault), static t => CustomizeApiBatchFault(t) },
            { typeof(ApiFault), static t => CustomizeApiFault(t) },
            { typeof(ApplicationFault), static t => CustomizeApplicationFault(t) },
            { typeof(BatchError), static t => CustomizeBatchError(t) },
            { typeof(BillingDocument), static t => CustomizeBillingDocument(t) },
            { typeof(BillingDocumentInfo), static t => CustomizeBillingDocumentInfo(t) },
            { typeof(CheckFeatureAdoptionCouponEligibilityRequest), static t => CustomizeCheckFeatureAdoptionCouponEligibilityRequest(t) },
            { typeof(CheckFeatureAdoptionCouponEligibilityResponse), static t => CustomizeCheckFeatureAdoptionCouponEligibilityResponse(t) },
            { typeof(ClaimFeatureAdoptionCouponsRequest), static t => CustomizeClaimFeatureAdoptionCouponsRequest(t) },
            { typeof(ClaimFeatureAdoptionCouponsResponse), static t => CustomizeClaimFeatureAdoptionCouponsResponse(t) },
            { typeof(Coupon), static t => CustomizeCoupon(t) },
            { typeof(CouponClaimInfo), static t => CustomizeCouponClaimInfo(t) },
            { typeof(CouponInfoData), static t => CustomizeCouponInfoData(t) },
            { typeof(CouponRedemption), static t => CustomizeCouponRedemption(t) },
            { typeof(DispatchCouponsRequest), static t => CustomizeDispatchCouponsRequest(t) },
            { typeof(DispatchCouponsResponse), static t => CustomizeDispatchCouponsResponse(t) },
            { typeof(DistributeCouponsRequest), static t => CustomizeDistributeCouponsRequest(t) },
            { typeof(DistributeCouponsResponse), static t => CustomizeDistributeCouponsResponse(t) },
            { typeof(GetAccountMonthlySpendRequest), static t => CustomizeGetAccountMonthlySpendRequest(t) },
            { typeof(GetAccountMonthlySpendResponse), static t => CustomizeGetAccountMonthlySpendResponse(t) },
            { typeof(GetBillingDocumentsInfoRequest), static t => CustomizeGetBillingDocumentsInfoRequest(t) },
            { typeof(GetBillingDocumentsInfoResponse), static t => CustomizeGetBillingDocumentsInfoResponse(t) },
            { typeof(GetBillingDocumentsRequest), static t => CustomizeGetBillingDocumentsRequest(t) },
            { typeof(GetBillingDocumentsResponse), static t => CustomizeGetBillingDocumentsResponse(t) },
            { typeof(GetCouponInfoRequest), static t => CustomizeGetCouponInfoRequest(t) },
            { typeof(GetCouponInfoResponse), static t => CustomizeGetCouponInfoResponse(t) },
            { typeof(InsertionOrder), static t => CustomizeInsertionOrder(t) },
            { typeof(InsertionOrderPendingChanges), static t => CustomizeInsertionOrderPendingChanges(t) },
            { typeof(KeyValueEntityOflongdateTime), static t => CustomizeKeyValueEntityOflongdateTime(t) },
            { typeof(KeyValueEntityOflongstring), static t => CustomizeKeyValueEntityOflongstring(t) },
            { typeof(OperationError), static t => CustomizeOperationError(t) },
            { typeof(OrderBy), static t => CustomizeOrderBy(t) },
            { typeof(Paging), static t => CustomizePaging(t) },
            { typeof(Predicate), static t => CustomizePredicate(t) },
            { typeof(RedeemCouponRequest), static t => CustomizeRedeemCouponRequest(t) },
            { typeof(RedeemCouponResponse), static t => CustomizeRedeemCouponResponse(t) },
            { typeof(SearchCouponsRequest), static t => CustomizeSearchCouponsRequest(t) },
            { typeof(SearchCouponsResponse), static t => CustomizeSearchCouponsResponse(t) },
            { typeof(SearchInsertionOrdersRequest), static t => CustomizeSearchInsertionOrdersRequest(t) },
            { typeof(SearchInsertionOrdersResponse), static t => CustomizeSearchInsertionOrdersResponse(t) },
            { typeof(UpdateInsertionOrderRequest), static t => CustomizeUpdateInsertionOrderRequest(t) },
            { typeof(UpdateInsertionOrderResponse), static t => CustomizeUpdateInsertionOrderResponse(t) }
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

        private static void CustomizeAddInsertionOrderRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeAddInsertionOrderResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeApiBatchFault(JsonTypeInfo jsonTypeInfo)
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
            newJsonPropertyInfo.Get = _ => "ApiBatchFault";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
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
        }

        private static void CustomizeBillingDocument(JsonTypeInfo jsonTypeInfo)
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
                    case "Number":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeBillingDocumentInfo(JsonTypeInfo jsonTypeInfo)
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
                    case "CampaignId":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "DocumentNumber":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeCheckFeatureAdoptionCouponEligibilityRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCheckFeatureAdoptionCouponEligibilityResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeClaimFeatureAdoptionCouponsRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeClaimFeatureAdoptionCouponsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCoupon(JsonTypeInfo jsonTypeInfo)
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
                    case "ClaimInfo":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeCouponClaimInfo(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCouponInfoData(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeCouponRedemption(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDispatchCouponsRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDispatchCouponsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDistributeCouponsRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeDistributeCouponsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetAccountMonthlySpendRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetAccountMonthlySpendResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetBillingDocumentsInfoRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetBillingDocumentsInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetBillingDocumentsRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetBillingDocumentsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetCouponInfoRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeGetCouponInfoResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeInsertionOrder(JsonTypeInfo jsonTypeInfo)
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
                    case "IsUnlimited":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEndless":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeInsertionOrderPendingChanges(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeKeyValueEntityOflongdateTime(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeKeyValueEntityOflongstring(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeRedeemCouponRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeRedeemCouponResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchCouponsRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchCouponsResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchInsertionOrdersRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeSearchInsertionOrdersResponse(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateInsertionOrderRequest(JsonTypeInfo jsonTypeInfo)
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

        private static void CustomizeUpdateInsertionOrderResponse(JsonTypeInfo jsonTypeInfo)
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