using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.CustomerManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to search for accounts that can be managed by the current authenticated user.
    /// </summary>
    public class SearchUserAccounts : ExampleBase
    {
        public override string Description
        {
            get { return "Search Accounts for Current User | Customer Management V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CustomerService = new ServiceClient<ICustomerManagementService>(authorizationData);

                var getUserResponse = await GetUserAsync(null);
                var user = getUserResponse.User;

                // Search for the Bing Ads accounts that the user can access.

                var accounts = await SearchAccountsByUserIdAsync(user.Id);

                OutputStatusMessage("The user can access the following Bing Ads accounts: \n");
                foreach (var account in accounts)
                {
                    OutputAccount(account);

                    // Optionally you can find out which pilot features the customer is able to use. 
                    // Each account could belong to a different customer, so use the customer ID in each account.
                    var featurePilotFlags = (await GetCustomerPilotFeaturesAsync(account.ParentCustomerId)).FeaturePilotFlags;
                    OutputStatusMessage("Customer Pilot flags:");
                    OutputStatusMessage(string.Join("; ", featurePilotFlags.Select(flag => string.Format("{0}", flag))));

                    // Optionally you can update each account with a tracking template.

                    // Uncomment if you want to update the tracking template. 
                    // You should first set your own tracking template above instead of the placeholder.
                    
                    //var accountFCM = new List<KeyValuePair<string, string>>();
                    //accountFCM.Add(new KeyValuePair<string, string>(
                    //    "TrackingUrlTemplate",
                    //    "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"));

                    //account.ForwardCompatibilityMap = accountFCM;
                    //await UpdateAccountAsync(account);
                    //OutputStatusMessage(string.Format("Updated the account with a TrackingUrlTemplate: {0}\n",
                    //    accountFCM.ToArray().SingleOrDefault(keyValuePair => keyValuePair.Key == "TrackingUrlTemplate").Value));

                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.V11.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

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

            return (await CustomerService.CallAsync((s, r) => s.SearchAccountsAsync(r), request)).Accounts.ToArray();
        }        
    }
}
