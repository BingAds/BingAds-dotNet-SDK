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

using System;
using Microsoft.BingAds.V13.CustomerBilling;

namespace Microsoft.BingAds.Internal
{
    internal class CustomerBillingService : ICustomerBillingService
    {
        private readonly RestServiceClient _restServiceClient;

        private readonly Type _serviceType;

        public CustomerBillingService(RestServiceClient restServiceClient, Type serviceType)
        {
            _restServiceClient = restServiceClient;

            _serviceType = serviceType;
        }

        public GetBillingDocumentsInfoResponse GetBillingDocumentsInfo(GetBillingDocumentsInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBillingDocumentsInfoResponse> GetBillingDocumentsInfoAsync(GetBillingDocumentsInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBillingDocumentsInfoResponse>("GetBillingDocumentsInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBillingDocumentsResponse GetBillingDocuments(GetBillingDocumentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBillingDocumentsResponse> GetBillingDocumentsAsync(GetBillingDocumentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBillingDocumentsResponse>("GetBillingDocuments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddInsertionOrderResponse AddInsertionOrder(AddInsertionOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddInsertionOrderResponse> AddInsertionOrderAsync(AddInsertionOrderRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddInsertionOrderResponse>("AddInsertionOrder", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateInsertionOrderResponse UpdateInsertionOrder(UpdateInsertionOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateInsertionOrderResponse> UpdateInsertionOrderAsync(UpdateInsertionOrderRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateInsertionOrderResponse>("UpdateInsertionOrder", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchInsertionOrdersResponse SearchInsertionOrders(SearchInsertionOrdersRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchInsertionOrdersResponse> SearchInsertionOrdersAsync(SearchInsertionOrdersRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchInsertionOrdersResponse>("SearchInsertionOrders", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAccountMonthlySpendResponse GetAccountMonthlySpend(GetAccountMonthlySpendRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccountMonthlySpendResponse> GetAccountMonthlySpendAsync(GetAccountMonthlySpendRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccountMonthlySpendResponse>("GetAccountMonthlySpend", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DispatchCouponsResponse DispatchCoupons(DispatchCouponsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DispatchCouponsResponse> DispatchCouponsAsync(DispatchCouponsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DispatchCouponsResponse>("DispatchCoupons", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public RedeemCouponResponse RedeemCoupon(RedeemCouponRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RedeemCouponResponse> RedeemCouponAsync(RedeemCouponRequest request)
        {
            return _restServiceClient.CallServiceAsync<RedeemCouponResponse>("RedeemCoupon", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchCouponsResponse SearchCoupons(SearchCouponsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchCouponsResponse> SearchCouponsAsync(SearchCouponsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchCouponsResponse>("SearchCoupons", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public CheckFeatureAdoptionCouponEligibilityResponse CheckFeatureAdoptionCouponEligibility(CheckFeatureAdoptionCouponEligibilityRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CheckFeatureAdoptionCouponEligibilityResponse> CheckFeatureAdoptionCouponEligibilityAsync(CheckFeatureAdoptionCouponEligibilityRequest request)
        {
            return _restServiceClient.CallServiceAsync<CheckFeatureAdoptionCouponEligibilityResponse>("CheckFeatureAdoptionCouponEligibility", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ClaimFeatureAdoptionCouponsResponse ClaimFeatureAdoptionCoupons(ClaimFeatureAdoptionCouponsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimFeatureAdoptionCouponsResponse> ClaimFeatureAdoptionCouponsAsync(ClaimFeatureAdoptionCouponsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ClaimFeatureAdoptionCouponsResponse>("ClaimFeatureAdoptionCoupons", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCouponInfoResponse GetCouponInfo(GetCouponInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCouponInfoResponse> GetCouponInfoAsync(GetCouponInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCouponInfoResponse>("GetCouponInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DistributeCouponsResponse DistributeCoupons(DistributeCouponsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DistributeCouponsResponse> DistributeCouponsAsync(DistributeCouponsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DistributeCouponsResponse>("DistributeCoupons", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}