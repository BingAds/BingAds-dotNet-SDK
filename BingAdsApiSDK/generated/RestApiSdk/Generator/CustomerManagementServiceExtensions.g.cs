using System.Threading.Tasks;
using Microsoft.BingAds.V13.CustomerManagement;

namespace Microsoft.BingAds
{
    public static partial class ServiceClientExtensions
    {
        public static Task<GetAccountsInfoResponse> GetAccountsInfoAsync(this ServiceClient<ICustomerManagementService> service, GetAccountsInfoRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccountsInfoAsync(r), request);
        }

        public static Task<FindAccountsResponse> FindAccountsAsync(this ServiceClient<ICustomerManagementService> service, FindAccountsRequest request)
        {
            return service.CallAsync((s, r) => s.FindAccountsAsync(r), request);
        }

        public static Task<AddAccountResponse> AddAccountAsync(this ServiceClient<ICustomerManagementService> service, AddAccountRequest request)
        {
            return service.CallAsync((s, r) => s.AddAccountAsync(r), request);
        }

        public static Task<UpdateAccountResponse> UpdateAccountAsync(this ServiceClient<ICustomerManagementService> service, UpdateAccountRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAccountAsync(r), request);
        }

        public static Task<GetCustomerResponse> GetCustomerAsync(this ServiceClient<ICustomerManagementService> service, GetCustomerRequest request)
        {
            return service.CallAsync((s, r) => s.GetCustomerAsync(r), request);
        }

        public static Task<UpdateCustomerResponse> UpdateCustomerAsync(this ServiceClient<ICustomerManagementService> service, UpdateCustomerRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateCustomerAsync(r), request);
        }

        public static Task<SignupCustomerResponse> SignupCustomerAsync(this ServiceClient<ICustomerManagementService> service, SignupCustomerRequest request)
        {
            return service.CallAsync((s, r) => s.SignupCustomerAsync(r), request);
        }

        public static Task<GetAccountResponse> GetAccountAsync(this ServiceClient<ICustomerManagementService> service, GetAccountRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccountAsync(r), request);
        }

        public static Task<GetCustomersInfoResponse> GetCustomersInfoAsync(this ServiceClient<ICustomerManagementService> service, GetCustomersInfoRequest request)
        {
            return service.CallAsync((s, r) => s.GetCustomersInfoAsync(r), request);
        }

        public static Task<DeleteAccountResponse> DeleteAccountAsync(this ServiceClient<ICustomerManagementService> service, DeleteAccountRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAccountAsync(r), request);
        }

        public static Task<DeleteCustomerResponse> DeleteCustomerAsync(this ServiceClient<ICustomerManagementService> service, DeleteCustomerRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteCustomerAsync(r), request);
        }

        public static Task<UpdateUserResponse> UpdateUserAsync(this ServiceClient<ICustomerManagementService> service, UpdateUserRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateUserAsync(r), request);
        }

        public static Task<UpdateUserRolesResponse> UpdateUserRolesAsync(this ServiceClient<ICustomerManagementService> service, UpdateUserRolesRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateUserRolesAsync(r), request);
        }

        public static Task<GetUserResponse> GetUserAsync(this ServiceClient<ICustomerManagementService> service, GetUserRequest request)
        {
            return service.CallAsync((s, r) => s.GetUserAsync(r), request);
        }

        public static Task<GetCurrentUserResponse> GetCurrentUserAsync(this ServiceClient<ICustomerManagementService> service, GetCurrentUserRequest request)
        {
            return service.CallAsync((s, r) => s.GetCurrentUserAsync(r), request);
        }

        public static Task<DeleteUserResponse> DeleteUserAsync(this ServiceClient<ICustomerManagementService> service, DeleteUserRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteUserAsync(r), request);
        }

        public static Task<GetUsersInfoResponse> GetUsersInfoAsync(this ServiceClient<ICustomerManagementService> service, GetUsersInfoRequest request)
        {
            return service.CallAsync((s, r) => s.GetUsersInfoAsync(r), request);
        }

        public static Task<GetCustomerPilotFeaturesResponse> GetCustomerPilotFeaturesAsync(this ServiceClient<ICustomerManagementService> service, GetCustomerPilotFeaturesRequest request)
        {
            return service.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request);
        }

        public static Task<GetAccountPilotFeaturesResponse> GetAccountPilotFeaturesAsync(this ServiceClient<ICustomerManagementService> service, GetAccountPilotFeaturesRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccountPilotFeaturesAsync(r), request);
        }

        public static Task<GetPilotFeaturesCountriesResponse> GetPilotFeaturesCountriesAsync(this ServiceClient<ICustomerManagementService> service, GetPilotFeaturesCountriesRequest request)
        {
            return service.CallAsync((s, r) => s.GetPilotFeaturesCountriesAsync(r), request);
        }

        public static Task<GetAccessibleCustomerResponse> GetAccessibleCustomerAsync(this ServiceClient<ICustomerManagementService> service, GetAccessibleCustomerRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccessibleCustomerAsync(r), request);
        }

        public static Task<FindAccountsOrCustomersInfoResponse> FindAccountsOrCustomersInfoAsync(this ServiceClient<ICustomerManagementService> service, FindAccountsOrCustomersInfoRequest request)
        {
            return service.CallAsync((s, r) => s.FindAccountsOrCustomersInfoAsync(r), request);
        }

        public static Task<UpgradeCustomerToAgencyResponse> UpgradeCustomerToAgencyAsync(this ServiceClient<ICustomerManagementService> service, UpgradeCustomerToAgencyRequest request)
        {
            return service.CallAsync((s, r) => s.UpgradeCustomerToAgencyAsync(r), request);
        }

        public static Task<AddPrepayAccountResponse> AddPrepayAccountAsync(this ServiceClient<ICustomerManagementService> service, AddPrepayAccountRequest request)
        {
            return service.CallAsync((s, r) => s.AddPrepayAccountAsync(r), request);
        }

        public static Task<UpdatePrepayAccountResponse> UpdatePrepayAccountAsync(this ServiceClient<ICustomerManagementService> service, UpdatePrepayAccountRequest request)
        {
            return service.CallAsync((s, r) => s.UpdatePrepayAccountAsync(r), request);
        }

        public static Task<MapCustomerIdToExternalCustomerIdResponse> MapCustomerIdToExternalCustomerIdAsync(this ServiceClient<ICustomerManagementService> service, MapCustomerIdToExternalCustomerIdRequest request)
        {
            return service.CallAsync((s, r) => s.MapCustomerIdToExternalCustomerIdAsync(r), request);
        }

        public static Task<MapAccountIdToExternalAccountIdsResponse> MapAccountIdToExternalAccountIdsAsync(this ServiceClient<ICustomerManagementService> service, MapAccountIdToExternalAccountIdsRequest request)
        {
            return service.CallAsync((s, r) => s.MapAccountIdToExternalAccountIdsAsync(r), request);
        }

        public static Task<SearchCustomersResponse> SearchCustomersAsync(this ServiceClient<ICustomerManagementService> service, SearchCustomersRequest request)
        {
            return service.CallAsync((s, r) => s.SearchCustomersAsync(r), request);
        }

        public static Task<AddClientLinksResponse> AddClientLinksAsync(this ServiceClient<ICustomerManagementService> service, AddClientLinksRequest request)
        {
            return service.CallAsync((s, r) => s.AddClientLinksAsync(r), request);
        }

        public static Task<UpdateClientLinksResponse> UpdateClientLinksAsync(this ServiceClient<ICustomerManagementService> service, UpdateClientLinksRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateClientLinksAsync(r), request);
        }

        public static Task<SearchClientLinksResponse> SearchClientLinksAsync(this ServiceClient<ICustomerManagementService> service, SearchClientLinksRequest request)
        {
            return service.CallAsync((s, r) => s.SearchClientLinksAsync(r), request);
        }

        public static Task<SearchAccountsResponse> SearchAccountsAsync(this ServiceClient<ICustomerManagementService> service, SearchAccountsRequest request)
        {
            return service.CallAsync((s, r) => s.SearchAccountsAsync(r), request);
        }

        public static Task<SendUserInvitationResponse> SendUserInvitationAsync(this ServiceClient<ICustomerManagementService> service, SendUserInvitationRequest request)
        {
            return service.CallAsync((s, r) => s.SendUserInvitationAsync(r), request);
        }

        public static Task<SearchUserInvitationsResponse> SearchUserInvitationsAsync(this ServiceClient<ICustomerManagementService> service, SearchUserInvitationsRequest request)
        {
            return service.CallAsync((s, r) => s.SearchUserInvitationsAsync(r), request);
        }

        public static Task<ValidateAddressResponse> ValidateAddressAsync(this ServiceClient<ICustomerManagementService> service, ValidateAddressRequest request)
        {
            return service.CallAsync((s, r) => s.ValidateAddressAsync(r), request);
        }

        public static Task<GetLinkedAccountsAndCustomersInfoResponse> GetLinkedAccountsAndCustomersInfoAsync(this ServiceClient<ICustomerManagementService> service, GetLinkedAccountsAndCustomersInfoRequest request)
        {
            return service.CallAsync((s, r) => s.GetLinkedAccountsAndCustomersInfoAsync(r), request);
        }

        public static Task<GetUserMFAStatusResponse> GetUserMFAStatusAsync(this ServiceClient<ICustomerManagementService> service, GetUserMFAStatusRequest request)
        {
            return service.CallAsync((s, r) => s.GetUserMFAStatusAsync(r), request);
        }

        public static Task<GetNotificationsResponse> GetNotificationsAsync(this ServiceClient<ICustomerManagementService> service, GetNotificationsRequest request)
        {
            return service.CallAsync((s, r) => s.GetNotificationsAsync(r), request);
        }

        public static Task<DismissNotificationsResponse> DismissNotificationsAsync(this ServiceClient<ICustomerManagementService> service, DismissNotificationsRequest request)
        {
            return service.CallAsync((s, r) => s.DismissNotificationsAsync(r), request);
        }
    }
}