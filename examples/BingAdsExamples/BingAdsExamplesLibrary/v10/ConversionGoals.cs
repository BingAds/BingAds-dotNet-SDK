using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to manage UET tags and conversion goals.
    /// </summary>
    public class ConversionGoals : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "UET Tags and Conversion Goals | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                // Before you can track conversions or target audiences using a remarketing list, 
                // you need to create a UET tag in Bing Ads (web application or API) and then 
                // add the UET tag tracking code to every page of your website. For more information, please see 
                // Universal Event Tracking at https://msdn.microsoft.com/library/bing-ads-universal-event-tracking-guide.aspx.

                // First you should call the GetUetTagsByIds operation to check whether a tag has already been created. 
                // You can leave the TagIds element null or empty to request all UET tags available for the customer.

                var uetTags = (await GetUetTagsByIdsAsync(null)).UetTags;

                // If you do not already have a UET tag that can be used, or if you need another UET tag, 
                // call the AddUetTags service operation to create a new UET tag. If the call is successful, 
                // the tracking script that you should add to your website is included in a corresponding 
                // UetTag within the response message. 

                if (uetTags == null || uetTags.Count < 1)
                {
                    var uetTag = new UetTag
                    {
                        Description = "My First Uet Tag",
                        Name = "New Uet Tag",
                    };
                    uetTags = (await AddUetTagsAsync(new[] { uetTag })).UetTags;
                }

                if (uetTags == null || uetTags.Count < 1)
                {
                    OutputStatusMessage(
                        string.Format("You do not have any UET tags registered for CustomerId {0}.\n", authorizationData.CustomerId)
                    );
                    return;
                }

                OutputStatusMessage("List of all UET Tags:\n");
                foreach (var uetTag in uetTags)
                {
                    OutputUetTag(uetTag);
                }


                // After you retreive the tracking script from the AddUetTags or GetUetTagsByIds operation, 
                // the next step is to add the UET tag tracking code to your website. We recommend that you, 
                // or your website administrator, add it to your entire website in either the head or body sections. 
                // If your website has a master page, then that is the best place to add it because you add it once 
                // and it is included on all pages. For more information, please see 
                // Universal Event Tracking at https://msdn.microsoft.com/library/bing-ads-universal-event-tracking-guide.aspx.


                // We will use the same UET tag for the remainder of this example.

                var tagId = uetTags[0].Id;

                // Optionally you can update the name and description of a UetTag with the UpdateUetTags operation.

                OutputStatusMessage("UET Tag BEFORE update:\n");
                OutputUetTag(uetTags[0]);

                uetTags = new[]
                {
                    new UetTag
                    {
                        Description = "Updated Uet Tag Description",
                        Id = tagId,
                        Name = "Updated Uet Tag Name " + DateTime.UtcNow,
                    }
                };

                await UpdateUetTagsAsync(uetTags);

                uetTags = (await GetUetTagsByIdsAsync(new[] { (long)tagId })).UetTags;

                OutputStatusMessage("UET Tag AFTER update:\n");
                OutputUetTag(uetTags[0]);

                // Add conversion goals that depend on the UET Tag Id retreived above.
                // Please note that you cannot delete conversion goals. If you want to stop 
                // tracking conversions for the goal, you can set the goal status to Paused.

                var conversionGoals = new ConversionGoal[]
                {
                    new DurationGoal
                    {
                        ConversionWindowInMinutes = 30,
                        CountType = ConversionGoalCountType.All,
                        MinimumDurationInSeconds = 60,
                        Name = "My Duration Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                            CurrencyCode = null
                        },
                        Scope = EntityScope.Account,
                        Status = ConversionGoalStatus.Active,
                        TagId = tagId,
                    },
                    new EventGoal
                    {
                        // The type of user interaction you want to track.
                        ActionExpression = "play",
                        ActionOperator = ExpressionOperator.Contains,
                        // The category of event you want to track. 
                        CategoryExpression = "video",
                        CategoryOperator = ExpressionOperator.Contains,
                        ConversionWindowInMinutes = 30,
                        CountType = ConversionGoalCountType.All,
                        // The name of the element that caused the action.
                        LabelExpression = "trailer",
                        LabelOperator = ExpressionOperator.Contains,
                        Name = "My Event Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                            CurrencyCode = null
                        },
                        Scope = EntityScope.Account,
                        Status = ConversionGoalStatus.Active,
                        TagId = tagId,
                        // A numerical value associated with that event. 
                        // Could be length of the video played etc.
                        Value = 5.00m,
                        ValueOperator = ValueOperator.Equals,
                    },
                    new PagesViewedPerVisitGoal
                    {
                        ConversionWindowInMinutes = 30,
                        CountType = ConversionGoalCountType.All,
                        MinimumPagesViewed = 5,
                        Name = "My Pages Viewed Per Visit Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                            CurrencyCode = null
                        },
                        Scope = EntityScope.Account,
                        Status = ConversionGoalStatus.Active,
                        TagId = tagId,
                    },
                    new UrlGoal
                    {
                        ConversionWindowInMinutes = 30,
                        CountType = ConversionGoalCountType.All,
                        Name = "My Url Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                            CurrencyCode = null
                        },
                        Scope = EntityScope.Account,
                        Status = ConversionGoalStatus.Active,
                        TagId = tagId,
                        UrlExpression = "contoso",
                        UrlOperator = ExpressionOperator.Contains
                    },
                    new AppInstallGoal
                    {
                        // You must provide a valid app platform and app store identifier, 
                        // otherwise this goal will not be added successfully. 
                        AppPlatform = "Windows",
                        AppStoreId = "AppStoreIdGoesHere",
                        ConversionWindowInMinutes = 30,
                        CountType = ConversionGoalCountType.All,
                        Name = "My App Install Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                            CurrencyCode = null
                        },
                        // Account scope is not supported for app install goals. You can
                        // set scope to Customer or don't set it for the same result.
                        Scope = EntityScope.Customer,
                        Status = ConversionGoalStatus.Active,
                        // The TagId is inherited from the ConversionGoal base class,
                        // however, App Install goals do not use a UET tag.
                        TagId = null,
                    },
                };

                var addConversionGoalsResponse = await AddConversionGoalsAsync(conversionGoals);

                // Find the conversion goals that were added successfully. 

                List<long> conversionGoalIds = new List<long>();
                foreach (var goalId in addConversionGoalsResponse.ConversionGoalIds)
                {
                    if (goalId != null)
                    {
                        conversionGoalIds.Add((long)goalId);
                    }
                }

                OutputStatusMessage("List of errors returned from AddConversionGoals (if any):\n");
                OutputPartialErrors(addConversionGoalsResponse.PartialErrors);

                var conversionGoalTypes =
                    ConversionGoalType.AppInstall |
                    ConversionGoalType.Duration |
                    ConversionGoalType.Event |
                    ConversionGoalType.PagesViewedPerVisit |
                    ConversionGoalType.Url;

                var getConversionGoals = 
                    (await GetConversionGoalsByIdsAsync(conversionGoalIds, conversionGoalTypes)).ConversionGoals;

                OutputStatusMessage("List of conversion goals BEFORE update:\n");
                foreach (var conversionGoal in getConversionGoals)
                {
                    OutputConversionGoal(conversionGoal);
                }

                var updateConversionGoals = new ConversionGoal[]
                {
                    new DurationGoal
                    {
                        ConversionWindowInMinutes = 60,
                        CountType = ConversionGoalCountType.Unique,
                        // You can change the conversion goal type e.g. in this example an event goal
                        // had been created above at index 1. Now we are using the returned identifier
                        // at index 1 to update the type from EventGoal to DurationGoal.
                        Id = conversionGoalIds[1],
                        MinimumDurationInSeconds = 120,
                        Name = "My Updated Duration Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 10.00m,
                            CurrencyCode = null
                        },
                        // The Scope cannot be updated, even if you update the goal type.
                        // You can either send the same value or leave Scope empty.
                        Scope = EntityScope.Account,
                        Status = ConversionGoalStatus.Paused,
                        // You can update the tag as needed. In this example we will explicitly use the same UET tag.
                        // To keep the UET tag unchanged, you can also leave this element nil or empty.
                        TagId = tagId,
                    },
                    new EventGoal
                    {
                        // For both add and update conversion goal operations, you must include one or more  
                        // of the following event operator pairs: 
                        // (ActionOperator and ActionExpression), (CategoryOperator and CategoryExpression), 
                        // (LabelOperator and LabelExpression), (ValueOperator and Value).
                        // Each event pair (e.g. ActionOperator and ActionExpression) is optional if you include 
                        // one or more of the other events.

                        // For example if you do not include ActionOperator and ActionExpression during update, 
                        // any existing ActionOperator and ActionExpression settings will be deleted.
                        ActionExpression = null,
                        ActionOperator = null,
                        CategoryExpression = "video",
                        CategoryOperator = ExpressionOperator.Equals,
                        Id = conversionGoalIds[0],
                        // You cannot update the expression unless you also include the expression.
                        // Likewise, you cannot update the operator unless you also include the expression.
                        // The following attempt to update LabelOperator will result in an error.
                        LabelExpression = null,
                        LabelOperator = ExpressionOperator.Equals,
                        Name = "My Updated Event Goal " + DateTime.UtcNow,
                        Revenue = new ConversionGoalRevenue
                        {
                            Type = ConversionGoalRevenueType.FixedValue,
                            Value = 5.00m,
                            CurrencyCode = null
                        },
                        // You must specify the previous settings for Value and ValueOperator,
                        // unless you want them deleted during the update conversion goal operation.
                        Value = 5.00m,
                        ValueOperator = ValueOperator.Equals,
                    },
                    new PagesViewedPerVisitGoal
                    {
                        Id = conversionGoalIds[2],
                        Name = "My Updated Pages Viewed Per Visit Goal " + DateTime.UtcNow,
                        // When updating a conversion goal, if the Revenue element is nil or empty then none 
                        // of the nested properties will be updated. However, if this element is not nil or empty 
                        // then you are effectively replacing any existing revenue properties. For example to delete 
                        // any previous revenue settings, set the Revenue element to an empty ConversionGoalRevenue object.
                        Revenue = new ConversionGoalRevenue(),
                    },
                    new UrlGoal
                    {
                        Id = conversionGoalIds[3],
                        Name = "My Updated Url Goal" + DateTime.UtcNow,
                        // If not specified during update, the previous Url settings are retained.
                        UrlExpression = null,
                        UrlOperator = ExpressionOperator.BeginsWith
                    }
                };

                var updateConversionGoalsResponse = await UpdateConversionGoalsAsync(updateConversionGoals);

                OutputStatusMessage("List of errors returned from UpdateConversionGoals (if any):\n");
                OutputPartialErrors(updateConversionGoalsResponse.PartialErrors);

                getConversionGoals = 
                    (await GetConversionGoalsByIdsAsync(conversionGoalIds, conversionGoalTypes)).ConversionGoals;

                OutputStatusMessage("List of conversion goals AFTER update:\n");
                foreach (var conversionGoal in getConversionGoals)
                {
                    OutputConversionGoal(conversionGoal);
                }
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch ConversionGoal Management service exceptions
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        // Adds one or more conversion goals.

        private async Task<AddConversionGoalsResponse> AddConversionGoalsAsync(IList<ConversionGoal> conversionGoals)
        {
            var request = new AddConversionGoalsRequest
            {
                ConversionGoals = conversionGoals
            };

            return await Service.CallAsync((s, r) => s.AddConversionGoalsAsync(r), request);
        }

        // Gets one or more conversion goals for the specified conversion goal identifiers.

        private async Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(
            IList<long> conversionGoalIds,
            ConversionGoalType conversionGoalTypes
            )
        {
            var request = new GetConversionGoalsByIdsRequest
            {
                ConversionGoalIds = conversionGoalIds,
                ConversionGoalTypes = conversionGoalTypes
            };

            return await Service.CallAsync((s, r) => s.GetConversionGoalsByIdsAsync(r), request);
        }

        // Updates one or more conversion goals.

        private async Task<UpdateConversionGoalsResponse> UpdateConversionGoalsAsync(IList<ConversionGoal> conversionGoals)
        {
            var request = new UpdateConversionGoalsRequest
            {
                ConversionGoals = conversionGoals
            };

            return await Service.CallAsync((s, r) => s.UpdateConversionGoalsAsync(r), request);
        }
        
        // Adds one or more UET tags.

        private async Task<AddUetTagsResponse> AddUetTagsAsync(IList<UetTag> uetTags)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = uetTags,
            };

            return (await Service.CallAsync((s, r) => s.AddUetTagsAsync(r), request));
        }

        // Gets one or more UET Tags.

        private async Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(IList<long> tagIds)
        {
            var request = new GetUetTagsByIdsRequest
            {
                TagIds = tagIds,
            };

            return await Service.CallAsync((s, r) => s.GetUetTagsByIdsAsync(r), request);
        }

        // Updates one or more UET tags.

        private async Task UpdateUetTagsAsync(IList<UetTag> uetTags)
        {
            var request = new UpdateUetTagsRequest
            {
                UetTags = uetTags
            };

            await Service.CallAsync((s, r) => s.UpdateUetTagsAsync(r), request);
        }
    }
}
