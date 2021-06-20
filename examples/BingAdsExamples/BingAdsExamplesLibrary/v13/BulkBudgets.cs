using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create shared budgets with the Bulk service.
    /// </summary>
    public class BulkBudgets : BulkExampleBase
    {
        public override string Description
        {
            get { return "Shared Budgets | Bulk V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                // Used to output the Campaign Management objects within Bulk entities.
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);
                
                var uploadEntities = new List<BulkEntity>();

                // Add a budget that can be shared by campaigns in the same account.                
                // Map properties in the Bulk file to the BulkBudget.

                var bulkBudget = new BulkBudget
                {
                    // 'Parent Id' column header in the Bulk file
                    AccountId = 0,

                    // Map properties in the Bulk file to the 
                    // Budget object of the Campaign Management service.
                    Budget = new Budget
                    {
                        // 'Budget' column header in the Bulk file
                        Amount = 50,
                        // 'Budget Type' column header in the Bulk file
                        BudgetType = BudgetLimitType.DailyBudgetAccelerated,
                        // 'Budget Name' column header in the Bulk file
                        Name = "My Shared Budget " + DateTime.UtcNow,
                        // 'Id' column header in the Bulk file
                        Id = budgetIdKey,
                    },

                    // 'Client Id' column header in the Bulk file
                    ClientId = "ClientIdGoesHere",
                    // 'Status' column header in the Bulk file
                    Status = Status.Active
                };

                uploadEntities.Add(bulkBudget);

                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        // You must set either the shared budget ID or daily amount.
                        BudgetId = budgetIdKey,
                        DailyBudget = null,
                        BudgetType = null,
                        Id = campaignIdKey,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };
                                
                uploadEntities.Add(bulkCampaign);

                // Upload and write the output

                OutputStatusMessage("-----\nAdding shared budget and campaign...");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                var budgetResults = downloadEntities.OfType<BulkBudget>().ToList();
                OutputBulkBudgets(budgetResults);

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);
                
                Reader.Dispose();
                
                // Delete the campaign and everything it contains e.g., ad groups and ads.
                // Delete the account's shared budget. 

                uploadEntities = new List<BulkEntity>();

                foreach (var budgetResult in budgetResults)
                {
                    budgetResult.Status = Status.Deleted;
                    uploadEntities.Add(budgetResult);
                }

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("-----\nDeleting the campaign and shared budget...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                budgetResults = downloadEntities.OfType<BulkBudget>().ToList();
                OutputBulkBudgets(budgetResults);
                campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                Reader.Dispose();                
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V13.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the bulk operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (BulkOperationCouldNotBeCompletedException<DownloadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationCouldNotBeCompletedException<UploadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
            finally
            {
                if (Reader != null) { Reader.Dispose(); }
                if (Writer != null) { Writer.Dispose(); }
            }
        }
    }
}
