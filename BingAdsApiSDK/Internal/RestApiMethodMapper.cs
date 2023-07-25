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

using System.Collections.Generic;
using System;
using System.Net.Http;

#pragma warning disable CA1815 // Override equals and operator equals on value types
struct RestMethodInfo
#pragma warning restore CA1815 // Override equals and operator equals on value types
{
    public string EntityName { get; set; }

    public HttpMethod HttpMethod { get; set; }

    public string Action { get; set; }
}

internal static class RestApiMethodMapper
{
    public static readonly Dictionary<string, (string Entity, string Action)> CampaignManagementServiceActionMethods = new()
    {
        { "SetNegativeSitesToCampaigns", (Entity: "NegativeSites", Action: "SetToCampaigns" ) },
        { "SetNegativeSitesToAdGroups", (Entity: "NegativeSites", Action: "SetToAdGroups" ) },
        { "AppealEditorialRejections", (Entity: "EditorialRejections", Action: "Appeal" ) },
        { "SetAccountProperties", (Entity: "AccountProperties", Action: "Set" ) },
        { "SetAdExtensionsAssociations", (Entity: "AdExtensionsAssociations", Action: "Set" ) },
        { "ApplyProductPartitionActions", (Entity: "ProductPartitionActions", Action: "Apply" ) },
        { "ApplyHotelGroupActions", (Entity: "HotelGroupActions", Action: "Apply" ) },
        { "SetSharedEntityAssociations", (Entity: "SharedEntityAssociations", Action: "Set" ) },
        { "ApplyOfflineConversions", (Entity: "OfflineConversions", Action: "Apply" ) },
        { "ApplyOfflineConversionAdjustments", (Entity: "OfflineConversionAdjustments", Action: "Apply" ) },
        { "ApplyOnlineConversionAdjustments", (Entity: "OnlineConversionAdjustments", Action: "Apply" ) },
        { "SetLabelAssociations", (Entity: "LabelAssociations", Action: "Set" ) },
        { "SearchCompanies", (Entity: "Companies", Action: "Search" ) },
        { "SetAudienceGroupAssetGroupAssociations", (Entity: "AudienceGroupAssetGroupAssociations", Action: "Set" ) },
        { "ApplyAssetGroupListingGroupActions", (Entity: "AssetGroupListingGroupActions", Action: "Apply" ) },
        { "ApplyCustomerListItems", (Entity: "CustomerListItems", Action: "Apply" ) }
    };

    public static readonly Dictionary<string, (string Entity, string Action)> BulkServiceActionMethods = new()
    {
        { "UploadEntityRecords", (Entity: "EntityRecords", Action: "Upload") },
        { "DownloadCampaignsByAccountIds", (Entity: "Campaigns", Action: "DownloadByAccountIds") },
        { "DownloadCampaignsByCampaignIds", (Entity: "Campaigns", Action: "DownloadByCampaignIds") }
    };

    public static RestMethodInfo? Map(string methodName, Dictionary<string, (string Entity, string Action)> nonStandardMethods)
    {
        if (methodName == null)
        {
            throw new ArgumentNullException(nameof(methodName));
        }

        if (nonStandardMethods == null)
        {
            throw new ArgumentNullException(nameof(nonStandardMethods));
        }

        if (methodName.StartsWith("Add", StringComparison.InvariantCulture))
        {
            var entityName = methodName.Substring(3);

            return new RestMethodInfo { HttpMethod = HttpMethod.Post, EntityName = entityName };
        }

        if (methodName.StartsWith("Update", StringComparison.InvariantCulture))
        {
            var entityName = methodName.Substring(6);

            return new RestMethodInfo { HttpMethod = HttpMethod.Put, EntityName = entityName };
        }

        if (methodName.StartsWith("Delete", StringComparison.InvariantCulture))
        {
            var entityName = methodName.Substring(6);

            return new RestMethodInfo { HttpMethod = HttpMethod.Delete, EntityName = entityName };
        }

        if (methodName.StartsWith("Get", StringComparison.InvariantCulture))
        {
            var byIndex = methodName.IndexOf("By", StringComparison.InvariantCulture);

            string filterType;

            string entityName;

            if (byIndex < 0)
            {
                filterType = "";

                entityName = methodName.Substring(3);
            }
            else
            {
                filterType = "By" + methodName.Substring(byIndex + 2);

                entityName = methodName.Substring(3, byIndex - 3);
            }

            var action = "/Query" + filterType;

            return new RestMethodInfo { HttpMethod = HttpMethod.Post, EntityName = entityName, Action = action };
        }

        if (nonStandardMethods.TryGetValue(methodName, out var actionMethod))
        {
            return new RestMethodInfo { HttpMethod = HttpMethod.Post, EntityName = actionMethod.Entity, Action = actionMethod.Action };
        }

        return null;
    }
}
