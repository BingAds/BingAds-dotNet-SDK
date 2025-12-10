//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

namespace Microsoft.BingAds.Internal;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Microsoft.BingAds.V13.CampaignManagement;

public static partial class RestApiGeneration
{
    public static class Microsoft_BingAds_V13_CampaignManagement_EntityModifiers
    {
        private static void AddPrivateField(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
        {
            var field = containingType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(field.FieldType, jsonName);
            jsonPropertyInfo.Get = field.GetValue;
            jsonPropertyInfo.Set = field.SetValue;
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }

        private static void AddPrivateProperty(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
        {
            var property = containingType.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(property.PropertyType, jsonName);
            jsonPropertyInfo.Get = property.GetValue;
            jsonPropertyInfo.Set = property.SetValue;
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }

        public static void CustomizeEntities(JsonTypeInfo jsonTypeInfo)
        {
            if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
                return;

            if (CustomizeActions.TryGetValue(jsonTypeInfo.Type, out var customize))
            {
                customize(jsonTypeInfo);
            }
        }

        private static Dictionary<Type, Action<JsonTypeInfo>> CustomizeActions = new Dictionary<Type, Action<JsonTypeInfo>>
        {
            { typeof(AccountMigrationStatusesInfo), static t => CustomizeAccountMigrationStatusesInfo(t) },
            { typeof(AccountNegativeKeywordList), static t => CustomizeAccountNegativeKeywordList(t) },
            { typeof(AccountPlacementExclusionList), static t => CustomizeAccountPlacementExclusionList(t) },
            { typeof(AccountPlacementInclusionList), static t => CustomizeAccountPlacementInclusionList(t) },
            { typeof(AccountProperty), static t => CustomizeAccountProperty(t) },
            { typeof(ActionAdExtension), static t => CustomizeActionAdExtension(t) },
            { typeof(Ad), static t => CustomizeAd(t) },
            { typeof(AdApiError), static t => CustomizeAdApiError(t) },
            { typeof(AdApiFaultDetail), static t => CustomizeAdApiFaultDetail(t) },
            { typeof(AddAdExtensionsRequest), static t => CustomizeAddAdExtensionsRequest(t) },
            { typeof(AddAdExtensionsResponse), static t => CustomizeAddAdExtensionsResponse(t) },
            { typeof(AddAdGroupCriterionsRequest), static t => CustomizeAddAdGroupCriterionsRequest(t) },
            { typeof(AddAdGroupCriterionsResponse), static t => CustomizeAddAdGroupCriterionsResponse(t) },
            { typeof(AddAdGroupsRequest), static t => CustomizeAddAdGroupsRequest(t) },
            { typeof(AddAdGroupsResponse), static t => CustomizeAddAdGroupsResponse(t) },
            { typeof(AddAdsRequest), static t => CustomizeAddAdsRequest(t) },
            { typeof(AddAdsResponse), static t => CustomizeAddAdsResponse(t) },
            { typeof(AddAssetGroupsRequest), static t => CustomizeAddAssetGroupsRequest(t) },
            { typeof(AddAssetGroupsResponse), static t => CustomizeAddAssetGroupsResponse(t) },
            { typeof(AddAudienceGroupsRequest), static t => CustomizeAddAudienceGroupsRequest(t) },
            { typeof(AddAudienceGroupsResponse), static t => CustomizeAddAudienceGroupsResponse(t) },
            { typeof(AddAudiencesRequest), static t => CustomizeAddAudiencesRequest(t) },
            { typeof(AddAudiencesResponse), static t => CustomizeAddAudiencesResponse(t) },
            { typeof(AddBidStrategiesRequest), static t => CustomizeAddBidStrategiesRequest(t) },
            { typeof(AddBidStrategiesResponse), static t => CustomizeAddBidStrategiesResponse(t) },
            { typeof(AddBrandKitsRequest), static t => CustomizeAddBrandKitsRequest(t) },
            { typeof(AddBrandKitsResponse), static t => CustomizeAddBrandKitsResponse(t) },
            { typeof(AddBudgetsRequest), static t => CustomizeAddBudgetsRequest(t) },
            { typeof(AddBudgetsResponse), static t => CustomizeAddBudgetsResponse(t) },
            { typeof(AddCampaignConversionGoalsRequest), static t => CustomizeAddCampaignConversionGoalsRequest(t) },
            { typeof(AddCampaignConversionGoalsResponse), static t => CustomizeAddCampaignConversionGoalsResponse(t) },
            { typeof(AddCampaignCriterionsRequest), static t => CustomizeAddCampaignCriterionsRequest(t) },
            { typeof(AddCampaignCriterionsResponse), static t => CustomizeAddCampaignCriterionsResponse(t) },
            { typeof(AddCampaignsRequest), static t => CustomizeAddCampaignsRequest(t) },
            { typeof(AddCampaignsResponse), static t => CustomizeAddCampaignsResponse(t) },
            { typeof(AddConversionGoalsRequest), static t => CustomizeAddConversionGoalsRequest(t) },
            { typeof(AddConversionGoalsResponse), static t => CustomizeAddConversionGoalsResponse(t) },
            { typeof(AddConversionValueRulesRequest), static t => CustomizeAddConversionValueRulesRequest(t) },
            { typeof(AddConversionValueRulesResponse), static t => CustomizeAddConversionValueRulesResponse(t) },
            { typeof(AddDataExclusionsRequest), static t => CustomizeAddDataExclusionsRequest(t) },
            { typeof(AddDataExclusionsResponse), static t => CustomizeAddDataExclusionsResponse(t) },
            { typeof(AddExperimentsRequest), static t => CustomizeAddExperimentsRequest(t) },
            { typeof(AddExperimentsResponse), static t => CustomizeAddExperimentsResponse(t) },
            { typeof(AddHTML5sRequest), static t => CustomizeAddHTML5sRequest(t) },
            { typeof(AddHTML5sResponse), static t => CustomizeAddHTML5sResponse(t) },
            { typeof(AddImportJobsRequest), static t => CustomizeAddImportJobsRequest(t) },
            { typeof(AddImportJobsResponse), static t => CustomizeAddImportJobsResponse(t) },
            { typeof(AddKeywordsRequest), static t => CustomizeAddKeywordsRequest(t) },
            { typeof(AddKeywordsResponse), static t => CustomizeAddKeywordsResponse(t) },
            { typeof(AddLabelsRequest), static t => CustomizeAddLabelsRequest(t) },
            { typeof(AddLabelsResponse), static t => CustomizeAddLabelsResponse(t) },
            { typeof(AddLinkedInSegmentsRequest), static t => CustomizeAddLinkedInSegmentsRequest(t) },
            { typeof(AddLinkedInSegmentsResponse), static t => CustomizeAddLinkedInSegmentsResponse(t) },
            { typeof(AddListItemsToSharedListRequest), static t => CustomizeAddListItemsToSharedListRequest(t) },
            { typeof(AddListItemsToSharedListResponse), static t => CustomizeAddListItemsToSharedListResponse(t) },
            { typeof(AddMediaRequest), static t => CustomizeAddMediaRequest(t) },
            { typeof(AddMediaResponse), static t => CustomizeAddMediaResponse(t) },
            { typeof(AddNegativeKeywordsToEntitiesRequest), static t => CustomizeAddNegativeKeywordsToEntitiesRequest(t) },
            { typeof(AddNegativeKeywordsToEntitiesResponse), static t => CustomizeAddNegativeKeywordsToEntitiesResponse(t) },
            { typeof(AddNewCustomerAcquisitionGoalsRequest), static t => CustomizeAddNewCustomerAcquisitionGoalsRequest(t) },
            { typeof(AddNewCustomerAcquisitionGoalsResponse), static t => CustomizeAddNewCustomerAcquisitionGoalsResponse(t) },
            { typeof(Address), static t => CustomizeAddress(t) },
            { typeof(AddSeasonalityAdjustmentsRequest), static t => CustomizeAddSeasonalityAdjustmentsRequest(t) },
            { typeof(AddSeasonalityAdjustmentsResponse), static t => CustomizeAddSeasonalityAdjustmentsResponse(t) },
            { typeof(AddSharedEntityRequest), static t => CustomizeAddSharedEntityRequest(t) },
            { typeof(AddSharedEntityResponse), static t => CustomizeAddSharedEntityResponse(t) },
            { typeof(AddUetTagsRequest), static t => CustomizeAddUetTagsRequest(t) },
            { typeof(AddUetTagsResponse), static t => CustomizeAddUetTagsResponse(t) },
            { typeof(AddVideosRequest), static t => CustomizeAddVideosRequest(t) },
            { typeof(AddVideosResponse), static t => CustomizeAddVideosResponse(t) },
            { typeof(AdExtension), static t => CustomizeAdExtension(t) },
            { typeof(AdExtensionAssociation), static t => CustomizeAdExtensionAssociation(t) },
            { typeof(AdExtensionAssociationCollection), static t => CustomizeAdExtensionAssociationCollection(t) },
            { typeof(AdExtensionEditorialReason), static t => CustomizeAdExtensionEditorialReason(t) },
            { typeof(AdExtensionEditorialReasonCollection), static t => CustomizeAdExtensionEditorialReasonCollection(t) },
            { typeof(AdExtensionIdentity), static t => CustomizeAdExtensionIdentity(t) },
            { typeof(AdExtensionIdToEntityIdAssociation), static t => CustomizeAdExtensionIdToEntityIdAssociation(t) },
            { typeof(AdGroup), static t => CustomizeAdGroup(t) },
            { typeof(AdGroupCriterion), static t => CustomizeAdGroupCriterion(t) },
            { typeof(AdGroupCriterionAction), static t => CustomizeAdGroupCriterionAction(t) },
            { typeof(AdGroupNegativeSites), static t => CustomizeAdGroupNegativeSites(t) },
            { typeof(AdRecommendationCustomizedProperty), static t => CustomizeAdRecommendationCustomizedProperty(t) },
            { typeof(AdRecommendationImageAssetProperty), static t => CustomizeAdRecommendationImageAssetProperty(t) },
            { typeof(AdRecommendationImageRefineOperation), static t => CustomizeAdRecommendationImageRefineOperation(t) },
            { typeof(AdRecommendationImageSuggestion), static t => CustomizeAdRecommendationImageSuggestion(t) },
            { typeof(AdRecommendationImageSuggestionMetadata), static t => CustomizeAdRecommendationImageSuggestionMetadata(t) },
            { typeof(AdRecommendationJobInfo), static t => CustomizeAdRecommendationJobInfo(t) },
            { typeof(AdRecommendationMediaRefineResult), static t => CustomizeAdRecommendationMediaRefineResult(t) },
            { typeof(AdRecommendationRefinedMedia), static t => CustomizeAdRecommendationRefinedMedia(t) },
            { typeof(AdRecommendationTextAssetProperty), static t => CustomizeAdRecommendationTextAssetProperty(t) },
            { typeof(AdRecommendationTextRefineOperation), static t => CustomizeAdRecommendationTextRefineOperation(t) },
            { typeof(AdRecommendationTextRefineResult), static t => CustomizeAdRecommendationTextRefineResult(t) },
            { typeof(AdRecommendationVideoSuggestion), static t => CustomizeAdRecommendationVideoSuggestion(t) },
            { typeof(AdRotation), static t => CustomizeAdRotation(t) },
            { typeof(AgeCriterion), static t => CustomizeAgeCriterion(t) },
            { typeof(AgeDimension), static t => CustomizeAgeDimension(t) },
            { typeof(AnnotationOptOut), static t => CustomizeAnnotationOptOut(t) },
            { typeof(ApiFaultDetail), static t => CustomizeApiFaultDetail(t) },
            { typeof(AppAdExtension), static t => CustomizeAppAdExtension(t) },
            { typeof(AppDownloadGoal), static t => CustomizeAppDownloadGoal(t) },
            { typeof(AppealEditorialRejectionsRequest), static t => CustomizeAppealEditorialRejectionsRequest(t) },
            { typeof(AppealEditorialRejectionsResponse), static t => CustomizeAppealEditorialRejectionsResponse(t) },
            { typeof(AppInstallAd), static t => CustomizeAppInstallAd(t) },
            { typeof(AppInstallGoal), static t => CustomizeAppInstallGoal(t) },
            { typeof(ApplicationFault), static t => CustomizeApplicationFault(t) },
            { typeof(ApplyAssetGroupListingGroupActionsRequest), static t => CustomizeApplyAssetGroupListingGroupActionsRequest(t) },
            { typeof(ApplyAssetGroupListingGroupActionsResponse), static t => CustomizeApplyAssetGroupListingGroupActionsResponse(t) },
            { typeof(ApplyCustomerListItemsRequest), static t => CustomizeApplyCustomerListItemsRequest(t) },
            { typeof(ApplyCustomerListItemsResponse), static t => CustomizeApplyCustomerListItemsResponse(t) },
            { typeof(ApplyCustomerListUserDataRequest), static t => CustomizeApplyCustomerListUserDataRequest(t) },
            { typeof(ApplyCustomerListUserDataResponse), static t => CustomizeApplyCustomerListUserDataResponse(t) },
            { typeof(ApplyHotelGroupActionsRequest), static t => CustomizeApplyHotelGroupActionsRequest(t) },
            { typeof(ApplyHotelGroupActionsResponse), static t => CustomizeApplyHotelGroupActionsResponse(t) },
            { typeof(ApplyOfflineConversionAdjustmentsRequest), static t => CustomizeApplyOfflineConversionAdjustmentsRequest(t) },
            { typeof(ApplyOfflineConversionAdjustmentsResponse), static t => CustomizeApplyOfflineConversionAdjustmentsResponse(t) },
            { typeof(ApplyOfflineConversionsRequest), static t => CustomizeApplyOfflineConversionsRequest(t) },
            { typeof(ApplyOfflineConversionsResponse), static t => CustomizeApplyOfflineConversionsResponse(t) },
            { typeof(ApplyOnlineConversionAdjustmentsRequest), static t => CustomizeApplyOnlineConversionAdjustmentsRequest(t) },
            { typeof(ApplyOnlineConversionAdjustmentsResponse), static t => CustomizeApplyOnlineConversionAdjustmentsResponse(t) },
            { typeof(ApplyProductPartitionActionsRequest), static t => CustomizeApplyProductPartitionActionsRequest(t) },
            { typeof(ApplyProductPartitionActionsResponse), static t => CustomizeApplyProductPartitionActionsResponse(t) },
            { typeof(AppSetting), static t => CustomizeAppSetting(t) },
            { typeof(AppUrl), static t => CustomizeAppUrl(t) },
            { typeof(Asset), static t => CustomizeAsset(t) },
            { typeof(AssetGroup), static t => CustomizeAssetGroup(t) },
            { typeof(AssetGroupEditorialReason), static t => CustomizeAssetGroupEditorialReason(t) },
            { typeof(AssetGroupEditorialReasonCollection), static t => CustomizeAssetGroupEditorialReasonCollection(t) },
            { typeof(AssetGroupListingGroup), static t => CustomizeAssetGroupListingGroup(t) },
            { typeof(AssetGroupListingGroupAction), static t => CustomizeAssetGroupListingGroupAction(t) },
            { typeof(AssetGroupSearchTheme), static t => CustomizeAssetGroupSearchTheme(t) },
            { typeof(AssetGroupUrlTarget), static t => CustomizeAssetGroupUrlTarget(t) },
            { typeof(AssetLink), static t => CustomizeAssetLink(t) },
            { typeof(Audience), static t => CustomizeAudience(t) },
            { typeof(AudienceCondition), static t => CustomizeAudienceCondition(t) },
            { typeof(AudienceConditionItem), static t => CustomizeAudienceConditionItem(t) },
            { typeof(AudienceCriterion), static t => CustomizeAudienceCriterion(t) },
            { typeof(AudienceDimension), static t => CustomizeAudienceDimension(t) },
            { typeof(AudienceGroup), static t => CustomizeAudienceGroup(t) },
            { typeof(AudienceGroupAssetGroupAssociation), static t => CustomizeAudienceGroupAssetGroupAssociation(t) },
            { typeof(AudienceGroupDimension), static t => CustomizeAudienceGroupDimension(t) },
            { typeof(AudienceIdName), static t => CustomizeAudienceIdName(t) },
            { typeof(AudienceInfo), static t => CustomizeAudienceInfo(t) },
            { typeof(AudioFilter), static t => CustomizeAudioFilter(t) },
            { typeof(AuditPointResult), static t => CustomizeAuditPointResult(t) },
            { typeof(BatchError), static t => CustomizeBatchError(t) },
            { typeof(BatchErrorCollection), static t => CustomizeBatchErrorCollection(t) },
            { typeof(Bid), static t => CustomizeBid(t) },
            { typeof(BiddableAdGroupCriterion), static t => CustomizeBiddableAdGroupCriterion(t) },
            { typeof(BiddableCampaignCriterion), static t => CustomizeBiddableCampaignCriterion(t) },
            { typeof(BiddingScheme), static t => CustomizeBiddingScheme(t) },
            { typeof(BidMultiplier), static t => CustomizeBidMultiplier(t) },
            { typeof(BidStrategy), static t => CustomizeBidStrategy(t) },
            { typeof(BMCStore), static t => CustomizeBMCStore(t) },
            { typeof(BrandItem), static t => CustomizeBrandItem(t) },
            { typeof(BrandKit), static t => CustomizeBrandKit(t) },
            { typeof(BrandKitColor), static t => CustomizeBrandKitColor(t) },
            { typeof(BrandKitFont), static t => CustomizeBrandKitFont(t) },
            { typeof(BrandKitImage), static t => CustomizeBrandKitImage(t) },
            { typeof(BrandKitPalette), static t => CustomizeBrandKitPalette(t) },
            { typeof(BrandList), static t => CustomizeBrandList(t) },
            { typeof(BrandVoice), static t => CustomizeBrandVoice(t) },
            { typeof(Budget), static t => CustomizeBudget(t) },
            { typeof(CallAdExtension), static t => CustomizeCallAdExtension(t) },
            { typeof(CalloutAdExtension), static t => CustomizeCalloutAdExtension(t) },
            { typeof(CallToActionSetting), static t => CustomizeCallToActionSetting(t) },
            { typeof(Campaign), static t => CustomizeCampaign(t) },
            { typeof(CampaignAdGroupIds), static t => CustomizeCampaignAdGroupIds(t) },
            { typeof(CampaignAssociation), static t => CustomizeCampaignAssociation(t) },
            { typeof(CampaignConversionGoal), static t => CustomizeCampaignConversionGoal(t) },
            { typeof(CampaignCriterion), static t => CustomizeCampaignCriterion(t) },
            { typeof(CampaignNegativeSites), static t => CustomizeCampaignNegativeSites(t) },
            { typeof(CampaignSize), static t => CustomizeCampaignSize(t) },
            { typeof(CashbackAdjustment), static t => CustomizeCashbackAdjustment(t) },
            { typeof(CategoryResult), static t => CustomizeCategoryResult(t) },
            { typeof(ClipchampTemplateInfo), static t => CustomizeClipchampTemplateInfo(t) },
            { typeof(CombinationRule), static t => CustomizeCombinationRule(t) },
            { typeof(CombinedList), static t => CustomizeCombinedList(t) },
            { typeof(CommissionBiddingScheme), static t => CustomizeCommissionBiddingScheme(t) },
            { typeof(Company), static t => CustomizeCompany(t) },
            { typeof(CompanyList), static t => CustomizeCompanyList(t) },
            { typeof(CompanyName), static t => CustomizeCompanyName(t) },
            { typeof(ConversionGoal), static t => CustomizeConversionGoal(t) },
            { typeof(ConversionGoalRevenue), static t => CustomizeConversionGoalRevenue(t) },
            { typeof(ConversionValueRule), static t => CustomizeConversionValueRule(t) },
            { typeof(CoOpSetting), static t => CustomizeCoOpSetting(t) },
            { typeof(CostPerSaleBiddingScheme), static t => CustomizeCostPerSaleBiddingScheme(t) },
            { typeof(CreateAssetGroupRecommendationRequest), static t => CustomizeCreateAssetGroupRecommendationRequest(t) },
            { typeof(CreateAssetGroupRecommendationResponse), static t => CustomizeCreateAssetGroupRecommendationResponse(t) },
            { typeof(CreateBrandKitRecommendationRequest), static t => CustomizeCreateBrandKitRecommendationRequest(t) },
            { typeof(CreateBrandKitRecommendationResponse), static t => CustomizeCreateBrandKitRecommendationResponse(t) },
            { typeof(CreateResponsiveAdRecommendationRequest), static t => CustomizeCreateResponsiveAdRecommendationRequest(t) },
            { typeof(CreateResponsiveAdRecommendationResponse), static t => CustomizeCreateResponsiveAdRecommendationResponse(t) },
            { typeof(CreateResponsiveSearchAdRecommendationRequest), static t => CustomizeCreateResponsiveSearchAdRecommendationRequest(t) },
            { typeof(CreateResponsiveSearchAdRecommendationResponse), static t => CustomizeCreateResponsiveSearchAdRecommendationResponse(t) },
            { typeof(Criterion), static t => CustomizeCriterion(t) },
            { typeof(CriterionBid), static t => CustomizeCriterionBid(t) },
            { typeof(CriterionCashback), static t => CustomizeCriterionCashback(t) },
            { typeof(CustomAudience), static t => CustomizeCustomAudience(t) },
            { typeof(CustomerAccountShare), static t => CustomizeCustomerAccountShare(t) },
            { typeof(CustomerAccountShareAssociation), static t => CustomizeCustomerAccountShareAssociation(t) },
            { typeof(CustomerList), static t => CustomizeCustomerList(t) },
            { typeof(CustomerListUserData), static t => CustomizeCustomerListUserData(t) },
            { typeof(CustomerShare), static t => CustomizeCustomerShare(t) },
            { typeof(CustomEventsRule), static t => CustomizeCustomEventsRule(t) },
            { typeof(CustomParameter), static t => CustomizeCustomParameter(t) },
            { typeof(CustomParameters), static t => CustomizeCustomParameters(t) },
            { typeof(CustomSegment), static t => CustomizeCustomSegment(t) },
            { typeof(CustomSegmentCatalog), static t => CustomizeCustomSegmentCatalog(t) },
            { typeof(DailySummary), static t => CustomizeDailySummary(t) },
            { typeof(DataExclusion), static t => CustomizeDataExclusion(t) },
            { typeof(Date), static t => CustomizeDate(t) },
            { typeof(DayTime), static t => CustomizeDayTime(t) },
            { typeof(DayTimeCriterion), static t => CustomizeDayTimeCriterion(t) },
            { typeof(DealCriterion), static t => CustomizeDealCriterion(t) },
            { typeof(DeleteAdExtensionsAssociationsRequest), static t => CustomizeDeleteAdExtensionsAssociationsRequest(t) },
            { typeof(DeleteAdExtensionsAssociationsResponse), static t => CustomizeDeleteAdExtensionsAssociationsResponse(t) },
            { typeof(DeleteAdExtensionsRequest), static t => CustomizeDeleteAdExtensionsRequest(t) },
            { typeof(DeleteAdExtensionsResponse), static t => CustomizeDeleteAdExtensionsResponse(t) },
            { typeof(DeleteAdGroupCriterionsRequest), static t => CustomizeDeleteAdGroupCriterionsRequest(t) },
            { typeof(DeleteAdGroupCriterionsResponse), static t => CustomizeDeleteAdGroupCriterionsResponse(t) },
            { typeof(DeleteAdGroupsRequest), static t => CustomizeDeleteAdGroupsRequest(t) },
            { typeof(DeleteAdGroupsResponse), static t => CustomizeDeleteAdGroupsResponse(t) },
            { typeof(DeleteAdsRequest), static t => CustomizeDeleteAdsRequest(t) },
            { typeof(DeleteAdsResponse), static t => CustomizeDeleteAdsResponse(t) },
            { typeof(DeleteAssetGroupsRequest), static t => CustomizeDeleteAssetGroupsRequest(t) },
            { typeof(DeleteAssetGroupsResponse), static t => CustomizeDeleteAssetGroupsResponse(t) },
            { typeof(DeleteAudienceGroupAssetGroupAssociationsRequest), static t => CustomizeDeleteAudienceGroupAssetGroupAssociationsRequest(t) },
            { typeof(DeleteAudienceGroupAssetGroupAssociationsResponse), static t => CustomizeDeleteAudienceGroupAssetGroupAssociationsResponse(t) },
            { typeof(DeleteAudienceGroupsRequest), static t => CustomizeDeleteAudienceGroupsRequest(t) },
            { typeof(DeleteAudienceGroupsResponse), static t => CustomizeDeleteAudienceGroupsResponse(t) },
            { typeof(DeleteAudiencesRequest), static t => CustomizeDeleteAudiencesRequest(t) },
            { typeof(DeleteAudiencesResponse), static t => CustomizeDeleteAudiencesResponse(t) },
            { typeof(DeleteBidStrategiesRequest), static t => CustomizeDeleteBidStrategiesRequest(t) },
            { typeof(DeleteBidStrategiesResponse), static t => CustomizeDeleteBidStrategiesResponse(t) },
            { typeof(DeleteBrandKitsRequest), static t => CustomizeDeleteBrandKitsRequest(t) },
            { typeof(DeleteBrandKitsResponse), static t => CustomizeDeleteBrandKitsResponse(t) },
            { typeof(DeleteBudgetsRequest), static t => CustomizeDeleteBudgetsRequest(t) },
            { typeof(DeleteBudgetsResponse), static t => CustomizeDeleteBudgetsResponse(t) },
            { typeof(DeleteCampaignConversionGoalsRequest), static t => CustomizeDeleteCampaignConversionGoalsRequest(t) },
            { typeof(DeleteCampaignConversionGoalsResponse), static t => CustomizeDeleteCampaignConversionGoalsResponse(t) },
            { typeof(DeleteCampaignCriterionsRequest), static t => CustomizeDeleteCampaignCriterionsRequest(t) },
            { typeof(DeleteCampaignCriterionsResponse), static t => CustomizeDeleteCampaignCriterionsResponse(t) },
            { typeof(DeleteCampaignsRequest), static t => CustomizeDeleteCampaignsRequest(t) },
            { typeof(DeleteCampaignsResponse), static t => CustomizeDeleteCampaignsResponse(t) },
            { typeof(DeleteDataExclusionsRequest), static t => CustomizeDeleteDataExclusionsRequest(t) },
            { typeof(DeleteDataExclusionsResponse), static t => CustomizeDeleteDataExclusionsResponse(t) },
            { typeof(DeleteExperimentsRequest), static t => CustomizeDeleteExperimentsRequest(t) },
            { typeof(DeleteExperimentsResponse), static t => CustomizeDeleteExperimentsResponse(t) },
            { typeof(DeleteHTML5sRequest), static t => CustomizeDeleteHTML5sRequest(t) },
            { typeof(DeleteHTML5sResponse), static t => CustomizeDeleteHTML5sResponse(t) },
            { typeof(DeleteImportJobsRequest), static t => CustomizeDeleteImportJobsRequest(t) },
            { typeof(DeleteImportJobsResponse), static t => CustomizeDeleteImportJobsResponse(t) },
            { typeof(DeleteKeywordsRequest), static t => CustomizeDeleteKeywordsRequest(t) },
            { typeof(DeleteKeywordsResponse), static t => CustomizeDeleteKeywordsResponse(t) },
            { typeof(DeleteLabelAssociationsRequest), static t => CustomizeDeleteLabelAssociationsRequest(t) },
            { typeof(DeleteLabelAssociationsResponse), static t => CustomizeDeleteLabelAssociationsResponse(t) },
            { typeof(DeleteLabelsRequest), static t => CustomizeDeleteLabelsRequest(t) },
            { typeof(DeleteLabelsResponse), static t => CustomizeDeleteLabelsResponse(t) },
            { typeof(DeleteLinkedInSegmentsRequest), static t => CustomizeDeleteLinkedInSegmentsRequest(t) },
            { typeof(DeleteLinkedInSegmentsResponse), static t => CustomizeDeleteLinkedInSegmentsResponse(t) },
            { typeof(DeleteListItemsFromSharedListRequest), static t => CustomizeDeleteListItemsFromSharedListRequest(t) },
            { typeof(DeleteListItemsFromSharedListResponse), static t => CustomizeDeleteListItemsFromSharedListResponse(t) },
            { typeof(DeleteMediaRequest), static t => CustomizeDeleteMediaRequest(t) },
            { typeof(DeleteMediaResponse), static t => CustomizeDeleteMediaResponse(t) },
            { typeof(DeleteNegativeKeywordsFromEntitiesRequest), static t => CustomizeDeleteNegativeKeywordsFromEntitiesRequest(t) },
            { typeof(DeleteNegativeKeywordsFromEntitiesResponse), static t => CustomizeDeleteNegativeKeywordsFromEntitiesResponse(t) },
            { typeof(DeleteSeasonalityAdjustmentsRequest), static t => CustomizeDeleteSeasonalityAdjustmentsRequest(t) },
            { typeof(DeleteSeasonalityAdjustmentsResponse), static t => CustomizeDeleteSeasonalityAdjustmentsResponse(t) },
            { typeof(DeleteSharedEntitiesRequest), static t => CustomizeDeleteSharedEntitiesRequest(t) },
            { typeof(DeleteSharedEntitiesResponse), static t => CustomizeDeleteSharedEntitiesResponse(t) },
            { typeof(DeleteSharedEntityAssociationsRequest), static t => CustomizeDeleteSharedEntityAssociationsRequest(t) },
            { typeof(DeleteSharedEntityAssociationsResponse), static t => CustomizeDeleteSharedEntityAssociationsResponse(t) },
            { typeof(DeleteVideosRequest), static t => CustomizeDeleteVideosRequest(t) },
            { typeof(DeleteVideosResponse), static t => CustomizeDeleteVideosResponse(t) },
            { typeof(DeviceCondition), static t => CustomizeDeviceCondition(t) },
            { typeof(DeviceCriterion), static t => CustomizeDeviceCriterion(t) },
            { typeof(DiagnosticsEntity), static t => CustomizeDiagnosticsEntity(t) },
            { typeof(DiagnosticsFilter), static t => CustomizeDiagnosticsFilter(t) },
            { typeof(DiagnosticsRequestStatus), static t => CustomizeDiagnosticsRequestStatus(t) },
            { typeof(DiagnosticsSettings), static t => CustomizeDiagnosticsSettings(t) },
            { typeof(DisclaimerAdExtension), static t => CustomizeDisclaimerAdExtension(t) },
            { typeof(DisclaimerSetting), static t => CustomizeDisclaimerSetting(t) },
            { typeof(DurationGoal), static t => CustomizeDurationGoal(t) },
            { typeof(DynamicFeedSetting), static t => CustomizeDynamicFeedSetting(t) },
            { typeof(DynamicSearchAd), static t => CustomizeDynamicSearchAd(t) },
            { typeof(DynamicSearchAdsSetting), static t => CustomizeDynamicSearchAdsSetting(t) },
            { typeof(EditorialApiFaultDetail), static t => CustomizeEditorialApiFaultDetail(t) },
            { typeof(EditorialError), static t => CustomizeEditorialError(t) },
            { typeof(EditorialErrorCollection), static t => CustomizeEditorialErrorCollection(t) },
            { typeof(EditorialReason), static t => CustomizeEditorialReason(t) },
            { typeof(EditorialReasonCollection), static t => CustomizeEditorialReasonCollection(t) },
            { typeof(EnhancedCpcBiddingScheme), static t => CustomizeEnhancedCpcBiddingScheme(t) },
            { typeof(EntityIdToParentIdAssociation), static t => CustomizeEntityIdToParentIdAssociation(t) },
            { typeof(EntityNegativeKeyword), static t => CustomizeEntityNegativeKeyword(t) },
            { typeof(EntityResult), static t => CustomizeEntityResult(t) },
            { typeof(EventGoal), static t => CustomizeEventGoal(t) },
            { typeof(ExpandedTextAd), static t => CustomizeExpandedTextAd(t) },
            { typeof(Experiment), static t => CustomizeExperiment(t) },
            { typeof(FileImportJob), static t => CustomizeFileImportJob(t) },
            { typeof(FileImportOption), static t => CustomizeFileImportOption(t) },
            { typeof(FilterLinkAdExtension), static t => CustomizeFilterLinkAdExtension(t) },
            { typeof(FixedBid), static t => CustomizeFixedBid(t) },
            { typeof(FlyerAdExtension), static t => CustomizeFlyerAdExtension(t) },
            { typeof(Frequency), static t => CustomizeFrequency(t) },
            { typeof(FrequencyCapSettings), static t => CustomizeFrequencyCapSettings(t) },
            { typeof(GenderCriterion), static t => CustomizeGenderCriterion(t) },
            { typeof(GenderDimension), static t => CustomizeGenderDimension(t) },
            { typeof(GenreCriterion), static t => CustomizeGenreCriterion(t) },
            { typeof(GeoPoint), static t => CustomizeGeoPoint(t) },
            { typeof(GetAccountMigrationStatusesRequest), static t => CustomizeGetAccountMigrationStatusesRequest(t) },
            { typeof(GetAccountMigrationStatusesResponse), static t => CustomizeGetAccountMigrationStatusesResponse(t) },
            { typeof(GetAccountPropertiesRequest), static t => CustomizeGetAccountPropertiesRequest(t) },
            { typeof(GetAccountPropertiesResponse), static t => CustomizeGetAccountPropertiesResponse(t) },
            { typeof(GetAdExtensionIdsByAccountIdRequest), static t => CustomizeGetAdExtensionIdsByAccountIdRequest(t) },
            { typeof(GetAdExtensionIdsByAccountIdResponse), static t => CustomizeGetAdExtensionIdsByAccountIdResponse(t) },
            { typeof(GetAdExtensionsAssociationsRequest), static t => CustomizeGetAdExtensionsAssociationsRequest(t) },
            { typeof(GetAdExtensionsAssociationsResponse), static t => CustomizeGetAdExtensionsAssociationsResponse(t) },
            { typeof(GetAdExtensionsByIdsRequest), static t => CustomizeGetAdExtensionsByIdsRequest(t) },
            { typeof(GetAdExtensionsByIdsResponse), static t => CustomizeGetAdExtensionsByIdsResponse(t) },
            { typeof(GetAdExtensionsEditorialReasonsRequest), static t => CustomizeGetAdExtensionsEditorialReasonsRequest(t) },
            { typeof(GetAdExtensionsEditorialReasonsResponse), static t => CustomizeGetAdExtensionsEditorialReasonsResponse(t) },
            { typeof(GetAdGroupCriterionsByIdsRequest), static t => CustomizeGetAdGroupCriterionsByIdsRequest(t) },
            { typeof(GetAdGroupCriterionsByIdsResponse), static t => CustomizeGetAdGroupCriterionsByIdsResponse(t) },
            { typeof(GetAdGroupsByCampaignIdRequest), static t => CustomizeGetAdGroupsByCampaignIdRequest(t) },
            { typeof(GetAdGroupsByCampaignIdResponse), static t => CustomizeGetAdGroupsByCampaignIdResponse(t) },
            { typeof(GetAdGroupsByIdsRequest), static t => CustomizeGetAdGroupsByIdsRequest(t) },
            { typeof(GetAdGroupsByIdsResponse), static t => CustomizeGetAdGroupsByIdsResponse(t) },
            { typeof(GetAdsByAdGroupIdRequest), static t => CustomizeGetAdsByAdGroupIdRequest(t) },
            { typeof(GetAdsByAdGroupIdResponse), static t => CustomizeGetAdsByAdGroupIdResponse(t) },
            { typeof(GetAdsByEditorialStatusRequest), static t => CustomizeGetAdsByEditorialStatusRequest(t) },
            { typeof(GetAdsByEditorialStatusResponse), static t => CustomizeGetAdsByEditorialStatusResponse(t) },
            { typeof(GetAdsByIdsRequest), static t => CustomizeGetAdsByIdsRequest(t) },
            { typeof(GetAdsByIdsResponse), static t => CustomizeGetAdsByIdsResponse(t) },
            { typeof(GetAnnotationOptOutRequest), static t => CustomizeGetAnnotationOptOutRequest(t) },
            { typeof(GetAnnotationOptOutResponse), static t => CustomizeGetAnnotationOptOutResponse(t) },
            { typeof(GetAssetGroupListingGroupsByIdsRequest), static t => CustomizeGetAssetGroupListingGroupsByIdsRequest(t) },
            { typeof(GetAssetGroupListingGroupsByIdsResponse), static t => CustomizeGetAssetGroupListingGroupsByIdsResponse(t) },
            { typeof(GetAssetGroupsByCampaignIdRequest), static t => CustomizeGetAssetGroupsByCampaignIdRequest(t) },
            { typeof(GetAssetGroupsByCampaignIdResponse), static t => CustomizeGetAssetGroupsByCampaignIdResponse(t) },
            { typeof(GetAssetGroupsByIdsRequest), static t => CustomizeGetAssetGroupsByIdsRequest(t) },
            { typeof(GetAssetGroupsByIdsResponse), static t => CustomizeGetAssetGroupsByIdsResponse(t) },
            { typeof(GetAssetGroupsEditorialReasonsRequest), static t => CustomizeGetAssetGroupsEditorialReasonsRequest(t) },
            { typeof(GetAssetGroupsEditorialReasonsResponse), static t => CustomizeGetAssetGroupsEditorialReasonsResponse(t) },
            { typeof(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest), static t => CustomizeGetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest(t) },
            { typeof(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse), static t => CustomizeGetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse(t) },
            { typeof(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest), static t => CustomizeGetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest(t) },
            { typeof(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse), static t => CustomizeGetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse(t) },
            { typeof(GetAudienceGroupsByIdsRequest), static t => CustomizeGetAudienceGroupsByIdsRequest(t) },
            { typeof(GetAudienceGroupsByIdsResponse), static t => CustomizeGetAudienceGroupsByIdsResponse(t) },
            { typeof(GetAudiencesByIdsRequest), static t => CustomizeGetAudiencesByIdsRequest(t) },
            { typeof(GetAudiencesByIdsResponse), static t => CustomizeGetAudiencesByIdsResponse(t) },
            { typeof(GetBidStrategiesByIdsRequest), static t => CustomizeGetBidStrategiesByIdsRequest(t) },
            { typeof(GetBidStrategiesByIdsResponse), static t => CustomizeGetBidStrategiesByIdsResponse(t) },
            { typeof(GetBMCStoresByCustomerIdRequest), static t => CustomizeGetBMCStoresByCustomerIdRequest(t) },
            { typeof(GetBMCStoresByCustomerIdResponse), static t => CustomizeGetBMCStoresByCustomerIdResponse(t) },
            { typeof(GetBrandKitsByAccountIdRequest), static t => CustomizeGetBrandKitsByAccountIdRequest(t) },
            { typeof(GetBrandKitsByAccountIdResponse), static t => CustomizeGetBrandKitsByAccountIdResponse(t) },
            { typeof(GetBrandKitsByIdsRequest), static t => CustomizeGetBrandKitsByIdsRequest(t) },
            { typeof(GetBrandKitsByIdsResponse), static t => CustomizeGetBrandKitsByIdsResponse(t) },
            { typeof(GetBSCCountriesRequest), static t => CustomizeGetBSCCountriesRequest(t) },
            { typeof(GetBSCCountriesResponse), static t => CustomizeGetBSCCountriesResponse(t) },
            { typeof(GetBudgetsByIdsRequest), static t => CustomizeGetBudgetsByIdsRequest(t) },
            { typeof(GetBudgetsByIdsResponse), static t => CustomizeGetBudgetsByIdsResponse(t) },
            { typeof(GetCampaignCriterionsByIdsRequest), static t => CustomizeGetCampaignCriterionsByIdsRequest(t) },
            { typeof(GetCampaignCriterionsByIdsResponse), static t => CustomizeGetCampaignCriterionsByIdsResponse(t) },
            { typeof(GetCampaignIdsByBidStrategyIdsRequest), static t => CustomizeGetCampaignIdsByBidStrategyIdsRequest(t) },
            { typeof(GetCampaignIdsByBidStrategyIdsResponse), static t => CustomizeGetCampaignIdsByBidStrategyIdsResponse(t) },
            { typeof(GetCampaignIdsByBudgetIdsRequest), static t => CustomizeGetCampaignIdsByBudgetIdsRequest(t) },
            { typeof(GetCampaignIdsByBudgetIdsResponse), static t => CustomizeGetCampaignIdsByBudgetIdsResponse(t) },
            { typeof(GetCampaignsByAccountIdRequest), static t => CustomizeGetCampaignsByAccountIdRequest(t) },
            { typeof(GetCampaignsByAccountIdResponse), static t => CustomizeGetCampaignsByAccountIdResponse(t) },
            { typeof(GetCampaignsByIdsRequest), static t => CustomizeGetCampaignsByIdsRequest(t) },
            { typeof(GetCampaignsByIdsResponse), static t => CustomizeGetCampaignsByIdsResponse(t) },
            { typeof(GetCampaignSizesByAccountIdRequest), static t => CustomizeGetCampaignSizesByAccountIdRequest(t) },
            { typeof(GetCampaignSizesByAccountIdResponse), static t => CustomizeGetCampaignSizesByAccountIdResponse(t) },
            { typeof(GetClipchampTemplatesRequest), static t => CustomizeGetClipchampTemplatesRequest(t) },
            { typeof(GetClipchampTemplatesResponse), static t => CustomizeGetClipchampTemplatesResponse(t) },
            { typeof(GetConfigValueRequest), static t => CustomizeGetConfigValueRequest(t) },
            { typeof(GetConfigValueResponse), static t => CustomizeGetConfigValueResponse(t) },
            { typeof(GetConversionGoalsByIdsRequest), static t => CustomizeGetConversionGoalsByIdsRequest(t) },
            { typeof(GetConversionGoalsByIdsResponse), static t => CustomizeGetConversionGoalsByIdsResponse(t) },
            { typeof(GetConversionGoalsByTagIdsRequest), static t => CustomizeGetConversionGoalsByTagIdsRequest(t) },
            { typeof(GetConversionGoalsByTagIdsResponse), static t => CustomizeGetConversionGoalsByTagIdsResponse(t) },
            { typeof(GetConversionValueRulesByAccountIdRequest), static t => CustomizeGetConversionValueRulesByAccountIdRequest(t) },
            { typeof(GetConversionValueRulesByAccountIdResponse), static t => CustomizeGetConversionValueRulesByAccountIdResponse(t) },
            { typeof(GetConversionValueRulesByIdsRequest), static t => CustomizeGetConversionValueRulesByIdsRequest(t) },
            { typeof(GetConversionValueRulesByIdsResponse), static t => CustomizeGetConversionValueRulesByIdsResponse(t) },
            { typeof(GetDataExclusionsByAccountIdRequest), static t => CustomizeGetDataExclusionsByAccountIdRequest(t) },
            { typeof(GetDataExclusionsByAccountIdResponse), static t => CustomizeGetDataExclusionsByAccountIdResponse(t) },
            { typeof(GetDataExclusionsByIdsRequest), static t => CustomizeGetDataExclusionsByIdsRequest(t) },
            { typeof(GetDataExclusionsByIdsResponse), static t => CustomizeGetDataExclusionsByIdsResponse(t) },
            { typeof(GetDiagnosticsRequest), static t => CustomizeGetDiagnosticsRequest(t) },
            { typeof(GetDiagnosticsResponse), static t => CustomizeGetDiagnosticsResponse(t) },
            { typeof(GetEditorialReasonsByIdsRequest), static t => CustomizeGetEditorialReasonsByIdsRequest(t) },
            { typeof(GetEditorialReasonsByIdsResponse), static t => CustomizeGetEditorialReasonsByIdsResponse(t) },
            { typeof(GetExperimentsByIdsRequest), static t => CustomizeGetExperimentsByIdsRequest(t) },
            { typeof(GetExperimentsByIdsResponse), static t => CustomizeGetExperimentsByIdsResponse(t) },
            { typeof(GetFileImportUploadUrlRequest), static t => CustomizeGetFileImportUploadUrlRequest(t) },
            { typeof(GetFileImportUploadUrlResponse), static t => CustomizeGetFileImportUploadUrlResponse(t) },
            { typeof(GetGeoLocationsFileUrlRequest), static t => CustomizeGetGeoLocationsFileUrlRequest(t) },
            { typeof(GetGeoLocationsFileUrlResponse), static t => CustomizeGetGeoLocationsFileUrlResponse(t) },
            { typeof(GetHealthCheckRequest), static t => CustomizeGetHealthCheckRequest(t) },
            { typeof(GetHealthCheckResponse), static t => CustomizeGetHealthCheckResponse(t) },
            { typeof(GetHTML5sByIdsRequest), static t => CustomizeGetHTML5sByIdsRequest(t) },
            { typeof(GetHTML5sByIdsResponse), static t => CustomizeGetHTML5sByIdsResponse(t) },
            { typeof(GetImportEntityIdsMappingRequest), static t => CustomizeGetImportEntityIdsMappingRequest(t) },
            { typeof(GetImportEntityIdsMappingResponse), static t => CustomizeGetImportEntityIdsMappingResponse(t) },
            { typeof(GetImportJobsByIdsRequest), static t => CustomizeGetImportJobsByIdsRequest(t) },
            { typeof(GetImportJobsByIdsResponse), static t => CustomizeGetImportJobsByIdsResponse(t) },
            { typeof(GetImportResultsRequest), static t => CustomizeGetImportResultsRequest(t) },
            { typeof(GetImportResultsResponse), static t => CustomizeGetImportResultsResponse(t) },
            { typeof(GetKeywordsByAdGroupIdRequest), static t => CustomizeGetKeywordsByAdGroupIdRequest(t) },
            { typeof(GetKeywordsByAdGroupIdResponse), static t => CustomizeGetKeywordsByAdGroupIdResponse(t) },
            { typeof(GetKeywordsByEditorialStatusRequest), static t => CustomizeGetKeywordsByEditorialStatusRequest(t) },
            { typeof(GetKeywordsByEditorialStatusResponse), static t => CustomizeGetKeywordsByEditorialStatusResponse(t) },
            { typeof(GetKeywordsByIdsRequest), static t => CustomizeGetKeywordsByIdsRequest(t) },
            { typeof(GetKeywordsByIdsResponse), static t => CustomizeGetKeywordsByIdsResponse(t) },
            { typeof(GetLabelAssociationsByEntityIdsRequest), static t => CustomizeGetLabelAssociationsByEntityIdsRequest(t) },
            { typeof(GetLabelAssociationsByEntityIdsResponse), static t => CustomizeGetLabelAssociationsByEntityIdsResponse(t) },
            { typeof(GetLabelAssociationsByLabelIdsRequest), static t => CustomizeGetLabelAssociationsByLabelIdsRequest(t) },
            { typeof(GetLabelAssociationsByLabelIdsResponse), static t => CustomizeGetLabelAssociationsByLabelIdsResponse(t) },
            { typeof(GetLabelsByIdsRequest), static t => CustomizeGetLabelsByIdsRequest(t) },
            { typeof(GetLabelsByIdsResponse), static t => CustomizeGetLabelsByIdsResponse(t) },
            { typeof(GetListItemsBySharedListRequest), static t => CustomizeGetListItemsBySharedListRequest(t) },
            { typeof(GetListItemsBySharedListResponse), static t => CustomizeGetListItemsBySharedListResponse(t) },
            { typeof(GetMediaAssociationsRequest), static t => CustomizeGetMediaAssociationsRequest(t) },
            { typeof(GetMediaAssociationsResponse), static t => CustomizeGetMediaAssociationsResponse(t) },
            { typeof(GetMediaMetaDataByAccountIdRequest), static t => CustomizeGetMediaMetaDataByAccountIdRequest(t) },
            { typeof(GetMediaMetaDataByAccountIdResponse), static t => CustomizeGetMediaMetaDataByAccountIdResponse(t) },
            { typeof(GetMediaMetaDataByIdsRequest), static t => CustomizeGetMediaMetaDataByIdsRequest(t) },
            { typeof(GetMediaMetaDataByIdsResponse), static t => CustomizeGetMediaMetaDataByIdsResponse(t) },
            { typeof(GetNegativeKeywordsByEntityIdsRequest), static t => CustomizeGetNegativeKeywordsByEntityIdsRequest(t) },
            { typeof(GetNegativeKeywordsByEntityIdsResponse), static t => CustomizeGetNegativeKeywordsByEntityIdsResponse(t) },
            { typeof(GetNegativeSitesByAdGroupIdsRequest), static t => CustomizeGetNegativeSitesByAdGroupIdsRequest(t) },
            { typeof(GetNegativeSitesByAdGroupIdsResponse), static t => CustomizeGetNegativeSitesByAdGroupIdsResponse(t) },
            { typeof(GetNegativeSitesByCampaignIdsRequest), static t => CustomizeGetNegativeSitesByCampaignIdsRequest(t) },
            { typeof(GetNegativeSitesByCampaignIdsResponse), static t => CustomizeGetNegativeSitesByCampaignIdsResponse(t) },
            { typeof(GetNewCustomerAcquisitionGoalsByAccountIdRequest), static t => CustomizeGetNewCustomerAcquisitionGoalsByAccountIdRequest(t) },
            { typeof(GetNewCustomerAcquisitionGoalsByAccountIdResponse), static t => CustomizeGetNewCustomerAcquisitionGoalsByAccountIdResponse(t) },
            { typeof(GetOfflineConversionReportsRequest), static t => CustomizeGetOfflineConversionReportsRequest(t) },
            { typeof(GetOfflineConversionReportsResponse), static t => CustomizeGetOfflineConversionReportsResponse(t) },
            { typeof(GetProfileDataFileUrlRequest), static t => CustomizeGetProfileDataFileUrlRequest(t) },
            { typeof(GetProfileDataFileUrlResponse), static t => CustomizeGetProfileDataFileUrlResponse(t) },
            { typeof(GetResponsiveAdRecommendationJobRequest), static t => CustomizeGetResponsiveAdRecommendationJobRequest(t) },
            { typeof(GetResponsiveAdRecommendationJobResponse), static t => CustomizeGetResponsiveAdRecommendationJobResponse(t) },
            { typeof(GetSeasonalityAdjustmentsByAccountIdRequest), static t => CustomizeGetSeasonalityAdjustmentsByAccountIdRequest(t) },
            { typeof(GetSeasonalityAdjustmentsByAccountIdResponse), static t => CustomizeGetSeasonalityAdjustmentsByAccountIdResponse(t) },
            { typeof(GetSeasonalityAdjustmentsByIdsRequest), static t => CustomizeGetSeasonalityAdjustmentsByIdsRequest(t) },
            { typeof(GetSeasonalityAdjustmentsByIdsResponse), static t => CustomizeGetSeasonalityAdjustmentsByIdsResponse(t) },
            { typeof(GetSharedEntitiesByAccountIdRequest), static t => CustomizeGetSharedEntitiesByAccountIdRequest(t) },
            { typeof(GetSharedEntitiesByAccountIdResponse), static t => CustomizeGetSharedEntitiesByAccountIdResponse(t) },
            { typeof(GetSharedEntitiesRequest), static t => CustomizeGetSharedEntitiesRequest(t) },
            { typeof(GetSharedEntitiesResponse), static t => CustomizeGetSharedEntitiesResponse(t) },
            { typeof(GetSharedEntityAssociationsByEntityIdsRequest), static t => CustomizeGetSharedEntityAssociationsByEntityIdsRequest(t) },
            { typeof(GetSharedEntityAssociationsByEntityIdsResponse), static t => CustomizeGetSharedEntityAssociationsByEntityIdsResponse(t) },
            { typeof(GetSharedEntityAssociationsBySharedEntityIdsRequest), static t => CustomizeGetSharedEntityAssociationsBySharedEntityIdsRequest(t) },
            { typeof(GetSharedEntityAssociationsBySharedEntityIdsResponse), static t => CustomizeGetSharedEntityAssociationsBySharedEntityIdsResponse(t) },
            { typeof(GetSupportedClipchampAudioRequest), static t => CustomizeGetSupportedClipchampAudioRequest(t) },
            { typeof(GetSupportedClipchampAudioResponse), static t => CustomizeGetSupportedClipchampAudioResponse(t) },
            { typeof(GetSupportedFontsRequest), static t => CustomizeGetSupportedFontsRequest(t) },
            { typeof(GetSupportedFontsResponse), static t => CustomizeGetSupportedFontsResponse(t) },
            { typeof(GetUetTagsByIdsRequest), static t => CustomizeGetUetTagsByIdsRequest(t) },
            { typeof(GetUetTagsByIdsResponse), static t => CustomizeGetUetTagsByIdsResponse(t) },
            { typeof(GetVideosByIdsRequest), static t => CustomizeGetVideosByIdsRequest(t) },
            { typeof(GetVideosByIdsResponse), static t => CustomizeGetVideosByIdsResponse(t) },
            { typeof(GoogleImportJob), static t => CustomizeGoogleImportJob(t) },
            { typeof(GoogleImportOption), static t => CustomizeGoogleImportOption(t) },
            { typeof(HealthCheckActionLinkMetadata), static t => CustomizeHealthCheckActionLinkMetadata(t) },
            { typeof(HealthCheckColumnMetadata), static t => CustomizeHealthCheckColumnMetadata(t) },
            { typeof(HealthCheckData), static t => CustomizeHealthCheckData(t) },
            { typeof(HealthCheckEntity), static t => CustomizeHealthCheckEntity(t) },
            { typeof(HealthCheckError), static t => CustomizeHealthCheckError(t) },
            { typeof(HealthCheckMetadata), static t => CustomizeHealthCheckMetadata(t) },
            { typeof(HealthCheckSubEntityData), static t => CustomizeHealthCheckSubEntityData(t) },
            { typeof(HotelAd), static t => CustomizeHotelAd(t) },
            { typeof(HotelAdvanceBookingWindowCriterion), static t => CustomizeHotelAdvanceBookingWindowCriterion(t) },
            { typeof(HotelCheckInDateCriterion), static t => CustomizeHotelCheckInDateCriterion(t) },
            { typeof(HotelCheckInDayCriterion), static t => CustomizeHotelCheckInDayCriterion(t) },
            { typeof(HotelDateSelectionTypeCriterion), static t => CustomizeHotelDateSelectionTypeCriterion(t) },
            { typeof(HotelGroup), static t => CustomizeHotelGroup(t) },
            { typeof(HotelLengthOfStayCriterion), static t => CustomizeHotelLengthOfStayCriterion(t) },
            { typeof(HotelListing), static t => CustomizeHotelListing(t) },
            { typeof(HotelSetting), static t => CustomizeHotelSetting(t) },
            { typeof(HTML5), static t => CustomizeHTML5(t) },
            { typeof(IdCollection), static t => CustomizeIdCollection(t) },
            { typeof(Image), static t => CustomizeImage(t) },
            { typeof(ImageAdExtension), static t => CustomizeImageAdExtension(t) },
            { typeof(ImageAsset), static t => CustomizeImageAsset(t) },
            { typeof(ImageMediaRepresentation), static t => CustomizeImageMediaRepresentation(t) },
            { typeof(ImportEntityStatistics), static t => CustomizeImportEntityStatistics(t) },
            { typeof(ImportJob), static t => CustomizeImportJob(t) },
            { typeof(ImportOption), static t => CustomizeImportOption(t) },
            { typeof(ImportResult), static t => CustomizeImportResult(t) },
            { typeof(ImportSearchAndReplaceForStringProperty), static t => CustomizeImportSearchAndReplaceForStringProperty(t) },
            { typeof(ImpressionBasedRemarketingList), static t => CustomizeImpressionBasedRemarketingList(t) },
            { typeof(InheritFromParentBiddingScheme), static t => CustomizeInheritFromParentBiddingScheme(t) },
            { typeof(InMarketAudience), static t => CustomizeInMarketAudience(t) },
            { typeof(InStoreTransactionGoal), static t => CustomizeInStoreTransactionGoal(t) },
            { typeof(Keyword), static t => CustomizeKeyword(t) },
            { typeof(Label), static t => CustomizeLabel(t) },
            { typeof(LabelAssociation), static t => CustomizeLabelAssociation(t) },
            { typeof(LinkedInSegment), static t => CustomizeLinkedInSegment(t) },
            { typeof(LocationAdExtension), static t => CustomizeLocationAdExtension(t) },
            { typeof(LocationCondition), static t => CustomizeLocationCondition(t) },
            { typeof(LocationConditionItem), static t => CustomizeLocationConditionItem(t) },
            { typeof(LocationCriterion), static t => CustomizeLocationCriterion(t) },
            { typeof(LocationIntentCriterion), static t => CustomizeLocationIntentCriterion(t) },
            { typeof(LogoAdExtension), static t => CustomizeLogoAdExtension(t) },
            { typeof(ManualCpaBiddingScheme), static t => CustomizeManualCpaBiddingScheme(t) },
            { typeof(ManualCpcBiddingScheme), static t => CustomizeManualCpcBiddingScheme(t) },
            { typeof(ManualCpmBiddingScheme), static t => CustomizeManualCpmBiddingScheme(t) },
            { typeof(ManualCpvBiddingScheme), static t => CustomizeManualCpvBiddingScheme(t) },
            { typeof(MaxClicksBiddingScheme), static t => CustomizeMaxClicksBiddingScheme(t) },
            { typeof(MaxConversionsBiddingScheme), static t => CustomizeMaxConversionsBiddingScheme(t) },
            { typeof(MaxConversionValueBiddingScheme), static t => CustomizeMaxConversionValueBiddingScheme(t) },
            { typeof(MaxRoasBiddingScheme), static t => CustomizeMaxRoasBiddingScheme(t) },
            { typeof(Media), static t => CustomizeMedia(t) },
            { typeof(MediaAssociation), static t => CustomizeMediaAssociation(t) },
            { typeof(MediaMetaData), static t => CustomizeMediaMetaData(t) },
            { typeof(MediaRepresentation), static t => CustomizeMediaRepresentation(t) },
            { typeof(MigrationStatusInfo), static t => CustomizeMigrationStatusInfo(t) },
            { typeof(NegativeAdGroupCriterion), static t => CustomizeNegativeAdGroupCriterion(t) },
            { typeof(NegativeCampaignCriterion), static t => CustomizeNegativeCampaignCriterion(t) },
            { typeof(NegativeKeyword), static t => CustomizeNegativeKeyword(t) },
            { typeof(NegativeKeywordList), static t => CustomizeNegativeKeywordList(t) },
            { typeof(NegativeSite), static t => CustomizeNegativeSite(t) },
            { typeof(NewCustomerAcquisitionGoal), static t => CustomizeNewCustomerAcquisitionGoal(t) },
            { typeof(NewCustomerAcquisitionGoalSetting), static t => CustomizeNewCustomerAcquisitionGoalSetting(t) },
            { typeof(NumberRuleItem), static t => CustomizeNumberRuleItem(t) },
            { typeof(OfflineConversion), static t => CustomizeOfflineConversion(t) },
            { typeof(OfflineConversionAdjustment), static t => CustomizeOfflineConversionAdjustment(t) },
            { typeof(OfflineConversionGoal), static t => CustomizeOfflineConversionGoal(t) },
            { typeof(OnlineConversionAdjustment), static t => CustomizeOnlineConversionAdjustment(t) },
            { typeof(OperationError), static t => CustomizeOperationError(t) },
            { typeof(PagesViewedPerVisitGoal), static t => CustomizePagesViewedPerVisitGoal(t) },
            { typeof(PageVisitorsRule), static t => CustomizePageVisitorsRule(t) },
            { typeof(PageVisitorsWhoDidNotVisitAnotherPageRule), static t => CustomizePageVisitorsWhoDidNotVisitAnotherPageRule(t) },
            { typeof(PageVisitorsWhoVisitedAnotherPageRule), static t => CustomizePageVisitorsWhoVisitedAnotherPageRule(t) },
            { typeof(Paging), static t => CustomizePaging(t) },
            { typeof(PercentCpcBiddingScheme), static t => CustomizePercentCpcBiddingScheme(t) },
            { typeof(PerformanceMaxSetting), static t => CustomizePerformanceMaxSetting(t) },
            { typeof(PlacementCriterion), static t => CustomizePlacementCriterion(t) },
            { typeof(PlacementExclusionList), static t => CustomizePlacementExclusionList(t) },
            { typeof(PriceAdExtension), static t => CustomizePriceAdExtension(t) },
            { typeof(PriceTableRow), static t => CustomizePriceTableRow(t) },
            { typeof(ProductAd), static t => CustomizeProductAd(t) },
            { typeof(ProductAudience), static t => CustomizeProductAudience(t) },
            { typeof(ProductCondition), static t => CustomizeProductCondition(t) },
            { typeof(ProductPartition), static t => CustomizeProductPartition(t) },
            { typeof(ProductScope), static t => CustomizeProductScope(t) },
            { typeof(ProfileCriterion), static t => CustomizeProfileCriterion(t) },
            { typeof(ProfileDimension), static t => CustomizeProfileDimension(t) },
            { typeof(ProfileInfo), static t => CustomizeProfileInfo(t) },
            { typeof(PromotionAdExtension), static t => CustomizePromotionAdExtension(t) },
            { typeof(RadiusCriterion), static t => CustomizeRadiusCriterion(t) },
            { typeof(RateAmount), static t => CustomizeRateAmount(t) },
            { typeof(RateBid), static t => CustomizeRateBid(t) },
            { typeof(RefineAssetGroupRecommendationRequest), static t => CustomizeRefineAssetGroupRecommendationRequest(t) },
            { typeof(RefineAssetGroupRecommendationResponse), static t => CustomizeRefineAssetGroupRecommendationResponse(t) },
            { typeof(RefineResponsiveAdRecommendationRequest), static t => CustomizeRefineResponsiveAdRecommendationRequest(t) },
            { typeof(RefineResponsiveAdRecommendationResponse), static t => CustomizeRefineResponsiveAdRecommendationResponse(t) },
            { typeof(RefineResponsiveSearchAdRecommendationRequest), static t => CustomizeRefineResponsiveSearchAdRecommendationRequest(t) },
            { typeof(RefineResponsiveSearchAdRecommendationResponse), static t => CustomizeRefineResponsiveSearchAdRecommendationResponse(t) },
            { typeof(RemarketingList), static t => CustomizeRemarketingList(t) },
            { typeof(RemarketingRule), static t => CustomizeRemarketingRule(t) },
            { typeof(ResponsiveAd), static t => CustomizeResponsiveAd(t) },
            { typeof(ResponsiveSearchAd), static t => CustomizeResponsiveSearchAd(t) },
            { typeof(ResponsiveSearchAdsSetting), static t => CustomizeResponsiveSearchAdsSetting(t) },
            { typeof(ReviewAdExtension), static t => CustomizeReviewAdExtension(t) },
            { typeof(RuleItem), static t => CustomizeRuleItem(t) },
            { typeof(RuleItemGroup), static t => CustomizeRuleItemGroup(t) },
            { typeof(Schedule), static t => CustomizeSchedule(t) },
            { typeof(SearchCompaniesRequest), static t => CustomizeSearchCompaniesRequest(t) },
            { typeof(SearchCompaniesResponse), static t => CustomizeSearchCompaniesResponse(t) },
            { typeof(SeasonalityAdjustment), static t => CustomizeSeasonalityAdjustment(t) },
            { typeof(SetAccountPropertiesRequest), static t => CustomizeSetAccountPropertiesRequest(t) },
            { typeof(SetAccountPropertiesResponse), static t => CustomizeSetAccountPropertiesResponse(t) },
            { typeof(SetAdExtensionsAssociationsRequest), static t => CustomizeSetAdExtensionsAssociationsRequest(t) },
            { typeof(SetAdExtensionsAssociationsResponse), static t => CustomizeSetAdExtensionsAssociationsResponse(t) },
            { typeof(SetAudienceGroupAssetGroupAssociationsRequest), static t => CustomizeSetAudienceGroupAssetGroupAssociationsRequest(t) },
            { typeof(SetAudienceGroupAssetGroupAssociationsResponse), static t => CustomizeSetAudienceGroupAssetGroupAssociationsResponse(t) },
            { typeof(SetLabelAssociationsRequest), static t => CustomizeSetLabelAssociationsRequest(t) },
            { typeof(SetLabelAssociationsResponse), static t => CustomizeSetLabelAssociationsResponse(t) },
            { typeof(SetNegativeSitesToAdGroupsRequest), static t => CustomizeSetNegativeSitesToAdGroupsRequest(t) },
            { typeof(SetNegativeSitesToAdGroupsResponse), static t => CustomizeSetNegativeSitesToAdGroupsResponse(t) },
            { typeof(SetNegativeSitesToCampaignsRequest), static t => CustomizeSetNegativeSitesToCampaignsRequest(t) },
            { typeof(SetNegativeSitesToCampaignsResponse), static t => CustomizeSetNegativeSitesToCampaignsResponse(t) },
            { typeof(SetSharedEntityAssociationsRequest), static t => CustomizeSetSharedEntityAssociationsRequest(t) },
            { typeof(SetSharedEntityAssociationsResponse), static t => CustomizeSetSharedEntityAssociationsResponse(t) },
            { typeof(Setting), static t => CustomizeSetting(t) },
            { typeof(SharedEntity), static t => CustomizeSharedEntity(t) },
            { typeof(SharedEntityAssociation), static t => CustomizeSharedEntityAssociation(t) },
            { typeof(SharedList), static t => CustomizeSharedList(t) },
            { typeof(SharedListItem), static t => CustomizeSharedListItem(t) },
            { typeof(ShoppingSetting), static t => CustomizeShoppingSetting(t) },
            { typeof(SimilarRemarketingList), static t => CustomizeSimilarRemarketingList(t) },
            { typeof(Site), static t => CustomizeSite(t) },
            { typeof(SitelinkAdExtension), static t => CustomizeSitelinkAdExtension(t) },
            { typeof(StoreCriterion), static t => CustomizeStoreCriterion(t) },
            { typeof(StringRuleItem), static t => CustomizeStringRuleItem(t) },
            { typeof(StructuredSnippetAdExtension), static t => CustomizeStructuredSnippetAdExtension(t) },
            { typeof(SupportedClipchampAudio), static t => CustomizeSupportedClipchampAudio(t) },
            { typeof(SupportedClipchampAudioData), static t => CustomizeSupportedClipchampAudioData(t) },
            { typeof(SupportedFont), static t => CustomizeSupportedFont(t) },
            { typeof(SupportedFontsData), static t => CustomizeSupportedFontsData(t) },
            { typeof(TargetCpaBiddingScheme), static t => CustomizeTargetCpaBiddingScheme(t) },
            { typeof(TargetImpressionShareBiddingScheme), static t => CustomizeTargetImpressionShareBiddingScheme(t) },
            { typeof(TargetRoasBiddingScheme), static t => CustomizeTargetRoasBiddingScheme(t) },
            { typeof(TargetSetting), static t => CustomizeTargetSetting(t) },
            { typeof(TargetSettingDetail), static t => CustomizeTargetSettingDetail(t) },
            { typeof(TextAd), static t => CustomizeTextAd(t) },
            { typeof(TextAsset), static t => CustomizeTextAsset(t) },
            { typeof(ThirdPartyMeasurementSetting), static t => CustomizeThirdPartyMeasurementSetting(t) },
            { typeof(TopicCriterion), static t => CustomizeTopicCriterion(t) },
            { typeof(UetTag), static t => CustomizeUetTag(t) },
            { typeof(UpdateAdExtensionsRequest), static t => CustomizeUpdateAdExtensionsRequest(t) },
            { typeof(UpdateAdExtensionsResponse), static t => CustomizeUpdateAdExtensionsResponse(t) },
            { typeof(UpdateAdGroupCriterionsRequest), static t => CustomizeUpdateAdGroupCriterionsRequest(t) },
            { typeof(UpdateAdGroupCriterionsResponse), static t => CustomizeUpdateAdGroupCriterionsResponse(t) },
            { typeof(UpdateAdGroupsRequest), static t => CustomizeUpdateAdGroupsRequest(t) },
            { typeof(UpdateAdGroupsResponse), static t => CustomizeUpdateAdGroupsResponse(t) },
            { typeof(UpdateAdsRequest), static t => CustomizeUpdateAdsRequest(t) },
            { typeof(UpdateAdsResponse), static t => CustomizeUpdateAdsResponse(t) },
            { typeof(UpdateAnnotationOptOutRequest), static t => CustomizeUpdateAnnotationOptOutRequest(t) },
            { typeof(UpdateAnnotationOptOutResponse), static t => CustomizeUpdateAnnotationOptOutResponse(t) },
            { typeof(UpdateAssetGroupsRequest), static t => CustomizeUpdateAssetGroupsRequest(t) },
            { typeof(UpdateAssetGroupsResponse), static t => CustomizeUpdateAssetGroupsResponse(t) },
            { typeof(UpdateAudienceGroupsRequest), static t => CustomizeUpdateAudienceGroupsRequest(t) },
            { typeof(UpdateAudienceGroupsResponse), static t => CustomizeUpdateAudienceGroupsResponse(t) },
            { typeof(UpdateAudiencesRequest), static t => CustomizeUpdateAudiencesRequest(t) },
            { typeof(UpdateAudiencesResponse), static t => CustomizeUpdateAudiencesResponse(t) },
            { typeof(UpdateBidStrategiesRequest), static t => CustomizeUpdateBidStrategiesRequest(t) },
            { typeof(UpdateBidStrategiesResponse), static t => CustomizeUpdateBidStrategiesResponse(t) },
            { typeof(UpdateBrandKitsRequest), static t => CustomizeUpdateBrandKitsRequest(t) },
            { typeof(UpdateBrandKitsResponse), static t => CustomizeUpdateBrandKitsResponse(t) },
            { typeof(UpdateBudgetsRequest), static t => CustomizeUpdateBudgetsRequest(t) },
            { typeof(UpdateBudgetsResponse), static t => CustomizeUpdateBudgetsResponse(t) },
            { typeof(UpdateCampaignCriterionsRequest), static t => CustomizeUpdateCampaignCriterionsRequest(t) },
            { typeof(UpdateCampaignCriterionsResponse), static t => CustomizeUpdateCampaignCriterionsResponse(t) },
            { typeof(UpdateCampaignsRequest), static t => CustomizeUpdateCampaignsRequest(t) },
            { typeof(UpdateCampaignsResponse), static t => CustomizeUpdateCampaignsResponse(t) },
            { typeof(UpdateConversionGoalsRequest), static t => CustomizeUpdateConversionGoalsRequest(t) },
            { typeof(UpdateConversionGoalsResponse), static t => CustomizeUpdateConversionGoalsResponse(t) },
            { typeof(UpdateConversionValueRulesRequest), static t => CustomizeUpdateConversionValueRulesRequest(t) },
            { typeof(UpdateConversionValueRulesResponse), static t => CustomizeUpdateConversionValueRulesResponse(t) },
            { typeof(UpdateConversionValueRulesStatusRequest), static t => CustomizeUpdateConversionValueRulesStatusRequest(t) },
            { typeof(UpdateConversionValueRulesStatusResponse), static t => CustomizeUpdateConversionValueRulesStatusResponse(t) },
            { typeof(UpdateDataExclusionsRequest), static t => CustomizeUpdateDataExclusionsRequest(t) },
            { typeof(UpdateDataExclusionsResponse), static t => CustomizeUpdateDataExclusionsResponse(t) },
            { typeof(UpdateExperimentsRequest), static t => CustomizeUpdateExperimentsRequest(t) },
            { typeof(UpdateExperimentsResponse), static t => CustomizeUpdateExperimentsResponse(t) },
            { typeof(UpdateImportJobsRequest), static t => CustomizeUpdateImportJobsRequest(t) },
            { typeof(UpdateImportJobsResponse), static t => CustomizeUpdateImportJobsResponse(t) },
            { typeof(UpdateKeywordsRequest), static t => CustomizeUpdateKeywordsRequest(t) },
            { typeof(UpdateKeywordsResponse), static t => CustomizeUpdateKeywordsResponse(t) },
            { typeof(UpdateLabelsRequest), static t => CustomizeUpdateLabelsRequest(t) },
            { typeof(UpdateLabelsResponse), static t => CustomizeUpdateLabelsResponse(t) },
            { typeof(UpdateLinkedInSegmentsRequest), static t => CustomizeUpdateLinkedInSegmentsRequest(t) },
            { typeof(UpdateLinkedInSegmentsResponse), static t => CustomizeUpdateLinkedInSegmentsResponse(t) },
            { typeof(UpdateNewCustomerAcquisitionGoalsRequest), static t => CustomizeUpdateNewCustomerAcquisitionGoalsRequest(t) },
            { typeof(UpdateNewCustomerAcquisitionGoalsResponse), static t => CustomizeUpdateNewCustomerAcquisitionGoalsResponse(t) },
            { typeof(UpdateSeasonalityAdjustmentsRequest), static t => CustomizeUpdateSeasonalityAdjustmentsRequest(t) },
            { typeof(UpdateSeasonalityAdjustmentsResponse), static t => CustomizeUpdateSeasonalityAdjustmentsResponse(t) },
            { typeof(UpdateSharedEntitiesRequest), static t => CustomizeUpdateSharedEntitiesRequest(t) },
            { typeof(UpdateSharedEntitiesResponse), static t => CustomizeUpdateSharedEntitiesResponse(t) },
            { typeof(UpdateUetTagsRequest), static t => CustomizeUpdateUetTagsRequest(t) },
            { typeof(UpdateUetTagsResponse), static t => CustomizeUpdateUetTagsResponse(t) },
            { typeof(UpdateVideosRequest), static t => CustomizeUpdateVideosRequest(t) },
            { typeof(UpdateVideosResponse), static t => CustomizeUpdateVideosResponse(t) },
            { typeof(UrlGoal), static t => CustomizeUrlGoal(t) },
            { typeof(VanityPharmaSetting), static t => CustomizeVanityPharmaSetting(t) },
            { typeof(VerifiedTrackingSetting), static t => CustomizeVerifiedTrackingSetting(t) },
            { typeof(Video), static t => CustomizeVideo(t) },
            { typeof(VideoAdExtension), static t => CustomizeVideoAdExtension(t) },
            { typeof(VideoAsset), static t => CustomizeVideoAsset(t) },
            { typeof(VideoTemplateFilter), static t => CustomizeVideoTemplateFilter(t) },
            { typeof(Webpage), static t => CustomizeWebpage(t) },
            { typeof(WebpageCondition), static t => CustomizeWebpageCondition(t) },
            { typeof(WebpageParameter), static t => CustomizeWebpageParameter(t) }
        };

        private static void CustomizeAccountMigrationStatusesInfo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAccountNegativeKeywordList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AccountNegativeKeywordList";
                        break;
                }
            }
        }

