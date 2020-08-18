using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to create experiment campaigns from a base campaign.
    /// </summary>
    public class Experiments : ExampleBase
    {
        public override string Description
        {
            get { return "Experiments | Campaign Management V13"; }
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

                // Choose a base campaign for the experiment.

                OutputStatusMessage("-----\nGetCampaignsByAccountId:");
                GetCampaignsByAccountIdResponse getCampaignsByAccountIdResponse = await CampaignManagementExampleHelper.GetCampaignsByAccountIdAsync(
                    accountId: authorizationData.AccountId,
                    campaignType: CampaignType.Search,
                    returnAdditionalFields: CampaignAdditionalField.AdScheduleUseSearcherTimeZone);
                var campaigns = getCampaignsByAccountIdResponse.Campaigns;
                OutputStatusMessage("Campaigns:");
                CampaignManagementExampleHelper.OutputArrayOfCampaign(campaigns);

                // The base campaign cannot be an experiment of another base campaign
                // i.e., the campaign's ExperimentId must be nil. 
                // Likewise the base campaign cannot use a shared budget
                // i.e., the campaign's BudgetId must be nil. 

                var baseCampaign = campaigns.FirstOrDefault(
                    campaign => campaign.ExperimentId == null && campaign.BudgetId == null);

                if (baseCampaign == null)
                {
                    OutputStatusMessage("You do not have any campaigns that are eligible for experiments.");
                    return;
                }

                // Create the experiment

                var experiments = new [] {
                    new Experiment
                    {
                        BaseCampaignId = baseCampaign.Id,
                        EndDate = new Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year
                        },
                        ExperimentCampaignId = null,
                        ExperimentStatus = "Active",
                        ExperimentType = null,
                        Id = null,
                        Name = baseCampaign.Name + "-Experiment",
                        StartDate = new Date
                        {
                            Month = DateTime.UtcNow.Month,
                            Day = DateTime.UtcNow.Day,
                            Year = DateTime.UtcNow.Year
                        },
                        TrafficSplitPercent = 50
                    }
                };

                OutputStatusMessage("-----\nAddExperiments:");
                AddExperimentsResponse addExperimentsResponse = await CampaignManagementExampleHelper.AddExperimentsAsync(
                        experiments: experiments);
                long?[] experimentIds = addExperimentsResponse.ExperimentIds.ToArray();
                BatchError[] experimentErrors = addExperimentsResponse.PartialErrors.ToArray();
                OutputStatusMessage("ExperimentIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(experimentIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(experimentErrors);

                OutputStatusMessage("-----\nGetExperimentsByIds:");
                GetExperimentsByIdsResponse getExperimentsByIdsResponse = await CampaignManagementExampleHelper.GetExperimentsByIdsAsync(
                        experimentIds: new[] { (long)experimentIds[0] },
                        pageInfo: null);
                OutputStatusMessage("Experiments:");
                CampaignManagementExampleHelper.OutputArrayOfExperiment(getExperimentsByIdsResponse.Experiments);

                var experiment = getExperimentsByIdsResponse.Experiments?.ToList()[0];

                // If the experiment is in a Graduated state, then the former experiment campaign 
                // is now an independent campaign that must be deleted separately. 
                // Otherwise if you delete the base campaign (not shown here), 
                // the experiment campaign and experiment itself are also deleted.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)experiment.ExperimentCampaignId });
                OutputStatusMessage(string.Format("Deleted Experiment Campaign Id {0} with Status '{1}'",
                    experiment.ExperimentCampaignId,
                    experiment.ExperimentStatus));

                OutputStatusMessage("-----\nDeleteExperiments:");
                await CampaignManagementExampleHelper.DeleteExperimentsAsync(
                    experimentIds: new[] { (long)experiment.Id });
                OutputStatusMessage(string.Format("Deleted Experiment Id {0}", experiment.Id));        
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
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
            catch (FaultException<Microsoft.BingAds.V13.CampaignManagement.EditorialApiFaultDetail> ex)
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
