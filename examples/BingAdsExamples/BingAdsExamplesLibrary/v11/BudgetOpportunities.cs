using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.AdInsight;
using Microsoft.BingAds.V11.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to get the budget opportunities for each campaign in the current authenticated account.
    /// </summary>
    public class BudgetOpportunities : ExampleBase
    {
        public override string Description
        {
            get { return "Budget Opportunities | AdInsight V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                AdInsightExampleHelper AdInsightExampleHelper = new AdInsightExampleHelper(this.OutputStatusMessage);
                AdInsightExampleHelper.AdInsightService = new ServiceClient<IAdInsightService>(authorizationData);

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

                var campaigns = (await CampaignManagementExampleHelper.GetCampaignsByAccountIdAsync(
                    authorizationData.AccountId,
                    AllCampaignTypes)).Campaigns;

                IList<BudgetOpportunity> opportunities = null;

                // Get the budget opportunities for each campaign in the current authenticated account.

                foreach (var campaign in campaigns)
                {
                    if (campaign.Id != null)
                    {
                        opportunities = (await AdInsightExampleHelper.GetBudgetOpportunitiesAsync((long)campaign.Id)).Opportunities;
                        AdInsightExampleHelper.OutputArrayOfBudgetOpportunity(opportunities);
                    }
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch AdInsight service exceptions
            catch (FaultException<Microsoft.BingAds.V11.AdInsight.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.AdInsight.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.ApiFaultDetail> ex)
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