using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.CustomerManagement;
using BingAdsConsoleApp.Properties;
using System.Security.Cryptography;
using BingAdsExamplesLibrary;

namespace BingAdsConsoleApp
{
    class Program
    {
        // Uncomment any examples that you want to run. 
        private static readonly ExampleBase[] _examples =
        {
            //new BingAdsExamplesLibrary.V10.BudgetOpportunities(),
            //new BingAdsExamplesLibrary.V10.BulkServiceManagerDemo(),
            //new BingAdsExamplesLibrary.V10.BulkAdExtensions(),
            //new BingAdsExamplesLibrary.V10.AdExtensions(),
            //new BingAdsExamplesLibrary.V10.BulkKeywordsAds(),
            //new BingAdsExamplesLibrary.V10.KeywordsAds(),
            //new BingAdsExamplesLibrary.V10.BulkNegativeKeywords(),
            //new BingAdsExamplesLibrary.V10.NegativeKeywords(),
            //new BingAdsExamplesLibrary.V10.BulkProductPartitionUpdateBid(),
            //new BingAdsExamplesLibrary.V10.BulkShoppingCampaigns(),
            //new BingAdsExamplesLibrary.V10.ShoppingCampaigns(),
            //new BingAdsExamplesLibrary.V10.BulkTargets(),
            //new BingAdsExamplesLibrary.V10.Targets(),
            //new BingAdsExamplesLibrary.V10.BulkNegativeSites(),
            //new BingAdsExamplesLibrary.GeographicalLocations(),
            new BingAdsExamplesLibrary.V9.SearchUserAccounts(),
            //new BingAdsExamplesLibrary.V9.InviteUser(),
            //new BingAdsExamplesLibrary.V9.CustomerSignup(),
            //new BingAdsExamplesLibrary.V9.ManageClient(),
            //new BingAdsExamplesLibrary.V9.ReportRequests(),
            //new BingAdsExamplesLibrary.V9.BulkAdExtensions(),
            //new BingAdsExamplesLibrary.V9.AdExtensions(),
            //new BingAdsExamplesLibrary.V9.BulkShoppingCampaigns(),
            //new BingAdsExamplesLibrary.V9.ShoppingCampaigns(),
            //new BingAdsExamplesLibrary.V9.BulkTargets(),
            //new BingAdsExamplesLibrary.V9.Targets(),
            //new BingAdsExamplesLibrary.V9.BulkKeywordsAds(),
            //new BingAdsExamplesLibrary.V9.KeywordsAds(),
            //new BingAdsExamplesLibrary.V9.NegativeKeywords(),
            //new BingAdsExamplesLibrary.V9.EstimatedBid(),
            //new BingAdsExamplesLibrary.V9.OptimizeBudget(),

        };

        private static AuthorizationData _authorizationData;
        private static ServiceClient<ICustomerManagementService> _customerService;


        static void Main(string[] args)
        {
            var authentication = new OAuthDesktopMobileAuthCodeGrant(Settings.Default["ClientId"].ToString());
            
            string refreshToken;

            // If you have previously securely stored a refresh token, try to use it.
            if (GetRefreshToken(out refreshToken))
            {
                AuthorizeWithRefreshTokenAsync(authentication, refreshToken).Wait();
            }
            else
            {
                // You must request user consent at least once through a web browser control. 
                // Call the GetAuthorizationEndpoint method of the OAuthDesktopMobileAuthCodeGrant instance that you created above.
                Console.WriteLine(string.Format(
                    "The Bing Ads user must provide consent for your application to access their Bing Ads accounts.\n" +
                    "Open a new web browser and navigate to {0}.\n\n" +
                    "After the user has granted consent in the web browser for the application to access their Bing Ads accounts, " +
                    "please enter the response URI that includes the authorization 'code' parameter: \n", authentication.GetAuthorizationEndpoint()));
                
                // Request access and refresh tokens using the URI that you provided manually during program execution.
                var responseUri = new Uri(Console.ReadLine());
                authentication.RequestAccessAndRefreshTokensAsync(responseUri).Wait();
            }

            // It is important to save the most recent refresh token whenever new OAuth tokens are received. 
            // You will want to subscribe to the NewOAuthTokensReceived event handler. 
            // When calling Bing Ads services with ServiceClient<TService>, BulkServiceManager, or ReportingServiceManager, 
            // each instance will refresh your access token automatically if they detect the AuthenticationTokenExpired (109) error code. 
            authentication.NewOAuthTokensReceived +=
                    (sender, tokens) => SaveRefreshToken(tokens.NewRefreshToken);

            // Most Bing Ads service operations require account and customer ID. This utiltiy operation sets the global 
            // authorization data instance to the first account that the current authenticated user can access. 
            SetAuthorizationDataAsync(authentication).Wait();

            // Run all of the examples that were included above.
            foreach (var example in _examples)
            {
                example.RunAsync(_authorizationData).Wait();
            }
        }

