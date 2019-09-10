using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.AdInsight;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to get keyword ideas and traffic estimates for search advertising campaigns.
    /// </summary>
    public class KeywordPlanner : ExampleBase
    {
        public override string Description
        {
            get { return "Keyword Planner | AdInsight V13"; }
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

                // Use the GetKeywordIdeaCategories operation to get a list of valid category identifiers.
                // A category identifier will be used in the CategorySearchParameter below.

                OutputStatusMessage("-----\nGetKeywordIdeaCategories:");
                var getKeywordIdeaCategoriesResponse = await AdInsightExampleHelper.GetKeywordIdeaCategoriesAsync();
                var categoryId = (long)(getKeywordIdeaCategoriesResponse?.KeywordIdeaCategories?.ToList()[0].CategoryId);
                OutputStatusMessage(string.Format("CategoryId {0} will be used in the CategorySearchParameter below", categoryId));

                // You must specify the attributes that you want in each returned KeywordIdea.

                var ideaAttributes = new List<KeywordIdeaAttribute>
                {
                    KeywordIdeaAttribute.AdGroupId,
                    KeywordIdeaAttribute.AdGroupName,
                    KeywordIdeaAttribute.AdImpressionShare,
                    KeywordIdeaAttribute.Competition,
                    KeywordIdeaAttribute.Keyword,
                    KeywordIdeaAttribute.MonthlySearchCounts,
                    KeywordIdeaAttribute.Relevance,
                    KeywordIdeaAttribute.Source,
                    KeywordIdeaAttribute.SuggestedBid,
                };

                var endDateTime = DateTime.UtcNow.AddMonths(-2);
                                
                // Only one of each SearchParameter type can be specified per call. 

                var searchParameters = new List<SearchParameter>
                {
                    // Determines the start and end month for MonthlySearchCounts data returned with each KeywordIdea.
                    // The date range search parameter is optional. If you do not include the DateRangeSearchParameter 
                    // in the GetKeywordIdeas request, then you will not be able to confirm whether the first list item 
                    // within MonthlySearchCounts is data for the previous month, or the month prior. If the date range is 
                    // specified and the most recent month's data is not yet available, then GetKeywordIdeas will return an error.

                    new DateRangeSearchParameter
                    {
                        EndDate = new DayMonthAndYear
                        {
                            Day = 30,
                            Month = 9,
                            Year = 2018
                        },
                        StartDate = new DayMonthAndYear
                        {
                            Day = 1,
                            Month = 9,
                            Year = 2018
                        },
                    },
                    
                    // The CategorySearchParameter corresponds to filling in 'Your product category' under
                    // 'Search for new keywords using a phrase, website, or category' in the 
                    // Microsoft Advertising web application's Keyword Planner tool.
                    // One or more CategorySearchParameter, QuerySearchParameter, or UrlSearchParameter is required.

                    new CategorySearchParameter
                    {
                        // Use the GetKeywordIdeaCategories operation to get a list of valid category identifiers.
                        CategoryId = categoryId
                    },

                    // The QuerySearchParameter corresponds to filling in 'Product or service' under
                    // 'Search for new keywords using a phrase, website, or category' in the 
                    // Microsoft Advertising web application's Keyword Planner tool.
                    // One or more CategorySearchParameter, QuerySearchParameter, or UrlSearchParameter is required.
                    // When calling GetKeywordIdeas, if ExpandIdeas = false the QuerySearchParameter is required. 

                    new QuerySearchParameter
                    {
                        Queries = new List<string>
                        {
                            "tennis",
                            "tennis shoes",
                            "running",
                            "running shoes",
                            "cross training",
                            "running",
                        },
                    },

                    // The UrlSearchParameter corresponds to filling in 'Your landing page' under
                    // 'Search for new keywords using a phrase, website, or category' in the 
                    // Microsoft Advertising web application's Keyword Planner tool.
                    // One or more CategorySearchParameter, QuerySearchParameter, or UrlSearchParameter is required.

                    new UrlSearchParameter
                    {
                        Url = "contoso.com"
                    },
                    
                    // The LanguageSearchParameter, LocationSearchParameter, and NetworkSearchParameter
                    // correspond to the 'Keyword Planner' -> 'Search for new keywords using a phrase, website, or category' ->
                    // 'Targeting' workflow in the Microsoft Advertising web application.
                    // Each of these search parameters are required.

                    new LanguageSearchParameter
                    {
                        // You must specify exactly one language

                        Languages = new List<LanguageCriterion>
                        {
                            new LanguageCriterion
                            {
                                Language = "English",
                            },
                        },
                    },
                    new LocationSearchParameter
                    {
                        // You must include at least one location.

                        Locations = new List<LocationCriterion>
                        {
                            new LocationCriterion
                            {
                                // United States
                                LocationId = 190,
                            },
                        }
                    },
                    new NetworkSearchParameter
                    {
                        Network = new NetworkCriterion
                        {
                            Network = NetworkType.OwnedAndOperatedAndSyndicatedSearch,
                        }
                    },

                    // The CompetitionSearchParameter, ExcludeAccountKeywordsSearchParameter, IdeaTextSearchParameter, 
                    // ImpressionShareSearchParameter, SearchVolumeSearchParameter, and SuggestedBidSearchParameter  
                    // correspond to the 'Keyword Planner' -> 'Search for new keywords using a phrase, website, or category' -> 
                    // 'Search options' workflow in the Microsoft Advertising web application.
                    // Use these options to refine what keywords we suggest. You can limit the keywords by historical data, 
                    // hide keywords already in your account, and include or exclude specific keywords.
                    // Each of these search parameters are optional.

                    new CompetitionSearchParameter
                    {
                        CompetitionLevels = new List<CompetitionLevel>
                        {
                            CompetitionLevel.High,
                            CompetitionLevel.Medium,
                            CompetitionLevel.Low
                        }
                    },
                    new ExcludeAccountKeywordsSearchParameter
                    {
                        ExcludeAccountKeywords = false,
                    },
                    new IdeaTextSearchParameter
                    {
                        // The match type is required. Only Broad is supported.

                        Excluded = new List<Keyword>
                        {
                            new Keyword
                            {
                                Text = "tennis court",
                                MatchType = MatchType.Broad
                            },
                            new Keyword
                            {
                                Text = "tennis pro",
                                MatchType = MatchType.Broad
                            }
                        },
                        Included = new List<Keyword>
                        {
                            new Keyword
                            {
                                Text = "athletic clothing",
                                MatchType = MatchType.Broad
                            },
                            new Keyword
                            {
                                Text = "athletic shoes",
                                MatchType = MatchType.Broad
                            }
                        },
                    },
                    new ImpressionShareSearchParameter
                    {
                        // Equivalent of '0 <= value <= 50'
                        Maximum = 50,
                        Minimum = 0,
                    },
                    new SearchVolumeSearchParameter
                    {
                        // Equivalent of 'value >= 50'
                        Maximum = null,
                        Minimum = 50,
                    },
                    new SuggestedBidSearchParameter
                    {
                        // Equivalent of both 'value <= 50' and '0 <= value <= 50'
                        Maximum = 50,
                        Minimum = null,
                    },

                    // Setting the device criterion is not available in the 
                    // 'Keyword Planner' -> 'Search for new keywords using a phrase, website, or category'
                    // workflow in the Microsoft Advertising web application.
                    // The DeviceSearchParameter is optional and by default the keyword ideas data
                    // are aggregated for all devices.

                    new DeviceSearchParameter
                    {
                        Device = new DeviceCriterion
                        {
                            // Possible values are All, Computers, Tablets, Smartphones
                            DeviceName = "All",
                        },
                    },
                };

                // If ExpandIdeas is false, the QuerySearchParameter is required.

                OutputStatusMessage("-----\nGetKeywordIdeas:");
                var getKeywordIdeasResponse = await AdInsightExampleHelper.GetKeywordIdeasAsync(
                    expandIdeas: true,
                    ideaAttributes: ideaAttributes,
                    searchParameters: searchParameters);

                var keywordIdeas = getKeywordIdeasResponse?.KeywordIdeas;
                if(keywordIdeas == null || keywordIdeas.Count < 1)
                {
                    OutputStatusMessage("No keyword ideas are available for the search parameters.");
                    return;
                }
                OutputStatusMessage("KeywordIdeas:");
                AdInsightExampleHelper.OutputArrayOfKeywordIdea(keywordIdeas);

                // Let's get traffic estimates for each returned keyword idea.

                // The returned ad group ID within each keyword idea will either be null or negative.
                // Negative identifiers can be used to map the keyword ideas into suggested new ad groups. 
                // A null ad group identifier indicates that the keyword idea was sourced from your 
                // keyword idea search parameter.

                // In this example we will use the suggested ad groups to request traffic estimates.
                // Each of the seed keyword ideas will be submitted in the same ad group.

                var adGroupIds = keywordIdeas.Select(idea => idea.AdGroupId).Distinct().ToList();
                var adGroupEstimatorCount = adGroupIds.Count;
                var seedOffset = adGroupIds.Contains(null) ? 0 : 1;
                
                var adGroupEstimators = new AdGroupEstimator[adGroupEstimatorCount];
                for(int index = 0; index < adGroupEstimatorCount; index++)
                {
                    adGroupEstimators[index] = new AdGroupEstimator
                    {
                        // The AdGroupId is reserved for future use.
                        // The traffic estimates are not based on any specific ad group. 
                        AdGroupId = null,

                        // We will add new keyword estimators while iterating the keyword ideas below.
                        KeywordEstimators = new List<KeywordEstimator>(),

                        // Optionally you can set an ad group level max CPC (maximum search bid)
                        MaxCpc = 5.00
                    };
                }

                foreach(var keywordIdea in keywordIdeas)
                {
                    var keywordEstimator = new KeywordEstimator
                    {
                        Keyword = new Keyword
                        {
                            // The keyword Id is reserved for future use.
                            // The returned estimates are not based on any specific keyword.
                            Id = null,
                            
                            // The match type is required. Exact, Broad, and Phrase are supported.
                            MatchType = MatchType.Exact,

                            // Use the suggested keyword
                            Text = keywordIdea.Keyword
                        },

                        // Round the suggested bid to two decimal places
                        MaxCpc = keywordIdea.SuggestedBid > 0.04 ? keywordIdea.SuggestedBid : null,
                    };

                    var index = keywordIdea.AdGroupId != null ? -(long)keywordIdea.AdGroupId - seedOffset : 0;

                    adGroupEstimators[index].KeywordEstimators.Add(keywordEstimator);
                }

                // Currently you can include only one CampaignEstimator per service call.

                var campaigns = new List<CampaignEstimator>
                {
                    new CampaignEstimator
                    {
                        // Let's use the ad group and keyword estimators that were sourced from keyword ideas above.

                        AdGroupEstimators = adGroupEstimators,

                        // The CampaignId is reserved for future use.
                        // The returned estimates are not based on any specific campaign.

                        CampaignId = null,

                        DailyBudget = 50.00,

                        NegativeKeywords = new List<NegativeKeyword>
                        {
                            new NegativeKeyword
                            {
                                Text = "foo",
                                MatchType = MatchType.Exact,
                            },
                        },

                        // The location, language, and network criterions are required for traffic estimates.

                        Criteria = new List<Criterion>
                        {
                            // You must include at least one location.

                            new LocationCriterion
                            {
                                // United States
                                LocationId = 190
                            },

                            // You must specify exactly one language criterion

                            new LanguageCriterion
                            {
                                Language = "English"
                            },

                            // You must specify exactly one network criterion

                            new NetworkCriterion
                            {
                                Network = NetworkType.OwnedAndOperatedAndSyndicatedSearch
                            },

                            // Optionally you can specify exactly one device.
                            // If you do not specify a device, the returned traffic estimates 
                            // are aggregated for all devices.
                            // The "All" device name is equivalent to omitting the DeviceCriterion.

                            new DeviceCriterion
                            {
                                DeviceName = "All"
                            },
                        },
                    },
                };

                OutputStatusMessage("-----\nGetKeywordTrafficEstimates:");
                var getKeywordTrafficEstimatesResponse = await AdInsightExampleHelper.GetKeywordTrafficEstimatesAsync(
                    campaignEstimators: campaigns);
                OutputStatusMessage("CampaignEstimates:");
                AdInsightExampleHelper.OutputArrayOfCampaignEstimate(getKeywordTrafficEstimatesResponse?.CampaignEstimates);
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch AdInsight service exceptions
            catch (FaultException<AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<ApiFaultDetail> ex)
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
