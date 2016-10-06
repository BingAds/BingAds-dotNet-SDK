using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.CustomerManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V9
{
    /// <summary>
    /// This example demonstrates how to search for accounts that can be managed by the current authenticated user.
    /// </summary>
    public class SearchUserAccounts : ExampleBase
    {
        public static ServiceClient<ICustomerManagementService> Service;

        public override string Description
        {
            get { return "Search Accounts for Current User | Customer Management V9"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICustomerManagementService>(authorizationData);

                var getUserResponse = await GetUserAsync(null);
                var user = getUserResponse.User;

                // Search for the Bing Ads accounts that the user can access.

                var accounts = await SearchAccountsByUserIdAsync(user.Id);

                // Optionally if you are enabled for Final Urls, you can update each account with a tracking template.
                var accountFCM = new List<KeyValuePair<string, string>>();
                accountFCM.Add(new KeyValuePair<string, string>(
                    "TrackingUrlTemplate",
                    "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"));
                
                OutputStatusMessage("The user can access the following Bing Ads accounts: \n");
                foreach (var account in accounts)
                {
                    OutputAccount(account);

                    // Optionally you can find out which pilot features the customer is able to use. 
                    // Each account could belong to a different customer, so use the customer ID in each account.
                    var featurePilotFlags = await GetCustomerPilotFeaturesAsync((long)account.ParentCustomerId);
                    OutputStatusMessage("Customer Pilot flags:");
                    OutputStatusMessage(string.Join("; ", featurePilotFlags.Select(flag => string.Format("{0}", flag))));
                    
                    // Optionally if you are enabled for Final Urls, you can update each account with a tracking template.
                    // The pilot flag value for Final Urls is 194.
                    if (featurePilotFlags.Any(pilotFlag => pilotFlag == 194))
                    {
                        account.ForwardCompatibilityMap = accountFCM;
                        await UpdateAccountAsync(account);
                        OutputStatusMessage(string.Format("Updated the account with a TrackingUrlTemplate: {0}\n", 
                            accountFCM.ToArray().SingleOrDefault(keyValuePair => keyValuePair.Key == "TrackingUrlTemplate").Value));
                    }
                }
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
        private async Task<GetUserResponse> GetUserAsync(long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await Service.CallAsync((s, r) => s.GetUserAsync(r), request));
        }

        /// <summary>
        /// Gets the list of pilot features that the customer is able to use.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<IList<int>> GetCustomerPilotFeaturesAsync(long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            return (await Service.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request)).FeaturePilotFlags.ToArray();
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
        /// Returns the account requested by account ID.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private async Task<Account> GetAccountAsync(long accountId)
        {
            var request = new GetAccountRequest
            {
                AccountId = accountId
            };

            return (await Service.CallAsync((s, r) => s.GetAccountAsync(r), request)).Account;
        }

        /// <summary>
        /// Updates the account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private async Task<UpdateAccountResponse> UpdateAccountAsync(Account account)
        {
            var request = new UpdateAccountRequest
            {
                Account = account
            };

            return (await Service.CallAsync((s, r) => s.UpdateAccountAsync(r), request));
        }

        /// <summary>
        /// Outputs a subset of the properties of an Account data object.
        /// </summary>
        /// <param name="account"></param>
        private void OutputAccount(Account account)
        {
            OutputStatusMessage(string.Format("Account Id: {0}", account.Id));
            OutputStatusMessage(string.Format("Account Number: {0}", account.Number));
            OutputStatusMessage(string.Format("Account Name: {0}", account.Name));
            OutputStatusMessage(string.Format("Account Parent Customer Id: {0}", account.ParentCustomerId));
        }
    }
}
