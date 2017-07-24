using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using BingAdsWpfApp.Properties;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.CustomerManagement;
using BingAdsExamplesLibrary;

namespace BingAdsWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly ExampleBase[] _examples =
        {
            // Current examples
            
            new BingAdsExamplesLibrary.V11.KeywordPlanner(),
            new BingAdsExamplesLibrary.V11.BudgetOpportunities(),
            new BingAdsExamplesLibrary.V11.BulkServiceManagerDemo(),
            new BingAdsExamplesLibrary.V11.BulkAdExtensions(),
            new BingAdsExamplesLibrary.V11.AdExtensions(),
            new BingAdsExamplesLibrary.V11.BulkKeywordsAds(),
            new BingAdsExamplesLibrary.V11.KeywordsAds(),
            new BingAdsExamplesLibrary.V11.BulkNegativeKeywords(),
            new BingAdsExamplesLibrary.V11.NegativeKeywords(),
            new BingAdsExamplesLibrary.V11.BulkAdGroupUpdate(),
            new BingAdsExamplesLibrary.V11.BulkProductPartitionUpdateBid(),
            new BingAdsExamplesLibrary.V11.ConversionGoals(),
            new BingAdsExamplesLibrary.V11.BulkRemarketingLists(),
            new BingAdsExamplesLibrary.V11.RemarketingLists(),
            new BingAdsExamplesLibrary.V11.BulkShoppingCampaigns(),
            new BingAdsExamplesLibrary.V11.ShoppingCampaigns(),
            new BingAdsExamplesLibrary.V11.DynamicSearchCampaigns(),
            new BingAdsExamplesLibrary.V11.BulkTargetCriterions(),
            new BingAdsExamplesLibrary.V11.TargetCriterions(),
            new BingAdsExamplesLibrary.V11.GeographicalLocations(),
            new BingAdsExamplesLibrary.V11.BulkNegativeSites(),
            new BingAdsExamplesLibrary.V11.SearchUserAccounts(),
            new BingAdsExamplesLibrary.V11.InviteUser(),
            new BingAdsExamplesLibrary.V11.CustomerSignup(),
            new BingAdsExamplesLibrary.V11.ManageClient(),
            new BingAdsExamplesLibrary.V11.ReportRequests(),

            // Deprected examples

            new BingAdsExamplesLibrary.V10.BudgetOpportunities(),
            new BingAdsExamplesLibrary.V10.BulkServiceManagerDemo(),
            new BingAdsExamplesLibrary.V10.BulkAdExtensions(),
            new BingAdsExamplesLibrary.V10.AdExtensions(),
            new BingAdsExamplesLibrary.V10.BulkKeywordsAds(),
            new BingAdsExamplesLibrary.V10.KeywordsAds(),
            new BingAdsExamplesLibrary.V10.BulkNegativeKeywords(),
            new BingAdsExamplesLibrary.V10.NegativeKeywords(),
            new BingAdsExamplesLibrary.V10.BulkProductPartitionUpdateBid(),
            new BingAdsExamplesLibrary.V10.ConversionGoals(),
            new BingAdsExamplesLibrary.V10.BulkRemarketingLists(),
            new BingAdsExamplesLibrary.V10.RemarketingLists(),
            new BingAdsExamplesLibrary.V10.BulkShoppingCampaigns(),
            new BingAdsExamplesLibrary.V10.ShoppingCampaigns(),
            new BingAdsExamplesLibrary.V10.DynamicSearchCampaigns(),
            new BingAdsExamplesLibrary.V10.BulkFindSharedTargetCriterions(),
            new BingAdsExamplesLibrary.V10.BulkTargets(),
            new BingAdsExamplesLibrary.V10.Targets(),
            new BingAdsExamplesLibrary.V10.GeographicalLocations(),
            new BingAdsExamplesLibrary.V10.BulkNegativeSites(),
            new BingAdsExamplesLibrary.V9.SearchUserAccounts(),
            new BingAdsExamplesLibrary.V9.InviteUser(),
            new BingAdsExamplesLibrary.V9.CustomerSignup(),
            new BingAdsExamplesLibrary.V9.ManageClient(),
            new BingAdsExamplesLibrary.V9.ReportRequests(),
        };

        private static long?[,] _accountCustomerIds;

        private static AuthorizationData _authorizationData;
        private static ServiceClient<ICustomerManagementService> _customerService;

        private delegate void SendStatusMessageDelegate(string message);

        
        public MainWindow()
        {
            InitializeComponent();
            
            foreach (var example in _examples)
            {
                ExamplesComboBox.Items.Add(example.Description);
            }

            ExamplesComboBox.SelectedIndex = 0;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AuthenticateUser();
        }

        /// <summary>
        /// Prompt for user credentials and then call the Bing Ads Customer Management service 
        /// with the current authenticated Microsoft account user.
        /// </summary>
        private async void AuthenticateUser()
        {
            try
            {
                Authentication authentication;

                if (OAuthCheckBox.IsChecked == true)
                {
                    authentication = await OAuthHelper.AuthorizeDesktopMobileAuthCodeGrant();
                    var authenticationToken =
                        ((OAuthDesktopMobileAuthCodeGrant) (authentication)).OAuthTokens.AccessToken;
                }
                else
                {
                    authentication = new PasswordAuthentication(UserNameTextBox.Text,
                        UserNamePasswordBox.Password);
                }

                ClearUserData();

                // Get user's CustomerId and AccountId

                _authorizationData = new AuthorizationData
                {
                    Authentication = authentication,
                    DeveloperToken =
                        (SandboxCheckBox.IsChecked == false)
                            ? Settings.Default["DeveloperToken"].ToString()
                            : Settings.Default["DeveloperTokenSandbox"].ToString()
                };

                _customerService = new ServiceClient<ICustomerManagementService>(_authorizationData);

                var user = await GetUserAsync(null);

                UserNameTextBox.Text = user.UserName;

                // Search for the accounts that matches the specified criteria.

                var accounts = await SearchAccountsByUserIdAsync(user.Id);

                // Store the parent customer identifier in the second dimension of the array.

                _accountCustomerIds = new long?[accounts.Length, 2];

                for (var i = 0; i < accounts.Length; i++)
                {
                    AccountIdsComboBox.Items.Add(accounts[i].Id);
                    _accountCustomerIds[i, 0] = accounts[i].Id;
                    _accountCustomerIds[i, 1] = accounts[i].ParentCustomerId;
                }

                AccountIdsComboBox.SelectedIndex = 0;
                SetAuthorizationDataByAccountIndex(AccountIdsComboBox.SelectedIndex);

                if (accounts.Length > 0) RunButton.IsEnabled = true;
            }
                // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}",
                    ex.Details.Error, ex.Details.Description));
            }
                // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.CustomerManagement.AdApiFaultDetail> ex)
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
            catch (FaultException<Microsoft.BingAds.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ",
                    ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (HttpRequestException ex)
            {
                OutputStatusMessage(ex.Message);
            }
            finally
            {
                SwitchUserButton.IsEnabled = true;
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

            return (await _customerService.CallAsync((s, r) => s.GetUserAsync(r), request)).User;
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

            return (await _customerService.CallAsync((s, r) => s.SearchAccountsAsync(r), request)).Accounts.ToArray();
        }

        /// <summary>
        /// Executes the specified Bing Ads code example.
        /// </summary>
        /// <param name="example">The Bing Ads code example to execute.</param>
        public async void ExecuteExample(ExampleBase example)
        {
            try
            {
                OutputStatusMessage(string.Format("Running example: {0}\n", example.ExampleName));
                OutputStatusMessage(string.Format("Description: {0}\n", example.Description));

                await example.RunAsync(_authorizationData);

                OutputStatusMessage(string.Format("\nExample finished running: {0}\n", example.ExampleName));
            }
            
                // The example's RunAsync method should catch exceptions specific to the corresponding Bing Ads services.
                // Catch application exceptions that were not caught within the specified example. 
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        private void OutputStatusMessage(String msg)
        {
            var splits = msg.Split('\n');
            foreach (var split in splits)
            {
                var trimmed = split.Trim('\r');

                // Remember that OutputStatusMessage is called from HandleReceiveMessage, which is assigned to a delegate of the example base class.
                // Since the example base class is executed outside of the main thread i.e. RunAsync, you must check to make sure this thread
                // has access to components of the application window i.e. OutputScrollViewer. 
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new SendStatusMessageDelegate(OutputStatusMessage), msg);
                    return;
                }

                OutputScrollViewer.Content += (trimmed + "\r\n");
                OutputScrollViewer.ScrollToBottom();
            }
        }

        /// <summary>
        /// Displays the message to this application's user interface
        /// </summary>
        /// <param name="msg">The message to display.</param>
        private void HandleReceiveMessage(String msg)
        {
            Debug.WriteLine(msg);
            OutputStatusMessage(msg);
        }


        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExamplesComboBox.SelectedIndex < 0 || ExamplesComboBox.SelectedIndex >= _examples.Length)
            {
                return;
            }

            RunButton.IsEnabled = false;
            SwitchUserButton.IsEnabled = false;
            OutputScrollViewer.Content = "";
            
            // Determines which example will be run.
            var example = _examples[ExamplesComboBox.SelectedIndex];

            // Messages received from the example will be handled by delegate.
            ExampleBase.SendMessageDelegate handler = HandleReceiveMessage;
            example.OutputStatusMessage = handler;

            ExecuteExample(example);

            RunButton.IsEnabled = true;
            SwitchUserButton.IsEnabled = true;
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void SwitchUserButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.RefreshToken = "";
            Settings.Default.Save();

            AuthenticateUser();
        }

        private void ClearUserData()
        {
            RunButton.IsEnabled = false;
            OutputScrollViewer.Content = "";
            UserNameTextBox.Text = "";
            UserNamePasswordBox.Password = "";
            CustomerIdLabel.Content = "";
            AccountIdsComboBox.Items.Clear();

            _authorizationData = null;
        }
        
        private void AccountIdsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetAuthorizationDataByAccountIndex(AccountIdsComboBox.SelectedIndex);
        }

        /// <summary>
        /// Utility method for setting the customer and account identifiers within the global 
        /// <see cref="_authorizationData"/> instance. 
        /// </summary>
        /// <param name="accountIndex">The accountIndex of the account within the <see cref="_accountCustomerIds"/>
        /// multi-dimensional array.</param>
        private void SetAuthorizationDataByAccountIndex(int accountIndex)
        {
            if (accountIndex < 0 || accountIndex > _accountCustomerIds.Length) return;

            var accountId = _accountCustomerIds[accountIndex, 0];
            var customerId = _accountCustomerIds[accountIndex, 1];

            if (accountId == null || customerId == null) return;

            _authorizationData.AccountId = (long)accountId;
            _authorizationData.CustomerId = (int)customerId;

            CustomerIdLabel.Content = _authorizationData.CustomerId;
        }

        private void OAuthCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ClearUserData();
            UserNameTextBox.IsEnabled = false;
            UserNamePasswordBox.IsEnabled = false;
            SwitchUserButton.Content = "Microsoft Account Login";

            if (SandboxCheckBox != null)
            {
                SandboxCheckBox.IsEnabled = false;
            }
        }

        private void OAuthCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ClearUserData();
            UserNameTextBox.IsEnabled = true;
            UserNamePasswordBox.IsEnabled = true;
            SwitchUserButton.Content = "UserName Login";

            if (SandboxCheckBox != null)
            {
                SandboxCheckBox.IsEnabled = true;
            }
        }

        private void ExamplesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            OutputScrollViewer.Content = "";
        }

        private void SandboxCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Overrides app.config to set the Bing Ads environment. 
            // The default environment is production, unless set exactly to "Sandbox". 
            ConfigurationManager.AppSettings["BingAdsEnvironment"] = "Sandbox";

            OAuthCheckBox.IsEnabled = false;
        }

        private void SandboxCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Overrides app.config to set the Bing Ads environment. 
            // The default environment is production, unless set exactly to "Sandbox". 
            ConfigurationManager.AppSettings["BingAdsEnvironment"] = "Production";
            
            OAuthCheckBox.IsEnabled = true;
        }
    }
}
