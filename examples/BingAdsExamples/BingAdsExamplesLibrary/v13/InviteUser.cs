using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CustomerManagement;
using Microsoft.BingAds;
using System.Globalization;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to invite a user to manage your advertising accounts.
    /// </summary>
    public class InviteUser : ExampleBase
    {
        // Specify the email address where the invitation should be sent. 
        // The recipient can accept the invitation and sign up 
        // with credentials that differ from the invitation email address.

        const string UserInviteRecipientEmail = "UserInviteRecipientEmailGoesHere";
        
        public override string Description
        {
            get { return "Invite a User to Manage accounts | Customer Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                OutputStatusMessage("You must edit this example to provide the email address (UserInviteRecipientEmail) for " +
                                    "the user invitation.");
                OutputStatusMessage("You must use Super Admin credentials to send a user invitation.");

                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CustomerManagementExampleHelper CustomerManagementExampleHelper = new CustomerManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                // Prepare to invite a new user
                var userInvitation = new UserInvitation
                {
                    // The identifier of the customer this user is invited to manage. 
                    // The AccountIds element determines which customer accounts the user can manage.
                    CustomerId = authorizationData.CustomerId,

                    // Users with account level roles such as Advertiser Campaign Manager can be restricted to specific accounts. 
                    // Users with customer level roles such as Super Admin can access all accounts within the user's customer, 
                    // and their access cannot be restricted to specific accounts.
                    AccountIds = null,

                    // The user role, which determines the level of access that the user has to the accounts specified in the AccountIds element.
                    // The identifier for an advertiser campaign manager is 16.
                    RoleId = 16,

                    // The email address where the invitation should be sent. 
                    Email = UserInviteRecipientEmail,

                    // The first name of the user. 
                    FirstName = "FirstNameGoesHere",

                    // The last name of the user. 
                    LastName = "LastNameGoesHere",

                    // The locale to use when sending correspondence to the user by email or postal mail. The default is EnglishUS.
                    Lcid = LCID.EnglishUS,
                };

                // Once you send a user invitation, there is no option to rescind the invitation using the API.
                // You can delete a pending invitation in the Accounts & Billing -> Users tab of the Microsoft Advertising web application. 

                OutputStatusMessage("-----\nSendUserInvitation:");
                var userInvitationId = (await CustomerManagementExampleHelper.SendUserInvitationAsync(
                    userInvitation: userInvitation))?.UserInvitationId;
                OutputStatusMessage(string.Format("Sent new user invitation to {0}.", UserInviteRecipientEmail));

                // It is possible to have multiple pending invitations sent to the same email address, 
                // which have not yet expired. It is also possible for those invitations to have specified 
                // different user roles, for example if you sent an invitation with an incorrect user role 
                // and then sent a second invitation with the correct user role. The recipient can accept 
                // any of the invitations. The Bing Ads API does not support any operations to delete 
                // pending user invitations. After you invite a user, the only way to cancel the invitation 
                // is through the Microsoft Advertising web application. You can find both pending and accepted invitations 
                // in the Users section of Accounts & Billing.

                // Since a recipient can accept the invitation with credentials that differ from 
                // the invitation email address, you cannot determine with certainty the mapping from UserInvitation 
                // to accepted User. You can only determine whether the invitation has been accepted or has expired. 
                // The SearchUserInvitations operation returns all pending invitations, whether or not they have expired. 
                // Accepted invitations are not included in the SearchUserInvitations response.  

                var predicate = new Predicate
                {
                    Field = "CustomerId",
                    Operator = PredicateOperator.In,
                    Value = authorizationData.CustomerId.ToString(CultureInfo.InvariantCulture)
                };

                OutputStatusMessage("-----\nSearchUserInvitations:");
                var userInvitations = (await CustomerManagementExampleHelper.SearchUserInvitationsAsync(
                    predicates: new[] { predicate }))?.UserInvitations;
                OutputStatusMessage("UserInvitations:");
                CustomerManagementExampleHelper.OutputArrayOfUserInvitation(userInvitations);

                // After the invitation has been accepted, you can call GetUsersInfo and GetUser to access the Microsoft Advertising user details. 
                // Once again though, since a recipient can accept the invitation with credentials that differ from 
                // the invitation email address, you cannot determine with certainty the mapping from UserInvitation 
                // to accepted User. 

                OutputStatusMessage("-----\nGetUsersInfo:");
                var usersInfo = (await CustomerManagementExampleHelper.GetUsersInfoAsync(
                    customerId: authorizationData.CustomerId,
                    statusFilter: null))?.UsersInfo;
                OutputStatusMessage("UsersInfo:");
                CustomerManagementExampleHelper.OutputArrayOfUserInfo(usersInfo);

                foreach (var info in usersInfo)
                {
                    OutputStatusMessage("-----\nGetUser:");
                    var getUserResponse = await CustomerManagementExampleHelper.GetUserAsync(
                        userId: info.Id);
                    var user = getUserResponse.User;
                    OutputStatusMessage("User:");
                    CustomerManagementExampleHelper.OutputUser(user);
                    OutputStatusMessage("CustomerRoles:");
                    CustomerManagementExampleHelper.OutputArrayOfCustomerRole(getUserResponse.CustomerRoles);
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Customer Management AdApiFaultDetail service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Customer Management ApiFault service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch other .NET framework exceptions
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }
    }
}
