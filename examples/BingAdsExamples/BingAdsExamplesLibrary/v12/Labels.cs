using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// This example demonstrates how to add labels and associate them with
    /// campaigns, ad groups, keywords, and ads.
    /// </summary>
    public class Labels : ExampleBase
    {
        public override string Description
        {
            get { return "Labels | Campaign Management V12"; }
        }

        protected const int MaxGetLabelsByIds = 1000;
        protected const int MaxLabelIdsForGetLabelAssociations = 1;
        protected const int MaxEntityIdsForGetLabelAssociations = 100;
        protected const int MaxPagingSize = 1000;

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);
                CampaignManagementExampleHelper.CampaignManagementService = new ServiceClient<ICampaignManagementService>(authorizationData);

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
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V12.CampaignManagement.Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
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

                for (var labelIndex = 0; labelIndex < 50; labelIndex++)
                {
                    var color = string.Format("#{0:X6}", random.Next(0x100000));
                    labels.Add(new Label
                    {
                        ColorCode = color,
                        Description = "Label Description",
                        Name = "Label Name " + color + " " + DateTime.UtcNow
                    });
                }

                AddLabelsResponse addLabelsResponse = await CampaignManagementExampleHelper.AddLabelsAsync(labels);
                long?[] nullableLabelIds = addLabelsResponse.LabelIds.ToArray();
                BatchError[] labelErrors = addLabelsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Label Ids:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableLabelIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(labelErrors);

                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(authorizationData.AccountId, campaigns);
                long?[] nullableCampaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Campaign Ids:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableCampaignIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync((long)nullableCampaignIds[0], adGroups, null);
                long?[] nullableAdGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Ad Group Ids:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableAdGroupIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);

                AddKeywordsResponse addKeywordsResponse = await CampaignManagementExampleHelper.AddKeywordsAsync((long)nullableAdGroupIds[0], keywords, null);
                long?[] nullableKeywordIds = addKeywordsResponse.KeywordIds.ToArray();
                BatchError[] keywordErrors = addKeywordsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Keyword Ids:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableKeywordIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(keywordErrors);

                AddAdsResponse addAdsResponse = await CampaignManagementExampleHelper.AddAdsAsync((long)nullableAdGroupIds[0], ads);
                long?[] nullableAdIds = addAdsResponse.AdIds.ToArray();
                BatchError[] adErrors = addAdsResponse.PartialErrors.ToArray();
                OutputStatusMessage("New Ad Ids:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableAdIds);
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adErrors);

                var labelIds = GetNonNullableIds(nullableLabelIds);

                OutputStatusMessage("\nGet all the labels that we added above...");

                var getLabelsByIdsResponse = await CampaignManagementExampleHelper.GetLabelsByIdsAsync(
                    labelIds,
                    new Paging
                    {
                        Index = 0,
                        Size = MaxGetLabelsByIds
                    }
                );
                CampaignManagementExampleHelper.OutputArrayOfLabel(getLabelsByIdsResponse.Labels);

                OutputStatusMessage("\nUpdate the label color and then retrieve the labels again to confirm the changes....");

                var updateLabels = new List<Label>();
                foreach (var label in getLabelsByIdsResponse.Labels)
                {
                    label.ColorCode = string.Format("#{0:X6}", random.Next(0x100000));
                    updateLabels.Add(label);
                }
                var updateLabelsResponse = await CampaignManagementExampleHelper.UpdateLabelsAsync(updateLabels);

                getLabelsByIdsResponse = await CampaignManagementExampleHelper.GetLabelsByIdsAsync(
                    labelIds,
                    new Paging
                    {
                        Index = 0,
                        Size = MaxGetLabelsByIds
                    }
                );
                CampaignManagementExampleHelper.OutputArrayOfLabel(getLabelsByIdsResponse.Labels);

                var campaignLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableCampaignIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with a campaign...");
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(campaignLabelAssociations);
                var setLabelAssociationsResponse = await CampaignManagementExampleHelper.SetLabelAssociationsAsync(EntityType.Campaign, campaignLabelAssociations);

                var adGroupLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableAdGroupIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with an ad group...");
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(adGroupLabelAssociations);
                setLabelAssociationsResponse = await CampaignManagementExampleHelper.SetLabelAssociationsAsync(EntityType.AdGroup, adGroupLabelAssociations);

                var keywordLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableKeywordIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with a keyword...");
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(keywordLabelAssociations);
                setLabelAssociationsResponse = await CampaignManagementExampleHelper.SetLabelAssociationsAsync(EntityType.Keyword, keywordLabelAssociations);

                var adLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)nullableAdIds[0], labelIds);
                OutputStatusMessage("\nAssociating all of the labels with an ad...");
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(adLabelAssociations);
                setLabelAssociationsResponse = await CampaignManagementExampleHelper.SetLabelAssociationsAsync(EntityType.Ad, adLabelAssociations);


                OutputStatusMessage("\nUse paging to get all campaign label associations...");
                var getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.Campaign,
                    labelIds);
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nUse paging to get all ad group label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.AdGroup,
                    labelIds);
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nUse paging to get all keyword label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.Keyword,
                    labelIds);
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nUse paging to get all ad label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.Ad,
                    labelIds);
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByLabelIds);

                OutputStatusMessage("\nGet all label associations for all specified campaigns...");
                var getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.Campaign,
                    GetNonNullableIds(nullableCampaignIds)
                );
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByEntityIds);

                OutputStatusMessage("\nGet all label associations for all specified ad groups...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.AdGroup,
                    GetNonNullableIds(nullableAdGroupIds)
                );
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByEntityIds);

                OutputStatusMessage("\nGet all label associations for all specified keywords...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.Keyword,
                    GetNonNullableIds(nullableKeywordIds)
                );
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByEntityIds);

                OutputStatusMessage("\nGet all label associations for all specified ads...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    EntityType.Ad,
                    GetNonNullableIds(nullableAdIds)
                );
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByEntityIds);

                OutputStatusMessage("\nDelete all label associations that we set above....");

                // This is not necessary if you are deleting the corresponding campaign(s), as the 
                // contained ad groups, keywords, ads, and associations would also be deleted.

                var deleteLabelAssociationsResponse = await CampaignManagementExampleHelper.DeleteLabelAssociationsAsync(EntityType.Campaign, campaignLabelAssociations);
                deleteLabelAssociationsResponse = await CampaignManagementExampleHelper.DeleteLabelAssociationsAsync(EntityType.AdGroup, adGroupLabelAssociations);
                deleteLabelAssociationsResponse = await CampaignManagementExampleHelper.DeleteLabelAssociationsAsync(EntityType.Keyword, keywordLabelAssociations);
                deleteLabelAssociationsResponse = await CampaignManagementExampleHelper.DeleteLabelAssociationsAsync(EntityType.Ad, adLabelAssociations);

                OutputStatusMessage("\nDelete all labels that we added above....");

                // Deleting the campaign(s) removes the corresponding label associations but not remove the labels.

                var deleteLabelsResponse = await CampaignManagementExampleHelper.DeleteLabelsAsync(labelIds);

                OutputStatusMessage("\nDelete the campaign, ad group, keyword, and ad that were added above....");

                await CampaignManagementExampleHelper.DeleteCampaignsAsync(authorizationData.AccountId, new[] { (long)nullableCampaignIds[0] });
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V12.CampaignManagement.EditorialApiFaultDetail> ex)
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
            CampaignManagementExampleHelper CampaignManagementExampleHelper,
            EntityType entityType,
            IList<long> labelIds
            )
        {
            var labelAssociations = new List<LabelAssociation>();
            var labelIdsPageIndex = 0;

            while (labelIdsPageIndex * MaxLabelIdsForGetLabelAssociations < labelIds.Count)
            {
                var getLabelIds =
                    labelIds.Skip(labelIdsPageIndex++ * MaxLabelIdsForGetLabelAssociations).Take(MaxLabelIdsForGetLabelAssociations).ToList();

                var labelAssociationsPageIndex = 0;
                var foundLastPage = false;

                while (!foundLastPage)
                {
                    var getLabelAssociationsByLabelIds = await CampaignManagementExampleHelper.GetLabelAssociationsByLabelIdsAsync(
                        entityType,
                        getLabelIds,
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
            CampaignManagementExampleHelper CampaignManagementExampleHelper,
            EntityType entityType,
            IList<long> entityIds
            )
        {
            var labelAssociations = new List<LabelAssociation>();
            var entityIdsPageIndex = 0;

            while (entityIdsPageIndex * MaxEntityIdsForGetLabelAssociations < entityIds.Count)
            {
                var getEntityIds =
                    entityIds.Skip(entityIdsPageIndex++ * MaxEntityIdsForGetLabelAssociations).Take(MaxEntityIdsForGetLabelAssociations).ToList();

                var getLabelAssociationsByEntityIds = await CampaignManagementExampleHelper.GetLabelAssociationsByEntityIdsAsync(
                    getEntityIds,
                    entityType
                ).ConfigureAwait(continueOnCapturedContext: false);

                labelAssociations.AddRange(getLabelAssociationsByEntityIds.LabelAssociations);
            }

            return labelAssociations;
        }
    }
}
