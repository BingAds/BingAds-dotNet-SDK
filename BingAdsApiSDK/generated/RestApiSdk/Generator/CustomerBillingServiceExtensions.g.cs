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

using System.Threading.Tasks;
using Microsoft.BingAds.V13.CustomerBilling;

namespace Microsoft.BingAds
{
    public static partial class ServiceClientExtensions
    {
        public static Task<GetBillingDocumentsInfoResponse> GetBillingDocumentsInfoAsync(this ServiceClient<ICustomerBillingService> service, GetBillingDocumentsInfoRequest request)
        {
            return service.CallAsync((s, r) => s.GetBillingDocumentsInfoAsync(r), request);
        }

        public static Task<GetBillingDocumentsResponse> GetBillingDocumentsAsync(this ServiceClient<ICustomerBillingService> service, GetBillingDocumentsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBillingDocumentsAsync(r), request);
        }

        public static Task<AddInsertionOrderResponse> AddInsertionOrderAsync(this ServiceClient<ICustomerBillingService> service, AddInsertionOrderRequest request)
        {
            return service.CallAsync((s, r) => s.AddInsertionOrderAsync(r), request);
        }

        public static Task<UpdateInsertionOrderResponse> UpdateInsertionOrderAsync(this ServiceClient<ICustomerBillingService> service, UpdateInsertionOrderRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateInsertionOrderAsync(r), request);
        }

        public static Task<SearchInsertionOrdersResponse> SearchInsertionOrdersAsync(this ServiceClient<ICustomerBillingService> service, SearchInsertionOrdersRequest request)
        {
            return service.CallAsync((s, r) => s.SearchInsertionOrdersAsync(r), request);
        }

        public static Task<GetAccountMonthlySpendResponse> GetAccountMonthlySpendAsync(this ServiceClient<ICustomerBillingService> service, GetAccountMonthlySpendRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccountMonthlySpendAsync(r), request);
        }

        public static Task<DispatchCouponsResponse> DispatchCouponsAsync(this ServiceClient<ICustomerBillingService> service, DispatchCouponsRequest request)
        {
            return service.CallAsync((s, r) => s.DispatchCouponsAsync(r), request);
        }

        public static Task<RedeemCouponResponse> RedeemCouponAsync(this ServiceClient<ICustomerBillingService> service, RedeemCouponRequest request)
        {
            return service.CallAsync((s, r) => s.RedeemCouponAsync(r), request);
        }

        public static Task<SearchCouponsResponse> SearchCouponsAsync(this ServiceClient<ICustomerBillingService> service, SearchCouponsRequest request)
        {
            return service.CallAsync((s, r) => s.SearchCouponsAsync(r), request);
        }

        public static Task<CheckFeatureAdoptionCouponEligibilityResponse> CheckFeatureAdoptionCouponEligibilityAsync(this ServiceClient<ICustomerBillingService> service, CheckFeatureAdoptionCouponEligibilityRequest request)
        {
            return service.CallAsync((s, r) => s.CheckFeatureAdoptionCouponEligibilityAsync(r), request);
        }

        public static Task<ClaimFeatureAdoptionCouponsResponse> ClaimFeatureAdoptionCouponsAsync(this ServiceClient<ICustomerBillingService> service, ClaimFeatureAdoptionCouponsRequest request)
        {
            return service.CallAsync((s, r) => s.ClaimFeatureAdoptionCouponsAsync(r), request);
        }

        public static Task<GetCouponInfoResponse> GetCouponInfoAsync(this ServiceClient<ICustomerBillingService> service, GetCouponInfoRequest request)
        {
            return service.CallAsync((s, r) => s.GetCouponInfoAsync(r), request);
        }

        public static Task<DistributeCouponsResponse> DistributeCouponsAsync(this ServiceClient<ICustomerBillingService> service, DistributeCouponsRequest request)
        {
            return service.CallAsync((s, r) => s.DistributeCouponsAsync(r), request);
        }
    }
}