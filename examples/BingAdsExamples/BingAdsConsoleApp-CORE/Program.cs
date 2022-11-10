using Microsoft.BingAds.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BingAdsExamplesLibrary_CORE;
using Microsoft.BingAds;
using BingAdsExamplesLibrary_CORE.Version13;
using System.ServiceModel;
using Microsoft.BingAds.V13.CustomerManagement;

namespace BingAdsConsoleApp_CORE;

public class Program
{
    private const long DefaultCustomerId = 0;
    private static List<long> DefaultAccountIdList = new() { 0 };
    private static string _developerToken = KeyVault.GetValeuFromKey("WebJobDeveloperToken");
    private static string _refreshToken = KeyVault.GetValeuFromKey("WebJobRefresh-txt");
    private static string _clientId = KeyVault.GetValeuFromKey("WebJobClientId");

    static void Main(string[] args)
    {
        #region This Code Adds the Soap request to the console window 
        ServiceProvider serviceProvider = new ServiceCollection().AddLogging((loggingBuilder) => loggingBuilder.SetMinimumLevel(LogLevel.Trace).AddConsole()).BuildServiceProvider();
        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        TraceBehavior.Instance.AddMessageInspector(new LogMessageInspector(logger, LogLevel.Information));
        #endregion

        try
        {
            #region This is an example of how to loop through multiple accounts at once
            //foreach (var account in DefaultAccountIdList)
            //{
            //    #region Set Account Id & Customer Id for Authentication
            //    Authentication authentication = Authorization.AuthenticateWithOAuth(ApiEnvironment.Production, _clientId, _refreshToken);
            //    AuthorizationData _authorizationData = new AuthorizationData { AccountId = account, Authentication = authentication, CustomerId = DefaultCustomerId, DeveloperToken = _developerToken };
            //    #endregion

            //    #region Create the customer managment service that contains all of the available API methods
            //    CustomerManagementExampleHelper customerManagementExampleHelper = new CustomerManagementExampleHelper(new ServiceClient<Microsoft.BingAds.V13.CustomerManagement.ICustomerManagementService>(_authorizationData, ApiEnvironment.Production));
            //    #endregion

            //    #region Make the Get User API call which will return your user if you do not specify a user id
            //    LogggingBase.OutputStatusMessage("-----\nGetUser:");
            //    var getUserResponse = customerManagementExampleHelper.GetUserAsync(userId: null).Result;
            //    var user = getUserResponse.User;
            //    LogggingBase.OutputStatusMessage("User:");
            //    customerManagementExampleHelper.OutputUser(user);
            //    LogggingBase.OutputStatusMessage("CustomerRoles:");
            //    customerManagementExampleHelper.OutputArrayOfCustomerRole(getUserResponse.CustomerRoles);
            //    #endregion
            //}
            #endregion

            #region Set Account Id & Customer Id for Authentication
            Authentication authentication = Authorization.AuthenticateWithOAuth(ApiEnvironment.Production, _clientId, _refreshToken);
            AuthorizationData _authorizationData = new AuthorizationData { AccountId = DefaultAccountIdList.First(), Authentication = authentication, CustomerId = DefaultCustomerId, DeveloperToken = _developerToken };
            #endregion

            #region Create the customer managment service that contains all of the available API methods
            CustomerManagementExampleHelper customerManagementExampleHelper = new CustomerManagementExampleHelper(new ServiceClient<Microsoft.BingAds.V13.CustomerManagement.ICustomerManagementService>(_authorizationData, ApiEnvironment.Production));
            #endregion

            #region Make the Get User API call which will return your user if you do not specify a user id
            LogggingBase.OutputStatusMessage("-----\nGetUser:");
            var getUserResponse = customerManagementExampleHelper.GetUserAsync(userId: null).Result;
            var user = getUserResponse.User;
            LogggingBase.OutputStatusMessage("User:");
            customerManagementExampleHelper.OutputUser(user);
            LogggingBase.OutputStatusMessage("CustomerRoles:");
            customerManagementExampleHelper.OutputArrayOfCustomerRole(getUserResponse.CustomerRoles);
            #endregion

            #region Search for the accounts that the user can access. To retrieve more than 100 accounts, increase the page sise. To get more than 1000 accounts you will need to add paging. 
            var predicate = new Predicate
            {
                Field = "UserId",
                Operator = PredicateOperator.Equals,
                Value = user.Id.ToString()
            };

            var paging = new Paging
            {
                Index = 0,
                Size = 100
            };

            LogggingBase.OutputStatusMessage("-----\nSearchAccounts:");
            var accounts = customerManagementExampleHelper.SearchAccountsAsync(
                predicates: new[] { predicate },
                ordering: null,
                pageInfo: paging,
                null)?.Result.Accounts.ToArray();
            LogggingBase.OutputStatusMessage("Accounts:");
            customerManagementExampleHelper.OutputArrayOfAdvertiserAccount(accounts);

            HashSet<long> distinctCustomerIds = new HashSet<long>();
            foreach (var account in accounts)
            {
                distinctCustomerIds.Add(account.ParentCustomerId);
            }

            foreach (var customerId in distinctCustomerIds)
            {
                // You can find out which pilot features the customer is able to use. 
                // Each account could belong to a different customer, so use the customer ID in each account.
                LogggingBase.OutputStatusMessage("-----\nGetCustomerPilotFeatures:");
                LogggingBase.OutputStatusMessage(string.Format("Requested by CustomerId: {0}", customerId));
                var featurePilotFlags = (customerManagementExampleHelper.GetCustomerPilotFeaturesAsync(
                    customerId: customerId)).Result.FeaturePilotFlags;
                LogggingBase.OutputStatusMessage("Customer Pilot flags:");
                LogggingBase.OutputStatusMessage(string.Join("; ", featurePilotFlags.Select(flag => string.Format("{0}", flag))));
            }
            #endregion
        }
        // Catch authentication exceptions
        catch (OAuthTokenRequestException ex)
        {
            LogggingBase.OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
        }
        // Catch Customer Management service exceptions
        catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.AdApiFaultDetail> ex)
        {
            LogggingBase.OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
        }
        catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.ApiFault> ex)
        {
            LogggingBase.OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
        }
        catch (Exception ex)
        {
            LogggingBase.OutputStatusMessage(ex.Message);
        }


    }
}
