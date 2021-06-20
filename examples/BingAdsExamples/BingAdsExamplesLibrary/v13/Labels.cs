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
    /// How to add labels and associate them with
    /// campaigns, ad groups, keywords, and ads.
    /// </summary>
    public class Labels : ExampleBase
    {
        public override string Description
        {
            get { return "Labels | Campaign Management V13"; }
        }

        protected const int MaxGetLabelsByIds = 1000;
        protected const int MaxLabelIdsForGetLabelAssociations = 1;
        protected const int MaxEntityIdsForGetLabelAssociations = 100;
        protected const int MaxPagingSize = 1000;

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

                // Add an ad group in a campaign. Later we will create labels for them. 
                // Although not included in this example you can also create labels for ads and keywords. 

                var campaigns = new[]{
                    new Campaign
                    {
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        CampaignType = CampaignType.Search,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    },
                };

                OutputStatusMessage("-----\nAddCampaigns:");
                AddCampaignsResponse addCampaignsResponse = await CampaignManagementExampleHelper.AddCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaigns: campaigns);
                long?[] campaignIds = addCampaignsResponse.CampaignIds.ToArray();
                BatchError[] campaignErrors = addCampaignsResponse.PartialErrors.ToArray();
                OutputStatusMessage("CampaignIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(campaignIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(campaignErrors);

                var adGroups = new[] {
                    new AdGroup
                    {
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Date {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                    }
                };

                OutputStatusMessage("-----\nAddAdGroups:");
                AddAdGroupsResponse addAdGroupsResponse = await CampaignManagementExampleHelper.AddAdGroupsAsync(
                    campaignId: (long)campaignIds[0],
                    adGroups: adGroups,
                    returnInheritedBidStrategyTypes: false);
                long?[] adGroupIds = addAdGroupsResponse.AdGroupIds.ToArray();
                BatchError[] adGroupErrors = addAdGroupsResponse.PartialErrors.ToArray();
                OutputStatusMessage("AdGroupIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(adGroupIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(adGroupErrors);
                
                // Add labels and associate them with the campaign and ad group.

                var random = new Random();
                var labels = new List<Label>();

                for (var labelIndex = 0; labelIndex < 5; labelIndex++)
                {
                    var color = string.Format("#{0:X6}", random.Next(0x100000));
                    labels.Add(new Label
                    {
                        ColorCode = color,
                        Description = "Label Description",
                        Name = "Label Name " + color + " " + DateTime.UtcNow
                    });
                }

                OutputStatusMessage("-----\nAddLabels:");
                AddLabelsResponse addLabelsResponse = await CampaignManagementExampleHelper.AddLabelsAsync(labels);
                long?[] nullableLabelIds = addLabelsResponse.LabelIds.ToArray();
                BatchError[] labelErrors = addLabelsResponse.PartialErrors.ToArray();
                OutputStatusMessage("LabelIds:");
                CampaignManagementExampleHelper.OutputArrayOfLong(nullableLabelIds);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(labelErrors);
                
                var labelIds = GetNonNullableIds(nullableLabelIds);

                OutputStatusMessage("-----\nGetLabelsByIds:");
                var getLabelsByIdsResponse = await CampaignManagementExampleHelper.GetLabelsByIdsAsync(
                    labelIds: labelIds,
                    pageInfo: new Paging
                    {
                        Index = 0,
                        Size = MaxGetLabelsByIds
                    }
                );
                var getLabels = getLabelsByIdsResponse.Labels;
                labelErrors = getLabelsByIdsResponse.PartialErrors.ToArray();
                OutputStatusMessage("Labels:");
                CampaignManagementExampleHelper.OutputArrayOfLabel(getLabels);
                OutputStatusMessage("PartialErrors:");
                CampaignManagementExampleHelper.OutputArrayOfBatchError(labelErrors);
                
                var campaignLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)campaignIds[0], labelIds);
                OutputStatusMessage("-----\nAssociating all of the labels with a campaign...");
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(campaignLabelAssociations);
                var setLabelAssociationsResponse = await CampaignManagementExampleHelper.SetLabelAssociationsAsync(
                    entityType: EntityType.Campaign, 
                    labelAssociations: campaignLabelAssociations);
                
                var adGroupLabelAssociations = CreateExampleLabelAssociationsByEntityId((long)adGroupIds[0], labelIds);
                OutputStatusMessage("-----\nAssociating all of the labels with an ad group...");
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(adGroupLabelAssociations);
                setLabelAssociationsResponse = await CampaignManagementExampleHelper.SetLabelAssociationsAsync(
                    entityType: EntityType.AdGroup,
                    labelAssociations: adGroupLabelAssociations);
                
                OutputStatusMessage("-----\nUse paging to get all campaign label associations...");
                var getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(
                    CampaignManagementExampleHelper,
                    entityType: EntityType.Campaign,
                    labelIds: labelIds);
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByLabelIds);

                OutputStatusMessage("-----\nUse paging to get all ad group label associations...");
                getLabelAssociationsByLabelIds = await GetLabelAssociationsByLabelIdsHelperAsync(
                    CampaignManagementExampleHelper: CampaignManagementExampleHelper,
                    entityType: EntityType.AdGroup,
                    labelIds: labelIds);
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByLabelIds);
                
                OutputStatusMessage("-----\nGet label associations for the campaigns...");
                var getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    CampaignManagementExampleHelper: CampaignManagementExampleHelper,
                    entityType: EntityType.Campaign,
                    entityIds: GetNonNullableIds(campaignIds)
                );
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByEntityIds);

                OutputStatusMessage("-----\nGet label associations for the ad groups...");
                getLabelAssociationsByEntityIds = await GetLabelAssociationsByEntityIdsHelperAsync(
                    CampaignManagementExampleHelper: CampaignManagementExampleHelper,
                    entityType: EntityType.AdGroup,
                    entityIds: GetNonNullableIds(adGroupIds)
                );
                CampaignManagementExampleHelper.OutputArrayOfLabelAssociation(getLabelAssociationsByEntityIds);
                
                OutputStatusMessage("-----\nDelete all label associations that we set above...");

                // Deleting the associations is not necessary if you are deleting the corresponding campaign(s), as the 
                // contained ad groups, ads, and associations would also be deleted.

                var deleteLabelAssociationsResponse = await CampaignManagementExampleHelper.DeleteLabelAssociationsAsync(
                    entityType: EntityType.Campaign,
                    labelAssociations: campaignLabelAssociations);
                deleteLabelAssociationsResponse = await CampaignManagementExampleHelper.DeleteLabelAssociationsAsync(
                    entityType: EntityType.AdGroup,
                    labelAssociations: adGroupLabelAssociations);
                
                // Delete the account's labels. 

                OutputStatusMessage("-----\nDeleteLabels:");
                var deleteLabelsResponse = await CampaignManagementExampleHelper.DeleteLabelsAsync(
                    labelIds: labelIds);

                foreach (var id in labelIds)
                {
                    OutputStatusMessage(string.Format("Deleted Label Id {0}", id));
                }

                // Delete the campaign and everything it contains e.g., ad groups and ads.

                OutputStatusMessage("-----\nDeleteCampaigns:");
                await CampaignManagementExampleHelper.DeleteCampaignsAsync(
                    accountId: authorizationData.AccountId,
                    campaignIds: new[] { (long)campaignIds[0] });
                OutputStatusMessage(string.Format("Deleted Campaign Id {0}", campaignIds[0]));
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
                        entityType: entityType,
                        labelIds: getLabelIds,
                        pageInfo: new Paging
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
                    entityIds: getEntityIds,
                    entityType: entityType
                ).ConfigureAwait(continueOnCapturedContext: false);

                labelAssociations.AddRange(getLabelAssociationsByEntityIds.LabelAssociations);
            }

            return labelAssociations;
        }
    }
}
