using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    public class CampaignManagementExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | CampaignManagement V13"; }
        }
        public ServiceClient<ICampaignManagementService> CampaignManagementService;
        public CampaignManagementExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }
        public async Task<AddAdExtensionsResponse> AddAdExtensionsAsync(
            long accountId,
            IList<AdExtension> adExtensions)
        {
            var request = new AddAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddAdExtensionsAsync(r), request));
        }
        public async Task<AddAdGroupCriterionsResponse> AddAdGroupCriterionsAsync(
            IList<AdGroupCriterion> adGroupCriterions,
            AdGroupCriterionType criterionType)
        {
            var request = new AddAdGroupCriterionsRequest
            {
                AdGroupCriterions = adGroupCriterions,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddAdGroupCriterionsAsync(r), request));
        }
        public async Task<AddAdGroupsResponse> AddAdGroupsAsync(
            long campaignId,
            IList<AdGroup> adGroups,
            bool? returnInheritedBidStrategyTypes)
        {
            var request = new AddAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups,
                ReturnInheritedBidStrategyTypes = returnInheritedBidStrategyTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddAdGroupsAsync(r), request));
        }
        public async Task<AddAdsResponse> AddAdsAsync(
            long adGroupId,
            IList<Ad> ads)
        {
            var request = new AddAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddAdsAsync(r), request));
        }
        public async Task<AddAudiencesResponse> AddAudiencesAsync(
            IList<Audience> audiences)
        {
            var request = new AddAudiencesRequest
            {
                Audiences = audiences
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddAudiencesAsync(r), request));
        }
        public async Task<AddBidStrategiesResponse> AddBidStrategiesAsync(
            IList<BidStrategy> bidStrategies)
        {
            var request = new AddBidStrategiesRequest
            {
                BidStrategies = bidStrategies
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddBidStrategiesAsync(r), request));
        }
        public async Task<AddBudgetsResponse> AddBudgetsAsync(
            IList<Budget> budgets)
        {
            var request = new AddBudgetsRequest
            {
                Budgets = budgets
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddBudgetsAsync(r), request));
        }
        public async Task<AddCampaignCriterionsResponse> AddCampaignCriterionsAsync(
            IList<CampaignCriterion> campaignCriterions,
            CampaignCriterionType criterionType)
        {
            var request = new AddCampaignCriterionsRequest
            {
                CampaignCriterions = campaignCriterions,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddCampaignCriterionsAsync(r), request));
        }
        public async Task<AddCampaignsResponse> AddCampaignsAsync(
            long accountId,
            IList<Campaign> campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddCampaignsAsync(r), request));
        }
        public async Task<AddConversionGoalsResponse> AddConversionGoalsAsync(
            IList<ConversionGoal> conversionGoals)
        {
            var request = new AddConversionGoalsRequest
            {
                ConversionGoals = conversionGoals
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddConversionGoalsAsync(r), request));
        }
        public async Task<AddExperimentsResponse> AddExperimentsAsync(
            IList<Experiment> experiments)
        {
            var request = new AddExperimentsRequest
            {
                Experiments = experiments
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddExperimentsAsync(r), request));
        }
        public async Task<AddImportJobsResponse> AddImportJobsAsync(
            IList<ImportJob> importJobs)
        {
            var request = new AddImportJobsRequest
            {
                ImportJobs = importJobs
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddImportJobsAsync(r), request));
        }
        public async Task<AddKeywordsResponse> AddKeywordsAsync(
            long adGroupId,
            IList<Keyword> keywords,
            bool? returnInheritedBidStrategyTypes)
        {
            var request = new AddKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords,
                ReturnInheritedBidStrategyTypes = returnInheritedBidStrategyTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddKeywordsAsync(r), request));
        }
        public async Task<AddLabelsResponse> AddLabelsAsync(
            IList<Label> labels)
        {
            var request = new AddLabelsRequest
            {
                Labels = labels
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddLabelsAsync(r), request));
        }
        public async Task<AddListItemsToSharedListResponse> AddListItemsToSharedListAsync(
            IList<SharedListItem> listItems,
            SharedList sharedList,
            EntityScope? sharedEntityScope)
        {
            var request = new AddListItemsToSharedListRequest
            {
                ListItems = listItems,
                SharedList = sharedList,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddListItemsToSharedListAsync(r), request));
        }
        public async Task<AddMediaResponse> AddMediaAsync(
            long accountId,
            IList<Media> media)
        {
            var request = new AddMediaRequest
            {
                AccountId = accountId,
                Media = media
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddMediaAsync(r), request));
        }
        public async Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(
            IList<EntityNegativeKeyword> entityNegativeKeywords)
        {
            var request = new AddNegativeKeywordsToEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddNegativeKeywordsToEntitiesAsync(r), request));
        }
        public async Task<AddSharedEntityResponse> AddSharedEntityAsync(
            SharedEntity sharedEntity,
            IList<SharedListItem> listItems,
            EntityScope? sharedEntityScope)
        {
            var request = new AddSharedEntityRequest
            {
                SharedEntity = sharedEntity,
                ListItems = listItems,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddSharedEntityAsync(r), request));
        }
        public async Task<AddUetTagsResponse> AddUetTagsAsync(
            IList<UetTag> uetTags)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = uetTags
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddUetTagsAsync(r), request));
        }
        public async Task<AddVideosResponse> AddVideosAsync(
            IList<Video> videos)
        {
            var request = new AddVideosRequest
            {
                Videos = videos
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AddVideosAsync(r), request));
        }
        public async Task<AppealEditorialRejectionsResponse> AppealEditorialRejectionsAsync(
            IList<EntityIdToParentIdAssociation> entityIdToParentIdAssociations,
            EntityType entityType,
            String justificationText)
        {
            var request = new AppealEditorialRejectionsRequest
            {
                EntityIdToParentIdAssociations = entityIdToParentIdAssociations,
                EntityType = entityType,
                JustificationText = justificationText
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.AppealEditorialRejectionsAsync(r), request));
        }
        public async Task<ApplyOfflineConversionAdjustmentsResponse> ApplyOfflineConversionAdjustmentsAsync(
            IList<OfflineConversionAdjustment> offlineConversionAdjustments)
        {
            var request = new ApplyOfflineConversionAdjustmentsRequest
            {
                OfflineConversionAdjustments = offlineConversionAdjustments
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.ApplyOfflineConversionAdjustmentsAsync(r), request));
        }
        public async Task<ApplyOfflineConversionsResponse> ApplyOfflineConversionsAsync(
            IList<OfflineConversion> offlineConversions)
        {
            var request = new ApplyOfflineConversionsRequest
            {
                OfflineConversions = offlineConversions
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.ApplyOfflineConversionsAsync(r), request));
        }
        public async Task<ApplyProductPartitionActionsResponse> ApplyProductPartitionActionsAsync(
            IList<AdGroupCriterionAction> criterionActions)
        {
            var request = new ApplyProductPartitionActionsRequest
            {
                CriterionActions = criterionActions
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.ApplyProductPartitionActionsAsync(r), request));
        }
        public async Task<ApplyHotelGroupActionsResponse> ApplyHotelGroupActionsAsync(
            IList<AdGroupCriterionAction> criterionActions)
        {
            var request = new ApplyHotelGroupActionsRequest
            {
                CriterionActions = criterionActions
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.ApplyHotelGroupActionsAsync(r), request));
        }
        public async Task<DeleteAdExtensionsResponse> DeleteAdExtensionsAsync(
            long accountId,
            IList<long> adExtensionIds)
        {
            var request = new DeleteAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteAdExtensionsAsync(r), request));
        }
        public async Task<DeleteAdExtensionsAssociationsResponse> DeleteAdExtensionsAssociationsAsync(
            long accountId,
            IList<AdExtensionIdToEntityIdAssociation> adExtensionIdToEntityIdAssociations,
            AssociationType associationType)
        {
            var request = new DeleteAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = adExtensionIdToEntityIdAssociations,
                AssociationType = associationType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteAdExtensionsAssociationsAsync(r), request));
        }
        public async Task<DeleteAdGroupCriterionsResponse> DeleteAdGroupCriterionsAsync(
            IList<long> adGroupCriterionIds,
            long adGroupId,
            AdGroupCriterionType criterionType)
        {
            var request = new DeleteAdGroupCriterionsRequest
            {
                AdGroupCriterionIds = adGroupCriterionIds,
                AdGroupId = adGroupId,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteAdGroupCriterionsAsync(r), request));
        }
        public async Task<DeleteAdGroupsResponse> DeleteAdGroupsAsync(
            long campaignId,
            IList<long> adGroupIds)
        {
            var request = new DeleteAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroupIds = adGroupIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteAdGroupsAsync(r), request));
        }
        public async Task<DeleteAdsResponse> DeleteAdsAsync(
            long adGroupId,
            IList<long> adIds)
        {
            var request = new DeleteAdsRequest
            {
                AdGroupId = adGroupId,
                AdIds = adIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteAdsAsync(r), request));
        }
        public async Task<DeleteAudiencesResponse> DeleteAudiencesAsync(
            IList<long> audienceIds)
        {
            var request = new DeleteAudiencesRequest
            {
                AudienceIds = audienceIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteAudiencesAsync(r), request));
        }
        public async Task<DeleteBidStrategiesResponse> DeleteBidStrategiesAsync(
            IList<long> bidStrategyIds)
        {
            var request = new DeleteBidStrategiesRequest
            {
                BidStrategyIds = bidStrategyIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteBidStrategiesAsync(r), request));
        }
        public async Task<DeleteBudgetsResponse> DeleteBudgetsAsync(
            IList<long> budgetIds)
        {
            var request = new DeleteBudgetsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteBudgetsAsync(r), request));
        }
        public async Task<DeleteCampaignCriterionsResponse> DeleteCampaignCriterionsAsync(
            IList<long> campaignCriterionIds,
            long campaignId,
            CampaignCriterionType criterionType)
        {
            var request = new DeleteCampaignCriterionsRequest
            {
                CampaignCriterionIds = campaignCriterionIds,
                CampaignId = campaignId,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteCampaignCriterionsAsync(r), request));
        }
        public async Task<DeleteCampaignsResponse> DeleteCampaignsAsync(
            long accountId,
            IList<long> campaignIds)
        {
            var request = new DeleteCampaignsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request));
        }
        public async Task<DeleteExperimentsResponse> DeleteExperimentsAsync(
            IList<long> experimentIds)
        {
            var request = new DeleteExperimentsRequest
            {
                ExperimentIds = experimentIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteExperimentsAsync(r), request));
        }
        public async Task<DeleteImportJobsResponse> DeleteImportJobsAsync(
            IList<long> importJobIds,
            String importType)
        {
            var request = new DeleteImportJobsRequest
            {
                ImportJobIds = importJobIds,
                ImportType = importType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteImportJobsAsync(r), request));
        }
        public async Task<DeleteKeywordsResponse> DeleteKeywordsAsync(
            long adGroupId,
            IList<long> keywordIds)
        {
            var request = new DeleteKeywordsRequest
            {
                AdGroupId = adGroupId,
                KeywordIds = keywordIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteKeywordsAsync(r), request));
        }
        public async Task<DeleteLabelAssociationsResponse> DeleteLabelAssociationsAsync(
            EntityType entityType,
            IList<LabelAssociation> labelAssociations)
        {
            var request = new DeleteLabelAssociationsRequest
            {
                EntityType = entityType,
                LabelAssociations = labelAssociations
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteLabelAssociationsAsync(r), request));
        }
        public async Task<DeleteLabelsResponse> DeleteLabelsAsync(
            IList<long> labelIds)
        {
            var request = new DeleteLabelsRequest
            {
                LabelIds = labelIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteLabelsAsync(r), request));
        }
        public async Task<DeleteListItemsFromSharedListResponse> DeleteListItemsFromSharedListAsync(
            IList<long> listItemIds,
            SharedList sharedList,
            EntityScope? sharedEntityScope)
        {
            var request = new DeleteListItemsFromSharedListRequest
            {
                ListItemIds = listItemIds,
                SharedList = sharedList,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteListItemsFromSharedListAsync(r), request));
        }
        public async Task<DeleteMediaResponse> DeleteMediaAsync(
            long accountId,
            IList<long> mediaIds)
        {
            var request = new DeleteMediaRequest
            {
                AccountId = accountId,
                MediaIds = mediaIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteMediaAsync(r), request));
        }
        public async Task<DeleteNegativeKeywordsFromEntitiesResponse> DeleteNegativeKeywordsFromEntitiesAsync(
            IList<EntityNegativeKeyword> entityNegativeKeywords)
        {
            var request = new DeleteNegativeKeywordsFromEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteNegativeKeywordsFromEntitiesAsync(r), request));
        }
        public async Task<DeleteSharedEntitiesResponse> DeleteSharedEntitiesAsync(
            IList<SharedEntity> sharedEntities,
            EntityScope? sharedEntityScope)
        {
            var request = new DeleteSharedEntitiesRequest
            {
                SharedEntities = sharedEntities,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteSharedEntitiesAsync(r), request));
        }
        public async Task<DeleteSharedEntityAssociationsResponse> DeleteSharedEntityAssociationsAsync(
            IList<SharedEntityAssociation> associations,
            EntityScope? sharedEntityScope)
        {
            var request = new DeleteSharedEntityAssociationsRequest
            {
                Associations = associations,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteSharedEntityAssociationsAsync(r), request));
        }
        public async Task<DeleteVideosResponse> DeleteVideosAsync(
            IList<long> videoIds)
        {
            var request = new DeleteVideosRequest
            {
                VideoIds = videoIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteVideosAsync(r), request));
        }
        public async Task<GetAccountMigrationStatusesResponse> GetAccountMigrationStatusesAsync(
            IList<long> accountIds,
            String migrationType)
        {
            var request = new GetAccountMigrationStatusesRequest
            {
                AccountIds = accountIds,
                MigrationType = migrationType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAccountMigrationStatusesAsync(r), request));
        }
        public async Task<GetAccountPropertiesResponse> GetAccountPropertiesAsync(
            IList<AccountPropertyName> accountPropertyNames)
        {
            var request = new GetAccountPropertiesRequest
            {
                AccountPropertyNames = accountPropertyNames
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAccountPropertiesAsync(r), request));
        }
        public async Task<GetAdExtensionIdsByAccountIdResponse> GetAdExtensionIdsByAccountIdAsync(
            long accountId,
            AdExtensionsTypeFilter adExtensionType,
            AssociationType? associationType)
        {
            var request = new GetAdExtensionIdsByAccountIdRequest
            {
                AccountId = accountId,
                AdExtensionType = adExtensionType,
                AssociationType = associationType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdExtensionIdsByAccountIdAsync(r), request));
        }
        public async Task<GetAdExtensionsAssociationsResponse> GetAdExtensionsAssociationsAsync(
            long accountId,
            AdExtensionsTypeFilter adExtensionType,
            AssociationType associationType,
            IList<long> entityIds,
            AdExtensionAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionType = adExtensionType,
                AssociationType = associationType,
                EntityIds = entityIds,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdExtensionsAssociationsAsync(r), request));
        }
        public async Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(
            long accountId,
            IList<long> adExtensionIds,
            AdExtensionsTypeFilter adExtensionType,
            AdExtensionAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds,
                AdExtensionType = adExtensionType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdExtensionsByIdsAsync(r), request));
        }
        public async Task<GetAdExtensionsEditorialReasonsResponse> GetAdExtensionsEditorialReasonsAsync(
            long accountId,
            IList<AdExtensionIdToEntityIdAssociation> adExtensionIdToEntityIdAssociations,
            AssociationType associationType)
        {
            var request = new GetAdExtensionsEditorialReasonsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = adExtensionIdToEntityIdAssociations,
                AssociationType = associationType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdExtensionsEditorialReasonsAsync(r), request));
        }
        public async Task<GetAdGroupCriterionsByIdsResponse> GetAdGroupCriterionsByIdsAsync(
            IList<long> adGroupCriterionIds,
            long adGroupId,
            AdGroupCriterionType criterionType,
            CriterionAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdGroupCriterionsByIdsRequest
            {
                AdGroupCriterionIds = adGroupCriterionIds,
                AdGroupId = adGroupId,
                CriterionType = criterionType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdGroupCriterionsByIdsAsync(r), request));
        }
        public async Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(
            long campaignId,
            AdGroupAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdGroupsByCampaignIdRequest
            {
                CampaignId = campaignId,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdGroupsByCampaignIdAsync(r), request));
        }
        public async Task<GetAdGroupsByIdsResponse> GetAdGroupsByIdsAsync(
            long campaignId,
            IList<long> adGroupIds,
            AdGroupAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdGroupsByIdsRequest
            {
                CampaignId = campaignId,
                AdGroupIds = adGroupIds,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdGroupsByIdsAsync(r), request));
        }
        public async Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(
            long adGroupId,
            IList<AdType> adTypes,
            AdAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdsByAdGroupIdRequest
            {
                AdGroupId = adGroupId,
                AdTypes = adTypes,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdsByAdGroupIdAsync(r), request));
        }
        public async Task<GetAdsByEditorialStatusResponse> GetAdsByEditorialStatusAsync(
            long adGroupId,
            AdEditorialStatus editorialStatus,
            IList<AdType> adTypes,
            AdAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdsByEditorialStatusRequest
            {
                AdGroupId = adGroupId,
                EditorialStatus = editorialStatus,
                AdTypes = adTypes,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdsByEditorialStatusAsync(r), request));
        }
        public async Task<GetAdsByIdsResponse> GetAdsByIdsAsync(
            long adGroupId,
            IList<long> adIds,
            IList<AdType> adTypes,
            AdAdditionalField? returnAdditionalFields)
        {
            var request = new GetAdsByIdsRequest
            {
                AdGroupId = adGroupId,
                AdIds = adIds,
                AdTypes = adTypes,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdsByIdsAsync(r), request));
        }
        public async Task<GetAudiencesByIdsResponse> GetAudiencesByIdsAsync(
            IList<long> audienceIds,
            AudienceType type,
            AudienceAdditionalField? returnAdditionalFields)
        {
            var request = new GetAudiencesByIdsRequest
            {
                AudienceIds = audienceIds,
                Type = type,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAudiencesByIdsAsync(r), request));
        }
        public async Task<GetBidStrategiesByIdsResponse> GetBidStrategiesByIdsAsync(
            IList<long> bidStrategyIds)
        {
            var request = new GetBidStrategiesByIdsRequest
            {
                BidStrategyIds = bidStrategyIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetBidStrategiesByIdsAsync(r), request));
        }
        public async Task<GetBMCStoresByCustomerIdResponse> GetBMCStoresByCustomerIdAsync(
            BMCStoreAdditionalField? returnAdditionalFields)
        {
            var request = new GetBMCStoresByCustomerIdRequest
            {
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetBMCStoresByCustomerIdAsync(r), request));
        }
        public async Task<GetBSCCountriesResponse> GetBSCCountriesAsync()
        {
            var request = new GetBSCCountriesRequest
            {
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetBSCCountriesAsync(r), request));
        }
        public async Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(
            IList<long> budgetIds)
        {
            var request = new GetBudgetsByIdsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetBudgetsByIdsAsync(r), request));
        }
        public async Task<GetCampaignCriterionsByIdsResponse> GetCampaignCriterionsByIdsAsync(
            IList<long> campaignCriterionIds,
            long campaignId,
            CampaignCriterionType criterionType,
            CriterionAdditionalField? returnAdditionalFields)
        {
            var request = new GetCampaignCriterionsByIdsRequest
            {
                CampaignCriterionIds = campaignCriterionIds,
                CampaignId = campaignId,
                CriterionType = criterionType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignCriterionsByIdsAsync(r), request));
        }
        public async Task<GetCampaignIdsByBidStrategyIdsResponse> GetCampaignIdsByBidStrategyIdsAsync(
            IList<long> bidStrategyIds)
        {
            var request = new GetCampaignIdsByBidStrategyIdsRequest
            {
                BidStrategyIds = bidStrategyIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignIdsByBidStrategyIdsAsync(r), request));
        }
        public async Task<GetCampaignIdsByBudgetIdsResponse> GetCampaignIdsByBudgetIdsAsync(
            IList<long> budgetIds)
        {
            var request = new GetCampaignIdsByBudgetIdsRequest
            {
                BudgetIds = budgetIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignIdsByBudgetIdsAsync(r), request));
        }
        public async Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(
            long accountId,
            CampaignType campaignType,
            CampaignAdditionalField? returnAdditionalFields)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request));
        }
        public async Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(
            long accountId,
            IList<long> campaignIds,
            CampaignType campaignType,
            CampaignAdditionalField? returnAdditionalFields)
        {
            var request = new GetCampaignsByIdsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds,
                CampaignType = campaignType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request));
        }
        public async Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(
            IList<long> conversionGoalIds,
            ConversionGoalType conversionGoalTypes,
            ConversionGoalAdditionalField? returnAdditionalFields)
        {
            var request = new GetConversionGoalsByIdsRequest
            {
                ConversionGoalIds = conversionGoalIds,
                ConversionGoalTypes = conversionGoalTypes,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetConversionGoalsByIdsAsync(r), request));
        }
        public async Task<GetConversionGoalsByTagIdsResponse> GetConversionGoalsByTagIdsAsync(
            IList<long> tagIds,
            ConversionGoalType conversionGoalTypes,
            ConversionGoalAdditionalField? returnAdditionalFields)
        {
            var request = new GetConversionGoalsByTagIdsRequest
            {
                TagIds = tagIds,
                ConversionGoalTypes = conversionGoalTypes,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetConversionGoalsByTagIdsAsync(r), request));
        }
        public async Task<GetEditorialReasonsByIdsResponse> GetEditorialReasonsByIdsAsync(
            long accountId,
            IList<EntityIdToParentIdAssociation> entityIdToParentIdAssociations,
            EntityType entityType)
        {
            var request = new GetEditorialReasonsByIdsRequest
            {
                AccountId = accountId,
                EntityIdToParentIdAssociations = entityIdToParentIdAssociations,
                EntityType = entityType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetEditorialReasonsByIdsAsync(r), request));
        }
        public async Task<GetExperimentsByIdsResponse> GetExperimentsByIdsAsync(
            IList<long> experimentIds,
            Paging pageInfo)
        {
            var request = new GetExperimentsByIdsRequest
            {
                ExperimentIds = experimentIds,
                PageInfo = pageInfo
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetExperimentsByIdsAsync(r), request));
        }
        public async Task<GetFileImportUploadUrlResponse> GetFileImportUploadUrlAsync()
        {
            var request = new GetFileImportUploadUrlRequest
            {
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetFileImportUploadUrlAsync(r), request));
        }
        public async Task<GetGeoLocationsFileUrlResponse> GetGeoLocationsFileUrlAsync(
            String version,
            String languageLocale)
        {
            var request = new GetGeoLocationsFileUrlRequest
            {
                Version = version,
                LanguageLocale = languageLocale
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetGeoLocationsFileUrlAsync(r), request));
        }
        public async Task<GetImportEntityIdsMappingResponse> GetImportEntityIdsMappingAsync(
            String importType,
            IList<long> sourceEntityIds,
            ImportEntityType importEntityType)
        {
            var request = new GetImportEntityIdsMappingRequest
            {
                ImportType = importType,
                SourceEntityIds = sourceEntityIds,
                ImportEntityType = importEntityType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetImportEntityIdsMappingAsync(r), request));
        }
        public async Task<GetImportJobsByIdsResponse> GetImportJobsByIdsAsync(
            IList<long> importJobIds,
            String importType,
            ImportAdditionalField? returnAdditionalFields)
        {
            var request = new GetImportJobsByIdsRequest
            {
                ImportJobIds = importJobIds,
                ImportType = importType,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetImportJobsByIdsAsync(r), request));
        }
        public async Task<GetImportResultsResponse> GetImportResultsAsync(
            String importType,
            Paging pageInfo,
            IList<long> importJobIds,
            ImportAdditionalField? returnAdditionalFields)
        {
            var request = new GetImportResultsRequest
            {
                ImportType = importType,
                PageInfo = pageInfo,
                ImportJobIds = importJobIds,
                ReturnAdditionalFields = returnAdditionalFields
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetImportResultsAsync(r), request));
        }
        public async Task<GetKeywordsByAdGroupIdResponse> GetKeywordsByAdGroupIdAsync(
            long adGroupId)
        {
            var request = new GetKeywordsByAdGroupIdRequest
            {
                AdGroupId = adGroupId
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetKeywordsByAdGroupIdAsync(r), request));
        }
        public async Task<GetKeywordsByEditorialStatusResponse> GetKeywordsByEditorialStatusAsync(
            long adGroupId,
            KeywordEditorialStatus editorialStatus)
        {
            var request = new GetKeywordsByEditorialStatusRequest
            {
                AdGroupId = adGroupId,
                EditorialStatus = editorialStatus
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetKeywordsByEditorialStatusAsync(r), request));
        }
        public async Task<GetKeywordsByIdsResponse> GetKeywordsByIdsAsync(
            long adGroupId,
            IList<long> keywordIds)
        {
            var request = new GetKeywordsByIdsRequest
            {
                AdGroupId = adGroupId,
                KeywordIds = keywordIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetKeywordsByIdsAsync(r), request));
        }
        public async Task<GetLabelAssociationsByEntityIdsResponse> GetLabelAssociationsByEntityIdsAsync(
            IList<long> entityIds,
            EntityType entityType)
        {
            var request = new GetLabelAssociationsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetLabelAssociationsByEntityIdsAsync(r), request));
        }
        public async Task<GetLabelAssociationsByLabelIdsResponse> GetLabelAssociationsByLabelIdsAsync(
            EntityType entityType,
            IList<long> labelIds,
            Paging pageInfo)
        {
            var request = new GetLabelAssociationsByLabelIdsRequest
            {
                EntityType = entityType,
                LabelIds = labelIds,
                PageInfo = pageInfo
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetLabelAssociationsByLabelIdsAsync(r), request));
        }
        public async Task<GetLabelsByIdsResponse> GetLabelsByIdsAsync(
            IList<long> labelIds,
            Paging pageInfo)
        {
            var request = new GetLabelsByIdsRequest
            {
                LabelIds = labelIds,
                PageInfo = pageInfo
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetLabelsByIdsAsync(r), request));
        }
        public async Task<GetListItemsBySharedListResponse> GetListItemsBySharedListAsync(
            SharedList sharedList,
            EntityScope? sharedEntityScope)
        {
            var request = new GetListItemsBySharedListRequest
            {
                SharedList = sharedList,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetListItemsBySharedListAsync(r), request));
        }
        public async Task<GetMediaAssociationsResponse> GetMediaAssociationsAsync(
            MediaEnabledEntityFilter mediaEnabledEntities,
            IList<long> mediaIds)
        {
            var request = new GetMediaAssociationsRequest
            {
                MediaEnabledEntities = mediaEnabledEntities,
                MediaIds = mediaIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetMediaAssociationsAsync(r), request));
        }
        public async Task<GetMediaMetaDataByAccountIdResponse> GetMediaMetaDataByAccountIdAsync(
            MediaEnabledEntityFilter mediaEnabledEntities,
            Paging pageInfo)
        {
            var request = new GetMediaMetaDataByAccountIdRequest
            {
                MediaEnabledEntities = mediaEnabledEntities,
                PageInfo = pageInfo
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetMediaMetaDataByAccountIdAsync(r), request));
        }
        public async Task<GetMediaMetaDataByIdsResponse> GetMediaMetaDataByIdsAsync(
            IList<long> mediaIds)
        {
            var request = new GetMediaMetaDataByIdsRequest
            {
                MediaIds = mediaIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetMediaMetaDataByIdsAsync(r), request));
        }
        public async Task<GetNegativeKeywordsByEntityIdsResponse> GetNegativeKeywordsByEntityIdsAsync(
            IList<long> entityIds,
            String entityType,
            long? parentEntityId)
        {
            var request = new GetNegativeKeywordsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                ParentEntityId = parentEntityId
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetNegativeKeywordsByEntityIdsAsync(r), request));
        }
        public async Task<GetNegativeSitesByAdGroupIdsResponse> GetNegativeSitesByAdGroupIdsAsync(
            long campaignId,
            IList<long> adGroupIds)
        {
            var request = new GetNegativeSitesByAdGroupIdsRequest
            {
                CampaignId = campaignId,
                AdGroupIds = adGroupIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetNegativeSitesByAdGroupIdsAsync(r), request));
        }
        public async Task<GetNegativeSitesByCampaignIdsResponse> GetNegativeSitesByCampaignIdsAsync(
            long accountId,
            IList<long> campaignIds)
        {
            var request = new GetNegativeSitesByCampaignIdsRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetNegativeSitesByCampaignIdsAsync(r), request));
        }
        public async Task<GetProfileDataFileUrlResponse> GetProfileDataFileUrlAsync(
            String languageLocale,
            ProfileType profileType)
        {
            var request = new GetProfileDataFileUrlRequest
            {
                LanguageLocale = languageLocale,
                ProfileType = profileType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetProfileDataFileUrlAsync(r), request));
        }
        public async Task<GetSharedEntitiesResponse> GetSharedEntitiesAsync(
            String sharedEntityType,
            EntityScope? sharedEntityScope)
        {
            var request = new GetSharedEntitiesRequest
            {
                SharedEntityType = sharedEntityType,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetSharedEntitiesAsync(r), request));
        }
        public async Task<GetSharedEntitiesByAccountIdResponse> GetSharedEntitiesByAccountIdAsync(
            String sharedEntityType)
        {
            var request = new GetSharedEntitiesByAccountIdRequest
            {
                SharedEntityType = sharedEntityType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetSharedEntitiesByAccountIdAsync(r), request));
        }
        public async Task<GetSharedEntityAssociationsByEntityIdsResponse> GetSharedEntityAssociationsByEntityIdsAsync(
            IList<long> entityIds,
            String entityType,
            String sharedEntityType,
            EntityScope? sharedEntityScope)
        {
            var request = new GetSharedEntityAssociationsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                SharedEntityType = sharedEntityType,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetSharedEntityAssociationsByEntityIdsAsync(r), request));
        }
        public async Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(
            String entityType,
            IList<long> sharedEntityIds,
            String sharedEntityType,
            EntityScope? sharedEntityScope)
        {
            var request = new GetSharedEntityAssociationsBySharedEntityIdsRequest
            {
                EntityType = entityType,
                SharedEntityIds = sharedEntityIds,
                SharedEntityType = sharedEntityType,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetSharedEntityAssociationsBySharedEntityIdsAsync(r), request));
        }
        public async Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(
            IList<long> tagIds)
        {
            var request = new GetUetTagsByIdsRequest
            {
                TagIds = tagIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetUetTagsByIdsAsync(r), request));
        }
        public async Task<GetVideosByIdsResponse> GetVideosByIdsAsync(
            IList<long> videoIds,
            Paging pageInfo)
        {
            var request = new GetVideosByIdsRequest
            {
                VideoIds = videoIds,
                PageInfo = pageInfo
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetVideosByIdsAsync(r), request));
        }
        public async Task<SearchCompaniesResponse> SearchCompaniesAsync(
            String companyNameFilter,
            String languageLocale)
        {
            var request = new SearchCompaniesRequest
            {
                CompanyNameFilter = companyNameFilter,
                LanguageLocale = languageLocale
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SearchCompaniesAsync(r), request));
        }
        public async Task<SetAccountPropertiesResponse> SetAccountPropertiesAsync(
            IList<AccountProperty> accountProperties)
        {
            var request = new SetAccountPropertiesRequest
            {
                AccountProperties = accountProperties
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SetAccountPropertiesAsync(r), request));
        }
        public async Task<SetAdExtensionsAssociationsResponse> SetAdExtensionsAssociationsAsync(
            long accountId,
            IList<AdExtensionIdToEntityIdAssociation> adExtensionIdToEntityIdAssociations,
            AssociationType associationType)
        {
            var request = new SetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = adExtensionIdToEntityIdAssociations,
                AssociationType = associationType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SetAdExtensionsAssociationsAsync(r), request));
        }
        public async Task<SetLabelAssociationsResponse> SetLabelAssociationsAsync(
            EntityType entityType,
            IList<LabelAssociation> labelAssociations)
        {
            var request = new SetLabelAssociationsRequest
            {
                EntityType = entityType,
                LabelAssociations = labelAssociations
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SetLabelAssociationsAsync(r), request));
        }
        public async Task<SetNegativeSitesToAdGroupsResponse> SetNegativeSitesToAdGroupsAsync(
            long campaignId,
            IList<AdGroupNegativeSites> adGroupNegativeSites)
        {
            var request = new SetNegativeSitesToAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroupNegativeSites = adGroupNegativeSites
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SetNegativeSitesToAdGroupsAsync(r), request));
        }
        public async Task<SetNegativeSitesToCampaignsResponse> SetNegativeSitesToCampaignsAsync(
            long accountId,
            IList<CampaignNegativeSites> campaignNegativeSites)
        {
            var request = new SetNegativeSitesToCampaignsRequest
            {
                AccountId = accountId,
                CampaignNegativeSites = campaignNegativeSites
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SetNegativeSitesToCampaignsAsync(r), request));
        }
        public async Task<SetSharedEntityAssociationsResponse> SetSharedEntityAssociationsAsync(
            IList<SharedEntityAssociation> associations,
            EntityScope? sharedEntityScope)
        {
            var request = new SetSharedEntityAssociationsRequest
            {
                Associations = associations,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.SetSharedEntityAssociationsAsync(r), request));
        }
        public async Task<UpdateAdExtensionsResponse> UpdateAdExtensionsAsync(
            long accountId,
            IList<AdExtension> adExtensions)
        {
            var request = new UpdateAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateAdExtensionsAsync(r), request));
        }
        public async Task<UpdateAdGroupCriterionsResponse> UpdateAdGroupCriterionsAsync(
            IList<AdGroupCriterion> adGroupCriterions,
            AdGroupCriterionType criterionType)
        {
            var request = new UpdateAdGroupCriterionsRequest
            {
                AdGroupCriterions = adGroupCriterions,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateAdGroupCriterionsAsync(r), request));
        }
        public async Task<UpdateAdGroupsResponse> UpdateAdGroupsAsync(
            long campaignId,
            IList<AdGroup> adGroups,
            bool updateAudienceAdsBidAdjustment,
            bool? returnInheritedBidStrategyTypes)
        {
            var request = new UpdateAdGroupsRequest
            {
                CampaignId = campaignId,
                AdGroups = adGroups,
                UpdateAudienceAdsBidAdjustment = updateAudienceAdsBidAdjustment,
                ReturnInheritedBidStrategyTypes = returnInheritedBidStrategyTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateAdGroupsAsync(r), request));
        }
        public async Task<UpdateAdsResponse> UpdateAdsAsync(
            long adGroupId,
            IList<Ad> ads)
        {
            var request = new UpdateAdsRequest
            {
                AdGroupId = adGroupId,
                Ads = ads
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateAdsAsync(r), request));
        }
        public async Task<UpdateAudiencesResponse> UpdateAudiencesAsync(
            IList<Audience> audiences)
        {
            var request = new UpdateAudiencesRequest
            {
                Audiences = audiences
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateAudiencesAsync(r), request));
        }
        public async Task<UpdateBidStrategiesResponse> UpdateBidStrategiesAsync(
            IList<BidStrategy> bidStrategies)
        {
            var request = new UpdateBidStrategiesRequest
            {
                BidStrategies = bidStrategies
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateBidStrategiesAsync(r), request));
        }
        public async Task<UpdateBudgetsResponse> UpdateBudgetsAsync(
            IList<Budget> budgets)
        {
            var request = new UpdateBudgetsRequest
            {
                Budgets = budgets
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateBudgetsAsync(r), request));
        }
        public async Task<UpdateCampaignCriterionsResponse> UpdateCampaignCriterionsAsync(
            IList<CampaignCriterion> campaignCriterions,
            CampaignCriterionType criterionType)
        {
            var request = new UpdateCampaignCriterionsRequest
            {
                CampaignCriterions = campaignCriterions,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateCampaignCriterionsAsync(r), request));
        }
        public async Task<UpdateCampaignsResponse> UpdateCampaignsAsync(
            long accountId,
            IList<Campaign> campaigns)
        {
            var request = new UpdateCampaignsRequest
            {
                AccountId = accountId,
                Campaigns = campaigns
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateCampaignsAsync(r), request));
        }
        public async Task<UpdateConversionGoalsResponse> UpdateConversionGoalsAsync(
            IList<ConversionGoal> conversionGoals)
        {
            var request = new UpdateConversionGoalsRequest
            {
                ConversionGoals = conversionGoals
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateConversionGoalsAsync(r), request));
        }
        public async Task<UpdateExperimentsResponse> UpdateExperimentsAsync(
            IList<Experiment> experiments)
        {
            var request = new UpdateExperimentsRequest
            {
                Experiments = experiments
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateExperimentsAsync(r), request));
        }
        public async Task<UpdateImportJobsResponse> UpdateImportJobsAsync(
            IList<ImportJob> importJobs)
        {
            var request = new UpdateImportJobsRequest
            {
                ImportJobs = importJobs
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateImportJobsAsync(r), request));
        }
        public async Task<UpdateKeywordsResponse> UpdateKeywordsAsync(
            long adGroupId,
            IList<Keyword> keywords,
            bool? returnInheritedBidStrategyTypes)
        {
            var request = new UpdateKeywordsRequest
            {
                AdGroupId = adGroupId,
                Keywords = keywords,
                ReturnInheritedBidStrategyTypes = returnInheritedBidStrategyTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateKeywordsAsync(r), request));
        }
        public async Task<UpdateLabelsResponse> UpdateLabelsAsync(
            IList<Label> labels)
        {
            var request = new UpdateLabelsRequest
            {
                Labels = labels
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateLabelsAsync(r), request));
        }
        public async Task<UpdateSharedEntitiesResponse> UpdateSharedEntitiesAsync(
            IList<SharedEntity> sharedEntities,
            EntityScope? sharedEntityScope)
        {
            var request = new UpdateSharedEntitiesRequest
            {
                SharedEntities = sharedEntities,
                SharedEntityScope = sharedEntityScope
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateSharedEntitiesAsync(r), request));
        }
        public async Task<UpdateUetTagsResponse> UpdateUetTagsAsync(
            IList<UetTag> uetTags)
        {
            var request = new UpdateUetTagsRequest
            {
                UetTags = uetTags
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateUetTagsAsync(r), request));
        }
        public async Task<UpdateVideosResponse> UpdateVideosAsync(
            IList<Video> videos)
        {
            var request = new UpdateVideosRequest
            {
                Videos = videos
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.UpdateVideosAsync(r), request));
        }
        public void OutputAccountMigrationStatusesInfo(AccountMigrationStatusesInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountMigrationStatusesInfo * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage("MigrationStatusInfos:");
                OutputArrayOfMigrationStatusInfo(dataObject.MigrationStatusInfos);
                OutputStatusMessage("* * * End OutputAccountMigrationStatusesInfo * * *");
            }
        }
        public void OutputArrayOfAccountMigrationStatusesInfo(IList<AccountMigrationStatusesInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountMigrationStatusesInfo(dataObject);
                    }
                }
            }
        }
        public void OutputAccountProperty(AccountProperty dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountProperty * * *");
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage("* * * End OutputAccountProperty * * *");
            }
        }
        public void OutputArrayOfAccountProperty(IList<AccountProperty> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountProperty(dataObject);
                    }
                }
            }
        }
        public void OutputActionAdExtension(ActionAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputActionAdExtension * * *");
                OutputStatusMessage(string.Format("ActionType: {0}", dataObject.ActionType));
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputActionAdExtension * * *");
            }
        }
        public void OutputArrayOfActionAdExtension(IList<ActionAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputActionAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputAd(Ad dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAd * * *");
                OutputStatusMessage(string.Format("AdFormatPreference: {0}", dataObject.AdFormatPreference));
                OutputStatusMessage(string.Format("DevicePreference: {0}", dataObject.DevicePreference));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                var appinstallad = dataObject as AppInstallAd;
                if(null != appinstallad)
                {
                    OutputAppInstallAd((AppInstallAd)dataObject);
                }
                var dynamicsearchad = dataObject as DynamicSearchAd;
                if(null != dynamicsearchad)
                {
                    OutputDynamicSearchAd((DynamicSearchAd)dataObject);
                }
                var expandedtextad = dataObject as ExpandedTextAd;
                if(null != expandedtextad)
                {
                    OutputExpandedTextAd((ExpandedTextAd)dataObject);
                }
                var productad = dataObject as ProductAd;
                if(null != productad)
                {
                    OutputProductAd((ProductAd)dataObject);
                }
                var responsivead = dataObject as ResponsiveAd;
                if(null != responsivead)
                {
                    OutputResponsiveAd((ResponsiveAd)dataObject);
                }
                var responsivesearchad = dataObject as ResponsiveSearchAd;
                if(null != responsivesearchad)
                {
                    OutputResponsiveSearchAd((ResponsiveSearchAd)dataObject);
                }
                var textad = dataObject as TextAd;
                if(null != textad)
                {
                    OutputTextAd((TextAd)dataObject);
                }
                OutputStatusMessage("* * * End OutputAd * * *");
            }
        }
        public void OutputArrayOfAd(IList<Ad> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAd(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputAdApiError * * *");
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiError(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiFaultDetail * * *");
                OutputStatusMessage("Errors:");
                OutputArrayOfAdApiError(dataObject.Errors);
                OutputStatusMessage("* * * End OutputAdApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputAddress(Address dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAddress * * *");
                OutputStatusMessage(string.Format("CityName: {0}", dataObject.CityName));
                OutputStatusMessage(string.Format("CountryCode: {0}", dataObject.CountryCode));
                OutputStatusMessage(string.Format("PostalCode: {0}", dataObject.PostalCode));
                OutputStatusMessage(string.Format("ProvinceCode: {0}", dataObject.ProvinceCode));
                OutputStatusMessage(string.Format("ProvinceName: {0}", dataObject.ProvinceName));
                OutputStatusMessage(string.Format("StreetAddress: {0}", dataObject.StreetAddress));
                OutputStatusMessage(string.Format("StreetAddress2: {0}", dataObject.StreetAddress2));
                OutputStatusMessage("* * * End OutputAddress * * *");
            }
        }
        public void OutputArrayOfAddress(IList<Address> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAddress(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtension(AdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtension * * *");
                OutputStatusMessage(string.Format("DevicePreference: {0}", dataObject.DevicePreference));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage("Scheduling:");
                OutputSchedule(dataObject.Scheduling);
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("Version: {0}", dataObject.Version));
                var actionadextension = dataObject as ActionAdExtension;
                if(null != actionadextension)
                {
                    OutputActionAdExtension((ActionAdExtension)dataObject);
                }
                var appadextension = dataObject as AppAdExtension;
                if(null != appadextension)
                {
                    OutputAppAdExtension((AppAdExtension)dataObject);
                }
                var calladextension = dataObject as CallAdExtension;
                if(null != calladextension)
                {
                    OutputCallAdExtension((CallAdExtension)dataObject);
                }
                var calloutadextension = dataObject as CalloutAdExtension;
                if(null != calloutadextension)
                {
                    OutputCalloutAdExtension((CalloutAdExtension)dataObject);
                }
                var filterlinkadextension = dataObject as FilterLinkAdExtension;
                if(null != filterlinkadextension)
                {
                    OutputFilterLinkAdExtension((FilterLinkAdExtension)dataObject);
                }
                var flyeradextension = dataObject as FlyerAdExtension;
                if(null != flyeradextension)
                {
                    OutputFlyerAdExtension((FlyerAdExtension)dataObject);
                }
                var imageadextension = dataObject as ImageAdExtension;
                if(null != imageadextension)
                {
                    OutputImageAdExtension((ImageAdExtension)dataObject);
                }
                var locationadextension = dataObject as LocationAdExtension;
                if(null != locationadextension)
                {
                    OutputLocationAdExtension((LocationAdExtension)dataObject);
                }
                var priceadextension = dataObject as PriceAdExtension;
                if(null != priceadextension)
                {
                    OutputPriceAdExtension((PriceAdExtension)dataObject);
                }
                var promotionadextension = dataObject as PromotionAdExtension;
                if(null != promotionadextension)
                {
                    OutputPromotionAdExtension((PromotionAdExtension)dataObject);
                }
                var reviewadextension = dataObject as ReviewAdExtension;
                if(null != reviewadextension)
                {
                    OutputReviewAdExtension((ReviewAdExtension)dataObject);
                }
                var sitelinkadextension = dataObject as SitelinkAdExtension;
                if(null != sitelinkadextension)
                {
                    OutputSitelinkAdExtension((SitelinkAdExtension)dataObject);
                }
                var structuredsnippetadextension = dataObject as StructuredSnippetAdExtension;
                if(null != structuredsnippetadextension)
                {
                    OutputStructuredSnippetAdExtension((StructuredSnippetAdExtension)dataObject);
                }
                var videoadextension = dataObject as VideoAdExtension;
                if(null != videoadextension)
                {
                    OutputVideoAdExtension((VideoAdExtension)dataObject);
                }
                OutputStatusMessage("* * * End OutputAdExtension * * *");
            }
        }
        public void OutputArrayOfAdExtension(IList<AdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionAssociation(AdExtensionAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionAssociation * * *");
                OutputStatusMessage("AdExtension:");
                OutputAdExtension(dataObject.AdExtension);
                OutputStatusMessage(string.Format("AssociationType: {0}", dataObject.AssociationType));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage("* * * End OutputAdExtensionAssociation * * *");
            }
        }
        public void OutputArrayOfAdExtensionAssociation(IList<AdExtensionAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionAssociationCollection(AdExtensionAssociationCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionAssociationCollection * * *");
                OutputStatusMessage("AdExtensionAssociations:");
                OutputArrayOfAdExtensionAssociation(dataObject.AdExtensionAssociations);
                OutputStatusMessage("* * * End OutputAdExtensionAssociationCollection * * *");
            }
        }
        public void OutputArrayOfAdExtensionAssociationCollection(IList<AdExtensionAssociationCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionAssociationCollection(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionEditorialReason(AdExtensionEditorialReason dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionEditorialReason * * *");
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage("PublisherCountries:");
                OutputArrayOfString(dataObject.PublisherCountries);
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage(string.Format("Term: {0}", dataObject.Term));
                OutputStatusMessage("* * * End OutputAdExtensionEditorialReason * * *");
            }
        }
        public void OutputArrayOfAdExtensionEditorialReason(IList<AdExtensionEditorialReason> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionEditorialReason(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionEditorialReasonCollection(AdExtensionEditorialReasonCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionEditorialReasonCollection * * *");
                OutputStatusMessage(string.Format("AdExtensionId: {0}", dataObject.AdExtensionId));
                OutputStatusMessage("Reasons:");
                OutputArrayOfAdExtensionEditorialReason(dataObject.Reasons);
                OutputStatusMessage("* * * End OutputAdExtensionEditorialReasonCollection * * *");
            }
        }
        public void OutputArrayOfAdExtensionEditorialReasonCollection(IList<AdExtensionEditorialReasonCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionEditorialReasonCollection(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionIdentity(AdExtensionIdentity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionIdentity * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Version: {0}", dataObject.Version));
                OutputStatusMessage("* * * End OutputAdExtensionIdentity * * *");
            }
        }
        public void OutputArrayOfAdExtensionIdentity(IList<AdExtensionIdentity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionIdentity(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionIdToEntityIdAssociation(AdExtensionIdToEntityIdAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionIdToEntityIdAssociation * * *");
                OutputStatusMessage(string.Format("AdExtensionId: {0}", dataObject.AdExtensionId));
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage("* * * End OutputAdExtensionIdToEntityIdAssociation * * *");
            }
        }
        public void OutputArrayOfAdExtensionIdToEntityIdAssociation(IList<AdExtensionIdToEntityIdAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionIdToEntityIdAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroup(AdGroup dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroup * * *");
                OutputStatusMessage("AdRotation:");
                OutputAdRotation(dataObject.AdRotation);
                OutputStatusMessage(string.Format("AudienceAdsBidAdjustment: {0}", dataObject.AudienceAdsBidAdjustment));
                OutputStatusMessage("BiddingScheme:");
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputStatusMessage("CpcBid:");
                OutputBid(dataObject.CpcBid);
                OutputStatusMessage("EndDate:");
                OutputDate(dataObject.EndDate);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("MultimediaAdsBidAdjustment: {0}", dataObject.MultimediaAdsBidAdjustment));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Network: {0}", dataObject.Network));
                OutputStatusMessage(string.Format("PrivacyStatus: {0}", dataObject.PrivacyStatus));
                OutputStatusMessage("Settings:");
                OutputArrayOfSetting(dataObject.Settings);
                OutputStatusMessage("StartDate:");
                OutputDate(dataObject.StartDate);
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage(string.Format("AdScheduleUseSearcherTimeZone: {0}", dataObject.AdScheduleUseSearcherTimeZone));
                OutputStatusMessage(string.Format("AdGroupType: {0}", dataObject.AdGroupType));
                OutputStatusMessage("CpvBid:");
                OutputBid(dataObject.CpvBid);
                OutputStatusMessage("CpmBid:");
                OutputBid(dataObject.CpmBid);
                OutputStatusMessage("* * * End OutputAdGroup * * *");
            }
        }
        public void OutputArrayOfAdGroup(IList<AdGroup> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroup(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupCriterion(AdGroupCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupCriterion * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage("Criterion:");
                OutputCriterion(dataObject.Criterion);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var biddableadgroupcriterion = dataObject as BiddableAdGroupCriterion;
                if(null != biddableadgroupcriterion)
                {
                    OutputBiddableAdGroupCriterion((BiddableAdGroupCriterion)dataObject);
                }
                var negativeadgroupcriterion = dataObject as NegativeAdGroupCriterion;
                if(null != negativeadgroupcriterion)
                {
                    OutputNegativeAdGroupCriterion((NegativeAdGroupCriterion)dataObject);
                }
                OutputStatusMessage("* * * End OutputAdGroupCriterion * * *");
            }
        }
        public void OutputArrayOfAdGroupCriterion(IList<AdGroupCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupCriterionAction(AdGroupCriterionAction dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupCriterionAction * * *");
                OutputStatusMessage(string.Format("Action: {0}", dataObject.Action));
                OutputStatusMessage("AdGroupCriterion:");
                OutputAdGroupCriterion(dataObject.AdGroupCriterion);
                OutputStatusMessage("* * * End OutputAdGroupCriterionAction * * *");
            }
        }
        public void OutputArrayOfAdGroupCriterionAction(IList<AdGroupCriterionAction> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupCriterionAction(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupNegativeSites(AdGroupNegativeSites dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupNegativeSites * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage("NegativeSites:");
                OutputArrayOfString(dataObject.NegativeSites);
                OutputStatusMessage("* * * End OutputAdGroupNegativeSites * * *");
            }
        }
        public void OutputArrayOfAdGroupNegativeSites(IList<AdGroupNegativeSites> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupNegativeSites(dataObject);
                    }
                }
            }
        }
        public void OutputAdRotation(AdRotation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdRotation * * *");
                OutputStatusMessage(string.Format("EndDate: {0}", dataObject.EndDate));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage("* * * End OutputAdRotation * * *");
            }
        }
        public void OutputArrayOfAdRotation(IList<AdRotation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdRotation(dataObject);
                    }
                }
            }
        }
        public void OutputAgeCriterion(AgeCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAgeCriterion * * *");
                OutputStatusMessage(string.Format("AgeRange: {0}", dataObject.AgeRange));
                OutputStatusMessage("* * * End OutputAgeCriterion * * *");
            }
        }
        public void OutputArrayOfAgeCriterion(IList<AgeCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAgeCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiFaultDetail * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                OutputStatusMessage("* * * End OutputApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputAppAdExtension(AppAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAppAdExtension * * *");
                OutputStatusMessage(string.Format("AppPlatform: {0}", dataObject.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", dataObject.AppStoreId));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayText: {0}", dataObject.DisplayText));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputAppAdExtension * * *");
            }
        }
        public void OutputArrayOfAppAdExtension(IList<AppAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAppAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputAppInstallAd(AppInstallAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAppInstallAd * * *");
                OutputStatusMessage(string.Format("AppPlatform: {0}", dataObject.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", dataObject.AppStoreId));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("Title: {0}", dataObject.Title));
                OutputStatusMessage("* * * End OutputAppInstallAd * * *");
            }
        }
        public void OutputArrayOfAppInstallAd(IList<AppInstallAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAppInstallAd(dataObject);
                    }
                }
            }
        }
        public void OutputAppInstallGoal(AppInstallGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAppInstallGoal * * *");
                OutputStatusMessage(string.Format("AppPlatform: {0}", dataObject.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", dataObject.AppStoreId));
                OutputStatusMessage("* * * End OutputAppInstallGoal * * *");
            }
        }
        public void OutputArrayOfAppInstallGoal(IList<AppInstallGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAppInstallGoal(dataObject);
                    }
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApplicationFault * * *");
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(null != adapifaultdetail)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(null != apifaultdetail)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
                var editorialapifaultdetail = dataObject as EditorialApiFaultDetail;
                if(null != editorialapifaultdetail)
                {
                    OutputEditorialApiFaultDetail((EditorialApiFaultDetail)dataObject);
                }
                OutputStatusMessage("* * * End OutputApplicationFault * * *");
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApplicationFault(dataObject);
                    }
                }
            }
        }
        public void OutputAppUrl(AppUrl dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAppUrl * * *");
                OutputStatusMessage(string.Format("OsType: {0}", dataObject.OsType));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                OutputStatusMessage("* * * End OutputAppUrl * * *");
            }
        }
        public void OutputArrayOfAppUrl(IList<AppUrl> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAppUrl(dataObject);
                    }
                }
            }
        }
        public void OutputAsset(Asset dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAsset * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var imageasset = dataObject as ImageAsset;
                if(null != imageasset)
                {
                    OutputImageAsset((ImageAsset)dataObject);
                }
                var textasset = dataObject as TextAsset;
                if(null != textasset)
                {
                    OutputTextAsset((TextAsset)dataObject);
                }
                var videoasset = dataObject as VideoAsset;
                if(null != videoasset)
                {
                    OutputVideoAsset((VideoAsset)dataObject);
                }
                OutputStatusMessage("* * * End OutputAsset * * *");
            }
        }
        public void OutputArrayOfAsset(IList<Asset> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAsset(dataObject);
                    }
                }
            }
        }
        public void OutputAssetLink(AssetLink dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAssetLink * * *");
                OutputStatusMessage("Asset:");
                OutputAsset(dataObject.Asset);
                OutputStatusMessage(string.Format("AssetPerformanceLabel: {0}", dataObject.AssetPerformanceLabel));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputStatusMessage(string.Format("PinnedField: {0}", dataObject.PinnedField));
                OutputStatusMessage("* * * End OutputAssetLink * * *");
            }
        }
        public void OutputArrayOfAssetLink(IList<AssetLink> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAssetLink(dataObject);
                    }
                }
            }
        }
        public void OutputAudience(Audience dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAudience * * *");
                OutputStatusMessage(string.Format("AudienceNetworkSize: {0}", dataObject.AudienceNetworkSize));
                OutputStatusMessage("CustomerShare:");
                OutputCustomerShare(dataObject.CustomerShare);
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MembershipDuration: {0}", dataObject.MembershipDuration));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("ParentId: {0}", dataObject.ParentId));
                OutputStatusMessage(string.Format("Scope: {0}", dataObject.Scope));
                OutputStatusMessage(string.Format("SearchSize: {0}", dataObject.SearchSize));
                OutputStatusMessage("SupportedCampaignTypes:");
                OutputArrayOfString(dataObject.SupportedCampaignTypes);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var combinedlist = dataObject as CombinedList;
                if(null != combinedlist)
                {
                    OutputCombinedList((CombinedList)dataObject);
                }
                var customaudience = dataObject as CustomAudience;
                if(null != customaudience)
                {
                    OutputCustomAudience((CustomAudience)dataObject);
                }
                var inmarketaudience = dataObject as InMarketAudience;
                if(null != inmarketaudience)
                {
                    OutputInMarketAudience((InMarketAudience)dataObject);
                }
                var productaudience = dataObject as ProductAudience;
                if(null != productaudience)
                {
                    OutputProductAudience((ProductAudience)dataObject);
                }
                var remarketinglist = dataObject as RemarketingList;
                if(null != remarketinglist)
                {
                    OutputRemarketingList((RemarketingList)dataObject);
                }
                var similarremarketinglist = dataObject as SimilarRemarketingList;
                if(null != similarremarketinglist)
                {
                    OutputSimilarRemarketingList((SimilarRemarketingList)dataObject);
                }
                OutputStatusMessage("* * * End OutputAudience * * *");
            }
        }
        public void OutputArrayOfAudience(IList<Audience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAudience(dataObject);
                    }
                }
            }
        }
        public void OutputAudienceCriterion(AudienceCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAudienceCriterion * * *");
                OutputStatusMessage(string.Format("AudienceId: {0}", dataObject.AudienceId));
                OutputStatusMessage(string.Format("AudienceType: {0}", dataObject.AudienceType));
                OutputStatusMessage("* * * End OutputAudienceCriterion * * *");
            }
        }
        public void OutputArrayOfAudienceCriterion(IList<AudienceCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAudienceCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBatchError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("FieldPath: {0}", dataObject.FieldPath));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var editorialerror = dataObject as EditorialError;
                if(null != editorialerror)
                {
                    OutputEditorialError((EditorialError)dataObject);
                }
                OutputStatusMessage("* * * End OutputBatchError * * *");
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBatchError(dataObject);
                    }
                }
            }
        }
        public void OutputBatchErrorCollection(BatchErrorCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBatchErrorCollection * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("FieldPath: {0}", dataObject.FieldPath));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var editorialerrorcollection = dataObject as EditorialErrorCollection;
                if(null != editorialerrorcollection)
                {
                    OutputEditorialErrorCollection((EditorialErrorCollection)dataObject);
                }
                OutputStatusMessage("* * * End OutputBatchErrorCollection * * *");
            }
        }
        public void OutputArrayOfBatchErrorCollection(IList<BatchErrorCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBatchErrorCollection(dataObject);
                    }
                }
            }
        }
        public void OutputBid(Bid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBid * * *");
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage("* * * End OutputBid * * *");
            }
        }
        public void OutputArrayOfBid(IList<Bid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBid(dataObject);
                    }
                }
            }
        }
        public void OutputBiddableAdGroupCriterion(BiddableAdGroupCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBiddableAdGroupCriterion * * *");
                OutputStatusMessage("CriterionBid:");
                OutputCriterionBid(dataObject.CriterionBid);
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("CriterionCashback:");
                OutputCriterionCashback(dataObject.CriterionCashback);
                OutputStatusMessage("* * * End OutputBiddableAdGroupCriterion * * *");
            }
        }
        public void OutputArrayOfBiddableAdGroupCriterion(IList<BiddableAdGroupCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBiddableAdGroupCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputBiddableCampaignCriterion(BiddableCampaignCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBiddableCampaignCriterion * * *");
                OutputStatusMessage("CriterionBid:");
                OutputCriterionBid(dataObject.CriterionBid);
                OutputStatusMessage("CriterionCashback:");
                OutputCriterionCashback(dataObject.CriterionCashback);
                OutputStatusMessage("* * * End OutputBiddableCampaignCriterion * * *");
            }
        }
        public void OutputArrayOfBiddableCampaignCriterion(IList<BiddableCampaignCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBiddableCampaignCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputBiddingScheme(BiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBiddingScheme * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var enhancedcpcbiddingscheme = dataObject as EnhancedCpcBiddingScheme;
                if(null != enhancedcpcbiddingscheme)
                {
                    OutputEnhancedCpcBiddingScheme((EnhancedCpcBiddingScheme)dataObject);
                }
                var inheritfromparentbiddingscheme = dataObject as InheritFromParentBiddingScheme;
                if(null != inheritfromparentbiddingscheme)
                {
                    OutputInheritFromParentBiddingScheme((InheritFromParentBiddingScheme)dataObject);
                }
                var manualcpcbiddingscheme = dataObject as ManualCpcBiddingScheme;
                if(null != manualcpcbiddingscheme)
                {
                    OutputManualCpcBiddingScheme((ManualCpcBiddingScheme)dataObject);
                }
                var manualcpmbiddingscheme = dataObject as ManualCpmBiddingScheme;
                if(null != manualcpmbiddingscheme)
                {
                    OutputManualCpmBiddingScheme((ManualCpmBiddingScheme)dataObject);
                }
                var manualcpvbiddingscheme = dataObject as ManualCpvBiddingScheme;
                if(null != manualcpvbiddingscheme)
                {
                    OutputManualCpvBiddingScheme((ManualCpvBiddingScheme)dataObject);
                }
                var maxclicksbiddingscheme = dataObject as MaxClicksBiddingScheme;
                if(null != maxclicksbiddingscheme)
                {
                    OutputMaxClicksBiddingScheme((MaxClicksBiddingScheme)dataObject);
                }
                var maxconversionsbiddingscheme = dataObject as MaxConversionsBiddingScheme;
                if(null != maxconversionsbiddingscheme)
                {
                    OutputMaxConversionsBiddingScheme((MaxConversionsBiddingScheme)dataObject);
                }
                var maxconversionvaluebiddingscheme = dataObject as MaxConversionValueBiddingScheme;
                if(null != maxconversionvaluebiddingscheme)
                {
                    OutputMaxConversionValueBiddingScheme((MaxConversionValueBiddingScheme)dataObject);
                }
                var maxroasbiddingscheme = dataObject as MaxRoasBiddingScheme;
                if(null != maxroasbiddingscheme)
                {
                    OutputMaxRoasBiddingScheme((MaxRoasBiddingScheme)dataObject);
                }
                var targetcpabiddingscheme = dataObject as TargetCpaBiddingScheme;
                if(null != targetcpabiddingscheme)
                {
                    OutputTargetCpaBiddingScheme((TargetCpaBiddingScheme)dataObject);
                }
                var targetimpressionsharebiddingscheme = dataObject as TargetImpressionShareBiddingScheme;
                if(null != targetimpressionsharebiddingscheme)
                {
                    OutputTargetImpressionShareBiddingScheme((TargetImpressionShareBiddingScheme)dataObject);
                }
                var targetroasbiddingscheme = dataObject as TargetRoasBiddingScheme;
                if(null != targetroasbiddingscheme)
                {
                    OutputTargetRoasBiddingScheme((TargetRoasBiddingScheme)dataObject);
                }
                OutputStatusMessage("* * * End OutputBiddingScheme * * *");
            }
        }
        public void OutputArrayOfBiddingScheme(IList<BiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputBidMultiplier(BidMultiplier dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBidMultiplier * * *");
                OutputStatusMessage(string.Format("Multiplier: {0}", dataObject.Multiplier));
                OutputStatusMessage("* * * End OutputBidMultiplier * * *");
            }
        }
        public void OutputArrayOfBidMultiplier(IList<BidMultiplier> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBidMultiplier(dataObject);
                    }
                }
            }
        }
        public void OutputBidStrategy(BidStrategy dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBidStrategy * * *");
                OutputStatusMessage(string.Format("AssociatedCampaignType: {0}", dataObject.AssociatedCampaignType));
                OutputStatusMessage(string.Format("AssociationCount: {0}", dataObject.AssociationCount));
                OutputStatusMessage("BiddingScheme:");
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage("* * * End OutputBidStrategy * * *");
            }
        }
        public void OutputArrayOfBidStrategy(IList<BidStrategy> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBidStrategy(dataObject);
                    }
                }
            }
        }
        public void OutputBMCStore(BMCStore dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBMCStore * * *");
                OutputStatusMessage(string.Format("HasCatalog: {0}", dataObject.HasCatalog));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("IsActive: {0}", dataObject.IsActive));
                OutputStatusMessage(string.Format("IsProductAdsEnabled: {0}", dataObject.IsProductAdsEnabled));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("SubType: {0}", dataObject.SubType));
                OutputStatusMessage("* * * End OutputBMCStore * * *");
            }
        }
        public void OutputArrayOfBMCStore(IList<BMCStore> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBMCStore(dataObject);
                    }
                }
            }
        }
        public void OutputBudget(Budget dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBudget * * *");
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage(string.Format("AssociationCount: {0}", dataObject.AssociationCount));
                OutputStatusMessage(string.Format("BudgetType: {0}", dataObject.BudgetType));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage("* * * End OutputBudget * * *");
            }
        }
        public void OutputArrayOfBudget(IList<Budget> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBudget(dataObject);
                    }
                }
            }
        }
        public void OutputCallAdExtension(CallAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCallAdExtension * * *");
                OutputStatusMessage(string.Format("CountryCode: {0}", dataObject.CountryCode));
                OutputStatusMessage(string.Format("IsCallOnly: {0}", dataObject.IsCallOnly));
                OutputStatusMessage(string.Format("IsCallTrackingEnabled: {0}", dataObject.IsCallTrackingEnabled));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", dataObject.PhoneNumber));
                OutputStatusMessage(string.Format("RequireTollFreeTrackingNumber: {0}", dataObject.RequireTollFreeTrackingNumber));
                OutputStatusMessage("* * * End OutputCallAdExtension * * *");
            }
        }
        public void OutputArrayOfCallAdExtension(IList<CallAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCallAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputCalloutAdExtension(CalloutAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCalloutAdExtension * * *");
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage("* * * End OutputCalloutAdExtension * * *");
            }
        }
        public void OutputArrayOfCalloutAdExtension(IList<CalloutAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCalloutAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputCampaign(Campaign dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaign * * *");
                OutputStatusMessage(string.Format("AudienceAdsBidAdjustment: {0}", dataObject.AudienceAdsBidAdjustment));
                OutputStatusMessage("BiddingScheme:");
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputStatusMessage(string.Format("BudgetType: {0}", dataObject.BudgetType));
                OutputStatusMessage(string.Format("DailyBudget: {0}", dataObject.DailyBudget));
                OutputStatusMessage(string.Format("ExperimentId: {0}", dataObject.ExperimentId));
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MultimediaAdsBidAdjustment: {0}", dataObject.MultimediaAdsBidAdjustment));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("SubType: {0}", dataObject.SubType));
                OutputStatusMessage(string.Format("TimeZone: {0}", dataObject.TimeZone));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage(string.Format("CampaignType: {0}", dataObject.CampaignType));
                OutputStatusMessage("Settings:");
                OutputArrayOfSetting(dataObject.Settings);
                OutputStatusMessage(string.Format("BudgetId: {0}", dataObject.BudgetId));
                OutputStatusMessage("Languages:");
                OutputArrayOfString(dataObject.Languages);
                OutputStatusMessage(string.Format("AdScheduleUseSearcherTimeZone: {0}", dataObject.AdScheduleUseSearcherTimeZone));
                OutputStatusMessage(string.Format("BidStrategyId: {0}", dataObject.BidStrategyId));
                OutputStatusMessage("* * * End OutputCampaign * * *");
            }
        }
        public void OutputArrayOfCampaign(IList<Campaign> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaign(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignAdGroupIds(CampaignAdGroupIds dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignAdGroupIds * * *");
                OutputStatusMessage(string.Format("ActiveAdGroupsOnly: {0}", dataObject.ActiveAdGroupsOnly));
                OutputStatusMessage("AdGroupIds:");
                OutputArrayOfLong(dataObject.AdGroupIds);
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage("* * * End OutputCampaignAdGroupIds * * *");
            }
        }
        public void OutputArrayOfCampaignAdGroupIds(IList<CampaignAdGroupIds> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignAdGroupIds(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignCriterion(CampaignCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignCriterion * * *");
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage("Criterion:");
                OutputCriterion(dataObject.Criterion);
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var biddablecampaigncriterion = dataObject as BiddableCampaignCriterion;
                if(null != biddablecampaigncriterion)
                {
                    OutputBiddableCampaignCriterion((BiddableCampaignCriterion)dataObject);
                }
                var negativecampaigncriterion = dataObject as NegativeCampaignCriterion;
                if(null != negativecampaigncriterion)
                {
                    OutputNegativeCampaignCriterion((NegativeCampaignCriterion)dataObject);
                }
                OutputStatusMessage("* * * End OutputCampaignCriterion * * *");
            }
        }
        public void OutputArrayOfCampaignCriterion(IList<CampaignCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignNegativeSites(CampaignNegativeSites dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignNegativeSites * * *");
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage("NegativeSites:");
                OutputArrayOfString(dataObject.NegativeSites);
                OutputStatusMessage("* * * End OutputCampaignNegativeSites * * *");
            }
        }
        public void OutputArrayOfCampaignNegativeSites(IList<CampaignNegativeSites> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignNegativeSites(dataObject);
                    }
                }
            }
        }
        public void OutputCashbackAdjustment(CashbackAdjustment dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCashbackAdjustment * * *");
                OutputStatusMessage(string.Format("CashbackPercent: {0}", dataObject.CashbackPercent));
                OutputStatusMessage("* * * End OutputCashbackAdjustment * * *");
            }
        }
        public void OutputArrayOfCashbackAdjustment(IList<CashbackAdjustment> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCashbackAdjustment(dataObject);
                    }
                }
            }
        }
        public void OutputCombinationRule(CombinationRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCombinationRule * * *");
                OutputStatusMessage("AudienceIds:");
                OutputArrayOfLong(dataObject.AudienceIds);
                OutputStatusMessage(string.Format("Operator: {0}", dataObject.Operator));
                OutputStatusMessage("* * * End OutputCombinationRule * * *");
            }
        }
        public void OutputArrayOfCombinationRule(IList<CombinationRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCombinationRule(dataObject);
                    }
                }
            }
        }
        public void OutputCombinedList(CombinedList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCombinedList * * *");
                OutputStatusMessage("CombinationRules:");
                OutputArrayOfCombinationRule(dataObject.CombinationRules);
                OutputStatusMessage("* * * End OutputCombinedList * * *");
            }
        }
        public void OutputArrayOfCombinedList(IList<CombinedList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCombinedList(dataObject);
                    }
                }
            }
        }
        public void OutputCompany(Company dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCompany * * *");
                OutputStatusMessage(string.Format("LogoUrl: {0}", dataObject.LogoUrl));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("ProfileId: {0}", dataObject.ProfileId));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputCompany * * *");
            }
        }
        public void OutputArrayOfCompany(IList<Company> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCompany(dataObject);
                    }
                }
            }
        }
        public void OutputConversionGoal(ConversionGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputConversionGoal * * *");
                OutputStatusMessage(string.Format("ConversionWindowInMinutes: {0}", dataObject.ConversionWindowInMinutes));
                OutputStatusMessage(string.Format("CountType: {0}", dataObject.CountType));
                OutputStatusMessage(string.Format("ExcludeFromBidding: {0}", dataObject.ExcludeFromBidding));
                OutputStatusMessage(string.Format("GoalCategory: {0}", dataObject.GoalCategory));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage("Revenue:");
                OutputConversionGoalRevenue(dataObject.Revenue);
                OutputStatusMessage(string.Format("Scope: {0}", dataObject.Scope));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("TagId: {0}", dataObject.TagId));
                OutputStatusMessage(string.Format("TrackingStatus: {0}", dataObject.TrackingStatus));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("ViewThroughConversionWindowInMinutes: {0}", dataObject.ViewThroughConversionWindowInMinutes));
                var appinstallgoal = dataObject as AppInstallGoal;
                if(null != appinstallgoal)
                {
                    OutputAppInstallGoal((AppInstallGoal)dataObject);
                }
                var durationgoal = dataObject as DurationGoal;
                if(null != durationgoal)
                {
                    OutputDurationGoal((DurationGoal)dataObject);
                }
                var eventgoal = dataObject as EventGoal;
                if(null != eventgoal)
                {
                    OutputEventGoal((EventGoal)dataObject);
                }
                var instoretransactiongoal = dataObject as InStoreTransactionGoal;
                if(null != instoretransactiongoal)
                {
                    OutputInStoreTransactionGoal((InStoreTransactionGoal)dataObject);
                }
                var offlineconversiongoal = dataObject as OfflineConversionGoal;
                if(null != offlineconversiongoal)
                {
                    OutputOfflineConversionGoal((OfflineConversionGoal)dataObject);
                }
                var pagesviewedpervisitgoal = dataObject as PagesViewedPerVisitGoal;
                if(null != pagesviewedpervisitgoal)
                {
                    OutputPagesViewedPerVisitGoal((PagesViewedPerVisitGoal)dataObject);
                }
                var urlgoal = dataObject as UrlGoal;
                if(null != urlgoal)
                {
                    OutputUrlGoal((UrlGoal)dataObject);
                }
                OutputStatusMessage("* * * End OutputConversionGoal * * *");
            }
        }
        public void OutputArrayOfConversionGoal(IList<ConversionGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputConversionGoal(dataObject);
                    }
                }
            }
        }
        public void OutputConversionGoalRevenue(ConversionGoalRevenue dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputConversionGoalRevenue * * *");
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage("* * * End OutputConversionGoalRevenue * * *");
            }
        }
        public void OutputArrayOfConversionGoalRevenue(IList<ConversionGoalRevenue> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputConversionGoalRevenue(dataObject);
                    }
                }
            }
        }
        public void OutputCoOpSetting(CoOpSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCoOpSetting * * *");
                OutputStatusMessage(string.Format("BidBoostValue: {0}", dataObject.BidBoostValue));
                OutputStatusMessage(string.Format("BidMaxValue: {0}", dataObject.BidMaxValue));
                OutputStatusMessage(string.Format("BidOption: {0}", dataObject.BidOption));
                OutputStatusMessage("* * * End OutputCoOpSetting * * *");
            }
        }
        public void OutputArrayOfCoOpSetting(IList<CoOpSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCoOpSetting(dataObject);
                    }
                }
            }
        }
        public void OutputCriterion(Criterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCriterion * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var agecriterion = dataObject as AgeCriterion;
                if(null != agecriterion)
                {
                    OutputAgeCriterion((AgeCriterion)dataObject);
                }
                var audiencecriterion = dataObject as AudienceCriterion;
                if(null != audiencecriterion)
                {
                    OutputAudienceCriterion((AudienceCriterion)dataObject);
                }
                var daytimecriterion = dataObject as DayTimeCriterion;
                if(null != daytimecriterion)
                {
                    OutputDayTimeCriterion((DayTimeCriterion)dataObject);
                }
                var devicecriterion = dataObject as DeviceCriterion;
                if(null != devicecriterion)
                {
                    OutputDeviceCriterion((DeviceCriterion)dataObject);
                }
                var gendercriterion = dataObject as GenderCriterion;
                if(null != gendercriterion)
                {
                    OutputGenderCriterion((GenderCriterion)dataObject);
                }
                var locationcriterion = dataObject as LocationCriterion;
                if(null != locationcriterion)
                {
                    OutputLocationCriterion((LocationCriterion)dataObject);
                }
                var locationintentcriterion = dataObject as LocationIntentCriterion;
                if(null != locationintentcriterion)
                {
                    OutputLocationIntentCriterion((LocationIntentCriterion)dataObject);
                }
                var productpartition = dataObject as ProductPartition;
                if(null != productpartition)
                {
                    OutputProductPartition((ProductPartition)dataObject);
                }
                var productscope = dataObject as ProductScope;
                if(null != productscope)
                {
                    OutputProductScope((ProductScope)dataObject);
                }
                var profilecriterion = dataObject as ProfileCriterion;
                if(null != profilecriterion)
                {
                    OutputProfileCriterion((ProfileCriterion)dataObject);
                }
                var radiuscriterion = dataObject as RadiusCriterion;
                if(null != radiuscriterion)
                {
                    OutputRadiusCriterion((RadiusCriterion)dataObject);
                }
                var storecriterion = dataObject as StoreCriterion;
                if(null != storecriterion)
                {
                    OutputStoreCriterion((StoreCriterion)dataObject);
                }
                var webpage = dataObject as Webpage;
                if(null != webpage)
                {
                    OutputWebpage((Webpage)dataObject);
                }
                OutputStatusMessage("* * * End OutputCriterion * * *");
            }
        }
        public void OutputArrayOfCriterion(IList<Criterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputCriterionBid(CriterionBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCriterionBid * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var bidmultiplier = dataObject as BidMultiplier;
                if(null != bidmultiplier)
                {
                    OutputBidMultiplier((BidMultiplier)dataObject);
                }
                var fixedbid = dataObject as FixedBid;
                if(null != fixedbid)
                {
                    OutputFixedBid((FixedBid)dataObject);
                }
                OutputStatusMessage("* * * End OutputCriterionBid * * *");
            }
        }
        public void OutputArrayOfCriterionBid(IList<CriterionBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCriterionBid(dataObject);
                    }
                }
            }
        }
        public void OutputCriterionCashback(CriterionCashback dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCriterionCashback * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var cashbackadjustment = dataObject as CashbackAdjustment;
                if(null != cashbackadjustment)
                {
                    OutputCashbackAdjustment((CashbackAdjustment)dataObject);
                }
                OutputStatusMessage("* * * End OutputCriterionCashback * * *");
            }
        }
        public void OutputArrayOfCriterionCashback(IList<CriterionCashback> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCriterionCashback(dataObject);
                    }
                }
            }
        }
        public void OutputCustomAudience(CustomAudience dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomAudience * * *");
                OutputStatusMessage("* * * End OutputCustomAudience * * *");
            }
        }
        public void OutputArrayOfCustomAudience(IList<CustomAudience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomAudience(dataObject);
                    }
                }
            }
        }
        public void OutputCustomerAccountShare(CustomerAccountShare dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomerAccountShare * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage("Associations:");
                OutputArrayOfCustomerAccountShareAssociation(dataObject.Associations);
                OutputStatusMessage(string.Format("CustomerId: {0}", dataObject.CustomerId));
                OutputStatusMessage("* * * End OutputCustomerAccountShare * * *");
            }
        }
        public void OutputArrayOfCustomerAccountShare(IList<CustomerAccountShare> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomerAccountShare(dataObject);
                    }
                }
            }
        }
        public void OutputCustomerAccountShareAssociation(CustomerAccountShareAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomerAccountShareAssociation * * *");
                OutputStatusMessage(string.Format("AssociationCount: {0}", dataObject.AssociationCount));
                OutputStatusMessage(string.Format("UsageType: {0}", dataObject.UsageType));
                OutputStatusMessage("* * * End OutputCustomerAccountShareAssociation * * *");
            }
        }
        public void OutputArrayOfCustomerAccountShareAssociation(IList<CustomerAccountShareAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomerAccountShareAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputCustomerShare(CustomerShare dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomerShare * * *");
                OutputStatusMessage("CustomerAccountShares:");
                OutputArrayOfCustomerAccountShare(dataObject.CustomerAccountShares);
                OutputStatusMessage(string.Format("OwnerCustomerId: {0}", dataObject.OwnerCustomerId));
                OutputStatusMessage("* * * End OutputCustomerShare * * *");
            }
        }
        public void OutputArrayOfCustomerShare(IList<CustomerShare> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomerShare(dataObject);
                    }
                }
            }
        }
        public void OutputCustomEventsRule(CustomEventsRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomEventsRule * * *");
                OutputStatusMessage(string.Format("Action: {0}", dataObject.Action));
                OutputStatusMessage(string.Format("ActionOperator: {0}", dataObject.ActionOperator));
                OutputStatusMessage(string.Format("Category: {0}", dataObject.Category));
                OutputStatusMessage(string.Format("CategoryOperator: {0}", dataObject.CategoryOperator));
                OutputStatusMessage(string.Format("Label: {0}", dataObject.Label));
                OutputStatusMessage(string.Format("LabelOperator: {0}", dataObject.LabelOperator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage(string.Format("ValueOperator: {0}", dataObject.ValueOperator));
                OutputStatusMessage("* * * End OutputCustomEventsRule * * *");
            }
        }
        public void OutputArrayOfCustomEventsRule(IList<CustomEventsRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomEventsRule(dataObject);
                    }
                }
            }
        }
        public void OutputCustomParameter(CustomParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomParameter * * *");
                OutputStatusMessage(string.Format("Key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage("* * * End OutputCustomParameter * * *");
            }
        }
        public void OutputArrayOfCustomParameter(IList<CustomParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomParameter(dataObject);
                    }
                }
            }
        }
        public void OutputCustomParameters(CustomParameters dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCustomParameters * * *");
                OutputStatusMessage("Parameters:");
                OutputArrayOfCustomParameter(dataObject.Parameters);
                OutputStatusMessage("* * * End OutputCustomParameters * * *");
            }
        }
        public void OutputArrayOfCustomParameters(IList<CustomParameters> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCustomParameters(dataObject);
                    }
                }
            }
        }
        public void OutputDate(Date dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDate * * *");
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
                OutputStatusMessage("* * * End OutputDate * * *");
            }
        }
        public void OutputArrayOfDate(IList<Date> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDate(dataObject);
                    }
                }
            }
        }
        public void OutputDayTime(DayTime dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDayTime * * *");
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("EndHour: {0}", dataObject.EndHour));
                OutputStatusMessage(string.Format("EndMinute: {0}", dataObject.EndMinute));
                OutputStatusMessage(string.Format("StartHour: {0}", dataObject.StartHour));
                OutputStatusMessage(string.Format("StartMinute: {0}", dataObject.StartMinute));
                OutputStatusMessage("* * * End OutputDayTime * * *");
            }
        }
        public void OutputArrayOfDayTime(IList<DayTime> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDayTime(dataObject);
                    }
                }
            }
        }
        public void OutputDayTimeCriterion(DayTimeCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDayTimeCriterion * * *");
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("FromHour: {0}", dataObject.FromHour));
                OutputStatusMessage(string.Format("FromMinute: {0}", dataObject.FromMinute));
                OutputStatusMessage(string.Format("ToHour: {0}", dataObject.ToHour));
                OutputStatusMessage(string.Format("ToMinute: {0}", dataObject.ToMinute));
                OutputStatusMessage("* * * End OutputDayTimeCriterion * * *");
            }
        }
        public void OutputArrayOfDayTimeCriterion(IList<DayTimeCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDayTimeCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputDeviceCriterion(DeviceCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDeviceCriterion * * *");
                OutputStatusMessage(string.Format("DeviceName: {0}", dataObject.DeviceName));
                OutputStatusMessage(string.Format("OSName: {0}", dataObject.OSName));
                OutputStatusMessage("* * * End OutputDeviceCriterion * * *");
            }
        }
        public void OutputArrayOfDeviceCriterion(IList<DeviceCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDeviceCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputDurationGoal(DurationGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDurationGoal * * *");
                OutputStatusMessage(string.Format("MinimumDurationInSeconds: {0}", dataObject.MinimumDurationInSeconds));
                OutputStatusMessage("* * * End OutputDurationGoal * * *");
            }
        }
        public void OutputArrayOfDurationGoal(IList<DurationGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDurationGoal(dataObject);
                    }
                }
            }
        }
        public void OutputDynamicFeedSetting(DynamicFeedSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDynamicFeedSetting * * *");
                OutputStatusMessage(string.Format("FeedId: {0}", dataObject.FeedId));
                OutputStatusMessage("* * * End OutputDynamicFeedSetting * * *");
            }
        }
        public void OutputArrayOfDynamicFeedSetting(IList<DynamicFeedSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDynamicFeedSetting(dataObject);
                    }
                }
            }
        }
        public void OutputDynamicSearchAd(DynamicSearchAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDynamicSearchAd * * *");
                OutputStatusMessage(string.Format("Path1: {0}", dataObject.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", dataObject.Path2));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("TextPart2: {0}", dataObject.TextPart2));
                OutputStatusMessage("* * * End OutputDynamicSearchAd * * *");
            }
        }
        public void OutputArrayOfDynamicSearchAd(IList<DynamicSearchAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDynamicSearchAd(dataObject);
                    }
                }
            }
        }
        public void OutputDynamicSearchAdsSetting(DynamicSearchAdsSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDynamicSearchAdsSetting * * *");
                OutputStatusMessage(string.Format("DomainName: {0}", dataObject.DomainName));
                OutputStatusMessage(string.Format("DynamicDescriptionEnabled: {0}", dataObject.DynamicDescriptionEnabled));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("PageFeedIds:");
                OutputArrayOfLong(dataObject.PageFeedIds);
                OutputStatusMessage(string.Format("Source: {0}", dataObject.Source));
                OutputStatusMessage("* * * End OutputDynamicSearchAdsSetting * * *");
            }
        }
        public void OutputArrayOfDynamicSearchAdsSetting(IList<DynamicSearchAdsSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDynamicSearchAdsSetting(dataObject);
                    }
                }
            }
        }
        public void OutputEditorialApiFaultDetail(EditorialApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEditorialApiFaultDetail * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage("EditorialErrors:");
                OutputArrayOfEditorialError(dataObject.EditorialErrors);
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                OutputStatusMessage("* * * End OutputEditorialApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfEditorialApiFaultDetail(IList<EditorialApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEditorialApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputEditorialError(EditorialError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEditorialError * * *");
                OutputStatusMessage(string.Format("Appealable: {0}", dataObject.Appealable));
                OutputStatusMessage(string.Format("DisapprovedText: {0}", dataObject.DisapprovedText));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("PublisherCountry: {0}", dataObject.PublisherCountry));
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage("* * * End OutputEditorialError * * *");
            }
        }
        public void OutputArrayOfEditorialError(IList<EditorialError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEditorialError(dataObject);
                    }
                }
            }
        }
        public void OutputEditorialErrorCollection(EditorialErrorCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEditorialErrorCollection * * *");
                OutputStatusMessage(string.Format("Appealable: {0}", dataObject.Appealable));
                OutputStatusMessage(string.Format("DisapprovedText: {0}", dataObject.DisapprovedText));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("PublisherCountry: {0}", dataObject.PublisherCountry));
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage("* * * End OutputEditorialErrorCollection * * *");
            }
        }
        public void OutputArrayOfEditorialErrorCollection(IList<EditorialErrorCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEditorialErrorCollection(dataObject);
                    }
                }
            }
        }
        public void OutputEditorialReason(EditorialReason dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEditorialReason * * *");
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage("PublisherCountries:");
                OutputArrayOfString(dataObject.PublisherCountries);
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage(string.Format("Term: {0}", dataObject.Term));
                OutputStatusMessage("* * * End OutputEditorialReason * * *");
            }
        }
        public void OutputArrayOfEditorialReason(IList<EditorialReason> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEditorialReason(dataObject);
                    }
                }
            }
        }
        public void OutputEditorialReasonCollection(EditorialReasonCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEditorialReasonCollection * * *");
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdOrKeywordId: {0}", dataObject.AdOrKeywordId));
                OutputStatusMessage(string.Format("AppealStatus: {0}", dataObject.AppealStatus));
                OutputStatusMessage("Reasons:");
                OutputArrayOfEditorialReason(dataObject.Reasons);
                OutputStatusMessage("* * * End OutputEditorialReasonCollection * * *");
            }
        }
        public void OutputArrayOfEditorialReasonCollection(IList<EditorialReasonCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEditorialReasonCollection(dataObject);
                    }
                }
            }
        }
        public void OutputEnhancedCpcBiddingScheme(EnhancedCpcBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEnhancedCpcBiddingScheme * * *");
                OutputStatusMessage("* * * End OutputEnhancedCpcBiddingScheme * * *");
            }
        }
        public void OutputArrayOfEnhancedCpcBiddingScheme(IList<EnhancedCpcBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEnhancedCpcBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputEntityIdToParentIdAssociation(EntityIdToParentIdAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEntityIdToParentIdAssociation * * *");
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("ParentId: {0}", dataObject.ParentId));
                OutputStatusMessage("* * * End OutputEntityIdToParentIdAssociation * * *");
            }
        }
        public void OutputArrayOfEntityIdToParentIdAssociation(IList<EntityIdToParentIdAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEntityIdToParentIdAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputEntityNegativeKeyword(EntityNegativeKeyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEntityNegativeKeyword * * *");
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("EntityType: {0}", dataObject.EntityType));
                OutputStatusMessage("NegativeKeywords:");
                OutputArrayOfNegativeKeyword(dataObject.NegativeKeywords);
                OutputStatusMessage("* * * End OutputEntityNegativeKeyword * * *");
            }
        }
        public void OutputArrayOfEntityNegativeKeyword(IList<EntityNegativeKeyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEntityNegativeKeyword(dataObject);
                    }
                }
            }
        }
        public void OutputEventGoal(EventGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputEventGoal * * *");
                OutputStatusMessage(string.Format("ActionExpression: {0}", dataObject.ActionExpression));
                OutputStatusMessage(string.Format("ActionOperator: {0}", dataObject.ActionOperator));
                OutputStatusMessage(string.Format("CategoryExpression: {0}", dataObject.CategoryExpression));
                OutputStatusMessage(string.Format("CategoryOperator: {0}", dataObject.CategoryOperator));
                OutputStatusMessage(string.Format("LabelExpression: {0}", dataObject.LabelExpression));
                OutputStatusMessage(string.Format("LabelOperator: {0}", dataObject.LabelOperator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage(string.Format("ValueOperator: {0}", dataObject.ValueOperator));
                OutputStatusMessage("* * * End OutputEventGoal * * *");
            }
        }
        public void OutputArrayOfEventGoal(IList<EventGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputEventGoal(dataObject);
                    }
                }
            }
        }
        public void OutputExpandedTextAd(ExpandedTextAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputExpandedTextAd * * *");
                OutputStatusMessage(string.Format("Domain: {0}", dataObject.Domain));
                OutputStatusMessage(string.Format("Path1: {0}", dataObject.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", dataObject.Path2));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("TextPart2: {0}", dataObject.TextPart2));
                OutputStatusMessage(string.Format("TitlePart1: {0}", dataObject.TitlePart1));
                OutputStatusMessage(string.Format("TitlePart2: {0}", dataObject.TitlePart2));
                OutputStatusMessage(string.Format("TitlePart3: {0}", dataObject.TitlePart3));
                OutputStatusMessage("* * * End OutputExpandedTextAd * * *");
            }
        }
        public void OutputArrayOfExpandedTextAd(IList<ExpandedTextAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputExpandedTextAd(dataObject);
                    }
                }
            }
        }
        public void OutputExperiment(Experiment dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputExperiment * * *");
                OutputStatusMessage(string.Format("BaseCampaignId: {0}", dataObject.BaseCampaignId));
                OutputStatusMessage("EndDate:");
                OutputDate(dataObject.EndDate);
                OutputStatusMessage(string.Format("ExperimentCampaignId: {0}", dataObject.ExperimentCampaignId));
                OutputStatusMessage(string.Format("ExperimentStatus: {0}", dataObject.ExperimentStatus));
                OutputStatusMessage(string.Format("ExperimentType: {0}", dataObject.ExperimentType));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage("StartDate:");
                OutputDate(dataObject.StartDate);
                OutputStatusMessage(string.Format("TrafficSplitPercent: {0}", dataObject.TrafficSplitPercent));
                OutputStatusMessage("* * * End OutputExperiment * * *");
            }
        }
        public void OutputArrayOfExperiment(IList<Experiment> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputExperiment(dataObject);
                    }
                }
            }
        }
        public void OutputFileImportJob(FileImportJob dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputFileImportJob * * *");
                OutputStatusMessage(string.Format("FileSource: {0}", dataObject.FileSource));
                OutputStatusMessage(string.Format("FileUrl: {0}", dataObject.FileUrl));
                OutputStatusMessage("* * * End OutputFileImportJob * * *");
            }
        }
        public void OutputArrayOfFileImportJob(IList<FileImportJob> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputFileImportJob(dataObject);
                    }
                }
            }
        }
        public void OutputFileImportOption(FileImportOption dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputFileImportOption * * *");
                OutputStatusMessage("* * * End OutputFileImportOption * * *");
            }
        }
        public void OutputArrayOfFileImportOption(IList<FileImportOption> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputFileImportOption(dataObject);
                    }
                }
            }
        }
        public void OutputFilterLinkAdExtension(FilterLinkAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputFilterLinkAdExtension * * *");
                OutputStatusMessage(string.Format("AdExtensionHeaderType: {0}", dataObject.AdExtensionHeaderType));
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("Texts:");
                OutputArrayOfString(dataObject.Texts);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputFilterLinkAdExtension * * *");
            }
        }
        public void OutputArrayOfFilterLinkAdExtension(IList<FilterLinkAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputFilterLinkAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputFixedBid(FixedBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputFixedBid * * *");
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage("* * * End OutputFixedBid * * *");
            }
        }
        public void OutputArrayOfFixedBid(IList<FixedBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputFixedBid(dataObject);
                    }
                }
            }
        }
        public void OutputFlyerAdExtension(FlyerAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputFlyerAdExtension * * *");
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("FlyerName: {0}", dataObject.FlyerName));
                OutputStatusMessage("ImageMediaIds:");
                OutputArrayOfLong(dataObject.ImageMediaIds);
                OutputStatusMessage("ImageMediaUrls:");
                OutputArrayOfString(dataObject.ImageMediaUrls);
                OutputStatusMessage(string.Format("StoreId: {0}", dataObject.StoreId));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputFlyerAdExtension * * *");
            }
        }
        public void OutputArrayOfFlyerAdExtension(IList<FlyerAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputFlyerAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputFrequency(Frequency dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputFrequency * * *");
                OutputStatusMessage(string.Format("Cron: {0}", dataObject.Cron));
                OutputStatusMessage(string.Format("TimeZone: {0}", dataObject.TimeZone));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage("* * * End OutputFrequency * * *");
            }
        }
        public void OutputArrayOfFrequency(IList<Frequency> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputFrequency(dataObject);
                    }
                }
            }
        }
        public void OutputGenderCriterion(GenderCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGenderCriterion * * *");
                OutputStatusMessage(string.Format("GenderType: {0}", dataObject.GenderType));
                OutputStatusMessage("* * * End OutputGenderCriterion * * *");
            }
        }
        public void OutputArrayOfGenderCriterion(IList<GenderCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGenderCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputGeoPoint(GeoPoint dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGeoPoint * * *");
                OutputStatusMessage(string.Format("LatitudeInMicroDegrees: {0}", dataObject.LatitudeInMicroDegrees));
                OutputStatusMessage(string.Format("LongitudeInMicroDegrees: {0}", dataObject.LongitudeInMicroDegrees));
                OutputStatusMessage("* * * End OutputGeoPoint * * *");
            }
        }
        public void OutputArrayOfGeoPoint(IList<GeoPoint> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGeoPoint(dataObject);
                    }
                }
            }
        }
        public void OutputGoogleImportJob(GoogleImportJob dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGoogleImportJob * * *");
                OutputStatusMessage("CampaignAdGroupIds:");
                OutputArrayOfCampaignAdGroupIds(dataObject.CampaignAdGroupIds);
                OutputStatusMessage(string.Format("CredentialId: {0}", dataObject.CredentialId));
                OutputStatusMessage(string.Format("GoogleAccountId: {0}", dataObject.GoogleAccountId));
                OutputStatusMessage(string.Format("GoogleUserName: {0}", dataObject.GoogleUserName));
                OutputStatusMessage("* * * End OutputGoogleImportJob * * *");
            }
        }
        public void OutputArrayOfGoogleImportJob(IList<GoogleImportJob> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGoogleImportJob(dataObject);
                    }
                }
            }
        }
        public void OutputGoogleImportOption(GoogleImportOption dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGoogleImportOption * * *");
                OutputStatusMessage(string.Format("AccountUrlOptions: {0}", dataObject.AccountUrlOptions));
                OutputStatusMessage(string.Format("AdjustmentForBids: {0}", dataObject.AdjustmentForBids));
                OutputStatusMessage(string.Format("AdjustmentForCampaignBudgets: {0}", dataObject.AdjustmentForCampaignBudgets));
                OutputStatusMessage(string.Format("AssociatedStoreId: {0}", dataObject.AssociatedStoreId));
                OutputStatusMessage(string.Format("AssociatedUetTagId: {0}", dataObject.AssociatedUetTagId));
                OutputStatusMessage(string.Format("AutoDeviceBidOptimization: {0}", dataObject.AutoDeviceBidOptimization));
                OutputStatusMessage(string.Format("DeleteRemovedEntities: {0}", dataObject.DeleteRemovedEntities));
                OutputStatusMessage(string.Format("EnableAutoCurrencyConversion: {0}", dataObject.EnableAutoCurrencyConversion));
                OutputStatusMessage(string.Format("EnableParentLocationMapping: {0}", dataObject.EnableParentLocationMapping));
                OutputStatusMessage(string.Format("NewActiveAdsForExistingAdGroups: {0}", dataObject.NewActiveAdsForExistingAdGroups));
                OutputStatusMessage(string.Format("NewActiveCampaignsAndChildEntities: {0}", dataObject.NewActiveCampaignsAndChildEntities));
                OutputStatusMessage(string.Format("NewAdCustomizerFeeds: {0}", dataObject.NewAdCustomizerFeeds));
                OutputStatusMessage(string.Format("NewAdGroupsAndChildEntitiesForExistingCampaigns: {0}", dataObject.NewAdGroupsAndChildEntitiesForExistingCampaigns));
                OutputStatusMessage(string.Format("NewAdSchedules: {0}", dataObject.NewAdSchedules));
                OutputStatusMessage(string.Format("NewAppAdExtensions: {0}", dataObject.NewAppAdExtensions));
                OutputStatusMessage(string.Format("NewAudienceTargets: {0}", dataObject.NewAudienceTargets));
                OutputStatusMessage(string.Format("NewCallAdExtensions: {0}", dataObject.NewCallAdExtensions));
                OutputStatusMessage(string.Format("NewCalloutAdExtensions: {0}", dataObject.NewCalloutAdExtensions));
                OutputStatusMessage(string.Format("NewDemographicTargets: {0}", dataObject.NewDemographicTargets));
                OutputStatusMessage(string.Format("NewDeviceTargets: {0}", dataObject.NewDeviceTargets));
                OutputStatusMessage(string.Format("NewEntities: {0}", dataObject.NewEntities));
                OutputStatusMessage(string.Format("NewKeywordUrls: {0}", dataObject.NewKeywordUrls));
                OutputStatusMessage(string.Format("NewKeywordsForExistingAdGroups: {0}", dataObject.NewKeywordsForExistingAdGroups));
                OutputStatusMessage(string.Format("NewLabels: {0}", dataObject.NewLabels));
                OutputStatusMessage(string.Format("NewLocationAdExtensions: {0}", dataObject.NewLocationAdExtensions));
                OutputStatusMessage(string.Format("NewLocationTargets: {0}", dataObject.NewLocationTargets));
                OutputStatusMessage(string.Format("NewNegativeKeywordLists: {0}", dataObject.NewNegativeKeywordLists));
                OutputStatusMessage(string.Format("NewNegativeKeywordsForExistingParents: {0}", dataObject.NewNegativeKeywordsForExistingParents));
                OutputStatusMessage(string.Format("NewNegativeSites: {0}", dataObject.NewNegativeSites));
                OutputStatusMessage(string.Format("NewPageFeeds: {0}", dataObject.NewPageFeeds));
                OutputStatusMessage(string.Format("NewPausedAdsForExistingAdGroups: {0}", dataObject.NewPausedAdsForExistingAdGroups));
                OutputStatusMessage(string.Format("NewPausedCampaignsAndChildEntities: {0}", dataObject.NewPausedCampaignsAndChildEntities));
                OutputStatusMessage(string.Format("NewPriceAdExtensions: {0}", dataObject.NewPriceAdExtensions));
                OutputStatusMessage(string.Format("NewProductFilters: {0}", dataObject.NewProductFilters));
                OutputStatusMessage(string.Format("NewPromotionAdExtensions: {0}", dataObject.NewPromotionAdExtensions));
                OutputStatusMessage(string.Format("NewReviewAdExtensions: {0}", dataObject.NewReviewAdExtensions));
                OutputStatusMessage(string.Format("NewSitelinkAdExtensions: {0}", dataObject.NewSitelinkAdExtensions));
                OutputStatusMessage(string.Format("NewStructuredSnippetAdExtensions: {0}", dataObject.NewStructuredSnippetAdExtensions));
                OutputStatusMessage(string.Format("NewUrlOptions: {0}", dataObject.NewUrlOptions));
                OutputStatusMessage(string.Format("PauseCampaignsWithoutSupportedLocations: {0}", dataObject.PauseCampaignsWithoutSupportedLocations));
                OutputStatusMessage(string.Format("PauseNewCampaigns: {0}", dataObject.PauseNewCampaigns));
                OutputStatusMessage(string.Format("RaiseBidsToMinimum: {0}", dataObject.RaiseBidsToMinimum));
                OutputStatusMessage(string.Format("RaiseCampaignBudgetsToMinimum: {0}", dataObject.RaiseCampaignBudgetsToMinimum));
                OutputStatusMessage(string.Format("RaiseProductGroupBidsToMinimum: {0}", dataObject.RaiseProductGroupBidsToMinimum));
                OutputStatusMessage(string.Format("SearchAndDsaMixedCampaignAsSearchCampaign: {0}", dataObject.SearchAndDsaMixedCampaignAsSearchCampaign));
                OutputStatusMessage("SearchAndReplaceForCampaignNames:");
                OutputImportSearchAndReplaceForStringProperty(dataObject.SearchAndReplaceForCampaignNames);
                OutputStatusMessage("SearchAndReplaceForCustomParameters:");
                OutputImportSearchAndReplaceForStringProperty(dataObject.SearchAndReplaceForCustomParameters);
                OutputStatusMessage("SearchAndReplaceForTrackingTemplates:");
                OutputImportSearchAndReplaceForStringProperty(dataObject.SearchAndReplaceForTrackingTemplates);
                OutputStatusMessage("SearchAndReplaceForUrls:");
                OutputImportSearchAndReplaceForStringProperty(dataObject.SearchAndReplaceForUrls);
                OutputStatusMessage(string.Format("SuffixForCampaignNames: {0}", dataObject.SuffixForCampaignNames));
                OutputStatusMessage(string.Format("SuffixForTrackingTemplates: {0}", dataObject.SuffixForTrackingTemplates));
                OutputStatusMessage(string.Format("SuffixForUrls: {0}", dataObject.SuffixForUrls));
                OutputStatusMessage(string.Format("UpdateAdCustomizerFeeds: {0}", dataObject.UpdateAdCustomizerFeeds));
                OutputStatusMessage(string.Format("UpdateAdGroupNetwork: {0}", dataObject.UpdateAdGroupNetwork));
                OutputStatusMessage(string.Format("UpdateAdSchedules: {0}", dataObject.UpdateAdSchedules));
                OutputStatusMessage(string.Format("UpdateAppAdExtensions: {0}", dataObject.UpdateAppAdExtensions));
                OutputStatusMessage(string.Format("UpdateAudienceTargets: {0}", dataObject.UpdateAudienceTargets));
                OutputStatusMessage(string.Format("UpdateBiddingStrategies: {0}", dataObject.UpdateBiddingStrategies));
                OutputStatusMessage(string.Format("UpdateBids: {0}", dataObject.UpdateBids));
                OutputStatusMessage(string.Format("UpdateCallAdExtensions: {0}", dataObject.UpdateCallAdExtensions));
                OutputStatusMessage(string.Format("UpdateCalloutAdExtensions: {0}", dataObject.UpdateCalloutAdExtensions));
                OutputStatusMessage(string.Format("UpdateCampaignAdGroupLanguages: {0}", dataObject.UpdateCampaignAdGroupLanguages));
                OutputStatusMessage(string.Format("UpdateCampaignBudgets: {0}", dataObject.UpdateCampaignBudgets));
                OutputStatusMessage(string.Format("UpdateCampaignNames: {0}", dataObject.UpdateCampaignNames));
                OutputStatusMessage(string.Format("UpdateDemographicTargets: {0}", dataObject.UpdateDemographicTargets));
                OutputStatusMessage(string.Format("UpdateDeviceTargets: {0}", dataObject.UpdateDeviceTargets));
                OutputStatusMessage(string.Format("UpdateEntities: {0}", dataObject.UpdateEntities));
                OutputStatusMessage(string.Format("UpdateKeywordUrls: {0}", dataObject.UpdateKeywordUrls));
                OutputStatusMessage(string.Format("UpdateLabels: {0}", dataObject.UpdateLabels));
                OutputStatusMessage(string.Format("UpdateLocationAdExtensions: {0}", dataObject.UpdateLocationAdExtensions));
                OutputStatusMessage(string.Format("UpdateLocationTargets: {0}", dataObject.UpdateLocationTargets));
                OutputStatusMessage(string.Format("UpdateNegativeKeywordLists: {0}", dataObject.UpdateNegativeKeywordLists));
                OutputStatusMessage(string.Format("UpdateNegativeSites: {0}", dataObject.UpdateNegativeSites));
                OutputStatusMessage(string.Format("UpdatePageFeeds: {0}", dataObject.UpdatePageFeeds));
                OutputStatusMessage(string.Format("UpdatePriceAdExtensions: {0}", dataObject.UpdatePriceAdExtensions));
                OutputStatusMessage(string.Format("UpdateProductFilters: {0}", dataObject.UpdateProductFilters));
                OutputStatusMessage(string.Format("UpdatePromotionAdExtensions: {0}", dataObject.UpdatePromotionAdExtensions));
                OutputStatusMessage(string.Format("UpdateReviewAdExtensions: {0}", dataObject.UpdateReviewAdExtensions));
                OutputStatusMessage(string.Format("UpdateSitelinkAdExtensions: {0}", dataObject.UpdateSitelinkAdExtensions));
                OutputStatusMessage(string.Format("UpdateStatusForAdGroups: {0}", dataObject.UpdateStatusForAdGroups));
                OutputStatusMessage(string.Format("UpdateStatusForAds: {0}", dataObject.UpdateStatusForAds));
                OutputStatusMessage(string.Format("UpdateStatusForCampaigns: {0}", dataObject.UpdateStatusForCampaigns));
                OutputStatusMessage(string.Format("UpdateStatusForKeywords: {0}", dataObject.UpdateStatusForKeywords));
                OutputStatusMessage(string.Format("UpdateStructuredSnippetAdExtensions: {0}", dataObject.UpdateStructuredSnippetAdExtensions));
                OutputStatusMessage(string.Format("UpdateUrlOptions: {0}", dataObject.UpdateUrlOptions));
                OutputStatusMessage("* * * End OutputGoogleImportOption * * *");
            }
        }
        public void OutputArrayOfGoogleImportOption(IList<GoogleImportOption> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGoogleImportOption(dataObject);
                    }
                }
            }
        }
        public void OutputIdCollection(IdCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputIdCollection * * *");
                OutputStatusMessage("Ids:");
                OutputArrayOfLong(dataObject.Ids);
                OutputStatusMessage("* * * End OutputIdCollection * * *");
            }
        }
        public void OutputArrayOfIdCollection(IList<IdCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputIdCollection(dataObject);
                    }
                }
            }
        }
        public void OutputImage(Image dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImage * * *");
                OutputStatusMessage(string.Format("Data: {0}", dataObject.Data));
                OutputStatusMessage("* * * End OutputImage * * *");
            }
        }
        public void OutputArrayOfImage(IList<Image> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImage(dataObject);
                    }
                }
            }
        }
        public void OutputImageAdExtension(ImageAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImageAdExtension * * *");
                OutputStatusMessage(string.Format("AlternativeText: {0}", dataObject.AlternativeText));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayText: {0}", dataObject.DisplayText));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage("ImageMediaIds:");
                OutputArrayOfLong(dataObject.ImageMediaIds);
                OutputStatusMessage("Images:");
                OutputArrayOfAssetLink(dataObject.Images);
                OutputStatusMessage("Layouts:");
                OutputArrayOfString(dataObject.Layouts);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputImageAdExtension * * *");
            }
        }
        public void OutputArrayOfImageAdExtension(IList<ImageAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImageAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputImageAsset(ImageAsset dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImageAsset * * *");
                OutputStatusMessage(string.Format("CropHeight: {0}", dataObject.CropHeight));
                OutputStatusMessage(string.Format("CropWidth: {0}", dataObject.CropWidth));
                OutputStatusMessage(string.Format("CropX: {0}", dataObject.CropX));
                OutputStatusMessage(string.Format("CropY: {0}", dataObject.CropY));
                OutputStatusMessage(string.Format("SubType: {0}", dataObject.SubType));
                OutputStatusMessage("* * * End OutputImageAsset * * *");
            }
        }
        public void OutputArrayOfImageAsset(IList<ImageAsset> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImageAsset(dataObject);
                    }
                }
            }
        }
        public void OutputImageMediaRepresentation(ImageMediaRepresentation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImageMediaRepresentation * * *");
                OutputStatusMessage(string.Format("Height: {0}", dataObject.Height));
                OutputStatusMessage(string.Format("Width: {0}", dataObject.Width));
                OutputStatusMessage("* * * End OutputImageMediaRepresentation * * *");
            }
        }
        public void OutputArrayOfImageMediaRepresentation(IList<ImageMediaRepresentation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImageMediaRepresentation(dataObject);
                    }
                }
            }
        }
        public void OutputImportEntityStatistics(ImportEntityStatistics dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImportEntityStatistics * * *");
                OutputStatusMessage(string.Format("Additions: {0}", dataObject.Additions));
                OutputStatusMessage(string.Format("Changes: {0}", dataObject.Changes));
                OutputStatusMessage(string.Format("Deletions: {0}", dataObject.Deletions));
                OutputStatusMessage(string.Format("EntityType: {0}", dataObject.EntityType));
                OutputStatusMessage(string.Format("Errors: {0}", dataObject.Errors));
                OutputStatusMessage(string.Format("Total: {0}", dataObject.Total));
                OutputStatusMessage("* * * End OutputImportEntityStatistics * * *");
            }
        }
        public void OutputArrayOfImportEntityStatistics(IList<ImportEntityStatistics> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImportEntityStatistics(dataObject);
                    }
                }
            }
        }
        public void OutputImportJob(ImportJob dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImportJob * * *");
                OutputStatusMessage(string.Format("CreatedByUserId: {0}", dataObject.CreatedByUserId));
                OutputStatusMessage(string.Format("CreatedByUserName: {0}", dataObject.CreatedByUserName));
                OutputStatusMessage(string.Format("CreatedDateTimeInUTC: {0}", dataObject.CreatedDateTimeInUTC));
                OutputStatusMessage("Frequency:");
                OutputFrequency(dataObject.Frequency);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage("ImportOption:");
                OutputImportOption(dataObject.ImportOption);
                OutputStatusMessage(string.Format("LastRunTimeInUTC: {0}", dataObject.LastRunTimeInUTC));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("NotificationEmail: {0}", dataObject.NotificationEmail));
                OutputStatusMessage(string.Format("NotificationType: {0}", dataObject.NotificationType));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var fileimportjob = dataObject as FileImportJob;
                if(null != fileimportjob)
                {
                    OutputFileImportJob((FileImportJob)dataObject);
                }
                var googleimportjob = dataObject as GoogleImportJob;
                if(null != googleimportjob)
                {
                    OutputGoogleImportJob((GoogleImportJob)dataObject);
                }
                OutputStatusMessage("* * * End OutputImportJob * * *");
            }
        }
        public void OutputArrayOfImportJob(IList<ImportJob> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImportJob(dataObject);
                    }
                }
            }
        }
        public void OutputImportOption(ImportOption dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImportOption * * *");
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var fileimportoption = dataObject as FileImportOption;
                if(null != fileimportoption)
                {
                    OutputFileImportOption((FileImportOption)dataObject);
                }
                var googleimportoption = dataObject as GoogleImportOption;
                if(null != googleimportoption)
                {
                    OutputGoogleImportOption((GoogleImportOption)dataObject);
                }
                OutputStatusMessage("* * * End OutputImportOption * * *");
            }
        }
        public void OutputArrayOfImportOption(IList<ImportOption> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImportOption(dataObject);
                    }
                }
            }
        }
        public void OutputImportResult(ImportResult dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImportResult * * *");
                OutputStatusMessage("EntityStatistics:");
                OutputArrayOfImportEntityStatistics(dataObject.EntityStatistics);
                OutputStatusMessage(string.Format("ErrorLogUrl: {0}", dataObject.ErrorLogUrl));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage("ImportJob:");
                OutputImportJob(dataObject.ImportJob);
                OutputStatusMessage(string.Format("StartTimeInUTC: {0}", dataObject.StartTimeInUTC));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputImportResult * * *");
            }
        }
        public void OutputArrayOfImportResult(IList<ImportResult> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImportResult(dataObject);
                    }
                }
            }
        }
        public void OutputImportSearchAndReplaceForStringProperty(ImportSearchAndReplaceForStringProperty dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputImportSearchAndReplaceForStringProperty * * *");
                OutputStatusMessage(string.Format("ReplaceString: {0}", dataObject.ReplaceString));
                OutputStatusMessage(string.Format("SearchString: {0}", dataObject.SearchString));
                OutputStatusMessage("* * * End OutputImportSearchAndReplaceForStringProperty * * *");
            }
        }
        public void OutputArrayOfImportSearchAndReplaceForStringProperty(IList<ImportSearchAndReplaceForStringProperty> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputImportSearchAndReplaceForStringProperty(dataObject);
                    }
                }
            }
        }
        public void OutputInheritFromParentBiddingScheme(InheritFromParentBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputInheritFromParentBiddingScheme * * *");
                OutputStatusMessage(string.Format("InheritedBidStrategyType: {0}", dataObject.InheritedBidStrategyType));
                OutputStatusMessage("* * * End OutputInheritFromParentBiddingScheme * * *");
            }
        }
        public void OutputArrayOfInheritFromParentBiddingScheme(IList<InheritFromParentBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputInheritFromParentBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputInMarketAudience(InMarketAudience dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputInMarketAudience * * *");
                OutputStatusMessage("* * * End OutputInMarketAudience * * *");
            }
        }
        public void OutputArrayOfInMarketAudience(IList<InMarketAudience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputInMarketAudience(dataObject);
                    }
                }
            }
        }
        public void OutputInStoreTransactionGoal(InStoreTransactionGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputInStoreTransactionGoal * * *");
                OutputStatusMessage("* * * End OutputInStoreTransactionGoal * * *");
            }
        }
        public void OutputArrayOfInStoreTransactionGoal(IList<InStoreTransactionGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputInStoreTransactionGoal(dataObject);
                    }
                }
            }
        }
        public void OutputKeyword(Keyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeyword * * *");
                OutputStatusMessage("Bid:");
                OutputBid(dataObject.Bid);
                OutputStatusMessage("BiddingScheme:");
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Param1: {0}", dataObject.Param1));
                OutputStatusMessage(string.Format("Param2: {0}", dataObject.Param2));
                OutputStatusMessage(string.Format("Param3: {0}", dataObject.Param3));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputKeyword * * *");
            }
        }
        public void OutputArrayOfKeyword(IList<Keyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeyword(dataObject);
                    }
                }
            }
        }
        public void OutputLabel(Label dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLabel * * *");
                OutputStatusMessage(string.Format("ColorCode: {0}", dataObject.ColorCode));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage("* * * End OutputLabel * * *");
            }
        }
        public void OutputArrayOfLabel(IList<Label> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLabel(dataObject);
                    }
                }
            }
        }
        public void OutputLabelAssociation(LabelAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLabelAssociation * * *");
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("LabelId: {0}", dataObject.LabelId));
                OutputStatusMessage("* * * End OutputLabelAssociation * * *");
            }
        }
        public void OutputArrayOfLabelAssociation(IList<LabelAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLabelAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputLocationAdExtension(LocationAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLocationAdExtension * * *");
                OutputStatusMessage("Address:");
                OutputAddress(dataObject.Address);
                OutputStatusMessage(string.Format("CompanyName: {0}", dataObject.CompanyName));
                OutputStatusMessage(string.Format("GeoCodeStatus: {0}", dataObject.GeoCodeStatus));
                OutputStatusMessage("GeoPoint:");
                OutputGeoPoint(dataObject.GeoPoint);
                OutputStatusMessage(string.Format("PhoneNumber: {0}", dataObject.PhoneNumber));
                OutputStatusMessage("* * * End OutputLocationAdExtension * * *");
            }
        }
        public void OutputArrayOfLocationAdExtension(IList<LocationAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLocationAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputLocationCriterion(LocationCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLocationCriterion * * *");
                OutputStatusMessage(string.Format("DisplayName: {0}", dataObject.DisplayName));
                OutputStatusMessage("EnclosedLocationIds:");
                OutputArrayOfLong(dataObject.EnclosedLocationIds);
                OutputStatusMessage(string.Format("LocationId: {0}", dataObject.LocationId));
                OutputStatusMessage(string.Format("LocationType: {0}", dataObject.LocationType));
                OutputStatusMessage("* * * End OutputLocationCriterion * * *");
            }
        }
        public void OutputArrayOfLocationCriterion(IList<LocationCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLocationCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputLocationIntentCriterion(LocationIntentCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputLocationIntentCriterion * * *");
                OutputStatusMessage(string.Format("IntentOption: {0}", dataObject.IntentOption));
                OutputStatusMessage("* * * End OutputLocationIntentCriterion * * *");
            }
        }
        public void OutputArrayOfLocationIntentCriterion(IList<LocationIntentCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputLocationIntentCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputManualCpcBiddingScheme(ManualCpcBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputManualCpcBiddingScheme * * *");
                OutputStatusMessage("* * * End OutputManualCpcBiddingScheme * * *");
            }
        }
        public void OutputArrayOfManualCpcBiddingScheme(IList<ManualCpcBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputManualCpcBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputManualCpmBiddingScheme(ManualCpmBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputManualCpmBiddingScheme * * *");
                OutputStatusMessage("* * * End OutputManualCpmBiddingScheme * * *");
            }
        }
        public void OutputArrayOfManualCpmBiddingScheme(IList<ManualCpmBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputManualCpmBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputManualCpvBiddingScheme(ManualCpvBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputManualCpvBiddingScheme * * *");
                OutputStatusMessage("* * * End OutputManualCpvBiddingScheme * * *");
            }
        }
        public void OutputArrayOfManualCpvBiddingScheme(IList<ManualCpvBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputManualCpvBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputMaxClicksBiddingScheme(MaxClicksBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMaxClicksBiddingScheme * * *");
                OutputStatusMessage("MaxCpc:");
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage("* * * End OutputMaxClicksBiddingScheme * * *");
            }
        }
        public void OutputArrayOfMaxClicksBiddingScheme(IList<MaxClicksBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMaxClicksBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputMaxConversionsBiddingScheme(MaxConversionsBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMaxConversionsBiddingScheme * * *");
                OutputStatusMessage("MaxCpc:");
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage("* * * End OutputMaxConversionsBiddingScheme * * *");
            }
        }
        public void OutputArrayOfMaxConversionsBiddingScheme(IList<MaxConversionsBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMaxConversionsBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputMaxConversionValueBiddingScheme(MaxConversionValueBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMaxConversionValueBiddingScheme * * *");
                OutputStatusMessage(string.Format("TargetRoas: {0}", dataObject.TargetRoas));
                OutputStatusMessage("* * * End OutputMaxConversionValueBiddingScheme * * *");
            }
        }
        public void OutputArrayOfMaxConversionValueBiddingScheme(IList<MaxConversionValueBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMaxConversionValueBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputMaxRoasBiddingScheme(MaxRoasBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMaxRoasBiddingScheme * * *");
                OutputStatusMessage("MaxCpc:");
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage("* * * End OutputMaxRoasBiddingScheme * * *");
            }
        }
        public void OutputArrayOfMaxRoasBiddingScheme(IList<MaxRoasBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMaxRoasBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputMedia(Media dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMedia * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MediaType: {0}", dataObject.MediaType));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var image = dataObject as Image;
                if(null != image)
                {
                    OutputImage((Image)dataObject);
                }
                OutputStatusMessage("* * * End OutputMedia * * *");
            }
        }
        public void OutputArrayOfMedia(IList<Media> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMedia(dataObject);
                    }
                }
            }
        }
        public void OutputMediaAssociation(MediaAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMediaAssociation * * *");
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("MediaEnabledEntity: {0}", dataObject.MediaEnabledEntity));
                OutputStatusMessage(string.Format("MediaId: {0}", dataObject.MediaId));
                OutputStatusMessage("* * * End OutputMediaAssociation * * *");
            }
        }
        public void OutputArrayOfMediaAssociation(IList<MediaAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMediaAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputMediaMetaData(MediaMetaData dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMediaMetaData * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MediaType: {0}", dataObject.MediaType));
                OutputStatusMessage("Representations:");
                OutputArrayOfMediaRepresentation(dataObject.Representations);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage("* * * End OutputMediaMetaData * * *");
            }
        }
        public void OutputArrayOfMediaMetaData(IList<MediaMetaData> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMediaMetaData(dataObject);
                    }
                }
            }
        }
        public void OutputMediaRepresentation(MediaRepresentation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMediaRepresentation * * *");
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                var imagemediarepresentation = dataObject as ImageMediaRepresentation;
                if(null != imagemediarepresentation)
                {
                    OutputImageMediaRepresentation((ImageMediaRepresentation)dataObject);
                }
                OutputStatusMessage("* * * End OutputMediaRepresentation * * *");
            }
        }
        public void OutputArrayOfMediaRepresentation(IList<MediaRepresentation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMediaRepresentation(dataObject);
                    }
                }
            }
        }
        public void OutputMigrationStatusInfo(MigrationStatusInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputMigrationStatusInfo * * *");
                OutputStatusMessage(string.Format("MigrationType: {0}", dataObject.MigrationType));
                OutputStatusMessage(string.Format("StartTimeInUtc: {0}", dataObject.StartTimeInUtc));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputMigrationStatusInfo * * *");
            }
        }
        public void OutputArrayOfMigrationStatusInfo(IList<MigrationStatusInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputMigrationStatusInfo(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeAdGroupCriterion(NegativeAdGroupCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeAdGroupCriterion * * *");
                OutputStatusMessage("* * * End OutputNegativeAdGroupCriterion * * *");
            }
        }
        public void OutputArrayOfNegativeAdGroupCriterion(IList<NegativeAdGroupCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeAdGroupCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeCampaignCriterion(NegativeCampaignCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeCampaignCriterion * * *");
                OutputStatusMessage("* * * End OutputNegativeCampaignCriterion * * *");
            }
        }
        public void OutputArrayOfNegativeCampaignCriterion(IList<NegativeCampaignCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeCampaignCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeKeyword(NegativeKeyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeKeyword * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage("* * * End OutputNegativeKeyword * * *");
            }
        }
        public void OutputArrayOfNegativeKeyword(IList<NegativeKeyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeKeyword(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeKeywordList(NegativeKeywordList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeKeywordList * * *");
                OutputStatusMessage("* * * End OutputNegativeKeywordList * * *");
            }
        }
        public void OutputArrayOfNegativeKeywordList(IList<NegativeKeywordList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeKeywordList(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeSite(NegativeSite dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeSite * * *");
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                OutputStatusMessage("* * * End OutputNegativeSite * * *");
            }
        }
        public void OutputArrayOfNegativeSite(IList<NegativeSite> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeSite(dataObject);
                    }
                }
            }
        }
        public void OutputOfflineConversion(OfflineConversion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOfflineConversion * * *");
                OutputStatusMessage(string.Format("ConversionCurrencyCode: {0}", dataObject.ConversionCurrencyCode));
                OutputStatusMessage(string.Format("ConversionName: {0}", dataObject.ConversionName));
                OutputStatusMessage(string.Format("ConversionTime: {0}", dataObject.ConversionTime));
                OutputStatusMessage(string.Format("ConversionValue: {0}", dataObject.ConversionValue));
                OutputStatusMessage(string.Format("ExternalAttributionCredit: {0}", dataObject.ExternalAttributionCredit));
                OutputStatusMessage(string.Format("ExternalAttributionModel: {0}", dataObject.ExternalAttributionModel));
                OutputStatusMessage(string.Format("MicrosoftClickId: {0}", dataObject.MicrosoftClickId));
                OutputStatusMessage("* * * End OutputOfflineConversion * * *");
            }
        }
        public void OutputArrayOfOfflineConversion(IList<OfflineConversion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOfflineConversion(dataObject);
                    }
                }
            }
        }
        public void OutputOfflineConversionAdjustment(OfflineConversionAdjustment dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOfflineConversionAdjustment * * *");
                OutputStatusMessage(string.Format("AdjustmentCurrencyCode: {0}", dataObject.AdjustmentCurrencyCode));
                OutputStatusMessage(string.Format("AdjustmentTime: {0}", dataObject.AdjustmentTime));
                OutputStatusMessage(string.Format("AdjustmentType: {0}", dataObject.AdjustmentType));
                OutputStatusMessage(string.Format("AdjustmentValue: {0}", dataObject.AdjustmentValue));
                OutputStatusMessage(string.Format("ConversionName: {0}", dataObject.ConversionName));
                OutputStatusMessage(string.Format("ConversionTime: {0}", dataObject.ConversionTime));
                OutputStatusMessage(string.Format("MicrosoftClickId: {0}", dataObject.MicrosoftClickId));
                OutputStatusMessage("* * * End OutputOfflineConversionAdjustment * * *");
            }
        }
        public void OutputArrayOfOfflineConversionAdjustment(IList<OfflineConversionAdjustment> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOfflineConversionAdjustment(dataObject);
                    }
                }
            }
        }
        public void OutputOfflineConversionGoal(OfflineConversionGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOfflineConversionGoal * * *");
                OutputStatusMessage(string.Format("IsExternallyAttributed: {0}", dataObject.IsExternallyAttributed));
                OutputStatusMessage("* * * End OutputOfflineConversionGoal * * *");
            }
        }
        public void OutputArrayOfOfflineConversionGoal(IList<OfflineConversionGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOfflineConversionGoal(dataObject);
                    }
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOperationError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputOperationError * * *");
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOperationError(dataObject);
                    }
                }
            }
        }
        public void OutputPagesViewedPerVisitGoal(PagesViewedPerVisitGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPagesViewedPerVisitGoal * * *");
                OutputStatusMessage(string.Format("MinimumPagesViewed: {0}", dataObject.MinimumPagesViewed));
                OutputStatusMessage("* * * End OutputPagesViewedPerVisitGoal * * *");
            }
        }
        public void OutputArrayOfPagesViewedPerVisitGoal(IList<PagesViewedPerVisitGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPagesViewedPerVisitGoal(dataObject);
                    }
                }
            }
        }
        public void OutputPageVisitorsRule(PageVisitorsRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPageVisitorsRule * * *");
                OutputStatusMessage(string.Format("NormalForm: {0}", dataObject.NormalForm));
                OutputStatusMessage("RuleItemGroups:");
                OutputArrayOfRuleItemGroup(dataObject.RuleItemGroups);
                OutputStatusMessage("* * * End OutputPageVisitorsRule * * *");
            }
        }
        public void OutputArrayOfPageVisitorsRule(IList<PageVisitorsRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPageVisitorsRule(dataObject);
                    }
                }
            }
        }
        public void OutputPageVisitorsWhoDidNotVisitAnotherPageRule(PageVisitorsWhoDidNotVisitAnotherPageRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPageVisitorsWhoDidNotVisitAnotherPageRule * * *");
                OutputStatusMessage("ExcludeRuleItemGroups:");
                OutputArrayOfRuleItemGroup(dataObject.ExcludeRuleItemGroups);
                OutputStatusMessage("IncludeRuleItemGroups:");
                OutputArrayOfRuleItemGroup(dataObject.IncludeRuleItemGroups);
                OutputStatusMessage("* * * End OutputPageVisitorsWhoDidNotVisitAnotherPageRule * * *");
            }
        }
        public void OutputArrayOfPageVisitorsWhoDidNotVisitAnotherPageRule(IList<PageVisitorsWhoDidNotVisitAnotherPageRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPageVisitorsWhoDidNotVisitAnotherPageRule(dataObject);
                    }
                }
            }
        }
        public void OutputPageVisitorsWhoVisitedAnotherPageRule(PageVisitorsWhoVisitedAnotherPageRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPageVisitorsWhoVisitedAnotherPageRule * * *");
                OutputStatusMessage("AnotherRuleItemGroups:");
                OutputArrayOfRuleItemGroup(dataObject.AnotherRuleItemGroups);
                OutputStatusMessage("RuleItemGroups:");
                OutputArrayOfRuleItemGroup(dataObject.RuleItemGroups);
                OutputStatusMessage("* * * End OutputPageVisitorsWhoVisitedAnotherPageRule * * *");
            }
        }
        public void OutputArrayOfPageVisitorsWhoVisitedAnotherPageRule(IList<PageVisitorsWhoVisitedAnotherPageRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPageVisitorsWhoVisitedAnotherPageRule(dataObject);
                    }
                }
            }
        }
        public void OutputPaging(Paging dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPaging * * *");
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Size: {0}", dataObject.Size));
                OutputStatusMessage("* * * End OutputPaging * * *");
            }
        }
        public void OutputArrayOfPaging(IList<Paging> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPaging(dataObject);
                    }
                }
            }
        }
        public void OutputPlacementExclusionList(PlacementExclusionList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPlacementExclusionList * * *");
                OutputStatusMessage("* * * End OutputPlacementExclusionList * * *");
            }
        }
        public void OutputArrayOfPlacementExclusionList(IList<PlacementExclusionList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPlacementExclusionList(dataObject);
                    }
                }
            }
        }
        public void OutputPriceAdExtension(PriceAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPriceAdExtension * * *");
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("PriceExtensionType: {0}", dataObject.PriceExtensionType));
                OutputStatusMessage("TableRows:");
                OutputArrayOfPriceTableRow(dataObject.TableRows);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputPriceAdExtension * * *");
            }
        }
        public void OutputArrayOfPriceAdExtension(IList<PriceAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPriceAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputPriceTableRow(PriceTableRow dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPriceTableRow * * *");
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("Header: {0}", dataObject.Header));
                OutputStatusMessage(string.Format("Price: {0}", dataObject.Price));
                OutputStatusMessage(string.Format("PriceQualifier: {0}", dataObject.PriceQualifier));
                OutputStatusMessage(string.Format("PriceUnit: {0}", dataObject.PriceUnit));
                OutputStatusMessage(string.Format("TermsAndConditions: {0}", dataObject.TermsAndConditions));
                OutputStatusMessage(string.Format("TermsAndConditionsUrl: {0}", dataObject.TermsAndConditionsUrl));
                OutputStatusMessage("* * * End OutputPriceTableRow * * *");
            }
        }
        public void OutputArrayOfPriceTableRow(IList<PriceTableRow> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPriceTableRow(dataObject);
                    }
                }
            }
        }
        public void OutputProductAd(ProductAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductAd * * *");
                OutputStatusMessage(string.Format("PromotionalText: {0}", dataObject.PromotionalText));
                OutputStatusMessage("* * * End OutputProductAd * * *");
            }
        }
        public void OutputArrayOfProductAd(IList<ProductAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductAd(dataObject);
                    }
                }
            }
        }
        public void OutputProductAudience(ProductAudience dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductAudience * * *");
                OutputStatusMessage(string.Format("ProductAudienceType: {0}", dataObject.ProductAudienceType));
                OutputStatusMessage(string.Format("TagId: {0}", dataObject.TagId));
                OutputStatusMessage("* * * End OutputProductAudience * * *");
            }
        }
        public void OutputArrayOfProductAudience(IList<ProductAudience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductAudience(dataObject);
                    }
                }
            }
        }
        public void OutputProductCondition(ProductCondition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductCondition * * *");
                OutputStatusMessage(string.Format("Attribute: {0}", dataObject.Attribute));
                OutputStatusMessage(string.Format("Operand: {0}", dataObject.Operand));
                OutputStatusMessage("* * * End OutputProductCondition * * *");
            }
        }
        public void OutputArrayOfProductCondition(IList<ProductCondition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductCondition(dataObject);
                    }
                }
            }
        }
        public void OutputProductPartition(ProductPartition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductPartition * * *");
                OutputStatusMessage("Condition:");
                OutputProductCondition(dataObject.Condition);
                OutputStatusMessage(string.Format("ParentCriterionId: {0}", dataObject.ParentCriterionId));
                OutputStatusMessage(string.Format("PartitionType: {0}", dataObject.PartitionType));
                OutputStatusMessage("* * * End OutputProductPartition * * *");
            }
        }
        public void OutputArrayOfProductPartition(IList<ProductPartition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductPartition(dataObject);
                    }
                }
            }
        }
        public void OutputProductScope(ProductScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductScope * * *");
                OutputStatusMessage("Conditions:");
                OutputArrayOfProductCondition(dataObject.Conditions);
                OutputStatusMessage("* * * End OutputProductScope * * *");
            }
        }
        public void OutputArrayOfProductScope(IList<ProductScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductScope(dataObject);
                    }
                }
            }
        }
        public void OutputProfileCriterion(ProfileCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProfileCriterion * * *");
                OutputStatusMessage(string.Format("ProfileId: {0}", dataObject.ProfileId));
                OutputStatusMessage(string.Format("ProfileType: {0}", dataObject.ProfileType));
                OutputStatusMessage("* * * End OutputProfileCriterion * * *");
            }
        }
        public void OutputArrayOfProfileCriterion(IList<ProfileCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProfileCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputPromotionAdExtension(PromotionAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPromotionAdExtension * * *");
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("DiscountModifier: {0}", dataObject.DiscountModifier));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("MoneyAmountOff: {0}", dataObject.MoneyAmountOff));
                OutputStatusMessage(string.Format("OrdersOverAmount: {0}", dataObject.OrdersOverAmount));
                OutputStatusMessage(string.Format("PercentOff: {0}", dataObject.PercentOff));
                OutputStatusMessage(string.Format("PromotionCode: {0}", dataObject.PromotionCode));
                OutputStatusMessage("PromotionEndDate:");
                OutputDate(dataObject.PromotionEndDate);
                OutputStatusMessage(string.Format("PromotionItem: {0}", dataObject.PromotionItem));
                OutputStatusMessage(string.Format("PromotionOccasion: {0}", dataObject.PromotionOccasion));
                OutputStatusMessage("PromotionStartDate:");
                OutputDate(dataObject.PromotionStartDate);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputPromotionAdExtension * * *");
            }
        }
        public void OutputArrayOfPromotionAdExtension(IList<PromotionAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPromotionAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputRadiusCriterion(RadiusCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputRadiusCriterion * * *");
                OutputStatusMessage(string.Format("LatitudeDegrees: {0}", dataObject.LatitudeDegrees));
                OutputStatusMessage(string.Format("LongitudeDegrees: {0}", dataObject.LongitudeDegrees));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Radius: {0}", dataObject.Radius));
                OutputStatusMessage(string.Format("RadiusUnit: {0}", dataObject.RadiusUnit));
                OutputStatusMessage("* * * End OutputRadiusCriterion * * *");
            }
        }
        public void OutputArrayOfRadiusCriterion(IList<RadiusCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputRadiusCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputRemarketingList(RemarketingList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputRemarketingList * * *");
                OutputStatusMessage("Rule:");
                OutputRemarketingRule(dataObject.Rule);
                OutputStatusMessage(string.Format("TagId: {0}", dataObject.TagId));
                OutputStatusMessage("* * * End OutputRemarketingList * * *");
            }
        }
        public void OutputArrayOfRemarketingList(IList<RemarketingList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputRemarketingList(dataObject);
                    }
                }
            }
        }
        public void OutputRemarketingRule(RemarketingRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputRemarketingRule * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var customeventsrule = dataObject as CustomEventsRule;
                if(null != customeventsrule)
                {
                    OutputCustomEventsRule((CustomEventsRule)dataObject);
                }
                var pagevisitorsrule = dataObject as PageVisitorsRule;
                if(null != pagevisitorsrule)
                {
                    OutputPageVisitorsRule((PageVisitorsRule)dataObject);
                }
                var pagevisitorswhodidnotvisitanotherpagerule = dataObject as PageVisitorsWhoDidNotVisitAnotherPageRule;
                if(null != pagevisitorswhodidnotvisitanotherpagerule)
                {
                    OutputPageVisitorsWhoDidNotVisitAnotherPageRule((PageVisitorsWhoDidNotVisitAnotherPageRule)dataObject);
                }
                var pagevisitorswhovisitedanotherpagerule = dataObject as PageVisitorsWhoVisitedAnotherPageRule;
                if(null != pagevisitorswhovisitedanotherpagerule)
                {
                    OutputPageVisitorsWhoVisitedAnotherPageRule((PageVisitorsWhoVisitedAnotherPageRule)dataObject);
                }
                OutputStatusMessage("* * * End OutputRemarketingRule * * *");
            }
        }
        public void OutputArrayOfRemarketingRule(IList<RemarketingRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputRemarketingRule(dataObject);
                    }
                }
            }
        }
        public void OutputResponsiveAd(ResponsiveAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputResponsiveAd * * *");
                OutputStatusMessage(string.Format("BusinessName: {0}", dataObject.BusinessName));
                OutputStatusMessage(string.Format("CallToAction: {0}", dataObject.CallToAction));
                OutputStatusMessage(string.Format("CallToActionLanguage: {0}", dataObject.CallToActionLanguage));
                OutputStatusMessage("Descriptions:");
                OutputArrayOfAssetLink(dataObject.Descriptions);
                OutputStatusMessage(string.Format("Headline: {0}", dataObject.Headline));
                OutputStatusMessage("Headlines:");
                OutputArrayOfAssetLink(dataObject.Headlines);
                OutputStatusMessage("Images:");
                OutputArrayOfAssetLink(dataObject.Images);
                OutputStatusMessage("ImpressionTrackingUrls:");
                OutputArrayOfString(dataObject.ImpressionTrackingUrls);
                OutputStatusMessage("LongHeadline:");
                OutputAssetLink(dataObject.LongHeadline);
                OutputStatusMessage(string.Format("LongHeadlineString: {0}", dataObject.LongHeadlineString));
                OutputStatusMessage("LongHeadlines:");
                OutputArrayOfAssetLink(dataObject.LongHeadlines);
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage("Videos:");
                OutputArrayOfAssetLink(dataObject.Videos);
                OutputStatusMessage("* * * End OutputResponsiveAd * * *");
            }
        }
        public void OutputArrayOfResponsiveAd(IList<ResponsiveAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputResponsiveAd(dataObject);
                    }
                }
            }
        }
        public void OutputResponsiveSearchAd(ResponsiveSearchAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputResponsiveSearchAd * * *");
                OutputStatusMessage("Descriptions:");
                OutputArrayOfAssetLink(dataObject.Descriptions);
                OutputStatusMessage(string.Format("Domain: {0}", dataObject.Domain));
                OutputStatusMessage("Headlines:");
                OutputArrayOfAssetLink(dataObject.Headlines);
                OutputStatusMessage(string.Format("Path1: {0}", dataObject.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", dataObject.Path2));
                OutputStatusMessage("* * * End OutputResponsiveSearchAd * * *");
            }
        }
        public void OutputArrayOfResponsiveSearchAd(IList<ResponsiveSearchAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputResponsiveSearchAd(dataObject);
                    }
                }
            }
        }
        public void OutputReviewAdExtension(ReviewAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputReviewAdExtension * * *");
                OutputStatusMessage(string.Format("IsExact: {0}", dataObject.IsExact));
                OutputStatusMessage(string.Format("Source: {0}", dataObject.Source));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                OutputStatusMessage("* * * End OutputReviewAdExtension * * *");
            }
        }
        public void OutputArrayOfReviewAdExtension(IList<ReviewAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputReviewAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputRuleItem(RuleItem dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputRuleItem * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var stringruleitem = dataObject as StringRuleItem;
                if(null != stringruleitem)
                {
                    OutputStringRuleItem((StringRuleItem)dataObject);
                }
                OutputStatusMessage("* * * End OutputRuleItem * * *");
            }
        }
        public void OutputArrayOfRuleItem(IList<RuleItem> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputRuleItem(dataObject);
                    }
                }
            }
        }
        public void OutputRuleItemGroup(RuleItemGroup dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputRuleItemGroup * * *");
                OutputStatusMessage("Items:");
                OutputArrayOfRuleItem(dataObject.Items);
                OutputStatusMessage("* * * End OutputRuleItemGroup * * *");
            }
        }
        public void OutputArrayOfRuleItemGroup(IList<RuleItemGroup> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputRuleItemGroup(dataObject);
                    }
                }
            }
        }
        public void OutputSchedule(Schedule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSchedule * * *");
                OutputStatusMessage("DayTimeRanges:");
                OutputArrayOfDayTime(dataObject.DayTimeRanges);
                OutputStatusMessage("EndDate:");
                OutputDate(dataObject.EndDate);
                OutputStatusMessage("StartDate:");
                OutputDate(dataObject.StartDate);
                OutputStatusMessage(string.Format("UseSearcherTimeZone: {0}", dataObject.UseSearcherTimeZone));
                OutputStatusMessage("* * * End OutputSchedule * * *");
            }
        }
        public void OutputArrayOfSchedule(IList<Schedule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSchedule(dataObject);
                    }
                }
            }
        }
        public void OutputSetting(Setting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSetting * * *");
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var coopsetting = dataObject as CoOpSetting;
                if(null != coopsetting)
                {
                    OutputCoOpSetting((CoOpSetting)dataObject);
                }
                var dynamicfeedsetting = dataObject as DynamicFeedSetting;
                if(null != dynamicfeedsetting)
                {
                    OutputDynamicFeedSetting((DynamicFeedSetting)dataObject);
                }
                var dynamicsearchadssetting = dataObject as DynamicSearchAdsSetting;
                if(null != dynamicsearchadssetting)
                {
                    OutputDynamicSearchAdsSetting((DynamicSearchAdsSetting)dataObject);
                }
                var shoppingsetting = dataObject as ShoppingSetting;
                if(null != shoppingsetting)
                {
                    OutputShoppingSetting((ShoppingSetting)dataObject);
                }
                var targetsetting = dataObject as TargetSetting;
                if(null != targetsetting)
                {
                    OutputTargetSetting((TargetSetting)dataObject);
                }
                OutputStatusMessage("* * * End OutputSetting * * *");
            }
        }
        public void OutputArrayOfSetting(IList<Setting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSetting(dataObject);
                    }
                }
            }
        }
        public void OutputSharedEntity(SharedEntity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSharedEntity * * *");
                OutputStatusMessage(string.Format("AssociationCount: {0}", dataObject.AssociationCount));
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var sharedlist = dataObject as SharedList;
                if(null != sharedlist)
                {
                    OutputSharedList((SharedList)dataObject);
                }
                OutputStatusMessage("* * * End OutputSharedEntity * * *");
            }
        }
        public void OutputArrayOfSharedEntity(IList<SharedEntity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSharedEntity(dataObject);
                    }
                }
            }
        }
        public void OutputSharedEntityAssociation(SharedEntityAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSharedEntityAssociation * * *");
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("EntityType: {0}", dataObject.EntityType));
                OutputStatusMessage(string.Format("SharedEntityCustomerId: {0}", dataObject.SharedEntityCustomerId));
                OutputStatusMessage(string.Format("SharedEntityId: {0}", dataObject.SharedEntityId));
                OutputStatusMessage(string.Format("SharedEntityType: {0}", dataObject.SharedEntityType));
                OutputStatusMessage("* * * End OutputSharedEntityAssociation * * *");
            }
        }
        public void OutputArrayOfSharedEntityAssociation(IList<SharedEntityAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSharedEntityAssociation(dataObject);
                    }
                }
            }
        }
        public void OutputSharedList(SharedList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSharedList * * *");
                OutputStatusMessage(string.Format("ItemCount: {0}", dataObject.ItemCount));
                var negativekeywordlist = dataObject as NegativeKeywordList;
                if(null != negativekeywordlist)
                {
                    OutputNegativeKeywordList((NegativeKeywordList)dataObject);
                }
                var placementexclusionlist = dataObject as PlacementExclusionList;
                if(null != placementexclusionlist)
                {
                    OutputPlacementExclusionList((PlacementExclusionList)dataObject);
                }
                OutputStatusMessage("* * * End OutputSharedList * * *");
            }
        }
        public void OutputArrayOfSharedList(IList<SharedList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSharedList(dataObject);
                    }
                }
            }
        }
        public void OutputSharedListItem(SharedListItem dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSharedListItem * * *");
                OutputStatusMessage("ForwardCompatibilityMap:");
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var negativekeyword = dataObject as NegativeKeyword;
                if(null != negativekeyword)
                {
                    OutputNegativeKeyword((NegativeKeyword)dataObject);
                }
                var negativesite = dataObject as NegativeSite;
                if(null != negativesite)
                {
                    OutputNegativeSite((NegativeSite)dataObject);
                }
                OutputStatusMessage("* * * End OutputSharedListItem * * *");
            }
        }
        public void OutputArrayOfSharedListItem(IList<SharedListItem> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSharedListItem(dataObject);
                    }
                }
            }
        }
        public void OutputShoppingSetting(ShoppingSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputShoppingSetting * * *");
                OutputStatusMessage(string.Format("LocalInventoryAdsEnabled: {0}", dataObject.LocalInventoryAdsEnabled));
                OutputStatusMessage(string.Format("Priority: {0}", dataObject.Priority));
                OutputStatusMessage(string.Format("SalesCountryCode: {0}", dataObject.SalesCountryCode));
                OutputStatusMessage(string.Format("StoreId: {0}", dataObject.StoreId));
                OutputStatusMessage("* * * End OutputShoppingSetting * * *");
            }
        }
        public void OutputArrayOfShoppingSetting(IList<ShoppingSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputShoppingSetting(dataObject);
                    }
                }
            }
        }
        public void OutputSimilarRemarketingList(SimilarRemarketingList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSimilarRemarketingList * * *");
                OutputStatusMessage(string.Format("SourceId: {0}", dataObject.SourceId));
                OutputStatusMessage("* * * End OutputSimilarRemarketingList * * *");
            }
        }
        public void OutputArrayOfSimilarRemarketingList(IList<SimilarRemarketingList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSimilarRemarketingList(dataObject);
                    }
                }
            }
        }
        public void OutputSitelinkAdExtension(SitelinkAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSitelinkAdExtension * * *");
                OutputStatusMessage(string.Format("Description1: {0}", dataObject.Description1));
                OutputStatusMessage(string.Format("Description2: {0}", dataObject.Description2));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayText: {0}", dataObject.DisplayText));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage("* * * End OutputSitelinkAdExtension * * *");
            }
        }
        public void OutputArrayOfSitelinkAdExtension(IList<SitelinkAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSitelinkAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputStoreCriterion(StoreCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputStoreCriterion * * *");
                OutputStatusMessage(string.Format("StoreId: {0}", dataObject.StoreId));
                OutputStatusMessage("* * * End OutputStoreCriterion * * *");
            }
        }
        public void OutputArrayOfStoreCriterion(IList<StoreCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputStoreCriterion(dataObject);
                    }
                }
            }
        }
        public void OutputStringRuleItem(StringRuleItem dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputStringRuleItem * * *");
                OutputStatusMessage(string.Format("Operand: {0}", dataObject.Operand));
                OutputStatusMessage(string.Format("Operator: {0}", dataObject.Operator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage("* * * End OutputStringRuleItem * * *");
            }
        }
        public void OutputArrayOfStringRuleItem(IList<StringRuleItem> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputStringRuleItem(dataObject);
                    }
                }
            }
        }
        public void OutputStructuredSnippetAdExtension(StructuredSnippetAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputStructuredSnippetAdExtension * * *");
                OutputStatusMessage(string.Format("Header: {0}", dataObject.Header));
                OutputStatusMessage("Values:");
                OutputArrayOfString(dataObject.Values);
                OutputStatusMessage("* * * End OutputStructuredSnippetAdExtension * * *");
            }
        }
        public void OutputArrayOfStructuredSnippetAdExtension(IList<StructuredSnippetAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputStructuredSnippetAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputTargetCpaBiddingScheme(TargetCpaBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTargetCpaBiddingScheme * * *");
                OutputStatusMessage("MaxCpc:");
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage(string.Format("TargetCpa: {0}", dataObject.TargetCpa));
                OutputStatusMessage("* * * End OutputTargetCpaBiddingScheme * * *");
            }
        }
        public void OutputArrayOfTargetCpaBiddingScheme(IList<TargetCpaBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTargetCpaBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputTargetImpressionShareBiddingScheme(TargetImpressionShareBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTargetImpressionShareBiddingScheme * * *");
                OutputStatusMessage("MaxCpc:");
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage(string.Format("TargetAdPosition: {0}", dataObject.TargetAdPosition));
                OutputStatusMessage(string.Format("TargetImpressionShare: {0}", dataObject.TargetImpressionShare));
                OutputStatusMessage("* * * End OutputTargetImpressionShareBiddingScheme * * *");
            }
        }
        public void OutputArrayOfTargetImpressionShareBiddingScheme(IList<TargetImpressionShareBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTargetImpressionShareBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputTargetRoasBiddingScheme(TargetRoasBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTargetRoasBiddingScheme * * *");
                OutputStatusMessage("MaxCpc:");
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage(string.Format("TargetRoas: {0}", dataObject.TargetRoas));
                OutputStatusMessage("* * * End OutputTargetRoasBiddingScheme * * *");
            }
        }
        public void OutputArrayOfTargetRoasBiddingScheme(IList<TargetRoasBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTargetRoasBiddingScheme(dataObject);
                    }
                }
            }
        }
        public void OutputTargetSetting(TargetSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTargetSetting * * *");
                OutputStatusMessage("Details:");
                OutputArrayOfTargetSettingDetail(dataObject.Details);
                OutputStatusMessage("* * * End OutputTargetSetting * * *");
            }
        }
        public void OutputArrayOfTargetSetting(IList<TargetSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTargetSetting(dataObject);
                    }
                }
            }
        }
        public void OutputTargetSettingDetail(TargetSettingDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTargetSettingDetail * * *");
                OutputStatusMessage(string.Format("CriterionTypeGroup: {0}", dataObject.CriterionTypeGroup));
                OutputStatusMessage(string.Format("TargetAndBid: {0}", dataObject.TargetAndBid));
                OutputStatusMessage("* * * End OutputTargetSettingDetail * * *");
            }
        }
        public void OutputArrayOfTargetSettingDetail(IList<TargetSettingDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTargetSettingDetail(dataObject);
                    }
                }
            }
        }
        public void OutputTextAd(TextAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTextAd * * *");
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayUrl: {0}", dataObject.DisplayUrl));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("Title: {0}", dataObject.Title));
                OutputStatusMessage("* * * End OutputTextAd * * *");
            }
        }
        public void OutputArrayOfTextAd(IList<TextAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTextAd(dataObject);
                    }
                }
            }
        }
        public void OutputTextAsset(TextAsset dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputTextAsset * * *");
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage("* * * End OutputTextAsset * * *");
            }
        }
        public void OutputArrayOfTextAsset(IList<TextAsset> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputTextAsset(dataObject);
                    }
                }
            }
        }
        public void OutputUetTag(UetTag dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUetTag * * *");
                OutputStatusMessage("CustomerShare:");
                OutputCustomerShare(dataObject.CustomerShare);
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("TrackingNoScript: {0}", dataObject.TrackingNoScript));
                OutputStatusMessage(string.Format("TrackingScript: {0}", dataObject.TrackingScript));
                OutputStatusMessage(string.Format("TrackingStatus: {0}", dataObject.TrackingStatus));
                OutputStatusMessage("* * * End OutputUetTag * * *");
            }
        }
        public void OutputArrayOfUetTag(IList<UetTag> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUetTag(dataObject);
                    }
                }
            }
        }
        public void OutputUrlGoal(UrlGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUrlGoal * * *");
                OutputStatusMessage(string.Format("UrlExpression: {0}", dataObject.UrlExpression));
                OutputStatusMessage(string.Format("UrlOperator: {0}", dataObject.UrlOperator));
                OutputStatusMessage("* * * End OutputUrlGoal * * *");
            }
        }
        public void OutputArrayOfUrlGoal(IList<UrlGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUrlGoal(dataObject);
                    }
                }
            }
        }
        public void OutputVideo(Video dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputVideo * * *");
                OutputStatusMessage(string.Format("AspectRatio: {0}", dataObject.AspectRatio));
                OutputStatusMessage(string.Format("CreatedDateTimeInUTC: {0}", dataObject.CreatedDateTimeInUTC));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("DurationInMilliseconds: {0}", dataObject.DurationInMilliseconds));
                OutputStatusMessage(string.Format("FailureCode: {0}", dataObject.FailureCode));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("ModifiedDateTimeInUTC: {0}", dataObject.ModifiedDateTimeInUTC));
                OutputStatusMessage(string.Format("SourceUrl: {0}", dataObject.SourceUrl));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("ThumbnailUrl: {0}", dataObject.ThumbnailUrl));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                OutputStatusMessage("* * * End OutputVideo * * *");
            }
        }
        public void OutputArrayOfVideo(IList<Video> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputVideo(dataObject);
                    }
                }
            }
        }
        public void OutputVideoAdExtension(VideoAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputVideoAdExtension * * *");
                OutputStatusMessage(string.Format("ActionText: {0}", dataObject.ActionText));
                OutputStatusMessage(string.Format("AlternativeText: {0}", dataObject.AlternativeText));
                OutputStatusMessage(string.Format("DisplayText: {0}", dataObject.DisplayText));
                OutputStatusMessage("FinalAppUrls:");
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputStatusMessage("FinalMobileUrls:");
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputStatusMessage(string.Format("FinalUrlSuffix: {0}", dataObject.FinalUrlSuffix));
                OutputStatusMessage("FinalUrls:");
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("ThumbnailId: {0}", dataObject.ThumbnailId));
                OutputStatusMessage(string.Format("ThumbnailUrl: {0}", dataObject.ThumbnailUrl));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage("UrlCustomParameters:");
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage(string.Format("VideoId: {0}", dataObject.VideoId));
                OutputStatusMessage("* * * End OutputVideoAdExtension * * *");
            }
        }
        public void OutputArrayOfVideoAdExtension(IList<VideoAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputVideoAdExtension(dataObject);
                    }
                }
            }
        }
        public void OutputVideoAsset(VideoAsset dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputVideoAsset * * *");
                OutputStatusMessage(string.Format("SubType: {0}", dataObject.SubType));
                OutputStatusMessage("ThumbnailImage:");
                OutputImageAsset(dataObject.ThumbnailImage);
                OutputStatusMessage("* * * End OutputVideoAsset * * *");
            }
        }
        public void OutputArrayOfVideoAsset(IList<VideoAsset> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputVideoAsset(dataObject);
                    }
                }
            }
        }
        public void OutputWebpage(Webpage dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputWebpage * * *");
                OutputStatusMessage("Parameter:");
                OutputWebpageParameter(dataObject.Parameter);
                OutputStatusMessage("* * * End OutputWebpage * * *");
            }
        }
        public void OutputArrayOfWebpage(IList<Webpage> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputWebpage(dataObject);
                    }
                }
            }
        }
        public void OutputWebpageCondition(WebpageCondition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputWebpageCondition * * *");
                OutputStatusMessage(string.Format("Argument: {0}", dataObject.Argument));
                OutputStatusMessage(string.Format("Operand: {0}", dataObject.Operand));
                OutputStatusMessage("* * * End OutputWebpageCondition * * *");
            }
        }
        public void OutputArrayOfWebpageCondition(IList<WebpageCondition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputWebpageCondition(dataObject);
                    }
                }
            }
        }
        public void OutputWebpageParameter(WebpageParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputWebpageParameter * * *");
                OutputStatusMessage("Conditions:");
                OutputArrayOfWebpageCondition(dataObject.Conditions);
                OutputStatusMessage(string.Format("CriterionName: {0}", dataObject.CriterionName));
                OutputStatusMessage("* * * End OutputWebpageParameter * * *");
            }
        }
        public void OutputArrayOfWebpageParameter(IList<WebpageParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputWebpageParameter(dataObject);
                    }
                }
            }
        }
        public void OutputAdEditorialStatus(AdEditorialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdEditorialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdEditorialStatus(IList<AdEditorialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdEditorialStatus(valueSet);
                }
            }
        }
        public void OutputAdStatus(AdStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdStatus(IList<AdStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdStatus(valueSet);
                }
            }
        }
        public void OutputAdType(AdType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdType(IList<AdType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdType(valueSet);
                }
            }
        }
        public void OutputCallToAction(CallToAction valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CallToAction)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCallToAction(IList<CallToAction> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCallToAction(valueSet);
                }
            }
        }
        public void OutputLanguageName(LanguageName valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(LanguageName)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfLanguageName(IList<LanguageName> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputLanguageName(valueSet);
                }
            }
        }
        public void OutputAssetLinkEditorialStatus(AssetLinkEditorialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AssetLinkEditorialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAssetLinkEditorialStatus(IList<AssetLinkEditorialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAssetLinkEditorialStatus(valueSet);
                }
            }
        }
        public void OutputBudgetLimitType(BudgetLimitType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BudgetLimitType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBudgetLimitType(IList<BudgetLimitType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBudgetLimitType(valueSet);
                }
            }
        }
        public void OutputCampaignStatus(CampaignStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignStatus(IList<CampaignStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignStatus(valueSet);
                }
            }
        }
        public void OutputCampaignType(CampaignType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignType(IList<CampaignType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignType(valueSet);
                }
            }
        }
        public void OutputDynamicSearchAdsSource(DynamicSearchAdsSource valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DynamicSearchAdsSource)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDynamicSearchAdsSource(IList<DynamicSearchAdsSource> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDynamicSearchAdsSource(valueSet);
                }
            }
        }
        public void OutputCriterionTypeGroup(CriterionTypeGroup valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CriterionTypeGroup)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCriterionTypeGroup(IList<CriterionTypeGroup> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCriterionTypeGroup(valueSet);
                }
            }
        }
        public void OutputBidOption(BidOption valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BidOption)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBidOption(IList<BidOption> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBidOption(valueSet);
                }
            }
        }
        public void OutputCampaignAdditionalField(CampaignAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignAdditionalField(IList<CampaignAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignAdditionalField(valueSet);
                }
            }
        }
        public void OutputAdRotationType(AdRotationType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdRotationType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdRotationType(IList<AdRotationType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdRotationType(valueSet);
                }
            }
        }
        public void OutputNetwork(Network valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(Network)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfNetwork(IList<Network> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputNetwork(valueSet);
                }
            }
        }
        public void OutputAdGroupPrivacyStatus(AdGroupPrivacyStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupPrivacyStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupPrivacyStatus(IList<AdGroupPrivacyStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupPrivacyStatus(valueSet);
                }
            }
        }
        public void OutputAdGroupStatus(AdGroupStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupStatus(IList<AdGroupStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupStatus(valueSet);
                }
            }
        }
        public void OutputAdGroupAdditionalField(AdGroupAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupAdditionalField(IList<AdGroupAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupAdditionalField(valueSet);
                }
            }
        }
        public void OutputAdAdditionalField(AdAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdAdditionalField(IList<AdAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdAdditionalField(valueSet);
                }
            }
        }
        public void OutputKeywordEditorialStatus(KeywordEditorialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(KeywordEditorialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfKeywordEditorialStatus(IList<KeywordEditorialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputKeywordEditorialStatus(valueSet);
                }
            }
        }
        public void OutputMatchType(MatchType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(MatchType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfMatchType(IList<MatchType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputMatchType(valueSet);
                }
            }
        }
        public void OutputKeywordStatus(KeywordStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(KeywordStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfKeywordStatus(IList<KeywordStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputKeywordStatus(valueSet);
                }
            }
        }
        public void OutputEntityType(EntityType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(EntityType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfEntityType(IList<EntityType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputEntityType(valueSet);
                }
            }
        }
        public void OutputAppealStatus(AppealStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AppealStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAppealStatus(IList<AppealStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAppealStatus(valueSet);
                }
            }
        }
        public void OutputMigrationStatus(MigrationStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(MigrationStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfMigrationStatus(IList<MigrationStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputMigrationStatus(valueSet);
                }
            }
        }
        public void OutputAccountPropertyName(AccountPropertyName valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AccountPropertyName)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAccountPropertyName(IList<AccountPropertyName> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAccountPropertyName(valueSet);
                }
            }
        }
        public void OutputDay(Day valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(Day)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDay(IList<Day> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDay(valueSet);
                }
            }
        }
        public void OutputMinute(Minute valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(Minute)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfMinute(IList<Minute> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputMinute(valueSet);
                }
            }
        }
        public void OutputAdExtensionStatus(AdExtensionStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionStatus(IList<AdExtensionStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionStatus(valueSet);
                }
            }
        }
        public void OutputBusinessGeoCodeStatus(BusinessGeoCodeStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BusinessGeoCodeStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBusinessGeoCodeStatus(IList<BusinessGeoCodeStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBusinessGeoCodeStatus(valueSet);
                }
            }
        }
        public void OutputActionAdExtensionActionType(ActionAdExtensionActionType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ActionAdExtensionActionType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfActionAdExtensionActionType(IList<ActionAdExtensionActionType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputActionAdExtensionActionType(valueSet);
                }
            }
        }
        public void OutputPriceExtensionType(PriceExtensionType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PriceExtensionType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPriceExtensionType(IList<PriceExtensionType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPriceExtensionType(valueSet);
                }
            }
        }
        public void OutputPriceQualifier(PriceQualifier valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PriceQualifier)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPriceQualifier(IList<PriceQualifier> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPriceQualifier(valueSet);
                }
            }
        }
        public void OutputPriceUnit(PriceUnit valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PriceUnit)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPriceUnit(IList<PriceUnit> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPriceUnit(valueSet);
                }
            }
        }
        public void OutputPromotionDiscountModifier(PromotionDiscountModifier valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PromotionDiscountModifier)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPromotionDiscountModifier(IList<PromotionDiscountModifier> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPromotionDiscountModifier(valueSet);
                }
            }
        }
        public void OutputPromotionOccasion(PromotionOccasion valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PromotionOccasion)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPromotionOccasion(IList<PromotionOccasion> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPromotionOccasion(valueSet);
                }
            }
        }
        public void OutputAdExtensionHeaderType(AdExtensionHeaderType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionHeaderType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionHeaderType(IList<AdExtensionHeaderType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionHeaderType(valueSet);
                }
            }
        }
        public void OutputAdExtensionsTypeFilter(AdExtensionsTypeFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionsTypeFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionsTypeFilter(IList<AdExtensionsTypeFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionsTypeFilter(valueSet);
                }
            }
        }
        public void OutputAdExtensionAdditionalField(AdExtensionAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionAdditionalField(IList<AdExtensionAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionAdditionalField(valueSet);
                }
            }
        }
        public void OutputAssociationType(AssociationType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AssociationType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAssociationType(IList<AssociationType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAssociationType(valueSet);
                }
            }
        }
        public void OutputAdExtensionEditorialStatus(AdExtensionEditorialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionEditorialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionEditorialStatus(IList<AdExtensionEditorialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionEditorialStatus(valueSet);
                }
            }
        }
        public void OutputMediaEnabledEntityFilter(MediaEnabledEntityFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(MediaEnabledEntityFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfMediaEnabledEntityFilter(IList<MediaEnabledEntityFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputMediaEnabledEntityFilter(valueSet);
                }
            }
        }
        public void OutputAdGroupCriterionType(AdGroupCriterionType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupCriterionType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupCriterionType(IList<AdGroupCriterionType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupCriterionType(valueSet);
                }
            }
        }
        public void OutputCriterionAdditionalField(CriterionAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CriterionAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCriterionAdditionalField(IList<CriterionAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCriterionAdditionalField(valueSet);
                }
            }
        }
        public void OutputProductPartitionType(ProductPartitionType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductPartitionType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductPartitionType(IList<ProductPartitionType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductPartitionType(valueSet);
                }
            }
        }
        public void OutputWebpageConditionOperand(WebpageConditionOperand valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(WebpageConditionOperand)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfWebpageConditionOperand(IList<WebpageConditionOperand> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputWebpageConditionOperand(valueSet);
                }
            }
        }
        public void OutputAgeRange(AgeRange valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AgeRange)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAgeRange(IList<AgeRange> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAgeRange(valueSet);
                }
            }
        }
        public void OutputGenderType(GenderType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(GenderType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfGenderType(IList<GenderType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputGenderType(valueSet);
                }
            }
        }
        public void OutputDistanceUnit(DistanceUnit valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DistanceUnit)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDistanceUnit(IList<DistanceUnit> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDistanceUnit(valueSet);
                }
            }
        }
        public void OutputIntentOption(IntentOption valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(IntentOption)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfIntentOption(IList<IntentOption> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputIntentOption(valueSet);
                }
            }
        }
        public void OutputAudienceType(AudienceType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AudienceType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAudienceType(IList<AudienceType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAudienceType(valueSet);
                }
            }
        }
        public void OutputProfileType(ProfileType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProfileType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProfileType(IList<ProfileType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProfileType(valueSet);
                }
            }
        }
        public void OutputAdGroupCriterionStatus(AdGroupCriterionStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupCriterionStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupCriterionStatus(IList<AdGroupCriterionStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupCriterionStatus(valueSet);
                }
            }
        }
        public void OutputAdGroupCriterionEditorialStatus(AdGroupCriterionEditorialStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupCriterionEditorialStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupCriterionEditorialStatus(IList<AdGroupCriterionEditorialStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupCriterionEditorialStatus(valueSet);
                }
            }
        }
        public void OutputItemAction(ItemAction valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ItemAction)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfItemAction(IList<ItemAction> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputItemAction(valueSet);
                }
            }
        }
        public void OutputBMCStoreAdditionalField(BMCStoreAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BMCStoreAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBMCStoreAdditionalField(IList<BMCStoreAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBMCStoreAdditionalField(valueSet);
                }
            }
        }
        public void OutputBMCStoreSubType(BMCStoreSubType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BMCStoreSubType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBMCStoreSubType(IList<BMCStoreSubType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBMCStoreSubType(valueSet);
                }
            }
        }
        public void OutputEntityScope(EntityScope valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(EntityScope)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfEntityScope(IList<EntityScope> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputEntityScope(valueSet);
                }
            }
        }
        public void OutputCampaignCriterionStatus(CampaignCriterionStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignCriterionStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignCriterionStatus(IList<CampaignCriterionStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignCriterionStatus(valueSet);
                }
            }
        }
        public void OutputCampaignCriterionType(CampaignCriterionType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignCriterionType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignCriterionType(IList<CampaignCriterionType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignCriterionType(valueSet);
                }
            }
        }
        public void OutputNormalForm(NormalForm valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(NormalForm)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfNormalForm(IList<NormalForm> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputNormalForm(valueSet);
                }
            }
        }
        public void OutputStringOperator(StringOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(StringOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfStringOperator(IList<StringOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputStringOperator(valueSet);
                }
            }
        }
        public void OutputNumberOperator(NumberOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(NumberOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfNumberOperator(IList<NumberOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputNumberOperator(valueSet);
                }
            }
        }
        public void OutputProductAudienceType(ProductAudienceType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductAudienceType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductAudienceType(IList<ProductAudienceType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductAudienceType(valueSet);
                }
            }
        }
        public void OutputLogicalOperator(LogicalOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(LogicalOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfLogicalOperator(IList<LogicalOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputLogicalOperator(valueSet);
                }
            }
        }
        public void OutputAudienceAdditionalField(AudienceAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AudienceAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAudienceAdditionalField(IList<AudienceAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAudienceAdditionalField(valueSet);
                }
            }
        }
        public void OutputUetTagTrackingStatus(UetTagTrackingStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(UetTagTrackingStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfUetTagTrackingStatus(IList<UetTagTrackingStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputUetTagTrackingStatus(valueSet);
                }
            }
        }
        public void OutputConversionGoalType(ConversionGoalType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalType(IList<ConversionGoalType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalType(valueSet);
                }
            }
        }
        public void OutputConversionGoalAdditionalField(ConversionGoalAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalAdditionalField(IList<ConversionGoalAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalAdditionalField(valueSet);
                }
            }
        }
        public void OutputConversionGoalCountType(ConversionGoalCountType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalCountType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalCountType(IList<ConversionGoalCountType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalCountType(valueSet);
                }
            }
        }
        public void OutputConversionGoalCategory(ConversionGoalCategory valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalCategory)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalCategory(IList<ConversionGoalCategory> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalCategory(valueSet);
                }
            }
        }
        public void OutputConversionGoalRevenueType(ConversionGoalRevenueType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalRevenueType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalRevenueType(IList<ConversionGoalRevenueType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalRevenueType(valueSet);
                }
            }
        }
        public void OutputConversionGoalStatus(ConversionGoalStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalStatus(IList<ConversionGoalStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalStatus(valueSet);
                }
            }
        }
        public void OutputConversionGoalTrackingStatus(ConversionGoalTrackingStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionGoalTrackingStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionGoalTrackingStatus(IList<ConversionGoalTrackingStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionGoalTrackingStatus(valueSet);
                }
            }
        }
        public void OutputExpressionOperator(ExpressionOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ExpressionOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfExpressionOperator(IList<ExpressionOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputExpressionOperator(valueSet);
                }
            }
        }
        public void OutputValueOperator(ValueOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ValueOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfValueOperator(IList<ValueOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputValueOperator(valueSet);
                }
            }
        }
        public void OutputImportAdditionalField(ImportAdditionalField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ImportAdditionalField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfImportAdditionalField(IList<ImportAdditionalField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputImportAdditionalField(valueSet);
                }
            }
        }
        public void OutputImportEntityType(ImportEntityType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ImportEntityType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfImportEntityType(IList<ImportEntityType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputImportEntityType(valueSet);
                }
            }
        }
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputKeyValuePairOfstringstring(KeyValuePair<string,string> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringstring(IList<KeyValuePair<string,string>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringstring(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOflonglong(KeyValuePair<long,long> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOflonglong(IList<KeyValuePair<long,long>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOflonglong(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOfstringbase64Binary(KeyValuePair<string,byte[]> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringbase64Binary(IList<KeyValuePair<string,byte[]>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringbase64Binary(dataObject);
                }
            }
        }
    }
}