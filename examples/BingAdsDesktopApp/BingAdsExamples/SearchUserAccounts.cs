using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.CustomerManagement;
using Microsoft.BingAds;


namespace BingAdsExamples
{
    /// <summary>
    /// This example demonstrates how to search for accounts that can be managed by the current authenticated user.
    /// </summary>
    public class SearchUserAccounts : ExampleBase
    {
        public static ServiceClient<ICustomerManagementService> Service;

        public override string Description
        {
            get { return "Customer Management | Search Accounts for Current User"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICustomerManagementService>(authorizationData);

                var user = await GetUserAsync(null);

                // Search for the accounts that matches the specified criteria.

                var accounts = await SearchAccountsByUserIdAsync(user.Id);

                PrintAccounts(accounts);
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        /// <summary>
        /// Gets a User object by the specified Bing Ads user identifier.
        /// </summary>
        /// <param name="userId">The identifier of the user to get. If null, this operation returns the User object 
        /// corresponding to the current authenticated user of the global customer management ServiceClient.</param>
        /// <returns>The User object corresponding to the specified Bing Ads user identifier.</returns>
        private async Task<User> GetUserAsync(long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await Service.CallAsync((s, r) => s.GetUserAsync(r), request)).User;
        }

        /// <summary>
        /// Searches by UserId for accounts that the user can manage.
        /// </summary>
        /// <param name="userId">The Bing Ads user identifier.</param>
        /// <returns>List of accounts that the user can manage.</returns>
        private async Task<Account[]> SearchAccountsByUserIdAsync(long? userId)
        {
            var predicate = new Predicate
            {
                Field = "UserId",
                Operator = PredicateOperator.Equals,
                Value = userId.ToString()
            };

            var paging = new Paging
            {
                Index = 0,
                Size = 10
            };

            var request = new SearchAccountsRequest
            {
                Ordering = null,
                PageInfo = paging,
                Predicates = new[] { predicate }
            };

            return (await Service.CallAsync((s, r) => s.SearchAccountsAsync(r), request)).Accounts.ToArray();
        }

        /// <summary>
        /// Outputs the account and parent customer identifiers for the specified accounts. 
        /// </summary>
        /// <param name="accounts">The list of accounts to print.</param>
        private void PrintAccounts(IEnumerable<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                OutputStatusMessage(string.Format("AccountId: {0}\n", account.Id));
                OutputStatusMessage(string.Format("CustomerId: {0}\n", account.ParentCustomerId));
            }
        }
    }
}
