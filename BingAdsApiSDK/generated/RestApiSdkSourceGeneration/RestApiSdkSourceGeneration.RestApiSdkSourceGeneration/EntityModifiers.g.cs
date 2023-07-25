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

namespace Microsoft.BingAds.V13.CampaignManagement;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

public static class EntityModifiers
{
    private static void AddPrivateField(JsonTypeInfo jsonTypeInfo, Type containingType, string fieldName, string jsonName)
    {
        var field = containingType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(field.FieldType, jsonName);
        jsonPropertyInfo.Get = field.GetValue;
        jsonPropertyInfo.Set = field.SetValue;
        jsonTypeInfo.Properties.Add(jsonPropertyInfo);
    }

    public static void CustomizeEntities(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
            return;

        JsonPropertyInfo jsonPropertyInfo;

        if (jsonTypeInfo.Type == typeof(Campaign))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "GoalIds":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "MultimediaAdsBidAdjustment":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "AdScheduleUseSearcherTimeZone":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "BidStrategyId":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomParameters))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Setting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Setting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MaxClicksBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxClicks";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MaxConversionsBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "TargetCpa":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxConversions";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TargetCpaBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetCpa";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ManualCpcBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpc";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EnhancedCpcBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EnhancedCpc";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ManualCpvBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpv";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ManualCpmBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpm";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(InheritFromParentBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "InheritedBidStrategyType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "InheritFromParent";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TargetRoasBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetRoasBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MaxRoasBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxRoasBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MaxConversionValueBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MaxConversionValueBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TargetImpressionShareBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetImpressionShareBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PercentCpcBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PercentCpcBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CommissionBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CommissionBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ManualCpaBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ManualCpaBiddingScheme";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CostPerSaleBiddingScheme))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CostPerSale";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Bid))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomParameter))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ShoppingSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "FeedLabel":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ShoppableAdsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ShoppingSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DynamicFeedSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DynamicFeedSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DynamicSearchAdsSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "DynamicDescriptionEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DynamicSearchAdsSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TargetSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TargetSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CoOpSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CoOpSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(VerifiedTrackingSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VerifiedTrackingSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DisclaimerSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DisclaimerSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ResponsiveSearchAdsSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ResponsiveSearchAdsSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PerformanceMaxSetting))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PerformanceMaxSetting";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TargetSettingDetail))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BatchError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BatchError";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EditorialError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EditorialError";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ApplicationFault))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "ApplicationFault";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(ApiFaultDetail))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "ApiFaultDetail";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(EditorialApiFaultDetail))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "EditorialApiFaultDetail";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdApiFaultDetail))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), "Type");
            jsonPropertyInfo.Get = _ => "AdApiFaultDetail";            
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
        else if (jsonTypeInfo.Type == typeof(AdApiError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(OperationError))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CampaignNegativeSites))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdGroup))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "CommissionRate":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "FrequencyCapSettings":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "MultimediaAdsBidAdjustment":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "PercentCpcBid":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "UseOptimizedTargeting":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "AdScheduleUseSearcherTimeZone":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "AdGroupType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "CpvBid":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "CpmBid":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "McpaBid":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdRotation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RateBid))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RateBid";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Date))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FrequencyCapSettings))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CriterionBid))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CriterionBid";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FixedBid))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FixedBid";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BidMultiplier))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BidMultiplier";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RateAmount))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdGroupNegativeSites))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Ad))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TextAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.Text;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ProductAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.Product;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.Hotel;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AppInstallAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.AppInstall;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ExpandedTextAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.ExpandedText;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DynamicSearchAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.DynamicSearch;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ResponsiveAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "ImpressionTrackingUrls":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "LongHeadlines":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "Videos":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.ResponsiveAd;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ResponsiveSearchAd))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AdType.ResponsiveSearch;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AppUrl))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AssetLink))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Asset))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Asset";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(TextAsset))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "TextAsset";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImageAsset))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImageAsset";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(VideoAsset))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VideoAsset";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Keyword))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EntityIdToParentIdAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EditorialReasonCollection))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EditorialReason))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AccountMigrationStatusesInfo))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MigrationStatusInfo))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AccountProperty))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Schedule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(LocationAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LocationAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CallAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CallAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImageAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "DisplayText":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "Images":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "Layouts":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImageAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AppAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AppAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ReviewAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ReviewAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CalloutAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CalloutAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SitelinkAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SitelinkAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ActionAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ActionAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(StructuredSnippetAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "StructuredSnippetAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PriceAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PriceAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PromotionAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PromotionAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FilterLinkAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FilterLinkAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FlyerAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FlyerAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(VideoAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "VideoAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DisclaimerAdExtension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DisclaimerAdExtension";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DayTime))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Address))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GeoPoint))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PriceTableRow))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionIdentity))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BatchErrorCollection))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BatchErrorCollection";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EditorialErrorCollection))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "EditorialErrorCollection";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionIdToEntityIdAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionEditorialReasonCollection))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionEditorialReason))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionAssociationCollection))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdExtensionAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Media))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Media";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Image))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Image";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Paging))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MediaMetaData))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MediaRepresentation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "MediaRepresentation";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImageMediaRepresentation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImageMediaRepresentation";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(MediaAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdGroupCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AdGroupCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Criterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Criterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BiddableAdGroupCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "CriterionCashback":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BiddableAdGroupCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(NegativeAdGroupCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeAdGroupCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ProductPartition))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ProductPartition";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelGroup))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelGroup";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelAdvanceBookingWindowCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelAdvanceBookingWindowCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelCheckInDateCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelCheckInDateCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelCheckInDayCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelCheckInDayCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelDateSelectionTypeCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelDateSelectionTypeCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelLengthOfStayCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "HotelLengthOfStayCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ProductScope))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ProductScope";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Webpage))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Webpage";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AgeCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AgeCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DeviceCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DeviceCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DayTimeCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DayTimeCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GenderCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GenderCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RadiusCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RadiusCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(LocationCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LocationCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(LocationIntentCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "LocationIntentCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AudienceCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "AudienceCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ProfileCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ProfileCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(StoreCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "StoreCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DealCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "DealCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ProductCondition))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(HotelListing))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(WebpageParameter))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(WebpageCondition))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "Operator":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CriterionCashback))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CriterionCashback";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CashbackAdjustment))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CashbackAdjustment";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AdGroupCriterionAction))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AssetGroupListingGroupAction))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AssetGroupListingGroup))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BMCStore))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "StoreUrl":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EntityNegativeKeyword))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(NegativeKeyword))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeKeyword";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SharedListItem))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SharedListItem";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(NegativeSite))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeSite";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(IdCollection))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SharedEntity))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SharedEntity";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SharedList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "SharedList";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(NegativeKeywordList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeKeywordList";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PlacementExclusionList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PlacementExclusionList";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SharedEntityAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "SharedEntityCustomerId":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CampaignSize))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CampaignCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CampaignCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(NegativeCampaignCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "NegativeCampaignCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BiddableCampaignCriterion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "CriterionCashback":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "BiddableCampaignCriterion";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Budget))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(BidStrategy))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AudienceGroup))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AudienceGroupDimension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AgeDimension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Age;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GenderDimension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Gender;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AudienceDimension))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceGroupDimensionType.Audience;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AudienceInfo))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AssetGroup))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AudienceGroupAssetGroupAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Audience))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomerShare))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RemarketingList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.RemarketingList;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomAudience))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.Custom;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(InMarketAudience))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.InMarket;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ProductAudience))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.Product;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(SimilarRemarketingList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.SimilarRemarketingList;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CombinedList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.CombinedList;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomerList))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => AudienceType.CustomerList;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomerAccountShare))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomerAccountShareAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RemarketingRule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RemarketingRule";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PageVisitorsRule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "NormalForm":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PageVisitors";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PageVisitorsWhoVisitedAnotherPageRule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PageVisitorsWhoVisitedAnotherPage";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PageVisitorsWhoDidNotVisitAnotherPageRule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "PageVisitorsWhoDidNotVisitAnotherPage";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CustomEventsRule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "CustomEvents";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RuleItemGroup))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(RuleItem))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "RuleItem";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(StringRuleItem))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "String";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(NumberRuleItem))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "Number";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CombinationRule))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UetTag))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ConversionGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ConversionGoalRevenue))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(UrlGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.Url;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(DurationGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.Duration;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(PagesViewedPerVisitGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.PagesViewedPerVisit;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(EventGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.Event;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AppInstallGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.AppInstall;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(OfflineConversionGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "IsExternallyAttributed":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.OfflineConversion;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(InStoreTransactionGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AttributionModelType":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "GoalCategory":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "IsEnhancedConversionsEnabled":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "ViewThroughConversionWindowInMinutes":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => ConversionGoalType.InStoreTransaction;
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(OfflineConversion))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "HashedEmailAddress":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "HashedPhoneNumber":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(OfflineConversionAdjustment))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "HashedEmailAddress":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "HashedPhoneNumber":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(OnlineConversionAdjustment))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Label))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(LabelAssociation))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Experiment))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Company))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImportJob))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "NotificationEmail":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImportJob";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Frequency))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImportOption))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "ImportOption";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GoogleImportJob))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "NotificationEmail":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GoogleImportJob";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FileImportJob))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "NotificationEmail":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FileImportJob";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(GoogleImportOption))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "AdScheduleUseSearcherTimezone":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "AutoDeviceBidOptimization":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "NewImageAdExtensions":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "PauseAIMAdGroupIfAllAudienceCriterionNotImported":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "RenameCampaignNameWithSuffix":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "SearchAndReplaceForCustomParameters":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "SearchAndReplaceForFinalURLSuffix":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "UpdateAdUrls":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                     case "UpdateImageAdExtensions":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                    case "Type":
                        jsonPropertyInfo.Get = _ => "GoogleImportOption";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(FileImportOption))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "Type":
                        jsonPropertyInfo.Get = _ => "FileImportOption";
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImportSearchAndReplaceForStringProperty))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CampaignAdGroupIds))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                     case "ActiveAdGroupsOnly":
                         jsonPropertyInfo.ShouldSerialize = (_, value) => value != null;
                         break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImportResult))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(ImportEntityStatistics))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(Video))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(CampaignConversionGoal))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
                switch (jsonPropertyInfo.Name)
                {
                    case "ExtensionData":
                        jsonTypeInfo.Properties.RemoveAt(i);
                        break;
                }
            }
        }
        else if (jsonTypeInfo.Type == typeof(AddCampaignsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetCampaignsByAccountIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetCampaignsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteCampaignsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateCampaignsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetNegativeSitesByCampaignIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetNegativeSitesToCampaignsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetConfigValueRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetBSCCountriesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAdGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAdGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdGroupsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdGroupsByCampaignIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAdGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetNegativeSitesByAdGroupIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetNegativeSitesToAdGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetGeoLocationsFileUrlRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdsByEditorialStatusRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdsByAdGroupIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddKeywordsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteKeywordsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetKeywordsByEditorialStatusRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetKeywordsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetKeywordsByAdGroupIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateKeywordsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AppealEditorialRejectionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetEditorialReasonsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAccountMigrationStatusesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetAccountPropertiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAccountPropertiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAdExtensionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdExtensionsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAdExtensionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAdExtensionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdExtensionsEditorialReasonsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetAdExtensionsAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdExtensionsAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAdExtensionsAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdExtensionIdsByAccountIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddMediaRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteMediaRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetMediaMetaDataByAccountIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetMediaMetaDataByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetMediaAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAdGroupCriterionsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAdGroupCriterionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAdGroupCriterionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAdGroupCriterionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(ApplyProductPartitionActionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(ApplyHotelGroupActionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(ApplyAssetGroupListingGroupActionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAssetGroupListingGroupsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetBMCStoresByCustomerIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddNegativeKeywordsToEntitiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetNegativeKeywordsByEntityIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteNegativeKeywordsFromEntitiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetSharedEntitiesByAccountIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetSharedEntitiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddSharedEntityRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetListItemsBySharedListRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddListItemsToSharedListRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateSharedEntitiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteListItemsFromSharedListRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetSharedEntityAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteSharedEntityAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetSharedEntityAssociationsBySharedEntityIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetSharedEntityAssociationsByEntityIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteSharedEntitiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetCampaignSizesByAccountIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddCampaignCriterionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateCampaignCriterionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteCampaignCriterionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetCampaignCriterionsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddBudgetsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateBudgetsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteBudgetsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetBudgetsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetCampaignIdsByBudgetIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddBidStrategiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateBidStrategiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteBidStrategiesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetBidStrategiesByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetCampaignIdsByBidStrategyIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAudienceGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAudienceGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAudienceGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAudienceGroupsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAssetGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAssetGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAssetGroupsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAssetGroupsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAssetGroupsByCampaignIdRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetAudienceGroupAssetGroupAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAudienceGroupAssetGroupAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAudienceGroupAssetGroupAssociationsByAssetGroupIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAudienceGroupAssetGroupAssociationsByAudienceGroupIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddAudiencesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateAudiencesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteAudiencesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetAudiencesByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetUetTagsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddUetTagsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateUetTagsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetConversionGoalsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetConversionGoalsByTagIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddConversionGoalsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateConversionGoalsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(ApplyOfflineConversionsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(ApplyOfflineConversionAdjustmentsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(ApplyOnlineConversionAdjustmentsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddLabelsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteLabelsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateLabelsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetLabelsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SetLabelAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteLabelAssociationsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetLabelAssociationsByEntityIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetLabelAssociationsByLabelIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddExperimentsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteExperimentsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateExperimentsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetExperimentsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetProfileDataFileUrlRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(SearchCompaniesRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetFileImportUploadUrlRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddImportJobsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetImportResultsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetImportJobsByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteImportJobsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetImportEntityIdsMappingRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateImportJobsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddVideosRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteVideosRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(GetVideosByIdsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(UpdateVideosRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(AddCampaignConversionGoalsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
        else if (jsonTypeInfo.Type == typeof(DeleteCampaignConversionGoalsRequest))
        {
            for (int i = jsonTypeInfo.Properties.Count - 1; i >= 0; i--)
            {
                jsonPropertyInfo = jsonTypeInfo.Properties[i];
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
    }
}