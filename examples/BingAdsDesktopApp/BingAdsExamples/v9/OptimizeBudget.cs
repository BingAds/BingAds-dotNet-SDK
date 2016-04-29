using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.Optimizer;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds;


namespace BingAdsExamples.V9
{
    /// <summary>
    /// This example demonstrates how to get the budget opportunities which have not expired for the specified account.
    /// </summary>
    public class OptimizeBudget : ExampleBase
    {
        public static ServiceClient<IOptimizerService> OptimizerService;
        public static ServiceClient<ICampaignManagementService> CampaignService;

        public override string Description
        {
            get { return "Budget Opportunity and Landscape | Optimizer V9 (Deprecated)"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                OptimizerService = new ServiceClient<IOptimizerService>(authorizationData);
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Get the budget opportunities which have not expired for the specified account.

                IList<BudgetOpportunity> opportunities = await GetBudgetOpportunitiesAsync(authorizationData.AccountId);
                IList<string> opportunityKeys = new List<string>();

                if (opportunities.Count == 0)
                {
                    OutputStatusMessage("There are no opportunities which have not yet expired for the specified account.");
                }
                else
                {
                    foreach (var budgetOpportunity in opportunities)
                    {
                        // Add the first 10,000 opportunity keys to an array

                        if (budgetOpportunity != null && opportunityKeys.ToArray().Length < 10000)
                        {
                            OutputStatusMessage(String.Format("OpportunityKey: {0}", budgetOpportunity.OpportunityKey));
                            opportunityKeys.Add(budgetOpportunity.OpportunityKey);
                        }
                    }

                    // Apply the suggested budget opportunities.
                    await ApplyOpportunitiesAsync(authorizationData.AccountId, opportunityKeys);
                }

                var campaigns = (Campaign[])await GetCampaignsByAccountIdAsync(
                    authorizationData.AccountId,
                    CampaignType.SearchAndContent | CampaignType.Shopping);
                
                IList<CampaignBudgetLandscape> campaignBudgetLandscapes = new List<CampaignBudgetLandscape>();
                IList<string> landscapeKeys = new List<string>();

                if (campaigns.Length > 0 && campaigns[0].Id != null)
                {
                    campaignBudgetLandscapes = await GetBudgetLandscapeAsync(authorizationData.AccountId, (long)campaigns[0].Id);
                    landscapeKeys = new List<string>();
                }
                
                if (campaignBudgetLandscapes.Count == 0)
                {
                    OutputStatusMessage("There is no campaign budget landscape data available for the specified account.");
                }
                else
                {
                    foreach (var campaignBudgetLandscape in campaignBudgetLandscapes)
                    {
                        // Add the first 10,000 opportunity keys to an array

                        if (campaignBudgetLandscape != null && campaignBudgetLandscapes.ToArray().Length < 10000)
                        {
                            OutputStatusMessage(String.Format("OpportunityKey: {0}", campaignBudgetLandscape.OpportunityKey));
                            landscapeKeys.Add(campaignBudgetLandscape.OpportunityKey);
                        }
                    }

                    // Apply the suggested budget opportunities.
                    await ApplyOpportunitiesAsync(authorizationData.AccountId, landscapeKeys);
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Optimizer service exceptions
            catch (FaultException<Microsoft.BingAds.Optimizer.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.Optimizer.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        // Gets the budget opportunities which have not expired for the specified account.

        private async Task<IList<BudgetOpportunity>> GetBudgetOpportunitiesAsync(long accountId)
        {
            var request = new GetBudgetOpportunitiesRequest
            {
                AccountId = accountId
            };

            return (await OptimizerService.CallAsync((s, r) => s.GetBudgetOpportunitiesAsync(r), request)).Opportunities;
        }

        // Gets campaigns of the specified type for the account.

        private async Task<IList<Campaign>> GetCampaignsByAccountIdAsync(
            long accountId,
            CampaignType? campaignType)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request)).Campaigns;
        }

        // Gets the budget opportunities which have not expired for the specified account.

        private async Task<IList<CampaignBudgetLandscape>> GetBudgetLandscapeAsync(long accountId, long campaignId)
        {
            var request = new GetBudgetLandscapeRequest
            {
                AccountId = accountId,
                CampaignId = campaignId
            };

            return (await OptimizerService.CallAsync((s, r) => s.GetBudgetLandscapeAsync(r), request)).CampaignBudgetLandscapes;
        }

        // Apply opportunties for the specified account.

        private async Task ApplyOpportunitiesAsync(long accountId, IList<string> opportunityKeys)
        {
            var request = new ApplyOpportunitiesRequest
            {
                AccountId = accountId,
                OpportunityKeys = opportunityKeys
            };

            await OptimizerService.CallAsync((s, r) => s.ApplyOpportunitiesAsync(r), request);
        }
    }
}
