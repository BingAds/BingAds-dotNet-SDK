using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.AdIntelligence;
using Microsoft.BingAds;


namespace BingAdsExamples.V9
{
    /// <summary>
    /// This example demonstrates how to get the minimum suggested bid value of one or more keywords 
    /// that would have resulted in an ad appearing in the targeted position in the search results.
    /// </summary>
    public class EstimatedBid : ExampleBase
    {
        public static ServiceClient<IAdIntelligenceService> Service;

        public override string Description
        {
            get { return "Estimated Bid | Ad Intelligence V9 (Deprecated)"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<IAdIntelligenceService>(authorizationData);
                // Set the Currency, Keywords, Language, PublisherCountries, and TargetPositionForAds
                // for the estimated bid by keywords request. 

                const Currency currency = Currency.USDollar;

                var keywordAndMatchTypes = new[]
                    {
                        new KeywordAndMatchType
                            {
                                KeywordText = "flower",
                                MatchTypes = new[]
                                    {
                                        MatchType.Broad,
                                        MatchType.Exact,
                                        MatchType.Phrase
                                    }
                            },
                        new KeywordAndMatchType
                            {
                                KeywordText = "delivery",
                                MatchTypes = new[]
                                    {
                                        MatchType.Broad,
                                        MatchType.Exact,
                                        MatchType.Phrase
                                    }
                            }
                    };

                const string language = "English";

                var publisherCountries = new[]
                    {
                        "US"
                    };

                const TargetAdPosition targetPositionForAds = TargetAdPosition.SideBar;

                // GetKeywordEstimatedBidByKeywords helper method calls the corresponding Bing Ads service operation 
                // to request the KeywordEstimatedBids.
                IEnumerable<KeywordEstimatedBid> keywordEstimatedBids = await GetKeywordEstimatedBidByKeywordsAsync(
                    currency,
                    keywordAndMatchTypes,
                    language,
                    publisherCountries,
                    targetPositionForAds
                    );

                // GetAdGroupEstimatedBidByKeywords helper method calls the corresponding Bing Ads service operation 
                // to request the AdGroupEstimatedBid.
                AdGroupEstimatedBid adGroupEstimatedBid = await GetAdGroupEstimatedBidByKeywordsAsync(
                    currency,
                    keywordAndMatchTypes,
                    language,
                    publisherCountries,
                    targetPositionForAds
                    );

                // Print the KeywordEstimatedBids

                if (keywordEstimatedBids != null)
                {

                    OutputStatusMessage("KeywordEstimatedBids");

                    foreach (KeywordEstimatedBid bid in keywordEstimatedBids)
                    {
                        if (bid == null)
                        {
                            OutputStatusMessage("The keyword is not valid.");
                        }
                        else
                        {
                            OutputStatusMessage(bid.Keyword);

                            if (bid.EstimatedBids.Count == 0)
                            {
                                OutputStatusMessage("  There is no bid information available for the keyword.\n");
                            }
                            else
                            {
                                foreach (EstimatedBidAndTraffic estimatedBidAndTraffic in bid.EstimatedBids)
                                {
                                    OutputStatusMessage("  " + estimatedBidAndTraffic.MatchType);
                                    OutputStatusMessage(String.Format("    Estimated Minimum Bid: {0:c}", estimatedBidAndTraffic.EstimatedMinBid));
                                    OutputStatusMessage("    Average CPC: " + estimatedBidAndTraffic.AverageCPC);
                                    OutputStatusMessage(String.Format("    Estimated clicks per week: {0} to {1}",
                                                      estimatedBidAndTraffic.MinClicksPerWeek, estimatedBidAndTraffic.MaxClicksPerWeek));
                                    OutputStatusMessage(String.Format("    Estimated impressions per week: {0} to {1}",
                                                      estimatedBidAndTraffic.MinImpressionsPerWeek,
                                                      estimatedBidAndTraffic.MaxImpressionsPerWeek));
                                    OutputStatusMessage(String.Format("    Estimated cost per week: {0} to {1}",
                                                      estimatedBidAndTraffic.MinTotalCostPerWeek, estimatedBidAndTraffic.MaxTotalCostPerWeek));
                                }
                            }
                        }
                    }
                }

                // Print the AdGroupEstimatedBid

                OutputStatusMessage("AdGroupEstimatedBid");

                OutputStatusMessage("  Average CPC: " + adGroupEstimatedBid.AverageCPC);
                OutputStatusMessage("  CTR: " + adGroupEstimatedBid.CTR);
                OutputStatusMessage("  Estimated Ad Group Bid: " + adGroupEstimatedBid.EstimatedAdGroupBid);
                OutputStatusMessage(String.Format("  Estimated clicks per week: {0} to {1}",
                                  adGroupEstimatedBid.MinClicksPerWeek, adGroupEstimatedBid.MaxClicksPerWeek));
                OutputStatusMessage(String.Format("  Estimated impressions per week: {0} to {1}",
                                  adGroupEstimatedBid.MinImpressionsPerWeek,
                                  adGroupEstimatedBid.MaxImpressionsPerWeek));
                OutputStatusMessage(String.Format("  Estimated cost per week: {0} to {1}",
                                  adGroupEstimatedBid.MinTotalCostPerWeek, adGroupEstimatedBid.MaxTotalCostPerWeek));
               
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Ad Intelligence service exceptions
            catch (FaultException<Microsoft.BingAds.AdIntelligence.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.AdIntelligence.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        // Get one or more keyword estimated bids corresponding to placement of your ad in the targeted position. 

        private async Task<IEnumerable<KeywordEstimatedBid>> GetKeywordEstimatedBidByKeywordsAsync(Currency currency, KeywordAndMatchType[] keywordAndMatchTypes,
            string language, string[] publisherCountries, TargetAdPosition targetPositionForAds)
        {
            var request = new GetEstimatedBidByKeywordsRequest
            {
                Currency = currency,
                GetBidsAtLevel = 0,  // Set GetBidsAtLevel to 0 to get a list of KeywordEstimatedBid.
                Keywords = keywordAndMatchTypes,
                Language = language,
                PublisherCountries = publisherCountries,
                TargetPositionForAds = targetPositionForAds
            };

            return (await Service.CallAsync((s, r) => s.GetEstimatedBidByKeywordsAsync(r), request)).KeywordEstimatedBids;
        }

        // Get one or more ad group estimated bids corresponding to placement of your ad in the targeted position. 

        private async Task<AdGroupEstimatedBid> GetAdGroupEstimatedBidByKeywordsAsync(Currency currency, IList<KeywordAndMatchType> keywordAndMatchTypes,
            string language, IList<string> publisherCountries, TargetAdPosition targetPositionForAds)
        {
            var request = new GetEstimatedBidByKeywordsRequest
            {
                Currency = currency,
                GetBidsAtLevel = 2,  // Set GetBidsAtLevel to 2 to get one AdGroupEstimatedBid.
                Keywords = keywordAndMatchTypes,
                Language = language,
                PublisherCountries = publisherCountries,
                TargetPositionForAds = targetPositionForAds
            };

            return (await Service.CallAsync((s, r) => s.GetEstimatedBidByKeywordsAsync(r), request)).AdGroupEstimatedBid;
        }
    }

}
