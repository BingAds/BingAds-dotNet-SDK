using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create shared budgets with the Campaign Management service.
    /// </summary>
    public class Budgets : ExampleBase
    {
        public override string Description
        {
            get { return "Shared Budgets | Campaign Management V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData, 
                    environment: environment);
                
                // Add a budget that can be shared by campaigns in the same account.

                var budgets = new[]
                {
                    new Budget
                    {
                        Amount = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        Name = "My Shared Budget " + DateTime.UtcNow,
                    }
                };

                OutputStatusMessage("-----\nAddBudgets:");
                AddBudgetsResponse addBudgetsResponse = await CampaignManagementExampleHelper.AddBudgetsAsync(
                    budgets: budgets);
                long?[] budgetIds = addBudgetsResponse.BudgetIds.ToArray();
                BatchError[] budgetErrors = addBudgetsResponse.PartialErrors.ToArray();
                OutputStatusMessage("BudgetIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(budgetIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(budgetErrors);                

                // Add one or more campaigns.

                var campaigns = new[]{
                    new Campaign
                    {
                        // We'll use the shared budget instead of defining a daily amount for this campaign.
                        BudgetId = budgetIds[0],
                        DailyBudget = null,
                        BudgetType = null,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
                };

                OutputStatusMessage("-----\nAddCampaigns:");
                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaigns: campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("CampaignIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}", campaignIds[0]));

                // Delete the account's shared budget. 

                OutputStatusMessage("-----\nDeleteBudgets:");
                await CampaignManagementExampleHelper.DeleteBudgetsAsync(
                    budgetIds: new[] { (long)budgetIds[0] });
                OutputStatusMessage(string.Format("Deleted Budget Id {0}", budgetIds[0]));
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }
    }
}
