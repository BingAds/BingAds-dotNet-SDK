using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CustomerManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to use agency credentials to invite a client, and use client credentials to accept the invitation.
    /// Run this sample multiple times alternating between agency and client credentials 
    /// to update and observe the status change, for example from LinkPending to LinkAccepted to Active. 
    /// </summary>
    public class ClientLinks : ExampleBase
    {
        // REQUIRED: The Client Account Id that you want to access.
        private long ClientAccountId = 0; 

        public override string Description
        {
            get { return "Manage Client | Customer Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                OutputStatusMessage("You must edit this example to provide the ClientAccountId for the client link." +
                    "When adding a client link, the client link's ManagingCustomerId is set to the CustomerId " +
                    "of the current authenticated user, who must be a Super Admin of the agency." +
                    "Login as an agency Super Admin user to send a client link invitation, or unlink an existing client link." +
                    "Login as a client Super Admin user to accept a client link invitation.");

                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CustomerManagementExampleHelper CustomerManagementExampleHelper = new CustomerManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CustomerManagementExampleHelper.CustomerManagementService = new ServiceClient<ICustomerManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                UpdateClientLinksResponse updateClientLinksResponse = null;

                // Set the client link search criteria.

                var pageInfo = new Paging
                {
                    Index = 0, // The first page
                    Size = 100 // The first 100 client links for this page of results
                };

                var ordering = new OrderBy
                {
                    Field = OrderByField.Number,
                    Order = SortOrder.Ascending
                };

                var predicate = new Predicate
                {
                    Field = "ClientAccountId",
                    Operator = PredicateOperator.In,
                    Value = ClientAccountId.ToString(CultureInfo.InvariantCulture)
                };

                // Search for client links that match the criteria.

                OutputStatusMessage("-----\nSearchClientLinks:");
                var searchClientLinksResponse = (await CustomerManagementExampleHelper.SearchClientLinksAsync(
                    predicates: new[] { predicate },
                    ordering: new[] { ordering },
                    pageInfo: pageInfo));
                var clientLinks = searchClientLinksResponse?.ClientLinks;
                OutputStatusMessage("ClientLinks:");
                CustomerManagementExampleHelper.OutputArrayOfClientLink(clientLinks);

                // Determine whether the agency is already managing the specified client account. 
                // If a link exists with status either Active, LinkInProgress, LinkPending, 
                // UnlinkInProgress, or UnlinkPending, the agency may not initiate a duplicate client link.

                ClientLink clientLink;
                var newLinkRequired = true;

                if (clientLinks.Count > 0)
                {
                    clientLink = clientLinks[0];
                    OutputStatusMessage("Using the first client link as an example.");
                    OutputStatusMessage(string.Format("Current ClientLink Status: {0}.", clientLink.Status));

                    switch (clientLink.Status)
                    {
                        // The agency may choose to initiate the unlink process, 
                        // which would terminate the existing relationship with the client. 
                        case ClientLinkStatus.Active:
                            clientLink.Status = ClientLinkStatus.UnlinkRequested;
                            OutputStatusMessage("-----\nUpdateClientLinks:");
                            updateClientLinksResponse = await CustomerManagementExampleHelper.UpdateClientLinksAsync(
                                clientLinks: new[] { clientLink });
                            OutputStatusMessage("UnlinkRequested");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition or waiting for the StartDate.
                        case ClientLinkStatus.LinkAccepted:
                            OutputStatusMessage("The status is transitioning towards LinkInProgress");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition.
                        case ClientLinkStatus.LinkInProgress:
                            OutputStatusMessage("The status is transitioning towards Active");
                            newLinkRequired = false;
                            break;
                        // When the status is LinkPending, either the agency or client may update the status.
                        // The agency may choose to cancel the client link invitation; however, in this example 
                        // the client will accept the invitation. 
                        // If the client does not accept or decline the invitation within 30 days, and if the agency
                        // does not update the status to LinkCanceled, the system updates the status to LinkExpired.
                        case ClientLinkStatus.LinkPending:
                            clientLink.Status = ClientLinkStatus.LinkAccepted;
                            OutputStatusMessage("-----\nUpdateClientLinks:");
                            updateClientLinksResponse = await CustomerManagementExampleHelper.UpdateClientLinksAsync(
                                clientLinks: new[] { clientLink });
                            OutputStatusMessage("LinkAccepted");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition.
                        case ClientLinkStatus.UnlinkInProgress:
                            OutputStatusMessage("The status is transitioning towards Inactive");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition.
                        case ClientLinkStatus.UnlinkPending:
                            OutputStatusMessage("The status is transitioning towards Inactive");
                            newLinkRequired = false;
                            break;
                        // The link lifecycle has ended.  
                        default:
                            OutputStatusMessage("A new client link invitation is required");
                            break;
                    }
                    
                    // Output errors if any occurred when updating the client link.

                    if (updateClientLinksResponse != null)
                    {
                        OutputStatusMessage("OperationErrors:");
                        CustomerManagementExampleHelper.OutputArrayOfOperationError(updateClientLinksResponse.OperationErrors);
                        OutputStatusMessage("PartialErrors:");
                        foreach (List<OperationError> operationErrors in updateClientLinksResponse.PartialErrors)
                        {
                            CustomerManagementExampleHelper.OutputArrayOfOperationError(operationErrors);
                        }
                    }
                }

                // If no links exist between the agency and specified client account, or a link exists with status  
                // either Inactive, LinkCanceled, LinkDeclined, LinkExpired, or LinkFailed, then the agency must
                // initiate a new client link.

                if (newLinkRequired)
                {
                    clientLink = new ClientLink
                    {
                        ClientEntityId = ClientAccountId,
                        ManagingCustomerId = authorizationData.CustomerId,
                        IsBillToClient = true,
                        Name = "My Client Link",
                        StartDate = null,
                        SuppressNotification = true
                    };

                    OutputStatusMessage("-----\nAddClientLinks:");
                    var addClientLinksResponse = await CustomerManagementExampleHelper.AddClientLinksAsync(
                        clientLinks: new[] { clientLink });
                    OutputStatusMessage("OperationErrors:");
                    CustomerManagementExampleHelper.OutputArrayOfOperationError(addClientLinksResponse.OperationErrors);
                    OutputStatusMessage("PartialErrors:");
                    foreach (List<OperationError> operationErrors in addClientLinksResponse.PartialErrors)
                    {
                        CustomerManagementExampleHelper.OutputArrayOfOperationError(operationErrors);
                    }
                }

                // Output the client links after any status updates above.

                OutputStatusMessage("-----\nSearchClientLinks:");
                searchClientLinksResponse = (await CustomerManagementExampleHelper.SearchClientLinksAsync(
                    predicates: new[] { predicate },
                    ordering: new[] { ordering },
                    pageInfo: pageInfo));
                clientLinks = searchClientLinksResponse?.ClientLinks;
                OutputStatusMessage("ClientLinks:");
                CustomerManagementExampleHelper.OutputArrayOfClientLink(clientLinks);
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
