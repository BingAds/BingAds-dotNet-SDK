using System;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.CustomerManagement;
using BingAdsConsoleApp.Properties;
using BingAdsExamplesLibrary;
using System.IO;

namespace BingAdsConsoleApp
{
    class Program
    {
        // Uncomment any examples that you want to run. 
        private static readonly ExampleBase[] _examples =
        {
            /*
            new BingAdsExamplesLibrary.V12.Labels(),
            new BingAdsExamplesLibrary.V12.OfflineConversions(),
            new BingAdsExamplesLibrary.V12.KeywordPlanner(),
            new BingAdsExamplesLibrary.V12.BudgetOpportunities(),
            new BingAdsExamplesLibrary.V12.BulkServiceManagerDemo(),
            new BingAdsExamplesLibrary.V12.BulkAdExtensions(),
            new BingAdsExamplesLibrary.V12.AdExtensions(),
            new BingAdsExamplesLibrary.V12.BulkKeywordsAds(),
            new BingAdsExamplesLibrary.V12.KeywordsAds(),
            new BingAdsExamplesLibrary.V12.BulkNegativeKeywords(),
            new BingAdsExamplesLibrary.V12.NegativeKeywords(),
            new BingAdsExamplesLibrary.V12.BulkAdGroupUpdate(),
            new BingAdsExamplesLibrary.V12.BulkProductPartitionUpdateBid(),
            new BingAdsExamplesLibrary.V12.ConversionGoals(),
            new BingAdsExamplesLibrary.V12.BulkRemarketingLists(),
            new BingAdsExamplesLibrary.V12.RemarketingLists(),
            new BingAdsExamplesLibrary.V12.BulkShoppingCampaigns(),
            new BingAdsExamplesLibrary.V12.ShoppingCampaigns(),
            new BingAdsExamplesLibrary.V12.DynamicSearchCampaigns(),
            new BingAdsExamplesLibrary.V12.BulkTargetCriterions(),
            new BingAdsExamplesLibrary.V12.TargetCriterions(),
            new BingAdsExamplesLibrary.V12.GeographicalLocations(),
            new BingAdsExamplesLibrary.V12.BulkNegativeSites(),
            new BingAdsExamplesLibrary.V12.InviteUser(),
            new BingAdsExamplesLibrary.V12.CustomerSignup(),
            new BingAdsExamplesLibrary.V12.ManageClient(),
            new BingAdsExamplesLibrary.V12.ReportRequests(),
            */
            new BingAdsExamplesLibrary.V12.SearchUserAccounts(),
        };

        private static AuthorizationData _authorizationData;
        private static string ClientState = "ClientStateGoesHere";

