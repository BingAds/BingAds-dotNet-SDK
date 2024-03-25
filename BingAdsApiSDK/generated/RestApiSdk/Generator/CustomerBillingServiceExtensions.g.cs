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
    }
}