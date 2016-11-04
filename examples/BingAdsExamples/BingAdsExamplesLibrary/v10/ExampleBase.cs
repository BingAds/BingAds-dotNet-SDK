using System;
using System.Collections.Generic;
using Microsoft.BingAds.V10.CampaignManagement;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads examples. 
    /// </summary>
    public abstract class ExampleBase : BingAdsExamplesLibrary.ExampleBase
    {
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

        /// <summary>
        /// Gets an example Campaign. 
        /// </summary>
        protected Campaign GetExampleCampaign()
        {
            return new Campaign
            {
                Id = null,
                Name = "Women's Shoes " + DateTime.UtcNow,
                Description = "Red shoes line.",
                BudgetType = BudgetLimitType.DailyBudgetStandard,
                DailyBudget = 50.00,
                TimeZone = "PacificTimeUSCanadaTijuana",
                DaylightSaving = true,
            };
        }

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
                OutputStatusMessage(string.Format("MonthlyBudget (Deprecated): {0}", campaign.MonthlyBudget));
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
        /// Gets an example AdGroup. 
        /// </summary>
        protected AdGroup GetExampleAdGroup()
        {
            return new AdGroup
            {
                Name = "Women's Red Shoe Sale " + DateTime.UtcNow,
                AdDistribution = AdDistribution.Search,
                PricingModel = PricingModel.Cpc,
                StartDate = null,
                EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date
                {
                    Month = 12,
                    Day = 31,
                    Year = DateTime.UtcNow.Year
                },
                SearchBid = new Bid { Amount = 0.09 },
                Language = "English",
            };
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
                OutputStatusMessage(string.Format("BiddingModel: {0}", adGroup.BiddingModel));
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
        /// Outputs the campaign identifiers, as well as any partial errors.
        /// </summary>
        /// <param name="campaigns"></param>
        /// <param name="campaignIds"></param>
        /// <param name="partialErrors"></param>
        protected void OutputCampaignsWithPartialErrors(
            Campaign[] campaigns,
            long?[] campaignIds,
            IEnumerable<BatchError> partialErrors)
        {
            if (campaigns == null || campaignIds == null || campaigns.Length != campaignIds.Length)
            {
                return;
            }

            // Output the identifier of each successfully added campaign.

            for (var index = 0; index < campaigns.Length; index++)
            {
                // The array of campaign identifiers equals the size of the attempted campaigns. If the element 
                // is not null, the campaign at that index was added successfully and has a campaign identifer. 

                if (campaignIds[index] != null)
                {
                    OutputStatusMessage(string.Format("Campaign[{0}] (Name:{1}) successfully added and assigned CampaignId {2}",
                        index,
                        campaigns[index].Name,
                        campaignIds[index]));
                }
            }

            // Output the error details for any campaign not successfully added.
            // Note also that multiple error reasons may exist for the same attempted campaign.

            foreach (BatchError error in partialErrors)
            {
                // The index of the partial errors is equal to the index of the list
                // specified in the call to AddCampaigns.

                OutputStatusMessage(string.Format("\nCampaign[{0}] (Name:{1}) not added due to the following error:",
                    error.Index, campaigns[error.Index].Name));

                OutputStatusMessage(string.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(string.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(string.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(string.Format("\tMessage: {0}", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(string.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(string.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(string.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(string.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }

            OutputStatusMessage("\n");
        }

        /// <summary>
        /// Outputs the ad group identifiers, as well as any partial errors
        /// </summary>
        /// <param name="adGroups"></param>
        /// <param name="adGroupIds"></param>
        /// <param name="partialErrors"></param>
        protected void OutputAdGroupsWithPartialErrors(
            AdGroup[] adGroups,
            long?[] adGroupIds,
            IEnumerable<BatchError> partialErrors)
        {
            if (adGroups == null || adGroupIds == null || adGroups.Length != adGroupIds.Length)
            {
                return;
            }

            // Output the identifier of each successfully added ad group.

            for (var index = 0; index < adGroupIds.Length; index++)
            {
                // The array of keyword identifiers equals the size of the attempted ad groups. If the element 
                // is not null, the ad group at that index was added successfully and has an ad group identifer. 

                if (adGroupIds[index] != null)
                {
                    OutputStatusMessage(string.Format("AdGroup[{0}] (Name:{1}) successfully added and assigned AdGroupId {2}",
                        index,
                        adGroups[index].Name,
                        adGroupIds[index]));
                }
            }

            // Output the error details for any ad group not successfully added.
            // Note also that multiple error reasons may exist for the same attempted ad group.

            foreach (BatchError error in partialErrors)
            {
                // The index of the partial errors is equal to the index of the list
                // specified in the call to AddAdGroups.

                OutputStatusMessage(string.Format("\nAdGroup[{0}] (Name:{1}) not added due to the following error:",
                    error.Index, adGroups[error.Index].Name));

                OutputStatusMessage(string.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(string.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(string.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(string.Format("\tMessage: {0}", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(string.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(string.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(string.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(string.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }

            OutputStatusMessage("\n");
        }

        /// <summary>
        /// Outputs the keyword identifiers, as well as any partial errors
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="keywordIds"></param>
        /// <param name="partialErrors"></param>
        protected void OutputKeywordsWithPartialErrors(
            Keyword[] keywords,
            long?[] keywordIds,
            IEnumerable<BatchError> partialErrors)
        {
            if (keywords == null || keywordIds == null || keywords.Length != keywordIds.Length)
            {
                return;
            }

            // Output the identifier of each successfully added keyword.

            for (var index = 0; index < keywords.Length; index++)
            {
                // The array of keyword identifiers equals the size of the attempted keywords. If the element 
                // is not null, the keyword at that index was added successfully and has a keyword identifer. 

                if (keywordIds[index] != null)
                {
                    OutputStatusMessage(string.Format("Keyword[{0}] (Text:{1}) successfully added and assigned KeywordId {2}",
                        index,
                        keywords[index].Text,
                        keywordIds[index]));
                }
            }

            // Output the error details for any keyword not successfully added.
            // Note also that multiple error reasons may exist for the same attempted keyword.

            foreach (BatchError error in partialErrors)
            {
                // The index of the partial errors is equal to the index of the list
                // specified in the call to AddKeywords.

                OutputStatusMessage(string.Format("\nKeyword[{0}] (Text:{1}) not added due to the following error:",
                    error.Index, keywords[error.Index].Text));

                OutputStatusMessage(string.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(string.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(string.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(string.Format("\tMessage: {0}", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(string.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(string.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(string.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(string.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }

            OutputStatusMessage("\n");
        }

        /// <summary>
        /// Outputs the ad identifiers, as well as any partial errors
        /// </summary>
        /// <param name="ads"></param>
        /// <param name="adIds"></param>
        /// <param name="partialErrors"></param>
        protected void OutputAdsWithPartialErrors(
            IList<Ad> ads,
            IList<long?> adIds,
            IEnumerable<BatchError> partialErrors)
        {
            if (ads == null || adIds == null || ads.Count != adIds.Count)
            {
                return;
            }

            for (var index = 0; index < ads.Count; index++)
            {
                // Determine the type of ad. Prepare the corresponding attribute value to be outputed,
                // both for successful new ads and partial errors. 

                if(adIds[index] != null)
                {
                    OutputStatusMessage(string.Format("\nAd[{0}] successfully added:", index));
                    ads[index].Id = adIds[index];
                    var expandedTextAd = ads[index] as ExpandedTextAd;
                    if (expandedTextAd != null)
                    {
                        OutputExpandedTextAd(expandedTextAd);
                    }
                    else
                    {
                        var textAd = ads[index] as TextAd;
                        if (textAd != null)
                        {
                            OutputTextAd(textAd);
                        }
                        else
                        {
                            var productAd = ads[index] as ProductAd;
                            if (productAd != null)
                            {
                                OutputProductAd(productAd);
                            }
                            else
                            {
                                var dynamicSearchAd = ads[index] as DynamicSearchAd;
                                if (dynamicSearchAd != null)
                                {
                                    OutputDynamicSearchAd(dynamicSearchAd);
                                }
                                else
                                {
                                    OutputStatusMessage("Unknown Ad Type");
                                }
                            }
                        }
                    }
                }
            }

            foreach (BatchError error in partialErrors)
            {
                // The index of the partial errors is equal to the index of the list
                // specified in the call to AddAds.

                OutputStatusMessage(string.Format("\nAd[{0}] not added due to the following error:", error.Index));

                OutputStatusMessage(string.Format("\tIndex: {0}", error.Index));
                OutputStatusMessage(string.Format("\tCode: {0}", error.Code));
                OutputStatusMessage(string.Format("\tErrorCode: {0}", error.ErrorCode));
                OutputStatusMessage(string.Format("\tMessage: {0}", error.Message));

                // In the case of an EditorialError, more details are available
                if (error.Type == "EditorialError" && error.ErrorCode == "CampaignServiceEditorialValidationError")
                {
                    OutputStatusMessage(string.Format("\tDisapprovedText: {0}", ((EditorialError)(error)).DisapprovedText));
                    OutputStatusMessage(string.Format("\tLocation: {0}", ((EditorialError)(error)).Location));
                    OutputStatusMessage(string.Format("\tPublisherCountry: {0}", ((EditorialError)(error)).PublisherCountry));
                    OutputStatusMessage(string.Format("\tReasonCode: {0}\n", ((EditorialError)(error)).ReasonCode));
                }
            }

            OutputStatusMessage("\n");
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
                if (productPartition != null)
                {
                    OutputProductPartition(productPartition);
                }
                else
                {
                    var productScope = criterion as ProductScope;
                    if (productScope != null)
                    {
                        OutputProductScope(productScope);
                    }
                    else
                    {
                        var webpage = criterion as Webpage;
                        if (webpage != null)
                        {
                            OutputWebpage(webpage);
                        }
                    }
                }
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
                OutputStatusMessage(string.Format("Webpage Conditions:"));
                foreach (var condition in webpage.Parameter.Conditions)
                {
                    OutputStatusMessage(string.Format("\tOperand: {0}", condition.Operand));
                    OutputStatusMessage(string.Format("\tArgument: {0}", condition.Argument));
                }
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
            }
        }

        /// <summary>
        /// Outputs the FixedBid.
        /// </summary>
        protected void OutputFixedBid(FixedBid fixedBid)
        {
            if (fixedBid != null && fixedBid.Bid != null)
            {
                OutputStatusMessage(string.Format("Bid Amount: {0}", fixedBid.Bid.Amount));
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
                OutputStatusMessage(string.Format("IsExact: {0}", criterion.DestinationUrl));
                OutputStatusMessage(string.Format("Source: {0}", criterion.EditorialStatus));
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
                OutputStatusMessage(string.Format("Id: {0}", criterion.Id));
                OutputStatusMessage(string.Format("Status: {0}", criterion.Status));
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
                OutputStatusMessage(string.Format("AdGroupId: {0}", criterion.BidAdjustment));
                OutputStatusMessage(string.Format("AdGroupId: {0}", criterion.CampaignId));
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
                OutputStatusMessage(string.Format("AdGroupCriterion Type: {0}", criterion.Type));
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
                    var campaignCriterion = criterion as CampaignCriterion;
                    if (campaignCriterion != null)
                    {
                        OutputCampaignCriterion(campaignCriterion);
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
                OutputStatusMessage("None\n");
                return;
            }
            
            foreach (long? id in ids)
            {
                OutputStatusMessage("" + id + "\n");
            }
        }

        /// <summary>
        /// Outputs a list of BatchError objects that represent partial errors from a service operation.
        /// </summary>
        /// <param name="partialErrors"></param>
        protected void OutputPartialErrors(IList<BatchError> partialErrors)
        {
            if (partialErrors == null || partialErrors.Count == 0)
            {
                OutputStatusMessage("None\n");
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
                OutputStatusMessage("None\n");
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
        /// Gets an example Keyword. 
        /// </summary>
        protected Keyword GetExampleKeyword()
        {
            return new Keyword
            {
                Bid = new Bid { Amount = 0.47 },
                Param2 = "10% Off",
                MatchType = MatchType.Phrase,
                Text = "Brand-A Shoes"
            };
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
        /// Gets an example ProductAd. 
        /// </summary>
        protected ProductAd GetExampleProductAd()
        {
            return new ProductAd
            {
                PromotionalText = "Free shipping on $99 purchases."
            };
        }

        /// <summary>
        /// Gets an example ExpandedTextAd. 
        /// </summary>
        protected ExpandedTextAd GetExampleExpandedTextAd()
        {
            return new ExpandedTextAd
            {
                TitlePart1 = "Contoso",
                TitlePart2 = "Fast & Easy Setup",
                Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                Path1 = "seattle",
                Path2 = "shoe sale",

                // With FinalUrls you can separate the tracking template, custom parameters, and 
                // landing page URLs. 
                FinalUrls = new[] {
                    "http://www.contoso.com/womenshoesale"
                },
                // Final Mobile URLs can also be used if you want to direct the user to a different page 
                // for mobile devices.
                FinalMobileUrls = new[] {
                    "http://mobile.contoso.com/womenshoesale"
                },
                // You could use a tracking template which would override the campaign level
                // tracking template. Tracking templates defined for lower level entities 
                // override those set for higher level entities.
                // In this example we are using the campaign level tracking template.
                TrackingUrlTemplate = null,

                // Set custom parameters that are specific to this ad, 
                // and can be used by the ad, ad group, campaign, or account level tracking template. 
                // In this example we are using the campaign level tracking template.
                UrlCustomParameters = new CustomParameters
                {
                    Parameters = new[] {
                        new CustomParameter(){
                            Key = "promoCode",
                            Value = "PROMO1"
                        },
                        new CustomParameter(){
                            Key = "season",
                            Value = "summer"
                        },
                    }
                }
            };
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
        /// Outputs the AdGroupCriterion that contains a ProductPartition.
        /// </summary>
        protected void OutputAdGroupCriterionWithProductPartition(AdGroupCriterion adGroupCriterion)
        {
            if (adGroupCriterion != null)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", adGroupCriterion.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupCriterion Id: {0}", adGroupCriterion.Id));
                OutputStatusMessage(string.Format("AdGroupCriterion Type: {0}", adGroupCriterion.Type));

                var biddableAdGroupCriterion = adGroupCriterion as BiddableAdGroupCriterion;
                if (biddableAdGroupCriterion != null)
                {
                    OutputStatusMessage(string.Format("DestinationUrl: {0}", biddableAdGroupCriterion.DestinationUrl));

                    OutputStatusMessage("FinalMobileUrls: ");
                    if (biddableAdGroupCriterion.FinalMobileUrls != null)
                    {
                        foreach (var finalMobileUrl in biddableAdGroupCriterion.FinalMobileUrls)
                        {
                            OutputStatusMessage(string.Format("\t{0}", finalMobileUrl));
                        }
                    }

                    OutputStatusMessage("FinalUrls: ");
                    if (biddableAdGroupCriterion.FinalUrls != null)
                    {
                        foreach (var finalUrl in biddableAdGroupCriterion.FinalUrls)
                        {
                            OutputStatusMessage(string.Format("\t{0}", finalUrl));
                        }
                    }

                    OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", biddableAdGroupCriterion.TrackingUrlTemplate));
                    OutputStatusMessage("UrlCustomParameters: ");
                    if (biddableAdGroupCriterion.UrlCustomParameters != null && biddableAdGroupCriterion.UrlCustomParameters.Parameters != null)
                    {
                        foreach (var customParameter in biddableAdGroupCriterion.UrlCustomParameters.Parameters)
                        {
                            OutputStatusMessage(string.Format("\tKey: {0}", customParameter.Key));
                            OutputStatusMessage(string.Format("\tValue: {0}", customParameter.Value));
                        }
                    }

                    // Output the Campaign Management FixedBid Object
                    OutputFixedBid((FixedBid)biddableAdGroupCriterion.CriterionBid);
                }
                else
                {
                    var negativeAdGroupCriterion = adGroupCriterion as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        OutputNegativeAdGroupCriterion(negativeAdGroupCriterion);
                    }
                }

                // Output the Campaign Management ProductPartition Object
                OutputProductPartition((ProductPartition)adGroupCriterion.Criterion);
            }
        }

        /// <summary>
        /// Outputs the CampaignCriterion that contains a Product Scope.
        /// </summary>
        protected void OutputCampaignCriterionWithProductScope(CampaignCriterion campaignCriterion)
        {
            if (campaignCriterion != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", campaignCriterion.BidAdjustment));
                OutputStatusMessage(string.Format("CampaignId: {0}", campaignCriterion.CampaignId));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (campaignCriterion.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in campaignCriterion.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("CampaignCriterion Id: {0}", campaignCriterion.Id));
                OutputStatusMessage(string.Format("CampaignCriterion Type: {0}", campaignCriterion.Type));

                // Output the Campaign Management ProductScope Object
                OutputProductScope((ProductScope)campaignCriterion.Criterion);
            }
        }

        /// <summary>
        /// Gets an example AgeTarget. 
        /// </summary>
        protected AgeTarget GetExampleAgeTarget()
        {
            return new AgeTarget
            {
                Bids = new List<AgeTargetBid>
                {
                    new AgeTargetBid{
                        Age = AgeRange.ThirtyFiveToFifty,
                        BidAdjustment = 10,
                    }
                }
            };
        }

        /// <summary>
        /// Gets an example AgeTargetBid. 
        /// </summary>
        protected AgeTargetBid GetExampleAgeTargetBid()
        {
            return new AgeTargetBid
            {
                Age = AgeRange.ThirtyFiveToFifty,
                BidAdjustment = 10,
            };
        }

        /// <summary>
        /// Outputs the AgeTargetBid.
        /// </summary>
        protected void OutputAgeTargetBid(AgeTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Age : {0}", targetBid.Age));
            }
        }

        /// <summary>
        /// Gets an example GenderTarget. 
        /// </summary>
        protected GenderTarget GetExampleGenderTarget()
        {
            return new GenderTarget
            {
                Bids = new List<GenderTargetBid>
                {
                    new GenderTargetBid
                    {
                        BidAdjustment = 10,
                        Gender = GenderType.Female,
                    },
                }
            };
        }

        /// <summary>
        /// Outputs the GenderTargetBid.
        /// </summary>
        protected void OutputGenderTargetBid(GenderTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Gender : {0}", targetBid.Gender));
            }
        }

        /// <summary>
        /// Gets an example DayTimeTarget. 
        /// </summary>
        protected DayTimeTarget GetExampleDayTimeTarget()
        {
            return new DayTimeTarget
            {
                Bids = new List<DayTimeTargetBid>
                {
                    new DayTimeTargetBid
                    {
                        BidAdjustment = 10,
                        Day = Day.Friday,
                        FromHour = 11,
                        FromMinute = Minute.Zero,
                        ToHour = 13,
                        ToMinute = Minute.Fifteen
                    },
                }
            };
        }

        /// <summary>
        /// Outputs the DayTimeTargetBid.
        /// </summary>
        protected void OutputDayTimeTargetBid(DayTimeTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Day : {0}", targetBid.Day));
                OutputStatusMessage(string.Format("FromHour : {0}", targetBid.FromHour));
                OutputStatusMessage(string.Format("FromMinute: {0}", targetBid.FromMinute));
                OutputStatusMessage(string.Format("ToHour : {0}", targetBid.ToHour));
                OutputStatusMessage(string.Format("ToMinute: {0}", targetBid.ToMinute));
            }
        }

        /// <summary>
        /// Gets an example DeviceOSTarget. 
        /// </summary>
        protected DeviceOSTarget GetExampleDeviceOSTarget()
        {
            return new DeviceOSTarget
            {
                Bids = new List<DeviceOSTargetBid>
                {
                    new DeviceOSTargetBid
                    {
                        BidAdjustment = 10,
                        DeviceName = "Tablets"
                    },
                }
            };
        }

        /// <summary>
        /// Outputs the DeviceOSTargetBid.
        /// </summary>
        protected void OutputDeviceOSTargetBid(DeviceOSTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("DeviceName : {0}", targetBid.DeviceName));
            }
        }

        /// <summary>
        /// Gets an example Target. 
        /// </summary>
        protected Target GetExampleTarget()
        {
            return new Target()
            {
                Id = null,
                Age = GetExampleAgeTarget(),
                DayTime = GetExampleDayTimeTarget(),
                DeviceOS = GetExampleDeviceOSTarget(),
                Gender = GetExampleGenderTarget(),
                Location = GetExampleLocationTarget(),
            };
        }

        /// <summary>
        /// Outputs the Target.
        /// </summary>
        protected void OutputTarget(Target target)
        {
            if (target != null)
            {
                OutputStatusMessage(string.Format("Target.Id: {0}", target.Id));
                if (target.Age != null)
                {
                    foreach (var bid in target.Age.Bids)
                    {
                        OutputAgeTargetBid(bid);
                    }
                }
                if (target.DayTime != null)
                {
                    foreach (var bid in target.DayTime.Bids)
                    {
                        OutputDayTimeTargetBid(bid);
                    }
                }
                if (target.DeviceOS != null)
                {
                    foreach (var bid in target.DeviceOS.Bids)
                    {
                        OutputDeviceOSTargetBid(bid);
                    }
                }
                if (target.Gender != null)
                {
                    foreach (var bid in target.Gender.Bids)
                    {
                        OutputGenderTargetBid(bid);
                    }
                }
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (target.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in target.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputLocationTarget(target.Location);
            }
        }

        /// <summary>
        /// Gets an example LocationTarget. 
        /// </summary>
        protected LocationTarget GetExampleLocationTarget()
        {
            return new LocationTarget
            {
                IntentOption = IntentOption.PeopleSearchingForOrViewingPages,
                CityTarget = GetExampleCityTarget(),
                CountryTarget = GetExampleCountryTarget(),
                MetroAreaTarget = GetExampleMetroAreaTarget(),
                StateTarget = GetExampleStateTarget(),
                PostalCodeTarget = GetExamplePostalCodeTarget(),
                RadiusTarget = null
            };
        }

        /// <summary>
        /// Outputs the LocationTarget.
        /// </summary>
        protected void OutputLocationTarget(LocationTarget locationTarget)
        {
            if (locationTarget != null)
            {
                OutputStatusMessage(string.Format("IntentOption: {0}", locationTarget.IntentOption));
                if (locationTarget.CityTarget != null)
                {
                    foreach (var bid in locationTarget.CityTarget.Bids)
                    {
                        OutputCityTargetBid(bid);
                    }
                }
                if (locationTarget.CountryTarget != null)
                {
                    foreach (var bid in locationTarget.CountryTarget.Bids)
                    {
                        OutputCountryTargetBid(bid);
                    }
                }
                if (locationTarget.MetroAreaTarget != null)
                {
                    foreach (var bid in locationTarget.MetroAreaTarget.Bids)
                    {
                        OutputMetroAreaTargetBid(bid);
                    }
                }
                if (locationTarget.PostalCodeTarget != null)
                {
                    foreach (var bid in locationTarget.PostalCodeTarget.Bids)
                    {
                        OutputPostalCodeTargetBid(bid);
                    }
                }
                if (locationTarget.RadiusTarget != null)
                {
                    foreach (var bid in locationTarget.RadiusTarget.Bids)
                    {
                        OutputRadiusTargetBid(bid);
                    }
                }
                if (locationTarget.StateTarget != null)
                {
                    foreach (var bid in locationTarget.StateTarget.Bids)
                    {
                        OutputStateTargetBid(bid);
                    }
                }
            }
        }

        /// <summary>
        /// Gets an example CityTarget. 
        /// </summary>
        protected CityTarget GetExampleCityTarget()
        {
            return new CityTarget
            {
                Bids = new List<CityTargetBid>
                {
                    new CityTargetBid
                    {
                        BidAdjustment = 15,
                        City = "Toronto, Toronto ON CA",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the CityTargetBid.
        /// </summary>
        protected void OutputCityTargetBid(CityTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("City : {0}", targetBid.City));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example CountryTarget. 
        /// </summary>
        protected CountryTarget GetExampleCountryTarget()
        {
            return new CountryTarget
            {
                Bids = new List<CountryTargetBid>
                {
                    new CountryTargetBid
                    {
                        BidAdjustment = 15,
                        CountryAndRegion = "CA",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the CountryTargetBid.
        /// </summary>
        protected void OutputCountryTargetBid(CountryTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("CountryAndRegion : {0}", targetBid.CountryAndRegion));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example MetroAreaTarget. 
        /// </summary>
        protected MetroAreaTarget GetExampleMetroAreaTarget()
        {
            return new MetroAreaTarget
            {
                Bids = new List<MetroAreaTargetBid>
                {
                    new MetroAreaTargetBid
                    {
                        BidAdjustment = 15,
                        MetroArea = "Seattle-Tacoma, WA, WA US",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the MetroAreaTargetBid.
        /// </summary>
        protected void OutputMetroAreaTargetBid(MetroAreaTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("MetroArea : {0}", targetBid.MetroArea));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example StateTarget. 
        /// </summary>
        protected StateTarget GetExampleStateTarget()
        {
            return new StateTarget
            {
                Bids = new List<StateTargetBid>
                {
                    new StateTargetBid
                    {
                        BidAdjustment = 15,
                        State = "US-WA",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the StateTargetBid.
        /// </summary>
        protected void OutputStateTargetBid(StateTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("State : {0}", targetBid.State));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example PostalCodeTarget. 
        /// </summary>
        protected PostalCodeTarget GetExamplePostalCodeTarget()
        {
            return new PostalCodeTarget
            {
                Bids = new List<PostalCodeTargetBid>
                {
                    new PostalCodeTargetBid
                    {
                        BidAdjustment = 10,
                        PostalCode = "98052, WA US",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the PostalCodeTargetBid.
        /// </summary>
        protected void OutputPostalCodeTargetBid(PostalCodeTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("PostalCode : {0}", targetBid.PostalCode));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example RadiusTarget. 
        /// </summary>
        protected RadiusTarget GetExampleRadiusTarget()
        {
            return new RadiusTarget
            {
                Bids = new List<RadiusTargetBid>
                {
                    new RadiusTargetBid
                    {
                        BidAdjustment = 50,
                        LatitudeDegrees = 47.755367,
                        LongitudeDegrees = -122.091827,
                        Radius = 11,
                        RadiusUnit = DistanceUnit.Kilometers
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the RadiusTargetBid.
        /// </summary>
        protected void OutputRadiusTargetBid(RadiusTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Name : {0}", targetBid.Name));
                OutputStatusMessage(string.Format("Radius : {0}", targetBid.Radius));
                var radiusUnit = targetBid.RadiusUnit == DistanceUnit.Kilometers ? "Kilometers" : "Miles";
                OutputStatusMessage(string.Format("RadiusUnit: {0}", radiusUnit));
            }
        }

        /// <summary>
        /// Gets an example NegativeKeyword. 
        /// </summary>
        protected NegativeKeyword GetExampleNegativeKeyword()
        {
            return new NegativeKeyword
            {
                Id = null,
                MatchType = MatchType.Exact,
                Text = "auto",
                Type = "NegativeKeyword"
            };
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
        /// Gets an example NegativeKeywordList. 
        /// </summary>
        protected NegativeKeywordList GetExampleNegativeKeywordList()
        {
            return new NegativeKeywordList
            {
                Id = null,
                Name = "My Negative Keyword List" + DateTime.UtcNow,
                Type = "NegativeKeywordList"
            };
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
        /// Outputs the RemarketingList.
        /// </summary>
        protected void OutputRemarketingList(RemarketingList remarketingList)
        {
            if (remarketingList != null)
            {
                OutputStatusMessage(string.Format("Description: {0}", remarketingList.Description));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (remarketingList.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in remarketingList.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("\tKey: {0}", pair.Key));
                        OutputStatusMessage(string.Format("\tValue: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", remarketingList.Id));
                OutputStatusMessage(string.Format("MembershipDuration: {0}", remarketingList.MembershipDuration));
                OutputStatusMessage(string.Format("Name: {0}", remarketingList.Name));
                OutputStatusMessage(string.Format("ParentId: {0}", remarketingList.ParentId));
                OutputStatusMessage(string.Format("Scope: {0}", remarketingList.Scope));
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
        /// Outputs the AdGroupRemarketingListAssociation.
        /// </summary>
        protected void OutputAdGroupRemarketingListAssociation(AdGroupRemarketingListAssociation adGroupRemarketingListAssociation)
        {
            if (adGroupRemarketingListAssociation != null)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", adGroupRemarketingListAssociation.AdGroupId));
                OutputStatusMessage(string.Format("BidAdjustment: {0}", adGroupRemarketingListAssociation.BidAdjustment));
                OutputStatusMessage(string.Format("Id: {0}", adGroupRemarketingListAssociation.Id));
                OutputStatusMessage(string.Format("RemarketingListId: {0}", adGroupRemarketingListAssociation.RemarketingListId));
                OutputStatusMessage(string.Format("Status: {0}\n", adGroupRemarketingListAssociation.Status));
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
        /// Gets a list of example negative websites. 
        /// </summary>
        protected List<string> GetExampleNegativeSites()
        {
            return new List<string>
            {
                "contoso.com/negativesite"
            };
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
        /// Gets an example CallAdExtension. 
        /// </summary>
        protected AppAdExtension GetExampleAppAdExtension()
        {
            return new AppAdExtension
            {
                AppPlatform = "Windows",
                AppStoreId = "AppStoreIdGoesHere",
                DevicePreference = 0,
                DisplayText = "Contoso",
                Id = null,
            };
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
        /// Gets an example CallAdExtension. 
        /// </summary>
        protected CallAdExtension GetExampleCallAdExtension()
        {
            return new CallAdExtension
            {
                CountryCode = "US",
                PhoneNumber = "2065550100",
                IsCallOnly = false,
                Id = null
            };
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
        /// Gets an example ImageAdExtension. 
        /// </summary>
        protected ImageAdExtension GetExampleImageAdExtension(long imageMediaId)
        {
            return new ImageAdExtension
            {
                AlternativeText = "Image Alternative Text",
                ImageMediaIds = new[] { imageMediaId },
                Id = null
            };
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
        /// Gets an example LocationAdExtension. 
        /// </summary>
        protected LocationAdExtension GetExampleLocationAdExtension()
        {
            return new LocationAdExtension
            {
                Id = null,
                PhoneNumber = "206-555-0100",
                CompanyName = "Contoso Shoes",
                IconMediaId = null,
                ImageMediaId = null,
                Address = new Address
                {
                    StreetAddress = "1234 Washington Place",
                    StreetAddress2 = "Suite 1210",
                    CityName = "Woodinville",
                    ProvinceName = "WA",
                    CountryCode = "US",
                    PostalCode = "98608"
                }
            };
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
        /// Gets an example SiteLinksAdExtension. 
        /// </summary>
        protected SiteLinksAdExtension GetExampleSiteLinksAdExtension()
        {
            return new SiteLinksAdExtension
            {
                Id = null,
                SiteLinks = new List<SiteLink>
                {
                    new SiteLink
                    {
                        DisplayText = "Women's Shoe Sale 1",

                        // If you are currently using the Destination URL, you must upgrade to Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // With FinalUrls you can separate the tracking template, custom parameters, and 
                        // landing page URLs. 
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                        // Final Mobile URLs can also be used if you want to direct the user to a different page 
                        // for mobile devices.
                        FinalMobileUrls = new[] {
                            "http://mobile.contoso.com/womenshoesale"
                        }, 
                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this sitelink, 
                        // and can be used by the sitelink, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
                        UrlCustomParameters = new CustomParameters {
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO1"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                new SiteLink
                    {
                        DisplayText = "Women's Shoe Sale 2",

                        // If you are currently using the Destination URL, you must upgrade to Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // With FinalUrls you can separate the tracking template, custom parameters, and 
                        // landing page URLs. 
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                        // Final Mobile URLs can also be used if you want to direct the user to a different page 
                        // for mobile devices.
                        FinalMobileUrls = new[] {
                            "http://mobile.contoso.com/womenshoesale"
                        }, 
                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this sitelink, 
                        // and can be used by the sitelink, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
                        UrlCustomParameters = new CustomParameters {
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO2"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    }
                }
            };
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
    }
}
