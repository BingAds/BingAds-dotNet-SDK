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
using Microsoft.BingAds.V13.CustomerManagement;

namespace Microsoft.BingAds.Internal
{
    internal class CustomerManagementService : ICustomerManagementService
    {
        private readonly RestServiceClient _restServiceClient;

        private readonly Type _serviceType;

        public CustomerManagementService(RestServiceClient restServiceClient, Type serviceType)
        {
            _restServiceClient = restServiceClient;

            _serviceType = serviceType;
        }

        public GetAccountsInfoResponse GetAccountsInfo(GetAccountsInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccountsInfoResponse> GetAccountsInfoAsync(GetAccountsInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccountsInfoResponse>("GetAccountsInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public FindAccountsResponse FindAccounts(FindAccountsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<FindAccountsResponse> FindAccountsAsync(FindAccountsRequest request)
        {
            return _restServiceClient.CallServiceAsync<FindAccountsResponse>("FindAccounts", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAccountResponse AddAccount(AddAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAccountResponse> AddAccountAsync(AddAccountRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAccountResponse>("AddAccount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAccountResponse UpdateAccount(UpdateAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAccountResponse> UpdateAccountAsync(UpdateAccountRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAccountResponse>("UpdateAccount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCustomerResponse GetCustomer(GetCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCustomerResponse>("GetCustomer", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateCustomerResponse>("UpdateCustomer", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SignupCustomerResponse SignupCustomer(SignupCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SignupCustomerResponse> SignupCustomerAsync(SignupCustomerRequest request)
        {
            return _restServiceClient.CallServiceAsync<SignupCustomerResponse>("SignupCustomer", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAccountResponse GetAccount(GetAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccountResponse> GetAccountAsync(GetAccountRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccountResponse>("GetAccount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCustomersInfoResponse GetCustomersInfo(GetCustomersInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCustomersInfoResponse> GetCustomersInfoAsync(GetCustomersInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCustomersInfoResponse>("GetCustomersInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAccountResponse DeleteAccount(DeleteAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAccountResponse>("DeleteAccount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteCustomerResponse DeleteCustomer(DeleteCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteCustomerResponse> DeleteCustomerAsync(DeleteCustomerRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteCustomerResponse>("DeleteCustomer", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateUserResponse>("UpdateUser", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateUserRolesResponse UpdateUserRoles(UpdateUserRolesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserRolesResponse> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateUserRolesResponse>("UpdateUserRoles", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserResponse> GetUserAsync(GetUserRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetUserResponse>("GetUser", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCurrentUserResponse GetCurrentUser(GetCurrentUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCurrentUserResponse> GetCurrentUserAsync(GetCurrentUserRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCurrentUserResponse>("GetCurrentUser", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteUserResponse>("DeleteUser", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetUsersInfoResponse GetUsersInfo(GetUsersInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetUsersInfoResponse> GetUsersInfoAsync(GetUsersInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetUsersInfoResponse>("GetUsersInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCustomerPilotFeaturesResponse GetCustomerPilotFeatures(GetCustomerPilotFeaturesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCustomerPilotFeaturesResponse> GetCustomerPilotFeaturesAsync(GetCustomerPilotFeaturesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCustomerPilotFeaturesResponse>("GetCustomerPilotFeatures", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAccountPilotFeaturesResponse GetAccountPilotFeatures(GetAccountPilotFeaturesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccountPilotFeaturesResponse> GetAccountPilotFeaturesAsync(GetAccountPilotFeaturesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccountPilotFeaturesResponse>("GetAccountPilotFeatures", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetPilotFeaturesCountriesResponse GetPilotFeaturesCountries(GetPilotFeaturesCountriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetPilotFeaturesCountriesResponse> GetPilotFeaturesCountriesAsync(GetPilotFeaturesCountriesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetPilotFeaturesCountriesResponse>("GetPilotFeaturesCountries", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAccessibleCustomerResponse GetAccessibleCustomer(GetAccessibleCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccessibleCustomerResponse> GetAccessibleCustomerAsync(GetAccessibleCustomerRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccessibleCustomerResponse>("GetAccessibleCustomer", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public FindAccountsOrCustomersInfoResponse FindAccountsOrCustomersInfo(FindAccountsOrCustomersInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<FindAccountsOrCustomersInfoResponse> FindAccountsOrCustomersInfoAsync(FindAccountsOrCustomersInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<FindAccountsOrCustomersInfoResponse>("FindAccountsOrCustomersInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpgradeCustomerToAgencyResponse UpgradeCustomerToAgency(UpgradeCustomerToAgencyRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpgradeCustomerToAgencyResponse> UpgradeCustomerToAgencyAsync(UpgradeCustomerToAgencyRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpgradeCustomerToAgencyResponse>("UpgradeCustomerToAgency", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddPrepayAccountResponse AddPrepayAccount(AddPrepayAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddPrepayAccountResponse> AddPrepayAccountAsync(AddPrepayAccountRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddPrepayAccountResponse>("AddPrepayAccount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdatePrepayAccountResponse UpdatePrepayAccount(UpdatePrepayAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdatePrepayAccountResponse> UpdatePrepayAccountAsync(UpdatePrepayAccountRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdatePrepayAccountResponse>("UpdatePrepayAccount", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public MapCustomerIdToExternalCustomerIdResponse MapCustomerIdToExternalCustomerId(MapCustomerIdToExternalCustomerIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MapCustomerIdToExternalCustomerIdResponse> MapCustomerIdToExternalCustomerIdAsync(MapCustomerIdToExternalCustomerIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<MapCustomerIdToExternalCustomerIdResponse>("MapCustomerIdToExternalCustomerId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public MapAccountIdToExternalAccountIdsResponse MapAccountIdToExternalAccountIds(MapAccountIdToExternalAccountIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<MapAccountIdToExternalAccountIdsResponse> MapAccountIdToExternalAccountIdsAsync(MapAccountIdToExternalAccountIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<MapAccountIdToExternalAccountIdsResponse>("MapAccountIdToExternalAccountIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchCustomersResponse SearchCustomers(SearchCustomersRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchCustomersResponse> SearchCustomersAsync(SearchCustomersRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchCustomersResponse>("SearchCustomers", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddClientLinksResponse AddClientLinks(AddClientLinksRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddClientLinksResponse> AddClientLinksAsync(AddClientLinksRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddClientLinksResponse>("AddClientLinks", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateClientLinksResponse UpdateClientLinks(UpdateClientLinksRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateClientLinksResponse> UpdateClientLinksAsync(UpdateClientLinksRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateClientLinksResponse>("UpdateClientLinks", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchClientLinksResponse SearchClientLinks(SearchClientLinksRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchClientLinksResponse> SearchClientLinksAsync(SearchClientLinksRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchClientLinksResponse>("SearchClientLinks", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchAccountsResponse SearchAccounts(SearchAccountsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchAccountsResponse> SearchAccountsAsync(SearchAccountsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchAccountsResponse>("SearchAccounts", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SendUserInvitationResponse SendUserInvitation(SendUserInvitationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SendUserInvitationResponse> SendUserInvitationAsync(SendUserInvitationRequest request)
        {
            return _restServiceClient.CallServiceAsync<SendUserInvitationResponse>("SendUserInvitation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchUserInvitationsResponse SearchUserInvitations(SearchUserInvitationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchUserInvitationsResponse> SearchUserInvitationsAsync(SearchUserInvitationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchUserInvitationsResponse>("SearchUserInvitations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ValidateAddressResponse ValidateAddress(ValidateAddressRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ValidateAddressResponse> ValidateAddressAsync(ValidateAddressRequest request)
        {
            return _restServiceClient.CallServiceAsync<ValidateAddressResponse>("ValidateAddress", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetLinkedAccountsAndCustomersInfoResponse GetLinkedAccountsAndCustomersInfo(GetLinkedAccountsAndCustomersInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetLinkedAccountsAndCustomersInfoResponse> GetLinkedAccountsAndCustomersInfoAsync(GetLinkedAccountsAndCustomersInfoRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetLinkedAccountsAndCustomersInfoResponse>("GetLinkedAccountsAndCustomersInfo", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetUserMFAStatusResponse GetUserMFAStatus(GetUserMFAStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserMFAStatusResponse> GetUserMFAStatusAsync(GetUserMFAStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetUserMFAStatusResponse>("GetUserMFAStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetNotificationsResponse GetNotifications(GetNotificationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetNotificationsResponse> GetNotificationsAsync(GetNotificationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetNotificationsResponse>("GetNotifications", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DismissNotificationsResponse DismissNotifications(DismissNotificationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DismissNotificationsResponse> DismissNotificationsAsync(DismissNotificationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DismissNotificationsResponse>("DismissNotifications", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}