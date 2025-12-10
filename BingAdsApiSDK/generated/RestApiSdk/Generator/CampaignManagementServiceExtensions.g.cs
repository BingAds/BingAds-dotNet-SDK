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

using System.Threading.Tasks;
using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds
{
    public static partial class ServiceClientExtensions
    {
        public static Task<AddCampaignsResponse> AddCampaignsAsync(this ServiceClient<ICampaignManagementService> service, AddCampaignsRequest request)
        {
            return service.CallAsync((s, r) => s.AddCampaignsAsync(r), request);
        }

        public static Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetCampaignsByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetCampaignsByAccountIdAsync(r), request);
        }

        public static Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetCampaignsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetCampaignsByIdsAsync(r), request);
        }

        public static Task<DeleteCampaignsResponse> DeleteCampaignsAsync(this ServiceClient<ICampaignManagementService> service, DeleteCampaignsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteCampaignsAsync(r), request);
        }

        public static Task<UpdateCampaignsResponse> UpdateCampaignsAsync(this ServiceClient<ICampaignManagementService> service, UpdateCampaignsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateCampaignsAsync(r), request);
        }

        public static Task<GetNegativeSitesByCampaignIdsResponse> GetNegativeSitesByCampaignIdsAsync(this ServiceClient<ICampaignManagementService> service, GetNegativeSitesByCampaignIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetNegativeSitesByCampaignIdsAsync(r), request);
        }

        public static Task<SetNegativeSitesToCampaignsResponse> SetNegativeSitesToCampaignsAsync(this ServiceClient<ICampaignManagementService> service, SetNegativeSitesToCampaignsRequest request)
        {
            return service.CallAsync((s, r) => s.SetNegativeSitesToCampaignsAsync(r), request);
        }

        public static Task<GetConfigValueResponse> GetConfigValueAsync(this ServiceClient<ICampaignManagementService> service, GetConfigValueRequest request)
        {
            return service.CallAsync((s, r) => s.GetConfigValueAsync(r), request);
        }

        public static Task<GetBSCCountriesResponse> GetBSCCountriesAsync(this ServiceClient<ICampaignManagementService> service, GetBSCCountriesRequest request)
        {
            return service.CallAsync((s, r) => s.GetBSCCountriesAsync(r), request);
        }

        public static Task<AddAdGroupsResponse> AddAdGroupsAsync(this ServiceClient<ICampaignManagementService> service, AddAdGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.AddAdGroupsAsync(r), request);
        }

        public static Task<DeleteAdGroupsResponse> DeleteAdGroupsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAdGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAdGroupsAsync(r), request);
        }

        public static Task<GetAdGroupsByIdsResponse> GetAdGroupsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAdGroupsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdGroupsByIdsAsync(r), request);
        }

        public static Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(this ServiceClient<ICampaignManagementService> service, GetAdGroupsByCampaignIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdGroupsByCampaignIdAsync(r), request);
        }

        public static Task<UpdateAdGroupsResponse> UpdateAdGroupsAsync(this ServiceClient<ICampaignManagementService> service, UpdateAdGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAdGroupsAsync(r), request);
        }

        public static Task<GetNegativeSitesByAdGroupIdsResponse> GetNegativeSitesByAdGroupIdsAsync(this ServiceClient<ICampaignManagementService> service, GetNegativeSitesByAdGroupIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetNegativeSitesByAdGroupIdsAsync(r), request);
        }

        public static Task<SetNegativeSitesToAdGroupsResponse> SetNegativeSitesToAdGroupsAsync(this ServiceClient<ICampaignManagementService> service, SetNegativeSitesToAdGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.SetNegativeSitesToAdGroupsAsync(r), request);
        }

        public static Task<GetGeoLocationsFileUrlResponse> GetGeoLocationsFileUrlAsync(this ServiceClient<ICampaignManagementService> service, GetGeoLocationsFileUrlRequest request)
        {
            return service.CallAsync((s, r) => s.GetGeoLocationsFileUrlAsync(r), request);
        }

        public static Task<AddAdsResponse> AddAdsAsync(this ServiceClient<ICampaignManagementService> service, AddAdsRequest request)
        {
            return service.CallAsync((s, r) => s.AddAdsAsync(r), request);
        }

        public static Task<DeleteAdsResponse> DeleteAdsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAdsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAdsAsync(r), request);
        }

        public static Task<GetAdsByEditorialStatusResponse> GetAdsByEditorialStatusAsync(this ServiceClient<ICampaignManagementService> service, GetAdsByEditorialStatusRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdsByEditorialStatusAsync(r), request);
        }

        public static Task<GetAdsByIdsResponse> GetAdsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAdsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdsByIdsAsync(r), request);
        }

        public static Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(this ServiceClient<ICampaignManagementService> service, GetAdsByAdGroupIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdsByAdGroupIdAsync(r), request);
        }

        public static Task<UpdateAdsResponse> UpdateAdsAsync(this ServiceClient<ICampaignManagementService> service, UpdateAdsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAdsAsync(r), request);
        }

        public static Task<AddKeywordsResponse> AddKeywordsAsync(this ServiceClient<ICampaignManagementService> service, AddKeywordsRequest request)
        {
            return service.CallAsync((s, r) => s.AddKeywordsAsync(r), request);
        }

        public static Task<DeleteKeywordsResponse> DeleteKeywordsAsync(this ServiceClient<ICampaignManagementService> service, DeleteKeywordsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteKeywordsAsync(r), request);
        }

        public static Task<GetKeywordsByEditorialStatusResponse> GetKeywordsByEditorialStatusAsync(this ServiceClient<ICampaignManagementService> service, GetKeywordsByEditorialStatusRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordsByEditorialStatusAsync(r), request);
        }

        public static Task<GetKeywordsByIdsResponse> GetKeywordsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetKeywordsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordsByIdsAsync(r), request);
        }

        public static Task<GetKeywordsByAdGroupIdResponse> GetKeywordsByAdGroupIdAsync(this ServiceClient<ICampaignManagementService> service, GetKeywordsByAdGroupIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetKeywordsByAdGroupIdAsync(r), request);
        }

        public static Task<UpdateKeywordsResponse> UpdateKeywordsAsync(this ServiceClient<ICampaignManagementService> service, UpdateKeywordsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateKeywordsAsync(r), request);
        }

        public static Task<AppealEditorialRejectionsResponse> AppealEditorialRejectionsAsync(this ServiceClient<ICampaignManagementService> service, AppealEditorialRejectionsRequest request)
        {
            return service.CallAsync((s, r) => s.AppealEditorialRejectionsAsync(r), request);
        }

        public static Task<GetEditorialReasonsByIdsResponse> GetEditorialReasonsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetEditorialReasonsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetEditorialReasonsByIdsAsync(r), request);
        }

        public static Task<GetAccountMigrationStatusesResponse> GetAccountMigrationStatusesAsync(this ServiceClient<ICampaignManagementService> service, GetAccountMigrationStatusesRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccountMigrationStatusesAsync(r), request);
        }

        public static Task<SetAccountPropertiesResponse> SetAccountPropertiesAsync(this ServiceClient<ICampaignManagementService> service, SetAccountPropertiesRequest request)
        {
            return service.CallAsync((s, r) => s.SetAccountPropertiesAsync(r), request);
        }

        public static Task<GetAccountPropertiesResponse> GetAccountPropertiesAsync(this ServiceClient<ICampaignManagementService> service, GetAccountPropertiesRequest request)
        {
            return service.CallAsync((s, r) => s.GetAccountPropertiesAsync(r), request);
        }

        public static Task<AddAdExtensionsResponse> AddAdExtensionsAsync(this ServiceClient<ICampaignManagementService> service, AddAdExtensionsRequest request)
        {
            return service.CallAsync((s, r) => s.AddAdExtensionsAsync(r), request);
        }

        public static Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAdExtensionsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdExtensionsByIdsAsync(r), request);
        }

        public static Task<UpdateAdExtensionsResponse> UpdateAdExtensionsAsync(this ServiceClient<ICampaignManagementService> service, UpdateAdExtensionsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAdExtensionsAsync(r), request);
        }

        public static Task<DeleteAdExtensionsResponse> DeleteAdExtensionsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAdExtensionsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAdExtensionsAsync(r), request);
        }

        public static Task<GetAdExtensionsEditorialReasonsResponse> GetAdExtensionsEditorialReasonsAsync(this ServiceClient<ICampaignManagementService> service, GetAdExtensionsEditorialReasonsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdExtensionsEditorialReasonsAsync(r), request);
        }

        public static Task<SetAdExtensionsAssociationsResponse> SetAdExtensionsAssociationsAsync(this ServiceClient<ICampaignManagementService> service, SetAdExtensionsAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.SetAdExtensionsAssociationsAsync(r), request);
        }

        public static Task<GetAdExtensionsAssociationsResponse> GetAdExtensionsAssociationsAsync(this ServiceClient<ICampaignManagementService> service, GetAdExtensionsAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdExtensionsAssociationsAsync(r), request);
        }

        public static Task<DeleteAdExtensionsAssociationsResponse> DeleteAdExtensionsAssociationsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAdExtensionsAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAdExtensionsAssociationsAsync(r), request);
        }

        public static Task<GetAdExtensionIdsByAccountIdResponse> GetAdExtensionIdsByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetAdExtensionIdsByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdExtensionIdsByAccountIdAsync(r), request);
        }

        public static Task<AddMediaResponse> AddMediaAsync(this ServiceClient<ICampaignManagementService> service, AddMediaRequest request)
        {
            return service.CallAsync((s, r) => s.AddMediaAsync(r), request);
        }

        public static Task<DeleteMediaResponse> DeleteMediaAsync(this ServiceClient<ICampaignManagementService> service, DeleteMediaRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteMediaAsync(r), request);
        }

        public static Task<GetMediaMetaDataByAccountIdResponse> GetMediaMetaDataByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetMediaMetaDataByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetMediaMetaDataByAccountIdAsync(r), request);
        }

        public static Task<GetMediaMetaDataByIdsResponse> GetMediaMetaDataByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetMediaMetaDataByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetMediaMetaDataByIdsAsync(r), request);
        }

        public static Task<GetMediaAssociationsResponse> GetMediaAssociationsAsync(this ServiceClient<ICampaignManagementService> service, GetMediaAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.GetMediaAssociationsAsync(r), request);
        }

        public static Task<GetAdGroupCriterionsByIdsResponse> GetAdGroupCriterionsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAdGroupCriterionsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAdGroupCriterionsByIdsAsync(r), request);
        }

        public static Task<AddAdGroupCriterionsResponse> AddAdGroupCriterionsAsync(this ServiceClient<ICampaignManagementService> service, AddAdGroupCriterionsRequest request)
        {
            return service.CallAsync((s, r) => s.AddAdGroupCriterionsAsync(r), request);
        }

        public static Task<UpdateAdGroupCriterionsResponse> UpdateAdGroupCriterionsAsync(this ServiceClient<ICampaignManagementService> service, UpdateAdGroupCriterionsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAdGroupCriterionsAsync(r), request);
        }

        public static Task<DeleteAdGroupCriterionsResponse> DeleteAdGroupCriterionsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAdGroupCriterionsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAdGroupCriterionsAsync(r), request);
        }

        public static Task<ApplyProductPartitionActionsResponse> ApplyProductPartitionActionsAsync(this ServiceClient<ICampaignManagementService> service, ApplyProductPartitionActionsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyProductPartitionActionsAsync(r), request);
        }

        public static Task<ApplyHotelGroupActionsResponse> ApplyHotelGroupActionsAsync(this ServiceClient<ICampaignManagementService> service, ApplyHotelGroupActionsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyHotelGroupActionsAsync(r), request);
        }

        public static Task<ApplyAssetGroupListingGroupActionsResponse> ApplyAssetGroupListingGroupActionsAsync(this ServiceClient<ICampaignManagementService> service, ApplyAssetGroupListingGroupActionsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyAssetGroupListingGroupActionsAsync(r), request);
        }

        public static Task<GetAssetGroupListingGroupsByIdsResponse> GetAssetGroupListingGroupsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAssetGroupListingGroupsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAssetGroupListingGroupsByIdsAsync(r), request);
        }

        public static Task<GetBMCStoresByCustomerIdResponse> GetBMCStoresByCustomerIdAsync(this ServiceClient<ICampaignManagementService> service, GetBMCStoresByCustomerIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetBMCStoresByCustomerIdAsync(r), request);
        }

        public static Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(this ServiceClient<ICampaignManagementService> service, AddNegativeKeywordsToEntitiesRequest request)
        {
            return service.CallAsync((s, r) => s.AddNegativeKeywordsToEntitiesAsync(r), request);
        }

        public static Task<GetNegativeKeywordsByEntityIdsResponse> GetNegativeKeywordsByEntityIdsAsync(this ServiceClient<ICampaignManagementService> service, GetNegativeKeywordsByEntityIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetNegativeKeywordsByEntityIdsAsync(r), request);
        }

        public static Task<DeleteNegativeKeywordsFromEntitiesResponse> DeleteNegativeKeywordsFromEntitiesAsync(this ServiceClient<ICampaignManagementService> service, DeleteNegativeKeywordsFromEntitiesRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteNegativeKeywordsFromEntitiesAsync(r), request);
        }

        public static Task<GetSharedEntitiesByAccountIdResponse> GetSharedEntitiesByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetSharedEntitiesByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetSharedEntitiesByAccountIdAsync(r), request);
        }

        public static Task<GetSharedEntitiesResponse> GetSharedEntitiesAsync(this ServiceClient<ICampaignManagementService> service, GetSharedEntitiesRequest request)
        {
            return service.CallAsync((s, r) => s.GetSharedEntitiesAsync(r), request);
        }

        public static Task<AddSharedEntityResponse> AddSharedEntityAsync(this ServiceClient<ICampaignManagementService> service, AddSharedEntityRequest request)
        {
            return service.CallAsync((s, r) => s.AddSharedEntityAsync(r), request);
        }

        public static Task<GetListItemsBySharedListResponse> GetListItemsBySharedListAsync(this ServiceClient<ICampaignManagementService> service, GetListItemsBySharedListRequest request)
        {
            return service.CallAsync((s, r) => s.GetListItemsBySharedListAsync(r), request);
        }

        public static Task<AddListItemsToSharedListResponse> AddListItemsToSharedListAsync(this ServiceClient<ICampaignManagementService> service, AddListItemsToSharedListRequest request)
        {
            return service.CallAsync((s, r) => s.AddListItemsToSharedListAsync(r), request);
        }

        public static Task<UpdateSharedEntitiesResponse> UpdateSharedEntitiesAsync(this ServiceClient<ICampaignManagementService> service, UpdateSharedEntitiesRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateSharedEntitiesAsync(r), request);
        }

        public static Task<DeleteListItemsFromSharedListResponse> DeleteListItemsFromSharedListAsync(this ServiceClient<ICampaignManagementService> service, DeleteListItemsFromSharedListRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteListItemsFromSharedListAsync(r), request);
        }

        public static Task<SetSharedEntityAssociationsResponse> SetSharedEntityAssociationsAsync(this ServiceClient<ICampaignManagementService> service, SetSharedEntityAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.SetSharedEntityAssociationsAsync(r), request);
        }

        public static Task<DeleteSharedEntityAssociationsResponse> DeleteSharedEntityAssociationsAsync(this ServiceClient<ICampaignManagementService> service, DeleteSharedEntityAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteSharedEntityAssociationsAsync(r), request);
        }

        public static Task<GetSharedEntityAssociationsBySharedEntityIdsResponse> GetSharedEntityAssociationsBySharedEntityIdsAsync(this ServiceClient<ICampaignManagementService> service, GetSharedEntityAssociationsBySharedEntityIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetSharedEntityAssociationsBySharedEntityIdsAsync(r), request);
        }

        public static Task<GetSharedEntityAssociationsByEntityIdsResponse> GetSharedEntityAssociationsByEntityIdsAsync(this ServiceClient<ICampaignManagementService> service, GetSharedEntityAssociationsByEntityIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetSharedEntityAssociationsByEntityIdsAsync(r), request);
        }

        public static Task<DeleteSharedEntitiesResponse> DeleteSharedEntitiesAsync(this ServiceClient<ICampaignManagementService> service, DeleteSharedEntitiesRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteSharedEntitiesAsync(r), request);
        }

        public static Task<GetCampaignSizesByAccountIdResponse> GetCampaignSizesByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetCampaignSizesByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetCampaignSizesByAccountIdAsync(r), request);
        }

        public static Task<AddCampaignCriterionsResponse> AddCampaignCriterionsAsync(this ServiceClient<ICampaignManagementService> service, AddCampaignCriterionsRequest request)
        {
            return service.CallAsync((s, r) => s.AddCampaignCriterionsAsync(r), request);
        }

        public static Task<UpdateCampaignCriterionsResponse> UpdateCampaignCriterionsAsync(this ServiceClient<ICampaignManagementService> service, UpdateCampaignCriterionsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateCampaignCriterionsAsync(r), request);
        }

        public static Task<DeleteCampaignCriterionsResponse> DeleteCampaignCriterionsAsync(this ServiceClient<ICampaignManagementService> service, DeleteCampaignCriterionsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteCampaignCriterionsAsync(r), request);
        }

        public static Task<GetCampaignCriterionsByIdsResponse> GetCampaignCriterionsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetCampaignCriterionsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetCampaignCriterionsByIdsAsync(r), request);
        }

        public static Task<AddBudgetsResponse> AddBudgetsAsync(this ServiceClient<ICampaignManagementService> service, AddBudgetsRequest request)
        {
            return service.CallAsync((s, r) => s.AddBudgetsAsync(r), request);
        }

        public static Task<UpdateBudgetsResponse> UpdateBudgetsAsync(this ServiceClient<ICampaignManagementService> service, UpdateBudgetsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateBudgetsAsync(r), request);
        }

        public static Task<DeleteBudgetsResponse> DeleteBudgetsAsync(this ServiceClient<ICampaignManagementService> service, DeleteBudgetsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteBudgetsAsync(r), request);
        }

        public static Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetBudgetsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBudgetsByIdsAsync(r), request);
        }

        public static Task<GetCampaignIdsByBudgetIdsResponse> GetCampaignIdsByBudgetIdsAsync(this ServiceClient<ICampaignManagementService> service, GetCampaignIdsByBudgetIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetCampaignIdsByBudgetIdsAsync(r), request);
        }

        public static Task<AddBidStrategiesResponse> AddBidStrategiesAsync(this ServiceClient<ICampaignManagementService> service, AddBidStrategiesRequest request)
        {
            return service.CallAsync((s, r) => s.AddBidStrategiesAsync(r), request);
        }

        public static Task<UpdateBidStrategiesResponse> UpdateBidStrategiesAsync(this ServiceClient<ICampaignManagementService> service, UpdateBidStrategiesRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateBidStrategiesAsync(r), request);
        }

        public static Task<DeleteBidStrategiesResponse> DeleteBidStrategiesAsync(this ServiceClient<ICampaignManagementService> service, DeleteBidStrategiesRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteBidStrategiesAsync(r), request);
        }

        public static Task<GetBidStrategiesByIdsResponse> GetBidStrategiesByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetBidStrategiesByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBidStrategiesByIdsAsync(r), request);
        }

        public static Task<GetCampaignIdsByBidStrategyIdsResponse> GetCampaignIdsByBidStrategyIdsAsync(this ServiceClient<ICampaignManagementService> service, GetCampaignIdsByBidStrategyIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetCampaignIdsByBidStrategyIdsAsync(r), request);
        }

        public static Task<AddAudienceGroupsResponse> AddAudienceGroupsAsync(this ServiceClient<ICampaignManagementService> service, AddAudienceGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.AddAudienceGroupsAsync(r), request);
        }

        public static Task<UpdateAudienceGroupsResponse> UpdateAudienceGroupsAsync(this ServiceClient<ICampaignManagementService> service, UpdateAudienceGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAudienceGroupsAsync(r), request);
        }

        public static Task<DeleteAudienceGroupsResponse> DeleteAudienceGroupsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAudienceGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAudienceGroupsAsync(r), request);
        }

        public static Task<GetAudienceGroupsByIdsResponse> GetAudienceGroupsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAudienceGroupsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAudienceGroupsByIdsAsync(r), request);
        }

        public static Task<AddAssetGroupsResponse> AddAssetGroupsAsync(this ServiceClient<ICampaignManagementService> service, AddAssetGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.AddAssetGroupsAsync(r), request);
        }

        public static Task<UpdateAssetGroupsResponse> UpdateAssetGroupsAsync(this ServiceClient<ICampaignManagementService> service, UpdateAssetGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAssetGroupsAsync(r), request);
        }

        public static Task<DeleteAssetGroupsResponse> DeleteAssetGroupsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAssetGroupsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAssetGroupsAsync(r), request);
        }

        public static Task<GetAssetGroupsByIdsResponse> GetAssetGroupsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAssetGroupsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAssetGroupsByIdsAsync(r), request);
        }

        public static Task<GetAssetGroupsByCampaignIdResponse> GetAssetGroupsByCampaignIdAsync(this ServiceClient<ICampaignManagementService> service, GetAssetGroupsByCampaignIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetAssetGroupsByCampaignIdAsync(r), request);
        }

        public static Task<GetAssetGroupsEditorialReasonsResponse> GetAssetGroupsEditorialReasonsAsync(this ServiceClient<ICampaignManagementService> service, GetAssetGroupsEditorialReasonsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAssetGroupsEditorialReasonsAsync(r), request);
        }

        public static Task<SetAudienceGroupAssetGroupAssociationsResponse> SetAudienceGroupAssetGroupAssociationsAsync(this ServiceClient<ICampaignManagementService> service, SetAudienceGroupAssetGroupAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.SetAudienceGroupAssetGroupAssociationsAsync(r), request);
        }

        public static Task<DeleteAudienceGroupAssetGroupAssociationsResponse> DeleteAudienceGroupAssetGroupAssociationsAsync(this ServiceClient<ICampaignManagementService> service, DeleteAudienceGroupAssetGroupAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAudienceGroupAssetGroupAssociationsAsync(r), request);
        }

        public static Task<GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsResponse> GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsAsync(r), request);
        }

        public static Task<GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsResponse> GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsAsync(r), request);
        }

        public static Task<AddAudiencesResponse> AddAudiencesAsync(this ServiceClient<ICampaignManagementService> service, AddAudiencesRequest request)
        {
            return service.CallAsync((s, r) => s.AddAudiencesAsync(r), request);
        }

        public static Task<UpdateAudiencesResponse> UpdateAudiencesAsync(this ServiceClient<ICampaignManagementService> service, UpdateAudiencesRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAudiencesAsync(r), request);
        }

        public static Task<DeleteAudiencesResponse> DeleteAudiencesAsync(this ServiceClient<ICampaignManagementService> service, DeleteAudiencesRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteAudiencesAsync(r), request);
        }

        public static Task<GetAudiencesByIdsResponse> GetAudiencesByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetAudiencesByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetAudiencesByIdsAsync(r), request);
        }

        public static Task<ApplyCustomerListItemsResponse> ApplyCustomerListItemsAsync(this ServiceClient<ICampaignManagementService> service, ApplyCustomerListItemsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyCustomerListItemsAsync(r), request);
        }

        public static Task<ApplyCustomerListUserDataResponse> ApplyCustomerListUserDataAsync(this ServiceClient<ICampaignManagementService> service, ApplyCustomerListUserDataRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyCustomerListUserDataAsync(r), request);
        }

        public static Task<GetUetTagsByIdsResponse> GetUetTagsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetUetTagsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetUetTagsByIdsAsync(r), request);
        }

        public static Task<AddUetTagsResponse> AddUetTagsAsync(this ServiceClient<ICampaignManagementService> service, AddUetTagsRequest request)
        {
            return service.CallAsync((s, r) => s.AddUetTagsAsync(r), request);
        }

        public static Task<UpdateUetTagsResponse> UpdateUetTagsAsync(this ServiceClient<ICampaignManagementService> service, UpdateUetTagsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateUetTagsAsync(r), request);
        }

        public static Task<GetConversionGoalsByIdsResponse> GetConversionGoalsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetConversionGoalsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetConversionGoalsByIdsAsync(r), request);
        }

        public static Task<GetConversionGoalsByTagIdsResponse> GetConversionGoalsByTagIdsAsync(this ServiceClient<ICampaignManagementService> service, GetConversionGoalsByTagIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetConversionGoalsByTagIdsAsync(r), request);
        }

        public static Task<AddConversionGoalsResponse> AddConversionGoalsAsync(this ServiceClient<ICampaignManagementService> service, AddConversionGoalsRequest request)
        {
            return service.CallAsync((s, r) => s.AddConversionGoalsAsync(r), request);
        }

        public static Task<UpdateConversionGoalsResponse> UpdateConversionGoalsAsync(this ServiceClient<ICampaignManagementService> service, UpdateConversionGoalsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateConversionGoalsAsync(r), request);
        }

        public static Task<ApplyOfflineConversionsResponse> ApplyOfflineConversionsAsync(this ServiceClient<ICampaignManagementService> service, ApplyOfflineConversionsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyOfflineConversionsAsync(r), request);
        }

        public static Task<ApplyOfflineConversionAdjustmentsResponse> ApplyOfflineConversionAdjustmentsAsync(this ServiceClient<ICampaignManagementService> service, ApplyOfflineConversionAdjustmentsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyOfflineConversionAdjustmentsAsync(r), request);
        }

        public static Task<ApplyOnlineConversionAdjustmentsResponse> ApplyOnlineConversionAdjustmentsAsync(this ServiceClient<ICampaignManagementService> service, ApplyOnlineConversionAdjustmentsRequest request)
        {
            return service.CallAsync((s, r) => s.ApplyOnlineConversionAdjustmentsAsync(r), request);
        }

        public static Task<GetOfflineConversionReportsResponse> GetOfflineConversionReportsAsync(this ServiceClient<ICampaignManagementService> service, GetOfflineConversionReportsRequest request)
        {
            return service.CallAsync((s, r) => s.GetOfflineConversionReportsAsync(r), request);
        }

        public static Task<AddLabelsResponse> AddLabelsAsync(this ServiceClient<ICampaignManagementService> service, AddLabelsRequest request)
        {
            return service.CallAsync((s, r) => s.AddLabelsAsync(r), request);
        }

        public static Task<DeleteLabelsResponse> DeleteLabelsAsync(this ServiceClient<ICampaignManagementService> service, DeleteLabelsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteLabelsAsync(r), request);
        }

        public static Task<UpdateLabelsResponse> UpdateLabelsAsync(this ServiceClient<ICampaignManagementService> service, UpdateLabelsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateLabelsAsync(r), request);
        }

        public static Task<GetLabelsByIdsResponse> GetLabelsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetLabelsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetLabelsByIdsAsync(r), request);
        }

        public static Task<SetLabelAssociationsResponse> SetLabelAssociationsAsync(this ServiceClient<ICampaignManagementService> service, SetLabelAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.SetLabelAssociationsAsync(r), request);
        }

        public static Task<DeleteLabelAssociationsResponse> DeleteLabelAssociationsAsync(this ServiceClient<ICampaignManagementService> service, DeleteLabelAssociationsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteLabelAssociationsAsync(r), request);
        }

        public static Task<GetLabelAssociationsByEntityIdsResponse> GetLabelAssociationsByEntityIdsAsync(this ServiceClient<ICampaignManagementService> service, GetLabelAssociationsByEntityIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetLabelAssociationsByEntityIdsAsync(r), request);
        }

        public static Task<GetLabelAssociationsByLabelIdsResponse> GetLabelAssociationsByLabelIdsAsync(this ServiceClient<ICampaignManagementService> service, GetLabelAssociationsByLabelIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetLabelAssociationsByLabelIdsAsync(r), request);
        }

        public static Task<AddExperimentsResponse> AddExperimentsAsync(this ServiceClient<ICampaignManagementService> service, AddExperimentsRequest request)
        {
            return service.CallAsync((s, r) => s.AddExperimentsAsync(r), request);
        }

        public static Task<DeleteExperimentsResponse> DeleteExperimentsAsync(this ServiceClient<ICampaignManagementService> service, DeleteExperimentsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteExperimentsAsync(r), request);
        }

        public static Task<UpdateExperimentsResponse> UpdateExperimentsAsync(this ServiceClient<ICampaignManagementService> service, UpdateExperimentsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateExperimentsAsync(r), request);
        }

        public static Task<GetExperimentsByIdsResponse> GetExperimentsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetExperimentsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetExperimentsByIdsAsync(r), request);
        }

        public static Task<GetProfileDataFileUrlResponse> GetProfileDataFileUrlAsync(this ServiceClient<ICampaignManagementService> service, GetProfileDataFileUrlRequest request)
        {
            return service.CallAsync((s, r) => s.GetProfileDataFileUrlAsync(r), request);
        }

        public static Task<SearchCompaniesResponse> SearchCompaniesAsync(this ServiceClient<ICampaignManagementService> service, SearchCompaniesRequest request)
        {
            return service.CallAsync((s, r) => s.SearchCompaniesAsync(r), request);
        }

        public static Task<GetFileImportUploadUrlResponse> GetFileImportUploadUrlAsync(this ServiceClient<ICampaignManagementService> service, GetFileImportUploadUrlRequest request)
        {
            return service.CallAsync((s, r) => s.GetFileImportUploadUrlAsync(r), request);
        }

        public static Task<AddImportJobsResponse> AddImportJobsAsync(this ServiceClient<ICampaignManagementService> service, AddImportJobsRequest request)
        {
            return service.CallAsync((s, r) => s.AddImportJobsAsync(r), request);
        }

        public static Task<GetImportResultsResponse> GetImportResultsAsync(this ServiceClient<ICampaignManagementService> service, GetImportResultsRequest request)
        {
            return service.CallAsync((s, r) => s.GetImportResultsAsync(r), request);
        }

        public static Task<GetImportJobsByIdsResponse> GetImportJobsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetImportJobsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetImportJobsByIdsAsync(r), request);
        }

        public static Task<DeleteImportJobsResponse> DeleteImportJobsAsync(this ServiceClient<ICampaignManagementService> service, DeleteImportJobsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteImportJobsAsync(r), request);
        }

        public static Task<GetImportEntityIdsMappingResponse> GetImportEntityIdsMappingAsync(this ServiceClient<ICampaignManagementService> service, GetImportEntityIdsMappingRequest request)
        {
            return service.CallAsync((s, r) => s.GetImportEntityIdsMappingAsync(r), request);
        }

        public static Task<UpdateImportJobsResponse> UpdateImportJobsAsync(this ServiceClient<ICampaignManagementService> service, UpdateImportJobsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateImportJobsAsync(r), request);
        }

        public static Task<AddVideosResponse> AddVideosAsync(this ServiceClient<ICampaignManagementService> service, AddVideosRequest request)
        {
            return service.CallAsync((s, r) => s.AddVideosAsync(r), request);
        }

        public static Task<DeleteVideosResponse> DeleteVideosAsync(this ServiceClient<ICampaignManagementService> service, DeleteVideosRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteVideosAsync(r), request);
        }

        public static Task<GetVideosByIdsResponse> GetVideosByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetVideosByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetVideosByIdsAsync(r), request);
        }

        public static Task<UpdateVideosResponse> UpdateVideosAsync(this ServiceClient<ICampaignManagementService> service, UpdateVideosRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateVideosAsync(r), request);
        }

        public static Task<AddHTML5sResponse> AddHTML5sAsync(this ServiceClient<ICampaignManagementService> service, AddHTML5sRequest request)
        {
            return service.CallAsync((s, r) => s.AddHTML5sAsync(r), request);
        }

        public static Task<GetHTML5sByIdsResponse> GetHTML5sByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetHTML5sByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetHTML5sByIdsAsync(r), request);
        }

        public static Task<DeleteHTML5sResponse> DeleteHTML5sAsync(this ServiceClient<ICampaignManagementService> service, DeleteHTML5sRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteHTML5sAsync(r), request);
        }

        public static Task<AddCampaignConversionGoalsResponse> AddCampaignConversionGoalsAsync(this ServiceClient<ICampaignManagementService> service, AddCampaignConversionGoalsRequest request)
        {
            return service.CallAsync((s, r) => s.AddCampaignConversionGoalsAsync(r), request);
        }

        public static Task<DeleteCampaignConversionGoalsResponse> DeleteCampaignConversionGoalsAsync(this ServiceClient<ICampaignManagementService> service, DeleteCampaignConversionGoalsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteCampaignConversionGoalsAsync(r), request);
        }

        public static Task<AddDataExclusionsResponse> AddDataExclusionsAsync(this ServiceClient<ICampaignManagementService> service, AddDataExclusionsRequest request)
        {
            return service.CallAsync((s, r) => s.AddDataExclusionsAsync(r), request);
        }

        public static Task<UpdateDataExclusionsResponse> UpdateDataExclusionsAsync(this ServiceClient<ICampaignManagementService> service, UpdateDataExclusionsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateDataExclusionsAsync(r), request);
        }

        public static Task<DeleteDataExclusionsResponse> DeleteDataExclusionsAsync(this ServiceClient<ICampaignManagementService> service, DeleteDataExclusionsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteDataExclusionsAsync(r), request);
        }

        public static Task<GetDataExclusionsByIdsResponse> GetDataExclusionsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetDataExclusionsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetDataExclusionsByIdsAsync(r), request);
        }

        public static Task<GetDataExclusionsByAccountIdResponse> GetDataExclusionsByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetDataExclusionsByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetDataExclusionsByAccountIdAsync(r), request);
        }

        public static Task<AddSeasonalityAdjustmentsResponse> AddSeasonalityAdjustmentsAsync(this ServiceClient<ICampaignManagementService> service, AddSeasonalityAdjustmentsRequest request)
        {
            return service.CallAsync((s, r) => s.AddSeasonalityAdjustmentsAsync(r), request);
        }

        public static Task<UpdateSeasonalityAdjustmentsResponse> UpdateSeasonalityAdjustmentsAsync(this ServiceClient<ICampaignManagementService> service, UpdateSeasonalityAdjustmentsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateSeasonalityAdjustmentsAsync(r), request);
        }

        public static Task<DeleteSeasonalityAdjustmentsResponse> DeleteSeasonalityAdjustmentsAsync(this ServiceClient<ICampaignManagementService> service, DeleteSeasonalityAdjustmentsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteSeasonalityAdjustmentsAsync(r), request);
        }

        public static Task<GetSeasonalityAdjustmentsByIdsResponse> GetSeasonalityAdjustmentsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetSeasonalityAdjustmentsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetSeasonalityAdjustmentsByIdsAsync(r), request);
        }

        public static Task<GetSeasonalityAdjustmentsByAccountIdResponse> GetSeasonalityAdjustmentsByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetSeasonalityAdjustmentsByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetSeasonalityAdjustmentsByAccountIdAsync(r), request);
        }

        public static Task<CreateAssetGroupRecommendationResponse> CreateAssetGroupRecommendationAsync(this ServiceClient<ICampaignManagementService> service, CreateAssetGroupRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.CreateAssetGroupRecommendationAsync(r), request);
        }

        public static Task<CreateResponsiveAdRecommendationResponse> CreateResponsiveAdRecommendationAsync(this ServiceClient<ICampaignManagementService> service, CreateResponsiveAdRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.CreateResponsiveAdRecommendationAsync(r), request);
        }

        public static Task<CreateResponsiveSearchAdRecommendationResponse> CreateResponsiveSearchAdRecommendationAsync(this ServiceClient<ICampaignManagementService> service, CreateResponsiveSearchAdRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.CreateResponsiveSearchAdRecommendationAsync(r), request);
        }

        public static Task<RefineAssetGroupRecommendationResponse> RefineAssetGroupRecommendationAsync(this ServiceClient<ICampaignManagementService> service, RefineAssetGroupRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.RefineAssetGroupRecommendationAsync(r), request);
        }

        public static Task<RefineResponsiveAdRecommendationResponse> RefineResponsiveAdRecommendationAsync(this ServiceClient<ICampaignManagementService> service, RefineResponsiveAdRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.RefineResponsiveAdRecommendationAsync(r), request);
        }

        public static Task<RefineResponsiveSearchAdRecommendationResponse> RefineResponsiveSearchAdRecommendationAsync(this ServiceClient<ICampaignManagementService> service, RefineResponsiveSearchAdRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.RefineResponsiveSearchAdRecommendationAsync(r), request);
        }

        public static Task<GetResponsiveAdRecommendationJobResponse> GetResponsiveAdRecommendationJobAsync(this ServiceClient<ICampaignManagementService> service, GetResponsiveAdRecommendationJobRequest request)
        {
            return service.CallAsync((s, r) => s.GetResponsiveAdRecommendationJobAsync(r), request);
        }

        public static Task<UpdateConversionValueRulesResponse> UpdateConversionValueRulesAsync(this ServiceClient<ICampaignManagementService> service, UpdateConversionValueRulesRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateConversionValueRulesAsync(r), request);
        }

        public static Task<UpdateConversionValueRulesStatusResponse> UpdateConversionValueRulesStatusAsync(this ServiceClient<ICampaignManagementService> service, UpdateConversionValueRulesStatusRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateConversionValueRulesStatusAsync(r), request);
        }

        public static Task<AddConversionValueRulesResponse> AddConversionValueRulesAsync(this ServiceClient<ICampaignManagementService> service, AddConversionValueRulesRequest request)
        {
            return service.CallAsync((s, r) => s.AddConversionValueRulesAsync(r), request);
        }

        public static Task<GetConversionValueRulesByAccountIdResponse> GetConversionValueRulesByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetConversionValueRulesByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetConversionValueRulesByAccountIdAsync(r), request);
        }

        public static Task<GetConversionValueRulesByIdsResponse> GetConversionValueRulesByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetConversionValueRulesByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetConversionValueRulesByIdsAsync(r), request);
        }

        public static Task<AddBrandKitsResponse> AddBrandKitsAsync(this ServiceClient<ICampaignManagementService> service, AddBrandKitsRequest request)
        {
            return service.CallAsync((s, r) => s.AddBrandKitsAsync(r), request);
        }

        public static Task<UpdateBrandKitsResponse> UpdateBrandKitsAsync(this ServiceClient<ICampaignManagementService> service, UpdateBrandKitsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateBrandKitsAsync(r), request);
        }

        public static Task<DeleteBrandKitsResponse> DeleteBrandKitsAsync(this ServiceClient<ICampaignManagementService> service, DeleteBrandKitsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteBrandKitsAsync(r), request);
        }

        public static Task<CreateBrandKitRecommendationResponse> CreateBrandKitRecommendationAsync(this ServiceClient<ICampaignManagementService> service, CreateBrandKitRecommendationRequest request)
        {
            return service.CallAsync((s, r) => s.CreateBrandKitRecommendationAsync(r), request);
        }

        public static Task<AddNewCustomerAcquisitionGoalsResponse> AddNewCustomerAcquisitionGoalsAsync(this ServiceClient<ICampaignManagementService> service, AddNewCustomerAcquisitionGoalsRequest request)
        {
            return service.CallAsync((s, r) => s.AddNewCustomerAcquisitionGoalsAsync(r), request);
        }

        public static Task<UpdateNewCustomerAcquisitionGoalsResponse> UpdateNewCustomerAcquisitionGoalsAsync(this ServiceClient<ICampaignManagementService> service, UpdateNewCustomerAcquisitionGoalsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateNewCustomerAcquisitionGoalsAsync(r), request);
        }

        public static Task<GetNewCustomerAcquisitionGoalsByAccountIdResponse> GetNewCustomerAcquisitionGoalsByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetNewCustomerAcquisitionGoalsByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetNewCustomerAcquisitionGoalsByAccountIdAsync(r), request);
        }

        public static Task<GetBrandKitsByAccountIdResponse> GetBrandKitsByAccountIdAsync(this ServiceClient<ICampaignManagementService> service, GetBrandKitsByAccountIdRequest request)
        {
            return service.CallAsync((s, r) => s.GetBrandKitsByAccountIdAsync(r), request);
        }

        public static Task<GetBrandKitsByIdsResponse> GetBrandKitsByIdsAsync(this ServiceClient<ICampaignManagementService> service, GetBrandKitsByIdsRequest request)
        {
            return service.CallAsync((s, r) => s.GetBrandKitsByIdsAsync(r), request);
        }

        public static Task<GetClipchampTemplatesResponse> GetClipchampTemplatesAsync(this ServiceClient<ICampaignManagementService> service, GetClipchampTemplatesRequest request)
        {
            return service.CallAsync((s, r) => s.GetClipchampTemplatesAsync(r), request);
        }

        public static Task<GetSupportedClipchampAudioResponse> GetSupportedClipchampAudioAsync(this ServiceClient<ICampaignManagementService> service, GetSupportedClipchampAudioRequest request)
        {
            return service.CallAsync((s, r) => s.GetSupportedClipchampAudioAsync(r), request);
        }

        public static Task<GetSupportedFontsResponse> GetSupportedFontsAsync(this ServiceClient<ICampaignManagementService> service, GetSupportedFontsRequest request)
        {
            return service.CallAsync((s, r) => s.GetSupportedFontsAsync(r), request);
        }

        public static Task<GetHealthCheckResponse> GetHealthCheckAsync(this ServiceClient<ICampaignManagementService> service, GetHealthCheckRequest request)
        {
            return service.CallAsync((s, r) => s.GetHealthCheckAsync(r), request);
        }

        public static Task<GetDiagnosticsResponse> GetDiagnosticsAsync(this ServiceClient<ICampaignManagementService> service, GetDiagnosticsRequest request)
        {
            return service.CallAsync((s, r) => s.GetDiagnosticsAsync(r), request);
        }

        public static Task<GetAnnotationOptOutResponse> GetAnnotationOptOutAsync(this ServiceClient<ICampaignManagementService> service, GetAnnotationOptOutRequest request)
        {
            return service.CallAsync((s, r) => s.GetAnnotationOptOutAsync(r), request);
        }

        public static Task<UpdateAnnotationOptOutResponse> UpdateAnnotationOptOutAsync(this ServiceClient<ICampaignManagementService> service, UpdateAnnotationOptOutRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateAnnotationOptOutAsync(r), request);
        }

        public static Task<AddLinkedInSegmentsResponse> AddLinkedInSegmentsAsync(this ServiceClient<ICampaignManagementService> service, AddLinkedInSegmentsRequest request)
        {
            return service.CallAsync((s, r) => s.AddLinkedInSegmentsAsync(r), request);
        }

        public static Task<DeleteLinkedInSegmentsResponse> DeleteLinkedInSegmentsAsync(this ServiceClient<ICampaignManagementService> service, DeleteLinkedInSegmentsRequest request)
        {
            return service.CallAsync((s, r) => s.DeleteLinkedInSegmentsAsync(r), request);
        }

        public static Task<UpdateLinkedInSegmentsResponse> UpdateLinkedInSegmentsAsync(this ServiceClient<ICampaignManagementService> service, UpdateLinkedInSegmentsRequest request)
        {
            return service.CallAsync((s, r) => s.UpdateLinkedInSegmentsAsync(r), request);
        }
    }
}