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
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.Internal
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

        public Task<AddCampaignsResponse> AddCampaignsAsync(AddCampaignsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddCampaignsResponse>("AddCampaigns", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCampaignsByAccountIdResponse GetCampaignsByAccountId(GetCampaignsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(GetCampaignsByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCampaignsByAccountIdResponse>("GetCampaignsByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCampaignsByIdsResponse GetCampaignsByIds(GetCampaignsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(GetCampaignsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCampaignsByIdsResponse>("GetCampaignsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteCampaignsResponse DeleteCampaigns(DeleteCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteCampaignsResponse> DeleteCampaignsAsync(DeleteCampaignsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteCampaignsResponse>("DeleteCampaigns", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateCampaignsResponse UpdateCampaigns(UpdateCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCampaignsResponse> UpdateCampaignsAsync(UpdateCampaignsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateCampaignsResponse>("UpdateCampaigns", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetNegativeSitesByCampaignIdsResponse GetNegativeSitesByCampaignIds(GetNegativeSitesByCampaignIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetNegativeSitesByCampaignIdsResponse> GetNegativeSitesByCampaignIdsAsync(GetNegativeSitesByCampaignIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetNegativeSitesByCampaignIdsResponse>("GetNegativeSitesByCampaignIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetNegativeSitesToCampaignsResponse SetNegativeSitesToCampaigns(SetNegativeSitesToCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetNegativeSitesToCampaignsResponse> SetNegativeSitesToCampaignsAsync(SetNegativeSitesToCampaignsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetNegativeSitesToCampaignsResponse>("SetNegativeSitesToCampaigns", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetConfigValueResponse GetConfigValue(GetConfigValueRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetConfigValueResponse> GetConfigValueAsync(GetConfigValueRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetConfigValueResponse>("GetConfigValue", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBSCCountriesResponse GetBSCCountries(GetBSCCountriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBSCCountriesResponse> GetBSCCountriesAsync(GetBSCCountriesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBSCCountriesResponse>("GetBSCCountries", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAdGroupsResponse AddAdGroups(AddAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAdGroupsResponse> AddAdGroupsAsync(AddAdGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAdGroupsResponse>("AddAdGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAdGroupsResponse DeleteAdGroups(DeleteAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAdGroupsResponse> DeleteAdGroupsAsync(DeleteAdGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAdGroupsResponse>("DeleteAdGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdGroupsByIdsResponse GetAdGroupsByIds(GetAdGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdGroupsByIdsResponse> GetAdGroupsByIdsAsync(GetAdGroupsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdGroupsByIdsResponse>("GetAdGroupsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdGroupsByCampaignIdResponse GetAdGroupsByCampaignId(GetAdGroupsByCampaignIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(GetAdGroupsByCampaignIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdGroupsByCampaignIdResponse>("GetAdGroupsByCampaignId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAdGroupsResponse UpdateAdGroups(UpdateAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAdGroupsResponse> UpdateAdGroupsAsync(UpdateAdGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAdGroupsResponse>("UpdateAdGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetNegativeSitesByAdGroupIdsResponse GetNegativeSitesByAdGroupIds(GetNegativeSitesByAdGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetNegativeSitesByAdGroupIdsResponse> GetNegativeSitesByAdGroupIdsAsync(GetNegativeSitesByAdGroupIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetNegativeSitesByAdGroupIdsResponse>("GetNegativeSitesByAdGroupIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetNegativeSitesToAdGroupsResponse SetNegativeSitesToAdGroups(SetNegativeSitesToAdGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetNegativeSitesToAdGroupsResponse> SetNegativeSitesToAdGroupsAsync(SetNegativeSitesToAdGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetNegativeSitesToAdGroupsResponse>("SetNegativeSitesToAdGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetGeoLocationsFileUrlResponse GetGeoLocationsFileUrl(GetGeoLocationsFileUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetGeoLocationsFileUrlResponse> GetGeoLocationsFileUrlAsync(GetGeoLocationsFileUrlRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetGeoLocationsFileUrlResponse>("GetGeoLocationsFileUrl", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAdsResponse AddAds(AddAdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAdsResponse> AddAdsAsync(AddAdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAdsResponse>("AddAds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAdsResponse DeleteAds(DeleteAdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAdsResponse> DeleteAdsAsync(DeleteAdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAdsResponse>("DeleteAds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdsByEditorialStatusResponse GetAdsByEditorialStatus(GetAdsByEditorialStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdsByEditorialStatusResponse> GetAdsByEditorialStatusAsync(GetAdsByEditorialStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdsByEditorialStatusResponse>("GetAdsByEditorialStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdsByIdsResponse GetAdsByIds(GetAdsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdsByIdsResponse> GetAdsByIdsAsync(GetAdsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdsByIdsResponse>("GetAdsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdsByAdGroupIdResponse GetAdsByAdGroupId(GetAdsByAdGroupIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(GetAdsByAdGroupIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdsByAdGroupIdResponse>("GetAdsByAdGroupId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAdsResponse UpdateAds(UpdateAdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAdsResponse> UpdateAdsAsync(UpdateAdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAdsResponse>("UpdateAds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddKeywordsResponse AddKeywords(AddKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddKeywordsResponse> AddKeywordsAsync(AddKeywordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddKeywordsResponse>("AddKeywords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteKeywordsResponse DeleteKeywords(DeleteKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteKeywordsResponse> DeleteKeywordsAsync(DeleteKeywordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteKeywordsResponse>("DeleteKeywords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordsByEditorialStatusResponse GetKeywordsByEditorialStatus(GetKeywordsByEditorialStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordsByEditorialStatusResponse> GetKeywordsByEditorialStatusAsync(GetKeywordsByEditorialStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordsByEditorialStatusResponse>("GetKeywordsByEditorialStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordsByIdsResponse GetKeywordsByIds(GetKeywordsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordsByIdsResponse> GetKeywordsByIdsAsync(GetKeywordsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordsByIdsResponse>("GetKeywordsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetKeywordsByAdGroupIdResponse GetKeywordsByAdGroupId(GetKeywordsByAdGroupIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetKeywordsByAdGroupIdResponse> GetKeywordsByAdGroupIdAsync(GetKeywordsByAdGroupIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetKeywordsByAdGroupIdResponse>("GetKeywordsByAdGroupId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateKeywordsResponse UpdateKeywords(UpdateKeywordsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateKeywordsResponse> UpdateKeywordsAsync(UpdateKeywordsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateKeywordsResponse>("UpdateKeywords", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AppealEditorialRejectionsResponse AppealEditorialRejections(AppealEditorialRejectionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AppealEditorialRejectionsResponse> AppealEditorialRejectionsAsync(AppealEditorialRejectionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AppealEditorialRejectionsResponse>("AppealEditorialRejections", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetEditorialReasonsByIdsResponse GetEditorialReasonsByIds(GetEditorialReasonsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetEditorialReasonsByIdsResponse> GetEditorialReasonsByIdsAsync(GetEditorialReasonsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetEditorialReasonsByIdsResponse>("GetEditorialReasonsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAccountMigrationStatusesResponse GetAccountMigrationStatuses(GetAccountMigrationStatusesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccountMigrationStatusesResponse> GetAccountMigrationStatusesAsync(GetAccountMigrationStatusesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccountMigrationStatusesResponse>("GetAccountMigrationStatuses", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetAccountPropertiesResponse SetAccountProperties(SetAccountPropertiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetAccountPropertiesResponse> SetAccountPropertiesAsync(SetAccountPropertiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetAccountPropertiesResponse>("SetAccountProperties", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAccountPropertiesResponse GetAccountProperties(GetAccountPropertiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAccountPropertiesResponse> GetAccountPropertiesAsync(GetAccountPropertiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAccountPropertiesResponse>("GetAccountProperties", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAdExtensionsResponse AddAdExtensions(AddAdExtensionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAdExtensionsResponse> AddAdExtensionsAsync(AddAdExtensionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAdExtensionsResponse>("AddAdExtensions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdExtensionsByIdsResponse GetAdExtensionsByIds(GetAdExtensionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(GetAdExtensionsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdExtensionsByIdsResponse>("GetAdExtensionsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAdExtensionsResponse UpdateAdExtensions(UpdateAdExtensionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAdExtensionsResponse> UpdateAdExtensionsAsync(UpdateAdExtensionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAdExtensionsResponse>("UpdateAdExtensions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAdExtensionsResponse DeleteAdExtensions(DeleteAdExtensionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAdExtensionsResponse> DeleteAdExtensionsAsync(DeleteAdExtensionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAdExtensionsResponse>("DeleteAdExtensions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdExtensionsEditorialReasonsResponse GetAdExtensionsEditorialReasons(GetAdExtensionsEditorialReasonsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdExtensionsEditorialReasonsResponse> GetAdExtensionsEditorialReasonsAsync(GetAdExtensionsEditorialReasonsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdExtensionsEditorialReasonsResponse>("GetAdExtensionsEditorialReasons", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetAdExtensionsAssociationsResponse SetAdExtensionsAssociations(SetAdExtensionsAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetAdExtensionsAssociationsResponse> SetAdExtensionsAssociationsAsync(SetAdExtensionsAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetAdExtensionsAssociationsResponse>("SetAdExtensionsAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdExtensionsAssociationsResponse GetAdExtensionsAssociations(GetAdExtensionsAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdExtensionsAssociationsResponse> GetAdExtensionsAssociationsAsync(GetAdExtensionsAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdExtensionsAssociationsResponse>("GetAdExtensionsAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAdExtensionsAssociationsResponse DeleteAdExtensionsAssociations(DeleteAdExtensionsAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAdExtensionsAssociationsResponse> DeleteAdExtensionsAssociationsAsync(DeleteAdExtensionsAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAdExtensionsAssociationsResponse>("DeleteAdExtensionsAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdExtensionIdsByAccountIdResponse GetAdExtensionIdsByAccountId(GetAdExtensionIdsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdExtensionIdsByAccountIdResponse> GetAdExtensionIdsByAccountIdAsync(GetAdExtensionIdsByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdExtensionIdsByAccountIdResponse>("GetAdExtensionIdsByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddMediaResponse AddMedia(AddMediaRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddMediaResponse> AddMediaAsync(AddMediaRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddMediaResponse>("AddMedia", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteMediaResponse DeleteMedia(DeleteMediaRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteMediaResponse> DeleteMediaAsync(DeleteMediaRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteMediaResponse>("DeleteMedia", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetMediaMetaDataByAccountIdResponse GetMediaMetaDataByAccountId(GetMediaMetaDataByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetMediaMetaDataByAccountIdResponse> GetMediaMetaDataByAccountIdAsync(GetMediaMetaDataByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetMediaMetaDataByAccountIdResponse>("GetMediaMetaDataByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetMediaMetaDataByIdsResponse GetMediaMetaDataByIds(GetMediaMetaDataByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetMediaMetaDataByIdsResponse> GetMediaMetaDataByIdsAsync(GetMediaMetaDataByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetMediaMetaDataByIdsResponse>("GetMediaMetaDataByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetMediaAssociationsResponse GetMediaAssociations(GetMediaAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetMediaAssociationsResponse> GetMediaAssociationsAsync(GetMediaAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetMediaAssociationsResponse>("GetMediaAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAdGroupCriterionsByIdsResponse GetAdGroupCriterionsByIds(GetAdGroupCriterionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAdGroupCriterionsByIdsResponse> GetAdGroupCriterionsByIdsAsync(GetAdGroupCriterionsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAdGroupCriterionsByIdsResponse>("GetAdGroupCriterionsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAdGroupCriterionsResponse AddAdGroupCriterions(AddAdGroupCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAdGroupCriterionsResponse> AddAdGroupCriterionsAsync(AddAdGroupCriterionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAdGroupCriterionsResponse>("AddAdGroupCriterions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAdGroupCriterionsResponse UpdateAdGroupCriterions(UpdateAdGroupCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAdGroupCriterionsResponse> UpdateAdGroupCriterionsAsync(UpdateAdGroupCriterionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAdGroupCriterionsResponse>("UpdateAdGroupCriterions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAdGroupCriterionsResponse DeleteAdGroupCriterions(DeleteAdGroupCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAdGroupCriterionsResponse> DeleteAdGroupCriterionsAsync(DeleteAdGroupCriterionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAdGroupCriterionsResponse>("DeleteAdGroupCriterions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyProductPartitionActionsResponse ApplyProductPartitionActions(ApplyProductPartitionActionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyProductPartitionActionsResponse> ApplyProductPartitionActionsAsync(ApplyProductPartitionActionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyProductPartitionActionsResponse>("ApplyProductPartitionActions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyHotelGroupActionsResponse ApplyHotelGroupActions(ApplyHotelGroupActionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyHotelGroupActionsResponse> ApplyHotelGroupActionsAsync(ApplyHotelGroupActionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyHotelGroupActionsResponse>("ApplyHotelGroupActions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyAssetGroupListingGroupActionsResponse ApplyAssetGroupListingGroupActions(ApplyAssetGroupListingGroupActionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyAssetGroupListingGroupActionsResponse> ApplyAssetGroupListingGroupActionsAsync(ApplyAssetGroupListingGroupActionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyAssetGroupListingGroupActionsResponse>("ApplyAssetGroupListingGroupActions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAssetGroupListingGroupsByIdsResponse GetAssetGroupListingGroupsByIds(GetAssetGroupListingGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAssetGroupListingGroupsByIdsResponse> GetAssetGroupListingGroupsByIdsAsync(GetAssetGroupListingGroupsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAssetGroupListingGroupsByIdsResponse>("GetAssetGroupListingGroupsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBMCStoresByCustomerIdResponse GetBMCStoresByCustomerId(GetBMCStoresByCustomerIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBMCStoresByCustomerIdResponse> GetBMCStoresByCustomerIdAsync(GetBMCStoresByCustomerIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBMCStoresByCustomerIdResponse>("GetBMCStoresByCustomerId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddNegativeKeywordsToEntitiesResponse AddNegativeKeywordsToEntities(AddNegativeKeywordsToEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(AddNegativeKeywordsToEntitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddNegativeKeywordsToEntitiesResponse>("AddNegativeKeywordsToEntities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetNegativeKeywordsByEntityIdsResponse GetNegativeKeywordsByEntityIds(GetNegativeKeywordsByEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetNegativeKeywordsByEntityIdsResponse> GetNegativeKeywordsByEntityIdsAsync(GetNegativeKeywordsByEntityIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetNegativeKeywordsByEntityIdsResponse>("GetNegativeKeywordsByEntityIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteNegativeKeywordsFromEntitiesResponse DeleteNegativeKeywordsFromEntities(DeleteNegativeKeywordsFromEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteNegativeKeywordsFromEntitiesResponse> DeleteNegativeKeywordsFromEntitiesAsync(DeleteNegativeKeywordsFromEntitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteNegativeKeywordsFromEntitiesResponse>("DeleteNegativeKeywordsFromEntities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSharedEntitiesByAccountIdResponse GetSharedEntitiesByAccountId(GetSharedEntitiesByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSharedEntitiesByAccountIdResponse> GetSharedEntitiesByAccountIdAsync(GetSharedEntitiesByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSharedEntitiesByAccountIdResponse>("GetSharedEntitiesByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSharedEntitiesResponse GetSharedEntities(GetSharedEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSharedEntitiesResponse> GetSharedEntitiesAsync(GetSharedEntitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSharedEntitiesResponse>("GetSharedEntities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddSharedEntityResponse AddSharedEntity(AddSharedEntityRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddSharedEntityResponse> AddSharedEntityAsync(AddSharedEntityRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddSharedEntityResponse>("AddSharedEntity", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetListItemsBySharedListResponse GetListItemsBySharedList(GetListItemsBySharedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetListItemsBySharedListResponse> GetListItemsBySharedListAsync(GetListItemsBySharedListRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetListItemsBySharedListResponse>("GetListItemsBySharedList", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddListItemsToSharedListResponse AddListItemsToSharedList(AddListItemsToSharedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddListItemsToSharedListResponse> AddListItemsToSharedListAsync(AddListItemsToSharedListRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddListItemsToSharedListResponse>("AddListItemsToSharedList", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateSharedEntitiesResponse UpdateSharedEntities(UpdateSharedEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateSharedEntitiesResponse> UpdateSharedEntitiesAsync(UpdateSharedEntitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateSharedEntitiesResponse>("UpdateSharedEntities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteListItemsFromSharedListResponse DeleteListItemsFromSharedList(DeleteListItemsFromSharedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteListItemsFromSharedListResponse> DeleteListItemsFromSharedListAsync(DeleteListItemsFromSharedListRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteListItemsFromSharedListResponse>("DeleteListItemsFromSharedList", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetSharedEntityAssociationsResponse SetSharedEntityAssociations(SetSharedEntityAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetSharedEntityAssociationsResponse> SetSharedEntityAssociationsAsync(SetSharedEntityAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetSharedEntityAssociationsResponse>("SetSharedEntityAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteSharedEntityAssociationsResponse DeleteSharedEntityAssociations(DeleteSharedEntityAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteSharedEntityAssociationsResponse> DeleteSharedEntityAssociationsAsync(DeleteSharedEntityAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteSharedEntityAssociationsResponse>("DeleteSharedEntityAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSharedEntityAssociationsBySharedEntityIdsResponse GetSharedEntityAssociationsBySharedEntityIds(GetSharedEntityAssociationsBySharedEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(GetSharedEntityAssociationsBySharedEntityIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSharedEntityAssociationsBySharedEntityIdsResponse>("GetSharedEntityAssociationsBySharedEntityIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSharedEntityAssociationsByEntityIdsResponse GetSharedEntityAssociationsByEntityIds(GetSharedEntityAssociationsByEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSharedEntityAssociationsByEntityIdsResponse> GetSharedEntityAssociationsByEntityIdsAsync(GetSharedEntityAssociationsByEntityIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSharedEntityAssociationsByEntityIdsResponse>("GetSharedEntityAssociationsByEntityIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteSharedEntitiesResponse DeleteSharedEntities(DeleteSharedEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteSharedEntitiesResponse> DeleteSharedEntitiesAsync(DeleteSharedEntitiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteSharedEntitiesResponse>("DeleteSharedEntities", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCampaignSizesByAccountIdResponse GetCampaignSizesByAccountId(GetCampaignSizesByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCampaignSizesByAccountIdResponse> GetCampaignSizesByAccountIdAsync(GetCampaignSizesByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCampaignSizesByAccountIdResponse>("GetCampaignSizesByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddCampaignCriterionsResponse AddCampaignCriterions(AddCampaignCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddCampaignCriterionsResponse> AddCampaignCriterionsAsync(AddCampaignCriterionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddCampaignCriterionsResponse>("AddCampaignCriterions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateCampaignCriterionsResponse UpdateCampaignCriterions(UpdateCampaignCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCampaignCriterionsResponse> UpdateCampaignCriterionsAsync(UpdateCampaignCriterionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateCampaignCriterionsResponse>("UpdateCampaignCriterions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteCampaignCriterionsResponse DeleteCampaignCriterions(DeleteCampaignCriterionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteCampaignCriterionsResponse> DeleteCampaignCriterionsAsync(DeleteCampaignCriterionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteCampaignCriterionsResponse>("DeleteCampaignCriterions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCampaignCriterionsByIdsResponse GetCampaignCriterionsByIds(GetCampaignCriterionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCampaignCriterionsByIdsResponse> GetCampaignCriterionsByIdsAsync(GetCampaignCriterionsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCampaignCriterionsByIdsResponse>("GetCampaignCriterionsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddBudgetsResponse AddBudgets(AddBudgetsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddBudgetsResponse> AddBudgetsAsync(AddBudgetsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddBudgetsResponse>("AddBudgets", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateBudgetsResponse UpdateBudgets(UpdateBudgetsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateBudgetsResponse> UpdateBudgetsAsync(UpdateBudgetsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateBudgetsResponse>("UpdateBudgets", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteBudgetsResponse DeleteBudgets(DeleteBudgetsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteBudgetsResponse> DeleteBudgetsAsync(DeleteBudgetsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteBudgetsResponse>("DeleteBudgets", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBudgetsByIdsResponse GetBudgetsByIds(GetBudgetsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(GetBudgetsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBudgetsByIdsResponse>("GetBudgetsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCampaignIdsByBudgetIdsResponse GetCampaignIdsByBudgetIds(GetCampaignIdsByBudgetIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCampaignIdsByBudgetIdsResponse> GetCampaignIdsByBudgetIdsAsync(GetCampaignIdsByBudgetIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCampaignIdsByBudgetIdsResponse>("GetCampaignIdsByBudgetIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddBidStrategiesResponse AddBidStrategies(AddBidStrategiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddBidStrategiesResponse> AddBidStrategiesAsync(AddBidStrategiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddBidStrategiesResponse>("AddBidStrategies", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateBidStrategiesResponse UpdateBidStrategies(UpdateBidStrategiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateBidStrategiesResponse> UpdateBidStrategiesAsync(UpdateBidStrategiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateBidStrategiesResponse>("UpdateBidStrategies", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteBidStrategiesResponse DeleteBidStrategies(DeleteBidStrategiesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteBidStrategiesResponse> DeleteBidStrategiesAsync(DeleteBidStrategiesRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteBidStrategiesResponse>("DeleteBidStrategies", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBidStrategiesByIdsResponse GetBidStrategiesByIds(GetBidStrategiesByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBidStrategiesByIdsResponse> GetBidStrategiesByIdsAsync(GetBidStrategiesByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBidStrategiesByIdsResponse>("GetBidStrategiesByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetCampaignIdsByBidStrategyIdsResponse GetCampaignIdsByBidStrategyIds(GetCampaignIdsByBidStrategyIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetCampaignIdsByBidStrategyIdsResponse> GetCampaignIdsByBidStrategyIdsAsync(GetCampaignIdsByBidStrategyIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetCampaignIdsByBidStrategyIdsResponse>("GetCampaignIdsByBidStrategyIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAudienceGroupsResponse AddAudienceGroups(AddAudienceGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAudienceGroupsResponse> AddAudienceGroupsAsync(AddAudienceGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAudienceGroupsResponse>("AddAudienceGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAudienceGroupsResponse UpdateAudienceGroups(UpdateAudienceGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAudienceGroupsResponse> UpdateAudienceGroupsAsync(UpdateAudienceGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAudienceGroupsResponse>("UpdateAudienceGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAudienceGroupsResponse DeleteAudienceGroups(DeleteAudienceGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAudienceGroupsResponse> DeleteAudienceGroupsAsync(DeleteAudienceGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAudienceGroupsResponse>("DeleteAudienceGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAudienceGroupsByIdsResponse GetAudienceGroupsByIds(GetAudienceGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAudienceGroupsByIdsResponse> GetAudienceGroupsByIdsAsync(GetAudienceGroupsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAudienceGroupsByIdsResponse>("GetAudienceGroupsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAssetGroupsResponse AddAssetGroups(AddAssetGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAssetGroupsResponse> AddAssetGroupsAsync(AddAssetGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAssetGroupsResponse>("AddAssetGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAssetGroupsResponse UpdateAssetGroups(UpdateAssetGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAssetGroupsResponse> UpdateAssetGroupsAsync(UpdateAssetGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAssetGroupsResponse>("UpdateAssetGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAssetGroupsResponse DeleteAssetGroups(DeleteAssetGroupsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAssetGroupsResponse> DeleteAssetGroupsAsync(DeleteAssetGroupsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAssetGroupsResponse>("DeleteAssetGroups", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAssetGroupsByIdsResponse GetAssetGroupsByIds(GetAssetGroupsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAssetGroupsByIdsResponse> GetAssetGroupsByIdsAsync(GetAssetGroupsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAssetGroupsByIdsResponse>("GetAssetGroupsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAssetGroupsByCampaignIdResponse GetAssetGroupsByCampaignId(GetAssetGroupsByCampaignIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAssetGroupsByCampaignIdResponse> GetAssetGroupsByCampaignIdAsync(GetAssetGroupsByCampaignIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAssetGroupsByCampaignIdResponse>("GetAssetGroupsByCampaignId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAssetGroupsEditorialReasonsResponse GetAssetGroupsEditorialReasons(GetAssetGroupsEditorialReasonsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAssetGroupsEditorialReasonsResponse> GetAssetGroupsEditorialReasonsAsync(GetAssetGroupsEditorialReasonsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAssetGroupsEditorialReasonsResponse>("GetAssetGroupsEditorialReasons", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetAudienceGroupAssetGroupAssociationsResponse SetAudienceGroupAssetGroupAssociations(SetAudienceGroupAssetGroupAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetAudienceGroupAssetGroupAssociationsResponse> SetAudienceGroupAssetGroupAssociationsAsync(SetAudienceGroupAssetGroupAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetAudienceGroupAssetGroupAssociationsResponse>("SetAudienceGroupAssetGroupAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAudienceGroupAssetGroupAssociationsResponse DeleteAudienceGroupAssetGroupAssociations(DeleteAudienceGroupAssetGroupAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAudienceGroupAssetGroupAssociationsResponse> DeleteAudienceGroupAssetGroupAssociationsAsync(DeleteAudienceGroupAssetGroupAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAudienceGroupAssetGroupAssociationsResponse>("DeleteAudienceGroupAssetGroupAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse GetAudienceGroupAssetGroupAssociationsByAssetGroupIds(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse> GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsAsync(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse>("GetAudienceGroupAssetGroupAssociationsByAssetGroupIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse GetAudienceGroupAssetGroupAssociationsByAudienceGroupIds(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse> GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsAsync(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse>("GetAudienceGroupAssetGroupAssociationsByAudienceGroupIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddAudiencesResponse AddAudiences(AddAudiencesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddAudiencesResponse> AddAudiencesAsync(AddAudiencesRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddAudiencesResponse>("AddAudiences", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAudiencesResponse UpdateAudiences(UpdateAudiencesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAudiencesResponse> UpdateAudiencesAsync(UpdateAudiencesRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAudiencesResponse>("UpdateAudiences", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteAudiencesResponse DeleteAudiences(DeleteAudiencesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteAudiencesResponse> DeleteAudiencesAsync(DeleteAudiencesRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteAudiencesResponse>("DeleteAudiences", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAudiencesByIdsResponse GetAudiencesByIds(GetAudiencesByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAudiencesByIdsResponse> GetAudiencesByIdsAsync(GetAudiencesByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAudiencesByIdsResponse>("GetAudiencesByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyCustomerListItemsResponse ApplyCustomerListItems(ApplyCustomerListItemsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyCustomerListItemsResponse> ApplyCustomerListItemsAsync(ApplyCustomerListItemsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyCustomerListItemsResponse>("ApplyCustomerListItems", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyCustomerListUserDataResponse ApplyCustomerListUserData(ApplyCustomerListUserDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyCustomerListUserDataResponse> ApplyCustomerListUserDataAsync(ApplyCustomerListUserDataRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyCustomerListUserDataResponse>("ApplyCustomerListUserData", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetUetTagsByIdsResponse GetUetTagsByIds(GetUetTagsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(GetUetTagsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetUetTagsByIdsResponse>("GetUetTagsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddUetTagsResponse AddUetTags(AddUetTagsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddUetTagsResponse> AddUetTagsAsync(AddUetTagsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddUetTagsResponse>("AddUetTags", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateUetTagsResponse UpdateUetTags(UpdateUetTagsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUetTagsResponse> UpdateUetTagsAsync(UpdateUetTagsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateUetTagsResponse>("UpdateUetTags", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetConversionGoalsByIdsResponse GetConversionGoalsByIds(GetConversionGoalsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(GetConversionGoalsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetConversionGoalsByIdsResponse>("GetConversionGoalsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetConversionGoalsByTagIdsResponse GetConversionGoalsByTagIds(GetConversionGoalsByTagIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetConversionGoalsByTagIdsResponse> GetConversionGoalsByTagIdsAsync(GetConversionGoalsByTagIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetConversionGoalsByTagIdsResponse>("GetConversionGoalsByTagIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddConversionGoalsResponse AddConversionGoals(AddConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddConversionGoalsResponse> AddConversionGoalsAsync(AddConversionGoalsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddConversionGoalsResponse>("AddConversionGoals", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateConversionGoalsResponse UpdateConversionGoals(UpdateConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateConversionGoalsResponse> UpdateConversionGoalsAsync(UpdateConversionGoalsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateConversionGoalsResponse>("UpdateConversionGoals", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyOfflineConversionsResponse ApplyOfflineConversions(ApplyOfflineConversionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyOfflineConversionsResponse> ApplyOfflineConversionsAsync(ApplyOfflineConversionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyOfflineConversionsResponse>("ApplyOfflineConversions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyOfflineConversionAdjustmentsResponse ApplyOfflineConversionAdjustments(ApplyOfflineConversionAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyOfflineConversionAdjustmentsResponse> ApplyOfflineConversionAdjustmentsAsync(ApplyOfflineConversionAdjustmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyOfflineConversionAdjustmentsResponse>("ApplyOfflineConversionAdjustments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public ApplyOnlineConversionAdjustmentsResponse ApplyOnlineConversionAdjustments(ApplyOnlineConversionAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApplyOnlineConversionAdjustmentsResponse> ApplyOnlineConversionAdjustmentsAsync(ApplyOnlineConversionAdjustmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<ApplyOnlineConversionAdjustmentsResponse>("ApplyOnlineConversionAdjustments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetOfflineConversionReportsResponse GetOfflineConversionReports(GetOfflineConversionReportsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetOfflineConversionReportsResponse> GetOfflineConversionReportsAsync(GetOfflineConversionReportsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetOfflineConversionReportsResponse>("GetOfflineConversionReports", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddLabelsResponse AddLabels(AddLabelsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddLabelsResponse> AddLabelsAsync(AddLabelsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddLabelsResponse>("AddLabels", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteLabelsResponse DeleteLabels(DeleteLabelsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteLabelsResponse> DeleteLabelsAsync(DeleteLabelsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteLabelsResponse>("DeleteLabels", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateLabelsResponse UpdateLabels(UpdateLabelsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateLabelsResponse> UpdateLabelsAsync(UpdateLabelsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateLabelsResponse>("UpdateLabels", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetLabelsByIdsResponse GetLabelsByIds(GetLabelsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetLabelsByIdsResponse> GetLabelsByIdsAsync(GetLabelsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetLabelsByIdsResponse>("GetLabelsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SetLabelAssociationsResponse SetLabelAssociations(SetLabelAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SetLabelAssociationsResponse> SetLabelAssociationsAsync(SetLabelAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<SetLabelAssociationsResponse>("SetLabelAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteLabelAssociationsResponse DeleteLabelAssociations(DeleteLabelAssociationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteLabelAssociationsResponse> DeleteLabelAssociationsAsync(DeleteLabelAssociationsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteLabelAssociationsResponse>("DeleteLabelAssociations", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetLabelAssociationsByEntityIdsResponse GetLabelAssociationsByEntityIds(GetLabelAssociationsByEntityIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetLabelAssociationsByEntityIdsResponse> GetLabelAssociationsByEntityIdsAsync(GetLabelAssociationsByEntityIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetLabelAssociationsByEntityIdsResponse>("GetLabelAssociationsByEntityIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetLabelAssociationsByLabelIdsResponse GetLabelAssociationsByLabelIds(GetLabelAssociationsByLabelIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetLabelAssociationsByLabelIdsResponse> GetLabelAssociationsByLabelIdsAsync(GetLabelAssociationsByLabelIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetLabelAssociationsByLabelIdsResponse>("GetLabelAssociationsByLabelIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddExperimentsResponse AddExperiments(AddExperimentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddExperimentsResponse> AddExperimentsAsync(AddExperimentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddExperimentsResponse>("AddExperiments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteExperimentsResponse DeleteExperiments(DeleteExperimentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteExperimentsResponse> DeleteExperimentsAsync(DeleteExperimentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteExperimentsResponse>("DeleteExperiments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateExperimentsResponse UpdateExperiments(UpdateExperimentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateExperimentsResponse> UpdateExperimentsAsync(UpdateExperimentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateExperimentsResponse>("UpdateExperiments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetExperimentsByIdsResponse GetExperimentsByIds(GetExperimentsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetExperimentsByIdsResponse> GetExperimentsByIdsAsync(GetExperimentsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetExperimentsByIdsResponse>("GetExperimentsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetProfileDataFileUrlResponse GetProfileDataFileUrl(GetProfileDataFileUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetProfileDataFileUrlResponse> GetProfileDataFileUrlAsync(GetProfileDataFileUrlRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetProfileDataFileUrlResponse>("GetProfileDataFileUrl", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public SearchCompaniesResponse SearchCompanies(SearchCompaniesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SearchCompaniesResponse> SearchCompaniesAsync(SearchCompaniesRequest request)
        {
            return _restServiceClient.CallServiceAsync<SearchCompaniesResponse>("SearchCompanies", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetFileImportUploadUrlResponse GetFileImportUploadUrl(GetFileImportUploadUrlRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetFileImportUploadUrlResponse> GetFileImportUploadUrlAsync(GetFileImportUploadUrlRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetFileImportUploadUrlResponse>("GetFileImportUploadUrl", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddImportJobsResponse AddImportJobs(AddImportJobsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddImportJobsResponse> AddImportJobsAsync(AddImportJobsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddImportJobsResponse>("AddImportJobs", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetImportResultsResponse GetImportResults(GetImportResultsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetImportResultsResponse> GetImportResultsAsync(GetImportResultsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetImportResultsResponse>("GetImportResults", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetImportJobsByIdsResponse GetImportJobsByIds(GetImportJobsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetImportJobsByIdsResponse> GetImportJobsByIdsAsync(GetImportJobsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetImportJobsByIdsResponse>("GetImportJobsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteImportJobsResponse DeleteImportJobs(DeleteImportJobsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteImportJobsResponse> DeleteImportJobsAsync(DeleteImportJobsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteImportJobsResponse>("DeleteImportJobs", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetImportEntityIdsMappingResponse GetImportEntityIdsMapping(GetImportEntityIdsMappingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetImportEntityIdsMappingResponse> GetImportEntityIdsMappingAsync(GetImportEntityIdsMappingRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetImportEntityIdsMappingResponse>("GetImportEntityIdsMapping", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateImportJobsResponse UpdateImportJobs(UpdateImportJobsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateImportJobsResponse> UpdateImportJobsAsync(UpdateImportJobsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateImportJobsResponse>("UpdateImportJobs", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddVideosResponse AddVideos(AddVideosRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddVideosResponse> AddVideosAsync(AddVideosRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddVideosResponse>("AddVideos", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteVideosResponse DeleteVideos(DeleteVideosRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteVideosResponse> DeleteVideosAsync(DeleteVideosRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteVideosResponse>("DeleteVideos", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetVideosByIdsResponse GetVideosByIds(GetVideosByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetVideosByIdsResponse> GetVideosByIdsAsync(GetVideosByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetVideosByIdsResponse>("GetVideosByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateVideosResponse UpdateVideos(UpdateVideosRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateVideosResponse> UpdateVideosAsync(UpdateVideosRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateVideosResponse>("UpdateVideos", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddHTML5sResponse AddHTML5s(AddHTML5sRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddHTML5sResponse> AddHTML5sAsync(AddHTML5sRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddHTML5sResponse>("AddHTML5s", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetHTML5sByIdsResponse GetHTML5sByIds(GetHTML5sByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetHTML5sByIdsResponse> GetHTML5sByIdsAsync(GetHTML5sByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetHTML5sByIdsResponse>("GetHTML5sByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteHTML5sResponse DeleteHTML5s(DeleteHTML5sRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteHTML5sResponse> DeleteHTML5sAsync(DeleteHTML5sRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteHTML5sResponse>("DeleteHTML5s", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddCampaignConversionGoalsResponse AddCampaignConversionGoals(AddCampaignConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddCampaignConversionGoalsResponse> AddCampaignConversionGoalsAsync(AddCampaignConversionGoalsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddCampaignConversionGoalsResponse>("AddCampaignConversionGoals", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteCampaignConversionGoalsResponse DeleteCampaignConversionGoals(DeleteCampaignConversionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteCampaignConversionGoalsResponse> DeleteCampaignConversionGoalsAsync(DeleteCampaignConversionGoalsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteCampaignConversionGoalsResponse>("DeleteCampaignConversionGoals", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddDataExclusionsResponse AddDataExclusions(AddDataExclusionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddDataExclusionsResponse> AddDataExclusionsAsync(AddDataExclusionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddDataExclusionsResponse>("AddDataExclusions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateDataExclusionsResponse UpdateDataExclusions(UpdateDataExclusionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateDataExclusionsResponse> UpdateDataExclusionsAsync(UpdateDataExclusionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateDataExclusionsResponse>("UpdateDataExclusions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteDataExclusionsResponse DeleteDataExclusions(DeleteDataExclusionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteDataExclusionsResponse> DeleteDataExclusionsAsync(DeleteDataExclusionsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteDataExclusionsResponse>("DeleteDataExclusions", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetDataExclusionsByIdsResponse GetDataExclusionsByIds(GetDataExclusionsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetDataExclusionsByIdsResponse> GetDataExclusionsByIdsAsync(GetDataExclusionsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetDataExclusionsByIdsResponse>("GetDataExclusionsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetDataExclusionsByAccountIdResponse GetDataExclusionsByAccountId(GetDataExclusionsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetDataExclusionsByAccountIdResponse> GetDataExclusionsByAccountIdAsync(GetDataExclusionsByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetDataExclusionsByAccountIdResponse>("GetDataExclusionsByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddSeasonalityAdjustmentsResponse AddSeasonalityAdjustments(AddSeasonalityAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddSeasonalityAdjustmentsResponse> AddSeasonalityAdjustmentsAsync(AddSeasonalityAdjustmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddSeasonalityAdjustmentsResponse>("AddSeasonalityAdjustments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateSeasonalityAdjustmentsResponse UpdateSeasonalityAdjustments(UpdateSeasonalityAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateSeasonalityAdjustmentsResponse> UpdateSeasonalityAdjustmentsAsync(UpdateSeasonalityAdjustmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateSeasonalityAdjustmentsResponse>("UpdateSeasonalityAdjustments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteSeasonalityAdjustmentsResponse DeleteSeasonalityAdjustments(DeleteSeasonalityAdjustmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteSeasonalityAdjustmentsResponse> DeleteSeasonalityAdjustmentsAsync(DeleteSeasonalityAdjustmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteSeasonalityAdjustmentsResponse>("DeleteSeasonalityAdjustments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSeasonalityAdjustmentsByIdsResponse GetSeasonalityAdjustmentsByIds(GetSeasonalityAdjustmentsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSeasonalityAdjustmentsByIdsResponse> GetSeasonalityAdjustmentsByIdsAsync(GetSeasonalityAdjustmentsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSeasonalityAdjustmentsByIdsResponse>("GetSeasonalityAdjustmentsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSeasonalityAdjustmentsByAccountIdResponse GetSeasonalityAdjustmentsByAccountId(GetSeasonalityAdjustmentsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSeasonalityAdjustmentsByAccountIdResponse> GetSeasonalityAdjustmentsByAccountIdAsync(GetSeasonalityAdjustmentsByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSeasonalityAdjustmentsByAccountIdResponse>("GetSeasonalityAdjustmentsByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public CreateAssetGroupRecommendationResponse CreateAssetGroupRecommendation(CreateAssetGroupRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CreateAssetGroupRecommendationResponse> CreateAssetGroupRecommendationAsync(CreateAssetGroupRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<CreateAssetGroupRecommendationResponse>("CreateAssetGroupRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public CreateResponsiveAdRecommendationResponse CreateResponsiveAdRecommendation(CreateResponsiveAdRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CreateResponsiveAdRecommendationResponse> CreateResponsiveAdRecommendationAsync(CreateResponsiveAdRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<CreateResponsiveAdRecommendationResponse>("CreateResponsiveAdRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public CreateResponsiveSearchAdRecommendationResponse CreateResponsiveSearchAdRecommendation(CreateResponsiveSearchAdRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CreateResponsiveSearchAdRecommendationResponse> CreateResponsiveSearchAdRecommendationAsync(CreateResponsiveSearchAdRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<CreateResponsiveSearchAdRecommendationResponse>("CreateResponsiveSearchAdRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public RefineAssetGroupRecommendationResponse RefineAssetGroupRecommendation(RefineAssetGroupRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RefineAssetGroupRecommendationResponse> RefineAssetGroupRecommendationAsync(RefineAssetGroupRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<RefineAssetGroupRecommendationResponse>("RefineAssetGroupRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public RefineResponsiveAdRecommendationResponse RefineResponsiveAdRecommendation(RefineResponsiveAdRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RefineResponsiveAdRecommendationResponse> RefineResponsiveAdRecommendationAsync(RefineResponsiveAdRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<RefineResponsiveAdRecommendationResponse>("RefineResponsiveAdRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public RefineResponsiveSearchAdRecommendationResponse RefineResponsiveSearchAdRecommendation(RefineResponsiveSearchAdRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RefineResponsiveSearchAdRecommendationResponse> RefineResponsiveSearchAdRecommendationAsync(RefineResponsiveSearchAdRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<RefineResponsiveSearchAdRecommendationResponse>("RefineResponsiveSearchAdRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetResponsiveAdRecommendationJobResponse GetResponsiveAdRecommendationJob(GetResponsiveAdRecommendationJobRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetResponsiveAdRecommendationJobResponse> GetResponsiveAdRecommendationJobAsync(GetResponsiveAdRecommendationJobRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetResponsiveAdRecommendationJobResponse>("GetResponsiveAdRecommendationJob", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateConversionValueRulesResponse UpdateConversionValueRules(UpdateConversionValueRulesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateConversionValueRulesResponse> UpdateConversionValueRulesAsync(UpdateConversionValueRulesRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateConversionValueRulesResponse>("UpdateConversionValueRules", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateConversionValueRulesStatusResponse UpdateConversionValueRulesStatus(UpdateConversionValueRulesStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateConversionValueRulesStatusResponse> UpdateConversionValueRulesStatusAsync(UpdateConversionValueRulesStatusRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateConversionValueRulesStatusResponse>("UpdateConversionValueRulesStatus", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddConversionValueRulesResponse AddConversionValueRules(AddConversionValueRulesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddConversionValueRulesResponse> AddConversionValueRulesAsync(AddConversionValueRulesRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddConversionValueRulesResponse>("AddConversionValueRules", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetConversionValueRulesByAccountIdResponse GetConversionValueRulesByAccountId(GetConversionValueRulesByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetConversionValueRulesByAccountIdResponse> GetConversionValueRulesByAccountIdAsync(GetConversionValueRulesByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetConversionValueRulesByAccountIdResponse>("GetConversionValueRulesByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetConversionValueRulesByIdsResponse GetConversionValueRulesByIds(GetConversionValueRulesByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetConversionValueRulesByIdsResponse> GetConversionValueRulesByIdsAsync(GetConversionValueRulesByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetConversionValueRulesByIdsResponse>("GetConversionValueRulesByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddBrandKitsResponse AddBrandKits(AddBrandKitsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddBrandKitsResponse> AddBrandKitsAsync(AddBrandKitsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddBrandKitsResponse>("AddBrandKits", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateBrandKitsResponse UpdateBrandKits(UpdateBrandKitsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateBrandKitsResponse> UpdateBrandKitsAsync(UpdateBrandKitsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateBrandKitsResponse>("UpdateBrandKits", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteBrandKitsResponse DeleteBrandKits(DeleteBrandKitsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteBrandKitsResponse> DeleteBrandKitsAsync(DeleteBrandKitsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteBrandKitsResponse>("DeleteBrandKits", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public CreateBrandKitRecommendationResponse CreateBrandKitRecommendation(CreateBrandKitRecommendationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CreateBrandKitRecommendationResponse> CreateBrandKitRecommendationAsync(CreateBrandKitRecommendationRequest request)
        {
            return _restServiceClient.CallServiceAsync<CreateBrandKitRecommendationResponse>("CreateBrandKitRecommendation", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddNewCustomerAcquisitionGoalsResponse AddNewCustomerAcquisitionGoals(AddNewCustomerAcquisitionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddNewCustomerAcquisitionGoalsResponse> AddNewCustomerAcquisitionGoalsAsync(AddNewCustomerAcquisitionGoalsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddNewCustomerAcquisitionGoalsResponse>("AddNewCustomerAcquisitionGoals", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateNewCustomerAcquisitionGoalsResponse UpdateNewCustomerAcquisitionGoals(UpdateNewCustomerAcquisitionGoalsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateNewCustomerAcquisitionGoalsResponse> UpdateNewCustomerAcquisitionGoalsAsync(UpdateNewCustomerAcquisitionGoalsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateNewCustomerAcquisitionGoalsResponse>("UpdateNewCustomerAcquisitionGoals", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetNewCustomerAcquisitionGoalsByAccountIdResponse GetNewCustomerAcquisitionGoalsByAccountId(GetNewCustomerAcquisitionGoalsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetNewCustomerAcquisitionGoalsByAccountIdResponse> GetNewCustomerAcquisitionGoalsByAccountIdAsync(GetNewCustomerAcquisitionGoalsByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetNewCustomerAcquisitionGoalsByAccountIdResponse>("GetNewCustomerAcquisitionGoalsByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBrandKitsByAccountIdResponse GetBrandKitsByAccountId(GetBrandKitsByAccountIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBrandKitsByAccountIdResponse> GetBrandKitsByAccountIdAsync(GetBrandKitsByAccountIdRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBrandKitsByAccountIdResponse>("GetBrandKitsByAccountId", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetBrandKitsByIdsResponse GetBrandKitsByIds(GetBrandKitsByIdsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetBrandKitsByIdsResponse> GetBrandKitsByIdsAsync(GetBrandKitsByIdsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetBrandKitsByIdsResponse>("GetBrandKitsByIds", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetClipchampTemplatesResponse GetClipchampTemplates(GetClipchampTemplatesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetClipchampTemplatesResponse> GetClipchampTemplatesAsync(GetClipchampTemplatesRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetClipchampTemplatesResponse>("GetClipchampTemplates", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSupportedClipchampAudioResponse GetSupportedClipchampAudio(GetSupportedClipchampAudioRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSupportedClipchampAudioResponse> GetSupportedClipchampAudioAsync(GetSupportedClipchampAudioRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSupportedClipchampAudioResponse>("GetSupportedClipchampAudio", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetSupportedFontsResponse GetSupportedFonts(GetSupportedFontsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetSupportedFontsResponse> GetSupportedFontsAsync(GetSupportedFontsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetSupportedFontsResponse>("GetSupportedFonts", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetHealthCheckResponse GetHealthCheck(GetHealthCheckRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetHealthCheckResponse> GetHealthCheckAsync(GetHealthCheckRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetHealthCheckResponse>("GetHealthCheck", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetDiagnosticsResponse GetDiagnostics(GetDiagnosticsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetDiagnosticsResponse> GetDiagnosticsAsync(GetDiagnosticsRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetDiagnosticsResponse>("GetDiagnostics", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public GetAnnotationOptOutResponse GetAnnotationOptOut(GetAnnotationOptOutRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetAnnotationOptOutResponse> GetAnnotationOptOutAsync(GetAnnotationOptOutRequest request)
        {
            return _restServiceClient.CallServiceAsync<GetAnnotationOptOutResponse>("GetAnnotationOptOut", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateAnnotationOptOutResponse UpdateAnnotationOptOut(UpdateAnnotationOptOutRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAnnotationOptOutResponse> UpdateAnnotationOptOutAsync(UpdateAnnotationOptOutRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateAnnotationOptOutResponse>("UpdateAnnotationOptOut", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public AddLinkedInSegmentsResponse AddLinkedInSegments(AddLinkedInSegmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AddLinkedInSegmentsResponse> AddLinkedInSegmentsAsync(AddLinkedInSegmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<AddLinkedInSegmentsResponse>("AddLinkedInSegments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public DeleteLinkedInSegmentsResponse DeleteLinkedInSegments(DeleteLinkedInSegmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteLinkedInSegmentsResponse> DeleteLinkedInSegmentsAsync(DeleteLinkedInSegmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<DeleteLinkedInSegmentsResponse>("DeleteLinkedInSegments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }

        public UpdateLinkedInSegmentsResponse UpdateLinkedInSegments(UpdateLinkedInSegmentsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateLinkedInSegmentsResponse> UpdateLinkedInSegmentsAsync(UpdateLinkedInSegmentsRequest request)
        {
            return _restServiceClient.CallServiceAsync<UpdateLinkedInSegmentsResponse>("UpdateLinkedInSegments", request, _serviceType, (r, t) => { r.TrackingId = t; });
        }
    }
}