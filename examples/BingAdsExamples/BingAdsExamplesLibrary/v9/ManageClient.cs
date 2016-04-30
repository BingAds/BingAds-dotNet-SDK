using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.CustomerManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V9
{
    /// <summary>
    /// This example demonstrates how to use agency credentials to invite a client, 
    /// and use client credentials to accept the invitation. 
    /// Run this sample multiple times alternating between agency and client credentials 
    /// to update and observe the status change, for example from LinkPending to LinkAccepted to Active. 
    /// </summary>
    public class ManageClient : ExampleBase
    {
        public static ServiceClient<ICustomerManagementService> Service;

        private const int ClientAccountId = 0; //<ClientAccountIdGoesHere>;
        
        public override string Description
        {
            get { return "Manage Client | Customer Management V9"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                OutputStatusMessage("You must edit the ManageClient.cs file to provide the ClientAccountId for " +
                                    "the client link.");
                OutputStatusMessage("When adding a client link, the client link's ManagingCustomerId is set to the CustomerId of the current " +
                                    "authenticated user, who must be a Super Admin of the agency.");
                OutputStatusMessage("Login as an agency Super Admin user to send a client link invitation, " +
                                    "or unlink an existing client link.");
                OutputStatusMessage("Login as a client Super Admin user to accept a client link invitation.\n");


                Service = new ServiceClient<ICustomerManagementService>(authorizationData);

                UpdateClientLinksResponse updateClientLinksResponse = null;

                // Specify the client link search criteria

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

                // Search for client links that match the specified criteria.

                var clientLinks = await SearchClientLinksAsync(
                    new[] { ordering },
                    pageInfo,
                    new[] { predicate });

                // Determine whether the agency is already managing the specified client account. 
                // If a link exists with status either Active, LinkInProgress, LinkPending, 
                // UnlinkInProgress, or UnlinkPending, the agency may not initiate a duplicate client link.

                ClientLink clientLink;
                var newLinkRequired = true;

                if (clientLinks.Count > 0)
                {
                    clientLink = clientLinks[0];
                    OutputStatusMessage(String.Format("Current ClientLink Status: {0}.\n", clientLink.Status));

                    switch (clientLink.Status)
                    {
                        // The agency may choose to initiate the unlink process, 
                        // which would terminate the existing relationship with the client. 
                        case ClientLinkStatus.Active:
                            clientLink.Status = ClientLinkStatus.UnlinkRequested;
                            updateClientLinksResponse = await UpdateClientLinksAsync(new[] { clientLink });
                            OutputStatusMessage("UpdateClientLinks : UnlinkRequested.\n");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition or waiting for the StartDate.
                        case ClientLinkStatus.LinkAccepted:
                            OutputStatusMessage("The status is transitioning towards LinkInProgress.\n");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition.
                        case ClientLinkStatus.LinkInProgress:
                            OutputStatusMessage("The status is transitioning towards Active.\n");
                            newLinkRequired = false;
                            break;
                        // When the status is LinkPending, either the agency or client may update the status.
                        // The agency may choose to cancel the client link invitation; however, in this example 
                        // the client will accept the invitation. 
                        // If the client does not accept or decline the invitation within 30 days, and if the agency
                        // does not update the status to LinkCanceled, the system updates the status to LinkExpired.
                        case ClientLinkStatus.LinkPending:
                            /*
                            clientLink.Status = ClientLinkStatus.LinkCanceled;
                            updateClientLinksResponse = UpdateClientLinks(new[] { clientLink });
                            WriteMessage(String.Format("The agency updated status: LinkCanceled.\n");
                             */
                            clientLink.Status = ClientLinkStatus.LinkAccepted;
                            updateClientLinksResponse = await UpdateClientLinksAsync(new[] { clientLink });
                            OutputStatusMessage("UpdateClientLinks: LinkAccepted.\n");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition.
                        case ClientLinkStatus.UnlinkInProgress:
                            OutputStatusMessage("The status is transitioning towards Inactive.\n");
                            newLinkRequired = false;
                            break;
                        // Waiting on a system status transition.
                        case ClientLinkStatus.UnlinkPending:
                            OutputStatusMessage("The status is transitioning towards Inactive.\n");
                            newLinkRequired = false;
                            break;
                        // The link lifecycle has ended.  
                        default:
                            OutputStatusMessage("A new client link invitation is required.\n");
                            break;
                    }

                    // Print errors if any occurred when updating the client link.
                    if (updateClientLinksResponse != null)
                    {
                        PrintPartialErrors(updateClientLinksResponse.OperationErrors,
                                           updateClientLinksResponse.PartialErrors);
                    }
                }

                // If no links exist between the agency and specified client account, or a link exists with status  
                // either Inactive, LinkCanceled, LinkDeclined, LinkExpired, or LinkFailed, then the agency must
                // initiate a new client link.

                if (newLinkRequired)
                {
                    clientLink = new ClientLink
                    {
                        ClientAccountId = ClientAccountId,
                        ManagingCustomerId = authorizationData.CustomerId,
                        IsBillToClient = true,
                        Name = "My Client Link",
                        StartDate = null,
                        SuppressNotification = true
                    };

                    var addClientLinksResponse = await AddClientLinksAsync(new[] { clientLink });

                    // Print errors if any occurred when adding the client link.

                    PrintPartialErrors(addClientLinksResponse.OperationErrors.ToArray(), addClientLinksResponse.PartialErrors.ToArray());
                    OutputStatusMessage(string.Format("The user attempted to add a new ClientLink.\n"));
                    OutputStatusMessage(string.Format("Login as the client Super Admin to accept the agency's request to manage AccountId {0}.\n", ClientAccountId));
                }

                // Get and print the current client link

                clientLinks = await SearchClientLinksAsync(
                        new[] { ordering },
                        pageInfo,
                        new[] { predicate });

                PrintClientLinks(clientLinks);
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

        // Searches client links for the customer of the current authenticated user,
        // filtered by the search criteria.

        private async Task<IList<ClientLink>> SearchClientLinksAsync(
           IList<OrderBy> ordering,
           Paging pageInfo,
           IList<Predicate> predicates)
        {
            var request = new SearchClientLinksRequest
            {
                Ordering = ordering,
                PageInfo = pageInfo,
                Predicates = predicates
            };

            return (await Service.CallAsync((s, r) => s.SearchClientLinksAsync(r), request)).ClientLinks;
        }

        // Initiates the client link process to manage the account of another customer. 
        // Sends an invitation from an agency to a potential client.

        private async Task<AddClientLinksResponse> AddClientLinksAsync(IList<ClientLink> clientLinks)
        {
            var request = new AddClientLinksRequest
            {
                ClientLinks = clientLinks
            };

            return (await Service.CallAsync((s, r) => s.AddClientLinksAsync(r), request));
        }

        // Using agency credentials, updates the status of the specified client links.

        private async Task<UpdateClientLinksResponse> UpdateClientLinksAsync(IList<ClientLink> clientLinks)
        {
            var request = new UpdateClientLinksRequest
            {
                ClientLinks = clientLinks
            };

            return (await Service.CallAsync((s, r) => s.UpdateClientLinksAsync(r), request));
        }

        // Prints a list of client links.

        private void PrintClientLinks(IEnumerable<ClientLink> clientLinks)
        {
            if (clientLinks == null)
            {
                return;
            }

            foreach (ClientLink clientLink in clientLinks)
            {
                OutputStatusMessage(String.Format("Status: {0}", clientLink.Status));
                OutputStatusMessage(String.Format("ClientAccountId: {0}", clientLink.ClientAccountId));
                OutputStatusMessage(String.Format("ClientAccountNumber: {0}", clientLink.ClientAccountNumber));
                OutputStatusMessage(String.Format("ManagingAgencyCustomerId: {0}", clientLink.ManagingCustomerId));
                OutputStatusMessage(String.Format("ManagingCustomerNumber: {0}", clientLink.ManagingCustomerNumber));
                OutputStatusMessage(String.Format(clientLink.IsBillToClient ? "IsBillToClient: True" : "IsBillToClient: False"));
                OutputStatusMessage(String.Format("InviterEmail: {0}", clientLink.InviterEmail));
                OutputStatusMessage(String.Format("InviterName: {0}", clientLink.InviterName));
                OutputStatusMessage(String.Format("InviterPhone: {0}", clientLink.InviterPhone));
                OutputStatusMessage(String.Format("LastModifiedByUserId: {0}", clientLink.LastModifiedByUserId));
                OutputStatusMessage(String.Format("LastModifiedDateTime: {0}", clientLink.LastModifiedDateTime));
                OutputStatusMessage(String.Format("Name: {0}", clientLink.Name));
                OutputStatusMessage(String.Format("Note: {0}", clientLink.Note));
                OutputStatusMessage("");
            }
        }

        // Print errors if any occurred when adding or updating the client link.

        private void PrintPartialErrors(IEnumerable<OperationError> operationErrors, IEnumerable<IList<OperationError>> partialErrors)
        {
            if (operationErrors == null)
            {
                return;
            }

            foreach (OperationError error in operationErrors)
            {
                OutputStatusMessage("OperationError");
                OutputStatusMessage(String.Format("Code: {0}\nMessage: {1}\n", error.Code, error.Message));
            }

            if (partialErrors == null)
            {
                return;
            }

            foreach (var errors in partialErrors)
            {
                if (errors != null)
                {
                    foreach (OperationError error in errors)
                    {
                        if (error != null)
                        {
                            OutputStatusMessage("OperationError");
                            OutputStatusMessage(String.Format("Code: {0}\nMessage: {1}\n", error.Code, error.Message));
                        }
                    }
                }
            }
        }
    }
}
