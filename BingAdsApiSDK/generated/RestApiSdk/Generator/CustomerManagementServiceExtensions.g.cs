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