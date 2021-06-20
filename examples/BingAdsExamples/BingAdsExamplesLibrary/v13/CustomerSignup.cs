using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CustomerManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How a reseller can call SignupCustomer to create a new customer and account.
    /// </summary>
    public class CustomerSignup : ExampleBase
    {
        public override string Description
        {
            get { return "Create new customer for reseller | Customer Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CustomerManagementExampleHelper CustomerManagementExampleHelper = new CustomerManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                OutputStatusMessage("-----\nGetUser:");
                var getUserResponse = await CustomerManagementExampleHelper.GetUserAsync(
                    userId: null);
                var user = getUserResponse.User;
                OutputStatusMessage("User:");
                CustomerManagementExampleHelper.OutputUser(user);
                OutputStatusMessage("CustomerRoles:");
                CustomerManagementExampleHelper.OutputArrayOfCustomerRole(getUserResponse.CustomerRoles);

                // Only a user with the aggregator role (33) can sign up new customers. 
                // If the user does not have the aggregator role, then do not continue.    
                
                if (!getUserResponse.CustomerRoles.Select(role => role.RoleId).Contains(33))
                {
                    OutputStatusMessage("Only a user with the aggregator role (33) can sign up new customers.");
                    return;
                }
                
                var customer = new Customer
                {
                    // The primary business segment of the customer, for example, automotive, food, or entertainment.
                    Industry = Industry.Other,

                    // The primary country where the customer operates. 
                    MarketCountry = "US",

                    // The primary language that the customer uses. 
                    MarketLanguage = LanguageType.English,

                    // The name of the customer. 
                    Name = "Child Customer " + DateTime.UtcNow,
                };
                
                var account = new AdvertiserAccount
                {
                    // The location where your business is legally registered. 
                    // The business address is used to determine your tax requirements.
                    BusinessAddress = new Address
                    {
                        BusinessName = "Contoso",
                        City = "Redmond",
                        Line1 = "One Microsoft Way",
                        CountryCode = "US",
                        PostalCode = "98052",
                        StateOrProvince = "WA",                        
                    },

                    // The type of currency that is used to settle the account. 
                    // The service uses the currency information for billing purposes.
                    CurrencyCode = CurrencyCode.USD,

                    // The name of the account. 
                    Name = "Child Account " + DateTime.UtcNow,

                    // The identifier of the customer that owns the account. 
                    ParentCustomerId = (long)user.CustomerId,

                    // The TaxId (VAT identifier) is optional. If specified, The VAT identifier must be valid 
                    // in the country that you specified in the BusinessAddress element. Without a VAT registration 
                    // number or exemption certificate, taxes might apply based on your business location.
                    TaxInformation = null,

                    // The default time-zone for campaigns in this account.
                    TimeZone = TimeZoneType.PacificTimeUSCanadaTijuana,
                };

                // Signup a new customer and account for the reseller. 
                OutputStatusMessage("-----\nSignupCustomer:");
                var signupCustomerResponse = await CustomerManagementExampleHelper.SignupCustomerAsync(
                    customer: customer,
                    account: account,
                    parentCustomerId: user.CustomerId,
                    userInvitation: null,
                    userId: null);

                OutputStatusMessage("New Customer and Account:");

                // This is the identifier that you will use to set the CustomerId 
                // element in most of the Bing Ads API service operations.
                OutputStatusMessage(string.Format("\tCustomerId: {0}", signupCustomerResponse.CustomerId));

                // The read-only system-generated customer number that is used in the Microsoft Advertising web application. 
                // The customer number is of the form, Cnnnnnnn, where nnnnnnn is a series of digits.
                OutputStatusMessage(string.Format("\tCustomerNumber: {0}", signupCustomerResponse.CustomerNumber));

                // This is the identifier that you will use to set the AccountId and CustomerAccountId 
                // elements in most of the Bing Ads API service operations.
                OutputStatusMessage(string.Format("\tAccountId: {0}", signupCustomerResponse.AccountId));

                // The read-only system generated account number that is used to identify the account in the Microsoft Advertising web application. 
                // The account number has the form xxxxxxxx, where xxxxxxxx is a series of any eight alphanumeric characters.
                OutputStatusMessage(string.Format("\tAccountNumber: {0}", signupCustomerResponse.AccountNumber));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.ApiFault> ex)
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
