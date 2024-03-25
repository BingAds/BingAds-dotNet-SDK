using System;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CustomerBilling;

namespace Microsoft.BingAds
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
    }
}