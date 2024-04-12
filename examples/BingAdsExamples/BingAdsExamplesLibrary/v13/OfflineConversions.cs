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
    /// How to send Microsoft Advertising your offline conversions using the Campaign Management service.
    /// </summary>
    public class OfflineConversions : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Offline Conversions | Campaign Management V13"; }
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

                // A conversion goal cannot be deleted, so even if this is a test
                // please choose an appropriate name accordingly. 
                var offlineConversionGoalName = "My Offline Conversion Goal";

                var conversionGoals = new ConversionGoal[]
                {
                    new OfflineConversionGoal
                    {
                        GoalCategory = ConversionGoalCategory.Purchase,
                        // Determines how long after a click that you want to count offline conversions. 
                        ConversionWindowInMinutes = 43200,

                        // If the count type is 'Unique' then only the first offline conversion will be counted.
                        // By setting the count type to 'All', then all offline conversions for the same
                        // MicrosoftClickId with different conversion times will be added cumulatively. 
                        CountType = ConversionGoalCountType.All,

                        Name = offlineConversionGoalName,

                        // The default conversion currency code and value. Each offline conversion can override it.
                        Revenue = new ConversionGoalRevenue
                        {
                            CurrencyCode = null,
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                        },
                        Scope = EntityScope.Account,
                        Status = ConversionGoalStatus.Active,
                        TagId = null
                    },
                };

                OutputStatusMessage("-----\nAddConversionGoals:");
                var addConversionGoalsResponse = await CampaignManagementExampleHelper.AddConversionGoalsAsync(
                    conversionGoals: conversionGoals);
                var conversionGoalIds = addConversionGoalsResponse.ConversionGoalIds.ToArray();
                BatchError[] conversionGoalErrors = addConversionGoalsResponse.PartialErrors.ToArray();
                OutputStatusMessage("ConversionGoalIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(conversionGoalIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(conversionGoalErrors);
                
                List<long> goalIds = GetNonNullableIds(conversionGoalIds);
                
                var conversionGoalTypes = ConversionGoalType.OfflineConversion;

                OutputStatusMessage("-----\nGetConversionGoalsByIds:");
                var getConversionGoalsResponse = (await CampaignManagementExampleHelper.GetConversionGoalsByIdsAsync(
                        conversionGoalIds: goalIds,
                        conversionGoalTypes: conversionGoalTypes,
                        returnAdditionalFields: ConversionGoalAdditionalField.ViewThroughConversionWindowInMinutes));
                var getConversionGoals = getConversionGoalsResponse.ConversionGoals;
                conversionGoalErrors = getConversionGoalsResponse.PartialErrors.ToArray();
                OutputStatusMessage("ConversionGoals:");
                CampaignManagementExampleHelper.OutputArrayOfConversionGoal(getConversionGoals);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(conversionGoalErrors);

                // Every time you create a new OfflineConversionGoal via either the Microsoft Advertising web application or Campaign Management API, 
                // the MSCLKIDAutoTaggingEnabled value of the corresponding AccountProperty is set to 'true' automatically.
                // We can confirm the setting now.

                var accountPropertyNames = new List<AccountPropertyName>();
                accountPropertyNames.Add(AccountPropertyName.MSCLKIDAutoTaggingEnabled);

                OutputStatusMessage("-----\nGetAccountProperties:");
                var getAccountPropertiesResponse = await CampaignManagementExampleHelper.GetAccountPropertiesAsync(
                    accountPropertyNames: accountPropertyNames);
                var accountProperties = getAccountPropertiesResponse.AccountProperties;
                BatchError[] accountPropertiesErrors = getAccountPropertiesResponse.PartialErrors.ToArray();
                OutputStatusMessage("AccountProperties:");
                CampaignManagementExampleHelper.OutputArrayOfAccountProperty(accountProperties);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(accountPropertiesErrors);
                
                var offlineConversions = new[]
                {
                    new OfflineConversion
                    {
                        // If you do not specify an offline conversion currency code, 
                        // then the 'CurrencyCode' element of the goal's 'ConversionGoalRevenue' is used.
                        ConversionCurrencyCode = "USD",

                        // The conversion name must match the 'Name' of the 'OfflineConversionGoal'.
                        // If it does not match you won't observe any error, although the offline
                        // conversion will not be counted.
                        ConversionName = offlineConversionGoalName,

                        // The date and time must be in UTC, should align to the date and time of the 
                        // recorded click (MicrosoftClickId), and cannot be in the future.
                        ConversionTime = DateTime.UtcNow,

                        // If you do not specify an offline conversion value, 
                        // then the 'Value' element of the goal's 'ConversionGoalRevenue' is used.
                        ConversionValue = 10,

                        MicrosoftClickId = "f894f652ea334e739002f7167ab8f8e3"
                    }
                };

                // After the OfflineConversionGoal is set up, wait two hours before submitting the offline conversions. 
                // This example would not succeed in production because we created the goal very recently i.e., 
                // please see above call to AddConversionGoalsAsync. 

                OutputStatusMessage("-----\nApplyOfflineConversions:");
                var applyOfflineConversionsResponse = await CampaignManagementExampleHelper.ApplyOfflineConversionsAsync(
                    offlineConversions: offlineConversions);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(applyOfflineConversionsResponse.PartialErrors);
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
