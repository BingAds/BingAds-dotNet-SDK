using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CustomerManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to search for accounts that can be managed by the current authenticated user.
    /// </summary>
    public class SearchUserAccounts : ExampleBase
    {
        public override string Description
        {
            get { return "Search Accounts for Current User | Customer Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CustomerManagementExampleHelper CustomerManagementExampleHelper = new CustomerManagementExampleHelper(this.OutputStatusMessage);
                CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(authorizationData);

                var getUserResponse = await CustomerManagementExampleHelper.GetUserAsync(null);
                var user = getUserResponse.User;

                // Search for the Bing Ads accounts that the user can access.

                var predicate = new Predicate
                {
                    Field = "UserId",
                    Operator = PredicateOperator.Equals,
                    Value = user.Id.ToString()
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

                var accounts = (await CustomerManagementExampleHelper.SearchAccountsAsync(
                    new[] { predicate },
                    null,
                    paging
                    ))?.Accounts.ToArray();

                OutputStatusMessage("The user can access the following Bing Ads accounts: \n");
                foreach (var account in accounts)
                {
                    CustomerManagementExampleHelper.OutputAdvertiserAccount(account);

                    // You can find out which pilot features the customer is able to use. 
                    // Each account could belong to a different customer, so use the customer ID in each account.
                    var featurePilotFlags = (await CustomerManagementExampleHelper.GetCustomerPilotFeaturesAsync(account.ParentCustomerId)).FeaturePilotFlags;
                    OutputStatusMessage("Customer Pilot flags:");
                    OutputStatusMessage(string.Join("; ", featurePilotFlags.Select(flag => string.Format("{0}", flag))));
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.V12.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }
    }
}