        static void Main(string[] args)
        {
            try
            {
                Authentication authentication = AuthenticateWithOAuth();

                // Most Bing Ads service operations require account and customer ID. 
                // This utiltiy operation sets the global authorization data instance 
                // to the first account that the current authenticated user can access. 
                SetAuthorizationDataAsync(authentication).Wait();

                // Run all of the examples that were included above.
                foreach (var example in _examples)
                {
                    example.RunAsync(_authorizationData).Wait();
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("OAuthTokenRequestException Message:\n{0}", ex.Message));
                if (ex.Details != null)
                {
                    OutputStatusMessage(string.Format("OAuthTokenRequestException Details:\nError: {0}\nDescription: {1}",
                    ex.Details.Error, ex.Details.Description));
                }
            }
            // Catch Customer Management service exceptions
            catch (FaultException<AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error =>
                {
                    if ((error.Code == 105) || (error.Code == 106))
                    {
                        return "Authorization data is missing or incomplete for the specified environment.\n" +
                               "To run the examples switch users or contact support for help with the following error.\n";
                    }
                    return string.Format("{0}: {1}", error.Code, error.Message);
                })));
                OutputStatusMessage(string.Join("; ",
                    ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ",
                    ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (HttpRequestException ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        private static Authentication AuthenticateWithOAuth()
        {
            var apiEnvironment = 
                ConfigurationManager.AppSettings["BingAdsEnvironment"] == ApiEnvironment.Sandbox.ToString() ?
                ApiEnvironment.Sandbox : ApiEnvironment.Production;
            var oAuthDesktopMobileAuthCodeGrant = new OAuthDesktopMobileAuthCodeGrant(
                Settings.Default["ClientId"].ToString(),
                apiEnvironment);

            // It is recommended that you specify a non guessable 'state' request parameter to help prevent
            // cross site request forgery (CSRF). 
            oAuthDesktopMobileAuthCodeGrant.State = ClientState;

            string refreshToken;

            // If you have previously securely stored a refresh token, try to use it.
            if (GetRefreshToken(out refreshToken))
            {
                AuthorizeWithRefreshTokenAsync(oAuthDesktopMobileAuthCodeGrant, refreshToken).Wait();
            }
            else
            {
                // You must request user consent at least once through a web browser control. 
                // Call the GetAuthorizationEndpoint method of the OAuthDesktopMobileAuthCodeGrant instance that you created above.
                Console.WriteLine(string.Format(
                    "The Bing Ads user must provide consent for your application to access their Bing Ads accounts.\n" +
                    "Open a new web browser and navigate to {0}.\n\n" +
                    "After the user has granted consent in the web browser for the application to access " +
                    "their Bing Ads accounts, please enter the response URI that includes " +
                    "the authorization 'code' parameter: \n", oAuthDesktopMobileAuthCodeGrant.GetAuthorizationEndpoint()));

                // Request access and refresh tokens using the URI that you provided manually during program execution.
                var responseUri = new Uri(Console.ReadLine());

                if (oAuthDesktopMobileAuthCodeGrant.State != ClientState)
                    throw new HttpRequestException("The OAuth response state does not match the client request state.");

                oAuthDesktopMobileAuthCodeGrant.RequestAccessAndRefreshTokensAsync(responseUri).Wait();
                SaveRefreshToken(oAuthDesktopMobileAuthCodeGrant.OAuthTokens.RefreshToken);
            }

            // It is important to save the most recent refresh token whenever new OAuth tokens are received. 
            // You will want to subscribe to the NewOAuthTokensReceived event handler. 
            // When calling Bing Ads services with ServiceClient<TService>, BulkServiceManager, or ReportingServiceManager, 
            // each instance will refresh your access token automatically if they detect the AuthenticationTokenExpired (109) error code. 
            oAuthDesktopMobileAuthCodeGrant.NewOAuthTokensReceived +=
                    (sender, tokens) => SaveRefreshToken(tokens.NewRefreshToken);

            return oAuthDesktopMobileAuthCodeGrant;
        }

        /// <summary>
        /// Write to the console by default.
        /// </summary>
        /// <param name="msg">The message sent to console output.</param>
        private static void OutputStatusMessage(String msg)
        {
            Console.WriteLine(msg);
        }

        private static bool GetRefreshToken(out string refreshToken)
        {
            var filePath = Environment.CurrentDirectory + @"\refreshtoken.txt";
            if (!File.Exists(filePath))
            {
                refreshToken = null;
                return false;
            }

            String fileContents;
            using (StreamReader sr = new StreamReader(filePath))
            {
                fileContents = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(fileContents))
            {
                refreshToken = null;
                return false;
            }

            try
            {
                refreshToken = fileContents;
                return true;
            }
            catch (FormatException)
            {
                refreshToken = null;
                return false;
            }
        }

        private static Task<OAuthTokens> AuthorizeWithRefreshTokenAsync(OAuthDesktopMobileAuthCodeGrant authentication, string refreshToken)
        {
            return authentication.RequestAccessAndRefreshTokensAsync(refreshToken);
        }

        private static void SaveRefreshToken(string newRefreshtoken)
        {
            if (newRefreshtoken != null)
            {
                using (StreamWriter outputFile = new StreamWriter(
                Environment.CurrentDirectory + @"\refreshtoken.txt",
                false))
                {
                    outputFile.WriteLine(newRefreshtoken);
                }
            }
        }

        /// <summary>
        /// Utility method for setting the customer and account identifiers within the global 
        /// <see cref="_authorizationData"/> instance. 
        /// </summary>
        /// <param name="authentication">The OAuth authentication credentials.</param>
        /// <returns></returns>
        private static async Task SetAuthorizationDataAsync(Authentication authentication)
        {
            _authorizationData = new AuthorizationData
            {
                Authentication = authentication,
                DeveloperToken = Settings.Default["DeveloperToken"].ToString()
            };

            BingAdsExamplesLibrary.V12.CustomerManagementExampleHelper CustomerManagementExampleHelper = 
                new BingAdsExamplesLibrary.V12.CustomerManagementExampleHelper(null);
            CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(_authorizationData);

            var getUserResponse = await CustomerManagementExampleHelper.GetUserAsync(null);
            var user = getUserResponse.User;

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
                paging)).Accounts.ToArray();
            
            if (accounts.Length <= 0) return;

            _authorizationData.AccountId = (long)accounts[0].Id;
            _authorizationData.CustomerId = (int)accounts[0].ParentCustomerId;

            return;
        }
    }
}
