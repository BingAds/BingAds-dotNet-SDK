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

#nullable enable

namespace Microsoft.BingAds.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.BingAds.V13.CustomerBilling;

public static partial class RestApiGeneration
{
    public class Microsoft_BingAds_V13_CustomerBilling_AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new Microsoft_BingAds_V13_CustomerBilling_ApiFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new Microsoft_BingAds_V13_CustomerBilling_ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
        }
    }

    class Microsoft_BingAds_V13_CustomerBilling_ApiFaultConverter : JsonConverter<ApiFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CustomerBilling_ApiFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ApiFault? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "ApiBatchFault" => jsonObj.Deserialize<ApiBatchFault>(options),
                "ApiFault" => jsonObj.Deserialize<ApiFault>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ApiFault value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case ApiBatchFault apiBatchFault:
                    JsonSerializer.Serialize(writer, apiBatchFault, options);
                    break;
                case ApiFault apiFault:
                    JsonSerializer.Serialize(writer, apiFault, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }

    class Microsoft_BingAds_V13_CustomerBilling_ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public Microsoft_BingAds_V13_CustomerBilling_ApplicationFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
        {
            _originalOptions = originalOptions;

            _createUnsupportedTypeValueException = createUnsupportedTypeValueException;
        }
        
        public override ApplicationFault? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            var jsonObj = JsonSerializer.Deserialize<JsonObject>(ref reader, options);

            var type = (string?)jsonObj!["Type"];

            return type switch
            {
                "AdApiFaultDetail" => jsonObj.Deserialize<AdApiFaultDetail>(options),
                "ApiBatchFault" => jsonObj.Deserialize<ApiBatchFault>(options),
                "ApiFault" => jsonObj.Deserialize<ApiFault>(options),
                "ApplicationFault" => jsonObj.Deserialize<ApplicationFault>(_originalOptions),
                _ => throw new JsonException(null, _createUnsupportedTypeValueException($"Unsupported Type value '{type}'"))
            };
        }

        public override void Write(Utf8JsonWriter writer, ApplicationFault value, JsonSerializerOptions options)
        {
            RuntimeHelpers.EnsureSufficientExecutionStack(); // Additional protection from any potential infinite recursion

            switch (value)
            {
                case AdApiFaultDetail adApiFaultDetail:
                    JsonSerializer.Serialize(writer, adApiFaultDetail, options);
                    break;
                case ApiBatchFault apiBatchFault:
                    JsonSerializer.Serialize(writer, apiBatchFault, options);
                    break;
                case ApiFault apiFault:
                    JsonSerializer.Serialize(writer, apiFault, options);
                    break;
                case ApplicationFault applicationFault:
                    JsonSerializer.Serialize(writer, applicationFault, _originalOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown type '{value.GetType().Name}'");
            }
        }
    }
}