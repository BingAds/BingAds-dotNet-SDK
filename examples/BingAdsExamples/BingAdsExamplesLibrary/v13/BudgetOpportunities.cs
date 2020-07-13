using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.AdInsight;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to get the budget opportunities for each campaign in the current authenticated account.
    /// </summary>
    public class BudgetOpportunities : ExampleBase
    {
        public override string Description
        {
            get { return "Budget Opportunities | AdInsight V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                AdInsightExampleHelper AdInsightExampleHelper = new AdInsightExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                AdInsightExampleHelper.AdInsightService = new ServiceClient<IAdInsightService>(
                    authorizationData: authorizationData,
                    environment: environment);

                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(
                    authorizationData: authorizationData,
                    environment: environment);

                OutputStatusMessage("-----\nGetCampaignsByAccountId:");
                var campaigns = (await CampaignManagementExampleHelper.GetCampaignsByAccountIdAsync(
                    accountId: authorizationData.AccountId,
                    campaignType: AllCampaignTypes,
                    returnAdditionalFields: CampaignAdditionalField.AdScheduleUseSearcherTimeZone)).Campaigns;
                OutputStatusMessage("Campaigns:");
                CampaignManagementExampleHelper.OutputArrayOfCampaign(campaigns);

                IList<BudgetOpportunity> opportunities = null;

                // Get the budget opportunities for each campaign in the current account.

                foreach (var campaign in campaigns)
                {
                    if (campaign.Id != null)
                    {
                        OutputStatusMessage("-----\nGetBudgetOpportunities:");
                        opportunities = (await AdInsightExampleHelper.GetBudgetOpportunitiesAsync(
                            campaignId: (long)campaign.Id)).Opportunities;
                        OutputStatusMessage("Opportunities:");
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
            catch (FaultException<Microsoft.BingAds.V13.AdInsight.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.AdInsight.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.ApiFaultDetail> ex)
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