        private static void CustomizeAccountPlacementExclusionList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AccountPlacementExclusionList";
                        break;
                }
            }
        }

        private static void CustomizeAccountPlacementInclusionList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AccountPlacementInclusionList";
                        break;
                }
            }
        }

        private static void CustomizeAccountProperty(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeActionAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ActionAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdApiError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "AdApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAddAdExtensionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdExtensionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdGroupCriterionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdGroupCriterionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAssetGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAssetGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAudienceGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAudienceGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAudiencesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddAudiencesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddBidStrategiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddBidStrategiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddBrandKitsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddBrandKitsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddBudgetsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddBudgetsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddCampaignConversionGoalsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddCampaignConversionGoalsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddCampaignCriterionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddCampaignCriterionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddCampaignsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddCampaignsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddConversionGoalsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddConversionGoalsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddConversionValueRulesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddConversionValueRulesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddDataExclusionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddDataExclusionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddExperimentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddExperimentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddHTML5sRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddHTML5sResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddImportJobsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddImportJobsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddKeywordsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddKeywordsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddLabelsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddLabelsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddLinkedInSegmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddLinkedInSegmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddListItemsToSharedListRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddListItemsToSharedListResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddMediaRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddMediaResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddNegativeKeywordsToEntitiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddNegativeKeywordsToEntitiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddNewCustomerAcquisitionGoalsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddNewCustomerAcquisitionGoalsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddress(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddSeasonalityAdjustmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddSeasonalityAdjustmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddSharedEntityRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddSharedEntityResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddUetTagsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddUetTagsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddVideosRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAddVideosResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AdExtension";
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionAssociationCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionEditorialReason(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionEditorialReasonCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionIdentity(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdExtensionIdToEntityIdAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdGroup(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "CommissionRate":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "FrequencyCapSettings":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "MultimediaAdsBidAdjustment":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "PercentCpcBid":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UseOptimizedTargeting":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UsePredictiveTargeting":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AdScheduleUseSearcherTimeZone":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AdGroupType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "CpvBid":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "CpmBid":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "McpaBid":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeAdGroupCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AdGroupCriterion";
                        break;
                }
            }
        }

        private static void CustomizeAdGroupCriterionAction(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdGroupNegativeSites(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationCustomizedProperty(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationImageAssetProperty(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationImageRefineOperation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationImageSuggestion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ImageMetadata":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationImageSuggestionMetadata(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationJobInfo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationMediaRefineResult(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationRefinedMedia(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationTextAssetProperty(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TextField1":
                        jsonPropertyInfo.Name = "TextField";
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationTextRefineOperation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationTextRefineResult(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRecommendationVideoSuggestion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAdRotation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAgeCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AgeCriterion";
                        break;
                }
            }
        }

        private static void CustomizeAgeDimension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Age;
                        break;
                }
            }
        }

        private static void CustomizeAnnotationOptOut(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeAppAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AppAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeAppDownloadGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.AppDownload;
                        break;
                }
            }
        }

        private static void CustomizeAppealEditorialRejectionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAppealEditorialRejectionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAppInstallAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.AppInstall;
                        break;
                }
            }
        }

        private static void CustomizeAppInstallGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.AppInstall;
                        break;
                }
            }
        }

        private static void CustomizeApplicationFault(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "ApplicationFault";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeApplyAssetGroupListingGroupActionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyAssetGroupListingGroupActionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyCustomerListItemsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyCustomerListItemsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyCustomerListUserDataRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyCustomerListUserDataResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyHotelGroupActionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyHotelGroupActionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyOfflineConversionAdjustmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyOfflineConversionAdjustmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyOfflineConversionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyOfflineConversionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyOnlineConversionAdjustmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyOnlineConversionAdjustmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyProductPartitionActionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeApplyProductPartitionActionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAppSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AppId":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AppStore":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => !EqualityComparer<AppStore>.Default.Equals(default, (AppStore)value);
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AppSetting";
                        break;
                }
            }
        }

        private static void CustomizeAppUrl(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAsset(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Asset";
                        break;
                }
            }
        }

        private static void CustomizeAssetGroup(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AssetGroupSearchThemes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AssetGroupUrlTargets":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "FinalUrlSuffix":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "TrackingUrlTemplate":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UrlCustomParameters":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Videos":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupEditorialReason(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupEditorialReasonCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupListingGroup(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ListingGroupPath":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupListingGroupAction(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupSearchTheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetGroupUrlTarget(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAssetLink(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudience(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceCondition(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceConditionItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AudienceCriterion";
                        break;
                }
            }
        }

        private static void CustomizeAudienceDimension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Audience;
                        break;
                }
            }
        }

        private static void CustomizeAudienceGroup(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceGroupAssetGroupAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceGroupDimension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceIdName(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudienceInfo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAudioFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeAuditPointResult(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBatchError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BatchError";
                        break;
                }
            }
        }

        private static void CustomizeBatchErrorCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BatchErrorCollection";
                        break;
                }
            }
        }

        private static void CustomizeBid(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBiddableAdGroupCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "CriterionCashback":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BiddableAdGroupCriterion";
                        break;
                }
            }
        }

        private static void CustomizeBiddableCampaignCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "CriterionCashback":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BiddableCampaignCriterion";
                        break;
                }
            }
        }

        private static void CustomizeBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeBidMultiplier(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BidMultiplier";
                        break;
                }
            }
        }

        private static void CustomizeBidStrategy(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "CurrencyCode":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ReportingTimeZone":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Scope":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeBMCStore(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "StoreUrl":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeBrandItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BrandItem";
                        break;
                }
            }
        }

        private static void CustomizeBrandKit(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "BrandVoice":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "BusinessName":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeBrandKitColor(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBrandKitFont(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBrandKitImage(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBrandKitPalette(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBrandList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BrandList";
                        break;
                }
            }
        }

        private static void CustomizeBrandVoice(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeBudget(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCallAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CallAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeCalloutAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CalloutAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeCallToActionSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "CallToActionOptOut":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CallToActionSetting";
                        break;
                }
            }
        }

        private static void CustomizeCampaign(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "BidStrategyScope":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "DealIds":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "EndDate":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalIds":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsDealCampaign":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsPolitical":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => !EqualityComparer<bool>.Default.Equals(default, (bool)value);
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "MultimediaAdsBidAdjustment":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "StartDate":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UseCampaignLevelDates":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AdScheduleUseSearcherTimeZone":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "BidStrategyId":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeCampaignAdGroupIds(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ActiveAdGroupsOnly":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeCampaignAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCampaignConversionGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCampaignCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CampaignCriterion";
                        break;
                }
            }
        }

        private static void CustomizeCampaignNegativeSites(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCampaignSize(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCashbackAdjustment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CashbackAdjustment";
                        break;
                }
            }
        }

        private static void CustomizeCategoryResult(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeClipchampTemplateInfo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCombinationRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCombinedList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.CombinedList;
                        break;
                }
            }
        }

        private static void CustomizeCommissionBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CommissionBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeCompany(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCompanyList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => LinkedInSegmentType.CompanyList;
                        break;
                }
            }
        }

        private static void CustomizeCompanyName(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeConversionGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeConversionGoalRevenue(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeConversionValueRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCoOpSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CoOpSetting";
                        break;
                }
            }
        }

        private static void CustomizeCostPerSaleBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CostPerSale";
                        break;
                }
            }
        }

        private static void CustomizeCreateAssetGroupRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateAssetGroupRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateBrandKitRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateBrandKitRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateResponsiveAdRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateResponsiveAdRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateResponsiveSearchAdRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCreateResponsiveSearchAdRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Criterion";
                        break;
                }
            }
        }

        private static void CustomizeCriterionBid(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CriterionBid";
                        break;
                }
            }
        }

        private static void CustomizeCriterionCashback(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CriterionCashback";
                        break;
                }
            }
        }

        private static void CustomizeCustomAudience(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.Custom;
                        break;
                }
            }
        }

        private static void CustomizeCustomerAccountShare(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCustomerAccountShareAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCustomerList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.CustomerList;
                        break;
                }
            }
        }

        private static void CustomizeCustomerListUserData(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCustomerShare(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCustomEventsRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CustomEvents";
                        break;
                }
            }
        }

        private static void CustomizeCustomParameter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCustomParameters(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeCustomSegment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.CustomSegment;
                        break;
                }
            }
        }

        private static void CustomizeCustomSegmentCatalog(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDailySummary(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDataExclusion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDate(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDayTime(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDayTimeCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DayTimeCriterion";
                        break;
                }
            }
        }

        private static void CustomizeDealCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DealCriterion";
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdExtensionsAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdExtensionsAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdExtensionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdExtensionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdGroupCriterionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdGroupCriterionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAssetGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAssetGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAudienceGroupAssetGroupAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAudienceGroupAssetGroupAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAudienceGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAudienceGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAudiencesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteAudiencesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteBidStrategiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteBidStrategiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteBrandKitsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteBrandKitsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteBudgetsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteBudgetsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteCampaignConversionGoalsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteCampaignConversionGoalsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteCampaignCriterionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteCampaignCriterionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteCampaignsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteCampaignsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteDataExclusionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteDataExclusionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteExperimentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteExperimentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteHTML5sRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteHTML5sResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteImportJobsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteImportJobsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteKeywordsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteKeywordsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteLabelAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteLabelAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteLabelsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteLabelsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteLinkedInSegmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteLinkedInSegmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteListItemsFromSharedListRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteListItemsFromSharedListResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteMediaRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteMediaResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteNegativeKeywordsFromEntitiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteNegativeKeywordsFromEntitiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteSeasonalityAdjustmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteSeasonalityAdjustmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteSharedEntitiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteSharedEntitiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteSharedEntityAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteSharedEntityAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteVideosRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeleteVideosResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeviceCondition(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDeviceCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DeviceCriterion";
                        break;
                }
            }
        }

        private static void CustomizeDiagnosticsEntity(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDiagnosticsFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDiagnosticsRequestStatus(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDiagnosticsSettings(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeDisclaimerAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DisclaimerAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeDisclaimerSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DisclaimerSetting";
                        break;
                }
            }
        }

        private static void CustomizeDurationGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.Duration;
                        break;
                }
            }
        }

        private static void CustomizeDynamicFeedSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DynamicFeedSetting";
                        break;
                }
            }
        }

        private static void CustomizeDynamicSearchAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.DynamicSearch;
                        break;
                }
            }
        }

        private static void CustomizeDynamicSearchAdsSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "DynamicDescriptionEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DynamicSearchAdsSetting";
                        break;
                }
            }
        }

        private static void CustomizeEditorialApiFaultDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            var newJsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            newJsonPropertyInfo.Get = _ => "EditorialApiFaultDetail";
            jsonTypeInfo.Properties.Add(newJsonPropertyInfo);
        }

        private static void CustomizeEditorialError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EditorialError";
                        break;
                }
            }
        }

        private static void CustomizeEditorialErrorCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EditorialErrorCollection";
                        break;
                }
            }
        }

        private static void CustomizeEditorialReason(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeEditorialReasonCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeEnhancedCpcBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EnhancedCpc";
                        break;
                }
            }
        }

        private static void CustomizeEntityIdToParentIdAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeEntityNegativeKeyword(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeEntityResult(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeEventGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.Event;
                        break;
                }
            }
        }

        private static void CustomizeExpandedTextAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.ExpandedText;
                        break;
                }
            }
        }

        private static void CustomizeExperiment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeFileImportJob(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "NotificationEmail":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FileImportJob";
                        break;
                }
            }
        }

        private static void CustomizeFileImportOption(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FileImportOption";
                        break;
                }
            }
        }

        private static void CustomizeFilterLinkAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FilterLinkAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeFixedBid(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FixedBid";
                        break;
                }
            }
        }

        private static void CustomizeFlyerAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FlyerAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeFrequency(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeFrequencyCapSettings(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGenderCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GenderCriterion";
                        break;
                }
            }
        }

        private static void CustomizeGenderDimension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Gender;
                        break;
                }
            }
        }

        private static void CustomizeGenreCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GenreCriterion";
                        break;
                }
            }
        }

        private static void CustomizeGeoPoint(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAccountMigrationStatusesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAccountMigrationStatusesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAccountPropertiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAccountPropertiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionIdsByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionIdsByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionsAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionsAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionsEditorialReasonsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdExtensionsEditorialReasonsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdGroupCriterionsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdGroupCriterionsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdGroupsByCampaignIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdGroupsByCampaignIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdGroupsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdGroupsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdsByAdGroupIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdsByAdGroupIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdsByEditorialStatusRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdsByEditorialStatusResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAdsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAnnotationOptOutRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAnnotationOptOutResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupListingGroupsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupListingGroupsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupsByCampaignIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupsByCampaignIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupsEditorialReasonsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAssetGroupsEditorialReasonsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudienceGroupsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudienceGroupsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudiencesByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetAudiencesByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBidStrategiesByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBidStrategiesByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBMCStoresByCustomerIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBMCStoresByCustomerIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBrandKitsByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBrandKitsByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBrandKitsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBrandKitsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBSCCountriesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBSCCountriesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBudgetsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetBudgetsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignCriterionsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignCriterionsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignIdsByBidStrategyIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignIdsByBidStrategyIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignIdsByBudgetIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignIdsByBudgetIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignsByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignsByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignSizesByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetCampaignSizesByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetClipchampTemplatesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetClipchampTemplatesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConfigValueRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConfigValueResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionGoalsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionGoalsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionGoalsByTagIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionGoalsByTagIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionValueRulesByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionValueRulesByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionValueRulesByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetConversionValueRulesByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetDataExclusionsByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetDataExclusionsByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetDataExclusionsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetDataExclusionsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetDiagnosticsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetDiagnosticsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetEditorialReasonsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetEditorialReasonsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetExperimentsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetExperimentsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetFileImportUploadUrlRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetFileImportUploadUrlResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetGeoLocationsFileUrlRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetGeoLocationsFileUrlResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetHealthCheckRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetHealthCheckResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetHTML5sByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetHTML5sByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetImportEntityIdsMappingRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetImportEntityIdsMappingResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetImportJobsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetImportJobsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetImportResultsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetImportResultsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetKeywordsByAdGroupIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetKeywordsByAdGroupIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetKeywordsByEditorialStatusRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetKeywordsByEditorialStatusResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetKeywordsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetKeywordsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetLabelAssociationsByEntityIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetLabelAssociationsByEntityIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetLabelAssociationsByLabelIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetLabelAssociationsByLabelIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetLabelsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetLabelsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetListItemsBySharedListRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetListItemsBySharedListResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetMediaAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetMediaAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetMediaMetaDataByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetMediaMetaDataByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetMediaMetaDataByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetMediaMetaDataByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNegativeKeywordsByEntityIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNegativeKeywordsByEntityIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNegativeSitesByAdGroupIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNegativeSitesByAdGroupIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNegativeSitesByCampaignIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNegativeSitesByCampaignIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNewCustomerAcquisitionGoalsByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetNewCustomerAcquisitionGoalsByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetOfflineConversionReportsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetOfflineConversionReportsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetProfileDataFileUrlRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetProfileDataFileUrlResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetResponsiveAdRecommendationJobRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetResponsiveAdRecommendationJobResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSeasonalityAdjustmentsByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSeasonalityAdjustmentsByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSeasonalityAdjustmentsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSeasonalityAdjustmentsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntitiesByAccountIdRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntitiesByAccountIdResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntitiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntitiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntityAssociationsByEntityIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntityAssociationsByEntityIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntityAssociationsBySharedEntityIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSharedEntityAssociationsBySharedEntityIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSupportedClipchampAudioRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSupportedClipchampAudioResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSupportedFontsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetSupportedFontsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetUetTagsByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetUetTagsByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetVideosByIdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGetVideosByIdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeGoogleImportJob(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "NotificationEmail":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GoogleImportJob";
                        break;
                }
            }
        }

        private static void CustomizeGoogleImportOption(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AdScheduleUseSearcherTimezone":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AutoDeviceBidOptimization":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "EnableCopilot":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewAccountNegativeKeywords":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewBrandSuitability":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewCarouselAd":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewConversionGoals":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewImageAdExtensions":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewLeadFormAdExtensions":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewLogoAdExtensions":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewPortfolioBidStrategy":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "NewTopicTargets":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "PauseAIMAdGroupIfAllAudienceCriterionNotImported":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "RenameCampaignNameWithSuffix":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "SearchAndReplaceForCustomParameters":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "SearchAndReplaceForFinalURLSuffix":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateAccountNegativeKeywords":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateAdCustomizerAttributes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateAdUrls":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateAssetAutomationCampaignSetting":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateBrandSuitability":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateConversionGoals":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateImageAdExtensions":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateLeadFormAdExtensions":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateLogoAdExtensions":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateSitelinkUrls":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "UpdateTopicTargets":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GoogleImportOption";
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckActionLinkMetadata(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckColumnMetadata(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckData(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckEntity(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckMetadata(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHealthCheckSubEntityData(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHotelAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.Hotel;
                        break;
                }
            }
        }

        private static void CustomizeHotelAdvanceBookingWindowCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelAdvanceBookingWindowCriterion";
                        break;
                }
            }
        }

        private static void CustomizeHotelCheckInDateCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelCheckInDateCriterion";
                        break;
                }
            }
        }

        private static void CustomizeHotelCheckInDayCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelCheckInDayCriterion";
                        break;
                }
            }
        }

        private static void CustomizeHotelDateSelectionTypeCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelDateSelectionTypeCriterion";
                        break;
                }
            }
        }

        private static void CustomizeHotelGroup(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelGroup";
                        break;
                }
            }
        }

        private static void CustomizeHotelLengthOfStayCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelLengthOfStayCriterion";
                        break;
                }
            }
        }

        private static void CustomizeHotelListing(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeHotelSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelSetting";
                        break;
                }
            }
        }

        private static void CustomizeHTML5(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeIdCollection(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeImage(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Text":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Image";
                        break;
                }
            }
        }

        private static void CustomizeImageAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "DisplayText":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Images":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Layouts":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "SourceType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImageAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeImageAsset(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TargetHeight":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "TargetWidth":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImageAsset";
                        break;
                }
            }
        }

        private static void CustomizeImageMediaRepresentation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImageMediaRepresentation";
                        break;
                }
            }
        }

        private static void CustomizeImportEntityStatistics(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeImportJob(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "NotificationEmail":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImportJob";
                        break;
                }
            }
        }

        private static void CustomizeImportOption(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImportOption";
                        break;
                }
            }
        }

        private static void CustomizeImportResult(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeImportSearchAndReplaceForStringProperty(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeImpressionBasedRemarketingList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AdGroupIds":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "CampaignIds":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.ImpressionBasedRemarketingList;
                        break;
                }
            }
        }

        private static void CustomizeInheritFromParentBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "InheritedBidStrategyType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "InheritFromParent";
                        break;
                }
            }
        }

        private static void CustomizeInMarketAudience(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.InMarket;
                        break;
                }
            }
        }

        private static void CustomizeInStoreTransactionGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.InStoreTransaction;
                        break;
                }
            }
        }

        private static void CustomizeKeyword(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeLabel(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeLabelAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeLinkedInSegment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeLocationAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LocationAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeLocationCondition(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeLocationConditionItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeLocationCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LocationCriterion";
                        break;
                }
            }
        }

        private static void CustomizeLocationIntentCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LocationIntentCriterion";
                        break;
                }
            }
        }

        private static void CustomizeLogoAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LogoAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeManualCpaBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ManualCpi":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpaBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeManualCpcBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ManualCpc":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpc";
                        break;
                }
            }
        }

        private static void CustomizeManualCpmBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpm";
                        break;
                }
            }
        }

        private static void CustomizeManualCpvBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpv";
                        break;
                }
            }
        }

        private static void CustomizeMaxClicksBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxClicks";
                        break;
                }
            }
        }

        private static void CustomizeMaxConversionsBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TargetCpa":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxConversions";
                        break;
                }
            }
        }

        private static void CustomizeMaxConversionValueBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "MaxCpc":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxConversionValueBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeMaxRoasBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxRoasBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeMedia(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Text":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Media";
                        break;
                }
            }
        }

        private static void CustomizeMediaAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeMediaMetaData(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Text":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeMediaRepresentation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MediaRepresentation";
                        break;
                }
            }
        }

        private static void CustomizeMigrationStatusInfo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeNegativeAdGroupCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeAdGroupCriterion";
                        break;
                }
            }
        }

        private static void CustomizeNegativeCampaignCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeCampaignCriterion";
                        break;
                }
            }
        }

        private static void CustomizeNegativeKeyword(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeKeyword";
                        break;
                }
            }
        }

        private static void CustomizeNegativeKeywordList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeKeywordList";
                        break;
                }
            }
        }

        private static void CustomizeNegativeSite(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeSite";
                        break;
                }
            }
        }

        private static void CustomizeNewCustomerAcquisitionGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeNewCustomerAcquisitionGoalSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NewCustomerAcquisitionGoalSetting";
                        break;
                }
            }
        }

        private static void CustomizeNumberRuleItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Number";
                        break;
                }
            }
        }

        private static void CustomizeOfflineConversion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "HashedEmailAddress":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "HashedPhoneNumber":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeOfflineConversionAdjustment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "HashedEmailAddress":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "HashedPhoneNumber":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeOfflineConversionGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "IsExternallyAttributed":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.OfflineConversion;
                        break;
                }
            }
        }

        private static void CustomizeOnlineConversionAdjustment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeOperationError(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizePagesViewedPerVisitGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.PagesViewedPerVisit;
                        break;
                }
            }
        }

        private static void CustomizePageVisitorsRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "NormalForm":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PageVisitors";
                        break;
                }
            }
        }

        private static void CustomizePageVisitorsWhoDidNotVisitAnotherPageRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PageVisitorsWhoDidNotVisitAnotherPage";
                        break;
                }
            }
        }

        private static void CustomizePageVisitorsWhoVisitedAnotherPageRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PageVisitorsWhoVisitedAnotherPage";
                        break;
                }
            }
        }

        private static void CustomizePaging(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizePercentCpcBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PercentCpcBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizePerformanceMaxSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AutoGeneratedImageOptOut":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "AutoGeneratedTextOptOut":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "CostPerSaleOptOut":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "PageFeedIds":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PerformanceMaxSetting";
                        break;
                }
            }
        }

        private static void CustomizePlacementCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PlacementCriterion";
                        break;
                }
            }
        }

        private static void CustomizePlacementExclusionList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PlacementExclusionList";
                        break;
                }
            }
        }

        private static void CustomizePriceAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PriceAdExtension";
                        break;
                }
            }
        }

        private static void CustomizePriceTableRow(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeProductAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.Product;
                        break;
                }
            }
        }

        private static void CustomizeProductAudience(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.Product;
                        break;
                }
            }
        }

        private static void CustomizeProductCondition(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Operator":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeProductPartition(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ProductPartition";
                        break;
                }
            }
        }

        private static void CustomizeProductScope(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ProductScope";
                        break;
                }
            }
        }

        private static void CustomizeProfileCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ProfileCriterion";
                        break;
                }
            }
        }

        private static void CustomizeProfileDimension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Profile;
                        break;
                }
            }
        }

        private static void CustomizeProfileInfo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizePromotionAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PromotionAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeRadiusCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RadiusCriterion";
                        break;
                }
            }
        }

        private static void CustomizeRateAmount(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRateBid(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RateBid";
                        break;
                }
            }
        }

        private static void CustomizeRefineAssetGroupRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRefineAssetGroupRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRefineResponsiveAdRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRefineResponsiveAdRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRefineResponsiveSearchAdRecommendationRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRefineResponsiveSearchAdRecommendationResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeRemarketingList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.RemarketingList;
                        break;
                }
            }
        }

        private static void CustomizeRemarketingRule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RemarketingRule";
                        break;
                }
            }
        }

        private static void CustomizeResponsiveAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AdSubType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ImpressionTrackingUrls":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "LongHeadlines":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "VerifiedTrackingSettings":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Videos":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.ResponsiveAd;
                        break;
                }
            }
        }

        private static void CustomizeResponsiveSearchAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.ResponsiveSearch;
                        break;
                }
            }
        }

        private static void CustomizeResponsiveSearchAdsSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ResponsiveSearchAdsSetting";
                        break;
                }
            }
        }

        private static void CustomizeReviewAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ReviewAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeRuleItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RuleItem";
                        break;
                }
            }
        }

        private static void CustomizeRuleItemGroup(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSchedule(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSearchCompaniesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSearchCompaniesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSeasonalityAdjustment(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetAccountPropertiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetAccountPropertiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetAdExtensionsAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetAdExtensionsAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetAudienceGroupAssetGroupAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetAudienceGroupAssetGroupAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetLabelAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetLabelAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetNegativeSitesToAdGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetNegativeSitesToAdGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetNegativeSitesToCampaignsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetNegativeSitesToCampaignsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetSharedEntityAssociationsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetSharedEntityAssociationsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Setting";
                        break;
                }
            }
        }

        private static void CustomizeSharedEntity(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SharedEntity";
                        break;
                }
            }
        }

        private static void CustomizeSharedEntityAssociation(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "SharedEntityCustomerId":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeSharedList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SharedList";
                        break;
                }
            }
        }

        private static void CustomizeSharedListItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SharedListItem";
                        break;
                }
            }
        }

        private static void CustomizeShoppingSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "FeedLabel":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ShoppableAdsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ShoppingSetting";
                        break;
                }
            }
        }

        private static void CustomizeSimilarRemarketingList(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.SimilarRemarketingList;
                        break;
                }
            }
        }

        private static void CustomizeSite(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Site";
                        break;
                }
            }
        }

        private static void CustomizeSitelinkAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SitelinkAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeStoreCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "StoreCriterion";
                        break;
                }
            }
        }

        private static void CustomizeStringRuleItem(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "String";
                        break;
                }
            }
        }

        private static void CustomizeStructuredSnippetAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "StructuredSnippetAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeSupportedClipchampAudio(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSupportedClipchampAudioData(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSupportedFont(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeSupportedFontsData(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeTargetCpaBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetCpa";
                        break;
                }
            }
        }

        private static void CustomizeTargetImpressionShareBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetImpressionShareBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeTargetRoasBiddingScheme(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetRoasBiddingScheme";
                        break;
                }
            }
        }

        private static void CustomizeTargetSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetSetting";
                        break;
                }
            }
        }

        private static void CustomizeTargetSettingDetail(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeTextAd(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.Text;
                        break;
                }
            }
        }

        private static void CustomizeTextAsset(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TextAsset";
                        break;
                }
            }
        }

        private static void CustomizeThirdPartyMeasurementSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ThirdPartyMeasurementSetting";
                        break;
                }
            }
        }

        private static void CustomizeTopicCriterion(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TopicCriterion";
                        break;
                }
            }
        }

        private static void CustomizeUetTag(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Industry":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdExtensionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdExtensionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdGroupCriterionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdGroupCriterionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAdsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAnnotationOptOutRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAnnotationOptOutResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAssetGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAssetGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAudienceGroupsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAudienceGroupsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAudiencesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateAudiencesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateBidStrategiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateBidStrategiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateBrandKitsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateBrandKitsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateBudgetsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateBudgetsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateCampaignCriterionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateCampaignCriterionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateCampaignsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateCampaignsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateConversionGoalsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateConversionGoalsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateConversionValueRulesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateConversionValueRulesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateConversionValueRulesStatusRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateConversionValueRulesStatusResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateDataExclusionsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateDataExclusionsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateExperimentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateExperimentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateImportJobsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateImportJobsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateKeywordsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateKeywordsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateLabelsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateLabelsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateLinkedInSegmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateLinkedInSegmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateNewCustomerAcquisitionGoalsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateNewCustomerAcquisitionGoalsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateSeasonalityAdjustmentsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateSeasonalityAdjustmentsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateSharedEntitiesRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateSharedEntitiesResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateUetTagsRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateUetTagsResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateVideosRequest(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ApplicationToken":
                    case "AuthenticationToken":
                    case "CustomerAccountId":
                    case "CustomerId":
                    case "DeveloperToken":
                    case "Password":
                    case "UserName":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUpdateVideosResponse(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "TrackingId":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeUrlGoal(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "AttributionModelType":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "GoalCategory":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsAutoGoal":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "IsEnhancedConversionsEnabled":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "ViewThroughConversionWindowInMinutes":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.Url;
                        break;
                }
            }
        }

        private static void CustomizeVanityPharmaSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "DisplayUrlMode":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "WebsiteDescription":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VanityPharmaSetting";
                        break;
                }
            }
        }

        private static void CustomizeVerifiedTrackingSetting(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VerifiedTrackingSetting";
                        break;
                }
            }
        }

        private static void CustomizeVideo(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeVideoAdExtension(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VideoAdExtension";
                        break;
                }
            }
        }

        private static void CustomizeVideoAsset(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VideoAsset";
                        break;
                }
            }
        }

        private static void CustomizeVideoTemplateFilter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }

        private static void CustomizeWebpage(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Webpage";
                        break;
                }
            }
        }

        private static void CustomizeWebpageCondition(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Operator":
                        jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                        jsonPropertyInfo.IsRequired = false;
                        break;
                }
            }
        }

        private static void CustomizeWebpageParameter(JsonTypeInfo jsonTypeInfo)
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                var jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
    }
}