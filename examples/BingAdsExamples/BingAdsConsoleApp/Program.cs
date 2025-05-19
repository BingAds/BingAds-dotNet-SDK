using System;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.CustomerManagement;
using BingAdsConsoleApp.Properties;
using BingAdsExamplesLibrary;
using System.IO;

using Microsoft.BingAds.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Collections.Generic;

namespace BingAdsConsoleApp
{
    class Program
    {
        // Set any examples that you want to run. 
        private static readonly ExampleBase[] _examples =
        {
            new BingAdsExamplesLibrary.V13.SearchUserAccounts(),
        };

        private static AuthorizationData _authorizationData;
        private static string ClientState = "ClientStateGoesHere";
        private static string DevToken = "BBD37VB98";
        private static string ClientId = "4c0b021c-00c3-4508-838f-d3127e8167ff";

        static void Main(string[] args)
        {
            try
            {                
                using (StreamWriter streamWriter = new StreamWriter(@"tracelog.txt"))
                {
                    streamWriter.AutoFlush = true;

                    // For console output instead of file output, use new TextWriterTraceListener(Console.Out).
                    // If you only need debug output, you can remove the StreamWriter, TraceListener, and AddTraceSource.
                    TraceListener traceListener = new TextWriterTraceListener(streamWriter.BaseStream);

                    IServiceCollection serviceCollection = new ServiceCollection();
                    serviceCollection.AddLogging(builder => builder
                        .AddTraceSource(new SourceSwitch("ProgramSourceSwitch", "verbose"), traceListener)
                        .AddDebug()
                        .AddFilter(level => level >= LogLevel.Debug)
                    );
                    var iLoggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
                    TraceBehavior.Instance.AddMessageInspector(
                        new LogMessageInspector(
                            iLoggerFactory.CreateLogger<Program>(),
                            LogLevel.Information)
                    );

                    Authentication authentication = AuthenticateWithOAuth();

                    // This utiltiy operation sets the global authorization data instance 
                    // to the first account that the current authenticated user can access. 

                    SetAuthorizationDataAsync(authentication).Wait();

                    // Run all of the examples that are included above.

                    foreach (var example in _examples)
                    {
                        example.RunAsync(_authorizationData).Wait();
                    }

                    streamWriter.Flush();
                    traceListener.Flush();
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
            catch (FaultException<ApiFault> ex)
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
                ClientId,
                apiEnvironment
            );

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
                var codeVerifier = "mycodeverifiermycodeverifiermycodeverifiermycodeverifiermycodeverifier";
                Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                
                Console.WriteLine(string.Format(
                    "Open a new web browser and navigate to {0}\n\n" +
                    "Grant consent in the web browser for the application to access " +
                    "your advertising accounts, and then enter the response URI that includes " +
                    "the authorization 'code' parameter: \n", 
                    oAuthDesktopMobileAuthCodeGrant.GetAuthorizationEndpoint() +
                    "&code_challenge_method=plain&code_challenge=" + codeVerifier)
                );
                
                // After consent has been granted, read the reponse URI that should contain the authorization code.
                var responseUri = new Uri(Console.ReadLine());

                if (oAuthDesktopMobileAuthCodeGrant.State != ClientState)
                    throw new HttpRequestException("The OAuth response state does not match the client request state.");

                // Request access and refresh tokens.
                var additionalParams = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>(
                        "code_verifier",
                        codeVerifier)
                };
                oAuthDesktopMobileAuthCodeGrant.RequestAccessAndRefreshTokensAsync(responseUri, additionalParams).Wait();

                // Store the refresh token for future use as needed. 
                SaveRefreshToken(oAuthDesktopMobileAuthCodeGrant.OAuthTokens.RefreshToken);
            }

            // It is important to save the most recent refresh token whenever new OAuth tokens are received. 
            // You will want to subscribe to the NewOAuthTokensReceived event handler. 
            // Each instance of ServiceClient<TService>, BulkServiceManager, or ReportingServiceManager 
            // will refresh your access token automatically if they detect the AuthenticationTokenExpired (109) error code. 
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
                DeveloperToken = DevToken
            };

            ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)_authorizationData.Authentication).Environment;

            BingAdsExamplesLibrary.V13.CustomerManagementExampleHelper CustomerManagementExampleHelper = 
                new BingAdsExamplesLibrary.V13.CustomerManagementExampleHelper(
                    OutputStatusMessageDefault: null);
            CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(
                    authorizationData: _authorizationData, 
                    environment: environment);

            var getUserResponse = await CustomerManagementExampleHelper.GetUserAsync(
                    userId: null);
            var user = getUserResponse.User;

            // Search for the accounts that the user can access.
            // To retrieve more than 100 accounts, increase the page size up to 1,000.
            // To retrieve more than 1,000 accounts you'll need to add paging.

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

            var request = new SearchAccountsRequest
            {
                Ordering = null,
                PageInfo = paging,
                Predicates = new[] { predicate }
            };

            var accounts = (await CustomerManagementExampleHelper.SearchAccountsAsync(
                    predicates: new[] { predicate },
                    ordering: null,
                    pageInfo: paging,
                    returnAdditionalFields: null))?.Accounts.ToArray();
            
            if (accounts.Length <= 0) return;

            _authorizationData.AccountId = (long)accounts[0].Id;
            _authorizationData.CustomerId = (long)accounts[0].ParentCustomerId;

            return;
        }
    }
}
