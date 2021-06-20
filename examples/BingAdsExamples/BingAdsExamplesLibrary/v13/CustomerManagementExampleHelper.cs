using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.CustomerManagement;

namespace BingAdsExamplesLibrary.V13
{
    public class CustomerManagementExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | CustomerManagement V13"; }
        }
        public ServiceClient<ICustomerManagementService> CustomerManagementService;
        public CustomerManagementExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }
        public async Task<AddAccountResponse> AddAccountAsync(
            AdvertiserAccount account)
        {
            var request = new AddAccountRequest
            {
                Account = account
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.AddAccountAsync(r), request));
        }
        public async Task<AddClientLinksResponse> AddClientLinksAsync(
            IList<ClientLink> clientLinks)
        {
            var request = new AddClientLinksRequest
            {
                ClientLinks = clientLinks
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.AddClientLinksAsync(r), request));
        }
        public async Task<DeleteAccountResponse> DeleteAccountAsync(
            long accountId,
            byte[] timeStamp)
        {
            var request = new DeleteAccountRequest
            {
                AccountId = accountId,
                TimeStamp = timeStamp
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.DeleteAccountAsync(r), request));
        }
        public async Task<DeleteCustomerResponse> DeleteCustomerAsync(
            long customerId,
            byte[] timeStamp)
        {
            var request = new DeleteCustomerRequest
            {
                CustomerId = customerId,
                TimeStamp = timeStamp
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.DeleteCustomerAsync(r), request));
        }
        public async Task<DeleteUserResponse> DeleteUserAsync(
            long userId,
            byte[] timeStamp)
        {
            var request = new DeleteUserRequest
            {
                UserId = userId,
                TimeStamp = timeStamp
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.DeleteUserAsync(r), request));
        }
        public async Task<FindAccountsResponse> FindAccountsAsync(
            long? customerId,
            String accountFilter,
            int topN)
        {
            var request = new FindAccountsRequest
            {
                CustomerId = customerId,
                AccountFilter = accountFilter,
                TopN = topN
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.FindAccountsAsync(r), request));
        }
        public async Task<FindAccountsOrCustomersInfoResponse> FindAccountsOrCustomersInfoAsync(
            String filter,
            int topN,
            AccountAdditionalField? returnAdditionalFields)
        {
            var request = new FindAccountsOrCustomersInfoRequest
            {
                Filter = filter,
                TopN = topN,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.FindAccountsOrCustomersInfoAsync(r), request));
        }
        public async Task<GetAccountResponse> GetAccountAsync(
            long accountId,
            AccountAdditionalField? returnAdditionalFields)
        {
            var request = new GetAccountRequest
            {
                AccountId = accountId,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetAccountAsync(r), request));
        }
        public async Task<GetAccountPilotFeaturesResponse> GetAccountPilotFeaturesAsync(
            long accountId)
        {
            var request = new GetAccountPilotFeaturesRequest
            {
                AccountId = accountId
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetAccountPilotFeaturesAsync(r), request));
        }
        public async Task<GetAccountsInfoResponse> GetAccountsInfoAsync(
            long? customerId,
            bool onlyParentAccounts)
        {
            var request = new GetAccountsInfoRequest
            {
                CustomerId = customerId,
                OnlyParentAccounts = onlyParentAccounts
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetAccountsInfoAsync(r), request));
        }
        public async Task<GetCustomerResponse> GetCustomerAsync(
            long customerId)
        {
            var request = new GetCustomerRequest
            {
                CustomerId = customerId
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetCustomerAsync(r), request));
        }
        public async Task<GetCustomerPilotFeaturesResponse> GetCustomerPilotFeaturesAsync(
            long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request));
        }
        public async Task<GetCustomersInfoResponse> GetCustomersInfoAsync(
            String customerNameFilter,
            int topN)
        {
            var request = new GetCustomersInfoRequest
            {
                CustomerNameFilter = customerNameFilter,
                TopN = topN
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetCustomersInfoAsync(r), request));
        }
        public async Task<GetLinkedAccountsAndCustomersInfoResponse> GetLinkedAccountsAndCustomersInfoAsync(
            long? customerId,
            bool onlyParentAccounts)
        {
            var request = new GetLinkedAccountsAndCustomersInfoRequest
            {
                CustomerId = customerId,
                OnlyParentAccounts = onlyParentAccounts
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetLinkedAccountsAndCustomersInfoAsync(r), request));
        }
        public async Task<GetUserResponse> GetUserAsync(
            long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetUserAsync(r), request));
        }
        public async Task<GetUserMFAStatusResponse> GetUserMFAStatusAsync()
        {
            var request = new GetUserMFAStatusRequest
            {
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetUserMFAStatusAsync(r), request));
        }
        public async Task<GetUsersInfoResponse> GetUsersInfoAsync(
            long customerId,
            UserLifeCycleStatus? statusFilter)
        {
            var request = new GetUsersInfoRequest
            {
                CustomerId = customerId,
                StatusFilter = statusFilter
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.GetUsersInfoAsync(r), request));
        }
        public async Task<SearchAccountsResponse> SearchAccountsAsync(
            IList<Predicate> predicates,
            IList<OrderBy> ordering,
            Paging pageInfo,
            AccountAdditionalField? returnAdditionalFields)
        {
            var request = new SearchAccountsRequest
            {
                Predicates = predicates,
                Ordering = ordering,
                PageInfo = pageInfo,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.SearchAccountsAsync(r), request));
        }
        public async Task<SearchClientLinksResponse> SearchClientLinksAsync(
            IList<Predicate> predicates,
            IList<OrderBy> ordering,
            Paging pageInfo)
        {
            var request = new SearchClientLinksRequest
            {
                Predicates = predicates,
                Ordering = ordering,
                PageInfo = pageInfo
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.SearchClientLinksAsync(r), request));
        }
        public async Task<SearchCustomersResponse> SearchCustomersAsync(
            IList<Predicate> predicates,
            DateRange dateRange,
            IList<OrderBy> ordering,
            Paging pageInfo)
        {
            var request = new SearchCustomersRequest
            {
                Predicates = predicates,
                DateRange = dateRange,
                Ordering = ordering,
                PageInfo = pageInfo
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.SearchCustomersAsync(r), request));
        }
        public async Task<SearchUserInvitationsResponse> SearchUserInvitationsAsync(
            IList<Predicate> predicates)
        {
            var request = new SearchUserInvitationsRequest
            {
                Predicates = predicates
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.SearchUserInvitationsAsync(r), request));
        }
        public async Task<SendUserInvitationResponse> SendUserInvitationAsync(
            UserInvitation userInvitation)
        {
            var request = new SendUserInvitationRequest
            {
                UserInvitation = userInvitation
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.SendUserInvitationAsync(r), request));
        }
        public async Task<SignupCustomerResponse> SignupCustomerAsync(
            Customer customer,
            AdvertiserAccount account,
            long? parentCustomerId,
            UserInvitation userInvitation,
            long? userId)
        {
            var request = new SignupCustomerRequest
            {
                Customer = customer,
                Account = account,
                ParentCustomerId = parentCustomerId,
                UserInvitation = userInvitation,
                UserId = userId
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.SignupCustomerAsync(r), request));
        }
        public async Task<UpdateAccountResponse> UpdateAccountAsync(
            AdvertiserAccount account)
        {
            var request = new UpdateAccountRequest
            {
                Account = account
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.UpdateAccountAsync(r), request));
        }
        public async Task<UpdateClientLinksResponse> UpdateClientLinksAsync(
            IList<ClientLink> clientLinks)
        {
            var request = new UpdateClientLinksRequest
            {
                ClientLinks = clientLinks
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.UpdateClientLinksAsync(r), request));
        }
        public async Task<UpdateCustomerResponse> UpdateCustomerAsync(
            Customer customer)
        {
            var request = new UpdateCustomerRequest
            {
                Customer = customer
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.UpdateCustomerAsync(r), request));
        }
        public async Task<UpdateUserResponse> UpdateUserAsync(
            User user)
        {
            var request = new UpdateUserRequest
            {
                User = user
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.UpdateUserAsync(r), request));
        }
        public async Task<UpdateUserRolesResponse> UpdateUserRolesAsync(
            long customerId,
            long userId,
            int? newRoleId,
            IList<long> newAccountIds,
            IList<long> newCustomerIds,
            int? deleteRoleId,
            IList<long> deleteAccountIds,
            IList<long> deleteCustomerIds)
        {
            var request = new UpdateUserRolesRequest
            {
                CustomerId = customerId,
                UserId = userId,
                NewRoleId = newRoleId,
                NewAccountIds = newAccountIds,
                NewCustomerIds = newCustomerIds,
                DeleteRoleId = deleteRoleId,
                DeleteAccountIds = deleteAccountIds,
                DeleteCustomerIds = deleteCustomerIds
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.UpdateUserRolesAsync(r), request));
        }
        public async Task<ValidateAddressResponse> ValidateAddressAsync(
            Address address)
        {
            var request = new ValidateAddressRequest
            {
                Address = address
            };

            return (await CustomerManagementService.CallAsync((s, r) => s.ValidateAddressAsync(r), request));
        }
        public void OutputAccountInfo(AccountInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountInfo * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Number: {0}", dataObject.Number));
                OutputStatusMessage(string.Format("AccountLifeCycleStatus: {0}", dataObject.AccountLifeCycleStatus));
                OutputStatusMessage(string.Format("PauseReason: {0}", dataObject.PauseReason));
                OutputStatusMessage("* * * End OutputAccountInfo * * *");
            }
        }
        public void OutputArrayOfAccountInfo(IList<AccountInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountInfo(dataObject);
                    }
                }
            }
        }
        public void OutputAccountInfoWithCustomerData(AccountInfoWithCustomerData dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountInfoWithCustomerData * * *");
                OutputStatusMessage(string.Format("CustomerId: {0}", dataObject.CustomerId));
                OutputStatusMessage(string.Format("CustomerName: {0}", dataObject.CustomerName));
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("AccountName: {0}", dataObject.AccountName));
                OutputStatusMessage(string.Format("AccountNumber: {0}", dataObject.AccountNumber));
                OutputStatusMessage(string.Format("AccountLifeCycleStatus: {0}", dataObject.AccountLifeCycleStatus));
                OutputStatusMessage(string.Format("PauseReason: {0}", dataObject.PauseReason));
                OutputStatusMessage(string.Format("AccountMode: {0}", dataObject.AccountMode));
                OutputStatusMessage("* * * End OutputAccountInfoWithCustomerData * * *");
            }
        }
        public void OutputArrayOfAccountInfoWithCustomerData(IList<AccountInfoWithCustomerData> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountInfoWithCustomerData(dataObject);
                    }
                }
            }
        }
        public void OutputAccountTaxCertificate(AccountTaxCertificate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountTaxCertificate * * *");
                OutputStatusMessage(string.Format("TaxCertificateBlobContainerName: {0}", dataObject.TaxCertificateBlobContainerName));
                OutputStatusMessage("TaxCertificates:");
                OutputArrayOfKeyValuePairOfstringbase64Binary(dataObject.TaxCertificates);
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputAccountTaxCertificate * * *");
            }
        }
        public void OutputArrayOfAccountTaxCertificate(IList<AccountTaxCertificate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountTaxCertificate(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputAdApiError * * *");
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiError(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiFaultDetail * * *");
                OutputStatusMessage("Errors:");
                OutputArrayOfAdApiError(dataObject.Errors);
                OutputStatusMessage("* * * End OutputAdApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputAddress(Address dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAddress * * *");
                OutputStatusMessage(string.Format("City: {0}", dataObject.City));
                OutputStatusMessage(string.Format("CountryCode: {0}", dataObject.CountryCode));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Line1: {0}", dataObject.Line1));
                OutputStatusMessage(string.Format("Line2: {0}", dataObject.Line2));
                OutputStatusMessage(string.Format("Line3: {0}", dataObject.Line3));
                OutputStatusMessage(string.Format("Line4: {0}", dataObject.Line4));
                OutputStatusMessage(string.Format("PostalCode: {0}", dataObject.PostalCode));
                OutputStatusMessage(string.Format("StateOrProvince: {0}", dataObject.StateOrProvince));
                OutputStatusMessage(string.Format("TimeStamp: {0}", dataObject.TimeStamp));
                OutputStatusMessage(string.Format("BusinessName: {0}", dataObject.BusinessName));
                OutputStatusMessage("* * * End OutputAddress * * *");
            }
        }
        public void OutputArrayOfAddress(IList<Address> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAddress(dataObject);
                    }
                }
            }
        }
        public void OutputAdvertiserAccount(AdvertiserAccount dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdvertiserAccount * * *");
                OutputStatusMessage(string.Format("BillToCustomerId: {0}", dataObject.BillToCustomerId));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("AccountFinancialStatus: {0}", dataObject.AccountFinancialStatus));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", dataObject.LastModifiedByUserId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", dataObject.LastModifiedTime));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Number: {0}", dataObject.Number));
                OutputStatusMessage(string.Format("ParentCustomerId: {0}", dataObject.ParentCustomerId));
                OutputStatusMessage(string.Format("PaymentMethodId: {0}", dataObject.PaymentMethodId));
                OutputStatusMessage(string.Format("PaymentMethodType: {0}", dataObject.PaymentMethodType));
                OutputStatusMessage(string.Format("PrimaryUserId: {0}", dataObject.PrimaryUserId));
                OutputStatusMessage(string.Format("AccountLifeCycleStatus: {0}", dataObject.AccountLifeCycleStatus));
                OutputStatusMessage(string.Format("TimeStamp: {0}", dataObject.TimeStamp));
                OutputStatusMessage(string.Format("TimeZone: {0}", dataObject.TimeZone));
                OutputStatusMessage(string.Format("PauseReason: {0}", dataObject.PauseReason));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage("LinkedAgencies:");
                OutputArrayOfCustomerInfo(dataObject.LinkedAgencies);
                OutputStatusMessage(string.Format("SalesHouseCustomerId: {0}", dataObject.SalesHouseCustomerId));
                OutputStatusMessage("TaxInformation:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.TaxInformation);
                OutputStatusMessage(string.Format("BackUpPaymentInstrumentId: {0}", dataObject.BackUpPaymentInstrumentId));
                OutputStatusMessage(string.Format("BillingThresholdAmount: {0}", dataObject.BillingThresholdAmount));
                OutputStatusMessage("BusinessAddress:");
                OutputAddress(dataObject.BusinessAddress);
                OutputStatusMessage(string.Format("AutoTagType: {0}", dataObject.AutoTagType));
                OutputStatusMessage(string.Format("SoldToPaymentInstrumentId: {0}", dataObject.SoldToPaymentInstrumentId));
                OutputStatusMessage("TaxCertificate:");
                OutputAccountTaxCertificate(dataObject.TaxCertificate);
                OutputStatusMessage(string.Format("AccountMode: {0}", dataObject.AccountMode));
                OutputStatusMessage("* * * End OutputAdvertiserAccount * * *");
            }
        }
        public void OutputArrayOfAdvertiserAccount(IList<AdvertiserAccount> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdvertiserAccount(dataObject);
                    }
                }
            }
        }
        public void OutputApiFault(ApiFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiFault * * *");
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                OutputStatusMessage("* * * End OutputApiFault * * *");
            }
        }
        public void OutputArrayOfApiFault(IList<ApiFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiFault(dataObject);
                    }
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApplicationFault * * *");
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(null != adapifaultdetail)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifault = dataObject as ApiFault;
                if(null != apifault)
                {
                    OutputApiFault((ApiFault)dataObject);
                }
                OutputStatusMessage("* * * End OutputApplicationFault * * *");
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApplicationFault(dataObject);
                    }
                }
            }
        }
        public void OutputClientLink(ClientLink dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputClientLink * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("ClientEntityId: {0}", dataObject.ClientEntityId));
                OutputStatusMessage(string.Format("ClientEntityNumber: {0}", dataObject.ClientEntityNumber));
                OutputStatusMessage(string.Format("ClientEntityName: {0}", dataObject.ClientEntityName));
                OutputStatusMessage(string.Format("ManagingCustomerId: {0}", dataObject.ManagingCustomerId));
                OutputStatusMessage(string.Format("ManagingCustomerNumber: {0}", dataObject.ManagingCustomerNumber));
                OutputStatusMessage(string.Format("ManagingCustomerName: {0}", dataObject.ManagingCustomerName));
                OutputStatusMessage(string.Format("Note: {0}", dataObject.Note));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("InviterEmail: {0}", dataObject.InviterEmail));
                OutputStatusMessage(string.Format("InviterName: {0}", dataObject.InviterName));
                OutputStatusMessage(string.Format("InviterPhone: {0}", dataObject.InviterPhone));
                OutputStatusMessage(string.Format("IsBillToClient: {0}", dataObject.IsBillToClient));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("SuppressNotification: {0}", dataObject.SuppressNotification));
                OutputStatusMessage(string.Format("LastModifiedDateTime: {0}", dataObject.LastModifiedDateTime));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", dataObject.LastModifiedByUserId));
                OutputStatusMessage(string.Format("Timestamp: {0}", dataObject.Timestamp));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("CustomerLinkPermission: {0}", dataObject.CustomerLinkPermission));
                OutputStatusMessage("* * * End OutputClientLink * * *");
            }
        }
        public void OutputArrayOfClientLink(IList<ClientLink> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputClientLink(dataObject);
                    }
                }
            }
        }
        public void OutputContactInfo(ContactInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputContactInfo * * *");
                OutputStatusMessage("Address:");
                OutputAddress(dataObject.Address);
                OutputStatusMessage(string.Format("ContactByPhone: {0}", dataObject.ContactByPhone));
                OutputStatusMessage(string.Format("ContactByPostalMail: {0}", dataObject.ContactByPostalMail));
                OutputStatusMessage(string.Format("Email: {0}", dataObject.Email));
                OutputStatusMessage(string.Format("EmailFormat: {0}", dataObject.EmailFormat));
                OutputStatusMessage(string.Format("Fax: {0}", dataObject.Fax));
                OutputStatusMessage(string.Format("HomePhone: {0}", dataObject.HomePhone));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Mobile: {0}", dataObject.Mobile));
                OutputStatusMessage(string.Format("Phone1: {0}", dataObject.Phone1));
                OutputStatusMessage(string.Format("Phone2: {0}", dataObject.Phone2));
                OutputStatusMessage("* * * End OutputContactInfo * * *");
            }
        }
        public void OutputArrayOfContactInfo(IList<ContactInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputContactInfo(dataObject);
                    }
                }
            }
        }
        public void OutputCustomer(Customer dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomer * * *");
                OutputStatusMessage(string.Format("CustomerFinancialStatus: {0}", dataObject.CustomerFinancialStatus));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Industry: {0}", dataObject.Industry));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", dataObject.LastModifiedByUserId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", dataObject.LastModifiedTime));
                OutputStatusMessage(string.Format("MarketCountry: {0}", dataObject.MarketCountry));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("MarketLanguage: {0}", dataObject.MarketLanguage));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("ServiceLevel: {0}", dataObject.ServiceLevel));
                OutputStatusMessage(string.Format("CustomerLifeCycleStatus: {0}", dataObject.CustomerLifeCycleStatus));
                OutputStatusMessage(string.Format("TimeStamp: {0}", dataObject.TimeStamp));
                OutputStatusMessage(string.Format("Number: {0}", dataObject.Number));
                OutputStatusMessage("CustomerAddress:");
                OutputAddress(dataObject.CustomerAddress);
                OutputStatusMessage("* * * End OutputCustomer * * *");
            }
        }
        public void OutputArrayOfCustomer(IList<Customer> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomer(dataObject);
                    }
                }
            }
        }
        public void OutputCustomerInfo(CustomerInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomerInfo * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage("* * * End OutputCustomerInfo * * *");
            }
        }
        public void OutputArrayOfCustomerInfo(IList<CustomerInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomerInfo(dataObject);
                    }
                }
            }
        }
        public void OutputCustomerRole(CustomerRole dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomerRole * * *");
                OutputStatusMessage(string.Format("RoleId: {0}", dataObject.RoleId));
                OutputStatusMessage(string.Format("CustomerId: {0}", dataObject.CustomerId));
                OutputStatusMessage("AccountIds:");
                OutputArrayOfLong(dataObject.AccountIds);
                OutputStatusMessage("LinkedAccountIds:");
                OutputArrayOfLong(dataObject.LinkedAccountIds);
                OutputStatusMessage(string.Format("CustomerLinkPermission: {0}", dataObject.CustomerLinkPermission));
                OutputStatusMessage("* * * End OutputCustomerRole * * *");
            }
        }
        public void OutputArrayOfCustomerRole(IList<CustomerRole> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomerRole(dataObject);
                    }
                }
            }
        }
        public void OutputDateRange(DateRange dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDateRange * * *");
                OutputStatusMessage(string.Format("MinDate: {0}", dataObject.MinDate));
                OutputStatusMessage(string.Format("MaxDate: {0}", dataObject.MaxDate));
                OutputStatusMessage("* * * End OutputDateRange * * *");
            }
        }
        public void OutputArrayOfDateRange(IList<DateRange> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDateRange(dataObject);
                    }
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOperationError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputOperationError * * *");
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOperationError(dataObject);
                    }
                }
            }
        }
        public void OutputOrderBy(OrderBy dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOrderBy * * *");
                OutputStatusMessage(string.Format("Field: {0}", dataObject.Field));
                OutputStatusMessage(string.Format("Order: {0}", dataObject.Order));
                OutputStatusMessage("* * * End OutputOrderBy * * *");
            }
        }
        public void OutputArrayOfOrderBy(IList<OrderBy> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOrderBy(dataObject);
                    }
                }
            }
        }
        public void OutputPaging(Paging dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPaging * * *");
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Size: {0}", dataObject.Size));
                OutputStatusMessage("* * * End OutputPaging * * *");
            }
        }
        public void OutputArrayOfPaging(IList<Paging> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPaging(dataObject);
                    }
                }
            }
        }
        public void OutputPersonName(PersonName dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPersonName * * *");
                OutputStatusMessage(string.Format("FirstName: {0}", dataObject.FirstName));
                OutputStatusMessage(string.Format("LastName: {0}", dataObject.LastName));
                OutputStatusMessage(string.Format("MiddleInitial: {0}", dataObject.MiddleInitial));
                OutputStatusMessage("* * * End OutputPersonName * * *");
            }
        }
        public void OutputArrayOfPersonName(IList<PersonName> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPersonName(dataObject);
                    }
                }
            }
        }
        public void OutputPredicate(Predicate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPredicate * * *");
                OutputStatusMessage(string.Format("Field: {0}", dataObject.Field));
                OutputStatusMessage(string.Format("Operator: {0}", dataObject.Operator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage("* * * End OutputPredicate * * *");
            }
        }
        public void OutputArrayOfPredicate(IList<Predicate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPredicate(dataObject);
                    }
                }
            }
        }
        public void OutputUser(User dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUser * * *");
                OutputStatusMessage("ContactInfo:");
                OutputContactInfo(dataObject.ContactInfo);
                OutputStatusMessage(string.Format("CustomerId: {0}", dataObject.CustomerId));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("JobTitle: {0}", dataObject.JobTitle));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", dataObject.LastModifiedByUserId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", dataObject.LastModifiedTime));
                OutputStatusMessage(string.Format("Lcid: {0}", dataObject.Lcid));
                OutputStatusMessage("Name:");
                OutputPersonName(dataObject.Name);
                OutputStatusMessage(string.Format("Password: {0}", dataObject.Password));
                OutputStatusMessage(string.Format("SecretAnswer: {0}", dataObject.SecretAnswer));
                OutputStatusMessage(string.Format("SecretQuestion: {0}", dataObject.SecretQuestion));
                OutputStatusMessage(string.Format("UserLifeCycleStatus: {0}", dataObject.UserLifeCycleStatus));
                OutputStatusMessage(string.Format("TimeStamp: {0}", dataObject.TimeStamp));
                OutputStatusMessage(string.Format("UserName: {0}", dataObject.UserName));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage("* * * End OutputUser * * *");
            }
        }
        public void OutputArrayOfUser(IList<User> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUser(dataObject);
                    }
                }
            }
        }
        public void OutputUserInfo(UserInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUserInfo * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("UserName: {0}", dataObject.UserName));
                OutputStatusMessage("* * * End OutputUserInfo * * *");
            }
        }
        public void OutputArrayOfUserInfo(IList<UserInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUserInfo(dataObject);
                    }
                }
            }
        }
        public void OutputUserInvitation(UserInvitation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUserInvitation * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("FirstName: {0}", dataObject.FirstName));
                OutputStatusMessage(string.Format("LastName: {0}", dataObject.LastName));
                OutputStatusMessage(string.Format("Email: {0}", dataObject.Email));
                OutputStatusMessage(string.Format("CustomerId: {0}", dataObject.CustomerId));
                OutputStatusMessage(string.Format("RoleId: {0}", dataObject.RoleId));
                OutputStatusMessage("AccountIds:");
                OutputArrayOfLong(dataObject.AccountIds);
                OutputStatusMessage(string.Format("ExpirationDate: {0}", dataObject.ExpirationDate));
                OutputStatusMessage(string.Format("Lcid: {0}", dataObject.Lcid));
                OutputStatusMessage("* * * End OutputUserInvitation * * *");
            }
        }
        public void OutputArrayOfUserInvitation(IList<UserInvitation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUserInvitation(dataObject);
                    }
                }
            }
        }
        public void OutputAccountLifeCycleStatus(AccountLifeCycleStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AccountLifeCycleStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAccountLifeCycleStatus(IList<AccountLifeCycleStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAccountLifeCycleStatus(valueSet);
                }
            }
        }
        public void OutputCurrencyCode(CurrencyCode valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CurrencyCode)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCurrencyCode(IList<CurrencyCode> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCurrencyCode(valueSet);
                }
            }
        }
        public void OutputAccountFinancialStatus(AccountFinancialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AccountFinancialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAccountFinancialStatus(IList<AccountFinancialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAccountFinancialStatus(valueSet);
                }
            }
        }
        public void OutputLanguageType(LanguageType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(LanguageType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfLanguageType(IList<LanguageType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputLanguageType(valueSet);
                }
            }
        }
        public void OutputPaymentMethodType(PaymentMethodType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PaymentMethodType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPaymentMethodType(IList<PaymentMethodType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPaymentMethodType(valueSet);
                }
            }
        }
        public void OutputTimeZoneType(TimeZoneType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(TimeZoneType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfTimeZoneType(IList<TimeZoneType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputTimeZoneType(valueSet);
                }
            }
        }
        public void OutputAutoTagType(AutoTagType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AutoTagType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAutoTagType(IList<AutoTagType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAutoTagType(valueSet);
                }
            }
        }
        public void OutputTaxCertificateStatus(TaxCertificateStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(TaxCertificateStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfTaxCertificateStatus(IList<TaxCertificateStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputTaxCertificateStatus(valueSet);
                }
            }
        }
        public void OutputCustomerFinancialStatus(CustomerFinancialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CustomerFinancialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCustomerFinancialStatus(IList<CustomerFinancialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCustomerFinancialStatus(valueSet);
                }
            }
        }
        public void OutputIndustry(Industry valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(Industry)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfIndustry(IList<Industry> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputIndustry(valueSet);
                }
            }
        }
        public void OutputServiceLevel(ServiceLevel valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ServiceLevel)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfServiceLevel(IList<ServiceLevel> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputServiceLevel(valueSet);
                }
            }
        }
        public void OutputCustomerLifeCycleStatus(CustomerLifeCycleStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CustomerLifeCycleStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCustomerLifeCycleStatus(IList<CustomerLifeCycleStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCustomerLifeCycleStatus(valueSet);
                }
            }
        }
        public void OutputLCID(LCID valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(LCID)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfLCID(IList<LCID> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputLCID(valueSet);
                }
            }
        }
        public void OutputAccountAdditionalField(AccountAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AccountAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAccountAdditionalField(IList<AccountAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAccountAdditionalField(valueSet);
                }
            }
        }
        public void OutputEmailFormat(EmailFormat valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(EmailFormat)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfEmailFormat(IList<EmailFormat> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputEmailFormat(valueSet);
                }
            }
        }
        public void OutputSecretQuestion(SecretQuestion valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SecretQuestion)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSecretQuestion(IList<SecretQuestion> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSecretQuestion(valueSet);
                }
            }
        }
        public void OutputUserLifeCycleStatus(UserLifeCycleStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(UserLifeCycleStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfUserLifeCycleStatus(IList<UserLifeCycleStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputUserLifeCycleStatus(valueSet);
                }
            }
        }
        public void OutputPredicateOperator(PredicateOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PredicateOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPredicateOperator(IList<PredicateOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPredicateOperator(valueSet);
                }
            }
        }
        public void OutputOrderByField(OrderByField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(OrderByField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfOrderByField(IList<OrderByField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputOrderByField(valueSet);
                }
            }
        }
        public void OutputSortOrder(SortOrder valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SortOrder)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSortOrder(IList<SortOrder> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSortOrder(valueSet);
                }
            }
        }
        public void OutputClientLinkStatus(ClientLinkStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ClientLinkStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfClientLinkStatus(IList<ClientLinkStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputClientLinkStatus(valueSet);
                }
            }
        }
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputKeyValuePairOfstringstring(KeyValuePair<string,string> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringstring(IList<KeyValuePair<string,string>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringstring(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOflonglong(KeyValuePair<long,long> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOflonglong(IList<KeyValuePair<long,long>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOflonglong(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOfstringbase64Binary(KeyValuePair<string,byte[]> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringbase64Binary(IList<KeyValuePair<string,byte[]>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringbase64Binary(dataObject);
                }
            }
        }
    }
}