using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.CampaignManagement;

namespace BingAdsExamplesLibrary.V12
{
    public class CampaignManagementExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | CampaignManagement V12"; }
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
            SharedList sharedList)
        {
            var request = new AddListItemsToSharedListRequest
            {
                ListItems = listItems,
                SharedList = sharedList
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
            IList<SharedListItem> listItems)
        {
            var request = new AddSharedEntityRequest
            {
                SharedEntity = sharedEntity,
                ListItems = listItems
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
            SharedList sharedList)
        {
            var request = new DeleteListItemsFromSharedListRequest
            {
                ListItemIds = listItemIds,
                SharedList = sharedList
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
            IList<SharedEntity> sharedEntities)
        {
            var request = new DeleteSharedEntitiesRequest
            {
                SharedEntities = sharedEntities
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteSharedEntitiesAsync(r), request));
        }
        public async Task<DeleteSharedEntityAssociationsResponse> DeleteSharedEntityAssociationsAsync(
            IList<SharedEntityAssociation> associations)
        {
            var request = new DeleteSharedEntityAssociationsRequest
            {
                Associations = associations
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.DeleteSharedEntityAssociationsAsync(r), request));
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
            IList<long> entityIds)
        {
            var request = new GetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionType = adExtensionType,
                AssociationType = associationType,
                EntityIds = entityIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdExtensionsAssociationsAsync(r), request));
        }
        public async Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(
            long accountId,
            IList<long> adExtensionIds,
            AdExtensionsTypeFilter adExtensionType)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds,
                AdExtensionType = adExtensionType
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
            AdGroupCriterionType criterionType)
        {
            var request = new GetAdGroupCriterionsByIdsRequest
            {
                AdGroupCriterionIds = adGroupCriterionIds,
                AdGroupId = adGroupId,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdGroupCriterionsByIdsAsync(r), request));
        }
        public async Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(
            long campaignId)
        {
            var request = new GetAdGroupsByCampaignIdRequest
            {
                CampaignId = campaignId
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdGroupsByCampaignIdAsync(r), request));
        }
        public async Task<GetAdGroupsByIdsResponse> GetAdGroupsByIdsAsync(
            long campaignId,
            IList<long> adGroupIds)
        {
            var request = new GetAdGroupsByIdsRequest
            {
                CampaignId = campaignId,
                AdGroupIds = adGroupIds
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdGroupsByIdsAsync(r), request));
        }
        public async Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(
            long adGroupId,
            IList<AdType> adTypes)
        {
            var request = new GetAdsByAdGroupIdRequest
            {
                AdGroupId = adGroupId,
                AdTypes = adTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdsByAdGroupIdAsync(r), request));
        }
        public async Task<GetAdsByEditorialStatusResponse> GetAdsByEditorialStatusAsync(
            long adGroupId,
            AdEditorialStatus editorialStatus,
            IList<AdType> adTypes)
        {
            var request = new GetAdsByEditorialStatusRequest
            {
                AdGroupId = adGroupId,
                EditorialStatus = editorialStatus,
                AdTypes = adTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdsByEditorialStatusAsync(r), request));
        }
        public async Task<GetAdsByIdsResponse> GetAdsByIdsAsync(
            long adGroupId,
            IList<long> adIds,
            IList<AdType> adTypes)
        {
            var request = new GetAdsByIdsRequest
            {
                AdGroupId = adGroupId,
                AdIds = adIds,
                AdTypes = adTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAdsByIdsAsync(r), request));
        }
        public async Task<GetAudiencesByIdsResponse> GetAudiencesByIdsAsync(
            IList<long> audienceIds,
            AudienceType type)
        {
            var request = new GetAudiencesByIdsRequest
            {
                AudienceIds = audienceIds,
                Type = type
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetAudiencesByIdsAsync(r), request));
        }
        public async Task<GetBMCStoresByCustomerIdResponse> GetBMCStoresByCustomerIdAsync()
        {
            var request = new GetBMCStoresByCustomerIdRequest
            {
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
            CampaignCriterionType criterionType)
        {
            var request = new GetCampaignCriterionsByIdsRequest
            {
                CampaignCriterionIds = campaignCriterionIds,
                CampaignId = campaignId,
                CriterionType = criterionType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignCriterionsByIdsAsync(r), request));
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
            CampaignType campaignType)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request));
        }
        public async Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(
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

            return (await CampaignManagementService.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request));
        }
        public async Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(
            IList<long> conversionGoalIds,
            ConversionGoalType conversionGoalTypes)
        {
            var request = new GetConversionGoalsByIdsRequest
            {
                ConversionGoalIds = conversionGoalIds,
                ConversionGoalTypes = conversionGoalTypes
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetConversionGoalsByIdsAsync(r), request));
        }
        public async Task<GetConversionGoalsByTagIdsResponse> GetConversionGoalsByTagIdsAsync(
            IList<long> tagIds,
            ConversionGoalType conversionGoalTypes)
        {
            var request = new GetConversionGoalsByTagIdsRequest
            {
                TagIds = tagIds,
                ConversionGoalTypes = conversionGoalTypes
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
            SharedList sharedList)
        {
            var request = new GetListItemsBySharedListRequest
            {
                SharedList = sharedList
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
            MediaEnabledEntityFilter mediaEnabledEntities)
        {
            var request = new GetMediaMetaDataByAccountIdRequest
            {
                MediaEnabledEntities = mediaEnabledEntities
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
            String sharedEntityType)
        {
            var request = new GetSharedEntityAssociationsByEntityIdsRequest
            {
                EntityIds = entityIds,
                EntityType = entityType,
                SharedEntityType = sharedEntityType
            };

            return (await CampaignManagementService.CallAsync((s, r) => s.GetSharedEntityAssociationsByEntityIdsAsync(r), request));
        }
        public async Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(
            String entityType,
            IList<long> sharedEntityIds,
            String sharedEntityType)
        {
            var request = new GetSharedEntityAssociationsBySharedEntityIdsRequest
            {
                EntityType = entityType,
                SharedEntityIds = sharedEntityIds,
                SharedEntityType = sharedEntityType
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
            IList<SharedEntityAssociation> associations)
        {
            var request = new SetSharedEntityAssociationsRequest
            {
                Associations = associations
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
            IList<SharedEntity> sharedEntities)
        {
            var request = new UpdateSharedEntitiesRequest
            {
                SharedEntities = sharedEntities
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
        public void OutputAccountMigrationStatusesInfo(AccountMigrationStatusesInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputArrayOfMigrationStatusInfo(dataObject.MigrationStatusInfos);
            }
        }
        public void OutputArrayOfAccountMigrationStatusesInfo(IList<AccountMigrationStatusesInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountMigrationStatusesInfo(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAccountProperty(AccountProperty dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfAccountProperty(IList<AccountProperty> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountProperty(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAd(Ad dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdFormatPreference: {0}", dataObject.AdFormatPreference));
                OutputStatusMessage(string.Format("DevicePreference: {0}", dataObject.DevicePreference));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputCustomParameters(dataObject.UrlCustomParameters);
                var appinstallad = dataObject as AppInstallAd;
                if(appinstallad != null)
                {
                    OutputAppInstallAd((AppInstallAd)dataObject);
                }
                var dynamicsearchad = dataObject as DynamicSearchAd;
                if(dynamicsearchad != null)
                {
                    OutputDynamicSearchAd((DynamicSearchAd)dataObject);
                }
                var expandedtextad = dataObject as ExpandedTextAd;
                if(expandedtextad != null)
                {
                    OutputExpandedTextAd((ExpandedTextAd)dataObject);
                }
                var productad = dataObject as ProductAd;
                if(productad != null)
                {
                    OutputProductAd((ProductAd)dataObject);
                }
                var responsivead = dataObject as ResponsiveAd;
                if(responsivead != null)
                {
                    OutputResponsiveAd((ResponsiveAd)dataObject);
                }
                var textad = dataObject as TextAd;
                if(textad != null)
                {
                    OutputTextAd((TextAd)dataObject);
                }
            }
        }
        public void OutputArrayOfAd(IList<Ad> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdApiError(dataObject.Errors);
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAddress(Address dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CityName: {0}", dataObject.CityName));
                OutputStatusMessage(string.Format("CountryCode: {0}", dataObject.CountryCode));
                OutputStatusMessage(string.Format("PostalCode: {0}", dataObject.PostalCode));
                OutputStatusMessage(string.Format("ProvinceCode: {0}", dataObject.ProvinceCode));
                OutputStatusMessage(string.Format("ProvinceName: {0}", dataObject.ProvinceName));
                OutputStatusMessage(string.Format("StreetAddress: {0}", dataObject.StreetAddress));
                OutputStatusMessage(string.Format("StreetAddress2: {0}", dataObject.StreetAddress2));
            }
        }
        public void OutputArrayOfAddress(IList<Address> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAddress(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtension(AdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("DevicePreference: {0}", dataObject.DevicePreference));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputSchedule(dataObject.Scheduling);
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("Version: {0}", dataObject.Version));
                var appadextension = dataObject as AppAdExtension;
                if(appadextension != null)
                {
                    OutputAppAdExtension((AppAdExtension)dataObject);
                }
                var calladextension = dataObject as CallAdExtension;
                if(calladextension != null)
                {
                    OutputCallAdExtension((CallAdExtension)dataObject);
                }
                var calloutadextension = dataObject as CalloutAdExtension;
                if(calloutadextension != null)
                {
                    OutputCalloutAdExtension((CalloutAdExtension)dataObject);
                }
                var imageadextension = dataObject as ImageAdExtension;
                if(imageadextension != null)
                {
                    OutputImageAdExtension((ImageAdExtension)dataObject);
                }
                var locationadextension = dataObject as LocationAdExtension;
                if(locationadextension != null)
                {
                    OutputLocationAdExtension((LocationAdExtension)dataObject);
                }
                var priceadextension = dataObject as PriceAdExtension;
                if(priceadextension != null)
                {
                    OutputPriceAdExtension((PriceAdExtension)dataObject);
                }
                var reviewadextension = dataObject as ReviewAdExtension;
                if(reviewadextension != null)
                {
                    OutputReviewAdExtension((ReviewAdExtension)dataObject);
                }
                var sitelinkadextension = dataObject as SitelinkAdExtension;
                if(sitelinkadextension != null)
                {
                    OutputSitelinkAdExtension((SitelinkAdExtension)dataObject);
                }
                var structuredsnippetadextension = dataObject as StructuredSnippetAdExtension;
                if(structuredsnippetadextension != null)
                {
                    OutputStructuredSnippetAdExtension((StructuredSnippetAdExtension)dataObject);
                }
            }
        }
        public void OutputArrayOfAdExtension(IList<AdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionAssociation(AdExtensionAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputAdExtension(dataObject.AdExtension);
                OutputStatusMessage(string.Format("AssociationType: {0}", dataObject.AssociationType));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
            }
        }
        public void OutputArrayOfAdExtensionAssociation(IList<AdExtensionAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionAssociation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionAssociationCollection(AdExtensionAssociationCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdExtensionAssociation(dataObject.AdExtensionAssociations);
            }
        }
        public void OutputArrayOfAdExtensionAssociationCollection(IList<AdExtensionAssociationCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionAssociationCollection(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionEditorialReason(AdExtensionEditorialReason dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputArrayOfString(dataObject.PublisherCountries);
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage(string.Format("Term: {0}", dataObject.Term));
            }
        }
        public void OutputArrayOfAdExtensionEditorialReason(IList<AdExtensionEditorialReason> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionEditorialReason(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionEditorialReasonCollection(AdExtensionEditorialReasonCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdExtensionId: {0}", dataObject.AdExtensionId));
                OutputArrayOfAdExtensionEditorialReason(dataObject.Reasons);
            }
        }
        public void OutputArrayOfAdExtensionEditorialReasonCollection(IList<AdExtensionEditorialReasonCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionEditorialReasonCollection(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionIdentity(AdExtensionIdentity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Version: {0}", dataObject.Version));
            }
        }
        public void OutputArrayOfAdExtensionIdentity(IList<AdExtensionIdentity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionIdentity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionIdToEntityIdAssociation(AdExtensionIdToEntityIdAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdExtensionId: {0}", dataObject.AdExtensionId));
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
            }
        }
        public void OutputArrayOfAdExtensionIdToEntityIdAssociation(IList<AdExtensionIdToEntityIdAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionIdToEntityIdAssociation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroup(AdGroup dataObject)
        {
            if (null != dataObject)
            {
                OutputAdRotation(dataObject.AdRotation);
                OutputStatusMessage(string.Format("AudienceAdsBidAdjustment: {0}", dataObject.AudienceAdsBidAdjustment));
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputBid(dataObject.CpcBid);
                OutputDate(dataObject.EndDate);
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Network: {0}", dataObject.Network));
                OutputStatusMessage(string.Format("PrivacyStatus: {0}", dataObject.PrivacyStatus));
                OutputArrayOfSetting(dataObject.Settings);
                OutputDate(dataObject.StartDate);
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfAdGroup(IList<AdGroup> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroup(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupCriterion(AdGroupCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputCriterion(dataObject.Criterion);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var biddableadgroupcriterion = dataObject as BiddableAdGroupCriterion;
                if(biddableadgroupcriterion != null)
                {
                    OutputBiddableAdGroupCriterion((BiddableAdGroupCriterion)dataObject);
                }
                var negativeadgroupcriterion = dataObject as NegativeAdGroupCriterion;
                if(negativeadgroupcriterion != null)
                {
                    OutputNegativeAdGroupCriterion((NegativeAdGroupCriterion)dataObject);
                }
            }
        }
        public void OutputArrayOfAdGroupCriterion(IList<AdGroupCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupCriterionAction(AdGroupCriterionAction dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Action: {0}", dataObject.Action));
                OutputAdGroupCriterion(dataObject.AdGroupCriterion);
            }
        }
        public void OutputArrayOfAdGroupCriterionAction(IList<AdGroupCriterionAction> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupCriterionAction(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupNegativeSites(AdGroupNegativeSites dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputArrayOfString(dataObject.NegativeSites);
            }
        }
        public void OutputArrayOfAdGroupNegativeSites(IList<AdGroupNegativeSites> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupNegativeSites(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdRotation(AdRotation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("EndDate: {0}", dataObject.EndDate));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
            }
        }
        public void OutputArrayOfAdRotation(IList<AdRotation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdRotation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAgeCriterion(AgeCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AgeRange: {0}", dataObject.AgeRange));
            }
        }
        public void OutputArrayOfAgeCriterion(IList<AgeCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAgeCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputArrayOfOperationError(dataObject.OperationErrors);
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAppAdExtension(AppAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AppPlatform: {0}", dataObject.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", dataObject.AppStoreId));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayText: {0}", dataObject.DisplayText));
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfAppAdExtension(IList<AppAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAppAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAppInstallAd(AppInstallAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AppPlatform: {0}", dataObject.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", dataObject.AppStoreId));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("Title: {0}", dataObject.Title));
            }
        }
        public void OutputArrayOfAppInstallAd(IList<AppInstallAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAppInstallAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAppInstallGoal(AppInstallGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AppPlatform: {0}", dataObject.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", dataObject.AppStoreId));
            }
        }
        public void OutputArrayOfAppInstallGoal(IList<AppInstallGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAppInstallGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(adapifaultdetail != null)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(apifaultdetail != null)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
                var editorialapifaultdetail = dataObject as EditorialApiFaultDetail;
                if(editorialapifaultdetail != null)
                {
                    OutputEditorialApiFaultDetail((EditorialApiFaultDetail)dataObject);
                }
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApplicationFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAppUrl(AppUrl dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("OsType: {0}", dataObject.OsType));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
            }
        }
        public void OutputArrayOfAppUrl(IList<AppUrl> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAppUrl(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAudience(Audience dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AudienceNetworkSize: {0}", dataObject.AudienceNetworkSize));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MembershipDuration: {0}", dataObject.MembershipDuration));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("ParentId: {0}", dataObject.ParentId));
                OutputStatusMessage(string.Format("Scope: {0}", dataObject.Scope));
                OutputStatusMessage(string.Format("SearchSize: {0}", dataObject.SearchSize));
                OutputArrayOfString(dataObject.SupportedCampaignTypes);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var customaudience = dataObject as CustomAudience;
                if(customaudience != null)
                {
                    OutputCustomAudience((CustomAudience)dataObject);
                }
                var inmarketaudience = dataObject as InMarketAudience;
                if(inmarketaudience != null)
                {
                    OutputInMarketAudience((InMarketAudience)dataObject);
                }
                var productaudience = dataObject as ProductAudience;
                if(productaudience != null)
                {
                    OutputProductAudience((ProductAudience)dataObject);
                }
                var remarketinglist = dataObject as RemarketingList;
                if(remarketinglist != null)
                {
                    OutputRemarketingList((RemarketingList)dataObject);
                }
            }
        }
        public void OutputArrayOfAudience(IList<Audience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAudience(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAudienceCriterion(AudienceCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AudienceId: {0}", dataObject.AudienceId));
                OutputStatusMessage(string.Format("AudienceType: {0}", dataObject.AudienceType));
            }
        }
        public void OutputArrayOfAudienceCriterion(IList<AudienceCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAudienceCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("FieldPath: {0}", dataObject.FieldPath));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var editorialerror = dataObject as EditorialError;
                if(editorialerror != null)
                {
                    OutputEditorialError((EditorialError)dataObject);
                }
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBatchError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBatchErrorCollection(BatchErrorCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("FieldPath: {0}", dataObject.FieldPath));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var editorialerrorcollection = dataObject as EditorialErrorCollection;
                if(editorialerrorcollection != null)
                {
                    OutputEditorialErrorCollection((EditorialErrorCollection)dataObject);
                }
            }
        }
        public void OutputArrayOfBatchErrorCollection(IList<BatchErrorCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBatchErrorCollection(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBid(Bid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
            }
        }
        public void OutputArrayOfBid(IList<Bid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBid(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBiddableAdGroupCriterion(BiddableAdGroupCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputCriterionBid(dataObject.CriterionBid);
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfBiddableAdGroupCriterion(IList<BiddableAdGroupCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBiddableAdGroupCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBiddableCampaignCriterion(BiddableCampaignCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputCriterionBid(dataObject.CriterionBid);
            }
        }
        public void OutputArrayOfBiddableCampaignCriterion(IList<BiddableCampaignCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBiddableCampaignCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBiddingScheme(BiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var enhancedcpcbiddingscheme = dataObject as EnhancedCpcBiddingScheme;
                if(enhancedcpcbiddingscheme != null)
                {
                    OutputEnhancedCpcBiddingScheme((EnhancedCpcBiddingScheme)dataObject);
                }
                var inheritfromparentbiddingscheme = dataObject as InheritFromParentBiddingScheme;
                if(inheritfromparentbiddingscheme != null)
                {
                    OutputInheritFromParentBiddingScheme((InheritFromParentBiddingScheme)dataObject);
                }
                var manualcpcbiddingscheme = dataObject as ManualCpcBiddingScheme;
                if(manualcpcbiddingscheme != null)
                {
                    OutputManualCpcBiddingScheme((ManualCpcBiddingScheme)dataObject);
                }
                var maxclicksbiddingscheme = dataObject as MaxClicksBiddingScheme;
                if(maxclicksbiddingscheme != null)
                {
                    OutputMaxClicksBiddingScheme((MaxClicksBiddingScheme)dataObject);
                }
                var maxconversionsbiddingscheme = dataObject as MaxConversionsBiddingScheme;
                if(maxconversionsbiddingscheme != null)
                {
                    OutputMaxConversionsBiddingScheme((MaxConversionsBiddingScheme)dataObject);
                }
                var targetcpabiddingscheme = dataObject as TargetCpaBiddingScheme;
                if(targetcpabiddingscheme != null)
                {
                    OutputTargetCpaBiddingScheme((TargetCpaBiddingScheme)dataObject);
                }
            }
        }
        public void OutputArrayOfBiddingScheme(IList<BiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBidMultiplier(BidMultiplier dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Multiplier: {0}", dataObject.Multiplier));
            }
        }
        public void OutputArrayOfBidMultiplier(IList<BidMultiplier> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBidMultiplier(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBMCStore(BMCStore dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("HasCatalog: {0}", dataObject.HasCatalog));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("IsActive: {0}", dataObject.IsActive));
                OutputStatusMessage(string.Format("IsProductAdsEnabled: {0}", dataObject.IsProductAdsEnabled));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("SubType: {0}", dataObject.SubType));
            }
        }
        public void OutputArrayOfBMCStore(IList<BMCStore> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBMCStore(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBudget(Budget dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage(string.Format("AssociationCount: {0}", dataObject.AssociationCount));
                OutputStatusMessage(string.Format("BudgetType: {0}", dataObject.BudgetType));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
            }
        }
        public void OutputArrayOfBudget(IList<Budget> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBudget(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCallAdExtension(CallAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CountryCode: {0}", dataObject.CountryCode));
                OutputStatusMessage(string.Format("IsCallOnly: {0}", dataObject.IsCallOnly));
                OutputStatusMessage(string.Format("IsCallTrackingEnabled: {0}", dataObject.IsCallTrackingEnabled));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", dataObject.PhoneNumber));
                OutputStatusMessage(string.Format("RequireTollFreeTrackingNumber: {0}", dataObject.RequireTollFreeTrackingNumber));
            }
        }
        public void OutputArrayOfCallAdExtension(IList<CallAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCallAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCalloutAdExtension(CalloutAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
            }
        }
        public void OutputArrayOfCalloutAdExtension(IList<CalloutAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCalloutAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaign(Campaign dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AudienceAdsBidAdjustment: {0}", dataObject.AudienceAdsBidAdjustment));
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputStatusMessage(string.Format("BudgetType: {0}", dataObject.BudgetType));
                OutputStatusMessage(string.Format("DailyBudget: {0}", dataObject.DailyBudget));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("SubType: {0}", dataObject.SubType));
                OutputStatusMessage(string.Format("TimeZone: {0}", dataObject.TimeZone));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
                OutputStatusMessage(string.Format("CampaignType: {0}", dataObject.CampaignType));
                OutputArrayOfSetting(dataObject.Settings);
                OutputStatusMessage(string.Format("BudgetId: {0}", dataObject.BudgetId));
                OutputArrayOfString(dataObject.Languages);
            }
        }
        public void OutputArrayOfCampaign(IList<Campaign> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaign(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignCriterion(CampaignCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputCriterion(dataObject.Criterion);
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var biddablecampaigncriterion = dataObject as BiddableCampaignCriterion;
                if(biddablecampaigncriterion != null)
                {
                    OutputBiddableCampaignCriterion((BiddableCampaignCriterion)dataObject);
                }
                var negativecampaigncriterion = dataObject as NegativeCampaignCriterion;
                if(negativecampaigncriterion != null)
                {
                    OutputNegativeCampaignCriterion((NegativeCampaignCriterion)dataObject);
                }
            }
        }
        public void OutputArrayOfCampaignCriterion(IList<CampaignCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignNegativeSites(CampaignNegativeSites dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputArrayOfString(dataObject.NegativeSites);
            }
        }
        public void OutputArrayOfCampaignNegativeSites(IList<CampaignNegativeSites> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignNegativeSites(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputConversionGoal(ConversionGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ConversionWindowInMinutes: {0}", dataObject.ConversionWindowInMinutes));
                OutputStatusMessage(string.Format("CountType: {0}", dataObject.CountType));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputConversionGoalRevenue(dataObject.Revenue);
                OutputStatusMessage(string.Format("Scope: {0}", dataObject.Scope));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("TagId: {0}", dataObject.TagId));
                OutputStatusMessage(string.Format("TrackingStatus: {0}", dataObject.TrackingStatus));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var appinstallgoal = dataObject as AppInstallGoal;
                if(appinstallgoal != null)
                {
                    OutputAppInstallGoal((AppInstallGoal)dataObject);
                }
                var durationgoal = dataObject as DurationGoal;
                if(durationgoal != null)
                {
                    OutputDurationGoal((DurationGoal)dataObject);
                }
                var eventgoal = dataObject as EventGoal;
                if(eventgoal != null)
                {
                    OutputEventGoal((EventGoal)dataObject);
                }
                var instoretransactiongoal = dataObject as InStoreTransactionGoal;
                if(instoretransactiongoal != null)
                {
                    OutputInStoreTransactionGoal((InStoreTransactionGoal)dataObject);
                }
                var offlineconversiongoal = dataObject as OfflineConversionGoal;
                if(offlineconversiongoal != null)
                {
                    OutputOfflineConversionGoal((OfflineConversionGoal)dataObject);
                }
                var pagesviewedpervisitgoal = dataObject as PagesViewedPerVisitGoal;
                if(pagesviewedpervisitgoal != null)
                {
                    OutputPagesViewedPerVisitGoal((PagesViewedPerVisitGoal)dataObject);
                }
                var urlgoal = dataObject as UrlGoal;
                if(urlgoal != null)
                {
                    OutputUrlGoal((UrlGoal)dataObject);
                }
            }
        }
        public void OutputArrayOfConversionGoal(IList<ConversionGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputConversionGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputConversionGoalRevenue(ConversionGoalRevenue dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfConversionGoalRevenue(IList<ConversionGoalRevenue> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputConversionGoalRevenue(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCoOpSetting(CoOpSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("BidBoostValue: {0}", dataObject.BidBoostValue));
                OutputStatusMessage(string.Format("BidMaxValue: {0}", dataObject.BidMaxValue));
                OutputStatusMessage(string.Format("BidOption: {0}", dataObject.BidOption));
            }
        }
        public void OutputArrayOfCoOpSetting(IList<CoOpSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCoOpSetting(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCriterion(Criterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var agecriterion = dataObject as AgeCriterion;
                if(agecriterion != null)
                {
                    OutputAgeCriterion((AgeCriterion)dataObject);
                }
                var audiencecriterion = dataObject as AudienceCriterion;
                if(audiencecriterion != null)
                {
                    OutputAudienceCriterion((AudienceCriterion)dataObject);
                }
                var daytimecriterion = dataObject as DayTimeCriterion;
                if(daytimecriterion != null)
                {
                    OutputDayTimeCriterion((DayTimeCriterion)dataObject);
                }
                var devicecriterion = dataObject as DeviceCriterion;
                if(devicecriterion != null)
                {
                    OutputDeviceCriterion((DeviceCriterion)dataObject);
                }
                var gendercriterion = dataObject as GenderCriterion;
                if(gendercriterion != null)
                {
                    OutputGenderCriterion((GenderCriterion)dataObject);
                }
                var locationcriterion = dataObject as LocationCriterion;
                if(locationcriterion != null)
                {
                    OutputLocationCriterion((LocationCriterion)dataObject);
                }
                var locationintentcriterion = dataObject as LocationIntentCriterion;
                if(locationintentcriterion != null)
                {
                    OutputLocationIntentCriterion((LocationIntentCriterion)dataObject);
                }
                var productpartition = dataObject as ProductPartition;
                if(productpartition != null)
                {
                    OutputProductPartition((ProductPartition)dataObject);
                }
                var productscope = dataObject as ProductScope;
                if(productscope != null)
                {
                    OutputProductScope((ProductScope)dataObject);
                }
                var profilecriterion = dataObject as ProfileCriterion;
                if(profilecriterion != null)
                {
                    OutputProfileCriterion((ProfileCriterion)dataObject);
                }
                var radiuscriterion = dataObject as RadiusCriterion;
                if(radiuscriterion != null)
                {
                    OutputRadiusCriterion((RadiusCriterion)dataObject);
                }
                var webpage = dataObject as Webpage;
                if(webpage != null)
                {
                    OutputWebpage((Webpage)dataObject);
                }
            }
        }
        public void OutputArrayOfCriterion(IList<Criterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCriterionBid(CriterionBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var bidmultiplier = dataObject as BidMultiplier;
                if(bidmultiplier != null)
                {
                    OutputBidMultiplier((BidMultiplier)dataObject);
                }
                var fixedbid = dataObject as FixedBid;
                if(fixedbid != null)
                {
                    OutputFixedBid((FixedBid)dataObject);
                }
            }
        }
        public void OutputArrayOfCriterionBid(IList<CriterionBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCriterionBid(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCustomAudience(CustomAudience dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfCustomAudience(IList<CustomAudience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCustomAudience(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCustomEventsRule(CustomEventsRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Action: {0}", dataObject.Action));
                OutputStatusMessage(string.Format("ActionOperator: {0}", dataObject.ActionOperator));
                OutputStatusMessage(string.Format("Category: {0}", dataObject.Category));
                OutputStatusMessage(string.Format("CategoryOperator: {0}", dataObject.CategoryOperator));
                OutputStatusMessage(string.Format("Label: {0}", dataObject.Label));
                OutputStatusMessage(string.Format("LabelOperator: {0}", dataObject.LabelOperator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage(string.Format("ValueOperator: {0}", dataObject.ValueOperator));
            }
        }
        public void OutputArrayOfCustomEventsRule(IList<CustomEventsRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCustomEventsRule(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCustomParameter(CustomParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfCustomParameter(IList<CustomParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCustomParameter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCustomParameters(CustomParameters dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfCustomParameter(dataObject.Parameters);
            }
        }
        public void OutputArrayOfCustomParameters(IList<CustomParameters> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCustomParameters(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDate(Date dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
            }
        }
        public void OutputArrayOfDate(IList<Date> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDayTime(DayTime dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("EndHour: {0}", dataObject.EndHour));
                OutputStatusMessage(string.Format("EndMinute: {0}", dataObject.EndMinute));
                OutputStatusMessage(string.Format("StartHour: {0}", dataObject.StartHour));
                OutputStatusMessage(string.Format("StartMinute: {0}", dataObject.StartMinute));
            }
        }
        public void OutputArrayOfDayTime(IList<DayTime> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDayTime(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDayTimeCriterion(DayTimeCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("FromHour: {0}", dataObject.FromHour));
                OutputStatusMessage(string.Format("FromMinute: {0}", dataObject.FromMinute));
                OutputStatusMessage(string.Format("ToHour: {0}", dataObject.ToHour));
                OutputStatusMessage(string.Format("ToMinute: {0}", dataObject.ToMinute));
            }
        }
        public void OutputArrayOfDayTimeCriterion(IList<DayTimeCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDayTimeCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDeviceCriterion(DeviceCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("DeviceName: {0}", dataObject.DeviceName));
                OutputStatusMessage(string.Format("OSName: {0}", dataObject.OSName));
            }
        }
        public void OutputArrayOfDeviceCriterion(IList<DeviceCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDeviceCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDurationGoal(DurationGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("MinimumDurationInSeconds: {0}", dataObject.MinimumDurationInSeconds));
            }
        }
        public void OutputArrayOfDurationGoal(IList<DurationGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDurationGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDynamicSearchAd(DynamicSearchAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Path1: {0}", dataObject.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", dataObject.Path2));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
            }
        }
        public void OutputArrayOfDynamicSearchAd(IList<DynamicSearchAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDynamicSearchAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDynamicSearchAdsSetting(DynamicSearchAdsSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("DomainName: {0}", dataObject.DomainName));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputArrayOfLong(dataObject.PageFeedIds);
                OutputStatusMessage(string.Format("Source: {0}", dataObject.Source));
            }
        }
        public void OutputArrayOfDynamicSearchAdsSetting(IList<DynamicSearchAdsSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDynamicSearchAdsSetting(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEditorialApiFaultDetail(EditorialApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputArrayOfEditorialError(dataObject.EditorialErrors);
                OutputArrayOfOperationError(dataObject.OperationErrors);
            }
        }
        public void OutputArrayOfEditorialApiFaultDetail(IList<EditorialApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEditorialApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEditorialError(EditorialError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Appealable: {0}", dataObject.Appealable));
                OutputStatusMessage(string.Format("DisapprovedText: {0}", dataObject.DisapprovedText));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("PublisherCountry: {0}", dataObject.PublisherCountry));
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
            }
        }
        public void OutputArrayOfEditorialError(IList<EditorialError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEditorialError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEditorialErrorCollection(EditorialErrorCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Appealable: {0}", dataObject.Appealable));
                OutputStatusMessage(string.Format("DisapprovedText: {0}", dataObject.DisapprovedText));
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputStatusMessage(string.Format("PublisherCountry: {0}", dataObject.PublisherCountry));
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
            }
        }
        public void OutputArrayOfEditorialErrorCollection(IList<EditorialErrorCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEditorialErrorCollection(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEditorialReason(EditorialReason dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Location: {0}", dataObject.Location));
                OutputArrayOfString(dataObject.PublisherCountries);
                OutputStatusMessage(string.Format("ReasonCode: {0}", dataObject.ReasonCode));
                OutputStatusMessage(string.Format("Term: {0}", dataObject.Term));
            }
        }
        public void OutputArrayOfEditorialReason(IList<EditorialReason> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEditorialReason(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEditorialReasonCollection(EditorialReasonCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage(string.Format("AdOrKeywordId: {0}", dataObject.AdOrKeywordId));
                OutputStatusMessage(string.Format("AppealStatus: {0}", dataObject.AppealStatus));
                OutputArrayOfEditorialReason(dataObject.Reasons);
            }
        }
        public void OutputArrayOfEditorialReasonCollection(IList<EditorialReasonCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEditorialReasonCollection(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEnhancedCpcBiddingScheme(EnhancedCpcBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfEnhancedCpcBiddingScheme(IList<EnhancedCpcBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEnhancedCpcBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEntityIdToParentIdAssociation(EntityIdToParentIdAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("ParentId: {0}", dataObject.ParentId));
            }
        }
        public void OutputArrayOfEntityIdToParentIdAssociation(IList<EntityIdToParentIdAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEntityIdToParentIdAssociation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEntityNegativeKeyword(EntityNegativeKeyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("EntityType: {0}", dataObject.EntityType));
                OutputArrayOfNegativeKeyword(dataObject.NegativeKeywords);
            }
        }
        public void OutputArrayOfEntityNegativeKeyword(IList<EntityNegativeKeyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEntityNegativeKeyword(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputEventGoal(EventGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ActionExpression: {0}", dataObject.ActionExpression));
                OutputStatusMessage(string.Format("ActionOperator: {0}", dataObject.ActionOperator));
                OutputStatusMessage(string.Format("CategoryExpression: {0}", dataObject.CategoryExpression));
                OutputStatusMessage(string.Format("CategoryOperator: {0}", dataObject.CategoryOperator));
                OutputStatusMessage(string.Format("LabelExpression: {0}", dataObject.LabelExpression));
                OutputStatusMessage(string.Format("LabelOperator: {0}", dataObject.LabelOperator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage(string.Format("ValueOperator: {0}", dataObject.ValueOperator));
            }
        }
        public void OutputArrayOfEventGoal(IList<EventGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputEventGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputExpandedTextAd(ExpandedTextAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Domain: {0}", dataObject.Domain));
                OutputStatusMessage(string.Format("Path1: {0}", dataObject.Path1));
                OutputStatusMessage(string.Format("Path2: {0}", dataObject.Path2));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("TitlePart1: {0}", dataObject.TitlePart1));
                OutputStatusMessage(string.Format("TitlePart2: {0}", dataObject.TitlePart2));
            }
        }
        public void OutputArrayOfExpandedTextAd(IList<ExpandedTextAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputExpandedTextAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputFixedBid(FixedBid dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
            }
        }
        public void OutputArrayOfFixedBid(IList<FixedBid> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputFixedBid(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputGenderCriterion(GenderCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("GenderType: {0}", dataObject.GenderType));
            }
        }
        public void OutputArrayOfGenderCriterion(IList<GenderCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputGenderCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputGeoPoint(GeoPoint dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("LatitudeInMicroDegrees: {0}", dataObject.LatitudeInMicroDegrees));
                OutputStatusMessage(string.Format("LongitudeInMicroDegrees: {0}", dataObject.LongitudeInMicroDegrees));
            }
        }
        public void OutputArrayOfGeoPoint(IList<GeoPoint> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputGeoPoint(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputIdCollection(IdCollection dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfLong(dataObject.Ids);
            }
        }
        public void OutputArrayOfIdCollection(IList<IdCollection> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputIdCollection(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputImage(Image dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Data: {0}", dataObject.Data));
            }
        }
        public void OutputArrayOfImage(IList<Image> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputImage(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputImageAdExtension(ImageAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AlternativeText: {0}", dataObject.AlternativeText));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputArrayOfLong(dataObject.ImageMediaIds);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfImageAdExtension(IList<ImageAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputImageAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputImageMediaRepresentation(ImageMediaRepresentation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Height: {0}", dataObject.Height));
                OutputStatusMessage(string.Format("Width: {0}", dataObject.Width));
            }
        }
        public void OutputArrayOfImageMediaRepresentation(IList<ImageMediaRepresentation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputImageMediaRepresentation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputInheritFromParentBiddingScheme(InheritFromParentBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("InheritedBidStrategyType: {0}", dataObject.InheritedBidStrategyType));
            }
        }
        public void OutputArrayOfInheritFromParentBiddingScheme(IList<InheritFromParentBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputInheritFromParentBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputInMarketAudience(InMarketAudience dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfInMarketAudience(IList<InMarketAudience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputInMarketAudience(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputInStoreTransactionGoal(InStoreTransactionGoal dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfInStoreTransactionGoal(IList<InStoreTransactionGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputInStoreTransactionGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeyword(Keyword dataObject)
        {
            if (null != dataObject)
            {
                OutputBid(dataObject.Bid);
                OutputBiddingScheme(dataObject.BiddingScheme);
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", dataObject.EditorialStatus));
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Param1: {0}", dataObject.Param1));
                OutputStatusMessage(string.Format("Param2: {0}", dataObject.Param2));
                OutputStatusMessage(string.Format("Param3: {0}", dataObject.Param3));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfKeyword(IList<Keyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyword(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLabel(Label dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ColorCode: {0}", dataObject.ColorCode));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
            }
        }
        public void OutputArrayOfLabel(IList<Label> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLabel(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLabelAssociation(LabelAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("LabelId: {0}", dataObject.LabelId));
            }
        }
        public void OutputArrayOfLabelAssociation(IList<LabelAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLabelAssociation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLocationAdExtension(LocationAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputAddress(dataObject.Address);
                OutputStatusMessage(string.Format("CompanyName: {0}", dataObject.CompanyName));
                OutputStatusMessage(string.Format("GeoCodeStatus: {0}", dataObject.GeoCodeStatus));
                OutputGeoPoint(dataObject.GeoPoint);
                OutputStatusMessage(string.Format("PhoneNumber: {0}", dataObject.PhoneNumber));
            }
        }
        public void OutputArrayOfLocationAdExtension(IList<LocationAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLocationAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLocationCriterion(LocationCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("DisplayName: {0}", dataObject.DisplayName));
                OutputArrayOfLong(dataObject.EnclosedLocationIds);
                OutputStatusMessage(string.Format("LocationId: {0}", dataObject.LocationId));
                OutputStatusMessage(string.Format("LocationType: {0}", dataObject.LocationType));
            }
        }
        public void OutputArrayOfLocationCriterion(IList<LocationCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLocationCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputLocationIntentCriterion(LocationIntentCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("IntentOption: {0}", dataObject.IntentOption));
            }
        }
        public void OutputArrayOfLocationIntentCriterion(IList<LocationIntentCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputLocationIntentCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputManualCpcBiddingScheme(ManualCpcBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfManualCpcBiddingScheme(IList<ManualCpcBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputManualCpcBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMaxClicksBiddingScheme(MaxClicksBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputBid(dataObject.MaxCpc);
            }
        }
        public void OutputArrayOfMaxClicksBiddingScheme(IList<MaxClicksBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMaxClicksBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMaxConversionsBiddingScheme(MaxConversionsBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputBid(dataObject.MaxCpc);
            }
        }
        public void OutputArrayOfMaxConversionsBiddingScheme(IList<MaxConversionsBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMaxConversionsBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMedia(Media dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MediaType: {0}", dataObject.MediaType));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var image = dataObject as Image;
                if(image != null)
                {
                    OutputImage((Image)dataObject);
                }
            }
        }
        public void OutputArrayOfMedia(IList<Media> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMedia(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMediaAssociation(MediaAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("MediaEnabledEntity: {0}", dataObject.MediaEnabledEntity));
                OutputStatusMessage(string.Format("MediaId: {0}", dataObject.MediaId));
            }
        }
        public void OutputArrayOfMediaAssociation(IList<MediaAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMediaAssociation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMediaMetaData(MediaMetaData dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MediaType: {0}", dataObject.MediaType));
                OutputArrayOfMediaRepresentation(dataObject.Representations);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
            }
        }
        public void OutputArrayOfMediaMetaData(IList<MediaMetaData> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMediaMetaData(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMediaRepresentation(MediaRepresentation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
                var imagemediarepresentation = dataObject as ImageMediaRepresentation;
                if(imagemediarepresentation != null)
                {
                    OutputImageMediaRepresentation((ImageMediaRepresentation)dataObject);
                }
            }
        }
        public void OutputArrayOfMediaRepresentation(IList<MediaRepresentation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMediaRepresentation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputMigrationStatusInfo(MigrationStatusInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("MigrationType: {0}", dataObject.MigrationType));
                OutputStatusMessage(string.Format("StartTimeInUtc: {0}", dataObject.StartTimeInUtc));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
            }
        }
        public void OutputArrayOfMigrationStatusInfo(IList<MigrationStatusInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputMigrationStatusInfo(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeAdGroupCriterion(NegativeAdGroupCriterion dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfNegativeAdGroupCriterion(IList<NegativeAdGroupCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeAdGroupCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeCampaignCriterion(NegativeCampaignCriterion dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfNegativeCampaignCriterion(IList<NegativeCampaignCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeCampaignCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeKeyword(NegativeKeyword dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", dataObject.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
            }
        }
        public void OutputArrayOfNegativeKeyword(IList<NegativeKeyword> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeKeyword(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeKeywordList(NegativeKeywordList dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfNegativeKeywordList(IList<NegativeKeywordList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeKeywordList(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOfflineConversion(OfflineConversion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ConversionCurrencyCode: {0}", dataObject.ConversionCurrencyCode));
                OutputStatusMessage(string.Format("ConversionName: {0}", dataObject.ConversionName));
                OutputStatusMessage(string.Format("ConversionTime: {0}", dataObject.ConversionTime));
                OutputStatusMessage(string.Format("ConversionValue: {0}", dataObject.ConversionValue));
                OutputStatusMessage(string.Format("MicrosoftClickId: {0}", dataObject.MicrosoftClickId));
            }
        }
        public void OutputArrayOfOfflineConversion(IList<OfflineConversion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOfflineConversion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOfflineConversionGoal(OfflineConversionGoal dataObject)
        {
            if (null != dataObject)
            {
            }
        }
        public void OutputArrayOfOfflineConversionGoal(IList<OfflineConversionGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOfflineConversionGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOperationError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPagesViewedPerVisitGoal(PagesViewedPerVisitGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("MinimumPagesViewed: {0}", dataObject.MinimumPagesViewed));
            }
        }
        public void OutputArrayOfPagesViewedPerVisitGoal(IList<PagesViewedPerVisitGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPagesViewedPerVisitGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPageVisitorsRule(PageVisitorsRule dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfRuleItemGroup(dataObject.RuleItemGroups);
            }
        }
        public void OutputArrayOfPageVisitorsRule(IList<PageVisitorsRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPageVisitorsRule(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPageVisitorsWhoDidNotVisitAnotherPageRule(PageVisitorsWhoDidNotVisitAnotherPageRule dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfRuleItemGroup(dataObject.ExcludeRuleItemGroups);
                OutputArrayOfRuleItemGroup(dataObject.IncludeRuleItemGroups);
            }
        }
        public void OutputArrayOfPageVisitorsWhoDidNotVisitAnotherPageRule(IList<PageVisitorsWhoDidNotVisitAnotherPageRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPageVisitorsWhoDidNotVisitAnotherPageRule(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPageVisitorsWhoVisitedAnotherPageRule(PageVisitorsWhoVisitedAnotherPageRule dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfRuleItemGroup(dataObject.AnotherRuleItemGroups);
                OutputArrayOfRuleItemGroup(dataObject.RuleItemGroups);
            }
        }
        public void OutputArrayOfPageVisitorsWhoVisitedAnotherPageRule(IList<PageVisitorsWhoVisitedAnotherPageRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPageVisitorsWhoVisitedAnotherPageRule(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPaging(Paging dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Size: {0}", dataObject.Size));
            }
        }
        public void OutputArrayOfPaging(IList<Paging> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPaging(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPriceAdExtension(PriceAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("PriceExtensionType: {0}", dataObject.PriceExtensionType));
                OutputArrayOfPriceTableRow(dataObject.TableRows);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfPriceAdExtension(IList<PriceAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPriceAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPriceTableRow(PriceTableRow dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("Header: {0}", dataObject.Header));
                OutputStatusMessage(string.Format("Price: {0}", dataObject.Price));
                OutputStatusMessage(string.Format("PriceQualifier: {0}", dataObject.PriceQualifier));
                OutputStatusMessage(string.Format("PriceUnit: {0}", dataObject.PriceUnit));
                OutputStatusMessage(string.Format("TermsAndConditions: {0}", dataObject.TermsAndConditions));
                OutputStatusMessage(string.Format("TermsAndConditionsUrl: {0}", dataObject.TermsAndConditionsUrl));
            }
        }
        public void OutputArrayOfPriceTableRow(IList<PriceTableRow> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPriceTableRow(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductAd(ProductAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("PromotionalText: {0}", dataObject.PromotionalText));
            }
        }
        public void OutputArrayOfProductAd(IList<ProductAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductAudience(ProductAudience dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ProductAudienceType: {0}", dataObject.ProductAudienceType));
                OutputStatusMessage(string.Format("TagId: {0}", dataObject.TagId));
            }
        }
        public void OutputArrayOfProductAudience(IList<ProductAudience> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductAudience(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductCondition(ProductCondition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Attribute: {0}", dataObject.Attribute));
                OutputStatusMessage(string.Format("Operand: {0}", dataObject.Operand));
            }
        }
        public void OutputArrayOfProductCondition(IList<ProductCondition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductCondition(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductPartition(ProductPartition dataObject)
        {
            if (null != dataObject)
            {
                OutputProductCondition(dataObject.Condition);
                OutputStatusMessage(string.Format("ParentCriterionId: {0}", dataObject.ParentCriterionId));
                OutputStatusMessage(string.Format("PartitionType: {0}", dataObject.PartitionType));
            }
        }
        public void OutputArrayOfProductPartition(IList<ProductPartition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductPartition(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductScope(ProductScope dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfProductCondition(dataObject.Conditions);
            }
        }
        public void OutputArrayOfProductScope(IList<ProductScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProfileCriterion(ProfileCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ProfileId: {0}", dataObject.ProfileId));
                OutputStatusMessage(string.Format("ProfileType: {0}", dataObject.ProfileType));
            }
        }
        public void OutputArrayOfProfileCriterion(IList<ProfileCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProfileCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputRadiusCriterion(RadiusCriterion dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("LatitudeDegrees: {0}", dataObject.LatitudeDegrees));
                OutputStatusMessage(string.Format("LongitudeDegrees: {0}", dataObject.LongitudeDegrees));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Radius: {0}", dataObject.Radius));
                OutputStatusMessage(string.Format("RadiusUnit: {0}", dataObject.RadiusUnit));
            }
        }
        public void OutputArrayOfRadiusCriterion(IList<RadiusCriterion> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputRadiusCriterion(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputRemarketingList(RemarketingList dataObject)
        {
            if (null != dataObject)
            {
                OutputRemarketingRule(dataObject.Rule);
                OutputStatusMessage(string.Format("TagId: {0}", dataObject.TagId));
            }
        }
        public void OutputArrayOfRemarketingList(IList<RemarketingList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputRemarketingList(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputRemarketingRule(RemarketingRule dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var customeventsrule = dataObject as CustomEventsRule;
                if(customeventsrule != null)
                {
                    OutputCustomEventsRule((CustomEventsRule)dataObject);
                }
                var pagevisitorsrule = dataObject as PageVisitorsRule;
                if(pagevisitorsrule != null)
                {
                    OutputPageVisitorsRule((PageVisitorsRule)dataObject);
                }
                var pagevisitorswhodidnotvisitanotherpagerule = dataObject as PageVisitorsWhoDidNotVisitAnotherPageRule;
                if(pagevisitorswhodidnotvisitanotherpagerule != null)
                {
                    OutputPageVisitorsWhoDidNotVisitAnotherPageRule((PageVisitorsWhoDidNotVisitAnotherPageRule)dataObject);
                }
                var pagevisitorswhovisitedanotherpagerule = dataObject as PageVisitorsWhoVisitedAnotherPageRule;
                if(pagevisitorswhovisitedanotherpagerule != null)
                {
                    OutputPageVisitorsWhoVisitedAnotherPageRule((PageVisitorsWhoVisitedAnotherPageRule)dataObject);
                }
            }
        }
        public void OutputArrayOfRemarketingRule(IList<RemarketingRule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputRemarketingRule(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputResponsiveAd(ResponsiveAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("BusinessName: {0}", dataObject.BusinessName));
                OutputStatusMessage(string.Format("CallToAction: {0}", dataObject.CallToAction));
                OutputStatusMessage(string.Format("Headline: {0}", dataObject.Headline));
                OutputStatusMessage(string.Format("LandscapeImageMediaId: {0}", dataObject.LandscapeImageMediaId));
                OutputStatusMessage(string.Format("LandscapeLogoMediaId: {0}", dataObject.LandscapeLogoMediaId));
                OutputStatusMessage(string.Format("LongHeadline: {0}", dataObject.LongHeadline));
                OutputStatusMessage(string.Format("SquareImageMediaId: {0}", dataObject.SquareImageMediaId));
                OutputStatusMessage(string.Format("SquareLogoMediaId: {0}", dataObject.SquareLogoMediaId));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
            }
        }
        public void OutputArrayOfResponsiveAd(IList<ResponsiveAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputResponsiveAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputReviewAdExtension(ReviewAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("IsExact: {0}", dataObject.IsExact));
                OutputStatusMessage(string.Format("Source: {0}", dataObject.Source));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("Url: {0}", dataObject.Url));
            }
        }
        public void OutputArrayOfReviewAdExtension(IList<ReviewAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputReviewAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputRuleItem(RuleItem dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var stringruleitem = dataObject as StringRuleItem;
                if(stringruleitem != null)
                {
                    OutputStringRuleItem((StringRuleItem)dataObject);
                }
            }
        }
        public void OutputArrayOfRuleItem(IList<RuleItem> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputRuleItem(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputRuleItemGroup(RuleItemGroup dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfRuleItem(dataObject.Items);
            }
        }
        public void OutputArrayOfRuleItemGroup(IList<RuleItemGroup> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputRuleItemGroup(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSchedule(Schedule dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfDayTime(dataObject.DayTimeRanges);
                OutputDate(dataObject.EndDate);
                OutputDate(dataObject.StartDate);
                OutputStatusMessage(string.Format("UseSearcherTimeZone: {0}", dataObject.UseSearcherTimeZone));
            }
        }
        public void OutputArrayOfSchedule(IList<Schedule> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSchedule(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSetting(Setting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var coopsetting = dataObject as CoOpSetting;
                if(coopsetting != null)
                {
                    OutputCoOpSetting((CoOpSetting)dataObject);
                }
                var dynamicsearchadssetting = dataObject as DynamicSearchAdsSetting;
                if(dynamicsearchadssetting != null)
                {
                    OutputDynamicSearchAdsSetting((DynamicSearchAdsSetting)dataObject);
                }
                var shoppingsetting = dataObject as ShoppingSetting;
                if(shoppingsetting != null)
                {
                    OutputShoppingSetting((ShoppingSetting)dataObject);
                }
                var targetsetting = dataObject as TargetSetting;
                if(targetsetting != null)
                {
                    OutputTargetSetting((TargetSetting)dataObject);
                }
            }
        }
        public void OutputArrayOfSetting(IList<Setting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSetting(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSharedEntity(SharedEntity dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AssociationCount: {0}", dataObject.AssociationCount));
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var sharedlist = dataObject as SharedList;
                if(sharedlist != null)
                {
                    OutputSharedList((SharedList)dataObject);
                }
            }
        }
        public void OutputArrayOfSharedEntity(IList<SharedEntity> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSharedEntity(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSharedEntityAssociation(SharedEntityAssociation dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("EntityId: {0}", dataObject.EntityId));
                OutputStatusMessage(string.Format("EntityType: {0}", dataObject.EntityType));
                OutputStatusMessage(string.Format("SharedEntityId: {0}", dataObject.SharedEntityId));
                OutputStatusMessage(string.Format("SharedEntityType: {0}", dataObject.SharedEntityType));
            }
        }
        public void OutputArrayOfSharedEntityAssociation(IList<SharedEntityAssociation> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSharedEntityAssociation(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSharedList(SharedList dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ItemCount: {0}", dataObject.ItemCount));
                var negativekeywordlist = dataObject as NegativeKeywordList;
                if(negativekeywordlist != null)
                {
                    OutputNegativeKeywordList((NegativeKeywordList)dataObject);
                }
            }
        }
        public void OutputArrayOfSharedList(IList<SharedList> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSharedList(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSharedListItem(SharedListItem dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfKeyValuePairOfstringstring(dataObject.ForwardCompatibilityMap);
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                var negativekeyword = dataObject as NegativeKeyword;
                if(negativekeyword != null)
                {
                    OutputNegativeKeyword((NegativeKeyword)dataObject);
                }
            }
        }
        public void OutputArrayOfSharedListItem(IList<SharedListItem> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSharedListItem(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputShoppingSetting(ShoppingSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("LocalInventoryAdsEnabled: {0}", dataObject.LocalInventoryAdsEnabled));
                OutputStatusMessage(string.Format("Priority: {0}", dataObject.Priority));
                OutputStatusMessage(string.Format("SalesCountryCode: {0}", dataObject.SalesCountryCode));
                OutputStatusMessage(string.Format("StoreId: {0}", dataObject.StoreId));
            }
        }
        public void OutputArrayOfShoppingSetting(IList<ShoppingSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputShoppingSetting(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSitelinkAdExtension(SitelinkAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Description1: {0}", dataObject.Description1));
                OutputStatusMessage(string.Format("Description2: {0}", dataObject.Description2));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayText: {0}", dataObject.DisplayText));
                OutputArrayOfAppUrl(dataObject.FinalAppUrls);
                OutputArrayOfString(dataObject.FinalMobileUrls);
                OutputArrayOfString(dataObject.FinalUrls);
                OutputStatusMessage(string.Format("TrackingUrlTemplate: {0}", dataObject.TrackingUrlTemplate));
                OutputCustomParameters(dataObject.UrlCustomParameters);
            }
        }
        public void OutputArrayOfSitelinkAdExtension(IList<SitelinkAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSitelinkAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputStringRuleItem(StringRuleItem dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Operand: {0}", dataObject.Operand));
                OutputStatusMessage(string.Format("Operator: {0}", dataObject.Operator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfStringRuleItem(IList<StringRuleItem> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputStringRuleItem(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputStructuredSnippetAdExtension(StructuredSnippetAdExtension dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Header: {0}", dataObject.Header));
                OutputArrayOfString(dataObject.Values);
            }
        }
        public void OutputArrayOfStructuredSnippetAdExtension(IList<StructuredSnippetAdExtension> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputStructuredSnippetAdExtension(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputTargetCpaBiddingScheme(TargetCpaBiddingScheme dataObject)
        {
            if (null != dataObject)
            {
                OutputBid(dataObject.MaxCpc);
                OutputStatusMessage(string.Format("TargetCpa: {0}", dataObject.TargetCpa));
            }
        }
        public void OutputArrayOfTargetCpaBiddingScheme(IList<TargetCpaBiddingScheme> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputTargetCpaBiddingScheme(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputTargetSetting(TargetSetting dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfTargetSettingDetail(dataObject.Details);
            }
        }
        public void OutputArrayOfTargetSetting(IList<TargetSetting> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputTargetSetting(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputTargetSettingDetail(TargetSettingDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("CriterionTypeGroup: {0}", dataObject.CriterionTypeGroup));
                OutputStatusMessage(string.Format("TargetAndBid: {0}", dataObject.TargetAndBid));
            }
        }
        public void OutputArrayOfTargetSettingDetail(IList<TargetSettingDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputTargetSettingDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputTextAd(TextAd dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("DestinationUrl: {0}", dataObject.DestinationUrl));
                OutputStatusMessage(string.Format("DisplayUrl: {0}", dataObject.DisplayUrl));
                OutputStatusMessage(string.Format("Text: {0}", dataObject.Text));
                OutputStatusMessage(string.Format("Title: {0}", dataObject.Title));
            }
        }
        public void OutputArrayOfTextAd(IList<TextAd> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputTextAd(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputUetTag(UetTag dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Description: {0}", dataObject.Description));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("TrackingNoScript: {0}", dataObject.TrackingNoScript));
                OutputStatusMessage(string.Format("TrackingScript: {0}", dataObject.TrackingScript));
                OutputStatusMessage(string.Format("TrackingStatus: {0}", dataObject.TrackingStatus));
            }
        }
        public void OutputArrayOfUetTag(IList<UetTag> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputUetTag(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputUrlGoal(UrlGoal dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("UrlExpression: {0}", dataObject.UrlExpression));
                OutputStatusMessage(string.Format("UrlOperator: {0}", dataObject.UrlOperator));
            }
        }
        public void OutputArrayOfUrlGoal(IList<UrlGoal> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputUrlGoal(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputWebpage(Webpage dataObject)
        {
            if (null != dataObject)
            {
                OutputWebpageParameter(dataObject.Parameter);
            }
        }
        public void OutputArrayOfWebpage(IList<Webpage> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputWebpage(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputWebpageCondition(WebpageCondition dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Argument: {0}", dataObject.Argument));
                OutputStatusMessage(string.Format("Operand: {0}", dataObject.Operand));
            }
        }
        public void OutputArrayOfWebpageCondition(IList<WebpageCondition> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputWebpageCondition(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputWebpageParameter(WebpageParameter dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfWebpageCondition(dataObject.Conditions);
                OutputStatusMessage(string.Format("CriterionName: {0}", dataObject.CriterionName));
            }
        }
        public void OutputArrayOfWebpageParameter(IList<WebpageParameter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputWebpageParameter(dataObject);
                    OutputStatusMessage("\n");
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
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the string: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the long: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable long: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the int: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable int: {0}", item));
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
    }
}