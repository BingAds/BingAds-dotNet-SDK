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
    /// This example demonstrates how to add ads and keywords to a new ad group, 
    /// and handle partial errors when some ads or keywords are not successfully created.
    /// </summary>
    public class Labels : ExampleBase
    {
        public override string Description
        {
            get { return "Labels | Campaign Management V11"; }
        }
        
        protected const int MaxGetLabelsByIds = 1000;
        protected const int MaxLabelIdsForGetLabelAssociations = 1;
        protected const int MaxEntityIdsForGetLabelAssociations = 100;
        protected const int MaxPagingSize = 1000;
        
        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);
                
                // Specify one or more campaigns.

                var campaigns = new[]{
                    new Campaign
                    {
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        DailyBudget = 50,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
                };

                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Women's Red Shoe Sale",
                        AdDistribution = AdDistribution.Search,
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        SearchBid = new Bid { Amount = 0.09 },
                        Language = "English",
                    }
                };
                
                var keywords = new[] 
                {
                    new Keyword
                    {
                        Bid = new Bid { Amount = 0.47 },
                        Param2 = "10% Off",
                        MatchType = MatchType.Phrase,
                        Text = "Brand-A Shoes",
                    },
                };
                
                var ads = new Ad[] {
                    new ExpandedTextAd
                    {
                        TitlePart1 = "Contoso",
                        TitlePart2 = "Fast & Easy Setup",
                        Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                        Path1 = "seattle",
                        Path2 = "shoe sale",
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                    },
                };
                
                var random = new Random();
                var labels = new List<Label>();

                for(var labelIndex = 0; labelIndex < 50; labelIndex++)
                {
                    var color = string.Format("#{0:X6}", random.Next(0x100000));
                    labels.Add(new Label
                    {
                        ColorCode = color,
                        Description = "Label Description",
                        Name = "Label Name " + color + " " + DateTime.UtcNow
                    });
                }
                                
                AddLabelsResponse addLabelsResponse = await AddLabelsAsync(labels);
                long?[] nullableLabelIds = addLabelsResponse.LabelIds.ToArray();
                BatchError[] labelErrors = addLabelsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Label Ids:");
                OutputIds(nullableLabelIds);
                OutputPartialErrors(labelErrors);

                AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] nullableCampaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Campaign Ids:");
                OutputIds(nullableCampaignIds);
                OutputPartialErrors(campaignErrors);

                AddAdGroupsResponse addAdGroupsResponse = await AddAdGroupsAsync((long)nullableCampaignIds[0], adGroups);
                long?[] nullableAdGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Ad Group Ids:");
                OutputIds(nullableAdGroupIds);
                OutputPartialErrors(adGroupErrors);

                AddKeywordsResponse addKeywordsResponse = await AddKeywordsAsync((long)nullableAdGroupIds[0], keywords);
                long?[] nullableKeywordIds = addKeywordsResponse.KeywordIds.ToArray();
                BatchError[] keywordErrors = addKeywordsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Keyword Ids:");
                OutputIds(nullableKeywordIds);
                OutputPartialErrors(keywordErrors);

                AddAdsResponse addAdsResponse = await AddAdsAsync((long)nullableAdGroupIds[0], ads);
                long?[] nullableAdIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Ad Ids:");
                OutputIds(nullableAdIds);
                OutputPartialErrors(adErrors);

                var labelIds = GetNonNullableIds(nullableLabelIds);
                
                OutputStatusMessage("\nGet all the labels that we added above...");

                var getLabelsByIdsResponse = await GetLabelsByIdsAsync(
                    labelIds,
                    new Paging
                    {
                        Index = 0,
                        Size = MaxGetLabelsByIds
                    }
                );
                OutputLabels(getLabelsByIdsResponse.Labels);

                OutputStatusMessage("\nUpdate the label color and then retrieve the labels again to confirm the changes....");
                
                var updateLabels = new List<Label>();
                foreach (var label in getLabelsByIdsResponse.Labels)
                {
                    label.ColorCode = string.Format("#{0:X6}", random.Next(0x100000));
                    updateLabels.Add(label);
                }
                var updateLabelsResponse = await UpdateLabelsAsync(updateLabels);

                getLabelsByIdsResponse = await GetLabelsByIdsAsync(
                    labelIds,
                    new Paging
                    {
                        Index = 0,
                        Size = MaxGetLabelsByIds
                    }
                );
                OutputLabels(getLabelsByIdsResponse.Labels);

                var campaignLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableCampaignIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with a campaign...");
                OutputLabelAssociations(campaignLabelAssociations);
                var setLabelAssociationsResponse = await SetLabelAssociationsAsync(campaignLabelAssociations, EntityType.Campaign);
                
                var adGroupLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableAdGroupIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with an ad group...");
                OutputLabelAssociations(adGroupLabelAssociations);
                setLabelAssociationsResponse = await SetLabelAssociationsAsync(adGroupLabelAssociations, EntityType.AdGroup);
                
                var keywordLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableKeywordIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with a keyword...");
                OutputLabelAssociations(keywordLabelAssociations);
                setLabelAssociationsResponse = await SetLabelAssociationsAsync(keywordLabelAssociations, EntityType.Keyword);
                
                var adLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableAdIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with an ad...");
                OutputLabelAssociations(adLabelAssociations);
                setLabelAssociationsResponse = await SetLabelAssociationsAsync(adLabelAssociations, EntityType.Ad);

                
                OutputStatusMessage("\nUse paging to get all campaign label associations...");
                var getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(labelIds, EntityType.Campaign);
                OutputLabelAssociations(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nUse paging to get all ad group label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(labelIds, EntityType.AdGroup);
                OutputLabelAssociations(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nUse paging to get all keyword label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(labelIds, EntityType.Keyword);
                OutputLabelAssociations(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nUse paging to get all ad label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(labelIds, EntityType.Ad);
                OutputLabelAssociations(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nGet all label associations for all specified campaigns...");
                var getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    GetNonNullableIds(nullableCampaignIds),
                    EntityType.Campaign
                );
                OutputLabelAssociations(getLabelAssociationsByEntityIds);
                
                OutputStatusMessage("\nGet all label associations for all specified ad groups...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    GetNonNullableIds(nullableAdGroupIds),
                    EntityType.AdGroup
                );
                OutputLabelAssociations(getLabelAssociationsByEntityIds);
                
                OutputStatusMessage("\nGet all label associations for all specified keywords...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    GetNonNullableIds(nullableKeywordIds),
                    EntityType.Keyword
                );
                OutputLabelAssociations(getLabelAssociationsByEntityIds);
                
                OutputStatusMessage("\nGet all label associations for all specified ads...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    GetNonNullableIds(nullableAdIds),
                    EntityType.Ad
                );
                OutputLabelAssociations(getLabelAssociationsByEntityIds);
                
                OutputStatusMessage("\nDelete all label associations that we set above....");

                // This is not necessary if you are deleting the corresponding campaign(s), as the 
                // contained ad groups, keywords, ads, and associations would also be deleted.

                var deleteLabelAssociationsResponse = await DeleteLabelAssociationsAsync(campaignLabelAssociations, EntityType.Campaign);
                deleteLabelAssociationsResponse = await DeleteLabelAssociationsAsync(adGroupLabelAssociations, EntityType.AdGroup);
                deleteLabelAssociationsResponse = await DeleteLabelAssociationsAsync(keywordLabelAssociations, EntityType.Keyword);
                deleteLabelAssociationsResponse = await DeleteLabelAssociationsAsync(adLabelAssociations, EntityType.Ad);
                
                OutputStatusMessage("\nDelete all labels that we added above....");

                // Deleting the campaign(s) removes the corresponding label associations but not remove the labels.

                var deleteLabelsResponse = await DeleteLabelsAsync(labelIds);
                
                OutputStatusMessage("\nDelete the campaign, ad group, keyword, and ad that were added above....");
                
                await DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)nullableCampaignIds[0] });
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

        private static List<LabelAssociation> CreateExampleLabelAssociationsByEntityId(long entityId, List<long> labelIds)
        {
            var labelAssociations = new List<LabelAssociation>();
            foreach (var labelId in labelIds)
            {
                labelAssociations.Add(
                    new LabelAssociation
                    {
                        EntityId = entityId,
                        LabelId = labelId
                    }
                );
            }
            return labelAssociations;
        }

        private async Task<List<LabelAssociation>> GetLabelAssociationsByLabelIdsHelperAsync(
            IList<long> labelIds,
            EntityType entityType
            )
        {
            var labelAssociations = new List<LabelAssociation>();
            var labelIdsPageIndex = 0;

            while(labelIdsPageIndex * MaxLabelIdsForGetLabelAssociations < labelIds.Count)
            {
                var getLabelIds = 
                    labelIds.Skip(labelIdsPageIndex++ * MaxLabelIdsForGetLabelAssociations).Take(MaxLabelIdsForGetLabelAssociations).ToList();

                var labelAssociationsPageIndex = 0;
                var foundLastPage = false;

                while (!foundLastPage)
                {
                    var getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsAsync(
                        getLabelIds,
                        entityType,
                        new Paging
                        {
                            Index = labelAssociationsPageIndex++,
                            Size = MaxPagingSize
                        }
                    ).ConfigureAwait(continueOnCapturedContext: false);

                    labelAssociations.AddRange(getLabelAssociationsByLabelIds.LabelAssociations);
                    foundLastPage = MaxPagingSize > getLabelAssociationsByLabelIds.LabelAssociations.Count;
                }
            }
            
            return labelAssociations;
        }

        private async Task<List<LabelAssociation>> GetLabelAssociationsByEntityIdsHelperAsync(
            IList<long> entityIds,
            EntityType entityType
            )
        {
            var labelAssociations = new List<LabelAssociation>();
            var entityIdsPageIndex = 0;

            while (entityIdsPageIndex * MaxEntityIdsForGetLabelAssociations < entityIds.Count)
            {
                var getEntityIds =
                    entityIds.Skip(entityIdsPageIndex++ * MaxEntityIdsForGetLabelAssociations).Take(MaxEntityIdsForGetLabelAssociations).ToList();

                var getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsAsync(
                    getEntityIds,
                    entityType
                ).ConfigureAwait(continueOnCapturedContext: false);

                labelAssociations.AddRange(getLabelAssociationsByEntityIds.LabelAssociations);
            }

            return labelAssociations;
        }
    }
}
