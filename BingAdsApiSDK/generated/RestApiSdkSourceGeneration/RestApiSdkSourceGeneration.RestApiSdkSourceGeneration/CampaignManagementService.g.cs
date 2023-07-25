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

using System;
using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds
{
    internal class CampaignManagementService : ICampaignManagementService
    {
        private readonly RestServiceClient _restServiceClient;

        private readonly Type _serviceType;

        public CampaignManagementService(RestServiceClient restServiceClient, Type serviceType)
        {
            _restServiceClient = restServiceClient;

            _serviceType = serviceType;
        }

        public AddCampaignsResponse AddCampaigns(AddCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddCampaignsResponse> AddCampaignsAsync(AddCampaignsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddCampaignsResponse>("AddCampaigns", request, _serviceType);
        }

        public GetCampaignsByAccountIdResponse GetCampaignsByAccountId(GetCampaignsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(GetCampaignsByAccountIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetCampaignsByAccountIdResponse>("GetCampaignsByAccountId", request, _serviceType);
        }

        public GetCampaignsByIdsResponse GetCampaignsByIds(GetCampaignsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(GetCampaignsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetCampaignsByIdsResponse>("GetCampaignsByIds", request, _serviceType);
        }

        public DeleteCampaignsResponse DeleteCampaigns(DeleteCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteCampaignsResponse> DeleteCampaignsAsync(DeleteCampaignsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteCampaignsResponse>("DeleteCampaigns", request, _serviceType);
        }

        public UpdateCampaignsResponse UpdateCampaigns(UpdateCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateCampaignsResponse> UpdateCampaignsAsync(UpdateCampaignsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateCampaignsResponse>("UpdateCampaigns", request, _serviceType);
        }

        public GetNegativeSitesByCampaignIdsResponse GetNegativeSitesByCampaignIds(GetNegativeSitesByCampaignIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetNegativeSitesByCampaignIdsResponse> GetNegativeSitesByCampaignIdsAsync(GetNegativeSitesByCampaignIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetNegativeSitesByCampaignIdsResponse>("GetNegativeSitesByCampaignIds", request, _serviceType);
        }

        public SetNegativeSitesToCampaignsResponse SetNegativeSitesToCampaigns(SetNegativeSitesToCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetNegativeSitesToCampaignsResponse> SetNegativeSitesToCampaignsAsync(SetNegativeSitesToCampaignsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetNegativeSitesToCampaignsResponse>("SetNegativeSitesToCampaigns", request, _serviceType);
        }

        public GetConfigValueResponse GetConfigValue(GetConfigValueRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetConfigValueResponse> GetConfigValueAsync(GetConfigValueRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetConfigValueResponse>("GetConfigValue", request, _serviceType);
        }

        public GetBSCCountriesResponse GetBSCCountries(GetBSCCountriesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetBSCCountriesResponse> GetBSCCountriesAsync(GetBSCCountriesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetBSCCountriesResponse>("GetBSCCountries", request, _serviceType);
        }

        public AddAdGroupsResponse AddAdGroups(AddAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAdGroupsResponse> AddAdGroupsAsync(AddAdGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAdGroupsResponse>("AddAdGroups", request, _serviceType);
        }

        public DeleteAdGroupsResponse DeleteAdGroups(DeleteAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAdGroupsResponse> DeleteAdGroupsAsync(DeleteAdGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAdGroupsResponse>("DeleteAdGroups", request, _serviceType);
        }

        public GetAdGroupsByIdsResponse GetAdGroupsByIds(GetAdGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdGroupsByIdsResponse> GetAdGroupsByIdsAsync(GetAdGroupsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdGroupsByIdsResponse>("GetAdGroupsByIds", request, _serviceType);
        }

        public GetAdGroupsByCampaignIdResponse GetAdGroupsByCampaignId(GetAdGroupsByCampaignIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(GetAdGroupsByCampaignIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdGroupsByCampaignIdResponse>("GetAdGroupsByCampaignId", request, _serviceType);
        }

        public UpdateAdGroupsResponse UpdateAdGroups(UpdateAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAdGroupsResponse> UpdateAdGroupsAsync(UpdateAdGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAdGroupsResponse>("UpdateAdGroups", request, _serviceType);
        }

        public GetNegativeSitesByAdGroupIdsResponse GetNegativeSitesByAdGroupIds(GetNegativeSitesByAdGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetNegativeSitesByAdGroupIdsResponse> GetNegativeSitesByAdGroupIdsAsync(GetNegativeSitesByAdGroupIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetNegativeSitesByAdGroupIdsResponse>("GetNegativeSitesByAdGroupIds", request, _serviceType);
        }

        public SetNegativeSitesToAdGroupsResponse SetNegativeSitesToAdGroups(SetNegativeSitesToAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetNegativeSitesToAdGroupsResponse> SetNegativeSitesToAdGroupsAsync(SetNegativeSitesToAdGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetNegativeSitesToAdGroupsResponse>("SetNegativeSitesToAdGroups", request, _serviceType);
        }

        public GetGeoLocationsFileUrlResponse GetGeoLocationsFileUrl(GetGeoLocationsFileUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetGeoLocationsFileUrlResponse> GetGeoLocationsFileUrlAsync(GetGeoLocationsFileUrlRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetGeoLocationsFileUrlResponse>("GetGeoLocationsFileUrl", request, _serviceType);
        }

        public AddAdsResponse AddAds(AddAdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAdsResponse> AddAdsAsync(AddAdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAdsResponse>("AddAds", request, _serviceType);
        }

        public DeleteAdsResponse DeleteAds(DeleteAdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAdsResponse> DeleteAdsAsync(DeleteAdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAdsResponse>("DeleteAds", request, _serviceType);
        }

        public GetAdsByEditorialStatusResponse GetAdsByEditorialStatus(GetAdsByEditorialStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdsByEditorialStatusResponse> GetAdsByEditorialStatusAsync(GetAdsByEditorialStatusRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdsByEditorialStatusResponse>("GetAdsByEditorialStatus", request, _serviceType);
        }

        public GetAdsByIdsResponse GetAdsByIds(GetAdsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdsByIdsResponse> GetAdsByIdsAsync(GetAdsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdsByIdsResponse>("GetAdsByIds", request, _serviceType);
        }

        public GetAdsByAdGroupIdResponse GetAdsByAdGroupId(GetAdsByAdGroupIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(GetAdsByAdGroupIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdsByAdGroupIdResponse>("GetAdsByAdGroupId", request, _serviceType);
        }

        public UpdateAdsResponse UpdateAds(UpdateAdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAdsResponse> UpdateAdsAsync(UpdateAdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAdsResponse>("UpdateAds", request, _serviceType);
        }

        public AddKeywordsResponse AddKeywords(AddKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddKeywordsResponse> AddKeywordsAsync(AddKeywordsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddKeywordsResponse>("AddKeywords", request, _serviceType);
        }

        public DeleteKeywordsResponse DeleteKeywords(DeleteKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteKeywordsResponse> DeleteKeywordsAsync(DeleteKeywordsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteKeywordsResponse>("DeleteKeywords", request, _serviceType);
        }

        public GetKeywordsByEditorialStatusResponse GetKeywordsByEditorialStatus(GetKeywordsByEditorialStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetKeywordsByEditorialStatusResponse> GetKeywordsByEditorialStatusAsync(GetKeywordsByEditorialStatusRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetKeywordsByEditorialStatusResponse>("GetKeywordsByEditorialStatus", request, _serviceType);
        }

        public GetKeywordsByIdsResponse GetKeywordsByIds(GetKeywordsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetKeywordsByIdsResponse> GetKeywordsByIdsAsync(GetKeywordsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetKeywordsByIdsResponse>("GetKeywordsByIds", request, _serviceType);
        }

        public GetKeywordsByAdGroupIdResponse GetKeywordsByAdGroupId(GetKeywordsByAdGroupIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetKeywordsByAdGroupIdResponse> GetKeywordsByAdGroupIdAsync(GetKeywordsByAdGroupIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetKeywordsByAdGroupIdResponse>("GetKeywordsByAdGroupId", request, _serviceType);
        }

        public UpdateKeywordsResponse UpdateKeywords(UpdateKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateKeywordsResponse> UpdateKeywordsAsync(UpdateKeywordsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateKeywordsResponse>("UpdateKeywords", request, _serviceType);
        }

        public AppealEditorialRejectionsResponse AppealEditorialRejections(AppealEditorialRejectionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AppealEditorialRejectionsResponse> AppealEditorialRejectionsAsync(AppealEditorialRejectionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AppealEditorialRejectionsResponse>("AppealEditorialRejections", request, _serviceType);
        }

        public GetEditorialReasonsByIdsResponse GetEditorialReasonsByIds(GetEditorialReasonsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetEditorialReasonsByIdsResponse> GetEditorialReasonsByIdsAsync(GetEditorialReasonsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetEditorialReasonsByIdsResponse>("GetEditorialReasonsByIds", request, _serviceType);
        }

        public GetAccountMigrationStatusesResponse GetAccountMigrationStatuses(GetAccountMigrationStatusesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAccountMigrationStatusesResponse> GetAccountMigrationStatusesAsync(GetAccountMigrationStatusesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAccountMigrationStatusesResponse>("GetAccountMigrationStatuses", request, _serviceType);
        }

        public SetAccountPropertiesResponse SetAccountProperties(SetAccountPropertiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetAccountPropertiesResponse> SetAccountPropertiesAsync(SetAccountPropertiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetAccountPropertiesResponse>("SetAccountProperties", request, _serviceType);
        }

        public GetAccountPropertiesResponse GetAccountProperties(GetAccountPropertiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAccountPropertiesResponse> GetAccountPropertiesAsync(GetAccountPropertiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAccountPropertiesResponse>("GetAccountProperties", request, _serviceType);
        }

        public AddAdExtensionsResponse AddAdExtensions(AddAdExtensionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAdExtensionsResponse> AddAdExtensionsAsync(AddAdExtensionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAdExtensionsResponse>("AddAdExtensions", request, _serviceType);
        }

        public GetAdExtensionsByIdsResponse GetAdExtensionsByIds(GetAdExtensionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(GetAdExtensionsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdExtensionsByIdsResponse>("GetAdExtensionsByIds", request, _serviceType);
        }

        public UpdateAdExtensionsResponse UpdateAdExtensions(UpdateAdExtensionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAdExtensionsResponse> UpdateAdExtensionsAsync(UpdateAdExtensionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAdExtensionsResponse>("UpdateAdExtensions", request, _serviceType);
        }

        public DeleteAdExtensionsResponse DeleteAdExtensions(DeleteAdExtensionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAdExtensionsResponse> DeleteAdExtensionsAsync(DeleteAdExtensionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAdExtensionsResponse>("DeleteAdExtensions", request, _serviceType);
        }

        public GetAdExtensionsEditorialReasonsResponse GetAdExtensionsEditorialReasons(GetAdExtensionsEditorialReasonsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdExtensionsEditorialReasonsResponse> GetAdExtensionsEditorialReasonsAsync(GetAdExtensionsEditorialReasonsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdExtensionsEditorialReasonsResponse>("GetAdExtensionsEditorialReasons", request, _serviceType);
        }

        public SetAdExtensionsAssociationsResponse SetAdExtensionsAssociations(SetAdExtensionsAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetAdExtensionsAssociationsResponse> SetAdExtensionsAssociationsAsync(SetAdExtensionsAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetAdExtensionsAssociationsResponse>("SetAdExtensionsAssociations", request, _serviceType);
        }

        public GetAdExtensionsAssociationsResponse GetAdExtensionsAssociations(GetAdExtensionsAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdExtensionsAssociationsResponse> GetAdExtensionsAssociationsAsync(GetAdExtensionsAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdExtensionsAssociationsResponse>("GetAdExtensionsAssociations", request, _serviceType);
        }

        public DeleteAdExtensionsAssociationsResponse DeleteAdExtensionsAssociations(DeleteAdExtensionsAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAdExtensionsAssociationsResponse> DeleteAdExtensionsAssociationsAsync(DeleteAdExtensionsAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAdExtensionsAssociationsResponse>("DeleteAdExtensionsAssociations", request, _serviceType);
        }

        public GetAdExtensionIdsByAccountIdResponse GetAdExtensionIdsByAccountId(GetAdExtensionIdsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdExtensionIdsByAccountIdResponse> GetAdExtensionIdsByAccountIdAsync(GetAdExtensionIdsByAccountIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdExtensionIdsByAccountIdResponse>("GetAdExtensionIdsByAccountId", request, _serviceType);
        }

        public AddMediaResponse AddMedia(AddMediaRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddMediaResponse> AddMediaAsync(AddMediaRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddMediaResponse>("AddMedia", request, _serviceType);
        }

        public DeleteMediaResponse DeleteMedia(DeleteMediaRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteMediaResponse> DeleteMediaAsync(DeleteMediaRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteMediaResponse>("DeleteMedia", request, _serviceType);
        }

        public GetMediaMetaDataByAccountIdResponse GetMediaMetaDataByAccountId(GetMediaMetaDataByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetMediaMetaDataByAccountIdResponse> GetMediaMetaDataByAccountIdAsync(GetMediaMetaDataByAccountIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetMediaMetaDataByAccountIdResponse>("GetMediaMetaDataByAccountId", request, _serviceType);
        }

        public GetMediaMetaDataByIdsResponse GetMediaMetaDataByIds(GetMediaMetaDataByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetMediaMetaDataByIdsResponse> GetMediaMetaDataByIdsAsync(GetMediaMetaDataByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetMediaMetaDataByIdsResponse>("GetMediaMetaDataByIds", request, _serviceType);
        }

        public GetMediaAssociationsResponse GetMediaAssociations(GetMediaAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetMediaAssociationsResponse> GetMediaAssociationsAsync(GetMediaAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetMediaAssociationsResponse>("GetMediaAssociations", request, _serviceType);
        }

        public GetAdGroupCriterionsByIdsResponse GetAdGroupCriterionsByIds(GetAdGroupCriterionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdGroupCriterionsByIdsResponse> GetAdGroupCriterionsByIdsAsync(GetAdGroupCriterionsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAdGroupCriterionsByIdsResponse>("GetAdGroupCriterionsByIds", request, _serviceType);
        }

        public AddAdGroupCriterionsResponse AddAdGroupCriterions(AddAdGroupCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAdGroupCriterionsResponse> AddAdGroupCriterionsAsync(AddAdGroupCriterionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAdGroupCriterionsResponse>("AddAdGroupCriterions", request, _serviceType);
        }

        public UpdateAdGroupCriterionsResponse UpdateAdGroupCriterions(UpdateAdGroupCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAdGroupCriterionsResponse> UpdateAdGroupCriterionsAsync(UpdateAdGroupCriterionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAdGroupCriterionsResponse>("UpdateAdGroupCriterions", request, _serviceType);
        }

        public DeleteAdGroupCriterionsResponse DeleteAdGroupCriterions(DeleteAdGroupCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAdGroupCriterionsResponse> DeleteAdGroupCriterionsAsync(DeleteAdGroupCriterionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAdGroupCriterionsResponse>("DeleteAdGroupCriterions", request, _serviceType);
        }

        public ApplyProductPartitionActionsResponse ApplyProductPartitionActions(ApplyProductPartitionActionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplyProductPartitionActionsResponse> ApplyProductPartitionActionsAsync(ApplyProductPartitionActionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<ApplyProductPartitionActionsResponse>("ApplyProductPartitionActions", request, _serviceType);
        }

        public ApplyHotelGroupActionsResponse ApplyHotelGroupActions(ApplyHotelGroupActionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplyHotelGroupActionsResponse> ApplyHotelGroupActionsAsync(ApplyHotelGroupActionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<ApplyHotelGroupActionsResponse>("ApplyHotelGroupActions", request, _serviceType);
        }

        public ApplyAssetGroupListingGroupActionsResponse ApplyAssetGroupListingGroupActions(ApplyAssetGroupListingGroupActionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplyAssetGroupListingGroupActionsResponse> ApplyAssetGroupListingGroupActionsAsync(ApplyAssetGroupListingGroupActionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<ApplyAssetGroupListingGroupActionsResponse>("ApplyAssetGroupListingGroupActions", request, _serviceType);
        }

        public GetAssetGroupListingGroupsByIdsResponse GetAssetGroupListingGroupsByIds(GetAssetGroupListingGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAssetGroupListingGroupsByIdsResponse> GetAssetGroupListingGroupsByIdsAsync(GetAssetGroupListingGroupsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAssetGroupListingGroupsByIdsResponse>("GetAssetGroupListingGroupsByIds", request, _serviceType);
        }

        public GetBMCStoresByCustomerIdResponse GetBMCStoresByCustomerId(GetBMCStoresByCustomerIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetBMCStoresByCustomerIdResponse> GetBMCStoresByCustomerIdAsync(GetBMCStoresByCustomerIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetBMCStoresByCustomerIdResponse>("GetBMCStoresByCustomerId", request, _serviceType);
        }

        public AddNegativeKeywordsToEntitiesResponse AddNegativeKeywordsToEntities(AddNegativeKeywordsToEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(AddNegativeKeywordsToEntitiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddNegativeKeywordsToEntitiesResponse>("AddNegativeKeywordsToEntities", request, _serviceType);
        }

        public GetNegativeKeywordsByEntityIdsResponse GetNegativeKeywordsByEntityIds(GetNegativeKeywordsByEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetNegativeKeywordsByEntityIdsResponse> GetNegativeKeywordsByEntityIdsAsync(GetNegativeKeywordsByEntityIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetNegativeKeywordsByEntityIdsResponse>("GetNegativeKeywordsByEntityIds", request, _serviceType);
        }

        public DeleteNegativeKeywordsFromEntitiesResponse DeleteNegativeKeywordsFromEntities(DeleteNegativeKeywordsFromEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteNegativeKeywordsFromEntitiesResponse> DeleteNegativeKeywordsFromEntitiesAsync(DeleteNegativeKeywordsFromEntitiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteNegativeKeywordsFromEntitiesResponse>("DeleteNegativeKeywordsFromEntities", request, _serviceType);
        }

        public GetSharedEntitiesByAccountIdResponse GetSharedEntitiesByAccountId(GetSharedEntitiesByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetSharedEntitiesByAccountIdResponse> GetSharedEntitiesByAccountIdAsync(GetSharedEntitiesByAccountIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetSharedEntitiesByAccountIdResponse>("GetSharedEntitiesByAccountId", request, _serviceType);
        }

        public GetSharedEntitiesResponse GetSharedEntities(GetSharedEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetSharedEntitiesResponse> GetSharedEntitiesAsync(GetSharedEntitiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetSharedEntitiesResponse>("GetSharedEntities", request, _serviceType);
        }

        public AddSharedEntityResponse AddSharedEntity(AddSharedEntityRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddSharedEntityResponse> AddSharedEntityAsync(AddSharedEntityRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddSharedEntityResponse>("AddSharedEntity", request, _serviceType);
        }

        public GetListItemsBySharedListResponse GetListItemsBySharedList(GetListItemsBySharedListRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetListItemsBySharedListResponse> GetListItemsBySharedListAsync(GetListItemsBySharedListRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetListItemsBySharedListResponse>("GetListItemsBySharedList", request, _serviceType);
        }

        public AddListItemsToSharedListResponse AddListItemsToSharedList(AddListItemsToSharedListRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddListItemsToSharedListResponse> AddListItemsToSharedListAsync(AddListItemsToSharedListRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddListItemsToSharedListResponse>("AddListItemsToSharedList", request, _serviceType);
        }

        public UpdateSharedEntitiesResponse UpdateSharedEntities(UpdateSharedEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateSharedEntitiesResponse> UpdateSharedEntitiesAsync(UpdateSharedEntitiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateSharedEntitiesResponse>("UpdateSharedEntities", request, _serviceType);
        }

        public DeleteListItemsFromSharedListResponse DeleteListItemsFromSharedList(DeleteListItemsFromSharedListRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteListItemsFromSharedListResponse> DeleteListItemsFromSharedListAsync(DeleteListItemsFromSharedListRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteListItemsFromSharedListResponse>("DeleteListItemsFromSharedList", request, _serviceType);
        }

        public SetSharedEntityAssociationsResponse SetSharedEntityAssociations(SetSharedEntityAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetSharedEntityAssociationsResponse> SetSharedEntityAssociationsAsync(SetSharedEntityAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetSharedEntityAssociationsResponse>("SetSharedEntityAssociations", request, _serviceType);
        }

        public DeleteSharedEntityAssociationsResponse DeleteSharedEntityAssociations(DeleteSharedEntityAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteSharedEntityAssociationsResponse> DeleteSharedEntityAssociationsAsync(DeleteSharedEntityAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteSharedEntityAssociationsResponse>("DeleteSharedEntityAssociations", request, _serviceType);
        }

        public GetSharedEntityAssociationsBySharedEntityIdsResponse GetSharedEntityAssociationsBySharedEntityIds(GetSharedEntityAssociationsBySharedEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(GetSharedEntityAssociationsBySharedEntityIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetSharedEntityAssociationsBySharedEntityIdsResponse>("GetSharedEntityAssociationsBySharedEntityIds", request, _serviceType);
        }

        public GetSharedEntityAssociationsByEntityIdsResponse GetSharedEntityAssociationsByEntityIds(GetSharedEntityAssociationsByEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetSharedEntityAssociationsByEntityIdsResponse> GetSharedEntityAssociationsByEntityIdsAsync(GetSharedEntityAssociationsByEntityIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetSharedEntityAssociationsByEntityIdsResponse>("GetSharedEntityAssociationsByEntityIds", request, _serviceType);
        }

        public DeleteSharedEntitiesResponse DeleteSharedEntities(DeleteSharedEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteSharedEntitiesResponse> DeleteSharedEntitiesAsync(DeleteSharedEntitiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteSharedEntitiesResponse>("DeleteSharedEntities", request, _serviceType);
        }

        public GetCampaignSizesByAccountIdResponse GetCampaignSizesByAccountId(GetCampaignSizesByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCampaignSizesByAccountIdResponse> GetCampaignSizesByAccountIdAsync(GetCampaignSizesByAccountIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetCampaignSizesByAccountIdResponse>("GetCampaignSizesByAccountId", request, _serviceType);
        }

        public AddCampaignCriterionsResponse AddCampaignCriterions(AddCampaignCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddCampaignCriterionsResponse> AddCampaignCriterionsAsync(AddCampaignCriterionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddCampaignCriterionsResponse>("AddCampaignCriterions", request, _serviceType);
        }

        public UpdateCampaignCriterionsResponse UpdateCampaignCriterions(UpdateCampaignCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateCampaignCriterionsResponse> UpdateCampaignCriterionsAsync(UpdateCampaignCriterionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateCampaignCriterionsResponse>("UpdateCampaignCriterions", request, _serviceType);
        }

        public DeleteCampaignCriterionsResponse DeleteCampaignCriterions(DeleteCampaignCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteCampaignCriterionsResponse> DeleteCampaignCriterionsAsync(DeleteCampaignCriterionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteCampaignCriterionsResponse>("DeleteCampaignCriterions", request, _serviceType);
        }

        public GetCampaignCriterionsByIdsResponse GetCampaignCriterionsByIds(GetCampaignCriterionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCampaignCriterionsByIdsResponse> GetCampaignCriterionsByIdsAsync(GetCampaignCriterionsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetCampaignCriterionsByIdsResponse>("GetCampaignCriterionsByIds", request, _serviceType);
        }

        public AddBudgetsResponse AddBudgets(AddBudgetsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddBudgetsResponse> AddBudgetsAsync(AddBudgetsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddBudgetsResponse>("AddBudgets", request, _serviceType);
        }

        public UpdateBudgetsResponse UpdateBudgets(UpdateBudgetsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateBudgetsResponse> UpdateBudgetsAsync(UpdateBudgetsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateBudgetsResponse>("UpdateBudgets", request, _serviceType);
        }

        public DeleteBudgetsResponse DeleteBudgets(DeleteBudgetsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteBudgetsResponse> DeleteBudgetsAsync(DeleteBudgetsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteBudgetsResponse>("DeleteBudgets", request, _serviceType);
        }

        public GetBudgetsByIdsResponse GetBudgetsByIds(GetBudgetsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(GetBudgetsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetBudgetsByIdsResponse>("GetBudgetsByIds", request, _serviceType);
        }

        public GetCampaignIdsByBudgetIdsResponse GetCampaignIdsByBudgetIds(GetCampaignIdsByBudgetIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCampaignIdsByBudgetIdsResponse> GetCampaignIdsByBudgetIdsAsync(GetCampaignIdsByBudgetIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetCampaignIdsByBudgetIdsResponse>("GetCampaignIdsByBudgetIds", request, _serviceType);
        }

        public AddBidStrategiesResponse AddBidStrategies(AddBidStrategiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddBidStrategiesResponse> AddBidStrategiesAsync(AddBidStrategiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddBidStrategiesResponse>("AddBidStrategies", request, _serviceType);
        }

        public UpdateBidStrategiesResponse UpdateBidStrategies(UpdateBidStrategiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateBidStrategiesResponse> UpdateBidStrategiesAsync(UpdateBidStrategiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateBidStrategiesResponse>("UpdateBidStrategies", request, _serviceType);
        }

        public DeleteBidStrategiesResponse DeleteBidStrategies(DeleteBidStrategiesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteBidStrategiesResponse> DeleteBidStrategiesAsync(DeleteBidStrategiesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteBidStrategiesResponse>("DeleteBidStrategies", request, _serviceType);
        }

        public GetBidStrategiesByIdsResponse GetBidStrategiesByIds(GetBidStrategiesByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetBidStrategiesByIdsResponse> GetBidStrategiesByIdsAsync(GetBidStrategiesByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetBidStrategiesByIdsResponse>("GetBidStrategiesByIds", request, _serviceType);
        }

        public GetCampaignIdsByBidStrategyIdsResponse GetCampaignIdsByBidStrategyIds(GetCampaignIdsByBidStrategyIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCampaignIdsByBidStrategyIdsResponse> GetCampaignIdsByBidStrategyIdsAsync(GetCampaignIdsByBidStrategyIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetCampaignIdsByBidStrategyIdsResponse>("GetCampaignIdsByBidStrategyIds", request, _serviceType);
        }

        public AddAudienceGroupsResponse AddAudienceGroups(AddAudienceGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAudienceGroupsResponse> AddAudienceGroupsAsync(AddAudienceGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAudienceGroupsResponse>("AddAudienceGroups", request, _serviceType);
        }

        public UpdateAudienceGroupsResponse UpdateAudienceGroups(UpdateAudienceGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAudienceGroupsResponse> UpdateAudienceGroupsAsync(UpdateAudienceGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAudienceGroupsResponse>("UpdateAudienceGroups", request, _serviceType);
        }

        public DeleteAudienceGroupsResponse DeleteAudienceGroups(DeleteAudienceGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAudienceGroupsResponse> DeleteAudienceGroupsAsync(DeleteAudienceGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAudienceGroupsResponse>("DeleteAudienceGroups", request, _serviceType);
        }

        public GetAudienceGroupsByIdsResponse GetAudienceGroupsByIds(GetAudienceGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAudienceGroupsByIdsResponse> GetAudienceGroupsByIdsAsync(GetAudienceGroupsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAudienceGroupsByIdsResponse>("GetAudienceGroupsByIds", request, _serviceType);
        }

        public AddAssetGroupsResponse AddAssetGroups(AddAssetGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAssetGroupsResponse> AddAssetGroupsAsync(AddAssetGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAssetGroupsResponse>("AddAssetGroups", request, _serviceType);
        }

        public UpdateAssetGroupsResponse UpdateAssetGroups(UpdateAssetGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAssetGroupsResponse> UpdateAssetGroupsAsync(UpdateAssetGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAssetGroupsResponse>("UpdateAssetGroups", request, _serviceType);
        }

        public DeleteAssetGroupsResponse DeleteAssetGroups(DeleteAssetGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAssetGroupsResponse> DeleteAssetGroupsAsync(DeleteAssetGroupsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAssetGroupsResponse>("DeleteAssetGroups", request, _serviceType);
        }

        public GetAssetGroupsByIdsResponse GetAssetGroupsByIds(GetAssetGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAssetGroupsByIdsResponse> GetAssetGroupsByIdsAsync(GetAssetGroupsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAssetGroupsByIdsResponse>("GetAssetGroupsByIds", request, _serviceType);
        }

        public GetAssetGroupsByCampaignIdResponse GetAssetGroupsByCampaignId(GetAssetGroupsByCampaignIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAssetGroupsByCampaignIdResponse> GetAssetGroupsByCampaignIdAsync(GetAssetGroupsByCampaignIdRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAssetGroupsByCampaignIdResponse>("GetAssetGroupsByCampaignId", request, _serviceType);
        }

        public SetAudienceGroupAssetGroupAssociationsResponse SetAudienceGroupAssetGroupAssociations(SetAudienceGroupAssetGroupAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetAudienceGroupAssetGroupAssociationsResponse> SetAudienceGroupAssetGroupAssociationsAsync(SetAudienceGroupAssetGroupAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetAudienceGroupAssetGroupAssociationsResponse>("SetAudienceGroupAssetGroupAssociations", request, _serviceType);
        }

        public DeleteAudienceGroupAssetGroupAssociationsResponse DeleteAudienceGroupAssetGroupAssociations(DeleteAudienceGroupAssetGroupAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAudienceGroupAssetGroupAssociationsResponse> DeleteAudienceGroupAssetGroupAssociationsAsync(DeleteAudienceGroupAssetGroupAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAudienceGroupAssetGroupAssociationsResponse>("DeleteAudienceGroupAssetGroupAssociations", request, _serviceType);
        }

        public GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse GetAudienceGroupAssetGroupAssociationsByAssetGroupIds(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse> GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsAsync(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse>("GetAudienceGroupAssetGroupAssociationsByAssetGroupIds", request, _serviceType);
        }

        public GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse GetAudienceGroupAssetGroupAssociationsByAudienceGroupIds(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse> GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsAsync(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse>("GetAudienceGroupAssetGroupAssociationsByAudienceGroupIds", request, _serviceType);
        }

        public AddAudiencesResponse AddAudiences(AddAudiencesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddAudiencesResponse> AddAudiencesAsync(AddAudiencesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddAudiencesResponse>("AddAudiences", request, _serviceType);
        }

        public UpdateAudiencesResponse UpdateAudiences(UpdateAudiencesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAudiencesResponse> UpdateAudiencesAsync(UpdateAudiencesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateAudiencesResponse>("UpdateAudiences", request, _serviceType);
        }

        public DeleteAudiencesResponse DeleteAudiences(DeleteAudiencesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteAudiencesResponse> DeleteAudiencesAsync(DeleteAudiencesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteAudiencesResponse>("DeleteAudiences", request, _serviceType);
        }

        public GetAudiencesByIdsResponse GetAudiencesByIds(GetAudiencesByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAudiencesByIdsResponse> GetAudiencesByIdsAsync(GetAudiencesByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetAudiencesByIdsResponse>("GetAudiencesByIds", request, _serviceType);
        }

        public GetUetTagsByIdsResponse GetUetTagsByIds(GetUetTagsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(GetUetTagsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetUetTagsByIdsResponse>("GetUetTagsByIds", request, _serviceType);
        }

        public AddUetTagsResponse AddUetTags(AddUetTagsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddUetTagsResponse> AddUetTagsAsync(AddUetTagsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddUetTagsResponse>("AddUetTags", request, _serviceType);
        }

        public UpdateUetTagsResponse UpdateUetTags(UpdateUetTagsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateUetTagsResponse> UpdateUetTagsAsync(UpdateUetTagsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateUetTagsResponse>("UpdateUetTags", request, _serviceType);
        }

        public GetConversionGoalsByIdsResponse GetConversionGoalsByIds(GetConversionGoalsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(GetConversionGoalsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetConversionGoalsByIdsResponse>("GetConversionGoalsByIds", request, _serviceType);
        }

        public GetConversionGoalsByTagIdsResponse GetConversionGoalsByTagIds(GetConversionGoalsByTagIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetConversionGoalsByTagIdsResponse> GetConversionGoalsByTagIdsAsync(GetConversionGoalsByTagIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetConversionGoalsByTagIdsResponse>("GetConversionGoalsByTagIds", request, _serviceType);
        }

        public AddConversionGoalsResponse AddConversionGoals(AddConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddConversionGoalsResponse> AddConversionGoalsAsync(AddConversionGoalsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddConversionGoalsResponse>("AddConversionGoals", request, _serviceType);
        }

        public UpdateConversionGoalsResponse UpdateConversionGoals(UpdateConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateConversionGoalsResponse> UpdateConversionGoalsAsync(UpdateConversionGoalsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateConversionGoalsResponse>("UpdateConversionGoals", request, _serviceType);
        }

        public ApplyOfflineConversionsResponse ApplyOfflineConversions(ApplyOfflineConversionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplyOfflineConversionsResponse> ApplyOfflineConversionsAsync(ApplyOfflineConversionsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<ApplyOfflineConversionsResponse>("ApplyOfflineConversions", request, _serviceType);
        }

        public ApplyOfflineConversionAdjustmentsResponse ApplyOfflineConversionAdjustments(ApplyOfflineConversionAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplyOfflineConversionAdjustmentsResponse> ApplyOfflineConversionAdjustmentsAsync(ApplyOfflineConversionAdjustmentsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<ApplyOfflineConversionAdjustmentsResponse>("ApplyOfflineConversionAdjustments", request, _serviceType);
        }

        public ApplyOnlineConversionAdjustmentsResponse ApplyOnlineConversionAdjustments(ApplyOnlineConversionAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplyOnlineConversionAdjustmentsResponse> ApplyOnlineConversionAdjustmentsAsync(ApplyOnlineConversionAdjustmentsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<ApplyOnlineConversionAdjustmentsResponse>("ApplyOnlineConversionAdjustments", request, _serviceType);
        }

        public AddLabelsResponse AddLabels(AddLabelsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddLabelsResponse> AddLabelsAsync(AddLabelsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddLabelsResponse>("AddLabels", request, _serviceType);
        }

        public DeleteLabelsResponse DeleteLabels(DeleteLabelsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteLabelsResponse> DeleteLabelsAsync(DeleteLabelsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteLabelsResponse>("DeleteLabels", request, _serviceType);
        }

        public UpdateLabelsResponse UpdateLabels(UpdateLabelsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateLabelsResponse> UpdateLabelsAsync(UpdateLabelsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateLabelsResponse>("UpdateLabels", request, _serviceType);
        }

        public GetLabelsByIdsResponse GetLabelsByIds(GetLabelsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetLabelsByIdsResponse> GetLabelsByIdsAsync(GetLabelsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetLabelsByIdsResponse>("GetLabelsByIds", request, _serviceType);
        }

        public SetLabelAssociationsResponse SetLabelAssociations(SetLabelAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SetLabelAssociationsResponse> SetLabelAssociationsAsync(SetLabelAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SetLabelAssociationsResponse>("SetLabelAssociations", request, _serviceType);
        }

        public DeleteLabelAssociationsResponse DeleteLabelAssociations(DeleteLabelAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteLabelAssociationsResponse> DeleteLabelAssociationsAsync(DeleteLabelAssociationsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteLabelAssociationsResponse>("DeleteLabelAssociations", request, _serviceType);
        }

        public GetLabelAssociationsByEntityIdsResponse GetLabelAssociationsByEntityIds(GetLabelAssociationsByEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetLabelAssociationsByEntityIdsResponse> GetLabelAssociationsByEntityIdsAsync(GetLabelAssociationsByEntityIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetLabelAssociationsByEntityIdsResponse>("GetLabelAssociationsByEntityIds", request, _serviceType);
        }

        public GetLabelAssociationsByLabelIdsResponse GetLabelAssociationsByLabelIds(GetLabelAssociationsByLabelIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetLabelAssociationsByLabelIdsResponse> GetLabelAssociationsByLabelIdsAsync(GetLabelAssociationsByLabelIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetLabelAssociationsByLabelIdsResponse>("GetLabelAssociationsByLabelIds", request, _serviceType);
        }

        public AddExperimentsResponse AddExperiments(AddExperimentsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddExperimentsResponse> AddExperimentsAsync(AddExperimentsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddExperimentsResponse>("AddExperiments", request, _serviceType);
        }

        public DeleteExperimentsResponse DeleteExperiments(DeleteExperimentsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteExperimentsResponse> DeleteExperimentsAsync(DeleteExperimentsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteExperimentsResponse>("DeleteExperiments", request, _serviceType);
        }

        public UpdateExperimentsResponse UpdateExperiments(UpdateExperimentsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateExperimentsResponse> UpdateExperimentsAsync(UpdateExperimentsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateExperimentsResponse>("UpdateExperiments", request, _serviceType);
        }

        public GetExperimentsByIdsResponse GetExperimentsByIds(GetExperimentsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetExperimentsByIdsResponse> GetExperimentsByIdsAsync(GetExperimentsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetExperimentsByIdsResponse>("GetExperimentsByIds", request, _serviceType);
        }

        public GetProfileDataFileUrlResponse GetProfileDataFileUrl(GetProfileDataFileUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetProfileDataFileUrlResponse> GetProfileDataFileUrlAsync(GetProfileDataFileUrlRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetProfileDataFileUrlResponse>("GetProfileDataFileUrl", request, _serviceType);
        }

        public SearchCompaniesResponse SearchCompanies(SearchCompaniesRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SearchCompaniesResponse> SearchCompaniesAsync(SearchCompaniesRequest request)
        {
            return await _restServiceClient.CallServiceAsync<SearchCompaniesResponse>("SearchCompanies", request, _serviceType);
        }

        public GetFileImportUploadUrlResponse GetFileImportUploadUrl(GetFileImportUploadUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetFileImportUploadUrlResponse> GetFileImportUploadUrlAsync(GetFileImportUploadUrlRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetFileImportUploadUrlResponse>("GetFileImportUploadUrl", request, _serviceType);
        }

        public AddImportJobsResponse AddImportJobs(AddImportJobsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddImportJobsResponse> AddImportJobsAsync(AddImportJobsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddImportJobsResponse>("AddImportJobs", request, _serviceType);
        }

        public GetImportResultsResponse GetImportResults(GetImportResultsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetImportResultsResponse> GetImportResultsAsync(GetImportResultsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetImportResultsResponse>("GetImportResults", request, _serviceType);
        }

        public GetImportJobsByIdsResponse GetImportJobsByIds(GetImportJobsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetImportJobsByIdsResponse> GetImportJobsByIdsAsync(GetImportJobsByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetImportJobsByIdsResponse>("GetImportJobsByIds", request, _serviceType);
        }

        public DeleteImportJobsResponse DeleteImportJobs(DeleteImportJobsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteImportJobsResponse> DeleteImportJobsAsync(DeleteImportJobsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteImportJobsResponse>("DeleteImportJobs", request, _serviceType);
        }

        public GetImportEntityIdsMappingResponse GetImportEntityIdsMapping(GetImportEntityIdsMappingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetImportEntityIdsMappingResponse> GetImportEntityIdsMappingAsync(GetImportEntityIdsMappingRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetImportEntityIdsMappingResponse>("GetImportEntityIdsMapping", request, _serviceType);
        }

        public UpdateImportJobsResponse UpdateImportJobs(UpdateImportJobsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateImportJobsResponse> UpdateImportJobsAsync(UpdateImportJobsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateImportJobsResponse>("UpdateImportJobs", request, _serviceType);
        }

        public AddVideosResponse AddVideos(AddVideosRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddVideosResponse> AddVideosAsync(AddVideosRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddVideosResponse>("AddVideos", request, _serviceType);
        }

        public DeleteVideosResponse DeleteVideos(DeleteVideosRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteVideosResponse> DeleteVideosAsync(DeleteVideosRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteVideosResponse>("DeleteVideos", request, _serviceType);
        }

        public GetVideosByIdsResponse GetVideosByIds(GetVideosByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetVideosByIdsResponse> GetVideosByIdsAsync(GetVideosByIdsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<GetVideosByIdsResponse>("GetVideosByIds", request, _serviceType);
        }

        public UpdateVideosResponse UpdateVideos(UpdateVideosRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateVideosResponse> UpdateVideosAsync(UpdateVideosRequest request)
        {
            return await _restServiceClient.CallServiceAsync<UpdateVideosResponse>("UpdateVideos", request, _serviceType);
        }

        public AddCampaignConversionGoalsResponse AddCampaignConversionGoals(AddCampaignConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddCampaignConversionGoalsResponse> AddCampaignConversionGoalsAsync(AddCampaignConversionGoalsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<AddCampaignConversionGoalsResponse>("AddCampaignConversionGoals", request, _serviceType);
        }

        public DeleteCampaignConversionGoalsResponse DeleteCampaignConversionGoals(DeleteCampaignConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteCampaignConversionGoalsResponse> DeleteCampaignConversionGoalsAsync(DeleteCampaignConversionGoalsRequest request)
        {
            return await _restServiceClient.CallServiceAsync<DeleteCampaignConversionGoalsResponse>("DeleteCampaignConversionGoals", request, _serviceType);
        }
    }
}