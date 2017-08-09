using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.AdInsight;
using Microsoft.BingAds.V11.CampaignManagement;
using BatchError = Microsoft.BingAds.V11.CampaignManagement.BatchError;
using Criterion = Microsoft.BingAds.V11.CampaignManagement.Criterion;
using DeviceCriterion = Microsoft.BingAds.V11.CampaignManagement.DeviceCriterion;
using LocationCriterion = Microsoft.BingAds.V11.CampaignManagement.LocationCriterion;
using Keyword = Microsoft.BingAds.V11.CampaignManagement.Keyword;
using NegativeKeyword = Microsoft.BingAds.V11.CampaignManagement.NegativeKeyword;
using Microsoft.BingAds.V11.CustomerManagement;
using Microsoft.BingAds;


namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads examples. 
    /// </summary>
    public abstract class ExampleBase : BingAdsExamplesLibrary.ExampleBase
    {
        /// <summary>
        /// Provides a proxy for using the Ad Insight Service. 
        /// </summary>
        protected static ServiceClient<IAdInsightService> AdInsightService;

        /// <summary>
        /// Provides a proxy for using the Campaign Management Service. 
        /// </summary>
        protected static ServiceClient<ICampaignManagementService> CampaignService;

        /// <summary>
        /// Provides a proxy for using the Customer Management Service. 
        /// </summary>
        protected static ServiceClient<ICustomerManagementService> CustomerService;

        protected static CampaignType AllCampaignTypes = CampaignType.DynamicSearchAds |
                                    CampaignType.SearchAndContent |
                                    CampaignType.Shopping;

        protected static List<AdType> AllAdTypes = new List<AdType>
        {
            AdType.AppInstall,
            AdType.DynamicSearch,
            AdType.ExpandedText,
            AdType.Product,
            AdType.Text
        };

        protected static CampaignCriterionType AllTargetCampaignCriterionTypes = CampaignCriterionType.Age |
                                    CampaignCriterionType.DayTime |
                                    CampaignCriterionType.Device |
                                    CampaignCriterionType.Gender |
                                    CampaignCriterionType.Location |
                                    CampaignCriterionType.LocationIntent |
                                    CampaignCriterionType.Radius;

        protected static AdGroupCriterionType AllTargetAdGroupCriterionTypes = AdGroupCriterionType.Age |
                                    AdGroupCriterionType.DayTime |
                                    AdGroupCriterionType.Device |
                                    AdGroupCriterionType.Gender |
                                    AdGroupCriterionType.Location |
                                    AdGroupCriterionType.LocationIntent |
                                    AdGroupCriterionType.Radius;

        /// <summary>
        /// Initializes a new instance of the ExampleBase class, and sets the default output status message.
        /// </summary>
        protected ExampleBase()
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }

        /// <summary>
        /// Write to the console by default, if the example does not implement its own OutputStatusMessage method.
        /// </summary>
        /// <param name="msg">The message sent to console output.</param>
        private void OutputStatusMessageDefault(String msg)
        {
            Console.WriteLine(msg);
        }


        #region AdInsight_Output

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
                    OutputStatusMessage(string.Format("BudgetType: {0}", budgetOpportunity.BudgetType));
                    OutputStatusMessage(string.Format("CampaignId: {0}", budgetOpportunity.CampaignId));
                    OutputStatusMessage(string.Format("CurrentBudget: {0}", budgetOpportunity.CurrentBudget));
                    OutputStatusMessage(string.Format("IncreaseInClicks: {0}", budgetOpportunity.IncreaseInClicks));
                    OutputStatusMessage(string.Format("IncreaseInImpressions: {0}", budgetOpportunity.IncreaseInImpressions));
                    OutputStatusMessage(string.Format("OpportunityKey: {0}", budgetOpportunity.OpportunityKey));
                    OutputStatusMessage(string.Format("PercentageIncreaseInClicks: {0}", budgetOpportunity.PercentageIncreaseInClicks));
                    OutputStatusMessage(string.Format("PercentageIncreaseInImpressions: {0}", budgetOpportunity.PercentageIncreaseInImpressions));
                    OutputStatusMessage(string.Format("RecommendedBudget: {0}", budgetOpportunity.RecommendedBudget));
                }
            }
            else
            {
                OutputStatusMessage(string.Format("There are no budget opportunities for CampaignId: {0}", campaignId));
            }
        }

        protected void OutputBudgetPoint(BudgetPoint budgetPoint)
        {
            if (budgetPoint != null)
            {
                OutputStatusMessage(string.Format("BudgetAmount: {0}", budgetPoint.BudgetAmount));
                OutputStatusMessage(string.Format("BudgetPointType: {0}", budgetPoint.BudgetPointType));
                OutputStatusMessage(string.Format("EstimatedWeeklyClicks: {0}", budgetPoint.EstimatedWeeklyClicks));
                OutputStatusMessage(string.Format("EstimatedWeeklyCost: {0}", budgetPoint.EstimatedWeeklyCost));
                OutputStatusMessage(string.Format("EstimatedWeeklyImpressions: {0}", budgetPoint.EstimatedWeeklyImpressions));
            }
        }

        protected void OutputKeywordIdeas(IList<KeywordIdea> keywordIdeas)
        {
            if (keywordIdeas != null)
            {
                foreach (var keywordIdea in keywordIdeas)
                {
                    if (keywordIdea != null)
                    {
                        OutputStatusMessage(string.Format("AdGroupId: {0}", keywordIdea.AdGroupId));
                        OutputStatusMessage(string.Format("AdGroupName: {0}", keywordIdea.AdGroupName));
                        OutputStatusMessage(string.Format("AdImpressionShare: {0}", keywordIdea.AdImpressionShare));
                        OutputStatusMessage(string.Format("Competition: {0}", keywordIdea.Competition));
                        OutputStatusMessage(string.Format("Keyword: {0}", keywordIdea.Keyword));
                        if (keywordIdea.MonthlySearchCounts.Count > 0)
                        {
                            OutputStatusMessage(string.Format("MonthlySearchCounts for last {0} months:", keywordIdea.MonthlySearchCounts.Count));
                            var countDuration = DateTime.UtcNow.AddMonths(-keywordIdea.MonthlySearchCounts.Count);
                            for (int index = 0; index < keywordIdea.MonthlySearchCounts.Count; index++)
                            {
                                OutputStatusMessage(string.Format("{0}/{1}: {2}",
                                    countDuration.Month,
                                    countDuration.Year,
                                    keywordIdea.MonthlySearchCounts[index]));
                                countDuration = countDuration.AddMonths(1);
                            }

                            OutputStatusMessage(string.Format("Average MonthlySearchCounts (Client Side Calculation): {0}",
                                keywordIdea.MonthlySearchCounts.Sum() / keywordIdea.MonthlySearchCounts.Count));
                        }
                        OutputStatusMessage(string.Format("Relevance: {0}", keywordIdea.Relevance));
                        OutputStatusMessage(string.Format("Source: {0}", keywordIdea.Source));
                        OutputStatusMessage(string.Format("SuggestedBid: {0}", keywordIdea.SuggestedBid));
                        OutputStatusMessage("\n");
                    }
                }
            }
        }

        protected void OutputCampaignEstimates(IList<CampaignEstimate> campaignEstimates)
        {
            if (campaignEstimates != null)
            {
                foreach (var campaignEstimate in campaignEstimates)
                {
                    OutputStatusMessage(string.Format("CampaignId: {0}", campaignEstimate.CampaignId));
                    OutputAdGroupEstimates(campaignEstimate.AdGroupEstimates);
                }
            }
        }

        protected void OutputAdGroupEstimates(IList<AdGroupEstimate> adGroupEstimates)
        {
            if (adGroupEstimates != null)
            {
                foreach (var adGroupEstimate in adGroupEstimates)
                {
                    OutputStatusMessage(string.Format("AdGroupId: {0}", adGroupEstimate.AdGroupId));
                    OutputKeywordEstimates(adGroupEstimate.KeywordEstimates);
                }
            }
        }

        protected void OutputKeywordEstimates(IList<KeywordEstimate> keywordEstimates)
        {
            if (keywordEstimates != null)
            {
                foreach (var keywordEstimate in keywordEstimates)
                {
                    OutputStatusMessage("KeywordEstimate Keyword:");
                    OutputKeyword(keywordEstimate.Keyword);
                    OutputStatusMessage("KeywordEstimate Maximum TrafficEstimate:");
                    OutputTrafficEstimate(keywordEstimate.Maximum);
                    OutputStatusMessage("KeywordEstimate Minimum TrafficEstimate:");
                    OutputTrafficEstimate(keywordEstimate.Minimum);
                    OutputStatusMessage("\n");
                }
            }
        }

        protected void OutputKeyword(Microsoft.BingAds.V11.AdInsight.Keyword keyword)
        {
            if (keyword != null)
            {
                OutputStatusMessage(string.Format("Id: {0}", keyword.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", keyword.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", keyword.Text));
            }
        }

        protected void OutputTrafficEstimate(TrafficEstimate estimate)
        {
            if (estimate != null)
            {
                OutputStatusMessage(string.Format("AverageCpc: {0}", estimate.AverageCpc));
                OutputStatusMessage(string.Format("AveragePosition: {0}", estimate.AveragePosition));
                OutputStatusMessage(string.Format("Clicks: {0}", estimate.Clicks));
                OutputStatusMessage(string.Format("Ctr: {0}", estimate.Ctr));
                OutputStatusMessage(string.Format("Impressions: {0}", estimate.Impressions));
                OutputStatusMessage(string.Format("TotalCost: {0}", estimate.TotalCost));
            }
        }

        #endregion AdInsight_Output

        #region AdInsight_ServiceOperations

        protected async Task<GetBudgetOpportunitiesResponse> GetBudgetOpportunitiesAsync(long campaignId)
        {
            var request = new GetBudgetOpportunitiesRequest
            {
                CampaignId = campaignId
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetBudgetOpportunitiesAsync(r), request));
        }

        protected async Task<GetDomainCategoriesResponse> GetDomainCategoriesAsync(
            string domainName,
            string language)
        {
            var request = new GetDomainCategoriesRequest
            {
                DomainName = domainName,
                Language = language
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetDomainCategoriesAsync(r), request));
        }

        protected async Task<GetKeywordIdeaCategoriesResponse> GetKeywordIdeaCategoriesAsync()
        {
            var request = new GetKeywordIdeaCategoriesRequest { };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordIdeaCategoriesAsync(r), request));
        }

        protected async Task<GetKeywordIdeasResponse> GetKeywordIdeasAsync(
            bool expandIdeas,
            IList<KeywordIdeaAttribute> ideaAttributes,
            IList<SearchParameter> searchParameters)
        {
            var request = new GetKeywordIdeasRequest
            {
                ExpandIdeas = expandIdeas,
                IdeaAttributes = ideaAttributes,
                SearchParameters = searchParameters
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordIdeasAsync(r), request));
        }

        protected async Task<GetKeywordTrafficEstimatesResponse> GetKeywordTrafficEstimatesAsync(
            IList<CampaignEstimator> campaigns)
        {
            var request = new GetKeywordTrafficEstimatesRequest
            {
                CampaignEstimators = campaigns
            };

            return (await AdInsightService.CallAsync((s, r) => s.GetKeywordTrafficEstimatesAsync(r), request));
        }

        #endregion AdInsight_ServiceOperations

        #region CampaignManagement_Output

        /// <summary>
        /// Outputs the Campaign.
        /// </summary>
        protected void OutputCampaign(Campaign campaign)
        {
            if (campaign != null)
            {
                OutputBiddingScheme(campaign.BiddingScheme);
                OutputStatusMessage(string.Format("BudgetId: {0}", campaign.BudgetId));
                OutputStatusMessage(string.Format("BudgetType: {0}", campaign.BudgetType));
                OutputStatusMessage(string.Format("CampaignType: {0}", campaign.CampaignType));
                OutputStatusMessage(string.Format("DailyBudget: {0}", campaign.DailyBudget));
                OutputStatusMessage(string.Format("Description: {0}", campaign.Description));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (campaign.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in campaign.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", campaign.Id));
                OutputStatusMessage(string.Format("Name: {0}", campaign.Name));
                OutputStatusMessage("Settings:");
                if (campaign.Settings != null)
                {
                    foreach (var setting in campaign.Settings)
                    {
                        var shoppingSetting = setting as ShoppingSetting;
                        if (shoppingSetting != null)
                        {
                            OutputStatusMessage("ShoppingSetting:");
                            OutputStatusMessage(string.Format("Priority: {0}", shoppingSetting.Priority));
                            OutputStatusMessage(string.Format("SalesCountryCode: {0}", shoppingSetting.SalesCountryCode));
                            OutputStatusMessage(string.Format("StoreId: {0}", shoppingSetting.StoreId));
                        }
                    }
                }
                OutputStatusMessage(string.Format("Status: {0}", campaign.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", campaign.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (campaign.UrlCustomParameters != null && campaign.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in campaign.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
                OutputStatusMessage(string.Format("TimeZone: {0}", campaign.TimeZone));
            }
        }

        /// <summary>
        /// Outputs the AdGroup.
        /// </summary>
        protected void OutputAdGroup(AdGroup adGroup)
        {
            if (adGroup != null)
            {
                OutputStatusMessage(string.Format("AdDistribution: {0}", adGroup.AdDistribution));
                OutputStatusMessage(string.Format("AdRotation Type: {0}",
                    adGroup.AdRotation != null ? adGroup.AdRotation.Type : null));
                OutputBiddingScheme(adGroup.BiddingScheme);
                if (adGroup.EndDate != null)
                {
                    OutputStatusMessage(string.Format("EndDate: {0}/{1}/{2}",
                    adGroup.EndDate.Month,
                    adGroup.EndDate.Day,
                    adGroup.EndDate.Year));
                }
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (adGroup.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in adGroup.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", adGroup.Id));
                OutputStatusMessage(string.Format("Language: {0}", adGroup.Language));
                OutputStatusMessage(string.Format("Name: {0}", adGroup.Name));
                OutputStatusMessage(string.Format("NativeBidAdjustment: {0}", adGroup.NativeBidAdjustment));
                OutputStatusMessage(string.Format("Network: {0}", adGroup.Network));
                OutputStatusMessage(string.Format("PricingModel: {0}", adGroup.PricingModel));
                OutputStatusMessage(string.Format("SearchBid: {0}",
                    adGroup.SearchBid != null ? adGroup.SearchBid.Amount : 0));
                if (adGroup.StartDate != null)
                {
                    OutputStatusMessage(string.Format("StartDate: {0}/{1}/{2}",
                    adGroup.StartDate.Month,
                    adGroup.StartDate.Day,
                    adGroup.StartDate.Year));
                }
                OutputStatusMessage(string.Format("Status: {0}", adGroup.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", adGroup.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (adGroup.UrlCustomParameters != null && adGroup.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in adGroup.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }
        
        /// <summary>
        /// Outputs the list of ads
        /// </summary>
        /// <param name="ads"></param>
        protected void OutputAds(IList<Ad> ads)
        {
            if (ads != null)
            {
                foreach(var ad in ads)
                {
                    var expandedTextAd = ad as ExpandedTextAd;
                    if (expandedTextAd != null)
                    {
                        OutputExpandedTextAd(expandedTextAd);
                    }
                    else
                    {
                        var textAd = ad as TextAd;
                        if (textAd != null)
                        {
                            OutputTextAd(textAd);
                        }
                        else
                        {
                            var productAd = ad as ProductAd;
                            if (productAd != null)
                            {
                                OutputProductAd(productAd);
                            }
                            else
                            {
                                OutputStatusMessage("Unknown Ad Type");
                            }
                        }
                    }

                    OutputStatusMessage("\n");
                }
            }
        }

        /// <summary>
        /// Set the read-only properties of an ad extension to null. This operation can be useful between calls to
        /// GetAdExtensionsByIds and UpdateAdExtensions. The update operation would fail if you send certain read-only
        /// fields.
        /// </summary>
        /// <param name="extension">The ad extension whose read-only properties you want to nullify.</param>
        /// <returns></returns>
        protected AdExtension SetReadOnlyAdExtensionElementsToNull(AdExtension extension)
        {
            if (extension == null)
            {
                return extension;
            }
            else
            {
                // Set to null for all extension types.
                extension.Version = null;

                var locationAdExtension = extension as LocationAdExtension;
                if (locationAdExtension != null)
                {
                    locationAdExtension.GeoCodeStatus = null;
                    return locationAdExtension;
                }
                else
                {
                    return extension;
                }
            }
        }

        /// <summary>
        /// Outputs the ad extensions with any editorial reasons
        /// </summary>
        /// <param name="adExtensions"></param>
        /// <param name="adExtensionEditorialReasonCollection"></param>
        protected void OutputAdExtensionsWithEditorialReasons(IEnumerable<AdExtension> adExtensions,
            IList<AdExtensionEditorialReasonCollection> adExtensionEditorialReasonCollection)
        {
            int index = 0;

            foreach (var extension in adExtensions)
            {
                if (extension == null)
                {
                    OutputStatusMessage("Extension is null or invalid.");
                }
                else
                {
                    var appAdExtension = extension as AppAdExtension;
                    if (appAdExtension != null)
                    {
                        OutputAppAdExtension(appAdExtension);
                        OutputStatusMessage("\n");
                    }
                    else
                    {
                        var callAdExtension = extension as CallAdExtension;
                        if (callAdExtension != null)
                        {
                            OutputCallAdExtension(callAdExtension);
                            OutputStatusMessage("\n");
                        }
                        else
                        {
                            var imageAdExtension = extension as ImageAdExtension;
                            if (imageAdExtension != null)
                            {
                                OutputImageAdExtension(imageAdExtension);
                                OutputStatusMessage("\n");
                            }
                            else
                            {
                                var locationAdExtension = extension as LocationAdExtension;
                                if (locationAdExtension != null)
                                {
                                    OutputLocationAdExtension(locationAdExtension);
                                    OutputStatusMessage("\n");
                                }
                                else
                                {
                                    var siteLinksAdExtension = extension as SiteLinksAdExtension;
                                    if (siteLinksAdExtension != null)
                                    {
                                        OutputSiteLinksAdExtension(siteLinksAdExtension);
                                        OutputStatusMessage("\n");
                                    }
                                    else
                                    {
                                        var sitelink2AdExtension = extension as Sitelink2AdExtension;
                                        if (sitelink2AdExtension != null)
                                        {
                                            OutputSitelink2AdExtension(sitelink2AdExtension);
                                            OutputStatusMessage("\n");
                                        }
                                        else
                                        {
                                            var calloutAdExtension = extension as CalloutAdExtension;
                                            if (calloutAdExtension != null)
                                            {
                                                OutputCalloutAdExtension(calloutAdExtension);
                                                OutputStatusMessage("\n");
                                            }
                                            else
                                            {
                                                var reviewAdExtension = extension as ReviewAdExtension;
                                                if (reviewAdExtension != null)
                                                {
                                                    OutputReviewAdExtension(reviewAdExtension);
                                                    OutputStatusMessage("\n");
                                                }
                                                else
                                                {
                                                    var structuredSnippetAdExtension = extension as StructuredSnippetAdExtension;
                                                    if (structuredSnippetAdExtension != null)
                                                    {
                                                        OutputStructuredSnippetAdExtension(structuredSnippetAdExtension);
                                                        OutputStatusMessage("\n");
                                                    }
                                                    else
                                                    {
                                                        OutputStatusMessage("Unknown extension type");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (adExtensionEditorialReasonCollection != null
                        && adExtensionEditorialReasonCollection.Count > 0
                        && adExtensionEditorialReasonCollection[index] != null)
                    {
                        // Print any editorial rejection reasons for the corresponding extension. This example 
                        // assumes the same list index for adExtensions and adExtensionEditorialReasonCollection
                        // as defined above.

                        foreach (var adExtensionEditorialReason in adExtensionEditorialReasonCollection[index].Reasons)
                        {
                            if (adExtensionEditorialReason != null &&
                                adExtensionEditorialReason.PublisherCountries != null)
                            {
                                OutputStatusMessage("Editorial Rejection Location: " + adExtensionEditorialReason.Location);
                                OutputStatusMessage("Editorial Rejection PublisherCountries: ");
                                foreach (var publisherCountry in adExtensionEditorialReason.PublisherCountries)
                                {
                                    OutputStatusMessage("  " + publisherCountry);
                                }
                                OutputStatusMessage("Editorial Rejection ReasonCode: " + adExtensionEditorialReason.ReasonCode);
                                OutputStatusMessage("Editorial Rejection Term: " + adExtensionEditorialReason.Term);
                                OutputStatusMessage("\n");
                            }
                        }
                    }
                }
                index++;
            }
        }

        /// <summary>
        /// Outputs the AdGroupCriterion.
        /// </summary>
        protected void OutputAdGroupCriterion(AdGroupCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", criterion.AdGroupId));
                OutputCriterion(criterion.Criterion);
                OutputStatusMessage(string.Format("Id: {0}", criterion.Id));
                OutputStatusMessage(string.Format("Status: {0}", criterion.Status));
                OutputStatusMessage(string.Format("AdGroupCriterion Type: {0}", criterion.Type));
            }
        }

        /// <summary>
        /// Outputs the Criterion.
        /// </summary>
        protected void OutputCriterion(Criterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("Criterion Type: {0}", criterion.Type));

                var productPartition = criterion as ProductPartition;
                if (productPartition != null) OutputProductPartition((ProductPartition)criterion);

                var productScope = criterion as ProductScope;
                if (productScope != null) OutputProductScope((ProductScope)criterion);

                var webpage = criterion as Webpage;
                if (webpage != null) OutputWebpage((Webpage)criterion);

                var audienceCriterion = criterion as AudienceCriterion;
                if (audienceCriterion != null) OutputAudienceCriterion((AudienceCriterion)criterion);

                var ageCriterion = criterion as AgeCriterion;
                if (ageCriterion != null) OutputAgeCriterion((AgeCriterion)criterion);

                var dayTimeCriterion = criterion as DayTimeCriterion;
                if (dayTimeCriterion != null) OutputDayTimeCriterion((DayTimeCriterion)criterion);

                var deviceCriterion = criterion as DeviceCriterion;
                if (deviceCriterion != null) OutputDeviceCriterion((DeviceCriterion)criterion);

                var genderCriterion = criterion as GenderCriterion;
                if (genderCriterion != null) OutputGenderCriterion((GenderCriterion)criterion);

                var locationCriterion = criterion as LocationCriterion;
                if (locationCriterion != null) OutputLocationCriterion((LocationCriterion)criterion);

                var locationIntentCriterion = criterion as LocationIntentCriterion;
                if (locationIntentCriterion != null) OutputLocationIntentCriterion((LocationIntentCriterion)criterion);

                var radiusCriterion = criterion as RadiusCriterion;
                if (radiusCriterion != null) OutputRadiusCriterion((RadiusCriterion)criterion);
            }
        }
        
        /// <summary>
        /// Outputs the ProductPartition.
        /// </summary>
        protected void OutputProductPartition(ProductPartition productPartition)
        {
            if (productPartition != null)
            {
                OutputStatusMessage(string.Format("ParentCriterionId: {0}", productPartition.ParentCriterionId));
                OutputStatusMessage(string.Format("PartitionType: {0}", productPartition.PartitionType));
                if (productPartition.Condition != null)
                {
                    OutputStatusMessage(string.Format("Condition: "));
                    OutputStatusMessage(string.Format("\tOperand: {0}", productPartition.Condition.Operand));
                    OutputStatusMessage(string.Format("\tAttribute: {0}", productPartition.Condition.Attribute));
                }
            }
        }

        /// <summary>
        /// Outputs the ProductScope.
        /// </summary>
        protected void OutputProductScope(ProductScope productScope)
        {
            if (productScope != null)
            {
                OutputStatusMessage(string.Format("Product Conditions:"));
                foreach (var condition in productScope.Conditions)
                {
                    OutputStatusMessage(string.Format("Operand: {0}", condition.Operand));
                    OutputStatusMessage(string.Format("Attribute: {0}", condition.Attribute));
                }
            }
        }

        /// <summary>
        /// Outputs the Webpage.
        /// </summary>
        protected void OutputWebpage(Webpage webpage)
        {
            if (webpage != null && webpage.Parameter != null && webpage.Parameter.Conditions != null)
            {
                OutputStatusMessage(string.Format("Webpage CriterionName: {0}", webpage.Parameter.CriterionName));
                OutputStatusMessage("Webpage Conditions:");
                foreach (var condition in webpage.Parameter.Conditions)
                {
                    OutputStatusMessage(string.Format("\tOperand: {0}", condition.Operand));
                    OutputStatusMessage(string.Format("\tArgument: {0}", condition.Argument));
                }
            }
        }

        /// <summary>
        /// Outputs the AgeCriterion.
        /// </summary>
        protected void OutputAgeCriterion(AgeCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("AgeRange: {0}", criterion.AgeRange));
            }
        }

        /// <summary>
        /// Outputs the DayTimeCriterion.
        /// </summary>
        protected void OutputDayTimeCriterion(DayTimeCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("Day: {0}", criterion.Day));
                OutputStatusMessage(string.Format("FromHour: {0}", criterion.FromHour));
                OutputStatusMessage(string.Format("FromMinute: {0}", criterion.FromMinute));
                OutputStatusMessage(string.Format("ToHour: {0}", criterion.ToHour));
                OutputStatusMessage(string.Format("ToMinute: {0}", criterion.ToMinute));
            }
        }

        /// <summary>
        /// Outputs the DeviceCriterion.
        /// </summary>
        protected void OutputDeviceCriterion(DeviceCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("DeviceName: {0}", criterion.DeviceName));
            }
        }

        /// <summary>
        /// Outputs the GenderCriterion.
        /// </summary>
        protected void OutputGenderCriterion(GenderCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("GenderType: {0}", criterion.GenderType));
            }
        }

        /// <summary>
        /// Outputs the LocationCriterion.
        /// </summary>
        protected void OutputLocationCriterion(LocationCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("DisplayName: {0}", criterion.DisplayName));
                OutputStatusMessage(string.Format("LocationId: {0}", criterion.LocationId));
                OutputStatusMessage(string.Format("LocationType: {0}", criterion.LocationType));
            }
        }

        /// <summary>
        /// Outputs the LocationIntentCriterion.
        /// </summary>
        protected void OutputLocationIntentCriterion(LocationIntentCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("IntentOption: {0}", criterion.IntentOption));
            }
        }

        /// <summary>
        /// Outputs the RadiusCriterion.
        /// </summary>
        protected void OutputRadiusCriterion(RadiusCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("LatitudeDegrees: {0}", criterion.LatitudeDegrees));
                OutputStatusMessage(string.Format("LongitudeDegrees: {0}", criterion.LongitudeDegrees));
                OutputStatusMessage(string.Format("Name: {0}", criterion.Name));
                OutputStatusMessage(string.Format("Radius: {0}", criterion.Radius));
                OutputStatusMessage(string.Format("RadiusUnit: {0}", criterion.RadiusUnit));
            }
        }


        /// <summary>
        /// Outputs the CriterionBid.
        /// </summary>
        protected void OutputCriterionBid(CriterionBid criterionBid)
        {
            if (criterionBid != null)
            {
                OutputStatusMessage(string.Format("CriterionBid Type: {0}", criterionBid.Type));
                var fixedBid = criterionBid as FixedBid;
                if (fixedBid != null)
                {
                    OutputFixedBid(fixedBid);
                }
                else
                {
                    var bidMultiplier = criterionBid as BidMultiplier;
                    if (bidMultiplier != null)
                    {
                        OutputBidMultiplier(bidMultiplier);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the FixedBid.
        /// </summary>
        protected void OutputFixedBid(FixedBid fixedBid)
        {
            if (fixedBid != null)
            {
                OutputStatusMessage(string.Format("Fixed Bid Amount: {0}", fixedBid.Amount));
            }
        }

        /// <summary>
        /// Outputs the BidMultiplier.
        /// </summary>
        protected void OutputBidMultiplier(BidMultiplier bidMultiplier)
        {
            if (bidMultiplier != null)
            {
                OutputStatusMessage(string.Format("Bid Multiplier: {0}", bidMultiplier.Multiplier));
            }
        }

        /// <summary>
        /// Outputs the BiddableAdGroupCriterion.
        /// </summary>
        protected void OutputBiddableAdGroupCriterion(BiddableAdGroupCriterion criterion)
        {
            if (criterion != null)
            {
                // Output inherited properties of the AdGroupCriterion base class.
                OutputAdGroupCriterion(criterion);

                // Output properties that are specific to the BiddableAdGroupCriterion
                OutputCriterionBid(criterion.CriterionBid);
                
                OutputStatusMessage(string.Format("DestinationUrl: {0}", criterion.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", criterion.EditorialStatus));
                OutputStatusMessage("FinalMobileUrls: ");
                if (criterion.FinalMobileUrls != null)
                {
                    foreach (var finalMobileUrl in criterion.FinalMobileUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                    }
                }

                OutputStatusMessage("FinalUrls: ");
                if (criterion.FinalUrls != null)
                {
                    foreach (var finalUrl in criterion.FinalUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalUrl));
                    }
                }
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", criterion.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (criterion.UrlCustomParameters != null && criterion.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in criterion.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the NegativeAdGroupCriterion.
        /// </summary>
        protected void OutputNegativeAdGroupCriterion(NegativeAdGroupCriterion criterion)
        {
            if (criterion != null)
            {
                // Output inherited properties of the AdGroupCriterion base class.
                OutputAdGroupCriterion(criterion);

                // There aren't any properties that are specific to the NegativeAdGroupCriterion
            }
        }

        /// <summary>
        /// Outputs the CampaignCriterion.
        /// </summary>
        protected void OutputCampaignCriterion(CampaignCriterion criterion)
        {
            if (criterion != null)
            {
                OutputStatusMessage(string.Format("CampaignId: {0}", criterion.CampaignId));
                OutputCriterion(criterion.Criterion);
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (criterion.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in criterion.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", criterion.Id));
                OutputStatusMessage(string.Format("Status: {0}", criterion.Status));
                OutputStatusMessage(string.Format("CampaignCriterion Type: {0}", criterion.Type));
            }
        }

        /// <summary>
        /// Outputs the BiddableCampaignCriterion.
        /// </summary>
        protected void OutputBiddableCampaignCriterion(BiddableCampaignCriterion criterion)
        {
            if (criterion != null)
            {
                // Output inherited properties of the CampaignCriterion base class.
                OutputCampaignCriterion(criterion);

                // Output properties that are specific to the BiddableCampaignCriterion
                OutputCriterionBid(criterion.CriterionBid);
            }
        }


        /// <summary>
        /// Outputs the NegativeCampaignCriterion.
        /// </summary>
        protected void OutputNegativeCampaignCriterion(NegativeCampaignCriterion criterion)
        {
            if (criterion != null)
            {
                // Output inherited properties of the CampaignCriterion base class.
                OutputCampaignCriterion(criterion);

                // There aren't any properties that are specific to the NegativeCampaignCriterion
            }
        }

        /// <summary>
        /// Outputs the ad group criterions.
        /// </summary>
        /// <param name="criterions"></param>
        protected void OutputAdGroupCriterions(IEnumerable<AdGroupCriterion> criterions)
        {
            foreach (var criterion in criterions)
            {
                if (criterion == null)
                {
                    OutputStatusMessage("Criterion is null or invalid.");
                }
                else
                {
                    var biddableAdGroupCriterion = criterion as BiddableAdGroupCriterion;
                    if (biddableAdGroupCriterion != null)
                    {
                        OutputBiddableAdGroupCriterion(biddableAdGroupCriterion);
                        OutputStatusMessage("\n");
                    }
                    else
                    {
                        var negativeAdGroupCriterion = criterion as NegativeAdGroupCriterion;
                        if (negativeAdGroupCriterion != null)
                        {
                            OutputNegativeAdGroupCriterion(negativeAdGroupCriterion);
                            OutputStatusMessage("\n");
                        }
                        else
                        {
                            OutputStatusMessage("Unknown ad group criterion type");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the campaign criterions.
        /// </summary>
        /// <param name="criterions"></param>
        protected void OutputCampaignCriterions(IEnumerable<CampaignCriterion> criterions)
        {
            foreach (var criterion in criterions)
            {
                if (criterion == null)
                {
                    OutputStatusMessage("Criterion is null or invalid.");
                }
                else
                {
                    var biddableCampaignCriterion = criterion as BiddableCampaignCriterion;
                    if (biddableCampaignCriterion != null)
                    {
                        OutputBiddableCampaignCriterion(biddableCampaignCriterion);
                        OutputStatusMessage("\n");
                    }
                    else
                    {
                        var negativeCampaignCriterion = criterion as NegativeCampaignCriterion;
                        if (negativeCampaignCriterion != null)
                        {
                            OutputNegativeCampaignCriterion(negativeCampaignCriterion);
                            OutputStatusMessage("\n");
                        }
                        else
                        {
                            OutputStatusMessage("Unknown campaign criterion type");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the negative keyword identifiers added to each campaign or ad group entity. 
        /// The IdCollection items are returned by calling AddNegativeKeywordsToEntities. 
        /// </summary>
        /// <param name="idCollections"></param>
        protected void OutputNegativeKeywordIds(IList<IdCollection> idCollections)
        {
            if (idCollections == null)
            {
                return;
            }

            for (int index = 0; index < idCollections.Count; index++)
            {
                OutputStatusMessage(string.Format("NegativeKeyword Ids at entity index {0}:\n", index));
                foreach (var id in idCollections[index].Ids)
                {
                    OutputStatusMessage(string.Format("\tId: {0}\n", id));
                }
            }
        }


        /// <summary>
        /// Outputs a list of EntityNegativeKeyword objects
        /// </summary>
        /// <param name="entityNegativeKeywords"></param>
        protected void OutputEntityNegativeKeywords(IEnumerable<EntityNegativeKeyword> entityNegativeKeywords)
        {
            if (entityNegativeKeywords == null)
            {
                return;
            }

            OutputStatusMessage("EntityNegativeKeyword item:\n");
            foreach (EntityNegativeKeyword entityNegativeKeyword in entityNegativeKeywords)
            {
                OutputStatusMessage(string.Format("\tEntityId: {0}", entityNegativeKeyword.EntityId));
                OutputStatusMessage(string.Format("\tEntityType: {0}\n", entityNegativeKeyword.EntityType));
                OutputNegativeKeywords(entityNegativeKeyword.NegativeKeywords);
            }
        }

        /// <summary>
        /// Outputs the negative keywords
        /// </summary>
        /// <param name="negativeKeywords"></param>
        protected void OutputNegativeKeywords(IEnumerable<NegativeKeyword> negativeKeywords)
        {
            if (negativeKeywords == null)
            {
                return;
            }

            OutputStatusMessage("NegativeKeyword item:\n");
            foreach (var negativeKeyword in negativeKeywords)
            {
                OutputStatusMessage(string.Format("\tText: {0}", negativeKeyword.Text));
                OutputStatusMessage(string.Format("\tId: {0}", negativeKeyword.Id));
                OutputStatusMessage(string.Format("\tMatchType: {0}\n", negativeKeyword.MatchType));
            }
        }

        /// <summary>
        /// Outputs the list item identifiers, as well as any partial errors
        /// </summary>
        /// <param name="sharedListId"></param>
        /// <param name="sharedListItems"></param>
        /// <param name="sharedListItemIds"></param>
        /// <param name="partialErrors"></param>
        protected void OutputNegativeKeywordsWithPartialErrors(
            long sharedListId,
            SharedListItem[] sharedListItems,
            long[] sharedListItemIds,
            BatchError[] partialErrors)
        {
            if (sharedListItemIds == null)
            {
                return;
            }

            for (var index = 0; index < sharedListItems.Length; index++)
            {
                // Determine if the SharedListItem is a NegativeKeyword.
                if (sharedListItems[index] is NegativeKeyword)
                {
                    // Determine if the corresponding index has a valid identifier
                    if (sharedListItemIds[index] > 0)
                    {
                        OutputStatusMessage(string.Format("NegativeKeyword[{0}] ({1}) successfully added to NegativeKeywordList ({2}) and assigned Negative Keyword Id {3}.",
                                  index,
                                  ((NegativeKeyword)(sharedListItems[index])).Text,
                                  sharedListId,
                                  sharedListItemIds[index]));
                    }
                }
                else
                {
                    OutputStatusMessage("SharedListItem is not a NegativeKeyword.");
                }
            }

            OutputPartialErrors(partialErrors);
        }

        /// <summary>
        /// Output the shared entity identifiers, for example negative keyword list identifiers.
        /// </summary>
        /// <param name="sharedEntityType"></param>
        /// <returns></returns>
        protected void OutputSharedEntityIdentifiersAsync(IList<SharedEntity> sharedEntities)
        {
            if (sharedEntities != null)
            {
                for (int index = 0; index < sharedEntities.Count; index++)
                {
                    if (sharedEntities[index].Id != null)
                    {
                        OutputStatusMessage(string.Format("SharedEntity[{0}] ({1}) has SharedEntity Id {2}.\n",
                            index,
                            sharedEntities[index].Name,
                            sharedEntities[index].Id));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs a list of SharedEntityAssociation objects.
        /// </summary>
        /// <param name="associations"></param>
        protected void OutputSharedEntityAssociations(IList<SharedEntityAssociation> associations)
        {
            if (associations == null || associations.Count == 0)
            {
                return;
            }

            OutputStatusMessage("SharedEntityAssociation item:\n");
            foreach (SharedEntityAssociation sharedEntityAssociation in associations)
            {
                OutputStatusMessage(string.Format("\tEntityId: {0}", sharedEntityAssociation.EntityId));
                OutputStatusMessage(string.Format("\tEntityType: {0}", sharedEntityAssociation.EntityType));
                OutputStatusMessage(string.Format("\tSharedEntityId: {0}", sharedEntityAssociation.SharedEntityId));
                OutputStatusMessage(string.Format("\tSharedEntityType: {0}\n", sharedEntityAssociation.SharedEntityType));
            }
        }

        protected void OutputIds(IList<long?> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                OutputStatusMessage("No Ids\n");
                return;
            }
            
            foreach (long? id in ids)
            {
                OutputStatusMessage("" + id);
            }
        }
        
        protected List<long> GetNonNullableIds(IList<long?> nullableIds)
        {
            List<long> ids = new List<long>();
            foreach (var nullableId in nullableIds)
            {
                ids.Add((long)nullableId);
            }
            return ids;
        }

        protected void OutputPartialErrors(IList<BatchError> partialErrors)
        {
            if (partialErrors == null || partialErrors.Count == 0)
            {
                OutputStatusMessage("No partial errors\n");
                return;
            }
            
            foreach (BatchError error in partialErrors)
            {
                OutputBatchError(error);
            }
        }
        
        /// <summary>
        /// Outputs a list of BatchErrorCollection objects.
        /// </summary>
        /// <param name="batchErrorCollections"></param>
        protected void OutputBatchErrorCollections(IList<BatchErrorCollection> batchErrorCollections)
        {
            if (batchErrorCollections == null || batchErrorCollections.Count == 0)
            {
                OutputStatusMessage("No batch error collections\n");
                return;
            }

            foreach (BatchErrorCollection collection in batchErrorCollections)
            {
                // The top level list index corresponds to the campaign or ad group index identifier.
                if (collection != null)
                {
                    OutputStatusMessage("BatchErrorCollection:\n");
                    if (collection.Code != null)
                    {
                        OutputStatusMessage(string.Format("\tIndex: {0}", collection.Index));
                        OutputStatusMessage(string.Format("\tCode: {0}", collection.Code));
                        OutputStatusMessage(string.Format("\tErrorCode: {0}", collection.ErrorCode));
                        OutputStatusMessage(string.Format("\tMessage: {0}\n", collection.Message));
                    }

                    // The nested list of batch errors can include any errors specific to the item 
                    // that you attempted to add or remove from the campaign or ad group.
                    if(collection.BatchErrors != null)
                    {
                        foreach (BatchError error in collection.BatchErrors)
                        {
                            OutputBatchError(error);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs a BatchError object.
        /// </summary>
        /// <param name="batchError"></param>
        protected void OutputBatchError(BatchError batchError)
        {
            if (batchError == null)
            {
                return;
            }

            OutputStatusMessage("BatchError:\n");
            OutputStatusMessage(string.Format("\tIndex: {0}", batchError.Index));
            OutputStatusMessage(string.Format("\tCode: {0}", batchError.Code));
            OutputStatusMessage(string.Format("\tErrorCode: {0}", batchError.ErrorCode));
            OutputStatusMessage(string.Format("\tMessage: {0}\n", batchError.Message));

            // In the case of an EditorialError, more details are available
            if (batchError.Type == "EditorialError" && batchError.ErrorCode == "CampaignServiceEditorialValidationError")
            {
                OutputStatusMessage(string.Format("\tDisapprovedText: {0}", ((EditorialError)(batchError)).DisapprovedText));
                OutputStatusMessage(string.Format("\tLocation: {0}", ((EditorialError)(batchError)).Location));
                OutputStatusMessage(string.Format("\tPublisherCountry: {0}", ((EditorialError)(batchError)).PublisherCountry));
                OutputStatusMessage(string.Format("\tReasonCode: {0}\n", ((EditorialError)(batchError)).ReasonCode));
            }
        }
        
        /// <summary>
        /// Outputs the Keyword.
        /// </summary>
        protected void OutputKeyword(Keyword keyword)
        {
            if (keyword != null)
            {
                OutputStatusMessage(string.Format("Bid.Amount: {0}",
                    keyword.Bid != null ? keyword.Bid.Amount : 0)
                );
                OutputBiddingScheme(keyword.BiddingScheme);
                OutputStatusMessage(string.Format("DestinationUrl: {0}", keyword.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", keyword.EditorialStatus));
                OutputStatusMessage("FinalMobileUrls: ");
                if (keyword.FinalMobileUrls != null)
                {
                    foreach (var finalMobileUrl in keyword.FinalMobileUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                    }
                }

                OutputStatusMessage("FinalUrls: ");
                if (keyword.FinalUrls != null)
                {
                    foreach (var finalUrl in keyword.FinalUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalUrl));
                    }
                }
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (keyword.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in keyword.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", keyword.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", keyword.MatchType));
                OutputStatusMessage(string.Format("Param1: {0}", keyword.Param1));
                OutputStatusMessage(string.Format("Param2: {0}", keyword.Param2));
                OutputStatusMessage(string.Format("Param3: {0}", keyword.Param3));
                OutputStatusMessage(string.Format("Status: {0}", keyword.Status));
                OutputStatusMessage(string.Format("Text: {0}", keyword.Text));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", keyword.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (keyword.UrlCustomParameters != null && keyword.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in keyword.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the BiddingScheme.
        /// </summary>
        protected void OutputBiddingScheme(BiddingScheme biddingScheme)
        {
            if (biddingScheme != null)
            {
                if (biddingScheme == null)
                    return;
                var enhancedCpcBiddingScheme = biddingScheme as EnhancedCpcBiddingScheme;
                if (enhancedCpcBiddingScheme != null)
                    OutputStatusMessage("BiddingScheme Type: EnhancedCpc");
                var inheritFromParentBiddingScheme = biddingScheme as InheritFromParentBiddingScheme;
                if (inheritFromParentBiddingScheme != null)
                    OutputStatusMessage("BiddingScheme Type: InheritFromParent");
                var manualCpcBiddingScheme = biddingScheme as ManualCpcBiddingScheme;
                if (manualCpcBiddingScheme != null)
                    OutputStatusMessage("BiddingScheme Type: ManualCpc");
                var maxClicksBiddingScheme = biddingScheme as MaxClicksBiddingScheme;
                if (maxClicksBiddingScheme != null)
                    OutputStatusMessage("BiddingScheme Type: MaxClicks");
                var maxConversionsBiddingScheme = biddingScheme as MaxConversionsBiddingScheme;
                if (maxConversionsBiddingScheme != null)
                    OutputStatusMessage("BiddingScheme Type: MaxConversions");
                var targetCpaBiddingScheme = biddingScheme as TargetCpaBiddingScheme;
                if (targetCpaBiddingScheme != null)
                    OutputStatusMessage("BiddingScheme Type: TargetCpa");
            }
        }

        /// <summary>
        /// Outputs the Budget.
        /// </summary>
        protected void OutputBudget(Budget budget)
        {
            if (budget != null)
            {
                OutputStatusMessage(string.Format("Amount: {0}", budget.Amount));
                OutputStatusMessage(string.Format("AssociationCount: {0}", budget.AssociationCount));
                OutputStatusMessage(string.Format("BudgetType: {0}", budget.BudgetType));
                OutputStatusMessage(string.Format("Id: {0}", budget.Id));
                OutputStatusMessage(string.Format("Name: {0}\n", budget.Name));
            }
        }
        
        /// <summary>
        /// Outputs properties of the Ad base class.
        /// </summary>
        protected void OutputAd(Ad ad)
        {
            if (ad != null)
            {
                OutputStatusMessage(string.Format("DevicePreference: {0}", ad.DevicePreference));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", ad.EditorialStatus));
                OutputStatusMessage("FinalMobileUrls: ");
                if (ad.FinalMobileUrls != null)
                {
                    foreach (var finalMobileUrl in ad.FinalMobileUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                    }
                }

                OutputStatusMessage("FinalUrls: ");
                if (ad.FinalUrls != null)
                {
                    foreach (var finalUrl in ad.FinalUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalUrl));
                    }
                }
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (ad.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in ad.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", ad.Id));
                OutputStatusMessage(string.Format("Status: {0}", ad.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", ad.TrackingUrlTemplate));
                OutputStatusMessage(string.Format("Type: {0}", ad.Type));
                OutputStatusMessage("UrlCustomParameters: ");
                if (ad.UrlCustomParameters != null && ad.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in ad.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the DynamicSearchAd.
        /// </summary>
        protected void OutputDynamicSearchAd(DynamicSearchAd ad)
        {
            if (ad != null)
            {
                // Output inherited properties of the Ad base class.
                OutputAd(ad);

                // Output properties that are specific to the DynamicSearchAd
                OutputStatusMessage(string.Format("Path1: {0}", ad.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", ad.Path2));
                OutputStatusMessage(string.Format("Text: {0}", ad.Text));
            }
        }


        /// <summary>
        /// Outputs the ExpandedTextAd.
        /// </summary>
        protected void OutputExpandedTextAd(ExpandedTextAd ad)
        {
            if (ad != null)
            {
                // Output inherited properties of the Ad base class.
                OutputAd(ad);

                // Output properties that are specific to the ExpandedTextAd
                OutputStatusMessage(string.Format("DisplayUrl: {0}", ad.DisplayUrl));
                OutputStatusMessage(string.Format("Path1: {0}", ad.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", ad.Path2));
                OutputStatusMessage(string.Format("Text: {0}", ad.Text));
                OutputStatusMessage(string.Format("TitlePart1: {0}", ad.TitlePart1));
                OutputStatusMessage(string.Format("TitlePart2: {0}", ad.TitlePart2));
            }
        }

        /// <summary>
        /// Outputs the TextAd.
        /// </summary>
        protected void OutputTextAd(TextAd ad)
        {
            if (ad != null)
            {
                // Output inherited properties of the Ad base class.
                OutputAd(ad);

                // Output properties that are specific to the TextAd
                OutputStatusMessage(string.Format("DestinationUrl: {0}", ad.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayUrl: {0}", ad.DisplayUrl));
                OutputStatusMessage(string.Format("Text: {0}", ad.Text));
                OutputStatusMessage(string.Format("Title: {0}", ad.Title));
            }
        }

        /// <summary>
        /// Outputs the ProductAd.
        /// </summary>
        protected void OutputProductAd(ProductAd ad)
        {
            if (ad != null)
            {
                // Output inherited properties of the Ad base class.
                OutputAd(ad);

                // Output properties that are specific to the ProductAd
                OutputStatusMessage(string.Format("PromotionalText: {0}", ad.PromotionalText));
            }
        }
        
        
        /// <summary>
        /// Outputs the NegativeKeyword.
        /// </summary>
        protected void OutputNegativeKeyword(NegativeKeyword negativeKeyword)
        {
            if (negativeKeyword != null)
            {
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (negativeKeyword.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in negativeKeyword.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", negativeKeyword.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", negativeKeyword.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", negativeKeyword.Text));
                OutputStatusMessage(string.Format("Type: {0}", negativeKeyword.Type));
            }
        }

        /// <summary>
        /// Outputs the NegativeKeywordList.
        /// </summary>
        protected void OutputNegativeKeywordList(NegativeKeywordList negativeKeywordList)
        {
            if (negativeKeywordList != null)
            {
                OutputStatusMessage(string.Format("AssociationCount: {0}", negativeKeywordList.AssociationCount));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (negativeKeywordList.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in negativeKeywordList.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", negativeKeywordList.Id));
                OutputStatusMessage(string.Format("ItemCount: {0}", negativeKeywordList.ItemCount));
                OutputStatusMessage(string.Format("Name: {0}", negativeKeywordList.Name));
            }
        }

        /// <summary>
        /// Outputs the audience.
        /// </summary>
        /// <param name="audience"></param>
        protected void OutputAudience(Audience audience)
        {
            if (audience != null)
            {
                OutputStatusMessage(string.Format("Description: {0}", audience.Description));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (audience.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in audience.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", pair.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", audience.Id));
                OutputStatusMessage(string.Format("MembershipDuration: {0}", audience.MembershipDuration));
                OutputStatusMessage(string.Format("Name: {0}", audience.Name));
                OutputStatusMessage(string.Format("ParentId: {0}", audience.ParentId));
                OutputStatusMessage(string.Format("Scope: {0}", audience.Scope));
            }
        }

        /// <summary>
        /// Outputs the RemarketingList.
        /// </summary>
        protected void OutputRemarketingList(RemarketingList remarketingList)
        {
            if (remarketingList != null)
            {
                // Output inherited properties of the Audience base class.
                OutputAudience(remarketingList);

                // Output properties that are specific to the RemarketingList
                OutputStatusMessage(string.Format("TagId: {0}", remarketingList.TagId));
                OutputRemarketingRule(remarketingList.Rule);
                OutputStatusMessage("\n");
            }
        }

        /// <summary>
        /// Outputs the RemarketingRule.
        /// </summary>
        protected void OutputRemarketingRule(RemarketingRule remarketingRule)
        {
            if (remarketingRule != null)
            {
                OutputStatusMessage(string.Format("Type: {0}", remarketingRule.Type));

                var customEventsRule = remarketingRule as CustomEventsRule;

                if (customEventsRule != null)
                {
                    OutputStatusMessage(string.Format("Action: {0}", customEventsRule.Action));
                    OutputStatusMessage(string.Format("ActionOperator: {0}", customEventsRule.ActionOperator));
                    OutputStatusMessage(string.Format("Category: {0}", customEventsRule.Category));
                    OutputStatusMessage(string.Format("CategoryOperator: {0}", customEventsRule.CategoryOperator));
                    OutputStatusMessage(string.Format("Label: {0}", customEventsRule.Label));
                    OutputStatusMessage(string.Format("LabelOperator: {0}", customEventsRule.LabelOperator));
                    OutputStatusMessage(string.Format("Value: {0}", customEventsRule.Value));
                    OutputStatusMessage(string.Format("ValueOperator: {0}", customEventsRule.ValueOperator));
                }
                else
                {
                    var pageVisitorsRule = remarketingRule as PageVisitorsRule;
                    if (pageVisitorsRule != null)
                    {
                        if (pageVisitorsRule.RuleItemGroups != null)
                        {
                            OutputStatusMessage("RuleItemGroups: ");
                            OutputRuleItemGroups(pageVisitorsRule.RuleItemGroups);
                        }
                    }
                    else
                    {
                        var pageVisitorsWhoDidNotVisitAnotherPageRule = remarketingRule as PageVisitorsWhoDidNotVisitAnotherPageRule;
                        if (pageVisitorsWhoDidNotVisitAnotherPageRule != null)
                        {
                            if (pageVisitorsWhoDidNotVisitAnotherPageRule.ExcludeRuleItemGroups != null)
                            {
                                OutputStatusMessage("ExcludeRuleItemGroups: ");
                                OutputRuleItemGroups(pageVisitorsWhoDidNotVisitAnotherPageRule.ExcludeRuleItemGroups);
                            }
                            if (pageVisitorsWhoDidNotVisitAnotherPageRule.IncludeRuleItemGroups != null)
                            {
                                OutputStatusMessage("IncludeRuleItemGroups: ");
                                OutputRuleItemGroups(pageVisitorsWhoDidNotVisitAnotherPageRule.IncludeRuleItemGroups);
                            }
                        }
                        else
                        {
                            var pageVisitorsWhoVisitedAnotherPageRule = remarketingRule as PageVisitorsWhoVisitedAnotherPageRule;
                            if (pageVisitorsWhoVisitedAnotherPageRule != null)
                            {
                                if (pageVisitorsWhoVisitedAnotherPageRule.AnotherRuleItemGroups != null)
                                {
                                    OutputStatusMessage("AnotherRuleItemGroups: ");
                                    OutputRuleItemGroups(pageVisitorsWhoVisitedAnotherPageRule.AnotherRuleItemGroups);
                                }
                                if (pageVisitorsWhoVisitedAnotherPageRule.RuleItemGroups != null)
                                {
                                    OutputStatusMessage("RuleItemGroups: ");
                                    OutputRuleItemGroups(pageVisitorsWhoVisitedAnotherPageRule.RuleItemGroups);
                                }
                            }
                            else
                            {
                                OutputStatusMessage("Unknown remarketing rule type.");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of RuleItemGroup.
        /// </summary>
        protected void OutputRuleItemGroups(IList<RuleItemGroup> ruleItemGroups)
        {
            if (ruleItemGroups != null)
            {
                foreach(var ruleItemGroup in ruleItemGroups)
                {
                    if (ruleItemGroup.Items != null)
                    {
                        foreach (var ruleItem in ruleItemGroup.Items)
                        {
                            var stringRuleItem = ruleItem as StringRuleItem;

                            if (stringRuleItem != null)
                            {
                                OutputStatusMessage("\tRuleItem:");
                                OutputStatusMessage(string.Format("\tOperand: {0}", stringRuleItem.Operand));
                                OutputStatusMessage(string.Format("\tOperator: {0}", stringRuleItem.Operator));
                                OutputStatusMessage(string.Format("\tValue: {0}\n", stringRuleItem.Value));
                            }
                            else
                            {
                                OutputStatusMessage("Unknown remarketing rule item type.");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the AudienceCriterion.
        /// </summary>
        protected void OutputAudienceCriterion(AudienceCriterion audienceCriterion)
        {
            if (audienceCriterion != null)
            {
                OutputStatusMessage(string.Format("AudienceId: {0}", audienceCriterion.AudienceId));
                OutputStatusMessage(string.Format("AudienceType: {0}", audienceCriterion.AudienceType));
            }
        }

        /// <summary>
        /// Outputs the UetTag.
        /// </summary>
        protected void OutputUetTag(UetTag uetTag)
        {
            if (uetTag != null)
            {
                OutputStatusMessage(string.Format("Description: {0}", uetTag.Description));
                OutputStatusMessage(string.Format("Id: {0}", uetTag.Id));
                OutputStatusMessage(string.Format("Name: {0}", uetTag.Name));
                OutputStatusMessage(string.Format("TrackingNoScript: {0}", uetTag.TrackingNoScript));
                OutputStatusMessage(string.Format("TrackingScript: {0}", uetTag.TrackingScript));
                OutputStatusMessage(string.Format("TrackingStatus: {0}\n", uetTag.TrackingStatus));
            }
        }

        /// <summary>
        /// Outputs the ConversionGoal list.
        /// </summary>
        protected void OutputConversionGoals(IList<ConversionGoal> conversionGoals)
        {
            if (conversionGoals != null)
            {
                foreach (var conversionGoal in conversionGoals)
                {
                    OutputConversionGoal(conversionGoal);
                    OutputStatusMessage("\n");
                }
            }
        }

        /// <summary>
        /// Outputs the ConversionGoal.
        /// </summary>
        protected void OutputConversionGoal(ConversionGoal conversionGoal)
        {
            if (conversionGoal != null)
            {
                OutputStatusMessage(string.Format("ConversionWindowInMinutes: {0}", conversionGoal.ConversionWindowInMinutes));
                OutputStatusMessage(string.Format("CountType: {0}", conversionGoal.CountType));
                OutputStatusMessage(string.Format("Id: {0}", conversionGoal.Id));
                OutputStatusMessage(string.Format("Name: {0}", conversionGoal.Name));
                OutputConversionGoalRevenue(conversionGoal.Revenue);
                OutputStatusMessage(string.Format("Scope: {0}", conversionGoal.Scope));
                OutputStatusMessage(string.Format("Status: {0}", conversionGoal.Status));
                OutputStatusMessage(string.Format("TagId: {0}", conversionGoal.TagId));
                OutputStatusMessage(string.Format("TrackingStatus: {0}", conversionGoal.TrackingStatus));
                OutputStatusMessage(string.Format("Type: {0}", conversionGoal.Type));

                var appInstallGoal = conversionGoal as AppInstallGoal;
                if (appInstallGoal != null)
                {
                    OutputStatusMessage(string.Format("AppPlatform: {0}", appInstallGoal.AppPlatform));
                    OutputStatusMessage(string.Format("AppStoreId: {0}\n", appInstallGoal.AppStoreId));
                }
                else
                {
                    var durationGoal = conversionGoal as DurationGoal;
                    if (durationGoal != null)
                    {
                        OutputStatusMessage(string.Format("MinimumDurationInSeconds: {0}\n", durationGoal.MinimumDurationInSeconds));
                    }
                    else
                    {
                        var eventGoal = conversionGoal as EventGoal;
                        if (eventGoal != null)
                        {
                            OutputStatusMessage(string.Format("ActionExpression: {0}", eventGoal.ActionExpression));
                            OutputStatusMessage(string.Format("ActionOperator: {0}", eventGoal.ActionOperator));
                            OutputStatusMessage(string.Format("CategoryExpression: {0}", eventGoal.CategoryExpression));
                            OutputStatusMessage(string.Format("CategoryOperator: {0}", eventGoal.CategoryOperator));
                            OutputStatusMessage(string.Format("LabelExpression: {0}", eventGoal.LabelExpression));
                            OutputStatusMessage(string.Format("LabelOperator: {0}", eventGoal.LabelOperator));
                            OutputStatusMessage(string.Format("Value: {0}", eventGoal.Value));
                            OutputStatusMessage(string.Format("ValueOperator: {0}\n", eventGoal.ValueOperator));
                        }
                        else
                        {
                            var pagesViewedPerVisitGoal = conversionGoal as PagesViewedPerVisitGoal;
                            if (pagesViewedPerVisitGoal != null)
                            {
                                OutputStatusMessage(string.Format("MinimumPagesViewed: {0}\n", pagesViewedPerVisitGoal.MinimumPagesViewed));
                            }
                            else
                            {
                                var urlGoal = conversionGoal as UrlGoal;
                                if (urlGoal != null)
                                {
                                    OutputStatusMessage(string.Format("UrlExpression: {0}", urlGoal.UrlExpression));
                                    OutputStatusMessage(string.Format("UrlOperator: {0}\n", urlGoal.UrlOperator));
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the ConversionGoalRevenue.
        /// </summary>
        protected void OutputConversionGoalRevenue(ConversionGoalRevenue conversionGoalRevenue)
        {
            if (conversionGoalRevenue != null)
            {
                OutputStatusMessage(string.Format("CurrencyCode: {0}", conversionGoalRevenue.CurrencyCode));
                OutputStatusMessage(string.Format("Type: {0}", conversionGoalRevenue.Type));
                OutputStatusMessage(string.Format("Value: {0}", conversionGoalRevenue.Value));
            }
        }

        /// <summary>
        /// Outputs the negative websites.
        /// </summary>
        protected void OutputNegativeSites(IList<string> negativeSites)
        {
            foreach (var negativeSite in negativeSites)
            {
                OutputStatusMessage(string.Format("NegativeSite: {0}", negativeSite));
            }
        }

        /// <summary>
        /// Outputs the AppAdExtension.
        /// </summary>
        protected void OutputAppAdExtension(AppAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the AppAdExtension
                OutputStatusMessage(string.Format("AppPlatform: {0}", extension.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", extension.AppStoreId));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", extension.DestinationUrl));
                OutputStatusMessage(string.Format("DevicePreference: {0}", extension.DevicePreference));
                OutputStatusMessage(string.Format("DisplayText: {0}", extension.DisplayText));
                OutputStatusMessage("FinalMobileUrls: ");
                if (extension.FinalMobileUrls != null)
                {
                    foreach (var finalMobileUrl in extension.FinalMobileUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                    }
                }

                OutputStatusMessage("FinalUrls: ");
                if (extension.FinalUrls != null)
                {
                    foreach (var finalUrl in extension.FinalUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalUrl));
                    }
                }
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", extension.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (extension.UrlCustomParameters != null && extension.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in extension.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the CallAdExtension.
        /// </summary>
        protected void OutputCallAdExtension(CallAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the CallAdExtension
                OutputStatusMessage(string.Format("CountryCode: {0}", extension.CountryCode));
                OutputStatusMessage(string.Format("DevicePreference: {0}", extension.DevicePreference));
                OutputStatusMessage(string.Format("IsCallOnly: {0}", extension.IsCallOnly));
                OutputStatusMessage(string.Format("IsCallTrackingEnabled: {0}", extension.IsCallTrackingEnabled));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", extension.PhoneNumber));
                OutputStatusMessage(string.Format("RequireTollFreeTrackingNumber: {0}", extension.RequireTollFreeTrackingNumber));
            }
        }

        /// <summary>
        /// Outputs the ImageAdExtension.
        /// </summary>
        protected void OutputImageAdExtension(ImageAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the ImageAdExtension
                OutputStatusMessage(string.Format("AlternativeText: {0}", extension.AlternativeText));
                OutputStatusMessage(string.Format("Description: {0}", extension.Description));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", extension.DestinationUrl));
                OutputStatusMessage("FinalMobileUrls: ");
                if (extension.FinalMobileUrls != null)
                {
                    foreach (var finalMobileUrl in extension.FinalMobileUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                    }
                }

                OutputStatusMessage("FinalUrls: ");
                if (extension.FinalUrls != null)
                {
                    foreach (var finalUrl in extension.FinalUrls)
                    {
                        OutputStatusMessage(string.Format("\t{0}", finalUrl));
                    }
                }
                OutputStatusMessage("ImageMediaIds: ");
                if (extension.ImageMediaIds != null)
                {
                    foreach (var id in extension.ImageMediaIds)
                    {
                        OutputStatusMessage(string.Format("\tId: {0}", id));
                    }
                }
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", extension.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (extension.UrlCustomParameters != null && extension.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in extension.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }
        
        /// <summary>
        /// Outputs the LocationAdExtension.
        /// </summary>
        protected void OutputLocationAdExtension(LocationAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the LocationAdExtension
                if (extension.Address != null)
                {
                    OutputStatusMessage(string.Format("CityName: {0}", extension.Address.CityName));
                    OutputStatusMessage(string.Format("CountryCode: {0}", extension.Address.CountryCode));
                    OutputStatusMessage(string.Format("PostalCode: {0}", extension.Address.PostalCode));
                    OutputStatusMessage(string.Format("ProvinceCode: {0}", extension.Address.ProvinceCode));
                    OutputStatusMessage(string.Format("ProvinceName: {0}", extension.Address.ProvinceName));
                    OutputStatusMessage(string.Format("StreetAddress: {0}", extension.Address.StreetAddress));
                    OutputStatusMessage(string.Format("StreetAddress2: {0}", extension.Address.StreetAddress2));
                }

                OutputStatusMessage(string.Format("CompanyName: {0}", extension.CompanyName));
                OutputStatusMessage(string.Format("GeoCodeStatus: {0}", extension.GeoCodeStatus));
                if (extension.GeoPoint != null)
                {
                    OutputStatusMessage("GeoPoint: ");
                    OutputStatusMessage(string.Format("LatitudeInMicroDegrees: {0}", extension.GeoPoint.LatitudeInMicroDegrees));
                    OutputStatusMessage(string.Format("LongitudeInMicroDegrees: {0}", extension.GeoPoint.LongitudeInMicroDegrees));
                }
                OutputStatusMessage(string.Format("IconMediaId: {0}", extension.IconMediaId));
                OutputStatusMessage(string.Format("ImageMediaId: {0}", extension.ImageMediaId));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", extension.PhoneNumber));
            }
        }

        /// <summary>
        /// Outputs the CalloutAdExtension.
        /// </summary>
        protected void OutputCalloutAdExtension(CalloutAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the CalloutAdExtension
                OutputStatusMessage(string.Format("Callout Text: {0}", extension.Text));
            }
        }

        /// <summary>
        /// Outputs the ReviewAdExtension.
        /// </summary>
        protected void OutputReviewAdExtension(ReviewAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the ReviewAdExtension
                OutputStatusMessage(string.Format("IsExact: {0}", extension.IsExact));
                OutputStatusMessage(string.Format("Source: {0}", extension.Source));
                OutputStatusMessage(string.Format("Text: {0}", extension.Text));
                OutputStatusMessage(string.Format("Url: {0}", extension.Url));
            }
        }

        /// <summary>
        /// Outputs the StructuredSnippetAdExtension.
        /// </summary>
        protected void OutputStructuredSnippetAdExtension(StructuredSnippetAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the StructuredSnippetAdExtension
                OutputStatusMessage(string.Format("Header: {0}", extension.Header));
                OutputStatusMessage(string.Format("Values: {0}", string.Join("; ", extension.Values)));
            }
        }

        /// <summary>
        /// Outputs properties of the AdExtension base class.
        /// </summary>
        protected void OutputAdExtension(AdExtension extension)
        {
            OutputStatusMessage(string.Format("Id: {0}", extension.Id));
            OutputStatusMessage(string.Format("Type: {0}", extension.Type));
            OutputStatusMessage("ForwardCompatibilityMap: ");
            if (extension.ForwardCompatibilityMap != null)
            {
                foreach (var pair in extension.ForwardCompatibilityMap)
                {
                    OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                    OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                }
            }
            OutputStatusMessage("Scheduling: ");
            if (extension.Scheduling != null)
            {
                OutputSchedule(extension.Scheduling);
            }
            OutputStatusMessage(string.Format("Status: {0}", extension.Status));
            OutputStatusMessage(string.Format("Version: {0}", extension.Version));
        }

        /// <summary>
        /// Outputs the Schedule.
        /// </summary>
        protected void OutputSchedule(Schedule schedule)
        {
            if (schedule != null)
            {
                if(schedule.DayTimeRanges != null)
                {
                    foreach (var dayTime in schedule.DayTimeRanges)
                    {
                        OutputStatusMessage(string.Format("Day: {0}", dayTime.Day));
                        OutputStatusMessage(string.Format("EndHour: {0}", dayTime.EndHour));
                        OutputStatusMessage(string.Format("EndMinute: {0}", dayTime.EndMinute));
                        OutputStatusMessage(string.Format("StartHour: {0}", dayTime.StartHour));
                        OutputStatusMessage(string.Format("StartMinute: {0}", dayTime.StartMinute));
                    }
                }
                if (schedule.EndDate != null)
                {
                    OutputStatusMessage(string.Format("EndDate: {0}/{1}/{2}",
                    schedule.EndDate.Month,
                    schedule.EndDate.Day,
                    schedule.EndDate.Year));
                }
                if (schedule.StartDate != null)
                {
                    OutputStatusMessage(string.Format("StartDate: {0}/{1}/{2}",
                    schedule.StartDate.Month,
                    schedule.StartDate.Day,
                    schedule.StartDate.Year));
                }
                var useSearcherTimeZone = 
                    (schedule.UseSearcherTimeZone != null && (bool)schedule.UseSearcherTimeZone) ? "True" : "False";
                OutputStatusMessage(string.Format("UseSearcherTimeZone: {0}", useSearcherTimeZone));
            }
        }
        
        /// <summary>
        /// Outputs the SiteLinksAdExtension.
        /// </summary>
        protected void OutputSiteLinksAdExtension(SiteLinksAdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the SiteLinksAdExtension
                OutputSiteLinks(extension.SiteLinks);
            }
        }

        /// <summary>
        /// Outputs the list of SiteLink.
        /// </summary>
        protected void OutputSiteLinks(IList<SiteLink> siteLinks)
        {
            if (siteLinks != null)
            {
                OutputStatusMessage("SiteLinks: ");

                foreach (var siteLink in siteLinks)
                {
                    OutputStatusMessage(string.Format("Description1: {0}", siteLink.Description1));
                    OutputStatusMessage(string.Format("Description2: {0}", siteLink.Description2));
                    OutputStatusMessage(string.Format("DestinationUrl: {0}", siteLink.DestinationUrl));
                    OutputStatusMessage(string.Format("DevicePreference: {0}", siteLink.DevicePreference));
                    OutputStatusMessage(string.Format("DisplayText: {0}", siteLink.DisplayText));
                    OutputStatusMessage("FinalMobileUrls: ");
                    if (siteLink.FinalMobileUrls != null)
                    {
                        foreach (var finalMobileUrl in siteLink.FinalMobileUrls)
                        {
                            OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                        }
                    }
                    OutputStatusMessage("Scheduling: ");
                    if (siteLink.Scheduling != null)
                    {
                        OutputSchedule(siteLink.Scheduling);
                    }
                    OutputStatusMessage("FinalUrls: ");
                    if (siteLink.FinalUrls != null)
                    {
                        foreach (var finalUrl in siteLink.FinalUrls)
                        {
                            OutputStatusMessage(string.Format("\t{0}", finalUrl));
                        }
                    }

                    OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", siteLink.TrackingUrlTemplate));
                    OutputStatusMessage("UrlCustomParameters: ");
                    if (siteLink.UrlCustomParameters != null && siteLink.UrlCustomParameters.Parameters != null)
                    {
                        foreach (var customParameter in siteLink.UrlCustomParameters.Parameters)
                        {
                            OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                            OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the Sitelink2AdExtension.
        /// </summary>
        protected void OutputSitelink2AdExtension(Sitelink2AdExtension extension)
        {
            if (extension != null)
            {
                // Output inherited properties of the AdExtension base class.
                OutputAdExtension(extension);

                // Output properties that are specific to the Sitelink2AdExtension
                OutputStatusMessage(string.Format("Description1: {0}", extension.Description1));
                OutputStatusMessage(string.Format("Description2: {0}", extension.Description2));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", extension.DestinationUrl));
                OutputStatusMessage(string.Format("DevicePreference: {0}", extension.DevicePreference));
                OutputStatusMessage(string.Format("DisplayText: {0}", extension.DisplayText));
                OutputStatusMessage("FinalMobileUrls: ");
                if (extension.FinalMobileUrls != null)
                {
                    foreach (var url in extension.FinalMobileUrls)
                    {
                        // List of 10 strings will be returned, but in this example we won't output empty lines.
                        if(url != null && url.Length > 0)
                        {
                            OutputStatusMessage(string.Format("\t{0}", url));
                        }
                    }
                }

                OutputStatusMessage("FinalUrls: ");
                if (extension.FinalUrls != null)
                {
                    foreach (var url in extension.FinalUrls)
                    {
                        // List of 10 strings will be returned, but in this example we won't output empty lines.
                        if (url != null && url.Length > 0)
                        {
                            OutputStatusMessage(string.Format("\t{0}", url));
                        }
                    }
                }
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", extension.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters: ");
                if (extension.UrlCustomParameters != null && extension.UrlCustomParameters.Parameters != null)
                {
                    foreach (var customParameter in extension.UrlCustomParameters.Parameters)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the AccountMigrationStatusesInfo.
        /// </summary>
        protected void OutputAccountMigrationStatusesInfo(AccountMigrationStatusesInfo accountMigrationStatusesInfo)
        {
            if (accountMigrationStatusesInfo != null)
            {
                OutputStatusMessage(string.Format("AccountId: {0}", accountMigrationStatusesInfo.AccountId));
                foreach (var migrationStatusInfo in accountMigrationStatusesInfo.MigrationStatusInfo)
                {
                    OutputMigrationStatusInfo(migrationStatusInfo);
                }
            }
        }

        /// <summary>
        /// Outputs the MigrationStatusInfo.
        /// </summary>
        protected void OutputMigrationStatusInfo(MigrationStatusInfo migrationStatusInfo)
        {
            if (migrationStatusInfo != null)
            {
                OutputStatusMessage(string.Format("MigrationType: {0}", migrationStatusInfo.MigrationType));
                OutputStatusMessage(string.Format("StartTimeInUtc: {0}", migrationStatusInfo.StartTimeInUtc));
                OutputStatusMessage(string.Format("Status: {0}\n", migrationStatusInfo.Status));
            }
        }

        /// <summary>
        /// Outputs the Label.
        /// </summary>
        protected void OutputLabel(Label label)
        {
            if (label != null)
            {
                OutputStatusMessage(string.Format("ColorCode: {0}", label.ColorCode));
                OutputStatusMessage(string.Format("Description: {0}", label.Description));
                OutputStatusMessage(string.Format("Id: {0}", label.Id));
                OutputStatusMessage(string.Format("Name: {0}", label.Name));
            }
        }

        /// <summary>
        /// Outputs the Label list.
        /// </summary>
        protected void OutputLabels(IList<Label> labels)
        {
            if (labels != null)
            {
                foreach(var label in labels)
                {
                    OutputLabel(label);
                    OutputStatusMessage("\n");
                }
            }
        }

        /// <summary>
        /// Outputs the LabelAssociation.
        /// </summary>
        protected void OutputLabelAssociation(LabelAssociation labelAssociation)
        {
            if (labelAssociation != null)
            {
                OutputStatusMessage(string.Format("EntityId: {0}", labelAssociation.EntityId));
                OutputStatusMessage(string.Format("LabelId: {0}", labelAssociation.LabelId));
            }
        }

        /// <summary>
        /// Outputs the LabelAssociation list.
        /// </summary>
        protected void OutputLabelAssociations(IList<LabelAssociation> labelAssociations)
        {
            if (labelAssociations != null)
            {
                foreach (var labelAssociation in labelAssociations)
                {
                    OutputLabelAssociation(labelAssociation);
                    OutputStatusMessage("\n");
                }
            }
        }

        /// <summary>
        /// Outputs the OfflineConversion list.
        /// </summary>
        protected void OutputOfflineConversions(IList<OfflineConversion> offlineConversions)
        {
            if (offlineConversions != null)
            {
                foreach (var offlineConversion in offlineConversions)
                {
                    OutputOfflineConversion(offlineConversion);
                    OutputStatusMessage("\n");
                }
            }
        }

        /// <summary>
        /// Outputs the OfflineConversion.
        /// </summary>
        protected void OutputOfflineConversion(OfflineConversion offlineConversion)
        {
            if (offlineConversion != null)
            {
                OutputStatusMessage(string.Format("ConversionCurrencyCode: {0}", offlineConversion.ConversionCurrencyCode));
                OutputStatusMessage(string.Format("ConversionName: {0}", offlineConversion.ConversionName));
                OutputStatusMessage(string.Format("ConversionTime: {0}", offlineConversion.ConversionTime));
                OutputStatusMessage(string.Format("ConversionValue: {0}", offlineConversion.ConversionValue));
                OutputStatusMessage(string.Format("MicrosoftClickId: {0}", offlineConversion.MicrosoftClickId));
            }
        }

        /// <summary>
        /// Outputs the AccountProperty list.
        /// </summary>
        protected void OutputAccountProperties(IList<AccountProperty> accountProperties)
        {
            if (accountProperties != null)
            {
                foreach (var accountProperty in accountProperties)
                {
                    OutputAccountProperty(accountProperty);
                    OutputStatusMessage("\n");
                }
            }
        }

        /// <summary>
        /// Outputs the AccountProperty.
        /// </summary>
        protected void OutputAccountProperty(AccountProperty accountProperty)
        {
            if (accountProperty != null)
            {
                OutputStatusMessage(string.Format("Name: {0}", accountProperty.Name));
                OutputStatusMessage(string.Format("Value: {0}", accountProperty.Value));
            }
        }

        #endregion CampaignManagement_Output

        #region CampaignManagement_ServiceOperations

        protected async Task<AddAdExtensionsResponse> AddAdExtensionsAsync(long accountId, IList<AdExtension> adExtensions)
        {
            var request = new AddAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdExtensionsAsync(r), request));
        }

        protected async Task<AddAdGroupCriterionsResponse> AddAdGroupCriterionsAsync(
           IList<AdGroupCriterion> adGroupCriterions,
           AdGroupCriterionType criterionType)
        {
            var request = new AddAdGroupCriterionsRequest
            {
                AdGroupCriterions = adGroupCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdGroupCriterionsAsync(r), request));
        }

        protected async Task<AddAdGroupsResponse> AddAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdGroupsAsync(r), request));
        }

        protected async Task<AddAdsResponse> AddAdsAsync(long adGroupId, IList<Ad> ads)
        {
            var request = new AddAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAdsAsync(r), request));
        }

        protected async Task<AddAudiencesResponse> AddAudiencesAsync(IList<Audience> audiences)
        {
            var request = new AddAudiencesRequest
            {
                Audiences = audiences,
            };

            return (await CampaignService.CallAsync((s, r) => s.AddAudiencesAsync(r), request));
        }
        
        protected async Task<AddCampaignsResponse> AddCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await CampaignService.CallAsync((s, r) => s.AddCampaignsAsync(r), request));
        }

        protected async Task<AddBudgetsResponse> AddBudgetsAsync(IList<Budget> budgets)
        {
            var request = new AddBudgetsRequest
            {
                Budgets = budgets
            };

            return (await CampaignService.CallAsync((s, r) => s.AddBudgetsAsync(r), request));
        }

        protected async Task<AddCampaignCriterionsResponse> AddCampaignCriterionsAsync(
            IList<CampaignCriterion> campaignCriterions,
            CampaignCriterionType criterionType)
        {
            var request = new AddCampaignCriterionsRequest
            {
                CampaignCriterions = campaignCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.AddCampaignCriterionsAsync(r), request));
        }

        protected async Task<AddConversionGoalsResponse> AddConversionGoalsAsync(IList<ConversionGoal> conversionGoals)
        {
            var request = new AddConversionGoalsRequest
            {
                ConversionGoals = conversionGoals
            };

            return await CampaignService.CallAsync((s, r) => s.AddConversionGoalsAsync(r), request);
        }

        protected async Task<AddKeywordsResponse> AddKeywordsAsync(long adGroupId, IList<Keyword> keywords)
        {
            var request = new AddKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords
            };

            return (await CampaignService.CallAsync((s, r) => s.AddKeywordsAsync(r), request));
        }
        
        protected async Task<AddLabelsResponse> AddLabelsAsync(IList<Label> labels)
        {
            var request = new AddLabelsRequest
            {
                Labels = labels
            };

            return (await CampaignService.CallAsync((s, r) => s.AddLabelsAsync(r), request));
        }

        protected async Task<AddListItemsToSharedListResponse> AddListItemsToSharedListAsync(
           IList<SharedListItem> listItems,
           SharedList sharedList)
        {
            var request = new AddListItemsToSharedListRequest
            {
                ListItems = listItems,
                SharedList = sharedList
            };

            return (await CampaignService.CallAsync((s, r) => s.AddListItemsToSharedListAsync(r), request));
        }

        protected async Task<AddMediaResponse> AddMediaAsync(IList<Media> media)
        {
            var request = new AddMediaRequest
            {
                Media = media
            };

            return (await CampaignService.CallAsync((s, r) => s.AddMediaAsync(r), request));
        }

        protected async Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(IList<EntityNegativeKeyword> entityNegativeKeywords)
        {
            var request = new AddNegativeKeywordsToEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords
            };

            return (await CampaignService.CallAsync((s, r) => s.AddNegativeKeywordsToEntitiesAsync(r), request));
        }

        protected async Task<AddSharedEntityResponse> AddSharedEntityAsync(
            SharedEntity sharedEntity,
            IList<SharedListItem> listItems)
        {
            var request = new AddSharedEntityRequest
            {
                ListItems = listItems,
                SharedEntity = sharedEntity
            };

            return (await CampaignService.CallAsync((s, r) => s.AddSharedEntityAsync(r), request));
        }

        protected async Task<AddUetTagsResponse> AddUetTagsAsync(IList<UetTag> uetTags)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = uetTags,
            };

            return (await CampaignService.CallAsync((s, r) => s.AddUetTagsAsync(r), request));
        }

        protected async Task<ApplyOfflineConversionsResponse> ApplyOfflineConversionsAsync(IList<OfflineConversion> offlineConversions)
        {
            var request = new ApplyOfflineConversionsRequest
            {
                OfflineConversions = offlineConversions
            };

            return (await CampaignService.CallAsync((s, r) => s.ApplyOfflineConversionsAsync(r), request));
        }

        protected async Task<ApplyProductPartitionActionsResponse> ApplyProductPartitionActionsAsync(
            IList<AdGroupCriterionAction> criterionActions)
        {
            var request = new ApplyProductPartitionActionsRequest
            {
                CriterionActions = criterionActions
            };

            return (await CampaignService.CallAsync((s, r) => s.ApplyProductPartitionActionsAsync(r), request));
        }

        protected async Task<DeleteAdExtensionsResponse> DeleteAdExtensionsAsync(long accountId, IList<long> adExtensionIds)
        {
            var request = new DeleteAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteAdExtensionsAsync(r), request));
        }

        protected async Task DeleteAdExtensionsAssociationsAsync(long accountId, IList<AdExtensionIdToEntityIdAssociation> associations, AssociationType associationType)
        {
            var request = new DeleteAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            await CampaignService.CallAsync((s, r) => s.DeleteAdExtensionsAssociationsAsync(r), request);
        }

        protected async Task<DeleteAdGroupCriterionsResponse> DeleteAdGroupCriterionsAsync(
          long adGroupId,
            IList<long> adGroupCriterionIds,
            AdGroupCriterionType criterionType)
        {
            var request = new DeleteAdGroupCriterionsRequest
            {
                AdGroupCriterionIds = adGroupCriterionIds,
                AdGroupId = adGroupId,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteAdGroupCriterionsAsync(r), request));
        }

        protected async Task<DeleteAudiencesResponse> DeleteAudiencesAsync(IList<long> audienceIds)
        {
            var request = new DeleteAudiencesRequest
            {
                AudienceIds = audienceIds
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteAudiencesAsync(r), request));
        }

        protected async Task<DeleteBudgetsResponse> DeleteBudgetsAsync(IList<long> budgetIds)
        {
            var request = new DeleteBudgetsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteBudgetsAsync(r), request));
        }

        protected async Task<DeleteCampaignsResponse> DeleteCampaignsAsync(long accountId, IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request));
        }
                
        protected async Task<DeleteLabelAssociationsResponse> DeleteLabelAssociationsAsync(
            IList<LabelAssociation> labelAssociations,
            EntityType entityType)
        {
            var request = new DeleteLabelAssociationsRequest
            {
                LabelAssociations = labelAssociations,
                EntityType = entityType,
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteLabelAssociationsAsync(r), request));
        }

        protected async Task<DeleteLabelsResponse> DeleteLabelsAsync(IList<long> labelIds)
        {
            var request = new DeleteLabelsRequest
            {
                LabelIds = labelIds
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteLabelsAsync(r), request));
        }

        protected async Task<DeleteListItemsFromSharedListResponse> DeleteListItemsFromSharedListAsync(
            IList<long> listItemIds,
            SharedList sharedList)
        {
            var request = new DeleteListItemsFromSharedListRequest
            {
                ListItemIds = listItemIds,
                SharedList = sharedList
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteListItemsFromSharedListAsync(r), request));
        }

        protected async Task<DeleteNegativeKeywordsFromEntitiesResponse> DeleteNegativeKeywordsFromEntitiesAsync(IList<EntityNegativeKeyword> entityNegativeKeywords)
        {
            var request = new DeleteNegativeKeywordsFromEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords,
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteNegativeKeywordsFromEntitiesAsync(r), request));
        }

        protected async Task<DeleteSharedEntitiesResponse> DeleteSharedEntitiesAsync(IList<SharedEntity> sharedEntities)
        {
            var request = new DeleteSharedEntitiesRequest
            {
                SharedEntities = sharedEntities
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteSharedEntitiesAsync(r), request));
        }

        protected async Task<DeleteSharedEntityAssociationsResponse> DeleteSharedEntityAssociationsAsync(IList<SharedEntityAssociation> associations)
        {
            var request = new DeleteSharedEntityAssociationsRequest
            {
                Associations = associations
            };

            return (await CampaignService.CallAsync((s, r) => s.DeleteSharedEntityAssociationsAsync(r), request));
        }

        protected async Task<GetAccountMigrationStatusesResponse> GetAccountMigrationStatusesAsync(
            long[] accountIds,
            string migrationType)
        {
            var request = new GetAccountMigrationStatusesRequest
            {
                AccountIds = accountIds,
                MigrationType = migrationType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAccountMigrationStatusesAsync(r), request));
        }

        protected async Task<GetAccountPropertiesResponse> GetAccountPropertiesAsync(IList<AccountPropertyName> accountPropertyNames)
        {
            var request = new GetAccountPropertiesRequest
            {
                AccountPropertyNames = accountPropertyNames,
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAccountPropertiesAsync(r), request));
        }

        protected async Task<GetAdGroupCriterionsByIdsResponse> GetAdGroupCriterionsByIdsAsync(
            long adGroupId,
            IList<long> adGroupCriterionIds,
            AdGroupCriterionType criterionType)
        {
            var request = new GetAdGroupCriterionsByIdsRequest
            {
                AdGroupId = adGroupId,
                CriterionType = criterionType,
                AdGroupCriterionIds = adGroupCriterionIds
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdGroupCriterionsByIdsAsync(r), request));
        }

        protected async Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(
            long accountId,
            IList<long> adExtensionIds,
            AdExtensionsTypeFilter adExtensionsTypeFilter)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds,
                AdExtensionType = adExtensionsTypeFilter
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdExtensionsByIdsAsync(r), request));
        }

        protected async Task<GetAdExtensionsEditorialReasonsResponse> GetAdExtensionsEditorialReasonsAsync(
            long accountId,
            IList<AdExtensionIdToEntityIdAssociation> associations,
            AssociationType associationType)
        {
            var request = new GetAdExtensionsEditorialReasonsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            return (await CampaignService.CallAsync(
                (s, r) => s.GetAdExtensionsEditorialReasonsAsync(r), request));
        }

        protected async Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(long campaignId)
        {
            var request = new GetAdGroupsByCampaignIdRequest
            {
                CampaignId = campaignId,
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdGroupsByCampaignIdAsync(r), request));
        }

        protected async Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(
            long adGroupId,
            IList<AdType> adTypes)
        {
            var request = new GetAdsByAdGroupIdRequest
            {
                AdGroupId = adGroupId,
                AdTypes = adTypes
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAdsByAdGroupIdAsync(r), request));
        }

        protected async Task<GetAudiencesByIdsResponse> GetAudiencesByIdsAsync(
            IList<long> audienceIds,
            AudienceType type)
        {
            var request = new GetAudiencesByIdsRequest
            {
                AudienceIds = audienceIds,
                Type = type
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAudiencesByIdsAsync(r), request));
        }

        protected async Task<GetBMCStoresByCustomerIdResponse> GetBMCStoresByCustomerIdAsync()
        {
            var request = new GetBMCStoresByCustomerIdRequest();

            return (await CampaignService.CallAsync((s, r) => s.GetBMCStoresByCustomerIdAsync(r), request));
        }

        protected async Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(IList<long> budgetIds)
        {
            var request = new GetBudgetsByIdsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignService.CallAsync((s, r) => s.GetBudgetsByIdsAsync(r), request));
        }

        protected async Task<GetCampaignCriterionsByIdsResponse> GetCampaignCriterionsByIdsAsync(
            long campaignId,
            IList<long> campaignCriterionIds,
            CampaignCriterionType criterionType)
        {
            var request = new GetCampaignCriterionsByIdsRequest
            {
                CampaignId = campaignId,
                CriterionType = criterionType,
                CampaignCriterionIds = campaignCriterionIds
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignCriterionsByIdsAsync(r), request));
        }

        protected async Task<GetCampaignIdsByBudgetIdsResponse> GetCampaignIdsByBudgetIdsAsync(IList<long> budgetIds)
        {
            var request = new GetCampaignIdsByBudgetIdsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignIdsByBudgetIdsAsync(r), request));
        }

        protected async Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(
            long accountId,
            CampaignType campaignType)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request));
        }

        protected async Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(
            long accountId,
            IList<long> campaignIds,
            CampaignType campaignType)
        {
            var request = new GetCampaignsByIdsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds,
                CampaignType = campaignType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request));
        }

        protected async Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(
            IList<long> conversionGoalIds,
            ConversionGoalType conversionGoalTypes
            )
        {
            var request = new GetConversionGoalsByIdsRequest
            {
                ConversionGoalIds = conversionGoalIds,
                ConversionGoalTypes = conversionGoalTypes
            };

            return await CampaignService.CallAsync((s, r) => s.GetConversionGoalsByIdsAsync(r), request);
        }

        protected async Task<GetGeoLocationsFileUrlResponse> GetGeoLocationsFileUrlAsync(string version, string languageLocale)
        {
            var request = new GetGeoLocationsFileUrlRequest
            {
                Version = version,
                LanguageLocale = languageLocale
            };

            return (await CampaignService.CallAsync((s, r) => s.GetGeoLocationsFileUrlAsync(r), request));
        }

        protected async Task<GetKeywordsByAdGroupIdResponse> GetKeywordsByAdGroupIdAsync(long adGroupId)
        {
            var request = new GetKeywordsByAdGroupIdRequest
            {
                AdGroupId = adGroupId
            };

            return (await CampaignService.CallAsync((s, r) => s.GetKeywordsByAdGroupIdAsync(r), request));
        }

        protected async Task<GetLabelAssociationsByEntityIdsResponse> GetLabelAssociationsByEntityIdsAsync(
            IList<long> entityIds,
            EntityType entityType)
        {
            var request = new GetLabelAssociationsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
            };

            return (await CampaignService.CallAsync((s, r) => s.GetLabelAssociationsByEntityIdsAsync(r), request));
        }

        protected async Task<GetLabelAssociationsByLabelIdsResponse> GetLabelAssociationsByLabelIdsAsync(
            IList<long> labelIds,
            EntityType entityType,
            Microsoft.BingAds.V11.CampaignManagement.Paging pageInfo)
        {
            var request = new GetLabelAssociationsByLabelIdsRequest
            {
                LabelIds = labelIds,
                EntityType = entityType,
                PageInfo = pageInfo
            };

            return (await CampaignService.CallAsync((s, r) => s.GetLabelAssociationsByLabelIdsAsync(r), request));
        }

        protected async Task<GetLabelsByIdsResponse> GetLabelsByIdsAsync(
            IList<long> labelIds,
            Microsoft.BingAds.V11.CampaignManagement.Paging pageInfo)
        {
            var request = new GetLabelsByIdsRequest
            {
                LabelIds = labelIds,
                PageInfo = pageInfo
            };

            return (await CampaignService.CallAsync((s, r) => s.GetLabelsByIdsAsync(r), request));
        }
        
        protected async Task<GetListItemsBySharedListResponse> GetListItemsBySharedListAsync(SharedList sharedList)
        {
            var request = new GetListItemsBySharedListRequest
            {
                SharedList = sharedList
            };

            return (await CampaignService.CallAsync((s, r) => s.GetListItemsBySharedListAsync(r), request));
        }

        protected async Task<GetNegativeKeywordsByEntityIdsResponse> GetNegativeKeywordsByEntityIdsAsync(
            IList<long> entityIds,
            string entityType,
            long parentEntityId)
        {
            var request = new GetNegativeKeywordsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                ParentEntityId = parentEntityId
            };

            return (await CampaignService.CallAsync((s, r) => s.GetNegativeKeywordsByEntityIdsAsync(r), request));
        }

        protected async Task<GetSharedEntitiesByAccountIdResponse> GetSharedEntitiesByAccountIdAsync(string sharedEntityType)
        {
            var request = new GetSharedEntitiesByAccountIdRequest
            {
                SharedEntityType = sharedEntityType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetSharedEntitiesByAccountIdAsync(r), request));
        }

        protected async Task<GetSharedEntityAssociationsByEntityIdsResponse> GetSharedEntityAssociationsByEntityIdsAsync(
            IList<long> entityIds,
            string entityType,
            string sharedEntityType)
        {
            var request = new GetSharedEntityAssociationsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                SharedEntityType = sharedEntityType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetSharedEntityAssociationsByEntityIdsAsync(r), request));
        }

        protected async Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(
            string entityType,
            IList<long> sharedEntityIds,
            string sharedEntityType)
        {
            var request = new GetSharedEntityAssociationsBySharedEntityIdsRequest
            {
                EntityType = entityType,
                SharedEntityIds = sharedEntityIds,
                SharedEntityType = sharedEntityType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetSharedEntityAssociationsBySharedEntityIdsAsync(r), request));
        }

        protected async Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(IList<long> tagIds)
        {
            var request = new GetUetTagsByIdsRequest
            {
                TagIds = tagIds,
            };

            return await CampaignService.CallAsync((s, r) => s.GetUetTagsByIdsAsync(r), request);
        }

        protected async Task<SetAccountPropertiesResponse> SetAccountPropertiesAsync(IList<AccountProperty> accountProperties)
        {
            var request = new SetAccountPropertiesRequest
            {
                AccountProperties = accountProperties
            };

            return (await CampaignService.CallAsync((s, r) => s.SetAccountPropertiesAsync(r), request));
        }

        protected async Task<SetAdExtensionsAssociationsResponse> SetAdExtensionsAssociationsAsync(long accountId, IList<AdExtensionIdToEntityIdAssociation> associations, AssociationType associationType)
        {
            var request = new SetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            return (await CampaignService.CallAsync((s, r) => s.SetAdExtensionsAssociationsAsync(r), request));
        }


        protected async Task<SetLabelAssociationsResponse> SetLabelAssociationsAsync(
            IList<LabelAssociation> labelAssociations,
            EntityType entityType)
        {
            var request = new SetLabelAssociationsRequest
            {
                LabelAssociations = labelAssociations,
                EntityType = entityType,
            };

            return (await CampaignService.CallAsync((s, r) => s.SetLabelAssociationsAsync(r), request));
        }

        protected async Task<SetSharedEntityAssociationsResponse> SetSharedEntityAssociationsAsync(IList<SharedEntityAssociation> sharedEntityAssociations)
        {
            var request = new SetSharedEntityAssociationsRequest
            {
                Associations = sharedEntityAssociations
            };

            return (await CampaignService.CallAsync((s, r) => s.SetSharedEntityAssociationsAsync(r), request));
        }

        protected async Task<UpdateAdExtensionsResponse> UpdateAdExtensionsAsync(long accountId, IList<AdExtension> adExtensions)
        {
            var request = new UpdateAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdExtensionsAsync(r), request));
        }

        protected async Task<UpdateAdGroupCriterionsResponse> UpdateAdGroupCriterionsAsync(
           IList<AdGroupCriterion> adGroupCriterions,
           AdGroupCriterionType criterionType)
        {
            var request = new UpdateAdGroupCriterionsRequest
            {
                AdGroupCriterions = adGroupCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdGroupCriterionsAsync(r), request));
        }

        protected async Task<UpdateAdGroupsResponse> UpdateAdGroupsAsync(long campaignId, IList<AdGroup> adGroups)
        {
            var request = new UpdateAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdGroupsAsync(r), request));
        }

        protected async Task<UpdateAdsResponse> UpdateAdsAsync(long adGroupId, IList<Ad> ads)
        {
            var request = new UpdateAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAdsAsync(r), request));
        }

        protected async Task<UpdateAudiencesResponse> UpdateAudiencesAsync(IList<Audience> audiences)
        {
            var request = new UpdateAudiencesRequest
            {
                Audiences = audiences
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateAudiencesAsync(r), request));
        }

        protected async Task<UpdateBudgetsResponse> UpdateBudgetsAsync(IList<Budget> budgets)
        {
            var request = new UpdateBudgetsRequest
            {
                Budgets = budgets
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateBudgetsAsync(r), request));
        }

        protected async Task<UpdateCampaignCriterionsResponse> UpdateCampaignCriterionsAsync(
            IList<CampaignCriterion> campaignCriterions,
            CampaignCriterionType criterionType)
        {
            var request = new UpdateCampaignCriterionsRequest
            {
                CampaignCriterions = campaignCriterions,
                CriterionType = criterionType
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateCampaignCriterionsAsync(r), request));
        }

        protected async Task<UpdateCampaignsResponse> UpdateCampaignsAsync(long accountId, IList<Campaign> campaigns)
        {
            var request = new UpdateCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateCampaignsAsync(r), request));
        }

        protected async Task<UpdateConversionGoalsResponse> UpdateConversionGoalsAsync(IList<ConversionGoal> conversionGoals)
        {
            var request = new UpdateConversionGoalsRequest
            {
                ConversionGoals = conversionGoals
            };

            return await CampaignService.CallAsync((s, r) => s.UpdateConversionGoalsAsync(r), request);
        }

        protected async Task<UpdateKeywordsResponse> UpdateKeywordsAsync(long adGroupId, IList<Keyword> keywords)
        {
            var request = new UpdateKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords
            };

            return await CampaignService.CallAsync((s, r) => s.UpdateKeywordsAsync(r), request);
        }

        protected async Task<UpdateLabelsResponse> UpdateLabelsAsync(IList<Label> labels)
        {
            var request = new UpdateLabelsRequest
            {
                Labels = labels
            };

            return await CampaignService.CallAsync((s, r) => s.UpdateLabelsAsync(r), request);
        }

        protected async Task<UpdateSharedEntitiesResponse> UpdateSharedEntitiesAsync(IList<SharedEntity> sharedEntities)
        {
            var request = new UpdateSharedEntitiesRequest
            {
                SharedEntities = sharedEntities
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateSharedEntitiesAsync(r), request));
        }

        protected async Task<UpdateUetTagsResponse> UpdateUetTagsAsync(IList<UetTag> uetTags)
        {
            var request = new UpdateUetTagsRequest
            {
                UetTags = uetTags
            };

            return (await CampaignService.CallAsync((s, r) => s.UpdateUetTagsAsync(r), request));
        }


        #endregion CampaignManagement_ServiceOperations

        #region CustomerManagement_Output

        protected void OutputCustomerNestedPartialErrors(IList<IList<Microsoft.BingAds.V11.CustomerManagement.OperationError>> partialErrors)
        {
            if (partialErrors == null || partialErrors.Count == 0)
            {
                OutputStatusMessage("No partial errors\n");
                return;
            }

            foreach (Microsoft.BingAds.V11.CustomerManagement.OperationError error in partialErrors)
            {
                OutputCustomerOperationError(error);
            }
        }

        protected void OutputCustomerPartialErrors(IList<Microsoft.BingAds.V11.CustomerManagement.OperationError> partialErrors)
        {
            if (partialErrors == null || partialErrors.Count == 0)
            {
                OutputStatusMessage("No partial errors\n");
                return;
            }

            foreach (Microsoft.BingAds.V11.CustomerManagement.OperationError error in partialErrors)
            {
                OutputCustomerOperationError(error);
            }
        }

        protected void OutputCustomerOperationError(Microsoft.BingAds.V11.CustomerManagement.OperationError operationError)
        {
            if (operationError == null)
            {
                return;
            }

            OutputStatusMessage("OperationError:\n");
            OutputStatusMessage(string.Format("\tCode: {0}", operationError.Code));
            OutputStatusMessage(string.Format("\tDetails: {0}", operationError.Details));
            OutputStatusMessage(string.Format("\tMessage: {0}\n", operationError.Message));
        }

        protected void OutputAccount(Account account)
        {
            OutputStatusMessage(string.Format("Account Id: {0}", account.Id));
            OutputStatusMessage(string.Format("Account Number: {0}", account.Number));
            OutputStatusMessage(string.Format("Account Name: {0}", account.Name));
            OutputStatusMessage(string.Format("Account Parent Customer Id: {0}", account.ParentCustomerId));
        }

        protected void OutputClientLinks(IEnumerable<ClientLink> clientLinks)
        {
            if (clientLinks == null)
            {
                return;
            }

            foreach (ClientLink clientLink in clientLinks)
            {
                OutputStatusMessage(string.Format("Status: {0}", clientLink.Status));
                OutputStatusMessage(string.Format("ClientAccountId: {0}", clientLink.ClientAccountId));
                OutputStatusMessage(string.Format("ClientAccountNumber: {0}", clientLink.ClientAccountNumber));
                OutputStatusMessage(string.Format("ManagingAgencyCustomerId: {0}", clientLink.ManagingCustomerId));
                OutputStatusMessage(string.Format("ManagingCustomerNumber: {0}", clientLink.ManagingCustomerNumber));
                OutputStatusMessage(string.Format(clientLink.IsBillToClient ? "IsBillToClient: True" : "IsBillToClient: False"));
                OutputStatusMessage(string.Format("InviterEmail: {0}", clientLink.InviterEmail));
                OutputStatusMessage(string.Format("InviterName: {0}", clientLink.InviterName));
                OutputStatusMessage(string.Format("InviterPhone: {0}", clientLink.InviterPhone));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", clientLink.LastModifiedByUserId));
                OutputStatusMessage(string.Format("LastModifiedDateTime: {0}", clientLink.LastModifiedDateTime));
                OutputStatusMessage(string.Format("Name: {0}", clientLink.Name));
                OutputStatusMessage(string.Format("Note: {0}", clientLink.Note));
                OutputStatusMessage("");
            }
        }

        protected void OutputUserInvitations(IEnumerable<UserInvitation> userInvitations)
        {
            if (userInvitations == null)
            {
                return;
            }

            foreach (UserInvitation userInvitation in userInvitations)
            {
                OutputStatusMessage(string.Format("FirstName: {0}", userInvitation.FirstName));
                OutputStatusMessage(string.Format("LastName: {0}", userInvitation.LastName));
                OutputStatusMessage(string.Format("Email: {0}", userInvitation.Email));
                OutputStatusMessage(string.Format("Role: {0}", userInvitation.Role));
                OutputStatusMessage(string.Format("Invitation Id: {0}\n", userInvitation.Id));
            }
        }

        protected void OutputUser(User user)
        {
            if (user == null)
            {
                return;
            }

            OutputStatusMessage(string.Format("Id: {0}", user.Id));
            OutputStatusMessage(string.Format("UserName: {0}", user.UserName));
            OutputStatusMessage(string.Format("Contact Email: {0}", user.ContactInfo.Email));
            OutputStatusMessage(string.Format("First Name: {0}", user.Name.FirstName));
            OutputStatusMessage(string.Format("Last Name: {0}", user.Name.LastName));
        }

        #endregion CustomerManagement_Output

        #region CustomerManagement_ServiceOperations
        
        protected async Task<AddClientLinksResponse> AddClientLinksAsync(IList<ClientLink> clientLinks)
        {
            var request = new AddClientLinksRequest
            {
                ClientLinks = clientLinks
            };

            return (await CustomerService.CallAsync((s, r) => s.AddClientLinksAsync(r), request));
        } 

        protected async Task<GetAccountResponse> GetAccountAsync(long accountId)
        {
            var request = new GetAccountRequest
            {
                AccountId = accountId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetAccountAsync(r), request));
        }

        protected async Task<GetCustomerPilotFeaturesResponse> GetCustomerPilotFeaturesAsync(long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request));
        }

        protected async Task<GetUserResponse> GetUserAsync(long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetUserAsync(r), request));
        }
        
        protected async Task<GetUsersInfoResponse> GetUsersInfoAsync(long customerId)
        {
            var request = new GetUsersInfoRequest
            {
                CustomerId = customerId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetUsersInfoAsync(r), request));
        }

        protected async Task<SearchClientLinksResponse> SearchClientLinksAsync(
           IList<Microsoft.BingAds.V11.CustomerManagement.OrderBy> ordering,
           Microsoft.BingAds.V11.CustomerManagement.Paging pageInfo,
           IList<Microsoft.BingAds.V11.CustomerManagement.Predicate> predicates)
        {
            var request = new SearchClientLinksRequest
            {
                Ordering = ordering,
                PageInfo = pageInfo,
                Predicates = predicates
            };

            return (await CustomerService.CallAsync((s, r) => s.SearchClientLinksAsync(r), request));
        }

        protected async Task<SearchUserInvitationsResponse> SearchUserInvitationsAsync(
            IList<Microsoft.BingAds.V11.CustomerManagement.Predicate> predicates)
        {
            var request = new SearchUserInvitationsRequest
            {
                Predicates = predicates,
            };

            return (await CustomerService.CallAsync((s, r) => s.SearchUserInvitationsAsync(r), request));
        }

        protected async Task<SendUserInvitationResponse> SendUserInvitationAsync(UserInvitation userInvitation)
        {
            var request = new SendUserInvitationRequest
            {
                UserInvitation = userInvitation
            };

            return (await CustomerService.CallAsync((s, r) => s.SendUserInvitationAsync(r), request));
        }
        
        protected async Task<SignupCustomerResponse> SignupCustomerAsync(
            Customer customer,
            Account account,
            long? parentCustomerId)
        {
            var request = new SignupCustomerRequest
            {
                Customer = customer,
                Account = account,
                ParentCustomerId = parentCustomerId,
            };

            return (await CustomerService.CallAsync((s, r) => s.SignupCustomerAsync(r), request));
        }

        protected async Task<UpdateAccountResponse> UpdateAccountAsync(Account account)
        {
            var request = new UpdateAccountRequest
            {
                Account = account
            };

            return (await CustomerService.CallAsync((s, r) => s.UpdateAccountAsync(r), request));
        }

        protected async Task<UpdateClientLinksResponse> UpdateClientLinksAsync(IList<ClientLink> clientLinks)
        {
            var request = new UpdateClientLinksRequest
            {
                ClientLinks = clientLinks
            };

            return (await CustomerService.CallAsync((s, r) => s.UpdateClientLinksAsync(r), request));
        }

        #endregion CustomerManagement_ServiceOperations

    }
}
