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
    /// This example demonstrates how a reseller can call SignupCustomer to create a new customer and account.
    /// </summary>
    public class CustomerSignup : ExampleBase
    {
        public static ServiceClient<ICustomerManagementService> Service;

        public override string Description
        {
            get { return "Create new customer for reseller | Customer Management V9"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICustomerManagementService>(authorizationData);

                var getUserResponse = await GetUserAsync(null);
                var user = getUserResponse.User;
                
                // Only a user with the aggregator role (33) can sign up new customers. 
                // If the user does not have the aggregator role, then do not continue.
                if (!getUserResponse.Roles.Contains(33))
                {
                    OutputStatusMessage("Only a user with the aggregator role (33) can sign up new customers.");
                    return;
                }

                // For Customer.CustomerAddress and Account.BusinessAddress, you can use the same address 
                // as your aggregator user, although you must set Id and TimeStamp to null.
                var userAddress = user.ContactInfo.Address;
                userAddress.Id = null;
                userAddress.TimeStamp = null;

                var customer = new Customer
                {
                    // The customer's business address.
                    CustomerAddress = userAddress,

                    // The list of key and value strings for forward compatibility. This element can be used
                    // to avoid otherwise breaking changes when new elements are added in future releases.
                    // There are currently no forward compatibility changes for the Customer object.
                    ForwardCompatibilityMap = null,

                    // The primary business segment of the customer, for example, automotive, food, or entertainment.
                    Industry = Industry.Other,

                    // The primary country where the customer operates. This country will be the 
                    // default country for ad groups in the customer’s campaigns.
                    MarketCountry = "US",

                    // The primary language that the customer uses. This language will be the 
                    // default language for ad groups in the customer’s campaigns.
                    MarketLanguage = LanguageType.English,

                    // The name of the customer. This element can contain a maximum of 100 characters.
                    Name = "Child Customer " + DateTime.UtcNow,
                };

                // Optionally you can set up each account with auto tagging.
                // The AutoTag key and value pair is an account level setting that determines whether to append or replace 
                // the supported UTM tracking codes within the final URL of ads delivered. The default value is '0', and
                // Bing Ads will not append any UTM tracking codes to your ad or keyword final URL.
                var accountFCM = new List<KeyValuePair<string, string>>();
                accountFCM.Add(new KeyValuePair<string, string>(
                    "AutoTag", 
                    "0"));

                var account = new AdvertiserAccount
                {
                    // The type of account. Bing Ads API only supports the Advertiser account.
                    AccountType = AccountType.Advertiser,

                    // The location where your business is legally registered. 
                    // The business address is used to determine your tax requirements.
                    // BusinessAddress will be required in a future version of the Bing Ads API.
                    // Please start using it.
                    BusinessAddress = userAddress,

                    // The type of currency that is used to settle the account. The service uses the currency information for billing purposes.
                    CurrencyType = CurrencyType.USDollar,

                    // The list of key and value strings for forward compatibility. This element can be used
                    // to avoid otherwise breaking changes when new elements are added in future releases.
                    ForwardCompatibilityMap = accountFCM,

                    // The name of the account. The name can contain a maximum of 100 characters and must be unique within the customer.
                    Name = "Child Account " + DateTime.UtcNow,

                    // The identifier of the customer that owns the account. In the Bing Ads API operations 
                    // that require a customer identifier, this is the identifier that you set the CustomerId SOAP header to.
                    ParentCustomerId = (long)user.CustomerId,

                    // The TaxId (VAT identifier) is optional. If specified, The VAT identifier must be valid 
                    // in the country that you specified in the BusinessAddress element. Without a VAT registration 
                    // number or exemption certificate, taxes might apply based on your business location.
                    TaxId = null,

                    // The default time-zone value to use for campaigns in this account.
                    // If not specified, the time zone will be set to PacificTimeUSCanadaTijuana by default.
                    // TimeZone will be required in a future version of the Bing Ads API.
                    // Please start using it.
                    TimeZone = TimeZoneType.PacificTimeUSCanadaTijuana,
                };

                // Signup a new customer and account for the reseller. 
                var signupCustomerResponse = await SignupCustomerAsync(
                    customer,
                    account,
                    user.CustomerId);

                OutputStatusMessage(string.Format("New Customer and Account:\n"));

                // This is the identifier that you will use to set the CustomerId 
                // element in most of the Bing Ads API service operations.
                OutputStatusMessage(string.Format("\tCustomerId: {0}", signupCustomerResponse.CustomerId));

                // The read-only system-generated customer number that is used in the Bing Ads web application. 
                // The customer number is of the form, Cnnnnnnn, where nnnnnnn is a series of digits.
                OutputStatusMessage(string.Format("\tCustomerNumber: {0}", signupCustomerResponse.CustomerNumber));

                // This is the identifier that you will use to set the AccountId and CustomerAccountId 
                // elements in most of the Bing Ads API service operations.
                OutputStatusMessage(string.Format("\tAccountId: {0}", signupCustomerResponse.AccountId));

                // The read-only system generated account number that is used to identify the account in the Bing Ads web application. 
                // The account number has the form xxxxxxxx, where xxxxxxxx is a series of any eight alphanumeric characters.
                OutputStatusMessage(string.Format("\tAccountNumber: {0}", signupCustomerResponse.AccountNumber));
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
        /// Gets a Bing Ads user and user roles by the specified Bing Ads user identifier.
        /// </summary>
        /// <param name="userId">The identifier of the user to get. If null, this operation returns the User object 
        /// corresponding to the current authenticated user of the global customer management ServiceClient.</param>
        /// <returns>The GetUserResponse object corresponding to the specified Bing Ads user identifier.</returns>
        private async Task<GetUserResponse> GetUserAsync(long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await Service.CallAsync((s, r) => s.GetUserAsync(r), request));
        }

        /// <summary>
        /// Creates a new child customer and account that rolls up to the reseller's billing invoice.
        /// </summary>
        /// <param name="customer">The new child customer.</param>
        /// <param name="account">The account within the new customer.</param>
        /// <param name="parentCustomerId">The customer identifier of the reseller that will manage this customer.</param>
        /// <returns></returns>
        private async Task<SignupCustomerResponse> SignupCustomerAsync(
            Customer customer, 
            Account account,
            long? parentCustomerId)
        {
            var request = new SignupCustomerRequest
            {
                Customer = customer,
                Account = account,
                ParentCustomerId = parentCustomerId,
            };

            return (await Service.CallAsync((s, r) => s.SignupCustomerAsync(r), request));
        }
    }
}
