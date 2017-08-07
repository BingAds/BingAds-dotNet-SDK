using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to associate remarketing lists with a new ad group.
    /// </summary>
    public class OfflineConversions : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Offline Conversions | Campaign Management V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                var offlineConversions = new []
                {
                    new OfflineConversion
                    {
                        ConversionCurrencyCode = "USD",
                        ConversionName = "TestGoal",
                        ConversionTime = DateTime.UtcNow,
                        ConversionValue = 10,
                        MicrosoftClickId = "f894f652ea334e739002f7167ab8f8e3"
                    }
                };
                
                var applyOfflineConversionsResponse = await ApplyOfflineConversionsAsync(offlineConversions);

            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
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
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.EditorialApiFaultDetail> ex)
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
