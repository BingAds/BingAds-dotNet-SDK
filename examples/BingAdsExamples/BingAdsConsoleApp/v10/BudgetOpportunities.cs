using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.AdInsight;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsConsoleApp.V10
{
    /// <summary>
    /// This example demonstrates how to get the budget opportunities for each campaign in the current authenticated account.
    /// </summary>
    public class BudgetOpportunities : ExampleBase
    {
        public static ServiceClient<IAdInsightService> AdInsightService;
        public static ServiceClient<ICampaignManagementService> CampaignService;

        public override string Description
        {
            get { return "Budget Opportunities | AdInsight V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                AdInsightService = new ServiceClient<IAdInsightService>(authorizationData);
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                var campaigns = (Campaign[])await GetCampaignsByAccountIdAsync(
                    authorizationData.AccountId,
                    CampaignType.SearchAndContent | CampaignType.Shopping);

                IList<BudgetOpportunity> opportunities = null;

                // Get the budget opportunities for each campaign in the current authenticated account.

                foreach (var campaign in campaigns)
                {
                    if (campaign.Id != null)
                    {
                        opportunities = await GetBudgetOpportunitiesAsync((long)campaign.Id);
                        OutputBudgetOpportunities(opportunities, (long) campaign.Id);
                    }
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch AdInsight service exceptions
            catch (FaultException<Microsoft.BingAds.V10.AdInsight.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.AdInsight.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        /// <summary>
        /// Gets campaigns of the specified type for the account.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="campaignType"></param>
        /// <returns></returns>
        private async Task<IList<Campaign>> GetCampaignsByAccountIdAsync(
            long accountId,
            CampaignType campaignType)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request)).Campaigns;
        }

        /// <summary>
        /// Gets the budget opportunities for the specified campaign.
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        private async Task<IList<BudgetOpportunity>> GetBudgetOpportunitiesAsync(long campaignId)
        {
            var request = new GetBudgetOpportunitiesRequest
            {
                CampaignId = campaignId
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetBudgetOpportunitiesAsync(r), request)).Opportunities;
        }

        /// <summary>
        /// Outputs the list of BudgetOpportunity objects.
        /// </summary>
        /// <param name="budgetOpportunities"></param>
        protected void OutputBudgetOpportunities(IList<BudgetOpportunity> budgetOpportunities, long campaignId)
        {
            if (budgetOpportunities != null && budgetOpportunities.Count > 0)
            {
                foreach (var budgetOpportunity in budgetOpportunities)
                {
                    OutputStatusMessage("BudgetPoints: ");
                    foreach (var budgetPoint in budgetOpportunity.BudgetPoints)
                    {
                        OutputBudgetPoint(budgetPoint);
                    }
                    OutputStatusMessage(String.Format("BudgetType: {0}", budgetOpportunity.BudgetType));
                    OutputStatusMessage(String.Format("CampaignId: {0}", budgetOpportunity.CampaignId));
                    OutputStatusMessage(String.Format("CurrentBudget: {0}", budgetOpportunity.CurrentBudget));
                    OutputStatusMessage(String.Format("IncreaseInClicks: {0}", budgetOpportunity.IncreaseInClicks));
                    OutputStatusMessage(String.Format("IncreaseInImpressions: {0}", budgetOpportunity.IncreaseInImpressions));
                    OutputStatusMessage(String.Format("OpportunityKey: {0}", budgetOpportunity.OpportunityKey));
                    OutputStatusMessage(String.Format("PercentageIncreaseInClicks: {0}", budgetOpportunity.PercentageIncreaseInClicks));
                    OutputStatusMessage(String.Format("PercentageIncreaseInImpressions: {0}", budgetOpportunity.PercentageIncreaseInImpressions));
                    OutputStatusMessage(String.Format("RecommendedBudget: {0}", budgetOpportunity.RecommendedBudget));
                }
            }
            else
            {
                OutputStatusMessage(String.Format("There are no budget opportunities for CampaignId: {0}", campaignId));
            }
        }

        /// <summary>
        /// Outputs the BudgetPoint object.
        /// </summary>
        /// <param name="budgetPoint"></param>
        protected void OutputBudgetPoint(BudgetPoint budgetPoint)
        {
            if (budgetPoint != null)
            {
                OutputStatusMessage(String.Format("BudgetAmount: {0}", budgetPoint.BudgetAmount));
                OutputStatusMessage(String.Format("BudgetPointType: {0}", budgetPoint.BudgetPointType));
                OutputStatusMessage(String.Format("EstimatedWeeklyClicks: {0}", budgetPoint.EstimatedWeeklyClicks));
                OutputStatusMessage(String.Format("EstimatedWeeklyCost: {0}", budgetPoint.EstimatedWeeklyCost));
                OutputStatusMessage(String.Format("EstimatedWeeklyImpressions: {0}", budgetPoint.EstimatedWeeklyImpressions));
            }
        }
    }
}
