#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.BingAds.V13.CustomerBilling
{
    public class AllPolymorphicConverters
    {
        public static void AddTo(JsonSerializerOptions options, Func<string, Exception> createUnsupportedTypeValueException)
        {
            var originalOptions = new JsonSerializerOptions(options);

            options.Converters.Add(new ApiFaultConverter(originalOptions, createUnsupportedTypeValueException));
            options.Converters.Add(new ApplicationFaultConverter(originalOptions, createUnsupportedTypeValueException));
        }
    }

    class ApiFaultConverter : JsonConverter<ApiFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public ApiFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
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

    class ApplicationFaultConverter : JsonConverter<ApplicationFault>
    {
        private readonly JsonSerializerOptions _originalOptions;

        private readonly Func<string, Exception> _createUnsupportedTypeValueException;

        public ApplicationFaultConverter(JsonSerializerOptions originalOptions, Func<string, Exception> createUnsupportedTypeValueException)
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