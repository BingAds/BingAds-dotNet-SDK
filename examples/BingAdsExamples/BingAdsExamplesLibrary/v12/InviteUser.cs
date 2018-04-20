using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CustomerManagement;
using Microsoft.BingAds;
using System.Globalization;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to invite a user to manage Bing Ads accounts.
    /// </summary>
    public class InviteUser : ExampleBase
    {
        /// <summary>
        /// Specify the email address where the invitation should be sent. 
        /// It is important to note that the recipient can accept the invitation 
        /// and sign into Bing Ads with a Microsoft account different than the invitation email address.
        /// </summary>
        const string UserInviteRecipientEmail = "<UserInviteRecipientEmailGoesHere>";
        
        public override string Description
        {
            get { return "Invite a User to Manage Bing Ads accounts | Customer Management V12"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                OutputStatusMessage("You must edit this example to provide the email address (UserInviteRecipientEmail) for " +
                                    "the user invitation.");
                OutputStatusMessage("You must use Super Admin credentials to send a user invitation.\n");

                CustomerManagementExampleHelper CustomerManagementExampleHelper = new CustomerManagementExampleHelper(this.OutputStatusMessage);
                CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(authorizationData);

                // Prepare to invite a new user
                var userInvitation = new UserInvitation
                {
                    // The identifier of the customer this user is invited to manage. 
                    // The AccountIds element determines which customer accounts the user can manage.
                    CustomerId = authorizationData.CustomerId,

                    // Users with account level roles such as Advertiser Campaign Manager can be restricted to specific accounts. 
                    // Users with customer level roles such as Super Admin can access all accounts within the user’s customer, 
                    // and their access cannot be restricted to specific accounts.
                    AccountIds = null,

                    // The user role, which determines the level of access that the user has to the accounts specified in the AccountIds element.
                    // The identifier for an advertiser campaign manager is 12.
                    RoleId = 12,

                    // The email address where the invitation should be sent. This element can contain a maximum of 100 characters.
                    Email = UserInviteRecipientEmail,

                    // The first name of the user. This element can contain a maximum of 40 characters.
                    FirstName = "FirstNameGoesHere",

                    // The last name of the user. This element can contain a maximum of 40 characters.
                    LastName = "LastNameGoesHere",

                    // The locale to use when sending correspondence to the user by email or postal mail. The default is EnglishUS.
                    Lcid = LCID.EnglishUS,
                };

                // Once you send a user invitation, there is no option to rescind the invitation using the API.
                // You can delete a pending invitation in the Accounts & Billing -> Users tab of the Bing Ads web application. 
                var userInvitationId = (await CustomerManagementExampleHelper.SendUserInvitationAsync(userInvitation))?.UserInvitationId;
                OutputStatusMessage(string.Format("Sent new user invitation to {0}.\n", UserInviteRecipientEmail));

                // It is possible to have multiple pending invitations sent to the same email address, 
                // which have not yet expired. It is also possible for those invitations to have specified 
                // different user roles, for example if you sent an invitation with an incorrect user role 
                // and then sent a second invitation with the correct user role. The recipient can accept 
                // any of the invitations. The Bing Ads API does not support any operations to delete 
                // pending user invitations. After you invite a user, the only way to cancel the invitation 
                // is through the Bing Ads web application. You can find both pending and accepted invitations 
                // in the Users section of Accounts & Billing.

                // Since a recipient can accept the invitation and sign into Bing Ads with a Microsoft account different 
                // than the invitation email address, you cannot determine with certainty the mapping from UserInvitation 
                // to accepted User. You can search by the invitation ID (returned by SendUserInvitations), 
                // only to the extent of finding out whether or not the invitation has been accepted or has expired. 
                // The SearchUserInvitations operation returns all pending invitations, whether or not they have expired. 
                // Accepted invitations are not included in the SearchUserInvitations response.  

                // This example searches for all user invitations of the customer that you manage,
                // and then filters the search results to find the invitation sent above.
                // Note: In this example the invitation (sent above) should be active and not expired. You can set a breakpoint 
                // and then either accept or delete the invitation in the Bing Ads web application to change the invitation status.

                var predicate = new Predicate
                {
                    Field = "CustomerId",
                    Operator = PredicateOperator.In,
                    Value = authorizationData.CustomerId.ToString(CultureInfo.InvariantCulture)
                };
                
                var userInvitations = (await CustomerManagementExampleHelper.SearchUserInvitationsAsync(new[] { predicate }))?.UserInvitations;
                OutputStatusMessage("Existing UserInvitation(s):\n");
                CustomerManagementExampleHelper.OutputArrayOfUserInvitation(userInvitations);

                // Determine whether the invitation has been accepted or has expired.
                // If you specified a valid InvitationId, and if the invitation is not found, 
                // then the recipient has accepted the invitation. 
                // If the invitation is found, and if the expiration date is later than the current date and time,
                // then the invitation is still pending and has not yet expired. 
                var pendingInvitation = userInvitations.SingleOrDefault(invitation => 
                    invitation.Id == userInvitationId && 
                    DateTime.Compare(invitation.ExpirationDate.ToUniversalTime(), DateTime.UtcNow) > 0);

                // You can send a new invitation if the invitation was either not found, has expired, 
                // or the user has accepted the invitation. This example does not send a new invitation if the 
                // invitationId was found and has not yet expired, i.e. the invitation is pending.
                if (pendingInvitation == null)
                {
                    // Once you send a user invitation, there is no option to rescind the invitation using the API.
                    // You can delete a pending invitation in the Accounts & Billing -> Users tab of the Bing Ads web application. 
                    userInvitationId = (await CustomerManagementExampleHelper.SendUserInvitationAsync(userInvitation))?.UserInvitationId;
                    OutputStatusMessage(string.Format("Sent new user invitation to {0}.\n", UserInviteRecipientEmail));
                }
                else
                {
                    OutputStatusMessage(string.Format("UserInvitationId {0} is pending.\n", userInvitationId));
                }

                // After the invitation has been accepted, you can call GetUsersInfo and GetUser to access the Bing Ads user details. 
                // Once again though, since a recipient can accept the invitation and sign into Bing Ads with a Microsoft account 
                // different than the invitation email address, you cannot determine with certainty the mapping from UserInvitation 
                // to accepted User. With the user ID returned by GetUsersInfo or GetUser, you can call DeleteUser to remove the user.

                var usersInfo = (await CustomerManagementExampleHelper.GetUsersInfoAsync(
                    authorizationData.CustomerId,
                    null))?.UsersInfo;
                var confirmedUserInfo = usersInfo.SingleOrDefault(info => info.UserName == UserInviteRecipientEmail);

                // If a user has already accepted an invitation, you can call GetUser to view all user details.
                if (confirmedUserInfo != null)
                {
                    var getUserResponse = (await CustomerManagementExampleHelper.GetUserAsync(confirmedUserInfo.Id));
                    OutputStatusMessage("Found Requested User Details (Not necessarily related to above Invitation ID(s):");
                    CustomerManagementExampleHelper.OutputUser(getUserResponse.User);
                    OutputStatusMessage("Role Ids:");
                    OutputStatusMessage(string.Join("; ", getUserResponse.CustomerRoles.Select(role => string.Format("{0}", role.RoleId))));
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Customer Management AdApiFaultDetail service exceptions
            catch (FaultException<Microsoft.BingAds.V12.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Customer Management ApiFault service exceptions
            catch (FaultException<Microsoft.BingAds.V12.CustomerManagement.ApiFault> ex)
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