        private static bool GetRefreshToken(out string refreshToken)
        {
            var protectedToken = Settings.Default["RefreshToken"].ToString();

            if (string.IsNullOrEmpty(protectedToken))
            {
                refreshToken = null;
                return false;
            }

            try
            {
                refreshToken = protectedToken.Unprotect();
                return true;
            }
            catch (CryptographicException)
            {
                refreshToken = null;
                return false;
            }
            catch (FormatException)
            {
                refreshToken = null;
                return false;
            }
        }

        private static Task<OAuthTokens> AuthorizeWithRefreshTokenAsync(OAuthDesktopMobileAuthCodeGrant auth, string refreshToken)
        {
            return auth.RequestAccessAndRefreshTokensAsync(refreshToken);
        }

        private static void SaveRefreshToken(string newRefreshtoken)
        {
            if(newRefreshtoken != null)
            {
                Settings.Default["RefreshToken"] = newRefreshtoken.Protect();
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Utility method for setting the customer and account identifiers within the global 
        /// <see cref="_authorizationData"/> instance. 
        /// </summary>
        /// <param name="authentication">The OAuth or Bing Ads managed (UserName/Password) authentication credentials.</param>
        /// <returns></returns>
        private static async Task SetAuthorizationDataAsync(Authentication authentication)
        {
            _authorizationData = new AuthorizationData
            {
                Authentication = authentication,
                DeveloperToken = (Settings.Default["DeveloperToken"] != null) ? Settings.Default["DeveloperToken"].ToString() : null
            };

            _customerService = new ServiceClient<ICustomerManagementService>(_authorizationData);

            var user = await GetUserAsync(null);
            var accounts = await SearchAccountsByUserIdAsync(user.Id);
            if (accounts.Length <= 0) return;

            _authorizationData.AccountId = (long)accounts[0].Id;
            _authorizationData.CustomerId = (int)accounts[0].ParentCustomerId;

            return;
        }

        /// <summary>
        /// Gets a User object by the specified Bing Ads user identifier.
        /// </summary>
        /// <param name="userId">The identifier of the user to get. If null, this operation returns the User object 
        /// corresponding to the current authenticated user of the global customer management ServiceClient.</param>
        /// <returns>The User object corresponding to the specified Bing Ads user identifier.</returns>
        private static async Task<User> GetUserAsync(long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await _customerService.CallAsync((s, r) => s.GetUserAsync(r), request)).User;
        }

        /// <summary>
        /// Searches by UserId for accounts that the user can manage.
        /// </summary>
        /// <param name="userId">The Bing Ads user identifier.</param>
        /// <returns>List of accounts that the user can manage.</returns>
        private static async Task<Account[]> SearchAccountsByUserIdAsync(long? userId)
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

            return (await _customerService.CallAsync((s, r) => s.SearchAccountsAsync(r), request)).Accounts.ToArray();
        }
    }

    /// <summary>
    /// This static class can be used to access and protect a string.
    /// </summary>
    public static class StringProtection
    {
        public static string Protect(this string sourceString)
        {
            var sourceBytes = Encoding.Unicode.GetBytes(sourceString);

            var encryptedBytes = ProtectedData.Protect(sourceBytes, null, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Unprotect(this string protectedString)
        {
            var protectedBytes = Convert.FromBase64String(protectedString);

            var unprotectedBytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(unprotectedBytes);
        }
    }
}
